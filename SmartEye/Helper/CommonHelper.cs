using Microsoft.Win32;
using SmartLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartVEye
{
    public class CommonHelper
    {
        /// <summary>
        /// 写入注册码
        /// </summary>
        /// <param name="validTime"></param>
        public static void CreateRegisteValidTime(string validTime)
        {
            RegistryKey rklm = Registry.CurrentUser;
            RegistryKey softKey = rklm.OpenSubKey(@"SOFTWARE", true);
            RegistryKey smartEyeKey = softKey.CreateSubKey(@"SmartEye", true);
            RegistryKey totalTime = smartEyeKey.CreateSubKey("ValidTime");
            totalTime.SetValue("ValidTime", validTime);
        }

        public static void WriteRegisteCode(string code)
        {
            RegistryKey rklm = Registry.CurrentUser;
            RegistryKey softKey = rklm.OpenSubKey(@"SOFTWARE", true);
            RegistryKey smartEyeKey = softKey.OpenSubKey(@"SmartEye", true);
            RegistryKey registeCode = smartEyeKey.CreateSubKey("RegisteCode");
            registeCode.SetValue("RegisteCode", code);
        }

        public static string ReadRegisteCode()
        {
            RegistryKey rklm = Registry.CurrentUser;
            RegistryKey softKey = rklm.OpenSubKey(@"SOFTWARE", true);
            RegistryKey smartEyeKey = softKey.OpenSubKey(@"SmartEye", true);
            if (smartEyeKey == null) return "";
            RegistryKey registeCodeKey = smartEyeKey.OpenSubKey(@"RegisteCode", true);
            if (registeCodeKey == null) return "";
            object registeCode = registeCodeKey.GetValue("RegisteCode");
            if (registeCode == null) return "";
            else return registeCode.ToString();
        }

        public static string ReadValidTime()
        {
            RegistryKey rklm = Registry.CurrentUser;
            RegistryKey softKey = rklm.OpenSubKey(@"SOFTWARE", true);
            RegistryKey smartEyeKey = softKey.OpenSubKey(@"SmartEye", true);
            if (smartEyeKey == null) return "";
            RegistryKey validTimeKey = smartEyeKey.OpenSubKey(@"ValidTime", true);
            if (validTimeKey == null) return "";
            object validTime = validTimeKey.GetValue("ValidTime");
            if (validTime == null) return "";
            else return validTime.ToString();
        }

        /// <summary>
        /// 验证注册码时间
        /// </summary>
        /// <returns></returns>
        public static Response ReadRegisteValidTime()
        {
            RegistryKey rklm = Registry.CurrentUser;
            RegistryKey softKey = rklm.OpenSubKey(@"SOFTWARE", true);
            RegistryKey smartEyeKey = softKey.OpenSubKey(@"SmartEye", true);
            if (smartEyeKey == null) return Response.Fail("软件未注册,请联络制造商!");
            RegistryKey validTimeKey = smartEyeKey.OpenSubKey("ValidTime", true);
            if (smartEyeKey == null) return Response.Fail("软件未注册,请联络制造商!");
            string validTimeStr = validTimeKey.GetValue("ValidTime").ToString();
            if (validTimeStr.Length <= 0) return Response.Fail("软件未注册,请联络制造商!");
            //获取当前时间（如果有网络则读取网络时间，否则获取本机时间）
            DateTime nowTime = Util.GetDateTimeNow();
            //校验注册码有效时间
            var resDay1 = Util.DiffDays(nowTime, Convert.ToDateTime(validTimeStr));
            if (resDay1 <= 0) return Response.Fail("软件已过期,请联络制造商!");
            else return Response.Ok();
        }
    }
}
