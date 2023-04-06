using System;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorksHelper;

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
            FNHE0001(swComp, length, exNo, exRightDis, exLength, exWidth, exDis, waterCollection, sidePanel, outlet, backToBack, ansul, anSide, anDetector, marvel, uvType);


        }







        //排风腔new
        internal void FNHE0001(Component2 swComp, double length, int exNo, double exRightDis, double exLength, double exWidth, double exDis, string waterCollection, string sidePanel, string outlet, string backToBack, string ansul, string anSide, string anDetector, string marvel, string uvType)
        {
            #region 基本参数
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("Length@SketchBase", length);
            #endregion

            #region MidRoof铆螺母孔
            //2023/3/10 11:44:03 Administrator:
            //修改MidRoof螺丝孔逻辑，以最低450间距计算间距即可(Length-300d)/((int)(Length-300)/450-1)
            var midRoofNutNumber = (int)((length - 300d) / 450d);
            var midRoofNutDis = (length - 300d)/(midRoofNutNumber-1);
            swPart.ChangeDim("Dis@LPatternMidRoofNut", midRoofNutDis);
            #endregion

            #region 集水翻边
            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "RIGHT")) swComp.UnSuppress("DrainChannelRight");
            else swComp.Suppress("DrainChannelRight");

            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "LEFT")) swComp.UnSuppress("DrainChannelLeft");
            else swComp.Suppress("DrainChannelLeft");
            #endregion

            #region 油塞
            if (outlet == "LEFTTAP")
            {
                swComp.UnSuppress("DrainTapLeft");
                swComp.Suppress("DrainTapRight");
            }
            else if (outlet == "RIGHTTAP")
            {
                swComp.Suppress("DrainTapLeft");
                swComp.UnSuppress("DrainTapRight");
            }
            else
            {
                swComp.Suppress("DrainTapLeft");
                swComp.Suppress("DrainTapRight");
            }
            #endregion

            #region 排风口
            if (exNo == 1)
            {
                swComp.UnSuppress("OneSpigot");
                swComp.Suppress("TwoSpigots");
                swPart.ChangeDim("ToRight@SketchOneSpigot", exRightDis);
                swPart.ChangeDim("Length@SketchOneSpigot", exLength);
                swPart.ChangeDim("Width@SketchOneSpigot", exWidth);
            }
            else
            {
                swComp.Suppress("OneSpigot");
                swComp.UnSuppress("TwoSpigots");
                swPart.ChangeDim("ToRight@SketchTwoSpigots", exRightDis);
                swPart.ChangeDim("Dis@SketchTwoSpigots", exDis);
                swPart.ChangeDim("Length@SketchTwoSpigots", exLength);
                swPart.ChangeDim("Width@SketchTwoSpigots", exWidth);
            }
            #endregion

            #region 背靠背
            if (backToBack == "YES") swComp.UnSuppress("BackToBack");
            else swComp.Suppress("BackToBack");
            #endregion

            #region ANSUL
            if (ansul == "YES")
            {
                //侧喷
                if (anSide == "LEFT")
                {
                    swComp.UnSuppress("AnsulSideLeft");
                    swComp.UnSuppress("ChannelLeft");
                }
                else if (anSide == "RIGHT")
                {
                    swComp.UnSuppress("AnsulSideRight");
                    swComp.UnSuppress("ChannelRight");
                }
                else
                {
                    swComp.Suppress("AnsulSideLeft");
                    swComp.Suppress("AnsulSideRight");
                    swComp.Suppress("ChannelLeft");
                    swComp.Suppress("ChannelRight");
                }
                //探测器
                swComp.Suppress("AnsulDetectorRight");
                swComp.Suppress("AnsulDetectorLeft");
                if (anDetector == "RIGHT" || anDetector == "BOTH")
                {
                    swComp.UnSuppress("AnsulDetectorRight");
                }
                if (anDetector == "LEFT" || anDetector == "BOTH")
                {
                    swComp.UnSuppress("AnsulDetectorLeft");
                }
            }
            #endregion

            #region MARVEL
            if (marvel == "YES")
            {
                swComp.UnSuppress("MarvelNtc");
                if (exNo == 1) swPart.ChangeDim("ToRight@SketchMarvelNtc", exRightDis + exLength / 2d + 50d);
                else swPart.ChangeDim("ToRight@SketchMarvelNtc", exRightDis + exDis / 2d + exLength + 50d);
            }
            else swComp.Suppress("MarvelNtc");
            #endregion

            #region UVHood
            if (uvType == "LONG")
            {
                swComp.UnSuppress("UvRack");
                swPart.ChangeDim("ToRight@SketchUvRack", exRightDis);
                swPart.ChangeDim("UvRack@SketchUvRack", 1640d);
                swComp.UnSuppress("UvCable");
                swPart.ChangeDim("ToRight@SketchUvCable", exRightDis);
                swPart.ChangeDim("UvCable@SketchUvCable", 1500d);
            }
            else if (uvType == "SHORT")
            {
                swComp.UnSuppress("UvRack");
                swPart.ChangeDim("ToRight@SketchUvRack", exRightDis);
                swPart.ChangeDim("UvRack@SketchUvRack", 930d);
                swComp.UnSuppress("UvCable");
                swPart.ChangeDim("ToRight@SketchUvCable", exRightDis);
                swPart.ChangeDim("UvCable@SketchUvCable", 790d);
            }
            else
            {
                swComp.Suppress("UvRack");
                swComp.Suppress("UvCable");
            }
            #endregion
        }



        //排风腔前面板
        internal void FNHE0002(Component2 swComp, double length, string UVType, double exRightDis)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("Length@SketchBase", length);
            #region UVHood
            if (UVType == "LONG")
            {
                swComp.UnSuppress("TabUp");
                swComp.UnSuppress("SensorCable");
                swComp.Suppress("UvDoorShort");
                swComp.UnSuppress("UvDoorLong");
                swPart.ChangeDim("ToRight@SketchUvDoorShort", exRightDis);
                swComp.UnSuppress("UvCable");
                swPart.ChangeDim("ToRight@SketchUvCable", exRightDis);
                swPart.ChangeDim("UvCable@SketchUvCable", 1500d);
            }
            else if (UVType == "SHORT")
            {
                swComp.UnSuppress("TabUp");
                swComp.UnSuppress("SensorCable");
                swComp.UnSuppress("UvDoorShort");
                swComp.Suppress("UvDoorLong");
                swPart.ChangeDim("ToRight@SketchUvDoorLong", exRightDis);
                swComp.UnSuppress("UvCable");
                swPart.ChangeDim("ToRight@SketchUvCable", exRightDis);
                swPart.ChangeDim("UvCable@SketchUvCable", 790d);
            }
            else
            {
                swComp.Suppress("TabUp");
                swComp.Suppress("SensorCable");
                swComp.Suppress("UvDoorShort");
                swComp.Suppress("UvDoorLong");
                swComp.Suppress("UvCable");
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
                swPart.ChangeDim("Length@SketchBase", ksaSideLength * 2d);
            }
            else if (ksaSideLength < 25d && ksaSideLength >= 12d)
            {
                swComp = swAssy.UnSuppress(suffix, leftPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("Length@SketchBase", ksaSideLength * 2);
                swAssy.Suppress(suffix, rightPart);
                swAssy.Suppress(suffix, specialPart);
            }
            else
            {
                swComp = swAssy.UnSuppress(suffix, leftPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("Length@SketchBase", ksaSideLength);
                swComp = swAssy.UnSuppress(suffix, rightPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("Length@SketchBase", ksaSideLength);
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
                swPart.ChangeDim("Length@SketchBase", exLength / 2d + 10d);
                swPart.ChangeDim("Width@SketchBase", exWidth + 20d);
            }
            if (marvel == "YES") swAssy.Suppress(suffix, railPart2);
            else swAssy.UnSuppress(suffix, railPart2);
            if (marvel == "YES") swAssy.Suppress(suffix, railPart1);
            else
            {
                swComp = swAssy.UnSuppress(suffix, railPart1);
                swPart = swComp.GetModelDoc2();
                if (exNo == 1) swPart.ChangeDim("Length@Base-Flange", exLength * 2d + 100d);
                else swPart.ChangeDim("Length@Base-Flange", exLength * 3d + exDis + 100d);
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
                swPart.ChangeDim("Length@Base-Flange", exLength + 50d);
                swPart.ChangeDim("Height@SketchBase", exHeight);
                if (ansul == "YES") swComp.UnSuppress("Ansul");
                else swComp.Suppress("Ansul");

                swComp = swAssy.UnSuppress(suffix, backPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("Length@Base-Flange", exLength + 50d);
                swPart.ChangeDim("Height@SketchBase", exHeight);

                swComp = swAssy.UnSuppress(suffix, leftPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("Length@Base-Flange", exWidth);
                swPart.ChangeDim("Height@SketchBase", exHeight);

                swComp = swAssy.UnSuppress(suffix, rightPart);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("Length@Base-Flange", exWidth);
                swPart.ChangeDim("Height@SketchBase", exHeight);
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
                swComp.UnSuppress("AnsulDetector");
            }
            else if (ansul == "YES" && sidePanel == "LEFT")
            {
                swAssy.Suppress(suffix, leftPart);
                swComp = swAssy.UnSuppress(suffix, rightPart);
                swComp.UnSuppress("AnsulDetector");
            }
            else if (ansul == "YES" && sidePanel == "RIGHT")
            {
                swAssy.Suppress(suffix, rightPart);
                swComp = swAssy.UnSuppress(suffix, leftPart);
                swComp.FeatureByName("AnsulDetector");
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
            //MESH侧板长度(除去排风三角板3dm计算)1500
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
                //应该是（2 * meshSideLength）总长剩余1.5就没有小侧板
                if (2 * meshSideLength < 15d && 2 * meshSideLength > 1.5d) meshSideLength += 249d;
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
                else if (meshSideLength <= 30d && 2 * meshSideLength > 1.5d)
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


        //UVIM排风腔
        public void FNHE0017(Component2 swComp, double length, int midRoofHoleNo, double midRoofSecondHoleDis, int exNo, double exRightDis, double exLength, double exWidth, double exDis, string waterCollection, string sidePanel, string outlet, string ansul, string anSide, string anDetector, string marvel, string uvType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();//打开零件3
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D3@Sketch14", midRoofSecondHoleDis);
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
                swPart.ChangeDim("D3@Sketch12", exRightDis);
                swPart.ChangeDim("D1@Sketch12", exLength);
                swPart.ChangeDim("D2@Sketch12", exWidth);
            }
            else
            {
                swComp.Suppress("EXCOONE");
                swComp.UnSuppress("EXCOTWO");
                swPart.ChangeDim("D4@Sketch13", exRightDis);
                swPart.ChangeDim("D3@Sketch13", exDis);
                swPart.ChangeDim("D1@Sketch13", exLength);
                swPart.ChangeDim("D2@Sketch13", exWidth);
            }
            //MARVEL
            if (marvel == "YES")
            {
                swComp.Suppress("MA-TAB");
                swComp.UnSuppress("MA-NTC");
                if (exNo == 1) swPart.ChangeDim("D1@Sketch17", exRightDis + exLength / 2 + 50d);
                else swPart.ChangeDim("D1@Sketch17", exRightDis + exDis / 2 + exLength + 50d);
            }
            else
            {
                swComp.Suppress("MA-TAB");
                swComp.Suppress("MA-NTC");
            }
            //UVHood,UVRack-UV灯架孔和UV cable-UV灯线缆穿孔
            swComp.UnSuppress("UVRACK");
            swPart.ChangeDim("D7@Sketch2", exRightDis);
            //非UVHood
            if (uvType == "LONG") swPart.ChangeDim("D3@Sketch2", 1680d);
            else swPart.ChangeDim("D3@Sketch2", 975d);
            swComp.UnSuppress("UVCABLE");
            swPart.ChangeDim("D4@Sketch15", exRightDis);
            //非UVHood
            if (uvType == "LONG") swPart.ChangeDim("D2@Sketch15", 1120d);
            else swPart.ChangeDim("D2@Sketch15", 520d);
            //UVHood解压特征
            swComp.UnSuppress("UV-TAB");
            swComp.UnSuppress("KSACABLE");
            if (sidePanel == "LEFT" || sidePanel == "BOTH") swComp.UnSuppress("JUNCTION BOX UV");
            else swComp.Suppress("JUNCTION BOX UV");
            swComp.UnSuppress("JUNCTION BOX LIGHT");
        }

        public void FNHE0018(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length);
        }
        public void FNHE0019(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length);
        }
        public void FNHE0020(Component2 swComp, double length, string waterCollection, string sidePanel, string outlet)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1", length);
            //集水翻边
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
            //油塞
            if (outlet == "LEFTTAP")
            {
                swComp.Suppress("DRAINTAP-RIGHT");
                swComp.UnSuppress("DRAINTAP-LEFT");
            }
            else if (outlet == "RIGHTTAP")
            {
                swComp.UnSuppress("DRAINTAP-RIGHT");
                swComp.Suppress("DRAINTAP-LEFT");
            }
            else
            {
                swComp.Suppress("DRAINTAP-RIGHT");
                swComp.Suppress("DRAINTAP-LEFT");
            }
        }
        public void FNHE0021(Component2 swComp, string uvType)
        {
            //UV烟罩解压缩特征
            //非UVHood
            if (uvType != "NO") swComp.UnSuppress("Cut-Extrude2");
            else swComp.Suppress("Cut-Extrude2");
        }

        public void MHoodKSAFilter(AssemblyDoc swAssy, string suffix, double ksaSideLength, string leftPart1, string rightPart1, string specialPart1, string leftPart2, string rightPart2, string specialPart2)
        {
            Component2 swComp;
            ModelDoc2 swPart;
            if (ksaSideLength < 12d && ksaSideLength > 2d)
            {
                swAssy.Suppress(suffix, leftPart1);
                swAssy.Suppress(suffix, rightPart1);
                swComp = swAssy.UnSuppress(suffix, specialPart1);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D1@草图1", ksaSideLength * 2d);

                swAssy.Suppress(suffix, leftPart2);
                swAssy.Suppress(suffix, rightPart2);
                swComp = swAssy.UnSuppress(suffix, specialPart2);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D1@草图1", ksaSideLength * 2d);


            }
            else if (ksaSideLength < 25d && ksaSideLength >= 12d)
            {
                swComp = swAssy.UnSuppress(suffix, leftPart1);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@草图1", ksaSideLength * 2);
                swAssy.Suppress(suffix, rightPart1);
                swAssy.Suppress(suffix, specialPart1);

                swComp = swAssy.UnSuppress(suffix, leftPart2);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@草图1", ksaSideLength * 2);
                swAssy.Suppress(suffix, rightPart2);
                swAssy.Suppress(suffix, specialPart2);
            }
            else
            {
                swComp = swAssy.UnSuppress(suffix, leftPart1);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@草图1", ksaSideLength);
                swComp = swAssy.UnSuppress(suffix, rightPart1);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@草图1", ksaSideLength);
                swAssy.Suppress(suffix, specialPart1);

                swComp = swAssy.UnSuppress(suffix, leftPart2);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@草图1", ksaSideLength);
                swComp = swAssy.UnSuppress(suffix, rightPart2);
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@草图1", ksaSideLength);
                swAssy.Suppress(suffix, specialPart2);
            }
        }

        public void MHoodMeshFilter(AssemblyDoc swAssy, string suffix, double length, string ansul, string anSide, string leftPart1, string rightPart1, string leftPart2, string rightPart2)
        {
            //MESH侧板长度(除去排风三角板3dm计算)1500
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
                        swComp = swAssy.UnSuppress(suffix, leftPart1);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        swComp.UnSuppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, rightPart1);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        swComp.Suppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, leftPart2);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        swComp.UnSuppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, rightPart2);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        swComp.Suppress("ANSUL");

                    }
                    else if (anSide == "RIGHT")
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart1);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        swComp.Suppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, rightPart1);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        swComp.UnSuppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, leftPart2);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        swComp.Suppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, rightPart2);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        swComp.UnSuppress("ANSUL");
                    }
                    else
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart1);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        swComp.Suppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, rightPart1);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        swComp.Suppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, leftPart2);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                        swComp.Suppress("ANSUL");

                        swComp = swAssy.UnSuppress(suffix, rightPart2);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                        swComp.Suppress("ANSUL");
                    }
                }
                else
                {
                    if (anSide == "LEFT")
                    {
                        swComp = swAssy.UnSuppress(suffix, leftPart1);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength * 2d);
                        swComp.UnSuppress("ANSUL");

                        swAssy.Suppress(suffix, rightPart1);

                        swComp = swAssy.UnSuppress(suffix, leftPart2);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength * 2d);
                        swComp.UnSuppress("ANSUL");

                        swAssy.Suppress(suffix, rightPart2);
                    }
                    else if (anSide == "RIGHT")
                    {
                        swAssy.Suppress(suffix, leftPart1);

                        swComp = swAssy.UnSuppress(suffix, rightPart1);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength * 2d);
                        swComp.UnSuppress("ANSUL");

                        swAssy.Suppress(suffix, leftPart2);

                        swComp = swAssy.UnSuppress(suffix, rightPart2);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength * 2d);
                        swComp.UnSuppress("ANSUL");
                    }
                    else
                    {
                        swAssy.Suppress(suffix, leftPart1);

                        swComp = swAssy.UnSuppress(suffix, rightPart1);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength * 2d);
                        swComp.Suppress("ANSUL");

                        swAssy.Suppress(suffix, leftPart2);

                        swComp = swAssy.UnSuppress(suffix, rightPart2);
                        swPart = swComp.GetModelDoc2();
                        swPart.ChangeDim("D2@Sketch1", meshSideLength * 2d);
                        swComp.Suppress("ANSUL");
                    }
                }
            }
            else
            {
                //非ANSUL
                //应该是（2 * meshSideLength）总长剩余1.5就没有小侧板
                if (2 * meshSideLength < 15d && 2 * meshSideLength > 1.5d) meshSideLength += 249d;
                if (meshSideLength > 30d)
                {
                    swComp = swAssy.UnSuppress(suffix, leftPart1);
                    swPart = swComp.GetModelDoc2();
                    swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                    swComp.Suppress("ANSUL");

                    swComp = swAssy.UnSuppress(suffix, rightPart1);
                    swPart = swComp.GetModelDoc2();
                    swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                    swComp.Suppress("ANSUL");

                    swComp = swAssy.UnSuppress(suffix, leftPart2);
                    swPart = swComp.GetModelDoc2();
                    swPart.ChangeDim("D2@Sketch1", meshSideLength - 20d);
                    swComp.Suppress("ANSUL");

                    swComp = swAssy.UnSuppress(suffix, rightPart2);
                    swPart = swComp.GetModelDoc2();
                    swPart.ChangeDim("D2@Sketch1", meshSideLength + 20d);
                    swComp.Suppress("ANSUL");

                }
                else if (meshSideLength <= 30d && 2 * meshSideLength > 1.5d)
                {
                    swAssy.Suppress(suffix, leftPart1);

                    swComp = swAssy.UnSuppress(suffix, rightPart1);
                    swPart = swComp.GetModelDoc2();
                    swPart.ChangeDim("D2@Sketch1", meshSideLength * 2d);
                    swComp.Suppress("ANSUL");

                    swAssy.Suppress(suffix, leftPart2);

                    swComp = swAssy.UnSuppress(suffix, rightPart2);
                    swPart = swComp.GetModelDoc2();
                    swPart.ChangeDim("D2@Sketch1", meshSideLength * 2d);
                    swComp.Suppress("ANSUL");
                }
                else
                {
                    swAssy.Suppress(suffix, leftPart1);
                    swAssy.Suppress(suffix, rightPart1);
                    swAssy.Suppress(suffix, leftPart2);
                    swAssy.Suppress(suffix, rightPart2);
                }
            }
        }

        public void FNHE0022(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 8d);
        }

        public void FNHE0023(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 5d);
        }

        public void FNHE0024(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 3.5d);
        }

        public void FNHE0025(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 3.5d);
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
        public void FNHM0001(Component2 swComp, string exType, double length, double depth, double exRightDis, string lightType, int ledSpotNo, double ledSpotDis, string ansul, int anDropNo, double anYDis, double anDropDis1, double anDropDis2, double anDropDis3, double anDropDis4, double anDropDis5, string anDetectorEnd, int anDetectorNo, double anDetectorDis1, double anDetectorDis2, double anDetectorDis3, double anDetectorDis4, double anDetectorDis5, string bluetooth, string uvType, string marvel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();

            #region 基本尺寸
            //计算净宽度，总宽度减去排风，减去新风再减1
            var netWidth = depth - 535d - 360d - 1d;
            swPart.ChangeDim("Length@SketchBase", length - 4d);
            swPart.ChangeDim("Width@SketchBase", depth - 669d);//depth - 669d
            swPart.ChangeDim("Width@SketchWidth", netWidth);//depth - 896d

            //因为后方一点距离前端固定90，这里计算前端一点移动的距离
            var midRoofTopHoleDis = depth - 535d - 360d - 90d - (int)((depth - 535d - 360d - 90d - 100d) / 50d) * 50d;
            swPart.ChangeDim("Dis@SketchTopHole", midRoofTopHoleDis);
            //侧向连接孔
            swPart.ChangeDim("Dis@SketchSideHole", netWidth / 3d);

            #endregion

            #region MidRoof铆螺母孔
            //2023/3/10 11:44:03 Administrator:
            //修改MidRoof螺丝孔逻辑，以最低450间距计算间距即可(Length-300d)/((int)(Length-300)/450-1)
            var midRoofNutNumber = (int)((length - 300d) / 450d);
            var midRoofNutDis = (length - 300d)/(midRoofNutNumber-1);
            swPart.ChangeDim("Dis@LPatternMidRoofNut", midRoofNutDis);
            #endregion

            #region UVHood
            if (uvType == "LONG")
            {
                swComp.UnSuppress("UvCable");
                swPart.ChangeDim("ToRight@SketchUvCable", exRightDis);
                swPart.ChangeDim("UvCable@SketchUvCable", 1500d);
            }
            else if (uvType == "SHORT")
            {
                swComp.UnSuppress("UvCable");
                swPart.ChangeDim("ToRight@SketchUvCable", exRightDis);
                swPart.ChangeDim("UvCable@SketchUvCable", 790);
            }
            else
            {
                swComp.Suppress("UvCable");
            }
            #endregion

            #region 开方孔,UV或带MARVEL时解压
            //KsaTabCable，KSA感应线和测风压口
            //UvDoorCable，UV门感应线
            //BluetoothCable，蓝牙线出口（Logo走风机线，不需要）
            //CutFrontRight，风机线，都需要
            if (uvType!="LONG" && uvType!="SHORT")
            {
                //UV
                swComp.UnSuppress("KsaTabCable");
                swComp.UnSuppress("UvDoorCable");
            }
            else
            {
                //非UV
                if (marvel == "YES") swComp.UnSuppress("KsaTabCable");
                else swComp.Suppress("KsaTabCable");
                swComp.Suppress("UvDoorCable");
            }
            swComp.UnSuppress("CjFanCable");
            if (bluetooth == "YES") swComp.UnSuppress("BluetoothCable");
            else swComp.Suppress("BluetoothCable");
            #endregion

            #region 灯具选项
            if (lightType == "LED60")
            {
                swComp.Suppress("Led140");
                swComp.Suppress("LPatternLed140");
                swComp.Suppress("FsLong");
                swComp.Suppress("FsShort");
                swComp.UnSuppress("Led60");
                if (ledSpotNo == 1) swPart.ChangeDim("ToMiddle@SketchLed60", 0d);
                else
                {
                    swPart.ChangeDim("ToMiddle@SketchLed60", ledSpotDis * (ledSpotNo / 2d - 1d) + ledSpotDis / 2d);
                    swComp.UnSuppress("LPatternLed60");
                    swPart.ChangeDim("Number@LPatternLed60", ledSpotNo);
                    swPart.ChangeDim("Dis@LPatternLed60", ledSpotDis);
                }
            }
            else if (lightType == "LED140")
            {
                swComp.Suppress("Led60");
                swComp.Suppress("LPatternLed60");
                swComp.Suppress("FsLong");
                swComp.Suppress("FsShort");
                swComp.UnSuppress("Led140");
                if (ledSpotNo == 1) swPart.ChangeDim("ToMiddle@SketchLed140", 0d);
                else
                {
                    swPart.ChangeDim("ToMiddle@SketchLed140", ledSpotDis * (ledSpotNo / 2d - 1d) + ledSpotDis / 2d);
                    swComp.UnSuppress("LPatternLed140");
                    swPart.ChangeDim("Number@LPatternLed140", ledSpotNo);
                    swPart.ChangeDim("Dis@LPatternLed140", ledSpotDis);
                }
            }
            else if (lightType == "FSLONG")
            {
                swComp.Suppress("Led60");
                swComp.Suppress("LPatternLed60");
                swComp.Suppress("Led140");
                swComp.Suppress("LPatternLed140");
                swComp.UnSuppress("FsLong");
                swComp.Suppress("FsShort");
            }
            else if (lightType == "FSSHORT")
            {
                swComp.Suppress("Led60");
                swComp.Suppress("LPatternLed60");
                swComp.Suppress("Led140");
                swComp.Suppress("LPatternLed140");
                swComp.Suppress("FsLong");
                swComp.UnSuppress("FsShort");
            }
            else
            {
                swComp.Suppress("Led60");
                swComp.Suppress("LPatternLed60");
                swComp.Suppress("Led140");
                swComp.Suppress("LPatternLed140");
                swComp.Suppress("FsLong");
                swComp.Suppress("FsShort");
            }
            #endregion

            #region ANSUL选项
            swComp.Suppress("AnsulDrop1");
            swComp.Suppress("AnsulDrop2");
            swComp.Suppress("AnsulDrop3");
            swComp.Suppress("AnsulDrop4");
            swComp.Suppress("AnsulDrop5");
            //UW/KW HOOD 水洗烟罩探测器在midRoof上，非水洗烟罩压缩
            swComp.Suppress("AnsulDetector1");
            swComp.Suppress("AnsulDetector2");
            swComp.Suppress("AnsulDetector3");
            swComp.Suppress("AnsulDetector4");
            swComp.Suppress("AnsulDetector5");
            swComp.Suppress("AnsulDetectorAcross");
            if (ansul == "YES")
            {

                #region Ansul下喷
                if (anDropNo > 0)
                {
                    swComp.UnSuppress("AnsulDrop1");
                    swPart.ChangeDim("Dis@SketchAnsulDrop1", anDropDis1);
                    swPart.ChangeDim("DisY@SketchAnsulDrop1", anYDis - 360d);
                }
                if (anDropNo > 1)
                {
                    swComp.UnSuppress("AnsulDrop2");
                    swPart.ChangeDim("Dis@SketchAnsulDrop2", anDropDis2);
                }
                if (anDropNo > 2)
                {
                    swComp.UnSuppress("AnsulDrop3");
                    swPart.ChangeDim("Dis@SketchAnsulDrop3", anDropDis3);
                }
                if (anDropNo > 3)
                {
                    swComp.UnSuppress("AnsulDrop4");
                    swPart.ChangeDim("Dis@SketchAnsulDrop4", anDropDis4);
                }
                if (anDropNo > 4)
                {
                    swComp.UnSuppress("AnsulDrop5");
                    swPart.ChangeDim("Dis@SketchAnsulDrop5", anDropDis5);
                }
                #endregion


                #region Ansul探测器，水洗烟罩需要探测器安装在MidRoof上
                if (exType == "UW" || exType == "KW" || exType == "CMOD")
                {
                    //探测器
                    swComp.UnSuppress("AnsulDetectorAcross");
                    if (anDetectorNo > 0)
                    {
                        swComp.UnSuppress("AnsulDetector1");
                        swPart.ChangeDim("Dis@SketchAnsulDetector1", anDetectorDis1);
                        if (anDetectorEnd == "LEFT" || (anDetectorEnd == "RIGHT" && anDetectorNo == 1))
                            swPart.ChangeDim("Length@SketchAnsulDetector1", 195d);
                        else swPart.ChangeDim("Length@SketchAnsulDetector1", 175d);
                    }
                    if (anDetectorNo > 1)
                    {
                        swComp.UnSuppress("AnsulDetector2");
                        swPart.ChangeDim("Dis@SketchAnsulDetector2", anDetectorDis2);
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 2)
                            swPart.ChangeDim("Length@SketchAnsulDetector2", 195d);
                        else swPart.ChangeDim("Length@SketchAnsulDetector2", 175d);
                    }
                    if (anDetectorNo > 2)
                    {
                        swComp.UnSuppress("AnsulDetector3");
                        swPart.ChangeDim("Dis@SketchAnsulDetector3", anDetectorDis3);
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 3)
                            swPart.ChangeDim("Length@SketchAnsulDetector3", 195d);
                        else swPart.ChangeDim("Length@SketchAnsulDetector3", 175d);
                    }
                    if (anDetectorNo > 3)
                    {
                        swComp.UnSuppress("AnsulDetector4");
                        swPart.ChangeDim("Dis@SketchAnsulDetector4", anDetectorDis4);
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 4)
                            swPart.ChangeDim("Length@SketchAnsulDetector4", 195d);
                        else swPart.ChangeDim("Length@SketchAnsulDetector4", 175d);
                    }
                    if (anDetectorNo > 4)
                    {
                        swComp.UnSuppress("AnsulDetector5");
                        swPart.ChangeDim("Dis@SketchAnsulDetector5", anDetectorDis5);
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 5)
                            swPart.ChangeDim("Length@SketchAnsulDetector5", 195d);
                        else swPart.ChangeDim("Length@SketchAnsulDetector5", 175d);
                    }
                }
                else
                {
                    //UW/KW HOOD 水洗烟罩探测器在midRoof上，非水洗烟罩压缩
                    swComp.Suppress("AnsulDetector1");
                    swComp.Suppress("AnsulDetector2");
                    swComp.Suppress("AnsulDetector3");
                    swComp.Suppress("AnsulDetector4");
                    swComp.Suppress("AnsulDetector5");
                    swComp.Suppress("AnsulDetectorAcross");
                }
                #endregion
            }
            #endregion

            #region IR
            if (marvel == "YES")
            {
                swComp.UnSuppress("IrLhc2");
                swComp.UnSuppress("Ir2");
            }
            else
            {
                swComp.Suppress("IrLhc2");
                swComp.Suppress("Ir2");
            }
            #endregion

            #region CMOD NTC Sensor
            if (exType == "CMOD") swComp.UnSuppress("NtcSensor");
            else swComp.Suppress("NtcSensor");
            #endregion
        }






        //----------灯板加强筋----------
        internal void FNHM0006(Component2 swComp, double depth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            //计算净宽度，总宽度减去排风，减去新风再减1
            var netWidth = depth - 535d - 360d - 1d;
            swPart.ChangeDim("Length@Base-Flange", netWidth-3d);
        }


        //UVIM
        public void FNHM0011(Component2 swComp, string exType, double length, double depth, double exHeight,
            double suHeight, double exRightDis, double midRoofTopHoleDis, double midRoofSecondHoleDis,
            int midRoofHoleNo, string lightType, double lightYDis, int ledSpotNo, double ledSpotDis, string ansul,
            int anDropNo, double anYDis, double anDropDis1, double anDropDis2, double anDropDis3, double anDropDis4,
            double anDropDis5, string anDetectorEnd, int anDetectorNo, double anDetectorDis1, double anDetectorDis2,
            double anDetectorDis3, double anDetectorDis4, double anDetectorDis5, string bluetooth, string uvType,
            string marvel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            #region 基本尺寸
            swPart.ChangeDim("D1@草图1", length - 4d);
            swPart.ChangeDim("D2@草图1", (depth - 800d - 260d) / 2d - 3d + 226d);
            swPart.ChangeDim("D1@草图6", (depth - 800d - 260d) / 2d - 3d + 1d);
            swPart.ChangeDim("D3@草图25", midRoofTopHoleDis);
            if ((depth - 800d - 260d) / 2d < 150d)
                swPart.ChangeDim("D2@草图26", ((depth - 800d - 260d) / 2d + 55d) / 3d - 30d);
            else
                swPart.ChangeDim("D2@草图26", ((depth - 800d - 260d) / 2d + 55d) / 3d);

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
            //swComp.Suppress("ANDTECACROSS");
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
            //M型烟罩从上面出线，因此压缩掉
            swComp.Suppress("UVCABLE");
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
        }

        public void FNHM0012(Component2 swComp, string exType, double length, double depth, double exHeight,
            double suHeight, double exRightDis, double midRoofTopHoleDis, double midRoofSecondHoleDis,
            int midRoofHoleNo, string lightType, double lightYDis, int ledSpotNo, double ledSpotDis, string ansul,
            int anDropNo, double anYDis, double anDropDis1, double anDropDis2, double anDropDis3, double anDropDis4,
            double anDropDis5, string anDetectorEnd, int anDetectorNo, double anDetectorDis1, double anDetectorDis2,
            double anDetectorDis3, double anDetectorDis4, double anDetectorDis5, string bluetooth, string uvType,
            string marvel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            #region 基本尺寸
            swPart.ChangeDim("D1@草图1", length - 4d);
            swPart.ChangeDim("D2@草图1", (depth - 800d - 260d) / 2d - 3d + 226d);
            swPart.ChangeDim("D1@草图6", (depth - 800d - 260d) / 2d - 3d + 1d);
            swPart.ChangeDim("D3@草图25", midRoofTopHoleDis);
            if ((depth - 800d - 260d) / 2d < 150d)
                swPart.ChangeDim("D2@草图26", ((depth - 800d - 260d) / 2d + 55d) / 3d - 30d);
            else
                swPart.ChangeDim("D2@草图26", ((depth - 800d - 260d) / 2d + 55d) / 3d);

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
            //swComp.Suppress("ANDTECACROSS");

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
            //M型烟罩从上面出线，因此压缩掉
            swComp.Suppress("UVCABLE");
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

        #region SidePanel 烟罩大侧板模块

        //普通烟罩大侧板
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
                FNHS0001(swComp, depth, exHeight, sidePanelSideCjNo);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0002(swComp, depth, exHeight);
                //RIGHT
                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0003(swComp, depth, exHeight, sidePanelSideCjNo);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0004(swComp, depth, exHeight);
            }
            else if (sidePanel == "LEFT")
            {
                swAssy.Suppress(suffix, rightOuter);
                swAssy.Suppress(suffix, rightInner);

                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FNHS0001(swComp, depth, exHeight, sidePanelSideCjNo);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0002(swComp, depth, exHeight);
            }
            else if (sidePanel == "RIGHT")
            {
                swAssy.Suppress(suffix, leftOuter);
                swAssy.Suppress(suffix, leftInner);

                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0003(swComp, depth, exHeight, sidePanelSideCjNo);
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

        internal void FNHS0001(Component2 swComp, double depth, double exHeight, int sidePanelSideCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            //todo:if,backcj,+90
            swPart.ChangeDim("Length@SketchBase", depth);
            swPart.ChangeDim("Height@SketchBase", exHeight);
            swPart.ChangeDim("CjSide@CjSide", sidePanelSideCjNo);
        }
        internal void FNHS0002(Component2 swComp, double depth, double exHeight)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("Length@SketchBase", depth);
            swPart.ChangeDim("Height@SketchBase", exHeight);
            if (exHeight == 555d || exHeight == 650d)
            {
                swComp.UnSuppress("FI555");
                swComp.Suppress("FI450");
                swComp.Suppress("FI400");
            }
            else if (exHeight == 450d)
            {
                swComp.Suppress("FI555");
                swComp.UnSuppress("FI450");
                swComp.Suppress("FI400");
            }
            else if (exHeight == 400d)
            {
                swComp.Suppress("FI555");
                swComp.Suppress("FI450");
                swComp.UnSuppress("FI400");
            }
            else
            {
                swComp.Suppress("FI555");
                swComp.Suppress("FI450");
                swComp.Suppress("FI400");
            }
        }
        internal void FNHS0003(Component2 swComp, double depth, double exHeight, int sidePanelSideCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("Length@SketchBase", depth);
            swPart.ChangeDim("Height@SketchBase", exHeight);
            swPart.ChangeDim("CjSide@CjSide", sidePanelSideCjNo);
        }
        internal void FNHS0004(Component2 swComp, double depth, double exHeight)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("Length@SketchBase", depth);
            swPart.ChangeDim("Height@SketchBase", exHeight);
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
            swPart.ChangeDim("Width@SketchHood", depth);

            swPart.ChangeDim("SuHeight@SketchHood", suHeight);//新风
            swPart.ChangeDim("ExHeight@SketchHood", exHeight);//排风

            // KVUV555排风尺寸，ExHeitht555，ExButton76.5，ExFront85,ExAngle135
            // KWUW排风尺寸，ExHeitht555，ExButton150，ExFront101,ExAngle145
            // 450高度排风没有水洗，ExHeitht450，ExButton105，ExFront50,ExAngle135
            // 角度特殊，不能除以1000,应当乘回去
            var exButton = exHeight.Equals(450d)? 105d: exType == "W"? 150d : 76.5d;
            var exFront = exHeight.Equals(450d) ? 50d : exType == "W" ? 101d : 85d;
            var exAngle = (exHeight.Equals(450d) ? 135d : exType == "W" ? 145d : 135d)*1000d* Math.PI/ 180d;
            
            swPart.ChangeDim("ExButton@SketchHood", exButton);//EX450,105
            swPart.ChangeDim("ExFront@SketchHood", exFront);//EX450,50
            swPart.ChangeDim("ExAngle@SketchHood", exAngle);


        }
        internal void FNHS0006(Component2 swComp, double depth, double exHeight, double suHeight, string exType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("Width@SketchHood", depth);

            swPart.ChangeDim("SuHeight@SketchHood", suHeight);//新风
            swPart.ChangeDim("ExHeight@SketchHood", exHeight);//排风

            // KVUV555排风尺寸，ExHeitht555，ExButton76.5，ExFront85,ExAngle135
            // KWUW排风尺寸，ExHeitht555，ExButton150，ExFront101,ExAngle145
            // 450高度排风没有水洗，ExHeitht450，ExButton105，ExFront50,ExAngle135
            // 角度特殊，不能除以1000,应当乘回去
            var exButton = exHeight.Equals(450d) ? 105d : exType == "W" ? 150d : 76.5d;
            var exFront = exHeight.Equals(450d) ? 50d : exType == "W" ? 101d : 85d;
            var exAngle = (exHeight.Equals(450d) ? 135d : exType == "W" ? 145d : 135d)*1000d* Math.PI/ 180d;

            swPart.ChangeDim("ExButton@SketchHood", exButton);//EX450,105
            swPart.ChangeDim("ExFront@SketchHood", exFront);//EX450,50
            swPart.ChangeDim("ExAngle@SketchHood", exAngle);
        }

        internal void SidePanelSpecial(AssemblyDoc swAssy, string suffix, string sidePanel, double depth, double exHeight, double suHeight, string waterCollection, string exType, string leftOuter, string leftInner, string rightOuter, string rightInner, string leftCollection, string rightCollection)
        {
            //UVF555400斜侧板CJ孔计算,77为排风底部长度，555-400为高度差
            int sidePanelDownCjNo = (int)((Math.Sqrt(Math.Pow(depth - 77d, 2) + Math.Pow(exHeight - suHeight, 2)) - 95d) / 32d);
            if (exType == "W") sidePanelDownCjNo = (int)((Math.Sqrt(Math.Pow(depth - 150d, 2) + Math.Pow(555d - 400d, 2)) - 95d) / 32d);
            //侧面多余一个CJ孔导致干涉
            int sidePanelSideCjNo = sidePanelDownCjNo - 4;
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


        //M型普通烟罩大侧板
        internal void MHoodSidePanel(AssemblyDoc swAssy, string suffix, string sidePanel, double depth, double exHeight, string waterCollection, string suType, string leftOuter, string leftInner, string rightOuter, string rightInner, string leftCollection, string rightCollection)
        {
            int sidePanelSideCjNo = (int)((depth - 217d) / 32d) - 5;
            int sidePanelDownCjNo = sidePanelSideCjNo + 6;
            Component2 swComp;
            #region 大侧板
            if (sidePanel == "BOTH")
            {
                //LEFT
                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FNHS0007(swComp, depth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0008(swComp, depth, exHeight, suType);
                //RIGHT
                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0007(swComp, depth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0009(swComp, depth, exHeight, suType);
            }
            else if (sidePanel == "LEFT")
            {
                swAssy.Suppress(suffix, rightOuter);
                swAssy.Suppress(suffix, rightInner);

                swComp = swAssy.UnSuppress(suffix, leftOuter);
                FNHS0007(swComp, depth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, leftInner);
                FNHS0008(swComp, depth, exHeight, suType);
            }
            else if (sidePanel == "RIGHT")
            {
                swAssy.Suppress(suffix, leftOuter);
                swAssy.Suppress(suffix, leftInner);

                swComp = swAssy.UnSuppress(suffix, rightOuter);
                FNHS0007(swComp, depth, exHeight, sidePanelSideCjNo, sidePanelDownCjNo);
                swComp = swAssy.UnSuppress(suffix, rightInner);
                FNHS0009(swComp, depth, exHeight, suType);
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
                FNHS0010(swComp, depth);
            }
            else
            {
                swAssy.Suppress(suffix, leftCollection);
            }
            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "RIGHT"))
            {
                swComp = swAssy.UnSuppress(suffix, rightCollection);
                FNHS0010(swComp, depth);
            }
            else
            {
                swAssy.Suppress(suffix, rightCollection);
            }
            #endregion
        }

        internal void FNHS0007(Component2 swComp, double depth, double exHeight, int sidePanelSideCjNo,
            int sidePanelDownCjNo)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            //Todo:判断高度
            swPart.ChangeDim("D1@草图1", depth);
            swPart.ChangeDim("D1@阵列(线性)1", sidePanelSideCjNo);
            swPart.ChangeDim("D1@阵列(线性)2", sidePanelDownCjNo);
            swPart.ChangeDim("D6@草图33", (depth - (sidePanelSideCjNo + 5) * 32d) / 2d);
        }

        internal void FNHS0008(Component2 swComp, double depth, double exHeight, string suType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", depth - 79d);
            //Todo:判断高度
            if (suType == "MR")
            {
                swComp.UnSuppress("R555");
                swComp.Suppress("T555");
            }
            else if (suType == "MT")
            {

                swComp.Suppress("R555");
                swComp.UnSuppress("T555");
            }
            else
            {
                swComp.Suppress("R555");
                swComp.Suppress("T555");
            }
        }

        internal void FNHS0009(Component2 swComp, double depth, double exHeight, string suType)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", depth - 79d);
            //Todo:判断高度
            if (suType == "MR")
            {
                swComp.UnSuppress("R555");
                swComp.Suppress("T555");
            }
            else if (suType == "MT")
            {

                swComp.Suppress("R555");
                swComp.UnSuppress("T555");
            }
            else
            {
                swComp.Suppress("R555");
                swComp.Suppress("T555");
            }
        }

        internal void FNHS0010(Component2 swComp, double depth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@Base-Flange1", depth - 308d);
        }

        #endregion

        #region Supply新风腔模块
        //------------I型新风腔主体----------
        internal void FNHA0001(Component2 swComp, double length, double width, string marvel, string sidePanel, string uvType, string bluetooth)
        {
            #region 基本尺寸
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("Length@Base-Flange", length);
            //吊装孔
            //因为后方一点距离前端固定90，这里计算前端一点移动的距离
            var midRoofTopHoleDis = width - 535d - 360d - 90d - (int)((width - 535d - 360d - 90d - 100d) / 50d) * 50d;
            swPart.ChangeDim("Dis@SketchTopHole", 200d - midRoofTopHoleDis);
            #endregion
            #region MidRoof铆螺母孔
            //2023/3/10 11:44:03 Administrator:
            //修改MidRoof螺丝孔逻辑，以最低450间距计算间距即可(Length-300d)/((int)(Length-300)/450-1)
            var midRoofNutNumber = (int)((length - 300d) / 450d);
            var midRoofNutDis = (length - 300d)/(midRoofNutNumber-1);
            swPart.ChangeDim("Dis@LPatternMidRoofNut", midRoofNutDis);
            #endregion

            #region 新风前面板卡口，距离与铆螺母数量相同，无需重复计算
            swPart.ChangeDim("Dis@LPatternPlug", midRoofNutDis);
            #endregion

            #region IR
            if (marvel == "YES") swComp.UnSuppress("IrLhc2");
            else swComp.Suppress("IrLhc2");
            #endregion

            #region UV HOOD
            if (uvType !="NO")
            {
                if (bluetooth == "YES") swComp.UnSuppress("BluetoothCable");
                else swComp.Suppress("BluetoothCable");
                if (sidePanel == "LEFT" || sidePanel == "BOTH") swComp.UnSuppress("JunctionBoxUv");
                else swComp.Suppress("JunctionBoxUv");
            }
            else
            {
                swComp.Suppress("BluetoothCable");
                if (marvel == "YES") swComp.UnSuppress("JunctionBoxUv");
                else swComp.Suppress("JunctionBoxUv");
            }
            #endregion
        }
        //----------I新风底部CJ孔板----------
        internal void FNHA0002(Component2 swComp, double length, string bluetooth, string ledLogo, string waterCollection, string sidePanel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            //新风面板螺丝孔数量及间距,最小间距900，距离边缘150
            int frontPanelNutNo = (int)((length - 300d) / 900d) + 2;
            double frontPanelNutDis = (length - 300d) / (frontPanelNutNo - 1);
            //新风CJ孔数量和新风CJ孔第一个CJ距离边缘距离
            int frontCjNo = (int)((length - 30d) / 32d) + 1;
            double frontCjFirstDis = (length - (frontCjNo - 1) * 32d) / 2d;
            #region 基本尺寸
            swPart.ChangeDim("Length@Base-Flange", length);
            //第一个CJ孔距离边缘
            swPart.ChangeDim("Dis@SketchCjSide", frontCjFirstDis);
            #endregion

            #region 前面板螺丝孔
            swPart.ChangeDim("Dis@LPatternFrontPanelNut", frontPanelNutDis);
            #endregion

            #region Logo与蓝牙
            if (bluetooth == "YES") swComp.UnSuppress("Bluetooth");
            else swComp.Suppress("Bluetooth");
            if (ledLogo == "YES") swComp.UnSuppress("Logo");
            else swComp.Suppress("Logo");
            #endregion


            #region 集水翻边
            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "RIGHT")) swComp.UnSuppress("DrainChannelRight");
            else swComp.Suppress("DrainChannelRight");

            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "LEFT")) swComp.UnSuppress("DrainChannelLeft");
            else swComp.Suppress("DrainChannelLeft");
            #endregion
        }


        //----------I新风前面板----------
        public void FNHA0003(Component2 swComp, double length)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();

            //新风面板螺丝孔数量及间距,最小间距900，距离边缘150
            int frontPanelNutNo = (int)((length - 300d) / 900d) + 2;
            double frontPanelNutDis = (length - 300d) / (frontPanelNutNo - 1);
            var midRoofNutNumber = (int)((length - 300d) / 450d);
            var midRoofNutDis = (length - 300d)/(midRoofNutNumber-1);

            swPart.ChangeDim("Length@SketchBase", length - 2d);
            #region 新风前面板卡口，距离与铆螺母数量相同，无需重复计算
            swPart.ChangeDim("Dis@LPatternPlug", midRoofNutDis);
            #endregion
            #region 前面板螺丝孔
            swPart.ChangeDim("Dis@LPatternFrontPanelNut", frontPanelNutDis);
            #endregion
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
           int midRoofHoleNo, double midRoofSecondHoleDis, double midRoofTopHoleDis, string marvel, string UVType, string bluetooth, string sidePanel)
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

            #region IR
            if (marvel == "YES") swComp.UnSuppress("Cut-Extrude12");
            else swComp.Suppress("Cut-Extrude12");
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
        //外圆
        public void FNHA0048(Component2 swComp, double depth)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", depth / 2d - 1d);
            swPart.ChangeDim("D3@Sketch1", depth / 2d - 2d);//外半径-2
        }
        //内圆
        public void FNHA0049(Component2 swComp, double depth)
        {
            //圆环新风
            double innerRadius = depth / 2d - 90d;
            double alpha = Math.Asin(400d/innerRadius);
            double beta = Math.PI - 2 * alpha;

            double innerRound = beta * innerRadius - 4d;
            int innerCjNo = (int)((innerRound - 30d) / 32d) + 1;
            double innerCjFirstDis = (innerRound - (innerCjNo - 1) * 32d) / 2d;

            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", depth / 2d - 90d + 1d);
            swPart.ChangeDim("D3@Sketch1", depth / 2d - 90d + 1d-0.5d-400d);//内半径-400.5
            swPart.ChangeDim("D3@Sketch6", innerCjFirstDis);
            swPart.ChangeDim("D1@LPattern1", innerCjNo);
        }

        public void FNHA0050(Component2 swComp, double depth)
        {
            double innerRadius = depth / 2d - 90d;
            double alpha = Math.Asin(400d/innerRadius);
            double beta = Math.PI - 2 * alpha;

            double downMidRound = beta * (innerRadius + 45d)-16d;
            int downCjNo = (int)((downMidRound - 30d) / 32d)+ 1;
            double downCjFirstDis = (downMidRound - (downCjNo -1) * 32d) / 2d;

            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@Sketch1", depth / 2d - 1d);
            swPart.ChangeDim("D4@Sketch1", depth / 2d - 90d +1d);

            swPart.ChangeDim("D3@Sketch2", depth - 90d);
            swPart.ChangeDim("D2@Sketch2", downCjFirstDis);
            swPart.ChangeDim("D1@CirPattern1", downCjNo);
            swPart.ChangeDim("D3@CirPattern1", beta*1000d);
        }

        //ding
        public void FNHA0051(Component2 swComp, double depth, double exBeamLength)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D6@Sketch1", depth / 2d - 1d);
            swPart.ChangeDim("D3@Sketch1", depth / 2d - 90d +1d);
            swPart.ChangeDim("D2@Sketch3", depth / 2d - 45d);
            swPart.ChangeDim("D1@Sketch3", exBeamLength - 50d);
        }



        //M型烟罩方形新风腔
        public void FNHA0012(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo, string marvel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D1@阵列(线性)1", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)1", frontPanelKaKouDis);
            swPart.ChangeDim("D1@Sketch2", midRoofSecondHoleDis);
            swPart.ChangeDim("D1@Sketch8", 150d - midRoofTopHoleDis);
            if (midRoofHoleNo == 1) swComp.Suppress("LPattern1");
            else
            {
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D1@LPattern1", midRoofHoleNo);
            }
            #region IR
            if (marvel == "YES") swComp.UnSuppress("IR-LHC-2");
            else swComp.Suppress("IR-LHC-2");
            #endregion
        }
        public void FNHA0013(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo, string marvel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D1@阵列(线性)1", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)1", frontPanelKaKouDis);
            swPart.ChangeDim("D1@Sketch2", midRoofSecondHoleDis);
            swPart.ChangeDim("D1@Sketch8", 150d  - midRoofTopHoleDis);
            if (midRoofHoleNo == 1) swComp.Suppress("LPattern1");
            else
            {
                swComp.UnSuppress("LPattern1");
                swPart.ChangeDim("D1@LPattern1", midRoofHoleNo);
            }
            #region IR
            if (marvel == "YES") swComp.UnSuppress("IR-LHC-2");
            else swComp.Suppress("IR-LHC-2");
            #endregion
        }
        internal void FNHA0016(Component2 swComp, double length, int frontCjNo, double frontCjFirstDis, int frontPanelHoleNo, double frontPanelHoleDis, string bluetooth, string ledLogo, string waterCollection, string sidePanel)
        {
            ModelDoc2 swPart = swComp.GetModelDoc2();

            #region 基本尺寸
            swPart.ChangeDim("D2@基体-法兰1", length);
            swPart.ChangeDim("D1@CJHOLES", frontCjNo);
            swPart.ChangeDim("D10@草图8", frontCjFirstDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
            swComp.Suppress("BLUETOOTH");
            swComp.Suppress("LOGO");
            #endregion

            #region 集水翻边
            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "RIGHT")) swComp.UnSuppress("DRAINCHANNEL-RIGHT");
            else swComp.Suppress("DRAINCHANNEL-RIGHT");

            if (waterCollection == "YES" && (sidePanel == "BOTH" || sidePanel == "LEFT")) swComp.UnSuppress("DRAINCHANNEL-LEFT");
            else swComp.Suppress("DRAINCHANNEL-LEFT");
            #endregion
        }

        #endregion
    }
}
