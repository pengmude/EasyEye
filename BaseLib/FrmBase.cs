using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SmartLib
{
    /// <summary>
    ///     可最大化的窗体
    /// </summary>
    public partial class FrmBase : Form
    {
        private const int PADDING_MINIMUM = 1;

        private const int BORDER_WIDTH = 7;

        /// <summary>
        ///     窗体标题
        /// </summary>
        protected static string FormTitle = "";

        /// <summary>
        ///     窗体文本内容
        /// </summary>
        protected static string FormText = "";

        private readonly Cursor[] _resizeCursors =
            { Cursors.SizeNESW, Cursors.SizeWE, Cursors.SizeNWSE, Cursors.SizeWE, Cursors.SizeNS };

        private bool _isShowMaxBtn;

        private ResizeDirection _resizeDir;

        /// <summary>
        ///     构造函数
        /// </summary>
        public FrmBase()
        {
            InitializeComponent();
            IsShowMaxBtn = false;
            IsShowBottomPanel = false;
            DoubleBuffered = true;
            Padding = new Padding(PADDING_MINIMUM, PADDING_MINIMUM, PADDING_MINIMUM,
                PADDING_MINIMUM); //Keep space for resize by mouse
        }

        /// <summary>
        /// 设置最大化按钮可见性
        /// </summary>
        [Category("RX.UI")]
        [Browsable(true)]
        [DefaultValue(false)]
        [Description("设置最大化按钮可见性")]
        public bool IsShowMaxBtn
        {
            get => _isShowMaxBtn;
            set
            {
                _isShowMaxBtn = value;
                RefreshMaxBtn();
            }
        }

        private bool _isShowBottomPanel;

        /// <summary>
        /// 设置底部面板可见性
        /// </summary>
        [Category("RX.UI")]
        [Browsable(true)]
        [DefaultValue(false)]
        [Description("设置底部面板可见性")]
        public bool IsShowBottomPanel
        {
            get => _isShowBottomPanel;
            set
            {
                _isShowBottomPanel = value;
                panelBottom.Visible = _isShowBottomPanel;
            }
        }

        /// <summary>
        /// 是否可变大小
        /// </summary>
        [Category("Layout")] public bool Sizable { get; set; } = true;

        private bool Maximized
        {
            get => WindowState == FormWindowState.Maximized;
            set
            {
                if (!MaximizeBox || !ControlBox) return;

                if (value)
                    WindowState = FormWindowState.Maximized;
                else
                    WindowState = FormWindowState.Normal;
            }
        }

        private void RefreshMaxBtn()
        {
            if (_isShowMaxBtn)
                ToolsPanel.Controls.Add(btn_max, 2, 0);
            //ToolsPanel.ColumnStyles[2]
            //    .Width = 25F;
            else
                ToolsPanel.Controls.Remove(btn_max);
            //ToolsPanel.ColumnStyles[2]
            //    .Width = 0;
        }


        private void FrmBaseSize_Load(object sender, EventArgs e)
        {
            //tb_WinTitle.Text = FormTitle;
            Text = FormText;
        }
        /// <summary>
        /// OnPaint事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            //Draw border
            using (var borderPen = new Pen(Color.Aqua, 3.8f))
            {
                g.DrawLine(borderPen, new Point(btn_title.Left - 1, 0), new Point(btn_title.Right + 1, 0));

                g.DrawLine(borderPen, new Point(0, btn_title.Top), new Point(0, ClientSize.Height - 2));

                g.DrawLine(borderPen, new Point(ClientSize.Width, btn_title.Top),
                    new Point(ClientSize.Width - 1, ClientSize.Height - 2));

                g.DrawLine(borderPen, new Point(0, ClientSize.Height - 1),
                    new Point(ClientSize.Width - 1, ClientSize.Height - 1));
            }
        }

        private void ResizeForm(ResizeDirection direction)
        {
            if (DesignMode)
                return;
            var dir = -1;
            switch (direction)
            {
                case ResizeDirection.BottomLeft:
                    dir = (int)HT.BottomLeft;
                    Cursor = Cursors.SizeNESW;
                    break;

                case ResizeDirection.Left:
                    dir = (int)HT.Left;
                    Cursor = Cursors.SizeWE;
                    break;

                case ResizeDirection.Right:
                    dir = (int)HT.Right;
                    break;

                case ResizeDirection.BottomRight:
                    dir = (int)HT.BottomRight;
                    break;

                case ResizeDirection.Bottom:
                    dir = (int)HT.Bottom;
                    break;

                case ResizeDirection.Top:
                    dir = (int)HT.Top;
                    break;

                case ResizeDirection.TopLeft:
                    dir = (int)HT.TopLeft;
                    break;

                case ResizeDirection.TopRight:
                    dir = (int)HT.TopRight;
                    break;
            }

            ReleaseCapture();
            if (dir != -1) SendMessage(Handle, (int)WM.NonClientLeftButtonDown, dir, 0);
        }

        #region Enums

        /// <summary>
        ///     Various directions the form can be resized in
        /// </summary>
        private enum ResizeDirection
        {
            BottomLeft,
            Left,
            Right,
            BottomRight,
            Bottom,
            Top,
            TopLeft,
            TopRight,
            None
        }


        /// <summary>
        ///     Window Messages
        ///     <see href="https://docs.microsoft.com/en-us/windows/win32/winmsg/about-messages-and-message-queues" />
        /// </summary>
        private enum WM
        {
            /// <summary>
            ///     WM_NCCALCSIZE
            /// </summary>
            NonClientCalcSize = 0x0083,

            /// <summary>
            ///     WM_NCACTIVATE
            /// </summary>
            NonClientActivate = 0x0086,

            /// <summary>
            ///     WM_NCLBUTTONDOWN
            /// </summary>
            NonClientLeftButtonDown = 0x00A1,

            /// <summary>
            ///     WM_SYSCOMMAND
            /// </summary>
            SystemCommand = 0x0112,

            /// <summary>
            ///     WM_MOUSEMOVE
            /// </summary>
            MouseMove = 0x0200,

            /// <summary>
            ///     WM_LBUTTONDOWN
            /// </summary>
            LeftButtonDown = 0x0201,

            /// <summary>
            ///     WM_LBUTTONUP
            /// </summary>
            LeftButtonUp = 0x0202,

            /// <summary>
            ///     WM_LBUTTONDBLCLK
            /// </summary>
            LeftButtonDoubleClick = 0x0203,

            /// <summary>
            ///     WM_RBUTTONDOWN
            /// </summary>
            RightButtonDown = 0x0204
        }

        /// <summary>
        ///     Hit Test Results
        ///     <see href="https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest" />
        /// </summary>
        private enum HT
        {
            /// <summary>
            ///     HTNOWHERE - Nothing under cursor
            /// </summary>
            None = 0,

            /// <summary>
            ///     HTCAPTION - Titlebar
            /// </summary>
            Caption = 2,

            /// <summary>
            ///     HTLEFT - Left border
            /// </summary>
            Left = 10,

            /// <summary>
            ///     HTRIGHT - Right border
            /// </summary>
            Right = 11,

            /// <summary>
            ///     HTTOP - Top border
            /// </summary>
            Top = 12,

            /// <summary>
            ///     HTTOPLEFT - Top left corner
            /// </summary>
            TopLeft = 13,

            /// <summary>
            ///     HTTOPRIGHT - Top right corner
            /// </summary>
            TopRight = 14,

            /// <summary>
            ///     HTBOTTOM - Bottom border
            /// </summary>
            Bottom = 15,

            /// <summary>
            ///     HTBOTTOMLEFT - Bottom left corner
            /// </summary>
            BottomLeft = 16,

            /// <summary>
            ///     HTBOTTOMRIGHT - Bottom right corner
            /// </summary>
            BottomRight = 17
        }

        /// <summary>
        ///     Window Styles
        ///     <see href="https://docs.microsoft.com/en-us/windows/win32/winmsg/window-styles" />
        /// </summary>
        private enum WS
        {
            /// <summary>
            ///     WS_MINIMIZEBOX - Allow minimizing from taskbar
            /// </summary>
            MinimizeBox = 0x20000,

            /// <summary>
            ///     WS_SIZEFRAME - Required for Aero Snapping
            /// </summary>
            SizeFrame = 0x40000,

            /// <summary>
            ///     WS_SYSMENU - Trigger the creation of the system menu
            /// </summary>
            SysMenu = 0x80000
        }

        /// <summary>
        ///     Track Popup Menu Flags
        ///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-trackpopupmenu" />
        /// </summary>
        private enum TPM
        {
            /// <summary>
            ///     TPM_LEFTALIGN
            /// </summary>
            LeftAlign = 0x0000,

            /// <summary>
            ///     TPM_RETURNCMD
            /// </summary>
            ReturnCommand = 0x0100
        }

        #endregion

        #region WinForms Methods

        /// <summary>
        /// CreateParams
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var par = base.CreateParams;
                par.Style |= (int)WS.MinimizeBox | (int)WS.SysMenu;
                return par;
            }
        }

        /// <summary>
        /// Winform OnCreateControl事件
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // Sets the Window Style for having a Size Frame after the form is created
            // This prevents unexpected sizing while still allowing for Aero Snapping
            var flags = GetWindowLongPtr(Handle, -16)
                .ToInt64();
            SetWindowLongPtr(Handle, -16, (IntPtr)(flags | (int)WS.SizeFrame));
        }
        /// <summary>
        /// Winform WndProc
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            var message = (WM)m.Msg;
            // Prevent the base class from receiving the message
            if (message == WM.NonClientCalcSize) return;

            // https://docs.microsoft.com/en-us/windows/win32/winmsg/wm-ncactivate?redirectedfrom=MSDN#parameters
            // "If this parameter is set to -1, DefWindowProc does not repaint the nonclient area to reflect the state change."
            if (message == WM.NonClientActivate)
            {
                m.Result = new IntPtr(-1);
                return;
            }

            base.WndProc(ref m);
            if (DesignMode || IsDisposed)
                return;

            var cursorPos = PointToClient(Cursor.Position);
            //var isOverCaption = (_statusBarBounds.Contains(cursorPos) || _actionBarBounds.Contains(cursorPos)) &&
            //                    !(_minButtonBounds.Contains(cursorPos) || _maxButtonBounds.Contains(cursorPos) ||
            //                      _xButtonBounds.Contains(cursorPos));


            //// Double click to maximize
            //if (message == WM.LeftButtonDoubleClick && isOverCaption)
            //{
            //    Maximized = !Maximized;
            //}
            //// Treat the Caption as if it was Non-Client
            //else if (message == WM.LeftButtonDown && isOverCaption)
            //{
            //    ReleaseCapture();
            //    SendMessage(Handle, (int)WM.NonClientLeftButtonDown, (int)HT.Caption, 0);
            //}
        }
        /// <summary>
        /// 窗体移动
        /// </summary>
        /// <param name="e">事件</param>
        protected override void OnMove(EventArgs e)
        {
            // Empty Point ensures the screen maximizes to the top left of the current screen
            MaximizedBounds = new Rectangle(Point.Empty, Screen.GetWorkingArea(Location).Size);
            base.OnMove(e);
        }
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="e">鼠标事件</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (DesignMode)
                return;
            //  UpdateButtons(e.Button, e.Location);

            if (e.Button == MouseButtons.Left && !Maximized && _resizeCursors.Contains(Cursor))
                ResizeForm(_resizeDir);
            base.OnMouseDown(e);
        }
        /// <summary>
        /// 鼠标进入
        /// </summary>
        /// <param name="e">事件</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Default;
        }
        /// <summary>
        /// 鼠标离开
        /// </summary>
        /// <param name="e">事件</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (DesignMode)
                return;
            //_buttonState = ButtonState.None;
            _resizeDir = ResizeDirection.None;
            //Only reset the cursor when needed
            if (_resizeCursors.Contains(Cursor)) Cursor = Cursors.Default;

            Invalidate();
        }
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="e">鼠标事件</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DesignMode) return;

            var coords = e.Location;

            //    UpdateButtons(e.Button, coords);

            if (!Sizable) return;

            //True if the mouse is hovering over a child control
            var isChildUnderMouse = GetChildAtPoint(coords) != null;

            if (!isChildUnderMouse && !Maximized && coords.Y < BORDER_WIDTH && coords.X > BORDER_WIDTH &&
                coords.X < ClientSize.Width - BORDER_WIDTH)
            {
                _resizeDir = ResizeDirection.Top;
                Cursor = Cursors.SizeNS;
            }
            else if (!isChildUnderMouse && !Maximized && coords.X <= BORDER_WIDTH && coords.Y < BORDER_WIDTH)
            {
                _resizeDir = ResizeDirection.TopLeft;
                Cursor = Cursors.SizeNWSE;
            }
            else if (!isChildUnderMouse && !Maximized && coords.X >= ClientSize.Width - BORDER_WIDTH &&
                     coords.Y < BORDER_WIDTH)
            {
                _resizeDir = ResizeDirection.TopRight;
                Cursor = Cursors.SizeNESW;
            }
            else if (!isChildUnderMouse && !Maximized && coords.X <= BORDER_WIDTH &&
                     coords.Y >= ClientSize.Height - BORDER_WIDTH)
            {
                _resizeDir = ResizeDirection.BottomLeft;
                Cursor = Cursors.SizeNESW;
            }
            else if (!isChildUnderMouse && !Maximized && coords.X <= BORDER_WIDTH)
            {
                _resizeDir = ResizeDirection.Left;
                Cursor = Cursors.SizeWE;
            }
            else if (!isChildUnderMouse && !Maximized && coords.X >= ClientSize.Width - BORDER_WIDTH &&
                     coords.Y >= ClientSize.Height - BORDER_WIDTH)
            {
                _resizeDir = ResizeDirection.BottomRight;
                Cursor = Cursors.SizeNWSE;
            }
            else if (!isChildUnderMouse && !Maximized && coords.X >= ClientSize.Width - BORDER_WIDTH)
            {
                _resizeDir = ResizeDirection.Right;
                Cursor = Cursors.SizeWE;
            }
            else if (!isChildUnderMouse && !Maximized && coords.Y >= ClientSize.Height - BORDER_WIDTH)
            {
                _resizeDir = ResizeDirection.Bottom;
                Cursor = Cursors.SizeNS;
            }
            else
            {
                _resizeDir = ResizeDirection.None;

                //Only reset the cursor when needed, this prevents it from flickering when a child control changes the cursor to its own needs
                if (_resizeCursors.Contains(Cursor))
                    Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// 鼠标抬起
        /// </summary>
        /// <param name="e">鼠标事件</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (DesignMode)
                return;


            base.OnMouseUp(e);
            ReleaseCapture();
        }

        #endregion


        #region 窗体移动

        private Point CPoint;

        /// <summary>
        /// 当前线程从窗体中释放鼠标
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")] //Namespace System.Runtime.InteropServices;
        public static extern bool ReleaseCapture(); //release the mouse capture from a window in the current thread

        //Send the specified message to a window,
        //The SendMessage fuction  calls the window procedure for the specified window and dose not return until  the window procedure has processed the message

        /// <summary>
        /// 向窗体发送指定信息
        /// SendMessage函数调用指定窗口的窗口过程，直到窗口过程处理完消息才返回
        /// </summary>
        /// <param name="hwnd">窗体句柄</param>
        /// <param name="wMsg">wMsg</param>
        /// <param name="wParam">wParam</param>
        /// <param name="lParam">lParam</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// WM_SYSCOMMAND
        /// </summary>
        public const int WM_SYSCOMMAND = 0x0112;
        /// <summary>
        /// SC_MAXIMIZE
        /// </summary>
        public const int SC_MAXIMIZE = 0xF030;
        /// <summary>
        /// SC_RESTORE
        /// </summary>
        public const int SC_RESTORE = 0xF120;

        /// <summary>
        /// 窗体标题鼠标按下
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">鼠标事件</param>
        protected void btn_title_MouseDown(object sender, MouseEventArgs e)
        {
            CPoint.X = -e.X;
            CPoint.Y = -e.Y;

            //双击
            if (e.Clicks == 2)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    // Restore the form
                    ReleaseCapture();
                    SendMessage(Handle, WM_SYSCOMMAND, SC_RESTORE, 0);
                }
                else if (WindowState == FormWindowState.Normal)
                {
                    //Maximize the form
                    ReleaseCapture();
                    SendMessage(Handle, WM_SYSCOMMAND, SC_MAXIMIZE, 0);
                }
            }
        }
        /// <summary>
        /// 窗体标题鼠标移动
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">鼠标事件</param>
        protected void btn_title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var myPosittion = MousePosition; //获取当前鼠标的屏幕坐标
                myPosittion.Offset(CPoint.X, CPoint.Y); //重载当前鼠标的位置
                DesktopLocation = myPosittion; //设置当前窗体在屏幕上的位置
            }
        }

        private void btn_min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool m_formMaxsize; //当前是否处于最大化模式

        private void btn_max_Click(object sender, EventArgs e)
        {
            if (m_formMaxsize)
            {
                WindowState = FormWindowState.Normal;
                StartPosition = FormStartPosition.CenterScreen;
                m_formMaxsize = false;
            }
            else
            {
                MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
                WindowState = FormWindowState.Maximized;
                m_formMaxsize = true;
            }
        }

        #endregion

        #region Low Level Windows Methods

        /// <summary>
        ///     Provides a single method to call either the 32-bit or 64-bit method based on the size of an <see cref="IntPtr" />
        ///     for getting the
        ///     Window Style flags.<br />
        ///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlongptra">GetWindowLongPtr</see>
        /// </summary>
        private static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 8)
                return GetWindowLongPtr64(hWnd, nIndex);
            return GetWindowLong(hWnd, nIndex);
        }

        /// <summary>
        ///     Provides a single method to call either the 32-bit or 64-bit method based on the size of an <see cref="IntPtr" />
        ///     for setting the
        ///     Window Style flags.<br />
        ///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlongptra">SetWindowLongPtr</see>
        /// </summary>
        private static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            return SetWindowLong(hWnd, nIndex, dwNewLong.ToInt32());
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);


        [DllImport("user32.dll")]
        private static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        #endregion
    }
}