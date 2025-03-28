﻿using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartVEye.VisCtrl
{
    public partial class PreviewWinCol : UserControl
    {
        private Queue<ImageInfo> imageQueue = new Queue<ImageInfo>(3);// 定义一个只能存放三张NG图像的队列

        public PreviewWinCol()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 开放给外部调用，添加图像
        /// </summary>
        /// <param name="newImage"></param>
        /// <param name="ImageTime"></param>
        public void AddImage(HObject newImage, string ImageTime)
        {
            UpdateImageQueueAndPictureBoxes(HObject2Bitmap8(newImage), ImageTime);
        }

        /// <summary>
        /// 提供外部一键清除全部NG历史图片
        /// </summary>
        public void ClearNgPic()
        {
            if (imageQueue.Count > 0)
            {
                ImageInfo oldestImage = imageQueue.Dequeue();
                oldestImage.Image.Dispose(); // 释放不再使用的图像资源
            }
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
        }

        [DllImport("kernel32.dll")]
        public static extern void CopyMemory(int Destination, int add, int Length);

        /// <summary>
        /// HObject转8位Bitmap(单通道)
        /// </summary>
        /// <param name="image"></param>
        /// <param name="res"></param>

        public static Bitmap HObject2Bitmap8(HObject image)
        {
            Bitmap res;
            HTuple hpoint, type, width, height;
            const int Alpha = 255;
            HOperatorSet.GetImagePointer1(image, out hpoint, out type, out width, out height);
            res = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            ColorPalette pal = res.Palette;
            for (int i = 0; i <= 255; i++)
            { pal.Entries[i] = Color.FromArgb(Alpha, i, i, i); }

            res.Palette = pal; Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bitmapData = res.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            int PixelSize = Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
            IntPtr ptr1 = bitmapData.Scan0;
            IntPtr ptr2 = hpoint; int bytes = width * height;
            byte[] rgbvalues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr2, rgbvalues, 0, bytes);
            System.Runtime.InteropServices.Marshal.Copy(rgbvalues, 0, ptr1, bytes);
            res.UnlockBits(bitmapData);
            return res;

        }

        /// <summary>
        /// 更新NG图像队列
        /// </summary>
        /// <param name="newImage"></param>
        private void UpdateImageQueueAndPictureBoxes(Image newImage, string ImageTime)
        {
            // 如果队列已满，则移除最旧的图像
            if (imageQueue.Count == 3)
            {
                ImageInfo oldestImage = imageQueue.Dequeue();
                oldestImage.Image.Dispose(); // 释放不再使用的图像资源
            }

            // 添加新图像到队列
            imageQueue.Enqueue(new ImageInfo(newImage, ImageTime));

            // 更新 PictureBox 控件以显示最新的三张图像
            UpdatePictureBoxesFromQueue();
        }


        /// <summary>
        /// 使用NG图片队列更新当前NG图的显示
        /// </summary>
        private void UpdatePictureBoxesFromQueue()
        {
            // 获取队列中的所有图像
            List<ImageInfo> imageInfos = imageQueue.ToList();

            // 根据队列中的图像数量更新 PictureBox 控件
            pictureBox1.Image = imageInfos.ElementAtOrDefault(0).Image;
            pictureBox2.Image = imageInfos.ElementAtOrDefault(1).Image;
            pictureBox3.Image = imageInfos.ElementAtOrDefault(2).Image;

            // 确保 PictureBox 的大小模式适应图像
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                FormPreview win = new FormPreview();
                // 获取队列中的所有图像
                List<ImageInfo> imageInfos = imageQueue.ToList();
                // 取列表第1张显示
                win.ShowImage(imageInfos.ElementAtOrDefault(0));
                win.Show();
            }
            catch (Exception)
            {
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                FormPreview win = new FormPreview();
                // 获取队列中的所有图像
                List<ImageInfo> imageInfos = imageQueue.ToList();
                // 取列表第2张显示
                win.ShowImage(imageInfos.ElementAtOrDefault(1));
                win.Show();
            }
            catch (Exception)
            {
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                FormPreview win = new FormPreview();
                // 获取队列中的所有图像
                List<ImageInfo> imageInfos = imageQueue.ToList();
                // 取列表第3张显示
                win.ShowImage(imageInfos.ElementAtOrDefault(2));
                win.Show();
            }
            catch (Exception)
            {
            }
        }
    }
}
