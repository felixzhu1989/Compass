using System;
using System.IO;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class CMODF700TSAutoDrawing : IAutoDrawing
    {
        readonly CMODF700TSService objCMODF700TSService = new CMODF700TSService();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            #region 准备工作
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
            CMODF700TS item = (CMODF700TS)objCMODF700TSService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix;//后缀
            //厚度为1.2的板材零件
            ThickSheetPart swEdit = new ThickSheetPart();
            //借用华为1.2板材零件
            HuaWeiHoodPart hwEdit = new HuaWeiHoodPart();
            //借用烟罩零件
            HoodPart hoodEdit = new HoodPart();

            //打开Pack后的模型
            var swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            var swAssy = swModel as AssemblyDoc;
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);//TopOnly参数设置成true，只重建顶层，不重建零件内部

            #endregion

            #region 计算中间值
            //新风面板卡扣数量及间距
            int frontPanelKaKouNo = (int)((item.Length - 300d) / 450d) + 2;
            double frontPanelKaKouDis = Convert.ToDouble((item.Length - 300d) / (frontPanelKaKouNo - 1));

            //新风CJ孔数量和新风CJ孔第一个CJ距离边缘距离
            int frontCjNo = (int)((item.Length - 30d) / 32d) + 1;
            double frontCjFirstDis = Convert.ToDouble((item.Length - (frontCjNo - 1) * 32d) / 2d);

            //Midroof灯板螺丝孔数量及第二个孔距离边缘距离,灯板顶面吊装槽钢螺丝孔位距离
            int midRoofHoleNo = (int)((item.Length - 300d) / 400d);
            double midRoofSecondHoleDis = Convert.ToDouble((item.Length - (midRoofHoleNo - 1) * 400d) / 2d);

            //侧板CJ孔整列到烟罩底部
            int sidePanelDownCjNo = (int)((item.Deepth - 95d) / 32d);
            //水洗烟罩KW/UW
            int sidePanelSideCjNo = (int)((item.Deepth - 380d) / 32d);

            ////新风面板螺丝孔数量及间距
            //int frontPanelHoleNo = (int)((item.Length - 300d) / 900d) + 2;
            //double frontPanelHoleDis = Convert.ToDouble((item.Length - 300) / (frontPanelHoleNo - 1));

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
                Component2 swComp;

                #region 装配体顶层
                //烟罩深度
                swModel.ChangeDim("D1@Distance93", item.Deepth);
                //灯板加强筋
                if (item.Deepth > 1649d && ((item.LightType == "FSLONG" && item.Length > 1900d) ||
                                            (item.LightType == "FSSHORT" && item.Length > 1500d) || (item.Length > 2000d)))
                {
                    swAssy.UnSuppress(suffix, "FNHM0006-3");
                    swComp = swAssy.UnSuppress(suffix, "FNHM0006-2");
                    hoodEdit.FNHM0006(swComp, item.Deepth + (535 - 294));//CMOD
                }
                else
                {
                    swAssy.Suppress(suffix, "FNHM0006-2");
                    swAssy.Suppress(suffix, "FNHM0006-3");
                }

                //F型新风
                //----------新风脖颈----------
                if (item.SuNo == 1) swAssy.Suppress("LocalLPattern2");
                else
                {
                    swAssy.UnSuppress("LocalLPattern2");
                    swModel.ChangeDim("D1@LocalLPattern2",item.SuNo); 
                    swModel.ChangeDim("D3@LocalLPattern2",item.SuDis);
                }
                //----------新风前面板中间加强筋----------
                if (item.Length > 1599d) swAssy.UnSuppress(suffix, "FNHA0011-1");
                else swAssy.Suppress(suffix, "FNHA0011-1");

                #endregion

                #region CMOD
                //----------排风腔----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FSHE0013-2");
                swEdit.FSHE0013(swComp, item.Length, item.Outlet, item.BackToBack);

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FSHE0014-1");
                swEdit.FSHE0014(swComp, item.Length, midRoofHoleNo, midRoofSecondHoleDis, item.ExLength, item.ExWidth, item.Outlet, item.Inlet, item.ANSUL, item.ANSide, item.ANDetectorEnd);

                //----------三角板/ANSUL----------
                //swEdit.ExaustSide(swAssy, suffix, item.ANSUL, item.SidePanel, "FSHE0015-1", "FSHE0016-1");


                //----------排风腔前面板及内部零件----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FSHE0018-1");
                swEdit.FSHE0018(swComp, item.Length);

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FSHE0017-1");
                swEdit.FSHE0017(swComp, item.Length);

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FSHE0031-1");
                swEdit.FSHE0031(swComp, item.Length);

                //----------排风脖颈----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FSHE0033-1");
                swEdit.FSHE0033(swComp, item.ExLength, item.ExHeight);

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FSHE0032-1");
                swEdit.FSHE0032(swComp, item.ExWidth, item.ExHeight);

                //---------水洗挡板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FSHE0022-1");
                swEdit.FSHE0022(swComp, item.Length);
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FSHE0020-1");
                swEdit.FSHE0020(swComp, item.Length);
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FSHE0021-1");
                swEdit.FSHE0021(swComp, item.Length);
                #endregion

                #region MidRoof
                //----------MiddleRoof灯板----------
                //深度方向多减去6mm
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0031-1");
                hwEdit.FNHM0031(swComp, "CMOD", item.Length, item.Deepth + (535 - 294 - 6), 700d, 700d, item.ExRightDis, 50d / 1000d, midRoofSecondHoleDis / 1000d, midRoofHoleNo, item.LightType, (item.Deepth - (294d + 360d)) / 2d + 360d, item.LEDSpotNo, item.LEDSpotDis, item.ANSUL, item.ANDropNo, item.ANYDis, item.ANDropDis1, item.ANDropDis2, item.ANDropDis3, item.ANDropDis4, item.ANDropDis5, "NO", 0, 0, 0, 0, 0, 0, "NO", "NO", item.MARVEL, item.IRNo, item.IRDis1, item.IRDis2, item.IRDis3);

                //华为灯板左右加高
                if (item.Length >= 2200d && item.Length <= 2400d)
                {
                    swAssy.UnSuppress(suffix, "FNHM0032-4");
                    swComp = swAssy.UnSuppress(suffix, "FNHM0032-3");
                    hwEdit.FNHM0032(swComp, "CMOD", item.Deepth, "700", 50d / 1000d);
                }
                else
                {
                    swAssy.Suppress(suffix, "FNHM0032-4");
                    swAssy.Suppress(suffix, "FNHM0032-3");
                }

                hoodEdit.Light(swAssy, suffix, item.LightType, "5201020410-1", "5201020409-1");

                #endregion

                #region 大侧板
                if (item.SidePanel == "BOTH")
                {
                    //LEFT
                    swComp = swAssy.UnSuppress(suffix, "FNHS0067-1");
                    hwEdit.FNHS0067(swComp, item.Deepth, 700d, sidePanelSideCjNo, sidePanelDownCjNo);
                    swComp = swAssy.UnSuppress(suffix, "FNHS0068-1");
                    hwEdit.FNHS0068(swComp, item.Deepth, 700d);

                    //RIGHT
                    swComp = swAssy.UnSuppress(suffix, "FNHS0069-1");
                    hwEdit.FNHS0069(swComp, item.Deepth, 700d, sidePanelSideCjNo, sidePanelDownCjNo);
                    swComp = swAssy.UnSuppress(suffix, "FNHS0070-1");
                    hwEdit.FNHS0070(swComp, item.Deepth, 700d);
                }
                else if (item.SidePanel == "LEFT")
                {
                    swAssy.Suppress(suffix, "FNHS0069-1");
                    swAssy.Suppress(suffix, "FNHS0070-1");

                    swComp = swAssy.UnSuppress(suffix, "FNHS0067-1");
                    hwEdit.FNHS0067(swComp, item.Deepth, 700d, sidePanelSideCjNo, sidePanelDownCjNo);
                    swComp = swAssy.UnSuppress(suffix, "FNHS0068-1");
                    hwEdit.FNHS0068(swComp, item.Deepth, 700d);
                }
                else if (item.SidePanel == "RIGHT")
                {
                    swAssy.Suppress(suffix, "FNHS0067-1");
                    swAssy.Suppress(suffix, "FNHS0068-1");

                    swComp = swAssy.UnSuppress(suffix, "FNHS0069-1");
                    hwEdit.FNHS0069(swComp, item.Deepth, 700d, sidePanelSideCjNo, sidePanelDownCjNo);
                    swComp = swAssy.UnSuppress(suffix, "FNHS0070-1");
                    hwEdit.FNHS0070(swComp, item.Deepth, 700d);
                }
                else
                {
                    swAssy.Suppress(suffix, "FNHS0067-1");
                    swAssy.Suppress(suffix, "FNHS0068-1");
                    swAssy.Suppress(suffix, "FNHS0069-1");
                    swAssy.Suppress(suffix, "FNHS0070-1");
                }


                #endregion

                #region F型新风

                //------------F型新风腔主体-----
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FSHA0011-1");
                swEdit.FSHA0011(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, midRoofSecondHoleDis, 0d, midRoofHoleNo, item.SuDis, item.SuNo, item.MARVEL, item.IRNo, item.IRDis1, item.IRDis2, item.IRDis3, item.SidePanel, "KV", "NO");

                //----------新风底部CJ孔板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FSHA0010-1");
                swEdit.FSHA0010(swComp, item.Length, frontCjNo, frontCjFirstDis, frontPanelHoleNo, frontPanelHoleDis, "NO", item.LEDlogo, "NO", item.SidePanel);

                //----------新风前面板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FSHA0012-1");
                swEdit.FSHA0012(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, frontPanelHoleNo, frontPanelHoleDis);

                //----------镀锌隔板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FSHA0013-1");
                swEdit.FSHA0013(swComp, item.Length);

                //----------滑门轨道----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0010-1");
                hoodEdit.FNHA0010(swComp,item.Length);
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0010-1");
                hoodEdit.FNHE0010(swComp, item.Length);

                #endregion



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
