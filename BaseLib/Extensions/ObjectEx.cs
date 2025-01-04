using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SmartLib
{
    /// <summary>
    /// Object扩展方法类
    /// </summary>
    public static class ObjectEx
    {
        /// <summary>
        ///     属性映射(静态对象，无需重复建立属性映射关系，提高效率)
        /// </summary>
        public static Dictionary<string, List<string>> MapDic = new Dictionary<string, List<string>>();

        /// <summary>
        ///     S复制到D(创建对象D)
        /// </summary>
        /// <typeparam name="D">输出对象类型</typeparam>
        /// <typeparam name="S">输入对象类型</typeparam>
        /// <param name="s">输入对象</param>
        /// <returns></returns>
        public static D Copy<S, D>(S s) where D : class, new() where S : class, new()
        {
            if (s == null) return default;
            //使用无参数构造函数，创建指定泛型类型参数所指定类型的实例
            var d = Activator.CreateInstance<D>();
            return s.Copy(d);
        }

        /// <summary>
        ///     S复制到D(对象D已存在)
        /// </summary>
        /// <typeparam name="D">输出对象类型</typeparam>
        /// <typeparam name="S">输入对象类型</typeparam>
        /// <param name="s">输入对象</param>
        /// <param name="d">输出对象</param>
        /// <returns></returns>
        public static D Copy<S, D>(this S s, D d)
        {
            if (s == null || d == null) return d;
            try
            {
                var sType = s.GetType();
                var dType = typeof(D);
                //属性映射Key
                var mapkey = dType.FullName + "_" + sType.FullName;
                if (MapDic.ContainsKey(mapkey))
                {
                    //已存在属性映射
                    foreach (var item in MapDic[mapkey])
                        //按照属性映射关系赋值
                        //.net 4
                        //dType.GetProperty(item).SetValue(d, sType.GetProperty(item).GetValue(s, null), null);
                        //.net 4.5
                        dType.GetProperty(item)
                            .SetValue(d, sType.GetProperty(item)
                                .GetValue(s));
                }
                else
                {
                    //不存在属性映射，需要建立属性映射
                    var namelist = new List<string>();
                    var dic = new Dictionary<string, TypeAndValue>();
                    //遍历获取输入类型的属性（属性名称，类型，值）
                    foreach (var sP in sType.GetProperties())
                        //.net 4
                        //dic.Add(sP.Name, new TypeAndValue() { type = sP.PropertyType, value = sP.GetValue(s, null) });
                        //.net 4.5
                        dic.Add(sP.Name, new TypeAndValue { type = sP.PropertyType, value = sP.GetValue(s) });
                    //遍历输出类型的属性，并与输入类型（相同名称和类型的属性）建立映射，并赋值
                    foreach (var dP in dType.GetProperties())
                        if (dic.Keys.Contains(dP.Name))
                            if (dP.PropertyType == dic[dP.Name]
                                .type)
                            {
                                namelist.Add(dP.Name);
                                //.net 4
                                dP.SetValue(d, dic[dP.Name]
                                    .value, null);
                                //.net 4.5
                                //dP.SetValue(d, dic[dP.Name].value);
                            }

                    //保存映射
                    if (!MapDic.ContainsKey(mapkey)) MapDic.Add(mapkey, namelist);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }

            return d;
        }

        /// <summary>
        ///     SList复制到DList
        /// </summary>
        /// <typeparam name="D">输出对象类型</typeparam>
        /// <typeparam name="S">输入对象类型</typeparam>
        /// <param name="sList">输入对象集合</param>
        /// <returns></returns>
        public static IQueryable<D> Copy<S, D>(IQueryable<S> sList) where D : class, new() where S : class, new()
        {
            var dList = new List<D>();
            foreach (var item in sList) dList.Add(Copy<S, D>(item));
            return dList.AsQueryable();
        }

        /// <summary>
        ///     SList复制到DList
        /// </summary>
        /// <typeparam name="D">输出对象类型</typeparam>
        /// <typeparam name="S">输入对象类型</typeparam>
        /// <param name="sList">输入对象集合</param>
        /// <returns></returns>
        public static IEnumerable<D> Copy<S, D>(IEnumerable<S> sList) where D : class, new() where S : class, new()
        {
            var dList = new List<D>();
            foreach (var item in sList) dList.Add(Copy<S, D>(item));
            return dList.AsEnumerable();
        }
    }

    /// <summary>
    ///     类型和值
    /// </summary>
    internal class TypeAndValue
    {
        /// <summary>
        ///     类型
        /// </summary>
        public Type type { get; set; }

        /// <summary>
        ///     值
        /// </summary>
        public object value { get; set; }
    }
}