
namespace SmartVEye
{
    partial class FrmTrain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_InfoBox = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tb_PWD = new System.Windows.Forms.TextBox();
            this.btn_UserParam = new System.Windows.Forms.Button();
            this.cb_DrawROI = new System.Windows.Forms.CheckBox();
            this.tb_MinScore = new System.Windows.Forms.NumericUpDown();
            this.tb_ScaleMax = new System.Windows.Forms.NumericUpDown();
            this.tb_Gain = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_AngleEnd = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_ScaleMin = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_ExposureTime = new System.Windows.Forms.NumericUpDown();
            this.tb_AngleStart = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Snap = new System.Windows.Forms.Button();
            this.btn_Test = new System.Windows.Forms.Button();
            this.btn_Train = new System.Windows.Forms.Button();
            this.WinCtrl = new SmartLib.DisplayCtrl();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_MinScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_ScaleMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Gain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_AngleEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_ScaleMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_ExposureTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_AngleStart)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tb_InfoBox);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(554, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 468);
            this.panel1.TabIndex = 1;
            // 
            // tb_InfoBox
            // 
            this.tb_InfoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_InfoBox.Location = new System.Drawing.Point(0, 341);
            this.tb_InfoBox.Name = "tb_InfoBox";
            this.tb_InfoBox.Size = new System.Drawing.Size(210, 127);
            this.tb_InfoBox.TabIndex = 1;
            this.tb_InfoBox.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tb_PWD);
            this.panel2.Controls.Add(this.btn_UserParam);
            this.panel2.Controls.Add(this.cb_DrawROI);
            this.panel2.Controls.Add(this.tb_MinScore);
            this.panel2.Controls.Add(this.tb_ScaleMax);
            this.panel2.Controls.Add(this.tb_Gain);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.tb_AngleEnd);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.tb_ScaleMin);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.tb_ExposureTime);
            this.panel2.Controls.Add(this.tb_AngleStart);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btn_Snap);
            this.panel2.Controls.Add(this.btn_Test);
            this.panel2.Controls.Add(this.btn_Train);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(210, 341);
            this.panel2.TabIndex = 2;
            // 
            // tb_PWD
            // 
            this.tb_PWD.Location = new System.Drawing.Point(89, 187);
            this.tb_PWD.Name = "tb_PWD";
            this.tb_PWD.Size = new System.Drawing.Size(109, 21);
            this.tb_PWD.TabIndex = 5;
            // 
            // btn_UserParam
            // 
            this.btn_UserParam.Location = new System.Drawing.Point(123, 158);
            this.btn_UserParam.Name = "btn_UserParam";
            this.btn_UserParam.Size = new System.Drawing.Size(75, 23);
            this.btn_UserParam.TabIndex = 4;
            this.btn_UserParam.Text = "自定义参数";
            this.btn_UserParam.UseVisualStyleBackColor = true;
            this.btn_UserParam.Click += new System.EventHandler(this.btn_UserParam_Click);
            // 
            // cb_DrawROI
            // 
            this.cb_DrawROI.AutoSize = true;
            this.cb_DrawROI.Location = new System.Drawing.Point(21, 162);
            this.cb_DrawROI.Name = "cb_DrawROI";
            this.cb_DrawROI.Size = new System.Drawing.Size(84, 16);
            this.cb_DrawROI.TabIndex = 3;
            this.cb_DrawROI.Text = "自定义区域";
            this.cb_DrawROI.UseVisualStyleBackColor = true;
            // 
            // tb_MinScore
            // 
            this.tb_MinScore.DecimalPlaces = 2;
            this.tb_MinScore.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.tb_MinScore.Location = new System.Drawing.Point(89, 311);
            this.tb_MinScore.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tb_MinScore.Name = "tb_MinScore";
            this.tb_MinScore.Size = new System.Drawing.Size(109, 21);
            this.tb_MinScore.TabIndex = 2;
            this.tb_MinScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_MinScore.Value = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            this.tb_MinScore.Visible = false;
            // 
            // tb_ScaleMax
            // 
            this.tb_ScaleMax.DecimalPlaces = 1;
            this.tb_ScaleMax.Location = new System.Drawing.Point(89, 287);
            this.tb_ScaleMax.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            this.tb_ScaleMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tb_ScaleMax.Name = "tb_ScaleMax";
            this.tb_ScaleMax.Size = new System.Drawing.Size(109, 21);
            this.tb_ScaleMax.TabIndex = 2;
            this.tb_ScaleMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_ScaleMax.Value = new decimal(new int[] {
            11,
            0,
            0,
            65536});
            this.tb_ScaleMax.Visible = false;
            // 
            // tb_Gain
            // 
            this.tb_Gain.DecimalPlaces = 2;
            this.tb_Gain.Location = new System.Drawing.Point(89, 84);
            this.tb_Gain.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.tb_Gain.Name = "tb_Gain";
            this.tb_Gain.Size = new System.Drawing.Size(109, 21);
            this.tb_Gain.TabIndex = 2;
            this.tb_Gain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 191);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "权限密码：";
            // 
            // tb_AngleEnd
            // 
            this.tb_AngleEnd.Location = new System.Drawing.Point(89, 238);
            this.tb_AngleEnd.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.tb_AngleEnd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tb_AngleEnd.Name = "tb_AngleEnd";
            this.tb_AngleEnd.Size = new System.Drawing.Size(109, 21);
            this.tb_AngleEnd.TabIndex = 2;
            this.tb_AngleEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_AngleEnd.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.tb_AngleEnd.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 316);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "匹配度：";
            this.label5.Visible = false;
            // 
            // tb_ScaleMin
            // 
            this.tb_ScaleMin.DecimalPlaces = 1;
            this.tb_ScaleMin.Location = new System.Drawing.Point(89, 263);
            this.tb_ScaleMin.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tb_ScaleMin.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.tb_ScaleMin.Name = "tb_ScaleMin";
            this.tb_ScaleMin.Size = new System.Drawing.Size(109, 21);
            this.tb_ScaleMin.TabIndex = 2;
            this.tb_ScaleMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_ScaleMin.Value = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            this.tb_ScaleMin.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 292);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "最大缩放：";
            this.label4.Visible = false;
            // 
            // tb_ExposureTime
            // 
            this.tb_ExposureTime.Location = new System.Drawing.Point(89, 60);
            this.tb_ExposureTime.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.tb_ExposureTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tb_ExposureTime.Name = "tb_ExposureTime";
            this.tb_ExposureTime.Size = new System.Drawing.Size(109, 21);
            this.tb_ExposureTime.TabIndex = 2;
            this.tb_ExposureTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_ExposureTime.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // tb_AngleStart
            // 
            this.tb_AngleStart.Location = new System.Drawing.Point(89, 214);
            this.tb_AngleStart.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.tb_AngleStart.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.tb_AngleStart.Name = "tb_AngleStart";
            this.tb_AngleStart.Size = new System.Drawing.Size(109, 21);
            this.tb_AngleStart.TabIndex = 2;
            this.tb_AngleStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_AngleStart.Value = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.tb_AngleStart.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "相机增益：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 268);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "最小缩放：";
            this.label3.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "曝光时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "终止范围：";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "起始角度：";
            this.label1.Visible = false;
            // 
            // btn_Snap
            // 
            this.btn_Snap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Snap.Location = new System.Drawing.Point(18, 11);
            this.btn_Snap.Name = "btn_Snap";
            this.btn_Snap.Size = new System.Drawing.Size(180, 43);
            this.btn_Snap.TabIndex = 0;
            this.btn_Snap.Text = "相机拍照";
            this.btn_Snap.UseVisualStyleBackColor = true;
            this.btn_Snap.Click += new System.EventHandler(this.btn_Snap_Click);
            // 
            // btn_Test
            // 
            this.btn_Test.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Test.Location = new System.Drawing.Point(111, 113);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(87, 43);
            this.btn_Test.TabIndex = 0;
            this.btn_Test.Text = "测试";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // btn_Train
            // 
            this.btn_Train.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Train.Location = new System.Drawing.Point(18, 113);
            this.btn_Train.Name = "btn_Train";
            this.btn_Train.Size = new System.Drawing.Size(87, 43);
            this.btn_Train.TabIndex = 0;
            this.btn_Train.Text = "学习模板";
            this.btn_Train.UseVisualStyleBackColor = true;
            this.btn_Train.Click += new System.EventHandler(this.btn_Train_Click);
            // 
            // WinCtrl
            // 
            this.WinCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WinCtrl.Location = new System.Drawing.Point(0, 0);
            this.WinCtrl.Name = "WinCtrl";
            this.WinCtrl.Size = new System.Drawing.Size(554, 468);
            this.WinCtrl.TabIndex = 9;
            // 
            // FrmTrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 468);
            this.Controls.Add(this.WinCtrl);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmTrain";
            this.Text = "SmartEye";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTrain_FormClosing);
            this.Load += new System.EventHandler(this.FrmTrain_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_MinScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_ScaleMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Gain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_AngleEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_ScaleMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_ExposureTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_AngleStart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Snap;
        private System.Windows.Forms.Button btn_Train;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox tb_InfoBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown tb_AngleEnd;
        private System.Windows.Forms.NumericUpDown tb_AngleStart;
        private System.Windows.Forms.NumericUpDown tb_ScaleMax;
        private System.Windows.Forms.NumericUpDown tb_ScaleMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown tb_MinScore;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cb_DrawROI;
        private System.Windows.Forms.Button btn_UserParam;
        private System.Windows.Forms.NumericUpDown tb_Gain;
        private System.Windows.Forms.NumericUpDown tb_ExposureTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_PWD;
        private System.Windows.Forms.Button btn_Test;
        private SmartLib.DisplayCtrl WinCtrl;
    }
}