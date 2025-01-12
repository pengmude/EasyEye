using HalconDotNet;
using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using NLog;
using SmartLib;

namespace SmartVEye
{
    /// <summary>
    /// 海康相机
    /// </summary>
    public class CamHik
    {
        //获取到的设备列表
        private MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        private int selectCamIdx = -1;
        private MyCamera curCamera = null;
        private MyCamera.MV_CC_DEVICE_INFO curDevice;
        private MyCamera.cbOutputExdelegate ImageCallback;
        //是否正在采集
        IntPtr pImgTemp = IntPtr.Zero;
        private object snapLock = new object();//拍照锁
        private object bufferLock = new object();//内存图像读取锁

        HObject grabHobj = null;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

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
                if (curCamera != null)
                {
                    //m_bGrabbing = false;
                    curCamera.MV_CC_CloseDevice_NET();
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
                if (curCamera != null)
                {
                    curCamera.MV_CC_DestroyDevice_NET();
                }
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
                GC.Collect();
                //获取当前连接的所有相机
                var nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
                if (m_pDeviceList.nDeviceNum <= 0)
                {
                    return Response.Fail("无相机可用,请检查连接!");
                }
                for (var i = 0; i < m_pDeviceList.nDeviceNum; i++)
                {
                    var device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i],
                        typeof(MyCamera.MV_CC_DEVICE_INFO));
                    if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                    {
                        var buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                        var usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(
                            device.SpecialInfo.stUsb3VInfo,
                            typeof(MyCamera.MV_USB3_DEVICE_INFO));
                        if (usbInfo.chUserDefinedName == camName)
                        {
                            selectCamIdx = i; break;
                        }
                    }
                    else if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                    {
                        var buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                        var gigeInfo =
                            (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer,
                                typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                        if (gigeInfo.chUserDefinedName == camName)
                        {
                            selectCamIdx = i; break;
                        }
                    }
                }
                if (selectCamIdx < 0)
                {
                    return Response.Fail("未找到名称为【" + camName + "】的相机");
                }
                curCamera = new MyCamera();
                curDevice = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(
                    m_pDeviceList.pDeviceInfo[selectCamIdx], typeof(MyCamera.MV_CC_DEVICE_INFO));
                nRet = curCamera.MV_CC_CreateDevice_NET(ref curDevice);
                if (nRet != MyCamera.MV_OK)
                {
                    return Response.Fail("实例化相机【" + camName + "】失败");
                }
                nRet = curCamera.MV_CC_OpenDevice_NET();
                if (nRet != MyCamera.MV_OK)
                {
                    return Response.Fail("相机【" + camName + "】打开失败");
                }
                if (CommonData.IsUseCamNet > 0)
                {
                    // ch:探测网络最佳包大小(只对GigE相机有效)
                    if (curDevice.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                    {
                        int nPacketSize = curCamera.MV_CC_GetOptimalPacketSize_NET();
                        if (nPacketSize > 0)
                        {
                            nRet = curCamera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                            if (nRet != MyCamera.MV_OK)
                            {
                                return Response.Fail("设置相机【" + camName + "】网络最佳包大小失败");
                            }
                        }
                        else
                        {
                            return Response.Fail("获取相机【" + camName + "】网络最佳包大小失败");
                        }
                    }
                }
                //关闭相机采集帧率限制
                nRet = curCamera.MV_CC_SetBoolValue_NET("AcquisitionFrameRateEnable", false);
                if (MyCamera.MV_OK != nRet) return Response.Fail("设置相机采集帧率使能失败!");
                IsOpen = true;
                //注册委托
                ImageCallback = new MyCamera.cbOutputExdelegate(OnImageGrabbed);
                nRet = this.curCamera.MV_CC_RegisterImageCallBackEx_NET(ImageCallback, IntPtr.Zero);
                if (MyCamera.MV_OK != nRet) return Response.Fail("注册相机回调函数失败！");
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
                var stParam = new MyCamera.MVCC_FLOATVALUE();
                var nRet = curCamera.MV_CC_GetFloatValue_NET("ExposureTime", ref stParam);
                return MyCamera.MV_OK == nRet ? Response<float>.Ok(stParam.fCurValue) : Response<float>.Fail("相机曝光时间获取失败");
            }
            else
            {
                return Response<float>.Fail("相机曝光时间获取失败,相机为空!");
            }
        }
        /// <summary>
        /// 获取增益值
        /// </summary>
        public Response<float> GetGain()
        {
            if (curCamera != null)
            {
                var stParam = new MyCamera.MVCC_FLOATVALUE();
                var nRet = curCamera.MV_CC_GetFloatValue_NET("Gain", ref stParam);
                return MyCamera.MV_OK == nRet ? Response<float>.Ok(stParam.fCurValue) : Response<float>.Fail("相机增益获取失败");
            }
            else
            {
                return Response<float>.Fail("相机增益获取失败,相机为空!");
            }
        }

