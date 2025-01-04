namespace SmartVEye
{
    partial class FrmAuthority
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_CPUSerial = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_BIOSSerial = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_MachineCode = new System.Windows.Forms.TextBox();
            this.btn_Register = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_ValidTime = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_DiskSerial = new System.Windows.Forms.TextBox();
            this.btn_SaveMachineCode = new System.Windows.Forms.Button();
            this.tb_AuthorityCode = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 107);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "CPU序列号:";
            // 
            // tb_CPUSerial
            // 
            this.tb_CPUSerial.Location = new System.Drawing.Point(121, 102);
            this.tb_CPUSerial.Name = "tb_CPUSerial";
            this.tb_CPUSerial.ReadOnly = true;
            this.tb_CPUSerial.Size = new System.Drawing.Size(177, 26);
            this.tb_CPUSerial.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 139);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "主板序列号:";
            // 
            // tb_BIOSSerial
            // 
            this.tb_BIOSSerial.Location = new System.Drawing.Point(121, 134);
            this.tb_BIOSSerial.Name = "tb_BIOSSerial";
            this.tb_BIOSSerial.ReadOnly = true;
            this.tb_BIOSSerial.Size = new System.Drawing.Size(177, 26);
            this.tb_BIOSSerial.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 171);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "机器码：";
            // 
            // tb_MachineCode
            // 
            this.tb_MachineCode.Location = new System.Drawing.Point(121, 166);
            this.tb_MachineCode.Name = "tb_MachineCode";
            this.tb_MachineCode.ReadOnly = true;
            this.tb_MachineCode.Size = new System.Drawing.Size(473, 26);
            this.tb_MachineCode.TabIndex = 1;
            // 
            // btn_Register
            // 
            this.btn_Register.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Register.Location = new System.Drawing.Point(495, 250);
            this.btn_Register.Name = "btn_Register";
            this.btn_Register.Size = new System.Drawing.Size(99, 46);
            this.btn_Register.TabIndex = 2;
            this.btn_Register.Text = "注册";
            this.btn_Register.UseVisualStyleBackColor = true;
            this.btn_Register.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(315, 139);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "授权期限：";
            // 
            // tb_ValidTime
            // 
            this.tb_ValidTime.Location = new System.Drawing.Point(417, 134);
            this.tb_ValidTime.Name = "tb_ValidTime";
            this.tb_ValidTime.ReadOnly = true;
            this.tb_ValidTime.Size = new System.Drawing.Size(177, 26);
            this.tb_ValidTime.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(315, 107);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "硬盘序列号:";
            // 
            // tb_DiskSerial
            // 
            this.tb_DiskSerial.Location = new System.Drawing.Point(417, 102);
            this.tb_DiskSerial.Name = "tb_DiskSerial";
            this.tb_DiskSerial.ReadOnly = true;
            this.tb_DiskSerial.Size = new System.Drawing.Size(177, 26);
            this.tb_DiskSerial.TabIndex = 1;
            // 
            // btn_SaveMachineCode
            // 
            this.btn_SaveMachineCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SaveMachineCode.Location = new System.Drawing.Point(495, 198);
            this.btn_SaveMachineCode.Name = "btn_SaveMachineCode";
            this.btn_SaveMachineCode.Size = new System.Drawing.Size(99, 46);
            this.btn_SaveMachineCode.TabIndex = 2;
            this.btn_SaveMachineCode.Text = "保存机器码";
            this.btn_SaveMachineCode.UseVisualStyleBackColor = true;
            this.btn_SaveMachineCode.Click += new System.EventHandler(this.btn_SaveMachineCode_Click);
            // 
            // tb_AuthorityCode
            // 
            this.tb_AuthorityCode.Location = new System.Drawing.Point(22, 198);
            this.tb_AuthorityCode.Name = "tb_AuthorityCode";
            this.tb_AuthorityCode.Size = new System.Drawing.Size(473, 98);
            this.tb_AuthorityCode.TabIndex = 0;
            this.tb_AuthorityCode.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 50.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(140, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(306, 90);
            this.label7.TabIndex = 4;
            this.label7.Text = "系统注册";
            // 
            // FrmAuthority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 308);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_AuthorityCode);
            this.Controls.Add(this.btn_SaveMachineCode);
            this.Controls.Add(this.btn_Register);
            this.Controls.Add(this.tb_ValidTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_MachineCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_DiskSerial);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_BIOSSerial);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_CPUSerial);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAuthority";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "授权管理";
            this.Load += new System.EventHandler(this.FrmAuthority_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_CPUSerial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_BIOSSerial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_MachineCode;
        private System.Windows.Forms.Button btn_Register;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_ValidTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_DiskSerial;
        private System.Windows.Forms.Button btn_SaveMachineCode;
        private System.Windows.Forms.RichTextBox tb_AuthorityCode;
        private System.Windows.Forms.Label label7;
    }
}