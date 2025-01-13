using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartVEye.VisCtrl
{
    public partial class FormPreview : Form
    {
        public FormPreview()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 显示第几张NG图片
        /// </summary>
        /// <param name="index"></param>
        public void ShowImage(Image image)
        {
            pictureBox1.Image = image;
        }
    }
}
