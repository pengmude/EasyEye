using HalconDotNet;
using NLog;
using SmartLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartVEye
{
    /// <summary>
    /// 共用工具类
    /// </summary>
    internal class VisCtrlHelper
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        #region 检测方法函数
        /// <summary>
        /// 白页检测方法
        /// </summary>
        /// <param name="CamName">相机名称</param>
        /// <param name="grabImage">要检测的图像</param>
        /// <param name="WinCtrl">显示窗口</param>
        /// <param name="PageWBContourNum">百页轮廓数量</param>
        /// <param name="PageWhite">百页阈值</param>
        /// <param name="IsWorkOnLine">是否联机</param>
        /// <returns></returns>
        internal static Response<InspResult> PageWhiteFunc(string CamName, HObject grabImage, DisplayCtrl WinCtrl, int PageWBContourNum, int PageWhite, bool IsWorkOnLine,
             double ROIRow, double ROICol, double ROIWidth, double ROIHeight)
        {
            HObject ImgEdges, ImgRect, rectangle;
            HOperatorSet.GenEmptyObj(out ImgEdges);
            HOperatorSet.GenEmptyObj(out ImgRect);
            HOperatorSet.GenEmptyObj(out rectangle);
            try
            {
                //计算ROI
                double rowLT = ROIRow - ROIHeight / 2;
                double colLT = ROICol - ROIWidth / 2;
                double rowRD = ROIRow + ROIHeight / 2;
                double colRD = ROICol + ROIWidth / 2;
                //查找轮廓
                HOperatorSet.GenRectangle1(out rectangle, rowLT, colLT, rowRD, colRD);
                HOperatorSet.ReduceDomain(grabImage, rectangle, out grabImage);
                HOperatorSet.EdgesSubPix(grabImage, out ImgEdges, "canny", 1, 10, 40);
                HOperatorSet.CountObj(ImgEdges, out HTuple number);
                //计算灰度值
                HOperatorSet.GenRectangle1(out ImgRect, (int)rowLT, (int)colLT, (int)rowRD, (int)colRD);
                HOperatorSet.Intensity(ImgRect, grabImage, out HTuple mean, out HTuple deviation);
                if (number <= PageWBContourNum && mean > PageWhite)//OK
                {
                    if (CommonData.IsSaveRunLog > 0)
                    {
                        Task.Run(() =>
                        {
                            _logger.Info($"相机[{CamName}]白页判断结果为OK");
                        });
                    }
                    return Response<InspResult>.Ok(new InspResult()
                    {
                        result = true,
                        resobj = ImgEdges
                    });
                }
                else//NG
                {
                    WinCtrl.DisplayObject(ImgEdges, "red");
                    WinCtrl.DisplayBuffer();
                    if (CommonData.IsSaveRunLog > 0)
                    {
                        Task.Run(() =>
                        {
                            _logger.Info($"相机[{CamName}]白页判断结果为NG");
                        });
                    }
                    //如果联机 给出NG信号
                    if (IsWorkOnLine)
                    {
                        Thread ioThread = new Thread(new ThreadStart(() =>
                        {
                            int ioidx = Convert.ToInt32(CamName.Substring(3));
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]准备打开IO{ioidx}!,当前设置的NG信号输出时间为:{CommonData.PLCAlarmTime}ms");
                            Response ioRet = IOHelper.SetBitOn(ioidx);
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]打开IO{ioidx}完成!");
                            if (!ioRet) _logger.Error($"相机[{CamName}]打开IO异常!{ioRet.Msg}");
                            Thread.Sleep(CommonData.PLCAlarmTime);
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]准备关闭IO{ioidx}!");
                            ioRet = IOHelper.SetBitOff(ioidx);
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]关闭IO{ioidx}完成!");
                            if (!ioRet) _logger.Error($"相机[{CamName}]关闭IO异常!{ioRet.Msg}");
                            _logger.Info($"联机状态,相机[{CamName}]发送NG信号给PLC完成!");
                        }));
                        ioThread.IsBackground = true;
                        ioThread.Start();
                    }
                    else
                    {
                        _logger.Info($"非联机状态,相机[{CamName}]白页检测不发送NG信号给PLC!");
                    }
                    return Response<InspResult>.Ok(new InspResult()
                    {
                        result = false,
                        resobj = ImgEdges
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"相机[{CamName}]白页检测异常!{ex.Message}");
                return Response<InspResult>.Fail($"相机[{CamName}]白页检测异常!{ex.Message}");
            }
            finally
            {
                if (ImgEdges != null) ImgEdges.Dispose();
                if (ImgRect != null) ImgRect.Dispose();
                //if (grabImage != null) grabImage.Dispose();
            }
        }

        /// <summary>
        /// 黑页检测方法
        /// </summary>
        /// <param name="CamName">相机名称</param>
        /// <param name="grabImage">要检测的图像</param>
        /// <param name="WinCtrl">显示窗口</param>
        /// <param name="PageWBContourNum">百页轮廓数量</param>
        /// <param name="PageBlack">百页阈值</param>
        /// <param name="IsWorkOnLine">是否联机</param>
        /// <returns></returns>
        internal static Response<InspResult> PageBlackFun(string CamName, HObject grabImage, DisplayCtrl WinCtrl, int PageWBContourNum, int PageBlack, bool IsWorkOnLine,
        double ROIRow, double ROICol, double ROIWidth, double ROIHeight)
        {
            HObject ImgEdges, ImgRect, rectangle;
            HOperatorSet.GenEmptyObj(out ImgEdges);
            HOperatorSet.GenEmptyObj(out ImgRect);
            HOperatorSet.GenEmptyObj(out rectangle);
            try
            {
                //计算ROI
                double rowLT = ROIRow - ROIHeight / 2;
                double colLT = ROICol - ROIWidth / 2;
                double rowRD = ROIRow + ROIHeight / 2;
                double colRD = ROICol + ROIWidth / 2;
                //查找轮廓
                HOperatorSet.GenRectangle1(out rectangle, rowLT, colLT, rowRD, colRD);
                HOperatorSet.ReduceDomain(grabImage, rectangle, out grabImage);
                HOperatorSet.EdgesSubPix(grabImage, out ImgEdges, "canny", 1, 10, 40);
                HOperatorSet.CountObj(ImgEdges, out HTuple number);
                //计算灰度值
                HOperatorSet.GenRectangle1(out ImgRect, (int)rowLT, (int)colLT, (int)rowRD, (int)colRD);
                HOperatorSet.Intensity(ImgRect, grabImage, out HTuple mean, out HTuple deviation);
                if (number <= PageWBContourNum && mean < PageBlack)//OK
                {
                    if (CommonData.IsSaveRunLog > 0)
                    {
                        Task.Run(() =>
                        {
                            _logger.Info($"相机[{CamName}]黑页判断结果为OK");
                        });
                    }
                    return Response<InspResult>.Ok(new InspResult()
                    {
                        result = true,
                        resobj = ImgEdges
                    });
                }
                else//NG
                {
                    if (CommonData.IsSaveRunLog > 0)
                    {
                        Task.Run(() =>
                        {
                            _logger.Info($"相机[{CamName}]黑页判断结果为NG");
                        });
                    }
                    //如果联机 给出NG信号
                    if (IsWorkOnLine)
                    {
                        Thread ioThread = new Thread(new ThreadStart(() =>
                        {
                            int ioidx = Convert.ToInt32(CamName.Substring(3));
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]准备打开IO{ioidx}!,当前设置的NG信号输出时间为:{CommonData.PLCAlarmTime}ms");
                            Response ioRet = IOHelper.SetBitOn(ioidx);
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]打开IO{ioidx}完成!");
                            if (!ioRet) _logger.Error($"相机[{CamName}]打开IO异常!{ioRet.Msg}");
                            Thread.Sleep(CommonData.PLCAlarmTime);
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]准备关闭IO{ioidx}!");
                            ioRet = IOHelper.SetBitOff(ioidx);
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]关闭IO{ioidx}完成!");
                            if (!ioRet) _logger.Error($"相机[{CamName}]关闭IO异常!{ioRet.Msg}");
                            _logger.Info($"联机状态,相机[{CamName}]发送NG信号给PLC完成!");
                        }));
                        ioThread.IsBackground = true;
                        ioThread.Start();
                    }
                    else
                    {
                        _logger.Info($"非联机状态,相机[{CamName}]黑页检测不发送NG信号给PLC!");
                    }
                    return Response<InspResult>.Ok(new InspResult()
                    {
                        result = false,
                        resobj = ImgEdges
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"相机[{CamName}]黑页检测异常!{ex.Message}");
                return Response<InspResult>.Fail($"相机[{CamName}]黑页检测异常!{ex.Message}");
            }
            finally
            {
                if (ImgEdges != null) ImgEdges.Dispose();
                if (grabImage != null) grabImage.Dispose();
            }
        }


        /// <summary>
        /// 图文轮廓检测方法
        /// </summary>
        /// <param name="CamName">相机名称</param>
        /// <param name="grabImage">要检测的图像</param>
        /// <param name="WinCtrl">显示窗口</param>
        /// <param name="hv_ModelHandle">模板句柄</param>
        /// <param name="AngleStart">起始角度</param>
        /// <param name="AngleEnd">终止角度</param>
        /// <param name="MinScale">最小缩放</param>
        /// <param name="MaxScale">最大缩放</param>
        /// <param name="MinScore">最小相似度</param>
        /// <param name="MinContrast">最小对比度</param>
        /// <param name="IsWorkOnLine">是否联机</param>
        /// <returns></returns>
        internal static Response<InspResult> ModelMatchFunc(string CamName, HObject grabImage, DisplayCtrl WinCtrl, HShapeModel hv_ModelHandle,
            int AngleStart, int AngleEnd, double MinScale, double MaxScale, double MinScore, double MinContrast, int ROIWidth, int ROIHeight,
        bool IsWorkOnLine)
        {
            HObject crossObj;
            HOperatorSet.GenEmptyObj(out crossObj);
            try
            {
                HTuple ho_row = new HTuple(), ho_column = new HTuple();
                HTuple ho_angle = new HTuple(), ho_scale = new HTuple();
                HTuple ho_score = new HTuple();
                HTuple radStart, radEnd;
                HOperatorSet.TupleRad((double)AngleStart, out radStart);
                HOperatorSet.TupleRad((double)AngleEnd, out radEnd);
                hv_ModelHandle.SetGenericShapeModelParam("min_score", MinScore);
                hv_ModelHandle.SetGenericShapeModelParam("num_matches", 1);
                hv_ModelHandle.SetGenericShapeModelParam("angle_start", radStart);
                hv_ModelHandle.SetGenericShapeModelParam("angle_end", radEnd);
                hv_ModelHandle.SetGenericShapeModelParam("greediness", 0.5);
                hv_ModelHandle.SetGenericShapeModelParam("timeout", CommonData.MatchTimeOut);
                hv_ModelHandle.SetGenericShapeModelParam("min_contrast", MinContrast);
                hv_ModelHandle.SetGenericShapeModelParam("pyramid_level_highest", 0);
                hv_ModelHandle.SetGenericShapeModelParam("pyramid_level_lowest", 1);
                //记录运行时间
                Stopwatch watch = new Stopwatch();
                if (CommonData.IsSaveRunLog > 0) watch.Start();
                int ho_MatchNum = 0;
                HGenericShapeModelResult matchResult = null;
                try
                {
                    matchResult = hv_ModelHandle.FindGenericShapeModel(grabImage, out ho_MatchNum);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Timeout occurred"))
                    {
                        ho_MatchNum = 0;
                    }
                    else
                    {
                        _logger.Info($"相机[{CamName}]处理异常!{ex.Message}");
                    }
                }
                if (CommonData.IsSaveRunLog > 0)
                {
                    watch.Stop();
                    _logger.Info($"相机[{CamName}]处理图像耗时:{watch.ElapsedMilliseconds}ms");
                }
                if (ho_MatchNum > 0)//OK品
                {
                    HTuple curRow = matchResult.GetGenericShapeModelResult("all", "row");
                    HTuple curColumn = matchResult.GetGenericShapeModelResult("all", "column");
                    HTuple curAngle = matchResult.GetGenericShapeModelResult("all", "angle");
                    HTuple curScore = matchResult.GetGenericShapeModelResult("all", "score");
                    HObject curContours = matchResult.GetGenericShapeModelResultObject("all", "contours");
                    HOperatorSet.GenCrossContourXld(out crossObj, curRow, curColumn, 200, 0);
                    WinCtrl.DisplayObject(crossObj, "blue");
                    WinCtrl.DisplayObject(curContours, "red");
                    if (IsWorkOnLine)
                    {
                        WinCtrl.ClearROIList();
                        WinCtrl.Ctrl_CreateRectROI(curRow, curColumn, ROIWidth, ROIHeight);
                    }
                    WinCtrl.DisplayBuffer();
                    if (CommonData.IsSaveRunLog > 0)
                    {
                        Thread thread = new Thread(new ThreadStart(() =>
                        {
                            _logger.Info($"相机[{CamName}]判断结果为OK");
                            _logger.Info($"相机[{CamName}]Row结果:{curRow}");
                            _logger.Info($"相机[{CamName}]Column结果:{curColumn}");
                            _logger.Info($"相机[{CamName}]Angle结果:{curAngle}");
                            _logger.Info($"相机[{CamName}]Score结果:{curScore}");
                        }));
                        thread.IsBackground = true;
                        thread.Start();
                    }
                    return Response<InspResult>.Ok(new InspResult()
                    {
                        result = true,
                        resobj = crossObj
                    });
                }
                else//NG品
                {
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]判断结果为NG");
                    //如果联机 给出NG信号
                    if (IsWorkOnLine)
                    {
                        Thread ioThread = new Thread(new ThreadStart(() =>
                        {
                            int ioidx = Convert.ToInt32(CamName.Substring(3));
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]准备打开IO{ioidx}!,当前设置的NG信号输出时间为:{CommonData.PLCAlarmTime}ms");
                            Response ioRet = IOHelper.SetBitOn(ioidx);
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]打开IO{ioidx}完成!");
                            if (!ioRet) _logger.Error($"相机[{CamName}]打开IO异常!{ioRet.Msg}");
                            Thread.Sleep(CommonData.PLCAlarmTime);
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]准备关闭IO{ioidx}!");
                            ioRet = IOHelper.SetBitOff(ioidx);
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]关闭IO{ioidx}完成!");
                            if (!ioRet) _logger.Error($"相机[{CamName}]关闭IO异常!{ioRet.Msg}");
                            _logger.Info($"联机状态,相机[{CamName}]发送NG信号给PLC完成!");
                        }));
                        ioThread.IsBackground = true;
                        ioThread.Start();
                    }
                    else
                    {
                        _logger.Info($"非联机状态,相机[{CamName}]不发送NG信号给PLC!");
                    }
                    return Response<InspResult>.Ok(new InspResult()
                    {
                        result = false,
                        resobj = crossObj
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Info($"相机[{CamName}]检测异常!{ex.Message}");
                return Response<InspResult>.Fail($"相机[{CamName}]检测异常!{ex.Message}");
            }
            finally
            {
                //if (grabImage != null) grabImage.Dispose();
                if (crossObj != null) crossObj.Dispose();
            }
        }

        #endregion


        internal static Response<InspResult> ImgModelMatchFunc(string CamName, HObject grabImage, DisplayCtrl WinCtrl, HTuple hv_ModelHandle,
       int AngleStart, int AngleEnd, double MinScale, double MaxScale, double MinScore, double MinContrast, int ROIWidth, int ROIHeight,
   bool IsWorkOnLine,int ImgScore, out double OutShowImgScore)
        {
            HObject crossObj;
            HOperatorSet.GenEmptyObj(out crossObj);
            try
            {
                HTuple ho_row = new HTuple(), ho_column = new HTuple();
                HTuple ho_angle = new HTuple(), ho_scale = new HTuple();
                HTuple ho_score = new HTuple();
                HTuple radStart, radEnd, hv_Smoothness = 21;
                HOperatorSet.TupleRad((double)AngleStart, out radStart);
                HOperatorSet.TupleRad((double)AngleEnd, out radEnd);
                HObject ho_ModelContours, ho_Image = null, ho_ImageRectified = new HObject();
                HObject ho_VectorField = new HObject(), ho_DeformedContours = new HObject();

                //记录运行时间
                Stopwatch watch = new Stopwatch();
                if (CommonData.IsSaveRunLog > 0) watch.Start();
                int ho_MatchNum = 0;
                HGenericShapeModelResult matchResult = null;
                try
                {
                    ho_ImageRectified.Dispose(); ho_VectorField.Dispose(); ho_DeformedContours.Dispose();
                    HOperatorSet.FindLocalDeformableModel(grabImage, out ho_ImageRectified, out ho_VectorField,
         out ho_DeformedContours, hv_ModelHandle, 0, 0, 1, 1, 1, 1, 0.5, 1, 1, 4, 1.0,
         ((new HTuple("image_rectified")).TupleConcat("vector_field")).TupleConcat(
         "deformed_contours"), ((new HTuple("deformation_smoothness")).TupleConcat(
         "expand_border")).TupleConcat("subpixel"), hv_Smoothness.TupleConcat((new HTuple(0)).TupleConcat(
         1)), out ho_score, out ho_row, out ho_column);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Timeout occurred"))
                    {
                        ho_MatchNum = 0;
                    }
                    else
                    {
                        _logger.Info($"相机[{CamName}]处理异常!{ex.Message}");
                    }
                }
                if (CommonData.IsSaveRunLog > 0)
                {
                    watch.Stop();
                    _logger.Info($"相机[{CamName}]处理图像耗时:{watch.ElapsedMilliseconds}ms");
                }
                if (ho_score.Length > 0)//OK品
                {
                    if (ho_score.D * 100 > ImgScore)
                    {
                        OutShowImgScore = ho_score.D;
                        //HTuple curRow = matchResult.GetGenericShapeModelResult("all", "row");
                        //HTuple curColumn = matchResult.GetGenericShapeModelResult("all", "column");
                        //HTuple curAngle = matchResult.GetGenericShapeModelResult("all", "angle");
                        //HTuple curScore = matchResult.GetGenericShapeModelResult("all", "score");
                        //HObject curContours = matchResult.GetGenericShapeModelResultObject("all", "contours");
                        HOperatorSet.GenCrossContourXld(out crossObj, ho_row, ho_column, 200, 0);
                        WinCtrl.DisplayObject(crossObj, "blue");
                        WinCtrl.DisplayObject(crossObj, "red");
                        if (IsWorkOnLine)
                        {
                            WinCtrl.ClearROIList();
                            WinCtrl.Ctrl_CreateRectROI(ho_row, ho_column, ROIWidth, ROIHeight);
                        }
                        WinCtrl.DisplayBuffer();
                        if (CommonData.IsSaveRunLog > 0)
                        {
                            Thread thread = new Thread(new ThreadStart(() =>
                            {
                                _logger.Info($"相机[{CamName}]判断结果为OK");
                                _logger.Info($"相机[{CamName}]Row结果:{ho_row}");
                                _logger.Info($"相机[{CamName}]Column结果:{ho_column}");
                                _logger.Info($"相机[{CamName}]Score结果:{ho_score}");
                            }));
                            thread.IsBackground = true;
                            thread.Start();
                        }
                        return Response<InspResult>.Ok(new InspResult()
                        {
                            result = true,
                            resobj = crossObj
                        });
                    }
                    else
                    {
                        OutShowImgScore = ho_score.D;
                        if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]判断结果为NG");
                        //如果联机 给出NG信号
                        if (IsWorkOnLine)
                        {
                            Thread ioThread = new Thread(new ThreadStart(() =>
                            {
                                int ioidx = Convert.ToInt32(CamName.Substring(3));
                                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]准备打开IO{ioidx}!,当前设置的NG信号输出时间为:{CommonData.PLCAlarmTime}ms");
                                Response ioRet = IOHelper.SetBitOn(ioidx);
                                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]打开IO{ioidx}完成!");
                                if (!ioRet) _logger.Error($"相机[{CamName}]打开IO异常!{ioRet.Msg}");
                                Thread.Sleep(CommonData.PLCAlarmTime);
                                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]准备关闭IO{ioidx}!");
                                ioRet = IOHelper.SetBitOff(ioidx);
                                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]关闭IO{ioidx}完成!");
                                if (!ioRet) _logger.Error($"相机[{CamName}]关闭IO异常!{ioRet.Msg}");
                                _logger.Info($"联机状态,相机[{CamName}]发送NG信号给PLC完成!");
                            }));
                            ioThread.IsBackground = true;
                            ioThread.Start();
                        }
                        else
                        {
                            _logger.Info($"非联机状态,相机[{CamName}]不发送NG信号给PLC!");
                        }
                        return Response<InspResult>.Ok(new InspResult()
                        {
                            result = false,
                            resobj = crossObj
                        });
                    }
                }
                else//NG品
                {
                    OutShowImgScore = 0;
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]判断结果为NG");
                    //如果联机 给出NG信号
                    if (IsWorkOnLine)
                    {
                        Thread ioThread = new Thread(new ThreadStart(() =>
                        {
                            int ioidx = Convert.ToInt32(CamName.Substring(3));
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]准备打开IO{ioidx}!,当前设置的NG信号输出时间为:{CommonData.PLCAlarmTime}ms");
                            Response ioRet = IOHelper.SetBitOn(ioidx);
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]打开IO{ioidx}完成!");
                            if (!ioRet) _logger.Error($"相机[{CamName}]打开IO异常!{ioRet.Msg}");
                            Thread.Sleep(CommonData.PLCAlarmTime);
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]准备关闭IO{ioidx}!");
                            ioRet = IOHelper.SetBitOff(ioidx);
                            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]关闭IO{ioidx}完成!");
                            if (!ioRet) _logger.Error($"相机[{CamName}]关闭IO异常!{ioRet.Msg}");
                            _logger.Info($"联机状态,相机[{CamName}]发送NG信号给PLC完成!");
                        }));
                        ioThread.IsBackground = true;
                        ioThread.Start();
                    }
                    else
                    {
                        _logger.Info($"非联机状态,相机[{CamName}]不发送NG信号给PLC!");
                    }
                    return Response<InspResult>.Ok(new InspResult()
                    {
                        result = false,
                        resobj = crossObj
                    });
                }
            }
            catch (Exception ex)
            {
                OutShowImgScore = 0;
                _logger.Info($"相机[{CamName}]检测异常!{ex.Message}");
                return Response<InspResult>.Fail($"相机[{CamName}]检测异常!{ex.Message}");
            }
            finally
            {
                //if (grabImage != null) grabImage.Dispose();
                if (crossObj != null) crossObj.Dispose();
            }
        }

    }


    [Serializable]
    public class InspResult
    {
        public bool result { get; set; } = false;
        public HObject resobj { get; set; } = null;

    }
}
