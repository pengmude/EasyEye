using System;
using System.Drawing;
using HalconDotNet;

namespace BaseData
{
	/// <summary>
	/// Բ��ROI
	/// </summary>
    [Serializable]
    public class ROICircle : ROI
	{
		private double radius;
		private double row1, col1;  // first handle
		private double midR, midC;  // second handle

		/// <summary>
		/// ���캯��
		/// </summary>
		public ROICircle()
		{
			NumHandles = 2; // one at corner of circle + midpoint
			activeHandleIdx = 1;
		}


		/// <summary>
		/// ����ROI
		/// </summary>
		/// <param name="roiCenterX">���ĵ�X����</param>
		/// <param name="roiCenterY">���ĵ�Y����</param>
		public override void createROI(double roiCenterX, double roiCenterY)
		{
			midR = roiCenterY;
			midC = roiCenterX;

			radius = 100;

			row1 = midR;
			col1 = midC + radius;
		}
		/// <summary>
		///  ����ROI  
		/// </summary>
		/// <param name="roiCenterX">���ĵ�X����</param>
		/// <param name="roiCenterY">���ĵ�Y����</param>
		/// <param name="rad">Բ�뾶</param>
		public void createROI(double roiCenterX, double roiCenterY, double rad)
		{
			midR = roiCenterY;
			midC = roiCenterX;

			radius = rad;

			row1 = midR;
			col1 = midC + radius;
		}

		/// <summary>
		/// �ڴ����л���ROI
		/// </summary>
		/// <param name="winHandle">�ṩ��halcon����</param>
		public override void draw(HTuple winHandle)
		{
			HOperatorSet.SetLineWidth(winHandle, roiLineWidth);
			HOperatorSet.DispCircle(winHandle, midR, midC, radius);
            HOperatorSet.DispRectangle2(winHandle, row1, col1, 0, 5, 5);
            HOperatorSet.DispRectangle2(winHandle, midR, midC, 0, 5, 5);
		}

		/// <summary>
		/// ���ؾ���ĳ����λ�����ROI�ľ���
		/// Returns the distance of the ROI handle being
		/// closest to the image point(x,y)
		/// </summary>
		/// <param name="x">ָ����X���� column</param>
		/// <param name="y">ָ����Y���� row</param>
		/// <returns>���ؾ���ĳ����λ�����ROI�ľ���</returns>
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
		/// ��ָ�������л���ѡ�е�ROI
		/// </summary>
		/// <param name="winHandle">�ṩ��halcon����</param>
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
		/// ��ȡROI���������
		/// </summary>
		/// <returns>����ROI���������</returns>
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
		/// ��ȡ����ָ����ľ���
		/// </summary>
		/// <param name="row">ָ����row����</param>
		/// <param name="col">ָ����column����</param>
		/// <returns>���ؾ���ָ����ľ���</returns>
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
		/// ��ȡROIģ����Ϣ
		/// </summary>
		/// <returns>����ROIģ����Ϣ</returns>
		public override HTuple getModelData()
		{
			return new HTuple(new double[] { midR, midC, radius });
		}

		/// <summary>
		/// �ƶ�ROI
		/// </summary>
		/// <param name="newX">��λ��X���� column</param>
		/// <param name="newY">��λ��Y���� row</param>
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
		/// ����ROI���ĵ�����
		/// </summary>
		/// <returns>����ROI���ĵ�����</returns>
		public override PointF getCenterPoint()
		{
			return new PointF((float)midR, (float)midC);
		}
	}
}
