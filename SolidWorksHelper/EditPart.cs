using SolidWorks.Interop.sldworks;
using System;

namespace SolidWorksHelper
{
    public class EditPart
    {
        ModelDoc2 swPart;
        Feature swFeat;

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
            if (height == 555m || height == 650m)
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
            if (height == 555m || height == 650m)
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
        /// 左集水翻边
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="suHeight">新风高度</param>
        /// <param name="exHeight">排风高度</param>
        /// <param name="exType">排风腔类型，"V"还是"W"</param>
        public void FNHS0005(Component2 swComp, decimal deepth,  decimal exHeight, decimal suHeight, string exType)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@Base-Flange1").SystemValue = deepth / 1000m;

            swPart.Parameter("D3@Sketch11").SystemValue = deepth / 1000m;

            swPart.Parameter("D2@Sketch11").SystemValue = suHeight / 1000m;//新风
            swPart.Parameter("D8@Sketch11").SystemValue = exHeight / 1000m;//排风
            if (exHeight == 450m)
            {
                swPart.Parameter("D6@Sketch11").SystemValue = 105m / 1000m;//EX450,105
                swPart.Parameter("D11@Sketch11").SystemValue = 50m / 1000m;//EX450,50
                swPart.Parameter("D9@Sketch11").SystemValue = 135d / 180d * Math.PI;//EX450,135
            }
            else
            {
                if (exType == "W")
                {
                    swPart.Parameter("D6@Sketch11").SystemValue = 150m / 1000m;//普通76.5水洗150
                    swPart.Parameter("D11@Sketch11").SystemValue = 101m / 1000m;//普通85水洗101
                    swPart.Parameter("D9@Sketch11").SystemValue = 145d / 180d * Math.PI;//普通135水洗145
                }
                else
                {
                    swPart.Parameter("D6@Sketch11").SystemValue = 76.5m / 1000m;//普通76.5水洗150
                    swPart.Parameter("D11@Sketch11").SystemValue = 85m / 1000m;//普通85水洗101
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
        public void FNHS0006(Component2 swComp, decimal deepth,  decimal exHeight, decimal suHeight, string exType)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@Base-Flange1").SystemValue = deepth / 1000m;

            swPart.Parameter("D3@Sketch11").SystemValue = deepth / 1000m;

            swPart.Parameter("D2@Sketch11").SystemValue = suHeight / 1000m;//新风
            swPart.Parameter("D8@Sketch11").SystemValue = exHeight / 1000m;//排风
            if (exHeight == 450m)
            {
                swPart.Parameter("D6@Sketch11").SystemValue = 105m / 1000m;//EX450,105
                swPart.Parameter("D11@Sketch11").SystemValue = 50m / 1000m;//EX450,50
                swPart.Parameter("D9@Sketch11").SystemValue = 135d / 180d * Math.PI;//EX450,135
            }
            else
            {
                if (exType == "W")
                {
                    swPart.Parameter("D6@Sketch11").SystemValue = 150m / 1000m;//普通76.5水洗150
                    swPart.Parameter("D11@Sketch11").SystemValue = 101m / 1000m;//普通85水洗101
                    swPart.Parameter("D9@Sketch11").SystemValue = 145d / 180d * Math.PI;//普通135水洗145
                }
                else
                {
                    swPart.Parameter("D6@Sketch11").SystemValue = 76.5m / 1000m;//普通76.5水洗150
                    swPart.Parameter("D11@Sketch11").SystemValue = 85m / 1000m;//普通85水洗101
                    swPart.Parameter("D9@Sketch11").SystemValue = 135d / 180d * Math.PI;//普通135水洗145
                }
            }
        }

        #endregion

        #region 法国烟罩大侧板
        internal void FNHS0073(Component2 swComp, decimal deepth, decimal height, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@Sketch1").SystemValue = deepth / 1000m;
            swPart.Parameter("D2@Sketch1").SystemValue = (height - 20m) / 1000m;
            swPart.Parameter("D1@LPattern2").SystemValue = sidePanelSideCjNo;
            swPart.Parameter("D1@LPattern1").SystemValue = sidePanelDownCjNo;
        }
        internal void FNHS0075(Component2 swComp, decimal deepth, decimal height, decimal withoutExSuMiDis)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D7@Sketch1").SystemValue = deepth / 1000m;
            swPart.Parameter("D6@Sketch1").SystemValue = (height - 1m) / 1000m;
            //侧板铆钉，大孔相对与铆钉孔居中分布，铆钉孔2个，距离边分别为20和30,与侧板联动
            if (withoutExSuMiDis >= 90)
            {
                //解压
                swFeat = swComp.FeatureByName("LPattern1");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D3@LPattern1").SystemValue = (withoutExSuMiDis - 50m) / 1000m;
                swFeat = swComp.FeatureByName("LPattern2");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D3@LPattern2").SystemValue = (withoutExSuMiDis - 50m) / 1000m;
            }
            else
            {
                //压缩
                swFeat = swComp.FeatureByName("LPattern1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("LPattern2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
        }

        internal void FNHS0074(Component2 swComp, decimal deepth, decimal height, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@Sketch1").SystemValue = deepth / 1000m;
            swPart.Parameter("D2@Sketch1").SystemValue = (height - 20m) / 1000m;
            swPart.Parameter("D1@LPattern2").SystemValue = sidePanelSideCjNo;
            swPart.Parameter("D1@LPattern1").SystemValue = sidePanelDownCjNo;
        }
        internal void FNHS0076(Component2 swComp, decimal deepth, decimal height, decimal withoutExSuMiDis)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D7@Sketch1").SystemValue = deepth / 1000m;
            swPart.Parameter("D6@Sketch1").SystemValue = (height - 1m) / 1000m;
            //侧板铆钉，大孔相对与铆钉孔居中分布，铆钉孔2个，距离边分别为20和30,与侧板联动
            if (withoutExSuMiDis >= 90)
            {
                //解压
                swFeat = swComp.FeatureByName("LPattern1");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D3@LPattern1").SystemValue = (withoutExSuMiDis - 50m) / 1000m;
                swFeat = swComp.FeatureByName("LPattern2");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D3@LPattern2").SystemValue = (withoutExSuMiDis - 50m) / 1000m;
            }
            else
            {
                //压缩
                swFeat = swComp.FeatureByName("LPattern1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("LPattern2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }
        }

        internal void FNHS0077(Component2 swComp, decimal deepth)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@Base-Flange1").SystemValue = (deepth - 283.5m) / 1000m;
        }
        internal void FNHS0078(Component2 swComp, decimal deepth)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@Base-Flange1").SystemValue = (deepth - 283.5m) / 1000m;
        }
        #endregion

