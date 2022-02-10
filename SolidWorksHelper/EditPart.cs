using SolidWorks.Interop.sldworks;
using System;

namespace SolidWorksHelper
{
    public class EditPart
    {
        ModelDoc2 swPart;
        Feature swFeat;

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





        #region Hood SidePanel 普通烟罩大侧板

        /// <summary>
        /// 左边大侧板外板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        /// <param name="sidePanelSideCjNo">侧板侧向CJ孔数量</param>
        /// <param name="sidePanelDownCjNo">侧板垂直CJ孔数量</param>
        public void FNHS0001(Component2 swComp, double deepth, double height, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000d;
            swPart.Parameter("D2@草图1").SystemValue = height / 1000d;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
        }
        /// <summary>
        /// 左边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0002(Component2 swComp, double deepth, double height)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = (deepth - 79d) / 1000d;
            swPart.Parameter("D2@草图1").SystemValue = (height - 39d) / 1000d;
            if (height == 555d || height == 650d)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else if (height == 450d)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else if (height == 400d)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
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
        public void FNHS0003(Component2 swComp, double deepth, double height, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000d;
            swPart.Parameter("D2@草图1").SystemValue = height / 1000d;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
        }
        /// <summary>
        /// 右边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0004(Component2 swComp, double deepth, double height)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = (deepth - 79d) / 1000d;
            swPart.Parameter("D2@草图1").SystemValue = (height - 39d) / 1000d;
            if (height == 555d || height == 650d)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            }
            else if (height == 450d)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else if (height == 400d)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
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
        public void FNHS0005(Component2 swComp, double deepth, double exHeight, double suHeight, string exType)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@Base-Flange1").SystemValue = deepth / 1000d;

            swPart.Parameter("D3@Sketch11").SystemValue = deepth / 1000d;

