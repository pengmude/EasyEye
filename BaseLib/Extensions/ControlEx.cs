using System;
using System.Windows.Forms;

namespace SmartLib
{
    /// <summary>
    /// 控件扩展方法类
    /// </summary>
    public static class ControlEx
    {
        /// <summary>
        /// 跨线程调用UI,不阻塞调用方
        /// </summary>
        /// <typeparam name="TControl">控件类型</typeparam>
        /// <param name="cont">控件实例</param>
        /// <param name="action">执行动作</param>
        public static void BeginInvokeControlAction<TControl>(this TControl cont, MethodInvoker action)
            where TControl : System.Windows.Forms.Control
        {
            if (cont.InvokeRequired)
                cont.BeginInvoke(action);
            else
                action();
        }

        /// <summary>
        /// 跨线程调用UI,不阻塞调用方
        /// </summary>
        /// <typeparam name="TForm">Form类型</typeparam>
        /// <param name="form">Form窗体实例</param>
        /// <param name="action">动作</param>
        public static void BeginInvokeFormAction<TForm>(this TForm form, MethodInvoker action)
            where TForm : Form
        {
            if (form.InvokeRequired)
                form.BeginInvoke(action);
            else
                action();
        }
    }
}