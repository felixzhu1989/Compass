using SolidWorks.Interop.sldworks;
using System;

namespace SolidWorksHelper
{
    internal class HuaWeiHoodPart
    {
        #region 华为UV650排风腔零件

        /// <summary>
        /// 排风腔体
        /// </summary>
        public void FNHE0186(Component2 swComp, double length, int midRoofHoleNo, double midRoofSecondHoleDis, int exNo, double exLength, double exWidth, double exDis, double exRightDis, string waterCollection, string sidePanel, string outlet, string backToBack, string ansul, string anSide, string anDetector, string UVType)
        {
            #region 基本尺寸

            ModelDoc2 swPart = swComp.GetModelDoc2();//打开零件3
            swPart.ChangeDim("D1@草图1", length);
            swPart.ChangeDim("D2@Sketch3", midRoofSecondHoleDis);
            if (midRoofHoleNo == 1)
            {
                swComp.Suppress("LPattern1");
            }
            else
            {
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D1@LPattern1", midRoofHoleNo);
            }

            #endregion 基本尺寸

            #region 排风口

            if (exNo == 1)
            {
                swComp.UnSuppress("EXCOONE");
                swComp.Suppress("EXCOTWO");
                swPart.ChangeDim("D4@Sketch9", exRightDis);
                swPart.ChangeDim("D2@Sketch9", exLength);
                swPart.ChangeDim("D3@Sketch9", exWidth);
            }
            else
            {
                swComp.Suppress("EXCOONE");
                swComp.UnSuppress("EXCOTWO");
                swPart.ChangeDim("D5@Sketch10", exRightDis);
                swPart.ChangeDim("D1@Sketch10", exDis);
                swPart.ChangeDim("D3@Sketch10", exLength);
                swPart.ChangeDim("D4@Sketch10", exWidth);
            }

            #endregion 排风口

            #region 集水翻边

            if (waterCollection == "YES")
            {
                if (sidePanel == "RIGHT")
                {
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                    swComp.Suppress("DRAINCHANNEL-LEFT");
                }
                else if (sidePanel == "LEFT")
                {
                    swComp.Suppress("DRAINCHANNEL-RIGHT");
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                }
                else if (sidePanel == "BOTH")
                {
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                }
            }
            else
            {
                swComp.Suppress("DRAINCHANNEL-RIGHT");
                swComp.Suppress("DRAINCHANNEL-LEFT");
            }

            #endregion 集水翻边

            #region 油塞

            if (outlet == "LEFTTAP")
            {
                swComp.UnSuppress("DRAINTAP-LEFT");
                swComp.Suppress("DRAINTAP-RIGHT");
            }
            else if (outlet == "RIGHTTAP")
            {
                swComp.Suppress("DRAINTAP-LEFT");
                swComp.UnSuppress("DRAINTAP-RIGHT");
            }
            else
            {
                swComp.Suppress("DRAINTAP-LEFT");
                swComp.Suppress("DRAINTAP-RIGHT");
            }

            #endregion 油塞

            #region 背靠背

            if (backToBack == "YES")
            {
                swComp.UnSuppress("BACKTOBACK");
            }
            else
            {
                swComp.Suppress("BACKTOBACK");
            }

            #endregion 背靠背

            #region ANSUL

            if (ansul == "YES")
            {
                //侧喷
                if (anSide == "LEFT")
                {
                    swComp.UnSuppress("ANSUL-LEFT");
                    swComp.UnSuppress("CHANNEL-LEFT");
                    swComp.Suppress("ANSUL-RIGHT");
                    swComp.Suppress("CHANNEL-RIGHT");
                }
                if (anSide == "RIGHT")
                {
                    swComp.UnSuppress("ANSUL-RIGHT");
                    swComp.UnSuppress("CHANNEL-RIGHT");
                    swComp.Suppress("ANSUL-LEFT");
                    swComp.Suppress("CHANNEL-LEFT");
                }

                //探测器
                swComp.Suppress("ANDTEC-RIGHT");
                swComp.Suppress("ANDTEC-LEFT");
                if (anDetector == "RIGHT" || anDetector == "BOTH")
                {
                    swComp.UnSuppress("ANDTEC-RIGHT");
                }
                if (anDetector == "LEFT" || anDetector == "BOTH")
                {
                    swComp.UnSuppress("ANDTEC-LEFT");
                }
            }
            else
            {
                swComp.Suppress("ANSUL-LEFT");
                swComp.Suppress("ANSUL-RIGHT");
                swComp.Suppress("CHANNEL-LEFT");
                swComp.Suppress("CHANNEL-RIGHT");
                swComp.Suppress("ANDTEC-LEFT");
                swComp.Suppress("ANDTEC-RIGHT");
            }

            #endregion ANSUL

            #region MARVEL

            //swComp.UnSuppress("MA-NTC");
            //if (item.MARVEL == "YES")
            //{
            //
            //    if (exNo == 1) swPart.ChangeDim("D1@Sketch21", (exRightDis + exLength / 2 + 50d));
            //    else swPart.ChangeDim("D1@Sketch21", (exRightDis + exDis / 2 + exLength + 50d));
            //}
            //else

            #endregion MARVEL

            #region UV灯门

            //UVHood,UVRack-UV灯架孔和UV cable-UV灯线缆穿孔
            if (UVType == "DOUBLE")
            {
                swComp.UnSuppress("UVDOUBLE");
                swPart.ChangeDim("D7@Sketch17", exRightDis - 3.2d);
                swComp.Suppress("UVRACK");
            }
            else if (UVType == "LONG")
            {
                swComp.UnSuppress("UVRACK");
                swPart.ChangeDim("D6@Sketch12", exRightDis);
                swPart.ChangeDim("D5@Sketch12", 1622d);
                swComp.Suppress("UVDOUBLE");
            }
            else if (UVType == "SHORT")
            {
                swComp.UnSuppress("UVRACK");
                swPart.ChangeDim("D6@Sketch12", exRightDis);
                swPart.ChangeDim("D5@Sketch12", 912d);
                swComp.Suppress("UVDOUBLE");
            }
            else
            {
                //非UVHood
                swComp.Suppress("UVRACK");
                swComp.Suppress("UVDOUBLE");
            }
            #endregion UV灯门
        }

        /// <summary>
        /// 排风腔前面板
        /// </summary>
        public void FNHE0187(Component2 swComp, double length, string UVType, double exRightDis)
        {
            #region 基本尺寸

            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length);
            //1150@Sketch14=22,D2@Sketch14=35，原先都是50
            if (length==1150)
            {
                swPart.ChangeDim("D3@Sketch14", 22d);
                swPart.ChangeDim("D2@Sketch14", 35d);
            }
            else
            {
                swPart.ChangeDim("D3@Sketch14", 50d);
                swPart.ChangeDim("D2@Sketch14", 50d);
            }
            #endregion 基本尺寸

            #region UV灯门

            //UV HoodParent,过滤器感应出线孔，UV门，UV cable-UV灯线缆穿孔避让缺口
            if (UVType == "DOUBLE")
            {
                swComp.UnSuppress("UVDOOR-DOUBLE");
                swPart.ChangeDim("D1@Sketch37", exRightDis);
                swComp.UnSuppress("UVCABLE-DOUBLE");
                swPart.ChangeDim("D1@Sketch41", exRightDis);
                swComp.Suppress("UVDOOR-SHORT");
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVCABLE");
            }
            else if (UVType == "LONG")
            {
                swComp.UnSuppress("UVDOOR-LONG");
                swPart.ChangeDim("D1@Sketch40", exRightDis);
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D1@Sketch42", exRightDis);
                swPart.ChangeDim("D2@Sketch42", 1500d);
                swComp.Suppress("UVDOOR-SHORT");
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.Suppress("UVCABLE-DOUBLE");
            }
            else if (UVType == "SHORT")
            {
                swComp.UnSuppress("UVDOOR-SHORT");
                swPart.ChangeDim("D1@Sketch39", exRightDis);
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D1@Sketch42", exRightDis);
                swPart.ChangeDim("D2@Sketch42", 790d);
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.Suppress("UVCABLE-DOUBLE");
            }
            else
            {
                swComp.Suppress("UVDOOR-SHORT");
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVCABLE");
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.Suppress("UVCABLE-DOUBLE");
            }
            #endregion UV灯门
        }

