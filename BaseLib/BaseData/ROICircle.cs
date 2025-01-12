using System;
using System.Drawing;
using HalconDotNet;

namespace BaseData
{
	/// <summary>
	/// 圆形ROI
	/// </summary>
    [Serializable]
    public class ROICircle : ROI
	{
		private double radius;
		private double row1, col1;  // first handle
		private double midR, midC;  // second handle

		/// <summary>
		/// 构造函数
		/// </summary>
		public ROICircle()
		{
			NumHandles = 2; // one at corner of circle + midpoint
			activeHandleIdx = 1;
		}


		/// <summary>
		/// 创建ROI
		/// </summary>
		/// <param name="roiCenterX">中心点X坐标</param>
		/// <param name="roiCenterY">中心点Y坐标</param>
		public override void createROI(double roiCenterX, double roiCenterY)
		{
			midR = roiCenterY;
			midC = roiCenterX;

			radius = 100;

			row1 = midR;
			col1 = midC + radius;
		}
		/// <summary>
		///  创建ROI  
		/// </summary>
		/// <param name="roiCenterX">中心点X坐标</param>
		/// <param name="roiCenterY">中心点Y坐标</param>
		/// <param name="rad">圆半径</param>
		public void createROI(double roiCenterX, double roiCenterY, double rad)
		{
			midR = roiCenterY;
			midC = roiCenterX;

			radius = rad;

			row1 = midR;
			col1 = midC + radius;
		}

		/// <summary>
		/// 在窗体中绘制ROI
		/// </summary>
		/// <param name="winHandle">提供的halcon窗体</param>
		public override void draw(HTuple winHandle)
		{
			HOperatorSet.SetLineWidth(winHandle, roiLineWidth);
			HOperatorSet.DispCircle(winHandle, midR, midC, radius);
            HOperatorSet.DispRectangle2(winHandle, row1, col1, 0, 5, 5);
            HOperatorSet.DispRectangle2(winHandle, midR, midC, 0, 5, 5);
		}

		/// <summary>
		/// 返回距离某个点位最近的ROI的距离
		/// Returns the distance of the ROI handle being
		/// closest to the image point(x,y)
		/// </summary>
		/// <param name="x">指定点X坐标 column</param>
		/// <param name="y">指定点Y坐标 row</param>
		/// <returns>返回距离某个点位最近的ROI的距离</returns>
		public override double distToClosestHandle(double x, double y)
		{
			double max = 10000;
			double [] val = new double[NumHandles];

			val[0] = HMisc.DistancePp(y, x, row1, col1); // border handle 
			val[1] = HMisc.DistancePp(y, x, midR, midC); // midpoint 

			for (int i=0; i < NumHandles; i++)
			{
				if (val[i] < max)
				{
					max = val[i];
					activeHandleIdx = i;
				}
			}// end of for 
			return val[activeHandleIdx];
		}

		/// <summary>
		/// 在指定窗口中绘制选中的ROI
		/// </summary>
		/// <param name="winHandle">提供的halcon窗体</param>
		public override void displayActive(HTuple winHandle)
		{

			switch (activeHandleIdx)
			{
				case 0:
                    HOperatorSet.DispRectangle2(winHandle, row1, col1, 0, 5, 5);
					break;
				case 1:
                    HOperatorSet.DispRectangle2(winHandle, midR, midC, 0, 5, 5);
					break;
			}
		}

		/// <summary>
		/// 获取ROI的区域对象
		/// </summary>
		/// <returns>返回ROI的区域对象</returns>
		public override HObject getRegion()
		{
			//HRegion region = new HRegion();
			//region.GenCircle(midR, midC, radius);
			//return region;


			HObject circle;
			HOperatorSet.GenCircle(out circle, midR, midC, radius);
			return circle;
		}

		/// <summary>
		/// 获取距离指定点的距离
		/// </summary>
		/// <param name="row">指定点row坐标</param>
		/// <param name="col">指定点column坐标</param>
		/// <returns>返回距离指定点的距离</returns>
		public override double getDistanceFromStartPoint(double row, double col)
		{
			double sRow = midR; // assumption: we have an angle starting at 0.0
			double sCol = midC + 1 * radius;

			double angle = HMisc.AngleLl(midR, midC, sRow, sCol, midR, midC, row, col);

			if (angle < 0)
				angle += 2 * Math.PI;

			return (radius * angle);
		}

		/// <summary>
		/// 获取ROI模型信息
		/// </summary>
		/// <returns>返回ROI模型信息</returns>
		public override HTuple getModelData()
		{
			return new HTuple(new double[] { midR, midC, radius });
		}

		/// <summary>
		/// 移动ROI
		/// </summary>
		/// <param name="newX">新位置X坐标 column</param>
		/// <param name="newY">新位置Y坐标 row</param>
		public override void moveByHandle(double newX, double newY)
		{
			HTuple distance;
			double shiftX,shiftY;

			switch (activeHandleIdx)
			{
				case 0: // handle at circle border

					row1 = newY;
					col1 = newX;
					HOperatorSet.DistancePp(new HTuple(row1), new HTuple(col1),
											new HTuple(midR), new HTuple(midC),
											out distance);

					radius = distance[0].D;
					break;
				case 1: // midpoint 

					shiftY = midR - newY;
					shiftX = midC - newX;

					midR = newY;
					midC = newX;

					row1 -= shiftY;
					col1 -= shiftX;
					break;
			}
		}

		/// <summary>
		/// 返回ROI中心点坐标
		/// </summary>
		/// <returns>返回ROI中心点坐标</returns>
		public override PointF getCenterPoint()
		{
			return new PointF((float)midR, (float)midC);
		}
	}
}
