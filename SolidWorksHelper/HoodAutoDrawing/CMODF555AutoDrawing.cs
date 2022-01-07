﻿using System;
using System.IO;
using System.Windows.Forms;
using Common;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class CMODF555AutoDrawing : IAutoDrawing
    {
        readonly CMODF555Service objCMODF555Service = new CMODF555Service();
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
            CMODF555 item = (CMODF555)objCMODF555Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix;//后缀
            ModelDoc2 swModel;
            ModelDoc2 swPart;
            AssemblyDoc swAssy;
            Component2 swComp;
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
            double frontPanelKaKouDis = Convert.ToDouble((item.Length - 300d) / (frontPanelKaKouNo - 1)) / 1000d;
            //新风面板螺丝孔数量及间距
            int frontPanelHoleNo = (int)((item.Length - 300d) / 900d) + 2;
            double frontPanelHoleDis = Convert.ToDouble((item.Length - 300) / (frontPanelHoleNo - 1)) / 1000d;
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
                swModel.Parameter("D1@Distance79").SystemValue = item.Deepth / 1000d;

                //灯板加强筋
                if (item.Deepth > 1649d && ((item.LightType == "FSLONG" && item.Length > 1900d) ||
                                           (item.LightType == "FSSHORT" && item.Length > 1500d) || (item.Length > 2000d)))
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0006-3"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0006-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@Base-Flange1").SystemValue = item.Deepth / 1000d - 898d / 1000d;
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0006-3"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0006-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                //----------新风脖颈----------
                swFeat = swAssy.FeatureByName("LocalLPattern2");
                if (item.SuNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern2").SystemValue = item.SuNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D3@LocalLPattern2").SystemValue = item.SuDis / 1000d; //D1阵列数量,D3阵列距离
                }
                //----------新风前面板中间加强筋----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0011-1"));
                if (item.Length > 1599d) swComp.SetSuppression2(2); //2解压缩，0压缩
                else swComp.SetSuppression2(0); //2解压缩，0压缩


                //----------排风腔----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0070-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Base-Flange1").SystemValue = item.Length / 1000d;
                swPart.Parameter("D2@Sketch9").SystemValue = midRoofSecondHoleDis;
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
                //if (item.ExNo == 1)
                //{
                swFeat = swComp.FeatureByName("EXCOONE");
                swFeat.SetSuppression2(1, 2, null);
                //swFeat = swComp.FeatureByName("EXCOTWO");
                //swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //swPart.Parameter("D4@Sketch11").SystemValue = item.ExRightDis / 1000d;
                swPart.Parameter("D2@Sketch4").SystemValue = item.ExLength / 1000d;
                swPart.Parameter("D3@Sketch4").SystemValue = item.ExWidth / 1000d;
                //}
                //else
                //{
                //    swFeat = swComp.FeatureByName("EXCOONE");
                //    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //    swFeat = swComp.FeatureByName("EXCOTWO");
                //    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //    swPart.Parameter("D5@Sketch12").SystemValue = item.ExRightDis / 1000d;
                //    swPart.Parameter("D4@Sketch12").SystemValue = item.ExDis / 1000d;
                //    swPart.Parameter("D1@Sketch12").SystemValue = item.ExLength / 1000d;
                //    swPart.Parameter("D2@Sketch12").SystemValue = item.ExWidth / 1000d;
                //}
                //下水口/上排水
                if (item.Outlet == "LEFT")
                {
                    swFeat = swComp.FeatureByName("DRAINPIPE-LEFT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINPIPE-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("AUTODRAIN LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("AUTODRAIN RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.Outlet == "RIGHT")
                {
                    swFeat = swComp.FeatureByName("DRAINPIPE-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINPIPE-RIGHT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("AUTODRAIN LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("AUTODRAIN RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.Outlet == "UPLEFT")
                {
                    swFeat = swComp.FeatureByName("DRAINPIPE-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINPIPE-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("AUTODRAIN LEFT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("AUTODRAIN RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.Outlet == "UPRIGHT")
                {
                    swFeat = swComp.FeatureByName("DRAINPIPE-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINPIPE-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("AUTODRAIN LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("AUTODRAIN RIGHT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("DRAINPIPE-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRAINPIPE-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("AUTODRAIN LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("AUTODRAIN RIGHT");
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

                //进水口
                if (item.Inlet == "LEFT")
                {
                    swFeat = swComp.FeatureByName("DRWATER INLET-L");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRWATER INLET-R");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.Inlet == "RIGHT")
                {
                    swFeat = swComp.FeatureByName("DRWATER INLET-L");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRWATER INLET-R");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("DRWATER INLET-L");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("DRWATER INLET-R");
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
                }

                //----------排风腔前面板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0071-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = item.Length / 1000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0074-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 7.5d) / 1000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0088-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 7.5d) / 1000d;

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
                swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0018-1"));
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                if (item.ExNo == 1) swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 2 + 100d) / 1000d;
                else swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 3 + item.ExDis + 100d) / 1000d;

                //----------排风脖颈----------

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0089-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = item.ExLength / 1000d;
                swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000d;
                //swFeat = swComp.FeatureByName("ANSUL");
                //if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0090-1"));
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.ExWidth - 2.5d) / 1000d;
                swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000d;

                //----------排风腔内部零件----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0075-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 113d) / 2000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0078-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 170d) / 2000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0079-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 200d) / 2000d;

                //----------灯具----------
                //日光灯
                if (item.LightType == "FSLONG")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020410-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020409-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else if (item.LightType == "FSSHORT")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020410-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020409-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020410-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020409-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                //----------MiddleRoof灯板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0001-1"));
                swEdit.FNHM0001(swComp, "CMOD", item.Length, item.Deepth, 555d, 555d, item.ExRightDis, midRoofTopHoleDis, midRoofSecondHoleDis, midRoofHoleNo, item.LightType, 0, item.LEDSpotNo, item.LEDSpotDis, item.ANSUL, item.ANDropNo, item.ANYDis, item.ANDropDis1, item.ANDropDis2, item.ANDropDis3, item.ANDropDis4, item.ANDropDis5, item.ANDetectorEnd, item.ANDetectorNo, item.ANDetectorDis1, item.ANDetectorDis2, item.ANDetectorDis3, item.ANDetectorDis4, item.ANDetectorDis5, "NO", "NO", item.MARVEL, item.IRNo, item.IRDis1, item.IRDis2, item.IRDis3);


                //----------吊装槽钢----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100001-1"));
                swPart = swComp.GetModelDoc2();
                if (item.ANSUL == "YES") swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 250) / 1000d;
                else swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 100d) / 1000d;
                //----------大侧板----------
                if (item.SidePanel == "BOTH")
                {
                    //LEFT
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0001-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = item.Deepth / 1000d;
                    swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
                    swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0002-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = (item.Deepth - 79d) / 1000d;
                    //RIGHT
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0003-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = item.Deepth / 1000d;
                    swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
                    swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0004-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = (item.Deepth - 79d) / 1000d;
                }
                else if (item.SidePanel == "LEFT")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0003-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0004-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0001-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = item.Deepth / 1000d;
                    swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
                    swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0002-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = (item.Deepth - 79d) / 1000d;

                }
                else if (item.SidePanel == "RIGHT")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0001-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0002-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0003-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = item.Deepth / 1000d;
                    swPart.Parameter("D1@阵列(线性)1").SystemValue = sidePanelSideCjNo;
                    swPart.Parameter("D1@阵列(线性)2").SystemValue = sidePanelDownCjNo;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0004-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = (item.Deepth - 79d) / 1000d;

                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0001-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0002-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0003-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0004-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0005-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0006-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }

                //------------F型新风腔主体----------

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0004-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.Length / 1000d;
                swPart.Parameter("D1@阵列(线性)1").SystemValue = frontPanelKaKouNo;
                swPart.Parameter("D3@阵列(线性)1").SystemValue = frontPanelKaKouDis;
                swPart.Parameter("D3@Sketch3").SystemValue = midRoofSecondHoleDis;
                swPart.Parameter("D4@草图7").SystemValue = 200d / 1000d - midRoofTopHoleDis;
                swFeat = swComp.FeatureByName("LPattern1");
                if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
                }
                //新风脖颈
                swPart.Parameter("D3@Sketch6").SystemValue = (item.SuDis * (item.SuNo / 2 - 1) + item.SuDis / 2d) / 1000d;
                swFeat = swComp.FeatureByName("LPattern2");
                if (item.SuNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern2").SystemValue = item.SuNo;
                    swPart.Parameter("D3@LPattern2").SystemValue = item.SuDis / 1000d;
                }
                //MARVEL
                if (item.MARVEL == "YES")
                {
                    if (item.IRNo > 0)
                    {
                        swFeat = swComp.FeatureByName("MA1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch16").SystemValue = item.IRDis1 / 1000d;
                        swFeat = swComp.FeatureByName("MACABLE1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        if (item.SuNo == 1) swPart.Parameter("D1@Sketch19").SystemValue = (item.Length + 400) / 2000d;
                        else swPart.Parameter("D1@Sketch19").SystemValue = item.Length / 2000d;
                    }
                    if (item.IRNo > 1)
                    {
                        swFeat = swComp.FeatureByName("MA2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch17").SystemValue = item.IRDis2 / 1000d;
                        swFeat = swComp.FeatureByName("MACABLE2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch19").SystemValue = (item.Length / 2 - 25d) / 1000d;
                        swPart.Parameter("D1@Sketch20").SystemValue = 50d / 1000d;
                    }
                    if (item.IRNo > 2)
                    {
                        swFeat = swComp.FeatureByName("MA3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch18").SystemValue = item.IRDis3 / 1000d;
                        swFeat = swComp.FeatureByName("MACABLE3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch19").SystemValue = 220d / 1000d;
                        swPart.Parameter("D1@Sketch20").SystemValue = (item.Length - 440d) / 2000d;
                        swPart.Parameter("D1@Sketch21").SystemValue = (item.Length - 440d) / 2000d;
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
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (item.MARVEL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩


                //----------新风前面板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0007-1"));
                swEdit.FNHA0007(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, frontPanelHoleNo, frontPanelHoleDis);

                //----------蜂窝板压条----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0008-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 6d) / 1000d;
                swPart.Parameter("D1@LPattern1").SystemValue = frontPanelHoleNo;
                swPart.Parameter("D3@LPattern1").SystemValue = frontPanelHoleDis;

                //----------镀锌隔板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0006-1"));
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
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0010-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@草图1").SystemValue = (item.Length - 200d) / 1000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0010-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 200d) / 1000d;
                //----------新风底部CJ孔板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0005-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.Length / 1000d;
                swPart.Parameter("D1@CJHOLES").SystemValue = frontCjNo;
                swPart.Parameter("D10@草图8").SystemValue = frontCjFirstDis;
                swPart.Parameter("D1@LPattern1").SystemValue = frontPanelHoleNo;
                swPart.Parameter("D3@LPattern1").SystemValue = frontPanelHoleDis;
                swFeat = swComp.FeatureByName("BLUETOOTH");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("LOGO");
                if (item.LEDlogo == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //集水翻边
                swFeat = swComp.FeatureByName("DRAINCHANNEL-RIGHT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("DRAINCHANNEL-LEFT");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                //----------LOGO----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900300005-2"));
                if (item.LEDlogo == "YES") swComp.SetSuppression2(2); //2解压缩，0压缩
                else swComp.SetSuppression2(0); //2解压缩，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201010401-2"));
                if (item.LEDlogo == "YES") swComp.SetSuppression2(2); //2解压缩，0压缩
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
