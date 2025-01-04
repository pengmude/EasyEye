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
    /// ComboBox扩展类
    /// </summary>
    public partial class ComboBoxEx : ComboBox, IControlEx
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComboBoxEx()
        {
            InitializeComponent();
        }

        #region 属性

        /// <summary>
        /// 是否 用变量的描述当Items源,用逗号分隔
        /// </summary>
        [Category("RX.UI")]
        [Description("用变量的描述当Items源,用逗号分隔")]
        public bool IsItemFormDescription { get; set; }

        /// <summary>
        /// 是否使用数据绑定
        /// </summary>
        [Category("RX.UI")]
        [Description("使用数据绑定")]
        public bool IsUseDataBinding { get; set; }

        /// <summary>
        /// 变量名称
        /// </summary>
        [Category("RX.UI")]
        [Description("变量名称")]
        public string VariableName { get; set; }

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
        /// 对象类名
        /// </summary>
        [Category("RX.UI")]
        [Description("选择对象变更,数据绑定")]
        public string CurrentSelectedItem { get; set; }

        private object _SelectItem;
        //private object _LastItem;

        /// <summary>
        /// 选择项
        /// </summary>
        public object SelectItem
        {
            get
            {
                return SelectedItem;
            }
            set
            {
                if (null == value)
                    return;
                if (SelectedItem != value)
                {
                    SelectedItem = value;
                    _SelectItem = SelectedItem;
                }
            }
        }

        //public object SelectItem
        //{
        //    get
        //    {
        //        return SelectedItem;
        //    }
        //    set
        //    {
        //        if (null == value)
        //            return;
        //        if (_SelectItem != value)
        //        {
        //            SelectedItem = value;
        //            _SelectItem = SelectedItem;
        //        }
        //    }
        //}

        /// <summary>
        /// 设置控件数据源
        /// </summary>
        /// <param name="AlldataSouces"></param>
        public void SetDataBinding(object[] AlldataSouces)
        {
            if (!ControlExHeldper.GetReflectionData(AlldataSouces, VariableName, ObjectClassName, out ReflectionData rd))
                return;
            int theIndex = -1;
            if (rd.objdd is int intdata)
                theIndex = intdata;
            else if (rd.objdd is ushort ushortdata)
                theIndex = ushortdata;
            if (theIndex == -1)
                return;

            if (IsItemFormDescription)
            {
                string Description = ControlExHeldper.GetDescription(rd);
                if (!string.IsNullOrEmpty(Description))
                {
                    Items.Clear();
                    var dts = Description.Split(',');
                    foreach (var VARIABLE in dts)
                    {
                        Items.Add(VARIABLE);
                    }
                }
            }
            if (IsUseDataBinding)
            {
                if (rd.propertyInfo == null)
                    return;
                this.DataBindings.Add("SelectedIndex", rd.DataContext, rd.FinalVariableName);
            }
            else
            {
                SelectedIndex = theIndex;
            }
        }

        /// <summary>
        /// 获取控件数据源
        /// </summary>
        /// <param name="AlldataSouces"></param>
        public void GettData(object[] AlldataSouces)
        {
            if (!IsUseDataBinding)
            {
                if (!ControlExHeldper.GetReflectionData(AlldataSouces, VariableName, ObjectClassName, out ReflectionData rd))
                    return;
                try
                {
                    object setData = Convert.ChangeType(SelectedIndex, rd.objdd.GetType());
                    if (rd.propertyInfo != null)
                        rd.propertyInfo.SetValue(rd.DataContext, setData);
                    else
                        rd.fieldInfo.SetValue(rd.DataContext, setData);
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
