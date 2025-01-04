using System;

namespace SmartVEye
{
    /// <summary>
    ///注册信息
    /// </summary>
    public class RegInfo
    {
        //加密key
        public static string machineCodeEncryptKey = "9832";

        /// <summary>
        /// 取本机机器码
        /// </summary>
        public static string GetMachineCode()
        {
            //CPU信息
            string cpuInfo = Util.GetMD5Value(DeviceHelper.GetCpuID() + typeof(string).ToString());
            if (cpuInfo.Equals("UnknowCpuInfo")) return null;
            //磁盘信息
            string diskInfo = Util.GetMD5Value(DeviceHelper.GetDiskID() + typeof(int).ToString());
            if (diskInfo.Equals("UnknowDiskInfo")) return null;
            //MAC地址
            string macInfo = Util.GetMD5Value(DeviceHelper.GetMacByNetworkInterface() + typeof(double).ToString());
            if (macInfo.Equals("UnknowMacInfo")) return null;
            //返回机器码
            return Util.GetNum(cpuInfo, 8) + Util.GetNum(diskInfo, 8) + Util.GetNum(macInfo, 8);
        }

        /// <summary>
        /// 根据机器码产生注册码
        /// </summary>
        /// <param name="machineCode">机器码</param>
        /// <param name="overTime">到期时间</param>
        /// <returns>注册码</returns>
        public static string CreateRegisterCode(string machineCode,DateTime overTime)
        {
            //格式：机器码&过期时间&注册时间
            var finalCode = machineCode + "&" + overTime.ToString("s") + "&" + DateTime.Now.ToString("s");
            //加密
            return Util.ToEncryptString(machineCodeEncryptKey, finalCode);
        }

        /// <summary>
        /// 检查注册码（校验本地机器码）
        /// </summary>
        /// <param name="registerCode">注册码</param>
        /// <param name="overTime">返回过期时间</param>
        /// <param name="registerTime">返回注册时间</param>
        /// <returns>机器码与注册码匹配结果</returns>
        public static bool CheckRegister(string registerCode, ref DateTime overTime, ref DateTime registerTime)
        {
            try
            {
                var finalCodeList = Util.ToDecryptString(machineCodeEncryptKey, registerCode).Split('&');
                if (finalCodeList.Length == 3)
                {
                    DateTime.TryParse(finalCodeList[1], out overTime);
                    DateTime.TryParse(finalCodeList[2], out registerTime);
                    var machineCode = GetMachineCode();
                    return machineCode != null && (finalCodeList[0] == machineCode);
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
