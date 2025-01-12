
namespace SmartVEye
{
    partial class FrmSetting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetting));
            this.dgv_SysParam = new System.Windows.Forms.DataGridView();
            this.tb_SysParamNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_ChnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_SysParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_SysParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox_System = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_SaveParam = new System.Windows.Forms.Button();
            this.btn_SaveAllCam = new System.Windows.Forms.Button();
            this.cb_CamList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_CamParam = new System.Windows.Forms.DataGridView();
            this.tb_NoCam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_ChnNameCam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_CamParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_CamParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox_Cam = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel_CamSet = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_Base = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SysParam)).BeginInit();
            this.groupBox_System.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CamParam)).BeginInit();
            this.groupBox_Cam.SuspendLayout();
            this.tableLayoutPanel_CamSet.SuspendLayout();
            this.tableLayoutPanel_Base.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_SysParam
            // 
            this.dgv_SysParam.AllowUserToAddRows = false;
            this.dgv_SysParam.AllowUserToDeleteRows = false;
            this.dgv_SysParam.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_SysParam.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_SysParam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_SysParam.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tb_SysParamNo,
            this.tb_ChnName,
            this.tb_SysParamName,
            this.tb_SysParamValue});
            this.dgv_SysParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_SysParam.Location = new System.Drawing.Point(4, 19);
            this.dgv_SysParam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgv_SysParam.Name = "dgv_SysParam";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 10F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_SysParam.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_SysParam.RowHeadersVisible = false;
            this.dgv_SysParam.RowTemplate.Height = 23;
            this.dgv_SysParam.Size = new System.Drawing.Size(468, 724);
            this.dgv_SysParam.TabIndex = 0;
            // 
            // tb_SysParamNo
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 15F);
            this.tb_SysParamNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.tb_SysParamNo.HeaderText = "序号";
            this.tb_SysParamNo.MinimumWidth = 50;
            this.tb_SysParamNo.Name = "tb_SysParamNo";
            this.tb_SysParamNo.ReadOnly = true;
            this.tb_SysParamNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tb_SysParamNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tb_SysParamNo.Width = 50;
            // 
            // tb_ChnName
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 15F);
            this.tb_ChnName.DefaultCellStyle = dataGridViewCellStyle3;
            this.tb_ChnName.HeaderText = "含义";
            this.tb_ChnName.MinimumWidth = 50;
            this.tb_ChnName.Name = "tb_ChnName";
            this.tb_ChnName.ReadOnly = true;
            this.tb_ChnName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tb_ChnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tb_ChnName.Width = 180;
            // 
            // tb_SysParamName
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 15F);
            this.tb_SysParamName.DefaultCellStyle = dataGridViewCellStyle4;
            this.tb_SysParamName.HeaderText = "参数名";
            this.tb_SysParamName.MinimumWidth = 80;
            this.tb_SysParamName.Name = "tb_SysParamName";
            this.tb_SysParamName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tb_SysParamName.Width = 140;
            // 
            // tb_SysParamValue
            // 
            this.tb_SysParamValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 15F);
            this.tb_SysParamValue.DefaultCellStyle = dataGridViewCellStyle5;
            this.tb_SysParamValue.HeaderText = "参数值";
            this.tb_SysParamValue.Name = "tb_SysParamValue";
            this.tb_SysParamValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox_System
            // 
            this.groupBox_System.Controls.Add(this.dgv_SysParam);
            this.groupBox_System.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_System.Location = new System.Drawing.Point(4, 3);
            this.groupBox_System.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox_System.Name = "groupBox_System";
            this.groupBox_System.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox_System.Size = new System.Drawing.Size(476, 746);
            this.groupBox_System.TabIndex = 1;
            this.groupBox_System.TabStop = false;
            this.groupBox_System.Text = "系统参数";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_SaveParam);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 752);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(969, 41);
            this.panel2.TabIndex = 4;
            // 
            // btn_SaveParam
            // 
            this.btn_SaveParam.BackColor = System.Drawing.Color.Aqua;
            this.btn_SaveParam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SaveParam.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_SaveParam.Location = new System.Drawing.Point(862, 0);
            this.btn_SaveParam.Name = "btn_SaveParam";
            this.btn_SaveParam.Size = new System.Drawing.Size(107, 41);
            this.btn_SaveParam.TabIndex = 0;
            this.btn_SaveParam.Text = "保存";
            this.btn_SaveParam.UseVisualStyleBackColor = false;
            this.btn_SaveParam.Click += new System.EventHandler(this.btn_SaveParam_Click);
            // 
            // btn_SaveAllCam
            // 
            this.btn_SaveAllCam.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_SaveAllCam.BackColor = System.Drawing.Color.Aqua;
            this.btn_SaveAllCam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SaveAllCam.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveAllCam.Location = new System.Drawing.Point(275, 7);
            this.btn_SaveAllCam.Name = "btn_SaveAllCam";
            this.btn_SaveAllCam.Size = new System.Drawing.Size(176, 42);
            this.btn_SaveAllCam.TabIndex = 2;
            this.btn_SaveAllCam.Text = "同步到所有相机";
            this.btn_SaveAllCam.UseVisualStyleBackColor = false;
            this.btn_SaveAllCam.Click += new System.EventHandler(this.btn_SaveAllCam_Click);
            // 
            // cb_CamList
            // 
            this.cb_CamList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cb_CamList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_CamList.Font = new System.Drawing.Font("宋体", 20F);
            this.cb_CamList.FormattingEnabled = true;
            this.cb_CamList.Location = new System.Drawing.Point(99, 10);
            this.cb_CamList.Name = "cb_CamList";
            this.cb_CamList.Size = new System.Drawing.Size(151, 35);
            this.cb_CamList.TabIndex = 1;
            this.cb_CamList.SelectedIndexChanged += new System.EventHandler(this.cb_CamList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 20F);
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "相机:";
            // 
            // dgv_CamParam
            // 
            this.dgv_CamParam.AllowUserToAddRows = false;
            this.dgv_CamParam.AllowUserToDeleteRows = false;
            this.dgv_CamParam.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 10F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_CamParam.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_CamParam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_CamParam.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tb_NoCam,
            this.tb_ChnNameCam,
            this.tb_CamParamName,
            this.tb_CamParamValue});
            this.tableLayoutPanel_CamSet.SetColumnSpan(this.dgv_CamParam, 3);
            this.dgv_CamParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_CamParam.Location = new System.Drawing.Point(4, 59);
            this.dgv_CamParam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgv_CamParam.Name = "dgv_CamParam";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 10F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_CamParam.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgv_CamParam.RowHeadersVisible = false;
            this.dgv_CamParam.RowTemplate.Height = 23;
            this.dgv_CamParam.Size = new System.Drawing.Size(461, 662);
            this.dgv_CamParam.TabIndex = 2;
            // 
            // tb_NoCam
            // 
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 15F);
            this.tb_NoCam.DefaultCellStyle = dataGridViewCellStyle8;
            this.tb_NoCam.HeaderText = "序号";
            this.tb_NoCam.MinimumWidth = 50;
            this.tb_NoCam.Name = "tb_NoCam";
            this.tb_NoCam.ReadOnly = true;
            this.tb_NoCam.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tb_NoCam.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tb_NoCam.Width = 50;
            // 
            // tb_ChnNameCam
            // 
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 15F);
            this.tb_ChnNameCam.DefaultCellStyle = dataGridViewCellStyle9;
            this.tb_ChnNameCam.HeaderText = "含义";
            this.tb_ChnNameCam.MinimumWidth = 50;
            this.tb_ChnNameCam.Name = "tb_ChnNameCam";
            this.tb_ChnNameCam.ReadOnly = true;
            this.tb_ChnNameCam.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tb_ChnNameCam.Width = 180;
            // 
            // tb_CamParamName
            // 
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 15F);
            this.tb_CamParamName.DefaultCellStyle = dataGridViewCellStyle10;
            this.tb_CamParamName.HeaderText = "参数名";
            this.tb_CamParamName.MinimumWidth = 50;
            this.tb_CamParamName.Name = "tb_CamParamName";
            this.tb_CamParamName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tb_CamParamName.Width = 140;
            // 
            // tb_CamParamValue
            // 
            this.tb_CamParamValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 15F);
            this.tb_CamParamValue.DefaultCellStyle = dataGridViewCellStyle11;
            this.tb_CamParamValue.HeaderText = "参数值";
            this.tb_CamParamValue.Name = "tb_CamParamValue";
            this.tb_CamParamValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox_Cam
            // 
            this.groupBox_Cam.Controls.Add(this.tableLayoutPanel_CamSet);
            this.groupBox_Cam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Cam.Location = new System.Drawing.Point(488, 3);
            this.groupBox_Cam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox_Cam.Name = "groupBox_Cam";
            this.groupBox_Cam.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox_Cam.Size = new System.Drawing.Size(477, 746);
            this.groupBox_Cam.TabIndex = 3;
            this.groupBox_Cam.TabStop = false;
            this.groupBox_Cam.Text = "相机参数";
            // 
            // tableLayoutPanel_CamSet
            // 
            this.tableLayoutPanel_CamSet.ColumnCount = 3;
            this.tableLayoutPanel_CamSet.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel_CamSet.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel_CamSet.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel_CamSet.Controls.Add(this.btn_SaveAllCam, 2, 0);
            this.tableLayoutPanel_CamSet.Controls.Add(this.dgv_CamParam, 0, 1);
            this.tableLayoutPanel_CamSet.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel_CamSet.Controls.Add(this.cb_CamList, 1, 0);
            this.tableLayoutPanel_CamSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_CamSet.Location = new System.Drawing.Point(4, 19);
            this.tableLayoutPanel_CamSet.Name = "tableLayoutPanel_CamSet";
            this.tableLayoutPanel_CamSet.RowCount = 2;
            this.tableLayoutPanel_CamSet.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel_CamSet.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_CamSet.Size = new System.Drawing.Size(469, 724);
            this.tableLayoutPanel_CamSet.TabIndex = 3;
            // 
            // tableLayoutPanel_Base
            // 
            this.tableLayoutPanel_Base.ColumnCount = 2;
            this.tableLayoutPanel_Base.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Base.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Base.Controls.Add(this.groupBox_System, 0, 0);
            this.tableLayoutPanel_Base.Controls.Add(this.groupBox_Cam, 1, 0);
            this.tableLayoutPanel_Base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Base.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Base.Name = "tableLayoutPanel_Base";
            this.tableLayoutPanel_Base.RowCount = 1;
            this.tableLayoutPanel_Base.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Base.Size = new System.Drawing.Size(969, 752);
            this.tableLayoutPanel_Base.TabIndex = 5;
            // 
            // FrmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 793);
            this.Controls.Add(this.tableLayoutPanel_Base);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimizeBox = false;
            this.Name = "FrmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数设定";
            this.Shown += new System.EventHandler(this.FrmSetting_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SysParam)).EndInit();
            this.groupBox_System.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CamParam)).EndInit();
            this.groupBox_Cam.ResumeLayout(false);
            this.tableLayoutPanel_CamSet.ResumeLayout(false);
            this.tableLayoutPanel_CamSet.PerformLayout();
            this.tableLayoutPanel_Base.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_SysParam;
        private System.Windows.Forms.GroupBox groupBox_System;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_SaveParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_SysParamNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_ChnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_SysParamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_SysParamValue;
        private System.Windows.Forms.Button btn_SaveAllCam;
        private System.Windows.Forms.ComboBox cb_CamList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_CamParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_NoCam;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_ChnNameCam;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_CamParamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_CamParamValue;
        private System.Windows.Forms.GroupBox groupBox_Cam;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_CamSet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Base;
    }
}