using System;

namespace SmartLib
{
    /// <summary>
    /// 数据转换工具类
    /// </summary>
    public static class ConvertHelper
    {
        /// <summary>
        /// 对象转int类型
        /// </summary>
        /// <param name="thisValue">需要转换的对象</param>
        /// <returns></returns>
        public static int ObjToInt(this object thisValue)
        {
            var reval = 0;
            if (thisValue == null) return 0;
            if (thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval)) return reval;
            return reval;
        }

        /// <summary>
        /// 对象转时间
        /// </summary>
        /// <param name="thisValue">需要转换的对象</param>
        /// <returns></returns>
        public static DateTime ObjToDate(this object thisValue)
        {
            var reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
                reval = Convert.ToDateTime(thisValue);
            return reval;
        }

        /// <summary>
        /// 对象转时间格式字符串
        /// </summary>
        /// <param name="thisValue">需要转换的对象</param>
        /// <returns></returns>
        public static string ObjToDateStr(this object thisValue)
        {
            var reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
                reval = Convert.ToDateTime(thisValue);
            return reval.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// 对象转时间格式字符串
        /// </summary>
        /// <param name="thisValue">需要转换的对象</param>
        /// <param name="forMat">字符串格式</param>
        /// <returns></returns>
        public static string ObjToDateStr(this object thisValue, string forMat = "yyyy-MM-dd HH:mm:ss.fff")
        {
            var reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
                reval = Convert.ToDateTime(thisValue);
            return reval.ToString(forMat);
        }

        /// <summary>
        /// 对象转bool类型
        /// </summary>
        /// <param name="thisValue">需要转换的对象</param>
        /// <returns></returns>
        public static bool ObjToBool(this object thisValue)
        {
            var reval = false;
            if (thisValue != null && thisValue != DBNull.Value &&
                bool.TryParse(thisValue.ToString(), out reval)) return reval;
            return reval;
        }

        /// <summary>
        ///     获取当前时间的时间戳
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static string DateToTimeStamp(this DateTime thisValue)
        {
            var ts = thisValue - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds)
                .ToString();
        }
    }
}