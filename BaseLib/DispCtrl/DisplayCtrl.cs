using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HalconDotNet;
using System.Collections;
using System.Text.RegularExpressions;
using BaseData;
using System.Runtime.InteropServices;

namespace SmartLib
{
    /// <summary>
    /// 窗体控件类
    /// </summary>
    public partial class DisplayCtrl : UserControl
    {
        #region 模拟鼠标点击事件

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);
        //移动鼠标 
        const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        //模拟鼠标滚轮滚动操作，必须配合dwData参数
        const int MOUSEEVENTF_WHEEL = 0x0800;


        public static void TestMoveMouse()
        {
            //mouse_event(MOUSEEVENTF_MOVE, 50, 50, 0, 0);//相对当前鼠标位置x轴和y轴分别移动50像素
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -20, 0);//鼠标滚动，使界面向下滚动20的高度
        }
        #endregion

        private HTuple winHandle;
        private bool isInitWindow = false;

        private HObject curImage;
        private HTuple image_x, image_y, image_x1, image_y1;
        private HTuple image_width, image_height;
        private double move_x, move_y;
        private bool imageCanMove = false;
        private bool OuterRefersh = false;

        private List<ROI> ROIList = new List<ROI>();
        /// <summary>
        /// 当前ROI索引号
        /// </summary>
        public int curROIIndex = -1;
        private bool ROICanMove = false;

        private bool needAutoFocus = false;
        private bool isShowCrossLine = false;

        private bool needClearWindow = false;

        HObject xLine, yLine;

        /// <summary>
        /// string 0:颜色 2：线宽度 3：fill or margin
        /// </summary>
        private List<string> objListParam = new List<string>();
        private List<HObject> objList = new List<HObject>();


        /// <summary>
        /// string 0：row  1：column  2：文本颜色  3:字体大小  4:是否需要背景  5:背景颜色  6:是否粗体  7：是否斜体
        /// </summary>
        private List<string> messageListParam = new List<string>();
        private List<string> messageList = new List<string>();


        object slock = new object();


        //ROI创建相关
        private bool ROINeedCreate = false;
        private int ROIType = -1;
        private bool IsMouseDown = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DisplayCtrl()
        {
            InitializeComponent();
        }

        private void DispControl_Load(object sender, EventArgs e)
        {
            ROIList = new List<ROI>();
            curROIIndex = -1;

            image_x = 0;
            image_y = 0;
            image_x1 = viewPort.ImagePart.Height;
            image_y1 = viewPort.ImagePart.Width;
            image_height = viewPort.ImagePart.Height;
            image_width = viewPort.ImagePart.Width;

            this.Disposed += DisplayCtrl_Disposed;
        }

        private void DisplayCtrl_Disposed(object sender, EventArgs e)
        {
            if (curImage != null)
                curImage.Dispose();
            if (xLine != null)
                xLine.Dispose();
            if (yLine != null)
                yLine.Dispose();
            objListParam.Clear();
            for (int i = 0; i < objList.Count; i++)
            {
                objList[i].Dispose();
            }
            objList.Clear();
            messageListParam.Clear();
            messageList.Clear();

            ROIList.Clear();
        }


        #region  系统委托
        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        public HMouseEventHandler WinMouseMoveEvent;
        void Ctrl_WinMouseMove(object sender, HMouseEventArgs e)
        {
            try
            {
                if (WinMouseMoveEvent != null)
                {
                    WinMouseMoveEvent(sender, e);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("自定义窗体鼠标移动委托异常，请联系开发商！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 鼠标抬起事件
        /// </summary>
        public HMouseEventHandler WinMouseUpEvent;
        void Ctrl_WinMouseUp(object sender, HMouseEventArgs e)
        {
            try
            {
                if (WinMouseUpEvent != null)
                {
                    WinMouseUpEvent(sender, e);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("自定义窗体鼠标抬起委托异常，请联系开发商！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        public HMouseEventHandler WinMouseDownEvent;
        void Ctrl_WinMouseDown(object sender, HMouseEventArgs e)
        {
            try
            {
                if (WinMouseDownEvent != null)
                {
                    WinMouseDownEvent(sender, e);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("自定义窗体鼠标按下委托异常，请联系开发商！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 鼠标滚轮事件
        /// </summary>
        public HMouseEventHandler WinMouseWheelEvent;
        void Ctrl_WinMouseWheel(object sender, HMouseEventArgs e)
        {
            try
            {
                if (WinMouseWheelEvent != null)
                {
                    WinMouseWheelEvent(sender, e);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("自定义窗体鼠标滚轮滑动委托异常，请联系开发商！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 自适应图像事件
        /// </summary>
        public EventHandler AdaptImageEvent;
        void Crtl_AdaptImage(object sender, EventArgs e)
        {
            try
            {
                if (AdaptImageEvent != null)
                {
                    AdaptImageEvent(sender, e);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("自定义窗体图片自适应委托异常，请联系开发商！\r\n" + ex.Message);
            }
        }



        #endregion


        #region 窗体外部调用Funcs
        /// <summary>
        /// 初始化窗体
        /// </summary>
        public Response InitWindow()
        {
            try
            {
                if (!isInitWindow)
                {
                    winHandle = viewPort.HalconWindow;
                    isInitWindow = true;
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("窗体" + this.Name + "初始化失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 设置窗体外部刷新模式
        /// </summary>
        public Response SetOutRefreshMode(bool isOpen)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");
                OuterRefersh = isOpen;
                if (isOpen)
                {
                    ROINeedCreate = false;
                    curROIIndex = -1;
                    ROICanMove = false;
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("设置窗体" + this.Name + "外部刷新模式：" + isOpen.ToString() + "失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 获取窗体句柄
        /// </summary>
        public Response<HTuple> GetWindowHandle()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");
                return Response<HTuple>.Ok(winHandle);
            }
            catch (Exception ex)
            {
                return Response<HTuple>.Fail("获取窗体" + this.Name + "句柄失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 设置相机设置运行模式
        /// </summary>
        public Response SetCameraSetMode(bool state)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");
                if (state)
                {
                    this.Invoke(new Action(() =>
                    {
                        tsmi_AutoFocus.Visible = true;
                    }));
                }
                else
                {
                    needAutoFocus = false;
                    this.Invoke(new Action(() =>
                    {
                        tsmi_AutoFocus.Visible = false;
                    }));
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("设置窗体" + this.Name + "相机运行模式失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 设置窗体右菜单是否显示
        /// </summary>
        public Response SetRightMenuEnable(bool state)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");
                if (state)
                {
                    this.Invoke(new Action(() =>
                    {
                        viewPort.ContextMenuStrip = contextMenuStrip1;
                    }));
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        viewPort.ContextMenuStrip = null;
                    }));
                }

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("设置窗体" + this.Name + "右菜单状态：" + state.ToString() + "失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 设置窗体显示区域大小
        /// </summary>
        public Response SetWindowSizeAndPosi(int Image_Height, int Image_Width, int x = 0, int y = 0)
        {
            try
            {
                //if (!isInitWindow)
                //    throw new Exception("未初始化窗体！");

                ////image_x = y;
                ////image_y = x;
                ////image_x1 = Image_Height;
                ////image_y1 = Image_Width;
                ////image_height = Image_Height;
                ////image_width = Image_Width;
                //int startR = 0;
                //int startC = 0;
                #region 新加
                int imgWidth, imgHeight, winRow, winCol, winWidth, winHeight, partWidth, partHeight;
                imgWidth = Image_Width;
                imgHeight = Image_Height;
                viewPort.HalconWindow.GetWindowExtents(out winRow, out winCol, out winWidth, out winHeight);
                int startR = 0;
                int startC = 0;
                double widthrate = imgWidth / (double)winWidth;
                double heightrate = imgHeight / (double)winHeight;

                //if (winWidth < winHeight)
                if (widthrate > heightrate)
                {
                    partWidth = imgWidth;
                    partHeight = imgWidth * winHeight / winWidth;
                    startR = (partHeight - imgHeight) / 2;
                }
                else
                {
                    partWidth = imgHeight * winWidth / winHeight;
                    partHeight = imgHeight;
                    startC = (partWidth - imgWidth) / 2;
                }
                #endregion

                image_x = -startR;
                image_y = -startC;
                image_x1 = partHeight - startR;
                image_y1 = partWidth - startC;
                image_height = imgHeight;
                image_width = imgWidth;

                this.Invoke(new Action(() =>
                {
                    lab_ImageSize.Text = Image_Height + " * " + Image_Width;
                    //viewPort.ImagePart = new Rectangle(-startC, -startR, Image_Width, Image_Height);
                    //viewPort.ImagePart = new Rectangle(image_y.I, image_x.I, partWidth, partHeight);
                    viewPort.ImagePart = new Rectangle(0, 0, imgWidth, imgHeight);
                }));

                //if (xLine != null)
                //    xLine.Dispose();
                //if (yLine != null)
                //    yLine.Dispose();
                //HOperatorSet.GenContourPolygonXld(out xLine, new HTuple(0).TupleConcat(image_height), new HTuple(image_width / 2).TupleConcat(new HTuple(image_width / 2)));
                //HOperatorSet.GenContourPolygonXld(out yLine, new HTuple(image_height / 2).TupleConcat(new HTuple(image_height / 2)), new HTuple(0).TupleConcat(image_width));
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("重置窗体" + this.Name + "显示区域失败！\r\nImage_X:" + x + " ,Image_Y:" + y + " ," +
                    "AreaHeight:" + Image_Height + " ,AreaWidth:" + Image_Width + "\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 获取窗体显示原始区域大小  宽,高
        /// </summary>
        public Response<List<int>> GetWindowOrigSize()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");
                List<int> stemp = new List<int>();
                stemp.Add(image_width.I);
                stemp.Add(image_height.I);
                return Response<List<int>>.Ok(stemp);
            }
            catch (Exception ex)
            {
                return Response<List<int>>.Fail("获取窗体" + this.Name + "显示原始区域失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 获取窗体当前显示区域大小  x,y,宽,高
        /// </summary>
        public Response<List<int>> GetWindowNowSize()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");
                List<int> stemp = new List<int>();
                stemp.Add(viewPort.ImagePart.X);
                stemp.Add(viewPort.ImagePart.Y);
                stemp.Add(viewPort.ImagePart.Width);
                stemp.Add(viewPort.ImagePart.Height);
                return Response<List<int>>.Ok(stemp);
            }
            catch (Exception ex)
            {
                return Response<List<int>>.Fail("获取窗体" + this.Name + "当前显示区域大小失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 获取当前窗体图像
        /// </summary>
        public Response<HObject> GetWindowImage()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (!OuterRefersh)
                {
                    if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                    {
                        throw new Exception("图像为空！");
                    }
                }
                else
                    throw new Exception("外部刷新模式不支持获取图像！");
                return Response<HObject>.Ok(curImage.Clone());
            }
            catch (Exception ex)
            {
                return Response<HObject>.Fail("获取窗体" + this.Name + "当前图像失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 获取窗体映射图片
        /// </summary>
        public Response<HObject> DumpWindowImage()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                HObject out_image;
                HOperatorSet.DumpWindowImage(out out_image, winHandle);
                return Response<HObject>.Ok(out_image);
            }
            catch (Exception ex)
            {
                return Response<HObject>.Fail("获取窗体" + this.Name + "映射图失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 显示图像
        /// </summary>
        public Response DisplayImage(HObject inputImage)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (inputImage == null)
                {
                    return Response.Fail("图像为空！");
                }

                if (curImage != null)
                    curImage.Dispose();
                curImage = inputImage.Clone();

                Response<List<HTuple>> stemp = VisionComFunc.GetImageSize(curImage);
                if (image_width == null || image_height == null || (stemp.Data[1].D != image_height.D) || (stemp.Data[0].D != image_width.D))
                {
                    SetWindowSizeAndPosi(stemp.Data[1], stemp.Data[0]);
                }
                //if (needAutoFocus)
                //{
                //    string value = VisionComFunc.GetImgQual(curImage).Data[1].D.ToString("N2");
                //    this.Invoke(new Action(() =>
                //    {
                //        this.lab_MouseQual.Text = value;
                //    }));
                //}

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("窗体" + this.Name + "显示图片失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 显示OBJ
        /// <para>objColor    Obj显示的颜色,如果是数字则显示多种颜色</para>
        /// <para>lineSize    Obj显示线宽</para>
        /// <para>drawMode    0：轮廓   1：填充</para>
        /// <para>lineStyle   数值表示线段间隔，不为0则为虚线</para>
        /// </summary>
        public Response DisplayObject(HObject inputObject, string objColor, int lineSize = 1, int drawMode = 0, int lineStyle = 0)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                objListParam.Add(objColor + "," + lineSize + "," + drawMode + "," + lineStyle);
                objList.Add(inputObject.Clone());
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("窗体" + this.Name + "显示OBJ失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 显示ROI
        /// </summary>
        public Response DisplayROI(ROI ROI)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                ROIList.Add(ROI.DeepClone());
                curROIIndex = ROIList.Count - 1;

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("窗体" + this.Name + "显示ROI失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 显示ROI
        /// </summary>
        public Response DisplayActiveROI()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");
                curROIIndex = 0;

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("窗体" + this.Name + "显示ROI失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 显示ROI
        /// </summary>
        public Response DisplayROINotClone(ROI ROI)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                ROIList.Add(ROI);
                curROIIndex = ROIList.Count - 1;

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("窗体" + this.Name + "显示ROI失败！\r\n" + ex.Message);
            }
        }


        /// <summary>
        /// 窗体显示信息
        /// <para>context：显示文本内容</para>
        ///  <para>row，column：显示文本坐标</para>
        ///  <para>font_color：显示文本颜色</para>
        ///  <para>size：文本字体大小</para>
        ///  <para>needBackGround：文本是否需要背景，默认false</para>
        ///  <para>back_color：文本背景颜色</para>
        ///  <para>bold：文本是否粗体，默认false</para>
        ///  <para>slant：文本是否斜体，默认false</para>
        /// </summary>
        public Response DisplayMessage(string context, HTuple row, HTuple column, string font_color, int size = 12, bool needBackGround = true,
            string back_color = "", bool bold = false, bool slant = false)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                messageListParam.Add((row.D).ToString("N2") + "%" + (column.D).ToString("N2") + "%" + font_color + "%" + size + "%" + (needBackGround.ToString()).ToLower() + "%" + back_color + "%" + (bold.ToString()).ToLower() + "%" + (slant.ToString()).ToLower());
                messageList.Add(context);

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("窗体" + this.Name + "显示信息" + context + "失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 窗体显示刷新
        /// </summary>
        public Response DisplayBuffer()
        {
            try
            {
                RefreshDisp();
                //Ctrl_LoadedImage(new object(), new EventArgs());
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("窗体" + this.Name + "显示刷新失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 清空窗体所有内容
        /// </summary>
        public Response ClearALLWindow()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (curImage != null)
                    curImage.Dispose();
                objListParam.Clear();
                for (int i = 0; i < objList.Count; i++)
                {
                    objList[i].Dispose();
                }
                objList.Clear();
                messageListParam.Clear();
                messageList.Clear();

                curROIIndex = -1;
                ROIList.Clear();

                imageCanMove = false;
                ROINeedCreate = false;
                ROICanMove = false;

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("清空窗体" + this.Name + "所有内容失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 获取窗体当前选中ROI
        /// </summary>
        public Response<ROI> GetActiveROI()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (curROIIndex != -1)
                {
                    return Response<ROI>.Ok(ROIList[curROIIndex].DeepClone());
                }
                else
                    throw new Exception("当前窗口未选中任何ROI！");

            }
            catch (Exception ex)
            {
                return Response<ROI>.Fail("获取窗体" + this.Name + "当前ROI失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 获取窗体所有ROI
        /// </summary>
        public Response<List<ROI>> GetROIList()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (ROIList.Count > 0)
                {
                    List<ROI> temp_ROIs = new List<ROI>();
                    for (int i = 0; i < ROIList.Count; i++)
                    {
                        temp_ROIs.Add(ROIList[i].DeepClone());
                    }
                    return Response<List<ROI>>.Ok(temp_ROIs);
                }
                else
                    throw new Exception("当前窗口无任何ROI");

            }
            catch (Exception ex)
            {
                return Response<List<ROI>>.Fail("获取窗体" + this.Name + "所有ROI失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 绘制图像
        /// </summary>
        public Response<HObject> DrawObject(string selectmode)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                HObject DrawObject = null;
                switch (selectmode)
                {
                    case "Circle":
                        HTuple col_circle, row_circle, rad_circle;
                        HOperatorSet.DrawCircle(winHandle, out row_circle, out col_circle, out rad_circle);
                        HOperatorSet.GenCircleContourXld(out DrawObject, row_circle, col_circle, rad_circle, 0, 6.28318, "positive", 1);
                        break;
                    case "Line":
                        HTuple row1_line, col1_line, row2_line, col2_line;
                        HOperatorSet.DrawLine(winHandle, out row1_line, out col1_line, out row2_line, out col2_line);
                        HOperatorSet.GenContourPolygonXld(out DrawObject, (row1_line).TupleConcat(row2_line), (col1_line).TupleConcat(col2_line));
                        break;
                    default:
                        throw new Exception("选择形状无法绘制!");
                }
                return Response<HObject>.Ok(DrawObject);
            }
            catch (Exception ex)
            {
                return Response<HObject>.Fail("窗体" + this.Name + "绘制图像失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 清空所有对象
        /// </summary>
        /// <returns></returns>
        public Response ClearAllObj()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                objListParam.Clear();
                for (int i = 0; i < objList.Count; i++)
                {
                    objList[i].Dispose();
                }
                objList.Clear();

                messageListParam.Clear();
                messageList.Clear();

                curROIIndex = -1;
                ROIList.Clear();

                ROINeedCreate = false;
                ROICanMove = false;

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("清空窗体" + this.Name + "所有OBJ失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 清空除ROI之外的对象
        /// </summary>
        /// <returns></returns>
        public Response ClearDispObj()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                objListParam.Clear();
                for (int i = 0; i < objList.Count; i++)
                {
                    objList[i].Dispose();
                }
                objList.Clear();

                messageListParam.Clear();
                messageList.Clear();

                ROINeedCreate = false;

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("清空窗体" + this.Name + "所有OBJ失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 加载图像
        /// </summary>
        /// <returns></returns>
        public Response Ctrl_LoadImage()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "bmp文件(*.bmp)|*.bmp|png文件(*.png)|*.png|jpg文件(*.jpg)|*.jpg";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Response<HObject> runInfo = VisionComFunc.ReadImage(openFileDialog.FileName);
                    if (runInfo.IsSuccessful)
                    {
                        ClearALLWindow();
                        DisplayImage(runInfo.Data);
                        RefreshDisp();
                        runInfo.Data.Dispose();
                    }
                    else
                    {
                        throw new Exception(runInfo.Msg);
                    }
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("加载图片失败！\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 保存图像
        /// </summary>
        /// <returns></returns>
        public Response Ctrl_SaveImage()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                {
                    throw new Exception("窗体不含图像！");
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "bmp文件(*.bmp)|*.bmp|png文件(*.png)|*.png";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Response runInfo2 = VisionComFunc.WriteImage(curImage, saveFileDialog.FileName);
                    if (!runInfo2.IsSuccessful)
                    {
                        throw new Exception(runInfo2.Msg);
                    }
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("保存图像失败！\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 保存窗体缩略图
        /// </summary>
        /// <returns></returns>
        public Response Ctrl_SaveWindow()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                Response<HObject> sdf = DumpWindowImage();
                if (!sdf.IsSuccessful)
                    throw new Exception(sdf.Msg);

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "jpg文件(*.jpg)|*.jpg";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Response runInfo2 = VisionComFunc.WriteImage(sdf.Data, saveFileDialog.FileName);
                    if (!runInfo2.IsSuccessful)
                    {
                        throw new Exception(runInfo2.Msg);
                    }
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("保存缩略图失败！\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 添加长方形ROI
        /// </summary>
        /// <returns></returns>
        public Response Ctrl_AddRectROI()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                {
                    throw new Exception("窗体不含图像,无法添加ROI！");
                }

                if (OuterRefersh)
                    throw new Exception("外部刷新模式下,无法添加ROI！");
                ROIType = 0;
                ROINeedCreate = true;
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("添加矩形ROI失败！\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 添加有方向矩形ROI
        /// </summary>
        /// <returns></returns>
        public Response Ctrl_AddDirRectROI()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                {
                    throw new Exception("窗体不含图像,无法添加ROI！");
                }

                if (OuterRefersh)
                    throw new Exception("外部刷新模式下,无法添加ROI！");
                ROIType = 1;
                ROINeedCreate = true;
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("添加带方向矩形ROI失败！\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 添加圆形ROI
        /// </summary>
        /// <returns></returns>
        public Response Ctrl_AddCircleROI()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                {
                    throw new Exception("窗体不含图像,无法添加ROI！");
                }

                if (OuterRefersh)
                    throw new Exception("外部刷新模式下,无法添加ROI！");
                ROIType = 2;
                ROINeedCreate = true;
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("添加圆形ROI失败！\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 保存选中的ROI
        /// </summary>
        /// <returns></returns>
        public Response Ctrl_SelectROISave()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (curROIIndex != -1)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "ROI文件(*.roi)|*.roi";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Response r = FileHelper.SerializeFile(ROIList[curROIIndex], saveFileDialog.FileName);
                        if (r.IsSuccessful)
                        {
                            MessageBox.Show("保存成功！");
                        }
                        else
                        {
                            throw new Exception(r.Msg);
                        }
                    }
                }
                else
                    throw new Exception("未选中任何ROI！");
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("保存当前ROI失败！\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 加载ROI
        /// </summary>
        /// <returns></returns>
        public Response Ctrl_ROILoad()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                {
                    throw new Exception("窗体不含图像,无法加载ROI！");
                }

                if (OuterRefersh)
                    throw new Exception("外部刷新模式下,无法加载ROI！");

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "ROI文件(*.roi)|*.roi";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Response<object> tempresult = FileHelper.AntiSerializeFile(openFileDialog.FileName);
                    if (tempresult.IsSuccessful)
                    {
                        ROINeedCreate = false;
                        curROIIndex = -1;
                        ROIList.Clear();
                        ROICanMove = false;

                        ROIList.Add((ROI)tempresult.Data);
                        RefreshDisp();
                    }
                    else
                    {
                        throw new Exception(tempresult.Msg);
                    }
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("读取ROI失败！\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 删除所有ROI
        /// </summary>
        /// <returns></returns>
        public Response Ctrl_DelAllROI()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                ROINeedCreate = false;
                curROIIndex = -1;
                ROIList.Clear();
                ROICanMove = false;

                RefreshDisp();
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("删除所有ROI失败！\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 删除选中ROI
        /// </summary>
        /// <returns></returns>
        public Response Ctrl_DelSelectROI()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (curROIIndex != -1)
                {
                    ROIList.RemoveAt(curROIIndex);
                    ROINeedCreate = false;
                    curROIIndex = -1;
                    ROICanMove = false;
                    RefreshDisp();
                }
                else
                    throw new Exception("未选中任何ROI！");
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("删除指定ROI失败！\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// ROI排序
        /// </summary>
        /// <returns></returns>
        public Response Ctrl_ROIOrder()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");


                objListParam.Clear();
                for (int j = 0; j < objList.Count; j++)
                {
                    objList[j].Dispose();
                }
                objList.Clear();
                messageListParam.Clear();
                messageList.Clear();

                for (int i = 0; i < ROIList.Count; i++)
                {
                    HObject region = ROIList[i].getRegion();
                    HTuple area, row, column;
                    HOperatorSet.AreaCenter(region, out area, out row, out column);

                    DisplayMessage(i.ToString(), row, column, "red", 12);
                    RefreshDisp();
                    region.Dispose();
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("显示ROI序号失败！\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 自适应图像
        /// </summary>
        public void Ctrl_ApaptImage()
        {
            tsmi_AdaptImage_Click(null, null);
        }

        #endregion

        /// <summary>
        /// 设置窗体显示OBJ颜色
        /// </summary>
        private Response SetObjectColor(string objectColor)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (Regex.IsMatch(objectColor, @"^[+-]?\d*$"))
                {
                    HOperatorSet.SetColored(winHandle, Convert.ToInt32(objectColor));
                }
                else
                {
                    HOperatorSet.SetColor(winHandle, objectColor);
                }

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("设置窗体" + this.Name + "显示OBJ颜色失败！" + ex.Message);
            }
        }


        /// <summary>
        /// 设置窗体OBJ绘制模式
        /// <para>0：轮廓</para>
        /// <para>1：填充</para>
        /// </summary>
        private Response SetDrawMode(int select)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");
                if (select == 0)
                {
                    HOperatorSet.SetDraw(winHandle, "margin");
                }
                else
                {
                    HOperatorSet.SetDraw(winHandle, "fill");
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("设置窗体" + this.Name + "OBJ绘制模式失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 设置线宽
        /// </summary>
        private Response SetLineSize(int size)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                HOperatorSet.SetLineWidth(winHandle, size);
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("窗体" + this.Name + "线宽" + size + "设置失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 设置线显示样式
        /// </summary>
        /// <param name="size">虚线点间隔</param>
        /// <returns></returns>
        private Response SetLineStyle(int size)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (size == 0)
                {
                    HOperatorSet.SetLineStyle(winHandle, new HTuple());
                }
                else
                {
                    HOperatorSet.SetLineStyle(winHandle, size);
                }

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("窗体" + this.Name + "线显示样式" + size + "设置失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 设置窗体显示缓冲
        /// </summary>
        private Response SetFlush(bool state)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (state)
                {
                    HOperatorSet.SetSystem("flush_graphic", "true");
                    HOperatorSet.SetColor(winHandle, "black");
                    HOperatorSet.DispLine(winHandle, -100.0, -100.0, -101.0, -101.0);
                }
                else
                {
                    HOperatorSet.SetSystem("flush_graphic", "false");
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("设置窗体" + this.Name + "显示缓冲失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 图像内部刷新
        /// </summary>
        private void RefreshDisp()
        {
            try
            {
                lock (slock)
                {
                    if (isInitWindow)
                    {
                        if (!needClearWindow)
                        {
                            SetFlush(false);
                            HOperatorSet.ClearWindow(winHandle);
                            //图片
                            if (!VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                            {
                                Response<List<HTuple>> stemp = VisionComFunc.GetImageSize(curImage);
                                if (stemp.Data != null)
                                {
                                    if (image_width == null || image_height == null || (stemp.Data[1].D != image_height.D) || (stemp.Data[0].D != image_width.D))
                                    {
                                        SetWindowSizeAndPosi(stemp.Data[1], stemp.Data[0]);
                                    }
                                    HOperatorSet.DispObj(curImage, winHandle);
                                }
                            }

                            if (!OuterRefersh)
                            {
                                //ROI
                                if (ROIList.Count > 0)
                                {
                                    SetDrawMode(0);
                                    SetLineStyle(0);
                                    for (int i = 0; i < ROIList.Count; i++)
                                    {
                                        if (curROIIndex == i)
                                        {
                                            SetObjectColor("green");
                                            ROIList[i].draw(winHandle);
                                            SetObjectColor("red");
                                            ROIList[i].displayActive(winHandle);
                                        }
                                        else
                                        {
                                            SetObjectColor("blue");
                                            ROIList[i].draw(winHandle);
                                        }
                                    }
                                    SetDrawMode(1);
                                }
                            }

                            //OBJ
                            for (int i = 0; i < objList.Count; i++)
                            {
                                string[] fsd = objListParam[i].Split(',');
                                SetObjectColor(fsd[0]);
                                SetLineSize(Convert.ToInt32(fsd[1]));
                                SetDrawMode(Convert.ToInt32(fsd[2]));
                                SetLineStyle(Convert.ToInt32(fsd[3]));
                                HOperatorSet.DispObj(objList[i], winHandle);
                            }

                            //显示文字
                            for (int i = 0; i < messageList.Count; i++)
                            {
                                string[] fsd = messageListParam[i].Split('%');

                                DispFunc.set_display_font(winHandle, Convert.ToInt32(fsd[3]), "mono", fsd[6], fsd[7]);

                                if (fsd[4] == "true")
                                {
                                    if (fsd[5] == "")
                                    {
                                        DispFunc.disp_message(winHandle, messageList[i], "image", Convert.ToDouble(fsd[0]), Convert.ToDouble(fsd[1]), fsd[2], "true");
                                    }
                                    else
                                    {
                                        DispFunc.disp_message(winHandle, messageList[i], "image", Convert.ToDouble(fsd[0]), Convert.ToDouble(fsd[1]), fsd[2], fsd[5]);
                                    }
                                }
                                else
                                {
                                    DispFunc.disp_message(winHandle, messageList[i], "image", Convert.ToDouble(fsd[0]), Convert.ToDouble(fsd[1]), fsd[2], "false");
                                }
                            }

                            if (isShowCrossLine)
                            {
                                SetObjectColor("red");
                                HOperatorSet.DispObj(xLine, winHandle);
                                HOperatorSet.DispObj(yLine, winHandle);
                            }
                            SetFlush(true);
                        }
                        else
                        {
                            needClearWindow = false;

                            if (curImage != null)
                                curImage.Dispose();
                            objListParam.Clear();
                            for (int i = 0; i < objList.Count; i++)
                            {
                                objList[i].Dispose();
                            }
                            objList.Clear();
                            messageListParam.Clear();
                            messageList.Clear();

                            curROIIndex = -1;
                            ROIList.Clear();

                            imageCanMove = false;
                            ROINeedCreate = false;
                            ROICanMove = false;

                            SetFlush(false);
                            HOperatorSet.ClearWindow(winHandle);
                            SetFlush(true);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("图像内部刷新异常！\r\n" + ex.Message);
            }
        }

        Action<Rectangle> action;

        /// <summary>
        /// 图像缩放
        /// </summary>
        private void ScaleImage(double x, double y, double scale)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                double num = (y - image_y.D) / (image_y1.D - image_y.D);
                double num2 = (x - image_x.D) / (image_x1.D - image_x.D);
                double num3 = (image_y1.D - image_y.D) * scale;
                double num4 = (image_x1.D - image_x.D) * scale;
                image_y = y - num3 * num;
                image_y1 = y + num3 * (1.0 - num);
                image_x = x - num4 * num2;
                image_x1 = x + num4 * (1.0 - num2);
                int num5 = Convert.ToInt32(Math.Round(num3));
                int num6 = Convert.ToInt32(Math.Round(num4));
                Rectangle imagePart = viewPort.ImagePart;
                imagePart.X = Convert.ToInt32(Math.Round(image_y.D));
                imagePart.Y = Convert.ToInt32(Math.Round(image_x.D));
                imagePart.Width = ((num5 > 0) ? num5 : 1);
                imagePart.Height = ((num6 > 0) ? num6 : 1);
                viewPort.ImagePart = imagePart;
                if (action != null)
                {
                    action(imagePart);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("图像缩放异常！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 图像移动
        /// </summary>
        private void moveImage(double motionX, double motionY)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                image_x = image_x.D - motionX;
                image_x1 = image_x1.D - motionX;
                image_y = image_y.D - motionY;
                image_y1 = image_y1.D - motionY;
                Rectangle rect = viewPort.ImagePart;
                rect.X = Convert.ToInt32(Math.Round(image_y.D));
                rect.Y = Convert.ToInt32(Math.Round(image_x.D));
                viewPort.ImagePart = rect;
            }
            catch (Exception ex)
            {
                throw new Exception("图像移动异常！\r\n" + ex.Message);
            }
        }

        private void viewPort_HMouseUp(object sender, HMouseEventArgs e)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (e.Button == MouseButtons.Middle)
                {
                    imageCanMove = false;
                }
                else if (e.Button == MouseButtons.Left)
                {
                    ROICanMove = false;
                }

                Ctrl_WinMouseUp(sender, e);
                IsMouseDown = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体鼠标抬起事件异常！\r\n" + ex.Message);
            }
        }

        private void viewPort_HMouseWheel(object sender, HMouseEventArgs e)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                double temp_x = e.X;
                double temp_y = e.Y;
                if (e.Delta > 0)
                {
                    ScaleImage(temp_y, temp_x, 0.9);
                }
                else
                {
                    ScaleImage(temp_y, temp_x, 1.1);
                }

                RefreshDisp();

                Ctrl_WinMouseWheel(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体鼠标滚轮事件异常！\r\n" + ex.Message);
            }
        }

        private void viewPort_HMouseMove(object sender, HMouseEventArgs e)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                double temp_x = e.X;
                double temp_y = e.Y;
                lab_MousePlace.Text = Convert.ToInt32(temp_y) + "*" + Convert.ToInt32(temp_x);
                if (!OuterRefersh)
                {
                    if (curImage != null)
                    {
                        int gray = VisionComFunc.GetImgGray(curImage, Convert.ToInt32(temp_y), Convert.ToInt32(temp_x)).Data;
                        lab_MouseGray.Text = "Gray：" + gray;
                    }
                }

                if (imageCanMove)
                {
                    double motionX = temp_y - move_x;
                    double motionY = temp_x - move_y;
                    if ((motionX != 0.0) || (motionY != 0.0))
                    {
                        moveImage(motionX, motionY);
                        move_x = temp_y - motionX;
                        move_y = temp_x - motionY;
                        RefreshDisp();
                    }
                }
                else if (ROICanMove)
                {
                    ROIList[curROIIndex].moveByHandle(temp_x, temp_y);
                    RefreshDisp();
                }
                if(IsMouseDown)
                    Ctrl_WinMouseMove(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体鼠标移动事件异常！\r\n原因:" + ex.Message);
            }
        }
        public void HMouseDown()
        {
            SetCursorPos(Width / 2, Height / 2);  //设置鼠标放的位置
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            //viewPort_HMouseDown(viewPort, viewPort.HMouseDown);
        }

        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewPort_HMouseDown(object sender, HMouseEventArgs e)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                #region 2025年1月12日 彭木德分析前人此处代码，应该和当前项目关系不大，应该是套用控件没删除冗余代码

                double temp_x = e.X;
                double temp_y = e.Y;

                if ((e.Clicks == 2) && (e.Button == MouseButtons.Left))   //双击
                {
                    CameraMoveFunc(temp_y, temp_x);
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    move_x = Convert.ToInt32(temp_y);
                    move_y = Convert.ToInt32(temp_x);
                    imageCanMove = true;
                }
                else if (e.Button == MouseButtons.Left)
                {
                    if (!OuterRefersh)
                    {
                        if (ROINeedCreate)
                        {
                            ROINeedCreate = false;
                            switch (ROIType)
                            {
                                case 0:
                                    ROI temp_rect1 = new ROIRectangle1();
                                    temp_rect1.createROI(temp_x, temp_y);
                                    ROIList.Add(temp_rect1);
                                    break;
                                case 1:
                                    ROI temp_rect2 = new ROIRectangle2();
                                    temp_rect2.createROI(temp_x, temp_y);
                                    ROIList.Add(temp_rect2);
                                    break;
                                case 2:
                                    ROI temp_circle = new ROICircle();
                                    temp_circle.createROI(temp_x, temp_y);
                                    ROIList.Add(temp_circle);
                                    break;
                                case 3:
                                    ROI temp_fixrect1 = new ROIFixRectangle1();
                                    temp_fixrect1.createROI(temp_x, temp_y);
                                    ROIList.Add(temp_fixrect1);
                                    break;
                            }
                            curROIIndex = ROIList.Count - 1;
                            RefreshDisp();
                        }
                        else if (ROIList.Count == 1)
                        {

                        }
                        else if (ROIList.Count > 0)
                        {
                            double max = 10000, dist = 0;
                            double epsilon = 35.0;
                            curROIIndex = -1;

                            for (int i = 0; i < ROIList.Count; i++)
                            {
                                dist = ROIList[i].distToClosestHandle(temp_x, temp_y);
                                if ((dist < max) && (dist < epsilon))
                                {
                                    max = dist;
                                    curROIIndex = i;
                                    break;
                                }
                            }

                            if (curROIIndex != -1)
                            {
                                ROICanMove = true;
                                RefreshDisp();
                            }
                        }
                    }
                    else
                    {
                        ROINeedCreate = false;
                        ROICanMove = false;
                    }
                }

                #endregion

                #region 之前的"建模时在鼠标按下位置创建ROI"的功能，实际上是通过发布鼠标按下事件给外部处理ROI的创建

                //Ctrl_WinMouseDown(sender, e);

                #endregion

                #region 如今要改为按下ROI内移动时是拖拽ROI，按下在ROI外和原来一样，本项目ROI只有一个可以直接选择ROI列表第一个元素

                // 创建 HTuple 类型的坐标
                HTuple row = e.Y;
                HTuple column = e.X;

                // 使用 test_region_point 操作符判断点是否在区域内
                HTuple isInside;

                HOperatorSet.TestRegionPoint(ROIList[0].getRegion(), row, column, out isInside);

                // 将鼠标按下点是否在ROI内的结果转换为布尔值并输出
                bool pointIsInside = isInside == 1;

                //如果在，则移动时是拖拽ROI，否则是创建ROI
                if (pointIsInside)
                    IsMouseDown = true;
                else
                    Ctrl_WinMouseDown(sender, e);

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体鼠标按下事件异常！\r\n" + ex.Message);
            }
        }

        private void viewPort_Resize(object sender, EventArgs e)
        {
            try
            {
                //RefreshDisp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmi_SaveImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                {
                    throw new Exception("窗体不含图像!");
                }
                else
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "bmp文件(*.bmp)|*.bmp|png文件(*.png)|*.png";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        VisionComFunc.WriteImage(curImage, saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存图像失败！\r\n" + ex.Message);
            }
        }

        private void tsmi_AdaptImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                    throw new Exception("当前窗口无图片!");

                SetWindowSizeAndPosi(image_height, image_width);

                objListParam.Clear();
                for (int i = 0; i < objList.Count; i++)
                {
                    objList[i].Dispose();
                }
                objList.Clear();
                messageListParam.Clear();
                messageList.Clear();

                imageCanMove = false;

                RefreshDisp();
                Crtl_AdaptImage(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体图像自适应失败！\r\n" + ex.Message);
            }
        }

        private void tsmi_ClearWindow_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                needClearWindow = true;
                RefreshDisp();
            }
            catch (Exception ex)
            {
                MessageBox.Show("清空窗体失败！\r\n" + ex.Message);
            }
        }

        private void tsmi_ComInfoEnable_Click(object sender, EventArgs e)
        {
            if (!isInitWindow)
                throw new Exception("未初始化窗体！");

            if (tsmi_ComInfoEnable.Text == "显示常规状态栏")
            {
                stip_ComInfo.Visible = true;
                tsmi_ComInfoEnable.Text = "关闭常规状态栏";
            }
            else
            {
                stip_ComInfo.Visible = false;
                tsmi_ComInfoEnable.Text = "显示常规状态栏";
            }
        }

        private void tsmi_CrossLine_Click(object sender, EventArgs e)
        {
            if (tsmi_CrossLine.Text == "显示中心线")
            {
                if (xLine == null)
                {
                    HOperatorSet.GenContourPolygonXld(out xLine, new HTuple(0).TupleConcat(image_height), new HTuple(image_width / 2).TupleConcat(new HTuple(image_width / 2)));
                    HOperatorSet.GenContourPolygonXld(out yLine, new HTuple(image_height / 2).TupleConcat(new HTuple(image_height / 2)), new HTuple(0).TupleConcat(image_width));
                }
                isShowCrossLine = true;
                RefreshDisp();
                tsmi_CrossLine.Text = "隐藏中心线";
            }
            else
            {
                isShowCrossLine = false;
                RefreshDisp();
                tsmi_CrossLine.Text = "显示中心线";
            }
        }

        private void tsmi_AutoFocus_Click(object sender, EventArgs e)
        {
            if (!isInitWindow)
                throw new Exception("未初始化窗体！");

            if (needAutoFocus)
            {
                tsmi_AutoFocus.Text = "辅助对焦关";
                needAutoFocus = false;
            }
            else
            {
                tsmi_AutoFocus.Text = "辅助对焦开";
                needAutoFocus = true;
            }
        }

        private void tsmi_LargeFrom_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");
                LargeView temp_view = new LargeView(viewPort.ImagePart, out winHandle, new HalconDotNet.HMouseEventHandler(this.viewPort_HMouseWheel), ref action);
                temp_view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体放大显示失败！\r\n" + ex.Message);
            }
            finally
            {
                winHandle = viewPort.HalconWindow;
            }
        }







        /////////////////////////////////////////////////////////////////新增/////////////////////////////////////////////

        /// <summary>
        /// 加载了图像事件
        /// </summary>
        public event EventHandler CtrlLoadedImage;
        private void Ctrl_LoadedImage(object sender, EventArgs e)
        {
            if (CtrlLoadedImage != null)
            {
                CtrlLoadedImage(sender, e);
            }
        }
        /// <summary>
        /// 将Mark点移动到相机视野中心事件句柄
        /// </summary>
        /// <param name="Row">Mark点 Row坐标</param>
        /// <param name="Col">Mark点 Column坐标</param>
        /// <param name="RowDist">Mark点距离视野中心Row方向距离</param>
        /// <param name="ColDist">Mark点距离视野中心Column方向距离</param>
        /// <param name="AllDist">Mark点距离视野中心直线距离</param>
        public delegate void MarkCamCenterEventHandler(double Row, double Col, double RowDist, double ColDist, double AllDist);
        /// <summary>
        /// 将Mark点移动到相机视野中心事件
        /// </summary>
        public MarkCamCenterEventHandler CtrlMarkCamCenterEvent;
        void MarkCamCenter(double Row, double Col, double RowDist, double ColDist, double AllDist)
        {
            try
            {
                if (CtrlMarkCamCenterEvent != null)
                {
                    CtrlMarkCamCenterEvent(Row, Col, RowDist, ColDist, AllDist);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取图片中心距离委托异常，请联系开发商！\r\n" + ex.Message);
            }
        }


        private void CameraMoveFunc(double Row, double Col)
        {
            try
            {
                if (!VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                {
                    double rowDist = Row - (image_height.D / 2);
                    double colDist = Col - (image_width.D / 2);
                    double allDist = Math.Sqrt(Math.Pow(rowDist, 2) + Math.Pow(colDist, 2));
                    MarkCamCenter(Row, Col, rowDist, colDist, allDist);
                    //MessageBox.Show(rowDist.ToString() + "\r\n" + colDist.ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("获取图片中心距离异常！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 显示ROI列表
        /// </summary>
        public Response DisplayROI(ArrayList roiList)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                foreach (ROI stemp in roiList)
                {
                    ROIList.Add(stemp.DeepClone());
                }

                curROIIndex = -1;

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("窗体" + this.Name + "显示ROI列表失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 获取当前ROI中心坐标列表
        /// </summary>
        /// <returns></returns>
        public Response<List<Point>> GetROICenterList()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (ROIList.Count <= 0)
                    throw new Exception("当前窗口中没有ROI");
                List<Point> centerList = new List<Point>();
                HTuple thisArea, thisRow, thisColumn;
                for (int idx = 0; idx < ROIList.Count; idx++)
                {
                    HObject thisROI = (ROIList[idx]).getRegion();
                    HOperatorSet.AreaCenter(thisROI, out thisArea, out thisRow, out thisColumn);
                    centerList.Add(new Point((int)thisRow.D, (int)thisColumn.D));
                    thisROI.Dispose();
                }
                return Response<List<Point>>.Ok(centerList);
            }
            catch (Exception ex)
            {
                return Response<List<Point>>.Fail("获取当前ROI中心坐标列表失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 获取当前选中ROI索引号
        /// </summary>
        /// <returns></returns>
        public Response<int> GetActiveROIIdx()
        {
            try
            {
                return Response<int>.Ok(curROIIndex);
            }
            catch (Exception ex)
            {
                return Response<int>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 获取窗体中ROI数量
        /// </summary>
        /// <returns></returns>
        public Response<int> GetROICount()
        {
            try
            {
                return Response<int>.Ok(ROIList.Count);
            }
            catch (Exception ex)
            {
                return Response<int>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 清除当前ROI列表
        /// </summary>
        public Response ClearROIList()
        {
            try
            {

                Response sd = Ctrl_DelAllROI();
                if (!sd.IsSuccessful)
                    throw new Exception(sd.Msg);
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 获取当前选中ROI的尺寸
        /// 数据依次为:
        /// Rectangle1: row1, col1, row2, col2
        /// Rectangle2: midR, midC, phi, length1, length2
        /// Circle: midR, midC, radius
        /// </summary>
        public Response<HTuple> GetROISize(int index)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if ((index >= 0) && (index < ROIList.Count))
                {
                    return Response<HTuple>.Ok((ROIList[index]).getModelData());
                }
                else
                    throw new Exception("不存在该ROI！");
            }
            catch (Exception ex)
            {
                return Response<HTuple>.Fail("获取当前选中ROI的尺寸失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 获取当前选中ROI所截取的区域
        /// </summary>
        /// <returns></returns>
        public Response<HObject> GetActiveRegion()
        {
            try
            {
                if (curROIIndex < 0)
                {
                    throw new Exception("未选中任何ROI！");
                }
                else
                {
                    return Response<HObject>.Ok((ROIList[curROIIndex]).getRegion());
                }
            }
            catch (Exception ex)
            {
                return Response<HObject>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 获取ROI列表所截取的区域列表
        /// </summary>
        /// <returns></returns>
        public Response<List<HObject>> GetRegionList()
        {
            try
            {
                List<HObject> resultList = new List<HObject>();
                for (int i = 0; i < ROIList.Count; i++)
                {
                    HObject activeROI = (ROIList[i]).getRegion();
                    resultList.Add(activeROI);
                }
                return Response<List<HObject>>.Ok(resultList);
            }
            catch (Exception ex)
            {
                return Response<List<HObject>>.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 获取当前选中ROI截取的图像
        /// </summary>
        /// <returns></returns>
        public Response<HObject> GetActiveImage()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (!OuterRefersh)
                {
                    if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                    {
                        throw new Exception("图像为空！");
                    }

                    if (curROIIndex < 0)
                    {
                        throw new Exception("未选中任何ROI！");
                    }

                    HObject activeImage;
                    HObject activeRegion = ROIList[curROIIndex].getRegion();
                    HOperatorSet.SetSystem("clip_region", "false");
                    HOperatorSet.ReduceDomain(curImage, activeRegion, out activeImage);
                    activeRegion.Dispose();
                    return Response<HObject>.Ok(activeImage);
                }
                else
                    throw new Exception("外部刷新模式不支持获取图像！");
            }
            catch (Exception ex)
            {
                return Response<HObject>.Fail("获取当前选中ROI截取的图像失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 获取当前窗体中ROI截取的图像列表
        /// </summary>
        /// <returns></returns>
        public Response<List<HObject>> GetActiveImages()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (!OuterRefersh)
                {
                    if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                    {
                        throw new Exception("图像为空！");
                    }

                    if (ROIList.Count < 0)
                    {
                        throw new Exception("当前窗口无任何ROI！");
                    }

                    HObject scurImage;
                    List<HObject> selectImgs = new List<HObject>();
                    for (int idx = 0; idx < ROIList.Count; idx++)
                    {
                        HObject curRegion = (ROIList[idx]).getRegion();
                        HOperatorSet.SetSystem("clip_region", "false");
                        scurImage = new HObject();
                        HOperatorSet.ReduceDomain(curImage, curRegion, out scurImage);
                        curRegion.Dispose();
                        selectImgs.Add(scurImage);
                    }
                    return Response<List<HObject>>.Ok(selectImgs);
                }
                else
                    throw new Exception("外部刷新模式不支持获取图像！");
            }
            catch (Exception ex)
            {
                return Response<List<HObject>>.Fail("获取当前选中ROI截取的图像列表失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 设置窗体底部状态栏是否显示
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Response CtrlShowStatus(bool state)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (state)
                {
                    this.Invoke(new Action(() =>
                    {

                        stip_ComInfo.Visible = true;
                        tsmi_ComInfoEnable.Text = "关闭常规状态栏";
                    }));

                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        stip_ComInfo.Visible = false;
                        tsmi_ComInfoEnable.Text = "显示常规状态栏";
                    }));

                }

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("设置窗体" + this.Name + "底部状态栏是否显示失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 显示窗体中心线
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public Response CtrlShowWinCross(bool state)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (state)
                {
                    tsmi_CrossLine.Text = "显示中心线";
                }
                else
                {
                    tsmi_CrossLine.Text = "显示中心线";
                }
                tsmi_CrossLine_Click(null, null);
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail(this.Name + "显示窗口中心线失败！\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 添加固定长度正方形ROI
        /// </summary>
        /// <returns></returns>
        public Response Ctrl_AddFixRectROI()
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                {
                    throw new Exception("窗体不含图像,无法添加ROI！");
                }

                if (OuterRefersh)
                    throw new Exception("外部刷新模式下,无法添加ROI！");
                ROIType = 3;
                ROINeedCreate = true;
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("添加矩形ROI失败！\r\n" + ex.Message);
            }
        }

        private void tsmi_LoadImage_Click(object sender, EventArgs e)
        {
            try
            {
                Response sd = Ctrl_LoadImage();
                if (!sd.IsSuccessful)
                    throw new Exception(sd.Msg);
                else
                    Ctrl_LoadedImage(sender, new EventArgs());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void tsmi_SaveWindow_Click(object sender, EventArgs e)
        {
            try
            {
                Response sd = Ctrl_SaveWindow();
                if (!sd.IsSuccessful)
                    throw new Exception(sd.Msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void tsmi_AddRect_Click(object sender, EventArgs e)
        {
            try
            {
                Response sd = Ctrl_AddRectROI();
                if (!sd.IsSuccessful)
                    throw new Exception(sd.Msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void tsmi_AddAcrRect_Click(object sender, EventArgs e)
        {
            try
            {
                Response sd = Ctrl_AddDirRectROI();
                if (!sd.IsSuccessful)
                    throw new Exception(sd.Msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void tsmi_AddCircle_Click(object sender, EventArgs e)
        {
            try
            {
                Response sd = Ctrl_AddCircleROI();
                if (!sd.IsSuccessful)
                    throw new Exception(sd.Msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void tsmi_AddFixRect_Click(object sender, EventArgs e)
        {
            try
            {
                Response sd = Ctrl_AddFixRectROI();
                if (!sd.IsSuccessful)
                    throw new Exception(sd.Msg);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void tsmi_DelSelectROI_Click(object sender, EventArgs e)
        {
            try
            {
                Response sd = Ctrl_DelSelectROI();
                if (!sd.IsSuccessful)
                    throw new Exception(sd.Msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void tsmi_DelAllROI_Click(object sender, EventArgs e)
        {
            try
            {
                Response sd = Ctrl_DelAllROI();
                if (!sd.IsSuccessful)
                    throw new Exception(sd.Msg);
                this.ClearAllObj();
                this.DisplayBuffer();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void tsmi_ROINum_Click(object sender, EventArgs e)
        {
            try
            {
                Response sd = Ctrl_ROIOrder();
                if (!sd.IsSuccessful)
                    throw new Exception(sd.Msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void tsmi_SaveSelectROI_Click(object sender, EventArgs e)
        {
            try
            {
                Response sd = Ctrl_SelectROISave();
                if (!sd.IsSuccessful)
                    throw new Exception(sd.Msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void tsmi_LoadROI_Click(object sender, EventArgs e)
        {
            try
            {
                Response sd = Ctrl_ROILoad();
                if (!sd.IsSuccessful)
                    throw new Exception(sd.Msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void btn_AddROI_Click(object sender, EventArgs e)
        {
            try
            {
                Response sd = Ctrl_AddRectROI();
                if (!sd.IsSuccessful)
                    throw new Exception(sd.Msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void btn_DelROI_Click(object sender, EventArgs e)
        {
            try
            {
                Response sd = Ctrl_DelAllROI();
                if (!sd.IsSuccessful)
                    throw new Exception(sd.Msg);
                this.ClearAllObj();
                this.DisplayBuffer();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void btn_FitImg_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                    throw new Exception("当前窗口无图片!");

                SetWindowSizeAndPosi(image_height, image_width);

                objListParam.Clear();
                for (int i = 0; i < objList.Count; i++)
                {
                    objList[i].Dispose();
                }
                objList.Clear();
                messageListParam.Clear();
                messageList.Clear();

                imageCanMove = false;

                RefreshDisp();
                Crtl_AdaptImage(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体图像自适应失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 添加ROI
        /// </summary>
        /// <param name="roiType">ROI类型</param>
        /// <param name="row">Row坐标</param>
        /// <param name="column">Column坐标</param>
        /// <returns></returns>
        public Response AddROI(int roiType, int row, int column)
        {
            try
            {
                ROI tempROI;
                switch (roiType)
                {
                    case 0:
                        tempROI = new ROIRectangle1();
                        tempROI.createROI(row, column); break;
                    case 1:
                        tempROI = new ROIRectangle2();
                        tempROI.createROI(row, column); break;
                    case 2:
                        tempROI = new ROICircle();
                        tempROI.createROI(row, column); break;
                    case 3:
                        tempROI = new ROIFixRectangle1();
                        tempROI.createROI(row, column); break;
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 添加ROI
        /// </summary>
        /// <param name="roiType">ROI类型</param>
        /// <param name="roiSize">ROI尺寸</param>
        /// <param name="row">Row坐标</param>
        /// <param name="column">Column坐标</param>
        /// <returns></returns>
        public Response AddROI(int roiType, HTuple roiSize, int row, int column)
        {
            try
            {
                ROI tempROI;
                switch (roiType)
                {
                    case 0:
                        //int roiWidth = ((int)roiSize[3].D - (int)roiSize[1].D) / 2;
                        //int roiHeight = ((int)roiSize[2].D - (int)roiSize[0].D) / 2;
                        tempROI = new ROIRectangle1();
                        tempROI.createROI(row, column); break;
                    case 1:
                        tempROI = new ROIRectangle2();
                        tempROI.createROI(row, column); break;
                    case 2:
                        tempROI = new ROICircle();
                        tempROI.createROI(row, column); break;
                    case 3:
                        tempROI = new ROIFixRectangle1();
                        tempROI.createROI(row, column); break;
                }
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 创建矩形ROI  len1:宽   len2:高
        /// </summary>
        public Response<ROI> Ctrl_CreateRectROI(double roiCenterY, double roiCenterX, double roiW, double roiH)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                {
                    throw new Exception("窗体不含图像,无法添加ROI！");
                }

                if (OuterRefersh)
                    throw new Exception("外部刷新模式下,无法添加ROI！");

                ROIRectangle1 temp_rect1 = new ROIRectangle1();
                temp_rect1.createROI(roiCenterX, roiCenterY, roiW, roiH);
                ROIList.Add(temp_rect1);
                curROIIndex = ROIList.Count - 1;
                RefreshDisp();

                return Response<ROI>.Ok(temp_rect1);
            }
            catch (Exception ex)
            {
                return Response<ROI>.Fail("添加矩形ROI失败！\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// 创建固定矩形ROI  len1:宽   len2:高
        /// </summary>
        public Response<ROI> Ctrl_CreateFixRectROI(double row, double col, double len1, double len2)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                {
                    throw new Exception("窗体不含图像,无法添加ROI！");
                }

                if (OuterRefersh)
                    throw new Exception("外部刷新模式下,无法添加ROI！");

                ROIFixRectangle1 temp_rect1 = new ROIFixRectangle1();
                temp_rect1.createROI(col, row, len1, len2);
                ROIList.Add(temp_rect1);
                curROIIndex = ROIList.Count - 1;
                RefreshDisp();

                return Response<ROI>.Ok(temp_rect1);
            }
            catch (Exception ex)
            {
                return Response<ROI>.Fail("添加固定矩形ROI失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 创建圆形ROI  radius:半径
        /// </summary>
        public Response<ROI> Ctrl_CreateCircleROI(double row, double col, double radius)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                {
                    throw new Exception("窗体不含图像,无法添加ROI！");
                }

                if (OuterRefersh)
                    throw new Exception("外部刷新模式下,无法添加ROI！");

                ROICircle temp_circle = new ROICircle();
                temp_circle.createROI(col, row, radius);
                ROIList.Add(temp_circle);
                curROIIndex = ROIList.Count - 1;
                RefreshDisp();

                return Response<ROI>.Ok(temp_circle);
            }
            catch (Exception ex)
            {
                return Response<ROI>.Fail("添加圆形ROI失败！\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 创建方向矩形ROI  len1:宽   len2:高  phi:矩形方向弧度
        /// </summary>
        public Response<ROI> Ctrl_CreateDirRectROI(double row, double col, double len1, double len2, double phi)
        {
            try
            {
                if (!isInitWindow)
                    throw new Exception("未初始化窗体！");

                if (VisionComFunc.ImageisEmpty(curImage).IsSuccessful)
                {
                    throw new Exception("窗体不含图像,无法添加ROI！");
                }

                if (OuterRefersh)
                    throw new Exception("外部刷新模式下,无法添加ROI！");

                ROIRectangle2 temp_rect2 = new ROIRectangle2();
                temp_rect2.createROI(col, row, len1, len2, phi);
                ROIList.Add(temp_rect2);
                curROIIndex = ROIList.Count - 1;
                RefreshDisp();

                return Response<ROI>.Ok(temp_rect2);
            }
            catch (Exception ex)
            {
                return Response<ROI>.Fail("添加方向矩形ROI失败！\r\n" + ex.Message);
            }
        }


    }
}
