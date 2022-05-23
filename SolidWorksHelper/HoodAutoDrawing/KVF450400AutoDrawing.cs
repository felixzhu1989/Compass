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
    public class KVF450400AutoDrawing : IAutoDrawing
    {
        Component2 swComp; 
        readonly KVF450400Service objKVF450400Service = new KVF450400Service();
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
            KVF450400 item = (KVF450400)objKVF450400Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

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
            //KSA数量，KSA侧板长度(以全长计算)
            int ksaNo = (int)((item.Length + 1) / 498d);
            double ksaSideLength = Convert.ToDouble((item.Length - ksaNo * 498d) / 2) / 1000d;
            //MESH侧板长度(除去排风三角板3dm计算)
            double meshSideLength = Convert.ToDouble((item.Length - 3d - (int)((item.Length - 2d) / 498d) * 498d) / 2) / 1000d;

            //侧板CJ孔整列到烟罩底部
            //int sidePanelDownCjNo = (int)((item.Deepth - 95d) / 32d);
            //非水洗烟罩KV/UV
            //int sidePanelSideCjNo = (int)((item.Deepth - 305d) / 32d);
            //水洗烟罩KW/UW
            //int sidePanelSideCjNo = (int)((item.Deepth - 380) / 32);

            //KVF450400斜侧板CJ孔计算,106为排风底部长度，450-400为高度差
            int sidePanelDownCjNo = (int)(((double)(Math.Sqrt(Math.Pow((double)item.Deepth - 106d, 2) + Math.Pow(450d - 400d, 2))) - 95d) / 32d);
            int sidePanelSideCjNo = sidePanelDownCjNo - 3;


            try
            {
                //----------Top Level----------
                //烟罩深度
                swModel.Parameter("D1@Distance86").SystemValue = item.Deepth / 1000d;
                //判断KSA数量，KSA侧板长度，如果太小，则使用特殊小侧板侧边
                swFeat = swAssy.FeatureByName("LocalLPattern1");
                if (ksaNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern1").SystemValue = ksaNo; //D1阵列数量,D3阵列距离
                }
                //KSA距离左边缘
                if (ksaSideLength < 12d / 1000d) swModel.Parameter("D1@Distance79").SystemValue = 0.5d / 1000d;
                else swModel.Parameter("D1@Distance79").SystemValue = ksaSideLength;

                //油塞
                if (item.Outlet == "LEFTTAP")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100014-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100014-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                }
                else if (item.Outlet == "RIGHTTAP")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100014-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100014-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100014-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100014-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                //排风脖颈数量和距离
                if (item.ExNo == 1)
                {
                    swModel.Parameter("D1@Distance81").SystemValue = (item.ExRightDis - item.ExLength / 2) / 1000d;
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
                    swModel.Parameter("D1@Distance81").SystemValue =
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
                //400新风腔IR安装支架
                if (item.MARVEL == "YES")
                {
                    if (item.IRNo > 0)
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHO0118-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                    }
                    if (item.IRNo > 1)
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHO0118-2");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                    }
                    if (item.IRNo > 2)
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHO0118-3");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                    }
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHO0118-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHO0118-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHO0118-3");
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
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0030-1");
                if (item.Length > 1599d) swComp.SetSuppression2(2); //2解压缩，0压缩
                else swComp.SetSuppression2(0); //2解压缩，0压缩

                //----------450排风腔----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0051-1");
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D1@草图1").SystemValue = item.Length / 1000d;
                swPart.Parameter("D2@Sketch3").SystemValue = midRoofSecondHoleDis;
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
                if (item.ExNo == 1)
                {
                    swFeat = swComp.FeatureByName("EXCOONE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("EXCOTWO");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D4@Sketch9").SystemValue = item.ExRightDis / 1000d;
                    swPart.Parameter("D2@Sketch9").SystemValue = item.ExLength / 1000d;
                    swPart.Parameter("D3@Sketch9").SystemValue = item.ExWidth / 1000d;
                }
                else
                {
                    swFeat = swComp.FeatureByName("EXCOONE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("EXCOTWO");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D5@Sketch10").SystemValue = item.ExRightDis / 1000d;
                    swPart.Parameter("D1@Sketch10").SystemValue = item.ExDis / 1000d;
                    swPart.Parameter("D3@Sketch10").SystemValue = item.ExLength / 1000d;
                    swPart.Parameter("D4@Sketch10").SystemValue = item.ExWidth / 1000d;
                }
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
                //油塞
                if (item.Outlet == "LEFTTAP")
                {
                    swFeat = swComp.FeatureByName("DRAINTAP-LEFT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINTAP-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.Outlet == "RIGHTTAP")
                {
                    swFeat = swComp.FeatureByName("DRAINTAP-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINTAP-RIGHT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("DRAINTAP-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINTAP-RIGHT");
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
                //ANSUL
                if (item.ANSUL == "YES")
                {
                    //侧喷
                    if (item.ANSide == "LEFT")
                    {
                        swFeat = swComp.FeatureByName("ANSUL-LEFT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    }
                    else if (item.ANSide == "RIGHT")
                    {
                        swFeat = swComp.FeatureByName("ANSUL-RIGHT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("ANSUL-LEFT");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANSUL-RIGHT");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                    //探测器
                    if (item.ANDetector == "RIGHT" || item.ANDetector == "BOTH")
                    {
                        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    }
                    if (item.ANDetector == "LEFT" || item.ANDetector == "BOTH")
                    {
                        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                }
                //MARVEL
                swFeat = swComp.FeatureByName("MA-NTC");
                if (item.MARVEL == "YES")
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    if (item.ExNo == 1) swPart.Parameter("D1@Sketch21").SystemValue = (item.ExRightDis + item.ExLength / 2 + 50d) / 1000d;
                    else swPart.Parameter("D1@Sketch21").SystemValue = (item.ExRightDis + item.ExDis / 2 + item.ExLength + 50d) / 1000d;
                }
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //UVHood,UVRack-UV灯架孔和UV cable-UV灯线缆穿孔
                swFeat = swComp.FeatureByName("UVRACK");
                //非UVHood
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("UVCABLE");
                //非UVHood
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩


                //----------排风腔前面板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0056-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = item.Length / 1000d;
                swFeat = swComp.FeatureByName("EXTAB-UP");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //UV HoodParent,过滤器感应出线孔，UV门，UV cable-UV灯线缆穿孔避让缺口
                swFeat = swComp.FeatureByName("FILTER-CABLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //非UVHood
                swFeat = swComp.FeatureByName("UVDOOR-LONG");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                swFeat = swComp.FeatureByName("UVCABLE");
                //非UVHood
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩


                //----------KSA侧边----------
                if (ksaSideLength < 12d / 1000d && ksaSideLength > 2d / 1000d)
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0003-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0004-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0005-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = ksaSideLength * 2;
                    swFeat = swComp.FeatureByName("Cut-Extrude1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                else if (ksaSideLength < 25d / 1000d && ksaSideLength >= 12d / 1000d)
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0003-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength * 2;
                    swFeat = swComp.FeatureByName("Cut-Extrude2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    if (item.WaterCollection == "YES" && (item.SidePanel == "BOTH" || item.SidePanel == "LEFT"))
                    {
                        swFeat = swComp.FeatureByName("DRAINCHANNEL");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    }
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0004-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0005-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0003-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swFeat = swComp.FeatureByName("Cut-Extrude2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    if (item.WaterCollection == "YES" && (item.SidePanel == "BOTH" || item.SidePanel == "LEFT"))
                    {
                        swFeat = swComp.FeatureByName("DRAINCHANNEL");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    }
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0004-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swFeat = swComp.FeatureByName("Cut-Extrude2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    if (item.WaterCollection == "YES" && (item.SidePanel == "BOTH" || item.SidePanel == "RIGHT"))
                    {
                        swFeat = swComp.FeatureByName("DRAINCHANNEL");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    }
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0005-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }

                //----------排风滑门/导轨----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNCE0013-5");
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
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNCE0018-4");
                if (item.MARVEL == "YES") swComp.SetSuppression2(0); //2解压缩，0压缩
                else swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNCE0018-3");
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
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0006-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0007-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0008-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0009-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0006-2");
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
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0009-2");
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                }

                //----------排风三角板----------
                if (item.ANSUL == "YES" && item.SidePanel == "MIDDLE")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0052-1");
                    swFeat = swComp.FeatureByName("ANDTEC");
                    swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
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
                swEdit.FNHM0001(swComp, "KV", item.Length, item.Deepth, 450d, 400d, item.ExRightDis, midRoofTopHoleDis, midRoofSecondHoleDis, midRoofHoleNo, item.LightType, 0, item.LEDSpotNo, item.LEDSpotDis, item.ANSUL, item.ANDropNo, item.ANYDis, item.ANDropDis1, item.ANDropDis2, item.ANDropDis3, item.ANDropDis4, item.ANDropDis5, "NO",0, 0, 0, 0, 0, 0, "NO", "NO", item.MARVEL, item.IRNo, item.IRDis1, item.IRDis2, item.IRDis3);

                //----------吊装槽钢----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100001-1");
                swPart = swComp.GetModelDoc2();
                if (item.ANSUL == "YES") swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 250) / 1000d;
                else swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 100d) / 1000d;

                #region 大侧板
                if (item.SidePanel == "BOTH")
                {
                    //LEFT
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0055-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0055(swComp, item.Deepth, 450d, 400d, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0056-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0056(swComp, item.Deepth, 450d, 400d);

                    //RIGHT
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0057-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0057(swComp, item.Deepth, 450d, 400d, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0058-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0058(swComp, item.Deepth, 450d, 400d);

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0005-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swEdit.FNHS0005(swComp, item.Deepth, 450d, 400d, "V");//普通烟罩"V"，水洗"W"
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0006-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swEdit.FNHS0006(swComp, item.Deepth, 450d, 400d, "V");//普通烟罩"V"，水洗"W"
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
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0057-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0058-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    //LEFT
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0055-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0055(swComp, item.Deepth, 450d, 400d, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0056-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0056(swComp, item.Deepth, 450d, 400d);

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0006-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0005-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swEdit.FNHS0005(swComp, item.Deepth, 460d, 400d, "V");//普通烟罩"V"，水洗"W"
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
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0055-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0056-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                                               //RIGHT
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0057-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0057(swComp, item.Deepth, 450d, 400d, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0058-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0058(swComp, item.Deepth, 450d, 400d);

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0005-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0006-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swEdit.FNHS0006(swComp, item.Deepth, 460d, 400d, "V");//普通烟罩"V"，水洗"W"
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
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0055-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0056-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0057-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0058-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0005-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0006-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                #endregion

                //------------F型新风腔主体----------

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0021-1");
                swEdit.FNHA0021(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, midRoofSecondHoleDis, midRoofTopHoleDis, midRoofHoleNo, item.SuDis, item.SuNo, item.MARVEL, item.IRNo, item.IRDis1, item.IRDis2, item.IRDis3, item.SidePanel, "KV", "NO");


                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0027-1");
                swEdit.FNHA0027(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, frontPanelHoleNo, frontPanelHoleDis);
                //----------蜂窝板压条----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0029-5");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 6d) / 1000d;
                swPart.Parameter("D1@LPattern1").SystemValue = frontPanelHoleNo;
                swPart.Parameter("D3@LPattern1").SystemValue = frontPanelHoleDis / 1000d;

                //----------镀锌隔板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0022-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 6d) / 1000d;
                //MARVEL
                if (item.MARVEL == "YES")
                {
                    swFeat = swComp.FeatureByName("IR3FAN");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    if (item.IRNo > 0)
                    {
                        swFeat = swComp.FeatureByName("MA1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch59").SystemValue = item.IRDis1 / 1000d;
                    }
                    if (item.IRNo > 1)
                    {
                        swFeat = swComp.FeatureByName("MA2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch60").SystemValue = item.IRDis2 / 1000d;
                    }
                    if (item.IRNo > 2)
                    {
                        swFeat = swComp.FeatureByName("MA3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch61").SystemValue = item.IRDis3 / 1000d;
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
                    swFeat = swComp.FeatureByName("IR3FAN");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                //----------IR支架----------
                if (item.MARVEL == "YES")
                {
                    if (item.IRNo > 0)
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHO0118-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                    }
                    if (item.IRNo > 1)
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHO0118-2");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                    }
                    if (item.IRNo > 2)
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHO0118-3");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                    }
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHO0118-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHO0118-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHO0118-3");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                //----------新风滑门导轨----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0010-4");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@草图1").SystemValue = (item.Length - 200d) / 1000d;
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0010-6");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 200d) / 1000d;
                //----------新风底部CJ孔板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0005-4");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.Length / 1000d;
                swPart.Parameter("D1@CJHOLES").SystemValue = frontCjNo;
                swPart.Parameter("D10@草图8").SystemValue = frontCjFirstDis;
                swPart.Parameter("D1@LPattern1").SystemValue = frontPanelHoleNo;
                swPart.Parameter("D3@LPattern1").SystemValue = frontPanelHoleDis / 1000d;
                swFeat = swComp.FeatureByName("BLUETOOTH");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
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
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900200001-2");
                swComp.SetSuppression2(0); //2解压缩，0压缩
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900300005-2");
                if (item.LEDlogo == "YES") swComp.SetSuppression2(2); //2解压缩，0压缩
                else swComp.SetSuppression2(0); //2解压缩，0压缩
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201010401-2");
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
