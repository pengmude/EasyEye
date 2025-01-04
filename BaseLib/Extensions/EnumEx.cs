using System;
using System.ComponentModel;
using System.Reflection;

namespace SmartLib
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumEx
    {
        /// <summary>
        ///     获取特性 (DescriptionAttribute) 的说明；如果未使用该特性，则返回枚举的名称。可指定的默认值。
        /// </summary>
        /// <param name="enum"></param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public static string GetDescription(this Enum @enum, string def = "")
        {
            var enumType = @enum.GetType();
            var value = int.Parse(Enum.Format(enumType, Enum.Parse(enumType, @enum.ToString()), "d"));
            var fieldInfo = enumType.GetField(Enum.GetName(enumType, value));

            var descriptionAttribute =
                fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute), false) as DescriptionAttribute;
            if (descriptionAttribute != null) return descriptionAttribute.Description;
            return def != "" ? def : @enum.ToString();
        }
    }
}