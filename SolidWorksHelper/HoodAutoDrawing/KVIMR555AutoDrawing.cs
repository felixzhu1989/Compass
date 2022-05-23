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
    public class KVIMR555AutoDrawing : IAutoDrawing
    {
        Component2 swComp; 
        readonly KVIMR555Service objKVIMR555Service = new KVIMR555Service();
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
            KVIMR555 item = (KVIMR555)objKVIMR555Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

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
            double midRoofTopHoleDis = Convert.ToDouble(item.Deepth / 2d - 400d - 130d - 75d - (int)((item.Deepth / 2d - 400d - 130d - 75d - 50d) / 50d) * 50d) / 1000d;
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
            //int sidePanelSideCjNo = (int)((item.Deepth - 380d) / 32d);
            //M型烟罩
            int sidePanelSideCjNo = (int)((item.Deepth - 217d) / 32d) - 5;
            int sidePanelDownCjNo = sidePanelSideCjNo + 6;

            try
            {
                //----------Top Level----------
                //烟罩深度
                swModel.Parameter("D1@Distance45").SystemValue = (item.Deepth - 800d) / 2000d;
                swModel.Parameter("D1@Distance46").SystemValue = (item.Deepth - 800d) / 2000d;
                //判断KSA数量，KSA侧板长度，如果太小，则使用特殊小侧板侧边
                swFeat = swAssy.FeatureByName("LocalLPattern1");
                if (ksaNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern1").SystemValue = ksaNo; //D1阵列数量,D3阵列距离
                }
                //KSA距离左边缘
                if (ksaSideLength < 12d / 1000d) swModel.Parameter("D1@Distance41").SystemValue = 0.5d / 1000d;
                else swModel.Parameter("D1@Distance41").SystemValue = ksaSideLength;

                //油塞
                if (item.Outlet == "LEFTTAP")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100014-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100014-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                }
                else if (item.Outlet == "RIGHTTAP")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100014-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100014-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100014-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100014-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                //排风脖颈数量和距离
                if (item.ExNo == 1)
                {
                    swModel.Parameter("D1@Distance40").SystemValue = (item.ExRightDis - item.ExLength / 2) / 1000d;
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
                    swModel.Parameter("D1@Distance40").SystemValue =
                        (item.ExRightDis - item.ExLength - item.ExDis / 2) / 1000d;
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern2").SystemValue = item.ExNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D3@LocalLPattern2").SystemValue =
                        (item.ExDis + item.ExLength) / 1000d; //D1阵列数量,D3阵列距离
                }
                //灯板加强筋
                //if (item.Deepth > 1649d && ((item.LightType == "FSLONG" && item.Length > 1900d) ||
                //                           (item.LightType == "FSSHORT" && item.Length > 1500d) || (item.Length > 2000d)))
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0006-1"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0006-2"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swPart = swComp.GetModelDoc2();//打开零件
                //    swPart.Parameter("D2@Base-Flange1").SystemValue = item.Deepth / 1000d - 898d / 1000d;
                //}
                //else
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0006-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0006-2"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //}
                //----------排风腔----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0017-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.Length / 1000d;
                swPart.Parameter("D3@Sketch14").SystemValue = midRoofSecondHoleDis;
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
                    swPart.Parameter("D3@Sketch12").SystemValue = item.ExRightDis / 1000d;
                    swPart.Parameter("D1@Sketch12").SystemValue = item.ExLength / 1000d;
                    swPart.Parameter("D2@Sketch12").SystemValue = item.ExWidth / 1000d;
                }
                else
                {
                    swFeat = swComp.FeatureByName("EXCOONE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("EXCOTWO");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D4@Sketch13").SystemValue = item.ExRightDis / 1000d;
                    swPart.Parameter("D3@Sketch13").SystemValue = item.ExDis / 1000d;
                    swPart.Parameter("D1@Sketch13").SystemValue = item.ExLength / 1000d;
                    swPart.Parameter("D2@Sketch13").SystemValue = item.ExWidth / 1000d;
                }


                //ANSUL
                //if (item.ANSUL == "YES")
                //{
                //    //侧喷
                //    if (item.ANSide == "LEFT")
                //    {
                //        swFeat = swComp.FeatureByName("ANSUL-LEFT");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //    }
                //    else if (item.ANSide == "RIGHT")
                //    {
                //        swFeat = swComp.FeatureByName("ANSUL-RIGHT");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //    }
                //    else
                //    {
                //        swFeat = swComp.FeatureByName("ANSUL-LEFT");
                //        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //        swFeat = swComp.FeatureByName("ANSUL-RIGHT");
                //        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //    }
                //    //探测器
                //    if (item.ANDetector == "RIGHT" || item.ANDetector == "BOTH")
                //    {
                //        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //    }
                //    if (item.ANDetector == "LEFT" || item.ANDetector == "BOTH")
                //    {
                //        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //    }
                //    else
                //    {
                //        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                //        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                //        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                //        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                //        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //    }
                //}
                //MARVEL
                if (item.MARVEL == "YES")
                {
                    swFeat = swComp.FeatureByName("MA-TAB");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("MA-NTC");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    if (item.ExNo == 1) swPart.Parameter("D1@Sketch17").SystemValue = (item.ExRightDis + item.ExLength / 2 + 50d) / 1000d;
                    else swPart.Parameter("D1@Sketch17").SystemValue = (item.ExRightDis + item.ExDis / 2 + item.ExLength + 50d) / 1000d;
                }
                else
                {
                    swFeat = swComp.FeatureByName("MA-TAB");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("MA-NTC");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                //UVHood,UVRack-UV灯架孔和UV cable-UV灯线缆穿孔
                swFeat = swComp.FeatureByName("UVRACK");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                //UVHood解压特征
                swFeat = swComp.FeatureByName("UV-TAB");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("KSACABLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX UV");
                if (item.MARVEL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else   swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX LIGHT");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩


                //----------排风腔前面板、后面板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0018-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = item.Length / 1000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0019-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = item.Length / 1000d;


                //----------排风腔底部----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0020-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.Length / 1000d;

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

                //----------三角板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0021-1"));
                //UV烟罩解压缩特征
                swFeat = swComp.FeatureByName("Cut-Extrude2");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                //----------KSA侧边----------
                if (ksaSideLength < 12d / 1000d && ksaSideLength > 2d / 1000d)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0003-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0004-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0005-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = ksaSideLength * 2;
                    //背面
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0027-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0026-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0028-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = ksaSideLength * 2;


                }
                else if (ksaSideLength < 25d / 1000d && ksaSideLength >= 12d / 1000d)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0003-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength * 2;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0004-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0005-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    //背面
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0027-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength * 2;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0026-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0028-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩

                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0003-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0004-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0005-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    //背面
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0027-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0026-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0028-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }

                //----------排风滑门/导轨----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0013-2"));
                if (item.ExWidth == 300d) swComp.SetSuppression2(0); //2解压缩，0压缩
                else swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0013-1"));
                if (item.ExWidth == 300d) swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@Sketch1").SystemValue = (item.ExLength / 2 + 10d) / 1000d;
                    swPart.Parameter("D2@Sketch1").SystemValue = (item.ExWidth + 20d) / 1000d;
                }
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0018-2"));
                if (item.MARVEL == "YES") swComp.SetSuppression2(0); //2解压缩，0压缩
                else swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0018-1"));
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
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0006-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0007-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0008-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0009-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0006-1"));
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50d) / 1000d;
                    swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000d;
                    swFeat = swComp.FeatureByName("ANSUL");
                    if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0007-2"));
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50d) / 1000d;
                    swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000d;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0008-1"));
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0009-2"));
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
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

                //----------MiddleRoof灯板/前----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0011-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 4d) / 1000d;
                swPart.Parameter("D2@草图1").SystemValue = ((item.Deepth - 800d - 260d) / 2d - 3d + 226d) / 1000d;
                swPart.Parameter("D1@草图6").SystemValue = ((item.Deepth - 800d - 260d) / 2d - 3d + 1d) / 1000d;
                swPart.Parameter("D3@草图25").SystemValue = midRoofTopHoleDis;
                if ((item.Deepth - 800d - 260d) / 2d < 150d)
                    swPart.Parameter("D2@草图26").SystemValue = ((item.Deepth - 800d - 260d) / 2d + 55d) / 3000d - 30d / 1000d;
                else
                    swPart.Parameter("D2@草图26").SystemValue = ((item.Deepth - 800d - 260d) / 2d + 55d) / 3000d;
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
                //if (item.ANSUL == "YES")
                //{
                //    swFeat = swComp.FeatureByName("AN1");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN2");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN3");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN4");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN5");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //    if (item.ANDropNo > 0)
                //    {
                //        swFeat = swComp.FeatureByName("AN1");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch11").SystemValue = item.ANDropDis1 / 1000d;
                //        swPart.Parameter("D3@Sketch11").SystemValue = (item.ANYDis - 360d) / 1000d;
                //    }
                //    if (item.ANDropNo > 1)
                //    {
                //        swFeat = swComp.FeatureByName("AN2");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch12").SystemValue = item.ANDropDis2 / 1000d;
                //    }
                //    if (item.ANDropNo > 2)
                //    {
                //        swFeat = swComp.FeatureByName("AN3");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch13").SystemValue = item.ANDropDis3 / 1000d;
                //    }
                //    if (item.ANDropNo > 3)
                //    {
                //        swFeat = swComp.FeatureByName("AN4");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch14").SystemValue = item.ANDropDis4 / 1000d;
                //    }
                //    if (item.ANDropNo > 4)
                //    {
                //        swFeat = swComp.FeatureByName("AN5");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch15").SystemValue = item.ANDropDis5 / 1000d;
                //    }
                //}
                //else
                //{
                //    swFeat = swComp.FeatureByName("AN1");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN2");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN3");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN4");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN5");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                //}
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
                //开方孔，UV或待MARVEL时解压
                swFeat = swComp.FeatureByName("CUT-BACK-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("CUT-BACK-RIGHT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("CUT-FRONT-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 

                //UV灯线缆穿孔
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 

                //----------MiddleRoof灯板/后----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0012-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 4d) / 1000d;
                swPart.Parameter("D2@草图1").SystemValue = ((item.Deepth - 800d - 260d) / 2d - 3d + 226d) / 1000d;
                swPart.Parameter("D1@草图6").SystemValue = ((item.Deepth - 800d - 260d) / 2d - 3d + 1d) / 1000d;
                swPart.Parameter("D3@草图25").SystemValue = midRoofTopHoleDis;
                if ((item.Deepth - 800d - 260d) / 2d < 150d)
                    swPart.Parameter("D2@草图26").SystemValue = ((item.Deepth - 800d - 260d) / 2d + 55d) / 3000d - 30d / 1000d;
                else
                    swPart.Parameter("D2@草图26").SystemValue = ((item.Deepth - 800d - 260d) / 2d + 55d) / 3000d;
                swPart.Parameter("D1@Sketch3").SystemValue = midRoofSecondHoleDis - 2d / 1000d;
                swFeat = swComp.FeatureByName("NAMEPLATE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
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
                //if (item.ANSUL == "YES")
                //{
                //    swFeat = swComp.FeatureByName("AN1");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN2");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN3");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN4");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN5");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //    if (item.ANDropNo > 0)
                //    {
                //        swFeat = swComp.FeatureByName("AN1");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch11").SystemValue = item.ANDropDis1 / 1000d;
                //        swPart.Parameter("D3@Sketch11").SystemValue = (item.ANYDis - 360d) / 1000d;
                //    }
                //    if (item.ANDropNo > 1)
                //    {
                //        swFeat = swComp.FeatureByName("AN2");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch12").SystemValue = item.ANDropDis2 / 1000d;
                //    }
                //    if (item.ANDropNo > 2)
                //    {
                //        swFeat = swComp.FeatureByName("AN3");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch13").SystemValue = item.ANDropDis3 / 1000d;
                //    }
                //    if (item.ANDropNo > 3)
                //    {
                //        swFeat = swComp.FeatureByName("AN4");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch14").SystemValue = item.ANDropDis4 / 1000d;
                //    }
                //    if (item.ANDropNo > 4)
                //    {
                //        swFeat = swComp.FeatureByName("AN5");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch15").SystemValue = item.ANDropDis5 / 1000d;
                //    }
                //}
                //else
                //{
                //    swFeat = swComp.FeatureByName("AN1");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN2");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN3");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN4");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN5");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                //}
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
                //开方孔，UV或待MARVEL时解压
                swFeat = swComp.FeatureByName("CUT-BACK-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                swFeat = swComp.FeatureByName("CUT-BACK-RIGHT");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("CUT-FRONT-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 

                //UV灯线缆穿孔
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 

                //----------吊装槽钢----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100001-1"));
                swPart = swComp.GetModelDoc2();
                if (item.ANSUL == "YES") swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 250) / 1000d;
                else swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 100d) / 1000d;

                //----------大侧板----------
                if (item.SidePanel == "BOTH")
                {
                    //LEFT
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0007-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = item.Deepth / 1000d;
                    swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
                    swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
                    swPart.Parameter("D6@草图33").SystemValue = (item.Deepth - (sidePanelSideCjNo + 5) * 32d) / 2000d;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0008-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = (item.Deepth - 79d) / 1000d;
                    swFeat = swComp.FeatureByName("T555");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("R555");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                    //RIGHT
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0007-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0009-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = (item.Deepth - 79d) / 1000d;
                    swFeat = swComp.FeatureByName("T555");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("R555");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0010-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Deepth - 308d) / 1000d;//水洗烟罩(item.Deepth - 368) / 1000d;
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0010-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0010-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0010-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                else if (item.SidePanel == "LEFT")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0007-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0009-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0007-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = item.Deepth / 1000d;
                    swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
                    swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
                    swPart.Parameter("D6@草图33").SystemValue = (item.Deepth - (sidePanelSideCjNo + 5) * 32d) / 2000d;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0008-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = (item.Deepth - 79d) / 1000d;
                    swFeat = swComp.FeatureByName("T555");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("R555");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0010-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0010-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Deepth - 308d) / 1000d;//水洗烟罩(item.Deepth - 368) / 1000d;
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0010-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0010-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                else if (item.SidePanel == "RIGHT")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0007-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0008-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0007-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = item.Deepth / 1000d;
                    swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
                    swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
                    swPart.Parameter("D6@草图33").SystemValue = (item.Deepth - (sidePanelSideCjNo + 5) * 32d) / 2000d;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0009-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = (item.Deepth - 79d) / 1000d;
                    swFeat = swComp.FeatureByName("T555");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("R555");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0010-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0010-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Deepth - 308d) / 1000d;//水洗烟罩(item.Deepth - 368) / 1000d;
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0010-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0010-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0007-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0008-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0007-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0009-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0010-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0010-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }

                //------------M型烟罩方形新风腔主体/前面----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0012-1"));
                swEdit.FNHA0012(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, midRoofSecondHoleDis, midRoofTopHoleDis, midRoofHoleNo);

                //------------M型烟罩方形新风腔主体/背面----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0013-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.Length / 1000d;
                swPart.Parameter("D1@阵列(线性)1").SystemValue = frontPanelKaKouNo;
                swPart.Parameter("D3@阵列(线性)1").SystemValue = frontPanelKaKouDis;
                swPart.Parameter("D1@Sketch2").SystemValue = midRoofSecondHoleDis;
                swPart.Parameter("D1@Sketch8").SystemValue = 150d / 1000d - midRoofTopHoleDis;
                swFeat = swComp.FeatureByName("LPattern1");
                if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
                }
                //MARVEL
                //if (item.MARVEL == "YES")
                //{
                //    if (item.IRNo > 0)
                //    {
                //        swFeat = swComp.FeatureByName("MA1");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D3@Sketch14").SystemValue = item.IRDis1 / 1000d;
                //        swFeat = swComp.FeatureByName("MACABLE1");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch17").SystemValue = item.IRDis1 / 1000d;
                //    }
                //    if (item.IRNo > 1)
                //    {
                //        swFeat = swComp.FeatureByName("MA2");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D3@Sketch15").SystemValue = item.IRDis2 / 1000d;
                //        swFeat = swComp.FeatureByName("MACABLE2");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch18").SystemValue = item.IRDis2 / 1000d;
                //    }
                //    if (item.IRNo > 2)
                //    {
                //        swFeat = swComp.FeatureByName("MA3");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D3@Sketch16").SystemValue = item.IRDis3 / 1000d;
                //        swFeat = swComp.FeatureByName("MACABLE3");
                //        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch19").SystemValue = item.IRDis3 / 1000d;
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
                //UV HOOD
                //swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                //if (item.Bluetooth == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                //----------新风前面板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0003-2"));
                swEdit.FNHA0003(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, frontPanelHoleNo, frontPanelHoleDis);

                //----------新风底部CJ孔板/前面----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0002-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.Length / 1000d;
                swPart.Parameter("D1@CJHOLES").SystemValue = frontCjNo;
                swPart.Parameter("D10@草图8").SystemValue = frontCjFirstDis;
                swPart.Parameter("D1@LPattern1").SystemValue = frontPanelHoleNo;
                swPart.Parameter("D3@LPattern1").SystemValue = frontPanelHoleDis/1000d;
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

                //----------新风底部CJ孔板/背面----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0016-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.Length / 1000d;
                swPart.Parameter("D1@CJHOLES").SystemValue = frontCjNo;
                swPart.Parameter("D10@草图8").SystemValue = frontCjFirstDis;
                swPart.Parameter("D1@LPattern1").SystemValue = frontPanelHoleNo;
                swPart.Parameter("D3@LPattern1").SystemValue = frontPanelHoleDis / 1000d;
                swFeat = swComp.FeatureByName("BLUETOOTH");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("LOGO");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
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



                //----------LOGO----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900300005-1"));
                if (item.LEDlogo == "YES") swComp.SetSuppression2(2); //2解压缩，0压缩
                else swComp.SetSuppression2(0); //2解压缩，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201010401-1"));
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
