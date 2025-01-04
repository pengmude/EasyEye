using System;
using System.Drawing;
using HalconDotNet;



namespace BaseData
{
	/// <summary>
	/// 矩形ROI2
	/// </summary>
	[Serializable]
    public class ROIRectangle2 : ROI
	{

		/// <summary>Half length of the rectangle side, perpendicular to phi</summary>
		private double length1;

		/// <summary>Half length of the rectangle side, in direction of phi</summary>
		private double length2;

		/// <summary>Row coordinate of midpoint of the rectangle</summary>
		private double midR;

		/// <summary>Column coordinate of midpoint of the rectangle</summary>
		private double midC;

		/// <summary>Orientation of rectangle defined in radians.</summary>
		private double phi;

		//auxiliary variables
		HTuple rowsInit;
		HTuple colsInit;
		HTuple rows;
		HTuple cols;

		HHomMat2D hom2D, tmp;

		/// <summary>
		/// 构造函数
		/// </summary>
		public ROIRectangle2()
		{
			NumHandles = 6; // 4 corners +  1 midpoint + 1 rotationpoint			
			activeHandleIdx = 4;
		}

		/// <summary>
		/// 创建ROI
		/// </summary>
		/// <param name="midX">中心点X坐标</param>
		/// <param name="midY">中心点Y坐标</param>
		public override void createROI(double midX, double midY)
		{
			midR = midY;
			midC = midX;

			length1 = 100;
			length2 = 50;

			phi = 0.0;

			rowsInit = new HTuple(new double[] {-1.0, -1.0, 1.0, 
												   1.0,  0.0, 0.0 });
			colsInit = new HTuple(new double[] {-1.0, 1.0,  1.0, 
												  -1.0, 0.0, 0.6 });
			//order        ul ,  ur,   lr,  ll,   mp, arrowMidpoint
			hom2D = new HHomMat2D();
			tmp = new HHomMat2D();

			updateHandlePos();
		}

		/// <summary>
		/// 创建ROI
		/// </summary>
		/// <param name="midX">中心点X坐标</param>
		/// <param name="midY">中心点Y坐标</param>
		/// <param name="len1">ROI宽度</param>
		/// <param name="len2">ROI高度</param>
		/// <param name="ph">ROI角度</param>
		public void createROI(double midX, double midY, double len1, double len2, double ph)
		{
			midR = midY;
			midC = midX;

			length1 = len1;
			length2 = len2;

			phi = ph;

			rowsInit = new HTuple(new double[] {-1.0, -1.0, 1.0,
			   1.0,  0.0, 0.0 });
			colsInit = new HTuple(new double[] {-1.0, 1.0,  1.0,
			  -1.0, 0.0, 0.6 });
			//order        ul ,  ur,   lr,  ll,   mp, arrowMidpoint
			hom2D = new HHomMat2D();
			tmp = new HHomMat2D();

			updateHandlePos();
		}

		/// <summary>
		/// 在窗体中绘制ROI
		/// </summary>
		/// <param name="winHandle">提供的halcon窗体</param>
		public override void draw(HTuple winHandle)
		{
			HOperatorSet.SetLineWidth(winHandle, roiLineWidth);
			HOperatorSet.DispRectangle2(winHandle, midR, midC, -phi, length1, length2);

 
			for (int i =0; i < NumHandles; i++)
                HOperatorSet.DispRectangle2(winHandle, rows[i].D, cols[i].D, -phi, 5, 5);

            HOperatorSet.DispArrow(winHandle, midR, midC, midR + (Math.Sin(phi) * length1 * 1.2),
                midC + (Math.Cos(phi) * length1 * 1.2), 2.0);

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


			for (int i=0; i < NumHandles; i++)
				val[i] = HMisc.DistancePp(y, x, rows[i].D, cols[i].D);

			for (int i=0; i < NumHandles; i++)
			{
				if (val[i] < max)
				{
					max = val[i];
					activeHandleIdx = i;
				}
			}
			return val[activeHandleIdx];
		}

