using System;
using System.Management;
using System.Net.NetworkInformation;

namespace SmartVEye
{
    /// <summary>
    /// 设备帮助类
    /// </summary>
    public class DeviceHelper
    {
        /// <summary>
        /// 获取本机MAC地址
        /// </summary>
        public static string GetMacByNetworkInterface()
        {
            try
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in interfaces)
                {
                    return BitConverter.ToString(ni.GetPhysicalAddress().GetAddressBytes());
                }
                return "UnknowMacInfo";
            }
            catch (Exception)
            {
                return "UnknowMacInfo";
            }
        }

        /// <summary>
        /// 取CPU序列号
        /// </summary>
        public static string GetCpuID()
        {
            try
            {
                string cpuInfo = "";
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc.Dispose();
                mc.Dispose();
                return cpuInfo;
            }
            catch
            {
                return "UnknowCpuInfo";
            }
        }

        /// <summary>
        /// 取硬盘序列号
        /// </summary>
        public static string GetDiskID()
        {
            try
            {
                string HDid = "";
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    HDid = (string)mo.Properties["Model"].Value;
                }
                moc.Dispose();
                mc.Dispose();
                return HDid;
            }
            catch
            {
                return "UnknowDiskInfo";
            }
        }
    }
}
