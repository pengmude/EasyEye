
namespace SmartVEye
{
    partial class FrmMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.tlp_MainContainer = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_Menu = new System.Windows.Forms.Panel();
            this.btnCancelAlarm = new System.Windows.Forms.Button();
            this.lbl_ROIStep = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_ROIStep = new System.Windows.Forms.TrackBar();
            this.btn_FrmTestStop = new System.Windows.Forms.Button();
            this.btn_TestAll = new System.Windows.Forms.Button();
            this.btn_TestLoad = new System.Windows.Forms.Button();
            this.btn_DisConnAll = new System.Windows.Forms.Button();
            this.btn_ConnAll = new System.Windows.Forms.Button();
            this.btn_TrainModeAll = new System.Windows.Forms.Button();
            this.btn_LearnAll = new System.Windows.Forms.Button();
            this.btn_ClearRecord = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tb_InfoBox = new System.Windows.Forms.Label();
            this.lbl_Setting = new System.Windows.Forms.Label();
            this.lbl_Phone = new System.Windows.Forms.LinkLabel();
            this.lbl_CurVersion = new System.Windows.Forms.LinkLabel();
            this.lbl_WinMode = new System.Windows.Forms.Label();
            this.pnl_Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_ROIStep)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_MainContainer
            // 
            this.tlp_MainContainer.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlp_MainContainer.ColumnCount = 3;
            this.tlp_MainContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00015F));
            this.tlp_MainContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00016F));
            this.tlp_MainContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00016F));
            this.tlp_MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_MainContainer.Enabled = false;
            this.tlp_MainContainer.Location = new System.Drawing.Point(0, 63);
            this.tlp_MainContainer.Name = "tlp_MainContainer";
            this.tlp_MainContainer.RowCount = 2;
            this.tlp_MainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_MainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_MainContainer.Size = new System.Drawing.Size(1186, 632);
            this.tlp_MainContainer.TabIndex = 2;
            // 
            // pnl_Menu
            // 
            this.pnl_Menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnl_Menu.Controls.Add(this.btnCancelAlarm);
            this.pnl_Menu.Controls.Add(this.lbl_ROIStep);
            this.pnl_Menu.Controls.Add(this.label1);
            this.pnl_Menu.Controls.Add(this.tb_ROIStep);
            this.pnl_Menu.Controls.Add(this.btn_FrmTestStop);
            this.pnl_Menu.Controls.Add(this.btn_TestAll);
            this.pnl_Menu.Controls.Add(this.btn_TestLoad);
            this.pnl_Menu.Controls.Add(this.btn_DisConnAll);
            this.pnl_Menu.Controls.Add(this.btn_ConnAll);
            this.pnl_Menu.Controls.Add(this.btn_TrainModeAll);
            this.pnl_Menu.Controls.Add(this.btn_LearnAll);
            this.pnl_Menu.Controls.Add(this.btn_ClearRecord);
            this.pnl_Menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Menu.Enabled = false;
            this.pnl_Menu.Location = new System.Drawing.Point(0, 0);
            this.pnl_Menu.Name = "pnl_Menu";
            this.pnl_Menu.Size = new System.Drawing.Size(1186, 63);
            this.pnl_Menu.TabIndex = 3;
            // 
            // btnCancelAlarm
            // 
            this.btnCancelAlarm.BackColor = System.Drawing.Color.Aqua;
            this.btnCancelAlarm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelAlarm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancelAlarm.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.btnCancelAlarm.Location = new System.Drawing.Point(1065, 0);
            this.btnCancelAlarm.Name = "btnCancelAlarm";
            this.btnCancelAlarm.Size = new System.Drawing.Size(121, 63);
            this.btnCancelAlarm.TabIndex = 11;
            this.btnCancelAlarm.Text = "取消告警";
            this.btnCancelAlarm.UseVisualStyleBackColor = false;
            this.btnCancelAlarm.Click += new System.EventHandler(this.btnCancelAlarm_Click);
            // 
            // lbl_ROIStep
            // 
            this.lbl_ROIStep.AutoSize = true;
            this.lbl_ROIStep.Font = new System.Drawing.Font("宋体", 18F);
            this.lbl_ROIStep.Location = new System.Drawing.Point(724, 5);
            this.lbl_ROIStep.Name = "lbl_ROIStep";
            this.lbl_ROIStep.Size = new System.Drawing.Size(34, 24);
            this.lbl_ROIStep.TabIndex = 9;
            this.lbl_ROIStep.Text = "50";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F);
            this.label1.Location = new System.Drawing.Point(624, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 24);
            this.label1.TabIndex = 10;
            this.label1.Text = "ROI尺寸";
            // 
            // tb_ROIStep
            // 
            this.tb_ROIStep.AutoSize = false;
            this.tb_ROIStep.Location = new System.Drawing.Point(620, 33);
            this.tb_ROIStep.Maximum = 100;
            this.tb_ROIStep.Minimum = 2;
            this.tb_ROIStep.Name = "tb_ROIStep";
            this.tb_ROIStep.Size = new System.Drawing.Size(145, 28);
            this.tb_ROIStep.TabIndex = 8;
            this.tb_ROIStep.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tb_ROIStep.Value = 50;
            this.tb_ROIStep.Scroll += new System.EventHandler(this.tb_ROIStep_Scroll);
            // 
            // btn_FrmTestStop
            // 
            this.btn_FrmTestStop.Location = new System.Drawing.Point(856, 21);
            this.btn_FrmTestStop.Name = "btn_FrmTestStop";
            this.btn_FrmTestStop.Size = new System.Drawing.Size(75, 26);
            this.btn_FrmTestStop.TabIndex = 2;
            this.btn_FrmTestStop.Text = "停止测试";
            this.btn_FrmTestStop.UseVisualStyleBackColor = true;
            this.btn_FrmTestStop.Visible = false;
            this.btn_FrmTestStop.Click += new System.EventHandler(this.btn_FrmTestStop_Click);
            // 
            // btn_TestAll
            // 
            this.btn_TestAll.Location = new System.Drawing.Point(764, 28);
            this.btn_TestAll.Name = "btn_TestAll";
            this.btn_TestAll.Size = new System.Drawing.Size(75, 26);
            this.btn_TestAll.TabIndex = 2;
            this.btn_TestAll.Text = "连续测试";
            this.btn_TestAll.UseVisualStyleBackColor = true;
            this.btn_TestAll.Visible = false;
            this.btn_TestAll.Click += new System.EventHandler(this.btn_TestAll_Click);
            // 
            // btn_TestLoad
            // 
            this.btn_TestLoad.Location = new System.Drawing.Point(764, 0);
            this.btn_TestLoad.Name = "btn_TestLoad";
            this.btn_TestLoad.Size = new System.Drawing.Size(75, 26);
            this.btn_TestLoad.TabIndex = 1;
            this.btn_TestLoad.Text = "加载图像";
            this.btn_TestLoad.UseVisualStyleBackColor = true;
            this.btn_TestLoad.Visible = false;
            this.btn_TestLoad.Click += new System.EventHandler(this.btn_TestLoad_Click);
            // 
            // btn_DisConnAll
            // 
            this.btn_DisConnAll.BackColor = System.Drawing.Color.Aqua;
            this.btn_DisConnAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DisConnAll.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.btn_DisConnAll.Location = new System.Drawing.Point(496, 3);
            this.btn_DisConnAll.Name = "btn_DisConnAll";
            this.btn_DisConnAll.Size = new System.Drawing.Size(121, 54);
            this.btn_DisConnAll.TabIndex = 0;
            this.btn_DisConnAll.Text = "一键脱机";
            this.btn_DisConnAll.UseVisualStyleBackColor = false;
            this.btn_DisConnAll.Click += new System.EventHandler(this.btn_DisConnAll_Click);
            this.btn_DisConnAll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseDown);
            this.btn_DisConnAll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseUp);
            // 
            // btn_ConnAll
            // 
            this.btn_ConnAll.BackColor = System.Drawing.Color.Aqua;
            this.btn_ConnAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ConnAll.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.btn_ConnAll.Location = new System.Drawing.Point(373, 3);
            this.btn_ConnAll.Name = "btn_ConnAll";
            this.btn_ConnAll.Size = new System.Drawing.Size(121, 54);
            this.btn_ConnAll.TabIndex = 0;
            this.btn_ConnAll.Text = "一键联机";
            this.btn_ConnAll.UseVisualStyleBackColor = false;
            this.btn_ConnAll.Click += new System.EventHandler(this.btn_ConnAll_Click);
            this.btn_ConnAll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseDown);
            this.btn_ConnAll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseUp);
            // 
            // btn_TrainModeAll
            // 
            this.btn_TrainModeAll.BackColor = System.Drawing.Color.Aqua;
            this.btn_TrainModeAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_TrainModeAll.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.btn_TrainModeAll.Location = new System.Drawing.Point(127, 3);
            this.btn_TrainModeAll.Name = "btn_TrainModeAll";
            this.btn_TrainModeAll.Size = new System.Drawing.Size(121, 54);
            this.btn_TrainModeAll.TabIndex = 0;
            this.btn_TrainModeAll.Text = "一键建模";
            this.btn_TrainModeAll.UseVisualStyleBackColor = false;
            this.btn_TrainModeAll.Click += new System.EventHandler(this.btn_TrainModeAll_Click);
            this.btn_TrainModeAll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseDown);
            this.btn_TrainModeAll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseUp);
            // 
            // btn_LearnAll
            // 
            this.btn_LearnAll.BackColor = System.Drawing.Color.Aqua;
            this.btn_LearnAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_LearnAll.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.btn_LearnAll.Location = new System.Drawing.Point(250, 3);
            this.btn_LearnAll.Name = "btn_LearnAll";
            this.btn_LearnAll.Size = new System.Drawing.Size(121, 54);
            this.btn_LearnAll.TabIndex = 0;
            this.btn_LearnAll.Text = "一键学习";
            this.btn_LearnAll.UseVisualStyleBackColor = false;
            this.btn_LearnAll.Click += new System.EventHandler(this.btn_LearnAll_Click);
            this.btn_LearnAll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseDown);
            this.btn_LearnAll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseUp);
            // 
            // btn_ClearRecord
            // 
            this.btn_ClearRecord.BackColor = System.Drawing.Color.Aqua;
            this.btn_ClearRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ClearRecord.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.btn_ClearRecord.Location = new System.Drawing.Point(4, 3);
            this.btn_ClearRecord.Name = "btn_ClearRecord";
            this.btn_ClearRecord.Size = new System.Drawing.Size(121, 54);
            this.btn_ClearRecord.TabIndex = 0;
            this.btn_ClearRecord.Text = "清除计数";
            this.btn_ClearRecord.UseVisualStyleBackColor = false;
            this.btn_ClearRecord.Click += new System.EventHandler(this.btn_ClearRecord_Click);
            this.btn_ClearRecord.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseDown);
            this.btn_ClearRecord.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tb_InfoBox);
            this.panel2.Controls.Add(this.lbl_Setting);
            this.panel2.Controls.Add(this.lbl_Phone);
            this.panel2.Controls.Add(this.lbl_CurVersion);
            this.panel2.Controls.Add(this.lbl_WinMode);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 695);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1186, 16);
            this.panel2.TabIndex = 4;
            // 
            // tb_InfoBox
            // 
            this.tb_InfoBox.Location = new System.Drawing.Point(0, 3);
            this.tb_InfoBox.Name = "tb_InfoBox";
            this.tb_InfoBox.Size = new System.Drawing.Size(629, 13);
            this.tb_InfoBox.TabIndex = 0;
            this.tb_InfoBox.Text = "          ";
            // 
            // lbl_Setting
            // 
            this.lbl_Setting.AutoSize = true;
            this.lbl_Setting.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_Setting.Location = new System.Drawing.Point(899, 0);
            this.lbl_Setting.Name = "lbl_Setting";
            this.lbl_Setting.Size = new System.Drawing.Size(77, 14);
            this.lbl_Setting.TabIndex = 6;
            this.lbl_Setting.Text = "          ";
            this.lbl_Setting.DoubleClick += new System.EventHandler(this.lbl_Setting_DoubleClick);
            // 
            // lbl_Phone
            // 
            this.lbl_Phone.AutoSize = true;
            this.lbl_Phone.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_Phone.Location = new System.Drawing.Point(976, 0);
            this.lbl_Phone.Name = "lbl_Phone";
            this.lbl_Phone.Size = new System.Drawing.Size(63, 14);
            this.lbl_Phone.TabIndex = 5;
            this.lbl_Phone.TabStop = true;
            this.lbl_Phone.Text = "        ";
            // 
            // lbl_CurVersion
            // 
            this.lbl_CurVersion.AutoSize = true;
            this.lbl_CurVersion.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_CurVersion.LinkColor = System.Drawing.Color.Blue;
            this.lbl_CurVersion.Location = new System.Drawing.Point(1039, 0);
            this.lbl_CurVersion.Name = "lbl_CurVersion";
            this.lbl_CurVersion.Size = new System.Drawing.Size(91, 14);
            this.lbl_CurVersion.TabIndex = 4;
            this.lbl_CurVersion.TabStop = true;
            this.lbl_CurVersion.Text = "Ver: 1.0.0.0";
            this.lbl_CurVersion.DoubleClick += new System.EventHandler(this.lbl_CurVersion_DoubleClick);
            // 
            // lbl_WinMode
            // 
            this.lbl_WinMode.AutoSize = true;
            this.lbl_WinMode.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_WinMode.ForeColor = System.Drawing.Color.Blue;
            this.lbl_WinMode.Location = new System.Drawing.Point(1130, 0);
            this.lbl_WinMode.Name = "lbl_WinMode";
            this.lbl_WinMode.Size = new System.Drawing.Size(56, 14);
            this.lbl_WinMode.TabIndex = 7;
            this.lbl_WinMode.Text = "  Win:1";
            this.lbl_WinMode.DoubleClick += new System.EventHandler(this.lbl_WinMode_DoubleClick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 711);
            this.Controls.Add(this.tlp_MainContainer);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnl_Menu);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.Text = "SmartEye";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.pnl_Menu.ResumeLayout(false);
            this.pnl_Menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_ROIStep)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlp_MainContainer;
        private System.Windows.Forms.Panel pnl_Menu;
        private System.Windows.Forms.Button btn_ClearRecord;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label tb_InfoBox;
        private System.Windows.Forms.LinkLabel lbl_CurVersion;
        private System.Windows.Forms.Button btn_LearnAll;
        private System.Windows.Forms.Button btn_ConnAll;
        private System.Windows.Forms.Button btn_DisConnAll;
        private System.Windows.Forms.Button btn_TestLoad;
        private System.Windows.Forms.Button btn_TestAll;
        private System.Windows.Forms.LinkLabel lbl_Phone;
        private System.Windows.Forms.Label lbl_Setting;
        private System.Windows.Forms.Label lbl_ROIStep;
        private System.Windows.Forms.TrackBar tb_ROIStep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_WinMode;
        private System.Windows.Forms.Button btn_FrmTestStop;
        private System.Windows.Forms.Button btn_TrainModeAll;
        private System.Windows.Forms.Button btnCancelAlarm;
    }
}