            swPart.Parameter("D2@Sketch11").SystemValue = suHeight / 1000d;//新风
            swPart.Parameter("D8@Sketch11").SystemValue = exHeight / 1000d;//排风
            if (exHeight == 450d)
            {
                swPart.Parameter("D6@Sketch11").SystemValue = 105d / 1000d;//EX450,105
                swPart.Parameter("D11@Sketch11").SystemValue = 50d / 1000d;//EX450,50
                swPart.Parameter("D9@Sketch11").SystemValue = 135d / 180d * Math.PI;//EX450,135
            }
            else
            {
                if (exType == "W")
                {
                    swPart.Parameter("D6@Sketch11").SystemValue = 150d / 1000d;//普通76.5水洗150
                    swPart.Parameter("D11@Sketch11").SystemValue = 101d / 1000d;//普通85水洗101
                    swPart.Parameter("D9@Sketch11").SystemValue = 145d / 180d * Math.PI;//普通135水洗145
                }
                else
                {
                    swPart.Parameter("D6@Sketch11").SystemValue = 76.5d / 1000d;//普通76.5水洗150
                    swPart.Parameter("D11@Sketch11").SystemValue = 85d / 1000d;//普通85水洗101
                    swPart.Parameter("D9@Sketch11").SystemValue = 135d / 180d * Math.PI;//普通135水洗145
                }
            }
        }
        /// <summary>
        /// 右集水翻边
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="suHeight">新风高度</param>
        /// <param name="exHeight">排风高度</param>
        /// <param name="exType">排风腔类型，"V"还是"W"</param>
        public void FNHS0006(Component2 swComp, double deepth, double exHeight, double suHeight, string exType)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@Base-Flange1").SystemValue = deepth / 1000d;

            swPart.Parameter("D3@Sketch11").SystemValue = deepth / 1000d;

            swPart.Parameter("D2@Sketch11").SystemValue = suHeight / 1000d;//新风
            swPart.Parameter("D8@Sketch11").SystemValue = exHeight / 1000d;//排风
            if (exHeight == 450d)
            {
                swPart.Parameter("D6@Sketch11").SystemValue = 105d / 1000d;//EX450,105
                swPart.Parameter("D11@Sketch11").SystemValue = 50d / 1000d;//EX450,50
                swPart.Parameter("D9@Sketch11").SystemValue = 135d / 180d * Math.PI;//EX450,135
            }
            else
            {
                if (exType == "W")
                {
                    swPart.Parameter("D6@Sketch11").SystemValue = 150d / 1000d;//普通76.5水洗150
                    swPart.Parameter("D11@Sketch11").SystemValue = 101d / 1000d;//普通85水洗101
                    swPart.Parameter("D9@Sketch11").SystemValue = 145d / 180d * Math.PI;//普通135水洗145
                }
                else
                {
                    swPart.Parameter("D6@Sketch11").SystemValue = 76.5d / 1000d;//普通76.5水洗150
                    swPart.Parameter("D11@Sketch11").SystemValue = 85d / 1000d;//普通85水洗101
                    swPart.Parameter("D9@Sketch11").SystemValue = 135d / 180d * Math.PI;//普通135水洗145
                }
            }
        }

        #endregion


        #region 新风腔零件
        public void FNHA0001(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo,  string MARVEL, int IRNo, double IRDis1, double IRDis2, double IRDis3, string sidePanel, string exType, string bluetooth)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@基体-法兰1").SystemValue = length / 1000d;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = frontPanelKaKouNo;
            swPart.Parameter("D3@阵列(线性)1").SystemValue = frontPanelKaKouDis / 1000d;
            swPart.Parameter("D3@Sketch3").SystemValue = midRoofSecondHoleDis;
            swPart.Parameter("D9@草图7").SystemValue = 200d / 1000d - midRoofTopHoleDis;
            swFeat = swComp.FeatureByName("LPattern1");
            if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            else
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
            }
            //MARVEL
            if (MARVEL == "YES")
            {
                if (IRNo > 0)
                {
                    swFeat = swComp.FeatureByName("MA1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch14").SystemValue = IRDis1 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch17").SystemValue = IRDis1 / 1000d;
                }
                if (IRNo > 1)
                {
                    swFeat = swComp.FeatureByName("MA2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch15").SystemValue = IRDis2 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch18").SystemValue = IRDis2 / 1000d;
                }
                if (IRNo > 2)
                {
                    swFeat = swComp.FeatureByName("MA3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch16").SystemValue = IRDis3 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch19").SystemValue = IRDis3 / 1000d;
                }
            }
            else
            {
                swFeat = swComp.FeatureByName("MA1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MA2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MA3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            if (exType == "UV") //"UV"/"KV"
            {
                //UV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                if (bluetooth == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (sidePanel == "LEFT" || sidePanel == "BOTH") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                //KV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (MARVEL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩  
            }

        }
        public void FNHA0004(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo, double suDis, int suNo, string MARVEL, int IRNo, double IRDis1, double IRDis2, double IRDis3, string sidePanel, string exType, string bluetooth)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@基体-法兰1").SystemValue = length / 1000d;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = frontPanelKaKouNo;
            swPart.Parameter("D3@阵列(线性)1").SystemValue = frontPanelKaKouDis / 1000d;
            swPart.Parameter("D3@Sketch4").SystemValue = midRoofSecondHoleDis;
            swPart.Parameter("D1@Sketch11").SystemValue = 200d / 1000d - midRoofTopHoleDis;
            swFeat = swComp.FeatureByName("LPattern1");
            if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            else
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
            }
            //新风脖颈
            swPart.Parameter("D3@Sketch6").SystemValue = (suDis * (suNo / 2 - 1) + suDis / 2d) / 1000d;
            swFeat = swComp.FeatureByName("LPattern2");
            if (suNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            else
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@LPattern2").SystemValue = suNo;
                swPart.Parameter("D3@LPattern2").SystemValue = suDis / 1000d;
            }
            //MARVEL
            if (MARVEL == "YES")
            {
                if (IRNo > 0)
                {
                    swFeat = swComp.FeatureByName("MA1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch16").SystemValue = IRDis1 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    if (suNo == 1) swPart.Parameter("D1@Sketch19").SystemValue = (length + 400) / 2000d;
                    else swPart.Parameter("D1@Sketch19").SystemValue = length / 2000d;
                }
                if (IRNo > 1)
                {
                    swFeat = swComp.FeatureByName("MA2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch17").SystemValue = IRDis2 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch19").SystemValue = (length / 2 - 25d) / 1000d;
                    swPart.Parameter("D1@Sketch20").SystemValue = 50d / 1000d;
                }
                if (IRNo > 2)
                {
                    swFeat = swComp.FeatureByName("MA3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch18").SystemValue = IRDis3 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch19").SystemValue = 220d / 1000d;
                    swPart.Parameter("D1@Sketch20").SystemValue = (length - 440d) / 2000d;
                    swPart.Parameter("D1@Sketch21").SystemValue = (length - 440d) / 2000d;
                }
            }
            else
            {
                swFeat = swComp.FeatureByName("MA1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MA2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MA3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            if (exType == "UV") //"UV"/"KV"
            {
                //UV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                if (bluetooth == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (sidePanel == "LEFT" || sidePanel == "BOTH") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                //KV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (MARVEL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩  
            }

        }


        public void FNHA0069(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo, string MARVEL, int IRNo, double IRDis1, double IRDis2, double IRDis3, string sidePanel, string exType, string bluetooth)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@基体-法兰1").SystemValue = length / 1000d;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = frontPanelKaKouNo;
            swPart.Parameter("D3@阵列(线性)1").SystemValue = frontPanelKaKouDis / 1000d;
            swPart.Parameter("D3@Sketch3").SystemValue = midRoofSecondHoleDis;
            swPart.Parameter("D9@草图7").SystemValue = 200d / 1000d - midRoofTopHoleDis;
            swFeat = swComp.FeatureByName("LPattern1");
            if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            else
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
            }
            //MARVEL
            //if (MARVEL == "YES")
            //{
            //    if (IRNo > 0)
            //    {
            //        swFeat = swComp.FeatureByName("MA1");
            //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            //        swPart.Parameter("D3@Sketch14").SystemValue = IRDis1 / 1000d;
            //        swFeat = swComp.FeatureByName("MACABLE1");
            //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            //        swPart.Parameter("D1@Sketch17").SystemValue = IRDis1 / 1000d;
            //    }
            //    if (IRNo > 1)
            //    {
            //        swFeat = swComp.FeatureByName("MA2");
            //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            //        swPart.Parameter("D3@Sketch15").SystemValue = IRDis2 / 1000d;
            //        swFeat = swComp.FeatureByName("MACABLE2");
            //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            //        swPart.Parameter("D1@Sketch18").SystemValue = IRDis2 / 1000d;
            //    }
            //    if (IRNo > 2)
            //    {
            //        swFeat = swComp.FeatureByName("MA3");
            //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            //        swPart.Parameter("D3@Sketch16").SystemValue = IRDis3 / 1000d;
            //        swFeat = swComp.FeatureByName("MACABLE3");
            //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            //        swPart.Parameter("D1@Sketch19").SystemValue = IRDis3 / 1000d;
            //    }
            //}
            //else
            //{
            //    swFeat = swComp.FeatureByName("MA1");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //    swFeat = swComp.FeatureByName("MA2");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //    swFeat = swComp.FeatureByName("MA3");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //    swFeat = swComp.FeatureByName("MACABLE1");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //    swFeat = swComp.FeatureByName("MACABLE2");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //    swFeat = swComp.FeatureByName("MACABLE3");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //}
            if (exType == "UV") //"UV"/"KV"
            {
                //UV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                if (bluetooth == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (sidePanel == "LEFT" || sidePanel == "BOTH") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                //KV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (MARVEL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩  
            }

        }
        public void FNHA0083(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo, double suDis, int suNo, string MARVEL, int IRNo, double IRDis1, double IRDis2, double IRDis3, string sidePanel, string exType, string bluetooth)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@基体-法兰1").SystemValue = length / 1000d;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = frontPanelKaKouNo;
            swPart.Parameter("D3@阵列(线性)1").SystemValue = frontPanelKaKouDis / 1000d;
            swPart.Parameter("D3@Sketch3").SystemValue = midRoofSecondHoleDis;
            //swPart.Parameter("D1@Sketch11").SystemValue = 200d / 1000d - midRoofTopHoleDis;
            swFeat = swComp.FeatureByName("LPattern1");
            if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            else
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
            }
            //新风脖颈
            swPart.Parameter("D3@Sketch6").SystemValue = (suDis * (suNo / 2 - 1) + suDis / 2d) / 1000d;
            swFeat = swComp.FeatureByName("LPattern2");
            if (suNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            else
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@LPattern2").SystemValue = suNo;
                swPart.Parameter("D3@LPattern2").SystemValue = suDis / 1000d;
            }
            //MARVEL
            if (MARVEL == "YES")
            {
                if (IRNo > 0)
                {
                    swFeat = swComp.FeatureByName("MA1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch16").SystemValue = IRDis1 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    if (suNo == 1) swPart.Parameter("D1@Sketch19").SystemValue = (length + 400) / 2000d;
                    else swPart.Parameter("D1@Sketch19").SystemValue = length / 2000d;
                }
                if (IRNo > 1)
                {
                    swFeat = swComp.FeatureByName("MA2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch17").SystemValue = IRDis2 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch19").SystemValue = (length / 2 - 25d) / 1000d;
                    swPart.Parameter("D1@Sketch20").SystemValue = 50d / 1000d;
                }
                if (IRNo > 2)
                {
                    swFeat = swComp.FeatureByName("MA3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch18").SystemValue = IRDis3 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch19").SystemValue = 220d / 1000d;
                    swPart.Parameter("D1@Sketch20").SystemValue = (length - 440d) / 2000d;
                    swPart.Parameter("D1@Sketch21").SystemValue = (length - 440d) / 2000d;
                }
            }
            else
            {
                swFeat = swComp.FeatureByName("MA1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MA2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MA3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            if (exType == "UV") //"UV"/"KV"
            {
                //UV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                if (bluetooth == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (sidePanel == "LEFT" || sidePanel == "BOTH") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                //KV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (MARVEL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩  
            }

        }


        #endregion

        #region 新风网孔板,前面板
        //F555型网孔板
        public void FNHA0007(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 2d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }
        //F450型网孔板
        public void FNHA0033(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 2d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }

        //F400型网孔板
        public void FNHA0027(Component2 swComp,double length,int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1",length - 2d);
            swPart.ChangeDim("D1@阵列(线性)7",frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7",frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }

        //I555型前面板
        public void FNHA0003(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 2d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }
        //I400型前面板
        public void FNHA0041(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 2d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }
        //I300型前面板
        public void FNHA0089(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, int frontPanelHoleNo, double frontPanelHoleDis)
        {
            swPart = swComp.GetModelDoc2();
            swPart.ChangeDim("D1@草图1", length - 2d);
            swPart.ChangeDim("D1@阵列(线性)7", frontPanelKaKouNo);
            swPart.ChangeDim("D3@阵列(线性)7", frontPanelKaKouDis);
            swPart.ChangeDim("D1@LPattern1", frontPanelHoleNo);
            swPart.ChangeDim("D3@LPattern1", frontPanelHoleDis);
        }


        #endregion


        #region 水洗排风腔零件

        /// <summary>
        /// 水洗排风腔顶部零件
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
        public void FNHE0032(Component2 swComp, double length, double midRoofSecondHoleDis, int midRoofHoleNo, int exNo, double exRightDis, double exLength, double exWidth, double exDis, string inlet, string ANSUL, string ANSide, string MARVEL, string UVType, string UWHood)
        {
            swPart = swComp.GetModelDoc2();//打开零件3
            swPart.Parameter("D1@草图1").SystemValue = length / 1000d;
            swPart.Parameter("D1@Sketch10").SystemValue = midRoofSecondHoleDis;
            if (midRoofHoleNo == 1)
            {
                swFeat = swComp.FeatureByName("LPattern1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                swFeat = swComp.FeatureByName("LPattern1");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
            }
            //排风口
            if (exNo == 1)
            {
                swFeat = swComp.FeatureByName("EXCOONE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("EXCOTWO");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D4@Sketch11").SystemValue = exRightDis / 1000d;
                swPart.Parameter("D2@Sketch11").SystemValue = exLength / 1000d;
                swPart.Parameter("D1@Sketch11").SystemValue = exWidth / 1000d;
            }
            else
            {
                swFeat = swComp.FeatureByName("EXCOONE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("EXCOTWO");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D5@Sketch12").SystemValue = exRightDis / 1000d;
                swPart.Parameter("D4@Sketch12").SystemValue = exDis / 1000d;
                swPart.Parameter("D1@Sketch12").SystemValue = exLength / 1000d;
                swPart.Parameter("D2@Sketch12").SystemValue = exWidth / 1000d;
            }
            //进水口
            if (inlet == "LEFT")
            {
                swFeat = swComp.FeatureByName("PIPEIN-LEFT");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("PIPEIN-RIGHT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else if (inlet == "RIGHT")
            {
                swFeat = swComp.FeatureByName("PIPEIN-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("PIPEIN-RIGHT");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                swFeat = swComp.FeatureByName("PIPEIN-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("PIPEIN-RIGHT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            //ANSUL
            if (ANSUL == "YES")
            {
                //侧喷
                if (ANSide == "LEFT")
                {
                    swFeat = swComp.FeatureByName("ANSUL-LEFT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("CHANNEL-LEFT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                else if (ANSide == "RIGHT")
                {
                    swFeat = swComp.FeatureByName("ANSUL-RIGHT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("CHANNEL-RIGHT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("ANSUL-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("ANSUL-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("CHANNEL-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("CHANNEL-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
            }
            //MARVEL
            swFeat = swComp.FeatureByName("MA-NTC");
            if (MARVEL == "YES")
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                if (exNo == 1) swPart.Parameter("D1@Sketch20").SystemValue = (exRightDis + exLength / 2 + 50d) / 1000d;
                else swPart.Parameter("D1@Sketch20").SystemValue = (exRightDis + exDis / 2 + exLength + 50d) / 1000d;
            }
            else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //UVHood,UVRack-UV灯架孔和UV cable-UV灯线缆穿孔
            if (UVType != null)
            {
                swFeat = swComp.FeatureByName("UVRACK");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@Sketch16").SystemValue = exRightDis / 1000d;
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@Sketch17").SystemValue = exRightDis / 1000d;

                if (UVType == "LONG")
                {

                    swPart.Parameter("D7@Sketch16").SystemValue = 1640d / 1000d;
                    swPart.Parameter("D4@Sketch17").SystemValue = 1500d / 1000d;
                }
                else
                {
                    //SHORT
                    swPart.Parameter("D7@Sketch16").SystemValue = 930d / 1000d;
                    swPart.Parameter("D4@Sketch17").SystemValue = 790d / 1000d;
                }
            }
            else
            {
                //非UVHood
                swFeat = swComp.FeatureByName("UVRACK");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            //解压检修门，水洗烟罩，且带UV，短灯>=1600,长灯>=2400
            if (UWHood != null && ((UVType == "SHORT" && length >= 1600d) || (UVType == "LONG" && length >= 2400d)))
            {
                if (inlet == "LEFT")
                {
                    swFeat = swComp.FeatureByName("DOOR-R");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DOOR-L");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                else if (inlet == "RIGHT")
                {
                    swFeat = swComp.FeatureByName("DOOR-R");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DOOR-L");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("DOOR-R");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DOOR-L");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
            }
            else
            {
                swFeat = swComp.FeatureByName("DOOR-R");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("DOOR-L");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
        }

        #endregion

        #region 斜侧板烟罩大侧板450-400/300

        /// <summary>
        /// 左边大侧板外板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        /// <param name="sidePanelSideCjNo">侧板侧向CJ孔数量</param>
        /// <param name="sidePanelDownCjNo">侧板垂直CJ孔数量</param>
        public void FNHS0055(Component2 swComp, double deepth, double height, double suHeight, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000d;
            swPart.Parameter("D3@草图1").SystemValue = height / 1000d;
            swPart.Parameter("D2@草图1").SystemValue = suHeight / 1000d;

            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelDownCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelSideCjNo - 1;
        }

        /// <summary>
        /// 左边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0056(Component2 swComp, double deepth, double height, double suHeight)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@Sketch1").SystemValue = deepth / 1000d;//D3@Sketch1
            swPart.Parameter("D3@Sketch1").SystemValue = height / 1000d;
            swPart.Parameter("D2@Sketch1").SystemValue = suHeight / 1000d;

            if (suHeight == 400d)
            {
                swFeat = swComp.FeatureByName("I400");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("I300");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            }
            else if (suHeight == 300d)
            {
                swFeat = swComp.FeatureByName("I400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("I300");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            }

            else
            {
                swFeat = swComp.FeatureByName("I400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("I300");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
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
        public void FNHS0057(Component2 swComp, double deepth, double height, double suHeight, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000d;
            swPart.Parameter("D3@草图1").SystemValue = height / 1000d;
            swPart.Parameter("D2@草图1").SystemValue = suHeight / 1000d;

            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelDownCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelSideCjNo - 1;
        }

        /// <summary>
        /// 右边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0058(Component2 swComp, double deepth, double height, double suHeight)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@Sketch1").SystemValue = deepth / 1000d;//D3@Sketch1
            swPart.Parameter("D3@Sketch1").SystemValue = height / 1000d;
            swPart.Parameter("D2@Sketch1").SystemValue = suHeight / 1000d;

            if (suHeight == 400d)
            {
                swFeat = swComp.FeatureByName("I400");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("I300");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            }
            else if (suHeight == 300d)
            {
                swFeat = swComp.FeatureByName("I400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("I300");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            }

            else
            {
                swFeat = swComp.FeatureByName("I400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("I300");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
        }

        #endregion

        #region 斜侧板CMOD烟罩大侧板555-400

        /// <summary>
        /// 左边大侧板外板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        /// <param name="sidePanelSideCjNo">侧板侧向CJ孔数量</param>
        /// <param name="sidePanelDownCjNo">侧板垂直CJ孔数量</param>
        public void FNHS0051(Component2 swComp, double deepth, double height, double suHeight, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000d;
            swPart.Parameter("D3@草图1").SystemValue = height / 1000d;
            swPart.Parameter("D2@草图1").SystemValue = suHeight / 1000d;

            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelDownCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelSideCjNo - 1;
        }

        /// <summary>
        /// 左边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0052(Component2 swComp, double deepth, double height, double suHeight)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@Sketch1").SystemValue = deepth / 1000d;//D3@Sketch1
            swPart.Parameter("D3@Sketch1").SystemValue = height / 1000d;
            swPart.Parameter("D2@Sketch1").SystemValue = suHeight / 1000d;

            //if (suHeight == 400d)
            //{
            //    swFeat = swComp.FeatureByName("I400");
            //    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            //    swFeat = swComp.FeatureByName("I300");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            //}
            ////else if (suHeight == 300d)
            //{
            //    swFeat = swComp.FeatureByName("I400");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //    swFeat = swComp.FeatureByName("I300");
            //    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            //}

            //else
            //{
            //    swFeat = swComp.FeatureByName("I400");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //    swFeat = swComp.FeatureByName("I300");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //}
        }

        /// <summary>
        /// 右边大侧板外板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        /// <param name="sidePanelSideCjNo">侧板侧向CJ孔数量</param>
        /// <param name="sidePanelDownCjNo">侧板垂直CJ孔数量</param>
        public void FNHS0053(Component2 swComp, double deepth, double height, double suHeight, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000d;
            swPart.Parameter("D3@草图1").SystemValue = height / 1000d;
            swPart.Parameter("D2@草图1").SystemValue = suHeight / 1000d;

            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelDownCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelSideCjNo - 1;
        }

        /// <summary>
        /// 右边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0054(Component2 swComp, double deepth, double height, double suHeight)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@Sketch1").SystemValue = deepth / 1000d;//D3@Sketch1
            swPart.Parameter("D3@Sketch1").SystemValue = height / 1000d;
            swPart.Parameter("D2@Sketch1").SystemValue = suHeight / 1000d;

            //if (suHeight == 400d)
            //{
            //    swFeat = swComp.FeatureByName("I400");
            //    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            //    swFeat = swComp.FeatureByName("I300");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            //}
            //else if (suHeight == 300d)
            //{
            //    swFeat = swComp.FeatureByName("I400");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //    swFeat = swComp.FeatureByName("I300");
            //    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            //}

            //else
            //{
            //    swFeat = swComp.FeatureByName("I400");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //    swFeat = swComp.FeatureByName("I300");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //}
        }

        #endregion

        #region MidRoof

        public void FNHM0001(Component2 swComp, string exType, double length, double deepth, double exHeight, double suHeight, double exRightDis, double midRoofTopHoleDis, double midRoofSecondHoleDis, int midRoofHoleNo, string lightType, double lightYDis, int ledSpotNo, double ledSpotDis, string ansul, int anDropNo, double anYDis, double anDropDis1, double anDropDis2, double anDropDis3, double anDropDis4, double anDropDis5, string anDetectorEnd, int anDetectorNo, double anDetectorDis1, double anDetectorDis2, double anDetectorDis3, double anDetectorDis4, double anDetectorDis5, string bluetooth, string uvType, string marvel, int irNo, double irDis1, double irDis2, double irDis3)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = (length - 4d) / 1000d;
            swPart.Parameter("D2@草图1").SystemValue = (deepth - 669d) / 1000d;
            swPart.Parameter("D1@草图6").SystemValue = (deepth - 896d) / 1000d;
            swPart.Parameter("D3@草图25").SystemValue = midRoofTopHoleDis;
            swPart.Parameter("D2@草图26").SystemValue = (deepth - 840d) / 3000;
            swPart.Parameter("D1@Sketch3").SystemValue = midRoofSecondHoleDis - 2d / 1000d;
            swFeat = swComp.FeatureByName("LPattern1");
            if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            else
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
            }
            //灯具
            if (lightType == "LED60")
            {
                swFeat = swComp.FeatureByName("LED140");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("LPattern3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("FSLONG");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("FSSHORT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("LED60");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                if (ledSpotNo == 1) swPart.Parameter("D2@Sketch1").SystemValue = 0;
                else
                {
                    swPart.Parameter("D2@Sketch1").SystemValue = (ledSpotDis * (ledSpotNo / 2d - 1) + ledSpotDis / 2d) / 1000d;
                    swFeat = swComp.FeatureByName("LPattern2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                    swPart.Parameter("D1@LPattern2").SystemValue = ledSpotNo;
                    swPart.Parameter("D3@LPattern2").SystemValue = ledSpotDis / 1000d;
                }
            }
            else if (lightType == "LED140")
            {
                swFeat = swComp.FeatureByName("LED60");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("LPattern2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("FSLONG");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("FSSHORT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("LED140");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                if (ledSpotNo == 1) swPart.Parameter("D5@Sketch7").SystemValue = 0;
                else
                {
                    swPart.Parameter("D5@Sketch7").SystemValue = (ledSpotDis * (ledSpotNo / 2d - 1) + ledSpotDis / 2d) / 1000d;
                    swFeat = swComp.FeatureByName("LPattern3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                    swPart.Parameter("D1@LPattern3").SystemValue = ledSpotNo;
                    swPart.Parameter("D3@LPattern3").SystemValue = ledSpotDis / 1000d;
                }
            }
            else if (lightType == "FSLONG")
            {
                swFeat = swComp.FeatureByName("LED60");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("LPattern2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("LED140");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("LPattern3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("FSLONG");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("FSSHORT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else if (lightType == "FSSHORT")
            {
                swFeat = swComp.FeatureByName("LED60");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("LPattern2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("LED140");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("LPattern3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("FSLONG");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("FSSHORT");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                swFeat = swComp.FeatureByName("LED60");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("LPattern2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("LED140");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("LPattern3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("FSLONG");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("FSSHORT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            //ANSUL
            if (ansul == "YES")
            {
                swFeat = swComp.FeatureByName("AN1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("AN2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("AN3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("AN4");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("AN5");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("ANDTEC1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("ANDTEC2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("ANDTEC3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("ANDTEC4");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("ANDTEC5");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                                                    //下喷
                if (anDropNo > 0)
                {
                    swFeat = swComp.FeatureByName("AN1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch11").SystemValue = anDropDis1 / 1000d;
                    swPart.Parameter("D3@Sketch11").SystemValue = (anYDis - 360d) / 1000d;
                }
                if (anDropNo > 1)
                {
                    swFeat = swComp.FeatureByName("AN2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch12").SystemValue = anDropDis2 / 1000d;
                }
                if (anDropNo > 2)
                {
                    swFeat = swComp.FeatureByName("AN3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch13").SystemValue = anDropDis3 / 1000d;
                }
                if (anDropNo > 3)
                {
                    swFeat = swComp.FeatureByName("AN4");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch14").SystemValue = anDropDis4 / 1000d;
                }
                if (anDropNo > 4)
                {
                    swFeat = swComp.FeatureByName("AN5");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch15").SystemValue = anDropDis5 / 1000d;
                }

                if (exType == "UW" || exType == "KW" || exType == "CMOD")
                {
                    //探测器
                    swFeat = swComp.FeatureByName("ANDTECACROSS");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    if (anDetectorNo > 0)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch31").SystemValue = anDetectorDis1 / 1000d;
                        if (anDetectorEnd == "LEFT" || (anDetectorEnd == "RIGHT" && anDetectorNo == 1))
                            swPart.Parameter("D2@Sketch31").SystemValue = 195d / 1000d;
                        else swPart.Parameter("D2@Sketch31").SystemValue = 175d / 1000d;
                    }
                    if (anDetectorNo > 1)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch32").SystemValue = anDetectorDis2 / 1000d;
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 2) swPart.Parameter("D2@Sketch32").SystemValue = 195d / 1000d;
                        else swPart.Parameter("D2@Sketch32").SystemValue = 175d / 1000d;
                    }
                    if (anDetectorNo > 2)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch33").SystemValue = anDetectorDis3 / 1000d;
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 3) swPart.Parameter("D2@Sketch33").SystemValue = 195d / 1000d;
                        else swPart.Parameter("D2@Sketch33").SystemValue = 175d / 1000d;
                    }
                    if (anDetectorNo > 3)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC4");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch34").SystemValue = anDetectorDis4 / 1000d;
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 4) swPart.Parameter("D2@Sketch34").SystemValue = 195d / 1000d;
                        else swPart.Parameter("D2@Sketch34").SystemValue = 175d / 1000d;
                    }
                    if (anDetectorNo > 4)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC5");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch35").SystemValue = anDetectorDis5 / 1000d;
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 5) swPart.Parameter("D2@Sketch35").SystemValue = 195d / 1000d;
                        else swPart.Parameter("D2@Sketch35").SystemValue = 175d / 1000d;
                    }
                }
                else
                {
                    //UW/KW HOOD 水洗烟罩探测器在midRoof上，非水洗烟罩压缩
                    swFeat = swComp.FeatureByName("ANDTEC1");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("ANDTEC2");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("ANDTEC3");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("ANDTEC4");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("ANDTEC5");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("ANDTECACROSS");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }

            }
            else
            {
                swFeat = swComp.FeatureByName("AN1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("AN2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("AN3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("AN4");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("AN5");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                                                    //UW/KW HOOD 水洗烟罩探测器在midRoof上，非水洗烟罩压缩
                swFeat = swComp.FeatureByName("ANDTEC1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("ANDTEC2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("ANDTEC3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("ANDTEC4");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("ANDTEC5");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("ANDTECACROSS");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }

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
                swPart.Parameter("D4@草图28").SystemValue = exRightDis / 1000d;
                swPart.Parameter("D3@草图28").SystemValue = 1500d / 1000d;
            }
            else if (uvType == "SHORT")
            {
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                swPart.Parameter("D4@草图28").SystemValue = exRightDis / 1000d;
                swPart.Parameter("D3@草图28").SystemValue = 790d / 1000d;
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
                    swPart.Parameter("D1@Sketch37").SystemValue = irDis1 / 1000d;
                }
                if (irNo > 1)
                {
                    swFeat = swComp.FeatureByName("MAINS2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch38").SystemValue = irDis2 / 1000d;
                }
                if (irNo > 2)
                {
                    swFeat = swComp.FeatureByName("MAINS3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch39").SystemValue = irDis3 / 1000d;
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


        #region 450新风
        //F型
        public void FNHA0039(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo, double suDis, int suNo, string MARVEL, int IRNo, double IRDis1, double IRDis2, double IRDis3, string sidePanel, string exType, string bluetooth)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@基体-法兰1").SystemValue = length / 1000d;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = frontPanelKaKouNo;
            swPart.Parameter("D3@阵列(线性)1").SystemValue = frontPanelKaKouDis / 1000d;
            swPart.Parameter("D3@Sketch1").SystemValue = midRoofSecondHoleDis;
            swPart.Parameter("D3@Sketch18").SystemValue = 200d / 1000d - midRoofTopHoleDis;
            swFeat = swComp.FeatureByName("LPattern1");
            if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            else
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
            }
            //新风脖颈
            swPart.Parameter("D3@Sketch5").SystemValue = (suDis * (suNo / 2 - 1) + suDis / 2d) / 1000d;
            swFeat = swComp.FeatureByName("LPattern2");
            if (suNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            else
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@LPattern2").SystemValue = suNo;
                swPart.Parameter("D3@LPattern2").SystemValue = suDis / 1000d;
            }
            //MARVEL
            if (MARVEL == "YES")
            {
                if (IRNo > 0)
                {
                    swFeat = swComp.FeatureByName("MA1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D2@Sketch24").SystemValue = IRDis1 / 1000d;
                    swFeat = swComp.FeatureByName("MAINS1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch22").SystemValue = IRDis1 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch23").SystemValue = 150d / 1000d;
                }
                if (IRNo > 1)
                {
                    swFeat = swComp.FeatureByName("MA2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D2@Sketch25").SystemValue = IRDis2 / 1000d;
                    swFeat = swComp.FeatureByName("MAINS2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch27").SystemValue = IRDis2 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch23").SystemValue = 150d / 1000d;
                    swPart.Parameter("D1@Sketch29").SystemValue = (length - 300d) / 1000d;
                }
                if (IRNo > 2)
                {
                    swFeat = swComp.FeatureByName("MA3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D2@Sketch26").SystemValue = IRDis3 / 1000d;
                    swFeat = swComp.FeatureByName("MAINS3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch28").SystemValue = IRDis3 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch23").SystemValue = 150d / 1000d;//第一出现孔
                    swPart.Parameter("D1@Sketch29").SystemValue = (length - 300d) / 1000d;//第二出线孔
                    swPart.Parameter("D1@Sketch30").SystemValue = 50d / 1000d;//第三出线孔
                }
            }
            else
            {
                swFeat = swComp.FeatureByName("MA1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MA2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MA3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MAINS1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MAINS2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MAINS3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            if (exType == "UV") //"UV"/"KV"
            {
                //UV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                if (bluetooth == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (sidePanel == "LEFT" || sidePanel == "BOTH") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                //KV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (MARVEL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩  
            }








        }
        #endregion

        #region 400新风

        //F型
        public void FNHA0021(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo, double suDis, int suNo, string MARVEL, int IRNo, double IRDis1, double IRDis2, double IRDis3, string sidePanel, string exType, string bluetooth)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@基体-法兰1").SystemValue = length / 1000d;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = frontPanelKaKouNo;
            swPart.Parameter("D3@阵列(线性)1").SystemValue = frontPanelKaKouDis/1000d;
            swPart.Parameter("D3@Sketch4").SystemValue = midRoofSecondHoleDis;
            swPart.Parameter("D1@Sketch11").SystemValue = 200d / 1000d - midRoofTopHoleDis;
            swFeat = swComp.FeatureByName("LPattern1");
            if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            else
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
            }
            //新风脖颈
            swPart.Parameter("D1@Sketch5").SystemValue = (suDis * (suNo / 2 - 1) + suDis / 2d) / 1000d;
            swFeat = swComp.FeatureByName("LPattern2");
            if (suNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            else
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@LPattern2").SystemValue = suNo;
                swPart.Parameter("D3@LPattern2").SystemValue = suDis / 1000d;
            }
            //MARVEL//400新风腔IR安装孔MAINS
            if (MARVEL == "YES")
            {
                swFeat = swComp.FeatureByName("IR3FAN");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                if (IRNo > 0)
                {
                    swFeat = swComp.FeatureByName("MA1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D2@Sketch14").SystemValue = IRDis1 / 1000d;
                    swFeat = swComp.FeatureByName("MAINS1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch20").SystemValue = IRDis1 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch17").SystemValue = 150d / 1000d;
                }
                if (IRNo > 1)
                {
                    swFeat = swComp.FeatureByName("MA2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D2@Sketch15").SystemValue = IRDis2 / 1000d;
                    swFeat = swComp.FeatureByName("MAINS2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch22").SystemValue = IRDis2 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch17").SystemValue = 150d / 1000d;
                    swPart.Parameter("D2@Sketch18").SystemValue = (length - 300d) / 1000d;
                }
                if (IRNo > 2)
                {
                    swFeat = swComp.FeatureByName("MA3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D2@Sketch16").SystemValue = IRDis3 / 1000d;
                    swFeat = swComp.FeatureByName("MAINS3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch23").SystemValue = IRDis3 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch17").SystemValue = 150d / 1000d;//第一出现孔
                    swPart.Parameter("D2@Sketch18").SystemValue = (length - 300d) / 1000d;//第二出线孔
                    swPart.Parameter("D3@Sketch19").SystemValue = 50d / 1000d;//第三出线孔
                    swFeat = swComp.FeatureByName("IR3FAN");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
            }
            else
            {
                swFeat = swComp.FeatureByName("MA1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MA2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MA3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MAINS1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MAINS2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MAINS3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("IR3FAN");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            if (exType == "UV") //"UV"/"KV"
            {
                //UV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                if (bluetooth == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (sidePanel == "LEFT" || sidePanel == "BOTH") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                //KV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (MARVEL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩  
            }








        }
        //I型
        public void FNHA0040(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo,  string MARVEL, int IRNo, double IRDis1, double IRDis2, double IRDis3, string sidePanel, string exType, string bluetooth)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@基体-法兰1").SystemValue = length / 1000d;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = frontPanelKaKouNo;
            swPart.Parameter("D3@阵列(线性)1").SystemValue = frontPanelKaKouDis / 1000d;
            swPart.Parameter("D3@Sketch4").SystemValue = midRoofSecondHoleDis;
            swPart.Parameter("D1@Sketch11").SystemValue = 200d / 1000d - midRoofTopHoleDis;
            swFeat = swComp.FeatureByName("LPattern1");
            if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            else
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
            }
            //MARVEL
            if (MARVEL == "YES")
            {
                if (IRNo > 0)
                {
                    swFeat = swComp.FeatureByName("MA1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch16").SystemValue =IRDis1 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D2@Sketch19").SystemValue = IRDis1 / 1000d;
                }
                if (IRNo > 1)
                {
                    swFeat = swComp.FeatureByName("MA2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch17").SystemValue = IRDis2 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D2@Sketch20").SystemValue =IRDis2 / 1000d;
                }
                if (IRNo > 2)
                {
                    swFeat = swComp.FeatureByName("MA3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch18").SystemValue =IRDis3 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D2@Sketch21").SystemValue = IRDis3 / 1000d;
                }
            }
            else
            {
                swFeat = swComp.FeatureByName("MA1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MA2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MA3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            if (exType == "UV") //"UV"/"KV"
            {
                //UV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                if (bluetooth == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (sidePanel == "LEFT" || sidePanel == "BOTH") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                //KV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (MARVEL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩  
            }








        }



        #endregion

        #region 300新风
        //I型
        public void FNHA0087(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo, string MARVEL, int IRNo, double IRDis1, double IRDis2, double IRDis3, string sidePanel, string exType, string bluetooth)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@基体-法兰1").SystemValue = length / 1000d;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = frontPanelKaKouNo;
            swPart.Parameter("D3@阵列(线性)1").SystemValue = frontPanelKaKouDis / 1000d;
            swPart.Parameter("D3@Sketch4").SystemValue = midRoofSecondHoleDis;
            swPart.Parameter("D1@Sketch11").SystemValue = 200d / 1000d - midRoofTopHoleDis;
            swFeat = swComp.FeatureByName("LPattern1");
            if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            else
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
            }
            //MARVEL
            if (MARVEL == "YES")
            {
                if (IRNo > 0)
                {
                    swFeat = swComp.FeatureByName("MA1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch16").SystemValue = IRDis1 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D2@Sketch19").SystemValue = IRDis1 / 1000d;
                }
                if (IRNo > 1)
                {
                    swFeat = swComp.FeatureByName("MA2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch17").SystemValue = IRDis2 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D2@Sketch20").SystemValue = IRDis2 / 1000d;
                }
                if (IRNo > 2)
                {
                    swFeat = swComp.FeatureByName("MA3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch18").SystemValue = IRDis3 / 1000d;
                    swFeat = swComp.FeatureByName("MACABLE3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D2@Sketch21").SystemValue = IRDis3 / 1000d;
                }
            }
            else
            {
                swFeat = swComp.FeatureByName("MA1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MA2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MA3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MACABLE3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            if (exType == "UV") //"UV"/"KV"
            {
                //UV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                if (bluetooth == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (sidePanel == "LEFT" || sidePanel == "BOTH") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                //KV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (MARVEL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩  
            }








        }


        #endregion
        #region 方形新风腔
        public void FNHA0012(Component2 swComp, double length, int frontPanelKaKouNo, double frontPanelKaKouDis, double midRoofSecondHoleDis, double midRoofTopHoleDis, int midRoofHoleNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@基体-法兰1").SystemValue = length / 1000d;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = frontPanelKaKouNo;
            swPart.Parameter("D3@阵列(线性)1").SystemValue = frontPanelKaKouDis / 1000d;
            swPart.Parameter("D1@Sketch2").SystemValue = midRoofSecondHoleDis;
            swPart.Parameter("D1@Sketch8").SystemValue = 150d / 1000d - midRoofTopHoleDis;
            swFeat = swComp.FeatureByName("LPattern1");
            if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            else
            {
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
            }
            








        }


        #endregion

    }
}
