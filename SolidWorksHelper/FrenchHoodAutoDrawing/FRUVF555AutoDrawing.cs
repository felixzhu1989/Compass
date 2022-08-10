using System;
using System.IO;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    //2.实现接口具体方法
    public class FRUVF555AutoDrawing : IAutoDrawing
    {
        private Component2 swComp;
        private readonly FRUVF555Service objFRUVF555Service = new FRUVF555Service();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            #region 准备工作
            //packandgo后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = swApp.PackAndGoHood(tree, projectPath, out string suffix);

            //查询参数
            FRUVF555 item = (FRUVF555)objFRUVF555Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            FrenchHoodPart swFrenchHoodPart = new FrenchHoodPart();

            //打开Pack后的模型
            var swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            var swAssy = swModel as AssemblyDoc;
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);//TopOnly参数设置成true，只重建顶层，不重建零件内部 
            #endregion

            #region 计算中间值

            //新风CJ孔数量和新风CJ孔第一个CJ距离边缘距离
            int frontCjNo = (int)((item.Length - 30d) / 32d) + 1;
            double frontCjFirstDis = (item.Length - (frontCjNo - 1) * 32d) / 2d;

            //KSA数量，KSA侧板长度(以全长计算)
            int ksaNo = (int)((item.Length + 1d) / 498d);
            double ksaSideLength = Convert.ToDouble((item.Length - ksaNo * 498d) / 2d);
            
            //侧板CJ孔整列到烟罩底部
            int sidePanelDownCjNo = (int)((item.Deepth - 95d) / 32d);
            //非水洗烟罩KV/UV
            int sidePanelSideCjNo = (int)((item.Deepth - 305d) / 32d);

            #endregion

            #region 法国烟罩新增中间值
            //排风腔背板铆钉孔,定距200
            int backRivetNum = (int)((item.Length - 90d) / 200) + 1;
            double backRivetSideDis = (item.Length - 200d * (backRivetNum - 1)) / 2d;
            //随着烟罩深度变化,除去排风，新风和灯板，剩下的距离等距居中处理
            double withoutExSuMiDis = (item.Deepth - 365d - 204d - 535d) / 2d;
            //吊装孔的位置
            double liftingHoleDis = (Math.Floor(withoutExSuMiDis / 50d)+1)*50d- withoutExSuMiDis+24d;

            //随着烟罩长度的变化，MidRoof侧板根据灯具的不同发生变化，长灯1353，短灯753,多减去1dm
            double midRoofSidePanel = (item.Length - 1335d) / 2d;//长
            if (item.LightType == "FSSHORT") midRoofSidePanel = (item.Length - 735d) / 2d;//短
            //新风面板螺丝孔数量及间距（计算规则改变，距离边缘250，间隔>600）
            int frontPanelHoleNo = (int)((item.Length - 500d) / 600d) + 1;
            double frontPanelHoleDis = item.Length - 500d;
            if (frontPanelHoleNo != 1)
            {
                frontPanelHoleDis = (item.Length - 500d) / (frontPanelHoleNo - 1d);
            }

            #endregion

            try
            {
                #region 装配体顶层
                //烟罩深度
                swModel.ChangeDim("D1@Distance39", item.Deepth);
                //判断KSA/Mesh数量，KSA/Mesh距离左边缘
                if (ksaNo < 2)
                {
                    swAssy.Suppress("LocalLPattern1");
                    swAssy.Suppress("LocalLPattern4");
                }
                else
                {
                    swAssy.UnSuppress("LocalLPattern1");
                    swAssy.UnSuppress("LocalLPattern4");
                    swModel.ChangeDim("D1@LocalLPattern1", ksaNo);
                    swModel.ChangeDim("D1@LocalLPattern4", ksaNo);
                }
                swModel.ChangeDim("D1@Distance30", ksaSideLength);
                swModel.ChangeDim("D1@Distance22", ksaSideLength + 10d);

                //油塞
                if (item.Outlet == "LEFTTAP")
                {
                    swAssy.Suppress(suffix, "2900100014-1");
                    swAssy.UnSuppress(suffix, "2900100014-2");
                }
                else if (item.Outlet == "RIGHTTAP")
                {
                    swAssy.UnSuppress(suffix, "2900100014-1");
                    swAssy.Suppress(suffix, "2900100014-2");
                }
                else
                {
                    swAssy.Suppress(suffix, "2900100014-1");
                    swAssy.Suppress(suffix, "2900100014-2");
                }
                //排风脖颈数量和距离
                if (item.ExNo == 1)
                {
                    swModel.ChangeDim("D1@Distance37", item.ExRightDis - item.ExLength / 2d);
                    swAssy.Suppress("LocalLPattern2");
                }
                else if (item.ExNo == 2 && (item.MARVEL == "YES" || item.ExHeight == 100d))
                {
                    swAssy.Suppress("LocalLPattern2");
                }
                else
                {
                    swModel.ChangeDim("D1@Distance37", item.ExRightDis - item.ExLength - item.ExDis / 2d);
                    swAssy.UnSuppress("LocalLPattern2");
                    swModel.ChangeDim("D1@LocalLPattern2", item.ExNo);
                    swModel.ChangeDim("D3@LocalLPattern2", item.ExDis + item.ExLength);
                }
                //----------新风脖颈----------
                if (item.SuNo == 1)
                    swAssy.Suppress("LocalLPattern3");
                else
                {
                    swAssy.UnSuppress("LocalLPattern3");
                    swModel.ChangeDim("D1@LocalLPattern3", item.SuNo);
                    swModel.ChangeDim("D3@LocalLPattern3", item.SuDis);
                }

                #endregion

                #region 排风腔背板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0195-1");
                swFrenchHoodPart.FNHE0195(swComp, item.Length, backRivetNum, backRivetSideDis, item.WaterCollection, item.SidePanel, item.Outlet, item.BackToBack);
                #endregion

                #region 排风腔顶板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0196-1");
                swFrenchHoodPart.FNHE0196(swComp, item.Length, backRivetNum, backRivetSideDis, item.ExRightDis, item.UVType,
                    item.ExNo, item.ExLength, item.ExWidth, item.ExDis, liftingHoleDis);
                #endregion

                #region 排风腔前面板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0197-1");
                swFrenchHoodPart.FNHE0197(swComp, item.Length, item.UVType, item.ExRightDis, withoutExSuMiDis, item.LightType, midRoofSidePanel);
                #endregion

                #region 排风三角板
                /*
                if (item.ANSUL == "YES" && item.SidePanel == "MIDDLE")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201030401-4");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201030401-5");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swFeat = swComp.FeatureByName("ANDTEC");
                    swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                }
                else if (item.ANSUL == "YES" && item.SidePanel == "LEFT")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201030401-4");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201030401-5");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swFeat = swComp.FeatureByName("ANDTEC");
                    swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                }
                else if (item.ANSUL == "YES" && item.SidePanel == "RIGHT")
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201030401-5");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201030401-4");
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swFeat = swComp.FeatureByName("ANDTEC");
                    swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201030401-4");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByNameWithSuffix(suffix, "5201030401-5");
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                } 
                */
                #endregion

                #region Mesh轨道支撑
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0198-1");
                ModelDoc2 swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@Base-Flange1", item.Length - 9d);
                //磁棒板位置,默认两边都有侧板，且对称，特殊情况以后处理
                swPart.ChangeDim("D3@Sketch85", ksaSideLength - 2.5d + 498d / 2d);
                if (ksaNo < 2) swComp.Suppress("LPattern1");
                else
                {
                    swComp.UnSuppress("LPattern1");
                    swPart.ChangeDim("D1@LPattern1", ksaNo);
                }
                #endregion

                #region Mesh轨道前
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0199-1");
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@Base-Flange1", item.Length - 7d);
                #endregion

                #region Mesh轨道后
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0200-1");
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@Base-Flange1", item.Length - 9d);
                //以后完善ANSUL选项，注意这个零件的拉丝面
                /*
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
                */
                #endregion

                #region UV灯，UV灯门
                if (item.UVType == "LONG")
                {
                    swAssy.UnSuppress(suffix, "5214050402-1");
                    swAssy.UnSuppress(suffix, "5201060409-1");
                    swAssy.UnSuppress(suffix, "2200600040-2");
                    swAssy.Suppress(suffix, "5214050401-1");
                    swAssy.Suppress(suffix, "5201060410-1");
                    swAssy.Suppress(suffix, "2200600040-1");
                }
                else
                {
                    swAssy.Suppress(suffix, "5214050402-1");
                    swAssy.Suppress(suffix, "5201060409-1");
                    swAssy.Suppress(suffix, "2200600040-2");
                    swAssy.UnSuppress(suffix, "5214050401-1");
                    swAssy.UnSuppress(suffix, "5201060410-1");
                    swAssy.UnSuppress(suffix, "2200600040-1");
                }
                #endregion

                #region 排风脖颈
                swFrenchHoodPart.ExaustSpigot(swAssy, suffix, item.ANSUL, item.MARVEL, item.ExLength, item.ExWidth, item.ExHeight);
                #endregion

                #region 排风滑门/导轨
                swFrenchHoodPart.ExaustRail(swAssy, suffix, item.MARVEL, item.ExLength, item.ExWidth, item.ExNo, item.ExDis);
                #endregion

                #region KSA侧边,默认两边都有侧板，且对称，特殊情况以后处理
                swFrenchHoodPart.KSAFilter(swAssy, suffix, ksaSideLength);
                #endregion

                #region MESH油网侧板,默认两边都有侧板，且对称，特殊情况以后处理，且与KSA错开10dm，即左边+10，右边-10
                swFrenchHoodPart.MeshFilter(swAssy, suffix, ksaSideLength);
                #endregion

                #region 日光灯
                swFrenchHoodPart.LightOperate(swAssy, suffix, item.LightType);
                #endregion

                #region 吊装槽钢
                swFrenchHoodPart.Hanger(swAssy, suffix, item.Deepth, item.ANSUL, item.SidePanel);
                #endregion

                #region MidRoof侧板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0041-1");
                swFrenchHoodPart.FNHM0041(swComp, midRoofSidePanel);
                #endregion

                #region 大侧板
                if (item.SidePanel == "BOTH")
                {
                    //LEFT
                    swComp = swAssy.UnSuppress(suffix, "FNHS0073-1");
                    swFrenchHoodPart.FNHS0073(swComp, item.Deepth, 555d, sidePanelSideCjNo, sidePanelDownCjNo);
                    swComp = swAssy.UnSuppress(suffix, "FNHS0075-1");
                    swFrenchHoodPart.FNHS0075(swComp, item.Deepth, 555d, withoutExSuMiDis);
                    //RIGHT
                    swComp = swAssy.UnSuppress(suffix, "FNHS0074-1");
                    swFrenchHoodPart.FNHS0074(swComp, item.Deepth, 555d, sidePanelSideCjNo, sidePanelDownCjNo);
                    swComp = swAssy.UnSuppress(suffix, "FNHS0076-1");
                    swFrenchHoodPart.FNHS0076(swComp, item.Deepth, 555d, withoutExSuMiDis);

                    if (item.WaterCollection == "YES")
                    {
                        swComp = swAssy.UnSuppress(suffix, "FNHS0077-1");
                        swFrenchHoodPart.FNHS0077(swComp, item.Deepth);
                        swComp = swAssy.UnSuppress(suffix, "FNHS0078-1");
                        swFrenchHoodPart.FNHS0078(swComp, item.Deepth);
                    }
                    else
                    {
                        swAssy.Suppress(suffix, "FNHS0077-1");
                        swAssy.Suppress(suffix, "FNHS0078-1");
                    }
                }
                else if (item.SidePanel == "LEFT")
                {
                    swAssy.Suppress(suffix, "FNHS0074-1");
                    swAssy.Suppress(suffix, "FNHS0076-1");

                    swComp= swAssy.UnSuppress(suffix, "FNHS0073-1");
                    swFrenchHoodPart.FNHS0073(swComp, item.Deepth, 555d, sidePanelSideCjNo, sidePanelDownCjNo);
                    swComp = swAssy.UnSuppress(suffix, "FNHS0075-1");
                    swFrenchHoodPart.FNHS0075(swComp, item.Deepth, 555d, withoutExSuMiDis);

                    if (item.WaterCollection == "YES")
                    {
                        swAssy.Suppress(suffix, "FNHS0078-1");
                        swComp = swAssy.UnSuppress(suffix, "FNHS0077-1");
                        swFrenchHoodPart.FNHS0077(swComp, item.Deepth);
                    }
                    else
                    {
                        swAssy.Suppress(suffix, "FNHS0077-1");
                        swAssy.Suppress(suffix, "FNHS0078-1");
                    }
                }
                else if (item.SidePanel == "RIGHT")
                {
                    swAssy.Suppress(suffix, "FNHS0073-1");
                    swAssy.Suppress(suffix, "FNHS0075-1");

                    swComp = swAssy.UnSuppress(suffix, "FNHS0074-1");
                    swFrenchHoodPart.FNHS0074(swComp, item.Deepth, 555d, sidePanelSideCjNo, sidePanelDownCjNo);
                    swComp = swAssy.UnSuppress(suffix, "FNHS0076-1");
                    swFrenchHoodPart.FNHS0076(swComp, item.Deepth, 555d, withoutExSuMiDis);

                    if (item.WaterCollection == "YES")
                    {
                        swAssy.Suppress(suffix, "FNHS0077-1");
                        swComp = swAssy.UnSuppress(suffix, "FNHS0078-1");
                        swFrenchHoodPart.FNHS0078(swComp, item.Deepth);
                    }
                    else
                    {
                        swAssy.Suppress(suffix, "FNHS0077-1");
                        swAssy.Suppress(suffix, "FNHS0078-1");
                    }
                }
                else
                {
                    swAssy.Suppress(suffix, "FNHS0073-1");
                    swAssy.Suppress(suffix, "FNHS0075-1");
                    swAssy.Suppress(suffix, "FNHS0074-1");
                    swAssy.Suppress(suffix, "FNHS0076-1");
                    swAssy.Suppress(suffix, "FNHS0077-1");
                    swAssy.Suppress(suffix, "FNHS0078-1");
                }
                #endregion

                #region F型新风腔底部CJ孔板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0115-1");
                swFrenchHoodPart.FNHA0115(swComp, item.Length, frontCjNo, frontCjFirstDis, frontPanelHoleNo, frontPanelHoleDis,
                    item.Bluetooth, item.LEDlogo, item.WaterCollection, item.SidePanel);
                #endregion

                #region 新风斜面板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0116-1");
                swFrenchHoodPart.FNHA0116(swComp, item.Length, withoutExSuMiDis, item.LightType, midRoofSidePanel);
                #endregion

                #region 新风顶面板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0117-1");
                swFrenchHoodPart.FNHA0117(swComp, item.Length, backRivetNum, backRivetSideDis, item.SuNo, item.SuDis, item.Bluetooth, item.SidePanel,liftingHoleDis);
                #endregion

                #region 新风导轨
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0123-1");
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@Base-Flange1", item.Length - 180d);
                #endregion

                #region 新风顶包边
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0118-1");
                swFrenchHoodPart.FNHA0118(swComp, item.Length, backRivetNum, backRivetSideDis, frontPanelHoleNo,
                    frontPanelHoleDis);
                #endregion

                #region 新风前网孔面板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0111-1");
                swFrenchHoodPart.FNHA0111(swComp, item.Length, frontPanelHoleNo, frontPanelHoleDis);

                #endregion

                #region 镀锌隔板
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0112-1");
                swPart = swComp.GetModelDoc2();
                swPart.ChangeDim("D2@Base-Flange1", item.Length - 6d);
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
