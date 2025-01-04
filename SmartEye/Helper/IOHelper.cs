using SmartLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartVEye
{
    /// <summary>
    /// IO工具类
    /// </summary>
    unsafe public class IOHelper
    {
        #region IO配置

        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        private static extern int GetGpio(int index);
        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        private static extern int SetGpio(int index, int bv);

        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        private static extern int SioGetGpio(int index);
        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        private static extern int SioSetGpio(int index, int bv);

        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        private static extern int SioWatchDogSetting(int StartWdt, int WdtMode, int WdtTime);

        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        private static extern int PchIoGetGpio(int index);
        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        private static extern int PchIoSetGpio(int index, int bv);

        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        private static extern void ShutdownWinIo();
        [DllImport("OemGpioProgramDll.dll", SetLastError = true)]
        private static extern bool RemoveWinIoDriver();

        #endregion

        /// <summary>
        /// IO初始化 本质是设置IO为输出模式
        /// </summary>
        /// <returns></returns>
        public static Response IOInit()
        {
            return Response.Ok();
        }

        /// <summary>
        /// 打开IO
        /// </summary>
        /// <param name="ioIdx">IO序号</param>
        /// <returns></returns>
        public static Response SetBitOn(int ioIdx)
        {
            bool result = true;
            string msg = "";
            if (ioIdx < 1 || ioIdx > 4)
            {
                msg = "不存在IO:" + ioIdx;
                result = false;
            }
            PchIoSetGpio(ioIdx - 1, 1);
            if (!result) return Response.Fail(msg);
            else return Response.Ok();
        }

        /// <summary>
        /// 关闭IO
        /// </summary>
        /// <param name="ioIdx">IO序号</param>
        /// <returns></returns>
        public static Response SetBitOff(int ioIdx)
        {
            bool result = true;
            string msg = "";
            if (ioIdx < 1 || ioIdx > 4)
            {
                msg = "不存在IO:" + ioIdx;
                result = false;
            }
            PchIoSetGpio(ioIdx - 1, 0);
            if (!result) return Response.Fail(msg);
            else return Response.Ok();
        }

        public static Response IOUnInit()
        {
            ShutdownWinIo();
            RemoveWinIoDriver();
            return Response.Ok();
        }
    }
}
