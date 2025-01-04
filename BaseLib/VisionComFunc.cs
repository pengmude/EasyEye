using HalconDotNet;
using System;
using System.Collections.Generic;
using System.IO;

namespace SmartLib
{
    internal class VisionComFunc
    {
        /// <summary>
        /// 判断图片是否为空
        /// </summary>
        internal static Response ImageisEmpty(HObject inputImage)
        {
            try
            {
                if (inputImage != null)
                {
                    if (inputImage.Key.ToString() != "0")
                    {
                        return Response.Fail("");
                    }
                }
                return Response.Ok();
            }
            catch
            {
                return Response.Ok();
            }
        }

        /// <summary>
        /// 判断HObject是否为空
        /// </summary>
        internal static Response ObjectisEmpty(HObject inputObject)
        {
            try
            {
                if (inputObject != null)
                {
                    if (inputObject.Key.ToString() != "0")
                    {
                        HObject tempObj;
                        HOperatorSet.GenEmptyObj(out tempObj);
                        HTuple result;
                        HOperatorSet.TestEqualObj(inputObject, tempObj, out result);
                        if (result.I != 1)
                        {
                            tempObj.Dispose();
                            result.Dispose();
                            return Response.Fail("");
                        }
                        tempObj.Dispose();
                        result.Dispose();
                    }
                }
                return Response.Ok();
            }
            catch
            {
                return Response.Ok();
            }
        }

        /// <summary>
        /// 获取图像某一点灰度值
        /// </summary>
        internal static Response<int> GetImgGray(HObject inputImage, int x, int y)
        {
            try
            {
                if (ImageisEmpty(inputImage).IsSuccessful)
                {
                    throw new Exception("图像为空！");
                }

                HTuple gray;
                HOperatorSet.GetGrayval(inputImage, x, y, out gray);
                return Response<int>.Ok(gray.I);
            }
            catch (Exception ex)
            {
                return Response<int>.Fail("获取图像某一点灰度值异常！\r\n原因：" + ex.Message,0);
            }
        }
        
        /// <summary>
        /// 获取图像尺寸
        /// </summary>
        /// <param name="inputImage">传入图像</param>
        /// <returns>返回图像尺寸 格式:宽 高</returns>
        internal static Response<List<HTuple>> GetImageSize(HObject inputImage)
        {
            try
            {
                if (ImageisEmpty(inputImage).IsSuccessful)
                {
                    throw new Exception("图像为空！");
                }

                HTuple image_width, image_height;
                HOperatorSet.GetImageSize(inputImage, out image_width, out image_height);
                List<HTuple> result = new List<HTuple>();
                result.Add(image_width);
                result.Add(image_height);
                return Response<List<HTuple>>.Ok(result);
            }
            catch (Exception ex)
            {
                return Response<List<HTuple>>.Fail("获取图像尺寸失败！\r\n原因：" + ex.Message);
            }
        }

