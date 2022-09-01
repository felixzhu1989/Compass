using System;
using System.IO;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class HWUVF555400AutoDrawing : IAutoDrawing
    {
        Component2 swComp;
        readonly HWUVF555400Service objHWUVF555400Service = new HWUVF555400Service();

        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            #region 准备工作
            //packandgo后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = swApp.PackAndGoHood(tree, projectPath, out string suffix);

            //查询参数
            HWUVF555400 item = (HWUVF555400)objHWUVF555400Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            
           
            ModelDoc2 swPart;
            
            Feature swFeat;
            HuaWeiHoodPart swEdit = new HuaWeiHoodPart();

            //打开Pack后的模型
            ModelDoc2 swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            AssemblyDoc swAssy = swModel as AssemblyDoc; //装配体
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true); //TopOnly参数设置成true，只重建顶层，不重建零件内部 
            #endregion

            #region 中间参数
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
            

            //HWUWF555400斜侧板CJ孔计算,150为排风底部长度,非水洗长度为76，555-400为高度差
            int sidePanelDownCjNo = (int)(((double)(Math.Sqrt(Math.Pow((double)item.Deepth - 76d, 2) + Math.Pow(555d - 400d, 2))) - 95d) / 32d);
            int sidePanelSideCjNo = sidePanelDownCjNo - 3;

            #endregion

            try
            {
                #region Top Level
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
                if (ksaSideLength < 12d / 1000d) swModel.Parameter("D1@Distance87").SystemValue = 0.5d / 1000d;
                else swModel.Parameter("D1@Distance87").SystemValue = ksaSideLength;

                
                //排风脖颈数量和距离
                if (item.ExNo == 1)
                {
                    swModel.Parameter("D1@Distance89").SystemValue = (item.ExRightDis - item.ExLength / 2) / 1000d;
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swModel.Parameter("D1@Distance89").SystemValue =
                        (item.ExRightDis - item.ExLength - item.ExDis / 2) / 1000d;
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern2").SystemValue = item.ExNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D3@LocalLPattern2").SystemValue =
                        (item.ExDis + item.ExLength) / 1000d; //D1阵列数量,D3阵列距离
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
                #endregion

                #region 排风腔
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0193-2");
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D1@草图1").SystemValue = item.Length/ 1000d;
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
                    swPart.Parameter("D4@Sketch9").SystemValue = item.ExRightDis/ 1000d;
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
                    if (item.ANDetector == "NO")
                    {
                        swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
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
                    swFeat = swComp.FeatureByName("ANDTEC-RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("ANDTEC-LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
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
                    //swPart.Parameter("D7@Sketch17").SystemValue = item.ExRightDis/ 1000d;
                    swFeat = swComp.FeatureByName("UVRACK");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.UVType == "LONG")
                {
                    swFeat = swComp.FeatureByName("UVRACK");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D6@Sketch12").SystemValue = item.ExRightDis/ 1000d;
                    swPart.Parameter("D5@Sketch12").SystemValue = 1640d / 1000d;
                    swFeat = swComp.FeatureByName("UVDOUBLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.UVType == "SHORT")
                {
                    //SHORT
                    swFeat = swComp.FeatureByName("UVRACK");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D6@Sketch12").SystemValue = item.ExRightDis/ 1000d;
                    swPart.Parameter("D5@Sketch12").SystemValue = 930d / 1000d;
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
                #endregion

                #region 排风腔前面板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0194-2");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = item.Length / 1000d;
                //UV HoodParent,过滤器感应出线孔，UV门，UV cable-UV灯线缆穿孔避让缺口
                //UV灯门
                if (item.UVType == "DOUBLE")
                {
                    swFeat = swComp.FeatureByName("UVDOOR-DOUBLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    //swPart.Parameter("D1@Sketch37").SystemValue = item.ExRightDis / 1000d;

                    //swFeat = swComp.FeatureByName("UVCABLE-DOUBLE");
                    //swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    //swPart.Parameter("D1@Sketch41").SystemValue = item.ExRightDis / 1000d;

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
                    swPart.Parameter("D2@Sketch17").SystemValue = item.ExRightDis / 1000d;

                    swFeat = swComp.FeatureByName("UVCABLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch22").SystemValue = item.ExRightDis / 1000d;
                    swPart.Parameter("D4@Sketch22").SystemValue = 1500d / 1000d;

                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-DOUBLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    
                }
                else if (item.UVType == "SHORT")
                {
                    swFeat = swComp.FeatureByName("UVDOOR-SHORT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch21").SystemValue = item.ExRightDis / 1000d;

                    swFeat = swComp.FeatureByName("UVCABLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch22").SystemValue = item.ExRightDis / 1000d;
                    swPart.Parameter("D4@Sketch22").SystemValue = 790d / 1000d;

                    swFeat = swComp.FeatureByName("UVDOOR-LONG");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("UVDOOR-DOUBLE");
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
                    
                }
                #endregion
                
                #region KSA侧边
                if (ksaSideLength <= 3.3d / 1000d)
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0160-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0161-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0170-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else if (ksaSideLength < 12d / 1000d && ksaSideLength > 3.3d / 1000d)//华为板材厚，改成3.3无法折弯
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0160-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0161-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0170-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D1@草图1").SystemValue = ksaSideLength * 2;
                }
                else if (ksaSideLength < 25d / 1000d && ksaSideLength >= 12d / 1000d)
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0160-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength * 2;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0161-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0170-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0160-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0161-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();
                    swPart.Parameter("D2@草图1").SystemValue = ksaSideLength;
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0170-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                #endregion

                #region 排风滑门/导轨
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0164-1");
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.ExLength / 2 + 10d) / 1000d;
                swPart.Parameter("D2@Sketch1").SystemValue = (item.ExWidth + 20d) / 1000d;


                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0165-1");
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                if (item.ExNo == 1) swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 2 + 100d) / 1000d;
                else swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 2 + 20d) / 1000d;
                #endregion

                #region 排风脖颈
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0166-1");
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50d) / 1000d;
                swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000d;
                swFeat = swComp.FeatureByName("ANSUL");
                if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0167-1");
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50d) / 1000d;
                swPart.Parameter("D2@Sketch1").SystemValue = item.ExHeight / 1000d;
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0168-6");
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                //swFeat = swComp.FeatureByName("ANDTEC");
                //if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0169-1");
                swComp.SetSuppression2(2);//2解压缩，0压缩
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                //swFeat = swComp.FeatureByName("ANDTEC");
                //if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                #endregion

                #region UV灯，UV灯门，双门
                if (item.UVType == "DOUBLE")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0155-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0156-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0157-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0158-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0158-2");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0155-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0156-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0157-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0158-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0158-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                #endregion

                //----------MESH油网侧板----------
                swEdit.MeshFilter(swAssy, suffix, item.Length, item.ANSUL, item.ANSide, "FNHE0162-1", "FNHE0163-1");

                #region 排风腔内部零件
                //MESH油网下导轨
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0188-1");
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
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0189-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 7d) / 1000d;

                //MESH油网轨道支架
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0190-1");
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
                #endregion

                #region MiddleRoof灯板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0031-1");
                swEdit.FNHM0031(swComp, "UV", item.Length, item.Deepth, 555d,400d, item.ExRightDis, midRoofTopHoleDis*1000d, midRoofSecondHoleDis*1000d, midRoofHoleNo, item.LightType, item.LightYDis, item.LEDSpotNo, item.LEDSpotDis, item.ANSUL, item.ANDropNo, item.ANYDis, item.ANDropDis1, item.ANDropDis2, item.ANDropDis3, item.ANDropDis4, item.ANDropDis5, "NO", 0, 0, 0, 0, 0, 0, item.Bluetooth, item.UVType, item.MARVEL, item.IRNo, item.IRDis1, item.IRDis2, item.IRDis3);

                //华为灯板左右加高
                if (item.Length >= 2200d && item.Length <= 2400d)
                {
                    swAssy.UnSuppress(suffix, "FNHM0032-2");
                    swComp = swAssy.UnSuppress(suffix, "FNHM0032-1");
                    swEdit.FNHM0032(swComp, "UV", item.Deepth, 555d, midRoofTopHoleDis*1000d);
                }
                else
                {
                    swAssy.Suppress(suffix, "FNHM0032-2");
                    swAssy.Suppress(suffix, "FNHM0032-1");
                }


                //----------吊装槽钢----------
                //swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100001-1");
                //swPart = swComp.GetModelDoc2();
                //if (item.ANSUL == "YES") swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 250) / 1000d;
                //else swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 100d) / 1000d;
                #endregion

                #region 大侧板
                if (item.SidePanel == "BOTH")
                {
                    //LEFT
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0059-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0059(swComp,item.Deepth,555d,400d,"V",sidePanelSideCjNo,sidePanelDownCjNo);//普通烟罩"V"，水洗"W"
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0061-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0061(swComp, item.Deepth, 555d, 400d, "V");//普通烟罩"V"，水洗"W"

                    //RIGHT
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0060-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0059(swComp, item.Deepth, 555d, 400d, "V", sidePanelSideCjNo, sidePanelDownCjNo);//普通烟罩"V"，水洗"W"

                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0062-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0061(swComp, item.Deepth, 555d, 400d, "V");//普通烟罩"V"，水洗"W"

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0063-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swEdit.FNHS0063(swComp, item.Deepth, 555d, 400d, "V");//普通烟罩"V"，水洗"W"

                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0064-2");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swEdit.FNHS0063(swComp, item.Deepth, 555d, 400d, "V");//普通烟罩"V"，水洗"W"
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0063-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0064-2");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                else if (item.SidePanel == "LEFT")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0060-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0062-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩

                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0059-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0059(swComp, item.Deepth, 555d, 400d, "V", sidePanelSideCjNo, sidePanelDownCjNo);//普通烟罩"V"，水洗"W"
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0061-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0061(swComp, item.Deepth, 555d, 400d, "V");//普通烟罩"V"，水洗"W"

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0064-2");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0063-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swEdit.FNHS0063(swComp, item.Deepth, 555d, 400d, "V");//普通烟罩"V"，水洗"W"
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0063-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0064-2");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                else if (item.SidePanel == "RIGHT")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0059-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0061-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩

                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0060-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0059(swComp, item.Deepth, 555d, 400d, "V", sidePanelSideCjNo, sidePanelDownCjNo);//普通烟罩"V"，水洗"W"

                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0062-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swEdit.FNHS0061(swComp, item.Deepth, 555d, 400d, "V");//普通烟罩"V"，水洗"W"

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0063-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0064-2");
                        swComp.SetSuppression2(2); //2解压缩，0压缩
                        swEdit.FNHS0063(swComp, item.Deepth, 555d, 400d, "V");//普通烟罩"V"，水洗"W"
                    }
                    else
                    {
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0063-1");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                        swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0064-2");
                        swComp.SetSuppression2(0); //2解压缩，0压缩
                    }
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0059-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0061-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0060-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0062-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0063-1");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHS0064-2");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                #endregion

                #region F型新风腔主体
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0092-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@基体-法兰1").SystemValue = item.Length / 1000d;
                swPart.Parameter("D1@阵列(线性)1").SystemValue = frontPanelKaKouNo;
                swPart.Parameter("D3@阵列(线性)1").SystemValue = frontPanelKaKouDis;
                swPart.Parameter("D3@Sketch4").SystemValue = midRoofSecondHoleDis;
                swPart.Parameter("D9@草图7").SystemValue = 200d / 1000d - midRoofTopHoleDis;
                swFeat = swComp.FeatureByName("LPattern1");
                if (midRoofHoleNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern1").SystemValue = midRoofHoleNo;
                }
                //新风脖颈
                swPart.Parameter("D1@Sketch8").SystemValue = (item.SuDis * (item.SuNo / 2 - 1) + item.SuDis / 2d) / 1000d;
                swFeat = swComp.FeatureByName("LPattern2");
                if (item.SuNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                else
                {
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern2").SystemValue = item.SuNo;
                    swPart.Parameter("D3@LPattern2").SystemValue = item.SuDis / 1000d;
                }
                //UV HOOD
                swFeat = swComp.FeatureByName("SUCABLE-LEFT");
                if (item.Bluetooth == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                swFeat = swComp.FeatureByName("JUNCTION BOX-LEFT");
                if (item.SidePanel == "LEFT" || item.SidePanel == "BOTH") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                #endregion

                #region 新风前面板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0094-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@草图1").SystemValue = (item.Length - 3d) / 1000d;
                swPart.Parameter("D1@阵列(线性)7").SystemValue = frontPanelKaKouNo;
                swPart.Parameter("D3@阵列(线性)7").SystemValue = frontPanelKaKouDis;
                swPart.Parameter("D1@LPattern1").SystemValue = frontPanelHoleNo;
                swPart.Parameter("D3@LPattern1").SystemValue = frontPanelHoleDis;
                #endregion
                
                #region 镀锌隔板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0022-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 10d) / 1000d;
                #endregion

                #region 新风滑门导轨
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0097-1");
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 200d) / 1000d;
                #endregion

                #region 新风底部CJ孔板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0093-1");
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
                #endregion
                
                swModel.ForceRebuild3(true);//设置成true，直接更新顶层，速度很快，设置成false，每个零件都会更新，很慢
                swModel.Save();//保存，很耗时间
                swApp.CloseDoc(packedAssyPath);//关闭，很快
            }
            catch (Exception ex)
            {
                //以后记录在日志中
                throw new Exception($"作图过程发生异常：{packedAssyPath} 。\n零件：{swComp.Name}\n对象：{ex.Source}\n详细：{ex.Message}");
            }
            finally
            {
                swApp.CommandInProgress = false; //及时关闭外部命令调用，否则影响SolidWorks的使用
            }

        }

    }
}
