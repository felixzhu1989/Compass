using System;
using Models;
using SolidWorks.Interop.sldworks;

namespace SolidWorksHelper.CeilingAutoDrawing
{
    internal class CeilingPart
    {
        #region 通用方法
        //重命名通用方法
        public Component2 RenameComp(ModelDoc2 swModel, AssemblyDoc swAssy, string suffix, string type, string module, string compName, int num, double length, double width)
        {
            string assyName = swModel.GetTitle().Substring(0, swModel.GetTitle().Length - 7);
            string originPath = $"{CommonFunc.AddSuffix(suffix, $"{compName}-{num}")}@{assyName}";
            string strRename = $"{compName}[{type}-{module}]{{{(int)length}}}({(int)width})";
            bool status = swModel.Extension.SelectByID2(originPath, "COMPONENT", 0, 0, 0, false, 0, null, 0);
            if (status)
            {
                swAssy.UnSuppress(suffix, $"{compName}-{num}");
                swModel.Extension.SelectByID2(originPath, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                swModel.Extension.RenameDocument(strRename);
            }
            swModel.ClearSelection2(true);
            status = swModel.Extension.SelectByID2($"{strRename}-{num}@{assyName}", "COMPONENT", 0, 0, 0, false,
                0, null, 0);
            swModel.ClearSelection2(true);
            if (status) return swAssy.GetComponentByName($"{strRename}-{num}");
            return null;
        }

        public void SuppressIfExist(ModelDoc2 swModel, AssemblyDoc swAssy, string suffix, string part, int num)
        {
            string assyName = swModel.GetTitle().Substring(0, swModel.GetTitle().Length - 7);
            bool status = swModel.Extension.SelectByID2($"{CommonFunc.AddSuffix(suffix, $"{part}-{num}")}@{assyName}", "COMPONENT", 0, 0, 0, false, 0, null, 0);
            if (status) swAssy.Suppress(suffix, $"{part}-{num}");
        }
        #endregion

        #region 排风腔公有
        //排风滑门
        internal void ExaustRail(AssemblyDoc swAssy, string suffix, string marvel, double exLength, double exWidth,
            int exNo, double exDis, string subAssy)
        {
            string doorPart1 = "FNCE0013-1";
            string doorPart2 = "FNCE0013-5";
            string railPart1 = "FNCE0018-1";
            string railPart2 = "FNCE0018-2";
            Component2 swComp = swAssy.UnSuppress(suffix, subAssy);
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Distance3", exWidth + 40d);

            if (exWidth == 300d) swAssy.Suppress(suffix, doorPart2);
            else swAssy.UnSuppress(suffix, doorPart2);
            if (exWidth == 300d) swAssy.Suppress(suffix, doorPart1);
            else
            {
                swComp = swAssy.UnSuppress(suffix, doorPart1);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("Length@SketchBase", exLength / 2d);
                swPart.ChangeDim("Width@SketchBase", exWidth + 40d);
            }
            if (marvel == "YES") swAssy.Suppress(suffix, railPart2);
            else swAssy.UnSuppress(suffix, railPart2);
            if (marvel == "YES") swAssy.Suppress(suffix, railPart1);
            else
            {
                swComp = swAssy.UnSuppress(suffix, railPart1);
                swPart = swComp.GetModelDoc2();
                if (exNo == 1) swPart.ChangeDim("Length@Base-Flange", exLength * 2d + 50d);
                else swPart.ChangeDim("Length@Base-Flange", exLength * 3d + exDis + 50d);
            }
        }

        //排风脖颈
        internal void ExaustSpigot(AssemblyDoc swAssy, string suffix, string ansul, string marvel, double exLength,
            double exWidth, double exHeight, string subAssy)
        {

            string frontPart = "FNCE0019-1";
            string backPart = "FNCE0020-1";
            string leftPart = "FNCE0047-1";
            string rightPart = "FNCE0048-2";
            if (ansul != "YES" && (exHeight == 100d || marvel == "YES"))
            {
                swAssy.Suppress(suffix, subAssy);
            }
            else
            {
                swAssy.UnSuppress(suffix, subAssy);
                var swComp = swAssy.UnSuppress(suffix, frontPart);
                ModelDoc2 swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@基体-法兰1", exLength + 50d);
                swPart.ChangeDim("D3@草图1", exHeight);
                if (ansul == "YES") swComp.UnSuppress("ANSUL");
                else swComp.Suppress("ANSUL");

                swComp = swAssy.UnSuppress(suffix, backPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@基体-法兰1", exLength + 50d);
                swPart.ChangeDim("D3@草图1", exHeight);

                swComp = swAssy.UnSuppress(suffix, leftPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@基体-法兰1", exWidth);
                swPart.ChangeDim("D3@草图1", exHeight);
                if (ansul == "YES") swComp.UnSuppress("ANDTEC");
                else swComp.Suppress("ANDTEC");
               

                swComp = swAssy.UnSuppress(suffix, rightPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@基体-法兰1", exWidth);
                swPart.ChangeDim("D3@草图1", exHeight);
                if (ansul == "YES") swComp.UnSuppress("ANDTEC");
                else swComp.Suppress("ANDTEC");
            }
        }

        //盲板
        internal void FCBlind(ModelDoc2 swModel, AssemblyDoc swAssy, string suffix, int fcBlindNo, double fcSideLeft,
            string fcBlindPart, string fcBlindPattern, string fcBlindDis, string lightType, string fcBlindDisHCL)
        {
            if (fcBlindNo > 0)
            {
                swAssy.UnSuppress(suffix, fcBlindPart);
                swAssy.UnSuppress(fcBlindPattern);//盲板的阵列
                swModel.ChangeDim($"D1@{fcBlindPattern}", fcBlindNo);
                if (lightType == "HCL") swModel.ChangeDim(fcBlindDisHCL, fcSideLeft);//HCL
                else swModel.ChangeDim(fcBlindDis, fcSideLeft);
            }
            else
            {
                swAssy.Suppress(suffix, fcBlindPart);
                swAssy.Suppress(fcBlindPattern);
            }
        }

        //KSA/FC
        internal void KSAorFC(ModelDoc2 swModel, AssemblyDoc swAssy, string suffix, int fcBlindNo, string fcType, int fcNo, double fcSideLeft, string ksaPart, string ksaPattern, string ksaDis, string fcPart, string fcPattern, string fcDis, string lightType, string fcDisHCL)
        {
            if (fcType == "KSA")
            {
                swAssy.UnSuppress(suffix, ksaPart);
                swAssy.UnSuppress(ksaPattern);
                swModel.ChangeDim($"D1@{ksaPattern}", fcNo); //D1阵列数量,D3阵列距离
                swModel.ChangeDim(ksaDis, fcSideLeft + 500d * fcBlindNo);
                swAssy.Suppress(suffix, fcPart);
                swAssy.Suppress(fcPattern);
            }
            else
            {
                swAssy.UnSuppress(suffix, fcPart);
                swAssy.UnSuppress(fcPattern);
                swModel.ChangeDim($"D1@{fcPattern}", fcNo); //D1阵列数量,D3阵列距离
                if (lightType == "HCL") swModel.ChangeDim(fcDisHCL, fcSideLeft + 500d * fcBlindNo);//HCL
                else swModel.ChangeDim(fcDis, fcSideLeft + 500d * fcBlindNo);
                swAssy.Suppress(suffix, ksaPart);
                swAssy.Suppress(ksaPattern);
            }
        }

        //FC侧板长度
        internal void FCFilter(ModelDoc2 swModel, AssemblyDoc swAssy, string suffix, string module, string fcSide, string fcType, int fcNo, double fcSideLeft, double fcSideRight, string leftPart, int leftNum, string rightPart, int rightNum)
        {
            Component2 swComp;
            int fcSideLength;
            switch (fcSide)
            {
                case "LEFT":
                    if (fcType == "KSA") fcSideLength = (int)(fcSideLeft + fcNo * 2.5d);
                    else fcSideLength = (int)(fcSideLeft - fcNo);
                    swComp = RenameComp(swModel, swAssy, suffix, "BP", module, leftPart, leftNum, fcSideLength, 250);
                    if (swComp != null) FNCE0108(swComp, fcSideLength);
                    SuppressIfExist(swModel, swAssy, suffix, rightPart, rightNum);
                    break;
                case "RIGHT":
                    if (fcType == "KSA") fcSideLength = (int)(fcSideRight + fcNo * 2.5d);
                    else fcSideLength = (int)(fcSideRight - fcNo);
                    swComp = RenameComp(swModel, swAssy, suffix, "BP", module, rightPart, rightNum, fcSideLength, 250);
                    if (swComp != null) FNCE0109(swComp, fcSideLength);
                    SuppressIfExist(swModel, swAssy, suffix, leftPart, leftNum);
                    break;
                case "BOTH":
                    //重命名装配体内部
                    if (fcType == "KSA") fcSideLength = (int)(fcSideLeft + fcNo * 1.25d);
                    else fcSideLength = (int)(fcSideLeft - fcNo / 2d);
                    swComp = RenameComp(swModel, swAssy, suffix, "BP", $"{module}.1", leftPart, leftNum, fcSideLength, 250);
                    if (swComp != null) FNCE0108(swComp, fcSideLength);
                    if (fcType == "KSA") fcSideLength = (int)(fcSideRight + fcNo * 1.25d);
                    else fcSideLength = (int)(fcSideRight - fcNo / 2d);
                    swComp = RenameComp(swModel, swAssy, suffix, "BP", $"{module}.2", rightPart, rightNum, fcSideLength, 250);
                    if (swComp != null) FNCE0109(swComp, fcSideLength);
                    break;
                default:
                    SuppressIfExist(swModel, swAssy, suffix, leftPart, leftNum);
                    SuppressIfExist(swModel, swAssy, suffix, rightPart, rightNum);
                    break;
            }
        }

        internal void FNCE0108(Component2 swComp, double fcSideLength)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@草图1", fcSideLength);
        }

        internal void FNCE0109(Component2 swComp, double fcSideLength)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@草图1", fcSideLength);
        }

        //灯板支撑条
        internal void SSPSupport(ModelDoc2 swModel, AssemblyDoc swAssy, string suffix, double length, string lightType, string sspType, string gutter, double gutterWidth, string domePart, string domeDis, string domeDisHcl, string flatPart, string flatDis, string flatDisHcl)
        {
            Component2 swComp;
            if (gutter != "YES") gutterWidth = 0.5d;
            if (sspType == "DOME")
            {
                swAssy.Suppress(suffix, flatPart);
                swComp = swAssy.UnSuppress(suffix, domePart);
                FNCE0035(swComp, length);
                if (lightType == "HCL") swModel.ChangeDim(domeDisHcl, gutterWidth);
                else swModel.ChangeDim(domeDis, gutterWidth);
            }
            else
            {
                swAssy.Suppress(suffix, domePart);
                swComp = swAssy.UnSuppress(suffix, flatPart);
                FNCE0036(swComp, length);
                if (lightType == "HCL") swModel.ChangeDim(flatDisHcl, gutterWidth);
                else swModel.ChangeDim(flatDis, gutterWidth);
            }
        }

        internal void FNCE0035(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();//打开零件
            swPart.ChangeDim("D2@Sketch1", length);
        }

        internal void FNCE0036(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();//打开零件
            swPart.ChangeDim("D2@Sketch1", length);
        }


        //UCJ FC
        internal void UCJFC(ModelDoc2 swModel, AssemblyDoc swAssy, string suffix, int fcBlindNo, int fcNo, double fcSideLeft, string fcPart, string fcPattern, string fcDis, string lightType, string fcDisHCL)
        {
            swAssy.UnSuppress(suffix, fcPart);
            swAssy.UnSuppress(fcPattern);
            swModel.ChangeDim($"D1@{fcPattern}", fcNo); //D1阵列数量,D3阵列距离
            if (lightType == "HCL") swModel.ChangeDim(fcDisHCL, fcSideLeft + 500d * fcBlindNo);//HCL
            else swModel.ChangeDim(fcDis, fcSideLeft + 500d * fcBlindNo);
        }

        //UCJ UVRack
        internal void SpecialUvRack(AssemblyDoc swAssy, string suffix, string uvType, string shortPart, string longPart)
        {
            if (uvType == "LONG")
            {
                swAssy.Suppress(suffix, shortPart);
                swAssy.UnSuppress(suffix, longPart);
            }
            else
            {
                swAssy.UnSuppress(suffix, shortPart);
                swAssy.Suppress(suffix, longPart);
            }
        }


        #endregion



        #region 各个排风腔不同

        //-------KCJSB535排风腔----------
        internal void FNCE0111(Component2 swComp, double length, string lightCable, string marvel, string ansul, string anSide, string anDetector, string japan, double exRightDis, double exLength, double exWidth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Linear austragen1", length);

            #region 出线孔
            if (lightCable == "LEFT")
            {
                swComp.UnSuppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            else if (lightCable == "RIGHT")
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.UnSuppress("LIGHT HOLE RIGHT");
            }
            else
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            #endregion

            #region MARVEL
            if (marvel == "YES")
            {
                swComp.UnSuppress("MA-NTC");
                swComp.UnSuppress("MA-TAB");
            }
            else
            {
                swComp.Suppress("MA-NTC");
                swComp.Suppress("MA-TAB");
            }
            #endregion

            #region ANSUL
            if (ansul == "YES")
            {
                //侧喷
                if (anSide == "LEFT")
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.UnSuppress("ANSULSIDE LEFT");
                }
                else if (anSide == "RIGHT")
                {
                    swComp.UnSuppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                else
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                //探测器
                if (anDetector == "LEFT")
                {
                    swComp.Suppress("ANDTECSIDE RIGHT");
                    swComp.UnSuppress("ANDTECSIDE LEFT");
                }
                else if (anDetector == "RIGHT")
                {
                    swComp.UnSuppress("ANDTECSIDE RIGHT");
                    swComp.Suppress("ANDTECSIDE LEFT");
                }
                else if (anDetector == "BOTH")
                {
                    swComp.UnSuppress("ANDTECSIDE RIGHT");
                    swComp.UnSuppress("ANDTECSIDE LEFT");
                }
                else
                {
                    swComp.Suppress("ANDTECSIDE RIGHT");
                    swComp.Suppress("ANDTECSIDE LEFT");
                }
            }
            else
            {
                swComp.Suppress("ANSULSIDE RIGHT");
                swComp.Suppress("ANSULSIDE LEFT");
                swComp.Suppress("ANDTECSIDE RIGHT");
                swComp.Suppress("ANDTECSIDE LEFT");
            }
            #endregion

            #region 日本的
            if (japan == "YES")
            {
                swComp.Suppress("EX");
                swComp.Suppress("Cut-Extrude4");
            }
            else
            {
                swComp.UnSuppress("EX");
                swComp.UnSuppress("Cut-Extrude4");
                swPart.ChangeDim("D3@Sketch1", exRightDis);
                swPart.ChangeDim("D1@Sketch1", exLength);
                swPart.ChangeDim("D2@Sketch1", exWidth);
            }
            #endregion
        }

        //-------KCJSB265排风腔-------
        internal void FNCE0125(Component2 swComp, double length, string marvel, string ansul, string anSide, string anDetector, string japan, double exRightDis, double exLength, double exWidth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Aufsatz-Linear austragen1", length);

            #region MARVEL
            if (marvel == "YES")
            {
                swComp.UnSuppress("MA-NTC");
                swComp.UnSuppress("MA-TAB");
            }
            else
            {
                swComp.Suppress("MA-NTC");
                swComp.Suppress("MA-TAB");
            }
            #endregion

            #region ANSUL
            if (ansul == "YES")
            {
                //侧喷
                if (anSide == "LEFT")
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.UnSuppress("ANSULSIDE LEFT");
                }
                else if (anSide == "RIGHT")
                {
                    swComp.UnSuppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                else
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                //探测器
                if (anDetector == "LEFT")
                {
                    swComp.Suppress("ANDTECSIDE RIGHT");
                    swComp.UnSuppress("ANDTECSIDE LEFT");
                }
                else if (anDetector == "RIGHT")
                {
                    swComp.UnSuppress("ANDTECSIDE RIGHT");
                    swComp.Suppress("ANDTECSIDE LEFT");
                }
                else if (anDetector == "BOTH")
                {
                    swComp.UnSuppress("ANDTECSIDE RIGHT");
                    swComp.UnSuppress("ANDTECSIDE LEFT");
                }
                else
                {
                    swComp.Suppress("ANDTECSIDE RIGHT");
                    swComp.Suppress("ANDTECSIDE LEFT");
                }
            }
            else
            {
                swComp.Suppress("ANSULSIDE RIGHT");
                swComp.Suppress("ANSULSIDE LEFT");
                swComp.Suppress("ANDTECSIDE RIGHT");
                swComp.Suppress("ANDTECSIDE LEFT");
            }
            #endregion

            #region 日本的
            if (japan == "YES")
            {
                swComp.Suppress("EX");
                swComp.Suppress("Cut-Extrude4");
            }
            else
            {
                swComp.UnSuppress("EX");
                swComp.UnSuppress("Cut-Extrude4");
                swPart.ChangeDim("D4@Sketch2", exRightDis);
                swPart.ChangeDim("D1@Sketch2", exLength);
                swPart.ChangeDim("D2@Sketch2", exWidth);
            }
            #endregion
        }

        //-------KCJSB290排风腔-------
        internal void FNCE0127(Component2 swComp, double length, string marvel, string ansul, string anSide, string anDetector, string japan, double exRightDis, double exLength, double exWidth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", length);

            #region MARVEL
            if (marvel == "YES")
            {
                swComp.UnSuppress("MA-NTC");
                swComp.UnSuppress("MA-TAB");
            }
            else
            {
                swComp.Suppress("MA-NTC");
                swComp.Suppress("MA-TAB");
            }
            #endregion

            #region ANSUL
            if (ansul == "YES")
            {
                //侧喷
                if (anSide == "LEFT")
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.UnSuppress("ANSULSIDE LEFT");
                }
                else if (anSide == "RIGHT")
                {
                    swComp.UnSuppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                else
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                //探测器
                if (anDetector == "LEFT")
                {
                    swComp.Suppress("ANDTECSIDE RIGHT");
                    swComp.UnSuppress("ANDTECSIDE LEFT");
                }
                else if (anDetector == "RIGHT")
                {
                    swComp.UnSuppress("ANDTECSIDE RIGHT");
                    swComp.Suppress("ANDTECSIDE LEFT");
                }
                else if (anDetector == "BOTH")
                {
                    swComp.UnSuppress("ANDTECSIDE RIGHT");
                    swComp.UnSuppress("ANDTECSIDE LEFT");
                }
                else
                {
                    swComp.Suppress("ANDTECSIDE RIGHT");
                    swComp.Suppress("ANDTECSIDE LEFT");
                }
            }
            else
            {
                swComp.Suppress("ANSULSIDE RIGHT");
                swComp.Suppress("ANSULSIDE LEFT");
                swComp.Suppress("ANDTECSIDE RIGHT");
                swComp.Suppress("ANDTECSIDE LEFT");
            }
            #endregion

            #region 日本的
            if (japan == "YES")
            {
                swComp.Suppress("EX");
                swComp.Suppress("Cut-Extrude4");
            }
            else
            {
                swComp.UnSuppress("EX");
                swComp.UnSuppress("Cut-Extrude4");
                swPart.ChangeDim("D3@Sketch6", exRightDis);
                swPart.ChangeDim("D2@Sketch6", exLength);
                swPart.ChangeDim("D1@Sketch6", exWidth);
            }
            #endregion
        }

        //-------KCJDB800排风腔----------
        internal void FNCE0115(Component2 swComp, double length, string lightType, string lightCable, string marvel, string ansul, string anSide, string anDetectorEnd, int anDetectorNo, double anDetectorDis1, double anDetectorDis2, double anDetectorDis3, double anDetectorDis4, double anDetectorDis5, string japan, double exRightDis, double exLength, double exWidth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Linear austragen1", length);

            #region 出线孔
            if (lightType == "HCL")
            {
                if (lightCable == "LEFT")
                {
                    swComp.UnSuppress("HCL LIGHT HOLE LEFT");
                    swComp.Suppress("HCL LIGHT HOLE RIGHT");
                }
                else if (lightCable == "RIGHT")
                {
                    swComp.Suppress("HCL LIGHT HOLE LEFT");
                    swComp.UnSuppress("HCL LIGHT HOLE RIGHT");
                }
                else
                {
                    swComp.Suppress("HCL LIGHT HOLE LEFT");
                    swComp.Suppress("HCL LIGHT HOLE RIGHT");
                }
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            else
            {
                if (lightCable == "LEFT")
                {
                    swComp.UnSuppress("LIGHT HOLE LEFT");
                    swComp.Suppress("LIGHT HOLE RIGHT");
                }
                else if (lightCable == "RIGHT")
                {
                    swComp.Suppress("LIGHT HOLE LEFT");
                    swComp.UnSuppress("LIGHT HOLE RIGHT");
                }
                else
                {
                    swComp.Suppress("LIGHT HOLE LEFT");
                    swComp.Suppress("LIGHT HOLE RIGHT");
                }
                swComp.Suppress("HCL LIGHT HOLE LEFT");
                swComp.Suppress("HCL LIGHT HOLE RIGHT");
            }

            #endregion

            #region MARVEL
            if (marvel == "YES")
            {
                swComp.UnSuppress("MA-NTC");
                swComp.UnSuppress("MA-TAB");
            }
            else
            {
                swComp.Suppress("MA-NTC");
                swComp.Suppress("MA-TAB");
            }
            #endregion

            #region ANSUL
            if (ansul == "YES")
            {
                //侧喷
                if (anSide == "LEFT")
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.UnSuppress("ANSULSIDE LEFT");
                }
                else if (anSide == "RIGHT")
                {
                    swComp.UnSuppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                else
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                //探测器
                swComp.Suppress("ANDTEC1");
                swComp.Suppress("ANDTEC2");
                swComp.Suppress("ANDTEC3");
                swComp.Suppress("ANDTEC4");
                swComp.Suppress("ANDTEC5");
                if (anDetectorNo > 0)
                {
                    swComp.UnSuppress("ANDTEC1");
                    swPart.ChangeDim("D2@Sketch9", anDetectorDis1);
                    if (anDetectorEnd == "RIGHT" || (anDetectorEnd == "LEFT" && anDetectorNo == 1))
                        swPart.ChangeDim("D1@Sketch9", 195d);
                    else swPart.ChangeDim("D1@Sketch9", 175d);
                }
                if (anDetectorNo > 1)
                {
                    swComp.UnSuppress("ANDTEC2");
                    swPart.ChangeDim("D1@Sketch10", anDetectorDis2);
                    if (anDetectorEnd == "LEFT" && anDetectorNo == 2)
                        swPart.ChangeDim("D2@Sketch10", 195d);
                    else swPart.ChangeDim("D2@Sketch10", 175d);
                }
                if (anDetectorNo > 2)
                {
                    swComp.UnSuppress("ANDTEC3");
                    swPart.ChangeDim("D3@Sketch11", anDetectorDis3);
                    if (anDetectorEnd == "LEFT" && anDetectorNo == 3)
                        swPart.ChangeDim("D1@Sketch11", 195d);
                    else swPart.ChangeDim("D1@Sketch11", 175d);
                }
                if (anDetectorNo > 3)
                {
                    swComp.UnSuppress("ANDTEC4");
                    swPart.ChangeDim("D2@Sketch12", anDetectorDis4);
                    if (anDetectorEnd == "LEFT" && anDetectorNo == 4)
                        swPart.ChangeDim("D3@Sketch12", 195d);
                    else swPart.ChangeDim("D3@Sketch12", 175d);
                }
                if (anDetectorNo > 4)
                {
                    swComp.UnSuppress("ANDTEC5");
                    swPart.ChangeDim("D3@Sketch13", anDetectorDis5);
                    if (anDetectorEnd == "LEFT" && anDetectorNo == 5)
                        swPart.ChangeDim("D1@Sketch13", 195d);
                    else swPart.ChangeDim("D1@Sketch13", 175d);
                }
            }
            else
            {
                swComp.Suppress("ANSULSIDE RIGHT");
                swComp.Suppress("ANSULSIDE LEFT");
                swComp.Suppress("ANDTEC1");
                swComp.Suppress("ANDTEC2");
                swComp.Suppress("ANDTEC3");
                swComp.Suppress("ANDTEC4");
                swComp.Suppress("ANDTEC5");
            }
            #endregion

            #region 日本的
            if (japan == "YES")
            {
                swComp.Suppress("EX");
                swComp.Suppress("Cut-Extrude4");
            }
            else
            {
                swComp.UnSuppress("EX");
                swComp.UnSuppress("Cut-Extrude4");
                swPart.ChangeDim("D4@Sketch1", exRightDis);
                swPart.ChangeDim("D2@Sketch1", exLength);
                swPart.ChangeDim("D1@Sketch1", exWidth);
            }
            #endregion
        }




        //-------KCJSB535排风腔内灯腔----------
        internal void FNCE0112(Component2 swComp, double length, string lightCable, string lightType, string japan)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Linear austragen1", length);
            swComp.Suppress("FC SUPPORT");
            #region 灯腔出线孔
            if (lightCable == "LEFT")
            {
                swComp.UnSuppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            else if (lightCable == "RIGHT")
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.UnSuppress("LIGHT HOLE RIGHT");
            }
            else
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            #endregion
            if (lightType == "T8") swComp.UnSuppress("LIGHT T8");
            else swComp.Suppress("LIGHT T8");
            if (japan == "YES") swComp.UnSuppress("JAP LED M8");
            else swComp.Suppress("JAP LED M8");
        }

        //-------KCJDB800/UCJDB800排风腔内灯腔----------
        internal void FNCE0116(Component2 swComp, double length,string uvType, string lightCable, string lightType, string japan)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Linear austragen1", length);
            if (uvType == "NO")
            {
                swComp.Suppress("FC SUPPORT");
                swComp.Suppress("FC SUPPORT B");
            }
            else
            {
                swComp.UnSuppress("FC SUPPORT");
                swComp.UnSuppress("FC SUPPORT B");
            }

            #region 出线孔
            if (lightCable == "LEFT")
            {
                swComp.UnSuppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            else if (lightCable == "RIGHT")
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.UnSuppress("LIGHT HOLE RIGHT");
            }
            else
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            #endregion
            if (lightType == "T8") swComp.UnSuppress("LIGHT T8");
            else swComp.Suppress("LIGHT T8");
            if (japan == "YES") swComp.UnSuppress("JAP LED M8");
            else swComp.Suppress("JAP LED M8");
        }


        //-------灯腔玻璃支架底部-------
        internal void FNCE0056(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Skizze1", length);
        }


        //-------UCJSB535排风腔----------
        internal void FNCE0159(Component2 swComp, double length, string uvType, string lightCable, string marvel, string ansul, string anSide, string anDetector, string japan, double exRightDis, double exLength, double exWidth)

        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            //公共的
            swPart.ChangeDim("D1@Linear austragen1", length);
            swComp.UnSuppress("FC CABLE");
            swComp.UnSuppress("MA-TAB");

            #region UV灯
            if (uvType == "LONG")
            {
                swComp.UnSuppress("UV L");
                swPart.ChangeDim("D8@Sketch14", exRightDis - 800d);
                swPart.ChangeDim("D9@Sketch14", exRightDis - 600d);
                swComp.Suppress("UV S");
            }
            else
            {
                swComp.UnSuppress("UV S");
                swPart.ChangeDim("D5@Sketch4", exRightDis - 446.5d);
                swPart.ChangeDim("D7@Sketch4", exRightDis - 300d);
                swComp.Suppress("UV L");
            }
            #endregion


            #region 出线孔
            if (lightCable == "LEFT")
            {
                swComp.UnSuppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            else if (lightCable == "RIGHT")
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.UnSuppress("LIGHT HOLE RIGHT");
            }
            else
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            #endregion

            #region MARVEL
            if (marvel == "YES")
            {
                swComp.UnSuppress("MA-NTC");
                swPart.ChangeDim("D1@Sketch12", exRightDis + exLength / 2d + 50d);
            }
            else
            {
                swComp.Suppress("MA-NTC");
            }
            #endregion

            #region ANSUL
            if (ansul == "YES")
            {
                //侧喷
                if (anSide == "LEFT")
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.UnSuppress("ANSULSIDE LEFT");
                }
                else if (anSide == "RIGHT")
                {
                    swComp.UnSuppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                else
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                //探测器
                if (anDetector == "LEFT")
                {
                    swComp.Suppress("ANDTECSIDE RIGHT");
                    swComp.UnSuppress("ANDTECSIDE LEFT");
                }
                else if (anDetector == "RIGHT")
                {
                    swComp.UnSuppress("ANDTECSIDE RIGHT");
                    swComp.Suppress("ANDTECSIDE LEFT");
                }
                else if (anDetector == "BOTH")
                {
                    swComp.UnSuppress("ANDTECSIDE RIGHT");
                    swComp.UnSuppress("ANDTECSIDE LEFT");
                }
                else
                {
                    swComp.Suppress("ANDTECSIDE RIGHT");
                    swComp.Suppress("ANDTECSIDE LEFT");
                }
            }
            else
            {
                swComp.Suppress("ANSULSIDE RIGHT");
                swComp.Suppress("ANSULSIDE LEFT");
                swComp.Suppress("ANDTECSIDE RIGHT");
                swComp.Suppress("ANDTECSIDE LEFT");
            }
            #endregion

            #region 日本的
            if (japan == "YES")
            {
                swComp.Suppress("EX");
                swComp.Suppress("Cut-Extrude4");
            }
            else
            {
                swComp.UnSuppress("EX");
                swComp.UnSuppress("Cut-Extrude4");
                swPart.ChangeDim("D3@Sketch1", exRightDis);
                swPart.ChangeDim("D1@Sketch1", exLength);
                swPart.ChangeDim("D2@Sketch1", exWidth);
            }
            #endregion
        }

