using SolidWorks.Interop.sldworks;

namespace SolidWorksHelper
{
    public class EditPart
    {
        ModelDoc2 swPart = default(ModelDoc2);
        Feature swFeat = default(Feature);

        #region Hood SidePanel 普通烟罩大侧板

        /// <summary>
        /// 左边大侧板外板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        /// <param name="sidePanelSideCjNo">侧板侧向CJ孔数量</param>
        /// <param name="sidePanelDownCjNo">侧板垂直CJ孔数量</param>
        public void FNHS0001(Component2 swComp, decimal deepth, decimal height, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = height / 1000m;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
        }
        /// <summary>
        /// 左边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0002(Component2 swComp, decimal deepth, decimal height)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = (deepth - 79m) / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = (height - 39m) / 1000m;
            if (height == 555m)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else if (height == 450m)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else if (height == 400m)
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
        public void FNHS0003(Component2 swComp, decimal deepth, decimal height, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = height / 1000m;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
        }
        /// <summary>
        /// 右边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0004(Component2 swComp, decimal deepth, decimal height)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = (deepth - 79m) / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = (height - 39m) / 1000m;
            if (height == 555m)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            }
            else if (height == 450m)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else if (height == 400m)
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

        #endregion

        #region Hood SidePanel 华为555烟罩大侧板