		/// <summary>
		/// 在指定窗口中绘制选中的ROI
		/// </summary>
		/// <param name="winHandle">提供的halcon窗体</param>
		public override void displayActive(HTuple winHandle)
		{
            HOperatorSet.DispRectangle2(winHandle, rows[activeHandleIdx].D,cols[activeHandleIdx].D,-phi, 5, 5);

			if (activeHandleIdx == 5)
                HOperatorSet.DispArrow(winHandle, midR, midC,midR + (Math.Sin(phi) * length1 * 1.2),midC + (Math.Cos(phi) * length1 * 1.2),2.0);         
		}


		/// <summary>
		/// 获取ROI的区域对象
		/// </summary>
		/// <returns>返回ROI的区域对象</returns>
		public override HObject getRegion()
		{
            //HRegion region = new HRegion();
            //region.GenRectangle2(midR, midC, -phi, length1, length2);
            //return region;

            HObject rect2;
			HOperatorSet.GenRectangle2(out rect2, midR, midC, -phi, length1, length2);
			return rect2;
		}

		/// <summary>
		/// 获取ROI模型信息
		/// </summary>
		/// <returns>返回ROI模型信息</returns>
		public override HTuple getModelData()
		{
			return new HTuple(new double[] { midR, midC, -phi, length1, length2 });
		}

		/// <summary>
		/// 移动ROI
		/// </summary>
		/// <param name="newX">新位置X坐标 column</param>
		/// <param name="newY">新位置Y坐标 row</param>
		public override void moveByHandle(double newX, double newY)
		{
			double vX, vY, x=0, y=0;

			switch (activeHandleIdx)
			{
				case 0:
				case 1:
				case 2:
				case 3:
					tmp = hom2D.HomMat2dInvert();
					x = tmp.AffineTransPoint2d(newX, newY, out y);

					length2 = Math.Abs(y);
					length1 = Math.Abs(x);

					checkForRange(x, y);
					break;
				case 4:
					midC = newX;
					midR = newY;
					break;
				case 5:
					vY = newY - rows[4].D;
					vX = newX - cols[4].D;
					phi = Math.Atan2(vY, vX);
					break;
			}
			updateHandlePos();
		}


		/// <summary>
		/// Auxiliary method to recalculate the contour points of 
		/// the rectangle by transforming the initial row and 
		/// column coordinates (rowsInit, colsInit) by the updated
		/// homography hom2D
		/// </summary>
		private void updateHandlePos()
		{
			hom2D.HomMat2dIdentity();
			hom2D = hom2D.HomMat2dTranslate(midC, midR);
			hom2D = hom2D.HomMat2dRotateLocal(phi);
			tmp = hom2D.HomMat2dScaleLocal(length1, length2);
			cols = tmp.AffineTransPoint2d(colsInit, rowsInit, out rows);
		}


		/* This auxiliary method checks the half lengths 
		 * (length1, length2) using the coordinates (x,y) of the four 
		 * rectangle corners (handles 0 to 3) to avoid 'bending' of 
		 * the rectangular ROI at its midpoint, when it comes to a
		 * 'collapse' of the rectangle for length1=length2=0.
		 * */
		private void checkForRange(double x, double y)
		{
			switch (activeHandleIdx)
			{
				case 0:
					if ((x < 0) && (y < 0))
						return;
					if (x >= 0) length1 = 0.01;
					if (y >= 0) length2 = 0.01;
					break;
				case 1:
					if ((x > 0) && (y < 0))
						return;
					if (x <= 0) length1 = 0.01;
					if (y >= 0) length2 = 0.01;
					break;
				case 2:
					if ((x > 0) && (y > 0))
						return;
					if (x <= 0) length1 = 0.01;
					if (y <= 0) length2 = 0.01;
					break;
				case 3:
					if ((x < 0) && (y > 0))
						return;
					if (x >= 0) length1 = 0.01;
					if (y <= 0) length2 = 0.01;
					break;
				default:
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

	}//end of class
}//end of namespace
