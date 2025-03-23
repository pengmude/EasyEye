using HalconDotNet;
using NLog;
using SmartLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartVEye
{
    public interface IVisCtrl
    {
        int RecordOK { get; set; }

        int RecordNG { get; set; }

        int CtrlNo { get; set; }//控件编号
        string CtrlName { get; set; } //控件名称

        int CamNumber { get; set; }//相机编号，只显示

        int CameraEnable { get; set; }//是否启用相机

        bool IsContinueSnap { get; set; }
        bool IsTrainMode { get; set; }//是否处于建模模式

        string CamName { get; set; }
        bool NeedClearRecord { get; set; }//是否需要清零生产计数 软件启动时候需要，保存相机参数时候不需要


        void VisWinMouseDown(object sender, HMouseEventArgs e);

        void StopContiSnap();

        Response InitCamera(string CamName);

        void StopConiSnapForAuto();

        Response TrainModelAuto();

        void ConnectPLC();
        void DisConnectPLC();

        void ClearProRecord();

        void SetRes(int number, bool result);

        void SetRes(bool result);

        Response ReadModel();
        void UnInitCamera();
        void TrainModeIn();
        void TrainOperateBtn();    //进行按钮操作
        void SetTriggerDelay(double delay);
        double GetTriggerDelay();
        void ClearNgPic();//清除三张历史NG图像的显示
    }
}
