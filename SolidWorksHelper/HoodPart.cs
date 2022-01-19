using SolidWorks.Interop.sldworks;

namespace SolidWorksHelper
{
    public class HoodPart
    {
        #region 排风腔
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
                if (anDetector == "RIGHT" || anDetector == "BOTH")
                {
                    swComp.UnSuppress("ANDTEC-RIGHT");
                }
                if (anDetector == "LEFT" || anDetector == "BOTH")
                {
                    swComp.UnSuppress("ANDTEC-LEFT");
                }
                else
                {
                    swComp.Suppress("ANDTEC-RIGHT");
                    swComp.Suppress("ANDTEC-LEFT");
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
            if (ksaSideLength < 12d / 1000d && ksaSideLength > 2d / 1000d)
            {
                swAssy.Suppress(suffix, leftPart);
                swAssy.Suppress(suffix, rightPart);
                swComp = swAssy.UnSuppress(suffix, specialPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D1@草图1", ksaSideLength * 2d);
            }
            else if (ksaSideLength < 25d / 1000d && ksaSideLength >= 12d / 1000d)
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
                else if (meshSideLength <= 40d / 1000d && meshSideLength > 1.5d / 1000d)
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





        #endregion

        #region MidRoof
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

        public void FNHM0001(Component2 swComp, string exType, double length, double deepth, double exHeight, double suHeight, double exRightDis, double midRoofTopHoleDis, double midRoofSecondHoleDis, int midRoofHoleNo, string lightType, double lightYDis, int ledSpotNo, double ledSpotDis, string ansul, int anDropNo, double anYDis, double anDropDis1, double anDropDis2, double anDropDis3, double anDropDis4, double anDropDis5, string anDetectorEnd, int anDetectorNo, double anDetectorDis1, double anDetectorDis2, double anDetectorDis3, double anDetectorDis4, double anDetectorDis5, string bluetooth, string uvType, string marvel, int irNo, double irDis1, double irDis2, double irDis3)
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
                            swPart.ChangeDim("D2@Sketch33",195d);
                        else swPart.ChangeDim("D2@Sketch33",175d);
                    }
                    if (anDetectorNo > 3)
                    {
                        swComp.UnSuppress("ANDTEC4");
                        swPart.ChangeDim("D1@Sketch34",anDetectorDis4);
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 4) 
                            swPart.ChangeDim("D2@Sketch34",195d);
                        else swPart.ChangeDim("D2@Sketch34",175d);
                    }
                    if (anDetectorNo > 4)
                    {
                        swComp.UnSuppress("ANDTEC5");
                        swPart.ChangeDim("D1@Sketch35",anDetectorDis5);
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 5)
                            swPart.ChangeDim("D2@Sketch35",195d);
                        else swPart.ChangeDim("D2@Sketch35",175d);
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

            //开方孔，UV或待MARVEL时解压
            if (exType == "UV" || exType == "UW")
            {
                swFeat = swComp.FeatureByName("CUT-BACK-LEFT");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("CUT-BACK-RIGHT");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
            }
            else
            {
                swFeat = swComp.FeatureByName("CUT-BACK-LEFT");
                if (marvel == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("CUT-BACK-RIGHT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            }
            swFeat = swComp.FeatureByName("CUT-FRONT-RIGHT");
            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩

            if (bluetooth == "YES")
            {
                swFeat = swComp.FeatureByName("CUT-FRONT-LEFT");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
            }
            else
            {
                swFeat = swComp.FeatureByName("CUT-FRONT-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            }
            //UV灯线缆穿孔
            if (uvType == "LONG")
            {
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                swPart.ChangeDim("D4@草图28").SystemValue = exRightDis / 1000d;
                swPart.ChangeDim("D3@草图28").SystemValue = 1500d / 1000d;
            }
            else if (uvType == "SHORT")
            {
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                swPart.ChangeDim("D4@草图28").SystemValue = exRightDis / 1000d;
                swPart.ChangeDim("D3@草图28").SystemValue = 790d / 1000d;
            }
            else
            {
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            }

            //400新风腔IR安装孔
            if (marvel == "YES" && suHeight != 555d)
            {
                if (irNo > 0)
                {
                    swFeat = swComp.FeatureByName("MAINS1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.ChangeDim("D1@Sketch37").SystemValue = irDis1 / 1000d;
                }
                if (irNo > 1)
                {
                    swFeat = swComp.FeatureByName("MAINS2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.ChangeDim("D1@Sketch38").SystemValue = irDis2 / 1000d;
                }
                if (irNo > 2)
                {
                    swFeat = swComp.FeatureByName("MAINS3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.ChangeDim("D1@Sketch39").SystemValue = irDis3 / 1000d;
                }
            }
            else
            {
                swFeat = swComp.FeatureByName("MAINS1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MAINS2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MAINS3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            //CMOD NTC Sensor
            swFeat = swComp.FeatureByName("NTC Sensor");
            if (exType == "CMOD") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
            else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
        }



        #endregion


    }
}
