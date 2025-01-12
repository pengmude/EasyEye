using HalconDotNet;
using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using NLog;
using SmartLib;
using Basler.Pylon;

namespace SmartVEye
{
    /// <summary>
    /// 海康相机
    /// </summary>
    public class CamBasler
    {
        //获取到的设备列表
        public Camera curCamera = null;
        //相机版本号
        private static Version cameraVersion = new Version(2, 0, 0);

        //相机拍得的照片
        private HObject attachedImage = null;

        private IntPtr frameMemoryAddress = IntPtr.Zero;
        private PixelDataConverter converter = new PixelDataConverter();


        //是否正在采集
        IntPtr pImgTemp = IntPtr.Zero;
        private object snapLock = new object();//拍照锁
        private object bufferLock = new object();//内存图像读取锁

        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        /// 相机参数
        public long imageWidth = 0;         // 图像宽
        public long imageHeight = 0;        // 图像高
        public long minExposureTime = 0;    // 最小曝光时间
        public long maxExposureTime = 0;    // 最大曝光时间
        public long minGain = 0;            // 最小增益
        public long maxGain = 0;            // 最大增益

        /// <summary>
        /// 相机返回图像
        /// </summary>
        public delegate void PostFrameEventHandler(HObject grabImage);
        public event PostFrameEventHandler PostFrameEvent;
        public void PostFrame(HObject grabImage)
        {
            if (PostFrameEvent != null)
            {
                PostFrameEvent(grabImage);
            }
        }

        /// <summary>
        /// 相机已经打开
        /// </summary>
        public bool IsOpen = false;

        /// <summary>
        /// 关闭相机
        /// </summary>
        public Response CamClose()
        {
            try
            {
                curCamera.Close();
                curCamera.Dispose();

                if (attachedImage != null)
                {
                    attachedImage.Dispose();
                }

                if (frameMemoryAddress != null)
                {
                    Marshal.FreeHGlobal(frameMemoryAddress);
                    frameMemoryAddress = IntPtr.Zero;
                }
                return Response.Ok();

            }
            catch (Exception ex)
            {
                return Response.Fail("错误0xAM004," + ex.Message);
            }
        }
        /// <summary>
        /// 销毁相机
        /// </summary>
        public Response CamFree()
        {
            try
            {
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("错误0xAM005," + ex.Message);
            }
        }
        /// <summary>
        /// 打开相机
        /// </summary>
        public Response CamOpen(string camName)
        {
            try
            {
                //获取当前连接的所有相机
                List<ICameraInfo> allCameraInfos = CameraFinder.Enumerate();
                foreach (ICameraInfo iCameraInfo in allCameraInfos)
                {
                    if (camName == iCameraInfo[CameraInfoKey.UserDefinedName])
                    {
                        curCamera = new Camera(iCameraInfo);
                        break;
                    }
                }
                if (curCamera == null)
                {
                    return Response.Fail($"未找到名称为[{camName}]的相机");
                }
                curCamera.CameraOpened += Configuration.AcquireContinuous;
                curCamera.Open();

                imageWidth = curCamera.Parameters[PLCamera.Width].GetValue();               // 获取图像宽 
                imageHeight = curCamera.Parameters[PLCamera.Height].GetValue();              // 获取图像高
                curCamera.StreamGrabber.ImageGrabbed += OnImageGrabbed;                      // 注册采集回调函数
                if (!curCamera.IsOpen) return Response.Fail("相机打开失败!");
                return Response.Ok();
            }
            catch (Exception ex)
            {
                IsOpen = false;
                return Response.Fail("错误0xAM001," + ex.Message);
            }
        }
        /// <summary>
        /// 获取曝光时间
        /// </summary>
        public Response<float> GetExposTime()
        {
            if (curCamera != null)
            {
                float ExposureTime = 100;
                if (curCamera.GetSfncVersion() < cameraVersion)
                {
                    //minExposureTime = curCamera.Parameters[PLCamera.ExposureTimeRaw].GetMinimum();
                    //maxExposureTime = curCamera.Parameters[PLCamera.ExposureTimeRaw].GetMaximum();
                    ExposureTime = curCamera.Parameters[PLCamera.ExposureTimeRaw].GetValue();
                }
                else
                {
                    //minExposureTime = (long)curCamera.Parameters[PLUsbCamera.ExposureTime].GetMinimum();
                    //maxExposureTime = (long)curCamera.Parameters[PLUsbCamera.ExposureTime].GetMaximum();
                    ExposureTime = (float)curCamera.Parameters[PLUsbCamera.ExposureTime].GetValue();
                }
                return Response<float>.Ok(ExposureTime);
            }
            else
            {
                return Response<float>.Fail("相机曝光时间获取失败,相机为空!");
            }
        }

