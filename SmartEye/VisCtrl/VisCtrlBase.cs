using HalconDotNet;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartVEye
{
    public partial class VisCtrlBase : UserControl
    {
        public int RecordOK { get; set; } = 0;
        public int RecordNG { get; set; } = 0;
        public int CtrlNo = 0;//控件编号
        public IPAddress CamIP = IPAddress.Parse("192.168.1.1");//控件编号
        public string CamName = "CAM1";
        public CamHik CurHikCam = null;
        public CamBasler CurBaslerCam = null;

        //模板参数
        public int AngleStart = -5;//模板参数 起始角度
        public int AngleEnd = 5;//模板参数 终止角度
        public double MinScale = 0.8;//模板参数 最小缩放
        public double MaxScale = 1.2;//模板参数 最大缩放
        public double MinScore = 0.7;//模板参数 相似度
        public double MinContrast = 10;//模板参数 色差
        public double MinContrastHigh = 10;//模板参数 高精度色差
        public double MinContrastMid = 10;//模板参数 中等精度色差
        public double MinContrastLow = 10;//模板参数 低精度色差
        public double MinEdgeLen = 6;//模板轮廓筛选
        public int ImgLight = 255;//数值减小图像变亮
        public int ImgDark = 0; //数值增大图像变暗
        public int Exposure = 100;//相机曝光时间
        public float Gain = 0;//相机增益
        public bool IsWorkOnLine = true;//是否在线运行
        public Thread SnapThread = null;
        public bool IsContinueSnap = false;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public int CameraEnable = 1;//是否启用相机
        public int CamNumber = 1;//相机编号，只显示
        public int PageWhite = 180;
        public int PageBlack = 80;
        public double TrainROIRow = 100;//训练得到的ROI坐标
        public double TrainROICol = 100;
        public double TrainROIWidth = 100;
        public double TrainROIHeight = 100;
        public int CheckMode = 0; //检测模式 0:模板匹配 1：百页 2：黑页
        public int PageWBContourNum = 1; //黑白页检测轮廓数量
        public int CamAccuracy = -1; //精度模式
        public bool IsTrainMode = false;//处于训练模式
        public bool ROIStepClick = false;//点击了ROI调整按钮
        public bool NeedClearRecord { get; set; } = true;//是否需要清零生产计数 软件启动时候需要，保存相机参数时候不需要

        public double oldROIRow = 100;//上次保存的ROIRow坐标
        public double oldROICol = 100;//上次保存的ROIColumn坐标
        public double oldROILen1 = 100;//上次保存的ROI宽度
        public double oldROILen2 = 100;//上次保存的ROI高度

        public HShapeModel hv_ModelHandle = new HShapeModel();//模板ID
        public HTuple hv_ImgModelHandle = new HTuple();//图像训练模板ID

        public int WhitePageExposure = 1000; //白页模式曝光
        public int ImgScore = 90; //当前图片相似度
        public int ImgScoreHight = 90; //图片相似度高
        public int ImgScoreMid = 80; //图片相似度中
        public int ImgScoreLow = 70; //图片相似度低
        /// <summary>
        /// 图像检测模式，0轮廓，1图像
        /// </summary>
        public int ImageCheckMode = 0;
        public VisCtrlBase()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
    }
}
