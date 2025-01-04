using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartVEye
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 获取当前运行的进程
            Process current = Process.GetCurrentProcess();
            // 获取所有与当前进程名称相同的进程
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            // 如果发现有其他的实例在运行，那么就退出当前的应用程序实例
            if (processes.Length > 1)
            {
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