        public Response<double> GetTriggerDelay()
        {
            if (curCamera != null)
            {
                double triggerDelay = curCamera.Parameters[PLCamera.TriggerDelay].GetValue();
                return Response<double>.Ok(triggerDelay);
            }
            else
            {
                return Response<double>.Fail("相机延迟触发时间获取失败,相机为空!");
            }
        }

        /// <summary>
        /// 设置触发延迟，单位us
        /// </summary>
        /// <param name="time">单位us</param>
        public Response SetExposTime(double value)
        {
            try
            {
                if (curCamera != null)
                {
                    //单位：微妙
                    curCamera.Parameters[PLUsbCamera.TriggerDelay].SetValue(value);
                    return Response.Ok();
                }
                else
                {
                    return Response.Fail("设置相机触发延迟时间失败,相机为空!");
                }
            }
            catch (Exception ex)
            {
                return Response.Fail("设置相机曝光时间失败!" + ex.Message);
            }
        }

        /// <summary>
        /// 获取增益值
        /// </summary>
        public Response<float> GetGain()
        {
            if (curCamera != null)
            {
                float GainRaw = 0f;
                if (curCamera.GetSfncVersion() < cameraVersion)
                {
                    // minGain = curCamera.Parameters[PLCamera.GainRaw].GetMinimum();
                    //maxGain = curCamera.Parameters[PLCamera.GainRaw].GetMaximum();
                    GainRaw = curCamera.Parameters[PLCamera.GainRaw].GetValue();
                }
                else
                {
                    //minGain = (long)curCamera.Parameters[PLUsbCamera.Gain].GetMinimum();
                    // maxGain = (long)curCamera.Parameters[PLUsbCamera.Gain].GetMaximum();
                    GainRaw = curCamera.Parameters[PLCamera.GainRaw].GetValue();
                }
                return Response<float>.Ok(GainRaw);
            }
            else
            {
                return Response<float>.Fail("相机增益获取失败,相机为空!");
            }
        }

