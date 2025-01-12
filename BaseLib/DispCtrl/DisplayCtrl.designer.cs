namespace SmartLib
{
    partial class DisplayCtrl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tsmi_ClearWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_LoadImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_SaveImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_SaveWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_AdaptImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_AddRect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_AddAcrRect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_AddCircle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_AddFixRect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_DelSelectROI = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_DelAllROI = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ROINum = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_SaveSelectROI = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_LoadROI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_CrossLine = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_AutoFocus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_LargeFrom = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ComInfoEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.viewPort = new HalconDotNet.HWindowControl();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btn_AddROI = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_DelROI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_FitImg = new System.Windows.Forms.ToolStripMenuItem();
            this.lab_ImageSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.lab_MousePlace = new System.Windows.Forms.ToolStripStatusLabel();
            this.lab_MouseGray = new System.Windows.Forms.ToolStripStatusLabel();
            this.lab_MouseQual = new System.Windows.Forms.ToolStripStatusLabel();
            this.stip_ComInfo = new System.Windows.Forms.StatusStrip();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.stip_ComInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsmi_ClearWindow
            // 
            this.tsmi_ClearWindow.Name = "tsmi_ClearWindow";
            this.tsmi_ClearWindow.Size = new System.Drawing.Size(160, 22);
            this.tsmi_ClearWindow.Text = "清空窗口";
            this.tsmi_ClearWindow.Click += new System.EventHandler(this.tsmi_ClearWindow_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_LoadImage,
            this.toolStripSeparator2,
            this.tsmi_SaveImage,
            this.tsmi_SaveWindow,
            this.tsmi_AdaptImage,
            this.tsmi_ClearWindow,
            this.toolStripSeparator1,
            this.toolStripMenuItem1,
            this.tsmi_DelSelectROI,
            this.tsmi_DelAllROI,
            this.tsmi_ROINum,
            this.toolStripMenuItem2,
            this.toolStripSeparator3,
            this.tsmi_CrossLine,
            this.tsmi_AutoFocus,
            this.tsmi_LargeFrom,
            this.tsmi_ComInfoEnable});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 330);
            // 
            // tsmi_LoadImage
            // 
            this.tsmi_LoadImage.Name = "tsmi_LoadImage";
            this.tsmi_LoadImage.Size = new System.Drawing.Size(160, 22);
            this.tsmi_LoadImage.Text = "加载图像";
            this.tsmi_LoadImage.Click += new System.EventHandler(this.tsmi_LoadImage_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // tsmi_SaveImage
            // 
            this.tsmi_SaveImage.Name = "tsmi_SaveImage";
            this.tsmi_SaveImage.Size = new System.Drawing.Size(160, 22);
            this.tsmi_SaveImage.Text = "保存图像";
            this.tsmi_SaveImage.Click += new System.EventHandler(this.tsmi_SaveImage_Click);
            // 
            // tsmi_SaveWindow
            // 
            this.tsmi_SaveWindow.Name = "tsmi_SaveWindow";
            this.tsmi_SaveWindow.Size = new System.Drawing.Size(160, 22);
            this.tsmi_SaveWindow.Text = "保存缩略图";
            this.tsmi_SaveWindow.Click += new System.EventHandler(this.tsmi_SaveWindow_Click);
            // 
            // tsmi_AdaptImage
            // 
            this.tsmi_AdaptImage.Name = "tsmi_AdaptImage";
            this.tsmi_AdaptImage.Size = new System.Drawing.Size(160, 22);
            this.tsmi_AdaptImage.Text = "自适应图像";
            this.tsmi_AdaptImage.Click += new System.EventHandler(this.tsmi_AdaptImage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_AddRect,
            this.tsmi_AddAcrRect,
            this.tsmi_AddCircle,
            this.tsmi_AddFixRect});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem1.Text = "添加ROI";
            // 
            // tsmi_AddRect
            // 
            this.tsmi_AddRect.Name = "tsmi_AddRect";
            this.tsmi_AddRect.Size = new System.Drawing.Size(136, 22);
            this.tsmi_AddRect.Text = "矩形";
            this.tsmi_AddRect.Click += new System.EventHandler(this.tsmi_AddRect_Click);
            // 
            // tsmi_AddAcrRect
            // 
            this.tsmi_AddAcrRect.Name = "tsmi_AddAcrRect";
            this.tsmi_AddAcrRect.Size = new System.Drawing.Size(136, 22);
            this.tsmi_AddAcrRect.Text = "方向矩形";
            this.tsmi_AddAcrRect.Click += new System.EventHandler(this.tsmi_AddAcrRect_Click);
            // 
            // tsmi_AddCircle
            // 
            this.tsmi_AddCircle.Name = "tsmi_AddCircle";
            this.tsmi_AddCircle.Size = new System.Drawing.Size(136, 22);
            this.tsmi_AddCircle.Text = "圆形";
            this.tsmi_AddCircle.Click += new System.EventHandler(this.tsmi_AddCircle_Click);
            // 
            // tsmi_AddFixRect
            // 
            this.tsmi_AddFixRect.Name = "tsmi_AddFixRect";
            this.tsmi_AddFixRect.Size = new System.Drawing.Size(136, 22);
            this.tsmi_AddFixRect.Text = "不可变矩形";
            this.tsmi_AddFixRect.Click += new System.EventHandler(this.tsmi_AddFixRect_Click);
            // 
            // tsmi_DelSelectROI
            // 
            this.tsmi_DelSelectROI.Name = "tsmi_DelSelectROI";
            this.tsmi_DelSelectROI.Size = new System.Drawing.Size(160, 22);
            this.tsmi_DelSelectROI.Text = "删除选中ROI";
            this.tsmi_DelSelectROI.Click += new System.EventHandler(this.tsmi_DelSelectROI_Click);
            // 
            // tsmi_DelAllROI
            // 
            this.tsmi_DelAllROI.Name = "tsmi_DelAllROI";
            this.tsmi_DelAllROI.Size = new System.Drawing.Size(160, 22);
            this.tsmi_DelAllROI.Text = "删除所有ROI";
            this.tsmi_DelAllROI.Click += new System.EventHandler(this.tsmi_DelAllROI_Click);
            // 
            // tsmi_ROINum
            // 
            this.tsmi_ROINum.Name = "tsmi_ROINum";
            this.tsmi_ROINum.Size = new System.Drawing.Size(160, 22);
            this.tsmi_ROINum.Text = "显示ROI编号";
            this.tsmi_ROINum.Click += new System.EventHandler(this.tsmi_ROINum_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_SaveSelectROI,
            this.tsmi_LoadROI});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem2.Text = "ROI读写";
            // 
            // tsmi_SaveSelectROI
            // 
            this.tsmi_SaveSelectROI.Name = "tsmi_SaveSelectROI";
            this.tsmi_SaveSelectROI.Size = new System.Drawing.Size(122, 22);
            this.tsmi_SaveSelectROI.Text = "保存ROI";
            this.tsmi_SaveSelectROI.Click += new System.EventHandler(this.tsmi_SaveSelectROI_Click);
            // 
            // tsmi_LoadROI
            // 
            this.tsmi_LoadROI.Name = "tsmi_LoadROI";
            this.tsmi_LoadROI.Size = new System.Drawing.Size(122, 22);
            this.tsmi_LoadROI.Text = "读取ROI";
            this.tsmi_LoadROI.Click += new System.EventHandler(this.tsmi_LoadROI_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
            // 
            // tsmi_CrossLine
            // 
            this.tsmi_CrossLine.Name = "tsmi_CrossLine";
            this.tsmi_CrossLine.Size = new System.Drawing.Size(160, 22);
            this.tsmi_CrossLine.Text = "显示中心线";
            this.tsmi_CrossLine.Click += new System.EventHandler(this.tsmi_CrossLine_Click);
            // 
            // tsmi_AutoFocus
            // 
            this.tsmi_AutoFocus.Name = "tsmi_AutoFocus";
            this.tsmi_AutoFocus.Size = new System.Drawing.Size(160, 22);
            this.tsmi_AutoFocus.Text = "辅助对焦关";
            this.tsmi_AutoFocus.Visible = false;
            this.tsmi_AutoFocus.Click += new System.EventHandler(this.tsmi_AutoFocus_Click);
            // 
            // tsmi_LargeFrom
            // 
            this.tsmi_LargeFrom.Name = "tsmi_LargeFrom";
            this.tsmi_LargeFrom.Size = new System.Drawing.Size(160, 22);
            this.tsmi_LargeFrom.Text = "窗体放大";
            this.tsmi_LargeFrom.Click += new System.EventHandler(this.tsmi_LargeFrom_Click);
            // 
            // tsmi_ComInfoEnable
            // 
            this.tsmi_ComInfoEnable.Name = "tsmi_ComInfoEnable";
            this.tsmi_ComInfoEnable.Size = new System.Drawing.Size(160, 22);
            this.tsmi_ComInfoEnable.Text = "显示常规状态栏";
            this.tsmi_ComInfoEnable.Click += new System.EventHandler(this.tsmi_ComInfoEnable_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.viewPort);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(605, 530);
            this.panel1.TabIndex = 12;
            // 
            // viewPort
            // 
            this.viewPort.BackColor = System.Drawing.Color.Black;
            this.viewPort.BorderColor = System.Drawing.Color.Black;
            this.viewPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPort.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.viewPort.Location = new System.Drawing.Point(0, 0);
            this.viewPort.Margin = new System.Windows.Forms.Padding(0);
            this.viewPort.Name = "viewPort";
            this.viewPort.Size = new System.Drawing.Size(605, 530);
            this.viewPort.TabIndex = 1;
            this.viewPort.WindowSize = new System.Drawing.Size(605, 530);
            this.viewPort.HMouseMove += new HalconDotNet.HMouseEventHandler(this.viewPort_HMouseMove);
            this.viewPort.HMouseDown += new HalconDotNet.HMouseEventHandler(this.viewPort_HMouseDown);
            this.viewPort.HMouseUp += new HalconDotNet.HMouseEventHandler(this.viewPort_HMouseUp);
            this.viewPort.Resize += new System.EventHandler(this.viewPort_Resize);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_AddROI,
            this.btn_DelROI,
            this.toolStripSeparator5,
            this.btn_FitImg});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(143, 88);
            // 
            // btn_AddROI
            // 
            this.btn_AddROI.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.btn_AddROI.Name = "btn_AddROI";
            this.btn_AddROI.Size = new System.Drawing.Size(142, 26);
            this.btn_AddROI.Text = "添加区域";
            this.btn_AddROI.Click += new System.EventHandler(this.btn_AddROI_Click);
            // 
            // btn_DelROI
            // 
            this.btn_DelROI.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.btn_DelROI.Name = "btn_DelROI";
            this.btn_DelROI.Size = new System.Drawing.Size(142, 26);
            this.btn_DelROI.Text = "删除区域";
            this.btn_DelROI.Click += new System.EventHandler(this.btn_DelROI_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(139, 6);
            // 
            // btn_FitImg
            // 
            this.btn_FitImg.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.btn_FitImg.Name = "btn_FitImg";
            this.btn_FitImg.Size = new System.Drawing.Size(142, 26);
            this.btn_FitImg.Text = "适应图像";
            this.btn_FitImg.Click += new System.EventHandler(this.btn_FitImg_Click);
            // 
            // lab_ImageSize
            // 
            this.lab_ImageSize.Name = "lab_ImageSize";
            this.lab_ImageSize.Size = new System.Drawing.Size(147, 17);
            this.lab_ImageSize.Spring = true;
            // 
            // lab_MousePlace
            // 
            this.lab_MousePlace.Name = "lab_MousePlace";
            this.lab_MousePlace.Size = new System.Drawing.Size(147, 17);
            this.lab_MousePlace.Spring = true;
            // 
            // lab_MouseGray
            // 
            this.lab_MouseGray.Name = "lab_MouseGray";
            this.lab_MouseGray.Size = new System.Drawing.Size(147, 17);
            this.lab_MouseGray.Spring = true;
            // 
            // lab_MouseQual
            // 
            this.lab_MouseQual.Name = "lab_MouseQual";
            this.lab_MouseQual.Size = new System.Drawing.Size(147, 17);
            this.lab_MouseQual.Spring = true;
            // 
            // stip_ComInfo
            // 
            this.stip_ComInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lab_ImageSize,
            this.lab_MousePlace,
            this.lab_MouseGray,
            this.lab_MouseQual});
            this.stip_ComInfo.Location = new System.Drawing.Point(0, 508);
            this.stip_ComInfo.Name = "stip_ComInfo";
            this.stip_ComInfo.Size = new System.Drawing.Size(605, 22);
            this.stip_ComInfo.TabIndex = 10;
            this.stip_ComInfo.Text = "statusStrip1";
            this.stip_ComInfo.Visible = false;
            // 
            // DisplayCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stip_ComInfo);
            this.DoubleBuffered = true;
            this.Name = "DisplayCtrl";
            this.Size = new System.Drawing.Size(605, 530);
            this.Load += new System.EventHandler(this.DispControl_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.stip_ComInfo.ResumeLayout(false);
            this.stip_ComInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem tsmi_ClearWindow;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_SaveImage;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AdaptImage;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AutoFocus;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ComInfoEnable;
        private System.Windows.Forms.Panel panel1;
        private HalconDotNet.HWindowControl viewPort;
        private System.Windows.Forms.ToolStripStatusLabel lab_ImageSize;
        private System.Windows.Forms.ToolStripStatusLabel lab_MousePlace;
        private System.Windows.Forms.ToolStripStatusLabel lab_MouseGray;
        private System.Windows.Forms.ToolStripStatusLabel lab_MouseQual;
        private System.Windows.Forms.StatusStrip stip_ComInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_LargeFrom;
        private System.Windows.Forms.ToolStripMenuItem tsmi_CrossLine;
        private System.Windows.Forms.ToolStripMenuItem tsmi_LoadImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmi_SaveWindow;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AddRect;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AddAcrRect;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AddCircle;
        private System.Windows.Forms.ToolStripMenuItem tsmi_DelSelectROI;
        private System.Windows.Forms.ToolStripMenuItem tsmi_DelAllROI;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ROINum;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AddFixRect;
        private System.Windows.Forms.ToolStripMenuItem tsmi_SaveSelectROI;
        private System.Windows.Forms.ToolStripMenuItem tsmi_LoadROI;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem btn_AddROI;
        private System.Windows.Forms.ToolStripMenuItem btn_DelROI;
        private System.Windows.Forms.ToolStripMenuItem btn_FitImg;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}
