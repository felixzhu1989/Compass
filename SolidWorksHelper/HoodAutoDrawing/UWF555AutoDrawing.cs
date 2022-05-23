using System;
using System.IO;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Windows.Forms;
using Common;

namespace SolidWorksHelper
{
    //2.实现接口具体方法
    public class UWF555AutoDrawing : IAutoDrawing
    {
        Component2 swComp;
        readonly UWF555Service objUwf555Service = new UWF555Service();
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
            UWF555 item = (UWF555)objUwf555Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix;//后缀
            ModelDoc2 swModel;
            ModelDoc2 swPart;
            AssemblyDoc swAssy;           
            Feature swFeat;
            EditPart swEdit = new EditPart();

            //打开Pack后的模型
            swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
            swAssy = swModel as AssemblyDoc;//装配体
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);//TopOnly参数设置成true，只重建顶层，不重建零件内部
            /*注意SolidWorks单位是m，计算是应当/1000d
             * 整形与整形运算得出的结果仍然时整形，1640 / 1000d结果为0，因此必须将其中一个转化成double型，使用后缀m就可以了
             * (int)不进行四舍五入，Convert.ToInt32会四舍五入
            */
            //-----------计算中间值，----------
            //新风面板卡扣数量及间距
            int frontPanelKaKouNo = (int)((item.Length - 300d) / 450d) + 2;
            double frontPanelKaKouDis = Convert.ToDouble((item.Length - 300d) / (frontPanelKaKouNo - 1));
            //新风面板螺丝孔数量及间距
            int frontPanelHoleNo = (int)((item.Length - 300d) / 900d) + 2;
            double frontPanelHoleDis = Convert.ToDouble((item.Length - 300) / (frontPanelHoleNo - 1));
            //新风CJ孔数量和新风CJ孔第一个CJ距离边缘距离
            int frontCjNo = (int)((item.Length - 30d) / 32d) + 1;
            double frontCjFirstDis = Convert.ToDouble((item.Length - (frontCjNo - 1) * 32d) / 2) / 1000d;
            //Midroof灯板螺丝孔数量及第二个孔距离边缘距离,灯板顶面吊装槽钢螺丝孔位距离
            int midRoofHoleNo = (int)((item.Length - 300d) / 400d);
            double midRoofSecondHoleDis = Convert.ToDouble((item.Length - (midRoofHoleNo - 1) * 400d) / 2) / 1000d;
            double midRoofTopHoleDis =
                Convert.ToDouble(item.Deepth - 535d - 360d - 90d -
                                  (int)((item.Deepth - 535d - 360d - 90d - 100d) / 50d) * 50d) / 1000d;
            //KSA数量，KSA侧板长度(以全长计算)水洗烟罩KSA在三角板内侧，减去3dm
            int ksaNo = (int)((item.Length - 2d) / 498d);
            double ksaSideLength = Convert.ToDouble((item.Length - 3d - ksaNo * 498d) / 2) / 1000d;
            //MESH侧板长度(除去排风三角板3dm计算)
            double meshSideLength = Convert.ToDouble((item.Length - 3d - (int)((item.Length - 2d) / 498d) * 498d) / 2) / 1000d;
            //侧板CJ孔整列到烟罩底部
            int sidePanelDownCjNo = (int)((item.Deepth - 95d) / 32d);
            //非水洗烟罩KV/UV
            //int sidePanelSideCjNo = (int)((item.Deepth - 305d) / 32d);
            //水洗烟罩KW/UW
            int sidePanelSideCjNo = (int)((item.Deepth - 380d) / 32d);