        /// <summary>
        /// 设置曝光时间
        /// </summary>
        public Response SetExposTime(float value)
        {
            try
            {
                if (curCamera != null)
                {
                    curCamera.Parameters[PLCamera.ExposureAuto].TrySetValue(PLCamera.ExposureAuto.Off);
                    curCamera.Parameters[PLCamera.ExposureMode].TrySetValue(PLCamera.ExposureMode.Timed);

                    if (curCamera.GetSfncVersion() < cameraVersion)
                    {
                        long min = curCamera.Parameters[PLCamera.ExposureTimeRaw].GetMinimum();
                        long max = curCamera.Parameters[PLCamera.ExposureTimeRaw].GetMaximum();
                        long incr = curCamera.Parameters[PLCamera.ExposureTimeRaw].GetIncrement();
                        if (value < min)
                        {
                            value = min;
                        }
                        else if (value > max)
                        {
                            value = max;
                        }
                        else
                        {
                            value = min + (((value - min) / incr) * incr);
                        }
                        curCamera.Parameters[PLCamera.ExposureTimeRaw].SetValue((long)value);
                    }
                    else
                    {
                        //单位：微妙
                        curCamera.Parameters[PLUsbCamera.ExposureTime].SetValue((double)value);
                    }
                    return Response.Ok();
                }
                else
                {
                    return Response.Fail("设置相机曝光时间失败,相机为空!");
                }
            }
            catch (Exception ex)
            {
                return Response.Fail("设置相机曝光时间失败!" + ex.Message);
            }
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        public Response SetGain(float val)
        {
            if (curCamera != null)
            {
                curCamera.Parameters[PLCamera.GainAuto].TrySetValue(PLCamera.GainAuto.Off);
                curCamera.Parameters[PLCamera.GainRaw].SetValue((long)val);
                return Response.Ok();
            }
            else
            {
                return Response.Fail("设置相机增益失败,相机为空!");
            }
        }
        /// <summary>
        /// 触发一次拍照
        /// </summary>
        public Response SnapOneImg()
        {
            lock (snapLock)
            {
                if (curCamera != null)
                {
                    string aa = curCamera.Parameters[PLCamera.AcquisitionMode].GetValue();
                    string cc = curCamera.Parameters[PLCamera.TriggerMode].GetValue();
                    string bb = curCamera.Parameters[PLCamera.TriggerSource].GetValue();
                    if (curCamera.WaitForFrameTriggerReady(1000, TimeoutHandling.ThrowException))
                    {
                        curCamera.ExecuteSoftwareTrigger();
                        //ReadBuffer();
                    }
                    return Response.Ok();
                }
                else
                {
                    return Response.Fail("相机触发失败,相机为空或不为软触发模式!");
                }
            }
        }

        /// <summary>
        /// 设置硬触发模式
        /// </summary>
        public Response SetLineTrig()
        {
            if (curCamera != null)
            {

                curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                var nRet = curCamera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                if (!nRet) return Response.Fail("设置相机触发模式失败!");
                nRet = curCamera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                curCamera.Parameters[PLCamera.LineMode].TrySetValue(PLCamera.LineMode.Input);
                if (!nRet) return Response.Fail("设置相机软触发模式失败!");
                //设置上升沿触发
                //curCamera.Parameters[PLCamera.TriggerActivation].SetValue(PLCamera.TriggerActivation.LevelHigh);
                curCamera.Parameters[PLCamera.AcquisitionMode].TrySetValue(PLCamera.AcquisitionMode.Continuous);
                return Response.Ok();
            }
            else
            {
                return Response.Fail("设置相机硬触发失败,相机为空!");
            }
        }

        /// <summary>
        /// 设置软触发模式
        /// </summary>
        public Response SetSoftTrig()
        {
            if (curCamera != null)
            {
                curCamera.Parameters[PLCamera.AcquisitionMode].TrySetValue(PLCamera.AcquisitionMode.Continuous);
                curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                var nRet = curCamera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                if (!nRet) return Response.Fail("设置相机触发模式失败!");
                nRet = curCamera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                if (!nRet) return Response.Fail("设置相机软触发模式失败!");

                //curCamera.Parameters[PLCamera.LineSelector].TrySetValue(PLCamera.LineSelector.Out1);
                //curCamera.Parameters[PLCamera.LineMode].TrySetValue(PLCamera.LineMode.Output);
                //curCamera.Parameters[PLCamera.LineSource].TrySetValue(PLCamera.LineSource.ExposureActive);

                //if (curCamera.GetSfncVersion() < cameraVersion)
                //{
                //    if (curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart))
                //    {
                //        if (curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                //        {
                //            curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                //            curCamera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                //            curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                //            curCamera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                //            curCamera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                //        }
                //        else
                //        {
                //            curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                //            curCamera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                //            curCamera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                //        }
                //    }
                //}
                //else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                //{
                //    if (curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart))
                //    {
                //        if (curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                //        {
                //            curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                //            curCamera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                //            curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                //            curCamera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                //            curCamera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                //        }
                //        else
                //        {
                //            curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                //            curCamera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                //            curCamera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                //        }
                //    }
                //}
                return Response.Ok();
            }
            else
            {
                return Response.Fail("设置相机软触发模式失败,相机为空!");
            }
        }

        /// <summary>
        /// 开启采集流
        /// </summary>
        public Response StartGrab()
        {
            try
            {
                curCamera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("开始采集失败！" + ex.Message);
            }
        }
        /// <summary>
        /// 停止采集流
        /// </summary>
        public Response StopGrab()
        {
            try
            {
                // Gige
                if (curCamera.GetSfncVersion() < cameraVersion)
                {
                    if (curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart))
                    {
                        if (curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            curCamera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            curCamera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);
                        }
                        else
                        {
                            curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            curCamera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);
                        }
                    }
                }
                else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                {
                    if (curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart))
                    {
                        if (curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            curCamera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            curCamera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);
                        }
                        else
                        {
                            curCamera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            curCamera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);
                        }
                    }
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("停止采集失败!" + ex.Message);
            }
        }

        /// <summary>
        /// 开启相机输出 功能待测试
        /// </summary>
        /// <returns></returns>
        public Response SetBitOn()
        {
            ///功能待测试
            curCamera.Parameters[PLCamera.LineSelector].TrySetValue(PLCamera.LineSelector.Out1);
            curCamera.Parameters[PLCamera.LineSource].TrySetValue(PLCamera.LineSource.UserOutput);
            curCamera.Parameters[PLCamera.UserOutputSelector].TrySetValue(PLCamera.LineSource.UserOutput);
            curCamera.Parameters[PLCamera.UserOutputValue].SetValue(true);

            return Response.Ok();
        }

        /// <summary>
        /// 关闭相机输出 功能待测试
        /// </summary>
        /// <returns></returns>
        public Response SetBitOff()
        {
            ///功能待测试
            curCamera.Parameters[PLCamera.LineSelector].TrySetValue(PLCamera.LineSelector.Out1);
            curCamera.Parameters[PLCamera.LineSource].TrySetValue(PLCamera.LineSource.UserOutput);
            curCamera.Parameters[PLCamera.UserOutputSelector].TrySetValue(PLCamera.LineSource.UserOutput);
            curCamera.Parameters[PLCamera.UserOutputValue].SetValue(false);

            return Response.Ok();
        }

        #region 私有方法

        private bool IsColorPixelFormat(MyCamera.MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGBA8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BGRA8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsMonoPixelFormat(MyCamera.MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;

                default:
                    return false;
            }
        }

        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            try
            {
                IGrabResult grabResult = e.GrabResult;
                if (grabResult.IsValid)
                {
                    // 判断是否是黑白图片格式
                    if (grabResult.PixelTypeValue == PixelType.Mono8)
                    {
                        if (frameMemoryAddress == IntPtr.Zero)
                        {
                            frameMemoryAddress = Marshal.AllocHGlobal((Int32)grabResult.PayloadSize);
                        }
                        converter.OutputPixelFormat = PixelType.Mono8;
                        converter.Convert(frameMemoryAddress, grabResult.PayloadSize, grabResult);

                        // 转换为Halcon图像显示
                        HOperatorSet.GenImage1Extern(out attachedImage, "byte", (HTuple)grabResult.Width, (HTuple)grabResult.Height, (HTuple)frameMemoryAddress, IntPtr.Zero);
                    }
                    //彩色图片
                    else if (grabResult.PixelTypeValue == PixelType.BayerBG8 || grabResult.PixelTypeValue == PixelType.BayerGB8
                                || grabResult.PixelTypeValue == PixelType.BayerRG8 || grabResult.PixelTypeValue == PixelType.BayerGR8)
                    {
                        int imageWidth = grabResult.Width - 1;
                        int imageHeight = grabResult.Height - 1;
                        int payloadSize = imageWidth * imageHeight;

                        //allocate the m_stream_size amount of bytes in non-managed environment 
                        if (frameMemoryAddress == IntPtr.Zero)
                        {
                            frameMemoryAddress = Marshal.AllocHGlobal((Int32)(3 * payloadSize));
                        }
                        //converter.OutputPixelFormat = PixelType.BGR8packed;
                        converter.OutputPixelFormat = PixelType.RGB8packed;
                        converter.Parameters[PLPixelDataConverter.InconvertibleEdgeHandling].SetValue("Clip");
                        converter.Convert(frameMemoryAddress, 3 * payloadSize, grabResult);

                        HOperatorSet.GenImageInterleaved(out attachedImage, frameMemoryAddress, "bgr",
                                    (HTuple)imageWidth, (HTuple)imageHeight, -1, "byte", (HTuple)imageWidth, (HTuple)imageHeight, 0, 0, -1, 0);
                    }
                    //对获取的图像进行相关处理
                    PostFrame(attachedImage);
                    attachedImage.Dispose();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("获取相机图像委托异常!" + ex.Message);
            }
            finally
            {
                e.DisposeGrabResultIfClone();
            }
        }

        private void ReadBuffer()
        {
            // Wait for an image and then retrieve it. A timeout of 5000 ms is used.
            IGrabResult grabResult = curCamera.StreamGrabber.RetrieveResult(1000, TimeoutHandling.ThrowException);
            //using (grabResult)
            //{
            //    // Image grabbed successfully?
            //    if (grabResult.GrabSucceeded)
            //    {
            //        byte[] buffer = grabResult.PixelData as byte[];
            //        IntPtr intPtr = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
            //        HOperatorSet.GenImage1(out attachedImage, "byte", (HTuple)grabResult.Width, (HTuple)grabResult.Height, (HTuple)intPtr);
            //        PostFrame(attachedImage);
            //        attachedImage.Dispose();

            //    }
            //    else
            //    {
            //        Console.WriteLine("Error: {0} {1}", grabResult.ErrorCode, grabResult.ErrorDescription);
            //    }
            //}

        }
        #endregion

    }
}
