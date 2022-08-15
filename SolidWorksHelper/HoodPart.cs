using System;
using SolidWorks.Interop.sldworks;

namespace SolidWorksHelper
{
    public class HoodPart
    {
        //模块化改造，Ctrl+M+O 折叠代码
        #region Exhaust排风腔模块
        //KvExhaust,UvExhaust,KwExhaust,UwExhaust
        internal void KvExhaust(Component2 swTopLevelComp, string suffix, double length, int exNo, double exRightDis, double exLength, double exWidth, double exDis, string waterCollection, string sidePanel, string outlet, string backToBack, string ansul, string anSide, string anDetector, string marvel, string uvType)
        {
            //计算中间值
            int midRoofHoleNo = (int)((length - 300d) / 400d);
            double midRoofSecondHoleDis = Convert.ToDouble((length - (midRoofHoleNo - 1) * 400d) / 2);


            //顶层子装配
            var swTopLevelAssy = swTopLevelComp as AssemblyDoc;
            var swComp = swTopLevelAssy.GetComponentByNameWithSuffix(suffix, "FNHE0001-1");
            FNHE0001(swComp, length, midRoofHoleNo, midRoofSecondHoleDis, exNo, exRightDis, exLength, exWidth, exDis, waterCollection, sidePanel, outlet, backToBack, ansul, anSide, anDetector, marvel, uvType);


        }







        //排风腔
        internal void FNHE0001(Component2 swComp, double length, int midRoofHoleNo, double midRoofSecondHoleDis, int exNo, double exRightDis, double exLength, double exWidth, double exDis, string waterCollection, string sidePanel, string outlet, string backToBack, string ansul, string anSide, string anDetector, string marvel, string uvType)
        {
            #region 基本参数
            ModelDoc2 swPart = swComp.GetModelDoc2();
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
            #endregion

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
            #endregion

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
            #endregion

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
            #endregion

            //背靠背
            if (backToBack == "YES") swComp.UnSuppress("BACKTOBACK");
            else swComp.Suppress("BACKTOBACK");

            #region ANSUL
            if (ansul == "YES")
            {
                //侧喷
                if (anSide == "LEFT")
                {
                    swComp.UnSuppress("ANSUL-LEFT");
                    swComp.UnSuppress("CHANNEL-LEFT");
                }
                else if (anSide == "RIGHT")
                {
                    swComp.UnSuppress("ANSUL-RIGHT");
                    swComp.UnSuppress("CHANNEL-RIGHT");
                }
                else
                {
                    swComp.Suppress("ANSUL-LEFT");
                    swComp.Suppress("ANSUL-RIGHT");
                    swComp.Suppress("CHANNEL-LEFT");
                    swComp.Suppress("CHANNEL-RIGHT");
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
            #endregion

            #region MARVEL
            if (marvel == "YES")
            {
                swComp.UnSuppress("MA-NTC");
                if (exNo == 1) swPart.ChangeDim("D1@Sketch21", exRightDis + exLength / 2d + 50d);
                else swPart.ChangeDim("D1@Sketch21", exRightDis + exDis / 2d + exLength + 50d);
            }
            else swComp.Suppress("MA-NTC");
            #endregion

            #region UVHood
            if (uvType == "LONG")
            {
                swComp.UnSuppress("UVRACK");
                swPart.ChangeDim("D6@Sketch12", exRightDis);
                swPart.ChangeDim("D5@Sketch12", 1640d);
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D4@Sketch11", exRightDis);
                swPart.ChangeDim("D1@Sketch11", 1500d);
            }
            else if (uvType == "SHORT")
            {
                swComp.UnSuppress("UVRACK");
                swPart.ChangeDim("D6@Sketch12", exRightDis);
                swPart.ChangeDim("D5@Sketch12", 930d);
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D4@Sketch11", exRightDis);
                swPart.ChangeDim("D1@Sketch11", 790d);
            }
            else
            {
                swComp.Suppress("UVRACK");
                swComp.Suppress("UVCABLE");
            }
            #endregion
        }

        //排风腔前面板
        internal void FNHE0002(Component2 swComp, double length, string UVType, double exRightDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length);
            #region UVHood
            if (UVType == "LONG")
            {
                swComp.UnSuppress("EXTAB-UP");
                swComp.UnSuppress("FILTER-CABLE");
                swComp.Suppress("UVDOOR-SHORT");
                swComp.UnSuppress("UVDOOR-LONG");
                swPart.ChangeDim("D2@Sketch17", exRightDis);
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D3@Sketch22", exRightDis);
                swPart.ChangeDim("D4@Sketch22", 1500d);
            }
            else if (UVType == "SHORT")
            {
                swComp.UnSuppress("EXTAB-UP");
                swComp.UnSuppress("FILTER-CABLE");
                swComp.UnSuppress("UVDOOR-SHORT");
                swComp.Suppress("UVDOOR-LONG");
                swPart.ChangeDim("D1@Sketch21", exRightDis);
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D3@Sketch22", exRightDis);
                swPart.ChangeDim("D4@Sketch22", 790d);
            }
            else
            {
                swComp.Suppress("EXTAB-UP");
                swComp.Suppress("FILTER-CABLE");
                swComp.Suppress("UVDOOR-SHORT");
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVCABLE");
            }
            #endregion
        }


        internal void FNHE0014(Component2 swComp, double length, string ansul, string anDetector)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", length - 8d);
            if (ansul == "YES")
            {
                if (anDetector == "BOTH")
                {
                    swComp.UnSuppress("ANDTEC-LEFT");
                    swComp.UnSuppress("ANDTEC-RIGHT");
                }
                else if (anDetector == "LEFT")
                {
                    swComp.UnSuppress("ANDTEC-LEFT");
                    swComp.Suppress("ANDTEC-RIGHT");
                }
                if (anDetector == "RIGHT")
                {
                    swComp.Suppress("ANDTEC-LEFT");
                    swComp.UnSuppress("ANDTEC-RIGHT");
                }
                else
                {
                    swComp.Suppress("ANDTEC-LEFT");
                    swComp.Suppress("ANDTEC-RIGHT");
                }
            }
            else
            {
                swComp.Suppress("ANDTEC-LEFT");
                swComp.Suppress("ANDTEC-RIGHT");
            }
        }

        internal void FNHE0015(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Sketch1", length - 5d);
        }

        internal void FNHE0016(Component2 swComp, double length, string UVType, double exRightDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", length - 5d);
            if (UVType == "LONG")
            {
                swComp.Suppress("UVDOOR-SHORT");
                swComp.UnSuppress("UVDOOR-LONG");
                swPart.ChangeDim("D5@Sketch8", exRightDis - 2.5d);
            }
            else if (UVType == "SHORT")
            {
                swComp.Suppress("UVDOOR-LONG");
                swComp.UnSuppress("UVDOOR-SHORT");
                swPart.ChangeDim("D1@Sketch9", exRightDis - 2.5d);
            }
            else
            {
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVDOOR-SHORT");
            }
        }






