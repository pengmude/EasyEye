
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
            this.tableLayoutPanel_Bottom = new System.Windows.Forms.TableLayoutPanel();
            this.tb_InfoBox = new System.Windows.Forms.Label();
            this.lbl_WinMode = new System.Windows.Forms.Label();
            this.lbl_Phone = new System.Windows.Forms.LinkLabel();
            this.lbl_CurVersion = new System.Windows.Forms.LinkLabel();
            this.pnl_Menu = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_ClearRecord = new System.Windows.Forms.Button();
            this.comboBox_CamList = new System.Windows.Forms.ComboBox();
            this.button_DelaySub = new System.Windows.Forms.Button();
            this.btn_TrainModeAll = new System.Windows.Forms.Button();
            this.btn_ConnAll = new System.Windows.Forms.Button();
            this.btn_LearnAll = new System.Windows.Forms.Button();
            this.btn_DisConnAll = new System.Windows.Forms.Button();
            this.tb_ROISize = new System.Windows.Forms.TrackBar();
            this.lbl_ROISize = new System.Windows.Forms.Label();
            this.btnCancelAlarm = new System.Windows.Forms.Button();
            this.label_ROITitle = new System.Windows.Forms.Label();
            this.btn_TestLoad = new System.Windows.Forms.Button();
            this.btn_TestAll = new System.Windows.Forms.Button();
            this.btn_FrmTestStop = new System.Windows.Forms.Button();
            this.btn_Settings = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_DelayPlus = new System.Windows.Forms.Button();
            this.label_TriggerDelay = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tlp_MainContainer.SuspendLayout();
            this.tableLayoutPanel_Bottom.SuspendLayout();
            this.pnl_Menu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_ROISize)).BeginInit();
            this.SuspendLayout();
            // 
            // tlp_MainContainer
            // 
            this.tlp_MainContainer.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlp_MainContainer.ColumnCount = 3;
            this.tlp_MainContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tlp_MainContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlp_MainContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlp_MainContainer.Controls.Add(this.tableLayoutPanel_Bottom, 0, 2);
            this.tlp_MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_MainContainer.Enabled = false;
            this.tlp_MainContainer.Location = new System.Drawing.Point(0, 63);
            this.tlp_MainContainer.Name = "tlp_MainContainer";
            this.tlp_MainContainer.RowCount = 3;
            this.tlp_MainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_MainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_MainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlp_MainContainer.Size = new System.Drawing.Size(1595, 648);
            this.tlp_MainContainer.TabIndex = 2;
            // 
            // tableLayoutPanel_Bottom
            // 
            this.tableLayoutPanel_Bottom.ColumnCount = 4;
            this.tlp_MainContainer.SetColumnSpan(this.tableLayoutPanel_Bottom, 3);
            this.tableLayoutPanel_Bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel_Bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel_Bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel_Bottom.Controls.Add(this.tb_InfoBox, 0, 0);
            this.tableLayoutPanel_Bottom.Controls.Add(this.lbl_WinMode, 3, 0);
            this.tableLayoutPanel_Bottom.Controls.Add(this.lbl_Phone, 1, 0);
            this.tableLayoutPanel_Bottom.Controls.Add(this.lbl_CurVersion, 2, 0);
            this.tableLayoutPanel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Bottom.Location = new System.Drawing.Point(4, 614);
            this.tableLayoutPanel_Bottom.Name = "tableLayoutPanel_Bottom";
            this.tableLayoutPanel_Bottom.RowCount = 1;
            this.tableLayoutPanel_Bottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Bottom.Size = new System.Drawing.Size(1587, 30);
            this.tableLayoutPanel_Bottom.TabIndex = 5;
            // 
            // tb_InfoBox
            // 
            this.tb_InfoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_InfoBox.Location = new System.Drawing.Point(3, 0);
            this.tb_InfoBox.Name = "tb_InfoBox";
            this.tb_InfoBox.Size = new System.Drawing.Size(1131, 30);
            this.tb_InfoBox.TabIndex = 0;
            this.tb_InfoBox.Text = "          ";
            this.tb_InfoBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_WinMode
            // 
            this.lbl_WinMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_WinMode.AutoSize = true;
            this.lbl_WinMode.ForeColor = System.Drawing.Color.Blue;
            this.lbl_WinMode.Location = new System.Drawing.Point(1519, 8);
            this.lbl_WinMode.Name = "lbl_WinMode";
            this.lbl_WinMode.Size = new System.Drawing.Size(56, 14);
            this.lbl_WinMode.TabIndex = 7;
            this.lbl_WinMode.Text = "  Win:1";
            this.lbl_WinMode.DoubleClick += new System.EventHandler(this.lbl_WinMode_DoubleClick);
            // 
            // lbl_Phone
            // 
            this.lbl_Phone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_Phone.AutoSize = true;
            this.lbl_Phone.Location = new System.Drawing.Point(1230, 8);
            this.lbl_Phone.Name = "lbl_Phone";
            this.lbl_Phone.Size = new System.Drawing.Size(63, 14);
            this.lbl_Phone.TabIndex = 5;
            this.lbl_Phone.TabStop = true;
            this.lbl_Phone.Text = "        ";
            // 
            // lbl_CurVersion
            // 
            this.lbl_CurVersion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_CurVersion.AutoSize = true;
            this.lbl_CurVersion.LinkColor = System.Drawing.Color.Blue;
            this.lbl_CurVersion.Location = new System.Drawing.Point(1401, 8);
            this.lbl_CurVersion.Name = "lbl_CurVersion";
            this.lbl_CurVersion.Size = new System.Drawing.Size(91, 14);
            this.lbl_CurVersion.TabIndex = 4;
            this.lbl_CurVersion.TabStop = true;
            this.lbl_CurVersion.Text = "Ver: 1.0.0.0";
            this.lbl_CurVersion.DoubleClick += new System.EventHandler(this.lbl_CurVersion_DoubleClick);
            // 
            // pnl_Menu
            // 
            this.pnl_Menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnl_Menu.Controls.Add(this.tableLayoutPanel1);
            this.pnl_Menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Menu.Enabled = false;
            this.pnl_Menu.Location = new System.Drawing.Point(0, 0);
            this.pnl_Menu.Name = "pnl_Menu";
            this.pnl_Menu.Size = new System.Drawing.Size(1595, 63);
            this.pnl_Menu.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 15;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Controls.Add(this.btn_ClearRecord, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_CamList, 8, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_DelaySub, 9, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_TrainModeAll, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_ConnAll, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_LearnAll, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_DisConnAll, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_ROISize, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_ROISize, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancelAlarm, 14, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_ROITitle, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_TestLoad, 11, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_TestAll, 11, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_FrmTestStop, 12, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Settings, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_DelayPlus, 10, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_TriggerDelay, 9, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1595, 63);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // btn_ClearRecord
            // 
            this.btn_ClearRecord.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_ClearRecord.BackColor = System.Drawing.Color.Aqua;
            this.btn_ClearRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ClearRecord.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ClearRecord.Location = new System.Drawing.Point(3, 4);
            this.btn_ClearRecord.Name = "btn_ClearRecord";
            this.tableLayoutPanel1.SetRowSpan(this.btn_ClearRecord, 2);
            this.btn_ClearRecord.Size = new System.Drawing.Size(114, 54);
            this.btn_ClearRecord.TabIndex = 0;
            this.btn_ClearRecord.Text = "清除计数";
            this.btn_ClearRecord.UseVisualStyleBackColor = false;
            this.btn_ClearRecord.Click += new System.EventHandler(this.btn_ClearRecord_Click);
            this.btn_ClearRecord.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseDown);
            this.btn_ClearRecord.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseUp);
            // 
            // comboBox_CamList
            // 
            this.comboBox_CamList.BackColor = System.Drawing.Color.Aqua;
            this.comboBox_CamList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_CamList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_CamList.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_CamList.FormattingEnabled = true;
            this.comboBox_CamList.Location = new System.Drawing.Point(960, 31);
            this.comboBox_CamList.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox_CamList.Name = "comboBox_CamList";
            this.comboBox_CamList.Size = new System.Drawing.Size(170, 28);
            this.comboBox_CamList.TabIndex = 6;
            this.comboBox_CamList.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button_DelaySub
            // 
            this.button_DelaySub.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_DelaySub.BackColor = System.Drawing.Color.Aqua;
            this.button_DelaySub.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_DelaySub.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold);
            this.button_DelaySub.Location = new System.Drawing.Point(1130, 31);
            this.button_DelaySub.Margin = new System.Windows.Forms.Padding(0);
            this.button_DelaySub.Name = "button_DelaySub";
            this.button_DelaySub.Size = new System.Drawing.Size(90, 32);
            this.button_DelaySub.TabIndex = 0;
            this.button_DelaySub.Text = "-";
            this.button_DelaySub.UseVisualStyleBackColor = false;
            this.button_DelaySub.Click += new System.EventHandler(this.button_DelaySub_Click);
            // 
            // btn_TrainModeAll
            // 
            this.btn_TrainModeAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_TrainModeAll.BackColor = System.Drawing.Color.Aqua;
            this.btn_TrainModeAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_TrainModeAll.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_TrainModeAll.Location = new System.Drawing.Point(123, 4);
            this.btn_TrainModeAll.Name = "btn_TrainModeAll";
            this.tableLayoutPanel1.SetRowSpan(this.btn_TrainModeAll, 2);
            this.btn_TrainModeAll.Size = new System.Drawing.Size(114, 54);
            this.btn_TrainModeAll.TabIndex = 0;
            this.btn_TrainModeAll.Text = "一键建模";
            this.btn_TrainModeAll.UseVisualStyleBackColor = false;
            this.btn_TrainModeAll.Click += new System.EventHandler(this.btn_TrainModeAll_Click);
            this.btn_TrainModeAll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseDown);
            this.btn_TrainModeAll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseUp);
            // 
            // btn_ConnAll
            // 
            this.btn_ConnAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_ConnAll.BackColor = System.Drawing.Color.Aqua;
            this.btn_ConnAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ConnAll.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ConnAll.Location = new System.Drawing.Point(363, 4);
            this.btn_ConnAll.Name = "btn_ConnAll";
            this.tableLayoutPanel1.SetRowSpan(this.btn_ConnAll, 2);
            this.btn_ConnAll.Size = new System.Drawing.Size(114, 54);
            this.btn_ConnAll.TabIndex = 0;
            this.btn_ConnAll.Text = "一键联机";
            this.btn_ConnAll.UseVisualStyleBackColor = false;
            this.btn_ConnAll.Click += new System.EventHandler(this.btn_ConnAll_Click);
            this.btn_ConnAll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseDown);
            this.btn_ConnAll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseUp);
            // 
            // btn_LearnAll
            // 
            this.btn_LearnAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_LearnAll.BackColor = System.Drawing.Color.Aqua;
            this.btn_LearnAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_LearnAll.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_LearnAll.Location = new System.Drawing.Point(243, 4);
            this.btn_LearnAll.Name = "btn_LearnAll";
            this.tableLayoutPanel1.SetRowSpan(this.btn_LearnAll, 2);
            this.btn_LearnAll.Size = new System.Drawing.Size(114, 54);
            this.btn_LearnAll.TabIndex = 0;
            this.btn_LearnAll.Text = "一键学习";
            this.btn_LearnAll.UseVisualStyleBackColor = false;
            this.btn_LearnAll.Click += new System.EventHandler(this.btn_LearnAll_Click);
            this.btn_LearnAll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseDown);
            this.btn_LearnAll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseUp);
            // 
            // btn_DisConnAll
            // 
            this.btn_DisConnAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_DisConnAll.BackColor = System.Drawing.Color.Aqua;
            this.btn_DisConnAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DisConnAll.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DisConnAll.Location = new System.Drawing.Point(483, 4);
            this.btn_DisConnAll.Name = "btn_DisConnAll";
            this.tableLayoutPanel1.SetRowSpan(this.btn_DisConnAll, 2);
            this.btn_DisConnAll.Size = new System.Drawing.Size(114, 54);
            this.btn_DisConnAll.TabIndex = 0;
            this.btn_DisConnAll.Text = "一键脱机";
            this.btn_DisConnAll.UseVisualStyleBackColor = false;
            this.btn_DisConnAll.Click += new System.EventHandler(this.btn_DisConnAll_Click);
            this.btn_DisConnAll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseDown);
            this.btn_DisConnAll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseUp);
            // 
            // tb_ROISize
            // 
            this.tb_ROISize.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_ROISize.AutoSize = false;
            this.tableLayoutPanel1.SetColumnSpan(this.tb_ROISize, 2);
            this.tb_ROISize.Location = new System.Drawing.Point(731, 34);
            this.tb_ROISize.Maximum = 100;
            this.tb_ROISize.Minimum = 2;
            this.tb_ROISize.Name = "tb_ROISize";
            this.tb_ROISize.Size = new System.Drawing.Size(217, 26);
            this.tb_ROISize.TabIndex = 8;
            this.tb_ROISize.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tb_ROISize.Value = 50;
            this.tb_ROISize.Scroll += new System.EventHandler(this.tb_ROIStep_Scroll);
            // 
            // lbl_ROISize
            // 
            this.lbl_ROISize.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_ROISize.AutoSize = true;
            this.lbl_ROISize.Font = new System.Drawing.Font("宋体", 18F);
            this.lbl_ROISize.Location = new System.Drawing.Point(898, 3);
            this.lbl_ROISize.Name = "lbl_ROISize";
            this.lbl_ROISize.Size = new System.Drawing.Size(34, 24);
            this.lbl_ROISize.TabIndex = 9;
            this.lbl_ROISize.Text = "50";
            this.lbl_ROISize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelAlarm
            // 
            this.btnCancelAlarm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancelAlarm.BackColor = System.Drawing.Color.Aqua;
            this.btnCancelAlarm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelAlarm.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancelAlarm.Location = new System.Drawing.Point(1478, 3);
            this.btnCancelAlarm.Name = "btnCancelAlarm";
            this.tableLayoutPanel1.SetRowSpan(this.btnCancelAlarm, 2);
            this.btnCancelAlarm.Size = new System.Drawing.Size(114, 57);
            this.btnCancelAlarm.TabIndex = 11;
            this.btnCancelAlarm.Text = "取消告警";
            this.btnCancelAlarm.UseVisualStyleBackColor = false;
            this.btnCancelAlarm.Click += new System.EventHandler(this.btnCancelAlarm_Click);
            // 
            // label_ROITitle
            // 
            this.label_ROITitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_ROITitle.AutoSize = true;
            this.label_ROITitle.Font = new System.Drawing.Font("宋体", 18F);
            this.label_ROITitle.Location = new System.Drawing.Point(724, 3);
            this.label_ROITitle.Name = "label_ROITitle";
            this.label_ROITitle.Size = new System.Drawing.Size(142, 24);
            this.label_ROITitle.TabIndex = 10;
            this.label_ROITitle.Text = "ROI增减幅度";
            this.label_ROITitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_TestLoad
            // 
            this.btn_TestLoad.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_TestLoad.Location = new System.Drawing.Point(1332, 3);
            this.btn_TestLoad.Name = "btn_TestLoad";
            this.btn_TestLoad.Size = new System.Drawing.Size(75, 25);
            this.btn_TestLoad.TabIndex = 1;
            this.btn_TestLoad.Text = "加载图像";
            this.btn_TestLoad.UseVisualStyleBackColor = true;
            this.btn_TestLoad.Visible = false;
            this.btn_TestLoad.Click += new System.EventHandler(this.btn_TestLoad_Click);
            // 
            // btn_TestAll
            // 
            this.btn_TestAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_TestAll.Location = new System.Drawing.Point(1332, 34);
            this.btn_TestAll.Name = "btn_TestAll";
            this.btn_TestAll.Size = new System.Drawing.Size(75, 26);
            this.btn_TestAll.TabIndex = 2;
            this.btn_TestAll.Text = "连续测试";
            this.btn_TestAll.UseVisualStyleBackColor = true;
            this.btn_TestAll.Visible = false;
            this.btn_TestAll.Click += new System.EventHandler(this.btn_TestAll_Click);
            // 
            // btn_FrmTestStop
            // 
            this.btn_FrmTestStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_FrmTestStop.Location = new System.Drawing.Point(1452, 3);
            this.btn_FrmTestStop.Name = "btn_FrmTestStop";
            this.btn_FrmTestStop.Size = new System.Drawing.Size(75, 25);
            this.btn_FrmTestStop.TabIndex = 2;
            this.btn_FrmTestStop.Text = "停止测试";
            this.btn_FrmTestStop.UseVisualStyleBackColor = true;
            this.btn_FrmTestStop.Visible = false;
            this.btn_FrmTestStop.Click += new System.EventHandler(this.btn_FrmTestStop_Click);
            // 
            // btn_Settings
            // 
            this.btn_Settings.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Settings.BackColor = System.Drawing.Color.Aqua;
            this.btn_Settings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Settings.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Settings.Location = new System.Drawing.Point(603, 4);
            this.btn_Settings.Name = "btn_Settings";
            this.tableLayoutPanel1.SetRowSpan(this.btn_Settings, 2);
            this.btn_Settings.Size = new System.Drawing.Size(114, 54);
            this.btn_Settings.TabIndex = 0;
            this.btn_Settings.Text = "设置";
            this.btn_Settings.UseVisualStyleBackColor = false;
            this.btn_Settings.Click += new System.EventHandler(this.btn_Settings_Click);
            this.btn_Settings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseDown);
            this.btn_Settings.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Function_MouseUp);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F);
            this.label1.Location = new System.Drawing.Point(980, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 10;
            this.label1.Text = "延迟触发ms";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_DelayPlus
            // 
            this.button_DelayPlus.BackColor = System.Drawing.Color.Aqua;
            this.button_DelayPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_DelayPlus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_DelayPlus.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_DelayPlus.Location = new System.Drawing.Point(1220, 31);
            this.button_DelayPlus.Margin = new System.Windows.Forms.Padding(0);
            this.button_DelayPlus.Name = "button_DelayPlus";
            this.button_DelayPlus.Size = new System.Drawing.Size(90, 32);
            this.button_DelayPlus.TabIndex = 0;
            this.button_DelayPlus.Text = "+";
            this.button_DelayPlus.UseVisualStyleBackColor = false;
            this.button_DelayPlus.Click += new System.EventHandler(this.button_DelayPlus_Click);
            // 
            // label_TriggerDelay
            // 
            this.label_TriggerDelay.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label_TriggerDelay, 2);
            this.label_TriggerDelay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_TriggerDelay.Font = new System.Drawing.Font("宋体", 18F);
            this.label_TriggerDelay.Location = new System.Drawing.Point(1133, 0);
            this.label_TriggerDelay.Name = "label_TriggerDelay";
            this.label_TriggerDelay.Size = new System.Drawing.Size(174, 31);
            this.label_TriggerDelay.TabIndex = 9;
            this.label_TriggerDelay.Text = "0";
            this.label_TriggerDelay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.SelectedPath = "C:\\Users\\Administrator\\Desktop";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "BMP 文件 (*.bmp)|*.bmp";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1595, 711);
            this.Controls.Add(this.tlp_MainContainer);
            this.Controls.Add(this.pnl_Menu);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.Text = "SmartEye";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.tlp_MainContainer.ResumeLayout(false);
            this.tableLayoutPanel_Bottom.ResumeLayout(false);
            this.tableLayoutPanel_Bottom.PerformLayout();
            this.pnl_Menu.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_ROISize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlp_MainContainer;
        private System.Windows.Forms.Panel pnl_Menu;
        private System.Windows.Forms.Button btn_ClearRecord;
        private System.Windows.Forms.Label tb_InfoBox;
        private System.Windows.Forms.LinkLabel lbl_CurVersion;
        private System.Windows.Forms.Button btn_LearnAll;
        private System.Windows.Forms.Button btn_ConnAll;
        private System.Windows.Forms.Button btn_DisConnAll;
        private System.Windows.Forms.Button btn_TestLoad;
        private System.Windows.Forms.Button btn_TestAll;
        private System.Windows.Forms.LinkLabel lbl_Phone;
        private System.Windows.Forms.Label lbl_ROISize;
        private System.Windows.Forms.TrackBar tb_ROISize;
        private System.Windows.Forms.Label label_ROITitle;
        private System.Windows.Forms.Label lbl_WinMode;
        private System.Windows.Forms.Button btn_FrmTestStop;
        private System.Windows.Forms.Button btn_TrainModeAll;
        private System.Windows.Forms.Button btnCancelAlarm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Bottom;
        private System.Windows.Forms.Button btn_Settings;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox comboBox_CamList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_DelaySub;
        private System.Windows.Forms.Button button_DelayPlus;
        private System.Windows.Forms.Label label_TriggerDelay;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

