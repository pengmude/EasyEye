using System;
using System.Drawing;
using HalconDotNet;


namespace BaseData
{
    /// <summary>
    /// ROI工具类
    /// </summary>
    [Serializable]
    public class ROI
    {
        /// <summary>
        /// 操作ROI的角点数
        /// </summary>
        protected int NumHandles;
        /// <summary>
        /// 选中ROI的索引号
        /// </summary>
        protected int activeHandleIdx;

        /// <summary>
        /// 定义ROI的标志 'positive' 或 'negative'.
        /// </summary>
        protected int OperatorFlag;

        /// <summary>
        /// ROI线类型
        /// </summary>
        public HTuple flagLineStyle;
        /// <summary>
        /// ROI线宽
        /// </summary>
        public HTuple roiLineWidth = 3;

        /// <summary>
        /// ROI类型 直线ROI
        /// </summary>
        public const int ROI_TYPE_LINE = 10;
        /// <summary>
        /// ROI类型 圆形ROI
        /// </summary>
        public const int ROI_TYPE_CIRCLE = 11;
        /// <summary>
        /// ROI类型 圆弧ROI
        /// </summary>
        public const int ROI_TYPE_CIRCLEARC = 12;
        /// <summary>
        /// ROI类型 长方形1
        /// </summary>
        public const int ROI_TYPE_RECTANCLE1 = 13;
        /// <summary>
        /// ROI类型 长方形2
        /// </summary>
        public const int ROI_TYPE_RECTANGLE2 = 14;

        /// <summary>
        /// 位置操作标志
        /// </summary>
        protected HTuple posOperation = new HTuple();
        /// <summary>
        /// neg操作标志
        /// </summary>
        protected HTuple negOperation = new HTuple(new int[] { 2, 2 });

        /// <summary>
        /// ROI工具类构造函数
        /// </summary>
        public ROI() { }

        /// <summary>
        /// 创建ROI
        /// </summary>
        /// <param name="roiCenterX">ROI X坐标(Column)</param>
        /// <param name="roiCenterY">ROI Y坐标(Row)</param>
        public virtual void createROI(double roiCenterX, double roiCenterY) { }

        /// <summary>
        /// 在窗体中绘制ROI
        /// </summary>
        /// <param name="winHandle">提供的halcon窗体</param>
        public virtual void draw(HTuple winHandle) { }

        /// <summary>
        /// 返回距离某个点位最近的ROI的距离
        /// Returns the distance of the ROI handle being
        /// closest to the image point(x,y)
        /// </summary>
        /// <param name="x">指定点X坐标 column</param>
        /// <param name="y">指定点Y坐标 row</param>
        /// <returns>返回距离某个点位最近的ROI的距离</returns>
        public virtual double distToClosestHandle(double x, double y)
        {
            return 0.0;
        }

        /// <summary>
        /// 在指定窗口中绘制选中的ROI
        /// </summary>
        /// <param name="winHandle">提供的halcon窗体</param>
        public virtual void displayActive(HTuple winHandle) { }

        /// <summary>
        /// 移动ROI
        /// </summary>
        /// <param name="x">指定点X坐标 column</param>
        /// <param name="y">指定点Y坐标 row</param>
        public virtual void moveByHandle(double x, double y) { }

        /// <summary>
        /// 获取ROI的区域对象
        /// </summary>
        /// <returns>返回ROI区域对象</returns>
        public virtual HObject getRegion()
        {
            return null;
        }

        /// <summary>
        /// 获取距离指定点的距离
        /// </summary>
        /// <param name="row">指定点row坐标</param>
        /// <param name="col">指定点column坐标</param>
        /// <returns>距离指定点的距离</returns>
        public virtual double getDistanceFromStartPoint(double row, double col)
        {
            return 0.0;
        }

        /// <summary>
        /// 获取ROI模型信息
        /// </summary>
        /// <returns>返回ROI模型信息</returns>
        public virtual HTuple getModelData()
        {
            return null;
        }

        /// <summary>
        /// 获取定义ROI的句柄数
        /// </summary>
        /// <returns>定义ROI的句柄数</returns>
        public int getNumHandles()
        {
            return NumHandles;
        }

        /// <summary>
        /// 获取选中ROI的句柄索引
        /// </summary>
        /// <returns>选中ROI的句柄索引</returns>
        public int getActHandleIdx()
        {
            return activeHandleIdx;
        }

        /// <summary>
        /// 获取ROI的标志符
        /// Gets the sign of the ROI object, being either 
        /// 'positive' or 'negative'. This sign is used when creating a model
        /// region for matching applications from a list of ROIs.
        /// </summary>
        /// <returns>返回ROI的标志符</returns>
        public int getOperatorFlag()
        {
            return OperatorFlag;
        }

        /// <summary>
        /// 返回ROI中心点坐标
        /// </summary>
        /// <returns>返回ROI中心点坐标</returns>
        public virtual PointF getCenterPoint()
        {
            return new PointF(0.0f,0.0f);
        }
    }
}