        #region Hood SidePanel 华为650，555烟罩大侧板

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
            swPart.Parameter("D1@草图1").SystemValue = (deepth - 79m - 3.4m) / 1000m;//华为板厚1.2,内板过大-3.4
            swPart.Parameter("D2@草图1").SystemValue = (height - 39m - 1m) / 1000m;
            if (height == 555m || height == 650m)
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
            swPart.Parameter("D1@草图1").SystemValue = (deepth - 79m - 3.4m) / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = (height - 39m - 1m) / 1000m;
            if (height == 555m || height == 650m)
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

        #region 华为烟罩
        /// <summary>
        /// 华为MiddleRoof灯板
        /// </summary>
        public void FNHM0031(Component2 swComp, string uw, decimal length, decimal deepth, string height, decimal exRightDis, decimal midRoofTopHoleDis, decimal midRoofSecondHoleDis, int midRoofHoleNo, string lightType, decimal lightYDis, int ledSpotNo, decimal ledSpotDis, string ansul, int anDropNo, decimal anYDis, decimal anDropDis1, decimal anDropDis2, decimal anDropDis3, decimal anDropDis4, decimal anDropDis5, string anDetectorEnd, int anDetectorNo, decimal anDetectorDis1, decimal anDetectorDis2, decimal anDetectorDis3, decimal anDetectorDis4, decimal anDetectorDis5, string bluetooth, string uvType, string marvel, int irNo, decimal irDis1, decimal irDis2, decimal irDis3)
        {
            swPart = swComp.GetModelDoc2();
            if (height == "650")
            {
                swPart.Parameter("D2@草图1").SystemValue = (deepth - 669m + 95m + 95m - 2m) / 1000m;
                swPart.Parameter("D1@Sketch5").SystemValue = (86.5m + 95m) / 1000m;
                swPart.Parameter("D1@Sketch6").SystemValue = (86.5m + 95m) / 1000m;
            }
            else
            {
                swPart.Parameter("D2@草图1").SystemValue = (deepth - 669m - 2m) / 1000m;
                swPart.Parameter("D1@Sketch5").SystemValue = 86.5m / 1000m;
                swPart.Parameter("D1@Sketch6").SystemValue = 86.5m / 1000m;
            }

            swPart.Parameter("D1@草图6").SystemValue = (deepth - 896m - 2m) / 1000m;
            swPart.Parameter("D1@草图1").SystemValue = (length - 6.5m) / 1000m;

            if (height == "650" || (height != "650" && length >= 2100m && length <= 2400m))
            {
                swFeat = swComp.FeatureByName("Edge-Flange1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("Edge-Flange2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("Break-Corner1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("Cut-Extrude4");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("Cut-Extrude5");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("ANDTECACROSS");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("Edge-Flange3");
                swFeat.SetSuppression2(2, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                swFeat = swComp.FeatureByName("Edge-Flange3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("Edge-Flange1");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("Edge-Flange2");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("Break-Corner1");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("Cut-Extrude4");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("Cut-Extrude5");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("ANDTECACROSS");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@Sketch23").SystemValue = 84.5m / 1000m;
                swPart.Parameter("D3@草图25").SystemValue = midRoofTopHoleDis;
                swPart.Parameter("D2@草图26").SystemValue = (deepth - 840m) / 3000m;
            }
            swPart.Parameter("D1@Sketch3").SystemValue = midRoofSecondHoleDis - 2m / 1000m;
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
                swPart.Parameter("D1@Sketch1").SystemValue = (lightYDis - 360m) / 1000m;
                if (ledSpotNo == 1) swPart.Parameter("D2@Sketch1").SystemValue = 0;
                else
                {
                    swPart.Parameter("D2@Sketch1").SystemValue = (ledSpotDis * (ledSpotNo / 2m - 1) + ledSpotDis / 2m) / 1000m;
                    swFeat = swComp.FeatureByName("LPattern2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                    swPart.Parameter("D1@LPattern2").SystemValue = ledSpotNo;
                    swPart.Parameter("D3@LPattern2").SystemValue = ledSpotDis / 1000m;
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
                swPart.Parameter("D1@Sketch7").SystemValue = (lightYDis - 360m) / 1000m;
                if (ledSpotNo == 1) swPart.Parameter("D5@Sketch7").SystemValue = 0;
                else
                {
                    swPart.Parameter("D5@Sketch7").SystemValue = (ledSpotDis * (ledSpotNo / 2m - 1) + ledSpotDis / 2m) / 1000m;
                    swFeat = swComp.FeatureByName("LPattern3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                    swPart.Parameter("D1@LPattern3").SystemValue = ledSpotNo;
                    swPart.Parameter("D3@LPattern3").SystemValue = ledSpotDis / 1000m;
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
                swPart.Parameter("D1@Sketch20").SystemValue = (lightYDis - 360m) / 1000m;
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
                swPart.Parameter("D1@Sketch21").SystemValue = (lightYDis - 360m) / 1000m;
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
                    swPart.Parameter("D1@Sketch11").SystemValue = anDropDis1 / 1000m;
                    swPart.Parameter("D3@Sketch11").SystemValue = (anYDis - 360m) / 1000m;
                }
                if (anDropNo > 1)
                {
                    swFeat = swComp.FeatureByName("AN2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch12").SystemValue = anDropDis2 / 1000m;
                }
                if (anDropNo > 2)
                {
                    swFeat = swComp.FeatureByName("AN3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch13").SystemValue = anDropDis3 / 1000m;
                }
                if (anDropNo > 3)
                {
                    swFeat = swComp.FeatureByName("AN4");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch14").SystemValue = anDropDis4 / 1000m;
                }
                if (anDropNo > 4)
                {
                    swFeat = swComp.FeatureByName("AN5");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch15").SystemValue = anDropDis5 / 1000m;
                }
                //探测器
                //swFeat = swComp.FeatureByName("ANDTECACROSS");
                //swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                if (uw == "UW")
                {
                    if (anDetectorNo > 0)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch31").SystemValue = anDetectorDis1 / 1000m;
                        if (anDetectorEnd == "LEFT" || (anDetectorEnd == "RIGHT" && anDetectorNo == 1))
                            swPart.Parameter("D2@Sketch31").SystemValue = 195m / 1000m;
                        else swPart.Parameter("D2@Sketch31").SystemValue = 175m / 1000m;
                    }
                    if (anDetectorNo > 1)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch32").SystemValue = anDetectorDis2 / 1000m;
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 2) swPart.Parameter("D2@Sketch32").SystemValue = 195m / 1000m;
                        else swPart.Parameter("D2@Sketch32").SystemValue = 175m / 1000m;
                    }
                    if (anDetectorNo > 2)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch33").SystemValue = anDetectorDis3 / 1000m;
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 3) swPart.Parameter("D2@Sketch33").SystemValue = 195m / 1000m;
                        else swPart.Parameter("D2@Sketch33").SystemValue = 175m / 1000m;
                    }
                    if (anDetectorNo > 3)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC4");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch34").SystemValue = anDetectorDis4 / 1000m;
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 4) swPart.Parameter("D2@Sketch34").SystemValue = 195m / 1000m;
                        else swPart.Parameter("D2@Sketch34").SystemValue = 175m / 1000m;
                    }
                    if (anDetectorNo > 4)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC5");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch35").SystemValue = anDetectorDis5 / 1000m;
                        if (anDetectorEnd == "RIGHT" && anDetectorNo == 5) swPart.Parameter("D2@Sketch35").SystemValue = 195m / 1000m;
                        else swPart.Parameter("D2@Sketch35").SystemValue = 175m / 1000m;
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
                                                    //swFeat = swComp.FeatureByName("ANDTECACROSS");
                                                    //swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            }

