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
    public class HWUVI650AutoDrawing : IAutoDrawing
    {
        readonly HWUVI650Service objHWUVI650Service = new HWUVI650Service();

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
            HWUVI650 item = (HWUVI650)objHWUVI650Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix; //后缀
            ModelDoc2 swModel ;
            ModelDoc2 swPart ;
            AssemblyDoc swAssy;
            Component2 swComp;
            Feature swFeat;
            HuaWeiHoodPart swEdit = new HuaWeiHoodPart();

            //打开Pack后的模型
            swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
            swAssy = swModel as AssemblyDoc; //装配体
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true); //TopOnly参数设置成true，只重建顶层，不重建零件内部
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
            //KSA数量，KSA侧板长度(以全长计算)
            int ksaNo = (int)((item.Length + 1) / 498d);
            double ksaSideLength = Convert.ToDouble((item.Length - ksaNo * 498d) / 2) / 1000d;
            //MESH侧板长度(除去排风三角板3dm计算),华为烟罩再减去2
            double meshSideLength =
                Convert.ToDouble((item.Length - 3d - (int)((item.Length - 2d) / 497d) * 497d) / 2d - 2d) / 1000d;
            //侧板CJ孔整列到烟罩底部
            int sidePanelDownCjNo = (int)((item.Deepth - 95d) / 32d);
            //非水洗烟罩KV/UV
            int sidePanelSideCjNo = (int)((item.Deepth - 305d) / 32d);
            //水洗烟罩KW/UW
            //int sidePanelSideCjNo = (int)((item.Deepth - 380) / 32);


            try
            {
                //----------Top Level----------

                //烟罩深度
                swModel.Parameter("D1@Distance87").SystemValue = item.Deepth / 1000d;
                //判断KSA数量，KSA侧板长度，如果太小，则使用特殊小侧板侧边
                swFeat = swAssy.FeatureByName("LocalLPattern1");
                if (ksaNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern1").SystemValue = ksaNo; //D1阵列数量,D3阵列距离
                }
                //KSA距离左边缘
                if (ksaSideLength < 12d / 1000d) swModel.Parameter("D1@Distance86").SystemValue = 0.5d / 1000d;
                else swModel.Parameter("D1@Distance86").SystemValue = ksaSideLength;

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
                    swModel.Parameter("D1@Distance95").SystemValue = (item.ExRightDis - item.ExLength / 2) / 1000d;
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swModel.Parameter("D1@Distance95").SystemValue =
                        (item.ExRightDis - item.ExLength - item.ExDis / 2) / 1000d;
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern2").SystemValue = item.ExNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D3@LocalLPattern2").SystemValue =
                        (item.ExDis + item.ExLength) / 1000d; //D1阵列数量,D3阵列距离
                }

                //UV灯支架，两个
                swFeat = swAssy.FeatureByName("LocalLPattern5");
                if (item.UVType == "DOUBLE") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

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
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0186-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 6.4d) / 1000d;
                swPart.Parameter("D2@Sketch3").SystemValue = midRoofSecondHoleDis - 3.2d / 1000d;
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
                    swPart.Parameter("D4@Sketch9").SystemValue = (item.ExRightDis - 3.2d) / 1000d;
                    swPart.Parameter("D2@Sketch9").SystemValue = item.ExLength / 1000d;
                    swPart.Parameter("D3@Sketch9").SystemValue = item.ExWidth / 1000d;
                }
                else
                {
                    swFeat = swComp.FeatureByName("EXCOONE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("EXCOTWO");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D5@Sketch10").SystemValue = (item.ExRightDis - 3.2d) / 1000d;
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
                        swFeat = swComp.FeatureByName("CHANNEL-LEFT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    }
                    else if (item.ANSide == "RIGHT")
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
                    //探测器
                    if (item.ANDetector == "RIGHT" || item.ANDetector == "BOTH")
                    {
                        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    }
                    if (item.ANDetector == "LEFT" || item.ANDetector == "BOTH")
                    {
                        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                }
                //MARVEL
                //swFeat = swComp.FeatureByName("MA-NTC");
                //if (item.MARVEL == "YES")
                //{
                //    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //    if (item.ExNo == 1) swPart.Parameter("D1@Sketch21").SystemValue = (item.ExRightDis + item.ExLength / 2 + 50d) / 1000d;
                //    else swPart.Parameter("D1@Sketch21").SystemValue = (item.ExRightDis + item.ExDis / 2 + item.ExLength + 50d) / 1000d;
                //}
                //else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩


                //UVHood,UVRack-UV灯架孔和UV cable-UV灯线缆穿孔

                if (item.UVType == "DOUBLE")
                {
                    swFeat = swComp.FeatureByName("UVDOUBLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D7@Sketch17").SystemValue = (item.ExRightDis - 3.2d) / 1000d;
                    swFeat = swComp.FeatureByName("UVRACK");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.UVType == "LONG")
                {
                    swFeat = swComp.FeatureByName("UVRACK");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D6@Sketch12").SystemValue = (item.ExRightDis - 3.2d) / 1000d;
                    swPart.Parameter("D5@Sketch12").SystemValue = 1622d / 1000d;
                    swFeat = swComp.FeatureByName("UVDOUBLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.UVType == "SHORT")
                {
                    //SHORT
                    swFeat = swComp.FeatureByName("UVRACK");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D6@Sketch12").SystemValue = (item.ExRightDis - 3.2d) / 1000d;
                    swPart.Parameter("D5@Sketch12").SystemValue = 912d / 1000d;
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

                //----------排风腔前面板----------//对应FNHE0149
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0187-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 6.4d) / 1000d;
                //UV HoodParent,过滤器感应出线孔，UV门，UV cable-UV灯线缆穿孔避让缺口
                //UV灯门
                if (item.UVType == "DOUBLE")
                {
                    swFeat = swComp.FeatureByName("UVDOOR-DOUBLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch37").SystemValue = item.ExRightDis / 1000d;

                    swFeat = swComp.FeatureByName("UVCABLE-DOUBLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch41").SystemValue = item.ExRightDis / 1000d;

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
                    swPart.Parameter("D1@Sketch40").SystemValue = item.ExRightDis / 1000d;

                    swFeat = swComp.FeatureByName("UVCABLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch42").SystemValue = item.ExRightDis / 1000d;
                    swPart.Parameter("D2@Sketch42").SystemValue = 1500d / 1000d;

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
                    swPart.Parameter("D1@Sketch39").SystemValue = item.ExRightDis / 1000d;

                    swFeat = swComp.FeatureByName("UVCABLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch42").SystemValue = item.ExRightDis / 1000d;
                    swPart.Parameter("D2@Sketch42").SystemValue = 790d / 1000d;

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

                //----------KSA侧边----------
                if (ksaSideLength <= 3.3d / 1000d)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0160-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0161-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0170-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else if (ksaSideLength < 12d / 1000d && ksaSideLength > 3.3d / 1000d)//华为板材厚，改成3.3无法折弯
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
                else if (ksaSideLength < 25d / 1000d && ksaSideLength >= 12d / 1000d)
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
                swPart.Parameter("D1@Sketch1").SystemValue = (item.ExLength / 2 + 10d) / 1000d;
                swPart.Parameter("D2@Sketch1").SystemValue = (item.ExWidth + 20d) / 1000d;


                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0165-1"));
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                if (item.ExNo == 1) swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 2 + 100d) / 1000d;
                else swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 2 + 20d) / 1000d;

                //----------排风脖颈----------

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0166-1"));
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50d) / 1000d;
                swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000d;
                swFeat = swComp.FeatureByName("ANSUL");
                if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0167-1"));
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50d) / 1000d;
                swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0168-6"));
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                //swFeat = swComp.FeatureByName("ANDTEC");
                //if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0169-1"));
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                //swFeat = swComp.FeatureByName("ANDTEC");
                //if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                //----------排风三角板----------
                //if (item.ANSUL == "YES" && item.SidePanel == "MIDDLE")
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0185-1"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0185-2"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swFeat = swComp.FeatureByName("ANDTEC");
                //    swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                //}
                //else if (item.ANSUL == "YES" && item.SidePanel == "LEFT")
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201030401-4"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201030401-5"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swFeat = swComp.FeatureByName("ANDTEC");
                //    swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                //}
                //else if (item.ANSUL == "YES" && item.SidePanel == "RIGHT")
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201030401-5"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201030401-4"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swFeat = swComp.FeatureByName("ANDTEC");
                //    swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                //}
                //else
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201030401-4"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201030401-5"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //}



                //----------UV灯，UV灯门，双门----------
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
                }

                ////----------UV灯，UV灯门，标准----------
                //if (item.UVType == "LONG")
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201050414-1"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201060409-1"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201050413-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201060410-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //}
                //else
                //{
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201050414-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201060409-1"));
                //    swComp.SetSuppression2(0); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201050413-1"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201060410-1"));
                //    swComp.SetSuppression2(2); //2解压缩，0压缩
                //}
                //----------MESH油网侧板----------
                if (item.ANSUL == "YES")
                {
                    if (meshSideLength * 2 < 57d / 1000d) meshSideLength += 249d / 1000d;
                    if ((meshSideLength - 20d / 1000d) > 57d / 1000d)
                    {
                        if (item.ANSide == "LEFT")
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0162-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0163-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        else if (item.ANSide == "RIGHT")
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0162-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0163-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0162-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0163-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                    }
                    else
                    {
                        if (item.ANSide == "LEFT")
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0162-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0163-1"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                        }
                        else if (item.ANSide == "RIGHT")
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0162-1"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0013-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0162-1"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0163-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩
                            swPart = swComp.GetModelDoc2();
                            swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                            swFeat = swComp.FeatureByName("ANSUL");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("KW");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                    }
                }
                else
                {
                    if (2 * meshSideLength < 15d / 1000d && meshSideLength > 1.5d / 1000d)
                        meshSideLength += 249d / 1000d;
                    if (meshSideLength > 40d / 1000d)
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0162-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength - 20d / 1000d;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("KW");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0163-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = meshSideLength + 20d / 1000d;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("KW");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                    else if (meshSideLength <= 40d / 1000d && meshSideLength > 1.5d / 1000d)
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0162-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0163-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Sketch1").SystemValue = 2 * meshSideLength;
                        swFeat = swComp.FeatureByName("ANSUL");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("KW");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0162-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0163-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }

                //----------排风腔内部零件----------
                //MESH油网下导轨
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0188-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Length - 12d) / 1000d;
                if (item.ANSUL == "YES")
                {
                    if (item.ANDetector == "BOTH")
                    {
                        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                        swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                        swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                    }
                    else if (item.ANDetector == "LEFT")
                    {
                        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                        swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                        swFeat.SetSuppression2(0, 2, null);//参数1：1解压，0压缩
                    }
                    else if (item.ANDetector == "RIGHT")
                    {
                        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                        swFeat.SetSuppression2(0, 2, null);//参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                        swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                        swFeat.SetSuppression2(0, 2, null);//参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                        swFeat.SetSuppression2(0, 2, null);//参数1：1解压，0压缩
                    }
                }
                else
                {
                    swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                    swFeat.SetSuppression2(0, 2, null);//参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                    swFeat.SetSuppression2(0, 2, null);//参数1：1解压，0压缩
                }
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0189-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 7d) / 1000d;

                //MESH油网轨道支架
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0190-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 7d) / 1000d;
                //UV灯门铰链孔
                if (item.UVType == "DOUBLE")
                {
                    swFeat = swComp.FeatureByName("UVDOOR-DOUBLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch8").SystemValue = (item.ExRightDis - 3.5d) / 1000d;
                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.UVType == "LONG")
                {
                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch9").SystemValue = (item.ExRightDis - 2.5d) / 1000d;
                    swFeat = swComp.FeatureByName("UVDOOR-DOUBLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.UVType == "SHORT")
                {
                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch10").SystemValue = (item.ExRightDis - 2.5d) / 1000d;
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
                swEdit.FNHM0031(swComp, "UV", item.Length, item.Deepth,650d,650d,item.ExRightDis, midRoofTopHoleDis, midRoofSecondHoleDis, midRoofHoleNo, item.LightType, item.LightYDis, item.LEDSpotNo, item.LEDSpotDis, item.ANSUL, item.ANDropNo, item.ANYDis, item.ANDropDis1, item.ANDropDis2, item.ANDropDis3, item.ANDropDis4, item.ANDropDis5, "NO", 0, 0, 0, 0, 0, 0, item.Bluetooth, item.UVType, item.MARVEL, item.IRNo, item.IRDis1, item.IRDis2, item.IRDis3);

                //华为灯板左右加高
                if (item.Length >= 2200d && item.Length <= 2400d)
                {
                    swAssy.UnSuppress(suffix, "FNHM0032-2");
                    swComp = swAssy.UnSuppress(suffix, "FNHM0032-1");
                    swEdit.FNHM0032(swComp, "UV", item.Deepth, "650", midRoofTopHoleDis);
                }
                else
                {
                    swAssy.Suppress(suffix, "FNHM0032-2");
                    swAssy.Suppress(suffix, "FNHM0032-1");
                }

                //----------吊装槽钢----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100001-1"));
                swPart = swComp.GetModelDoc2();
                if (item.ANSUL == "YES") swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 250) / 1000d;
                else swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 100d) / 1000d;

                #region 大侧板
                if (item.SidePanel == "BOTH")
                {
                    //LEFT
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0067-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0067(swComp, item.Deepth, 650d, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0068-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0068(swComp, item.Deepth, 650d);

                    //RIGHT
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0069-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0069(swComp, item.Deepth, 650d, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0070-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0070(swComp, item.Deepth, 650d);

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0071-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Deepth - 368d + 88d + 3d) / 1000d;//水洗烟罩(item.Deepth - 368d) / 1000d;
                        swPart.Parameter("D5@Sketch7").SystemValue = 12d / 1000d;//水洗烟罩19.87d / 1000d
                        swPart.Parameter("D5@Sketch10").SystemValue = 27.3d / 1000d;
                        //UV555400，22d,标准烟罩27.3
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0072-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Deepth - 368d + 88d + 3d) / 1000d;//水洗烟罩(item.Deepth - 368d) / 1000d;
                        swPart.Parameter("D5@Sketch7").SystemValue = 12d / 1000d;//水洗烟罩19.87d / 1000d
                        swPart.Parameter("D5@Sketch8").SystemValue = 27.3d / 1000d;
                        //UV555400，22d,标准烟罩27.3
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
                    swEdit.FNHS0067(swComp, item.Deepth, 650d, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0068-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0068(swComp, item.Deepth, 650d);

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0072-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0071-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Deepth - 368d + 88d + 3d) / 1000d;//水洗烟罩(item.Deepth - 368d) / 1000d;
                        swPart.Parameter("D5@Sketch7").SystemValue = 12d / 1000d;//水洗烟罩19.87d / 1000d
                        swPart.Parameter("D5@Sketch10").SystemValue = 27.3d / 1000d;
                        //UV555400，22d,标准烟罩27.3
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
                    swEdit.FNHS0069(swComp, item.Deepth, 650d, sidePanelSideCjNo, sidePanelDownCjNo);

                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0070-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0070(swComp, item.Deepth, 650d);

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0071-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0082-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swPart = swComp.GetModelDoc2();
                        swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Deepth - 368d + 88d + 3d) / 1000d;//水洗烟罩(item.Deepth - 368) / 1000d;
                        swPart.Parameter("D5@Sketch7").SystemValue = 12d / 1000d;//水洗烟罩19.87d / 1000d
                        swPart.Parameter("D5@Sketch8").SystemValue = 27.3d / 1000d;
                        //UV555400，22d,标准烟罩27.3
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
                #endregion

                //------------I型新风腔主体----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0113-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Length - 6.4d) / 1000d;
                swPart.Parameter("D1@阵列(线性)1").SystemValue = frontPanelKaKouNo;
                swPart.Parameter("D3@阵列(线性)1").SystemValue = frontPanelKaKouDis;
                swPart.Parameter("D3@Sketch3").SystemValue = midRoofSecondHoleDis - 3.2d / 1000d;
                swPart.Parameter("D5@草图7").SystemValue = 200d / 1000d - midRoofTopHoleDis;
                swFeat = swComp.FeatureByName("LPattern1");
                if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
                }
                //MARVEL
                if (item.MARVEL == "YES")
                {
                    if (item.IRNo > 0)
                    {
                        swFeat = swComp.FeatureByName("MA1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch46").SystemValue = item.IRDis1 / 1000d;
                        swFeat = swComp.FeatureByName("MACABLE1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch49").SystemValue = item.IRDis1 / 1000d;
                    }
                    if (item.IRNo > 1)
                    {
                        swFeat = swComp.FeatureByName("MA2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch47").SystemValue = item.IRDis2 / 1000d;
                        swFeat = swComp.FeatureByName("MACABLE2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch50").SystemValue = item.IRDis2 / 1000d;
                    }
                    if (item.IRNo > 2)
                    {
                        swFeat = swComp.FeatureByName("MA3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch48").SystemValue = item.IRDis3 / 1000d;
                        swFeat = swComp.FeatureByName("MACABLE3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch51").SystemValue = item.IRDis3 / 1000d;
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
                swFeat = swComp.FeatureByName("SUCABLE-RIGHT");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (item.SidePanel == "LEFT" || item.SidePanel == "BOTH") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("JUNCTION BOX-RIGHT");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩

                //----------新风前面板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0120-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 3d) / 1000d;
                swPart.Parameter("D1@阵列(线性)7").SystemValue = frontPanelKaKouNo;
                swPart.Parameter("D3@阵列(线性)7").SystemValue = frontPanelKaKouDis;
                swPart.Parameter("D1@LPattern1").SystemValue = frontPanelHoleNo;
                swPart.Parameter("D3@LPattern1").SystemValue = frontPanelHoleDis;
                //----------新风底部CJ孔板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0114-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.Length / 1000d;
                swPart.Parameter("D1@CJHOLES").SystemValue = frontCjNo;
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
