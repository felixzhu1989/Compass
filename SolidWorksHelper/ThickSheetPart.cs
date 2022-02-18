using SolidWorks.Interop.sldworks;

namespace SolidWorksHelper
{
    //板厚1.2
    public class ThickSheetPart
    {

        #region KVX555TP，中东项目，板厚1.2
        //排风腔
        internal void FSHE0008(Component2 swComp, double length, int midRoofHoleNo, double midRoofSecondHoleDis, int exNo, double exRightDis, double exLength, double exWidth, double exDis, string waterCollection, string sidePanel, string outlet, string backToBack, string ansul, string anSide, string anDetector, string marvel, string UVType)
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
        //排风腔前面板
        internal void FSHE0009(Component2 swComp, double length, string UVType, double exRightDis)
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

        internal void FSHM0002(Component2 swComp, string exType, double length, double deepth, double exHeight, double suHeight, double exRightDis, double midRoofTopHoleDis, double midRoofSecondHoleDis, int midRoofHoleNo, string lightType, double lightYDis, int ledSpotNo, double ledSpotDis, string ansul, int anDropNo, double anYDis, double anDropDis1, double anDropDis2, double anDropDis3, double anDropDis4, double anDropDis5, string anDetectorEnd, int anDetectorNo, double anDetectorDis1, double anDetectorDis2, double anDetectorDis3, double anDetectorDis4, double anDetectorDis5, string bluetooth, string uvType, string marvel, int irNo, double irDis1, double irDis2, double irDis3)
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
                if (irNo > 0)
                {
                    swComp.UnSuppress("MAINS1");
                    swPart.ChangeDim("D1@Sketch37", irDis1);
                }
                if (irNo > 1)
                {
                    swComp.UnSuppress("MAINS2");
                    swPart.ChangeDim("D1@Sketch38", irDis2);
                }
                if (irNo > 2)
                {
                    swComp.FeatureByName("MAINS3");
                    swPart.ChangeDim("D1@Sketch39", irDis3);
                }
            }
            #endregion

