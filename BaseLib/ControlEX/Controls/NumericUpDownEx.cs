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
    /// NumericUpDown扩展类
    /// </summary>
    public partial class NumericUpDownEx : NumericUpDown, IControlEx
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NumericUpDownEx()
        {
            InitializeComponent();
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
        /// 设置数据绑定
        /// </summary>
        /// <param name="AlldataSouces"></param>
        public void SetDataBinding(object[] AlldataSouces)
        {
            if (!ControlExHeldper.GetReflectionData(AlldataSouces, VariableName, ObjectClassName, out ReflectionData rd))
                return;

            if (IsUseDataBinding)
            {
                if (rd.propertyInfo == null)
                    return;
                DataBindings.Add("Value", rd.DataContext, rd.FinalVariableName);
            }
            else
            {
                Value = Convert.ToDecimal(rd.objdd);
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="AlldataSouces"></param>
        public void GettData(object[] AlldataSouces)
        {
            if (IsUseDataBinding)
                return;

            if (!ControlExHeldper.GetReflectionData(AlldataSouces, VariableName, ObjectClassName
                    , out ReflectionData rd))
                return;

            bool valueIsok = true;
            string errMess = "";
            try
            {
                object setData = Convert.ChangeType(Value, rd.objdd.GetType());
                if (valueIsok)
                {
                    ControlExHeldper.SetReflectionData(rd, setData);
                    //if (rd.propertyInfo != null)
                    //    rd.propertyInfo.SetValue(rd.DataContext, setData);
                    //else
                    //    rd.fieldInfo.SetValue(rd.DataContext, setData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("[" + VariableName + "]输入不合法!" + ex.Message);
            }

            if (!valueIsok)
            {
                throw new Exception(errMess);
            }
        }

    }
}
