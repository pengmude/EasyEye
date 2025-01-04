using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartLib
{
    /// <summary>
    /// CheckBox扩展类
    /// </summary>
    public partial class CheckBoxEx : CheckBox, IControlEx
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckBoxEx()
        {
            InitializeComponent();
        }

        #region 属性

        /// <summary>
        /// 是否使用数据绑定
        /// </summary>
        [Category("RX.UI")]
        [Description("使用数据绑定")]
        public bool IsUseDataBinding { get; set; }

        string _VariableName;
        /// <summary>
        /// 变量名称
        /// </summary>
        [Category("RX.UI")]
        [Description("变量名称")]
        public string VariableName
        {
            get { return _VariableName; }
            set
            {
                _VariableName = value;
                Text = _VariableName;
            }
        }

        /// <summary>
        /// 对象类名
        /// </summary>
        [Category("RX.UI")]
        [Description("对象类名")]
        public string ObjectClassName
        {
            get;
            set;
        }

        /// <summary>
        /// 设置控件数据源
        /// </summary>
        /// <param name="AlldataSouces">控件数据源</param>
        public void SetDataBinding(object[] AlldataSouces)
        {
            if (!ControlExHeldper.GetReflectionData(AlldataSouces, VariableName,ObjectClassName,out ReflectionData rd))
                return;

            if (rd.objdd is bool booldata)
                if (IsUseDataBinding)
                {
                    if (rd.propertyInfo == null)
                        return;
                    this.DataBindings.Add("Checked", rd.DataContext, rd.FinalVariableName);
                }
                else
                {
                    Checked = booldata;
                }
            else
                return;

            var customAttributes = rd.propertyInfo != null
                ? rd.propertyInfo.GetCustomAttributes(false)
                : rd.fieldInfo.GetCustomAttributes(false);

            if (customAttributes.Length > 0)
            {
                for (int i = 0; i < customAttributes.Length; i++)
                {
                    if (customAttributes[i] is DescriptionAttribute theAttribute)
                    {
                        string str_Description = theAttribute.Description;
                        Text = str_Description;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 获取控件数据源
        /// </summary>
        /// <param name="AlldataSouces">控件数据源</param>
        public void GettData(object[] AlldataSouces)
        {
            if (!IsUseDataBinding)
            {
                if (!ControlExHeldper.GetReflectionData(AlldataSouces, VariableName, ObjectClassName, out ReflectionData rd))
                    return;

                try
                {
                    object setData = Convert.ChangeType(Checked, rd.objdd.GetType());
                    ControlExHeldper.SetReflectionData(rd, setData);
                    //if (rd.propertyInfo != null)
                    //    rd.propertyInfo.SetValue(rd.DataContext, setData);
                    //else
                    //    rd.fieldInfo.SetValue(rd.DataContext, setData);
                }
                catch (Exception ex)
                {
                    throw new Exception("[" + Text + "]输入不合法!" + ex.Message);
                }
            }
        }


        #endregion
    }
}
