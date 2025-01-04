using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartVEye
{
    public partial class FrmAuthority : Form
    {
        string authorDir = "C:\\";
        string authorFile = "";
        public FrmAuthority()
        {
            InitializeComponent();
        }

        private void FrmAuthority_Load(object sender, EventArgs e)
        {
            //CPU信息
            string cpuInfo = Util.GetMD5Value(DeviceHelper.GetCpuID() + typeof(string).ToString());
            tb_CPUSerial.Text = Util.GetNum(cpuInfo, 8);
            //磁盘信息
            string diskInfo = Util.GetMD5Value(DeviceHelper.GetDiskID() + typeof(int).ToString());
            tb_DiskSerial.Text = Util.GetNum(diskInfo, 8);
            //MAC地址
            string macInfo = Util.GetMD5Value(DeviceHelper.GetMacByNetworkInterface() + typeof(double).ToString());
            tb_BIOSSerial.Text = Util.GetNum(macInfo, 8);
            //机器码
            var machineCode = RegInfo.GetMachineCode();
            this.tb_MachineCode.Text = machineCode ?? "获取机器码失败";

            tb_AuthorityCode.Text = CommonHelper.ReadRegisteCode();
            tb_ValidTime.Text = CommonHelper.ReadValidTime();
            tb_AuthorityCode.Focus();
        }



        /// <summary>
        /// 获取BIOS序列号
        /// </summary>
        /// <returns></returns>
        private string GetBIOSSerial()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_BIOS");
                string sBIOSSerialNumber = "";
                foreach (ManagementObject mo in searcher.Get())
                {
                    sBIOSSerialNumber = mo.GetPropertyValue("SerialNumber").ToString().Trim();
                    break;
                }
                return sBIOSSerialNumber;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string GetCPUSerial()
        {
            try
            {
                string cpuInfo = "";//cpu序列号
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
                cpuInfo = cpuInfo.Substring(cpuInfo.Length - 5);//只保留最后五位数
                return cpuInfo;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private void GetDiskSerial()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher();
            mos.Query = new SelectQuery("Win32_DiskDrive", "", new string[] { "PNPDeviceID", "Signature" });
            ManagementObjectCollection myCollection = mos.Get();
            ManagementObjectCollection.ManagementObjectEnumerator em = myCollection.GetEnumerator();
            em.MoveNext();
            ManagementBaseObject mbo = em.Current;
            string id = mbo.Properties["signature"].Value.ToString().Trim();
            MessageBox.Show("硬盘序列号:" + id.Trim(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btn_SaveMachineCode_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 0;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                    {
                        writer.Write(tb_MachineCode.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("机器码保存失败!" + ex.Message);
            }
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_AuthorityCode.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("请填写注册码!");
                    return;
                }
                DateTime overTime = DateTime.Now;
                DateTime registerTime = DateTime.Now;
                var checkRes = RegInfo.CheckRegister(tb_AuthorityCode.Text.Trim(), ref overTime, ref registerTime);
                if (checkRes)
                {
                    //获取当前时间（如果有网络则读取网络时间，否则获取本机时间）
                    DateTime nowTime = Util.GetDateTimeNow();
                    //校验注册码注册时间
                    var resDay = Util.DiffDays(nowTime, registerTime);
                    if (resDay > 0)
                    {
                        MessageBox.Show("注册码错误（当前时间小于注册时间，可能断网修改电脑时间导致）");
                        return;
                    }
                    //校验注册码有效时间
                    var resDay1 = Util.DiffDays(nowTime, overTime);
                    if (resDay1 <= 0)
                    {
                        MessageBox.Show("注册码错误（当前时间大于注册码有效时间，注册码过期）");
                    }
                    else
                    {
                        tb_ValidTime.Text = overTime.ToString("yyyy-MM-dd HH:mm:ss");
                        CommonHelper.CreateRegisteValidTime(tb_ValidTime.Text);
                        CommonHelper.WriteRegisteCode(tb_AuthorityCode.Text);
                        CommonData.AuthorityValid = true;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("注册码错误（不是本机注册码）");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("注册异常!" + ex.Message);
            }
        }




    }
}