            try
            {
                //----------Top Level----------
                //烟罩深度
                swModel.Parameter("D1@Distance70").SystemValue = item.Deepth / 1000d;
                //判断KSA数量，KSA侧板长度，如果太小，则使用特殊小侧板侧边
                swFeat = swAssy.FeatureByName("LocalLPattern1");
                if (ksaNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern1").SystemValue = ksaNo; //D1阵列数量,D3阵列距离
                }
                //KSA距离左边缘
                if (ksaSideLength < 12d / 1000d) swModel.Parameter("D1@Distance56").SystemValue = 0.5d / 1000d;
                else swModel.Parameter("D1@Distance56").SystemValue = ksaSideLength;
                //下水口管件
                //进水口管件
                if (item.Inlet == "LEFT")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "Connection asm Inlet L-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "Connection asm KSA L-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHB0037-3");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHB0038-3");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "Connection asm Inlet R-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "Connection asm KSA R-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHB0037-4");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHB0038-4");
                    swComp.SetSuppression2(0); //2解压缩，0压缩 
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "Connection asm Inlet L-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "Connection asm KSA L-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHB0037-3");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHB0038-3");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "Connection asm Inlet R-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "Connection asm KSA R-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHB0037-4");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHB0038-4");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                }
                //排风脖颈数量和距离
                if (item.ExNo == 1)
                {
                    swModel.Parameter("D1@Distance66").SystemValue = (item.ExRightDis - item.ExLength / 2) / 1000d;
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.ExNo == 2 && (item.MARVEL == "YES" || item.ExHeight == 100d))
                {
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swModel.Parameter("D1@Distance66").SystemValue =
                        (item.ExRightDis - item.ExLength - item.ExDis / 2) / 1000d;
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern2").SystemValue = item.ExNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D3@LocalLPattern2").SystemValue =
                        (item.ExDis + item.ExLength) / 1000d; //D1阵列数量,D3阵列距离
                }
                //灯板加强筋
                if (item.Deepth > 1649d && ((item.LightType == "FSLONG" && item.Length > 1900d) ||
                                           (item.LightType == "FSSHORT" && item.Length > 1500d) || (item.Length > 2000d)))
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0006-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0006-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@Base-Flange1").SystemValue = item.Deepth / 1000d - 898d / 1000d;
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0006-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0006-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                //----------新风脖颈----------
                swFeat = swAssy.FeatureByName("LocalLPattern3");
                if (item.SuNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern3").SystemValue = item.SuNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D3@LocalLPattern3").SystemValue = item.SuDis / 1000d; //D1阵列数量,D3阵列距离
                }
                //----------新风前面板中间加强筋----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0011-1");
                if (item.Length > 1599d) swComp.SetSuppression2(2); //2解压缩，0压缩
                else swComp.SetSuppression2(0); //2解压缩，0压缩

                //----------排风腔----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0031-1");
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D1@草图1").SystemValue = item.Length / 1000d;
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
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0032-1");
                swEdit.FNHE0032(swComp, item.Length, midRoofSecondHoleDis, midRoofHoleNo, item.ExNo, item.ExRightDis, item.ExLength, item.ExWidth, item.ExDis, item.Inlet, item.ANSUL, item.ANSide, item.MARVEL, item.UVType, "UW");

                //----------UWHood排风腔顶部检修门盖板？----------
                if ((item.UVType == "SHORT" && item.Length >= 1600d) || (item.UVType == "LONG" && item.Length >= 2400d))
                {
                    if (item.Inlet == "LEFT")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0146-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0146-2");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                    }
                    else if (item.Inlet == "RIGHT")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0146-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0146-2");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0146-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0146-2");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0146-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0146-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }


                //----------排风腔前面板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0033-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = item.Length / 1000d;
                swFeat = swComp.FeatureByName("EXTAB-UP");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //UV HoodParent,过滤器感应出线孔，UV门，UV cable-UV灯线缆穿孔避让缺口
                swFeat = swComp.FeatureByName("FILTER-CABLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
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
                if (item.UVType == "LONG")
                {
                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch7").SystemValue = item.ExRightDis / 1000d;
                }
                else
                {
                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch6").SystemValue = item.ExRightDis / 1000d;
                }
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swPart.Parameter("D1@Sketch10").SystemValue = item.ExRightDis / 1000d;
                //非UVHood
                //swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                if (item.UVType == "LONG") swPart.Parameter("D2@Sketch10").SystemValue = 1500d / 1000d;
                else swPart.Parameter("D2@Sketch10").SystemValue = 790d / 1000d;
                //水洗挡板感应器穿线孔
                swFeat = swComp.FeatureByName("BFCABLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //----------三角板上的UV----------内部运水
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0034-1");
                swFeat = swComp.FeatureByName("UWHOOD");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("DRAINPIPE-NO");
                if (item.Outlet == "NO" && (item.SidePanel == "RIGHT" || item.SidePanel == "MIDDLE"))
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0035-1");
                swFeat = swComp.FeatureByName("DRAINPIPE-NO");
                if (item.Outlet == "NO" && (item.SidePanel == "LEFT" || item.SidePanel == "MIDDLE"))
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //----------水洗挡板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0036-2");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length- 105d - 1.5d) / 1000d;
                //2021.06.08 july更改模型，磁铁拉铆钉避让1.5dm
                swFeat = swComp.FeatureByName("UWHOOD");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //----------KSA侧边----------
                if (ksaSideLength < 12d / 1000d && ksaSideLength > 2d / 1000d)
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0003-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0004-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0005-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = ksaSideLength * 2;
                }
                else if (ksaSideLength < 25d / 1000d && ksaSideLength >= 12d / 1000d)
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0003-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength * 2;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0004-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0005-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0003-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0004-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0005-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                //----------排风滑门/导轨----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNCE0013-2");
                if (item.ExWidth == 300d) swComp.SetSuppression2(0); //2解压缩，0压缩
                else swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNCE0013-1");
                if (item.ExWidth == 300d) swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@Sketch1").SystemValue = (item.ExLength / 2 + 10d) / 1000d;
                    swPart.Parameter("D2@Sketch1").SystemValue = (item.ExWidth + 20d) / 1000d;
                }
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNCE0018-2");
                if (item.MARVEL == "YES") swComp.SetSuppression2(0); //2解压缩，0压缩
                else swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNCE0018-1");
                if (item.MARVEL == "YES") swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    if (item.ExNo == 1) swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 2 + 100d) / 1000d;
                    else swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 3 + item.ExDis + 100d) / 1000d;
                }
                //----------排风脖颈----------
                if (item.ANSUL != "YES" && (item.ExHeight == 100d || item.MARVEL == "YES"))
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0006-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0007-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0008-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0009-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0006-1");
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50d) / 1000d;
                    swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000d;
                    swFeat = swComp.FeatureByName("ANSUL");
                    if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0007-1");
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50d) / 1000d;
                    swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000d;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0008-1");
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                    swFeat = swComp.FeatureByName("ANDTEC");
                    if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0009-1");
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                    swFeat = swComp.FeatureByName("ANDTEC");
                    if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                //----------UV灯，UV灯门----------
                if (item.UVType == "LONG")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201050416-asm-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201050418-4");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201060409-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201050415-asm-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201050417-4");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201060410-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201050416-asm-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201050418-4");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201060409-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201050415-asm-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201050417-4");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201060410-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                }
                //----------MESH油网侧板----------
                if ((item.Inlet == "LEFT" && item.ANSide == "RIGHT") || (item.ANSide == "LEFT" && item.Inlet == "RIGHT"))//不同一侧
                {
                    if ((meshSideLength - 20d / 1000d) < 57d / 1000d) meshSideLength += 249d / 1000d;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0038-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
                    swFeat = swComp.FeatureByName("KW");
                    if (item.Inlet == "LEFT") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("ANSUL");
                    if (item.ANSUL == "YES" && item.ANSide == "LEFT")
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0039-3");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
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
                    if (meshSideLength * 2 < 57d / 1000d) meshSideLength += 249d / 1000d;
                    if ((meshSideLength - 20d / 1000d) > 57d / 1000d)
                    {
                        if (item.Inlet == "LEFT")
                        {
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0038-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSUL");
                            if (item.ANSUL == "YES" && item.ANSide == "LEFT")
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0039-3");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0038-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0039-3");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
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
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0038-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSUL");
                            if (item.ANSUL == "YES" && item.ANSide == "LEFT")
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0039-3");
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                        }
                        else
                        {
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0038-1");
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                            swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0039-3");
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
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0040-2");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Length - 8d) / 1000d;
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0015-2");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 5d) / 1000d;
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900500003-2");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Boss-Extrude1").SystemValue = (item.Length - 125d) / 1000d;
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0037-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 5d) / 1000d;

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0041-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Length - 5d) / 1000d;
                //非UVHood
                //swFeat = swComp.FeatureByName("UVDOOR-LONG");
                //swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                //swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                if (item.UVType == "LONG")
                {
                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D12@Sketch3").SystemValue = (item.ExRightDis - 2.5d) / 1000d;
                }
                else
                {
                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D10@Sketch4").SystemValue = (item.ExRightDis - 2.5d) / 1000d;
                }

                //----------灯具----------
                //日光灯
                if (item.LightType == "FSLONG")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201020410-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201020409-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else if (item.LightType == "FSSHORT")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201020410-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201020409-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201020410-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201020409-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                //----------MiddleRoof灯板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0001-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 4d) / 1000d;
                swPart.Parameter("D2@草图1").SystemValue = (item.Deepth - 669d) / 1000d;
                swPart.Parameter("D1@草图6").SystemValue = (item.Deepth - 896d) / 1000d;
                swPart.Parameter("D3@草图25").SystemValue = midRoofTopHoleDis;
                swPart.Parameter("D2@草图26").SystemValue = (item.Deepth - 840d) / 3000;
                swPart.Parameter("D1@Sketch3").SystemValue = midRoofSecondHoleDis - 2d / 1000d;
                swFeat = swComp.FeatureByName("LPattern1");
                if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                    swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
                }
                //灯具
                if (item.LightType == "LED60")
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
                    if (item.LEDSpotNo == 1) swPart.Parameter("D2@Sketch1").SystemValue = 0;
                    else
                    {
                        swPart.Parameter("D2@Sketch1").SystemValue = (item.LEDSpotDis * (item.LEDSpotNo / 2d - 1) + item.LEDSpotDis / 2d) / 1000d;
                        swFeat = swComp.FeatureByName("LPattern2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                        swPart.Parameter("D1@LPattern2").SystemValue = item.LEDSpotNo;
                        swPart.Parameter("D3@LPattern2").SystemValue = item.LEDSpotDis / 1000d;
                    }
                }
                else if (item.LightType == "LED140")
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
                    if (item.LEDSpotNo == 1) swPart.Parameter("D5@Sketch7").SystemValue = 0;
                    else
                    {
                        swPart.Parameter("D5@Sketch7").SystemValue = (item.LEDSpotDis * (item.LEDSpotNo / 2d - 1) + item.LEDSpotDis / 2d) / 1000d;
                        swFeat = swComp.FeatureByName("LPattern3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                        swPart.Parameter("D1@LPattern3").SystemValue = item.LEDSpotNo;
                        swPart.Parameter("D3@LPattern3").SystemValue = item.LEDSpotDis / 1000d;
                    }
                }
                else if (item.LightType == "FSLONG")
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
                else if (item.LightType == "FSSHORT")
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
                if (item.ANSUL == "YES")
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
                    if (item.ANDropNo > 0)
                    {
                        swFeat = swComp.FeatureByName("AN1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch11").SystemValue = item.ANDropDis1 / 1000d;
                        swPart.Parameter("D3@Sketch11").SystemValue = (item.ANYDis - 360d) / 1000d;
                    }
                    if (item.ANDropNo > 1)
                    {
                        swFeat = swComp.FeatureByName("AN2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch12").SystemValue = item.ANDropDis2 / 1000d;
                    }
                    if (item.ANDropNo > 2)
                    {
                        swFeat = swComp.FeatureByName("AN3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch13").SystemValue = item.ANDropDis3 / 1000d;
                    }
                    if (item.ANDropNo > 3)
                    {
                        swFeat = swComp.FeatureByName("AN4");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch14").SystemValue = item.ANDropDis4 / 1000d;
                    }
                    if (item.ANDropNo > 4)
                    {
                        swFeat = swComp.FeatureByName("AN5");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch15").SystemValue = item.ANDropDis5 / 1000d;
                    }
                    //探测器
                    swFeat = swComp.FeatureByName("ANDTECACROSS");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    if (item.ANDetectorNo > 0)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch31").SystemValue = item.ANDetectorDis1 / 1000d;
                        if (item.ANDetectorEnd == "LEFT" || (item.ANDetectorEnd == "RIGHT" && item.ANDetectorNo == 1))
                            swPart.Parameter("D2@Sketch31").SystemValue = 195d / 1000d;
                        else swPart.Parameter("D2@Sketch31").SystemValue = 175d / 1000d;
                    }
                    if (item.ANDetectorNo > 1)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch32").SystemValue = item.ANDetectorDis2 / 1000d;
                        if(item.ANDetectorEnd == "RIGHT" && item.ANDetectorNo == 2) swPart.Parameter("D2@Sketch32").SystemValue = 195d / 1000d;
                        else swPart.Parameter("D2@Sketch32").SystemValue = 175d / 1000d;
                    }
                    if (item.ANDetectorNo > 2)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch33").SystemValue = item.ANDetectorDis3 / 1000d;
                        if (item.ANDetectorEnd == "RIGHT" && item.ANDetectorNo == 3) swPart.Parameter("D2@Sketch33").SystemValue = 195d / 1000d;
                        else swPart.Parameter("D2@Sketch33").SystemValue = 175d / 1000d;
                    }
                    if (item.ANDetectorNo > 3)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC4");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch34").SystemValue = item.ANDetectorDis4 / 1000d;
                        if (item.ANDetectorEnd == "RIGHT" && item.ANDetectorNo == 4) swPart.Parameter("D2@Sketch34").SystemValue = 195d / 1000d;
                        else swPart.Parameter("D2@Sketch34").SystemValue = 175d / 1000d;
                    }
                    if (item.ANDetectorNo > 4)
                    {
                        swFeat = swComp.FeatureByName("ANDTEC5");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch35").SystemValue = item.ANDetectorDis5 / 1000d;
                        if (item.ANDetectorEnd == "RIGHT" && item.ANDetectorNo == 5) swPart.Parameter("D2@Sketch35").SystemValue = 195d / 1000d;
                        else swPart.Parameter("D2@Sketch35").SystemValue = 175d / 1000d;
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
                swFeat = swComp.FeatureByName("CUT-BACK-LEFT");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("CUT-BACK-RIGHT");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                if (item.Bluetooth == "YES")
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
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                if (item.UVType == "LONG")
                {
                    swPart.Parameter("D4@草图28").SystemValue = item.ExRightDis / 1000d;
                    swPart.Parameter("D3@草图28").SystemValue = 1500d / 1000d;
                }
                else
                {
                    swPart.Parameter("D4@草图28").SystemValue = item.ExRightDis / 1000d;
                    swPart.Parameter("D3@草图28").SystemValue = 790d / 1000d;
                }

                swFeat = swComp.FeatureByName("MAINS1");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MAINS2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("MAINS3");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩


                //----------吊装槽钢----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100001-1");
                swPart = swComp.GetModelDoc2();
                if (item.ANSUL == "YES") swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 250) / 1000d;
                else swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 100d) / 1000d;

                #region 大侧板
                if (item.SidePanel == "BOTH")
                {
                    //LEFT
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0001-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0001(swComp, item.Deepth, 555d, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0002-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0002(swComp, item.Deepth, 555d);

                    //RIGHT
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0003-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0003(swComp, item.Deepth, 555d, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0004-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0004(swComp, item.Deepth, 555d);

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0005-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swEdit.FNHS0005(swComp, item.Deepth, 555d, 555d, "W");//普通烟罩"V"，水洗"W"
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0006-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swEdit.FNHS0006(swComp, item.Deepth, 555d, 555d, "W");//普通烟罩"V"，水洗"W"
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0005-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0006-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                else if (item.SidePanel == "LEFT")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0003-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0004-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0001-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0001(swComp, item.Deepth, 555d, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0002-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0002(swComp, item.Deepth, 555d);

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0006-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0005-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swEdit.FNHS0005(swComp, item.Deepth, 555d, 555d, "W");//普通烟罩"V"，水洗"W"
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0005-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0006-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                else if (item.SidePanel == "RIGHT")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0001-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0002-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0003-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0003(swComp, item.Deepth, 555d, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0004-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0004(swComp, item.Deepth, 555d);

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0005-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0006-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swEdit.FNHS0006(swComp, item.Deepth, 555d, 555d, "W");//普通烟罩"V"，水洗"W"
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0005-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0006-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0001-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0002-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0003-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0004-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0005-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0006-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                } 
                #endregion

                //------------F型新风腔主体----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0004-1");
                swEdit.FNHA0004(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, midRoofSecondHoleDis, midRoofTopHoleDis, midRoofHoleNo, item.SuDis, item.SuNo, item.MARVEL, item.IRNo, item.IRDis1, item.IRDis2, item.IRDis3, item.SidePanel, "UV", item.Bluetooth);

                //----------新风前面板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0007-1");
                swEdit.FNHA0007(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, frontPanelHoleNo, frontPanelHoleDis);
                //----------蜂窝板压条----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0008-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 6d) / 1000d;
                swPart.Parameter("D1@LPattern1").SystemValue = frontPanelHoleNo;
                swPart.Parameter("D3@LPattern1").SystemValue = frontPanelHoleDis / 1000d;
                //----------镀锌隔板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0006-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 6d) / 1000d;
                //MARVEL
                if (item.MARVEL == "YES")
                {
                    if (item.IRNo > 0)
                    {
                        swFeat = swComp.FeatureByName("MA1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch2").SystemValue = item.IRDis1 / 1000d;
                    }
                    if (item.IRNo > 1)
                    {
                        swFeat = swComp.FeatureByName("MA2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch3").SystemValue = item.IRDis2 / 1000d;
                    }
                    if (item.IRNo > 2)
                    {
                        swFeat = swComp.FeatureByName("MA3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch4").SystemValue = item.IRDis3 / 1000d;
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
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0010-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@草图1").SystemValue = (item.Length - 200d) / 1000d;
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0010-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 200d) / 1000d;
                //----------新风底部CJ孔板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0005-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.Length / 1000d;
                swPart.Parameter("D1@CJHOLES").SystemValue = frontCjNo;
                swPart.Parameter("D10@草图8").SystemValue = frontCjFirstDis;
                swPart.Parameter("D1@LPattern1").SystemValue = frontPanelHoleNo;
                swPart.Parameter("D3@LPattern1").SystemValue = frontPanelHoleDis / 1000d;
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
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900200001-1");
                if (item.Bluetooth == "YES") swComp.SetSuppression2(2); //2解压缩，0压缩
                else swComp.SetSuppression2(0); //2解压缩，0压缩
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900300005-1");
                if (item.LEDlogo == "YES") swComp.SetSuppression2(2); //2解压缩，0压缩
                else swComp.SetSuppression2(0); //2解压缩，0压缩
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201010401-1");
                if (item.LEDlogo == "YES") swComp.SetSuppression2(2); //2解压缩，0压缩
                else swComp.SetSuppression2(0); //2解压缩，0压缩

                swModel.ForceRebuild3(true);//设置成true，直接更新顶层，速度很快，设置成false，每个零件都会更新，很慢
                swModel.Save();//保存，很耗时间
                swApp.CloseDoc(packedAssyPath);//关闭，很快
            }
            catch (Exception ex)
            {
                throw new Exception($"{packedAssyPath} 作图过程发生异常。\n零件：{swComp.Name}\n详细：{ex.Message}");
            }
            finally
            {
                swApp.CommandInProgress = false; //及时关闭外部命令调用，否则影响SolidWorks的使用
            }
        }
    }
}
