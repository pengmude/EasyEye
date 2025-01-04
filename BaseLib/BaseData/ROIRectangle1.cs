using System;
using System.Drawing;
using HalconDotNet;


namespace BaseData
{
	/// <summary>
	/// ����ROI1
	/// </summary>
	[Serializable]
    public class ROIRectangle1 : ROI
	{

		private double row1, col1;   // upper left
		private double row2, col2;   // lower right 
		private double midR, midC;   // midpoint 


		/// <summary>
		/// ���캯��
		/// </summary>
		public ROIRectangle1()
		{
			NumHandles = 5; // 4 corner points + midpoint
			activeHandleIdx = 4;
		}

		/// <summary>
		/// ����ROI
		/// </summary>
		/// <param name="midX">���ĵ�X����</param>
		/// <param name="midY">���ĵ�Y����</param>
		public override void createROI(double midX, double midY)
		{
			midR = midY;
			midC = midX;

			row1 = midR - 50;
			col1 = midC - 50;
			row2 = midR + 50;
			col2 = midC + 50;
		}

		/// <summary>
		/// ����ROI
		/// </summary>
		/// <param name="midX">���ĵ�X����</param>
		/// <param name="midY">���ĵ�Y����</param>
		/// <param name="len1">ROI���</param>
		/// <param name="len2">ROI�߶�</param>
		public void createROI(double midX, double midY, double len1, double len2)
		{
			midR = midY;
			midC = midX;

			row1 = midR - (len2 / 2);
			col1 = midC - (len1 / 2);
			row2 = midR + (len2 / 2);
			col2 = midC + (len1 / 2);
		}

		/// <summary>
		/// �ڴ����л���ROI
		/// </summary>
		/// <param name="winHandle">�ṩ��halcon����</param>
		public override void draw(HTuple winHandle)
		{
			HOperatorSet.SetLineWidth(winHandle, roiLineWidth);
			HOperatorSet.DispRectangle1(winHandle, row1, col1, row2, col2);
            HOperatorSet.DispRectangle2(winHandle, row1, col1, 0, 5, 5);
            HOperatorSet.DispRectangle2(winHandle, row1, col2, 0, 5, 5);
            HOperatorSet.DispRectangle2(winHandle, row2, col2, 0, 5, 5);
            HOperatorSet.DispRectangle2(winHandle, row2, col1, 0, 5, 5);
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

			midR = ((row2 - row1) / 2) + row1;
			midC = ((col2 - col1) / 2) + col1;

			val[0] = HMisc.DistancePp(y, x, row1, col1); // upper left 
			val[1] = HMisc.DistancePp(y, x, row1, col2); // upper right 
			val[2] = HMisc.DistancePp(y, x, row2, col2); // lower right 
			val[3] = HMisc.DistancePp(y, x, row2, col1); // lower left 
			val[4] = HMisc.DistancePp(y, x, midR, midC); // midpoint 

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
                    HOperatorSet.DispRectangle2(winHandle, row1, col2, 0, 5, 5);
					break;
				case 2:
                    HOperatorSet.DispRectangle2(winHandle, row2, col2, 0, 5, 5);
					break;
				case 3:
                    HOperatorSet.DispRectangle2(winHandle, row2, col1, 0, 5, 5);
					break;
				case 4:
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
			//region.GenRectangle1(row1, col1, row2, col2);
			//return region;

			HObject rect1;
			HOperatorSet.GenRectangle1(out rect1, row1, col1, row2, col2);
			return rect1;
		}

		/// <summary>
		/// ��ȡROIģ����Ϣ
		/// </summary>
		/// <returns>����ROIģ����Ϣ</returns>
		public override HTuple getModelData()
		{
			return new HTuple(new double[] { row1, col1, row2, col2 });
		}


		/// <summary>
		/// �ƶ�ROI
		/// </summary>
		/// <param name="newX">��λ��X���� column</param>
		/// <param name="newY">��λ��Y���� row</param>
		public override void moveByHandle(double newX, double newY)
		{
			double len1, len2;
			double tmp;

			switch (activeHandleIdx)
			{
				case 0: // upper left 
					row1 = newY;
					col1 = newX;
					break;
				case 1: // upper right 
					row1 = newY;
					col2 = newX;
					break;
				case 2: // lower right 
					row2 = newY;
					col2 = newX;
					break;
				case 3: // lower left
					row2 = newY;
					col1 = newX;
					break;
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

		}//end of method

		/// <summary>
		/// ����ROI���ĵ�����
		/// </summary>
		/// <returns>����ROI���ĵ�����</returns>
		public override PointF getCenterPoint()
		{
			return new PointF((float)midR, (float)midC);
		}

	}//end of class
}//end of namespace