        internal void KSAFilter(AssemblyDoc swAssy, string suffix, double ksaSideLength, string leftPart, string rightPart, string specialPart)
        {
            Component2 swComp;
            ModelDoc2 swPart;
            if (ksaSideLength < 12d && ksaSideLength > 2d)
            {
                swAssy.Suppress(suffix, leftPart);
                swAssy.Suppress(suffix, rightPart);
                swComp = swAssy.UnSuppress(suffix, specialPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D1@草图1", ksaSideLength * 2d);
            }
            else if (ksaSideLength < 25d && ksaSideLength >= 12d)
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

        internal void ExhaustRail(AssemblyDoc swAssy, string suffix, string marvel, double exLength, double exWidth,
            int exNo, double exDis, string doorPart1, string doorPart2, string railPart1, string railPart2)
        {
            Component2 swComp;
            ModelDoc2 swPart;
            if (exWidth == 300d) swAssy.Suppress(suffix, doorPart2);
            else swAssy.UnSuppress(suffix, doorPart2);
            if (exWidth == 300d) swAssy.Suppress(suffix, doorPart1);
            else
            {
                swComp = swAssy.UnSuppress(suffix, doorPart1);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D1@Sketch1", exLength / 2d + 10d);
                swPart.ChangeDim("D2@Sketch1", exWidth + 20d);
            }
            if (marvel == "YES") swAssy.Suppress(suffix, railPart2);
            else swAssy.UnSuppress(suffix, railPart2);
            if (marvel == "YES") swAssy.Suppress(suffix, railPart1);
            else
            {
                swComp = swAssy.UnSuppress(suffix, railPart1);
                swPart = swComp.GetModelDoc2();
                if (exNo == 1) swPart.ChangeDim("D2@Base-Flange1", exLength * 2d + 100d);
                else swPart.ChangeDim("D2@Base-Flange1", exLength * 3d + exDis + 100d);
            }
        }

        internal void ExhaustSpigot(AssemblyDoc swAssy, string suffix, string ansul, string marvel, double exLength,
            double exWidth, double exHeight, string frontPart, string backPart, string leftPart, string rightPart)
        {
            Component2 swComp;
            ModelDoc2 swPart;
            if (ansul != "YES" && (exHeight == 100d || marvel == "YES"))
            {
                swAssy.Suppress(suffix, frontPart);
                swAssy.Suppress(suffix, backPart);
                swAssy.Suppress(suffix, leftPart);
                swAssy.Suppress(suffix, rightPart);
            }
            else
            {
                swComp = swAssy.UnSuppress(suffix, frontPart);
                swPart = swComp.GetModelDoc2();
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
        }

        //三角板
        internal void ExhaustSide(AssemblyDoc swAssy, string suffix, string ansul, string sidePanel, string leftPart, string rightPart)
        {
            Component2 swComp;
            if (ansul == "YES" && sidePanel == "MIDDLE")
            {
                swAssy.UnSuppress(suffix, leftPart);
                swComp = swAssy.UnSuppress(suffix, rightPart);
                swComp.UnSuppress("ANDTEC");
            }
            else if (ansul == "YES" && sidePanel == "LEFT")
            {
                swAssy.Suppress(suffix, leftPart);
                swComp = swAssy.UnSuppress(suffix, rightPart);
                swComp.UnSuppress("ANDTEC");
            }
            else if (ansul == "YES" && sidePanel == "RIGHT")
            {
                swAssy.Suppress(suffix, rightPart);
                swComp = swAssy.UnSuppress(suffix, leftPart);
                swComp.FeatureByName("ANDTEC");
            }
            else
            {
                swAssy.Suppress(suffix, leftPart);
                swAssy.Suppress(suffix, rightPart);
            }
        }

        //----------UV灯，UV灯门----------
        internal void UvLightDoor(AssemblyDoc swAssy, string suffix, string uvType, string doorLong, string uvLong, string doorShort, string uvShort)
        {
            if (uvType == "LONG")
            {
                swAssy.UnSuppress(suffix, doorLong);
                swAssy.UnSuppress(suffix, uvLong);
                swAssy.Suppress(suffix, doorShort);
                swAssy.Suppress(suffix, uvShort);
            }
            else
            {
                swAssy.Suppress(suffix, doorLong);
                swAssy.Suppress(suffix, uvLong);
                swAssy.UnSuppress(suffix, doorShort);
                swAssy.UnSuppress(suffix, uvShort);
            }
        }

        public void MeshFilter(AssemblyDoc swAssy, string suffix, double length, string ansul, string anSide, string leftPart, string rightPart)
        {
            //MESH侧板长度(除去排风三角板3dm计算)
            double meshSideLength = Convert.ToDouble((length - 3d - (int)((length - 2d) / 498d) * 498d) / 2);
            Component2 swComp;
            ModelDoc2 swPart;
            if (ansul == "YES")
            {
                if (meshSideLength * 2d < 55d) meshSideLength += 249d;
                if ((meshSideLength + 20d) > 55d)
                {
                    if (anSide == "LEFT")
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        swComp.UnSuppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        swComp.Suppress("ANSUL");
                    }
                    else if (anSide == "RIGHT")
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        swComp.Suppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        swComp.UnSuppress("ANSUL");
                    }
                    else
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        swComp.Suppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        swComp.Suppress("ANSUL");
                    }
                }
                else
                {
                    if (anSide == "LEFT")
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength * 2d);
                        swComp.UnSuppress("ANSUL");

                        swAssy.Suppress(suffix, rightPart);
                    }
                    else
                    {
                        swAssy.Suppress(suffix, leftPart);

                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength * 2d);
                        swComp.Suppress("ANSUL");
                    }
                }
            }
            else
            {
                if (2 * meshSideLength < 15d && meshSideLength > 1.5d) meshSideLength += 249d;
                if (meshSideLength > 30d)
                {
                    swComp = swAssy.UnSuppress(suffix, leftPart);
                    swPart = swComp.GetModelDoc2();
                    swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                    swComp.Suppress("ANSUL");

                    swComp = swAssy.UnSuppress(suffix, rightPart);
                    swPart = swComp.GetModelDoc2();
                    swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                    swComp.Suppress("ANSUL");
                }
                else if (meshSideLength <= 30d && meshSideLength > 1.5d)
                {
                    swAssy.Suppress(suffix, leftPart);

                    swComp = swAssy.UnSuppress(suffix, rightPart);
                    swPart = swComp.GetModelDoc2();
                    swPart.ChangeDim("D2@Sketch1", meshSideLength * 2d);
                    swComp.Suppress("ANSUL");
                }
                else
                {
                    swAssy.Suppress(suffix, leftPart);
                    swAssy.Suppress(suffix, rightPart);
                }
            }
        }

        //----------吊装槽钢----------
        public void Std2900100001(Component2 swComp, string ansul, double depth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            if (ansul == "YES") swPart.ChangeDim("D2@基体-法兰1", depth - 250);
            else swPart.ChangeDim("D2@基体-法兰1", depth - 100d);
        }



        //水洗排风腔体
        public void FNHE0031(Component2 swComp, double length, string waterCollection, string sidePanel, string outlet, string backToBack)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length);
            //集水翻边
            if (waterCollection == "YES")
            {
                if (sidePanel == "RIGHT")
                {
                    swComp.Suppress("DRAINCHANNEL-LEFT");
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                }
                else if (sidePanel == "LEFT")
                {
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                    swComp.Suppress("DRAINCHANNEL-RIGHT");
                }
                else if (sidePanel == "BOTH")
                {
                    swComp.UnSuppress("DRAINCHANNEL-LEFT");
                    swComp.UnSuppress("DRAINCHANNEL-RIGHT");
                }
            }
            else
            {
                swComp.Suppress("DRAINCHANNEL-LEFT");
                swComp.Suppress("DRAINCHANNEL-RIGHT");
            }
            //下水口
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
            //背靠背
            if (backToBack == "YES") swComp.UnSuppress("BACKTOBACK");
            else swComp.Suppress("BACKTOBACK");
        }

        //水洗排风腔顶部零件
        public void FNHE0032(Component2 swComp, double length, double midRoofSecondHoleDis, int midRoofHoleNo, int exNo, double exRightDis, double exLength, double exWidth, double exDis, string inlet, string ansul, string anSide, string marvel, string uvType)
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
            if (ansul == "YES")
            {
                //侧喷
                if (anSide == "LEFT")
                {
                    swComp.UnSuppress("ANSUL-LEFT");
                    swComp.UnSuppress("CHANNEL-LEFT");
                }
                else if (anSide == "RIGHT")
                {
                    swComp.UnSuppress("ANSUL-RIGHT");
                    swComp.UnSuppress("CHANNEL-RIGHT");
                }
                else
                {
                    swComp.Suppress("ANSUL-LEFT");
                    swComp.Suppress("CHANNEL-LEFT");
                    swComp.Suppress("ANSUL-RIGHT");
                    swComp.Suppress("CHANNEL-RIGHT");
                }
            }
            //MARVEL
            if (marvel == "YES")
            {
                swComp.UnSuppress("MA-NTC");
                if (exNo == 1) swPart.ChangeDim("D1@Sketch20", exRightDis + exLength / 2 + 50d);
                else swPart.ChangeDim("D1@Sketch20", exRightDis + exDis / 2 + exLength + 50d);
            }
            else swComp.Suppress("MA-NTC");
            //UVHood,UVRack-UV灯架孔和UV cable-UV灯线缆穿孔
            if (uvType != "NO")
            {
                swComp.UnSuppress("UVRACK");
                swPart.ChangeDim("D1@Sketch16", exRightDis);
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D1@Sketch17", exRightDis);
                if (uvType == "LONG")
                {

                    swPart.ChangeDim("D7@Sketch16", 1640d);
                    swPart.ChangeDim("D4@Sketch17", 1500d);
                }
                else
                {
                    //SHORT
                    swPart.ChangeDim("D7@Sketch16", 930d);
                    swPart.ChangeDim("D4@Sketch17", 790d);
                }
            }
            else
            {
                //非UVHood
                swComp.Suppress("UVRACK");
                swComp.Suppress("UVCABLE");
            }
            //解压检修门，水洗烟罩，且带UV，短灯>=1600,长灯>=2400
            if ((uvType == "SHORT" && length >= 1600d) || (uvType == "LONG" && length >= 2400d))
            {
                if (inlet == "LEFT")
                {
                    swComp.Suppress("DOOR-R");
                    swComp.UnSuppress("DOOR-L");
                }
                else if (inlet == "RIGHT")
                {
                    swComp.UnSuppress("DOOR-R");
                    swComp.Suppress("DOOR-L");
                }
                else
                {
                    swComp.Suppress("DOOR-R");
                    swComp.Suppress("DOOR-L");
                }
            }
            else
            {
                swComp.Suppress("DOOR-R");
                swComp.Suppress("DOOR-L");
            }
        }

        //----------水洗UV排风腔顶部检修门盖板----------
        public void ExhaustTopCover(AssemblyDoc swAssy, string suffix, string uvType, double length, string inlet, string leftPart, string rightPart)
        {
            if ((uvType == "SHORT" && length >= 1600d) || (uvType == "LONG" && length >= 2400d))
            {
                if (inlet == "LEFT")
                {
                    swAssy.UnSuppress(suffix, leftPart);
                    swAssy.Suppress(suffix, rightPart);
                }
                else if (inlet == "RIGHT")
                {
                    swAssy.Suppress(suffix, leftPart);
                    swAssy.UnSuppress(suffix, rightPart);
                }
                else
                {
                    swAssy.Suppress(suffix, leftPart);
                    swAssy.Suppress(suffix, rightPart);
                }
            }
            else
            {
                swAssy.Suppress(suffix, leftPart);
                swAssy.Suppress(suffix, rightPart);
            }
        }


        //----------水洗排风腔前面板----------
        public void FNHE0033(Component2 swComp, double length, string inlet, string marvel, double exRightDis, string uvType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", length);
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
            if (marvel == "YES" || uvType!="NO") swComp.UnSuppress("EXTAB-UP");
            else swComp.Suppress("EXTAB-UP");
            //UV HoodParent,过滤器感应出线孔，UV门，UV cable-UV灯线缆穿孔避让缺口
            if (uvType == "LONG")
            {
                swComp.Suppress("UVDOOR-SHORT");
                swComp.UnSuppress("UVDOOR-LONG");
                swPart.ChangeDim("D1@Sketch7", exRightDis);
                swComp.UnSuppress("FILTER-CABLE");
                swPart.ChangeDim("D1@Sketch10", exRightDis);
                swComp.UnSuppress("BFCABLE");
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D2@Sketch10", 1500d);
            }
            else if (uvType == "SHORT")
            {
                swComp.Suppress("UVDOOR-LONG");
                swComp.UnSuppress("UVDOOR-SHORT");
                swPart.ChangeDim("D1@Sketch6", exRightDis);
                swComp.UnSuppress("FILTER-CABLE");
                swPart.ChangeDim("D1@Sketch10", exRightDis);
                swComp.UnSuppress("BFCABLE");
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D2@Sketch10", 790d);
            }
            else
            {
                swComp.Suppress("UVDOOR-SHORT");
                swComp.Suppress("UVDOOR-LONG");
                swComp.Suppress("UVCABLE");
                swComp.Suppress("FILTER-CABLE");
                swComp.Suppress("BFCABLE");
            }
        }

        //----------水洗三角板上的UV----------内部运水
        public void FNHE0034(Component2 swComp, string outlet, string sidePanel, string uvType)
        {
            if (outlet == "NO" && (sidePanel == "RIGHT" || sidePanel == "MIDDLE"))
                swComp.UnSuppress("DRAINPIPE-NO");
            else swComp.Suppress("DRAINPIPE-NO");
            if (uvType == "NO") swComp.Suppress("UWHOOD");
            else swComp.UnSuppress("UWHOOD");
        }
        public void FNHE0035(Component2 swComp, string outlet, string sidePanel)
        {
            if (outlet == "NO" && (sidePanel == "LEFT" || sidePanel == "MIDDLE"))
                swComp.UnSuppress("DRAINPIPE-NO");
            else swComp.Suppress("DRAINPIPE-NO");
        }
        //----------水洗挡板----------
        public void FNHE0036(Component2 swComp, double length, string uvType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            //2021.06.08 july更改模型，磁铁拉铆钉避让1.5dm
            swPart.ChangeDim("D2@Sketch1", length - 105d - 1.5d);
            if (uvType == "NO") swComp.Suppress("UWHOOD");
            else swComp.UnSuppress("UWHOOD");
        }

        //----------水洗水管----------
        public void Std2900500003(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Boss-Extrude1", length - 125d);
        }
        //----------水洗KSA下轨道支架----------
        public void FNHE0037(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Sketch1", length - 5d);
        }

        //----------水洗UV灯，UV灯门----------
        internal void UwLightDoor(AssemblyDoc swAssy, string suffix, string uvType, string doorLong, string doorFrameLong, string uvLong, string doorShort, string doorFrameShort, string uvShort)
        {
            if (uvType == "LONG")
            {
                swAssy.UnSuppress(suffix, doorLong);
                swAssy.UnSuppress(suffix, doorFrameLong);
                swAssy.UnSuppress(suffix, uvLong);
                swAssy.Suppress(suffix, doorShort);
                swAssy.Suppress(suffix, doorFrameShort);
                swAssy.Suppress(suffix, uvShort);
            }
            else
            {
                swAssy.Suppress(suffix, doorLong);
                swAssy.Suppress(suffix, doorFrameLong);
                swAssy.Suppress(suffix, uvLong);
                swAssy.UnSuppress(suffix, doorShort);
                swAssy.UnSuppress(suffix, doorFrameShort);
                swAssy.UnSuppress(suffix, uvShort);
            }
        }

        //----------水洗MESH油网侧板----------
        public void UwMeshFilter(AssemblyDoc swAssy, string suffix, double length, string inlet, string ansul, string anSide, string leftPart, string rightPart)
        {
            //MESH侧板长度(除去排风三角板3dm计算,20220812-增加了考虑水管孔避让，最小55，再加上20错开KSA)
            double meshSideLength = Convert.ToDouble((length - 3d -(int)((length-2d-35d) / 498d) * 498d) / 2d);

            Component2 swComp;
            ModelDoc2 swPart;

            if ((inlet == "LEFT" && anSide == "RIGHT") || (anSide == "LEFT" && inlet == "RIGHT"))//ANSUL和进水管不同一侧
            {
                if ((meshSideLength - 20d) < 55d) meshSideLength += 249d;//再减少一个MESH(498/2)
                swComp = swAssy.UnSuppress(suffix, leftPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                if (inlet == "LEFT") swComp.UnSuppress("KW");
                else swComp.Suppress("KW");
                if (ansul == "YES" && anSide == "LEFT") swComp.UnSuppress("ANSUL");
                else swComp.Suppress("ANSUL");

                swComp = swAssy.UnSuppress(suffix, rightPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                if (inlet == "RIGHT") swComp.UnSuppress("KW");
                else swComp.Suppress("KW");
                if (ansul == "YES" && anSide == "RIGHT") swComp.UnSuppress("ANSUL");
                else swComp.Suppress("ANSUL");
            }
            else
            {
                //ANSUL和进水管在同一侧（保险，有可能不可能）
                if (meshSideLength * 2 < 55d) meshSideLength += 249d;//如果只有一个MESH侧板，再减少一个MESH(498/2)

                if ((meshSideLength + 20d) > 55d) //最大侧板>55，才能穿水管
                {
                    if (inlet == "LEFT")
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        if (inlet == "LEFT") swComp.UnSuppress("KW");
                        else swComp.Suppress("KW");
                        if (ansul == "YES" && anSide == "LEFT") swComp.UnSuppress("ANSUL");
                        else swComp.Suppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        if (inlet == "RIGHT") swComp.UnSuppress("KW");
                        else swComp.Suppress("KW");
                        if (ansul == "YES" && anSide == "RIGHT") swComp.UnSuppress("ANSUL");
                        else swComp.Suppress("ANSUL");
                    }
                    else
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        if (inlet == "LEFT") swComp.UnSuppress("KW");
                        else swComp.Suppress("KW");
                        if (ansul == "YES" && anSide == "LEFT") swComp.UnSuppress("ANSUL");
                        else swComp.Suppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        if (inlet == "RIGHT") swComp.UnSuppress("KW");
                        else swComp.Suppress("KW");
                        if (ansul == "YES" && anSide == "RIGHT") swComp.UnSuppress("ANSUL");
                        else swComp.Suppress("ANSUL");
                    }
                }
                else
                {
                    //只做一个MESH侧板的情况
                    if (inlet == "LEFT")
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", 2d * meshSideLength);
                        if (inlet == "LEFT") swComp.UnSuppress("KW");
                        else swComp.Suppress("KW");
                        if (ansul == "YES" && anSide == "LEFT") swComp.UnSuppress("ANSUL");
                        else swComp.Suppress("ANSUL");
                        swAssy.Suppress(suffix, rightPart);
                    }
                    else
                    {
                        swAssy.Suppress(suffix, leftPart);
                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", 2d*meshSideLength);
                        if (inlet == "RIGHT") swComp.UnSuppress("KW");
                        else swComp.Suppress("KW");
                        if (ansul == "YES" && anSide == "RIGHT") swComp.UnSuppress("ANSUL");
                        else swComp.Suppress("ANSUL");
                    }
                }
            }
        }

        //----------水洗排风腔内部零件----------
        public void FNHE0040(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", length - 8d);
        }

        public void FNHE0041(Component2 swComp, double length, double exRightDis, string uvType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", length - 5d);
            if (uvType == "LONG")
            {
                swComp.Suppress("UVDOOR-SHORT");
                swComp.UnSuppress("UVDOOR-LONG");
                swPart.ChangeDim("D12@Sketch3", exRightDis - 2.5d);
            }
            else if (uvType == "SHORT")
            {
                swComp.UnSuppress("UVDOOR-SHORT");
                swPart.ChangeDim("D10@Sketch4", exRightDis - 2.5d);
                swComp.Suppress("UVDOOR-LONG");
            }
            else
            {
                swComp.Suppress("UVDOOR-SHORT");
                swComp.Suppress("UVDOOR-LONG");
            }

        }




        //KVIR圆心烟罩排风腔体
        //----------R圆形烟罩排风腔----------
        public void FNHE0042(Component2 swComp, double exBeamLength, double midRoofSecondHoleDis, int midRoofHoleNo, int exNo, double exRightDis, double exLength, double exWidth, double exDis, string marvel, string uvType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1", exBeamLength);
            swPart.ChangeDim("D1@Sketch16", midRoofSecondHoleDis);
            if (midRoofHoleNo == 1)
            {
                swComp.Suppress("LPattern1");
            }
            else
            {
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D1@LPattern1", midRoofHoleNo);
            }

            #region 排风口
            if (exNo == 1)
            {
                swComp.UnSuppress("EXCOONE");
                swComp.Suppress("EXCOTWO");
                swPart.ChangeDim("D3@Sketch12", exRightDis);
                swPart.ChangeDim("D1@Sketch12", exLength);
                swPart.ChangeDim("D2@Sketch12", exWidth);
            }
            else
            {
                swComp.Suppress("EXCOONE");
                swComp.UnSuppress("EXCOTWO");
                swPart.ChangeDim("D1@Sketch13", exRightDis);
                swPart.ChangeDim("D2@Sketch13", exDis);
                swPart.ChangeDim("D3@Sketch13", exLength);
                swPart.ChangeDim("D4@Sketch13", exWidth);
            }
            #endregion

            #region MARVEL
            if (marvel == "YES")
            {
                swComp.UnSuppress("MA-TAB");
                swComp.UnSuppress("MA-NTC");
                if (exNo == 1) swPart.ChangeDim("D1@Sketch23", exRightDis + exLength / 2d + 50d);
                else swPart.ChangeDim("D1@Sketch23", exRightDis + exDis / 2d + exLength + 50d);
            }
            else
            {
                swComp.Suppress("MA-TAB");
                swComp.Suppress("MA-NTC");
            }
            #endregion

            #region UV
            if (uvType == "LONG")
            {
                swComp.UnSuppress("UVRACK");
                swPart.ChangeDim("D7@Sketch2", exRightDis);
                swPart.ChangeDim("D3@Sketch2", 1680d);
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D4@Sketch15", exRightDis);
                swPart.ChangeDim("D2@Sketch15", 1120d);
                swComp.UnSuppress("UV-TAB");
                swComp.UnSuppress("KSACABLE");
                swComp.UnSuppress("JUNCTION BOX UV");
            }
            else if (uvType == "SHORT")
            {
                swComp.UnSuppress("UVRACK");
                swPart.ChangeDim("D7@Sketch2", exRightDis);
                swPart.ChangeDim("D3@Sketch2", 975d);
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D4@Sketch15", exRightDis);
                swPart.ChangeDim("D2@Sketch15", 520d);
                swComp.UnSuppress("UV-TAB");
                swComp.UnSuppress("KSACABLE");
                swComp.UnSuppress("JUNCTION BOX UV");
            }
            else
            {
                //UVHood,UVRack-UV灯架孔和UV cable-UV灯线缆穿孔
                swComp.Suppress("UVRACK");
                swComp.Suppress("UVCABLE");
                swComp.Suppress("UV-TAB");
                swComp.Suppress("KSACABLE");
                swComp.Suppress("JUNCTION BOX UV");
            }
            swComp.UnSuppress("JUNCTION BOX LIGHT");
            #endregion
        }

        //----------排风腔底部----------
        public void FNHE0043(Component2 swComp, double exBeamLength, string outlet)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1", exBeamLength);
            //油塞
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
        }

        //----------排风腔前面板、后面板----------
        public void FNHE0044(Component2 swComp, double exBeamLength)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", exBeamLength);
        }
        public void FNHE0045(Component2 swComp, double exBeamLength)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", exBeamLength);
        }

        //----------三角板----------
        public void FNHE0050(Component2 swComp, string uvType)
        {
            //UV烟罩解压缩特征
            if (uvType=="NO") swComp.Suppress("Cut-Extrude2");
            else swComp.UnSuppress("Cut-Extrude2");
        }





        #endregion

        #region MidRoof模块
        //----------灯具,日光灯----------
        internal void Light(AssemblyDoc swAssy, string suffix, string lightType, string LongPart, string shortPart)
        {
            if (lightType == "FSLONG")
            {
                swAssy.UnSuppress(suffix, LongPart);
                swAssy.Suppress(suffix, shortPart);
            }
            else if (lightType == "FSSHORT")
            {
                swAssy.Suppress(suffix, LongPart);
                swAssy.UnSuppress(suffix, shortPart);
            }
            else
            {
                swAssy.Suppress(suffix, LongPart);
                swAssy.Suppress(suffix, shortPart);
            }
        }


        //----------MiddleRoof灯板----------
        internal void FNHM0001(Component2 swComp, string exType, double length, double depth, double exHeight, double suHeight, double exRightDis, double midRoofTopHoleDis, double midRoofSecondHoleDis, int midRoofHoleNo, string lightType, double lightYDis, int ledSpotNo, double ledSpotDis, string ansul, int anDropNo, double anYDis, double anDropDis1, double anDropDis2, double anDropDis3, double anDropDis4, double anDropDis5, string anDetectorEnd, int anDetectorNo, double anDetectorDis1, double anDetectorDis2, double anDetectorDis3, double anDetectorDis4, double anDetectorDis5, string bluetooth, string uvType, string marvel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();

            #region 基本尺寸
            swPart.ChangeDim("D1@草图1", length - 4d);
            swPart.ChangeDim("D2@草图1", depth - 669d);
            swPart.ChangeDim("D1@草图6", depth - 896d);
            swPart.ChangeDim("D3@草图25", midRoofTopHoleDis);
            swPart.ChangeDim("D2@草图26", (depth - 840d) / 3d);
            swPart.ChangeDim("D1@Sketch3", midRoofSecondHoleDis - 2d);

            if (midRoofHoleNo == 1) swComp.Suppress("LPattern1");
            else
            {
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D1@LPattern1", midRoofHoleNo);
            }
            #endregion

            #region 灯具
            if (lightType == "LED60")
            {
                swComp.Suppress("LED140");
                swComp.Suppress("LPattern3");
                swComp.Suppress("FSLONG");
                swComp.Suppress("FSSHORT");
                swComp.UnSuppress("LED60");
                if (ledSpotNo == 1) swPart.ChangeDim("D2@Sketch1", 0d);
                else
                {
                    swPart.ChangeDim("D2@Sketch1", ledSpotDis * (ledSpotNo / 2d - 1d) + ledSpotDis / 2d);
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
                if (ledSpotNo == 1) swPart.ChangeDim("D5@Sketch7", 0d);
                else
                {
                    swPart.ChangeDim("D5@Sketch7", ledSpotDis * (ledSpotNo / 2d - 1d) + ledSpotDis / 2d);
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
            #endregion

            #region ANSUL
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
            swComp.Suppress("ANDTECACROSS");
            if (ansul == "YES")
            {
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

                if (exType == "UW" || exType == "KW" || exType == "CMOD")
                {
                    //探测器
                    swComp.UnSuppress("ANDTECACROSS");
                    if (anDetectorNo > 0)
                    {
                        swComp.UnSuppress("ANDTEC1");
                        swPart.ChangeDim("D1@Sketch31", anDetectorDis1);
                        if (anDetectorEnd == "LEFT" || (anDetectorEnd == "RIGHT" && anDetectorNo == 1))
                            swPart.ChangeDim("D2@Sketch31", 195d);
                        else swPart.ChangeDim("D2@Sketch31", 175d);
                    }
                    if (anDetectorNo > 1)
                    {
                        swComp.UnSuppress("ANDTEC2");
                        swPart.ChangeDim("D1@Sketch32", anDetectorDis2);
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 2)
                            swPart.ChangeDim("D2@Sketch32", 195d);
                        else swPart.ChangeDim("D2@Sketch32", 175d);
                    }
                    if (anDetectorNo > 2)
                    {
                        swComp.UnSuppress("ANDTEC3");
                        swPart.ChangeDim("D1@Sketch33", anDetectorDis3);
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 3)
                            swPart.ChangeDim("D2@Sketch33", 195d);
                        else swPart.ChangeDim("D2@Sketch33", 175d);
                    }
                    if (anDetectorNo > 3)
                    {
                        swComp.UnSuppress("ANDTEC4");
                        swPart.ChangeDim("D1@Sketch34", anDetectorDis4);
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 4)
                            swPart.ChangeDim("D2@Sketch34", 195d);
                        else swPart.ChangeDim("D2@Sketch34", 175d);
                    }
                    if (anDetectorNo > 4)
                    {
                        swComp.UnSuppress("ANDTEC5");
                        swPart.ChangeDim("D1@Sketch35", anDetectorDis5);
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 5)
                            swPart.ChangeDim("D2@Sketch35", 195d);
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
                    swComp.Suppress("ANDTECACROSS");
                }
            }
            #endregion

            #region 开方孔,UV或待MARVEL时解压
            if (exType == "UV" || exType == "UW")
            {
                swComp.UnSuppress("CUT-BACK-LEFT");
                swComp.UnSuppress("CUT-BACK-RIGHT");
            }
            else
            {

                if (marvel == "YES") swComp.UnSuppress("CUT-BACK-LEFT");
                else swComp.Suppress("CUT-BACK-LEFT");
                swComp.Suppress("CUT-BACK-RIGHT");
            }
            swComp.UnSuppress("CUT-FRONT-RIGHT");
            if (bluetooth == "YES") swComp.UnSuppress("CUT-FRONT-LEFT");
            else swComp.Suppress("CUT-FRONT-LEFT");
            #endregion

            #region UV灯线缆穿孔
            if (uvType == "LONG")
            {
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D4@草图28", exRightDis);
                swPart.ChangeDim("D3@草图28", 1500d);
            }
            else if (uvType == "SHORT")
            {
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D4@草图28", exRightDis);
                swPart.ChangeDim("D3@草图28", 790d);
            }
            else
            {
                swComp.Suppress("UVCABLE");
            }
            #endregion

            #region IR
            if (marvel == "YES")
            {
                swComp.UnSuppress("IR-LHC-2");
                swComp.UnSuppress("IR-2");
            }
            else
            {
                swComp.Suppress("IR-LHC-2");
                swComp.Suppress("IR-2");
            }
            #endregion

            //CMOD NTC Sensor
            if (exType == "CMOD") swComp.UnSuppress("NTC Sensor");
            else swComp.Suppress("NTC Sensor");
        }

        //----------灯板加强筋----------
        internal void FNHM0006(Component2 swComp, double depth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", depth - 898d);
        }




        //KVIR圆心烟罩灯板
        //内半径（depth/2d）=外半径（depth/2d）-90d
        //----------MiddleRoof灯板/后----------
        public void FNHM0018(Component2 swComp, double depth, double midRoofSecondHoleDis, int midRoofHoleNo, string lightType, int ledSpotNo, double ledSpotDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D5@Sketch2", depth / 2d - 90d + 1d);
            swPart.ChangeDim("D4@Sketch18", midRoofSecondHoleDis - 52.5d);
            if (midRoofHoleNo == 1) swComp.Suppress("LPattern1");
            else
            {
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D1@LPattern1", midRoofHoleNo);
            }
            //灯具
            if (lightType == "LED60")
            {
                swComp.UnSuppress("LED60");
                if (ledSpotNo == 1)
                {
                    swPart.ChangeDim("D4@Sketch10", 0);
                    swComp.Suppress("LPattern2");
                }
                else
                {
                    swPart.ChangeDim("D4@Sketch10", ledSpotDis * (ledSpotNo / 2d - 1) + ledSpotDis / 2d);
                    swComp.UnSuppress("LPattern2");
                    swPart.ChangeDim("D1@LPattern2", ledSpotNo);
                    swPart.ChangeDim("D3@LPattern2", ledSpotDis);
                }
            }
            else
            {
                swComp.UnSuppress("LED60");
                swComp.UnSuppress("LPattern2");
            }
        }

        //----------MiddleRoof灯板/左右----------
        public void FNHM0009(Component2 swComp, double depth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D4@Sketch9", depth / 2d - 1d);
        }


        //----------MiddleRoof灯板/弯条----------
        public void FNHM0021(Component2 swComp, double depth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Sketch1", depth / 2d - 90d);
        }


        #endregion

        #region SidePanel 普通烟罩大侧板模块

        internal void SidePanel(AssemblyDoc swAssy, string suffix, string sidePanel, double depth, double exHeight, string waterCollection, string exType, string leftOuter, string leftInner, string rightOuter, string rightInner, string leftCollection, string rightCollection)
        {
            //侧板CJ孔整列到烟罩底部
            int sidePanelDownCjNo = (int)((depth - 95d) / 32d);
            //非水洗烟罩KV/UV
            int sidePanelSideCjNo = (int)((depth - 305d) / 32d);
            //水洗烟罩KW/UW
            if (exType == "W") sidePanelSideCjNo = (int)((depth - 380) / 32);
            Component2 swComp;
            #region 大侧板
            if (sidePanel == "BOTH")
            {
                //LEFT
                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FNHS0001(swComp, depth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0002(swComp, depth, exHeight);
                //RIGHT
                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0003(swComp, depth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0004(swComp, depth, exHeight);
            }
            else if (sidePanel == "LEFT")
            {
                swAssy.Suppress(suffix, rightOuter);
                swAssy.Suppress(suffix, rightInner);

                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FNHS0001(swComp, depth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0002(swComp, depth, exHeight);
            }
            else if (sidePanel == "RIGHT")
            {
                swAssy.Suppress(suffix, leftOuter);
                swAssy.Suppress(suffix, leftInner);

                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0003(swComp, depth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0004(swComp, depth, exHeight);
            }
            else
            {
                swAssy.Suppress(suffix, leftOuter);
                swAssy.Suppress(suffix, leftInner);
                swAssy.Suppress(suffix, rightOuter);
                swAssy.Suppress(suffix, rightInner);
            }
            #endregion

            #region 集水翻边
            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "LEFT"))
            {
                swComp = swAssy.UnSuppress(suffix, leftCollection);
                FNHS0005(swComp, depth, exHeight, exHeight, exType);//普通烟罩"V"，水洗"W"
            }
            else
            {
                swAssy.Suppress(suffix, leftCollection);
            }
            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "RIGHT"))
            {
                swComp = swAssy.UnSuppress(suffix, rightCollection);
                FNHS0006(swComp, depth, exHeight, exHeight, exType);//普通烟罩"V"，水洗"W"
            }
            else
            {
                swAssy.Suppress(suffix, rightCollection);
            }
            #endregion
        }

        internal void FNHS0001(Component2 swComp, double depth, double exHeight, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", depth);
            swPart.ChangeDim("D2@草图1", exHeight);
            swPart.ChangeDim("D1@阵列(线性)1", sidePanelSideCjNo);
            swPart.ChangeDim("D1@阵列(线性)2", sidePanelDownCjNo);
        }
        internal void FNHS0002(Component2 swComp, double depth, double exHeight)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", depth - 79d);
            swPart.ChangeDim("D2@草图1", exHeight - 39d);
            if (exHeight == 555d || exHeight == 650d)
            {
                swComp.UnSuppress("F555");
                swComp.Suppress("F450");
                swComp.Suppress("F400");
            }
            else if (exHeight == 450d)
            {
                swComp.Suppress("F555");
                swComp.UnSuppress("F450");
                swComp.Suppress("F400");
            }
            else if (exHeight == 400d)
            {
                swComp.Suppress("F555");
                swComp.Suppress("F450");
                swComp.UnSuppress("F400");
            }
            else
            {
                swComp.Suppress("F555");
                swComp.Suppress("F450");
                swComp.Suppress("F400");
            }
        }
        internal void FNHS0003(Component2 swComp, double depth, double exHeight, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", depth);
            swPart.ChangeDim("D2@草图1", exHeight);
            swPart.ChangeDim("D1@阵列(线性)1", sidePanelSideCjNo);
            swPart.ChangeDim("D1@阵列(线性)2", sidePanelDownCjNo);
        }
        internal void FNHS0004(Component2 swComp, double depth, double exHeight)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", depth - 79d);
            swPart.ChangeDim("D2@草图1", exHeight - 39d);
            if (exHeight == 555d || exHeight == 650d)
            {
                swComp.UnSuppress("F555");
                swComp.Suppress("F450");
                swComp.Suppress("F400");
            }
            else if (exHeight == 450d)
            {
                swComp.Suppress("F555");
                swComp.UnSuppress("F450");
                swComp.Suppress("F400");
            }
            else if (exHeight == 400d)
            {
                swComp.Suppress("F555");
                swComp.Suppress("F450");
                swComp.UnSuppress("F400");
            }
            else
            {
                swComp.Suppress("F555");
                swComp.Suppress("F450");
                swComp.Suppress("F400");
            }
        }
        internal void FNHS0005(Component2 swComp, double depth, double exHeight, double suHeight, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", depth);

            swPart.ChangeDim("D3@Sketch11", depth);

            swPart.ChangeDim("D2@Sketch11", suHeight);//新风
            swPart.ChangeDim("D8@Sketch11", exHeight);//排风
            if (exHeight == 450d)
            {
                swPart.ChangeDim("D6@Sketch11", 105d);//EX450,105
                swPart.ChangeDim("D11@Sketch11", 50d);//EX450,50
                swPart.ChangeDim("D9@Sketch11", 1000d * 135d / 180d * Math.PI);//EX450,135，角度特殊，不能除以1000,应当乘回去
            }
            else
            {
                if (exType == "W")
                {
                    swPart.ChangeDim("D6@Sketch11", 150d);//普通76.5水洗150
                    swPart.ChangeDim("D11@Sketch11", 101d);//普通85水洗101
                    swPart.ChangeDim("D9@Sketch11", 1000d * 145d / 180d * Math.PI);//普通135水洗145
                }
                else
                {
                    swPart.ChangeDim("D6@Sketch11", 76.5d);//普通76.5水洗150
                    swPart.ChangeDim("D11@Sketch11", 85d);//普通85水洗101
                    swPart.ChangeDim("D9@Sketch11", 1000d * 135d / 180d * Math.PI);//普通135水洗145
                }
            }
        }
        internal void FNHS0006(Component2 swComp, double depth, double exHeight, double suHeight, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", depth);

            swPart.ChangeDim("D3@Sketch11", depth);

            swPart.ChangeDim("D2@Sketch11", suHeight);//新风
            swPart.ChangeDim("D8@Sketch11", exHeight);//排风
            if (exHeight == 450d)
            {
                swPart.ChangeDim("D6@Sketch11", 105d);//EX450,105
                swPart.ChangeDim("D11@Sketch11", 50d);//EX450,50
                swPart.ChangeDim("D9@Sketch11", 1000d * 135d / 180d * Math.PI);//EX450,135
            }
            else
            {
                if (exType == "W")
                {
                    swPart.ChangeDim("D6@Sketch11", 150d);//普通76.5水洗150
                    swPart.ChangeDim("D11@Sketch11", 101d);//普通85水洗101
                    swPart.ChangeDim("D9@Sketch11", 1000d * 145d / 180d * Math.PI);//普通135水洗145
                }
                else
                {
                    swPart.ChangeDim("D6@Sketch11", 76.5d);//普通76.5水洗150
                    swPart.ChangeDim("D11@Sketch11", 85d);//普通85水洗101
                    swPart.ChangeDim("D9@Sketch11", 1000d * 135d / 180d * Math.PI);//普通135水洗145
                }
            }
        }

        internal void SidePanelSpecial(AssemblyDoc swAssy, string suffix, string sidePanel, double depth, double exHeight, double suHeight, string waterCollection, string exType, string leftOuter, string leftInner, string rightOuter, string rightInner, string leftCollection, string rightCollection)
        {
            //UVF555400斜侧板CJ孔计算,77为排风底部长度，555-400为高度差
            int sidePanelDownCjNo = (int)((Math.Sqrt(Math.Pow(depth - 77d, 2) + Math.Pow(exHeight - suHeight, 2)) - 95d) / 32d);
            if (exType == "W") sidePanelDownCjNo = (int)((Math.Sqrt(Math.Pow(depth - 150d, 2) + Math.Pow(555d - 400d, 2)) - 95d) / 32d);
            int sidePanelSideCjNo = sidePanelDownCjNo - 3;
            Component2 swComp;
            #region 大侧板
            if (sidePanel == "BOTH")
            {
                //LEFT
                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FNHS0012(swComp, depth, exHeight, suHeight, sidePanelSideCjNo, sidePanelDownCjNo, exType);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0013(swComp, depth, exHeight, suHeight, exType);
                //RIGHT
                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0014(swComp, depth, exHeight, suHeight, sidePanelSideCjNo, sidePanelDownCjNo, exType);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0015(swComp, depth, exHeight, suHeight, exType);
            }
            else if (sidePanel == "LEFT")
            {
                swAssy.Suppress(suffix, rightOuter);
                swAssy.Suppress(suffix, rightInner);

                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FNHS0012(swComp, depth, exHeight, suHeight, sidePanelSideCjNo, sidePanelDownCjNo, exType);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0013(swComp, depth, exHeight, suHeight, exType);
            }
            else if (sidePanel == "RIGHT")
            {
                swAssy.Suppress(suffix, leftOuter);
                swAssy.Suppress(suffix, leftInner);

                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0014(swComp, depth, exHeight, suHeight, sidePanelSideCjNo, sidePanelDownCjNo, exType);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0015(swComp, depth, exHeight, suHeight, exType);
            }
            else
            {
                swAssy.Suppress(suffix, leftOuter);
                swAssy.Suppress(suffix, leftInner);
                swAssy.Suppress(suffix, rightOuter);
                swAssy.Suppress(suffix, rightInner);
            }
            #endregion

            #region 集水翻边
            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "LEFT"))
            {
                swComp = swAssy.UnSuppress(suffix, leftCollection);
                FNHS0005(swComp, depth, exHeight, suHeight, exType);//普通烟罩"V"，水洗"W"
            }
            else
            {
                swAssy.Suppress(suffix, leftCollection);
            }
            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "RIGHT"))
            {
                swComp = swAssy.UnSuppress(suffix, rightCollection);
                FNHS0006(swComp, depth, exHeight, suHeight, exType);//普通烟罩"V"，水洗"W"
            }
            else
            {
                swAssy.Suppress(suffix, rightCollection);
            }
            #endregion
        }

        internal void FNHS0012(Component2 swComp, double depth, double exHeight, double suHeight, int sidePanelSideCjNo,
            int sidePanelDownCjNo, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", depth);
            swPart.ChangeDim("D3@草图1", exHeight);
            swPart.ChangeDim("D2@草图1", suHeight);
            swPart.ChangeDim("D1@阵列(线性)1", sidePanelDownCjNo);
            swPart.ChangeDim("D1@阵列(线性)2", sidePanelSideCjNo);
            if (exType == "V") swPart.ChangeDim("D4@草图1", 77d);
            else swPart.ChangeDim("D4@草图1", 150d);//W水洗
        }

        internal void FNHS0013(Component2 swComp, double depth, double exHeight, double suHeight, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", depth);
            swPart.ChangeDim("D3@Sketch1", exHeight);
            swPart.ChangeDim("D2@Sketch1", suHeight);
            if (exType == "V") swPart.ChangeDim("D4@Sketch1", 77d);
            else swPart.ChangeDim("D4@Sketch1", 150d);//W水洗
            if (suHeight == 400d)
            {
                swComp.UnSuppress("F400");
            }
            else
            {
                swComp.Suppress("F400");
            }
        }

        internal void FNHS0014(Component2 swComp, double depth, double exHeight, double suHeight, int sidePanelSideCjNo,
            int sidePanelDownCjNo, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", depth);
            swPart.ChangeDim("D3@草图1", exHeight);
            swPart.ChangeDim("D2@草图1", suHeight);
            swPart.ChangeDim("D1@阵列(线性)1", sidePanelDownCjNo);
            swPart.ChangeDim("D1@阵列(线性)2", sidePanelSideCjNo);
            if (exType == "V") swPart.ChangeDim("D4@草图1", 77d);
            else swPart.ChangeDim("D4@草图1", 150d);//W水洗
        }

        internal void FNHS0015(Component2 swComp, double depth, double exHeight, double suHeight, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", depth);
            swPart.ChangeDim("D3@Sketch1", exHeight);
            swPart.ChangeDim("D2@Sketch1", suHeight);
            if (exType == "V") swPart.ChangeDim("D4@Sketch1", 77d);
            else swPart.ChangeDim("D4@Sketch1", 150d);//W水洗
            if (suHeight == 400d)
            {
                swComp.UnSuppress("F400");
            }
            else
            {
                swComp.Suppress("F400");
            }
        }






        #endregion

        #region Supply新风腔模块
        //------------I型新风腔主体----------
        internal void FNHA0001(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int midRoofHoleNo, double midRoofSecondHoleDis, double midRoofTopHoleDis, string marvel, string sidePanel, string uvType, string bluetooth)
        {
            #region 基本尺寸
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D1@阵列(线性)1", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)1", frontPanelKaKouDis);
            swPart.ChangeDim("D3@Sketch3", midRoofSecondHoleDis);
            swPart.ChangeDim("D9@草图7", 200d - midRoofTopHoleDis);
            if (midRoofHoleNo == 1) swComp.Suppress("LPattern1");
            else
            {
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D1@LPattern1", midRoofHoleNo);
            }
            #endregion

            #region IR
            if (marvel == "YES") swComp.UnSuppress("IR-LHC-2");
            else swComp.Suppress("IR-LHC-2");
            #endregion

            #region UV HOOD
            if (uvType !="NO")
            {
                if (bluetooth == "YES") swComp.UnSuppress("SUCABLE-LEFT");
                else swComp.Suppress("SUCABLE-LEFT");
                if (sidePanel == "LEFT" || sidePanel == "BOTH") swComp.UnSuppress("JUNCTION BOX-LEFT");
                else swComp.Suppress("JUNCTION BOX-LEFT");
            }
            else
            {
                swComp.Suppress("SUCABLE-LEFT");
                if (marvel == "YES") swComp.UnSuppress("JUNCTION BOX-LEFT");
                else swComp.Suppress("JUNCTION BOX-LEFT");
            }
            #endregion
        }
        //----------I新风底部CJ孔板----------
        internal void FNHA0002(Component2 swComp, double length, int frontCjNo, double frontCjFirstDis, int frontPanelHoleNo, double frontPanelHoleDis, string bluetooth, string ledLogo, string waterCollection, string sidePanel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();

            #region 基本尺寸
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D1@CJHOLES", frontCjNo);
            swPart.ChangeDim("D10@草图8", frontCjFirstDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
            if (bluetooth == "YES") swComp.UnSuppress("BLUETOOTH");
            else swComp.Suppress("BLUETOOTH");
            if (ledLogo == "YES") swComp.UnSuppress("LOGO");
            else swComp.Suppress("LOGO");
            #endregion

            #region 集水翻边
            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "RIGHT")) swComp.UnSuppress("DRAINCHANNEL-RIGHT");
            else swComp.Suppress("DRAINCHANNEL-RIGHT");

            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "LEFT")) swComp.UnSuppress("DRAINCHANNEL-LEFT");
            else swComp.Suppress("DRAINCHANNEL-LEFT");
            #endregion
        }


        //----------I新风前面板----------
        public void FNHA0003(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 2d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }

        //------------F型新风腔主体----------
        public void FNHA0004(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo, double suDis, int suNo, string marvel, string sidePanel, string uvType, string bluetooth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D1@阵列(线性)1", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)1", frontPanelKaKouDis);
            swPart.ChangeDim("D3@Sketch3", midRoofSecondHoleDis);
            swPart.ChangeDim("D4@草图7", 200d - midRoofTopHoleDis);
            if (midRoofHoleNo == 1) swComp.Suppress("LPattern1");
            else
            {
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D1@LPattern1", midRoofHoleNo);
            }
            //新风脖颈
            swPart.ChangeDim("D3@Sketch6", suDis * (suNo / 2d - 1) + suDis / 2d);
            if (suNo == 1) swComp.Suppress("LPattern2");
            else
            {
                swComp.UnSuppress("LPattern2");
                swPart.ChangeDim("D1@LPattern2", suNo);
                swPart.ChangeDim("D3@LPattern2", suDis);
            }
            #region IR
            if (marvel == "YES") swComp.UnSuppress("IR-LHC-2");
            else swComp.Suppress("IR-LHC-2");
            #endregion
            if (uvType !="NO")
            {
                //UV HOOD
                if (bluetooth == "YES") swComp.UnSuppress("SUCABLE-LEFT");
                else swComp.Suppress("SUCABLE-LEFT");
                if (sidePanel == "LEFT" || sidePanel == "BOTH") swComp.UnSuppress("JUNCTION BOX-LEFT");
                else swComp.Suppress("JUNCTION BOX-LEFT");
            }
            else
            {
                //KV HOOD
                swComp.Suppress("SUCABLE-LEFT");
                if (marvel == "YES") swComp.UnSuppress("JUNCTION BOX-LEFT");
                else swComp.Suppress("JUNCTION BOX-LEFT");
            }
        }

        //----------F新风底部CJ孔板----------
        public void FNHA0005(Component2 swComp, double length, int frontCjNo, double frontCjFirstDis,
            int frontPanelHoleNo, double frontPanelHoleDis, string bluetooth, string ledLogo, string waterCollection,
            string sidePanel)
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
            #endregion

            #region 集水翻边
            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "RIGHT")) swComp.UnSuppress("DRAINCHANNEL-RIGHT");
            else swComp.Suppress("DRAINCHANNEL-RIGHT");

            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "LEFT")) swComp.UnSuppress("DRAINCHANNEL-LEFT");
            else swComp.Suppress("DRAINCHANNEL-LEFT");
            #endregion
        }

        //----------F镀锌隔板----------
        public void FNHA0006(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 6d);
        }


        //----------F新风前面板----------
        public void FNHA0007(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 2d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }

        //----------F新风滑门导轨----------
        public void FNHA0010(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@草图1", length - 200d);
        }
        public void FNHE0010(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 200d);
        }


        public void FNHA0040(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis,
           int midRoofHoleNo, double midRoofSecondHoleDis, double midRoofTopHoleDis, string marvel, int irNo,
           double irDis1, double irDis2, double irDis3, string UVType, string bluetooth, string sidePanel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            #region 基本尺寸
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D1@阵列(线性)1", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)1", frontPanelKaKouDis);
            swPart.ChangeDim("D3@Sketch4", midRoofSecondHoleDis);
            swPart.ChangeDim("D3@Sketch11", 200d - midRoofTopHoleDis);
            if (midRoofHoleNo == 1) swComp.Suppress("LPattern1");
            else
            {
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D1@LPattern1", midRoofHoleNo);
            }
            #endregion

            #region MARVEL
            swComp.Suppress("MA1");
            swComp.Suppress("MA2");
            swComp.Suppress("MA3");
            swComp.Suppress("MACABLE1");
            swComp.Suppress("MACABLE2");
            swComp.Suppress("MACABLE3");
            if (marvel == "YES")
            {
                if (irNo > 0)
                {
                    swComp.UnSuppress("MA1");
                    swPart.ChangeDim("D3@Sketch16", irDis1);
                    swComp.UnSuppress("MACABLE1");
                    swPart.ChangeDim("D2@Sketch19", irDis1);
                }
                if (irNo > 1)
                {
                    swComp.UnSuppress("MA2");
                    swPart.ChangeDim("D3@Sketch17", irDis2);
                    swComp.UnSuppress("MACABLE2");
                    swPart.ChangeDim("D2@Sketch20", irDis2);
                }
                if (irNo > 2)
                {
                    swComp.UnSuppress("MA3");
                    swPart.ChangeDim("D3@Sketch18", irDis3);
                    swComp.UnSuppress("MACABLE3");
                    swPart.ChangeDim("D2@Sketch21", irDis3);
                }
            }
            #endregion

            #region UV HOOD
            if (UVType == "LONG" || UVType == "SHORT")
            {
                if (bluetooth == "YES") swComp.UnSuppress("SUCABLE-LEFT");
                else swComp.Suppress("SUCABLE-LEFT");
                if (sidePanel == "LEFT" || sidePanel == "BOTH") swComp.UnSuppress("JUNCTION BOX-LEFT");
                else swComp.Suppress("JUNCTION BOX-LEFT");
            }
            else
            {
                swComp.Suppress("SUCABLE-LEFT");
                if (marvel == "YES") swComp.UnSuppress("JUNCTION BOX-LEFT");
                else swComp.Suppress("JUNCTION BOX-LEFT");
            }
            #endregion
        }


        public void FNHA0041(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 2d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }

        //----------蓝牙----------
        internal void Bluetooth(AssemblyDoc swAssy, string suffix, string bluetooth, string bluetoothPart)
        {
            if (bluetooth == "YES") swAssy.UnSuppress(suffix, bluetoothPart);
            else swAssy.Suppress(suffix, bluetoothPart);
        }

        //----------LOGO----------
        internal void LedLogo(AssemblyDoc swAssy, string suffix, string ledlogo, string ledLogoPart, string ledLogoSupport)
        {
            if (ledlogo == "YES")
            {
                swAssy.UnSuppress(suffix, ledLogoPart);
                swAssy.UnSuppress(suffix, ledLogoSupport);
            }
            else
            {
                swAssy.Suppress(suffix, ledLogoPart);
                swAssy.UnSuppress(suffix, ledLogoSupport);
            }
        }


        //----------圆环新风----------
        public void FNHA0048(Component2 swComp, double depth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", depth / 2d - 1d);
            swPart.ChangeDim("D3@Sketch1", depth / 2d - 3d);
        }

        public void FNHA0049(Component2 swComp, double depth, double innerCjFirstDis, int innerCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", depth / 2d - 90d + 1d);
            swPart.ChangeDim("D3@Sketch1", depth / 2d - 90d + 1d+0.5d-400d);
            swPart.ChangeDim("D3@Sketch6", innerCjFirstDis);
            swPart.ChangeDim("D1@LPattern1", innerCjNo);
        }

        public void FNHA0050(Component2 swComp, double depth, double downCjFirstDis, int downCjNo,double beta)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", depth / 2d - 1d);
            swPart.ChangeDim("D4@Sketch1", depth / 2d - 90d +1d);

            swPart.ChangeDim("D3@Sketch2", depth - 90d);
            swPart.ChangeDim("D2@Sketch2", downCjFirstDis);
            swPart.ChangeDim("D1@CirPattern1", downCjNo);
            swPart.ChangeDim("D3@CirPattern1", (beta*180)/Math.PI);
        }

        public void FNHA0051(Component2 swComp, double depth, double exBeamLength)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D6@Sketch1", depth / 2d - 1d);
            swPart.ChangeDim("D3@Sketch1", depth / 2d - 90d +1d);
            swPart.ChangeDim("D2@Sketch3", depth / 2d - 45d);
            swPart.ChangeDim("D1@Sketch3", exBeamLength - 50d);
        }


        #endregion
    }
}
