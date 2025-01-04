using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLib
{
    /// <summary>
    /// 单位描述
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ValueUnitAttribute : Attribute
    {
        /// <summary>
        /// 单位描述
        /// </summary>
        public string ValueUnit { get; set; }
        /// <summary>
        /// 单位描述
        /// </summary>
        /// <param name="data">数值</param>
        public ValueUnitAttribute(string data)
        {
            ValueUnit = data;
        }
    }

    /// <summary>
    /// 变量的最大最小值
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ValueMinMaxAttribute : Attribute
    {
        /// <summary>
        /// 最小值
        /// </summary>
        public double ValueMin { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public double ValueMax { get; set; }
        /// <summary>
        /// 变量的最大最小值
        /// </summary>
        /// <param name="data">数值</param>
        public ValueMinMaxAttribute(double min, double max)
        {
            ValueMin = min;
            ValueMax = max;
        }
    }
}
