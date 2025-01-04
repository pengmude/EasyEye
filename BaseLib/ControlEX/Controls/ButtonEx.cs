using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartLib
{
    /// <summary>
    /// 按钮扩展类
    /// </summary>
    public partial class ButtonEx : Button
    {
        /// <summary>
        /// 设置按键颜色的委托
        /// </summary>
        /// <param name="StatusTagName">标签名称</param>
        /// <param name="color">颜色</param>
        delegate void ChangeBtnColorEventhandle(string StatusTagName, Color color);

        /// <summary>
        /// 设置按键颜色的委托
        /// </summary>
        private static event ChangeBtnColorEventhandle ChangeBtnColorEvent;

        /// <summary>
        /// 设置按键Text的委托
        /// </summary>
        /// <param name="StatusTagName">标签名称</param>
        /// <param name="text">Text</param>
        delegate void ChangeBtnTextEventhandle(string StatusTagName, string text);

        /// <summary>
        /// 设置按键Text的委托
        /// </summary>
        private static event ChangeBtnTextEventhandle ChangeBtnTextEvent;

        private bool _HaveHandleCreated;

        /// <summary>
        /// 记录颜色
        /// </summary>
        private static Dictionary<string, Color> _colorsDic = new Dictionary<string, Color>();
        /// <summary>
        /// 记录text
        /// </summary>
        private static Dictionary<string, string> _textsDic = new Dictionary<string, string>();


        /// <summary>
        /// 构造函数
        /// </summary>
        public ButtonEx()
        {
            InitializeComponent();
            this.HandleCreated += ButtonEx_HandleCreated;
            this.HandleDestroyed += ButtonEx_HandleDestroyed;
            ChangeBtnColorEvent += ButtonEx_ChangeBtnColorEvent;
            ChangeBtnTextEvent += ButtonEx_ChangeBtnTextEvent;
        }

        private void ButtonEx_HandleCreated(object sender, EventArgs e)
        {
            _HaveHandleCreated = true;
            if (_colorsDic.TryGetValue(_StatusTagName, out Color color))
            {
                this.BackColor = color;
            }
            if (_textsDic.TryGetValue(_StatusTagName, out string text))
            {
                if (!string.IsNullOrEmpty(text))
                    this.Text = text;
            }
        }

        private void ButtonEx_HandleDestroyed(object sender, EventArgs e)
        {
            ChangeBtnColorEvent -= ButtonEx_ChangeBtnColorEvent;
            ChangeBtnTextEvent -= ButtonEx_ChangeBtnTextEvent;
        }
        private void ButtonEx_ChangeBtnColorEvent(string StatusTagName, Color color)
        {
            if (_StatusTagName == StatusTagName)
            {
                _colorsDic[_StatusTagName] = color;
            }
            if (_HaveHandleCreated)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (_StatusTagName == StatusTagName)
                    {
                        //_nowColor = color;
                        this.BackColor = color;
                    }
                }));
            }
        }

        private void ButtonEx_ChangeBtnTextEvent(string StatusTagName, string text)
        {
            if (_StatusTagName == StatusTagName)
            {
                _textsDic[_StatusTagName] = text;
            }
            if (_HaveHandleCreated)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (_StatusTagName == StatusTagName)
                    {
                        this.Text = text;
                    }
                }));
            }
        }
        #region 属性
        /// <summary>
        /// 标记名称
        /// </summary>
        string _StatusTagName;
        /// <summary>
        /// 标记名称
        /// </summary>
        [Category("RX.UI")]
        [Description("标记名称\r\n用标记名称设置按键颜色")]
        public string StatusTagName
        {
            get { return _StatusTagName; }
            set
            {
                if (value != "")
                {
                    _StatusTagName = value;
                }
            }
        }
        #endregion

        #region 静态调用方法

        /// <summary>
        /// 设置按钮颜色
        /// </summary>
        /// <param name="StatusTagName">按钮标签名称</param>
        /// <param name="color">颜色</param>
        public static void ChangeBtnColor(string StatusTagName, Color color)
        {
            if (ChangeBtnColorEvent != null)
            {
                ChangeBtnColorEvent(StatusTagName, color);
            }
        }

        /// <summary>
        /// 设置按钮颜色
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="properValue">原值</param>
        /// <param name="newValue">新值</param>
        /// <param name="color">按钮颜色</param>
        /// <param name="StatusTagName">对应变量名称</param>
        public static void ChangeBtnColor<T>(ref T properValue, T newValue, Color color, [CallerMemberName] string StatusTagName = null)
        {
            properValue = newValue;
            if (ChangeBtnColorEvent != null)
            {
                if (StatusTagName != null)
                {
                    ChangeBtnColorEvent(StatusTagName, color);
                }
            }
        }

        /// <summary>
        /// 设置按钮颜色
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="properValue">原值</param>
        /// <param name="newValue">新值</param>
        /// <param name="color">按钮颜色</param>
        /// <param name="text">按钮Text</param>
        /// <param name="StatusTagName">对应变量名称</param>
        public static void ChangeBtnColor<T>(ref T properValue, T newValue, Color color, string text, [CallerMemberName] string StatusTagName = null)
        {
            properValue = newValue;
            if (ChangeBtnColorEvent != null)
            {
                if (StatusTagName != null)
                {
                    ChangeBtnColorEvent(StatusTagName, color);
                }
            }
            if (ChangeBtnTextEvent != null)
            {
                if (StatusTagName != null)
                {
                    ChangeBtnTextEvent(StatusTagName, text);
                }
            }
        }

        /// <summary>
        /// 设置按钮颜色和显示文本
        /// </summary>
        /// <param name="properValue"></param>
        /// <param name="newValue"></param>
        /// <param name="color"></param>
        /// <param name="AllTexts"></param>
        /// <param name="StatusTagName"></param>
        public static void ChangeBtnColorAndText(ref int properValue, int newValue, Color color, string AllTexts, [CallerMemberName] string StatusTagName = null)
        {
            properValue = newValue;
            if (ChangeBtnColorEvent != null)
            {
                if (StatusTagName != null)
                {
                    ChangeBtnColorEvent(StatusTagName, color);
                }
            }
            if (ChangeBtnTextEvent != null)
            {
                if (StatusTagName != null)
                {
                    var arry = AllTexts.Split(',');
                    if (arry.Length > newValue)
                    {
                        ChangeBtnTextEvent(StatusTagName, arry[newValue]);
                    }
                }
            }
        }
        #endregion
    }
}