        /// <summary>
        /// MESH油网后导轨
        /// </summary>
        public void FNHE0188(Component2 swComp, double length, string ansul, string anDetector)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", (length - 12d));
            if (ansul == "YES")
            {
                swComp.Suppress("ANDTEC-RIGHT");
                swComp.Suppress("ANDTEC-LEFT");
                //探测器
                if (anDetector == "RIGHT" || anDetector == "BOTH")
                {
                    swComp.UnSuppress("ANDTEC-RIGHT");
                }
                if (anDetector == "LEFT" || anDetector == "BOTH")
                {
                    swComp.UnSuppress("ANDTEC-LEFT");
                }
            }
            else
            {
                swComp.Suppress("ANDTEC-RIGHT");
                swComp.Suppress("ANDTEC-LEFT");
            }
        }

        /// <summary>
        /// MESH油网前导轨
        /// </summary>
        public void FNHE0189(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Sketch1", length - 7d);
        }

        /// <summary>
        /// MESH油网轨道支架
        /// </summary>
        public void FNHE0190(Component2 swComp, double length, string UVType, double exRightDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 7d);
            //UV灯门铰链孔
            if (UVType == "DOUBLE")
            {
                swComp.UnSuppress("UVDOOR-DOUBLE");
                swPart.ChangeDim("D1@Sketch8", exRightDis - 3.5d);
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVDOOR-SHORT");
            }
            else if (UVType == "LONG")
            {
                swComp.UnSuppress("UVDOOR-LONG");

                swPart.ChangeDim("D1@Sketch9", (exRightDis - 2.5d));
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.Suppress("UVDOOR-SHORT");
            }
            else if (UVType == "SHORT")
            {
                swComp.UnSuppress("UVDOOR-SHORT");
                swPart.ChangeDim("D1@Sketch10", (exRightDis - 2.5d));
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.Suppress("UVDOOR-LONG");
            }
            else
            {
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVDOOR-SHORT");
            }
        }

        /// <summary>
        /// MESH油网侧板，非水洗
        /// </summary>
        public void MeshFilter(AssemblyDoc swAssy, string suffix, double length, string ansul, string anSide, string leftPart, string rightPart)
        {
            //MESH侧板长度(除去排风三角板3dm计算)，肖启才说，手划伤了，华为不锈钢mesh用498.5去计算,包边再减去4
            double meshSideLength = Convert.ToDouble((length - 3d-4d - (int)((length - 2d) / 498.5d) * 498.5d) / 2);

            Component2 swComp;
            ModelDoc2 swPart;
            if (ansul == "YES")
            {
                if (meshSideLength * 2d < 55d) meshSideLength += 249d;
                if ((meshSideLength - 20d) > 55d)
                {
                    if (anSide == "LEFT")
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        swComp.UnSuppress("ANSUL");
                        swComp.Suppress("KW");
                        swPart.ChangeDim("D3@Sketch25", 30d);
                        swPart.ChangeDim("D3@Sketch26", 190d);

                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        swComp.Suppress("ANSUL");
                        swComp.Suppress("KW");
                        swPart.ChangeDim("D1@Sketch25", 30d);
                        swPart.ChangeDim("D3@Sketch26", 190d);
                    }
                    else if (anSide == "RIGHT")
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        swComp.Suppress("ANSUL");
                        swComp.Suppress("KW");
                        swPart.ChangeDim("D3@Sketch25", 30d);
                        swPart.ChangeDim("D3@Sketch26", 190d);

                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        swComp.UnSuppress("ANSUL");
                        swComp.Suppress("KW");
                        swPart.ChangeDim("D1@Sketch25", 30d);
                        swPart.ChangeDim("D3@Sketch26", 190d);
                    }
                    else
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        swComp.Suppress("ANSUL");
                        swComp.Suppress("KW");
                        swPart.ChangeDim("D3@Sketch25", 30d);
                        swPart.ChangeDim("D3@Sketch26", 190d);
                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        swComp.Suppress("ANSUL");
                        swComp.Suppress("KW");
                        swPart.ChangeDim("D1@Sketch25", 30d);
                        swPart.ChangeDim("D3@Sketch26", 190d);
                    }
                }
                else
                {
                    if (anSide == "LEFT")
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", 2d * meshSideLength);
                        swComp.UnSuppress("ANSUL");
                        swComp.Suppress("KW");
                        swPart.ChangeDim("D3@Sketch25", 30d);
                        swPart.ChangeDim("D3@Sketch26", 190d);
                        swAssy.Suppress(suffix, rightPart);
                    }
                    else if (anSide == "RIGHT")
                    {
                        swAssy.Suppress(suffix, leftPart);
                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", 2d * meshSideLength);
                        swComp.UnSuppress("ANSUL");
                        swComp.Suppress("KW");
                        swPart.ChangeDim("D1@Sketch25", 30d);
                        swPart.ChangeDim("D3@Sketch26", 190d);
                    }
                    else
                    {
                        swAssy.Suppress(suffix, leftPart);
                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", 2 * meshSideLength);
                        swComp.Suppress("ANSUL");
                        swComp.Suppress("KW");
                        swPart.ChangeDim("D1@Sketch25", 30d);
                        swPart.ChangeDim("D3@Sketch26", 190d);
                    }
                }
            }
            else
            {
                if (2d * meshSideLength < 15d && meshSideLength > 1.5d) meshSideLength += 249d;
                if (meshSideLength > 30d)
                {
                    swComp = swAssy.UnSuppress(suffix, leftPart);
                    swPart = swComp.GetModelDoc2();
                    swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                    swComp.Suppress("ANSUL");
                    swComp.Suppress("KW");
                    swPart.ChangeDim("D3@Sketch25", 30d);
                    swPart.ChangeDim("D3@Sketch26", 190d);
                    swComp = swAssy.UnSuppress(suffix, rightPart);
                    swPart = swComp.GetModelDoc2();
                    swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                    swComp.Suppress("ANSUL");
                    swComp.Suppress("KW");
                    swPart.ChangeDim("D1@Sketch25", 30d);
                    swPart.ChangeDim("D3@Sketch26", 190d);
                }
                else if (meshSideLength <= 30d && meshSideLength > 1.5d)
                {
                    swAssy.Suppress(suffix, leftPart);
                    swComp = swAssy.UnSuppress(suffix, rightPart);
                    swPart = swComp.GetModelDoc2();
                    swPart.ChangeDim("D2@Sketch1", 2 * meshSideLength);
                    swComp.Suppress("ANSUL");
                    swComp.Suppress("KW");
                    swPart.ChangeDim("D1@Sketch25", 30d);
                    swPart.ChangeDim("D3@Sketch26", 190d);
                }
                else
                {
                    swAssy.Suppress(suffix, leftPart);
                    swAssy.Suppress(suffix, rightPart);
                }
            }
        }

        #endregion 华为UV650排风腔零件

        #region 华为水洗UW650排风腔零件

        /// <summary>
        /// 华为水洗UW650,UW555排风腔背板
        /// </summary>
        public void FNHE0179(Component2 swComp, double length, string waterCollection, string sidePanel, string outlet, string backToBack)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();//打开零件3
            swPart.ChangeDim("D1@草图1", length);

            #region 集水翻边

            if (waterCollection == "YES")
            {
                if (sidePanel == "RIGHT")
                {
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                    swComp.Suppress("DRAINCHANNEL-LEFT");
                }
                else if (sidePanel == "LEFT")
                {
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                    swComp.Suppress("DRAINCHANNEL-RIGHT");
                }
                else if (sidePanel == "BOTH")
                {
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                }
            }
            else
            {
                swComp.Suppress("DRAINCHANNEL-RIGHT");
                swComp.Suppress("DRAINCHANNEL-LEFT");
            }

            #endregion 集水翻边

            #region 下水口

            if (outlet == "LEFT")
            {
                swComp.UnSuppress("DRAINPIPE-LEFT");
                swComp.Suppress("DRAINPIPE-RIGHT");
            }
            else if (outlet == "RIGHT")
            {
                swComp.Suppress("DRAINPIPE-LEFT");
                swComp.UnSuppress("DRAINPIPE-RIGHT");
            }
            else
            {
                swComp.Suppress("DRAINPIPE-LEFT");
                swComp.Suppress("DRAINPIPE-RIGHT");
            }

            #endregion 下水口

            #region 背靠背

            if (backToBack == "YES")
            {
                swComp.UnSuppress("BACKTOBACK");
            }
            else
            {
                swComp.Suppress("BACKTOBACK");
            }

            #endregion 背靠背
        }

        /// <summary>
        /// 华为水洗UW650排风腔顶部零件
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="length">烟罩长度</param>
        /// <param name="midRoofSecondHoleDis">灯板第二个安装孔</param>
        /// <param name="midRoofHoleNo">灯板安装孔数量</param>
        /// <param name="exNo">排风脖颈数量</param>
        /// <param name="exRightDis">排风中心距离右边距离</param>
        /// <param name="exLength">排风脖颈长度</param>
        /// <param name="exWidth">排风脖颈宽度</param>
        /// <param name="exDis">排风脖颈间距</param>
        /// <param name="inlet">入水口位置</param>
        /// <param name="ANSUL">ANSUL选项</param>
        /// <param name="ANSide">ANSUL侧喷位置</param>
        /// <param name="MARVEL">MARVEL选项</param>
        /// <param name="UVType">UV灯类型，没有UV灯请填写null</param>
        /// <param name="UWHood">是否为UW烟罩，不是请填写null</param>
        public void FNHE0148(Component2 swComp, double length, double midRoofSecondHoleDis, int midRoofHoleNo, int exNo, double exRightDis, double exLength, double exWidth, double exDis, string inlet, string ANSUL, string ANSide, string MARVEL, string UVType, string UWHood)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();//打开零件3
            swPart.ChangeDim("D1@草图1", length);
            swPart.ChangeDim("D1@Sketch10", midRoofSecondHoleDis);
            if (midRoofHoleNo == 1)
            {
                swComp.Suppress("LPattern1");
            }
            else
            {
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D1@LPattern1", midRoofHoleNo);
            }
            //排风口
            if (exNo == 1)
            {
                swComp.UnSuppress("EXCOONE");
                swComp.Suppress("EXCOTWO");
                swPart.ChangeDim("D4@Sketch11", exRightDis);
                swPart.ChangeDim("D2@Sketch11", exLength);
                swPart.ChangeDim("D1@Sketch11", exWidth);
            }
            else
            {
                swComp.Suppress("EXCOONE");
                swComp.UnSuppress("EXCOTWO");
                swPart.ChangeDim("D5@Sketch12", exRightDis);
                swPart.ChangeDim("D4@Sketch12", exDis);
                swPart.ChangeDim("D1@Sketch12", exLength);
                swPart.ChangeDim("D2@Sketch12", exWidth);
            }
            //进水口
            if (inlet == "LEFT")
            {
                swComp.UnSuppress("PIPEIN-LEFT");
                swComp.Suppress("PIPEIN-RIGHT");
            }
            else if (inlet == "RIGHT")
            {
                swComp.Suppress("PIPEIN-LEFT");
                swComp.UnSuppress("PIPEIN-RIGHT");
            }
            else
            {
                swComp.Suppress("PIPEIN-LEFT");
                swComp.Suppress("PIPEIN-RIGHT");
            }
            //ANSUL
            if (ANSUL == "YES")
            {
                //侧喷
                if (ANSide == "LEFT")
                {
                    swComp.UnSuppress("ANSUL-LEFT");
                    swComp.Suppress("ANSUL-RIGHT");
                }
                if (ANSide == "RIGHT")
                {
                    swComp.Suppress("ANSUL-LEFT");
                    swComp.UnSuppress("ANSUL-RIGHT");
                }
            }
            else
            {
                swComp.Suppress("ANSUL-LEFT");
                swComp.Suppress("ANSUL-RIGHT");
            }

            //MARVEL
            //swComp.UnSuppress("MA-NTC");
            //if (MARVEL == "YES")
            //{
            //
            //    if (exNo == 1) swPart.ChangeDim("D1@Sketch20",(exRightDis + exLength / 2 + 50d));
            //    else swPart.ChangeDim("D1@Sketch20",(exRightDis + exDis / 2 + exLength + 50d));
            //}
            //else

            //UVHood,UVRack-UV灯架孔和UV cable-UV灯线缆穿孔

            if (UVType == "DOUBLE")
            {
                swComp.UnSuppress("UVDOUBLE");
                swPart.ChangeDim("D7@Sketch15", exRightDis);
                swComp.Suppress("UVRACK");
            }
            else if (UVType == "LONG")
            {
                swComp.UnSuppress("UVRACK");
                swPart.ChangeDim("D10@Sketch16", exRightDis);
                swPart.ChangeDim("D8@Sketch16", 1640d);
                swPart.ChangeDim("D9@Sketch16", 1500d);
                swComp.Suppress("UVDOUBLE");
            }
            else if (UVType == "SHORT")
            {
                //SHORT
                swComp.UnSuppress("UVRACK");
                swPart.ChangeDim("D10@Sketch16", exRightDis);
                swPart.ChangeDim("D8@Sketch16", 930d);
                swPart.ChangeDim("D9@Sketch16", 790d);
                swComp.Suppress("UVDOUBLE");
            }
            else
            {
                //非UVHood
                swComp.Suppress("UVRACK");
                swComp.Suppress("UVDOUBLE");
            }

            //解压检修门，水洗烟罩，且带UV，短灯>=1600,长灯>=2400
            //if (UWHood != null && ((UVType == "SHORT" && length >= 1600d) || (UVType == "LONG" && length >= 2400d)))
            //{
            //    if (inlet == "LEFT")
            //    {
            //        swComp.UnSuppress("DOOR-R");
            //
            //        swComp.UnSuppress("DOOR-L");
            //
            //    }
            //    else if (inlet == "RIGHT")
            //    {
            //        swComp.UnSuppress("DOOR-R");
            //
            //        swComp.UnSuppress("DOOR-L");
            //
            //    }
            //    else
            //    {
            //        swComp.UnSuppress("DOOR-R");
            //
            //        swComp.UnSuppress("DOOR-L");
            //
            //    }
            //}
            //else
            //{
            //    swComp.UnSuppress("DOOR-R");
            //
            //    swComp.UnSuppress("DOOR-L");
            //
            //}
        }

        /// <summary>
        /// 华为水洗UW650排风腔前面板
        /// </summary>
        public void FNHE0180(Component2 swComp, double length, string inlet, string UVType, double exRightDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length);
            if (inlet == "LEFT")
            {
                swComp.UnSuppress("PIPEIN-LEFT");
                swComp.Suppress("PIPEIN-RIGHT");
            }
            else
            {
                swComp.Suppress("PIPEIN-LEFT");
                swComp.UnSuppress("PIPEIN-RIGHT");
            }
            //UV灯门
            if (UVType == "DOUBLE")
            {
                swComp.UnSuppress("UVDOOR-DOUBLE");
                swPart.ChangeDim("D1@Sketch6", exRightDis);
                swComp.UnSuppress("UVCABLE-DOUBLE");
                swPart.ChangeDim("D6@Sketch11", exRightDis);
                swComp.Suppress("UVDOOR-SHORT");
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVCABLE");
            }
            else if (UVType == "LONG")
            {
                swComp.UnSuppress("UVDOOR-LONG");
                swPart.ChangeDim("D1@Sketch17", exRightDis);
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D4@Sketch10", exRightDis);
                swPart.ChangeDim("D3@Sketch10", 1500d);
                swComp.Suppress("UVDOOR-SHORT");
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.Suppress("UVCABLE-DOUBLE");
            }
            else if (UVType == "SHORT")
            {
                swComp.UnSuppress("UVDOOR-SHORT");
                swPart.ChangeDim("D1@Sketch15", exRightDis);
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D4@Sketch10", exRightDis);
                swPart.ChangeDim("D3@Sketch10", 790d);

                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.Suppress("UVCABLE-DOUBLE");
            }
            else
            {
                swComp.Suppress("UVDOOR-SHORT");
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVCABLE");
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.Suppress("UVCABLE-DOUBLE");
            }
            //水洗挡板感应器穿线孔
            swComp.UnSuppress("BFCABLE");
        }
        /// <summary>
        /// 水洗挡板
        /// </summary>        
        /// <param name="exType">UW,KW</param>
        public void FNHE0150(Component2 swComp, double length, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Sketch1", length - 105d - 5d);
            if (exType=="UW") swComp.UnSuppress("UWHOOD");
            else swComp.Suppress("UWHOOD");
        }

        public void FNHE0151(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Sketch1", length - 5d);
        }

        public void FNHE0152(Component2 swComp, double length, string UVType, double exRightDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 5d);
            //UV灯门铰链孔
            if (UVType == "DOUBLE")
            {
                swComp.UnSuppress("UVDOOR-DOUBLE");
                swPart.ChangeDim("D1@Sketch10", exRightDis - 2.5d);
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVDOOR-SHORT");
            }
            else if (UVType == "LONG")
            {
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.UnSuppress("UVDOOR-LONG");
                swPart.ChangeDim("D1@Sketch12", exRightDis - 2.5d);
                swComp.Suppress("UVDOOR-SHORT");
            }
            else if (UVType == "SHORT")
            {
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.Suppress("UVDOOR-LONG");
                swComp.UnSuppress("UVDOOR-SHORT");
                swPart.ChangeDim("D1@Sketch11", exRightDis - 2.5d);
            }
            else
            {
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVDOOR-SHORT");
            }
        }

        public void FNHE0153(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Sketch1", length - 5d);
        }

        public void FNHE0154(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", length - 8d);
        }

        /// <summary>
        /// KSA侧边
        /// </summary>
        public void KSAFilter(AssemblyDoc swAssy, string suffix, double ksaSideLength, string leftPart, string rightPart, string specialPart)
        {
            Component2 swComp;
            ModelDoc2 swPart;
            if (ksaSideLength <= 3.3d)
            {
                swAssy.Suppress(suffix, leftPart);
                swAssy.Suppress(suffix, rightPart);
                swAssy.Suppress(suffix, specialPart);
            }
            else if (ksaSideLength < 12d && ksaSideLength > 3.3d)//华为板材厚，改成3.3无法折弯
            {
                swAssy.Suppress(suffix, leftPart);
                swAssy.Suppress(suffix, rightPart);
                swComp = swAssy.UnSuppress(suffix, specialPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D1@草图1", ksaSideLength * 2);
            }
            else if (ksaSideLength < 25d  && ksaSideLength >= 12d)
            {
                swComp = swAssy.UnSuppress(suffix, leftPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@草图1", ksaSideLength * 2);
                swAssy.Suppress(suffix, rightPart);
                swAssy.Suppress(suffix, specialPart);
            }
            else
            {
                swComp = swAssy.UnSuppress(suffix, leftPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@草图1", ksaSideLength);
                swComp = swAssy.UnSuppress(suffix, rightPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@草图1", ksaSideLength);
                swAssy.Suppress(suffix, specialPart);
            }
        }

        /// <summary>
        /// MESH油网侧板,水洗
        /// </summary>
        public void UwMeshFilter(AssemblyDoc swAssy, string suffix, double length, string inlet, string ansul, string anSide, string leftPart, string rightPart)
        {
            //MESH侧板长度(除去排风三角板3dm计算)，肖启才说，手划伤了，华为不锈钢mesh用498.5去计算
            double meshSideLength = Convert.ToDouble((length - 3d -(int)((length-2d-35d) / 498.5d) * 498.5d) / 2d);

            Component2 swComp;
            ModelDoc2 swPart;
            if ((inlet == "LEFT" && anSide == "RIGHT") || (anSide == "LEFT" && inlet == "RIGHT"))//不同一侧
            {
                if ((meshSideLength - 20d) < 55d) meshSideLength += 249d;
                swComp = swAssy.UnSuppress(suffix, leftPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                if (inlet == "LEFT")
                {
                    swComp.UnSuppress("KW");
                    swPart.ChangeDim("D3@Sketch25", 100d);
                    swPart.ChangeDim("D3@Sketch26", 120d);
                }
                else
                {
                    swComp.Suppress("KW");
                    swPart.ChangeDim("D3@Sketch25", 30d);
                    swPart.ChangeDim("D3@Sketch26", 190d);
                }
                if (ansul == "YES" && anSide == "LEFT")
                    swComp.UnSuppress("ANSUL");
                else swComp.Suppress("ANSUL");

                swComp = swAssy.UnSuppress(suffix, rightPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                if (inlet == "RIGHT")
                {
                    swComp.UnSuppress("KW");
                    swPart.ChangeDim("D1@Sketch25", 100d);
                    swPart.ChangeDim("D3@Sketch26", 120d);
                }
                else
                {
                    swComp.Suppress("KW");
                    swPart.ChangeDim("D1@Sketch25", 30d);
                    swPart.ChangeDim("D3@Sketch26", 190d);
                }
                if (ansul == "YES" && anSide == "RIGHT")
                    swComp.UnSuppress("ANSUL");
                else swComp.Suppress("ANSUL");
            }
            else
            {
                if (meshSideLength * 2 < 55d) meshSideLength += 249d;
                if ((meshSideLength - 20d) > 55d)
                {
                    if (inlet == "LEFT")
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        swComp.UnSuppress("KW");
                        swPart.ChangeDim("D3@Sketch25", 100d);
                        swPart.ChangeDim("D3@Sketch26", 120d);
                        if (ansul == "YES" && anSide == "LEFT")
                            swComp.UnSuppress("ANSUL");
                        else swComp.Suppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        swComp.Suppress("KW");
                        swPart.ChangeDim("D1@Sketch25", 30d);
                        swPart.ChangeDim("D3@Sketch26", 190d);
                        swComp.Suppress("ANSUL");
                    }
                    else
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        swComp.Suppress("KW");
                        swPart.ChangeDim("D3@Sketch25", 30d);
                        swPart.ChangeDim("D3@Sketch26", 190d);
                        swComp.Suppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        swComp.UnSuppress("KW");
                        swPart.ChangeDim("D1@Sketch25", 100d);
                        swPart.ChangeDim("D3@Sketch26", 120d);
                        if (ansul == "YES" && anSide == "RIGHT")
                            swComp.UnSuppress("ANSUL");
                        else swComp.Suppress("ANSUL");
                    }
                }
                else
                {
                    if (inlet == "LEFT")
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", 2d * meshSideLength);
                        swComp.UnSuppress("KW");
                        swPart.ChangeDim("D3@Sketch25", 100d);
                        swPart.ChangeDim("D3@Sketch26", 120d);
                        if (ansul == "YES" && anSide == "LEFT")
                            swComp.UnSuppress("ANSUL");
                        else swComp.Suppress("ANSUL");

                        swAssy.Suppress(suffix, rightPart);
                    }
                    else
                    {
                        swAssy.Suppress(suffix, leftPart);

                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", 2d * meshSideLength);
                        swComp.UnSuppress("KW");
                        swPart.ChangeDim("D1@Sketch25", 100d);
                        swPart.ChangeDim("D3@Sketch26", 120d);
                        if (ansul == "YES" && anSide == "RIGHT")
                            swComp.UnSuppress("ANSUL");
                        else swComp.Suppress("ANSUL");
                    }
                }
            }
        }

        /// <summary>
        /// 排风滑门/导轨
        /// </summary>
        public void ExaustRail(AssemblyDoc swAssy, string suffix, double exLength, double exWidth,
            int exNo, string doorPart, string railPart)
        {
            Component2 swComp = swAssy.UnSuppress(suffix, doorPart);
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", exLength / 2d + 10d);
            swPart.ChangeDim("D2@Sketch1", exWidth + 20d);

            swComp = swAssy.UnSuppress(suffix, railPart);
            swPart = swComp.GetModelDoc2();
            if (exNo == 1) swPart.ChangeDim("D2@Base-Flange1", exLength * 2d + 100d);
            else swPart.ChangeDim("D2@Base-Flange1", exLength * 2d + 20d);
        }

        /// <summary>
        /// 排风脖颈
        /// </summary>
        public void ExaustSpigot(AssemblyDoc swAssy, string suffix, string ansul, double exLength,
            double exWidth, double exHeight, string frontPart, string backPart, string leftPart, string rightPart)
        {
            Component2 swComp = swAssy.UnSuppress(suffix, frontPart);
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", exLength + 50d);
            swPart.ChangeDim("D2@Sketch1", exHeight);
            if (ansul == "YES") swComp.UnSuppress("ANSUL");
            else swComp.Suppress("ANSUL");

            swComp = swAssy.UnSuppress(suffix, backPart);
            swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", exLength + 50d);
            swPart.ChangeDim("D2@Sketch1", exHeight);
            swComp = swAssy.UnSuppress(suffix, leftPart);
            swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1", exWidth);
            swPart.ChangeDim("D3@草图1", exHeight);

            swComp = swAssy.UnSuppress(suffix, rightPart);
            swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1", exWidth);
            swPart.ChangeDim("D3@草图1", exHeight);
        }

        //----------UV灯，UV灯门----------
        public void UVLightDoor(AssemblyDoc swAssy, string suffix, string UVType, string[] partList)
        {
            foreach (string part in partList)
            {
                if (UVType == "DOUBLE") swAssy.UnSuppress(suffix, part);
                else swAssy.Suppress(suffix, part);
            }
        }
        #endregion 华为水洗UW650排风腔零件


        #region 华为水洗UW555排风腔零件
        /// <summary>
        /// 华为水洗UW555排风腔背板
        /// </summary>
        public void FNHE0147(Component2 swComp, double length, string waterCollection, string sidePanel, string outlet, string backToBack)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();//打开零件3
            swPart.ChangeDim("D1@草图1", length);
            #region 集水翻边
            if (waterCollection == "YES")
            {
                if (sidePanel == "RIGHT")
                {
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                    swComp.Suppress("DRAINCHANNEL-LEFT");
                }
                else if (sidePanel == "LEFT")
                {
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                    swComp.Suppress("DRAINCHANNEL-RIGHT");
                }
                else if (sidePanel == "BOTH")
                {
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                }
            }
            else
            {
                swComp.Suppress("DRAINCHANNEL-RIGHT");
                swComp.Suppress("DRAINCHANNEL-LEFT");
            }

            #endregion 集水翻边

            #region 下水口

            if (outlet == "LEFT")
            {
                swComp.UnSuppress("DRAINPIPE-LEFT");
                swComp.Suppress("DRAINPIPE-RIGHT");
            }
            else if (outlet == "RIGHT")
            {
                swComp.Suppress("DRAINPIPE-LEFT");
                swComp.UnSuppress("DRAINPIPE-RIGHT");
            }
            else
            {
                swComp.Suppress("DRAINPIPE-LEFT");
                swComp.Suppress("DRAINPIPE-RIGHT");
            }

            #endregion 下水口

            #region 背靠背

            if (backToBack == "YES")
            {
                swComp.UnSuppress("BACKTOBACK");
            }
            else
            {
                swComp.Suppress("BACKTOBACK");
            }

            #endregion 背靠背            
        }
        /// <summary>
        /// 华为水洗UW555排风腔前面板
        /// </summary>
        public void FNHE0149(Component2 swComp, double length, string inlet, string UVType, double exRightDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length);
            if (inlet == "LEFT")
            {
                swComp.UnSuppress("PIPEIN-LEFT");
                swComp.Suppress("PIPEIN-RIGHT");
            }
            else
            {
                swComp.Suppress("PIPEIN-LEFT");
                swComp.UnSuppress("PIPEIN-RIGHT");
            }
            //UV灯门
            if (UVType == "DOUBLE")
            {
                swComp.UnSuppress("UVDOOR-DOUBLE");
                swPart.ChangeDim("D1@Sketch6", exRightDis);
                swComp.UnSuppress("UVCABLE-DOUBLE");
                swPart.ChangeDim("D6@Sketch11", exRightDis);
                swComp.Suppress("UVDOOR-SHORT");
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVCABLE");
            }
            else if (UVType == "LONG")
            {
                swComp.UnSuppress("UVDOOR-LONG");
                swPart.ChangeDim("D1@Sketch17", exRightDis);
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D4@Sketch10", exRightDis);
                swPart.ChangeDim("D3@Sketch10", 1500d);
                swComp.Suppress("UVDOOR-SHORT");
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.Suppress("UVCABLE-DOUBLE");
            }
            else if (UVType == "SHORT")
            {
                swComp.UnSuppress("UVDOOR-SHORT");
                swPart.ChangeDim("D1@Sketch15", exRightDis);
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D4@Sketch10", exRightDis);
                swPart.ChangeDim("D3@Sketch10", 790d);

                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.Suppress("UVCABLE-DOUBLE");
            }
            else
            {
                swComp.Suppress("UVDOOR-SHORT");
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVCABLE");
                swComp.Suppress("UVDOOR-DOUBLE");
                swComp.Suppress("UVCABLE-DOUBLE");
            }
            //水洗挡板感应器穿线孔
            swComp.UnSuppress("BFCABLE");
        }


        #endregion

        #region 华为烟罩MiddleRoof

        /// <summary>
        /// 华为MiddleRoof灯板
        /// </summary>
        public void FNHM0031(Component2 swComp, string exType, double length, double deepth, double exHeight, double suHeight, double exRightDis, double midRoofTopHoleDis, double midRoofSecondHoleDis, int midRoofHoleNo, string lightType, double lightYDis, int ledSpotNo, double ledSpotDis, string ansul, int anDropNo, double anYDis, double anDropDis1, double anDropDis2, double anDropDis3, double anDropDis4, double anDropDis5, string anDetectorEnd, int anDetectorNo, double anDetectorDis1, double anDetectorDis2, double anDetectorDis3, double anDetectorDis4, double anDetectorDis5, string bluetooth, string uvType, string marvel, int irNo, double irDis1, double irDis2, double irDis3)
        {
            #region 基本尺寸

            ModelDoc2 swPart = swComp.GetModelDoc2();
            if (suHeight == 650d)
            {
                swPart.ChangeDim("D2@草图1", deepth-535d-360d-3d+224d+95d+95d);
                swPart.ChangeDim("D1@Sketch5", 85d + 95d);
                swPart.ChangeDim("D1@Sketch6", 85d + 95d);
            }
            else
            {
                swPart.ChangeDim("D2@草图1", deepth-535d-360d-3d+224d);
                swPart.ChangeDim("D1@Sketch5", 85d);
                swPart.ChangeDim("D1@Sketch6", 85d);
            }

            swPart.ChangeDim("D1@草图6", deepth-535d-360d-3d);
            swPart.ChangeDim("D1@草图1", length);

            //if (exHeight == 650d || (exHeight != 650d && length >= 2100d && length <= 2400d))
            //{
            swComp.Suppress("Edge-Flange1");
            swComp.Suppress("Edge-Flange2");
            swComp.Suppress("Break-Corner1");
            swComp.Suppress("Cut-Extrude4");
            swComp.Suppress("Cut-Extrude5");
            swComp.Suppress("ANDTECACROSS");
            swComp.UnSuppress("Edge-Flange3");
            swComp.UnSuppress("Cut-Extrude6");
            swComp.UnSuppress("Closed Corner8");
            swPart.ChangeDim("D2@Sketch52", (deepth - 840d)/3d+100d);
            //}
            //else
            //{
            //swComp.Suppress("Edge-Flange3");
            //swComp.Suppress("Cut-Extrude6");
            //swComp.Suppress("Closed Corner8");
            //swComp.UnSuppress("Edge-Flange1");
            //swComp.UnSuppress("Edge-Flange2");
            //swComp.UnSuppress("Break-Corner1");
            //swComp.UnSuppress("Cut-Extrude4");
            //swComp.UnSuppress("Cut-Extrude5");
            //swComp.UnSuppress("ANDTECACROSS");
            //swPart.ChangeDim("D1@Sketch23", 84.5d);
            //swPart.ChangeDim("D3@草图25", midRoofTopHoleDis);
            //swPart.ChangeDim("D2@草图26", (deepth - 840d) / 6d);
            //}
            swPart.ChangeDim("D1@Sketch3", midRoofSecondHoleDis);

            if (midRoofHoleNo == 1) swComp.Suppress("LPattern1");
            else
            {
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D1@LPattern1", midRoofHoleNo);
            }

            #endregion 基本尺寸

            #region 灯具

            if (lightType == "LED60")
            {
                swComp.Suppress("LED140");
                swComp.Suppress("LPattern3");
                swComp.Suppress("FSLONG");
                swComp.Suppress("FSSHORT");
                swComp.UnSuppress("LED60");
                swPart.ChangeDim("D1@Sketch1", lightYDis - 360d);
                if (ledSpotNo == 1) swPart.ChangeDim("D2@Sketch1", 0);
                else
                {
                    swPart.ChangeDim("D2@Sketch1", ledSpotDis * (ledSpotNo / 2d - 1) + ledSpotDis / 2d);
                    swComp.UnSuppress("LPattern2");
                    swPart.ChangeDim("D1@LPattern2", ledSpotNo);
                    swPart.ChangeDim("D3@LPattern2", ledSpotDis);
                }
            }
            else if (lightType == "LED140")
            {
                swComp.Suppress("LED60");
                swComp.Suppress("LPattern2");
                swComp.Suppress("FSLONG");
                swComp.Suppress("FSSHORT");
                swComp.UnSuppress("LED140");
                swPart.ChangeDim("D1@Sketch7", lightYDis - 360d);
                if (ledSpotNo == 1) swPart.ChangeDim("D5@Sketch7", 0);
                else
                {
                    swPart.ChangeDim("D5@Sketch7", ledSpotDis * (ledSpotNo / 2d - 1) + ledSpotDis / 2d);
                    swComp.UnSuppress("LPattern3");
                    swPart.ChangeDim("D1@LPattern3", ledSpotNo);
                    swPart.ChangeDim("D3@LPattern3", ledSpotDis);
                }
            }
            else if (lightType == "FSLONG")
            {
                swComp.Suppress("LED60");
                swComp.Suppress("LPattern2");
                swComp.Suppress("LED140");
                swComp.Suppress("LPattern3");
                swComp.UnSuppress("FSLONG");
                swPart.ChangeDim("D1@Sketch20", lightYDis - 360d);
                swComp.Suppress("FSSHORT");
            }
            else if (lightType == "FSSHORT")
            {
                swComp.Suppress("LED60");
                swComp.Suppress("LPattern2");
                swComp.Suppress("LED140");
                swComp.Suppress("LPattern3");
                swComp.Suppress("FSLONG");
                swComp.UnSuppress("FSSHORT");
                swPart.ChangeDim("D1@Sketch21", lightYDis - 360d);
            }
            else
            {
                swComp.Suppress("LED60");
                swComp.Suppress("LPattern2");
                swComp.Suppress("LED140");
                swComp.Suppress("LPattern3");
                swComp.Suppress("FSLONG");
                swComp.Suppress("FSSHORT");
            }

            #endregion 灯具

            #region ANSUL

            if (ansul == "YES")
            {
                swComp.Suppress("AN1");
                swComp.Suppress("AN2");
                swComp.Suppress("AN3");
                swComp.Suppress("AN4");
                swComp.Suppress("AN5");
                swComp.Suppress("ANDTEC1");
                swComp.Suppress("ANDTEC2");
                swComp.Suppress("ANDTEC3");
                swComp.Suppress("ANDTEC4");
                swComp.Suppress("ANDTEC5");
                //下喷
                if (anDropNo > 0)
                {
                    swComp.UnSuppress("AN1");
                    swPart.ChangeDim("D1@Sketch11", anDropDis1);
                    swPart.ChangeDim("D3@Sketch11", anYDis - 360d);
                }
                if (anDropNo > 1)
                {
                    swComp.UnSuppress("AN2");
                    swPart.ChangeDim("D1@Sketch12", anDropDis2);
                }
                if (anDropNo > 2)
                {
                    swComp.UnSuppress("AN3");
                    swPart.ChangeDim("D1@Sketch13", anDropDis3);
                }
                if (anDropNo > 3)
                {
                    swComp.UnSuppress("AN4");
                    swPart.ChangeDim("D1@Sketch14", anDropDis4);
                }
                if (anDropNo > 4)
                {
                    swComp.UnSuppress("AN5");
                    swPart.ChangeDim("D1@Sketch15", anDropDis5);
                }
                //探测器
                if (exType == "UW" || exType == "KW")
                {
                    if (anDetectorNo > 0)
                    {
                        swComp.UnSuppress("ANDTEC1");
                        swPart.ChangeDim("D1@Sketch31", anDetectorDis1);
                        if (anDetectorEnd == "LEFT" || (anDetectorEnd == "RIGHT" && anDetectorNo == 1)) swPart.ChangeDim("D2@Sketch31", 195d);
                        else swPart.ChangeDim("D2@Sketch31", 175d);
                    }
                    if (anDetectorNo > 1)
                    {
                        swComp.UnSuppress("ANDTEC2");
                        swPart.ChangeDim("D1@Sketch32", anDetectorDis2);
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 2) swPart.ChangeDim("D2@Sketch32", 195d);
                        else swPart.ChangeDim("D2@Sketch32", 175d);
                    }
                    if (anDetectorNo > 2)
                    {
                        swComp.UnSuppress("ANDTEC3");
                        swPart.ChangeDim("D1@Sketch33", anDetectorDis3);
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 3) swPart.ChangeDim("D2@Sketch33", 195d);
                        else swPart.ChangeDim("D2@Sketch33", 175d);
                    }
                    if (anDetectorNo > 3)
                    {
                        swComp.UnSuppress("ANDTEC4");
                        swPart.ChangeDim("D1@Sketch34", anDetectorDis4);
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 4) swPart.ChangeDim("D2@Sketch34", 195d);
                        else swPart.ChangeDim("D2@Sketch34", 175d);
                    }
                    if (anDetectorNo > 4)
                    {
                        swComp.UnSuppress("ANDTEC5");
                        swPart.ChangeDim("D1@Sketch35", anDetectorDis5);
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 5) swPart.ChangeDim("D2@Sketch35", 195d);
                        else swPart.ChangeDim("D2@Sketch35", 175d);
                    }
                }
                else
                {
                    //UW/KW HOOD 水洗烟罩探测器在midRoof上，非水洗烟罩压缩
                    swComp.Suppress("ANDTEC1");
                    swComp.Suppress("ANDTEC2");
                    swComp.Suppress("ANDTEC3");
                    swComp.Suppress("ANDTEC4");
                    swComp.Suppress("ANDTEC5");
                }
            }
            else
            {
                swComp.Suppress("AN1");
                swComp.Suppress("AN2");
                swComp.Suppress("AN3");
                swComp.Suppress("AN4");
                swComp.Suppress("AN5");

                //UW/KW HOOD 水洗烟罩探测器在midRoof上，非水洗烟罩压缩
                swComp.Suppress("ANDTEC1");
                swComp.Suppress("ANDTEC2");
                swComp.Suppress("ANDTEC3");
                swComp.Suppress("ANDTEC4");
                swComp.Suppress("ANDTEC5");
            }

            #endregion ANSUL

            #region 开方孔，UV或待MARVEL时解压

            swComp.UnSuppress("CUT-BACK-LEFT");
            swComp.UnSuppress("CUT-BACK-RIGHT");
            swComp.UnSuppress("CUT-FRONT-RIGHT");

            if (bluetooth == "YES") swComp.UnSuppress("CUT-FRONT-LEFT");
            else swComp.Suppress("CUT-FRONT-LEFT");

            #endregion 开方孔，UV或待MARVEL时解压

            #region UV灯线缆穿孔

            if (uvType == "DOUBLE")
            {
                swComp.UnSuppress("UVCABLE-DOUBLE");
                swPart.ChangeDim("D1@Sketch41", exRightDis);
                swComp.Suppress("UVCABLE");
            }
            else if (uvType == "LONG")
            {
                swComp.Suppress("UVCABLE-DOUBLE");
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D4@草图28", exRightDis);
                swPart.ChangeDim("D3@草图28", 1500d);
            }
            else if (uvType == "SHORT")
            {
                swComp.Suppress("UVCABLE-DOUBLE");
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D4@草图28", exRightDis);
                swPart.ChangeDim("D3@草图28", 790d);
            }
            else
            {
                swComp.Suppress("UVCABLE-DOUBLE");
                swComp.Suppress("UVCABLE");
            }

            #endregion UV灯线缆穿孔

            
        }

        public void FNHM0032(Component2 swComp, string exType, double deepth, double height, double midRoofTopHoleDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Sketch25", deepth - 535d - 360d - 3.5d);
            swPart.ChangeDim("D1@Sketch32", midRoofTopHoleDis - 3d);
            swPart.ChangeDim("D1@Sketch31", (deepth - 840d)/6d);
            swPart.ChangeDim("D3@Sketch38", (deepth - 840d)/3d+100d);
            swComp.UnSuppress("Cut-Extrude4");
            if (exType == "UW" || exType == "KW") swComp.UnSuppress("Cut-Extrude4");
            else swComp.Suppress("Cut-Extrude4");
            if (height == 650d) swPart.ChangeDim("D1@Sketch25", 77d + 95d);
            else swPart.ChangeDim("D1@Sketch25", 77d);
        }

        public void Std2900100001(Component2 swComp, double deepth, string ansul)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            if (ansul == "YES") swPart.ChangeDim("D2@基体-法兰1", deepth - 250d);
            else swPart.ChangeDim("D2@基体-法兰1", deepth - 100d);
        }

        #endregion 华为烟罩MiddleRoof

        #region Hood SidePanel 华为650，555烟罩大侧板

        /// <summary>
        /// 左边大侧板外板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        /// <param name="sidePanelSideCjNo">侧板侧向CJ孔数量</param>
        /// <param name="sidePanelDownCjNo">侧板垂直CJ孔数量</param>
        public void FNHS0067(Component2 swComp, double deepth, double height, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", deepth);
            swPart.ChangeDim("D2@草图1", height);
            swPart.ChangeDim("D1@阵列(线性)1", sidePanelSideCjNo);
            swPart.ChangeDim("D1@阵列(线性)2", sidePanelDownCjNo);
        }

        /// <summary>
        /// 左边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0068(Component2 swComp, double deepth, double height)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", deepth - 79d - 3.4d);//华为板厚1.2,内板过大-3.4
            swPart.ChangeDim("D2@草图1", height - 39d - 1d);
            if (height == 700d)
            {
                swComp.UnSuppress("F700");
                swComp.Suppress("F555");
                swComp.Suppress("F450");
                swComp.Suppress("F400");
            }
            else if (height == 555d || height == 650d)
            {
                swComp.Suppress("F700");
                swComp.UnSuppress("F555");
                swComp.Suppress("F450");
                swComp.Suppress("F400");
            }
            else if (height == 450d)
            {
                swComp.Suppress("F700");
                swComp.Suppress("F555");
                swComp.UnSuppress("F450");
                swComp.Suppress("F400");
            }
            else if (height == 400d)
            {
                swComp.Suppress("F700");
                swComp.Suppress("F555");
                swComp.Suppress("F450");
                swComp.UnSuppress("F400");
            }
            else
            {
                swComp.Suppress("F700");
                swComp.Suppress("F555");
                swComp.Suppress("F450");
                swComp.Suppress("F400");
            }
        }

        /// <summary>
        /// 右边大侧板外板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        /// <param name="sidePanelSideCjNo">侧板侧向CJ孔数量</param>
        /// <param name="sidePanelDownCjNo">侧板垂直CJ孔数量</param>
        public void FNHS0069(Component2 swComp, double deepth, double height, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", deepth);
            swPart.ChangeDim("D2@草图1", height);
            swPart.ChangeDim("D1@阵列(线性)1", sidePanelSideCjNo);
            swPart.ChangeDim("D1@阵列(线性)2", sidePanelDownCjNo);
        }

        /// <summary>
        /// 右边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0070(Component2 swComp, double deepth, double height)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", deepth - 79d - 3.4d);//华为板厚1.2,内板过大-3.4
            swPart.ChangeDim("D2@草图1", height - 39d - 1d);
            if (height == 700d)
            {
                swComp.UnSuppress("F700");
                swComp.Suppress("F555");
                swComp.Suppress("F450");
                swComp.Suppress("F400");
            }
            else if (height == 555d || height == 650d)
            {
                swComp.Suppress("F700");
                swComp.UnSuppress("F555");
                swComp.Suppress("F450");
                swComp.Suppress("F400");
            }
            else if (height == 450d)
            {
                swComp.Suppress("F700");
                swComp.Suppress("F555");
                swComp.UnSuppress("F450");
                swComp.Suppress("F400");
            }
            else if (height == 400d)
            {
                swComp.Suppress("F700");
                swComp.Suppress("F555");
                swComp.Suppress("F450");
                swComp.UnSuppress("F400");
            }
            else
            {
                swComp.Suppress("F700");
                swComp.Suppress("F555");
                swComp.Suppress("F450");
                swComp.Suppress("F400");
            }
        }

        /// <summary>
        /// 左集水翻边
        /// </summary>
        public void FNHS0071(Component2 swComp, double deepth, double exHeight, double suHeight, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            if (exType == "W")
            {
                swPart.ChangeDim("D2@Base-Flange1", deepth - 368+5d);
                swPart.ChangeDim("D1@Sketch7", 22d);
            }
            else
            {
                swPart.ChangeDim("D2@Base-Flange1", deepth - 368d + 88d + 5d);
                swPart.ChangeDim("D1@Sketch7", 14.5d);
            }
        }

        /// <summary>
        /// 右集水翻边
        /// </summary>
        public void FNHS0072(Component2 swComp, double deepth, double exHeight, double suHeight, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            if (exType == "W")
            {
                swPart.ChangeDim("D2@Base-Flange1", deepth - 368+5d);
                swPart.ChangeDim("D1@Sketch7", 22d);
            }
            else
            {
                swPart.ChangeDim("D2@Base-Flange1", deepth - 368d + 88d + 5d);
                swPart.ChangeDim("D1@Sketch7", 14.5d);
            }
        }

        public void SidePanel(AssemblyDoc swAssy, string suffix, string sidePanel, double deepth, double exHeight, string waterCollection, string exType, string leftOuter, string leftInner, string rightOuter, string rightInner, string leftCollection, string rightCollection)
        {
            //侧板CJ孔整列到烟罩底部
            int sidePanelDownCjNo = (int)((deepth - 95d) / 32d);
            //非水洗烟罩KV/UV
            int sidePanelSideCjNo = (int)((deepth - 305d) / 32d);
            //水洗烟罩KW/UW
            if (exType == "W") sidePanelSideCjNo = (int)((deepth - 380) / 32);

            Component2 swComp;

            #region 大侧板

            if (sidePanel == "BOTH")
            {
                //LEFT
                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FNHS0067(swComp, deepth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0068(swComp, deepth, exHeight);
                //RIGHT
                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0069(swComp, deepth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0070(swComp, deepth, exHeight);
            }
            else if (sidePanel == "LEFT")
            {
                swAssy.Suppress(suffix, rightOuter);
                swAssy.Suppress(suffix, rightInner);

                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FNHS0067(swComp, deepth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0068(swComp, deepth, exHeight);
            }
            else if (sidePanel == "RIGHT")
            {
                swAssy.Suppress(suffix, leftOuter);
                swAssy.Suppress(suffix, leftInner);

                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0069(swComp, deepth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0070(swComp, deepth, exHeight);
            }
            else
            {
                swAssy.Suppress(suffix, leftOuter);
                swAssy.Suppress(suffix, leftInner);
                swAssy.Suppress(suffix, rightOuter);
                swAssy.Suppress(suffix, rightInner);
            }

            #endregion 大侧板

            #region 集水翻边

            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "LEFT"))
            {
                swComp = swAssy.UnSuppress(suffix, leftCollection);
                FNHS0071(swComp, deepth, exHeight, exHeight, exType);//普通烟罩"V"，水洗"W"
            }
            else
            {
                swAssy.Suppress(suffix, leftCollection);
            }
            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "RIGHT"))
            {
                swComp = swAssy.UnSuppress(suffix, rightCollection);
                FNHS0072(swComp, deepth, exHeight, exHeight, exType);//普通烟罩"V"，水洗"W"
            }
            else
            {
                swAssy.Suppress(suffix, rightCollection);
            }
            #endregion 集水翻边
        }
        #endregion Hood SidePanel 华为650，555烟罩大侧板

        #region 华为斜烟罩大侧板

        /// <summary>
        /// 左边大侧板外板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        /// <param name="sidePanelSideCjNo">侧板侧向CJ孔数量</param>
        /// <param name="sidePanelDownCjNo">侧板垂直CJ孔数量</param>
        public void FNHS0059(Component2 swComp, double deepth, double exHeight, double suHeight, string exType, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", deepth);
            swPart.ChangeDim("D2@草图1", exHeight);
            swPart.ChangeDim("D3@草图1", suHeight);
            if (exType == "W") swPart.ChangeDim("D4@草图1", 150d);
            else swPart.ChangeDim("D4@草图1", 76d);

            swPart.ChangeDim("D1@阵列(线性)1", sidePanelDownCjNo);
            swPart.ChangeDim("D1@阵列(线性)2", sidePanelSideCjNo - 1);
        }

        /// <summary>
        /// 左边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0061(Component2 swComp, double deepth, double exHeight, double suHeight, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D8@草图1", deepth);//D3@Sketch1
            swPart.ChangeDim("D6@草图1", exHeight);
            swPart.ChangeDim("D7@草图1", suHeight);
            if (exType == "W") swPart.ChangeDim("D9@草图1", 150d);
            else swPart.ChangeDim("D9@草图1", 76d);

            if (suHeight == 400d)
            {
                swComp.UnSuppress("I400");
            }
            else
            {
                swComp.Suppress("I400");
            }
        }

        /// <summary>
        /// 左集水翻边
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="suHeight">新风高度</param>
        /// <param name="exHeight">排风高度</param>
        /// <param name="exType">排风腔类型，"V"还是"W"</param>
        public void FNHS0063(Component2 swComp, double deepth, double exHeight, double suHeight, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            if (exType == "W")
            {
                swPart.ChangeDim("D2@Base-Flange1", Math.Sqrt(Math.Pow((double)deepth - 365d, 2) + Math.Pow((double)exHeight - (double)suHeight, 2))+8d);
                swPart.ChangeDim("D2@Sketch8", 35d);
            }
            else
            {
                swPart.ChangeDim("D2@Base-Flange1", (double)(Math.Sqrt(Math.Pow((double)deepth - 291d, 2) + Math.Pow((double)exHeight - (double)suHeight, 2)) + 20d));
                swPart.ChangeDim("D2@Sketch8", 21d);
            }
        }

        public void SidePanelSpecial(AssemblyDoc swAssy, string suffix, string sidePanel, double deepth, double exHeight, double suHeight, string waterCollection, string exType, string leftOuter, string leftInner, string rightOuter, string rightInner, string leftCollection, string rightCollection)
        {
            //HWUWF650斜侧板CJ孔计算,150为排风底部长度，555-400为高度差
            int sidePanelDownCjNo = (int)(((double)(Math.Sqrt(Math.Pow((double)deepth - 150d, 2) + Math.Pow(555d - 400d, 2))) - 95d) / 32d);
            int sidePanelSideCjNo = sidePanelDownCjNo - 3;

            Component2 swComp;

            #region 大侧板

            if (sidePanel == "BOTH")
            {
                //LEFT
                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FNHS0059(swComp, deepth, exHeight, suHeight, exType, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0061(swComp, deepth, exHeight, suHeight, exType);
                //RIGHT
                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0059(swComp, deepth, exHeight, suHeight, exType, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0061(swComp, deepth, exHeight, suHeight, exType);
            }
            else if (sidePanel == "LEFT")
            {
                swAssy.Suppress(suffix, rightOuter);
                swAssy.Suppress(suffix, rightInner);

                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FNHS0059(swComp, deepth, exHeight, suHeight, exType, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0061(swComp, deepth, exHeight, suHeight, exType);
            }
            else if (sidePanel == "RIGHT")
            {
                swAssy.Suppress(suffix, leftOuter);
                swAssy.Suppress(suffix, leftInner);

                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0059(swComp, deepth, exHeight, suHeight, exType, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0061(swComp, deepth, exHeight, suHeight, exType);
            }
            else
            {
                swAssy.Suppress(suffix, leftOuter);
                swAssy.Suppress(suffix, leftInner);
                swAssy.Suppress(suffix, rightOuter);
                swAssy.Suppress(suffix, rightInner);
            }

            #endregion 大侧板

            #region 集水翻边

            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "LEFT"))
            {
                swComp = swAssy.UnSuppress(suffix, leftCollection);
                FNHS0063(swComp, deepth, exHeight, suHeight, exType);//普通烟罩"V"，水洗"W"
            }
            else
            {
                swAssy.Suppress(suffix, leftCollection);
            }
            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "RIGHT"))
            {
                swComp = swAssy.UnSuppress(suffix, rightCollection);
                FNHS0063(swComp, deepth, exHeight, suHeight, exType); ;//普通烟罩"V"，水洗"W"
            }
            else
            {
                swAssy.Suppress(suffix, rightCollection);
            }
            #endregion 集水翻边
        }
        #endregion 华为斜烟罩大侧板

        #region 华为新风F650

        /// <summary>
        /// F新风底部CJ孔板
        /// </summary>
        public void FNHA0093(Component2 swComp, double length, int frontCjNo, double frontCjFirstDis, int frontPanelHoleNo, double frontPanelHoleDis, string bluetooth, string ledLogo, string waterCollection, string sidePanel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D1@CJHOLES", frontCjNo);
            swPart.ChangeDim("D10@草图8", frontCjFirstDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
            if (bluetooth == "YES") swComp.UnSuppress("BLUETOOTH");
            else swComp.Suppress("BLUETOOTH");
            if (ledLogo == "YES") swComp.UnSuppress("LOGO");
            else swComp.Suppress("LOGO");

            #region 集水翻边

            if (waterCollection == "YES")
            {
                if (sidePanel == "RIGHT")
                {
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                    swComp.Suppress("DRAINCHANNEL-LEFT");
                }
                else if (sidePanel == "LEFT")
                {
                    swComp.Suppress("DRAINCHANNEL-RIGHT");
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                }
                else if (sidePanel == "BOTH")
                {
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                }
            }
            else
            {
                swComp.Suppress("DRAINCHANNEL-RIGHT");
                swComp.Suppress("DRAINCHANNEL-LEFT");
            }

            #endregion 集水翻边
        }

        /// <summary>
        /// 新风滑门导轨
        /// </summary>
        public void FNHA0097(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", length - 200d);
        }

        /// <summary>
        /// F650新风前面板
        /// </summary>
        public void FNHA0107(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 1d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }

        /// <summary>
        /// F650型新风腔主体
        /// </summary>
        public void FNHA0108(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo, int suNo, double suDis, string marvel, int irNo, double irDis1, double irDis2, double irDis3, string bluetooth, string sidePanel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", length);
            swPart.ChangeDim("D1@LPattern1", frontPanelKaKouNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelKaKouDis);
            swPart.ChangeDim("D3@Sketch14", midRoofSecondHoleDis);
            swPart.ChangeDim("D4@Sketch12", 200d - midRoofTopHoleDis);
            if (midRoofHoleNo == 1) swComp.Suppress("LPattern2");
            else
            {
                swComp.UnSuppress("LPattern2");
                swPart.ChangeDim("D1@LPattern2", midRoofHoleNo);
            }
            //新风脖颈
            swPart.ChangeDim("D2@Sketch18", suDis * (suNo / 2d - 1) + suDis / 2d);
            if (suNo == 1) swComp.Suppress("LPattern3");
            else
            {
                swComp.UnSuppress("LPattern3");
                swPart.ChangeDim("D1@LPattern3", suNo);
                swPart.ChangeDim("D3@LPattern3", suDis);
            }
            //MARVEL
            
            //UV HOOD
            if (bluetooth == "YES") swComp.UnSuppress("SUCABLE-LEFT");
            else swComp.Suppress("SUCABLE-LEFT");
            if (sidePanel == "LEFT" || sidePanel == "BOTH") swComp.UnSuppress("JUNCTION BOX-LEFT");
            else swComp.Suppress("JUNCTION BOX-LEFT");
        }
        /// <summary>
        /// F650镀锌隔板
        /// </summary>
        public void FNHA0006(Component2 swComp, double length, string marvel, int irNo, double irDis1, double irDis2, double irDis3)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 8d);
           
        }
        #endregion 华为新风F650

        #region 华为新风I650
        /// <summary>
        /// I型新风腔主体
        /// </summary>
        public void FNHA0113(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int midRoofHoleNo, double midRoofSecondHoleDis, double midRoofTopHoleDis, string marvel, int irNo, double irDis1, double irDis2, double irDis3, string bluetooth, string sidePanel)
        {
            #region 基本尺寸
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D1@阵列(线性)1", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)1", frontPanelKaKouDis);
            swPart.ChangeDim("D3@Sketch3", midRoofSecondHoleDis);
            swPart.ChangeDim("D5@草图7", 200d  - midRoofTopHoleDis);
            if (midRoofHoleNo == 1) swComp.Suppress("LPattern1");
            else
            {
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D1@LPattern1", midRoofHoleNo);
            }
            #endregion 基本尺寸

            #region MARVEL
            
            #endregion MARVEL

            #region UV HOOD
            if (bluetooth == "YES") swComp.UnSuppress("SUCABLE-LEFT");
            else swComp.Suppress("SUCABLE-LEFT");
            swComp.UnSuppress("SUCABLE-RIGHT");
            if (sidePanel == "LEFT" || sidePanel == "BOTH") swComp.UnSuppress("JUNCTION BOX-LEFT");
            else swComp.Suppress("JUNCTION BOX-LEFT");
            swComp.UnSuppress("JUNCTION BOX-RIGHT");
            #endregion UV HOOD
        }

        /// <summary>
        /// 新风前面板
        /// </summary>
        public void FNHA0120(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 1d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }

        /// <summary>
        /// 新风底部CJ孔板
        /// </summary>
        public void FNHA0114(Component2 swComp, double length, int frontCjNo, double frontCjFirstDis, int frontPanelHoleNo, double frontPanelHoleDis, string bluetooth, string ledLogo, string waterCollection, string sidePanel)
        {
            #region 基本尺寸
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D1@CJHOLES", frontCjNo);
            swPart.ChangeDim("D10@草图8", frontCjFirstDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
            if (bluetooth == "YES") swComp.UnSuppress("BLUETOOTH");
            else swComp.Suppress("BLUETOOTH");
            if (ledLogo == "YES") swComp.UnSuppress("LOGO");
            else swComp.Suppress("LOGO");
            #endregion 基本尺寸

            #region 集水翻边
            if (waterCollection == "YES")
            {
                if (sidePanel == "RIGHT")
                {
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                    swComp.Suppress("DRAINCHANNEL-LEFT");
                }
                else if (sidePanel == "LEFT")
                {
                    swComp.Suppress("DRAINCHANNEL-RIGHT");
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                }
                else if (sidePanel == "BOTH")
                {
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                }
            }
            else
            {
                swComp.Suppress("DRAINCHANNEL-RIGHT");
                swComp.Suppress("DRAINCHANNEL-LEFT");
            }
            #endregion 集水翻边
        }

        #endregion 华为新风I650

        #region 华为新风F400
        /// <summary>
        /// F400型新风腔主体
        /// </summary>
        public void FNHA0092(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo, int suNo, double suDis, string marvel, int irNo, double irDis1, double irDis2, double irDis3, string bluetooth, string sidePanel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D1@阵列(线性)1", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)1", frontPanelKaKouDis);
            swPart.ChangeDim("D3@Sketch4", midRoofSecondHoleDis);
            swPart.ChangeDim("D9@草图7", 200d - midRoofTopHoleDis);
            if (midRoofHoleNo == 1) swComp.Suppress("LPattern1");
            else
            {
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D1@LPattern1", midRoofHoleNo);
            }
            //新风脖颈
            swPart.ChangeDim("D1@Sketch8", suDis * (suNo / 2 - 1) + suDis / 2d);
            if (suNo == 1) swComp.Suppress("LPattern2");
            else
            {
                swComp.UnSuppress("LPattern2");
                swPart.ChangeDim("D1@LPattern2", suNo);
                swPart.ChangeDim("D3@LPattern2", suDis);
            }
            //UV HOOD
            if (bluetooth == "YES") swComp.UnSuppress("SUCABLE-LEFT");
            else swComp.Suppress("SUCABLE-LEFT");
            if (sidePanel == "LEFT" || sidePanel == "BOTH") swComp.UnSuppress("JUNCTION BOX-LEFT");
            else swComp.Suppress("JUNCTION BOX-LEFT");
        }

        /// <summary>
        /// F400镀锌隔板
        /// </summary>
        public void FNHA0099(Component2 swComp, double length, string marvel, int irNo, double irDis1, double irDis2, double irDis3)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Sketch1", length - 9d);
        }
        /// <summary>
        /// F650新风前面板
        /// </summary>
        public void FNHA0094(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 1d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }

        #endregion 华为新风F400

    }
}