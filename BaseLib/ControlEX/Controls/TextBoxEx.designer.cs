
namespace SmartLib
{
    partial class TextBoxEx
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
            components = new System.ComponentModel.Container();
        }

        private void SomeComponent()
        {
            this.p6 = new System.Windows.Forms.Panel();
            this.TheTextBox = new System.Windows.Forms.TextBox();
            this.p5 = new System.Windows.Forms.Panel();
            this.LabelHint = new System.Windows.Forms.Label();
            this.p4 = new System.Windows.Forms.Panel();
            this.p3 = new System.Windows.Forms.Panel();
            this.p2 = new System.Windows.Forms.Panel();
            this.p6.SuspendLayout();
            this.SuspendLayout();
            // 
            // p1
            // 
            this.Controls.Add(this.p6);
            this.Controls.Add(this.p5);
            this.Controls.Add(this.LabelHint);
            this.Controls.Add(this.p4);
            this.Controls.Add(this.p3);
            this.Controls.Add(this.p2);
            // 
            // p6
            // 
            this.p6.Controls.Add(this.TheTextBox);
            this.p6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p6.Location = new System.Drawing.Point(8, 19);
            this.p6.Name = "p6";
            this.p6.Size = new System.Drawing.Size(117, 19);
            this.p6.TabIndex = 5;
            // 
            // TheTextBox
            // 
            this.TheTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TheTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TheTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TheTextBox.Location = new System.Drawing.Point(0, 0);
            this.TheTextBox.Font = new System.Drawing.Font("宋体", 12F);
            this.TheTextBox.Multiline = true;
            this.TheTextBox.Name = "TheTextBox";
            this.TheTextBox.Size = new System.Drawing.Size(117, 19);
            this.TheTextBox.TabIndex = 0;
            // 
            // p5
            // 
            this.p5.Dock = System.Windows.Forms.DockStyle.Top;
            this.p5.Location = new System.Drawing.Point(8, 14);
            this.p5.Name = "p5";
            this.p5.Size = new System.Drawing.Size(117, 1);
            this.p5.TabIndex = 4;
            // 
            // LabelHint
            // 
            this.LabelHint.AutoSize = true;
            this.LabelHint.Dock = System.Windows.Forms.DockStyle.Top;
            this.LabelHint.Location = new System.Drawing.Point(8, 2);
            this.LabelHint.Name = "LabelHint";
            this.LabelHint.Size = new System.Drawing.Size(41, 12);
            this.LabelHint.TabIndex = 3;
            this.LabelHint.Text = "label1";
            this.LabelHint.Font = new System.Drawing.Font("宋体", 9F);
            this.LabelHint.ForeColor = System.Drawing.SystemColors.WindowFrame;
            // 
            // p4
            // 
            this.p4.Dock = System.Windows.Forms.DockStyle.Top;
            this.p4.Location = new System.Drawing.Point(8, 0);
            this.p4.Name = "p4";
            this.p4.Size = new System.Drawing.Size(169, 1);
            this.p4.TabIndex = 2;
            // 
            // p3
            // 
            this.p3.Dock = System.Windows.Forms.DockStyle.Left;
            this.p3.Location = new System.Drawing.Point(0, 0);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(3, 38);
            this.p3.TabIndex = 1;
            // 
            // p2
            // 
            this.p2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.p2.Location = new System.Drawing.Point(0, 38);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(117, 2);
            this.p2.TabIndex = 0;
            // 
            // TextBoxEx3
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Name = "TextBoxEx";
            this.Size = new System.Drawing.Size(117, 37);
            this.p6.ResumeLayout(false);
            this.p6.PerformLayout();
            this.ResumeLayout(false);
            //
            this.SeletColor = System.Drawing.Color.Blue;
            this.AllBackColor = System.Drawing.SystemColors.ControlLight;
            this.LineColor= System.Drawing.SystemColors.ControlText;

            p5.Click += TextBoxEx2_Click;
            LabelHint.Click += TextBoxEx2_Click;
            p2.Click += TextBoxEx2_Click;
            p3.Click += TextBoxEx2_Click;
            p4.Click += TextBoxEx2_Click;
            p5.Click += TextBoxEx2_Click;
            p6.Click += TextBoxEx2_Click;
            this.Click += TextBoxEx2_Click;
            TheTextBox.KeyPress += TheTextBox_KeyPress;
            TheTextBox.Leave += TheTextBox_Leave;
            TheTextBox.Enter += TheTextBox_Enter;
            if (TheTextBox.Text == "")
                HideTextBox();
        }

        #endregion

        private System.Windows.Forms.Panel p3;
        private System.Windows.Forms.Panel p2;
        private System.Windows.Forms.Panel p4;
        private System.Windows.Forms.Panel p5;
        private System.Windows.Forms.Label LabelHint;
        private System.Windows.Forms.Panel p6;
        private System.Windows.Forms.TextBox TheTextBox;
    }
}
