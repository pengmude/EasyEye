﻿using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartVEye
{
    public class DESHelper
    {
        /// <summary>
        /// 验证是否是正整数
        /// </summary>
        public static bool IsPositiveInteger(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            string pat = @"^[0-9]\d*$";
            Regex r = new Regex(pat, RegexOptions.Compiled);
            Match m = r.Match(value);
            if (!m.Success)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 计算时间天数差
        /// </summary>
        public static double DiffDays(DateTime startTime, DateTime endTime)
        {
            TimeSpan daysSpan = new TimeSpan(endTime.Ticks - startTime.Ticks);
            return daysSpan.TotalDays;
        }

        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="key">加密key</param>
        /// <param name="str">要加密的字符串</param>
        public static string ToEncryptString(string key, string str)
        {
            try
            {
                //将密钥字符串转换为字节序列
                var P_byte_key = Encoding.Unicode.GetBytes(key);
                //将字符串转换为字节序列
                var P_byte_data = Encoding.Unicode.GetBytes(str);
                //创建内存流对象
                MemoryStream mStream = new MemoryStream();
                {
                    using (CryptoStream P_CryptStream_Stream = new CryptoStream(mStream, new DESCryptoServiceProvider().CreateEncryptor(P_byte_key, P_byte_key), CryptoStreamMode.Write))
                    {
                        //向加密流中写入字节序列
                        P_CryptStream_Stream.Write(P_byte_data, 0, P_byte_data.Length);
                        //将数据压入基础流
                        P_CryptStream_Stream.FlushFinalBlock();
                        //从内存流中获取字节序列
                        var res = mStream.ToArray();
                        P_CryptStream_Stream.Dispose();
                        mStream.Dispose();
                        return Convert.ToBase64String(res);
                    }
                }
            }
            catch (CryptographicException ce)
            {
                throw new Exception(ce.Message);
            }
        }

        /// <summary>
        /// 字符串解密
        /// </summary>
        /// <param name="key">加密key</param>
        /// <param name="str">要解密的字符串</param>
        public static string ToDecryptString(string key, string str)
        {
            try
            {
                //将密钥字符串转换为字节序列
                var P_byte_key = Encoding.Unicode.GetBytes(key);
                //将加密后的字符串转换为字节序列
                var P_byte_data = Convert.FromBase64String(str);
                //创建内存流对象并写入数据,创建加密流对象
                CryptoStream cStream = new CryptoStream(new MemoryStream(P_byte_data), new DESCryptoServiceProvider().CreateDecryptor(P_byte_key, P_byte_key), CryptoStreamMode.Read);
                //创建字节序列对象
                var tempDate = new byte[200];
                //创建内存流对象
                MemoryStream mStream = new MemoryStream();
                //创建记数器
                int i = 0;
                //使用while循环得到解密数据
                while ((i = cStream.Read(tempDate, 0, tempDate.Length)) > 0)
                {
                    //将解密后的数据放入内存流
                    mStream.Write(tempDate, 0, i);
                }
                var res = Encoding.Unicode.GetString(mStream.ToArray());
                mStream.Dispose();
                cStream.Dispose();
                return res;
            }
            catch (CryptographicException ce)
            {
                throw new Exception(ce.Message);
            }
        }

        /// <summary>
        /// 取MD5
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        public static string GetMD5Value(string value)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] targetData = md5.ComputeHash(Encoding.Unicode.GetBytes(value));
            string resString = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                resString += targetData[i].ToString("x");
            }
            return resString;
        }

        /// <summary>
        /// 取数字
        /// </summary>
        /// <param name="md5"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GetNum(string md5, int len)
        {
            Regex regex = new Regex(@"\d");
            MatchCollection listMatch = regex.Matches(md5);
            string str = "";
            for (int i = 0; i < len; i++)
            {
                str += listMatch[i].Value;
            }
            while (str.Length < len)
            {
                //不足补0
                str += "0";
            }
            return str;
        }

        /// <summary>
        /// 获取网络时间
        /// </summary>
        public static DateTime GetInternetTime()
        {
            WebRequest request = null;
            WebResponse response = null;
            WebHeaderCollection headerCollection = null;
            string datetime = string.Empty;
            try
            {
                request = WebRequest.Create("https://www.baidu.com");
                request.Timeout = 1000;
                request.Credentials = CredentialCache.DefaultCredentials;
                response = (WebResponse)request.GetResponse();
                headerCollection = response.Headers;

                foreach (var h in headerCollection.AllKeys)
                {
                    if (h == "Date")
                    {
                        datetime = headerCollection[h];

                        var dt = DateTime.Parse(datetime);
                        return dt;
                    }
                }
                return new DateTime();
            }
            catch (Exception) { return new DateTime(); }
            finally
            {
                if (request != null)
                { request.Abort(); }
                if (response != null)
                { response.Close(); }
                if (headerCollection != null)
                { headerCollection.Clear(); }
            }
        }

        /// <summary>
        /// 获取当前时间（如果有网络则读取网络时间，否则获取本机时间）
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateTimeNow()
        {
            DateTime nowTime = DateTime.Now;
            try
            {
                nowTime = Util.GetInternetTime();
                if (nowTime.ToString() == "0001/1/1 0:00:00")
                {
                    nowTime = DateTime.Now;
                }
            }
            catch
            {
                throw;
            }
            return nowTime;
        }

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
        public static string CreateRegisterCode(string machineCode, DateTime overTime)
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
