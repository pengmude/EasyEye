
namespace SmartVEye
{
    partial class VisCtrlV124
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_ProRecord = new System.Windows.Forms.DataGridView();
            this.tb_RecVal1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_RecVal2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cb_WorkOnLine = new System.Windows.Forms.CheckBox();
            this.cb_RealImg = new System.Windows.Forms.CheckBox();
            this.lb_ImgSimilarScore = new System.Windows.Forms.Label();
            this.lbl_Res = new System.Windows.Forms.Label();
            this.lbl_CamIndex = new System.Windows.Forms.Label();
            this.comboBox_RunMode = new System.Windows.Forms.ComboBox();
            this.btn_HighPlus = new System.Windows.Forms.Button();
            this.btn_WidthPlus = new System.Windows.Forms.Button();
            this.WinCtrl = new SmartLib.DisplayCtrl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_TrainMode = new System.Windows.Forms.Button();
            this.btn_Train = new System.Windows.Forms.Button();
            this.comboBox_DetectAccuracy = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_WidthSub = new System.Windows.Forms.Button();
            this.btn_HighSub = new System.Windows.Forms.Button();
            this.previewWin1 = new SmartVEye.VisCtrl.PreviewWinCol();
            this.tableLayoutPanel_Result = new System.Windows.Forms.TableLayoutPanel();
            this.panel_View = new System.Windows.Forms.Panel();
            this.panel_Top = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ProRecord)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel_Result.SuspendLayout();
            this.panel_View.SuspendLayout();
            this.panel_Top.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_ProRecord
            // 
            this.dgv_ProRecord.AllowUserToAddRows = false;
            this.dgv_ProRecord.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ProRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ProRecord.ColumnHeadersHeight = 22;
            this.dgv_ProRecord.ColumnHeadersVisible = false;
            this.dgv_ProRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tb_RecVal1,
            this.tb_RecVal2});
            this.dgv_ProRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ProRecord.Location = new System.Drawing.Point(0, 396);
            this.dgv_ProRecord.Margin = new System.Windows.Forms.Padding(0);
            this.dgv_ProRecord.MultiSelect = false;
            this.dgv_ProRecord.Name = "dgv_ProRecord";
            this.dgv_ProRecord.ReadOnly = true;
            this.dgv_ProRecord.RowHeadersVisible = false;
            this.dgv_ProRecord.RowHeadersWidth = 82;
            this.tableLayoutPanel1.SetRowSpan(this.dgv_ProRecord, 2);
            this.dgv_ProRecord.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ProRecord.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgv_ProRecord.Size = new System.Drawing.Size(164, 135);
            this.dgv_ProRecord.TabIndex = 2;
            // 
            // tb_RecVal1
            // 
            this.tb_RecVal1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("黑体", 12F);
            this.tb_RecVal1.DefaultCellStyle = dataGridViewCellStyle2;
            this.tb_RecVal1.HeaderText = "RecVal1";
            this.tb_RecVal1.MinimumWidth = 10;
            this.tb_RecVal1.Name = "tb_RecVal1";
            this.tb_RecVal1.ReadOnly = true;
            this.tb_RecVal1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tb_RecVal1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tb_RecVal2
            // 
            this.tb_RecVal2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("黑体", 12F);
            this.tb_RecVal2.DefaultCellStyle = dataGridViewCellStyle3;
            this.tb_RecVal2.HeaderText = "RecVal2";
            this.tb_RecVal2.MinimumWidth = 10;
            this.tb_RecVal2.Name = "tb_RecVal2";
            this.tb_RecVal2.ReadOnly = true;
            this.tb_RecVal2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tb_RecVal2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cb_WorkOnLine
            // 
            this.cb_WorkOnLine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cb_WorkOnLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cb_WorkOnLine.Font = new System.Drawing.Font("黑体", 15F);
            this.cb_WorkOnLine.Location = new System.Drawing.Point(12, 142);
            this.cb_WorkOnLine.Margin = new System.Windows.Forms.Padding(2);
            this.cb_WorkOnLine.Name = "cb_WorkOnLine";
            this.cb_WorkOnLine.Size = new System.Drawing.Size(140, 45);
            this.cb_WorkOnLine.TabIndex = 4;
            this.cb_WorkOnLine.Text = "联机";
            this.cb_WorkOnLine.UseVisualStyleBackColor = false;
            this.cb_WorkOnLine.CheckedChanged += new System.EventHandler(this.cb_WorkOnLine_CheckedChanged);
            this.cb_WorkOnLine.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cb_WorkOnLine_MouseUp);
            // 
            // cb_RealImg
            // 
            this.cb_RealImg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cb_RealImg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cb_RealImg.Font = new System.Drawing.Font("黑体", 15F);
            this.cb_RealImg.Location = new System.Drawing.Point(12, 208);
            this.cb_RealImg.Margin = new System.Windows.Forms.Padding(2);
            this.cb_RealImg.Name = "cb_RealImg";
            this.cb_RealImg.Size = new System.Drawing.Size(140, 45);
            this.cb_RealImg.TabIndex = 4;
            this.cb_RealImg.Text = "视频";
            this.cb_RealImg.UseVisualStyleBackColor = false;
            this.cb_RealImg.CheckedChanged += new System.EventHandler(this.cb_RealImg_CheckedChanged);
            this.cb_RealImg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cb_RealImg_MouseUp);
            // 
            // lb_ImgSimilarScore
            // 
            this.lb_ImgSimilarScore.BackColor = System.Drawing.Color.LightGray;
            this.lb_ImgSimilarScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_ImgSimilarScore.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.lb_ImgSimilarScore.ForeColor = System.Drawing.Color.Black;
            this.lb_ImgSimilarScore.Location = new System.Drawing.Point(480, 0);
            this.lb_ImgSimilarScore.Margin = new System.Windows.Forms.Padding(0);
            this.lb_ImgSimilarScore.Name = "lb_ImgSimilarScore";
            this.lb_ImgSimilarScore.Size = new System.Drawing.Size(240, 45);
            this.lb_ImgSimilarScore.TabIndex = 5;
            this.lb_ImgSimilarScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Res
            // 
            this.lbl_Res.BackColor = System.Drawing.Color.Lime;
            this.lbl_Res.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Res.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.lbl_Res.ForeColor = System.Drawing.Color.Black;
            this.lbl_Res.Location = new System.Drawing.Point(240, 0);
            this.lbl_Res.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Res.Name = "lbl_Res";
            this.lbl_Res.Size = new System.Drawing.Size(240, 45);
            this.lbl_Res.TabIndex = 4;
            this.lbl_Res.Text = "OK";
            this.lbl_Res.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_CamIndex
            // 
            this.lbl_CamIndex.BackColor = System.Drawing.Color.LightGray;
            this.lbl_CamIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_CamIndex.Font = new System.Drawing.Font("黑体", 25F, System.Drawing.FontStyle.Bold);
            this.lbl_CamIndex.ForeColor = System.Drawing.Color.Black;
            this.lbl_CamIndex.Location = new System.Drawing.Point(0, 0);
            this.lbl_CamIndex.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_CamIndex.Name = "lbl_CamIndex";
            this.lbl_CamIndex.Size = new System.Drawing.Size(240, 45);
            this.lbl_CamIndex.TabIndex = 4;
            this.lbl_CamIndex.Text = "1";
            this.lbl_CamIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_RunMode
            // 
            this.comboBox_RunMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox_RunMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.comboBox_RunMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_RunMode.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_RunMode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox_RunMode.FormattingEnabled = true;
            this.comboBox_RunMode.Items.AddRange(new object[] {
            "图文检测",
            "白页检测",
            "黑页检测"});
            this.comboBox_RunMode.Location = new System.Drawing.Point(12, 281);
            this.comboBox_RunMode.Name = "comboBox_RunMode";
            this.comboBox_RunMode.Size = new System.Drawing.Size(140, 32);
            this.comboBox_RunMode.TabIndex = 6;
            this.comboBox_RunMode.SelectedIndexChanged += new System.EventHandler(this.comboBox_RunMode_SelectedIndexChanged);
            // 
            // btn_HighPlus
            // 
            this.btn_HighPlus.BackColor = System.Drawing.Color.Aqua;
            this.btn_HighPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_HighPlus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_HighPlus.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.btn_HighPlus.Location = new System.Drawing.Point(360, 444);
            this.btn_HighPlus.Margin = new System.Windows.Forms.Padding(0);
            this.btn_HighPlus.Name = "btn_HighPlus";
            this.btn_HighPlus.Size = new System.Drawing.Size(180, 42);
            this.btn_HighPlus.TabIndex = 5;
            this.btn_HighPlus.Text = "高+";
            this.btn_HighPlus.UseVisualStyleBackColor = false;
            this.btn_HighPlus.Visible = false;
            this.btn_HighPlus.Click += new System.EventHandler(this.btn_HighPlus_Click);
            // 
            // btn_WidthPlus
            // 
            this.btn_WidthPlus.BackColor = System.Drawing.Color.Aqua;
            this.btn_WidthPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_WidthPlus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_WidthPlus.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.btn_WidthPlus.Location = new System.Drawing.Point(0, 444);
            this.btn_WidthPlus.Margin = new System.Windows.Forms.Padding(0);
            this.btn_WidthPlus.Name = "btn_WidthPlus";
            this.btn_WidthPlus.Size = new System.Drawing.Size(180, 42);
            this.btn_WidthPlus.TabIndex = 5;
            this.btn_WidthPlus.Text = "宽+";
            this.btn_WidthPlus.UseVisualStyleBackColor = false;
            this.btn_WidthPlus.Visible = false;
            this.btn_WidthPlus.Click += new System.EventHandler(this.btn_WidthPlus_Click);
            // 
            // WinCtrl
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.WinCtrl, 3);
            this.WinCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WinCtrl.Location = new System.Drawing.Point(183, 3);
            this.WinCtrl.Name = "WinCtrl";
            this.tableLayoutPanel2.SetRowSpan(this.WinCtrl, 3);
            this.WinCtrl.Size = new System.Drawing.Size(534, 438);
            this.WinCtrl.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.comboBox_RunMode, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btn_TrainMode, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cb_RealImg, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn_Train, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cb_WorkOnLine, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgv_ProRecord, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_DetectAccuracy, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(720, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(164, 531);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // btn_TrainMode
            // 
            this.btn_TrainMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_TrainMode.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_TrainMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_TrainMode.Font = new System.Drawing.Font("黑体", 25F, System.Drawing.FontStyle.Bold);
            this.btn_TrainMode.Image = global::SmartVEye.Properties.Resources.no;
            this.btn_TrainMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_TrainMode.Location = new System.Drawing.Point(12, 3);
            this.btn_TrainMode.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.btn_TrainMode.Name = "btn_TrainMode";
            this.btn_TrainMode.Size = new System.Drawing.Size(140, 58);
            this.btn_TrainMode.TabIndex = 5;
            this.btn_TrainMode.Text = "建模";
            this.btn_TrainMode.UseVisualStyleBackColor = false;
            this.btn_TrainMode.Click += new System.EventHandler(this.btn_TrainMode_Click);
            // 
            // btn_Train
            // 
            this.btn_Train.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Train.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_Train.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Train.Font = new System.Drawing.Font("黑体", 25F, System.Drawing.FontStyle.Bold);
            this.btn_Train.Image = global::SmartVEye.Properties.Resources.no;
            this.btn_Train.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Train.Location = new System.Drawing.Point(12, 70);
            this.btn_Train.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Train.Name = "btn_Train";
            this.btn_Train.Size = new System.Drawing.Size(140, 58);
            this.btn_Train.TabIndex = 2;
            this.btn_Train.Text = "学习";
            this.btn_Train.UseVisualStyleBackColor = false;
            this.btn_Train.Click += new System.EventHandler(this.btn_Train_Click);
            this.btn_Train.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Train_MouseDown);
            this.btn_Train.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Train_MouseUp);
            // 
            // comboBox_DetectAccuracy
            // 
            this.comboBox_DetectAccuracy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox_DetectAccuracy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.comboBox_DetectAccuracy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DetectAccuracy.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_DetectAccuracy.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox_DetectAccuracy.FormattingEnabled = true;
            this.comboBox_DetectAccuracy.Items.AddRange(new object[] {
            "高精度",
            "中精度",
            "低精度"});
            this.comboBox_DetectAccuracy.Location = new System.Drawing.Point(12, 347);
            this.comboBox_DetectAccuracy.Name = "comboBox_DetectAccuracy";
            this.comboBox_DetectAccuracy.Size = new System.Drawing.Size(140, 32);
            this.comboBox_DetectAccuracy.TabIndex = 6;
            this.comboBox_DetectAccuracy.SelectedIndexChanged += new System.EventHandler(this.comboBox_DetectAccuracy_SelectionChangeCommitted);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.btn_WidthPlus, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.btn_WidthSub, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.btn_HighPlus, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.btn_HighSub, 3, 3);
            this.tableLayoutPanel2.Controls.Add(this.WinCtrl, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.previewWin1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(720, 486);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // btn_WidthSub
            // 
            this.btn_WidthSub.BackColor = System.Drawing.Color.Aqua;
            this.btn_WidthSub.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_WidthSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_WidthSub.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.btn_WidthSub.Location = new System.Drawing.Point(180, 444);
            this.btn_WidthSub.Margin = new System.Windows.Forms.Padding(0);
            this.btn_WidthSub.Name = "btn_WidthSub";
            this.btn_WidthSub.Size = new System.Drawing.Size(180, 42);
            this.btn_WidthSub.TabIndex = 5;
            this.btn_WidthSub.Text = "宽-";
            this.btn_WidthSub.UseVisualStyleBackColor = false;
            this.btn_WidthSub.Visible = false;
            this.btn_WidthSub.Click += new System.EventHandler(this.btn_WidthSub_Click);
            // 
            // btn_HighSub
            // 
            this.btn_HighSub.BackColor = System.Drawing.Color.Aqua;
            this.btn_HighSub.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_HighSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_HighSub.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.btn_HighSub.Location = new System.Drawing.Point(540, 444);
            this.btn_HighSub.Margin = new System.Windows.Forms.Padding(0);
            this.btn_HighSub.Name = "btn_HighSub";
            this.btn_HighSub.Size = new System.Drawing.Size(180, 42);
            this.btn_HighSub.TabIndex = 5;
            this.btn_HighSub.Text = "高-";
            this.btn_HighSub.UseVisualStyleBackColor = false;
            this.btn_HighSub.Visible = false;
            this.btn_HighSub.Click += new System.EventHandler(this.btn_HighSub_Click);
            // 
            // previewWin1
            // 
            this.previewWin1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewWin1.Location = new System.Drawing.Point(3, 3);
            this.previewWin1.Name = "previewWin1";
            this.tableLayoutPanel2.SetRowSpan(this.previewWin1, 3);
            this.previewWin1.Size = new System.Drawing.Size(174, 438);
            this.previewWin1.TabIndex = 9;
            // 
            // tableLayoutPanel_Result
            // 
            this.tableLayoutPanel_Result.ColumnCount = 3;
            this.tableLayoutPanel_Result.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_Result.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_Result.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_Result.Controls.Add(this.lb_ImgSimilarScore, 2, 0);
            this.tableLayoutPanel_Result.Controls.Add(this.lbl_Res, 1, 0);
            this.tableLayoutPanel_Result.Controls.Add(this.lbl_CamIndex, 0, 0);
            this.tableLayoutPanel_Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Result.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Result.Name = "tableLayoutPanel_Result";
            this.tableLayoutPanel_Result.RowCount = 1;
            this.tableLayoutPanel_Result.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Result.Size = new System.Drawing.Size(720, 45);
            this.tableLayoutPanel_Result.TabIndex = 13;
            // 
            // panel_View
            // 
            this.panel_View.Controls.Add(this.tableLayoutPanel2);
            this.panel_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_View.Location = new System.Drawing.Point(0, 45);
            this.panel_View.Name = "panel_View";
            this.panel_View.Size = new System.Drawing.Size(720, 486);
            this.panel_View.TabIndex = 15;
            // 
            // panel_Top
            // 
            this.panel_Top.Controls.Add(this.tableLayoutPanel_Result);
            this.panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Top.Location = new System.Drawing.Point(0, 0);
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Size = new System.Drawing.Size(720, 45);
            this.panel_Top.TabIndex = 14;
            // 
            // VisCtrlV2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel_View);
            this.Controls.Add(this.panel_Top);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "VisCtrlV2";
            this.Size = new System.Drawing.Size(884, 531);
            this.Load += new System.EventHandler(this.VisCtrl_Load);
            this.Resize += new System.EventHandler(this.VisCtrlV2_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ProRecord)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel_Result.ResumeLayout(false);
            this.panel_View.ResumeLayout(false);
            this.panel_Top.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgv_ProRecord;
        private System.Windows.Forms.CheckBox cb_RealImg;
        private System.Windows.Forms.Label lbl_CamIndex;
        private System.Windows.Forms.CheckBox cb_WorkOnLine;
        private System.Windows.Forms.Button btn_Train;
        private System.Windows.Forms.Button btn_WidthPlus;
        private System.Windows.Forms.Button btn_HighPlus;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_RecVal1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_RecVal2;
        private System.Windows.Forms.Button btn_TrainMode;
        private System.Windows.Forms.Label lbl_Res;
        private SmartLib.DisplayCtrl WinCtrl;
        private System.Windows.Forms.Label lb_ImgSimilarScore;
        private System.Windows.Forms.ComboBox comboBox_RunMode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBox_DetectAccuracy;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Result;
        private System.Windows.Forms.Panel panel_View;
        private System.Windows.Forms.Panel panel_Top;
        private System.Windows.Forms.Button btn_WidthSub;
        private System.Windows.Forms.Button btn_HighSub;
        private VisCtrl.PreviewWinCol previewWin1;
    }
}
