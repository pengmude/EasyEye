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
    public partial class FrmPassword : Form
    {
        public FrmPassword()
        {
            InitializeComponent();
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (tb_PWD.Text.Trim().Length <= 0)
            {
                MessageBox.Show("请输入权限密码");
                tb_PWD.Focus();
                return;
            }
            int month = Convert.ToInt32(DateTime.Now.ToString("MM"));
            int day = Convert.ToInt32(DateTime.Now.ToString("dd"));
            string pwd = (month + day).ToString();
            if (tb_PWD.Text != pwd)
            {
                MessageBox.Show("权限密码错误,请重试!");
                this.DialogResult = DialogResult.No;
            }
            else
                this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tb_PWD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) btn_Submit_Click(sender, e);
        }

        private void buttons_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            tb_PWD.Text = tb_PWD.Text + button.Text;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (tb_PWD.Text.Length > 0)
            {
                string pwdNow = tb_PWD.Text;
                tb_PWD.Text = pwdNow.Substring(0, pwdNow.Length - 1);
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            tb_PWD.Text = "";
        }
    }
}
