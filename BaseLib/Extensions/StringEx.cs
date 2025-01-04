using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace SmartLib
{
    /// <summary>
    /// 字符串扩展工具类
    /// </summary>
    [Serializable]
    public static class StringEx
    {
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        ///     文件名是否有效
        /// </summary>
        /// <returns></returns>
        public static bool IsValidFileName(this string fileName)
        {
            return !string.IsNullOrEmpty(fileName) && fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
        }

        /// <summary>
        ///     校验IP地址的正确性，同时支持IPv4和IPv6
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="isMatch">是否匹配成功，若返回true，则会得到一个Match对象，否则为null</param>
        /// <returns>匹配对象</returns>
        public static IPAddress MatchInetAddress(this string s, out bool isMatch)
        {
            isMatch = IPAddress.TryParse(s, out var ip);
            return ip;
        }

        /// <summary>
        ///     校验IP地址的正确性，同时支持IPv4和IPv6
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>是否匹配成功</returns>
        public static bool MatchInetAddress(this string s)
        {
            MatchInetAddress(s, out var success);
            return success;
        }

        #region 校验手机号码的正确性

        /// <summary>
        ///     匹配手机号码
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="isMatch">是否匹配成功，若返回true，则会得到一个Match对象，否则为null</param>
        /// <returns>匹配对象</returns>
        public static Match MatchPhoneNumber(this string s, out bool isMatch)
        {
            if (string.IsNullOrEmpty(s))
            {
                isMatch = false;
                return null;
            }

            var match = Regex.Match(s, @"^((1[3,5,6,8][0-9])|(14[5,7])|(17[0,1,3,6,7,8])|(19[8,9]))\d{8}$");
            isMatch = match.Success;
            return isMatch ? match : null;
        }

        /// <summary>
        ///     匹配手机号码
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>是否匹配成功</returns>
        public static bool MatchPhoneNumber(this string s)
        {
            MatchPhoneNumber(s, out var success);
            return success;
        }

        #endregion 校验手机号码的正确性

        /// <summary>
        /// 返回路径字符的合并值
        /// </summary>
        /// <param name="path1">路径1</param>
        /// <param name="path2">路径2</param>
        /// <returns></returns>
        public static string PathCombine(this string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }

        /// <summary>
        /// 返回路径字符的合并值
        /// </summary>
        /// <param name="path1">路径1</param>
        /// <param name="path2">路径2</param>
        /// <param name="path3">路径3</param>
        /// <returns></returns>
        public static string PathCombine(this string path1, string path2, string path3)
        {
            return Path.Combine(path1, path2, path3);
        }

        /// <summary>
        /// 返回路径字符的合并值
        /// </summary>
        /// <param name="path1">path1</param>
        /// <param name="path2">path2</param>
        /// <param name="path3">path3</param>
        /// <param name="path4">path4</param>
        /// <returns></returns>
        public static string PathCombine(this string path1, string path2, string path3, string path4)
        {
            return Path.Combine(path1, path2, path3, path4);
        }

        /// <summary>
        /// 返回路径字符的合并值
        /// </summary>
        /// <param name="path1">path1</param>
        /// <param name="path2">path2</param>
        /// <param name="path3">path3</param>
        /// <param name="path4">path4</param>
        /// <param name="path5">path5</param>
        /// <returns></returns>
        public static string PathCombine(this string path1, string path2, string path3, string path4, string path5)
        {
            return Path.Combine(path1, path2, path3, path4, path5);
        }
    }
}