
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetting));
            this.dgv_SysParam = new System.Windows.Forms.DataGridView();
            this.tb_SysParamNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_ChnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_SysParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_SysParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_SaveParam = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_CamList = new System.Windows.Forms.ComboBox();
            this.btn_SaveAllCam = new System.Windows.Forms.Button();
            this.dgv_CamParam = new System.Windows.Forms.DataGridView();
            this.tb_CamParamValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_CamParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_ChnNameCam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_NoCam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SysParam)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CamParam)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            this.dgv_SysParam.Size = new System.Drawing.Size(481, 598);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_SysParam);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(489, 620);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "系统参数";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_SaveParam);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 620);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 41);
            this.panel2.TabIndex = 4;
            // 
            // btn_SaveParam
            // 
            this.btn_SaveParam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SaveParam.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_SaveParam.Location = new System.Drawing.Point(877, 0);
            this.btn_SaveParam.Name = "btn_SaveParam";
            this.btn_SaveParam.Size = new System.Drawing.Size(107, 41);
            this.btn_SaveParam.TabIndex = 0;
            this.btn_SaveParam.Text = "保存";
            this.btn_SaveParam.UseVisualStyleBackColor = true;
            this.btn_SaveParam.Click += new System.EventHandler(this.btn_SaveParam_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_SaveAllCam);
            this.panel1.Controls.Add(this.cb_CamList);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(487, 50);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 20F);
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "相机:";
            // 
            // cb_CamList
            // 
            this.cb_CamList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_CamList.Font = new System.Drawing.Font("宋体", 20F);
            this.cb_CamList.FormattingEnabled = true;
            this.cb_CamList.Location = new System.Drawing.Point(93, 7);
            this.cb_CamList.Name = "cb_CamList";
            this.cb_CamList.Size = new System.Drawing.Size(151, 35);
            this.cb_CamList.TabIndex = 1;
            this.cb_CamList.SelectedIndexChanged += new System.EventHandler(this.cb_CamList_SelectedIndexChanged);
            // 
            // btn_SaveAllCam
            // 
            this.btn_SaveAllCam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SaveAllCam.Font = new System.Drawing.Font("宋体", 20F);
            this.btn_SaveAllCam.Location = new System.Drawing.Point(250, 7);
            this.btn_SaveAllCam.Name = "btn_SaveAllCam";
            this.btn_SaveAllCam.Size = new System.Drawing.Size(210, 35);
            this.btn_SaveAllCam.TabIndex = 2;
            this.btn_SaveAllCam.Text = "同步到所有相机";
            this.btn_SaveAllCam.UseVisualStyleBackColor = true;
            this.btn_SaveAllCam.Click += new System.EventHandler(this.btn_SaveAllCam_Click);
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
            this.dgv_CamParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_CamParam.Location = new System.Drawing.Point(4, 69);
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
            this.dgv_CamParam.Size = new System.Drawing.Size(487, 548);
            this.dgv_CamParam.TabIndex = 2;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_CamParam);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(489, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(495, 620);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "相机参数";
            // 
            // FrmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimizeBox = false;
            this.Name = "FrmSetting";
            this.Text = "参数设定";
            this.Shown += new System.EventHandler(this.FrmSetting_Shown);
            this.Resize += new System.EventHandler(this.FrmSetting_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SysParam)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CamParam)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_SysParam;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_SaveParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_SysParamNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_ChnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_SysParamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_SysParamValue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_SaveAllCam;
        private System.Windows.Forms.ComboBox cb_CamList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_CamParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_NoCam;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_ChnNameCam;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_CamParamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn tb_CamParamValue;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}