            //CMOD NTC Sensor
            if (exType == "CMOD") swComp.UnSuppress("NTC Sensor");
            else swComp.Suppress("NTC Sensor");
        }

        internal void KvxSidePanel(AssemblyDoc swAssy, string suffix, string sidePanel, double deepth, double exHeight, string waterCollection, string exType, string leftOuter, string leftInner, string rightOuter, string rightInner, string leftCollection, string rightCollection)
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
                FSHS0001(swComp, deepth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FSHS0002(swComp, deepth, exHeight);
                //RIGHT
                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FSHS0003(swComp, deepth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FSHS0004(swComp, deepth, exHeight);
            }
            else if (sidePanel == "LEFT")
            {
                swAssy.Suppress(suffix, rightOuter);
                swAssy.Suppress(suffix, rightInner);

                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FSHS0001(swComp, deepth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FSHS0002(swComp, deepth, exHeight);
            }
            else if (sidePanel == "RIGHT")
            {
                swAssy.Suppress(suffix, leftOuter);
                swAssy.Suppress(suffix, leftInner);

                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FSHS0003(swComp, deepth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FSHS0004(swComp, deepth, exHeight);
            }
            else
            {
                swAssy.Suppress(suffix, leftOuter);
                swAssy.Suppress(suffix, leftInner);
                swAssy.Suppress(suffix, rightOuter);
                swAssy.Suppress(suffix, rightInner);
            }
            #endregion

            //#region 集水翻边
            //if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "LEFT"))
            //{
            //    swComp = swAssy.UnSuppress(suffix, leftCollection);
            //    FNHS0005(swComp, deepth, exHeight, exHeight, exType);//普通烟罩"V"，水洗"W"
            //}
            //else
            //{
            //    swAssy.Suppress(suffix, leftCollection);
            //}
            //if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "RIGHT"))
            //{
            //    swComp = swAssy.UnSuppress(suffix, rightCollection);
            //    FNHS0006(swComp, deepth, exHeight, exHeight, exType);//普通烟罩"V"，水洗"W"
            //}
            //else
            //{
            //    swAssy.Suppress(suffix, rightCollection);
            //}
            //#endregion
        }

        internal void FSHS0001(Component2 swComp, double deepth, double exHeight, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", deepth);
            swPart.ChangeDim("D2@草图1", exHeight);
            swPart.ChangeDim("D1@阵列(线性)1", sidePanelSideCjNo);
            swPart.ChangeDim("D1@阵列(线性)2", sidePanelDownCjNo);
        }
        internal void FSHS0002(Component2 swComp, double deepth, double exHeight)
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
        internal void FSHS0003(Component2 swComp, double deepth, double exHeight, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", deepth);
            swPart.ChangeDim("D2@草图1", exHeight);
            swPart.ChangeDim("D1@阵列(线性)1", sidePanelSideCjNo);
            swPart.ChangeDim("D1@阵列(线性)2", sidePanelDownCjNo);
        }
        internal void FSHS0004(Component2 swComp, double deepth, double exHeight)
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

        #endregion

        #region CMODI700,中东项目， 板厚1.2
        //CMOD腔体背面
        internal void FSHE0013(Component2 swComp, double length, string outlet, string backToBack)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2(); //打开零件3
            swPart.ChangeDim("D2@Base-Flange1", length);

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

            #endregion

            //背靠背
            if (backToBack == "YES") swComp.UnSuppress("BACKTOBACK");
            else swComp.Suppress("BACKTOBACK");
        }
        //CMOD腔体顶面
        internal void FSHE0014(Component2 swComp, double length, int midRoofHoleNo, double midRoofSecondHoleDis, double exLength, double exWidth, string outlet, string inlet, string ansul, string anSide,string anDetector)
        {
            #region 基本尺寸
            ModelDoc2 swPart = swComp.GetModelDoc2(); //打开零件3
            swPart.ChangeDim("D2@Base-Flange1", length);
            swPart.ChangeDim("D2@Sketch9", midRoofSecondHoleDis);
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
            //if (item.ExNo == 1)
            //{
            swComp.UnSuppress("EXCOONE");
            //swFeat = swComp.Suppress("EXCOTWO");
            //swPart.ChangeDim("D4@Sketch11",exRightDis);
            swPart.ChangeDim("D2@Sketch4", exLength);
            swPart.ChangeDim("D3@Sketch4", exWidth);
            //}
            //else
            //{
            //    swComp.Suppress("EXCOONE");    
            //    swComp.UnSuppress("EXCOTWO");   
            //    swPart.ChangeDim("D5@Sketch12",exRightDis);
            //    swPart.ChangeDim("D4@Sketch12",exDis);
            //    swPart.ChangeDim("D1@Sketch12",exLength);
            //    swPart.ChangeDim("D2@Sketch12",exWidth);
            //} 
            #endregion

            #region 上排水
            if (outlet == "UPLEFT")
            {
                swComp.UnSuppress("AUTODRAIN LEFT");
                swComp.Suppress("AUTODRAIN RIGHT");
            }
            else if (outlet == "UPRIGHT")
            {
                swComp.Suppress("AUTODRAIN LEFT");
                swComp.UnSuppress("AUTODRAIN RIGHT");
            }
            else
            {
                swComp.Suppress("AUTODRAIN LEFT");
                swComp.Suppress("AUTODRAIN RIGHT");
            }
            #endregion

            #region 进水口
            if (inlet == "LEFT")
            {
                swComp.UnSuppress("DRWATER INLET-L");
                swComp.Suppress("DRWATER INLET-R");
            }
            else if (inlet == "RIGHT")
            {
                swComp.Suppress("DRWATER INLET-L");
                swComp.UnSuppress("DRWATER INLET-R");
            }
            else
            {
                swComp.Suppress("DRWATER INLET-L");
                swComp.Suppress("DRWATER INLET-R");
            }
            #endregion

            #region ANSUL
            if (ansul == "YES")
            {
                //侧喷
                if (anSide == "LEFT")
                {
                    swComp.UnSuppress("ANSUL-LEFT");
                    swComp.Suppress("ANSUL-RIGHT");
                }
                else if (anSide == "RIGHT")
                {
                    swComp.Suppress("ANSUL-LEFT");
                    swComp.UnSuppress("ANSUL-RIGHT");
                }
                else
                {
                    swComp.Suppress("ANSUL-LEFT");
                    swComp.Suppress("ANSUL-RIGHT");
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
        }

        internal void ExaustSide(AssemblyDoc swAssy, string suffix, string ansul, string sidePanel, string leftPart,
            string rightPart)
        {
            Component2 swComp;
            swComp = swAssy.GetComponentByNameWithSuffix(suffix, leftPart);
            if (ansul == "YES" && (sidePanel == "MIDDLE"|| sidePanel == "LEFT")) swComp.UnSuppress("ANDTEC");
            else swComp.Suppress("ANDTEC");
            swComp = swAssy.GetComponentByNameWithSuffix(suffix, rightPart);
            if (ansul == "YES" && (sidePanel == "MIDDLE" || sidePanel == "RIGHT")) swComp.UnSuppress("ANDTEC");
            else swComp.Suppress("ANDTEC");
        }

        internal void FSHE0018(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", length);
        }
        internal void FSHE0017(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", length - 9d);
        }
        internal void FSHE0031(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", length - 10d);
        }
        //排风脖颈
        internal void FSHE0033(Component2 swComp,double exLength,double exHeight )
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1",exLength);
            swPart.ChangeDim("D2@Sketch1",exHeight);
            //if (ansul == "YES")  swComp.UnSuppress("ANSUL");
            //else  swComp.Suppress("ANSUL");
        }
        
        internal void FSHE0032(Component2 swComp,double exWidth,double exHeight)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1" ,exWidth - 2.5d);
            swPart.ChangeDim("D2@Sketch1",exHeight);
        }

        //水洗挡板
        internal void FSHE0022(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", (length - 120d)/2d);
        }
        internal void FSHE0020(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", (length - 170d) / 2d);
        }
        internal void FSHE0021(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", (length - 200d) / 2d);
        }

        //I型新风
        //I700型新风腔体
        internal void FSHA0006(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int midRoofHoleNo, double midRoofSecondHoleDis, double midRoofTopHoleDis, string marvel, int irNo, double irDis1, double irDis2, double irDis3, string UVType, string bluetooth, string sidePanel)
        {
            #region 基本尺寸
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D1@阵列(线性)1", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)1", frontPanelKaKouDis);
            swPart.ChangeDim("D3@Sketch3", midRoofSecondHoleDis);
            //swPart.ChangeDim("D9@草图7", 200d - midRoofTopHoleDis);
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
                    swPart.ChangeDim("D3@Sketch14", irDis1);
                    swComp.UnSuppress("MACABLE1");
                    swPart.ChangeDim("D1@Sketch17", irDis1);
                }
                if (irNo > 1)
                {
                    swComp.UnSuppress("MA2");
                    swPart.ChangeDim("D3@Sketch15", irDis2);
                    swComp.UnSuppress("MACABLE2");
                    swPart.ChangeDim("D1@Sketch18", irDis2);
                }
                if (irNo > 2)
                {
                    swComp.UnSuppress("MA3");
                    swPart.ChangeDim("D3@Sketch16", irDis3);
                    swComp.UnSuppress("MACABLE3");
                    swPart.ChangeDim("D1@Sketch19", irDis3);
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

        //新风底部CJ板
        internal void FSHA0007(Component2 swComp, double length, int frontCjNo, double frontCjFirstDis, int frontPanelHoleNo, double frontPanelHoleDis, string bluetooth, string ledLogo, string waterCollection, string sidePanel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();

            #region 基本尺寸
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D1@CJHOLES", frontCjNo);
            swPart.ChangeDim("D10@草图8", frontCjFirstDis);
            swPart.ChangeDim("D1@LPattern2", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern2", frontPanelHoleDis);
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


        //I700型前面板
        public void FSHA0008(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 2d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern2", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern2", frontPanelHoleDis);
        }

        #endregion


        #region F700新风
        public void FSHA0011(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo, double suDis, int suNo, string MARVEL, int IRNo, double IRDis1, double IRDis2, double IRDis3, string sidePanel, string exType, string bluetooth)
        {
           ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1",length);
            swPart.ChangeDim("D1@阵列(线性)1",frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)1",frontPanelKaKouDis);
            swPart.ChangeDim("D3@Sketch3",midRoofSecondHoleDis);
            //swPart.ChangeDim("D4@草图7",200dd- midRoofTopHoleDis);
            if (midRoofHoleNo == 1) swComp.Suppress("LPattern1");
            else
            {
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D1@LPattern1",midRoofHoleNo);
            }
            //新风脖颈
            swPart.ChangeDim("D3@Sketch6",suDis * (suNo / 2d - 1) + suDis / 2d);
            if (suNo == 1) swComp.Suppress("LPattern2");
            else
            {
                swComp.UnSuppress("LPattern2");
                swPart.ChangeDim("D1@LPattern2",suNo);
                swPart.ChangeDim("D3@LPattern2",suDis);
            }
            //MARVEL
            if (MARVEL == "YES")
            {
                swComp.UnSuppress("Cut-Extrude10");
            }
            else
            {
                swComp.Suppress("Cut-Extrude10");
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
        //新风底部CJ板
        internal void FSHA0010(Component2 swComp, double length, int frontCjNo, double frontCjFirstDis, int frontPanelHoleNo, double frontPanelHoleDis, string bluetooth, string ledLogo, string waterCollection, string sidePanel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();

            #region 基本尺寸
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D1@CJHOLES", frontCjNo);
            swPart.ChangeDim("D10@草图8", frontCjFirstDis);
            swPart.ChangeDim("D1@LPattern2", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern2", frontPanelHoleDis);
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

        //F700型前面板
        public void FSHA0012(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 2d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern2", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern2", frontPanelHoleDis);
        }
        //镀锌隔板
        public void FSHA0013(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 6d);
        }

        #endregion

    }
}