        /// <summary>
        /// 从文件读取图片
        /// </summary>
        internal static Response<HObject> ReadImage(string readFile)
        {
            try
            {        
                if (!File.Exists(readFile))
                    throw new Exception("图片路径不存在！");
                HObject readImage;
                HOperatorSet.ReadImage(out readImage, readFile);
                return Response<HObject>.Ok(readImage);
            }
            catch (Exception ex)
            {
                return Response<HObject>.Fail("从文件读取图片失败！\r\n原因：" + ex.Message);
            }
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        internal static Response WriteImage(HObject inputImage, string readFile)
        {
            try
            {
                if (ImageisEmpty(inputImage).IsSuccessful)
                {
                    throw new Exception("图像为空！");
                }
                HOperatorSet.WriteImage(inputImage, "bmp", 0, readFile);
                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail("保存图片失败！\r\n原因：" + ex.Message);
            }
        }

        /// <summary>
        /// 判断图片是否是彩色图像
        /// </summary>
        internal static Response<bool> BoolImgColor(HObject inputImage)
        {
            try
            {
                if (ImageisEmpty(inputImage).IsSuccessful)
                {
                    throw new Exception("图像为空！");
                }

                HTuple curChannels;
                HOperatorSet.CountChannels(inputImage, out curChannels);
                if (curChannels.I == 1)
                    return Response<bool>.Ok(false);
                else
                    return Response<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail("判断是否彩图失败！\r\n原因：" + ex.Message);
            }
        }

 		/// <summary>
        /// 判断图片是否是彩色图像
        /// </summary>
        internal static Response<bool> BoolMoreObject(HObject inputRegion)
        {
            try
            {
                if (ObjectisEmpty(inputRegion).IsSuccessful)
                {
                    throw new Exception("Obj为空！");
                }


                HTuple nums;
                HOperatorSet.CountObj(inputRegion, out nums);
                if (nums.I == 1)
                    return Response<bool>.Ok(false);
                else
                    return Response<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail("判断是否彩图失败！\r\n原因：" + ex.Message);
            }
        }

        /// <summary>
        /// 获取彩色图片HSV通道图像
        /// </summary>
        internal static Response<List<HObject>> GetColorImgHSV(HObject inputImage)
        {
            try
            {
                if (ImageisEmpty(inputImage).IsSuccessful)
                {
                    throw new Exception("图像为空！");
                }

                Response<bool> result = BoolImgColor(inputImage);
                if (result.IsSuccessful && result.Data)
                {
                    HObject r, g, b, h, s, v;
                    HOperatorSet.Decompose3(inputImage, out r, out g, out b);
                    HOperatorSet.TransFromRgb(r, g, b, out h, out s, out v, "hsv");
                    r.Dispose(); g.Dispose(); b.Dispose();
                    List<HObject> sd = new List<HObject>();
                    sd.Add(h); sd.Add(s); sd.Add(v);
                    return Response<List<HObject>>.Ok(sd);
                }
                else
                {
                    throw new Exception(result.Msg);
                }
            }
            catch (Exception ex)
            {
                return Response<List<HObject>>.Fail("判断是否彩图失败！\r\n原因：" + ex.Message);
            }
        }

        /// <summary>
        /// 获取图像质量评价 ；  均值，偏差值
        /// </summary>
        internal static Response<List<HTuple>> GetImgQual(HObject inputImage)
        {
            HObject hobject;
            HOperatorSet.GenEmptyObj(out hobject);
            HObject hobject2;
            HOperatorSet.GenEmptyObj(out hobject2);
            HObject hobject3;
            HOperatorSet.GenEmptyObj(out hobject3);
            HObject hobject4;
            HOperatorSet.GenEmptyObj(out hobject4);
            try
            {
                if (ImageisEmpty(inputImage).IsSuccessful)
                {
                    throw new Exception("图像为空！");
                }
                HOperatorSet.Laplace(inputImage, out hobject, "signed", 3, "n_4");
                HOperatorSet.Laplace(inputImage, out hobject2, "signed", 3, "n_8");
                HOperatorSet.AddImage(hobject, hobject, out hobject3, 1, 0);
                HOperatorSet.AddImage(hobject, hobject3, out hobject3, 1, 0);
                HOperatorSet.AddImage(hobject2, hobject3, out hobject3, 1, 0);
                HOperatorSet.MultImage(hobject3, hobject3, out hobject4, 1, 0);
                HTuple ho_Mean, ho_Deviation;
                HOperatorSet.Intensity(hobject4, hobject4, out ho_Mean, out ho_Deviation);
                hobject.Dispose();
                hobject2.Dispose();
                hobject3.Dispose();
                hobject4.Dispose();
                List<HTuple> result = new List<HTuple>();
                result.Add(ho_Mean);
                result.Add(ho_Deviation);
                return Response<List<HTuple>>.Ok(result);
            }
            catch (Exception ex)
            {
                hobject.Dispose();
                hobject2.Dispose();
                hobject3.Dispose();
                hobject4.Dispose();
                return Response<List<HTuple>>.Fail("获取图像质量评价失败！\r\n原因：" + ex.Message, new List<HTuple> { 0,0});
            }
        }


        internal static Response<HObject> ScaleImageRange(HObject inputImage,int minValue,int maxValue)
        {
            try
            {
                if (ImageisEmpty(inputImage).IsSuccessful)
                {
                    throw new Exception("图像为空！");
                }
                HObject outImage;
                scale_image_range(inputImage, out outImage, minValue, maxValue);

                return Response<HObject>.Ok(outImage);
            }
            catch (Exception ex)
            {
                return Response<HObject>.Fail("线性缩放图片失败！\r\n原因：" + ex.Message);
            }
        }


        private static void scale_image_range(HObject ho_Image, out HObject ho_ImageScaled, HTuple hv_Min,
    HTuple hv_Max)
        {




            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_ImageSelected = null, ho_SelectedChannel = null;
            HObject ho_LowerRegion = null, ho_UpperRegion = null, ho_ImageSelectedScaled = null;

            // Local copy input parameter variables 
            HObject ho_Image_COPY_INP_TMP;
            ho_Image_COPY_INP_TMP = new HObject(ho_Image);



            // Local control variables 

            HTuple hv_LowerLimit = new HTuple(), hv_UpperLimit = new HTuple();
            HTuple hv_Mult = new HTuple(), hv_Add = new HTuple(), hv_NumImages = new HTuple();
            HTuple hv_ImageIndex = new HTuple(), hv_Channels = new HTuple();
            HTuple hv_ChannelIndex = new HTuple(), hv_MinGray = new HTuple();
            HTuple hv_MaxGray = new HTuple(), hv_Range = new HTuple();
            HTuple hv_Max_COPY_INP_TMP = new HTuple(hv_Max);
            HTuple hv_Min_COPY_INP_TMP = new HTuple(hv_Min);

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageScaled);
            HOperatorSet.GenEmptyObj(out ho_ImageSelected);
            HOperatorSet.GenEmptyObj(out ho_SelectedChannel);
            HOperatorSet.GenEmptyObj(out ho_LowerRegion);
            HOperatorSet.GenEmptyObj(out ho_UpperRegion);
            HOperatorSet.GenEmptyObj(out ho_ImageSelectedScaled);
            //Convenience procedure to scale the gray values of the
            //input image Image from the interval [Min,Max]
            //to the interval [0,255] (default).
            //Gray values < 0 or > 255 (after scaling) are clipped.
            //
            //If the image shall be scaled to an interval different from [0,255],
            //this can be achieved by passing tuples with 2 values [From, To]
            //as Min and Max.
            //Example:
            //scale_image_range(Image:ImageScaled:[100,50],[200,250])
            //maps the gray values of Image from the interval [100,200] to [50,250].
            //All other gray values will be clipped.
            //
            //input parameters:
            //Image: the input image
            //Min: the minimum gray value which will be mapped to 0
            //     If a tuple with two values is given, the first value will
            //     be mapped to the second value.
            //Max: The maximum gray value which will be mapped to 255
            //     If a tuple with two values is given, the first value will
            //     be mapped to the second value.
            //
            //Output parameter:
            //ImageScale: the resulting scaled image.
            //
            if ((int)(new HTuple((new HTuple(hv_Min_COPY_INP_TMP.TupleLength())).TupleEqual(
                2))) != 0)
            {
                hv_LowerLimit.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_LowerLimit = hv_Min_COPY_INP_TMP.TupleSelect(
                        1);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Min = hv_Min_COPY_INP_TMP.TupleSelect(
                            0);
                        hv_Min_COPY_INP_TMP.Dispose();
                        hv_Min_COPY_INP_TMP = ExpTmpLocalVar_Min;
                    }
                }
            }
            else
            {
                hv_LowerLimit.Dispose();
                hv_LowerLimit = 0.0;
            }
            if ((int)(new HTuple((new HTuple(hv_Max_COPY_INP_TMP.TupleLength())).TupleEqual(
                2))) != 0)
            {
                hv_UpperLimit.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_UpperLimit = hv_Max_COPY_INP_TMP.TupleSelect(
                        1);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Max = hv_Max_COPY_INP_TMP.TupleSelect(
                            0);
                        hv_Max_COPY_INP_TMP.Dispose();
                        hv_Max_COPY_INP_TMP = ExpTmpLocalVar_Max;
                    }
                }
            }
            else
            {
                hv_UpperLimit.Dispose();
                hv_UpperLimit = 255.0;
            }
            //
            //Calculate scaling parameters.
            //Only scale if the scaling range is not zero.
            if ((int)((new HTuple(((((hv_Max_COPY_INP_TMP - hv_Min_COPY_INP_TMP)).TupleAbs()
                )).TupleLess(1.0E-6))).TupleNot()) != 0)
            {
                hv_Mult.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Mult = (((hv_UpperLimit - hv_LowerLimit)).TupleReal()
                        ) / (hv_Max_COPY_INP_TMP - hv_Min_COPY_INP_TMP);
                }
                hv_Add.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Add = ((-hv_Mult) * hv_Min_COPY_INP_TMP) + hv_LowerLimit;
                }
                //Scale image.
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ScaleImage(ho_Image_COPY_INP_TMP, out ExpTmpOutVar_0, hv_Mult,
                        hv_Add);
                    ho_Image_COPY_INP_TMP.Dispose();
                    ho_Image_COPY_INP_TMP = ExpTmpOutVar_0;
                }
            }
            //
            //Clip gray values if necessary.
            //This must be done for each image and channel separately.
            ho_ImageScaled.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ImageScaled);
            hv_NumImages.Dispose();
            HOperatorSet.CountObj(ho_Image_COPY_INP_TMP, out hv_NumImages);
            HTuple end_val51 = hv_NumImages;
            HTuple step_val51 = 1;
            for (hv_ImageIndex = 1; hv_ImageIndex.Continue(end_val51, step_val51); hv_ImageIndex = hv_ImageIndex.TupleAdd(step_val51))
            {
                ho_ImageSelected.Dispose();
                HOperatorSet.SelectObj(ho_Image_COPY_INP_TMP, out ho_ImageSelected, hv_ImageIndex);
                hv_Channels.Dispose();
                HOperatorSet.CountChannels(ho_ImageSelected, out hv_Channels);
                HTuple end_val54 = hv_Channels;
                HTuple step_val54 = 1;
                for (hv_ChannelIndex = 1; hv_ChannelIndex.Continue(end_val54, step_val54); hv_ChannelIndex = hv_ChannelIndex.TupleAdd(step_val54))
                {
                    ho_SelectedChannel.Dispose();
                    HOperatorSet.AccessChannel(ho_ImageSelected, out ho_SelectedChannel, hv_ChannelIndex);
                    hv_MinGray.Dispose(); hv_MaxGray.Dispose(); hv_Range.Dispose();
                    HOperatorSet.MinMaxGray(ho_SelectedChannel, ho_SelectedChannel, 0, out hv_MinGray,
                        out hv_MaxGray, out hv_Range);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_LowerRegion.Dispose();
                        HOperatorSet.Threshold(ho_SelectedChannel, out ho_LowerRegion, ((hv_MinGray.TupleConcat(
                            hv_LowerLimit))).TupleMin(), hv_LowerLimit);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_UpperRegion.Dispose();
                        HOperatorSet.Threshold(ho_SelectedChannel, out ho_UpperRegion, hv_UpperLimit,
                            ((hv_UpperLimit.TupleConcat(hv_MaxGray))).TupleMax());
                    }
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.PaintRegion(ho_LowerRegion, ho_SelectedChannel, out ExpTmpOutVar_0,
                            hv_LowerLimit, "fill");
                        ho_SelectedChannel.Dispose();
                        ho_SelectedChannel = ExpTmpOutVar_0;
                    }
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.PaintRegion(ho_UpperRegion, ho_SelectedChannel, out ExpTmpOutVar_0,
                            hv_UpperLimit, "fill");
                        ho_SelectedChannel.Dispose();
                        ho_SelectedChannel = ExpTmpOutVar_0;
                    }
                    if ((int)(new HTuple(hv_ChannelIndex.TupleEqual(1))) != 0)
                    {
                        ho_ImageSelectedScaled.Dispose();
                        HOperatorSet.CopyObj(ho_SelectedChannel, out ho_ImageSelectedScaled, 1,
                            1);
                    }
                    else
                    {
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.AppendChannel(ho_ImageSelectedScaled, ho_SelectedChannel,
                                out ExpTmpOutVar_0);
                            ho_ImageSelectedScaled.Dispose();
                            ho_ImageSelectedScaled = ExpTmpOutVar_0;
                        }
                    }
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ImageScaled, ho_ImageSelectedScaled, out ExpTmpOutVar_0
                        );
                    ho_ImageScaled.Dispose();
                    ho_ImageScaled = ExpTmpOutVar_0;
                }
            }
            ho_Image_COPY_INP_TMP.Dispose();
            ho_ImageSelected.Dispose();
            ho_SelectedChannel.Dispose();
            ho_LowerRegion.Dispose();
            ho_UpperRegion.Dispose();
            ho_ImageSelectedScaled.Dispose();

            hv_Max_COPY_INP_TMP.Dispose();
            hv_Min_COPY_INP_TMP.Dispose();
            hv_LowerLimit.Dispose();
            hv_UpperLimit.Dispose();
            hv_Mult.Dispose();
            hv_Add.Dispose();
            hv_NumImages.Dispose();
            hv_ImageIndex.Dispose();
            hv_Channels.Dispose();
            hv_ChannelIndex.Dispose();
            hv_MinGray.Dispose();
            hv_MaxGray.Dispose();
            hv_Range.Dispose();

            return;
        }


        internal static void gen_arrow_contour_xld(out HObject ho_Arrow, HTuple hv_Row1, HTuple hv_Column1,
          HTuple hv_Row2, HTuple hv_Column2, HTuple hv_HeadLength, HTuple hv_HeadWidth)
        {



            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_TempArrow = null;

            // Local control variables 

            HTuple hv_Length = null, hv_ZeroLengthIndices = null;
            HTuple hv_DR = null, hv_DC = null, hv_HalfHeadWidth = null;
            HTuple hv_RowP1 = null, hv_ColP1 = null, hv_RowP2 = null;
            HTuple hv_ColP2 = null, hv_Index = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            HOperatorSet.GenEmptyObj(out ho_TempArrow);
            //This procedure generates arrow shaped XLD contours,
            //pointing from (Row1, Column1) to (Row2, Column2).
            //If starting and end point are identical, a contour consisting
            //of a single point is returned.
            //
            //input parameteres:
            //Row1, Column1: Coordinates of the arrows' starting points
            //Row2, Column2: Coordinates of the arrows' end points
            //HeadLength, HeadWidth: Size of the arrow heads in pixels
            //
            //output parameter:
            //Arrow: The resulting XLD contour
            //
            //The input tuples Row1, Column1, Row2, and Column2 have to be of
            //the same length.
            //HeadLength and HeadWidth either have to be of the same length as
            //Row1, Column1, Row2, and Column2 or have to be a single element.
            //If one of the above restrictions is violated, an error will occur.
            //
            //
            //Init
            ho_Arrow.Dispose();
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            //
            //Calculate the arrow length
            HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Length);
            //
            //Mark arrows with identical start and end point
            //(set Length to -1 to avoid division-by-zero exception)
            hv_ZeroLengthIndices = hv_Length.TupleFind(0);
            if ((int)(new HTuple(hv_ZeroLengthIndices.TupleNotEqual(-1))) != 0)
            {
                if (hv_Length == null)
                    hv_Length = new HTuple();
                hv_Length[hv_ZeroLengthIndices] = -1;
            }
            //
            //Calculate auxiliary variables.
            hv_DR = (1.0 * (hv_Row2 - hv_Row1)) / hv_Length;
            hv_DC = (1.0 * (hv_Column2 - hv_Column1)) / hv_Length;
            hv_HalfHeadWidth = hv_HeadWidth / 2.0;
            //
            //Calculate end points of the arrow head.
            hv_RowP1 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) + (hv_HalfHeadWidth * hv_DC);
            hv_ColP1 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) - (hv_HalfHeadWidth * hv_DR);
            hv_RowP2 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) - (hv_HalfHeadWidth * hv_DC);
            hv_ColP2 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) + (hv_HalfHeadWidth * hv_DR);
            //
            //Finally create output XLD contour for each input point pair
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
            {
                if ((int)(new HTuple(((hv_Length.TupleSelect(hv_Index))).TupleEqual(-1))) != 0)
                {
                    //Create_ single points for arrows with identical start and end point
                    ho_TempArrow.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_TempArrow, hv_Row1.TupleSelect(hv_Index),
                        hv_Column1.TupleSelect(hv_Index));
                }
                else
                {
                    //Create arrow contour
                    ho_TempArrow.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_TempArrow, ((((((((((hv_Row1.TupleSelect(
                        hv_Index))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                        hv_RowP1.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                        hv_RowP2.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)),
                        ((((((((((hv_Column1.TupleSelect(hv_Index))).TupleConcat(hv_Column2.TupleSelect(
                        hv_Index)))).TupleConcat(hv_ColP1.TupleSelect(hv_Index)))).TupleConcat(
                        hv_Column2.TupleSelect(hv_Index)))).TupleConcat(hv_ColP2.TupleSelect(
                        hv_Index)))).TupleConcat(hv_Column2.TupleSelect(hv_Index)));
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_Arrow, ho_TempArrow, out ExpTmpOutVar_0);
                    ho_Arrow.Dispose();
                    ho_Arrow = ExpTmpOutVar_0;
                }
            }
            ho_TempArrow.Dispose();

            return;
        }


        internal static HObject display_line(HTuple hv_RowEdge, HTuple hv_ColumnEdge, HTuple hv_len2, HTuple hv_ph1)
        {
            HTuple begin_row = hv_RowEdge - (hv_len2 * (new HTuple(hv_ph1).TupleCos()));
            HTuple begin_col = hv_ColumnEdge - (hv_len2 * (new HTuple((hv_ph1)).TupleSin()));
            HTuple end_row = hv_RowEdge + (hv_len2 * (new HTuple(hv_ph1).TupleCos()));
            HTuple end_col = hv_ColumnEdge + (hv_len2 * (new HTuple((hv_ph1)).TupleSin()));
            HObject xldLine1, xldLine2;
            HOperatorSet.GenEmptyObj(out xldLine1);
            HOperatorSet.GenEmptyObj(out xldLine2);
            xldLine1.Dispose();
            for (int i = 0; i < begin_row.Length; i++)
            {
                HOperatorSet.GenContourPolygonXld(out xldLine1, ((begin_row.TupleSelect(i)).TupleConcat(end_row.TupleSelect(i))),
                                                   ((begin_col.TupleSelect(i))).TupleConcat(end_col.TupleSelect(i)));
                HOperatorSet.ConcatObj(xldLine1, xldLine2, out xldLine2);
            }
            xldLine1.Dispose();
            return xldLine2;
        }


        /// <summary>
        /// 匹配结果轮廓
        /// </summary>
        internal static Response<HObject> GetMatchShapeContours(HTuple hv_ModelID, HTuple hv_Row, HTuple hv_Column, HTuple hv_Angle, HTuple hv_Scale)
        {
            try
            {
                if (hv_Row.Length <= 0)
                    throw new Exception("匹配结果为空！");
                HObject modelContour;
                HOperatorSet.GetShapeModelContours(out modelContour, hv_ModelID, 1);
                HObject outContours;
                HOperatorSet.GenEmptyObj(out outContours);
                HTuple transHom;
                for(int i=0;i< hv_Row.Length;i++)
                {
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_Row[i], hv_Column[i], hv_Angle[i], out transHom);
                    HOperatorSet.HomMat2dScale(transHom, hv_Scale[i], hv_Scale[i], hv_Row[i], hv_Column[i], out transHom);
                    HObject sfn;
                    HOperatorSet.AffineTransContourXld(modelContour, out sfn, transHom);
                    HOperatorSet.ConcatObj(outContours, sfn, out outContours);
                    sfn.Dispose();
                }
                modelContour.Dispose();

                return Response<HObject>.Ok(outContours);
            }
            catch (Exception ex)
            {
                return Response<HObject>.Fail("获取匹配结果轮廓失败！\r\n" + ex.Message);
            }
        }

        internal static void gen_measure_arrow(out HObject ho_Arrow,bool dir, HTuple hv_Row1, HTuple hv_Column1, HTuple hv_Phi, HTuple hv_Length1, HTuple hv_Length2)
        {
            // Local iconic variables 
            HObject ho_Rectangle;
            // Local control variables 
            HTuple  hv_beginRow = null, hv_beginCol = null, hv_EndRow = null, hv_EndCol = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            try
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_Row1, hv_Column1, hv_Phi, hv_Length1, hv_Length2);
                if (dir)
                {
                    hv_beginRow = hv_Row1 + ((hv_Phi.TupleSin()) * (hv_Length1 + 20));
                    hv_beginCol = hv_Column1 - ((hv_Phi.TupleCos()) * (hv_Length1 + 20));
                    hv_EndRow = hv_Row1 + ((hv_Phi.TupleSin()) * (hv_Length1));
                    hv_EndCol = hv_Column1 - ((hv_Phi.TupleCos()) * (hv_Length1));
                }
                else
                {
                    hv_beginRow = hv_Row1 - ((hv_Phi.TupleSin()) * (hv_Length1));
                    hv_beginCol = hv_Column1 + ((hv_Phi.TupleCos()) * (hv_Length1));
                    hv_EndRow = hv_Row1 - ((hv_Phi.TupleSin()) * (hv_Length1 + 20));
                    hv_EndCol = hv_Column1 + ((hv_Phi.TupleCos()) * (hv_Length1 + 20));
                }
                ho_Arrow.Dispose();

                gen_arrow_contour_xld(out ho_Arrow, hv_beginRow, hv_beginCol, hv_EndRow, hv_EndCol,
                    10, 10);

                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_Arrow, ho_Rectangle, out ExpTmpOutVar_0);
                    ho_Arrow.Dispose();
                    ho_Arrow = ExpTmpOutVar_0;
                }
                ho_Rectangle.Dispose();
                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Rectangle.Dispose();

                throw HDevExpDefaultException;
            }
        }

        internal static void gen_measure_arrow(out HObject ho_Arrow, HTuple homMat2D,bool dir, HTuple hv_Row1, HTuple hv_Column1, HTuple hv_Phi, HTuple hv_Length1, HTuple hv_Length2)
        {
            // Local iconic variables 
            HObject ho_Rectangle;
            // Local control variables 
            HTuple hv_beginRow = null, hv_beginCol = null, hv_EndRow = null, hv_EndCol = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            try
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_Row1, hv_Column1, hv_Phi, hv_Length1, hv_Length2);
                if (dir)
                {
                    hv_beginRow = hv_Row1 + ((hv_Phi.TupleSin()) * (hv_Length1 + 20));
                    hv_beginCol = hv_Column1 - ((hv_Phi.TupleCos()) * (hv_Length1 + 20));
                    hv_EndRow = hv_Row1 + ((hv_Phi.TupleSin()) * (hv_Length1));
                    hv_EndCol = hv_Column1 - ((hv_Phi.TupleCos()) * (hv_Length1));
                }
                else
                {
                    hv_beginRow = hv_Row1 - ((hv_Phi.TupleSin()) * (hv_Length1));
                    hv_beginCol = hv_Column1 + ((hv_Phi.TupleCos()) * (hv_Length1));
                    hv_EndRow = hv_Row1 - ((hv_Phi.TupleSin()) * (hv_Length1 + 20));
                    hv_EndCol = hv_Column1 + ((hv_Phi.TupleCos()) * (hv_Length1 + 20));
                }
                ho_Arrow.Dispose();

                gen_arrow_contour_xld(out ho_Arrow, hv_beginRow, hv_beginCol, hv_EndRow, hv_EndCol,
                    10, 10);

                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_Arrow, ho_Rectangle, out ExpTmpOutVar_0);
                    ho_Arrow.Dispose();
                    ho_Arrow = ExpTmpOutVar_0;
                }
                ho_Rectangle.Dispose();
                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Rectangle.Dispose();

                throw HDevExpDefaultException;
            }
        }

        internal static void rake_edge(HObject ho_Image, out HObject ho_Regions, HTuple hv_CenterRow,
           HTuple hv_CenterCol, HTuple hv_Phi, HTuple hv_Length1, HTuple hv_Length2,
           HTuple hv_Elements,
           HTuple hv_DetectHeight,
           HTuple hv_DetectWidth,
           HTuple hv_Sigma,
           HTuple hv_Threshold,
           HTuple hv_Transition,
           HTuple hv_Select,
           out HTuple hv_ResultRow, out HTuple hv_ResultColumn)
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            // Local iconic variables 
            HObject ho_Rectangle = null, ho_Arrow1 = null;
            // Local control variables 
            HTuple hv_Row1 = null, hv_Col1 = null;
            HTuple hv_Rowlen2 = null, hv_Collen2 = null, hv_Width = null;
            HTuple hv_Height = null, hv_RowEdge = new HTuple(), hv_Amplitude = new HTuple();
            HTuple hv_Distance = new HTuple(), hv_i = null, hv_RowC = new HTuple();
            HTuple hv_ColC = new HTuple(), hv_RowL1 = new HTuple();
            HTuple hv_ColL1 = new HTuple(), hv_RowL2 = new HTuple();
            HTuple hv_ColL2 = new HTuple(), hv_MsrHandle_Measure = new HTuple();
            HTuple hv_ColEdge = new HTuple(), hv_tRow = new HTuple();
            HTuple hv_tCol = new HTuple(), hv_t = new HTuple(), hv_Number = new HTuple();
            HTuple hv_j = new HTuple();
            HTuple hv_DetectWidth_COPY_INP_TMP = hv_DetectWidth.Clone();
            HTuple hv_Select_COPY_INP_TMP = hv_Select.Clone();
            HTuple hv_Transition_COPY_INP_TMP = hv_Transition.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Arrow1);
            //*************************输入
            //Image:输入图像
            //CenterRow:输入矩形框的行中心
            //CenterCol:输入矩形框的列中心
            //Phi:输入矩形框的角度
            //Length1:输入矩形框的宽度
            //Length2:输入矩形框的高度
            //Step:扫描间隔
            //Sigma:高期平滑度
            //Threshold:最小边缘过渡
            //Transition:边缘提取方式,'negative'从白到黑,'positive'从黑到白,'all'都可以
            //Select:选取点方式,'first'第一个,'last'最后一个,'all'两者都有

            //************************输出
            //RowBegin:边的起始行
            //ColBegin:边的起始列
            //RowEnd:边的终点行
            //ColEnd:边的终点列

            //是否显示搜索区域
            //if (ShowSearchArea='TRUE')
            //在搜索框外显示角度指示箭头
            //gen_arrow_contour_xld (Arrow, CenterRow+sin(Phi)*(Length1+20), CenterCol-cos(Phi)*(Length1+20), CenterRow+sin(Phi)*(Length1), CenterCol-cos(Phi)*(Length1), 10, 10)
            //生成搜索框
            //gen_rectangle2 (Rectangle, CenterRow, CenterCol, Phi, Length1, Length2)
            //dev_set_color ('green')
            //dev_set_line_width (2)
            //dev_display (Arrow)
            //dev_display (Rectangle)
            //endif

            //产生一个空显示对象，用于显示
            ho_Regions.Dispose();
            HOperatorSet.GenEmptyObj(out ho_Regions);

            // HOperatorSet.SetLineWidth(Win, 1);
            //初始化结果点的行列
            hv_ResultRow = new HTuple();
            hv_ResultColumn = new HTuple();
            hv_Row1 = new HTuple();
            hv_Col1 = new HTuple();

            if (hv_Rowlen2 == null)
                hv_Rowlen2 = new HTuple();
            hv_Rowlen2[0] = hv_CenterRow + ((((hv_Phi + ((new HTuple(90)).TupleRad())))  .TupleSin()
                ) * hv_Length2);
            if (hv_Collen2 == null)
                hv_Collen2 = new HTuple();
            hv_Collen2[0] = hv_CenterCol - ((((hv_Phi + ((new HTuple(90)).TupleRad()))).TupleCos()
                ) * hv_Length2);
            if (hv_Rowlen2 == null)
                hv_Rowlen2 = new HTuple();
            hv_Rowlen2[1] = hv_CenterRow - ((((hv_Phi + ((new HTuple(90)).TupleRad()))).TupleSin()
                ) * hv_Length2);
            if (hv_Collen2 == null)
                hv_Collen2 = new HTuple();
            hv_Collen2[1] = hv_CenterCol + ((((hv_Phi + ((new HTuple(90)).TupleRad()))).TupleCos()
                ) * hv_Length2);
            //gen_arrow_contour_xld (Arrow1, Rowlen2[0], Collen2[0], Rowlen2[1], Collen2[1], 10, 10)
            //* concat_obj (Regions, Arrow1, Regions)

            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            //循环扫描点
            //* for Index := 0 to Length2*2/Elements by 1
            //生成测量矩形,宽度为0,即为一条直线
            //* gen_measure_rectangle2 (CenterRow-cos(Phi)*(Length2-Elements*Index), CenterCol-sin(Phi)*(Length2-Elements*Index), Phi, Length1, 0, Width, Height, 'nearest_neighbor', MeasureHandle1)
            //是否显示搜索线
            //* Row1[0] := (CenterRow-cos(Phi)*(Length2-Elements*Index))+sin(Phi)*(Length1)
            //* Col1[0] := (CenterCol-sin(Phi)*(Length2-Elements*Index))-cos(Phi)*(Length1)
            //* Row1[1] := (CenterRow-cos(Phi)*(Length2-Elements*Index))-sin(Phi)*(Length1)
            //* Col1[1] := (CenterCol-sin(Phi)*(Length2-Elements*Index))+cos(Phi)*(Length1)
            //gen_contour_polygon_xld (Contour1, Row1, Col1)
            //把xld存储到显示对象
            //* concat_obj (Regions, Contour1, Regions)
            //测量点
            //* measure_pos (Image, MeasureHandle1, Sigma, Threshold, Transition, Select, RowEdge, ColumnEdge, Amplitude, Distance)
            //* close_measure (MeasureHandle1)
            //计算结果点的行列个数
            //* if (|RowEdge|>0)
            //* ResultRow := [ResultRow,RowEdge]
            //* ResultColumn := [ResultColumn,ColumnEdge]
            //* endif

            HTuple end_val73 = hv_Elements;
            HTuple step_val73 = 1;
            for (hv_i = 1; hv_i.Continue(end_val73, step_val73); hv_i = hv_i.TupleAdd(step_val73))
            {
                //如果只有一个测量矩形，作为卡尺工具，宽度为检测直线的长度
                if ((int)(new HTuple(hv_Elements.TupleEqual(1))) != 0)
                {
                    hv_RowC = ((hv_Rowlen2.TupleSelect(0)) + (hv_Rowlen2.TupleSelect(1))) * 0.5;
                    hv_ColC = ((hv_Collen2.TupleSelect(0)) + (hv_Collen2.TupleSelect(1))) * 0.5;
                    //判断是否超出图像,超出不检测边缘
                    if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(
                        new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(
                        hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                    {
                        continue;
                    }
                    HOperatorSet.DistancePp(hv_Rowlen2.TupleSelect(0), hv_Collen2.TupleSelect(
                        0), hv_Rowlen2.TupleSelect(1), hv_Collen2.TupleSelect(1), out hv_Distance);
                    hv_DetectWidth_COPY_INP_TMP = hv_Length2.Clone();
                    ho_Rectangle.Dispose();
                    HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_RowC, hv_ColC,
                        hv_Phi, hv_DetectHeight, hv_DetectWidth_COPY_INP_TMP / 2);
                }
                else
                {
                    //如果有多个测量矩形，产生该测量矩形xld
                    hv_RowC = (hv_Rowlen2.TupleSelect(0)) + ((((hv_Rowlen2.TupleSelect(1)) - (hv_Rowlen2.TupleSelect(
                        0))) * (hv_i - 1)) / (hv_Elements - 1));
                    hv_ColC = (hv_Collen2.TupleSelect(0)) + ((((hv_Collen2.TupleSelect(1)) - (hv_Collen2.TupleSelect(
                        0))) * (hv_i - 1)) / (hv_Elements - 1));
                    //判断是否超出图像,超出不检测边缘
                    if ((int)((new HTuple((new HTuple((new HTuple(hv_RowC.TupleGreater(hv_Height - 1))).TupleOr(
                        new HTuple(hv_RowC.TupleLess(0))))).TupleOr(new HTuple(hv_ColC.TupleGreater(
                        hv_Width - 1))))).TupleOr(new HTuple(hv_ColC.TupleLess(0)))) != 0)
                    {
                        continue;
                    }
                    ho_Rectangle.Dispose();
                    HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_RowC, hv_ColC,
                        hv_Phi, hv_DetectHeight, hv_DetectWidth_COPY_INP_TMP / 2);
                }
                //把测量矩形xld存储到显示对象
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_Regions, ho_Rectangle, out ExpTmpOutVar_0);
                    ho_Regions.Dispose();
                    ho_Regions = ExpTmpOutVar_0;
                }

                if ((int)(new HTuple(hv_i.TupleEqual(1))) != 0)
                {
                    //在第一个测量矩形绘制一个箭头xld，用于只是边缘检测方向
                    hv_RowL1 = hv_RowC + (hv_DetectHeight * (hv_Phi.TupleSin()));
                    hv_ColL1 = hv_ColC - (hv_DetectHeight * (hv_Phi.TupleCos()));
                    hv_RowL2 = hv_RowC - (hv_DetectHeight * (hv_Phi.TupleSin()));
                    hv_ColL2 = hv_ColC + (hv_DetectHeight * (hv_Phi.TupleCos()));
                    ho_Arrow1.Dispose();
                    gen_arrow_contour_xld(out ho_Arrow1, hv_RowL1, hv_ColL1, hv_RowL2, hv_ColL2,
                        hv_DetectWidth_COPY_INP_TMP, hv_DetectWidth_COPY_INP_TMP);
                    //把xld存储到显示对象
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConcatObj(ho_Regions, ho_Arrow1, out ExpTmpOutVar_0);
                        ho_Regions.Dispose();
                        ho_Regions = ExpTmpOutVar_0;
                    }
                }

                //产生测量对象句柄
                HOperatorSet.GenMeasureRectangle2(hv_RowC, hv_ColC, hv_Phi, hv_DetectHeight, hv_DetectWidth_COPY_INP_TMP / 2,
                    hv_Width, hv_Height, "nearest_neighbor", out hv_MsrHandle_Measure);
                //设置极性
                if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("negative"))) != 0)
                {
                    hv_Transition_COPY_INP_TMP = "negative";
                }
                else
                {
                    if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("positive"))) != 0)
                    {

                        hv_Transition_COPY_INP_TMP = "positive";
                    }
                    else
                    {
                        hv_Transition_COPY_INP_TMP = "all";
                    }
                }
                //设置边缘位置。最强点是从所有边缘中选择幅度绝对值最大点，需要设置为'all'
                if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("first"))) != 0)
                {
                    hv_Select_COPY_INP_TMP = "first";
                }
                else
                {
                    if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("last"))) != 0)
                    {

                        hv_Select_COPY_INP_TMP = "last";
                    }
                    else
                    {
                        hv_Select_COPY_INP_TMP = "all";
                    }
                }
                //检测边缘
                HOperatorSet.MeasurePos(ho_Image, hv_MsrHandle_Measure, hv_Sigma, hv_Threshold,
                    hv_Transition_COPY_INP_TMP, hv_Select_COPY_INP_TMP, out hv_RowEdge, out hv_ColEdge,
                    out hv_Amplitude, out hv_Distance);
                //清除测量对象句柄
                HOperatorSet.CloseMeasure(hv_MsrHandle_Measure);

                //临时变量初始化
                //tRow，tCol保存找到指定边缘的坐标
                hv_tRow = 0;
                hv_tCol = 0;
                //t保存边缘的幅度绝对值
                hv_t = 0;
                //找到的边缘必须至少为1个
                HOperatorSet.TupleLength(hv_RowEdge, out hv_Number);
                if ((int)(new HTuple(hv_Number.TupleLess(1))) != 0)
                {
                    continue;
                }
                //有多个边缘时，选择幅度绝对值最大的边缘
                HTuple end_val150 = hv_Number - 1;
                HTuple step_val150 = 1;
                for (hv_j = 0; hv_j.Continue(end_val150, step_val150); hv_j = hv_j.TupleAdd(step_val150))
                {
                    if ((int)(new HTuple(((((hv_Amplitude.TupleSelect(hv_j))).TupleAbs())).TupleGreater(
                        hv_t))) != 0)
                    {

                        hv_tRow = hv_RowEdge.TupleSelect(hv_j);
                        hv_tCol = hv_ColEdge.TupleSelect(hv_j);
                        hv_t = ((hv_Amplitude.TupleSelect(hv_j))).TupleAbs();
                    }
                }
                //把找到的边缘保存在输出数组
                if ((int)(new HTuple(hv_t.TupleGreater(0))) != 0)
                {
                    hv_ResultRow = hv_ResultRow.TupleConcat(hv_tRow);
                    hv_ResultColumn = hv_ResultColumn.TupleConcat(hv_tCol);
                }
            }

            //tuple_length (ResultRow, RL)
            //tuple_length (ResultColumn, CL)
            //结果点的行个数或列个数>=2拟合,防止报错
            //if (RL>1 or CL>1)
            //生成Contour
            //gen_contour_polygon_xld (Contour, ResultRow, ResultColumn)
            //拟合直线,参数可以更改或改成接口
            //*     fit_line_contour_xld (Contour, 'tukey', -1, 0, 0, 1, RowBegin, ColBegin, RowEnd, ColEnd, Nr, Nc, Dist)
            //else
            //置为-1,防止输出时画直线报错
            //RowBegin := -1
            //ColBegin := -1
            //RowEnd := -1
            //ColEnd := -1
            //endif
            ho_Rectangle.Dispose();
            ho_Arrow1.Dispose();

            return;
        }


        internal static void pts_to_best_line(out HObject ho_Line, HTuple hv_Rows, HTuple hv_Cols,
    HTuple hv_ActiveNum, out HTuple hv_Row1, out HTuple hv_Column1, out HTuple hv_Row2,
    out HTuple hv_Column2)
        {
            // Local iconic variables 
            HObject ho_Contour = null;
            // Local control variables 
            HTuple hv_Length = null, hv_Nr = new HTuple();
            HTuple hv_Nc = new HTuple(), hv_Dist = new HTuple(), hv_Length1 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Line);
            HOperatorSet.GenEmptyObj(out ho_Contour);
            //初始化
            hv_Row1 = 0;
            hv_Column1 = 0;
            hv_Row2 = 0;
            hv_Column2 = 0;
            //产生一个空的直线对象，用于保存拟合后的直线
            ho_Line.Dispose();
            HOperatorSet.GenEmptyObj(out ho_Line);
            //计算边缘数量
            HOperatorSet.TupleLength(hv_Cols, out hv_Length);
            //当边缘数量不小于有效点数时进行拟合
            if ((int)((new HTuple(hv_Length.TupleGreaterEqual(hv_ActiveNum))).TupleAnd(new HTuple(hv_ActiveNum.TupleGreater(
                1)))) != 0)
            {
                //halcon的拟合是基于xld的，需要把边缘连接成xld
                ho_Contour.Dispose();
                HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Rows, hv_Cols);
                //拟合直线。使用的算法是'tukey'，其他算法请参考fit_line_contour_xld的描述部分。
                HOperatorSet.FitLineContourXld(ho_Contour, "tukey", -1, 0, 5, 2, out hv_Row1,
                    out hv_Column1, out hv_Row2, out hv_Column2, out hv_Nr, out hv_Nc, out hv_Dist);
                //判断拟合结果是否有效：如果拟合成功，数组中元素的数量大于0
                HOperatorSet.TupleLength(hv_Dist, out hv_Length1);
                if ((int)(new HTuple(hv_Length1.TupleLess(1))) != 0)
                {
                    ho_Contour.Dispose();
                    return;
                }
                //根据拟合结果，产生直线xld
                ho_Line.Dispose();
                HOperatorSet.GenContourPolygonXld(out ho_Line, hv_Row1.TupleConcat(hv_Row2),
                    hv_Column1.TupleConcat(hv_Column2));
            }

            ho_Contour.Dispose();

            return;
        }



        /// <summary>
        /// 扩展直线长度
        /// </summary>
        /// <param name="hv_Row1"></param>
        /// <param name="hv_Column1"></param>
        /// <param name="hv_Row2"></param>
        /// <param name="hv_Column2"></param>
        /// <param name="hv_lineLenEx"></param>
        /// <param name="hv_RowEx1"></param>
        /// <param name="hv_ColEx1"></param>
        /// <param name="hv_RowEx2"></param>
        /// <param name="hv_ColEx2"></param>
        internal static void lineLenEx(HTuple hv_Row1, HTuple hv_Column1, HTuple hv_Row2, HTuple hv_Column2,
        HTuple hv_lineLenEx, out HTuple hv_RowEx1, out HTuple hv_ColEx1, out HTuple hv_RowEx2,
        out HTuple hv_ColEx2)
        {
            // Local iconic variables 

            // Local control variables 

            HTuple hv_Angle = null;
            // Initialize local and output iconic variables 
            HOperatorSet.AngleLx(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Angle);
            hv_RowEx1 = hv_Row1 + ((hv_Angle.TupleSin()) * hv_lineLenEx);
            hv_ColEx1 = hv_Column1 - ((hv_Angle.TupleCos()) * hv_lineLenEx);
            hv_RowEx2 = hv_Row2 - ((hv_Angle.TupleSin()) * hv_lineLenEx);
            hv_ColEx2 = hv_Column2 + ((hv_Angle.TupleCos()) * hv_lineLenEx);
            return;
        }



        internal static void disp_angle(HObject ho_Image, HTuple hv_Row1, HTuple hv_Column1, HTuple hv_Row2,
     HTuple hv_Column2, HTuple hv_Row11, HTuple hv_Column11, HTuple hv_Row21, HTuple hv_Column22, out HTuple hv_Row, out HTuple hv_Column, out HObject result_angle)
        {
            // Local iconic variables 

            HObject ho_Cross, ho_Arrow1, ho_Arrow2, ho_CircleSector, ho_result;

            // Local control variables 

            HTuple hv_IsOverlapping = null;
            HTuple hv_Angle1 = null, hv_Angle2 = null, hv_Width = null;
            HTuple hv_Height = null, hv_size = null, hv_size1 = null;
            HTuple hv_rad1 = new HTuple(), hv_rad2 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Cross);
            HOperatorSet.GenEmptyObj(out ho_Arrow1);
            HOperatorSet.GenEmptyObj(out ho_Arrow2);
            HOperatorSet.GenEmptyObj(out ho_CircleSector);
            HOperatorSet.GenEmptyObj(out ho_result);
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            HOperatorSet.IntersectionLines(hv_Row1, hv_Column1, hv_Row2, hv_Column2, hv_Row11,
                hv_Column11, hv_Row21, hv_Column22, out hv_Row, out hv_Column, out hv_IsOverlapping);
            ho_Cross.Dispose();
            if (hv_Row.Length > 0)
            {
                HOperatorSet.GenCrossContourXld(out ho_Cross, hv_Row, hv_Column, hv_Height / 50, new HTuple(45).TupleRad());
                HOperatorSet.AngleLx(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Angle1);
                HOperatorSet.AngleLx(hv_Row11, hv_Column11, hv_Row21, hv_Column22, out hv_Angle2);

                hv_size = hv_Width / 50;
                hv_size1 = hv_Width / 20;
                ho_Arrow1.Dispose();
                gen_arrow_contour_xld(out ho_Arrow1, hv_Row1, hv_Column1, hv_Row2, hv_Column2,
                    hv_size, hv_size);
                ho_Arrow2.Dispose();
                gen_arrow_contour_xld(out ho_Arrow2, hv_Row11, hv_Column11, hv_Row21, hv_Column22,
                    hv_size, hv_size);
                if ((int)(new HTuple(hv_Angle1.TupleLess(0))) != 0)
                {
                    hv_rad1 = ((new HTuple(360)).TupleRad()) + hv_Angle1;
                }
                else
                {
                    hv_rad1 = hv_Angle1.Clone();
                }
                if ((int)(new HTuple(hv_Angle2.TupleLess(0))) != 0)
                {
                    hv_rad2 = ((new HTuple(360)).TupleRad()) + hv_Angle2;
                }
                else
                {
                    hv_rad2 = hv_Angle2.Clone();
                }
                ho_CircleSector.Dispose();
                HOperatorSet.GenCircleContourXld(out ho_CircleSector, hv_Row, hv_Column, hv_size1, hv_rad1, hv_rad2, "positive", 1);



                HOperatorSet.ConcatObj(ho_result, ho_Cross, out ho_result);
                HOperatorSet.ConcatObj(ho_result, ho_CircleSector, out ho_result);
                HOperatorSet.ConcatObj(ho_result, ho_Arrow1, out ho_result);
                HOperatorSet.ConcatObj(ho_result, ho_Arrow2, out ho_result);
            }


            ho_Cross.Dispose();
            ho_Arrow1.Dispose();
            ho_Arrow2.Dispose();
            ho_CircleSector.Dispose();
            result_angle = ho_result;
        }


        internal static void disp_mesuer_ll(HObject ho_Image, HObject ho_Line1, HObject ho_Line2,
     out HTuple hv_Row1, out HTuple hv_Col1, out HTuple hv_Row2, out HTuple hv_Col2,
     out HTuple hv_RowProj1, out HTuple hv_ColProj1, out HTuple hv_RowProj2, out HTuple hv_ColProj2, out HObject result)
        {
            // Local iconic variables 

            HObject ho_Arrow1, ho_Arrow2, ho_Arrow3, ho_Arrow4, ho_Result;

            // Local control variables 

            HTuple hv_Width = null, hv_Height = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Arrow1);
            HOperatorSet.GenEmptyObj(out ho_Arrow2);
            HOperatorSet.GenEmptyObj(out ho_Arrow3);
            HOperatorSet.GenEmptyObj(out ho_Arrow4);
            HOperatorSet.GenEmptyObj(out ho_Result);
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            HOperatorSet.GetContourXld(ho_Line1, out hv_Row1, out hv_Col1);
            HOperatorSet.GetContourXld(ho_Line2, out hv_Row2, out hv_Col2);
            HOperatorSet.ProjectionPl(hv_Row2.TupleSelect(0), hv_Col2.TupleSelect(0), hv_Row1.TupleSelect(
                0), hv_Col1.TupleSelect(0), hv_Row1.TupleSelect(1), hv_Col1.TupleSelect(1),
                out hv_RowProj1, out hv_ColProj1);
            ho_Arrow1.Dispose();
            gen_arrow_contour_xld(out ho_Arrow1, hv_Row2.TupleSelect(0), hv_Col2.TupleSelect(
                 0), hv_RowProj1, hv_ColProj1, hv_Height / 50, hv_Height / 50);
            ho_Arrow2.Dispose();
            gen_arrow_contour_xld(out ho_Arrow2, hv_RowProj1, hv_ColProj1, hv_Row2.TupleSelect(
                0), hv_Col2.TupleSelect(0), hv_Height / 50, hv_Height / 50);
            HOperatorSet.ConcatObj(ho_Result, ho_Arrow1, out ho_Result);
            HOperatorSet.ConcatObj(ho_Result, ho_Arrow2, out ho_Result);
            HOperatorSet.ProjectionPl(hv_Row2.TupleSelect(1), hv_Col2.TupleSelect(1), hv_Row1.TupleSelect(
                0), hv_Col1.TupleSelect(0), hv_Row1.TupleSelect(1), hv_Col1.TupleSelect(1),
                out hv_RowProj2, out hv_ColProj2);
            ho_Arrow3.Dispose();
            gen_arrow_contour_xld(out ho_Arrow3, hv_Row2.TupleSelect(1), hv_Col2.TupleSelect(
                1), hv_RowProj2, hv_ColProj2, hv_Height / 50, hv_Height / 50);
            ho_Arrow4.Dispose();
            gen_arrow_contour_xld(out ho_Arrow4, hv_RowProj2, hv_ColProj2, hv_Row2.TupleSelect(
                1), hv_Col2.TupleSelect(1), hv_Height / 50, hv_Height / 50);
            HOperatorSet.ConcatObj(ho_Result, ho_Arrow3, out ho_Result);
            HOperatorSet.ConcatObj(ho_Result, ho_Arrow4, out ho_Result);
            ho_Arrow1.Dispose();
            ho_Arrow2.Dispose();
            ho_Arrow3.Dispose();
            ho_Arrow4.Dispose();
            result = ho_Result;
            return;
        }



        internal static void get_cenline_point(HTuple hv_Row1, HTuple hv_Column1, HTuple hv_Row2,
      HTuple hv_Column2, out HTuple hv_Angle, out HTuple hv_cenrow, out HTuple hv_cencol)
        {
            // Local iconic variables 
            // Local control variables 
            HTuple hv_Distance = null;
            // Initialize local and output iconic variables 
            HOperatorSet.AngleLx(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Angle);
            HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Distance);
            hv_cenrow = hv_Row1 - ((hv_Angle.TupleSin()) * (hv_Distance / 2));
            hv_cencol = hv_Column1 + ((hv_Angle.TupleCos()) * (hv_Distance / 2));
            HOperatorSet.TupleAbs(hv_cenrow, out hv_cenrow);
            return;
        }

        internal static void spoke(HObject ho_Image, out HObject ho_Regions, HTuple hv_Elements,
          HTuple hv_DetectHeight, HTuple hv_DetectWidth, HTuple hv_Sigma, HTuple hv_Threshold,
          HTuple hv_Transition, HTuple hv_Select, HTuple hv_ROIRows, HTuple hv_ROICols,
          HTuple hv_Direct, out HTuple hv_ResultRow, out HTuple hv_ResultColumn, out HTuple hv_ArcType)
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            // Local iconic variables 
            HObject ho_Contour, ho_ContCircle, ho_Rectangle1 = null;
            HObject ho_Arrow1 = null;
            // Local control variables 
            HTuple hv_Width = null, hv_Height = null, hv_RowC = null;
            HTuple hv_ColumnC = null, hv_Radius = null, hv_StartPhi = null;
            HTuple hv_EndPhi = null, hv_PointOrder = null, hv_RowXLD = null;
            HTuple hv_ColXLD = null, hv_Length2 = null, hv_i = null;
            HTuple hv_j = new HTuple(), hv_RowE = new HTuple(), hv_ColE = new HTuple();
            HTuple hv_ATan = new HTuple(), hv_RowL2 = new HTuple();
            HTuple hv_RowL1 = new HTuple(), hv_ColL2 = new HTuple();
            HTuple hv_ColL1 = new HTuple(), hv_MsrHandle_Measure = new HTuple();
            HTuple hv_RowEdge = new HTuple(), hv_ColEdge = new HTuple();
            HTuple hv_Amplitude = new HTuple(), hv_Distance = new HTuple();
            HTuple hv_tRow = new HTuple(), hv_tCol = new HTuple();
            HTuple hv_t = new HTuple(), hv_Number = new HTuple(), hv_k = new HTuple();
            HTuple hv_Select_COPY_INP_TMP = hv_Select.Clone();
            HTuple hv_Transition_COPY_INP_TMP = hv_Transition.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_Contour);
            HOperatorSet.GenEmptyObj(out ho_ContCircle);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_Arrow1);
            hv_ArcType = new HTuple();
            //获取图像尺寸
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            //产生一个空显示对象，用于显示
            ho_Regions.Dispose();
            HOperatorSet.GenEmptyObj(out ho_Regions);
            //初始化边缘坐标数组
            hv_ResultRow = new HTuple();
            hv_ResultColumn = new HTuple();

            //产生xld
            ho_Contour.Dispose();
            HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_ROIRows, hv_ROICols);
            //用回归线法（不抛出异常点，所有点权重一样）拟合圆

            HOperatorSet.FitCircleContourXld(ho_Contour, "algebraic", -1, 0, 0, 1, 2, out hv_RowC,
                out hv_ColumnC, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
            //根据拟合结果产生xld，并保持到显示对象
            ho_ContCircle.Dispose();
            HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_RowC, hv_ColumnC, hv_Radius,
                hv_StartPhi, hv_EndPhi, hv_PointOrder, 3);
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.ConcatObj(ho_Regions, ho_ContCircle, out ExpTmpOutVar_0);
                ho_Regions.Dispose();
                ho_Regions = ExpTmpOutVar_0;
            }

            //获取圆或圆弧xld上的点坐标
            HOperatorSet.GetContourXld(ho_ContCircle, out hv_RowXLD, out hv_ColXLD);

            //求圆或圆弧xld上的点的数量
            HOperatorSet.TupleLength(hv_ColXLD, out hv_Length2);
            if ((int)(new HTuple(hv_Elements.TupleLess(3))) != 0)
            {
                ho_Contour.Dispose();
                ho_ContCircle.Dispose();
                ho_Rectangle1.Dispose();
                ho_Arrow1.Dispose();

                return;
            }
            //如果xld是圆弧，有Length2个点，从起点开始，等间距（间距为Length2/(Elements-1)）取Elements个点，作为卡尺工具的中点
            //如果xld是圆，有Length2个点，以0°为起点，从起点开始，等间距（间距为Length2/(Elements)）取Elements个点，作为卡尺工具的中点
            HTuple end_val28 = hv_Elements - 1;
            HTuple step_val28 = 1;
            for (hv_i = 0; hv_i.Continue(end_val28, step_val28); hv_i = hv_i.TupleAdd(step_val28))
            {

                if ((int)(new HTuple(((hv_RowXLD.TupleSelect(0))).TupleEqual(hv_RowXLD.TupleSelect(
                    hv_Length2 - 1)))) != 0)
                {
                    //xld的起点和终点坐标相对，为圆
                    HOperatorSet.TupleInt(((1.0 * hv_Length2) / hv_Elements) * hv_i, out hv_j);
                    hv_ArcType = "circle";
                }
                else
                {
                    //否则为圆弧
                    HOperatorSet.TupleInt(((1.0 * hv_Length2) / (hv_Elements - 1)) * hv_i, out hv_j);
                    hv_ArcType = "arc";
                }
                //索引越界，强制赋值为最后一个索引
                if ((int)(new HTuple(hv_j.TupleGreaterEqual(hv_Length2))) != 0)
                {
                    hv_j = hv_Length2 - 1;
                    //continue
                }
                //获取卡尺工具中心
                hv_RowE = hv_RowXLD.TupleSelect(hv_j);
                hv_ColE = hv_ColXLD.TupleSelect(hv_j);

                //超出图像区域，不检测，否则容易报异常
                if ((int)((new HTuple((new HTuple((new HTuple(hv_RowE.TupleGreater(hv_Height - 1))).TupleOr(
                    new HTuple(hv_RowE.TupleLess(0))))).TupleOr(new HTuple(hv_ColE.TupleGreater(
                    hv_Width - 1))))).TupleOr(new HTuple(hv_ColE.TupleLess(0)))) != 0)
                {
                    continue;
                }
                //边缘搜索方向类型：'inner'搜索方向由圆外指向圆心；'outer'搜索方向由圆心指向圆外
                if ((int)(new HTuple(hv_Direct.TupleEqual("inner"))) != 0)
                {
                    //求卡尺工具的边缘搜索方向
                    //求圆心指向边缘的矢量的角度
                    HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                    //角度反向
                    hv_ATan = ((new HTuple(180)).TupleRad()) + hv_ATan;
                }
                else
                {
                    //求卡尺工具的边缘搜索方向
                    //求圆心指向边缘的矢量的角度
                    HOperatorSet.TupleAtan2((-hv_RowE) + hv_RowC, hv_ColE - hv_ColumnC, out hv_ATan);
                }


                //产生卡尺xld，并保持到显示对象
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle1, hv_RowE, hv_ColE, hv_ATan,
                    hv_DetectHeight / 2, hv_DetectWidth / 2);
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_Regions, ho_Rectangle1, out ExpTmpOutVar_0);
                    ho_Regions.Dispose();
                    ho_Regions = ExpTmpOutVar_0;
                }
                //用箭头xld指示边缘搜索方向，并保持到显示对象
                if ((int)(new HTuple(hv_i.TupleEqual(0))) != 0)
                {
                    hv_RowL2 = hv_RowE + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                    hv_RowL1 = hv_RowE - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleSin()));
                    hv_ColL2 = hv_ColE + ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                    hv_ColL1 = hv_ColE - ((hv_DetectHeight / 2) * (((-hv_ATan)).TupleCos()));
                    ho_Arrow1.Dispose();
                    gen_arrow_contour_xld(out ho_Arrow1, hv_RowL1, hv_ColL1, hv_RowL2, hv_ColL2,
                        25, 25);
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConcatObj(ho_Regions, ho_Arrow1, out ExpTmpOutVar_0);
                        ho_Regions.Dispose();
                        ho_Regions = ExpTmpOutVar_0;
                    }
                }


                //产生测量对象句柄
                HOperatorSet.GenMeasureRectangle2(hv_RowE, hv_ColE, hv_ATan, hv_DetectHeight / 2,
                    hv_DetectWidth / 2, hv_Width, hv_Height, "nearest_neighbor", out hv_MsrHandle_Measure);

                //设置极性
                if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("negative"))) != 0)
                {
                    hv_Transition_COPY_INP_TMP = "negative";
                }
                else
                {
                    if ((int)(new HTuple(hv_Transition_COPY_INP_TMP.TupleEqual("positive"))) != 0)
                    {

                        hv_Transition_COPY_INP_TMP = "positive";
                    }
                    else
                    {
                        hv_Transition_COPY_INP_TMP = "all";
                    }
                }
                //设置边缘位置。最强点是从所有边缘中选择幅度绝对值最大点，需要设置为'all'
                if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("first"))) != 0)
                {
                    hv_Select_COPY_INP_TMP = "first";
                }
                else
                {
                    if ((int)(new HTuple(hv_Select_COPY_INP_TMP.TupleEqual("last"))) != 0)
                    {

                        hv_Select_COPY_INP_TMP = "last";
                    }
                    else
                    {
                        hv_Select_COPY_INP_TMP = "all";
                    }
                }
                //检测边缘
                HOperatorSet.MeasurePos(ho_Image, hv_MsrHandle_Measure, hv_Sigma, hv_Threshold,
                    hv_Transition_COPY_INP_TMP, hv_Select_COPY_INP_TMP, out hv_RowEdge, out hv_ColEdge,
                    out hv_Amplitude, out hv_Distance);
                //清除测量对象句柄
                HOperatorSet.CloseMeasure(hv_MsrHandle_Measure);
                //临时变量初始化
                //tRow，tCol保存找到指定边缘的坐标
                hv_tRow = 0;
                hv_tCol = 0;
                //t保存边缘的幅度绝对值
                hv_t = 0;
                HOperatorSet.TupleLength(hv_RowEdge, out hv_Number);
                //找到的边缘必须至少为1个
                if ((int)(new HTuple(hv_Number.TupleLess(1))) != 0)
                {
                    continue;
                }
                //有多个边缘时，选择幅度绝对值最大的边缘
                HTuple end_val121 = hv_Number - 1;
                HTuple step_val121 = 1;
                for (hv_k = 0; hv_k.Continue(end_val121, step_val121); hv_k = hv_k.TupleAdd(step_val121))
                {
                    if ((int)(new HTuple(((((hv_Amplitude.TupleSelect(hv_k))).TupleAbs())).TupleGreater(
                        hv_t))) != 0)
                    {

                        hv_tRow = hv_RowEdge.TupleSelect(hv_k);
                        hv_tCol = hv_ColEdge.TupleSelect(hv_k);
                        hv_t = ((hv_Amplitude.TupleSelect(hv_k))).TupleAbs();
                    }
                }
                //把找到的边缘保存在输出数组
                if ((int)(new HTuple(hv_t.TupleGreater(0))) != 0)
                {

                    hv_ResultRow = hv_ResultRow.TupleConcat(hv_tRow);
                    hv_ResultColumn = hv_ResultColumn.TupleConcat(hv_tCol);
                }
            }


            ho_Contour.Dispose();
            ho_ContCircle.Dispose();
            ho_Rectangle1.Dispose();
            ho_Arrow1.Dispose();

            return;
        }


        internal static void pts_to_best_circle(out HObject ho_Circle, HTuple hv_Rows, HTuple hv_Cols,
         HTuple hv_ActiveNum, HTuple hv_ArcType, out HTuple hv_RowCenter, out HTuple hv_ColCenter,
         out HTuple hv_Radius, out HTuple hv_StartPhi, out HTuple hv_EndPhi, out HTuple hv_PointOrder,
         out HTuple hv_ArcAngle)
        {



            // Local iconic variables 

            HObject ho_Contour = null;

            // Local control variables 

            HTuple hv_Length = null, hv_Length1 = new HTuple();
            HTuple hv_CircleLength = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_Contour);
            hv_StartPhi = new HTuple();
            hv_EndPhi = new HTuple();
            hv_PointOrder = new HTuple();
            hv_ArcAngle = new HTuple();
            //初始化
            hv_RowCenter = 0;
            hv_ColCenter = 0;
            hv_Radius = 0;
            //产生一个空的直线对象，用于保存拟合后的圆
            ho_Circle.Dispose();
            HOperatorSet.GenEmptyObj(out ho_Circle);
            //计算边缘数量
            HOperatorSet.TupleLength(hv_Cols, out hv_Length);
            //当边缘数量不小于有效点数时进行拟合
            if ((int)((new HTuple(hv_Length.TupleGreaterEqual(hv_ActiveNum))).TupleAnd(new HTuple(hv_ActiveNum.TupleGreater(
                2)))) != 0)
            {
                //halcon的拟合是基于xld的，需要把边缘连接成xld
                if ((int)(new HTuple(hv_ArcType.TupleEqual("circle"))) != 0)
                {
                    //如果是闭合的圆，轮廓需要首尾相连
                    ho_Contour.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Rows.TupleConcat(hv_Rows.TupleSelect(
                        0)), hv_Cols.TupleConcat(hv_Cols.TupleSelect(0)));
                }
                else
                {
                    ho_Contour.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Rows, hv_Cols);
                }
                //拟合圆。使用的算法是''geotukey''，其他算法请参考fit_circle_contour_xld的描述部分。
                HOperatorSet.FitCircleContourXld(ho_Contour, "geotukey", -1, 0, 0, 3, 2, out hv_RowCenter,
                    out hv_ColCenter, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
                //判断拟合结果是否有效：如果拟合成功，数组中元素的数量大于0
                HOperatorSet.TupleLength(hv_StartPhi, out hv_Length1);
                if ((int)(new HTuple(hv_Length1.TupleLess(1))) != 0)
                {
                    ho_Contour.Dispose();

                    return;
                }
                //根据拟合结果，产生直线xld
                if ((int)(new HTuple(hv_ArcType.TupleEqual("arc"))) != 0)
                {
                    //判断圆弧的方向：顺时针还是逆时针
                    //halcon求圆弧会出现方向混乱的问题
                    //tuple_mean (Rows, RowsMean)
                    //tuple_mean (Cols, ColsMean)
                    //gen_cross_contour_xld (Cross, RowsMean, ColsMean, 6, 0.785398)
                    //gen_circle_contour_xld (Circle1, RowCenter, ColCenter, Radius, StartPhi, EndPhi, 'positive', 1)
                    //求轮廓1中心
                    //area_center_points_xld (Circle1, Area, Row1, Column1)
                    //gen_circle_contour_xld (Circle2, RowCenter, ColCenter, Radius, StartPhi, EndPhi, 'negative', 1)
                    //求轮廓2中心
                    //area_center_points_xld (Circle2, Area, Row2, Column2)
                    //distance_pp (RowsMean, ColsMean, Row1, Column1, Distance1)
                    //distance_pp (RowsMean, ColsMean, Row2, Column2, Distance2)
                    //ArcAngle := EndPhi-StartPhi
                    //if (Distance1<Distance2)

                    //PointOrder := 'positive'
                    //copy_obj (Circle1, Circle, 1, 1)
                    //else

                    //PointOrder := 'negative'
                    //if (abs(ArcAngle)>3.1415926)
                    //ArcAngle := ArcAngle-2.0*3.1415926
                    //endif
                    //copy_obj (Circle2, Circle, 1, 1)
                    //endif
                    ho_Circle.Dispose();
                    HOperatorSet.GenCircleContourXld(out ho_Circle, hv_RowCenter, hv_ColCenter,
                        hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder, 1);

                    HOperatorSet.LengthXld(ho_Circle, out hv_CircleLength);
                    hv_ArcAngle = hv_EndPhi - hv_StartPhi;
                    if ((int)(new HTuple(hv_CircleLength.TupleGreater(((new HTuple(180)).TupleRad()
                        ) * hv_Radius))) != 0)
                    {
                        if ((int)(new HTuple(((hv_ArcAngle.TupleAbs())).TupleLess((new HTuple(180)).TupleRad()
                            ))) != 0)
                        {
                            if ((int)(new HTuple(hv_ArcAngle.TupleGreater(0))) != 0)
                            {
                                hv_ArcAngle = ((new HTuple(360)).TupleRad()) - hv_ArcAngle;
                            }
                            else
                            {

                                hv_ArcAngle = ((new HTuple(360)).TupleRad()) + hv_ArcAngle;
                            }
                        }
                    }
                    else
                    {
                        if ((int)(new HTuple(hv_CircleLength.TupleLess(((new HTuple(180)).TupleRad()
                            ) * hv_Radius))) != 0)
                        {
                            if ((int)(new HTuple(((hv_ArcAngle.TupleAbs())).TupleGreater((new HTuple(180)).TupleRad()
                                ))) != 0)
                            {
                                if ((int)(new HTuple(hv_ArcAngle.TupleGreater(0))) != 0)
                                {
                                    hv_ArcAngle = hv_ArcAngle - ((new HTuple(360)).TupleRad());

                                }
                                else
                                {
                                    hv_ArcAngle = ((new HTuple(360)).TupleRad()) + hv_ArcAngle;
                                }
                            }
                        }

                    }

                }
                else
                {
                    hv_StartPhi = 0;
                    hv_EndPhi = (new HTuple(360)).TupleRad();
                    hv_ArcAngle = (new HTuple(360)).TupleRad();
                    ho_Circle.Dispose();
                    HOperatorSet.GenCircleContourXld(out ho_Circle, hv_RowCenter, hv_ColCenter,
                        hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder, 1);
                }
            }

            ho_Contour.Dispose();

            return;
        }



    }
}
