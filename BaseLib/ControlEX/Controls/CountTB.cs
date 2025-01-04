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
    /// CountTB 计数TextBox工具类
    /// </summary>
    public partial class CountTB : TextBox
    {
        /// <summary>
        /// 设置按键颜色的委托
        /// </summary>
        /// <param name="StatusTagName">标签名称</param>
        /// <param name="showMes">显示信息</param>
        delegate void ChangeTextEventhandle(string StatusTagName, string showMes);

        /// <summary>
        /// 设置按键颜色的委托
        /// </summary>
        private static event ChangeTextEventhandle ChangeBtnColorEvent;

        private string Mess="";
        private bool _HaveHandleCreated;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public CountTB()
        {
            InitializeComponent();
            this.HandleCreated += ButtonEx_HandleCreated;
            this.HandleDestroyed += ButtonEx_HandleDestroyed;
            ChangeBtnColorEvent += ButtonEx_ChangeBtnColorEvent;
        }

        private void ButtonEx_HandleCreated(object sender, EventArgs e)
        {
            _HaveHandleCreated = true;
            this.Text = Mess;
        }

        private void ButtonEx_HandleDestroyed(object sender, EventArgs e)
        {
            ChangeBtnColorEvent -= ButtonEx_ChangeBtnColorEvent;
        }
        private void ButtonEx_ChangeBtnColorEvent(string StatusTagName, string showMes)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (_StatusTagName == StatusTagName)
                {
                    Mess = showMes;
                    if (_HaveHandleCreated)
                    {
                        this.Text = showMes;
                    }
                }
            }));
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
        /// 改变控件内容
        /// </summary>
        /// <param name="StatusTagName"></param>
        /// <param name="showMes"></param>
        public static void ChangeText(string StatusTagName, string showMes)
        {
            if (ChangeBtnColorEvent != null)
            {
                ChangeBtnColorEvent(StatusTagName, showMes);
            }
        }
        #endregion
    }
}
