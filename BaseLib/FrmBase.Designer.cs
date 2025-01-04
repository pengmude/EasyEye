
namespace SmartLib
{
    partial class FrmBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBase));
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btn_title = new System.Windows.Forms.Panel();
            this.lab_curpro = new System.Windows.Forms.Label();
            this.ToolsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btn_close = new System.Windows.Forms.PictureBox();
            this.btn_max = new System.Windows.Forms.PictureBox();
            this.btn_Min = new System.Windows.Forms.PictureBox();
            this.btn_mainLogo = new System.Windows.Forms.PictureBox();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.btn_title.SuspendLayout();
            this.ToolsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_mainLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 641);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1024, 25);
            this.panelBottom.TabIndex = 53;
            // 
            // btn_title
            // 
            this.btn_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.btn_title.Controls.Add(this.lab_curpro);
            this.btn_title.Controls.Add(this.ToolsPanel);
            this.btn_title.Controls.Add(this.btn_mainLogo);
            this.btn_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_title.Location = new System.Drawing.Point(0, 0);
            this.btn_title.Margin = new System.Windows.Forms.Padding(5);
            this.btn_title.Name = "btn_title";
            this.btn_title.Size = new System.Drawing.Size(1024, 35);
            this.btn_title.TabIndex = 54;
            this.btn_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_title_MouseDown);
            this.btn_title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_title_MouseMove);
            // 
            // lab_curpro
            // 
            this.lab_curpro.AutoSize = true;
            this.lab_curpro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_curpro.ForeColor = System.Drawing.Color.White;
            this.lab_curpro.Location = new System.Drawing.Point(37, 7);
            this.lab_curpro.Name = "lab_curpro";
            this.lab_curpro.Size = new System.Drawing.Size(64, 20);
            this.lab_curpro.TabIndex = 7;
            this.lab_curpro.Text = "RXSVP";
            // 
            // ToolsPanel
            // 
            this.ToolsPanel.AutoSize = true;
            this.ToolsPanel.ColumnCount = 4;
            this.ToolsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ToolsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ToolsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ToolsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ToolsPanel.Controls.Add(this.btn_close, 3, 0);
            this.ToolsPanel.Controls.Add(this.btn_max, 2, 0);
            this.ToolsPanel.Controls.Add(this.btn_Min, 1, 0);
            this.ToolsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ToolsPanel.Location = new System.Drawing.Point(884, 0);
            this.ToolsPanel.Name = "ToolsPanel";
            this.ToolsPanel.RowCount = 1;
            this.ToolsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ToolsPanel.Size = new System.Drawing.Size(140, 35);
            this.ToolsPanel.TabIndex = 8;
            // 
            // btn_close
            // 
            this.btn_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_close.Image = ((System.Drawing.Image)(resources.GetObject("btn_close.Image")));
            this.btn_close.Location = new System.Drawing.Point(105, 0);
            this.btn_close.Margin = new System.Windows.Forms.Padding(0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Padding = new System.Windows.Forms.Padding(5);
            this.btn_close.Size = new System.Drawing.Size(35, 35);
            this.btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_close.TabIndex = 15;
            this.btn_close.TabStop = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_max
            // 
            this.btn_max.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_max.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_max.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_max.Image = ((System.Drawing.Image)(resources.GetObject("btn_max.Image")));
            this.btn_max.Location = new System.Drawing.Point(70, 0);
            this.btn_max.Margin = new System.Windows.Forms.Padding(0);
            this.btn_max.Name = "btn_max";
            this.btn_max.Padding = new System.Windows.Forms.Padding(10);
            this.btn_max.Size = new System.Drawing.Size(35, 35);
            this.btn_max.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_max.TabIndex = 14;
            this.btn_max.TabStop = false;
            this.btn_max.Click += new System.EventHandler(this.btn_max_Click);
            // 
            // btn_Min
            // 
            this.btn_Min.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Min.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Min.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Min.Image = ((System.Drawing.Image)(resources.GetObject("btn_Min.Image")));
            this.btn_Min.Location = new System.Drawing.Point(35, 0);
            this.btn_Min.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Min.Name = "btn_Min";
            this.btn_Min.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btn_Min.Size = new System.Drawing.Size(35, 35);
            this.btn_Min.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_Min.TabIndex = 13;
            this.btn_Min.TabStop = false;
            this.btn_Min.Click += new System.EventHandler(this.btn_min_Click);
            // 
            // btn_mainLogo
            // 
            this.btn_mainLogo.BackColor = System.Drawing.Color.Transparent;
            this.btn_mainLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_mainLogo.Image = ((System.Drawing.Image)(resources.GetObject("btn_mainLogo.Image")));
            this.btn_mainLogo.Location = new System.Drawing.Point(0, 0);
            this.btn_mainLogo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_mainLogo.Name = "btn_mainLogo";
            this.btn_mainLogo.Size = new System.Drawing.Size(28, 35);
            this.btn_mainLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_mainLogo.TabIndex = 6;
            this.btn_mainLogo.TabStop = false;
            // 
            // MainPanel
            // 
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 35);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1024, 606);
            this.MainPanel.TabIndex = 57;
            // 
            // FrmBase
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1024, 666);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.btn_title);
            this.Controls.Add(this.panelBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmBaseSize_Load);
            this.btn_title.ResumeLayout(false);
            this.btn_title.PerformLayout();
            this.ToolsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_mainLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        /// <summary>
        /// 窗体标题栏
        /// </summary>
        protected System.Windows.Forms.Panel btn_title;
        /// <summary>
        /// 窗体标题栏显示内容
        /// </summary>
        protected System.Windows.Forms.Label lab_curpro;
        /// <summary>
        /// 窗体工具栏
        /// </summary>
        public System.Windows.Forms.TableLayoutPanel ToolsPanel;
        /// <summary>
        /// 关闭按钮
        /// </summary>
        protected System.Windows.Forms.PictureBox btn_close;
        /// <summary>
        /// 最大化按钮
        /// </summary>
        protected System.Windows.Forms.PictureBox btn_max;
        /// <summary>
        /// 最小化按钮
        /// </summary>
        protected System.Windows.Forms.PictureBox btn_Min;
        /// <summary>
        /// Logo按钮
        /// </summary>
        protected System.Windows.Forms.PictureBox btn_mainLogo;
        /// <summary>
        /// 主窗体控件
        /// </summary>
        public System.Windows.Forms.Panel MainPanel;
        /// <summary>
        /// 底部工具
        /// </summary>
        protected System.Windows.Forms.Panel panelBottom;
    }
}