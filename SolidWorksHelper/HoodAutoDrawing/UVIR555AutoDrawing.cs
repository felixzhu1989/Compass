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
    public class UVIR555AutoDrawing : IAutoDrawing
    {
        readonly UVIR555Service objUVIR555Service = new UVIR555Service();
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
            UVIR555 item = (UVIR555)objUVIR555Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix;//后缀
            ModelDoc2 swModel = default(ModelDoc2);
            ModelDoc2 swPart = default(ModelDoc2);
            AssemblyDoc swAssy = default(AssemblyDoc);
            Component2 swComp;
            Feature swFeat = default(Feature);
            object configNames = null;

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
            //int frontPanelKaKouNo = (int)((item.ExBeamLength - 300m) / 450m) + 2;
            //decimal frontPanelKaKouDis = Convert.ToDecimal((item.ExBeamLength - 300m) / (frontPanelKaKouNo - 1)) / 1000m;
            //新风面板螺丝孔数量及间距
            //int frontPanelHoleNo = (int)((item.ExBeamLength - 300m) / 900m) + 2;
            //decimal frontPanelHoleDis = Convert.ToDecimal((item.ExBeamLength - 300m) / (frontPanelHoleNo - 1)) / 1000m;
            //新风CJ孔数量和新风CJ孔第一个CJ距离边缘距离
            //int frontCjNo = (int)((item.ExBeamLength - 30m) / 32m) + 1;
            //decimal frontCjFirstDis = Convert.ToDecimal((item.ExBeamLength - (frontCjNo - 1) * 32m) / 2) / 1000m;
            //Midroof灯板螺丝孔数量及第二个孔距离边缘距离,灯板顶面吊装槽钢螺丝孔位距离
            int midRoofHoleNo = (int)((item.ExBeamLength - 300m) / 400m);
            decimal midRoofSecondHoleDis = Convert.ToDecimal((item.ExBeamLength - (midRoofHoleNo - 1) * 400m) / 2) / 1000m;
            decimal midRoofTopHoleDis =
                Convert.ToDecimal(item.Deepth - 535m - 360m - 90m -
                                  (int)((item.Deepth - 535m - 360m - 90m - 100m) / 50m) * 50m) / 1000m;
            //KSA数量，KSA侧板长度(以全长计算)/M型侧板与MESH一样需要减去三角板厚度
            int ksaNo = (int)((item.ExBeamLength - 2m) / 498m);
            decimal ksaSideLength = Convert.ToDecimal((item.ExBeamLength - 3m - ksaNo * 498m) / 2) / 1000m;
            //MESH侧板长度(除去排风三角板3mm计算)
            decimal meshSideLength = Convert.ToDecimal((item.ExBeamLength - 3m - (int)((item.ExBeamLength - 2m) / 498m) * 498m) / 2) / 1000m;

            //侧板CJ孔整列到烟罩底部
            //int sidePanelDownCjNo = (int)((item.Deepth - 95m) / 32m);
            //非水洗烟罩KV/UV
            //int sidePanelSideCjNo = (int)((item.Deepth - 305m) / 32m);
            //水洗烟罩KW/UW
            //int sidePanelSideCjNo = (int)((item.Deepth - 380m) / 32m);
            //M型烟罩
            //int sidePanelSideCjNo = (int)((item.Deepth - 217m) / 32m) - 5;
            //int sidePanelDownCjNo = sidePanelSideCjNo + 6;

            //圆环新风
            decimal innerRound = 3.14m * (item.Deepth / 2m - 90m) - 4m;
            int innerCjNo = (int)((innerRound - 30m) / 32m) + 1;
            decimal innerCjFirstDis = (innerRound - (innerCjNo - 1) * 32m) / 2m;

            decimal downRound = 3.14m * (item.Deepth / 2m - 45m);
            int downCjNo = (int)((downRound - 30m) / 32m) + 1;

            try
            {
                //----------Top Level----------
                //吊装槽钢
                swModel.Parameter("D3@LocalLPattern1").SystemValue = (item.ExBeamLength - 50m) / 1000m;

                //判断KSA数量，KSA侧板长度，如果太小，则使用特殊小侧板侧边
                //swFeat = swAssy.FeatureByName("LocalLPattern1");
                //if (ksaNo == 1) swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //else
                //{
                //    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //    swModel.Parameter("D1@LocalLPattern1").SystemValue = ksaNo; //D1阵列数量,D3阵列距离
                //}
                //KSA距离左边缘
                //if (ksaSideLength < 12m / 1000m) swModel.Parameter("D1@Distance41").SystemValue = 0.5m / 1000m;
                //else swModel.Parameter("D1@Distance41").SystemValue = ksaSideLength;

                //油塞
                //if (item.Outlet == "LEFTTAP")
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100014-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100014-2"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //}
                //else if (item.Outlet == "RIGHTTAP")
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100014-1"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100014-2"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //}
                //else
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100014-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100014-2"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //}
                //排风脖颈数量和距离
                if (item.ExNo == 1)
                {
                    swModel.Parameter("D1@Distance40").SystemValue = (item.ExRightDis - item.ExLength / 2) / 1000m;
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                else if (item.ExNo == 2 && (item.MARVEL == "YES" || item.ExHeight == 100m))
                {
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                else
                {
                    swModel.Parameter("D1@Distance40").SystemValue =
                        (item.ExRightDis - item.ExLength - item.ExDis / 2) / 1000m;
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern2").SystemValue = item.ExNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D3@LocalLPattern2").SystemValue =
                        (item.ExDis + item.ExLength) / 1000m; //D1阵列数量,D3阵列距离
                }
                //灯板加强筋
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
                //----------排风腔----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0042-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExBeamLength / 1000m;
                swPart.Parameter("D1@Sketch16").SystemValue = midRoofSecondHoleDis;
                if (midRoofHoleNo == 1)
                {
                    swFeat = swComp.FeatureByName("LPattern1");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("LPattern1");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
                }
                //排风口
                if (item.ExNo == 1)
                {
                    swFeat = swComp.FeatureByName("EXCOONE");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("EXCOTWO");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch12").SystemValue = item.ExRightDis / 1000m;
                    swPart.Parameter("D1@Sketch12").SystemValue = item.ExLength / 1000m;
                    swPart.Parameter("D2@Sketch12").SystemValue = item.ExWidth / 1000m;
                }
                else
                {
                    swFeat = swComp.FeatureByName("EXCOONE");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("EXCOTWO");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch13").SystemValue = item.ExRightDis / 1000m;
                    swPart.Parameter("D2@Sketch13").SystemValue = item.ExDis / 1000m;
                    swPart.Parameter("D3@Sketch13").SystemValue = item.ExLength / 1000m;
                    swPart.Parameter("D4@Sketch13").SystemValue = item.ExWidth / 1000m;
                }


                //ANSUL
                //if (item.ANSUL == "YES")
                //{
                //    //侧喷
                //    if (item.ANSide == "LEFT")
                //    {
                //        swFeat = swComp.FeatureByName("ANSUL-LEFT");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //    }
                //    else if (item.ANSide == "RIGHT")
                //    {
                //        swFeat = swComp.FeatureByName("ANSUL-RIGHT");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //    }
                //    else
                //    {
                //        swFeat = swComp.FeatureByName("ANSUL-LEFT");
                //        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //        swFeat = swComp.FeatureByName("ANSUL-RIGHT");
                //        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    }
                //    //探测器
                //    if (item.ANDetector == "RIGHT" || item.ANDetector == "BOTH")
                //    {
                //        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //    }
                //    if (item.ANDetector == "LEFT" || item.ANDetector == "BOTH")
                //    {
                //        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //    }
                //    else
                //    {
                //        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                //        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                //        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                //        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                //        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    }
                //}
                //MARVEL
                if (item.MARVEL == "YES")
                {
                    swFeat = swComp.FeatureByName("MA-TAB");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("MA-NTC");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    if (item.ExNo == 1) swPart.Parameter("D1@Sketch23").SystemValue = (item.ExRightDis + item.ExLength / 2 + 50m) / 1000m;
                    else swPart.Parameter("D1@Sketch23").SystemValue = (item.ExRightDis + item.ExDis / 2 + item.ExLength + 50m) / 1000m;
                }
                else
                {
                    swFeat = swComp.FeatureByName("MA-TAB");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("MA-NTC");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                //UVHood,UVRack-UV灯架孔和UV cable-UV灯线缆穿孔
                swFeat = swComp.FeatureByName("UVRACK");
                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                swPart.Parameter("D7@Sketch2").SystemValue = item.ExRightDis / 1000m;
                //非UVHood
                //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                if (item.UVType == "LONG") swPart.Parameter("D3@Sketch2").SystemValue = 1680m / 1000m;
                else swPart.Parameter("D3@Sketch2").SystemValue = 975m / 1000m;
                swFeat = swComp.FeatureByName("UVCABLE");
                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                swPart.Parameter("D4@Sketch15").SystemValue = item.ExRightDis / 1000m;
                //非UVHood
                //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                if (item.UVType == "LONG") swPart.Parameter("D2@Sketch15").SystemValue = 1120m / 1000m;
                else swPart.Parameter("D2@Sketch15").SystemValue = 520m / 1000m;
                //UVHood解压特征
                swFeat = swComp.FeatureByName("UV-TAB");
                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("KSACABLE");
                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX UV");
                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX LIGHT");
                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩


                //----------排风腔前面板、后面板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0044-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = item.ExBeamLength / 1000m;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0045-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = item.ExBeamLength / 1000m;


                //----------排风腔底部----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0043-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExBeamLength / 1000m;


                //油塞
                if (item.Outlet == "LEFTTAP")
                {
                    swFeat = swComp.FeatureByName("DRAINTAP-LEFT");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINTAP-RIGHT");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                else if (item.Outlet == "RIGHTTAP")
                {
                    swFeat = swComp.FeatureByName("DRAINTAP-LEFT");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINTAP-RIGHT");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("DRAINTAP-LEFT");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINTAP-RIGHT");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }

                //----------三角板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0050-1"));
                //UV烟罩解压缩特征
                swFeat = swComp.FeatureByName("Cut-Extrude2");
                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩

                //----------KSA侧边----------
                if (ksaSideLength < 12m / 1000m && ksaSideLength > 2m / 1000m)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0003-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0004-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0005-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = ksaSideLength * 2;
                    //背面
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0027-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0026-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0028-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = ksaSideLength * 2;


                }
                else if (ksaSideLength < 25m / 1000m && ksaSideLength >= 12m / 1000m)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0003-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength * 2;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0004-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0005-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    //背面
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0027-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength * 2;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0026-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0028-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩

                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0003-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0004-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0005-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    //背面
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0027-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0026-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0028-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }

                //----------排风滑门/导轨----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0013-3"));
                if (item.ExWidth == 300m) swComp.SetSuppression2(0); //2解压缩，0压缩
                else swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0013-4"));
                if (item.ExWidth == 300m) swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@Sketch1").SystemValue = (item.ExLength / 2 + 10m) / 1000m;
                    swPart.Parameter("D2@Sketch1").SystemValue = (item.ExWidth + 20m) / 1000m;
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
                    if (item.ExNo == 1) swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 2 + 100m) / 1000m;
                    else swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 3 + item.ExDis + 100m) / 1000m;
                }

                //----------排风脖颈----------
                if (item.ANSUL != "YES" && (item.ExHeight == 100m || item.MARVEL == "YES"))
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0006-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0007-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0008-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0009-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0006-1"));
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50m) / 1000m;
                    swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000m;
                    swFeat = swComp.FeatureByName("ANSUL");
                    if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0007-1"));
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50m) / 1000m;
                    swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000m;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0008-1"));
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000m;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000m;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0009-1"));
                    swComp.SetSuppression2(2);//2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000m;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000m;
                }


                //----------UV灯----------
                //if (item.UVType == "LONG")
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201060409-1"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201060410-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //}
                //else
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201060409-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201060410-1"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //}

                //----------MESH油网侧板----------
                if (item.ANSUL == "YES")
                {
                    if (meshSideLength * 2 < 57m / 1000m) meshSideLength = meshSideLength + 249m / 1000m;
                    if ((meshSideLength - 20m / 1000m) > 57m / 1000m)
                    {
                        if (item.ANSide == "LEFT")
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0012-2"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20m / 1000m;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0013-2"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20m / 1000m;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            //背面
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0030-3"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20m / 1000m;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0029-2"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20m / 1000m;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩

                        }
                        else if (item.ANSide == "RIGHT")
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0012-2"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20m / 1000m;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0013-2"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20m / 1000m;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            //背面
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0030-3"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20m / 1000m;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0029-2"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20m / 1000m;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0012-2"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20m / 1000m;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0013-2"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20m / 1000m;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            //背面
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0030-3"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20m / 1000m;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0029-2"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20m / 1000m;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                    }
                    else
                    {
                        if (item.ANSide == "LEFT")
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0012-2"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0013-2"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                            //背面
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0030-3"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0029-2"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                        }
                        else if (item.ANSide == "RIGHT")
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0012-2"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0013-2"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            //背面
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0030-3"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0029-2"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0012-2"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0013-2"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            //背面
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0030-3"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0029-2"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                    }
                }
                else
                {
                    if (2 * meshSideLength < 15m / 1000m && meshSideLength > 1.5m / 1000m)
                        meshSideLength = meshSideLength + 249m / 1000m;
                    if (meshSideLength > 40m / 1000m)
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0012-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20m / 1000m;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0013-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20m / 1000m;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        //背面
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0030-3"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20m / 1000m;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0029-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20m / 1000m;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    else if (meshSideLength <= 40m / 1000m && meshSideLength > 1.5m / 1000m)
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0012-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0013-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        //背面
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0030-3"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0029-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0012-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0013-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        //背面
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0030-3"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0029-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                //----------排风腔内部零件----------
                //MESH油网下导轨
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0047-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.ExBeamLength - 8m) / 1000m;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0046-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.ExBeamLength - 5m) / 1000m;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0048-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.ExBeamLength - 3.5m) / 1000m;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0049-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.ExBeamLength - 9m) / 1000m;

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

                //内半径（Deepth/2m）=外半径（Deepth/2m）-90m
                //----------MiddleRoof灯板/前----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0018-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D5@Sketch2").SystemValue = (item.Deepth / 2m - 90m + 1m) / 1000m;
                swPart.Parameter("D4@Sketch18").SystemValue = midRoofSecondHoleDis - 52.5m / 1000m;
                //swFeat = swComp.FeatureByName("NAMEPLATE");
                //swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("LPattern1");
                if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                else
                {
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                    swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
                }
                //灯具
                if (item.LightType == "LED60")
                {
                    //swFeat = swComp.FeatureByName("LED140");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    //swFeat = swComp.FeatureByName("LPattern3");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    //swFeat = swComp.FeatureByName("FSLONG");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    //swFeat = swComp.FeatureByName("FSSHORT");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("LED60");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                    if (item.LEDSpotNo == 1) swPart.Parameter("D4@Sketch10").SystemValue = 0;
                    else
                    {
                        swPart.Parameter("D4@Sketch10").SystemValue = (item.LEDSpotDis * (item.LEDSpotNo / 2m - 1) + item.LEDSpotDis / 2m) / 1000m;
                        swFeat = swComp.FeatureByName("LPattern2");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                        swPart.Parameter("D1@LPattern2").SystemValue = item.LEDSpotNo;
                        swPart.Parameter("D3@LPattern2").SystemValue = item.LEDSpotDis / 1000m;
                    }
                }
                //else if (item.LightType == "LED140")
                //{
                //    swFeat = swComp.FeatureByName("LED60");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("LPattern2");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("FSLONG");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("FSSHORT");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("LED140");
                //    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                //    if (item.LEDSpotNo == 1) swPart.Parameter("D5@Sketch7").SystemValue = 0;
                //    else
                //    {
                //        swPart.Parameter("D5@Sketch7").SystemValue = (item.LEDSpotDis * (item.LEDSpotNo / 2m - 1) + item.LEDSpotDis / 2m) / 1000m;
                //        swFeat = swComp.FeatureByName("LPattern3");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                //        swPart.Parameter("D1@LPattern3").SystemValue = item.LEDSpotNo;
                //        swPart.Parameter("D3@LPattern3").SystemValue = item.LEDSpotDis / 1000m;
                //    }
                //}
                //else if (item.LightType == "FSLONG")
                //{
                //    swFeat = swComp.FeatureByName("LED60");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("LPattern2");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("LED140");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("LPattern3");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("FSLONG");
                //    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("FSSHORT");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //}
                //else if (item.LightType == "FSSHORT")
                //{
                //    swFeat = swComp.FeatureByName("LED60");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("LPattern2");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("LED140");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("LPattern3");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("FSLONG");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("FSSHORT");
                //    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //}
                else
                {
                    swFeat = swComp.FeatureByName("LED60");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("LPattern2");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    //swFeat = swComp.FeatureByName("LED140");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    //swFeat = swComp.FeatureByName("LPattern3");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    //swFeat = swComp.FeatureByName("FSLONG");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    //swFeat = swComp.FeatureByName("FSSHORT");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                //ANSUL
                //if (item.ANSUL == "YES")
                //{
                //    swFeat = swComp.FeatureByName("AN1");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN2");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN3");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN4");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN5");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    if (item.ANDropNo > 0)
                //    {
                //        swFeat = swComp.FeatureByName("AN1");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch11").SystemValue = item.ANDropDis1 / 1000m;
                //        swPart.Parameter("D3@Sketch11").SystemValue = (item.ANYDis - 360m) / 1000m;
                //    }
                //    if (item.ANDropNo > 1)
                //    {
                //        swFeat = swComp.FeatureByName("AN2");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch12").SystemValue = item.ANDropDis2 / 1000m;
                //    }
                //    if (item.ANDropNo > 2)
                //    {
                //        swFeat = swComp.FeatureByName("AN3");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch13").SystemValue = item.ANDropDis3 / 1000m;
                //    }
                //    if (item.ANDropNo > 3)
                //    {
                //        swFeat = swComp.FeatureByName("AN4");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch14").SystemValue = item.ANDropDis4 / 1000m;
                //    }
                //    if (item.ANDropNo > 4)
                //    {
                //        swFeat = swComp.FeatureByName("AN5");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch15").SystemValue = item.ANDropDis5 / 1000m;
                //    }
                //}
                //else
                //{
                //    swFeat = swComp.FeatureByName("AN1");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN2");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN3");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN4");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN5");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //}
                //UW/KW HOOD 水洗烟罩探测器在midRoof上，非水洗烟罩压缩
                //swFeat = swComp.FeatureByName("ANDTEC1");
                //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //swFeat = swComp.FeatureByName("ANDTEC2");
                //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //swFeat = swComp.FeatureByName("ANDTEC3");
                //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //swFeat = swComp.FeatureByName("ANDTEC4");
                //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //swFeat = swComp.FeatureByName("ANDTEC5");
                //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 

                //----------MiddleRoof灯板/后----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0019-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D5@Sketch2").SystemValue = (item.Deepth / 2m - 90m + 1m) / 1000m;
                swPart.Parameter("D4@Sketch18").SystemValue = midRoofSecondHoleDis - 52.5m / 1000m;
                //swFeat = swComp.FeatureByName("NAMEPLATE");
                //swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("LPattern1");
                if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                else
                {
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                    swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
                }
                //灯具
                if (item.LightType == "LED60")
                {
                    //swFeat = swComp.FeatureByName("LED140");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    //swFeat = swComp.FeatureByName("LPattern3");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    //swFeat = swComp.FeatureByName("FSLONG");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    //swFeat = swComp.FeatureByName("FSSHORT");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("LED60");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                    if (item.LEDSpotNo == 1) swPart.Parameter("D4@Sketch10").SystemValue = 0;
                    else
                    {
                        swPart.Parameter("D4@Sketch10").SystemValue = (item.LEDSpotDis * (item.LEDSpotNo / 2m - 1) + item.LEDSpotDis / 2m) / 1000m;
                        swFeat = swComp.FeatureByName("LPattern2");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                        swPart.Parameter("D1@LPattern2").SystemValue = item.LEDSpotNo;
                        swPart.Parameter("D3@LPattern2").SystemValue = item.LEDSpotDis / 1000m;
                    }
                }
                //else if (item.LightType == "LED140")
                //{
                //    swFeat = swComp.FeatureByName("LED60");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("LPattern2");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("FSLONG");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("FSSHORT");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("LED140");
                //    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                //    if (item.LEDSpotNo == 1) swPart.Parameter("D5@Sketch7").SystemValue = 0;
                //    else
                //    {
                //        swPart.Parameter("D5@Sketch7").SystemValue = (item.LEDSpotDis * (item.LEDSpotNo / 2m - 1) + item.LEDSpotDis / 2m) / 1000m;
                //        swFeat = swComp.FeatureByName("LPattern3");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                //        swPart.Parameter("D1@LPattern3").SystemValue = item.LEDSpotNo;
                //        swPart.Parameter("D3@LPattern3").SystemValue = item.LEDSpotDis / 1000m;
                //    }
                //}
                //else if (item.LightType == "FSLONG")
                //{
                //    swFeat = swComp.FeatureByName("LED60");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("LPattern2");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("LED140");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("LPattern3");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("FSLONG");
                //    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("FSSHORT");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //}
                //else if (item.LightType == "FSSHORT")
                //{
                //    swFeat = swComp.FeatureByName("LED60");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("LPattern2");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("LED140");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("LPattern3");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("FSLONG");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("FSSHORT");
                //    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //}
                else
                {
                    swFeat = swComp.FeatureByName("LED60");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("LPattern2");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    //swFeat = swComp.FeatureByName("LED140");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    //swFeat = swComp.FeatureByName("LPattern3");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    //swFeat = swComp.FeatureByName("FSLONG");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                    //swFeat = swComp.FeatureByName("FSSHORT");
                    //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                //ANSUL
                //if (item.ANSUL == "YES")
                //{
                //    swFeat = swComp.FeatureByName("AN1");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN2");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN3");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN4");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("AN5");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //    if (item.ANDropNo > 0)
                //    {
                //        swFeat = swComp.FeatureByName("AN1");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch11").SystemValue = item.ANDropDis1 / 1000m;
                //        swPart.Parameter("D3@Sketch11").SystemValue = (item.ANYDis - 360m) / 1000m;
                //    }
                //    if (item.ANDropNo > 1)
                //    {
                //        swFeat = swComp.FeatureByName("AN2");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch12").SystemValue = item.ANDropDis2 / 1000m;
                //    }
                //    if (item.ANDropNo > 2)
                //    {
                //        swFeat = swComp.FeatureByName("AN3");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch13").SystemValue = item.ANDropDis3 / 1000m;
                //    }
                //    if (item.ANDropNo > 3)
                //    {
                //        swFeat = swComp.FeatureByName("AN4");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch14").SystemValue = item.ANDropDis4 / 1000m;
                //    }
                //    if (item.ANDropNo > 4)
                //    {
                //        swFeat = swComp.FeatureByName("AN5");
                //        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                //        swPart.Parameter("D1@Sketch15").SystemValue = item.ANDropDis5 / 1000m;
                //    }
                //}
                //else
                //{
                //    swFeat = swComp.FeatureByName("AN1");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN2");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN3");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN4");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //    swFeat = swComp.FeatureByName("AN5");
                //    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //}
                //UW/KW HOOD 水洗烟罩探测器在midRoof上，非水洗烟罩压缩
                //swFeat = swComp.FeatureByName("ANDTEC1");
                //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //swFeat = swComp.FeatureByName("ANDTEC2");
                //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //swFeat = swComp.FeatureByName("ANDTEC3");
                //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //swFeat = swComp.FeatureByName("ANDTEC4");
                //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                //swFeat = swComp.FeatureByName("ANDTEC5");
                //swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 

                //----------MiddleRoof灯板/左右----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0020-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Deepth / 2m - 90m + 1m) / 1000m;
                swPart.Parameter("D2@Sketch1").SystemValue = (item.ExBeamLength / 2m + 1.5m) / 1000m;
                //----------MiddleRoof灯板/弯条----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0021-2"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Deepth / 2m - 90m) / 1000m;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0022-2"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Deepth / 2m - 90m) / 1000m;

                //----------吊装槽钢----------
                //利用三角关系半径平方-长度一半的平方再开根号乘以2
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100001-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = 2d * Math.Sqrt(Math.Pow((double)item.Deepth / 2d, 2) - Math.Pow((double)item.ExBeamLength / 2d, 2))/1000d;

                //----------圆环新风----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0048-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Deepth / 2m - 1m) / 1000m;
                swPart.Parameter("D3@Sketch1").SystemValue = (item.Deepth / 2m - 3m) / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0049-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Deepth / 2m - 90m + 1m) / 1000m;
                swPart.Parameter("D4@Sketch1").SystemValue = (item.Deepth / 2m - 90m + 1m) / 1000m;
                swPart.Parameter("D3@Sketch6").SystemValue = innerCjFirstDis / 1000m;
                swPart.Parameter("D1@LPattern1").SystemValue = innerCjNo;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0050-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Deepth / 2m - 1m) / 1000m;
                swPart.Parameter("D3@Sketch2").SystemValue = (item.Deepth - 90m) / 1000m;
                swPart.Parameter("D1@CirPattern1").SystemValue = downCjNo;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0051-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D6@Sketch1").SystemValue = (item.Deepth / 2m - 1m) / 1000m;
                swPart.Parameter("D2@Sketch3").SystemValue = (item.Deepth / 2m - 45m) / 1000m;
                swPart.Parameter("D1@Sketch3").SystemValue = (item.ExBeamLength - 50m) / 1000m;
                
                //----------蓝牙盒子----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHO0003-1"));
                if (item.Bluetooth == "YES") swComp.SetSuppression2(2); //2解压缩，0压缩
                else swComp.SetSuppression2(0); //2解压缩，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHO0004-1"));
                if (item.Bluetooth == "YES") swComp.SetSuppression2(2); //2解压缩，0压缩
                else swComp.SetSuppression2(0); //2解压缩，0压缩

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
