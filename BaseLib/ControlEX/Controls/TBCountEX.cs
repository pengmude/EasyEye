using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartLib
{
    /// <summary>
    /// TBCount扩展工具类
    /// </summary>
    public partial class TBCountEX : TextBox
    {
        /// <summary>
        /// 设置控件内容
        /// </summary>
        /// <param name="TagName"></param>
        /// <param name="showMes"></param>
        delegate void ChangeTextEventhandle(string TagName, string showMes);

        /// <summary>
        /// 设置按键颜色的委托
        /// </summary>
        private static event ChangeTextEventhandle ChangeTextEvent;

        private string Mess = "";
        private bool _HaveHandleCreated;

        #region 属性
        /// <summary>
        /// 标记名称
        /// </summary>
        string _StatusTagName;
        /// <summary>
        /// 标记名称
        /// </summary>
        [Category("RX.UI")]
        [Description("标记名称\r\n用标记名称设置文字内容")]
        public string TagName
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

        /// <summary>
        /// 构造函数
        /// </summary>
        public TBCountEX()
        {
            InitializeComponent();
            this.HandleCreated += TBCountEX_HandleCreated;
            this.HandleDestroyed += TBCountEX_HandleDestroyed;
            ChangeTextEvent += TBCountEX_ChangeTextEvent;
        }

        private void TBCountEX_HandleCreated(object sender, EventArgs e)
        {
            _HaveHandleCreated = true;
            this.Text = Mess;
        }

        private void TBCountEX_HandleDestroyed(object sender, EventArgs e)
        {
            ChangeTextEvent -= TBCountEX_ChangeTextEvent;
        }
        private void TBCountEX_ChangeTextEvent(string StatusTagName, string showMess)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (_StatusTagName == StatusTagName)
                {
                    Mess = showMess;
                    if (_HaveHandleCreated)
                    {
                        this.Text = showMess;
                    }
                }
            }));
        }

        #region 静态调用方法
        /// <summary>
        /// 改变控件文本内容
        /// </summary>
        /// <param name="TagName"></param>
        /// <param name="showMess"></param>
        public static void ChangeText(string TagName, string showMess)
        {
            if (ChangeTextEvent != null)
            {
                ChangeTextEvent(TagName, showMess);
            }
        }
        #endregion

    }
}
