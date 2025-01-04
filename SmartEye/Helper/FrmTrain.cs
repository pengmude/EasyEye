using BaseData;
using SmartLib;
using HalconDotNet;
using NLog;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SmartVEye
{
    public partial class FrmTrain : Form
    {
        string CurCamName = "";
        CamHik CurCam = null;
        HObject CurImage = null;

        //模板参数
        public int AngleStart = -5;
        public int AngleEnd = 5;
        public double MinScale = 0.9;
        public double MaxScale = 1.1;
        public double MinScore = 0.8;

        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public FrmTrain(string camName, CamHik cam)
        {
            CurCam = cam;
            CurCamName = camName;
            InitializeComponent();
            WinCtrl.InitWindow();
            this.Text = CurCamName;

        }

        private void btn_Snap_Click(object sender, EventArgs e)
        {
            try
            {
                ////本地读取图片测试用
                //HOperatorSet.ReadImage(out CurImage, @"D:\\curimg11.bmp");
                //WinCtrl.DisplayImage(CurImage);
                //WinCtrl.DisplayBuffer();
                //return;

                CurCam.PostFrameEvent += CurCam_PostFrameEvent;
                Response ret = this.CurCam.StopGrab();
                if (!ret) tb_InfoBox.Text = "相机停止采集失败!" + ret.Msg;
                ret = this.CurCam.SetExposTime((float)tb_ExposureTime.Value);
                if (!ret) tb_InfoBox.Text = "设置相机曝光时间失败!" + ret.Msg;
                ret = this.CurCam.SetGain((float)tb_Gain.Value);
                if (!ret) tb_InfoBox.Text = "设置相机增益失败!" + ret.Msg;
                ret = this.CurCam.SetSoftTrig();
                if (!ret) tb_InfoBox.Text = "设置相机软触发模式失败!" + ret.Msg;
                Response snapRet = this.CurCam.SnapOneImg();
                if (!snapRet) tb_InfoBox.Text = "相机拍照失败!" + snapRet.Msg;

                IniFileHelper.SaveINI(CommonData.SetFilePath, CurCamName, "exposure", tb_ExposureTime.Value.ToString());
                IniFileHelper.SaveINI(CommonData.SetFilePath, CurCamName, "gain", tb_Gain.Value.ToString());
            }
            catch (Exception ex)
            {
                tb_InfoBox.Text = "相机拍照异常!" + ex.Message;
            }
            finally
            {
                this.CurCam.SetLineTrig();
            }
        }

        private void CurCam_PostFrameEvent(HObject grabImage)
        {
            CurCam.PostFrameEvent -= CurCam_PostFrameEvent;
            CurImage = grabImage.Clone();
            WinCtrl.DisplayImage(CurImage);
            WinCtrl.DisplayBuffer();
            if (grabImage != null)
            {
                grabImage.Dispose();
            }
        }

        private void btn_Train_Click(object sender, EventArgs e)
        {
            HTuple hv_Row1 = new HTuple(), hv_Row2 = new HTuple();
            HTuple hv_Column1 = new HTuple(), hv_Column2 = new HTuple();
            HShapeModel hv_ModelHandle = new HShapeModel();

            HObject ho_Rectangle, ho_ImageReduced;
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);

            try
            {
                if (cb_DrawROI.Checked)
                {
                    Response<int> roiRet = WinCtrl.GetActiveROIIdx();
                    if (roiRet.Data < 0)
                    {
                        MessageBox.Show("当前处于自定义区域模式,请框选自定义区域再操作!");
                        return;
                    }
                    Response<ROI> roiRRet = WinCtrl.GetActiveROI();
                    //HOperatorSet.WriteImage(CurImage, "bmp", 0, "D:\\curimg11.bmp");
                    ho_ImageReduced.Dispose();
                    HOperatorSet.SetSystem("clip_region", "false");
                    HOperatorSet.ReduceDomain(CurImage.Clone(), roiRRet.Data.getRegion(), out ho_ImageReduced);
                }
                else
                {
                    HOperatorSet.GetImageSize(CurImage, out HTuple width, out HTuple height);
                    hv_Row1 = height / 2 - 400;
                    hv_Column1 = width / 2 - 400;
                    hv_Row2 = height / 2 + 400;
                    hv_Column2 = width / 2 + 400;
                    ho_Rectangle.Dispose();
                    HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Row1, hv_Column1, hv_Row2, hv_Column2);
                    HOperatorSet.SetSystem("clip_region", "false");
                    HOperatorSet.ReduceDomain(CurImage.Clone(), ho_Rectangle, out ho_ImageReduced);
                }
                HTuple radStart, radEnd;
                HOperatorSet.TupleRad((double)tb_AngleStart.Value, out radStart);
                HOperatorSet.TupleRad((double)tb_AngleEnd.Value, out radEnd);
                //HOperatorSet.WriteImage(ho_ImageReduced, "bmp", 0, "D:\\reduce.bmp");
                hv_ModelHandle.Dispose();
                hv_ModelHandle.CreateGenericShapeModel();
                hv_ModelHandle.SetGenericShapeModelParam("min_score", (double)tb_MinScore.Value);
                hv_ModelHandle.SetGenericShapeModelParam("num_matches", 1);
                hv_ModelHandle.SetGenericShapeModelParam("timeout", 1000);
                hv_ModelHandle.SetGenericShapeModelParam("angle_start", radStart);
                hv_ModelHandle.SetGenericShapeModelParam("angle_end", radEnd);
                hv_ModelHandle.SetGenericShapeModelParam("num_levels", "auto");
                hv_ModelHandle.SetGenericShapeModelParam("greediness", 0.8);
                hv_ModelHandle.SetGenericShapeModelParam("max_overlap", 0);
                hv_ModelHandle.SetGenericShapeModelParam("iso_scale_min", (HTuple)((float)tb_ScaleMin.Value));
                hv_ModelHandle.SetGenericShapeModelParam("iso_scale_max", (HTuple)((float)tb_ScaleMax.Value));
                hv_ModelHandle.SetGenericShapeModelParam("min_score", (double)tb_MinScore.Value);
                hv_ModelHandle.SetGenericShapeModelParam("num_matches", 1);
                hv_ModelHandle.SetGenericShapeModelParam("timeout", 1000);
                hv_ModelHandle.TrainGenericShapeModel(ho_ImageReduced);
                hv_ModelHandle.WriteShapeModel($"{CommonData.CurProPath}\\{CurCamName}.md");

                //HOperatorSet.CreateScaledShapeModel(ho_ImageReduced, "auto", radStart, radEnd, "auto",
                //(HTuple)((double)tb_ScaleMin.Value), (HTuple)((double)tb_ScaleMax.Value), "auto", "auto", "use_polarity",
                //"auto", "auto", out hv_ModelID);
                //HOperatorSet.WriteShapeModel(hv_ModelID, $"{CommonData.CurProPath}\\{CurCamName}.md");
                IniFileHelper.SaveINI(CommonData.SetFilePath, CurCamName, "anglestart", tb_AngleStart.Value.ToString());
                IniFileHelper.SaveINI(CommonData.SetFilePath, CurCamName, "angleend", tb_AngleEnd.Value.ToString());
                IniFileHelper.SaveINI(CommonData.SetFilePath, CurCamName, "minscale", tb_ScaleMin.Value.ToString());
                IniFileHelper.SaveINI(CommonData.SetFilePath, CurCamName, "maxscale", tb_ScaleMax.Value.ToString());
                IniFileHelper.SaveINI(CommonData.SetFilePath, CurCamName, "score", tb_MinScore.Value.ToString());
                tb_InfoBox.Text = "模板学习成功!";
            }
            catch (Exception ex)
            {
                tb_InfoBox.Text = "模板学习异常!" + ex.Message;
            }
        }

        private void FrmTrain_Load(object sender, EventArgs e)
        {
            try
            {
                tb_AngleStart.Value = AngleStart;
                tb_AngleEnd.Value = AngleEnd;
                tb_ScaleMin.Value = (decimal)MinScale;
                tb_ScaleMax.Value = (decimal)MaxScale;
                tb_MinScore.Value = (decimal)MinScore;

                tb_ExposureTime.Value = IniFileHelper.ReadINI(CommonData.SetFilePath, CurCamName, "exposure", 100);
                tb_Gain.Value = IniFileHelper.ReadINI(CommonData.SetFilePath, CurCamName, "gain", 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据赋值异常!" + ex.Message);
            }
        }

        private void btn_UserParam_Click(object sender, EventArgs e)
        {
            if (tb_PWD.Text.Trim().Length <= 0)
            {
                MessageBox.Show("权限不足!");
                return;
            }
            string pwd = DateTime.Now.Year + ".." + (DateTime.Now.Month + DateTime.Now.Day);
            if (!tb_PWD.Text.Trim().Equals(pwd))
            {
                MessageBox.Show("权限不足!");
                return;
            }
            tb_PWD.Text = "";
            tb_AngleStart.Visible = true;
            tb_AngleEnd.Visible = true;
            tb_ScaleMin.Visible = true;
            tb_ScaleMax.Visible = true;
            tb_MinScore.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            try
            {
                string modelPath = $"{CommonData.CurProPath}\\{CurCamName}.md";
                if (!File.Exists(modelPath))
                {
                    MessageBox.Show("模板不存在,请先学习!");
                    return;
                }
                if (CurImage == null || ImageisEmpty(CurImage))
                {
                    MessageBox.Show("图像为空,请先拍照!");
                    return;
                }
                //HOperatorSet.ReadShapeModel(modelPath, out HTuple hv_ModelID);

                HTuple radStart, radEnd;
                HOperatorSet.TupleRad((double)tb_AngleStart.Value, out radStart);
                HOperatorSet.TupleRad((double)tb_AngleEnd.Value, out radEnd);
                Stopwatch watch = new Stopwatch();
                watch.Start();
                HShapeModel hv_ModelHandle = new HShapeModel();
                hv_ModelHandle.ReadShapeModel(modelPath);
                hv_ModelHandle.SetGenericShapeModelParam("min_score", (double)tb_MinScore.Value);
                hv_ModelHandle.SetGenericShapeModelParam("num_matches", 1);
                hv_ModelHandle.SetGenericShapeModelParam("angle_start", radStart);
                hv_ModelHandle.SetGenericShapeModelParam("angle_end", radEnd);
                hv_ModelHandle.SetGenericShapeModelParam("greediness", 0.8);
                hv_ModelHandle.SetGenericShapeModelParam("timeout", 1000);
                int ho_MatchNum = 0;
                HGenericShapeModelResult matchResult = hv_ModelHandle.FindGenericShapeModel(CurImage, out ho_MatchNum);
                watch.Stop();
                if (ho_MatchNum > 0)//OK品
                {
                    HTuple curRow = matchResult.GetGenericShapeModelResult("all", "row");
                    HTuple curColumn = matchResult.GetGenericShapeModelResult("all", "column");
                    HTuple curAngle = matchResult.GetGenericShapeModelResult("all", "angle");
                    HTuple curScore = matchResult.GetGenericShapeModelResult("all", "score");
                    HObject curContours = matchResult.GetGenericShapeModelResultObject("all", "contours");

                    tb_InfoBox.Text = $"当前判断为OK品!\r\n测试耗时:{watch.ElapsedMilliseconds}ms;\r\n识别结果:\r\nX:{curRow};\r\nY:{curColumn};\r\nR:{curAngle};\r\nS:{curScore}";
                    HOperatorSet.GenCrossContourXld(out HObject cross, curRow, curColumn, 300, 0);
                    WinCtrl.DisplayObject(cross, "red");
                    WinCtrl.DisplayObject(curContours, "red");
                    WinCtrl.DisplayBuffer();
                }
                else//NG品
                {
                    tb_InfoBox.Text = $"当前判断为NG品!";
                }
            }
            catch (Exception ex)
            {
                _logger.Error("模板测试异常!" + ex.Message);
                MessageBox.Show("模板测试异常!" + ex.Message);
            }
        }

        /// <summary>
        /// 判断HObject是否为空
        /// </summary>
        internal static Response ImageisEmpty(HObject inputImage)
        {
            try
            {
                if (inputImage != null)
                {
                    if (inputImage.Key.ToString() != "0")
                    {
                        return Response.Fail("");
                    }
                }
                return Response.Ok();
            }
            catch
            {
                return Response.Ok();
            }
        }

        private void FrmTrain_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}
