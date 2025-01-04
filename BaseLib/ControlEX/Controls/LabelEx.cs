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
    /// Label扩展类
    /// </summary>
    public partial class LabelEx : Label, IControlEx
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LabelEx()
        {
            InitializeComponent();
        }

        #region 属性
        /// <summary>
        /// 显示模式 ShowData:显示数据 ShowDescription:显示数据 ShowValueUnit:显示数据的单位 
        /// </summary>
        [Category("RX.UI")]
        [Description("显示模式\r\nShowData:显示数据\r\nShowDescription:显示数据\r\nShowValueUnit:显示数据的单位\r\n")]
        public ShowDataMode IsShowValue { get; set; }

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
            set { _VariableName = value;
                Text = _VariableName;
            }
        }
        #endregion

        /// <summary>
        /// 设置数据绑定
        /// </summary>
        /// <param name="AlldataSouces"></param>
        public void SetDataBinding(object[] AlldataSouces)
        {
            if (!ControlExHeldper.GetReflectionData(AlldataSouces, VariableName, ObjectClassName, out ReflectionData rd))
                return;

            //显示数据
            if (IsShowValue == ShowDataMode.ShowData)
            {
                if (IsUseDataBinding)
                {
                    if (rd.propertyInfo == null)
                        return;
                    DataBindings.Add("Value", rd.DataContext, rd.FinalVariableName);
                }
                else
                {
                    Invoke(new Action(() => { Text = rd.objdd.ToString(); }));
                }
            }

            //显示描述Description
            else if (IsShowValue == ShowDataMode.ShowDescription)
            {
                var customAttributes = rd.propertyInfo != null ? rd.propertyInfo.GetCustomAttributes(false) : rd.fieldInfo.GetCustomAttributes(false);
                if (customAttributes.Length > 0)
                {
                    for (int i = 0; i < customAttributes.Length; i++)
                    {
                        if (customAttributes[i] is DescriptionAttribute theAttribute)
                        {
                            string str_Description = theAttribute.Description;
                            Invoke(new Action(() => { Text = str_Description; }));
                            break;
                        }
                    }
                }
            }
            else if (IsShowValue == ShowDataMode.ShowValueUnit)
            {
                var customAttributes = rd.propertyInfo != null ? rd.propertyInfo.GetCustomAttributes(false) : rd.fieldInfo.GetCustomAttributes(false);
                if (customAttributes.Length > 0)
                {
                    for (int i = 0; i < customAttributes.Length; i++)
                    {
                        if (customAttributes[i] is ValueUnitAttribute unitAttribute)
                        {
                            string str_Description = unitAttribute.ValueUnit;
                            Invoke(new Action(() => { Text = str_Description; }));
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取数据 空函数 不可用
        /// </summary>
        /// <param name="AlldataSouces">数据源</param>
        public void GettData(object[] AlldataSouces)
        {
            
        }
    }
}
