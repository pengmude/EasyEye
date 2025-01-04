using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartVEye
{
    public class CommonData
    {
        public static string SetFilePath = Application.StartupPath + "\\SystemSet.ini";
        public static string RecordFilePath = Application.StartupPath + "\\Record.ini";
        public static string CurProPath = "";
        public static int CameraCount = 4;//相机数量
        public static int PLCAlarmTime = 50;
        public static bool IsRunning = true;//系统是否自动运行
        public static string CompanyName = "";//客户公司名称
        public static string SoftName = "";//客户公司名称
        public static int IsSaveRunLog = 0;//是否记录运行日志
        public static int IsSaveImg = 0;//是否保存图像
        public static int IsShowIOForm = 0;//是否显示IO工具界面
        public static int ContiTrigTime = 100;//视频模式下触发时间间隔
        public static int DisConnPLCNeedPWD = 1;//关闭联机模式需要密码
        public static int IsUseCamNet = 0;//是否启用相机网络探包
        public static List<ushort> IOOutAddressList = new List<ushort>();//IO列表清单
        public static int SysDpiType = 1;//界面分辨率版本 0：VisCtrl  1：VisCtrlV1
        public static int IOResetNum = 60;//IO复位时间设置
        public static bool AuthorityValid = false;//注册权限验证OK

        /// <summary>
        /// 是否手动脱机
        /// </summary>
        public static int IsHandOffline = 0;//是否手动脱机
        
        /// <summary>
        /// 相机类型 
        /// 0：HikCam  1：BaslerCam
        /// </summary>
        public static int CameraType = 0;
        /// <summary>
        /// 匹配超时时间
        /// </summary>
        public static int MatchTimeOut = 300;
        public static bool IsLearning = false;
        /// <summary>
        /// 加载图像测试运行时效果 仅用于测试用
        /// </summary>
        public delegate void PostReadFrameEventHandler(HObject grabImage);
        public static event PostReadFrameEventHandler PostReadFrameEvent;
        public static void PostReadFrame(HObject grabImage)
        {
            if (PostReadFrameEvent != null)
            {
                PostReadFrameEvent(grabImage);
            }
        }

        /// <summary>
        /// 相机加载模板数据 委托
        /// </summary>
        public delegate void CamReadModelEventHandler(int whichCam);
        public static event CamReadModelEventHandler CamReadModelEvent;
        public static void CamReadModel(int whichCam)
        {
            if (CamReadModelEvent != null)
            {
                CamReadModelEvent(whichCam);
            }
        }
    }

}