        public Response<float> GetTriggerDelay()
        {
            if (curCamera != null)
            {
                var delay = new MyCamera.MVCC_FLOATVALUE();
                var nRet = curCamera.MV_CC_GetFloatValue_NET("TriggerDelayAbs", ref delay);
                return MyCamera.MV_OK == nRet ? Response<float>.Ok(delay.fCurValue) : Response<float>.Fail("相机触发延迟获取失败");
            }
            else
            {
                return Response<float>.Fail("相机增益获取失败,相机为空!");
            }
        }

        /// <summary>
        /// 设置触发延迟，单位us
        /// </summary>
        /// <param name="time">单位us</param>
        public void SetTriggerDelay(float delay)
        {
            if (curCamera == null) throw new Exception("相机对象为空！");

            curCamera.MV_CC_SetFloatValue_NET("TriggerDelayAbs", delay);
        }

        /// <summary>
        /// 设置曝光时间
        /// </summary>
        public Response SetExposTime(float val)
        {

            if (curCamera != null)
            {
                var nRet = curCamera.MV_CC_SetFloatValue_NET("ExposureTime", val);
                return MyCamera.MV_OK == nRet ? Response.Ok() : Response.Fail("设置相机曝光时间失败");
            }
            else
            {
                return Response.Fail("设置相机曝光时间失败,相机为空!");
            }
        }
        /// <summary>
        /// 设置增益
        /// </summary>
        public Response SetGain(float val)
        {

            if (curCamera != null)
            {
                curCamera.MV_CC_SetEnumValue_NET("GainAuto", 0);
                var nRet = curCamera.MV_CC_SetFloatValue_NET("Gain", val);
                return MyCamera.MV_OK == nRet ? Response.Ok() : Response.Fail("设置相机增益失败");
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
                    var nRet = curCamera.MV_CC_SetEnumValue_NET("TriggerMode", 1);
                    if (MyCamera.MV_OK != nRet) return Response.Fail("设置相机触发模式失败!");
                    nRet = curCamera.MV_CC_SetEnumValue_NET("TriggerSource", 7);
                    if (MyCamera.MV_OK != nRet) return Response.Fail("设置相机软触发模式失败!");
                    curCamera.MV_CC_StartGrabbing_NET();
                    Thread.Sleep(1);
                    nRet = curCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
                    if (MyCamera.MV_OK != nRet) return Response.Fail("相机触发失败!");
                    //Thread.Sleep(5);
                    //return GetImage();
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
                var nRet = curCamera.MV_CC_SetEnumValue_NET("TriggerMode", 1);
                if (MyCamera.MV_OK != nRet) return Response.Fail("设置相机触发模式失败!");
                nRet = curCamera.MV_CC_SetEnumValue_NET("TriggerSource", 0);
                return MyCamera.MV_OK == nRet ? Response.Ok() : Response.Fail("设置相机硬触发模式失败");
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
                var nRet = curCamera.MV_CC_SetEnumValue_NET("TriggerMode", 1);
                if (MyCamera.MV_OK != nRet) return Response.Fail("设置相机触发模式失败!");
                nRet = curCamera.MV_CC_SetEnumValue_NET("TriggerSource", 7);
                return MyCamera.MV_OK == nRet ? Response.Ok() : Response.Fail("设置相机软触发模式失败");
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
                var nRet = curCamera.MV_CC_StartGrabbing_NET();
                if (MyCamera.MV_OK != nRet) return Response.Fail("开始采集失败！");
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
                if (curCamera != null)
                {
                    var nRet = curCamera.MV_CC_StopGrabbing_NET();
                    if (MyCamera.MV_OK != nRet) return Response.Fail("停止采集失败!");
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("停止采集失败!" + ex.Message);
            }
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

        /// <summary>
        /// 获取触发的图像
        /// </summary>
        /// <returns></returns>
        private Response<HObject> GetImage()
        {
            int nRet = MyCamera.MV_OK;
            MyCamera.MV_FRAME_OUT stFrameOut = new MyCamera.MV_FRAME_OUT();
            IntPtr pImageBuf = IntPtr.Zero;
            int nImageBufSize = 0;
            HObject Hobj = null;
            HOperatorSet.GenEmptyObj(out Hobj);
            try
            {
                nRet = curCamera.MV_CC_GetImageBuffer_NET(ref stFrameOut, 1000);
                if (MyCamera.MV_OK == nRet)
                {
                    lock (bufferLock)
                    {
                        if (IsColorPixelFormat(stFrameOut.stFrameInfo.enPixelType))
                        {
                            if (stFrameOut.stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed)
                            {
                                if (pImgTemp != IntPtr.Zero)
                                {
                                    pImgTemp = IntPtr.Zero;
                                    GC.Collect();
                                }
                                pImgTemp = stFrameOut.pBufAddr;
                            }
                            else
                            {
                                if (IntPtr.Zero == pImageBuf || nImageBufSize < (stFrameOut.stFrameInfo.nWidth * stFrameOut.stFrameInfo.nHeight * 3))
                                {
                                    if (pImageBuf != IntPtr.Zero)
                                    {
                                        Marshal.FreeHGlobal(pImageBuf);
                                        pImageBuf = IntPtr.Zero;
                                    }

                                    pImageBuf = Marshal.AllocHGlobal((int)stFrameOut.stFrameInfo.nWidth * stFrameOut.stFrameInfo.nHeight * 3);
                                    if (IntPtr.Zero == pImageBuf)
                                    {
                                        return Response<HObject>.Fail("取图失败,图像指针为空!");
                                    }
                                    nImageBufSize = stFrameOut.stFrameInfo.nWidth * stFrameOut.stFrameInfo.nHeight * 3;
                                }

                                MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

                                stPixelConvertParam.pSrcData = stFrameOut.pBufAddr;//源数据
                                stPixelConvertParam.nWidth = stFrameOut.stFrameInfo.nWidth;//图像宽度
                                stPixelConvertParam.nHeight = stFrameOut.stFrameInfo.nHeight;//图像高度
                                stPixelConvertParam.enSrcPixelType = stFrameOut.stFrameInfo.enPixelType;//源数据的格式
                                stPixelConvertParam.nSrcDataLen = stFrameOut.stFrameInfo.nFrameLen;

                                stPixelConvertParam.nDstBufferSize = (uint)nImageBufSize;
                                stPixelConvertParam.pDstBuffer = pImageBuf;//转换后的数据
                                stPixelConvertParam.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
                                nRet = curCamera.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
                                if (MyCamera.MV_OK != nRet)
                                {
                                    return Response<HObject>.Fail("取图失败,图像格式转换失败!");
                                }
                                pImgTemp = pImageBuf;
                            }
                            try
                            {
                                HOperatorSet.GenImageInterleaved(out Hobj, (HTuple)pImgTemp, (HTuple)"rgb", (HTuple)stFrameOut.stFrameInfo.nWidth, (HTuple)stFrameOut.stFrameInfo.nHeight, -1, "byte", 0, 0, 0, 0, -1, 0);
                            }
                            catch
                            {
                                return Response<HObject>.Fail("取图失败,彩色图像生成失败!");
                            }
                        }
                        else if (IsMonoPixelFormat(stFrameOut.stFrameInfo.enPixelType))
                        {
                            if (stFrameOut.stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                            {
                                if (pImgTemp != IntPtr.Zero)
                                {
                                    pImgTemp = IntPtr.Zero;
                                    GC.Collect();
                                }
                                pImgTemp = stFrameOut.pBufAddr;
                            }
                            else
                            {
                                if (IntPtr.Zero == pImageBuf || nImageBufSize < (stFrameOut.stFrameInfo.nWidth * stFrameOut.stFrameInfo.nHeight))
                                {
                                    if (pImageBuf != IntPtr.Zero)
                                    {
                                        Marshal.FreeHGlobal(pImageBuf);
                                        pImageBuf = IntPtr.Zero;
                                    }
                                    pImageBuf = Marshal.AllocHGlobal((int)stFrameOut.stFrameInfo.nWidth * stFrameOut.stFrameInfo.nHeight);
                                    if (IntPtr.Zero == pImageBuf)
                                    {
                                        return Response<HObject>.Fail("取图失败,图像指针为空!");
                                    }
                                    nImageBufSize = stFrameOut.stFrameInfo.nWidth * stFrameOut.stFrameInfo.nHeight;
                                }

                                //格式转换
                                MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

                                stPixelConvertParam.pSrcData = stFrameOut.pBufAddr;//源数据
                                stPixelConvertParam.nWidth = stFrameOut.stFrameInfo.nWidth;//图像宽度
                                stPixelConvertParam.nHeight = stFrameOut.stFrameInfo.nHeight;//图像高度
                                stPixelConvertParam.enSrcPixelType = stFrameOut.stFrameInfo.enPixelType;//源数据的格式
                                stPixelConvertParam.nSrcDataLen = stFrameOut.stFrameInfo.nFrameLen;

                                stPixelConvertParam.nDstBufferSize = (uint)nImageBufSize;
                                stPixelConvertParam.pDstBuffer = pImageBuf;//转换后的数据
                                stPixelConvertParam.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
                                nRet = curCamera.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
                                if (MyCamera.MV_OK != nRet)
                                {
                                    return Response<HObject>.Fail("取图失败,图像格式转换失败!");
                                }
                                pImgTemp = pImageBuf;
                            }
                        }
                        else
                        {
                            nRet = curCamera.MV_CC_FreeImageBuffer_NET(ref stFrameOut);
                            return Response<HObject>.Fail("取图图像成功,既不属于彩色图像,又不属于灰度图像!");
                        }
                    }
                    return Response<HObject>.Ok(Hobj);
                }
                else
                {
                    curCamera.MV_CC_StopGrabbing_NET();
                    Thread.Sleep(10);
                    curCamera.MV_CC_StartGrabbing_NET();
                    return Response<HObject>.Fail("图像获取失败! 错误码:" + nRet);
                }
            }
            catch (Exception ex)
            {
                return Response<HObject>.Fail("图像获取失败!" + ex.Message);
            }
            finally
            {
                nRet = curCamera.MV_CC_FreeImageBuffer_NET(ref stFrameOut);
                curCamera.MV_CC_StopGrabbing_NET();
                //if (nRet != MyCamera.MV_OK)
                //{
                //    curCamera.MV_CC_StopGrabbing_NET();
                //    curCamera.MV_CC_StartGrabbing_NET();
                //}
                if (pImageBuf != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pImageBuf);
                    pImageBuf = IntPtr.Zero;
                }
                if (pImgTemp != IntPtr.Zero)
                {
                    pImgTemp = IntPtr.Zero;
                    GC.Collect();
                }
            }
        }

        private void OnImageGrabbed(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            int nRet = MyCamera.MV_OK;
            IntPtr pImageBuf = IntPtr.Zero;
            int nImageBufSize = 0;

            HOperatorSet.GenEmptyObj(out grabHobj);
            try
            {
                if (IsColorPixelFormat(pFrameInfo.enPixelType))
                {
                    if (pFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed)
                    {
                        if (pImgTemp != IntPtr.Zero)
                        {
                            pImgTemp = IntPtr.Zero;
                            GC.Collect();
                        }
                        pImgTemp = pData;
                    }
                    else
                    {
                        if (IntPtr.Zero == pImageBuf || nImageBufSize < (pFrameInfo.nWidth * pFrameInfo.nHeight * 3))
                        {
                            if (pImageBuf != IntPtr.Zero)
                            {
                                Marshal.FreeHGlobal(pImageBuf);
                                pImageBuf = IntPtr.Zero;
                            }

                            pImageBuf = Marshal.AllocHGlobal((int)pFrameInfo.nWidth * pFrameInfo.nHeight * 3);
                            if (IntPtr.Zero == pImageBuf)
                            {
                                //lock (grabLock)
                                //{
                                //    grabObjList.Enqueue(Response<HObject>.Fail("取图失败,图像指针为空!"));
                                //}
                            }
                            nImageBufSize = pFrameInfo.nWidth * pFrameInfo.nHeight * 3;
                        }

                        MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

                        stPixelConvertParam.pSrcData = pData;//源数据
                        stPixelConvertParam.nWidth = pFrameInfo.nWidth;//图像宽度
                        stPixelConvertParam.nHeight = pFrameInfo.nHeight;//图像高度
                        stPixelConvertParam.enSrcPixelType = pFrameInfo.enPixelType;//源数据的格式
                        stPixelConvertParam.nSrcDataLen = pFrameInfo.nFrameLen;

                        stPixelConvertParam.nDstBufferSize = (uint)nImageBufSize;
                        stPixelConvertParam.pDstBuffer = pImageBuf;//转换后的数据
                        stPixelConvertParam.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
                        nRet = curCamera.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
                        if (MyCamera.MV_OK != nRet)
                        {
                            //lock (grabLock)
                            //{
                            //    grabObjList.Enqueue(Response<HObject>.Fail("取图失败,图像格式转换失败!"));
                            //}
                        }
                        pImgTemp = pImageBuf;
                    }
                    try
                    {
                        HOperatorSet.GenImageInterleaved(out grabHobj, (HTuple)pImgTemp, (HTuple)"rgb", (HTuple)pFrameInfo.nWidth, (HTuple)pFrameInfo.nHeight, -1, "byte", 0, 0, 0, 0, -1, 0);
                    }
                    catch
                    {
                        //lock (grabLock)
                        //{
                        //    grabObjList.Enqueue(Response<HObject>.Fail("取图失败,彩色图像生成失败!"));
                        //}
                    }
                }
                else if (IsMonoPixelFormat(pFrameInfo.enPixelType))
                {
                    if (pFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                    {
                        pImgTemp = pData;
                    }
                    else
                    {
                        if (IntPtr.Zero == pImageBuf || nImageBufSize < (pFrameInfo.nWidth * pFrameInfo.nHeight))
                        {
                            if (pImageBuf != IntPtr.Zero)
                            {
                                Marshal.FreeHGlobal(pImageBuf);
                                pImageBuf = IntPtr.Zero;
                            }

                            pImageBuf = Marshal.AllocHGlobal((int)pFrameInfo.nWidth * pFrameInfo.nHeight);
                            if (IntPtr.Zero == pImageBuf)
                            {
                                //lock (grabLock)
                                //{
                                //    grabObjList.Enqueue(Response<HObject>.Fail("取图失败,图像指针为空!"));
                                //}
                            }
                            nImageBufSize = pFrameInfo.nWidth * pFrameInfo.nHeight;
                        }

                        MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

                        stPixelConvertParam.pSrcData = pData;//源数据
                        stPixelConvertParam.nWidth = pFrameInfo.nWidth;//图像宽度
                        stPixelConvertParam.nHeight = pFrameInfo.nHeight;//图像高度
                        stPixelConvertParam.enSrcPixelType = pFrameInfo.enPixelType;//源数据的格式
                        stPixelConvertParam.nSrcDataLen = pFrameInfo.nFrameLen;

                        stPixelConvertParam.nDstBufferSize = (uint)nImageBufSize;
                        stPixelConvertParam.pDstBuffer = pImageBuf;//转换后的数据
                        stPixelConvertParam.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
                        nRet = curCamera.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
                        if (MyCamera.MV_OK != nRet)
                        {
                            //lock (grabLock)
                            //{
                            //    grabObjList.Enqueue(Response<HObject>.Fail("取图失败,图像格式转换失败!"));
                            //}
                        }
                        pImgTemp = pImageBuf;
                    }
                }
                HOperatorSet.GenImage1Extern(out grabHobj, "byte", pFrameInfo.nWidth, pFrameInfo.nHeight, pImgTemp, IntPtr.Zero);
                PostFrame(grabHobj);
            }
            catch (Exception ex)
            {
                _logger.Error("获取相机图像委托异常!" + ex.Message);
                //lock (grabLock)
                //{
                //    if (grabObjList == null) grabObjList = new Queue<Response<HObject>>();
                //    grabObjList.Enqueue(Response<HObject>.Fail("图像获取异常!" + ex.Message));
                //}
            }
        }

        #endregion

    }
}