        //-------UCJSB535磁棒板----------
        internal void FNCE0145(Component2 swComp, double length, string fcSide, int fcNo, int fcBlindNo, double fcSideLeft)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();//打开零件
            swPart.ChangeDim("D2@Base-Flange1", length - 5d);
            swPart.ChangeDim("D1@LPattern1", fcNo + fcBlindNo);
            //只有左边有侧板才有意义
            if (fcSide == "LEFT" || fcSide == "BOTH") swPart.ChangeDim("D3@Sketch6", fcSideLeft + 250d);
            else swPart.ChangeDim("D3@Sketch6", 250d);
            if (fcSide == "LEFT"||fcSide=="BOTH")
            {
                swComp.UnSuppress("Cut-Extrude4");
                swPart.ChangeDim("D2@Sketch10", fcSideLeft/2d-2.5d);
            }
            else swComp.Suppress("Cut-Extrude4");
        }
        internal void FNCE0161(Component2 swComp, double length, string fcSide, int fcNo, int fcBlindNo, double fcSideRight, double fcSideLeft)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();//打开零件
            swPart.ChangeDim("D2@Base-Flange1", length - 5d);
            swPart.ChangeDim("D1@LPattern1", fcNo + fcBlindNo);
            if (fcSide == "LEFT" || fcSide == "BOTH") swPart.ChangeDim("D3@Sketch6", fcSideLeft + 250d);
            else swPart.ChangeDim("D3@Sketch6", 250d);
            if (fcSide == "RIGHT"||fcSide=="BOTH")
            {
                swComp.UnSuppress("Cut-Extrude4");
                swPart.ChangeDim("D3@Sketch10", fcSideRight/2d-2.5d);
            }
            else swComp.Suppress("Cut-Extrude4");
        }

        //-------UCJDB800排风腔----------
        internal void FNCE0141(Component2 swComp, double length,string uvType, string lightType, string lightCable, string marvel, string ansul, string anSide, string anDetectorEnd, int anDetectorNo, double anDetectorDis1, double anDetectorDis2, double anDetectorDis3, double anDetectorDis4, double anDetectorDis5, string japan, double exRightDis, double exLength, double exWidth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Linear austragen1", length);
            swComp.UnSuppress("MA-TAB");

            #region UV灯
            if (uvType == "LONG")
            {
                swComp.UnSuppress("UV L");
                swPart.ChangeDim("D5@Sketch4", exRightDis - 800d);
                swPart.ChangeDim("D8@Sketch4", exRightDis - 600d);
                swComp.Suppress("UV S");
            }
            else
            {
                swComp.UnSuppress("UV S");
                swPart.ChangeDim("D3@Sketch11", exRightDis - 446.5d);
                swPart.ChangeDim("D8@Sketch11", exRightDis - 300d);
                swComp.Suppress("UV L");
            }
            #endregion

            #region 出线孔
            /*if (lightType == "HCL")
            {
                if (lightCable == "LEFT")
                {
                    swComp.UnSuppress("HCL LIGHT HOLE LEFT");
                    swComp.Suppress("HCL LIGHT HOLE RIGHT");
                }
                else if (lightCable == "RIGHT")
                {
                    swComp.Suppress("HCL LIGHT HOLE LEFT");
                    swComp.UnSuppress("HCL LIGHT HOLE RIGHT");
                }
                else
                {
                    swComp.Suppress("HCL LIGHT HOLE LEFT");
                    swComp.Suppress("HCL LIGHT HOLE RIGHT");
                }
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            else
            {
                if (lightCable == "LEFT")
                {
                    swComp.UnSuppress("LIGHT HOLE LEFT");
                    swComp.Suppress("LIGHT HOLE RIGHT");
                }
                else if (lightCable == "RIGHT")
                {
                    swComp.Suppress("LIGHT HOLE LEFT");
                    swComp.UnSuppress("LIGHT HOLE RIGHT");
                }
                else
                {
                    swComp.Suppress("LIGHT HOLE LEFT");
                    swComp.Suppress("LIGHT HOLE RIGHT");
                }
                swComp.Suppress("HCL LIGHT HOLE LEFT");
                swComp.Suppress("HCL LIGHT HOLE RIGHT");
            }*/
            if (lightCable == "LEFT")
            {
                swComp.UnSuppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            else if (lightCable == "RIGHT")
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.UnSuppress("LIGHT HOLE RIGHT");
            }
            else
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            #endregion

            #region MARVEL
            if (marvel == "YES")
            {
                swComp.UnSuppress("MA-NTC");
            }
            else
            {
                swComp.Suppress("MA-NTC");
            }
            #endregion

            #region ANSUL
            if (ansul == "YES")
            {
                //侧喷
                if (anSide == "LEFT")
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.UnSuppress("ANSULSIDE LEFT");
                }
                else if (anSide == "RIGHT")
                {
                    swComp.UnSuppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                else
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                //探测器
                swComp.Suppress("ANDTEC1");
                swComp.Suppress("ANDTEC2");
                swComp.Suppress("ANDTEC3");
                swComp.Suppress("ANDTEC4");
                swComp.Suppress("ANDTEC5");
                if (anDetectorNo > 0)
                {
                    swComp.UnSuppress("ANDTEC1");
                    swPart.ChangeDim("D3@Sketch9", anDetectorDis1);
                    if (anDetectorEnd == "RIGHT" || (anDetectorEnd == "LEFT" && anDetectorNo == 1))
                        swPart.ChangeDim("D1@Sketch9", 195d);
                    else swPart.ChangeDim("D1@Sketch9", 175d);
                }
                if (anDetectorNo > 1)
                {
                    swComp.UnSuppress("ANDTEC2");
                    swPart.ChangeDim("D1@Sketch15", anDetectorDis2);
                    if (anDetectorEnd == "LEFT" && anDetectorNo == 2)
                        swPart.ChangeDim("D2@Sketch15", 195d);
                    else swPart.ChangeDim("D2@Sketch15", 175d);
                }
                if (anDetectorNo > 2)
                {
                    swComp.UnSuppress("ANDTEC3");
                    swPart.ChangeDim("D1@Sketch16", anDetectorDis3);
                    if (anDetectorEnd == "LEFT" && anDetectorNo == 3)
                        swPart.ChangeDim("D2@Sketch16", 195d);
                    else swPart.ChangeDim("D2@Sketch16", 175d);
                }
                if (anDetectorNo > 3)
                {
                    swComp.UnSuppress("ANDTEC4");
                    swPart.ChangeDim("D1@Sketch17", anDetectorDis4);
                    if (anDetectorEnd == "LEFT" && anDetectorNo == 4)
                        swPart.ChangeDim("D2@Sketch17", 195d);
                    else swPart.ChangeDim("D2@Sketch17", 175d);
                }
                if (anDetectorNo > 4)
                {
                    swComp.UnSuppress("ANDTEC5");
                    swPart.ChangeDim("D1@Sketch18", anDetectorDis5);
                    if (anDetectorEnd == "LEFT" && anDetectorNo == 5)
                        swPart.ChangeDim("D2@Sketch18", 195d);
                    else swPart.ChangeDim("D2@Sketch18", 175d);
                }
            }
            else
            {
                swComp.Suppress("ANSULSIDE RIGHT");
                swComp.Suppress("ANSULSIDE LEFT");
                swComp.Suppress("ANDTEC1");
                swComp.Suppress("ANDTEC2");
                swComp.Suppress("ANDTEC3");
                swComp.Suppress("ANDTEC4");
                swComp.Suppress("ANDTEC5");
            }
            #endregion

            #region 日本的
            if (japan == "YES")
            {
                swComp.Suppress("EX");
                swComp.Suppress("Cut-Extrude4");
            }
            else
            {
                swComp.UnSuppress("EX");
                swComp.UnSuppress("Cut-Extrude4");
                swPart.ChangeDim("D3@Sketch3", exRightDis);
                swPart.ChangeDim("D1@Sketch3", exLength);
                swPart.ChangeDim("D2@Sketch3", exWidth);
            }
            #endregion
        }



        #endregion

        #region HCL
        //-------KCJ535HCL排风腔-------
        internal void FNCE0089(Component2 swComp, double length, string lightCable, string marvel, string ansul, string anSide, string anDetector, string lightPanelSide, double lightPanelLeft, double lightPanelRight, string japan, double exRightDis, double exLength, double exWidth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            //公共的
            swPart.ChangeDim("D1@Linear austragen1", length);

            #region 出线孔
            if (lightCable == "LEFT")
            {
                swComp.UnSuppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            else if (lightCable == "RIGHT")
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.UnSuppress("LIGHT HOLE RIGHT");
            }
            else
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            #endregion

            #region MARVEL
            if (marvel == "YES")
            {
                swComp.UnSuppress("MA-NTC");
                swComp.UnSuppress("MA-TAB");
            }
            else
            {
                swComp.Suppress("MA-NTC");
                swComp.Suppress("MA-TAB");
            }
            #endregion

            #region ANSUL
            if (ansul == "YES")
            {
                //侧喷
                if (anSide == "LEFT")
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.UnSuppress("ANSULSIDE LEFT");
                }
                else if (anSide == "RIGHT")
                {
                    swComp.UnSuppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                else
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                //探测器
                if (anDetector == "LEFT")
                {
                    swComp.Suppress("ANDTECSIDE RIGHT");
                    swComp.UnSuppress("ANDTECSIDE LEFT");
                }
                else if (anDetector == "RIGHT")
                {
                    swComp.UnSuppress("ANDTECSIDE RIGHT");
                    swComp.Suppress("ANDTECSIDE LEFT");
                }
                else if (anDetector == "BOTH")
                {
                    swComp.UnSuppress("ANDTECSIDE RIGHT");
                    swComp.UnSuppress("ANDTECSIDE LEFT");
                }
                else
                {
                    swComp.Suppress("ANDTECSIDE RIGHT");
                    swComp.Suppress("ANDTECSIDE LEFT");
                }
            }
            else
            {
                swComp.Suppress("ANSULSIDE RIGHT");
                swComp.Suppress("ANSULSIDE LEFT");
                swComp.Suppress("ANDTECSIDE RIGHT");
                swComp.Suppress("ANDTECSIDE LEFT");
            }
            #endregion

            #region 镀锌板安装距离
            if (lightPanelSide == "LEFT" || lightPanelSide == "BOTH")
            {
                swComp.UnSuppress("LightPanelLeft");
                swPart.ChangeDim("D5@Sketch21", lightPanelLeft - 150d);
            }
            else
            {
                swComp.Suppress("LightPanelLeft");
            }
            if (lightPanelSide == "RIGHT" || lightPanelSide == "BOTH")
            {
                swComp.UnSuppress("LightPanelRight");
                swPart.ChangeDim("D5@Sketch22", lightPanelRight - 150d);
            }
            else
            {
                swComp.Suppress("LightPanelRight");
            }
            #endregion

            #region 日本的
            if (japan == "YES")
            {
                swComp.Suppress("EX");
                swComp.Suppress("Cut-Extrude4");
            }
            else
            {
                swComp.UnSuppress("EX");
                swComp.UnSuppress("Cut-Extrude4");
                swPart.ChangeDim("D3@Sketch1", exRightDis);
                swPart.ChangeDim("D1@Sketch1", exLength);
                swPart.ChangeDim("D2@Sketch1", exWidth);
            }
            #endregion
        }

        //-------KCJ535HCL排风腔内灯腔----------
        internal void FNCE0085(Component2 swComp, double length, string lightCable, string lightType, string japan)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", length);
            swComp.Suppress("FC SUPPORT");
            #region 灯腔出线孔
            if (lightCable == "LEFT")
            {
                swComp.UnSuppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            else if (lightCable == "RIGHT")
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.UnSuppress("LIGHT HOLE RIGHT");
            }
            else
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            #endregion
            if (lightType == "T8") swComp.UnSuppress("LIGHT T8");
            else swComp.Suppress("LIGHT T8");
            if (japan == "YES") swComp.UnSuppress("JAP LED M8");
            else swComp.Suppress("JAP LED M8");
        }

        //-------KCJDB800HCL排风腔内灯腔----------
        internal void FNCE0087(Component2 swComp, double length, string lightCable, string lightType, string japan)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Linear austragen1", length);
            swComp.Suppress("FC SUPPORT");
            swComp.Suppress("FC SUPPORT B");
            swComp.Suppress("LIGHT T8");
            swComp.Suppress("JAP LED M8");

            #region 灯腔出线孔
            if (lightCable == "LEFT")
            {
                swComp.UnSuppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            else if (lightCable == "RIGHT")
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.UnSuppress("LIGHT HOLE RIGHT");
            }
            else
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            #endregion
            
        }





        //-------HCL灯腔玻璃支架底部-------
        internal void FNCE0090(Component2 swComp, double length, string lightPanelSide, double lightPanelLeft, double lightPanelRight)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Skizze1", length);
            #region 镀锌板安装距离
            if (lightPanelSide == "LEFT" || lightPanelSide == "BOTH")
            {
                swComp.UnSuppress("LightPanelLeft");
                swPart.ChangeDim("D5@Sketch25", (lightPanelLeft - 150));
            }
            else
            {
                swComp.Suppress("LightPanelLeft");
            }
            if (lightPanelSide == "RIGHT" || lightPanelSide == "BOTH")
            {
                swComp.UnSuppress("LightPanelRight");
                swPart.ChangeDim("D5@Sketch24", (lightPanelRight - 150));
            }
            else
            {
                swComp.Suppress("LightPanelRight");
            }
            #endregion
        }
        internal void FNCE0099(Component2 swComp, double length, string lightPanelSide, double lightPanelLeft, double lightPanelRight)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Skizze1", length);
            #region 镀锌板安装距离
            if (lightPanelSide == "LEFT" || lightPanelSide == "BOTH")
            {
                swComp.UnSuppress("LightPanelLeft");
                swPart.ChangeDim("D5@Sketch24", (lightPanelLeft - 150));
            }
            else
            {
                swComp.Suppress("LightPanelLeft");
            }
            if (lightPanelSide == "RIGHT" || lightPanelSide == "BOTH")
            {
                swComp.UnSuppress("LightPanelRight");
                swPart.ChangeDim("D5@Sketch25", (lightPanelRight - 150));
            }
            else
            {
                swComp.Suppress("LightPanelRight");
            }
            #endregion
        }


        //-------HCL灯腔玻璃支架支撑条上部-------
        internal void FNCE0091(Component2 swComp, double length, string lightPanelSide, double lightPanelLeft, double lightPanelRight)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2(); //打开零件
            double totalLength;
            if (lightPanelSide == "LEFT") totalLength = length - lightPanelLeft;
            else if (lightPanelSide == "RIGHT") totalLength = length - lightPanelRight;
            else if (lightPanelSide == "BOTH") totalLength = length - lightPanelLeft - lightPanelRight;
            else totalLength = length;
            swPart.ChangeDim("D1@Skizze1", totalLength);
        }


        //HCL灯腔侧板
        internal void HCLSidePanel(ModelDoc2 swModel, AssemblyDoc swAssy, string suffix, string module, string lightPanelSide, double lightPanelLeft, double lightPanelRight, string leftPart, int leftNum, string rightPart, int rightNum)
        {
            Component2 swComp;
            switch (lightPanelSide)
            {
                case "LEFT":
                    swComp = RenameComp(swModel, swAssy, suffix, "HCLSP", module, leftPart, leftNum, lightPanelLeft, 200);
                    if (swComp != null) FNCE0092(swComp, lightPanelLeft);
                    SuppressIfExist(swModel, swAssy, suffix, rightPart, rightNum);
                    break;
                case "RIGHT":
                    swComp = RenameComp(swModel, swAssy, suffix, "HCLSP", module, rightPart, rightNum, lightPanelRight, 200);
                    if (swComp != null) FNCE0094(swComp, lightPanelRight);
                    SuppressIfExist(swModel, swAssy, suffix, leftPart, leftNum);
                    break;
                case "BOTH":
                    swComp = RenameComp(swModel, swAssy, suffix, "HCLSP", $"{module}.1", leftPart, leftNum, lightPanelLeft, 200);
                    if (swComp != null) FNCE0092(swComp, lightPanelLeft);
                    swComp = RenameComp(swModel, swAssy, suffix, "HCLSP", $"{module}.2", rightPart, rightNum, lightPanelRight, 200);
                    if (swComp != null) FNCE0094(swComp, lightPanelRight);
                    break;
                default:
                    SuppressIfExist(swModel, swAssy, suffix, leftPart, leftNum);
                    SuppressIfExist(swModel, swAssy, suffix, rightPart, rightNum);
                    break;
            }
        }

        internal void FNCE0092(Component2 swComp, double lightPanelLeft)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2(); //打开零件
            swPart.ChangeDim("D1@Sketch1", lightPanelLeft);
            swPart.ChangeDim("D6@Sketch42", lightPanelLeft - 124d);
        }

        internal void FNCE0094(Component2 swComp, double lightPanelRight)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2(); //打开零件
            swPart.ChangeDim("D1@Sketch1", lightPanelRight);
            swPart.ChangeDim("D6@Sketch42", lightPanelRight - 124d);
        }

        //-------UCJ535HCL排风腔-------
        internal void FNCE0102(Component2 swComp, double length, string uvType, string lightCable, string marvel, string ansul, string anSide, string anDetector, string lightPanelSide, double lightPanelLeft, double lightPanelRight, string japan, double exRightDis, double exLength, double exWidth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            //公共的
            swPart.ChangeDim("D1@Linear austragen1", length);
            swComp.UnSuppress("FC CABLE");
            swComp.UnSuppress("MA-TAB");

            #region UV灯
            if (uvType == "LONG")
            {
                swComp.UnSuppress("UV L");
                swPart.ChangeDim("D8@Sketch14", exRightDis - 800d);
                swPart.ChangeDim("D9@Sketch14", exRightDis - 600d);
                swComp.Suppress("UV S");
            }
            else
            {
                swComp.UnSuppress("UV S");
                swPart.ChangeDim("D5@Sketch4", exRightDis - 446.5d);
                swPart.ChangeDim("D7@Sketch4", exRightDis - 300d);
                swComp.Suppress("UV L");
            }
            #endregion


            #region 出线孔
            if (lightCable == "LEFT")
            {
                swComp.UnSuppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            else if (lightCable == "RIGHT")
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.UnSuppress("LIGHT HOLE RIGHT");
            }
            else
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            #endregion

            #region MARVEL
            if (marvel == "YES")
            {
                swComp.UnSuppress("MA-NTC");
                swPart.ChangeDim("D1@Sketch12", exRightDis + exLength / 2d + 50d);
            }
            else
            {
                swComp.Suppress("MA-NTC");
            }
            #endregion

            #region ANSUL
            if (ansul == "YES")
            {
                //侧喷
                if (anSide == "LEFT")
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.UnSuppress("ANSULSIDE LEFT");
                }
                else if (anSide == "RIGHT")
                {
                    swComp.UnSuppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                else
                {
                    swComp.Suppress("ANSULSIDE RIGHT");
                    swComp.Suppress("ANSULSIDE LEFT");
                }
                //探测器
                if (anDetector == "LEFT")
                {
                    swComp.Suppress("ANDTECSIDE RIGHT");
                    swComp.UnSuppress("ANDTECSIDE LEFT");
                }
                else if (anDetector == "RIGHT")
                {
                    swComp.UnSuppress("ANDTECSIDE RIGHT");
                    swComp.Suppress("ANDTECSIDE LEFT");
                }
                else if (anDetector == "BOTH")
                {
                    swComp.UnSuppress("ANDTECSIDE RIGHT");
                    swComp.UnSuppress("ANDTECSIDE LEFT");
                }
                else
                {
                    swComp.Suppress("ANDTECSIDE RIGHT");
                    swComp.Suppress("ANDTECSIDE LEFT");
                }
            }
            else
            {
                swComp.Suppress("ANSULSIDE RIGHT");
                swComp.Suppress("ANSULSIDE LEFT");
                swComp.Suppress("ANDTECSIDE RIGHT");
                swComp.Suppress("ANDTECSIDE LEFT");
            }
            #endregion

            #region 镀锌板安装距离
            if (lightPanelSide == "LEFT" || lightPanelSide == "BOTH")
            {
                swComp.UnSuppress("Cut-Extrude5");
                swPart.ChangeDim("D5@Sketch21", lightPanelLeft - 150d);
            }
            else
            {
                swComp.Suppress("Cut-Extrude5");
            }
            if (lightPanelSide == "RIGHT" || lightPanelSide == "BOTH")
            {
                swComp.UnSuppress("Cut-Extrude6");
                swPart.ChangeDim("D5@Sketch22", lightPanelRight - 150d);
            }
            else
            {
                swComp.Suppress("Cut-Extrude6");
            }
            #endregion

            #region 日本的
            if (japan == "YES")
            {
                swComp.Suppress("EX");
                swComp.Suppress("Cut-Extrude4");
            }
            else
            {
                swComp.UnSuppress("EX");
                swComp.UnSuppress("Cut-Extrude4");
                swPart.ChangeDim("D3@Sketch1", exRightDis);
                swPart.ChangeDim("D1@Sketch1", exLength);
                swPart.ChangeDim("D2@Sketch1", exWidth);
            }
            #endregion
        }

        //-------UCJ535HCL排风腔内灯腔----------
        internal void FNCE0067(Component2 swComp, double length, string lightCable, string lightType, string japan)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", length);
            swComp.UnSuppress("FC SUPPORT");
            #region 灯腔出线孔
            if (lightCable == "LEFT")
            {
                swComp.UnSuppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            else if (lightCable == "RIGHT")
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.UnSuppress("LIGHT HOLE RIGHT");
            }
            else
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            #endregion
            if (lightType == "T8") swComp.UnSuppress("LIGHT T8");
            else swComp.Suppress("LIGHT T8");
            if (japan == "YES") swComp.UnSuppress("JAP LED M8");
            else swComp.Suppress("JAP LED M8");
        }

        //-------HCL磁棒板----------
        internal void FNCE0069(Component2 swComp, double length, string fcSide, int fcNo, int fcBlindNo, double fcSideLeft)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();//打开零件
            swPart.ChangeDim("D2@Base-Flange1", length - 5d);
            swPart.ChangeDim("D1@LPattern1", fcNo + fcBlindNo);
            
            if (fcSide == "LEFT" || fcSide == "BOTH") swPart.ChangeDim("D3@Sketch6", fcSideLeft + 250d);
            else swPart.ChangeDim("D3@Sketch6", 250d);
            
            if (fcSide == "LEFT"||fcSide=="BOTH")
            {
                swComp.UnSuppress("Cut-Extrude4");
                swPart.ChangeDim("D3@Sketch10", fcSideLeft/2d-2.5d);
            }
            else swComp.Suppress("Cut-Extrude4");
        }
        internal void FNCE0071(Component2 swComp, double length, string fcSide, int fcNo, int fcBlindNo, double fcSideRight,double fcSideLeft)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();//打开零件
            swPart.ChangeDim("D2@Base-Flange1", length - 5d);
            swPart.ChangeDim("D1@LPattern1", fcNo + fcBlindNo);
            if (fcSide == "LEFT" || fcSide == "BOTH") swPart.ChangeDim("D3@Sketch6", fcSideLeft + 250d);
            else swPart.ChangeDim("D3@Sketch6", 250d);
            if (fcSide == "RIGHT"||fcSide=="BOTH")
            {
                swComp.UnSuppress("Cut-Extrude4");
                swPart.ChangeDim("D3@Sketch10", fcSideRight/2d-2.5d);
            }
            else swComp.Suppress("Cut-Extrude4");
        }



        //-------UCJDB800HCL排风腔内灯腔----------
        internal void FNCE0054(Component2 swComp, double length, string lightCable, string lightType, string japan)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Linear austragen1", length);
            swComp.UnSuppress("FC SUPPORT");
            swComp.UnSuppress("FC SUPPORT B");
            swComp.Suppress("LIGHT T8");
            swComp.Suppress("JAP LED M8");

            #region 灯腔出线孔
            if (lightCable == "LEFT")
            {
                swComp.UnSuppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            else if (lightCable == "RIGHT")
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.UnSuppress("LIGHT HOLE RIGHT");
            }
            else
            {
                swComp.Suppress("LIGHT HOLE LEFT");
                swComp.Suppress("LIGHT HOLE RIGHT");
            }
            #endregion
        }





        #endregion

        #region 日本灯腔侧板，玻璃

        public void FNCL0026(Component2 swComp, double leftLength)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2(); //打开零件
            swPart.ChangeDim("D2@Skizze1", leftLength);
        }
        #endregion

    }
}