            //开方孔，UV或待MARVEL时解压
            swFeat = swComp.FeatureByName("CUT-BACK-LEFT");
            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
            swFeat = swComp.FeatureByName("CUT-BACK-RIGHT");
            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
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

            if (uvType == "DOUBLE")
            {
                swFeat = swComp.FeatureByName("UVCABLE-DOUBLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                swPart.Parameter("D1@Sketch41").SystemValue = (exRightDis - 3.25m) / 1000m;
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
            }
            else if (uvType == "LONG")
            {
                swFeat = swComp.FeatureByName("UVCABLE-DOUBLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                swPart.Parameter("D4@草图28").SystemValue = (exRightDis - 3.25m) / 1000m;
                swPart.Parameter("D3@草图28").SystemValue = 1500m / 1000m;
            }
            else if (uvType == "SHORT")
            {
                swFeat = swComp.FeatureByName("UVCABLE-DOUBLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                swPart.Parameter("D4@草图28").SystemValue = (exRightDis - 3.25m) / 1000m;
                swPart.Parameter("D3@草图28").SystemValue = 790m / 1000m;
            }
            else
            {
                swFeat = swComp.FeatureByName("UVCABLE-DOUBLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 

            }

            //400新风腔IR安装孔
            if (marvel == "YES" && height == "400")
            {
                if (irNo > 0)
                {
                    swFeat = swComp.FeatureByName("MAINS1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch37").SystemValue = irDis1 / 1000m;
                }
                if (irNo > 1)
                {
                    swFeat = swComp.FeatureByName("MAINS2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch38").SystemValue = irDis2 / 1000m;
                }
                if (irNo > 2)
                {
                    swFeat = swComp.FeatureByName("MAINS3");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch39").SystemValue = irDis3 / 1000m;
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
        }



        public void FNHM0032(Component2 swComp, string uw, decimal deepth, string height, decimal midRoofTopHoleDis)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D2@Sketch25").SystemValue = (deepth - 534m - 360m - 5m) / 1000m;
            swPart.Parameter("D1@Sketch32").SystemValue = midRoofTopHoleDis - 3m / 1000m;
            swPart.Parameter("D1@Sketch31").SystemValue = (deepth - 840m) / 3000;
            swFeat = swComp.FeatureByName("Cut-Extrude4");
            if (uw == "UW") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
            if (height == "650") swPart.Parameter("D1@Sketch25").SystemValue = (77m + 95m) / 1000;
            else swPart.Parameter("D1@Sketch25").SystemValue = 77m / 1000;
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

        #region 华为斜烟罩大侧板
        /// <summary>
        /// 左边大侧板外板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        /// <param name="sidePanelSideCjNo">侧板侧向CJ孔数量</param>
        /// <param name="sidePanelDownCjNo">侧板垂直CJ孔数量</param>
        public void FNHS0059(Component2 swComp, decimal deepth, decimal exHeight, decimal suHeight, string exType, int sidePanelSideCjNo, int sidePanelDownCjNo)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D1@草图1").SystemValue = deepth / 1000m;
            swPart.Parameter("D2@草图1").SystemValue = exHeight / 1000m;
            swPart.Parameter("D3@草图1").SystemValue = suHeight / 1000m;
            if (exType == "W")  swPart.Parameter("D4@草图1").SystemValue = 150m / 1000m;
            else swPart.Parameter("D4@草图1").SystemValue = 76m / 1000m;

            swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelDownCjNo;
            swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelSideCjNo - 1;
        }

        /// <summary>
        /// 左边大侧板内板
        /// </summary>
        /// <param name="swComp"></param>
        /// <param name="deepth">烟罩深度</param>
        /// <param name="height">烟罩高度</param>
        public void FNHS0061(Component2 swComp, decimal deepth, decimal exHeight, decimal suHeight, string exType)
        {
            swPart = swComp.GetModelDoc2();
            swPart.Parameter("D8@草图1").SystemValue = deepth / 1000m;//D3@Sketch1
            swPart.Parameter("D6@草图1").SystemValue = exHeight / 1000m;
            swPart.Parameter("D7@草图1").SystemValue = suHeight / 1000m;
            if (exType == "W") swPart.Parameter("D9@草图1").SystemValue = 150m / 1000m;
            else swPart.Parameter("D9@草图1").SystemValue = 76m / 1000m;

            if (suHeight == 400m)
            {
                swFeat = swComp.FeatureByName("I400");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
            }
            else
            {
                swFeat = swComp.FeatureByName("I400");
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
        public void FNHS0063(Component2 swComp, decimal deepth, decimal exHeight, decimal suHeight, string exType)
        {
            swPart = swComp.GetModelDoc2();
            if (exType == "W") swPart.Parameter("D2@Base-Flange1").SystemValue = (decimal)(Math.Sqrt(Math.Pow((double)deepth - 365d, 2) + Math.Pow((double)exHeight - (double)suHeight, 2))) / 1000m;
            else swPart.Parameter("D2@Base-Flange1").SystemValue = (decimal)(Math.Sqrt(Math.Pow((double)deepth - 291d, 2) + Math.Pow((double)exHeight - (double)suHeight, 2))) / 1000m;

            //swPart.Parameter("D3@Sketch11").SystemValue = deepth / 1000m;

            //swPart.Parameter("D2@Sketch11").SystemValue = suHeight / 1000m;//新风
            //swPart.Parameter("D8@Sketch11").SystemValue = exHeight / 1000m;//排风
            //if (exHeight == 450m)
            //{
            //    swPart.Parameter("D6@Sketch11").SystemValue = 105m / 1000m;//EX450,105
            //    swPart.Parameter("D11@Sketch11").SystemValue = 50m / 1000m;//EX450,50
            //    swPart.Parameter("D9@Sketch11").SystemValue = 135d / 180d * Math.PI;//EX450,135
            //}
            //else
            //{
            //    if (exType == "W")
            //    {
            //        swPart.Parameter("D6@Sketch11").SystemValue = 150m / 1000m;//普通76.5水洗150
            //        swPart.Parameter("D11@Sketch11").SystemValue = 101m / 1000m;//普通85水洗101
            //        swPart.Parameter("D9@Sketch11").SystemValue = 145d / 180d * Math.PI;//普通135水洗145
            //    }
            //    else
            //    {
            //        swPart.Parameter("D6@Sketch11").SystemValue = 76.5m / 1000m;//普通76.5水洗150
            //        swPart.Parameter("D11@Sketch11").SystemValue = 85m / 1000m;//普通85水洗101
            //        swPart.Parameter("D9@Sketch11").SystemValue = 135d / 180d * Math.PI;//普通135水洗145
            //    }
            //}
        }

        #endregion

    }
}
