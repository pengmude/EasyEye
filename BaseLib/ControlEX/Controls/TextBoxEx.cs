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
    /// TextBox扩展类
    /// </summary>
    public partial class TextBoxEx : Control, IControlEx
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TextBoxEx()
        {
            InitializeComponent();
            SomeComponent();

            this.TheTextBox.MouseWheel += new MouseEventHandler(textBox1_MouseWheel);

            TheTextBox.TextChanged += TheTextBox_TextChanged;
            TheTextBox.Leave += TheTextBox_Leave1;
        }

        #region TheTextBox的事件

        private void TheTextBox_Leave1(object sender, EventArgs e)
        {
            if (NeedCheckMinMax)
            {
                double value;
                if (double.TryParse(TheTextBox.Text, out value))
                {
                    if (value < Minimum)
                    {
                        Value = Minimum.ToString();
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
                if (double.TryParse(TheTextBox.Text, out value))
                {
                    if (value > Maximum)
                    {
                        Value = Maximum.ToString();
                        Task.Run(() => { MessageBox.Show("[" + Text + "]不在范围" + Minimum + "到" + Maximum + "之间!"); });
                    }
                }
            }
        }


        private void TheTextBox_Enter(object sender, EventArgs e)
        {
            _SaveColorLabel = LabelHint.ForeColor;
            _SaveColorLine = p2.BackColor;
            LabelHint.ForeColor = _SeletColor;
            p2.BackColor = _SeletColor;
        }

        private bool haveBig = false;
        private void TheTextBox_Leave(object sender, EventArgs e)
        {
            if (TheTextBox.Text == "")
            {
                HideTextBox();
            }
            LabelHint.ForeColor = _SaveColorLabel;
            p2.BackColor = _SaveColorLine;
        }

        private void TheTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (TheTextBox.Text == "")
                {
                    HideTextBox();
                }
                else
                {
                    if (NeedCheckMinMax)
                    {
                        double value;
                        if (double.TryParse(TheTextBox.Text, out value))
                        {
                            if (value < Minimum)
                            {
                                Value = Minimum.ToString();
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
                    double datava = System.Convert.ToDouble(this.Value);
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
                    this.Value = datava.ToString();
                }
            }
            catch (System.Exception)
            {

            }
        }
        #endregion

        #region 属性

        /// <summary>
        /// Lable和TextBox间距
        /// </summary>
        [Category("RX.布局")]
        [Description("Lable和TextBox间距")]
        public int HeightP5
        {
            get { return p5.Height; }
            set
            {
                this.p5.Height = value;
            }
        }

        /// <summary>
        /// 左边的间距
        /// </summary>
        [Category("RX.布局")]
        [Description("左边的间距")]
        public int WidthP3
        {
            get { return p3.Width; }
            set
            {
                this.p3.Width = value;
            }
        }

        /// <summary>
        /// Lable字体颜色
        /// </summary>
        [Category("RX.布局")]
        [Description("Lable字体颜色")]
        public Color LableForeColor
        {
            get { return LabelHint.ForeColor; }
            set
            {
                this.LabelHint.ForeColor = value;
            }
        }

        /// <summary>
        /// 文本字体颜色
        /// </summary>
        [Category("RX.布局")]
        [Description("文本字体颜色")]
        public Color TextBoxForeColor
        {
            get { return TheTextBox.ForeColor; }
            set
            {
                this.TheTextBox.ForeColor = value;
            }
        }

        /// <summary>
        /// 下划线颜色
        /// </summary>
        [Category("RX.布局")]
        [Description("下划线颜色")]
        public Color LineColor
        {
            get { return p2.BackColor; }
            set
            {
                this.p2.BackColor = value;
            }
        }

        /// <summary>
        /// 背景颜色
        /// </summary>
        [Category("RX.布局")]
        [Description("背景颜色")]
        public Color AllBackColor
        {
            get { return this.BackColor; }
            set
            {
                this.BackColor = value;
                this.p3.BackColor = value;
                this.p4.BackColor = value;
                this.p5.BackColor = value;
                this.p6.BackColor = value;
                this.LabelHint.BackColor = value;
                this.TheTextBox.BackColor = value;
            }
        }

        private Color _SeletColor;
        private Color _SaveColorLabel;
        private Color _SaveColorLine;

        /// <summary>
        /// 选中颜色
        /// </summary>
        [Category("RX.布局")]
        [Description("选中颜色")]
        public Color SeletColor
        {
            get { return _SeletColor; }
            set
            {
                this._SeletColor = value;
            }
        }

        /// <summary>
        /// Lable字体大小
        /// </summary>
        [Category("RX.布局")]
        [Description("Lable字体大小")]
        public Font LableFont
        {
            get { return LabelHint.Font; }
            set { LabelHint.Font = value; }
        }

        /// <summary>
        /// 字体大小
        /// </summary>
        [Category("RX.布局")]
        [Description("字体大小")]
        public Font TextBoxFont
        {
            get { return TheTextBox.Font; }
            set { TheTextBox.Font = value; }
        }

        /// <summary>
        /// Label的Text
        /// </summary>
        [Category("SX.UI")]
        [Description("Label的Text")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        public override string Text
        {
            get
            {
                return LabelHint.Text;
            }
            set
            {
                LabelHint.Text = value;
            }
        }


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

        #region 点击显示TextBox
        private void TextBoxEx2_Click(object sender, EventArgs e)
        {
            ShowTextBox();
            TheTextBox.Focus();
        }

        #endregion

        #region Label上拉下拉
        private Font SaveFont;
        /// <summary>
        /// 隐藏TexBox
        /// </summary>
        private void HideTextBox()
        {
            if (!haveBig)
            {
                haveBig = true;
                SaveFont = new Font(LableFont, LableFont.Style);
                LableFont = new Font(LableFont.FontFamily, LableFont.Size + 3);
                p4.Height = (this.Height - LabelHint.Height) / 2;
                p6.Visible = false;

            }

        }

        /// <summary>
        /// 隐藏TexBox
        /// </summary>
        private void ShowTextBox()
        {
            if (haveBig)
            {
                haveBig = false;
                if (SaveFont != null)
                {
                    LableFont = SaveFont;
                }


                p4.Height = 2;
                p6.Visible = true;
            }
        }
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
        /// 文本
        /// </summary>
        [Category("RX.UI")]
        [Description("文本")]

        public string Value
        {
            get
            {
                return TheTextBox.Text;
            }
            set
            {
                if (value == null)
                    return;
                if (TheTextBox.Text == value.ToString())
                    return;
                TheTextBox.Text = value.ToString();
                if (TheTextBox.Text == "")
                    HideTextBox();
                else
                    ShowTextBox();
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
                Value = rd.objdd.ToString();
            }
            var customAttributes = rd.propertyInfo != null ? rd.propertyInfo.GetCustomAttributes(false) : rd.fieldInfo.GetCustomAttributes(false);
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
        /// 获取数据
        /// </summary>
        /// <param name="AlldataSouces"></param>
        public void GettData(object[] AlldataSouces)
        {
            if (IsUseDataBinding)
                return;

            if (!ControlExHeldper.GetReflectionData(AlldataSouces, VariableName, ObjectClassName, out ReflectionData rd))
                return;

            bool valueIsok = true;
            string errMess = "";
            try
            {
                object setData = Convert.ChangeType(Value, rd.objdd.GetType());
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
                                valueIsok = false;
                                errMess = "[" + Text + "]不在范围" + Minimum +
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
            catch (Exception ex)
            {
                throw new Exception("[" + Text + "]输入不合法!" + ex.Message);
            }
            if (!valueIsok)
            {
                throw new Exception(errMess);
            }
        }
    }
}
