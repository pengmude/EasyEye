using SmartLib;
using NLog;
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
    public partial class FrmIOTest : Form
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public FrmIOTest()
        {
            InitializeComponent();
        }

        private void cb_IOBtns_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                CheckBox checkBox = sender as CheckBox;
                int ioidx = Convert.ToInt32(checkBox.Name.Substring(5));
                if (ioidx > CommonData.IOOutAddressList.Count)
                {
                    lbl_Info.Visible = true;
                    lbl_Info.Text = $"IO[{ioidx}]不存在,请检查配置!";
                    return;
                }
                if (checkBox.Checked)
                {
                    Response ioRet = IOHelper.SetBitOn(ioidx);
                    if (!ioRet) MessageBox.Show($"打开IO[{ioidx}]失败!  {ioRet.Msg}");
                    else
                    {
                        lbl_Info.Visible = true;
                        lbl_Info.Text = $"打开IO[{ioidx}]成功!";
                    }
                }
                else
                {
                    Response ioRet = IOHelper.SetBitOff(ioidx);
                    if (!ioRet) MessageBox.Show($"关闭IO[{ioidx}]失败!  {ioRet.Msg}");
                    else
                    {
                        lbl_Info.Visible = true;
                        lbl_Info.Text = $"关闭IO[{ioidx}]输出成功!";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("IO输出异常!" + ex.Message);
                MessageBox.Show("IO输出异常!" + ex.Message);
            }
        }
    }
}
