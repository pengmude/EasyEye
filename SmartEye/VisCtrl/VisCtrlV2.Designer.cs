
namespace SmartVEye
{
    partial class VisCtrlV2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_ProRecord = new System.Windows.Forms.DataGridView();
            this.tb_RecVal1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_RecVal2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cb_CamEnable = new System.Windows.Forms.CheckBox();
            this.cb_WorkOnLine = new System.Windows.Forms.CheckBox();
            this.cb_RealImg = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lb_ImgSimilarScore = new System.Windows.Forms.Label();
            this.lbl_Res = new System.Windows.Forms.Label();
            this.lbl_CamIndex = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Train = new System.Windows.Forms.Button();
            this.cb_FindByte = new System.Windows.Forms.CheckBox();
            this.cb_BlackPage = new System.Windows.Forms.CheckBox();
            this.cb_WhitePage = new System.Windows.Forms.CheckBox();
            this.btn_TrainMode = new System.Windows.Forms.Button();
            this.btn_HighSub = new System.Windows.Forms.Button();
            this.btn_HighPlus = new System.Windows.Forms.Button();
            this.btn_WidthSub = new System.Windows.Forms.Button();
            this.btn_WidthPlus = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.WinCtrl = new SmartLib.DisplayCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ProRecord)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_ProRecord
            // 
            this.dgv_ProRecord.AllowUserToAddRows = false;
            this.dgv_ProRecord.AllowUserToDeleteRows = false;
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ProRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
            this.dgv_ProRecord.ColumnHeadersHeight = 22;
            this.dgv_ProRecord.ColumnHeadersVisible = false;
            this.dgv_ProRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tb_RecVal1,
            this.tb_RecVal2});
            this.dgv_ProRecord.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv_ProRecord.Location = new System.Drawing.Point(0, 467);
            this.dgv_ProRecord.Margin = new System.Windows.Forms.Padding(0);
            this.dgv_ProRecord.MultiSelect = false;
            this.dgv_ProRecord.Name = "dgv_ProRecord";
            this.dgv_ProRecord.ReadOnly = true;
            this.dgv_ProRecord.RowHeadersVisible = false;
            this.dgv_ProRecord.RowHeadersWidth = 82;
            this.dgv_ProRecord.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ProRecord.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgv_ProRecord.Size = new System.Drawing.Size(158, 85);
            this.dgv_ProRecord.TabIndex = 2;
            // 
            // tb_RecVal1
            // 
            this.tb_RecVal1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("黑体", 12F);
            this.tb_RecVal1.DefaultCellStyle = dataGridViewCellStyle26;
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
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("黑体", 12F);
            this.tb_RecVal2.DefaultCellStyle = dataGridViewCellStyle27;
            this.tb_RecVal2.HeaderText = "RecVal2";
            this.tb_RecVal2.MinimumWidth = 10;
            this.tb_RecVal2.Name = "tb_RecVal2";
            this.tb_RecVal2.ReadOnly = true;
            this.tb_RecVal2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tb_RecVal2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cb_CamEnable
            // 
            this.cb_CamEnable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cb_CamEnable.Checked = true;
            this.cb_CamEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_CamEnable.Font = new System.Drawing.Font("黑体", 15F);
            this.cb_CamEnable.Location = new System.Drawing.Point(196, 164);
            this.cb_CamEnable.Margin = new System.Windows.Forms.Padding(2);
            this.cb_CamEnable.Name = "cb_CamEnable";
            this.cb_CamEnable.Size = new System.Drawing.Size(141, 25);
            this.cb_CamEnable.TabIndex = 4;
            this.cb_CamEnable.Text = "启用";
            this.cb_CamEnable.UseVisualStyleBackColor = false;
            this.cb_CamEnable.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cb_CamEnable_MouseUp);
            // 
            // cb_WorkOnLine
            // 
            this.cb_WorkOnLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cb_WorkOnLine.Font = new System.Drawing.Font("黑体", 15F);
            this.cb_WorkOnLine.Location = new System.Drawing.Point(10, 173);
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
            this.cb_RealImg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cb_RealImg.Font = new System.Drawing.Font("黑体", 15F);
            this.cb_RealImg.Location = new System.Drawing.Point(10, 230);
            this.cb_RealImg.Margin = new System.Windows.Forms.Padding(2);
            this.cb_RealImg.Name = "cb_RealImg";
            this.cb_RealImg.Size = new System.Drawing.Size(140, 45);
            this.cb_RealImg.TabIndex = 4;
            this.cb_RealImg.Text = "视频";
            this.cb_RealImg.UseVisualStyleBackColor = false;
            this.cb_RealImg.CheckedChanged += new System.EventHandler(this.cb_RealImg_CheckedChanged);
            this.cb_RealImg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cb_RealImg_MouseUp);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lb_ImgSimilarScore);
            this.panel3.Controls.Add(this.lbl_Res);
            this.panel3.Controls.Add(this.lbl_CamIndex);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(50, 552);
            this.panel3.TabIndex = 6;
            // 
            // lb_ImgSimilarScore
            // 
            this.lb_ImgSimilarScore.BackColor = System.Drawing.Color.LightGray;
            this.lb_ImgSimilarScore.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lb_ImgSimilarScore.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.lb_ImgSimilarScore.ForeColor = System.Drawing.Color.Black;
            this.lb_ImgSimilarScore.Location = new System.Drawing.Point(0, 487);
            this.lb_ImgSimilarScore.Margin = new System.Windows.Forms.Padding(0);
            this.lb_ImgSimilarScore.Name = "lb_ImgSimilarScore";
            this.lb_ImgSimilarScore.Size = new System.Drawing.Size(50, 65);
            this.lb_ImgSimilarScore.TabIndex = 5;
            this.lb_ImgSimilarScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Res
            // 
            this.lbl_Res.BackColor = System.Drawing.Color.Lime;
            this.lbl_Res.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Res.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold);
            this.lbl_Res.ForeColor = System.Drawing.Color.Black;
            this.lbl_Res.Location = new System.Drawing.Point(0, 65);
            this.lbl_Res.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Res.Name = "lbl_Res";
            this.lbl_Res.Size = new System.Drawing.Size(50, 487);
            this.lbl_Res.TabIndex = 4;
            this.lbl_Res.Text = "OK";
            this.lbl_Res.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_CamIndex
            // 
            this.lbl_CamIndex.BackColor = System.Drawing.Color.LightGray;
            this.lbl_CamIndex.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_CamIndex.Font = new System.Drawing.Font("黑体", 25F, System.Drawing.FontStyle.Bold);
            this.lbl_CamIndex.ForeColor = System.Drawing.Color.Black;
            this.lbl_CamIndex.Location = new System.Drawing.Point(0, 0);
            this.lbl_CamIndex.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_CamIndex.Name = "lbl_CamIndex";
            this.lbl_CamIndex.Size = new System.Drawing.Size(50, 65);
            this.lbl_CamIndex.TabIndex = 4;
            this.lbl_CamIndex.Text = "1";
            this.lbl_CamIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.cb_RealImg);
            this.panel2.Controls.Add(this.btn_Train);
            this.panel2.Controls.Add(this.dgv_ProRecord);
            this.panel2.Controls.Add(this.cb_WorkOnLine);
            this.panel2.Controls.Add(this.cb_FindByte);
            this.panel2.Controls.Add(this.cb_BlackPage);
            this.panel2.Controls.Add(this.cb_WhitePage);
            this.panel2.Controls.Add(this.btn_TrainMode);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(453, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(158, 552);
            this.panel2.TabIndex = 9;
            // 
            // btn_Train
            // 
            this.btn_Train.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_Train.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Train.Font = new System.Drawing.Font("黑体", 25F, System.Drawing.FontStyle.Bold);
            this.btn_Train.Image = global::SmartVEye.Properties.Resources.no;
            this.btn_Train.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Train.Location = new System.Drawing.Point(10, 89);
            this.btn_Train.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Train.Name = "btn_Train";
            this.btn_Train.Size = new System.Drawing.Size(140, 75);
            this.btn_Train.TabIndex = 2;
            this.btn_Train.Text = "学习";
            this.btn_Train.UseVisualStyleBackColor = false;
            this.btn_Train.Click += new System.EventHandler(this.btn_Train_Click);
            this.btn_Train.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Train_MouseDown);
            this.btn_Train.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Train_MouseUp);
            // 
            // cb_FindByte
            // 
            this.cb_FindByte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cb_FindByte.Checked = true;
            this.cb_FindByte.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_FindByte.Font = new System.Drawing.Font("黑体", 15F);
            this.cb_FindByte.Location = new System.Drawing.Point(10, 286);
            this.cb_FindByte.Margin = new System.Windows.Forms.Padding(2);
            this.cb_FindByte.Name = "cb_FindByte";
            this.cb_FindByte.Size = new System.Drawing.Size(140, 45);
            this.cb_FindByte.TabIndex = 4;
            this.cb_FindByte.Text = "图文检测";
            this.cb_FindByte.UseVisualStyleBackColor = false;
            this.cb_FindByte.CheckedChanged += new System.EventHandler(this.cb_FindByte_CheckedChanged);
            this.cb_FindByte.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cb_FindByte_MouseUp);
            // 
            // cb_BlackPage
            // 
            this.cb_BlackPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cb_BlackPage.Font = new System.Drawing.Font("黑体", 15F);
            this.cb_BlackPage.Location = new System.Drawing.Point(10, 398);
            this.cb_BlackPage.Margin = new System.Windows.Forms.Padding(2);
            this.cb_BlackPage.Name = "cb_BlackPage";
            this.cb_BlackPage.Size = new System.Drawing.Size(140, 45);
            this.cb_BlackPage.TabIndex = 4;
            this.cb_BlackPage.Text = "黑页检测";
            this.cb_BlackPage.UseVisualStyleBackColor = false;
            this.cb_BlackPage.CheckedChanged += new System.EventHandler(this.cb_BlackPage_CheckedChanged);
            this.cb_BlackPage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cb_BlackPage_MouseUp);
            // 
            // cb_WhitePage
            // 
            this.cb_WhitePage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cb_WhitePage.Font = new System.Drawing.Font("黑体", 15F);
            this.cb_WhitePage.Location = new System.Drawing.Point(10, 343);
            this.cb_WhitePage.Margin = new System.Windows.Forms.Padding(2);
            this.cb_WhitePage.Name = "cb_WhitePage";
            this.cb_WhitePage.Size = new System.Drawing.Size(140, 45);
            this.cb_WhitePage.TabIndex = 4;
            this.cb_WhitePage.Text = "白页检测";
            this.cb_WhitePage.UseVisualStyleBackColor = false;
            this.cb_WhitePage.CheckedChanged += new System.EventHandler(this.cb_WhitePage_CheckedChanged);
            this.cb_WhitePage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cb_WhitePage_MouseUp);
            // 
            // btn_TrainMode
            // 
            this.btn_TrainMode.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_TrainMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_TrainMode.Font = new System.Drawing.Font("黑体", 25F, System.Drawing.FontStyle.Bold);
            this.btn_TrainMode.Image = global::SmartVEye.Properties.Resources.no;
            this.btn_TrainMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_TrainMode.Location = new System.Drawing.Point(10, 8);
            this.btn_TrainMode.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.btn_TrainMode.Name = "btn_TrainMode";
            this.btn_TrainMode.Size = new System.Drawing.Size(140, 75);
            this.btn_TrainMode.TabIndex = 5;
            this.btn_TrainMode.Text = "建模";
            this.btn_TrainMode.UseVisualStyleBackColor = false;
            this.btn_TrainMode.Click += new System.EventHandler(this.btn_TrainMode_Click);
            // 
            // btn_HighSub
            // 
            this.btn_HighSub.BackColor = System.Drawing.Color.Aqua;
            this.btn_HighSub.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_HighSub.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.btn_HighSub.Location = new System.Drawing.Point(230, 487);
            this.btn_HighSub.Name = "btn_HighSub";
            this.btn_HighSub.Size = new System.Drawing.Size(60, 65);
            this.btn_HighSub.TabIndex = 5;
            this.btn_HighSub.Text = "高-";
            this.btn_HighSub.UseVisualStyleBackColor = false;
            this.btn_HighSub.Click += new System.EventHandler(this.btn_HighSub_Click);
            // 
            // btn_HighPlus
            // 
            this.btn_HighPlus.BackColor = System.Drawing.Color.Aqua;
            this.btn_HighPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_HighPlus.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.btn_HighPlus.Location = new System.Drawing.Point(230, 65);
            this.btn_HighPlus.Name = "btn_HighPlus";
            this.btn_HighPlus.Size = new System.Drawing.Size(60, 65);
            this.btn_HighPlus.TabIndex = 5;
            this.btn_HighPlus.Text = "高+";
            this.btn_HighPlus.UseVisualStyleBackColor = false;
            this.btn_HighPlus.Click += new System.EventHandler(this.btn_HighPlus_Click);
            // 
            // btn_WidthSub
            // 
            this.btn_WidthSub.BackColor = System.Drawing.Color.Aqua;
            this.btn_WidthSub.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_WidthSub.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.btn_WidthSub.Location = new System.Drawing.Point(393, 273);
            this.btn_WidthSub.Name = "btn_WidthSub";
            this.btn_WidthSub.Size = new System.Drawing.Size(60, 65);
            this.btn_WidthSub.TabIndex = 5;
            this.btn_WidthSub.Text = "宽-";
            this.btn_WidthSub.UseVisualStyleBackColor = false;
            this.btn_WidthSub.Click += new System.EventHandler(this.btn_WidthSub_Click);
            // 
            // btn_WidthPlus
            // 
            this.btn_WidthPlus.BackColor = System.Drawing.Color.Aqua;
            this.btn_WidthPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_WidthPlus.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold);
            this.btn_WidthPlus.Location = new System.Drawing.Point(50, 266);
            this.btn_WidthPlus.Name = "btn_WidthPlus";
            this.btn_WidthPlus.Size = new System.Drawing.Size(60, 65);
            this.btn_WidthPlus.TabIndex = 5;
            this.btn_WidthPlus.Text = "宽+";
            this.btn_WidthPlus.UseVisualStyleBackColor = false;
            this.btn_WidthPlus.Click += new System.EventHandler(this.btn_WidthPlus_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(50, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(403, 65);
            this.panel1.TabIndex = 10;
            // 
            // WinCtrl
            // 
            this.WinCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WinCtrl.Location = new System.Drawing.Point(50, 65);
            this.WinCtrl.Name = "WinCtrl";
            this.WinCtrl.Size = new System.Drawing.Size(403, 487);
            this.WinCtrl.TabIndex = 8;
            // 
            // VisCtrlV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WinCtrl);
            this.Controls.Add(this.btn_HighSub);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_HighPlus);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btn_WidthSub);
            this.Controls.Add(this.btn_WidthPlus);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.cb_CamEnable);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "VisCtrlV2";
            this.Size = new System.Drawing.Size(611, 552);
            this.Load += new System.EventHandler(this.VisCtrl_Load);
            this.Resize += new System.EventHandler(this.VisCtrlV2_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ProRecord)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgv_ProRecord;
        private System.Windows.Forms.CheckBox cb_RealImg;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl_CamIndex;
        private System.Windows.Forms.CheckBox cb_WorkOnLine;
        private System.Windows.Forms.Button btn_Train;
        private System.Windows.Forms.CheckBox cb_CamEnable;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_WidthPlus;
        private System.Windows.Forms.Button btn_WidthSub;
        private System.Windows.Forms.Button btn_HighSub;
        private System.Windows.Forms.Button btn_HighPlus;
        private System.Windows.Forms.CheckBox cb_WhitePage;
        private System.Windows.Forms.CheckBox cb_FindByte;
        private System.Windows.Forms.CheckBox cb_BlackPage;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_RecVal1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_RecVal2;
        private System.Windows.Forms.Button btn_TrainMode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_Res;
        private SmartLib.DisplayCtrl WinCtrl;
        private System.Windows.Forms.Label lb_ImgSimilarScore;
    }
}
