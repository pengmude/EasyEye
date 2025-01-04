using HalconDotNet;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartLib
{
    internal partial class LargeView : Form
    {
        HMouseEventHandler _hMouseWheelEvent;
        Action<Rectangle> SetRectangleEvent;
        public LargeView(Rectangle viewport, out HTuple view_handle, HMouseEventHandler hMouseWheelEvent, ref Action<Rectangle> act)
        {
            InitializeComponent();
            hWindowControl1.ImagePart = viewport;
            view_handle = hWindowControl1.HalconWindow;
            hWindowControl1.HMouseWheel += hMouseWheelEvent;
            _hMouseWheelEvent = hMouseWheelEvent;
            act += SetRectangle;
            SetRectangleEvent = act;
        }

        public void SetRectangle(Rectangle viewport)
        {
            hWindowControl1.ImagePart = viewport;
        }

        private void LargeView_FormClosing(object sender, FormClosingEventArgs e)
        {
            hWindowControl1.HMouseWheel -= _hMouseWheelEvent;
            SetRectangleEvent -= SetRectangle;
        }
    }
}
