using SmartLib;
using HalconDotNet;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Security.Cryptography;
using System.Linq.Expressions;

namespace SmartVEye
{
    public partial class FrmMain : Form
    {
        string WorkSpaceDir = Application.StartupPath + "\\WorkSpace";
        string pngPath = Application.StartupPath + "\\AppSmartEye.png";
        List<IVisCtrl> VisCtrlList = new List<IVisCtrl>();
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public static int ROIStep = 50;//ROI调节尺寸步长
        public static bool IsDisconnectAll = false;//是否是一键脱机 一键脱机屏蔽单个的密码
        bool IsFrmText = false;//界面是否要连续测试
        System.Windows.Forms.Timer AuthorityTimer = new System.Windows.Forms.Timer();//注册码检验定时器
        FrmAuthority frmAuthority = null;//软件注册界面
        public FrmMain()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //设置Icon
            if (File.Exists(pngPath))
            {
                Bitmap srcBitmap = new Bitmap(pngPath);
                //获得原位图的图标句柄
                IntPtr hIco = srcBitmap.GetHicon();
                //从图标的指定WINDOWS句柄创建Icon
                Icon icon = Icon.FromHandle(hIco);
                this.Icon = icon;
            }
            lbl_CurVersion.Text = "Ver: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            IniFileHelper.SetINISaveFilePath(CommonData.SetFilePath);
            //读软件参数
            CommonData.SoftName = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "soft", "");
            CommonData.CompanyName = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "company", "");
            //相机数量
            CommonData.CameraCount = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "camera", 4);
            //读DPI方案
            CommonData.SysDpiType = IniFileHelper.ReadINI(Application.StartupPath + "\\DPI.ini", "DPI", "DPI", 0);
            //IO复位时间
            CommonData.IOResetNum = IniFileHelper.ReadINI(Application.StartupPath + "\\DPI.ini", "DPI", "IOResetNum", 0);
            //读相机类型
            CommonData.CameraType = IniFileHelper.ReadINI(Application.StartupPath + "\\DPI.ini", "CamType", "CamType", 0);
            //读识别超时时间设置
            CommonData.MatchTimeOut = IniFileHelper.ReadINI(Application.StartupPath + "\\DPI.ini", "MatchTimeOut", "MatchTimeOut", 300);
            if (CommonData.SysDpiType > 1) CommonData.SysDpiType = 1;
            if (CommonData.CameraType > 1) CommonData.CameraType = 1;
            IniFileHelper.SaveINI(Application.StartupPath + "\\DPI.ini", "DPI", "DPI", CommonData.SysDpiType);
            IniFileHelper.SaveINI(Application.StartupPath + "\\DPI.ini", "CamType", "CamType", CommonData.CameraType);
            IniFileHelper.SaveINI(Application.StartupPath + "\\DPI.ini", "DPI", "IOResetNum", CommonData.IOResetNum);
            IniFileHelper.SaveINI(Application.StartupPath + "\\DPI.ini", "MatchTimeOut", "MatchTimeOut", CommonData.MatchTimeOut);
            //根据相机数量调整界面
            if (CommonData.CameraCount < 0) CommonData.CameraCount = 0;
            if (CommonData.CameraCount > 6) CommonData.CameraCount = 6;
            //初始化界面工具
            if (VisCtrlList == null) VisCtrlList = new List<IVisCtrl>();
            VisCtrlList.Clear();
            IVisCtrl curVisCtrl = null;
            for (int idx = 0; idx < CommonData.CameraCount; idx++)
            {
                #region 旧的控件

                //if (CommonData.CameraCount == 1)
                //{
                //    curVisCtrl = new VisCtrlV1();
                //    lbl_WinMode.Text = "  Win:1";//用于标识窗体类型
                //}
                //else if (CommonData.CameraCount == 4 || CommonData.CameraCount == 6)
                //{
                //    if (CommonData.SysDpiType == 0)//DPI方案1
                //    {
                //        curVisCtrl = new VisCtrlV3();
                //        curVisCtrl.CtrlName = "Win" + idx;
                //        lbl_WinMode.Text = "  Win:3";//用于标识窗体类型
                //    }
                //    else//DPI方案0
                //    {
                //        curVisCtrl = new VisCtrlV2();
                //        lbl_WinMode.Text = "  Win:2";//用于标识窗体类型
                //    }
                //}
                //else if (CommonData.CameraCount == 2 || CommonData.CameraCount == 3)
                //{
                //    curVisCtrl = new VisCtrlV4();
                //    lbl_WinMode.Text = "  Win:4";//用于标识窗体类型
                //}

                #endregion

                #region 新的控件

                curVisCtrl = new VisCtrlV2();
                lbl_WinMode.Text = "  Win:2";//用于标识窗体类型

                #endregion
                curVisCtrl.CamName = "CAM" + (idx + 1);
                ((UserControl)curVisCtrl).Dock = DockStyle.Fill;
                VisCtrlList.Add(curVisCtrl);
            }
            VisCtrlV2.CameraOpenEvent += CameraOpenEvent;

            if (CommonData.CameraCount == 1)
            {
                tlp_MainContainer.ColumnStyles[1].Width = 0;
                tlp_MainContainer.ColumnStyles[2].Width = 0;
                tlp_MainContainer.RowStyles[1].SizeType = SizeType.Absolute;
                tlp_MainContainer.RowStyles[1].Height = 0;
                tlp_MainContainer.Controls.Add((Control)VisCtrlList[0], 0, 0);
            }
            else if (CommonData.CameraCount == 2)
            {
                tlp_MainContainer.ColumnStyles[2].Width = 0;
                tlp_MainContainer.RowStyles[1].SizeType = SizeType.Absolute;
                tlp_MainContainer.RowStyles[1].Height = 0;
                tlp_MainContainer.Controls.Add((Control)VisCtrlList[0], 0, 0);
                tlp_MainContainer.Controls.Add((Control)VisCtrlList[1], 1, 0);
            }
            else if (CommonData.CameraCount == 3)
            {
                tlp_MainContainer.RowStyles[1].SizeType = SizeType.Absolute;
                tlp_MainContainer.RowStyles[1].Height = 0;
                tlp_MainContainer.Controls.Add((Control)VisCtrlList[0], 0, 0);
                tlp_MainContainer.Controls.Add((Control)VisCtrlList[1], 1, 0);
                tlp_MainContainer.Controls.Add((Control)VisCtrlList[2], 2, 0);
            }
            else if (CommonData.CameraCount == 4)
            {
                tlp_MainContainer.ColumnStyles[2].Width = 0;
                tlp_MainContainer.Controls.Add((UserControl)VisCtrlList[0], 0, 0);
                tlp_MainContainer.Controls.Add((UserControl)VisCtrlList[1], 1, 0);
                tlp_MainContainer.Controls.Add((UserControl)VisCtrlList[2], 0, 1);
                tlp_MainContainer.Controls.Add((UserControl)VisCtrlList[3], 1, 1);
            }
            else if (CommonData.CameraCount == 6)
            {
                tlp_MainContainer.Controls.Add((Control)VisCtrlList[0], 0, 0);
                tlp_MainContainer.Controls.Add((Control)VisCtrlList[1], 1, 0);
                tlp_MainContainer.Controls.Add((Control)VisCtrlList[2], 2, 0);
                tlp_MainContainer.Controls.Add((Control)VisCtrlList[3], 0, 1);
                tlp_MainContainer.Controls.Add((Control)VisCtrlList[4], 1, 1);
                tlp_MainContainer.Controls.Add((Control)VisCtrlList[5], 2, 1);
            }
            //注册码检验定时器
            AuthorityTimer.Interval = 36 * 1000;
            AuthorityTimer.Tick += AuthorityTimer_Tick;
            AuthorityTimer.Start();

        }

        /// <summary>
        /// 确保打开相机之后获取一次触发延迟的值给主界面显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CameraOpenEvent(object sender, EventArgs e)
        {
            if(sender is VisCtrlV2 visCtrl)
            {
                try
                {
                    // 触发延迟相机默认选中第一项
                    comboBox_CamList.Items.Add(visCtrl.CamName);
                    if (comboBox_CamList.Items.Count > 0)
                        comboBox_CamList.SelectedIndex = 0;

                    double delay = IniFileHelper.ReadINI(CommonData.SetFilePath, visCtrl.CamName, "triggerdelay", 0);
                    visCtrl.SetTriggerDelay(delay);
                    label_TriggerDelay.Text = delay.ToString();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AuthorityTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Response authorityRet = CommonHelper.ReadRegisteValidTime();
                if (!authorityRet)
                {
                    CommonData.AuthorityValid = false;
                    if (frmAuthority == null)
                    {
                        frmAuthority = new FrmAuthority();
                    }
                    else
                    {
                        frmAuthority.Visible = false;
                    }
                    frmAuthority.TopMost = true;
                    frmAuthority.ShowDialog();
                }
                else
                {
                    CommonData.AuthorityValid = true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("注册码校验异常!" + ex.Message);
                CommonData.AuthorityValid = false;
            }
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            try
            {
                //string hddSerial = CommonHelper.GetHardDiskSerialNumber();
                //string hddSerialDES = DESHelper.DesEncrypt(hddSerial);
                //string readCode = IniFileHelper.ReadINI(Application.StartupPath + "\\AAAuthor.ini", "code", "code", "nocode");
                //if (!hddSerialDES.Equals(readCode))
                //{
                //    IniFileHelper.SaveINI(Application.StartupPath + "\\AAAuthor.ini", "machine", "hdd", hddSerial);
                //    IniFileHelper.SaveINI(Application.StartupPath + "\\AAAuthor.ini", "machine", "bios", CommonHelper.GetBIOSSerialNumber());
                //    IniFileHelper.SaveINI(Application.StartupPath + "\\AAAuthor.ini", "machine", "cpu", CommonHelper.GetCPUSerialNumber());
                //    IniFileHelper.SaveINI(Application.StartupPath + "\\AAAuthor.ini", "machine", "mac", CommonHelper.GetNetCardMACAddress());
                //    MessageBox.Show($"授权码不存在,请联络制造商!\r\n错误代码:139\r\n");
                //    return;
                //}
                this.Invoke(new Action(() => { this.Text = $"{CommonData.SoftName}  {CommonData.CompanyName}"; }));
                ShowSystemInfo("系统初始化中...", 0);
                Thread initThread = new Thread(new ThreadStart(SystemInit));
                initThread.IsBackground = true;
                initThread.Start();
                CommonData.CamReadModelEvent += CamReadModel;
                Response authorityRet = CommonHelper.ReadRegisteValidTime();
                if (!authorityRet)
                {
                    CommonData.AuthorityValid = false;
                    if (frmAuthority == null) frmAuthority = new FrmAuthority();
                    frmAuthority.TopMost = true;
                    frmAuthority.ShowDialog();
                }
                else
                {
                    CommonData.AuthorityValid = true;
                }
            }
            catch (Exception ex)
            {
                ShowSystemInfo("系统初始化异常" + ex.Message, 1);
                MessageBox.Show("系统初始化异常!\r\n" + ex.Message);
            }
        }

        private void CamReadModel(int whichCam)
        {
            VisCtrlList[whichCam].ReadModel();
        }

        private void btn_LearnAll_Click(object sender, EventArgs e)
        {
            try
            {
                //只要有一个不在建模模式，全部返回
                bool allInTrainMode = true;
                string camName = "";
                foreach (IVisCtrl item in VisCtrlList)
                {
                    if (!item.IsTrainMode)
                    {
                        allInTrainMode = false;
                        camName = item.CamName;
                        break;
                    }
                    item.TrainOperateBtn();

                    //((VisCtrlV3)item).btn_WidthPlus
                    //((SmartVEye.VisCtrlV3)item).btn_HighPlus.Visible = false;
                    //tlp_MainContainer.Controls.Add((Control)VisCtrlList[0], 0, 0);
                    //MessageBox.Show("aaaaaaaaaa");
                }
                if (!allInTrainMode)
                {
                    MessageBox.Show($"相机[{camName}]不处于建模模式,请先执行 [一键建模]!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btn_LearnAll.Enabled = false;
                CommonData.IsLearning = true;
                bool contiRes = true;
                foreach (IVisCtrl item in VisCtrlList)
                {
                    if (item.IsContinueSnap)
                    {
                        contiRes = false;
                        item.StopConiSnapForAuto();
                        _logger.Info($"相机[{item.CamName}]一键学习,停止视频模式完成!");
                    }
                }
                if (!contiRes) return;
                Thread.Sleep(10);
                foreach (var item in VisCtrlList)
                {
                    if (item.CameraEnable <= 0) continue;
                    Response modelRet = item.TrainModelAuto();
                    if (!modelRet)
                    {
                        _logger.Error($"相机[{item.CamName}]一键学习异常!{modelRet.Msg}");
                        MessageBox.Show($"相机[{item.CamName}]一键学习异常!{modelRet.Msg}");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("一键学习异常!" + ex.Message);
                MessageBox.Show("一键学习异常!" + ex.Message);
            }
            finally
            {
                Thread.Sleep(50);
                CommonData.IsLearning = false;
                btn_LearnAll.Enabled = true;
            }
        }
        private void btn_ConnAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in VisCtrlList)
                {
                    item.ConnectPLC();
                }
                ShowSystemInfo("一键联机成功!", 0);
            }
            catch (Exception ex)
            {
                _logger.Error("一键联机失败!" + ex.Message);
                MessageBox.Show("一键联机失败!" + ex.Message);
            }
        }

        private void btn_DisConnAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (CommonData.DisConnPLCNeedPWD > 0)
                {
                    FrmPassword frmPassword = new FrmPassword();
                    if (frmPassword.ShowDialog() == DialogResult.OK)
                    {
                        IsDisconnectAll = true;
                        foreach (var item in VisCtrlList)
                        {
                            item.DisConnectPLC();
                        }
                        ShowSystemInfo("一键脱机成功!", 0);
                        frmPassword.Close();
                    }
                }
                else
                {
                    IsDisconnectAll = true;
                    foreach (var item in VisCtrlList)
                    {
                        item.DisConnectPLC();
                    }
                    ShowSystemInfo("一键脱机成功!", 0);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("一键脱机失败!" + ex.Message);
                MessageBox.Show("一键脱机失败!" + ex.Message);
            }
            finally
            {
                IsDisconnectAll = false;
            }
        }

        private void btn_ClearRecord_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否清除全部相机计数?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                for (int idx = 0; idx < VisCtrlList.Count; idx++)
                {
                    VisCtrlList[idx].ClearProRecord();
                }
                _logger.Info("清除全部相机计数!");
                ShowSystemInfo("清除全部相机计数!", 0);
            }
            catch (Exception ex)
            {
                _logger.Error("清除全部相机计数异常!" + ex.Message);
                MessageBox.Show("清除全部相机计数异常!" + ex.Message);
            }
        }

        /// <summary>
        /// 界面输出信息
        /// </summary>
        /// <param name="mess">信息内容</param>
        /// <param name="level">信息等级 0：正常信息 1：异常信息</param>
        public void ShowSystemInfo(string mess, int level)
        {
            try
            {
                tb_InfoBox.Invoke(new Action(() =>
                {
                    tb_InfoBox.Text = mess;
                    if (level == 0) { tb_InfoBox.ForeColor = Color.Black; _logger.Info(mess); }
                    else { tb_InfoBox.ForeColor = Color.Red; _logger.Error(mess); }
                }));
            }
            catch (Exception ex)
            {
                _logger.Error("显示运行信息异常!" + ex.Message);
            }
        }

        public void SystemInit()
        {
            try
            {
                bool HaveError = false;//初始化过程中是否有异常发生
                //读料号
                CommonData.CurProPath = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "curpro", $"{Application.StartupPath}\\WorkSpace");
                if (!Directory.Exists(CommonData.CurProPath))
                {
                    CommonData.CurProPath = $"{Application.StartupPath}\\WorkSpace";
                    IniFileHelper.SaveINI(CommonData.SetFilePath, "set", "curpro", CommonData.CurProPath);
                }
                if (!Directory.Exists(CommonData.CurProPath)) Directory.CreateDirectory(CommonData.CurProPath);
                //读全局参数配置
                CommonData.PLCAlarmTime = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "alarmtime", 50);
                CommonData.IsSaveRunLog = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "savelog", 0);
                CommonData.IsSaveImg = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "saveimage", 0);
                CommonData.IsShowIOForm = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "showio", 0);
                CommonData.ContiTrigTime = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "contitrig", 100);
                CommonData.DisConnPLCNeedPWD = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "disconnplc", 1);
                CommonData.IsUseCamNet = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "usecamnet", 0);
                int showphone = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "showphone", 1);
                string readPhone = "e541c819d910b140aac240f6336133b29ad835a9ec36d";
                if (showphone > 0)
                {
                    readPhone = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "serial", "e541c819d910b140aac240f6336133b29ad835a9ec36d");
                    try
                    {
                        string realPhone = "";
                        for (int idx = 0; idx < 11; idx++)
                        {
                            realPhone += readPhone.Substring(3 + idx * 4, 1);
                        }
                        lbl_Phone.Invoke(new Action(() => { lbl_Phone.Text = $"销售+技术支持: {realPhone} "; }));
                    }
                    catch (Exception) { }
                }
                CommonData.IsHandOffline = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "isHandOffline", 0);

                //保存 防止没有默认值
                IniFileHelper.SaveINI(CommonData.SetFilePath, "set", "alarmtime", CommonData.PLCAlarmTime);
                IniFileHelper.SaveINI(CommonData.SetFilePath, "set", "savelog", CommonData.IsSaveRunLog);
                IniFileHelper.SaveINI(CommonData.SetFilePath, "set", "saveimage", CommonData.IsSaveImg);
                IniFileHelper.SaveINI(CommonData.SetFilePath, "set", "showio", CommonData.IsShowIOForm);
                IniFileHelper.SaveINI(CommonData.SetFilePath, "set", "contitrig", CommonData.ContiTrigTime);
                IniFileHelper.SaveINI(CommonData.SetFilePath, "set", "disconnplc", CommonData.DisConnPLCNeedPWD);
                IniFileHelper.SaveINI(CommonData.SetFilePath, "set", "usecamnet", CommonData.IsUseCamNet);
                IniFileHelper.SaveINI(CommonData.SetFilePath, "set", "showphone", showphone);
                IniFileHelper.SaveINI(CommonData.SetFilePath, "set", "serial", readPhone);
                IniFileHelper.SaveINI(CommonData.SetFilePath, "set", "isHandOffline", CommonData.IsHandOffline);
                //读IO配置
                if (CommonData.IOOutAddressList == null)
                    CommonData.IOOutAddressList = new List<ushort>();
                int numCount = IniFileHelper.ReadINI(CommonData.SetFilePath, "ioset", "ionumber", 4);
                CommonData.IOOutAddressList.Clear();
                for (int idx = 1; idx <= numCount; idx++)
                {
                    int curIOIdx = IniFileHelper.ReadINI(CommonData.SetFilePath, "ioset", "out" + idx, 33);
                    CommonData.IOOutAddressList.Add((ushort)curIOIdx);
                }
                //初始化IO
                Response ioret = IOHelper.IOInit();
                if (!ioret) ShowSystemInfo($"IO初始化异常!" + ioret.Msg, 1);
                //读模板
                for (int idx = 0; idx < VisCtrlList.Count; idx++)
                {
                    VisCtrlList[idx].CameraEnable = IniFileHelper.ReadINI(CommonData.SetFilePath, VisCtrlList[idx].CamName, "enable", 1);
                    VisCtrlList[idx].CamNumber = IniFileHelper.ReadINI(CommonData.SetFilePath, VisCtrlList[idx].CamName, "number", 1);
                    //VisCtrlList[idx].SetCamEnable();
                    VisCtrlList[idx].SetRes(true);
                    if (VisCtrlList[idx].CameraEnable <= 0) continue;
                    Response ret = VisCtrlList[idx].ReadModel();
                    if (!ret)
                    {
                        HaveError = true;
                        ShowSystemInfo($"相机[{VisCtrlList[idx].CamName}]读取产品模板异常!", 1);
                    }
                }
                for (int idx = 0; idx < VisCtrlList.Count; idx++)
                {
                    if (VisCtrlList[idx].CameraEnable <= 0) continue;
                    Response camRet = VisCtrlList[idx].InitCamera(VisCtrlList[idx].CamName);
                    if (!camRet)
                    {
                        HaveError = true;
                        ShowSystemInfo($"相机[{VisCtrlList[idx].CamName}]打开异常!{camRet}", 1);
                        //MessageBox.Show($"相机[{VisCtrlList[idx].CamName}]打开异常!{camRet}");
                    }
                }
                pnl_Menu.Invoke(new Action(() => { pnl_Menu.Enabled = true; tlp_MainContainer.Enabled = true; }));
                if (!HaveError)
                {
                    ShowSystemInfo($"系统初始化完成!", 0);
                }
                _logger.Info("系统初始化完成!当前系统版本为: Ver: " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "; " + lbl_WinMode.Text);
                //开发人员加载本地图像测试用
                //HOperatorSet.ReadImage(out HObject CurImage, "D:\\1.bmp");
                //CommonData.PostReadFrame(CurImage.Clone());
            }
            catch (Exception ex)
            {
                _logger.Error("系统初始化异常,请重启软件!" + ex.Message);
                ShowSystemInfo($"系统初始化异常,请重启软件!\r\n" + ex.Message, 1);
                MessageBox.Show($"系统初始化异常,请重启软件!\r\n" + ex.Message);
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _logger.Info("软件系统关闭!");
                if (AuthorityTimer != null) AuthorityTimer.Stop();
                for (int idx = 0; idx < CommonData.CameraCount; idx++)
                {
                    try
                    {
                        if (VisCtrlList[idx] != null)
                        {
                            VisCtrlList[idx].StopContiSnap();
                            _logger.Info($"记录生产统计信息!相机[{VisCtrlList[idx].CamName}]: OK:{VisCtrlList[idx].RecordOK};NG:{VisCtrlList[idx].RecordNG}");
                            IniFileHelper.SaveINI(CommonData.RecordFilePath, "recordok", "CAM" + (idx + 1), VisCtrlList[idx].RecordOK.ToString());
                            IniFileHelper.SaveINI(CommonData.RecordFilePath, "recordng", "CAM" + (idx + 1), VisCtrlList[idx].RecordNG.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"相机[{VisCtrlList[idx].CamName}]停止采集异常!" + ex.Message);
                    }
                }
                Thread.Sleep(100);
                try
                {
                    foreach (var item in VisCtrlList)
                    {
                        item.UnInitCamera();
                        _logger.Info($"相机[{item.CamName}]已释放!");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error($"软件系统退出,相机释放异常!" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    _logger.Error($"记录产品数量异常!" + ex.Message);
                    for (int idx = 0; idx < CommonData.CameraCount; idx++)
                    {
                        _logger.Error($"记录产品数量异常!CAM{idx + 1};OK:{VisCtrlList[idx].RecordOK};NG:{VisCtrlList[idx].RecordNG}" + ex.Message);
                    }
                }
                catch (Exception) { }
            }
            finally
            {
                try
                {
                    _logger.Info("关闭所有IO!");
                    for (int idx = 0; idx < CommonData.CameraCount; idx++)
                    {
                        IOHelper.SetBitOff(idx + 1);
                    }
                    IOHelper.IOUnInit();
                }
                catch (Exception) { }
            }
        }

        private void btn_TestLoad_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                HOperatorSet.ReadImage(out HObject CurImage, openFileDialog1.FileName);
                //HOperatorSet.ScaleImage(CurImage, out CurImage, 3.49, 0);
                CommonData.PostReadFrame(CurImage.Clone());
            }
        }

        Stopwatch watch = new Stopwatch();
        private void btn_TestAll_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                string[] files = Directory.GetFiles(path);
                IsFrmText = true;
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    int exceptionIdx = 0;
                    try
                    {
                        int idx = 0;
                        while (IsFrmText)
                        {
                            if (idx >= files.Length) idx = 0;
                            exceptionIdx = idx;
                            HOperatorSet.ReadImage(out HObject CurImage, files[idx]);
                            CommonData.PostReadFrame(CurImage);
                            Thread.Sleep(200);
                            Console.WriteLine(exceptionIdx + "  " + files[exceptionIdx]);
                            idx++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(exceptionIdx + "  " + files[exceptionIdx] + "  " + ex.Message);
                        throw;
                    }


                }));
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void lbl_CurVersion_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (CommonData.IsShowIOForm <= 0) return;
                FrmIOTest frmIOTest = new FrmIOTest();
                frmIOTest.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("IO测试界面打开失败!" + ex.Message);
            }
        }

        private void lbl_Setting_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                FrmSetting frmSetting = new FrmSetting();
                frmSetting.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("参数设定界面打开失败!" + ex.Message);
            }
        }

        private void btn_Function_MouseDown(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.BackColor = Color.DodgerBlue;
            }
        }

        private void btn_Function_MouseUp(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.BackColor = Color.Aqua;
            }
        }

        private void tb_ROIStep_Scroll(object sender, EventArgs e)
        {
            lbl_ROISize.Text = tb_ROISize.Value.ToString();
            ROIStep = Convert.ToInt32(lbl_ROISize.Text);
        }

        private void btn_FrmTestStop_Click(object sender, EventArgs e)
        {
            IsFrmText = false;
        }

        private void btn_TrainModeAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in VisCtrlList)
                {
                    item.TrainModeIn();
                }
                ShowSystemInfo("一键建模成功!", 0);
            }
            catch (Exception ex)
            {
                _logger.Error("一键建模失败!" + ex.Message);
                MessageBox.Show("一键建模失败!" + ex.Message);
            }
        }

        private void lbl_WinMode_DoubleClick(object sender, EventArgs e)
        {
            FrmAuthority frmAuthority = new FrmAuthority();
            frmAuthority.ShowDialog();
        }
        /// <summary>
        /// 取消告警灯显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelAlarm_Click(object sender, EventArgs e)
        {
            foreach (var item in VisCtrlList)
            {

                //_logger.Info($"联机状态,相机[{item.CamName}]发送NG信号给PLC完成!");

                int ioidx = Convert.ToInt32( item.CamName.Substring(3));
                Response ioRet = IOHelper.SetBitOff(ioidx);

                if (!ioRet) _logger.Error($"相机[{item.CamName}]关闭IO异常!{ioRet.Msg}");

            }
        }

        /// <summary>
        /// 点击打开设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Settings_Click(object sender, EventArgs e)
        {
            FrmPassword frmPassword = new FrmPassword();
            if (frmPassword.ShowDialog() == DialogResult.OK)
            {
                frmPassword.Close();
                FrmSetting frmSetting = new FrmSetting();
                frmSetting.ShowDialog();
            }
        }
        
        /// <summary>
        /// 选择不同的相机时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (VisCtrlList.Find((item)=>item.CamName == comboBox_CamList.Text) is VisCtrlV2 visCtrl)
                {
                    label_TriggerDelay.Text = visCtrl.GetTriggerDelay().ToString();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 点击一次减少100ms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_DelaySub_Click(object sender, EventArgs e)
        {
            double curVal = double.Parse(label_TriggerDelay.Text);
            try
            {
                if (VisCtrlList.Find((item) => item.CamName == comboBox_CamList.Text) is VisCtrlV2 visCtrl)
                {
                    if (curVal < 100)
                        throw new Exception("最多减到0");
                    curVal -= 100;
                    visCtrl.SetTriggerDelay(curVal);
                    IniFileHelper.SaveINI(CommonData.SetFilePath, visCtrl.CamName, "triggerdelay", curVal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            label_TriggerDelay.Text = curVal.ToString();
        }

        /// <summary>
        /// 点击一次增加100ms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_DelayPlus_Click(object sender, EventArgs e)
        {
            double curVal = double.Parse(label_TriggerDelay.Text);
            try
            {
                if (VisCtrlList.Find((item) => item.CamName == comboBox_CamList.Text) is VisCtrlV2 visCtrl)
                {
                    if (curVal >= double.MaxValue - 100)
                        throw new Exception("不能超过上限！");
                    curVal += 100;
                    visCtrl.SetTriggerDelay(curVal);
                    IniFileHelper.SaveINI(CommonData.SetFilePath, visCtrl.CamName, "triggerdelay", curVal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            label_TriggerDelay.Text = curVal.ToString();
        }
    }
}
