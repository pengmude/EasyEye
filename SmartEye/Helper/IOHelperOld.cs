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
    /// IO工具类 最早的自封装函数
    /// </summary>
    public class IOHelperOld
    {
        #region GPIO配置
        ///
        /// *******************************************************
        /// IO_PROTOCOL_WIDTH
        /// *******************************************************
        ///
        public enum IO_PROTOCOL_WIDTH
        {
            IoWidthUint8 = 0,
            IoWidthUint16 = 1,
            IoWidthUint32 = 2,
            IoWidthUint64 = 3,
            IoWidthMaximum
        };
        ///
        /// *******************************************************
        /// MEM_PROTOCOL_WIDTH
        /// *******************************************************
        ///
        public enum MEM_PROTOCOL_WIDTH
        {
            MemWidthUint8 = 0,
            MemWidthUint16 = 1,
            MemWidthUint32 = 2,
            MemWidthUint64 = 3,
            MemWidthMaximum
        }

        public const int GPIO_BASE_ADDRESS = 0x1C00;
        //
        // GPIO Init register offsets from GPIOBASE
        //
        public const int R_PCH_GPIO_USE_SEL = 0x00;
        public const int R_PCH_GPIO_IO_SEL = 0x04;
        public const int R_PCH_GPIO_LVL = 0x0C;
        public const int R_PCH_GPIO_IOAPIC_SEL = 0x10;
        public const int V_PCH_GPIO_IOAPIC_SEL = 0xFFFF;
        public const int R_PCH_GPIO_BLINK = 0x18;
        public const int R_PCH_GPIO_SER_BLINK = 0x1C;
        public const int R_PCH_GPIO_SB_CMDSTS = 0x20;
        public const int B_PCH_GPIO_SB_CMDSTS_DLS_MASK = 0x00C00000;
        public const int B_PCH_GPIO_SB_CMDSTS_DRS_MASK = 0x003F0000;
        public const int B_PCH_GPIO_SB_CMDSTS_BUSY = 0x100;
        public const int B_PCH_GPIO_SB_CMDSTS_GO = 0x01;
        public const int R_PCH_GPIO_SB_DATA = 0x24;
        public const int R_PCH_GPIO_NMI_EN = 0x28;
        public const int B_PCH_GPIO_NMI_EN = 0xFFFF;
        public const int R_PCH_GPIO_NMI_STS = 0x2A;
        public const int B_PCH_GPIO_NMI_STS = 0xFFFF;
        public const int R_PCH_GPIO_GPI_INV = 0x2C;
        public const int R_PCH_GPIO_USE_SEL2 = 0x30;
        public const int R_PCH_GPIO_IO_SEL2 = 0x34;
        public const int R_PCH_GPIO_LVL2 = 0x38;
        public const int R_PCH_GPIO_USE_SEL3 = 0x40;
        public const int R_PCH_GPIO_IO_SEL3 = 0x44;
        public const int _PCH_GPIO_LVL3 = 0x48;
        public const int R_PCH_GPIO_CONFIG_REG = 0x100;
        public const bool GPIO_FUNC = true;
        public const bool NVTIVE_FUNC = false;
        public const bool OUTPUT = true;
        public const bool INPUT = false;
        public const bool HIGH = true;
        public const bool LOW = false;
        public const bool ENABLE = true;
        public const bool DISABLE = false;

        // 必须要有 CallingConvention.Cdecl，否则程序会报错
        [DllImport("WinIoDllx64.dll", EntryPoint = "SetPhyMemValue", SetLastError = true)]
        private static extern uint SetPhyMemValue(ulong PhyMemAddr, ulong PortValue, MEM_PROTOCOL_WIDTH MemWidth);
        // 必须要有 CallingConvention.Cdecl，否则程序会报错
        [DllImport("WinIoDllx64.dll", EntryPoint = "GetPhyMemValue", SetLastError = true)]
        unsafe private static extern uint GetPhyMemValue(ulong PhyMemAddr, ulong* PortValue, MEM_PROTOCOL_WIDTH MemWidth);
        // 必须要有 CallingConvention.Cdecl，否则程序会报错
        [DllImport("WinIoDllx64.dll", EntryPoint = "SetIoPortValue", SetLastError = true)]
        private static extern uint SetIoPortValue(ushort PortAddr, uint PortValue, IO_PROTOCOL_WIDTH IoWidth);
        // 必须要有 CallingConvention.Cdecl，否则程序会报错
        [DllImport("WinIoDllx64.dll", EntryPoint = "GetIoPortValue", SetLastError = true)]
        unsafe private static extern uint GetIoPortValue(ushort PortAddr, uint* PortValue, IO_PROTOCOL_WIDTH IoWidth);

        #endregion

        /// <summary>
        /// IO初始化 本质是设置IO为输出模式
        /// </summary>
        /// <returns></returns>
        unsafe public static Response IOInit()
        {
            uint RetStatus;
            uint GpioData;
            //设置为GPIO模式
            SetGpioFunc(33, GPIO_FUNC);
            SetGpioFunc(48, GPIO_FUNC);
            SetGpioFunc(49, GPIO_FUNC);
            SetGpioFunc(50, GPIO_FUNC);
            //设置为输出模式
            SetGpioDir(33, OUTPUT);
            SetGpioSenseEn(33, ENABLE);
            SetGpioDir(48, OUTPUT);
            SetGpioSenseEn(48, ENABLE);
            SetGpioDir(49, OUTPUT);
            SetGpioSenseEn(49, ENABLE);
            SetGpioDir(50, OUTPUT);
            SetGpioSenseEn(50, ENABLE);
            //默认输出电平配置
            SetGpioOut(33, LOW);
            SetGpioOut(48, LOW);
            SetGpioOut(49, LOW);
            SetGpioOut(50, LOW);

            //初始化GPIO 程序设置地址为输出模式
            //33
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
                return Response.Fail($"IO初始化异常!0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16));
            }
            GpioData &= 0xFFFFFFFD;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            //48
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
                return Response.Fail($"IO初始化异常!0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16));
            }
            GpioData &= 0xFFFEFFFF;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            //49
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
                return Response.Fail($"IO初始化异常!0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16));
            }
            GpioData &= 0xFFFDFFFF;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            //50
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16) + " failed.");
                return Response.Fail($"IO初始化异常!0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16));
            }
            GpioData &= 0xFFFBFFFF;
            SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);

            #region MyRegion
            //ushort gpioIdx = 33;//主板上的io序号
            //for (int ioIdx = 0; ioIdx < CommonData.IOOutAddressList.Count; ioIdx++)
            //{
            //    gpioIdx = CommonData.IOOutAddressList[ioIdx];
            //    SetGpioDir(gpioIdx, OUTPUT);
            //    SetGpioSenseEn(gpioIdx, ENABLE);
            //    //默认输出电平配置
            //    SetGpioOut(gpioIdx, LOW);
            //    // Program GPIO33/GPIO48/GPIO49/GPIO50 as GPIO mode
            //    SetGpioFunc(gpioIdx, GPIO_FUNC);
            //    // Program GPIO33 output high, 1: High, 0: Low
            //    GpioData = 0x00000000;
            //    RetStatus = GetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            //    if (RetStatus != 0)
            //    {
            //        return Response.Fail($"IO[{gpioIdx}]初始化异常!0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2), 16));
            //    }
            //    switch (ioIdx)
            //    {
            //        case 0: GpioData |= 0x00070002; break;
            //        case 1: GpioData &= 0xFFFEFFFF; break;
            //        case 2: GpioData &= 0xFFFDFFFF; break;
            //        case 3: GpioData &= 0xFFFBFFFF; break;
            //    }
            //    SetIoPortValue(GPIO_BASE_ADDRESS + R_PCH_GPIO_IO_SEL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            //}
            #endregion

            return Response.Ok();
        }
        /// <summary>
        /// 读IO
        /// </summary>
        /// <param name="ioIdx">IO序号</param>
        /// <returns></returns>
        public static Response<int> GetBit(int ioIdx)
        {
            return Response<int>.Ok(1);
        }
        /// <summary>
        /// 打开IO
        /// </summary>
        /// <param name="ioIdx">IO序号</param>
        /// <returns></returns>
        unsafe public static Response SetBitOn(int ioIdx)
        {
            if (ioIdx > CommonData.IOOutAddressList.Count)
            {
                return Response.Fail($"IO[{ioIdx}]不存在!请检查配置");
            }
            uint RetStatus;
            uint GpioData;
            ushort gpioIdx = 33;//主板上的io序号
            gpioIdx = CommonData.IOOutAddressList[ioIdx - 1];
            SetGpioOut(gpioIdx, HIGH);
            // Program GPIO33 output high, 1: High, 0: Low
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue((ushort)GPIO_BASE_ADDRESS + (ushort)R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                IOInit();
                return Response.Fail($"IO[{ioIdx}]打开输出异常!0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + ";RetStatus: " + RetStatus);
            }
            switch (ioIdx)
            {
                case 1: GpioData |= 0x00000002; break;
                case 2: GpioData |= 0x00010000; break;
                case 3: GpioData |= 0x00020000; break;
                case 4: GpioData |= 0x00040000; break;
            }
            SetIoPortValue((ushort)GPIO_BASE_ADDRESS + (ushort)R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            return Response.Ok();
        }
        /// <summary>
        /// 关闭IO
        /// </summary>
        /// <param name="ioIdx">IO序号</param>
        /// <returns></returns>
        unsafe public static Response SetBitOff(int ioIdx)
        {
            if (ioIdx > CommonData.IOOutAddressList.Count)
            {
                return Response.Fail($"IO[{ioIdx}]不存在!请检查配置");
            }
            uint RetStatus;
            uint GpioData;
            ushort gpioIdx = 33;//主板上的io序号
            gpioIdx = CommonData.IOOutAddressList[ioIdx - 1];
            SetGpioOut(gpioIdx, LOW);
            // Program GPIO33 output low, 1: High, 0: Low
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue((ushort)GPIO_BASE_ADDRESS + (ushort)R_PCH_GPIO_LVL2, &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                IOInit();
                return Response.Fail($"IO[{ioIdx}]关闭输出异常!0x" + Convert.ToString((GPIO_BASE_ADDRESS + R_PCH_GPIO_LVL2), 16) + ";RetStatus: " + RetStatus);
            }
            switch (ioIdx)
            {
                case 1: GpioData &= 0xFFFFFFFD; break;
                case 2: GpioData &= 0xFFFEFFFF; break;
                case 3: GpioData &= 0xFFFDFFFF; break;
                case 4: GpioData &= 0xFFFBFFFF; break;
            }
            SetIoPortValue((ushort)GPIO_BASE_ADDRESS + (ushort)R_PCH_GPIO_LVL2, GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            return Response.Ok();
        }

        //
        // GpioDataDir => 1: Input, 0: Output
        //
        unsafe private static void SetGpioDir(ushort GpioPinNum, bool Dir)
        {
            uint RetStatus;
            uint GpioData;

            // Program GPIO Dir
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8), &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString(((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8)), 16) + " failed.");
                return;
            }

            //
            // BIT2 is GPIO DIR control, 1: input, 0: output
            //
            if (Dir == OUTPUT)
            {
                GpioData &= 0xFFFFFFFB;
            }
            else
            {
                GpioData |= 0x00000004;
            }
            SetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8), GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
        }

        //
        // GpioSenseEn => 1: Input sensing disable, 0: Input sensing enable.
        //
        unsafe private static void SetGpioSenseEn(ushort GpioPinNum, bool SenseEn)
        {
            uint RetStatus;
            uint GpioData;

            // Program GPIO Dir
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + 0x04 + GpioPinNum * 8), &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString(((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + 0x04 + GpioPinNum * 8)), 16) + " failed.");
                return;
            }

            //
            // BIT2 is GPIO sense input state control, 1: Input sensing disable, 0: Input sensing enable.
            //
            if (SenseEn == ENABLE)
            {
                GpioData &= 0xFFFFFFFB;
            }
            else
            {
                GpioData |= 0x00000004;
            }
            SetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + 0x04 + GpioPinNum * 8), GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
        }

        //
        // GpioDataFunc => 1: Gpio Function, 0: Native function
        //
        unsafe private static void SetGpioFunc(ushort GpioPinNum, bool Func)
        {
            uint RetStatus;
            uint GpioData;

            // Program GPIO Dir
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8), &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString(((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8)), 16) + " failed.");
                return;
            }

            //
            // BIT0 is GPIO function control, 1: GPIO function, 0: Native function
            //
            if (Func == GPIO_FUNC)
            {
                GpioData |= 0x00000001;
            }
            else
            {
                GpioData &= 0xFFFFFFFE;
            }
            SetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8), GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
        }

        //
        // GpioDataDir => 1: Output High, 0: Output Low
        //
        unsafe private static void SetGpioOut(ushort GpioPinNum, bool Out)
        {
            uint RetStatus;
            uint GpioData;

            // Program GPIO Dir
            GpioData = 0x00000000;
            RetStatus = GetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8), &GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
            if (RetStatus != 0)
            {
                Console.WriteLine("Get register 0x" + Convert.ToString(((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8)), 16) + " failed.");
                return;
            }

            //
            // BIT31 is GPIO OUTPUT state control, 1: HIGH, 0: LOW
            //
            if (Out == HIGH)
            {
                GpioData |= 0x80000000;
            }
            else
            {
                GpioData &= 0x7FFFFFFF;
            }
            SetIoPortValue((ushort)(GPIO_BASE_ADDRESS + R_PCH_GPIO_CONFIG_REG + GpioPinNum * 8), GpioData, IO_PROTOCOL_WIDTH.IoWidthUint32);
        }


    }
}
