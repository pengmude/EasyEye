using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartLib
{
    /// <summary>
    /// 控件扩展工具类
    /// </summary>
    public class ControlExHeldper
    {
        /// <summary>
        /// 自动查找控件并赋值(只能是属性)
        /// </summary>
        /// <param name="control">保存TextBoxEx和CheckBoxEx的容器</param>
        /// <param name="data">保存属性的对象</param>
        /// <returns></returns>
        public static bool SetAllTBExValue(Control control, params object[] data)
        {
            bool result = true;
            List<IControlEx> allTBEx = new List<IControlEx>();
            if (control is IControlEx rxcontrol)
            {
                allTBEx.Add(rxcontrol);
            }
            GetAllExControl(control, ref allTBEx);
            foreach (var rxControl in allTBEx)
            {
                rxControl.SetDataBinding(data);
            }
            return result;
        }


        /// <summary>
        /// 自动查找控件并保存数据(只能是属性)
        /// </summary>
        /// <param name="control">保存TextBoxEx和CheckBoxEx的容器</param>
        /// <param name="data">保存属性的对象</param>
        /// <returns></returns>
        public static bool GetAllTBExValue(Control control, params object[] data)
        {
            bool result = true;
            List<IControlEx> allTBEx = new List<IControlEx>();
            if (control is IControlEx rxcontrol)
            {
                allTBEx.Add(rxcontrol);
            }
            GetAllExControl(control, ref allTBEx);
            foreach (var rxControl in allTBEx)
            {
                rxControl.GettData(data);
            }
            return result;
        }


        private static void GetAllExControl(Control control, ref List<IControlEx> exControls)
        {
            foreach (Control tbex in control.Controls)
            {
                if (tbex is IControlEx rxControl)
                {
                    exControls.Add(rxControl);
                }
                if (tbex.Controls.Count > 0)
                {
                    GetAllExControl(tbex, ref exControls);
                }
            }
        }

        internal static bool GetReflectionData(object[] AlldataSouces, string VariableName, string ObjectClassName, out ReflectionData rD)
        {
            rD = null;
            #region 获取最后的对象和值
            if (string.IsNullOrEmpty(VariableName))
                return false;
            var nameArry = SplitVariableName(VariableName);
            //var nameArry = VariableName.Split('.');
            //最终变量的名称
            DicOrArryRef FinalVariableName = Checkname(nameArry[0]);
            object DataContext = null;
            PropertyInfo propertyInfo = null;
            FieldInfo fieldInfo = null;
            for (int i = 0; i < AlldataSouces.Length; i++)
            {
                Type tt = AlldataSouces[i].GetType();
                propertyInfo = tt.GetProperty(FinalVariableName.VarName);
                if (propertyInfo != null)
                {
                    if (!string.IsNullOrEmpty(ObjectClassName))
                        if (ObjectClassName != tt.Name)
                            continue;
                    DataContext = AlldataSouces[i];
                    break;
                }

                fieldInfo = tt.GetField(FinalVariableName.VarName);
                if (fieldInfo != null)
                {
                    if (!string.IsNullOrEmpty(ObjectClassName))
                        if (ObjectClassName != tt.Name)
                            continue;
                    DataContext = AlldataSouces[i];
                    break;
                }
            }

            if (DataContext == null)
                return false;

            var objdd = propertyInfo != null
                ? propertyInfo.GetValue(DataContext, null)
                : fieldInfo.GetValue(DataContext);

            if (objdd == null)
                return false;

            if (FinalVariableName.FinalDicOrArryOrList)
            {
                objdd = GetDicOrListValue(objdd, FinalVariableName.KeyValue);
                if (objdd == null)
                    return false;
            }

            if (nameArry.Count > 1)
            {
                //获取下一个对像
                DataContext = objdd;
                //倒数第2的对象
                object LastDataContext = null;
                //上一次名字存储器
                var lastDicOrArryRef = FinalVariableName;
                for (int i = 1; i < nameArry.Count; i++)
                {
                    if (i == (nameArry.Count - 1))
                    {
                        LastDataContext = DataContext;
                        FinalVariableName = Checkname(nameArry[i]);
                    }
                    Type tt = DataContext.GetType();
                    lastDicOrArryRef = Checkname(nameArry[i]);
                    propertyInfo = tt.GetProperty(lastDicOrArryRef.VarName);
                    if (propertyInfo != null)
                    {
                        var objddNext = propertyInfo.GetValue(DataContext, null);
                        if (objddNext == null)
                            return false;
                        if (lastDicOrArryRef.FinalDicOrArryOrList)
                        {
                            objddNext = GetDicOrListValue(objddNext, lastDicOrArryRef.KeyValue);
                            if (objddNext == null)
                                return false;
                        }
                        DataContext = objddNext;
                        continue;
                    }
                    fieldInfo = tt.GetField(lastDicOrArryRef.VarName);
                    if (fieldInfo != null)
                    {
                        var objddNext = fieldInfo.GetValue(DataContext);
                        if (objddNext == null)
                            return false;
                        if (lastDicOrArryRef.FinalDicOrArryOrList)
                        {
                            objddNext = GetDicOrListValue(objddNext, lastDicOrArryRef.KeyValue);
                            if (objddNext == null)
                                return false;
                        }
                        DataContext = objddNext;
                        continue;
                    }
                }
                objdd = DataContext;
                DataContext = LastDataContext;
            }
            if (DataContext == null)
                return false;
            if (objdd == null)
                return false;
            rD = new ReflectionData();
            rD.FinalVariableName = FinalVariableName.VarName;
            rD.DataContext = DataContext;

            if (FinalVariableName.FinalDicOrArryOrList)
            {
                rD.FinalDicOrArryOrList = true;
                rD.KeyValue = FinalVariableName.KeyValue;
            }

            if (propertyInfo != null)
            {
                rD.propertyInfo = propertyInfo;
            }
            if (fieldInfo != null)
            {
                rD.fieldInfo = fieldInfo;
            }
            rD.objdd = objdd;
            return true;
            #endregion
        }


        /// <summary>
        /// 将连接名分开,后面引进字典和数组添加的方法
        /// </summary>
        /// <param name="VariableName"></param>
        /// <returns></returns>
        internal static List<string> SplitVariableName(string VariableName)
        {
            List<string> nameList = new List<string>();
            bool isinkh = false;
            string nameone = "";
            for (int i = 0; i < VariableName.Length; i++)
            {
                if (!isinkh && VariableName[i] == '.')
                {
                    nameList.Add(nameone);
                    nameone = "";
                }
                else
                {
                    nameone += VariableName[i].ToString();
                }
                if (VariableName[i] == '[')
                {
                    isinkh = true;
                }
                else if (VariableName[i] == ']')
                {
                    isinkh = false;
                }
            }
            if (nameone != "")
            {
                nameList.Add(nameone);
            }
            return nameList;
        }


        /// <summary>
        /// 反射设置值
        /// </summary>
        /// <param name="rD"></param>
        /// <param name="theSetValue"></param>
        /// <returns></returns>
        internal static bool SetReflectionData(ReflectionData rD, object theSetValue)
        {
            //是数组
            if (rD.FinalDicOrArryOrList)
            {
                object dicorlist = null;
                if (rD.propertyInfo != null)
                    dicorlist = rD.propertyInfo.GetValue(rD.DataContext);
                else
                    dicorlist = rD.fieldInfo.GetValue(rD.DataContext);
                if (dicorlist is IDictionary idic)
                {
                    if (!idic.Contains(rD.KeyValue))
                        return false;
                    idic[rD.KeyValue] = theSetValue;
                }
                else if (dicorlist is IList il)
                {
                    if (!(il.Count > (int)rD.KeyValue))
                        return false;
                    il[(int)rD.KeyValue] = theSetValue;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (rD.propertyInfo != null)
                    rD.propertyInfo.SetValue(rD.DataContext, theSetValue);
                else
                    rD.fieldInfo.SetValue(rD.DataContext, theSetValue);
            }
            return true;
        }

        /// <summary>
        /// 判断是否为数组或字典
        /// </summary>
        /// <param name="objdd"></param>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        internal static object GetDicOrListValue(object objdd, object KeyValue)
        {
            if (objdd is IDictionary idic)
            {
                if (!idic.Contains(KeyValue))
                    return null;
                return idic[KeyValue];
            }
            else if (objdd is IList il)
            {
                if (!(il.Count > (int)KeyValue))
                    return null;
                return il[(int)KeyValue];
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 是否为值类型
        /// </summary>
        /// <param name="thevalue"></param>
        /// <returns></returns>
        internal static bool IsNormalValue(object thevalue)
        {
            if ((thevalue is double || thevalue is int || thevalue is float
                 || thevalue is uint || thevalue is long || thevalue is short
                 || thevalue is ushort))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 检查变量是否是字典或数组
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static DicOrArryRef Checkname(string name)
        {
            if (!name.Contains('['))
            {
                return new DicOrArryRef(name);
            }
            int index = name.IndexOf('[');
            string norname = name.Substring(0, index);
            string key = name.Substring(index);
            DicOrArryRef res = new DicOrArryRef();
            res.VarName = norname;
            res.FinalDicOrArryOrList = true;
            key = key.Trim('[', ']');
            if (key.Contains('"'))
            {
                res.KeyValue = key.Trim('"');
            }
            else if (key.Contains('.'))
            {
                res.KeyValue = Convert.ToDouble(key);
            }
            else
            {
                res.KeyValue = Convert.ToInt32(key);
            }
            return res;
        }



        /// <summary>
        /// 反射设置值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="instance"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ReflectSetValue(string name, object instance, object value)
        {
            if (!GetReflectionData(new[] { instance }, name, "", out ReflectionData rd))
                return false;
            return SetReflectionData(rd, value);
        }

        /// <summary>
        /// 反射获取值值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static object ReflectGetValue(string name, object instance)
        {
            if (!GetReflectionData(new[] { instance }, name, "", out ReflectionData rd))
                return false;
            return rd.objdd;
        }

        /// <summary>
        /// 返回描述
        /// </summary>
        /// <param name="rD"></param>
        /// <returns></returns>
        internal static string GetDescription(ReflectionData rD)
        {
            string str_Description = "";
            var customAttributes = rD.propertyInfo != null ? rD.propertyInfo.GetCustomAttributes(false) : rD.fieldInfo.GetCustomAttributes(false);
            if (customAttributes.Length > 0)
            {
                for (int i = 0; i < customAttributes.Length; i++)
                {
                    if (customAttributes[i] is DescriptionAttribute theAttribute)
                    {
                        str_Description = theAttribute.Description;
                        break;
                    }
                }
            }
            return str_Description;
        }

        /// <summary>
        /// 获取最大最小值特性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static Response<ValueMinMaxAttribute> GetMinMaxAttribute(string name, object instance)
        {
            if (!GetReflectionData(new[] { instance }, name, "", out ReflectionData rd))
                return Response<ValueMinMaxAttribute>.Fail("不存在该变量!");
            var customAttributes = rd.propertyInfo != null ? rd.propertyInfo.GetCustomAttributes(false) : rd.fieldInfo.GetCustomAttributes(false);
            if (customAttributes.Length > 0)
            {
                for (int i = 0; i < customAttributes.Length; i++)
                {
                    if (customAttributes[i] is ValueMinMaxAttribute theAttribute)
                    {
                        return Response<ValueMinMaxAttribute>.Ok(theAttribute);
                    }
                }
            }
            return Response<ValueMinMaxAttribute>.Fail("不存在ValueMinMaxAttribute!");
        }
    }

    internal class ReflectionData
    {
        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo propertyInfo = null;
        /// <summary>
        /// 字段信息
        /// </summary>
        public FieldInfo fieldInfo = null;
        /// <summary>
        /// 最后变量的名字
        /// </summary>
        public string FinalVariableName;
        /// <summary>
        /// 最后的对象
        /// </summary>
        public object DataContext = null;
        /// <summary>
        /// 值
        /// </summary>
        public object objdd = null;

        /// <summary>
        /// 是字典或数组或列表的键值
        /// </summary>
        public object KeyValue = null;

        /// <summary>
        /// 最后一个变量是字典或数组或列表
        /// </summary>
        public bool FinalDicOrArryOrList = false;

    }


    /// <summary>
    /// 字典类型或数组反射
    /// </summary>
    internal class DicOrArryRef
    {
        public DicOrArryRef(string name)
        {
            VarName = name;
        }
        public DicOrArryRef()
        {
        }

        /// <summary>
        /// 是字典或数组或列表的键值
        /// </summary>
        public object KeyValue = null;

        /// <summary>
        /// 属性名或字段名
        /// </summary>
        public string VarName = "";

        /// <summary>
        /// 最后一个变量是字典或数组或列表
        /// </summary>
        public bool FinalDicOrArryOrList = false;
    }

    /// <summary>
    /// 数据显示模式
    /// </summary>
    public enum ShowDataMode
    {
        /// <summary>
        /// 显示数据,可以显示描述的话会显示
        /// </summary>
        ShowData = 0,
        /// <summary>
        /// 显示描述
        /// </summary>
        ShowDescription = 1,
        /// <summary>
        /// 显示数据的单位
        /// </summary>
        ShowValueUnit = 2
    }
}
