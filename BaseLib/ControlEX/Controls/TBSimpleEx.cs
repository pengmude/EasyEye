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
    /// TBSimple扩展类
    /// </summary>
    public partial class TBSimpleEx : TextBox, IControlEx
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TBSimpleEx()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(textBox1_MouseWheel);
            this.TextChanged += TheTextBox_TextChanged;
            this.Leave += TheTextBox_Leave1;
            this.KeyPress += TheTextBox_KeyPress;
        }

        #region TheTextBox的事件

        private void TheTextBox_Leave1(object sender, EventArgs e)
        {
            if (NeedCheckMinMax)
            {
                double value;
                if (double.TryParse(Text, out value))
                {
                    if (value < Minimum)
                    {
                        Text = Minimum.ToString();
                        Task.Run(() => { MessageBox.Show("[" + Text + "]不在范围" + Minimum + "到" + Maximum + "之间!"); });
                    }
                }
            }
        }


        private void TheTextBox_TextChanged(object sender, EventArgs e)
        {

            if (NeedCheckMinMax)
            {
                double value;
                if (double.TryParse(Text, out value))
                {
                    if (value > Maximum)
                    {
                        Text = Maximum.ToString();
                        Task.Run(() => { MessageBox.Show("[" + Text + "]不在范围" + Minimum + "到" + Maximum + "之间!"); });
                    }
                }
            }
        }

        private void TheTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Text != "")
                {
                    if (NeedCheckMinMax)
                    {
                        double value;
                        if (double.TryParse(Text, out value))
                        {
                            if (value < Minimum)
                            {
                                Text = Minimum.ToString();
                                Task.Run(() => { MessageBox.Show("[" + Text + "]不在范围" + Minimum + "到" + Maximum + "之间!"); });
                            }
                        }
                    }
                    DataBindings["Value"]
                        ?.WriteValue();
                }
                e.Handled = true;
            }
        }

        private void textBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.Increment != 0)
                {
                    double datava = System.Convert.ToDouble(this.Text);
                    if (e.Delta > 0)
                    {
                        datava += Increment;
                    }
                    else
                    {
                        datava -= Increment;
                    }
                    if (datava < Minimum || datava > Maximum)
                        return;
                    this.Text = datava.ToString();
                }
            }
            catch (System.Exception)
            {

            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 最大值
        /// </summary>
        [Category("RX.UI")]
        [Description("最大值")]
        public double Maximum
        {
            get;
            set;
        } = 6666666666666666;


        /// <summary>
        /// 最小值
        /// </summary>
        [Category("RX.UI")]
        [Description("最小值")]
        public double Minimum
        {
            get;
            set;
        } = -6666666666666666;


        /// <summary>
        /// 鼠标滚轮变化改变的数值
        /// </summary>
        [Category("RX.UI")]
        [Description("鼠标滚轮变化改变的数值")]
        public double Increment
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 是否需要检查数值范围
        /// </summary>
        [Category("RX.UI")]
        [Description("需要检查数值范围")]
        public bool NeedCheckMinMax { get; set; }

        #endregion

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
                DataBindings.Add("Text", rd.DataContext, rd.FinalVariableName);
            }
            else
            {
                Invoke(new Action(() => { Text = rd.objdd.ToString(); }));
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="AlldataSouces"></param>
        public void GettData(object[] AlldataSouces)
        {

            if (!IsUseDataBinding)
            {
                if (!ControlExHeldper.GetReflectionData(AlldataSouces, VariableName, ObjectClassName, out ReflectionData rd))
                    return;
                bool valueIsok = true;
                string errMess = "";
                try
                {
                    object setData = Convert.ChangeType(Text, rd.objdd.GetType());
                    try
                    {
                        if (ControlExHeldper.IsNormalValue(setData))
                        {
                            if (!NeedCheckMinMax)
                            {
                                var doublevalue = Convert.ToDouble(setData);
                                if (doublevalue > Maximum ||
                                    doublevalue < Minimum)
                                {
                                    var customAttributes = rd.propertyInfo != null ? rd.propertyInfo.GetCustomAttributes(false) : rd.fieldInfo.GetCustomAttributes(false);
                                    string Description = VariableName;
                                    if (customAttributes.Length > 0)
                                    {
                                        for (int i = 0; i < customAttributes.Length; i++)
                                        {
                                            if (customAttributes[i] is DescriptionAttribute theAttribute)
                                            {
                                                string str_Description = theAttribute.Description;
                                                Description = str_Description;
                                                break;
                                            }
                                        }
                                    }
                                    valueIsok = false;
                                    errMess = "[" + Description + "]不在范围" + Minimum +
                                              "到" + Maximum + "之间!";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        valueIsok = false;
                        errMess = ex.Message;
                    }

                    if (valueIsok)
                    {
                        ControlExHeldper.SetReflectionData(rd, setData);
                        //if (rd.propertyInfo != null)
                        //    rd.propertyInfo.SetValue(rd.DataContext, setData);
                        //else
                        //    rd.fieldInfo.SetValue(rd.DataContext, setData);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("[" + VariableName + "]输入不合法!" + e.Message);
                }

                if (!valueIsok)
                {
                    throw new Exception(errMess);
                }

            }
        }
    }
}