        /// <summary>
        /// 左边大侧板外板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        /// <param name="sidePanelSideCjNo">侧板侧向CJ孔数量</param>
        /// <param name="sidePanelDownCjNo">侧板垂直CJ孔数量</param>
        public void FNHS0067(Component2 swComp, decimal deepth, decimal height, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = height / 1000m;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
        }
        /// <summary>
        /// 左边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0068(Component2 swComp, decimal deepth, decimal height)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = (deepth - 79m) / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = (height - 39m) / 1000m;
            if (height == 555m)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else if (height == 450m)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else if (height == 400m)
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
        public void FNHS0069(Component2 swComp, decimal deepth, decimal height, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = height / 1000m;
            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
        }
        /// <summary>
        /// 右边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0070(Component2 swComp, decimal deepth, decimal height)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = (deepth - 79m) / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = (height - 39m) / 1000m;
            if (height == 555m)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            }
            else if (height == 450m)
            {
                swFeat = swComp.FeatureByName("F555");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F450");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("F400");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else if (height == 400m)
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
        public void FNHE0032(Component2 swComp, decimal length, decimal midRoofSecondHoleDis, int midRoofHoleNo, int exNo, decimal exRightDis, decimal exLength, decimal exWidth, decimal exDis, string inlet, string ANSUL, string ANSide, string MARVEL, string UVType, string UWHood)
        {
            swPart = swComp.GetModelDoc2();//打开零件3
            swPart.Parameter("D1@草图1").SystemValue = length / 1000m;
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
                swPart.Parameter("D4@Sketch11").SystemValue = exRightDis / 1000m;
                swPart.Parameter("D2@Sketch11").SystemValue = exLength / 1000m;
                swPart.Parameter("D1@Sketch11").SystemValue = exWidth / 1000m;
            }
            else
            {
                swFeat = swComp.FeatureByName("EXCOONE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("EXCOTWO");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D5@Sketch12").SystemValue = exRightDis / 1000m;
                swPart.Parameter("D4@Sketch12").SystemValue = exDis / 1000m;
                swPart.Parameter("D1@Sketch12").SystemValue = exLength / 1000m;
                swPart.Parameter("D2@Sketch12").SystemValue = exWidth / 1000m;
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
                if (exNo == 1) swPart.Parameter("D1@Sketch20").SystemValue = (exRightDis + exLength / 2 + 50m) / 1000m;
                else swPart.Parameter("D1@Sketch20").SystemValue = (exRightDis + exDis / 2 + exLength + 50m) / 1000m;
            }
            else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //UVHood,UVRack-UV灯架孔和UV cable-UV灯线缆穿孔
            if (UVType != null)
            {
                swFeat = swComp.FeatureByName("UVRACK");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@Sketch16").SystemValue = exRightDis / 1000m;
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@Sketch17").SystemValue = exRightDis / 1000m;

                if (UVType == "LONG")
                {

                    swPart.Parameter("D7@Sketch16").SystemValue = 1640m / 1000m;
                    swPart.Parameter("D4@Sketch17").SystemValue = 1500m / 1000m;
                }
                else
                {
                    //SHORT
                    swPart.Parameter("D7@Sketch16").SystemValue = 930m / 1000m;
                    swPart.Parameter("D4@Sketch17").SystemValue = 790m / 1000m;
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
            if (UWHood != null && ((UVType == "SHORT" && length >= 1600m) || (UVType == "LONG" && length >= 2400m)))
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

        #region 华为水洗排风腔零件

        /// <summary>
        /// 华为水洗排风腔顶部零件
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
        public void FNHE0148(Component2 swComp, decimal length, decimal midRoofSecondHoleDis, int midRoofHoleNo, int exNo, decimal exRightDis, decimal exLength, decimal exWidth, decimal exDis, string inlet, string ANSUL, string ANSide, string MARVEL, string UVType, string UWHood)
        {
            swPart = swComp.GetModelDoc2();//打开零件3
            swPart.Parameter("D1@草图1").SystemValue = length / 1000m;
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
                swPart.Parameter("D4@Sketch11").SystemValue = exRightDis / 1000m;
                swPart.Parameter("D2@Sketch11").SystemValue = exLength / 1000m;
                swPart.Parameter("D1@Sketch11").SystemValue = exWidth / 1000m;
            }
            else
            {
                swFeat = swComp.FeatureByName("EXCOONE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("EXCOTWO");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D5@Sketch12").SystemValue = exRightDis / 1000m;
                swPart.Parameter("D4@Sketch12").SystemValue = exDis / 1000m;
                swPart.Parameter("D1@Sketch12").SystemValue = exLength / 1000m;
                swPart.Parameter("D2@Sketch12").SystemValue = exWidth / 1000m;
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
                    //swFeat = swComp.FeatureByName("CHANNEL-LEFT");
                    //swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                else if (ANSide == "RIGHT")
                {
                    swFeat = swComp.FeatureByName("ANSUL-RIGHT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    //swFeat = swComp.FeatureByName("CHANNEL-RIGHT");
                    //swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("ANSUL-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("ANSUL-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    //swFeat = swComp.FeatureByName("CHANNEL-LEFT");
                    //swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    //swFeat = swComp.FeatureByName("CHANNEL-RIGHT");
                    //swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
            }
            //MARVEL
            //swFeat = swComp.FeatureByName("MA-NTC");
            //if (MARVEL == "YES")
            //{
            //    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            //    if (exNo == 1) swPart.Parameter("D1@Sketch20").SystemValue = (exRightDis + exLength / 2 + 50m) / 1000m;
            //    else swPart.Parameter("D1@Sketch20").SystemValue = (exRightDis + exDis / 2 + exLength + 50m) / 1000m;
            //}
            //else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

            //UVHood,UVRack-UV灯架孔和UV cable-UV灯线缆穿孔

            if (UVType == "DOUBLE")
            {
                swFeat = swComp.FeatureByName("UVDOUBLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D7@Sketch15").SystemValue = exRightDis / 1000m;
                swFeat = swComp.FeatureByName("UVRACK");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else if (UVType == "LONG")
            {
                swFeat = swComp.FeatureByName("UVRACK");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D10@Sketch16").SystemValue = exRightDis / 1000m;
                swPart.Parameter("D8@Sketch16").SystemValue = 1640m / 1000m;
                swPart.Parameter("D9@Sketch16").SystemValue = 1500m / 1000m;
                swFeat = swComp.FeatureByName("UVDOUBLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else if (UVType == "SHORT")
            {
                //SHORT
                swFeat = swComp.FeatureByName("UVRACK");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D10@Sketch16").SystemValue = exRightDis / 1000m;
                swPart.Parameter("D8@Sketch16").SystemValue = 930m / 1000m;
                swPart.Parameter("D9@Sketch16").SystemValue = 790m / 1000m;
                swFeat = swComp.FeatureByName("UVDOUBLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                //非UVHood
                swFeat = swComp.FeatureByName("UVRACK");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("UVDOUBLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
            //解压检修门，水洗烟罩，且带UV，短灯>=1600,长灯>=2400
            //if (UWHood != null && ((UVType == "SHORT" && length >= 1600m) || (UVType == "LONG" && length >= 2400m)))
            //{
            //    if (inlet == "LEFT")
            //    {
            //        swFeat = swComp.FeatureByName("DOOR-R");
            //        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //        swFeat = swComp.FeatureByName("DOOR-L");
            //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            //    }
            //    else if (inlet == "RIGHT")
            //    {
            //        swFeat = swComp.FeatureByName("DOOR-R");
            //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            //        swFeat = swComp.FeatureByName("DOOR-L");
            //        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //    }
            //    else
            //    {
            //        swFeat = swComp.FeatureByName("DOOR-R");
            //        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //        swFeat = swComp.FeatureByName("DOOR-L");
            //        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //    }
            //}
            //else
            //{
            //    swFeat = swComp.FeatureByName("DOOR-R");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //    swFeat = swComp.FeatureByName("DOOR-L");
            //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            //}
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
        public void FNHS0055(Component2 swComp, decimal deepth, decimal height, decimal suHeight, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000m;
            swPart.Parameter("D3@草图1").SystemValue = height / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = suHeight / 1000m;

            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelDownCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelSideCjNo - 1;
        }

        /// <summary>
        /// 左边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0056(Component2 swComp, decimal deepth, decimal height, decimal suHeight)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@Sketch1").SystemValue = deepth / 1000m;//D3@Sketch1
            swPart.Parameter("D3@Sketch1").SystemValue = height / 1000m;
            swPart.Parameter("D2@Sketch1").SystemValue = suHeight / 1000m;

            if (suHeight == 400m)
            {
                swFeat = swComp.FeatureByName("I400");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("I300");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            }
            else if (suHeight == 300m)
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
        public void FNHS0057(Component2 swComp, decimal deepth, decimal height, decimal suHeight, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000m;
            swPart.Parameter("D3@草图1").SystemValue = height / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = suHeight / 1000m;

            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelDownCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelSideCjNo - 1;
        }

        /// <summary>
        /// 右边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0058(Component2 swComp, decimal deepth, decimal height, decimal suHeight)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@Sketch1").SystemValue = deepth / 1000m;//D3@Sketch1
            swPart.Parameter("D3@Sketch1").SystemValue = height / 1000m;
            swPart.Parameter("D2@Sketch1").SystemValue = suHeight / 1000m;

            if (suHeight == 400m)
            {
                swFeat = swComp.FeatureByName("I400");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("I300");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            }
            else if (suHeight == 300m)
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
    }
}
