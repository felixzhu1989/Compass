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
            FNHE0001(swComp,length,midRoofHoleNo,midRoofSecondHoleDis,exNo,exRightDis,exLength,exWidth,exDis,waterCollection,sidePanel,outlet,backToBack,ansul,anSide,anDetector,marvel, uvType);


        }







        //排风腔
        internal void FNHE0001(Component2 swComp, double length, int midRoofHoleNo, double midRoofSecondHoleDis, int exNo, double exRightDis, double exLength, double exWidth, double exDis, string waterCollection, string sidePanel, string outlet, string backToBack, string ansul, string anSide, string anDetector, string marvel, string UVType)
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
            if (UVType == "LONG")
            {
                swComp.UnSuppress("UVRACK");
                swPart.ChangeDim("D6@Sketch12", exRightDis);
                swPart.ChangeDim("D5@Sketch12", 1640d);
                swComp.UnSuppress("UVCABLE");
                swPart.ChangeDim("D4@Sketch11", exRightDis);
                swPart.ChangeDim("D1@Sketch11", 1500d);
            }
            else if (UVType == "SHORT")
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

        internal void ExaustRail(AssemblyDoc swAssy, string suffix, string marvel, double exLength, double exWidth,
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

        internal void ExaustSpigot(AssemblyDoc swAssy, string suffix, string ansul, string marvel, double exLength,
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

        internal void ExaustSide(AssemblyDoc swAssy, string suffix, string ansul, string sidePanel, string leftPart, string rightPart)
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

        internal void UVLightDoor(AssemblyDoc swAssy, string suffix, string UVType, string doorLong, string uvLong, string doorShort, string uvShort)
        {
            if (UVType == "LONG")
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

        internal void MeshFilter(AssemblyDoc swAssy, string suffix, double meshSideLength, string ansul, string anSide, string leftPart, string rightPart)
        {
            Component2 swComp;
            ModelDoc2 swPart;
            if (ansul == "YES")
            {
                if (meshSideLength * 2d < 57d) meshSideLength += 249d;
                if ((meshSideLength - 20d) > 57d)
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
                    else if (anSide == "RIGHT")
                    {
                        swAssy.Suppress(suffix, leftPart);
                        swComp = swAssy.UnSuppress(suffix, rightPart);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength * 2d);
                        swComp.UnSuppress("ANSUL");
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
                if (2 * meshSideLength < 15d && meshSideLength > 1.5d)
                    meshSideLength += 249d;
                if (meshSideLength > 40d)
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
                else if (meshSideLength <= 40d && meshSideLength > 1.5d)
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

        public void Std2900100001(Component2 swComp, string ansul, double deepth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            if (ansul == "YES") swPart.ChangeDim("D2@基体-法兰1", deepth - 250);
            else swPart.ChangeDim("D2@基体-法兰1", deepth - 100d);
        }


        #endregion

        #region MidRoof模块
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



        internal void FNHM0001(Component2 swComp, string exType, double length, double deepth, double exHeight, double suHeight, double exRightDis, double midRoofTopHoleDis, double midRoofSecondHoleDis, int midRoofHoleNo, string lightType, double lightYDis, int ledSpotNo, double ledSpotDis, string ansul, int anDropNo, double anYDis, double anDropDis1, double anDropDis2, double anDropDis3, double anDropDis4, double anDropDis5, string anDetectorEnd, int anDetectorNo, double anDetectorDis1, double anDetectorDis2, double anDetectorDis3, double anDetectorDis4, double anDetectorDis5, string bluetooth, string uvType, string marvel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();

            #region 基本尺寸
            swPart.ChangeDim("D1@草图1", length - 4d);
            swPart.ChangeDim("D2@草图1", deepth - 669d);
            swPart.ChangeDim("D1@草图6", deepth - 896d);
            swPart.ChangeDim("D3@草图25", midRoofTopHoleDis);
            swPart.ChangeDim("D2@草图26", (deepth - 840d) / 3d);
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

            #region 400新风腔IR安装孔
            swComp.Suppress("MAINS1");
            swComp.Suppress("MAINS2");
            swComp.Suppress("MAINS3");
            if (marvel == "YES" && suHeight != 555d)
            {

            }
            #endregion

            //CMOD NTC Sensor
            if (exType == "CMOD") swComp.UnSuppress("NTC Sensor");
            else swComp.Suppress("NTC Sensor");
        }

        internal void FNHM0006(Component2 swComp, double deepth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", deepth - 898d);
        }




        #endregion

        #region SidePanel 普通烟罩大侧板模块

        internal void SidePanel(AssemblyDoc swAssy, string suffix, string sidePanel, double deepth, double exHeight, string waterCollection, string exType, string leftOuter, string leftInner, string rightOuter, string rightInner, string leftCollection, string rightCollection)
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
                FNHS0001(swComp, deepth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0002(swComp, deepth, exHeight);
                //RIGHT
                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0003(swComp, deepth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0004(swComp, deepth, exHeight);
            }
            else if (sidePanel == "LEFT")
            {
                swAssy.Suppress(suffix, rightOuter);
                swAssy.Suppress(suffix, rightInner);

                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FNHS0001(swComp, deepth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0002(swComp, deepth, exHeight);
            }
            else if (sidePanel == "RIGHT")
            {
                swAssy.Suppress(suffix, leftOuter);
                swAssy.Suppress(suffix, leftInner);

                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0003(swComp, deepth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0004(swComp, deepth, exHeight);
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
                FNHS0005(swComp, deepth, exHeight, exHeight, exType);//普通烟罩"V"，水洗"W"
            }
            else
            {
                swAssy.Suppress(suffix, leftCollection);
            }
            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "RIGHT"))
            {
                swComp = swAssy.UnSuppress(suffix, rightCollection);
                FNHS0006(swComp, deepth, exHeight, exHeight, exType);//普通烟罩"V"，水洗"W"
            }
            else
            {
                swAssy.Suppress(suffix, rightCollection);
            }
            #endregion
        }

        internal void FNHS0001(Component2 swComp, double deepth, double exHeight, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", deepth);
            swPart.ChangeDim("D2@草图1", exHeight);
            swPart.ChangeDim("D1@阵列(线性)1", sidePanelSideCjNo);
            swPart.ChangeDim("D1@阵列(线性)2", sidePanelDownCjNo);
        }
        internal void FNHS0002(Component2 swComp, double deepth, double exHeight)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", deepth - 79d);
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
        internal void FNHS0003(Component2 swComp, double deepth, double exHeight, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", deepth);
            swPart.ChangeDim("D2@草图1", exHeight);
            swPart.ChangeDim("D1@阵列(线性)1", sidePanelSideCjNo);
            swPart.ChangeDim("D1@阵列(线性)2", sidePanelDownCjNo);
        }
        internal void FNHS0004(Component2 swComp, double deepth, double exHeight)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", deepth - 79d);
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
        internal void FNHS0005(Component2 swComp, double deepth, double exHeight, double suHeight, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", deepth);

            swPart.ChangeDim("D3@Sketch11", deepth);

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
        internal void FNHS0006(Component2 swComp, double deepth, double exHeight, double suHeight, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", deepth);

            swPart.ChangeDim("D3@Sketch11", deepth);

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

        internal void SidePanelSpecial(AssemblyDoc swAssy, string suffix, string sidePanel, double deepth, double exHeight, double suHeight, string waterCollection, string exType, string leftOuter, string leftInner, string rightOuter, string rightInner, string leftCollection, string rightCollection)
        {
            //UVF555400斜侧板CJ孔计算,77为排风底部长度，555-400为高度差
            int sidePanelDownCjNo = (int)((Math.Sqrt(Math.Pow(deepth - 77d, 2) + Math.Pow(exHeight - suHeight, 2)) - 95d) / 32d);
            if (exType == "W") sidePanelDownCjNo = (int)((Math.Sqrt(Math.Pow(deepth - 150d, 2) + Math.Pow(555d - 400d, 2)) - 95d) / 32d);
            int sidePanelSideCjNo = sidePanelDownCjNo - 3;
            Component2 swComp;
            #region 大侧板
            if (sidePanel == "BOTH")
            {
                //LEFT
                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FNHS0012(swComp, deepth, exHeight, suHeight, sidePanelSideCjNo, sidePanelDownCjNo, exType);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0013(swComp, deepth, exHeight, suHeight, exType);
                //RIGHT
                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0014(swComp, deepth, exHeight, suHeight, sidePanelSideCjNo, sidePanelDownCjNo, exType);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0015(swComp, deepth, exHeight, suHeight, exType);
            }
            else if (sidePanel == "LEFT")
            {
                swAssy.Suppress(suffix, rightOuter);
                swAssy.Suppress(suffix, rightInner);

                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FNHS0012(swComp, deepth, exHeight, suHeight, sidePanelSideCjNo, sidePanelDownCjNo, exType);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0013(swComp, deepth, exHeight, suHeight, exType);
            }
            else if (sidePanel == "RIGHT")
            {
                swAssy.Suppress(suffix, leftOuter);
                swAssy.Suppress(suffix, leftInner);

                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0014(swComp, deepth, exHeight, suHeight, sidePanelSideCjNo, sidePanelDownCjNo, exType);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0015(swComp, deepth, exHeight, suHeight, exType);
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
                FNHS0005(swComp, deepth, exHeight, suHeight, exType);//普通烟罩"V"，水洗"W"
            }
            else
            {
                swAssy.Suppress(suffix, leftCollection);
            }
            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "RIGHT"))
            {
                swComp = swAssy.UnSuppress(suffix, rightCollection);
                FNHS0006(swComp, deepth, exHeight, suHeight, exType);//普通烟罩"V"，水洗"W"
            }
            else
            {
                swAssy.Suppress(suffix, rightCollection);
            }
            #endregion
        }

        internal void FNHS0012(Component2 swComp, double deepth, double exHeight, double suHeight, int sidePanelSideCjNo,
            int sidePanelDownCjNo, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", deepth);
            swPart.ChangeDim("D3@草图1", exHeight);
            swPart.ChangeDim("D2@草图1", suHeight);
            swPart.ChangeDim("D1@阵列(线性)1", sidePanelDownCjNo);
            swPart.ChangeDim("D1@阵列(线性)2", sidePanelSideCjNo);
            if (exType == "V") swPart.ChangeDim("D4@草图1", 77d);
            else swPart.ChangeDim("D4@草图1", 150d);//W水洗
        }

        internal void FNHS0013(Component2 swComp, double deepth, double exHeight, double suHeight, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", deepth);
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

        internal void FNHS0014(Component2 swComp, double deepth, double exHeight, double suHeight, int sidePanelSideCjNo,
            int sidePanelDownCjNo, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", deepth);
            swPart.ChangeDim("D3@草图1", exHeight);
            swPart.ChangeDim("D2@草图1", suHeight);
            swPart.ChangeDim("D1@阵列(线性)1", sidePanelDownCjNo);
            swPart.ChangeDim("D1@阵列(线性)2", sidePanelSideCjNo);
            if (exType == "V") swPart.ChangeDim("D4@草图1", 77d);
            else swPart.ChangeDim("D4@草图1", 150d);//W水洗
        }

        internal void FNHS0015(Component2 swComp, double deepth, double exHeight, double suHeight, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", deepth);
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
        //I555型新风腔体
        internal void FNHA0001(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int midRoofHoleNo, double midRoofSecondHoleDis, double midRoofTopHoleDis, string marvel, string UVType, string bluetooth, string sidePanel)
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

            #region MARVEL
            swComp.Suppress("MA1");
            swComp.Suppress("MA2");
            swComp.Suppress("MA3");
            swComp.Suppress("MACABLE1");
            swComp.Suppress("MACABLE2");
            swComp.Suppress("MACABLE3");
            if (marvel == "YES")
            {

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
        //新风底部CJ板
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


        //I555型前面板
        public void FNHA0003(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 2d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }

        //F555新风腔体
        public void FNHA0004(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo, double suDis, int suNo, string MARVEL, string sidePanel, string exType, string bluetooth)
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
            //MARVEL
            if (MARVEL == "YES")
            {

            }
            else
            {

            }
            if (exType == "UV") //"UV"/"KV"
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
                if (MARVEL == "YES") swComp.UnSuppress("JUNCTION BOX-LEFT");
                else swComp.Suppress("JUNCTION BOX-LEFT");
            }
        }

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

        //F555镀锌隔板
        public void FNHA0006(Component2 swComp, double length, string marvel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 6d);
            #region MARVEL
            if (marvel == "YES")
            {

            }
            else
            {

            }
            #endregion
        }


        //F555型网孔板
        public void FNHA0007(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 2d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }
        //新风滑门轨道
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


        internal void BluetoothLogo(AssemblyDoc swAssy, string suffix, string bluetooth, string ledlogo, string bluetoothPart, string ledLogoPart, string ledLogoSupport)
        {
            if (bluetooth == "YES") swAssy.UnSuppress(suffix, bluetoothPart);
            else swAssy.Suppress(suffix, bluetoothPart);
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









        #endregion
    }
}
