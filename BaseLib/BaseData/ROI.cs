using System;
using System.Drawing;
using HalconDotNet;


namespace BaseData
{
    /// <summary>
    /// ROI������
    /// </summary>
    [Serializable]
    public class ROI
    {
        /// <summary>
        /// ����ROI�Ľǵ���
        /// </summary>
        protected int NumHandles;
        /// <summary>
        /// ѡ��ROI��������
        /// </summary>
        protected int activeHandleIdx;

        /// <summary>
        /// ����ROI�ı�־ 'positive' �� 'negative'.
        /// </summary>
        protected int OperatorFlag;

        /// <summary>
        /// ROI������
        /// </summary>
        public HTuple flagLineStyle;
        /// <summary>
        /// ROI�߿�
        /// </summary>
        public HTuple roiLineWidth = 3;

        /// <summary>
        /// ROI���� ֱ��ROI
        /// </summary>
        public const int ROI_TYPE_LINE = 10;
        /// <summary>
        /// ROI���� Բ��ROI
        /// </summary>
        public const int ROI_TYPE_CIRCLE = 11;
        /// <summary>
        /// ROI���� Բ��ROI
        /// </summary>
        public const int ROI_TYPE_CIRCLEARC = 12;
        /// <summary>
        /// ROI���� ������1
        /// </summary>
        public const int ROI_TYPE_RECTANCLE1 = 13;
        /// <summary>
        /// ROI���� ������2
        /// </summary>
        public const int ROI_TYPE_RECTANGLE2 = 14;

        /// <summary>
        /// λ�ò�����־
        /// </summary>
        protected HTuple posOperation = new HTuple();
        /// <summary>
        /// neg������־
        /// </summary>
        protected HTuple negOperation = new HTuple(new int[] { 2, 2 });

        /// <summary>
        /// ROI�����๹�캯��
        /// </summary>
        public ROI() { }

        /// <summary>
        /// ����ROI
        /// </summary>
        /// <param name="roiCenterX">ROI X����(Column)</param>
        /// <param name="roiCenterY">ROI Y����(Row)</param>
        public virtual void createROI(double roiCenterX, double roiCenterY) { }

        /// <summary>
        /// �ڴ����л���ROI
        /// </summary>
        /// <param name="winHandle">�ṩ��halcon����</param>
        public virtual void draw(HTuple winHandle) { }

        /// <summary>
        /// ���ؾ���ĳ����λ�����ROI�ľ���
        /// Returns the distance of the ROI handle being
        /// closest to the image point(x,y)
        /// </summary>
        /// <param name="x">ָ����X���� column</param>
        /// <param name="y">ָ����Y���� row</param>
        /// <returns>���ؾ���ĳ����λ�����ROI�ľ���</returns>
        public virtual double distToClosestHandle(double x, double y)
        {
            return 0.0;
        }

        /// <summary>
        /// ��ָ�������л���ѡ�е�ROI
        /// </summary>
        /// <param name="winHandle">�ṩ��halcon����</param>
        public virtual void displayActive(HTuple winHandle) { }

        /// <summary>
        /// �ƶ�ROI
        /// </summary>
        /// <param name="x">ָ����X���� column</param>
        /// <param name="y">ָ����Y���� row</param>
        public virtual void moveByHandle(double x, double y) { }

        /// <summary>
        /// ��ȡROI���������
        /// </summary>
        /// <returns>����ROI�������</returns>
        public virtual HObject getRegion()
        {
            return null;
        }

        /// <summary>
        /// ��ȡ����ָ����ľ���
        /// </summary>
        /// <param name="row">ָ����row����</param>
        /// <param name="col">ָ����column����</param>
        /// <returns>����ָ����ľ���</returns>
        public virtual double getDistanceFromStartPoint(double row, double col)
        {
            return 0.0;
        }

        /// <summary>
        /// ��ȡROIģ����Ϣ
        /// </summary>
        /// <returns>����ROIģ����Ϣ</returns>
        public virtual HTuple getModelData()
        {
            return null;
        }

        /// <summary>
        /// ��ȡ����ROI�ľ����
        /// </summary>
        /// <returns>����ROI�ľ����</returns>
        public int getNumHandles()
        {
            return NumHandles;
        }

        /// <summary>
        /// ��ȡѡ��ROI�ľ������
        /// </summary>
        /// <returns>ѡ��ROI�ľ������</returns>
        public int getActHandleIdx()
        {
            return activeHandleIdx;
        }

        /// <summary>
        /// ��ȡROI�ı�־��
        /// Gets the sign of the ROI object, being either 
        /// 'positive' or 'negative'. This sign is used when creating a model
        /// region for matching applications from a list of ROIs.
        /// </summary>
        /// <returns>����ROI�ı�־��</returns>
        public int getOperatorFlag()
        {
            return OperatorFlag;
        }

        /// <summary>
        /// ����ROI���ĵ�����
        /// </summary>
        /// <returns>����ROI���ĵ�����</returns>
        public virtual PointF getCenterPoint()
        {
            return new PointF(0.0f,0.0f);
        }
    }
}
