using System;
using System.IO;
using System.Windows.Forms;
using Common;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class HWUWF555AutoDrawing : IAutoDrawing
    {
        HWUWF555Service objHWUWF555Service = new HWUWF555Service();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            //创建项目模型存放地址
            string itemPath = $@"{projectPath}\{tree.Item}-{tree.Module}-{tree.CategoryName}";
            if (!CommonFunc.CreateProjectPath(itemPath)) return;
            //Pack的后缀
            string suffix = $@"{tree.Item}-{tree.Module}-{tree.ODPNo.Substring(tree.ODPNo.Length - 6)}";

            //判断文件是否存在，如果存在将不执行pack，如果不存在则执行pack
            //packango后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = $@"{itemPath}\{tree.CategoryName.ToLower()}_{suffix}.sldasm";
            if (!File.Exists(packedAssyPath)) packedAssyPath = CommonFunc.PackAndGoFunc(suffix, swApp, tree.ModelPath, itemPath);

            //查询参数
            HWUWF555 item = (HWUWF555)objHWUWF555Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix;//后缀
            ModelDoc2 swModel = default(ModelDoc2);
            ModelDoc2 swPart = default(ModelDoc2);
            AssemblyDoc swAssy = default(AssemblyDoc);
            Component2 swComp;
            Feature swFeat = default(Feature);
            EditPart swEdit = new EditPart();

            //打开Pack后的模型
            swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
            swAssy = swModel as AssemblyDoc;//装配体
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);//TopOnly参数设置成true，只重建顶层，不重建零件内部
            /*注意SolidWorks单位是m，计算是应当/1000m
             * 整形与整形运算得出的结果仍然时整形，1640 / 1000m结果为0，因此必须将其中一个转化成decimal型，使用后缀m就可以了
             * (int)不进行四舍五入，Convert.ToInt32会四舍五入
            */
            //-----------计算中间值，----------
            //新风面板卡扣数量及间距
            int frontPanelKaKouNo = (int)((item.Length - 300m) / 450m) + 2;
            decimal frontPanelKaKouDis = Convert.ToDecimal((item.Length - 300m) / (frontPanelKaKouNo - 1)) / 1000m;
            //新风面板螺丝孔数量及间距
            int frontPanelHoleNo = (int)((item.Length - 300m) / 900m) + 2;
            decimal frontPanelHoleDis = Convert.ToDecimal((item.Length - 300) / (frontPanelHoleNo - 1)) / 1000m;
            //新风CJ孔数量和新风CJ孔第一个CJ距离边缘距离
            int frontCjNo = (int)((item.Length - 30m) / 32m) + 1;
            decimal frontCjFirstDis = Convert.ToDecimal((item.Length - (frontCjNo - 1) * 32m) / 2) / 1000m;
            //Midroof灯板螺丝孔数量及第二个孔距离边缘距离,灯板顶面吊装槽钢螺丝孔位距离
            int midRoofHoleNo = (int)((item.Length - 300m) / 400m);
            decimal midRoofSecondHoleDis = Convert.ToDecimal((item.Length - (midRoofHoleNo - 1) * 400m) / 2) / 1000m;
            decimal midRoofTopHoleDis =
                Convert.ToDecimal(item.Deepth - 535m - 360m - 90m -
                                  (int)((item.Deepth - 535m - 360m - 90m - 100m) / 50m) * 50m) / 1000m;
            //KSA数量，KSA侧板长度(以全长计算)水洗烟罩KSA在三角板内侧，减去3mm
            int ksaNo = (int)((item.Length - 2m) / 498m);
            decimal ksaSideLength = Convert.ToDecimal((item.Length - 3m - ksaNo * 498m) / 2) / 1000m;
            //MESH侧板长度(除去排风三角板3mm计算)
            decimal meshSideLength = Convert.ToDecimal((item.Length - 3m - (int)((item.Length - 2m) / 498m) * 498m) / 2) / 1000m;
            //侧板CJ孔整列到烟罩底部
            //int sidePanelDownCjNo = (int)((item.Deepth - 95m) / 32m);
            ////非水洗烟罩KV/UV
            ////int sidePanelSideCjNo = (int)((item.Deepth - 305m) / 32m);
            ////水洗烟罩KW/UW
            //int sidePanelSideCjNo = (int)((item.Deepth - 380m) / 32m);

            //HWUWF555斜侧板CJ孔计算,150为排风底部长度，555-400为高度差
            int sidePanelDownCjNo = (int)(((decimal)(Math.Sqrt(Math.Pow((double)item.Deepth - 150d, 2) + Math.Pow(555d - 400d, 2))) - 95m) / 32m);
            int sidePanelSideCjNo = sidePanelDownCjNo - 3;


            try
            {
                //----------Top Level----------
                //烟罩深度
                swModel.Parameter("D1@Distance75").SystemValue = item.Deepth / 1000m;
                //判断KSA数量，KSA侧板长度，如果太小，则使用特殊小侧板侧边
                swFeat = swAssy.FeatureByName("LocalLPattern1");
                if (ksaNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern1").SystemValue = ksaNo; //D1阵列数量,D3阵列距离
                }
                //KSA距离左边缘
                if (ksaSideLength < 12m / 1000m) swModel.Parameter("D1@Distance71").SystemValue = 0.5m / 1000m;
                else swModel.Parameter("D1@Distance71").SystemValue = ksaSideLength;
                //下水口管件
                //进水口管件
                //if (item.Inlet == "LEFT")
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "Connection asm Inlet L-2"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "Connection asm KSA L-2"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHB0037-3"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHB0038-3"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "Connection asm Inlet R-2"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "Connection asm KSA R-2"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHB0037-4"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHB0038-4"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩 
                //}
                //else
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "Connection asm Inlet L-2"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "Connection asm KSA L-2"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHB0037-3"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHB0038-3"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "Connection asm Inlet R-2"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "Connection asm KSA R-2"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHB0037-4"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHB0038-4"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //}
                //排风脖颈数量和距离
                if (item.ExNo == 1)
                {
                    swModel.Parameter("D1@Distance68").SystemValue = (item.ExRightDis - item.ExLength / 2) / 1000m;
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swModel.Parameter("D1@Distance68").SystemValue =
                        (item.ExRightDis - item.ExLength - item.ExDis / 2) / 1000m;
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern2").SystemValue = item.ExNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D3@LocalLPattern2").SystemValue =
                        (item.ExDis + item.ExLength) / 1000m; //D1阵列数量,D3阵列距离
                }
                //?灯板加强筋
                //if (item.Deepth > 1649m && ((item.LightType == "FSLONG" && item.Length > 1900m) ||
                //                           (item.LightType == "FSSHORT" && item.Length > 1500m) || (item.Length > 2000m)))
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0006-1"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0006-2"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swPart = swComp.GetModelDoc2();//打开零件
                //    swPart.Parameter("D2@Base-Flange1").SystemValue = item.Deepth / 1000m - 898m / 1000m;
                //}
                //else
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0006-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0006-2"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //}
                //----------新风脖颈----------
                swFeat = swAssy.FeatureByName("LocalLPattern3");
                if (item.SuNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern3").SystemValue = item.SuNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D3@LocalLPattern3").SystemValue = item.SuDis / 1000m; //D1阵列数量,D3阵列距离
                }
                //----------新风前面板中间加强筋----------
                //swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0105-1"));
                //if (item.Length > 1599m) swComp.SetSuppression2(2); //2解压缩，0压缩
                //else swComp.SetSuppression2(0); //2解压缩，0压缩




                //----------排风腔----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0147-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D1@草图1").SystemValue = item.Length / 1000m;
                //集水翻边
                if (item.WaterCollection == "YES")
                {
                    if (item.SidePanel == "RIGHT")
                    {
                        swFeat = swComp.FeatureByName("DRAINCHANNEL-RIGHT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DRAINCHANNEL-LEFT");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                    else if (item.SidePanel == "LEFT")
                    {
                        swFeat = swComp.FeatureByName("DRAINCHANNEL-LEFT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DRAINCHANNEL-RIGHT");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                    else if (item.SidePanel == "BOTH")
                    {
                        swFeat = swComp.FeatureByName("DRAINCHANNEL-RIGHT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DRAINCHANNEL-LEFT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    }
                }
                else
                {
                    swFeat = swComp.FeatureByName("DRAINCHANNEL-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINCHANNEL-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                //下水口
                if (item.Outlet == "LEFT")
                {
                    swFeat = swComp.FeatureByName("DRAINPIPE-LEFT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINPIPE-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.Outlet == "RIGHT")
                {
                    swFeat = swComp.FeatureByName("DRAINPIPE-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINPIPE-RIGHT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("DRAINPIPE-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINPIPE-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                //背靠背
                if (item.BackToBack == "YES")
                {
                    swFeat = swComp.FeatureByName("BACKTOBACK");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("BACKTOBACK");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                //----------排风腔顶部零件----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0148-1"));
                swEdit.FNHE0148(swComp, item.Length, midRoofSecondHoleDis, midRoofHoleNo, item.ExNo, item.ExRightDis, item.ExLength, item.ExWidth, item.ExDis, item.Inlet, item.ANSUL, item.ANSide, item.MARVEL, item.UVType, "UW");

                //----------UWHood排风腔顶部检修门盖板？----------
                //if ((item.UVType == "SHORT" && item.Length >= 1600m) || (item.UVType == "LONG" && item.Length >= 2400m))
                //{
                //    if (item.Inlet == "LEFT")
                //    {
                //        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0146-1"));
                //        swComp.SetSuppression2(0); //2解压缩，0压缩
                //        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0146-2"));
                //        swComp.SetSuppression2(2); //2解压缩，0压缩
                //    }
                //    else if (item.Inlet == "RIGHT")
                //    {
                //        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0146-1"));
                //        swComp.SetSuppression2(2); //2解压缩，0压缩
                //        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0146-2"));
                //        swComp.SetSuppression2(0); //2解压缩，0压缩
                //    }
                //    else
                //    {
                //        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0146-1"));
                //        swComp.SetSuppression2(0); //2解压缩，0压缩
                //        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0146-2"));
                //        swComp.SetSuppression2(0); //2解压缩，0压缩
                //    }
                //}
                //else
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0146-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0146-2"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //}



                //----------排风腔前面板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0149-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = item.Length / 1000m;
                //swFeat = swComp.FeatureByName("EXTAB-UP");
                //swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //UV HoodParent,过滤器感应出线孔，UV门，UV cable-UV灯线缆穿孔避让缺口
                //swFeat = swComp.FeatureByName("FILTER-CABLE");
                //swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //非UVHood
                //swFeat = swComp.FeatureByName("UVDOOR-LONG");
                //swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                //swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                if (item.Inlet == "LEFT")
                {
                    swFeat = swComp.FeatureByName("PIPEIN-LEFT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("PIPEIN-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("PIPEIN-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("PIPEIN-RIGHT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                //UV灯门
                if (item.UVType == "DOUBLE")
                {
                    swFeat = swComp.FeatureByName("UVDOOR-DOUBLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch6").SystemValue = item.ExRightDis / 1000m;

                    swFeat = swComp.FeatureByName("UVCABLE-DOUBLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D6@Sketch11").SystemValue = item.ExRightDis / 1000m;

                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                    swFeat = swComp.FeatureByName("UVCABLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.UVType == "LONG")
                {
                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch17").SystemValue = item.ExRightDis / 1000m;

                    swFeat = swComp.FeatureByName("UVCABLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D4@Sketch10").SystemValue = item.ExRightDis / 1000m;
                    swPart.Parameter("D3@Sketch10").SystemValue = 1500m / 1000m;

                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-DOUBLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVCABLE-DOUBLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.UVType == "SHORT")
                {
                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch15").SystemValue = item.ExRightDis / 1000m;

                    swFeat = swComp.FeatureByName("UVCABLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D4@Sketch10").SystemValue = item.ExRightDis / 1000m;
                    swPart.Parameter("D3@Sketch10").SystemValue = 790m / 1000m;

                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-DOUBLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVCABLE-DOUBLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                    swFeat = swComp.FeatureByName("UVCABLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                    swFeat = swComp.FeatureByName("UVDOOR-DOUBLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVCABLE-DOUBLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                //水洗挡板感应器穿线孔
                swFeat = swComp.FeatureByName("BFCABLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩

                //----------三角板上的UV----------内部运水
                //swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0034-1"));
                //swFeat = swComp.FeatureByName("UWHOOD");
                //swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //swFeat = swComp.FeatureByName("DRAINPIPE-NO");
                //if (item.Outlet == "NO" && (item.SidePanel == "RIGHT" || item.SidePanel == "MIDDLE"))
                //    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0035-1"));
                //swFeat = swComp.FeatureByName("DRAINPIPE-NO");
                //if (item.Outlet == "NO" && (item.SidePanel == "LEFT" || item.SidePanel == "MIDDLE"))
                //    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                //----------水洗挡板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0150-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 105m - 1.5m) / 1000m;
                //2021.06.08 july更改模型，磁铁拉铆钉避让1.5mm
                swFeat = swComp.FeatureByName("UWHOOD");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩

                //----------KSA侧边----------
                if (ksaSideLength <= 3.3m / 1000m)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0160-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0161-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0170-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else if (ksaSideLength < 12m / 1000m && ksaSideLength > 3.3m / 1000m)//华为板材厚，改成3.3无法折弯
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0160-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0161-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0170-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = ksaSideLength * 2;
                }
                else if (ksaSideLength < 25m / 1000m && ksaSideLength >= 12m / 1000m)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0160-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength * 2;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0161-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0170-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0160-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0161-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0170-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }

                //----------排风滑门/导轨----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0164-1"));
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.ExLength / 2 + 10m) / 1000m;
                swPart.Parameter("D2@Sketch1").SystemValue = (item.ExWidth + 20m) / 1000m;


                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0165-1"));
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                if (item.ExNo == 1) swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 2 + 100m) / 1000m;
                else swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 2 + 20m) / 1000m;


                //----------排风脖颈----------

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0166-1"));
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50m) / 1000m;
                swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000m;
                swFeat = swComp.FeatureByName("ANSUL");
                if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0167-1"));
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50m) / 1000m;
                swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000m;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0168-6"));
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000m;
                swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000m;
                //swFeat = swComp.FeatureByName("ANDTEC");
                //if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0169-1"));
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000m;
                swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000m;
                //swFeat = swComp.FeatureByName("ANDTEC");
                //if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                //----------UV灯，UV灯门----------
                if (item.UVType == "DOUBLE")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0155-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0156-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0157-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0158-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0158-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0171-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0172-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0172-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0155-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0156-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0157-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0158-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0158-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0171-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0172-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0172-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                //----------MESH油网侧板----------
                if ((item.Inlet == "LEFT" && item.ANSide == "RIGHT") || (item.ANSide == "LEFT" && item.Inlet == "RIGHT"))//不同一侧
                {
                    if ((meshSideLength - 20m / 1000m) < 57m / 1000m) meshSideLength = meshSideLength + 249m / 1000m;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0162-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20m / 1000m;
                    swFeat = swComp.FeatureByName("KW");
                    if (item.Inlet == "LEFT") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("ANSUL");
                    if (item.ANSUL == "YES" && item.ANSide == "LEFT")
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0163-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20m / 1000m;
                    swFeat = swComp.FeatureByName("KW");
                    if (item.Inlet == "RIGHT") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("ANSUL");
                    if (item.ANSUL == "YES" && item.ANSide == "RIGHT")
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    if (meshSideLength * 2 < 57m / 1000m) meshSideLength = meshSideLength + 249m / 1000m;
                    if ((meshSideLength - 20m / 1000m) > 57m / 1000m)
                    {
                        if (item.Inlet == "LEFT")
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0162-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20m / 1000m;
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSUL");
                            if (item.ANSUL == "YES" && item.ANSide == "LEFT")
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0163-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20m / 1000m;
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0162-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20m / 1000m;
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0163-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20m / 1000m;
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSUL");
                            if (item.ANSUL == "YES" && item.ANSide == "RIGHT")
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                    }
                    else
                    {
                        if (item.Inlet == "LEFT")
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0162-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSUL");
                            if (item.ANSUL == "YES" && item.ANSide == "LEFT")
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0163-1"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                        }
                        else
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0162-1"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0163-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSUL");
                            if (item.ANSUL == "YES" && item.ANSide == "RIGHT")
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                    }
                }

                //----------排风腔内部零件----------
                //MESH油网下导轨
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0154-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Length - 8m) / 1000m;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0153-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 5m) / 1000m;

                //KSA下轨道
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0151-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 5m) / 1000m;

                //MESH油网轨道支架
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0152-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 5m) / 1000m;
                //UV灯门铰链孔
                if (item.UVType == "DOUBLE")
                {
                    swFeat = swComp.FeatureByName("UVDOOR-DOUBLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch10").SystemValue = (item.ExRightDis - 2.5m) / 1000m;
                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.UVType == "LONG")
                {
                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch12").SystemValue = (item.ExRightDis - 2.5m) / 1000m;
                    swFeat = swComp.FeatureByName("UVDOOR-DOUBLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.UVType == "SHORT")
                {
                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch11").SystemValue = (item.ExRightDis - 2.5m) / 1000m;
                    swFeat = swComp.FeatureByName("UVDOOR-DOUBLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("UVDOOR-DOUBLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }


                //----------灯具----------
                //日光灯
                //if (item.LightType == "FSLONG")
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020410-1"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020409-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //}
                //else if (item.LightType == "FSSHORT")
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020410-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020409-1"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //}
                //else
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020410-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020409-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //}
                //----------MiddleRoof灯板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0031-1"));
                swEdit.FNHM0031(swComp,"UW",item.Length,item.Deepth,"555",item.ExRightDis,midRoofTopHoleDis,midRoofSecondHoleDis,midRoofHoleNo,item.LightType,item.LightYDis,item.LEDSpotNo,item.LEDSpotDis,item.ANSUL,item.ANDropNo,item.ANYDis,item.ANDropDis1,item.ANDropDis2,item.ANDropDis3,item.ANDropDis4,item.ANDropDis5,item.ANDetectorEnd,item.ANDetectorNo,item.ANDetectorDis1,item.ANDetectorDis2,item.ANDetectorDis3,item.ANDetectorDis4,item.ANDetectorDis5,item.Bluetooth,item.UVType,item.MARVEL,item.IRNo,item.IRDis1,item.IRDis2,item.IRDis3);
                
                //华为灯板左右加高
                if (item.Length >= 2200m && item.Length <= 2400m)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0032-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0032-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHM0032(swComp, "UW", item.Deepth, "555", midRoofTopHoleDis);
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0032-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0032-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }

                //----------吊装槽钢----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100001-1"));
                swPart = swComp.GetModelDoc2();
                if (item.ANSUL == "YES") swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 250) / 1000m;
                else swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 100m) / 1000m;

                //----------大侧板----------
                if (item.SidePanel == "BOTH")
                {
                    //LEFT
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0067-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0001(swComp, item.Deepth, 555m, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0068-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0002(swComp, item.Deepth, 555m);

                    //RIGHT
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0069-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0003(swComp, item.Deepth, 555m, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0070-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0004(swComp, item.Deepth, 555m);

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0071-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Deepth - 368m) / 1000m;//水洗烟罩(item.Deepth - 368m) / 1000m;
                        swPart.Parameter("D5@Sketch7").SystemValue = 19.87m / 1000m;//水洗烟罩19.87m / 1000m
                        swPart.Parameter("D5@Sketch10").SystemValue = 27.3m / 1000m;
                        //UV555400，22m,标准烟罩27.3
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0072-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Deepth - 368m) / 1000m;//水洗烟罩(item.Deepth - 368m) / 1000m;
                        swPart.Parameter("D5@Sketch7").SystemValue = 19.87m / 1000m;//水洗烟罩19.87m / 1000m
                        swPart.Parameter("D5@Sketch8").SystemValue = 27.3m / 1000m;
                        //UV555400，22m,标准烟罩27.3
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0071-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0072-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                else if (item.SidePanel == "LEFT")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0069-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0070-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0067-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0001(swComp, item.Deepth, 555m, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0068-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0002(swComp, item.Deepth, 555m);

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0072-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0071-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Deepth - 368m) / 1000m;//水洗烟罩(item.Deepth - 368m) / 1000m;
                        swPart.Parameter("D5@Sketch7").SystemValue = 19.87m / 1000m;//水洗烟罩19.87m / 1000m
                        swPart.Parameter("D5@Sketch10").SystemValue = 27.3m / 1000m;
                        //UV555400，22m,标准烟罩27.3
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0071-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0072-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                else if (item.SidePanel == "RIGHT")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0067-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0068-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0069-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0003(swComp, item.Deepth, 555m, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0070-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0004(swComp, item.Deepth, 555m);

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0071-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0082-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Deepth - 368m) / 1000m;//水洗烟罩(item.Deepth - 368) / 1000m;
                        swPart.Parameter("D5@Sketch7").SystemValue = 19.87m / 1000m;//水洗烟罩19.87m / 1000m
                        swPart.Parameter("D5@Sketch8").SystemValue = 27.3m / 1000m;
                        //UV555400，22m,标准烟罩27.3
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0071-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0072-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0067-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0068-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0069-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0070-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0071-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0072-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }



                //------------F型新风腔主体----------

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0106-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.Length / 1000m;
                swPart.Parameter("D1@阵列(线性)1").SystemValue = frontPanelKaKouNo;
                swPart.Parameter("D3@阵列(线性)1").SystemValue = frontPanelKaKouDis;
                swPart.Parameter("D3@Sketch3").SystemValue = midRoofSecondHoleDis;
                swPart.Parameter("D4@草图7").SystemValue = 200m / 1000m - midRoofTopHoleDis;
                swFeat = swComp.FeatureByName("LPattern1");
                if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
                }
                //新风脖颈
                swPart.Parameter("D3@Sketch6").SystemValue = (item.SuDis * (item.SuNo / 2 - 1) + item.SuDis / 2m) / 1000m;
                swFeat = swComp.FeatureByName("LPattern2");
                if (item.SuNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern2").SystemValue = item.SuNo;
                    swPart.Parameter("D3@LPattern2").SystemValue = item.SuDis / 1000m;
                }
                //MARVEL
                if (item.MARVEL == "YES")
                {
                    if (item.IRNo > 0)
                    {
                        swFeat = swComp.FeatureByName("MA1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch16").SystemValue = item.IRDis1 / 1000m;
                        swFeat = swComp.FeatureByName("MACABLE1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        if (item.SuNo == 1) swPart.Parameter("D1@Sketch19").SystemValue = (item.Length + 400) / 2000m;
                        else swPart.Parameter("D1@Sketch19").SystemValue = item.Length / 2000m;
                    }
                    if (item.IRNo > 1)
                    {
                        swFeat = swComp.FeatureByName("MA2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch17").SystemValue = item.IRDis2 / 1000m;
                        swFeat = swComp.FeatureByName("MACABLE2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch19").SystemValue = (item.Length / 2 - 25m) / 1000m;
                        swPart.Parameter("D1@Sketch20").SystemValue = 50m / 1000m;
                    }
                    if (item.IRNo > 2)
                    {
                        swFeat = swComp.FeatureByName("MA3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch18").SystemValue = item.IRDis3 / 1000m;
                        swFeat = swComp.FeatureByName("MACABLE3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch19").SystemValue = 220m / 1000m;
                        swPart.Parameter("D1@Sketch20").SystemValue = (item.Length - 440m) / 2000m;
                        swPart.Parameter("D1@Sketch21").SystemValue = (item.Length - 440m) / 2000m;
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
                //UV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                if (item.Bluetooth == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (item.SidePanel == "LEFT" || item.SidePanel == "BOTH") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //----------新风前面板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0107-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 3m) / 1000m;
                swPart.Parameter("D1@阵列(线性)7").SystemValue = frontPanelKaKouNo;
                swPart.Parameter("D3@阵列(线性)7").SystemValue = frontPanelKaKouDis;
                swPart.Parameter("D1@LPattern1").SystemValue = frontPanelHoleNo;
                swPart.Parameter("D3@LPattern1").SystemValue = frontPanelHoleDis;
                //----------蜂窝板压条----------
                //swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0008-1"));
                //swPart = swComp.GetModelDoc2();
                //swPart.Parameter("D1@草图1").SystemValue = (item.Length - 6m) / 1000m;
                //swPart.Parameter("D1@LPattern1").SystemValue = frontPanelHoleNo;
                //swPart.Parameter("D3@LPattern1").SystemValue = frontPanelHoleDis;
                //----------镀锌隔板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0006-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 6m) / 1000m;
                //MARVEL
                if (item.MARVEL == "YES")
                {
                    if (item.IRNo > 0)
                    {
                        swFeat = swComp.FeatureByName("MA1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch2").SystemValue = item.IRDis1 / 1000m;
                    }
                    if (item.IRNo > 1)
                    {
                        swFeat = swComp.FeatureByName("MA2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch3").SystemValue = item.IRDis2 / 1000m;
                    }
                    if (item.IRNo > 2)
                    {
                        swFeat = swComp.FeatureByName("MA3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch4").SystemValue = item.IRDis3 / 1000m;
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
                }
                //----------新风滑门导轨----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0097-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 200m) / 1000m;
                //swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0010-1"));
                //swPart = swComp.GetModelDoc2();
                //swPart.Parameter("D1@草图1").SystemValue = (item.Length - 200m) / 1000m;
                //----------新风底部CJ孔板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0093-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.Length / 1000m;
                swPart.Parameter("D1@阵列(线性)5").SystemValue = frontCjNo;
                swPart.Parameter("D10@草图8").SystemValue = frontCjFirstDis;
                swPart.Parameter("D1@LPattern1").SystemValue = frontPanelHoleNo;
                swPart.Parameter("D3@LPattern1").SystemValue = frontPanelHoleDis;
                swFeat = swComp.FeatureByName("BLUETOOTH");
                if (item.Bluetooth == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("LOGO");
                if (item.LEDlogo == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //集水翻边
                if (item.WaterCollection == "YES")
                {
                    if (item.SidePanel == "RIGHT")
                    {
                        swFeat = swComp.FeatureByName("DRAINCHANNEL-RIGHT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DRAINCHANNEL-LEFT");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                    else if (item.SidePanel == "LEFT")
                    {
                        swFeat = swComp.FeatureByName("DRAINCHANNEL-RIGHT");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DRAINCHANNEL-LEFT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    }
                    else if (item.SidePanel == "BOTH")
                    {
                        swFeat = swComp.FeatureByName("DRAINCHANNEL-RIGHT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DRAINCHANNEL-LEFT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                    }
                }
                else
                {
                    swFeat = swComp.FeatureByName("DRAINCHANNEL-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINCHANNEL-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                //----------蓝牙和LOGO----------
                //swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900200001-1"));
                //if (item.Bluetooth == "YES") swComp.SetSuppression2(2); //2解压缩，0压缩
                //else swComp.SetSuppression2(0); //2解压缩，0压缩
                //swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900300005-1"));
                //if (item.LEDlogo == "YES") swComp.SetSuppression2(2); //2解压缩，0压缩
                //else swComp.SetSuppression2(0); //2解压缩，0压缩
                //swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201010401-1"));
                //if (item.LEDlogo == "YES") swComp.SetSuppression2(2); //2解压缩，0压缩
                //else swComp.SetSuppression2(0); //2解压缩，0压缩


                swModel.ForceRebuild3(true);//设置成true，直接更新顶层，速度很快，设置成false，每个零件都会更新，很慢
                swModel.Save();//保存，很耗时间
                swApp.CloseDoc(packedAssyPath);//关闭，很快
            }
            catch (Exception ex)
            {
                throw new Exception(packedAssyPath + "作图过程发生异常，详细：" + ex.Message);
            }
            finally
            {
                swApp.CommandInProgress = false; //及时关闭外部命令调用，否则影响SolidWorks的使用
            }
        }
    }
}
