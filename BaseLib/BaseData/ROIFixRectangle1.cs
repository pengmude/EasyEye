using System;
using System.Drawing;
using HalconDotNet;


namespace BaseData
{
	/// <summary>
	/// 固定尺寸ROI
	/// </summary>
	[Serializable]
	public class ROIFixRectangle1 : ROI
	{

		private double row1, col1;   // upper left
		private double row2, col2;   // lower right 
		private double midR, midC;   // midpoint 


		/// <summary>
		/// 构造函数
		/// </summary>
		public ROIFixRectangle1()
		{

			NumHandles = 5; // 4 corner points + midpoint
			activeHandleIdx = 4;
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

			row1 = midR - 50;
			col1 = midC - 50;
			row2 = midR + 50;
			col2 = midC + 50;
		}

		/// <summary>
		/// 创建矩形1 ROI
		/// </summary>
		/// <param name="roiCenterX">中心点X坐标</param>
		/// <param name="roiCenterY">中心点Y坐标</param>
		/// <param name="len1">ROI宽度</param>
		/// <param name="len2">ROI高度</param>
		public void createROI(double roiCenterX, double roiCenterY, double len1, double len2)
		{
			midR = roiCenterY;
			midC = roiCenterX;

			row1 = midR - (len2 / 2);
			col1 = midC - (len1 / 2);
			row2 = midR + (len2 / 2);
			col2 = midC + (len1 / 2);
		}

		/// <summary>
		/// 在窗体中绘制ROI
		/// </summary>
		/// <param name="winHandle">提供的halcon窗体</param>
		public override void draw(HTuple winHandle)
		{
			HOperatorSet.SetLineWidth(winHandle, roiLineWidth);
			HOperatorSet.DispRectangle1(winHandle, row1, col1, row2, col2);
			// 显示四个顶点小矩形
			//HOperatorSet.DispRectangle2(winHandle, midR, midC, 0, 5, 5);
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
			double[] val = new double[NumHandles];

			midR = ((row2 - row1) / 2) + row1;
			midC = ((col2 - col1) / 2) + col1;

			val[0] = HMisc.DistancePp(y, x, row1, col1); // upper left 
			val[1] = HMisc.DistancePp(y, x, row1, col2); // upper right 
			val[2] = HMisc.DistancePp(y, x, row2, col2); // lower right 
			val[3] = HMisc.DistancePp(y, x, row2, col1); // lower left 
			val[4] = HMisc.DistancePp(y, x, midR, midC); // midpoint 

			for (int i = 0; i < NumHandles; i++)
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
				//case 0:
				//	HOperatorSet.DispRectangle2(winHandle, row1, col1, 0, 5, 5);
				//	break;
				//case 1:
				//	HOperatorSet.DispRectangle2(winHandle, row1, col2, 0, 5, 5);
				//	break;
				//case 2:
				//	HOperatorSet.DispRectangle2(winHandle, row2, col2, 0, 5, 5);
				//	break;
				//case 3:
				//	HOperatorSet.DispRectangle2(winHandle, row2, col1, 0, 5, 5);
				//	break;
				case 4:
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
			//region.GenRectangle1(row1, col1, row2, col2);
			//return region;

			HObject rect1;
			HOperatorSet.GenRectangle1(out rect1, row1, col1, row2, col2);
			return rect1;
		}

		/// <summary>
		/// 获取ROI模型信息
		/// </summary>
		/// <returns>返回ROI模型信息</returns>
		public override HTuple getModelData()
		{
			return new HTuple(new double[] { row1, col1, row2, col2 });
		}


		/// <summary>
		/// 移动ROI
		/// </summary>
		/// <param name="newX">新位置X坐标 column</param>
		/// <param name="newY">新位置Y坐标 row</param>
		public override void moveByHandle(double newX, double newY)
		{
			double len1, len2;
			double tmp;

			switch (activeHandleIdx)
			{
				//case 0: // upper left 
				//	row1 = newY;
				//	col1 = newX;
				//	break;
				//case 1: // upper right 
				//	row1 = newY;
				//	col2 = newX;
				//	break;
				//case 2: // lower right 
				//	row2 = newY;
				//	col2 = newX;
				//	break;
				//case 3: // lower left
				//	row2 = newY;
				//	col1 = newX;
				//	break;
				case 4: // midpoint 
					len1 = ((row2 - row1) / 2);
					len2 = ((col2 - col1) / 2);

					row1 = newY - len1;
					row2 = newY + len1;

					col1 = newX - len2;
					col2 = newX + len2;

					break;
			}

			if (row2 <= row1)
			{
				tmp = row1;
				row1 = row2;
				row2 = tmp;
			}

			if (col2 <= col1)
			{
				tmp = col1;
				col1 = col2;
				col2 = tmp;
			}

			midR = ((row2 - row1) / 2) + row1;
			midC = ((col2 - col1) / 2) + col1;

		}


		/// <summary>
		/// 返回ROI中心点坐标
		/// </summary>
		/// <returns>返回ROI中心点坐标</returns>
		public override PointF getCenterPoint()
		{
			return new PointF((float)midR, (float)midC);
		}
	}//end of class
}//end of namespace
