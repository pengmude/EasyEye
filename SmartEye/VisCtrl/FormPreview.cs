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
        public void ShowImage(ImageInfo imageInfo)
        {
            if (imageInfo.Image == null)
            {
                pictureBox1.Image = null;
                this.Text = $"NG图像为空！";
                return;
            }
            pictureBox1.Image = imageInfo.Image;
            this.Text = $"NG图查看（图像生成时间{imageInfo.ImageTime}）";
        }
    }

    /// <summary>
    /// 预览图像的信息
    /// </summary>
    public struct ImageInfo 
    {
        public ImageInfo(Image image, string imageTime)
        {
            Image = image;
            ImageTime = imageTime;
        }
        /// <summary>
        /// 图像本身
        /// </summary>
        public Image Image {  get; set; }
        /// <summary>
        /// 图像生成时间
        /// </summary>
        public string ImageTime { get; set; }
    }

}
