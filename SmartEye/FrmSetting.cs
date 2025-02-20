using NLog;
using SmartLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartVEye
{
    public partial class FrmSetting : Form
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        string[] SetNameList = { "相机数量", "模板路径", "软件名称", "公司名称", "排废输出时间", "是否保存日志","是否保存图像",
            "显示销售电话","IO测试工具","销售电话","视频模式触发间隔","启用相机探包" ,"脱机是否需要密码","是否手动联机","清除计数是否需要密码"};
        string[] CamNameList = { "起始角度", "终止角度", "最小缩小倍数", "最大放大倍数","相似分数值", "模板色差" ,"相机曝光时间","相机增益",
            "是否启用相机","相机编号","ROI宽度","ROI高度","白页检测阈值","黑页检测阈值","训练ROI横坐标","训练ROI纵坐标","训练ROI宽度",
            "训练ROI高度", "检测模式","黑白页检测轮廓数量","数值减小图像变亮","数值增大图像变暗","筛选轮廓最小值","高精度模板色差",
            "中精度模板色差","低精度模板色差","精度模式","白页模式曝光","当前检测精度","检测精度-高","检测精度-中","检测精度-低","图像检测模式0LK-1TX"};

        //string[] CamNameList = { "起始角度", "终止角度", "最小缩小倍数", "最大放大倍数","相似分数值", "模板色差" ,"相机曝光时间","相机增益",
        //    "是否启用相机","相机编号","ROI宽度","ROI高度","白页检测阈值","黑页检测阈值","训练ROI横坐标","训练ROI纵坐标","训练ROI宽度",
        //    "训练ROI高度", "检测模式","黑白页检测轮廓数量","数值减小图像变亮","数值增大图像变暗","筛选轮廓最小值","高精度模板色差",
        //    "中精度模板色差","低精度模板色差","精度模式","白页检测曝光"};

        public FrmSetting()
        {
            InitializeComponent();
        }
        private void FrmSetting_Shown(object sender, EventArgs e)
        {
            try
            {
                groupBox_System.Width = this.Width / 2;
                cb_CamList.Items.Clear();
                for (int idx = 0; idx < CommonData.CameraCount; idx++)
                {
                    cb_CamList.Items.Add("CAM" + (idx + 1));
                }
                //读全局参数
                while (dgv_SysParam.RowCount > 0)
                {
                    dgv_SysParam.Rows.RemoveAt(0);
                }
                string[] keys = IniFileHelper.ReadKeys("set", CommonData.SetFilePath);
                for (int idx = 0; idx < keys.Length; idx++)
                {
                    string curVal = IniFileHelper.ReadINI(CommonData.SetFilePath, "set", keys[idx], "");
                    string chnName = "";
                    if (idx < SetNameList.Length) chnName = SetNameList[idx];
                    dgv_SysParam.Rows.Add(new string[] { (idx + 1).ToString(), chnName, keys[idx], curVal });
                }
                cb_CamList.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                _logger.Error("全局参数读取异常!" + ex.Message);
                MessageBox.Show("全局参数读取异常!" + ex.Message);
            }
        }

        /// <summary>
        /// 给主界面判断是否需要密码
        /// </summary>
        /// <returns></returns>
        public static bool IsNeedPasswoed()
        {
            return IniFileHelper.ReadINI(CommonData.SetFilePath, "set", "showpassword", "1") == "1" ? true : false;
        }

        private void cb_CamList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //读相机参数
                if (cb_CamList.Text.Length <= 0) return;
                while (dgv_CamParam.RowCount > 0)
                    dgv_CamParam.Rows.RemoveAt(0);
                string[] keys = IniFileHelper.ReadKeys(cb_CamList.Text, CommonData.SetFilePath);
                for (int idx = 0; idx < keys.Length; idx++)
                {
                    string curVal = IniFileHelper.ReadINI(CommonData.SetFilePath, cb_CamList.Text, keys[idx], "");
                    string chnName = "";
                    if (idx < CamNameList.Length) chnName = CamNameList[idx];
                    dgv_CamParam.Rows.Add(new string[] { (idx + 1).ToString(), chnName, keys[idx], curVal });
                }
            }
            catch (Exception ex)
            {
                _logger.Error("相机参数读取异常!" + ex.Message);
                MessageBox.Show("相机参数读取异常!" + ex.Message);
            }
        }

        private void btn_SaveParam_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否保存当前参数?", "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                //保存全局参数
                for (int idx = 0; idx < dgv_SysParam.RowCount; idx++)
                {
                    IniFileHelper.SaveINI(CommonData.SetFilePath, "set", dgv_SysParam.Rows[idx].Cells["tb_SysParamName"].Value.ToString(), dgv_SysParam.Rows[idx].Cells["tb_SysParamValue"].Value.ToString());
                }
                //保存相机参数
                for (int idx = 0; idx < dgv_CamParam.RowCount; idx++)
                {
                    IniFileHelper.SaveINI(CommonData.SetFilePath, cb_CamList.Text, dgv_CamParam.Rows[idx].Cells["tb_CamParamName"].Value.ToString(), dgv_CamParam.Rows[idx].Cells["tb_CamParamValue"].Value.ToString());
                }
                CommonData.CamReadModel(cb_CamList.SelectedIndex);
                MessageBox.Show("参数保存成功!");
            }
            catch (Exception ex)
            {
                _logger.Error("参数保存异常!" + ex.Message);
                MessageBox.Show("参数保存异常!" + ex.Message);
            }
        }

        private void btn_SaveAllCam_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否将当前相机参数同步到所有相机?", "", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                //保存相机参数
                for (int idx = 1; idx <= CommonData.CameraCount; idx++)
                {
                    for (int jdx = 0; jdx < dgv_CamParam.RowCount; jdx++)
                    {
                        IniFileHelper.SaveINI(CommonData.SetFilePath, "CAM" + idx, dgv_CamParam.Rows[jdx].Cells[2].Value.ToString(), dgv_CamParam.Rows[jdx].Cells[3].Value.ToString());
                    }
                }
                MessageBox.Show("参数同步成功!");
            }
            catch (Exception ex)
            {
                _logger.Error("相机参数同步异常!" + ex.Message);
                MessageBox.Show("相机参数同步异常!" + ex.Message);
            }
        }
    }
}
