using BaseData;
using SmartLib;
using HalconDotNet;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SmartVEye
{
    /// <summary>
    /// 3个相机固定排版 忽略DPI
    /// </summary>
    public partial class VisCtrlV4 : VisCtrlBase, IVisCtrl
    {
        #region 参数清单
        public int CtrlNo { get; set; } = 0;//控件编号
        public string CtrlName { get; set; } = "";//控件名称
        public string CamName { get; set; } = "CAM1";
        public bool IsContinueSnap { get; set; } = false;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public int CameraEnable { get; set; } = 1;//是否启用相机
        public int CamNumber { get; set; } = 1;//相机编号，只显示
        public bool IsTrainMode { get; set; } = false;

        #endregion

        public VisCtrlV4()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            WinCtrl.InitWindow();
            WinCtrl.WinMouseDownEvent += new HMouseEventHandler(VisWinMouseDown);
        }
        ~VisCtrlV4()
        {
            WinCtrl.WinMouseDownEvent -= new HMouseEventHandler(VisWinMouseDown);
        }
        public void VisWinMouseDown(object sender, HMouseEventArgs e)
        {
            try
            {
                if (!IsTrainMode) return;
                WinCtrl.Ctrl_DelAllROI();
                WinCtrl.Ctrl_CreateRectROI(e.Y, e.X, TrainROIWidth, TrainROIHeight);
                TrainROIRow = e.Y;
                TrainROICol = e.X;
            }
            catch (Exception ex)
            {
                _logger.Error($"鼠标点击移动ROI异常!{ex.Message}");
            }
        }
        private void VisCtrl_Load(object sender, EventArgs e)
        {
            ShowProRecord();
            ChkBackcolorSet();
        }
        public void StopContiSnap()
        {
            IsContinueSnap = false;
        }
        public Response InitCamera(string CamName)
        {
            if (CommonData.CameraType == 0)
            {
                CurHikCam = new CamHik();
                Response ret = CurHikCam.CamOpen(CamName);
                CurHikCam.SetLineTrig();
                //CurHikCam.SetExposTime(Exposure);
                if (cb_WhitePage.Checked)        //lcl 白页检测时修改曝光
                {
                    CurHikCam.SetExposTime(WhitePageExposure);
                }
                else
                {
                    CurHikCam.SetExposTime(Exposure);
                }
                CurHikCam.SetGain(Gain);
                CurHikCam.PostFrameEvent += CurCam_PostFrameEvent;
                CommonData.PostReadFrameEvent += CurCam_PostFrameEvent;
                if (!ret) return ret;
                ret = CurHikCam.StartGrab();
                if (!ret) return ret;
            }
            else if (CommonData.CameraType == 1)
            {
                CurBaslerCam = new CamBasler();
                Response ret = CurBaslerCam.CamOpen(CamName);
                CurBaslerCam.SetLineTrig();
                //CurBaslerCam.SetExposTime(Exposure);
                if (cb_WhitePage.Checked)        //lcl 白页检测时修改曝光
                {
                    CurBaslerCam.SetExposTime(WhitePageExposure);
                }
                else
                {
                    CurBaslerCam.SetExposTime(Exposure);
                }
                CurBaslerCam.SetGain(Gain);
                CurBaslerCam.PostFrameEvent += CurCam_PostFrameEvent;
                CommonData.PostReadFrameEvent += CurCam_PostFrameEvent;
                if (!ret) return ret;
                ret = CurBaslerCam.StartGrab();
                if (!ret) return ret;
            }
            return Response.Ok();
        }
        public void UnInitCamera()
        {
            if (CameraEnable > 0 && CurHikCam != null)
            {
                CurHikCam.SetSoftTrig(); _logger.Info($"相机[{CamName}]设置为软触发!");
                Thread.Sleep(100);
                CurHikCam.StopGrab(); _logger.Info($"相机[{CamName}]停止采集!");
                CurHikCam.CamClose(); _logger.Info($"相机[{CamName}]关闭!");
            }
        }
        public void StopConiSnapForAuto()
        {
            IsContinueSnap = false;
            Thread.Sleep(10);
            if (CommonData.CameraType == 0)
            {
                CurHikCam.SetLineTrig();
            }
            else if (CommonData.CameraType == 1)
            {
                CurBaslerCam.StopGrab();
                CurBaslerCam.SetLineTrig();
                CurBaslerCam.StartGrab();
            }
            if (cb_RealImg.IsHandleCreated)
            {
                cb_RealImg.Invoke(new Action(() =>
                {
                    cb_RealImg.Checked = false;
                }));
            }
            if (CommonData.IsSaveRunLog > 1)
            {
                _logger.Info($"相机[{CamName}]关闭视频模式!");
            }
        }
        public void SetCamEnable()
        {
            if (cb_CamEnable.IsHandleCreated)
            {
                cb_CamEnable.Invoke(new Action(() =>
                {
                    if (CameraEnable > 0) cb_CamEnable.Checked = true;
                    else cb_CamEnable.Checked = false;
                }));
            }
        }
        public void ConnectPLC()
        {
            if (cb_WorkOnLine.IsHandleCreated)
            {
                cb_WorkOnLine.Invoke(new Action(() =>
                {
                    cb_WorkOnLine.Checked = true;
                    cb_WorkOnLine_MouseUp(null, null);
                }));
            }
        }
        public void DisConnectPLC()
        {
            if (cb_WorkOnLine.IsHandleCreated)
                cb_WorkOnLine.Invoke(new Action(() =>
                {
                    cb_WorkOnLine.Checked = false;
                    cb_WorkOnLine_MouseUp(null, null);
                }));
        }

        private void CurCam_PostFrameEvent(HObject grabImage)
        {
            if (!CommonData.AuthorityValid)//软件未注册不生产
            {
                grabImage?.Dispose();
                return;
            }
            if (IsTrainMode)
            {
                grabImage?.Dispose();
                return;
            }
            Stopwatch imgProcTimer = new Stopwatch();
            try
            {
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]收到图像,开始处理!");
                if (CommonData.IsLearning)
                {
                    grabImage.Dispose();
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]正在学习,不处理图像,释放图像返回!");
                    return;
                }
                imgProcTimer.Start();
                Response<int> roiNumRet = WinCtrl.GetROICount();
                if (roiNumRet && roiNumRet.Data > 0)
                {
                    //原来ROI位置
                    Response<HTuple> roiObjRet = WinCtrl.GetROISize(0);
                    oldROIRow = (int)((roiObjRet.Data.TupleSelect(0).D + roiObjRet.Data.TupleSelect(2).D) / 2);
                    oldROICol = (int)((roiObjRet.Data.TupleSelect(1).D + roiObjRet.Data.TupleSelect(3).D) / 2);
                    oldROILen1 = (int)(roiObjRet.Data.TupleSelect(3).D - roiObjRet.Data.TupleSelect(1).D);
                    oldROILen2 = (int)(roiObjRet.Data.TupleSelect(2).D - roiObjRet.Data.TupleSelect(0).D);
                }
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]获取ROI完成!");
                //图像缩放处理
                double mult = 255 / ((double)ImgLight - (double)ImgDark);
                double add = -1 * mult * (double)ImgDark;
                HOperatorSet.ScaleImage(grabImage, out grabImage, mult, add);
                //
                WinCtrl.ClearDispObj();
                WinCtrl.ClearROIList();
                WinCtrl.DisplayImage(grabImage);
                if (CommonData.IsSaveImg > 0) Task.Run(() =>
                {
                    if (!Directory.Exists($"D:\\Imgs\\{CamName}")) Directory.CreateDirectory($"D:\\Imgs\\{CamName}");
                    //获取文件数量
                    string[] AllFiles = Directory.GetFiles($"D:\\Imgs\\{CamName}");
                    if (AllFiles.Length <= 1000)
                    {
                        HOperatorSet.WriteImage(grabImage, "bmp", 0, $"D:\\Imgs\\{CamName}\\{DateTime.Now.ToString("HH_mm_ss_fff")}.bmp");
                        if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]保存图像完成!");
                    }
                });
                if (IsContinueSnap) return;
                if (CommonData.IsLearning) return;
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]接收到图像");
                if (!btn_Train.Enabled)
                {
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]正在学习,不处理图像!");
                    return;
                }
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]接收到图像");
                HTuple imgWidth = new HTuple(), imgHeight = new HTuple();
                if (cb_RealImg.Checked)
                {
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]正在实时图像，不进行处理!");
                    WinCtrl.ClearROIList();
                    WinCtrl.DisplayBuffer();
                    return;
                }
                if (cb_WorkOnLine.Checked && ROIStepClick)
                {
                    //画ROI
                    WinCtrl.Ctrl_CreateRectROI(TrainROIRow, TrainROICol, TrainROIWidth, TrainROIHeight);
                    ROIStepClick = false;
                    //WinCtrl.DisplayBuffer();
                }
                else//防止非联机状态下点ROI高度宽度变化无变化
                {
                    WinCtrl.Ctrl_CreateRectROI(oldROIRow, oldROICol, oldROILen1, oldROILen2);
                    //WinCtrl.DisplayBuffer();
                }
                if (cb_WorkOnLine.IsHandleCreated)
                {
                    cb_WorkOnLine.Invoke(new Action(() => { IsWorkOnLine = cb_WorkOnLine.Checked; }));
                }
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]正在处理图像，获取相机联网状态!");
                if (!CommonData.IsLearning)
                {
                    //检测方法
                    if (cb_FindByte.Checked)
                    {
                        if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]正在处理图像，当前为文本检测模式!");

                        double OutShowImgScore = 0;
                        Response<InspResult> ret;
                        if (ImageCheckMode == 1)
                        {
                            ret = VisCtrlHelper.ImgModelMatchFunc(CamName, grabImage, WinCtrl, hv_ImgModelHandle,
                                 AngleStart, AngleEnd, MinScale, MaxScale, MinScore,
                                 MinContrast, (int)TrainROIWidth, (int)TrainROIHeight, IsWorkOnLine, ImgScore, out OutShowImgScore);
                        }
                        else
                        {
                            ret = VisCtrlHelper.ModelMatchFunc(CamName, grabImage, WinCtrl, hv_ModelHandle,
                                AngleStart, AngleEnd, MinScale, MaxScale, MinScore,
                                 MinContrast, (int)TrainROIWidth, (int)TrainROIHeight, IsWorkOnLine);
                        }
                        if (ret && ret.Data != null)
                        {
                            lb_ImgSimilarScore.Text = OutShowImgScore.ToString("P2");
                            if (ret.Data.result == true)
                            {
                                SetRes(true);
                                RecordOK += 1;
                            }
                            else
                            {
                                SetRes(false);
                                RecordNG += 1;
                            }
                        }
                        else
                        {
                            _logger.Error($"相机{CamName}检测异常!{ret.Msg}");
                        }
                    }
                    else if (cb_WhitePage.Checked)
                    {
                        if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]正在处理图像，当前为白页检测模式!");
                        Response<InspResult> ret = VisCtrlHelper.PageWhiteFunc(CamName, grabImage, WinCtrl, PageWBContourNum,
                            PageWhite, IsWorkOnLine, TrainROIRow, TrainROICol, TrainROIWidth, TrainROIHeight);
                        if (ret && ret.Data != null)
                        {
                            if (ret.Data.result == true)
                            {
                                SetRes(true);
                                RecordOK += 1;
                            }
                            else
                            {
                                SetRes(false);
                                RecordNG += 1;
                            }
                        }
                        else
                        {
                            _logger.Error($"相机{CamName}检测异常!{ret.Msg}");
                        }
                    }
                    else if (cb_BlackPage.Checked)
                    {
                        if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]正在处理图像，当前为黑页检测模式!");
                        Response<InspResult> ret = VisCtrlHelper.PageBlackFun(CamName, grabImage, WinCtrl, PageWBContourNum,
                            PageBlack, IsWorkOnLine, TrainROIRow, TrainROICol, TrainROIWidth, TrainROIHeight);
                        if (ret && ret.Data != null)
                        {
                            if (ret.Data.result == true)
                            {
                                SetRes(true);
                                RecordOK += 1;
                            }
                            else
                            {
                                SetRes(false);
                                RecordNG += 1;
                            }
                        }
                        else
                        {
                            _logger.Error($"相机{CamName}检测异常!{ret.Msg}");
                        }
                    }
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]正在处理图像，检测完成!");
                    ShowProRecord();
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]正在处理图像，检测完成，显示生产记录完成!");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("图像处理异常!" + ex.Message);
            }
            finally
            {
                if (grabImage != null) grabImage.Dispose();
                imgProcTimer.Stop();
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]检测耗时:{imgProcTimer.ElapsedMilliseconds}ms");
            }
        }

        public Response ReadModel()
        {
            try
            {
                AngleStart = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "anglestart", -10);
                AngleEnd = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "angleend", 20);
                MinScale = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "minscale", 0.8);
                MaxScale = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "maxscale", 1.2);
                MinScore = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "score", 0.8);
                MinContrast = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "contrast", 20);
                MinEdgeLen = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "minedgelen", 6);
                ImgLight = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "imglight", 255);
                ImgDark = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "imgdark", 0);
                CameraEnable = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "enable", 1);
                CamNumber = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "number", 1);
                TrainROIWidth = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "roiwidth", 200);
                TrainROIHeight = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "roiheight", 100);
                PageWhite = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "white", 180);
                PageBlack = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "black", 80);
                TrainROIRow = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "trainrow", 100);
                TrainROICol = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "traincol", 100);
                TrainROIWidth = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "trainlen1", 100);
                TrainROIHeight = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "trainlen2", 100);
                CheckMode = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "checkmode", 0);
                WhitePageExposure = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "whitePageExposure", 400);   //lcl 白页检测曝光
                ImgScore = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "imgscore", 90);   //lcl 图片相似度
                ImageCheckMode = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "imagecheckmode", 0);   //lcl 图像检测模式，0轮廓对比，1图像对比

                PageWBContourNum = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "wbcontour", 1);
                if (cb_WhitePage.IsHandleCreated)
                {
                    cb_WhitePage.Invoke(new Action(() =>
                    {
                        if (CheckMode == 0) { cb_FindByte.Checked = true; cb_WhitePage.Checked = false; cb_BlackPage.Checked = false; }
                        else if (CheckMode == 1) { cb_WhitePage.Checked = true; cb_FindByte.Checked = false; cb_BlackPage.Checked = false; }
                        else if (CheckMode == 2) { cb_BlackPage.Checked = true; cb_FindByte.Checked = false; cb_WhitePage.Checked = false; }
                    }));
                }
                oldROIRow = TrainROIRow;
                oldROICol = TrainROICol;
                oldROILen1 = TrainROIWidth;
                oldROILen2 = TrainROIHeight;

                Exposure = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "Exposure", 100);
                Gain = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "Gain", 0);

                //写参数 防止没有默认值
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "anglestart", AngleStart);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "angleend", AngleEnd);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "minscale", MinScale);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "maxscale", MaxScale);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "score", MinScore);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "contrast", MinContrast);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "minedgelen", MinEdgeLen);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "enable", CameraEnable);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "number", CamNumber);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "roiwidth", TrainROIWidth);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "roiheight", TrainROIHeight);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "white", PageWhite);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "black", PageBlack);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "Exposure", Exposure);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "Gain", Gain);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainrow", TrainROIRow);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "traincol", TrainROICol);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainlen1", TrainROIWidth);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainlen2", TrainROIHeight);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "wbcontour", PageWBContourNum);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "imglight", ImgLight);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "imgdark", ImgDark);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "whitePageExposure", WhitePageExposure);   //lcl 白页检测曝光
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "imgscore", ImgScore);   //lcl 图片相似度
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "imagecheckmode", ImageCheckMode);   //lcl 图像检测模式，0轮廓对比，1图像对比

                //要求每次打开软件记录都为0
                if (NeedClearRecord)
                {
                    RecordOK = 0;
                    RecordNG = 0;
                }
                ShowProRecord();
                SetRes(true);
                if (ImageCheckMode == 1)
                {
                    HOperatorSet.ReadDeformableModel($"{CommonData.CurProPath}\\{CamName}.md", out hv_ImgModelHandle);//读取图像模板
                }
                else
                {
                    hv_ModelHandle.ReadShapeModel($"{CommonData.CurProPath}\\{CamName}.md");   ////读取轮廓模板
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"相机[{CamName}]模板读取异常!{ex.Message}");
                return Response.Fail($"相机[{CamName}]模板读取异常!{ex.Message}");
            }
        }

        public void ShowProRecord()
        {
            try
            {
                if (dgv_ProRecord.IsHandleCreated)
                {
                    dgv_ProRecord.Invoke(new Action(() =>
                    {
                        while (dgv_ProRecord.RowCount > 0)
                            dgv_ProRecord.Rows.RemoveAt(0);
                        dgv_ProRecord.Rows.Add(new string[] { "OK数：", $"{RecordOK}" });
                        dgv_ProRecord.Rows.Add(new string[] { "NG数：", $"{RecordNG}" });
                        int total = RecordOK + RecordNG;
                        double ngRate = total == 0 ? 0 : (((double)RecordNG / (double)total));
                        string ngRateStr = (ngRate * 100).ToString("0.00") + "%";
                        dgv_ProRecord.Rows.Add(new string[] { "总数：", $"{RecordOK + RecordNG}" });
                        dgv_ProRecord.Rows.Add(new string[] { "NG率：", $"{ngRateStr}" });
                        dgv_ProRecord.Rows[0].Selected = false;
                    }));
                }
            }
            catch (Exception ex)
            {
                _logger.Error("显示计数信息异常!" + ex.Message);
            }
        }
        public void ClearProRecord()
        {
            RecordOK = 0;
            RecordNG = 0;
            IniFileHelper.SaveINI(CommonData.RecordFilePath, "recordok", CamName, 0);
            IniFileHelper.SaveINI(CommonData.RecordFilePath, "recordng", CamName, 0);
            ShowProRecord();
        }
        public void SetRes(int number, bool result)
        {
            if (lbl_CamIndex.IsHandleCreated)
            {
                lbl_CamIndex.Invoke(new Action(() =>
                {
                    lbl_CamIndex.Text = number.ToString();
                }));
            }
            if (lbl_Res.IsHandleCreated)
            {
                lbl_Res.Invoke(new Action(() =>
                {
                    lbl_Res.Text = result ? "OK" : "NG";
                    lbl_Res.BackColor = result ? Color.LimeGreen : Color.Red;
                }));
            }
        }
        public void SetRes(bool result)
        {
            if (lbl_CamIndex.IsHandleCreated)
                lbl_CamIndex.Invoke(new Action(() =>
                {
                    lbl_CamIndex.Text = CamNumber.ToString();
                }));
            if (lbl_Res.IsHandleCreated)
                lbl_Res.Invoke(new Action(() =>
                {
                    lbl_Res.Text = result ? "OK" : "NG";
                    lbl_Res.BackColor = result ? Color.LimeGreen : Color.Red;
                }));
        }

        private void btn_Train_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsTrainMode) return;
                CommonData.IsLearning = true;
                btn_Train.Enabled = false;
                Thread.Sleep(100);
                if (cb_RealImg.Checked)
                {
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,处于视频模式,开始关闭视频模式!");
                    cb_RealImg.Checked = false;
                    cb_RealImg_MouseUp(null, null);
                    Thread.Sleep(100);
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]视频模式已关闭!");
                }
                Response<HObject> imgRet = WinCtrl.GetWindowImage();
                if (!imgRet)
                {
                    MessageBox.Show("窗口中没有图像,请触发拍照!");
                    return;
                }
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,获取窗口图像成功!");
                Response<List<ROI>> roiRet = WinCtrl.GetROIList();
                HTuple imgW = new HTuple(), imgH = new HTuple();
                imgW = 0;
                imgH = 0;
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,获取窗口图像成功!");
                HOperatorSet.GetImageSize(imgRet.Data, out imgW, out imgH);
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,获取窗口图像尺寸成功!");
                if (!roiRet || roiRet.Data.Count <= 0)
                {
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,窗口中无ROI,创建ROI!");
                    WinCtrl.Ctrl_CreateRectROI(imgH / 2, imgW / 2, TrainROIWidth, TrainROIHeight);
                    WinCtrl.DisplayBuffer();
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,创建ROI后返回!");
                    return;
                }
                //重新刷新窗口 消除无用的内容
                WinCtrl.DisplayImage(imgRet.Data);
                WinCtrl.DisplayBuffer();
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,图像及ROI获取完成,开始执行学习方法!");
                //训练
                Response trainRet;
                if (ImageCheckMode == 1)
                {
                    trainRet = TrainModelImage();//图像训练
                }
                else
                {
                    trainRet = TrainModel();//轮廓训练
                }
                if (!trainRet)
                {
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,模板学习异常!{trainRet.Msg}");
                    btn_Train.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("no");
                    MessageBox.Show("模板学习异常!" + trainRet.Msg);
                    ImageCheckMode = 1;   //建模不成功就默认成图像对比模式
                }
                else
                {
                    if (CommonData.IsHandOffline == 0)
                    {
                        cb_WorkOnLine.Checked = true;
                    }
                    btn_Train.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("yes");
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,训练成功!");
                }
            }
            catch (Exception ex)
            {
                btn_Train.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("no");
                _logger.Error("学习异常!" + ex.Message);
                MessageBox.Show("学习异常!" + ex.Message);
            }
            finally
            {
                Thread.Sleep(50);
                btn_Train.Enabled = true;
                CommonData.IsLearning = false;
                IsTrainMode = false;
                btn_TrainMode.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("no");
                btn_TrainMode.BackColor = SystemColors.ActiveCaption;
                DisplayOperateBtn(false);
            }
        }

        public Response TrainModelAuto()
        {
            try
            {
                if (!IsTrainMode) return Response.Ok();
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]开始进入一键学习!");
                if (cb_WorkOnLine.IsHandleCreated)
                    cb_WorkOnLine.Invoke(new Action(() =>
                    {
                        cb_WorkOnLine.Checked = false;
                        IsWorkOnLine = false;
                        Thread.Sleep(50);
                        if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]一键学习,取消联机模式!");
                    }));
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]一键学习,开始获取窗口图像!");
                Response<HObject> imgRet = WinCtrl.GetWindowImage();
                if (!imgRet) return Response.Fail("窗口中没有图像,请触发拍照!");
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]一键学习,获取窗口图像成功!");
                WinCtrl.ClearALLWindow();
                WinCtrl.DisplayImage(imgRet.Data);
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]一键学习,获取窗口图像尺寸成功!");
                //本地读取图片测试用
                WinCtrl.Ctrl_CreateRectROI(TrainROIRow, TrainROICol, TrainROIWidth, TrainROIHeight);
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]一键学习,创建ROI;{TrainROIRow};{TrainROICol};{TrainROIWidth};{TrainROIHeight}!");
                WinCtrl.DisplayBuffer();
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]一键学习,开始执行训练步骤!");
                //训练
                Response trainRet;
                if (ImageCheckMode == 1)
                {
                    trainRet = TrainModelImage();//图像训练
                }
                else
                {
                    trainRet = TrainModel();//轮廓训练
                }
                if (!trainRet)
                {
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,模板学习异常!{trainRet.Msg}");
                    btn_Train.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("no");
                    MessageBox.Show("模板学习异常!" + trainRet.Msg);
                    ImageCheckMode = 1;   //建模不成功就默认成图像对比模式
                    return Response.Fail("模板学习异常!" + trainRet.Msg);
                }
                else
                {
                    if (CommonData.IsHandOffline == 0)
                    {
                        cb_WorkOnLine.Checked = true;
                    }
                    btn_Train.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("yes");
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,训练成功!");
                    return Response.Ok();
                }
            }
            catch (Exception ex)
            {
                return Response.Fail("一键学习异常!" + ex);
            }
            finally
            {
                Thread.Sleep(50);
                btn_Train.Enabled = true;
                CommonData.IsLearning = false;
                IsTrainMode = false;
                btn_TrainMode.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("no");
                btn_TrainMode.BackColor = SystemColors.ActiveCaption;
            }
        }
        private Response TrainModel(int imgW = 0, int imgH = 0)
        {
            //检测模式
            int checkmode = 0;
            if (cb_FindByte.Checked) checkmode = 0;
            else if (cb_WhitePage.Checked) checkmode = 1;
            else if (cb_BlackPage.Checked) checkmode = 2;
            IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "checkmode", checkmode);
            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习模式:{checkmode}!");
            //执行训练
            if (cb_BlackPage.Checked || cb_WhitePage.Checked)
            {
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]开始执行模式{checkmode}学习!");
                Response<int> roiRet = WinCtrl.GetROICount();
                if (!roiRet || roiRet.Data <= 0)
                {
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,窗口未添加ROI!{roiRet.Msg}!");
                    return Response.Fail("窗口未添加区域!" + roiRet.Msg);
                }
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,窗口ROI数量:{roiRet.Data}!");
                //保存ROI坐标
                Response<HTuple> roiObjRet = WinCtrl.GetROISize(0);
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,获取ROI0尺寸成功!{roiObjRet.Data}");
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,获取ROI0尺寸成功!{roiObjRet.Data.TupleSelect(0).D};" +
                    $"{roiObjRet.Data.TupleSelect(1).D};{roiObjRet.Data.TupleSelect(2).D};{roiObjRet.Data.TupleSelect(3).D}");
                TrainROIRow = (roiObjRet.Data.TupleSelect(0).D + roiObjRet.Data.TupleSelect(2).D) / 2;
                TrainROICol = (roiObjRet.Data.TupleSelect(1).D + roiObjRet.Data.TupleSelect(3).D) / 2;
                TrainROIWidth = roiObjRet.Data.TupleSelect(3).D - roiObjRet.Data.TupleSelect(1).D;
                TrainROIHeight = roiObjRet.Data.TupleSelect(2).D - roiObjRet.Data.TupleSelect(0).D;
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainrow", (int)TrainROIRow);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "traincol", (int)TrainROICol);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainlen1", (int)TrainROIWidth);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainlen2", (int)TrainROIHeight);
                HOperatorSet.GenCrossContourXld(out HObject cross, TrainROIRow, TrainROICol, 300, 0);
                WinCtrl.DisplayObject(cross, "red");
                WinCtrl.DisplayBuffer();
                btn_TrainMode.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("yes");
                return Response.Ok();
            }
            //模板匹配
            HObject ImgEdges, SelectContours;
            HOperatorSet.GenEmptyObj(out ImgEdges);
            HOperatorSet.GenEmptyObj(out SelectContours);
            Stopwatch findWatch = new Stopwatch();
            try
            {
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]开始执行模式{checkmode}学习!");
                Response<int> roiRet = WinCtrl.GetROICount();
                if (!roiRet || roiRet.Data <= 0)
                {
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,窗口未添加ROI!{roiRet.Msg}!");
                    return Response.Fail("窗口未添加区域!" + roiRet.Msg);
                }
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,窗口ROI数量:{roiRet.Data}!");
                //保存ROI坐标
                Response<HTuple> roiObjRet = WinCtrl.GetROISize(0);
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,获取ROI0尺寸成功!{roiObjRet.Data}");
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,获取ROI0尺寸成功!{roiObjRet.Data.TupleSelect(0).D};" +
                    $"{roiObjRet.Data.TupleSelect(1).D};{roiObjRet.Data.TupleSelect(2).D};{roiObjRet.Data.TupleSelect(3).D}");
                TrainROIRow = (roiObjRet.Data.TupleSelect(0).D + roiObjRet.Data.TupleSelect(2).D) / 2;
                TrainROICol = (roiObjRet.Data.TupleSelect(1).D + roiObjRet.Data.TupleSelect(3).D) / 2;
                TrainROIWidth = roiObjRet.Data.TupleSelect(3).D - roiObjRet.Data.TupleSelect(1).D;
                TrainROIHeight = roiObjRet.Data.TupleSelect(2).D - roiObjRet.Data.TupleSelect(0).D;
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainrow", (int)TrainROIRow);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "traincol", (int)TrainROICol);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainlen1", (int)TrainROIWidth);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainlen2", (int)TrainROIHeight);
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,训练参数保存成功!");
                //训练图像
                Response<HObject> imgRet = WinCtrl.GetActiveImage();
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,获取激活图像成功!");
                if (!imgRet) return Response.Fail("获取激活图像异常!" + roiRet.Msg);
                HOperatorSet.EdgesSubPix(imgRet.Data, out ImgEdges, "canny", 1, 10, 35);
                HOperatorSet.SelectContoursXld(ImgEdges, out ImgEdges, "contour_length", MinEdgeLen, 99999, MinEdgeLen, 99999);
                HOperatorSet.CountObj(ImgEdges, out HTuple number);
                if (number <= 0) return Response.Fail("特征轮廓提取数量不足,请调整区域后重试!");
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,轮廓提取成功!");
                HOperatorSet.SelectContoursXld(ImgEdges, out SelectContours, "contour_length", 10, 9999999, 10, 9999999);
                HOperatorSet.CountObj(SelectContours, out number);
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,轮廓筛选及计数成功!");
                if (number <= 0) return Response.Fail("特征轮廓筛选数量不足,请调整区域后重试!");
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板训练获取轮廓正常!");
                //读取最新参数
                AngleStart = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "anglestart", -1);
                AngleEnd = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "angleend", 1);
                MinScale = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "minscale", 0.9);
                MaxScale = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "maxscale", 1.1);
                MinContrast = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "min_contrast", 10);
                MinScore = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "score", 0.8);
                Exposure = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "Exposure", 1000);
                Gain = IniFileHelper.ReadINI(CommonData.SetFilePath, CamName, "Gain", 19);
                HTuple radStart, radEnd;
                HOperatorSet.TupleRad((double)AngleStart, out radStart);
                HOperatorSet.TupleRad((double)AngleEnd, out radEnd);
                if (hv_ModelHandle == null) hv_ModelHandle = new HShapeModel();
                if (hv_ModelHandle != null) hv_ModelHandle.Dispose();
                hv_ModelHandle.CreateGenericShapeModel();
                hv_ModelHandle.SetGenericShapeModelParam("min_score", MinScore);
                hv_ModelHandle.SetGenericShapeModelParam("num_matches", 1);
                hv_ModelHandle.SetGenericShapeModelParam("timeout", CommonData.MatchTimeOut);
                hv_ModelHandle.SetGenericShapeModelParam("angle_start", radStart);
                hv_ModelHandle.SetGenericShapeModelParam("angle_end", radEnd);
                hv_ModelHandle.SetGenericShapeModelParam("num_levels", "auto");
                hv_ModelHandle.SetGenericShapeModelParam("greediness", 0.5);
                hv_ModelHandle.SetGenericShapeModelParam("max_overlap", 0);
                hv_ModelHandle.SetGenericShapeModelParam("iso_scale_min", (HTuple)((float)MinScale));
                hv_ModelHandle.SetGenericShapeModelParam("iso_scale_max", (HTuple)((float)MaxScale));
                hv_ModelHandle.TrainGenericShapeModel(SelectContours);
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,模板训练成功!");
                string modelPath = $"{CommonData.CurProPath}\\{CamName}.md";
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,模板保存成功!");
                if (File.Exists(modelPath)) File.Delete(modelPath);
                hv_ModelHandle.WriteShapeModel(modelPath);
                findWatch.Start();
                HGenericShapeModelResult matchResult = hv_ModelHandle.FindGenericShapeModel(imgRet.Data, out int ho_MatchNum);
                if (ho_MatchNum > 0)//OK品
                {
                    HTuple curRow = matchResult.GetGenericShapeModelResult("all", "row");
                    HTuple curColumn = matchResult.GetGenericShapeModelResult("all", "column");
                    HObject curContours = matchResult.GetGenericShapeModelResultObject("all", "contours");
                    HOperatorSet.GenCrossContourXld(out HObject cross, curRow, curColumn, 300, 0);
                    WinCtrl.DisplayObject(cross, "red");
                    WinCtrl.DisplayObject(curContours, "red");
                    WinCtrl.DisplayBuffer();
                    if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,模板训练后查找成功!");
                    btn_TrainMode.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("yes");
                    findWatch.Stop();
                    long findTime = findWatch.ElapsedMilliseconds;
                    if (findTime >= 800)
                    {
                        return Response.Fail($"当前所学习的模板查找耗时为[{findTime}ms],大于800ms,请优化参数!");
                    }
                    else
                    {
                        return Response.Ok();
                    }
                }
                else
                {
                    if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,模板训练成功,但未查询到模板!");
                    btn_TrainMode.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("no");
                    return Response.Fail("模板训练成功,但未查询到模板!");
                }
            }
            catch (Exception ex)
            {
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,模板训练异常!" + ex.Message);
                btn_TrainMode.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("no");
                return Response.Fail("模板训练异常!" + ex.Message);
            }
            finally
            {
                if (ImgEdges != null) ImgEdges.Dispose();
                if (SelectContours != null) SelectContours.Dispose();
            }
        }
        private Response TrainModelImage()
        {
            //检测模式
            int checkmode = 0;
            if (cb_FindByte.Checked) checkmode = 0;
            else if (cb_WhitePage.Checked) checkmode = 1;
            else if (cb_BlackPage.Checked) checkmode = 2;
            IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "checkmode", checkmode);
            if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习模式:{checkmode}!");
            //执行训练
            if (cb_BlackPage.Checked || cb_WhitePage.Checked)
            {
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]开始执行模式{checkmode}学习!");
                Response<int> roiRet = WinCtrl.GetROICount();
                if (!roiRet || roiRet.Data <= 0)
                {
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,窗口未添加ROI!{roiRet.Msg}!");
                    return Response.Fail("窗口未添加区域!" + roiRet.Msg);
                }
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,窗口ROI数量:{roiRet.Data}!");
                //保存ROI坐标
                Response<HTuple> roiObjRet = WinCtrl.GetROISize(0);
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,获取ROI0尺寸成功!{roiObjRet.Data}");
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,获取ROI0尺寸成功!{roiObjRet.Data.TupleSelect(0).D};" +
                    $"{roiObjRet.Data.TupleSelect(1).D};{roiObjRet.Data.TupleSelect(2).D};{roiObjRet.Data.TupleSelect(3).D}");
                TrainROIRow = (roiObjRet.Data.TupleSelect(0).D + roiObjRet.Data.TupleSelect(2).D) / 2;
                TrainROICol = (roiObjRet.Data.TupleSelect(1).D + roiObjRet.Data.TupleSelect(3).D) / 2;
                TrainROIWidth = roiObjRet.Data.TupleSelect(3).D - roiObjRet.Data.TupleSelect(1).D;
                TrainROIHeight = roiObjRet.Data.TupleSelect(2).D - roiObjRet.Data.TupleSelect(0).D;
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainrow", (int)TrainROIRow);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "traincol", (int)TrainROICol);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainlen1", (int)TrainROIWidth);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainlen2", (int)TrainROIHeight);
                HOperatorSet.GenCrossContourXld(out HObject cross, TrainROIRow, TrainROICol, 300, 0);
                WinCtrl.DisplayObject(cross, "red");
                WinCtrl.DisplayBuffer();
                btn_TrainMode.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("yes");
                return Response.Ok();
            }
            //模板匹配
            HObject ImgEdges, SelectContours;
            HOperatorSet.GenEmptyObj(out ImgEdges);
            HOperatorSet.GenEmptyObj(out SelectContours);
            Stopwatch findWatch = new Stopwatch();
            try
            {
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]开始执行模式{checkmode}学习!");
                Response<int> roiRet = WinCtrl.GetROICount();
                if (!roiRet || roiRet.Data <= 0)
                {
                    if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,窗口未添加ROI!{roiRet.Msg}!");
                    return Response.Fail("窗口未添加区域!" + roiRet.Msg);
                }
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,窗口ROI数量:{roiRet.Data}!");
                //保存ROI坐标
                Response<HTuple> roiObjRet = WinCtrl.GetROISize(0);
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,获取ROI0尺寸成功!{roiObjRet.Data}");
                if (CommonData.IsSaveRunLog > 0) _logger.Info($"相机[{CamName}]学习,获取ROI0尺寸成功!{roiObjRet.Data.TupleSelect(0).D};" +
                    $"{roiObjRet.Data.TupleSelect(1).D};{roiObjRet.Data.TupleSelect(2).D};{roiObjRet.Data.TupleSelect(3).D}");
                TrainROIRow = (roiObjRet.Data.TupleSelect(0).D + roiObjRet.Data.TupleSelect(2).D) / 2;
                TrainROICol = (roiObjRet.Data.TupleSelect(1).D + roiObjRet.Data.TupleSelect(3).D) / 2;
                TrainROIWidth = roiObjRet.Data.TupleSelect(3).D - roiObjRet.Data.TupleSelect(1).D;
                TrainROIHeight = roiObjRet.Data.TupleSelect(2).D - roiObjRet.Data.TupleSelect(0).D;
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainrow", (int)TrainROIRow);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "traincol", (int)TrainROICol);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainlen1", (int)TrainROIWidth);
                IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "trainlen2", (int)TrainROIHeight);
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,训练参数保存成功!");
                //训练图像
                Response<HObject> imgRet = WinCtrl.GetActiveImage();
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,获取激活图像成功!");
                if (!imgRet) return Response.Fail("获取激活图像异常!" + roiRet.Msg);

                if (hv_ImgModelHandle == null) hv_ImgModelHandle = new HTuple();
                if (hv_ImgModelHandle != null) hv_ImgModelHandle.Dispose();
                HOperatorSet.CreateLocalDeformableModel(imgRet.Data, "auto", new HTuple(),
    new HTuple(), "auto", 1, new HTuple(), "auto", 1, new HTuple(), "auto", "none",
    "use_polarity", "auto", "auto", new HTuple(), new HTuple(), out hv_ImgModelHandle);
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,模板训练成功!");
                string modelPath = $"{CommonData.CurProPath}\\{CamName}.md";
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,模板保存成功!");
                if (File.Exists(modelPath)) File.Delete(modelPath);
                HOperatorSet.WriteDeformableModel(hv_ImgModelHandle, modelPath);
                //hv_ModelHandle.WriteShapeModel(modelPath);
                findWatch.Start();
                HTuple ho_row = new HTuple(), ho_column = new HTuple();
                HTuple ho_score = new HTuple();
                HTuple hv_Smoothness = 21;
                HObject ho_ImageRectified = new HObject();
                HObject ho_VectorField = new HObject(), ho_DeformedContours = new HObject();
                HOperatorSet.FindLocalDeformableModel(imgRet.Data, out ho_ImageRectified, out ho_VectorField,
        out ho_DeformedContours, hv_ImgModelHandle, 0, 0, 1, 1, 1, 1, 0.5, 1, 1, 4, 0.9,
        ((new HTuple("image_rectified")).TupleConcat("vector_field")).TupleConcat(
        "deformed_contours"), ((new HTuple("deformation_smoothness")).TupleConcat(
        "expand_border")).TupleConcat("subpixel"), hv_Smoothness.TupleConcat((new HTuple(0)).TupleConcat(
        1)), out ho_score, out ho_row, out ho_column);
                //HGenericShapeModelResult matchResult = hv_ModelHandle.FindGenericShapeModel(imgRet.Data, out int ho_MatchNum);
                if (ho_score.Length > 0)//OK品
                {
                    //HTuple curRow = matchResult.GetGenericShapeModelResult("all", "row");
                    //HTuple curColumn = matchResult.GetGenericShapeModelResult("all", "column");
                    //HObject curContours = matchResult.GetGenericShapeModelResultObject("all", "contours");
                    HOperatorSet.GenCrossContourXld(out HObject cross, ho_row, ho_column, 300, 0);
                    WinCtrl.DisplayObject(cross, "red");
                    //WinCtrl.DisplayObject(curContours, "red");
                    WinCtrl.DisplayBuffer();
                    if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,模板训练后查找成功!");
                    btn_TrainMode.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("yes");
                    findWatch.Stop();
                    long findTime = findWatch.ElapsedMilliseconds;
                    if (findTime >= 800)
                    {
                        return Response.Fail($"当前所学习的模板查找耗时为[{findTime}ms],大于800ms,请优化参数!");
                    }
                    else
                    {
                        return Response.Ok();
                    }
                }
                else
                {
                    if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,模板训练成功,但未查询到模板!");
                    btn_TrainMode.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("no");
                    return Response.Fail("模板训练成功,但未查询到模板!");
                }
            }
            catch (Exception ex)
            {
                if (CommonData.IsSaveRunLog > 1) _logger.Info($"相机[{CamName}]模板学习,模板训练异常!" + ex.Message);
                btn_TrainMode.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("no");
                return Response.Fail("模板训练异常!" + ex.Message);
            }
            finally
            {
                if (ImgEdges != null) ImgEdges.Dispose();
                if (SelectContours != null) SelectContours.Dispose();

            }
        }
        private void cb_RealImg_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (CommonData.CameraType == 0)
                {
                    if (cb_RealImg.Checked)
                    {
                        IsContinueSnap = true;
                        Thread.Sleep(20);
                        CurHikCam.StopGrab();
                        Thread.Sleep(20);
                        CurHikCam.SetSoftTrig();
                        Thread.Sleep(20);
                        CurHikCam.StartGrab();
                        SnapThread = new Thread(new ThreadStart(CamContiSnap));
                        SnapThread.IsBackground = true;
                        SnapThread.Start();
                    }
                    else
                    {
                        IsContinueSnap = false;
                        Thread.Sleep(20);
                        CurHikCam.StopGrab();
                        Thread.Sleep(20);
                        CurHikCam.SetLineTrig();
                        Thread.Sleep(20);
                        CurHikCam.StartGrab();
                    }
                }
                else if (CommonData.CameraType == 1)
                {
                    if (cb_RealImg.Checked)
                    {
                        IsContinueSnap = true;
                        Thread.Sleep(20);
                        CurBaslerCam.StopGrab();
                        Thread.Sleep(20);
                        CurBaslerCam.SetSoftTrig();
                        Thread.Sleep(20);
                        CurBaslerCam.StartGrab();
                        SnapThread = new Thread(new ThreadStart(CamContiSnap));
                        SnapThread.IsBackground = true;
                        SnapThread.Start();
                    }
                    else
                    {
                        IsContinueSnap = false;
                        Thread.Sleep(20);
                        CurBaslerCam.StopGrab();
                        Thread.Sleep(20);
                        CurBaslerCam.SetLineTrig();
                        Thread.Sleep(20);
                        CurBaslerCam.StartGrab();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"设置相机视频模式[{cb_RealImg.Checked}]异常!" + ex.Message);
            }
        }
        private void cb_CamEnable_MouseUp(object sender, MouseEventArgs e)
        {
            CameraEnable = cb_CamEnable.Checked ? 1 : 0;
            IniFileHelper.SaveINI(CommonData.SetFilePath, CamName, "enable", CameraEnable);

        }
        private void cb_WorkOnLine_MouseUp(object sender, MouseEventArgs e)
        {
            if (!cb_WorkOnLine.Checked && !FrmMain.IsDisconnectAll)
            {
                if (CommonData.DisConnPLCNeedPWD > 0)
                {
                    FrmPassword frmPassword = new FrmPassword();
                    if (frmPassword.ShowDialog() == DialogResult.OK)
                    {
                        IsWorkOnLine = cb_WorkOnLine.Checked;
                    }
                    else
                    {
                        cb_WorkOnLine.Checked = true;
                    }
                }
                else
                {
                    IsWorkOnLine = cb_WorkOnLine.Checked;
                }
            }
            if (!cb_WorkOnLine.Checked) _logger.Info($"相机[{CamName}]联机被关闭!");
            WinCtrl.Focus();
        }

        private void CamContiSnap()
        {
            try
            {
                while (IsContinueSnap)
                {
                    if (CommonData.CameraType == 0)
                    {
                        this.CurHikCam.SetSoftTrig();
                        Response snapRet = this.CurHikCam.SnapOneImg();
                    }
                    else if (CommonData.CameraType == 1)
                    {
                        //this.CurBaslerCam.SetSoftTrig();
                        Response snapRet = this.CurBaslerCam.SnapOneImg();
                    }
                    //this.CurCam.SetLineTrig();
                    //if (!snapRet) break;
                    Thread.Sleep(CommonData.ContiTrigTime);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("相机连续取图异常!" + ex.Message);
            }
        }

        private void btn_WidthPlus_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsTrainMode) return;
                TrainROIWidth += FrmMain.ROIStep;
                WinCtrl.Ctrl_DelAllROI();
                WinCtrl.Ctrl_CreateRectROI(TrainROIRow, TrainROICol, TrainROIWidth, TrainROIHeight);
                ROIStepClick = true;
            }
            catch (Exception ex)
            {
                _logger.Error("宽度+异常!" + ex.Message);
            }
        }

        private void btn_WidthSub_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsTrainMode) return;
                TrainROIWidth -= FrmMain.ROIStep;
                WinCtrl.Ctrl_DelAllROI();
                WinCtrl.Ctrl_CreateRectROI(TrainROIRow, TrainROICol, TrainROIWidth, TrainROIHeight);
                ROIStepClick = true;
            }
            catch (Exception ex)
            {
                _logger.Error("宽度-异常!" + ex.Message);
            }
        }

        private void btn_HighPlus_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsTrainMode) return;
                TrainROIHeight += FrmMain.ROIStep;
                WinCtrl.Ctrl_DelAllROI();
                WinCtrl.Ctrl_CreateRectROI(TrainROIRow, TrainROICol, TrainROIWidth, TrainROIHeight);
                ROIStepClick = true;
            }
            catch (Exception ex)
            {
                _logger.Error("高度+异常!" + ex.Message);
            }
        }

        private void btn_HighSub_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsTrainMode) return;
                TrainROIHeight -= FrmMain.ROIStep;
                WinCtrl.Ctrl_DelAllROI();
                WinCtrl.Ctrl_CreateRectROI(TrainROIRow, TrainROICol, TrainROIWidth, TrainROIHeight);
                ROIStepClick = true;
            }
            catch (Exception ex)
            {
                _logger.Error("高度-异常!" + ex.Message);
            }
        }
        private void cb_FindByte_MouseUp(object sender, MouseEventArgs e)
        {
            if (MessageBox.Show("是否切换成图文模式？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // 如果用户确定，则取消选中复选框
                cb_FindByte.Checked = true;
                CurHikCam.SetExposTime(Exposure);
            }
            else
            {
                // 如果用户确定，则取消选中复选框
                cb_FindByte.Checked = false;
            }
            if (cb_FindByte.Checked)
            {
                cb_WhitePage.Checked = false;
                cb_BlackPage.Checked = false;
            }
            if (!cb_BlackPage.Checked && !cb_WhitePage.Checked) cb_FindByte.Checked = true;
        }
        private void cb_WhitePage_MouseUp(object sender, MouseEventArgs e)
        {
            if (MessageBox.Show("是否切换成白页模式？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // 如果用户确定，则取消选中复选框
                cb_WhitePage.Checked = true;
                CurHikCam.SetExposTime(WhitePageExposure);
            }
            else
            {
                // 如果用户确定，则取消选中复选框
                cb_WhitePage.Checked = false;
            }
            if (cb_WhitePage.Checked)
            {
                cb_FindByte.Checked = false;
                cb_BlackPage.Checked = false;
            }
            if (!cb_BlackPage.Checked && !cb_WhitePage.Checked) cb_FindByte.Checked = true;
        }

        private void cb_BlackPage_MouseUp(object sender, MouseEventArgs e)
        {
            if (MessageBox.Show("是否切换成黑页模式？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // 如果用户确定，则取消选中复选框
                cb_BlackPage.Checked = true;
                CurHikCam.SetExposTime(Exposure);
            }
            else
            {
                // 如果用户确定，则取消选中复选框
                cb_BlackPage.Checked = false;
            }
            if (cb_BlackPage.Checked)
            {
                cb_FindByte.Checked = false;
                cb_WhitePage.Checked = false;
            }
            if (!cb_BlackPage.Checked && !cb_WhitePage.Checked) cb_FindByte.Checked = true;
        }

        private void btn_Train_MouseDown(object sender, MouseEventArgs e)
        {
            btn_Train.BackColor = Color.Aqua;
        }

        private void btn_Train_MouseUp(object sender, MouseEventArgs e)
        {
            btn_Train.BackColor = SystemColors.ActiveCaption;
        }

        private void VisCtrlV4_Resize(object sender, EventArgs e)
        {
            //btn_WidthPlus.Width = panel1.Width / 4;
            btn_WidthPlus.Width = btn_TrainMode.Width / 2;
            btn_WidthPlus.Width = lbl_CamIndex.Width;
            btn_WidthSub.Width = btn_WidthPlus.Width;
            btn_HighPlus.Width = btn_WidthPlus.Width;
            btn_HighSub.Width = btn_WidthPlus.Width;

            btn_TrainMode.Width = (panel4.Width- dgv_ProRecord.Width) / 2;
            btn_Train.Width = btn_TrainMode.Width;

        }

        public void TrainModeIn()
        {
            btn_TrainMode_Click(null, null);
        }

        private void btn_TrainMode_Click(object sender, EventArgs e)
        {
            IsTrainMode = true;
            cb_WorkOnLine.Checked = false;
            IsWorkOnLine = false;
            btn_TrainMode.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("yes");
            btn_TrainMode.BackColor = Color.Aqua;
            DisplayOperateBtn(true);
        }

        /// <summary>
        /// 执行按钮操作
        /// </summary>
        public void TrainOperateBtn()
        {
            DisplayOperateBtn(false);
        }

        /// <summary>
        /// LCL20240818 建模时控件显示，学习时隐藏
        /// </summary>
        public void DisplayOperateBtn(bool type)
        {
            if (type == true)
            {
                btn_WidthSub.Visible = true;
                btn_WidthPlus.Visible = true;
                btn_HighPlus.Visible = true;
                btn_HighSub.Visible = true;

                //btn_WidthPlus.Location = new Point(0, 310);  //宽+
                //btn_WidthSub.Location = new Point(865, 310);  //宽-
                //btn_HighPlus.Location = new Point(450, 48);    //高+
                //btn_HighSub.Location = new Point(450, 660);    //高-


                btn_WidthPlus.Location = new Point(0, 310);  //宽+
                btn_WidthSub.Location = new Point(550, 310);  //宽-
                btn_HighPlus.Location = new Point(250, 48);    //高+
                btn_HighSub.Location = new Point(250, 660);    //高-

                this.Controls.Add(btn_WidthPlus);
                btn_WidthPlus.BringToFront();
                this.Controls.Add(btn_WidthSub);
                btn_WidthSub.BringToFront();
                this.Controls.Add(btn_HighPlus);
                btn_HighPlus.BringToFront();
                this.Controls.Add(btn_HighSub);
                btn_HighSub.BringToFront();

            }
            else
            {
                btn_WidthSub.Visible = false;
                btn_WidthPlus.Visible = false;
                btn_HighPlus.Visible = false;
                btn_HighSub.Visible = false;
            }
        }

        private void cb_WorkOnLine_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_WorkOnLine.Checked)
            {
                cb_WorkOnLine.BackColor = Color.LimeGreen;
            }
            else
            {
                cb_WorkOnLine.BackColor = Color.Salmon;
            }
        }

        private void cb_RealImg_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_RealImg.Checked)
            {
                cb_RealImg.BackColor = Color.LimeGreen;
            }
            else
            {
                cb_RealImg.BackColor = Color.Salmon;
            }
        }

        private void cb_FindByte_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_FindByte.Checked)
            {
                cb_FindByte.BackColor = Color.LimeGreen;
            }
            else
            {
                cb_FindByte.BackColor = Color.Salmon;
            }
        }

        private void cb_WhitePage_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_WhitePage.Checked)
            {
                cb_WhitePage.BackColor = Color.LimeGreen;
            }
            else
            {
                cb_WhitePage.BackColor = Color.Salmon;
            }
        }

        private void cb_BlackPage_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_BlackPage.Checked)
            {
                cb_BlackPage.BackColor = Color.LimeGreen;
            }
            else
            {
                cb_BlackPage.BackColor = Color.Salmon;
            }
        }


        /// <summary>
        /// 复选框背景色默认设置
        /// </summary>
        private void ChkBackcolorSet()
        {
            if (cb_WorkOnLine.Checked) cb_WorkOnLine.BackColor = Color.LimeGreen;
            if (cb_RealImg.Checked) cb_RealImg.BackColor = Color.LimeGreen;
            if (cb_FindByte.Checked) cb_FindByte.BackColor = Color.LimeGreen;
            if (cb_WhitePage.Checked) cb_WhitePage.BackColor = Color.LimeGreen;
            if (cb_BlackPage.Checked) cb_BlackPage.BackColor = Color.LimeGreen;
        }

    }
}
