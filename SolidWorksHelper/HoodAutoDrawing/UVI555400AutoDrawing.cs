using System;
using System.IO;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
//已通过扩展方法改造
namespace SolidWorksHelper
{
    public class UVI555400AutoDrawing : IAutoDrawing
    {
        Component2 swComp; readonly UVI555400Service objUvi555400Service = new UVI555400Service();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {

            #region 准备工作
            //packandgo后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = swApp.PackAndGoHood(tree, projectPath, out string suffix);


            //查询参数
            UVI555400 item = (UVI555400)objUvi555400Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            HoodPart swEdit = new HoodPart();

            //打开Pack后的模型
            var swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            var swAssy = swModel as AssemblyDoc;//装配体
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);//TopOnly参数设置成true，只重建顶层，不重建零件内部 
            #endregion

            #region 计算中间值
            //新风面板卡扣数量及间距
            int frontPanelKaKouNo = (int)((item.Length - 300d) / 450d) + 2;
            double frontPanelKaKouDis = Convert.ToDouble((item.Length - 300d) / (frontPanelKaKouNo - 1));
            //新风面板螺丝孔数量及间距
            int frontPanelHoleNo = (int)((item.Length - 300d) / 900d) + 2;
            double frontPanelHoleDis = Convert.ToDouble((item.Length - 300) / (frontPanelHoleNo - 1));
            //新风CJ孔数量和新风CJ孔第一个CJ距离边缘距离
            int frontCjNo = (int)((item.Length - 30d) / 32d) + 1;
            double frontCjFirstDis = Convert.ToDouble((item.Length - (frontCjNo - 1) * 32d) / 2);
            //Midroof灯板螺丝孔数量及第二个孔距离边缘距离,灯板顶面吊装槽钢螺丝孔位距离
            int midRoofHoleNo = (int)((item.Length - 300d) / 400d);
            double midRoofSecondHoleDis = Convert.ToDouble((item.Length - (midRoofHoleNo - 1) * 400d) / 2);
            double midRoofTopHoleDis = Convert.ToDouble(item.Deepth - 535d - 360d - 90d - (int)((item.Deepth - 535d - 360d - 90d - 100d) / 50d) * 50d);
            //KSA数量，KSA侧板长度(以全长计算)
            int ksaNo = (int)((item.Length + 1) / 498d);
            double ksaSideLength = Convert.ToDouble((item.Length - ksaNo * 498d) / 2) ;
            
            
            #endregion

            try
            {
                
                #region 装配体顶层
                //烟罩深度
                swModel.ChangeDim("D1@Distance40", item.Deepth);
                //判断KSA数量，KSA侧板长度，如果太小，则使用特殊小侧板侧边
                if (ksaNo == 1) swAssy.Suppress("LocalLPattern1");
                else
                {
                    swAssy.UnSuppress("LocalLPattern1");
                    swModel.ChangeDim("D1@LocalLPattern1", ksaNo);
                }
                //KSA距离左边缘
                if (ksaSideLength < 12d ) swModel.ChangeDim("D1@Distance15", 0.5d);
                else swModel.ChangeDim("D1@Distance15", ksaSideLength);
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

                    swModel.ChangeDim("D1@Distance22", item.ExRightDis - item.ExLength / 2d);
                    swAssy.Suppress("LocalLPattern2");
                }
                else if (item.ExNo == 2 && (item.MARVEL == "YES" || item.ExHeight == 100d))
                {
                    swAssy.Suppress("LocalLPattern2");
                }
                else
                {
                    swModel.ChangeDim("D1@Distance22", item.ExRightDis - item.ExLength - item.ExDis / 2d);
                    swAssy.UnSuppress("LocalLPattern2");
                    swModel.ChangeDim("D1@LocalLPattern2", item.ExNo);
                    swModel.ChangeDim("D3@LocalLPattern2", item.ExDis + item.ExLength);
                }
                //灯板加强筋
               
                if (item.Deepth > 1649d && ((item.LightType == "FSLONG" && item.Length > 1900d) ||
                                           (item.LightType == "FSSHORT" && item.Length > 1500d) || (item.Length > 2000d)))
                {
                    swAssy.UnSuppress(suffix, "FNHM0006-1");
                   swComp = swAssy.UnSuppress(suffix, "FNHM0006-2");
                   swEdit.FNHM0006(swComp, item.Deepth);
                }
                else
                {
                    swAssy.Suppress(suffix, "FNHM0006-1");
                    swAssy.Suppress(suffix, "FNHM0006-2");
                }

                //IR-LHC-2
                if (item.MARVEL == "YES") swAssy.UnSuppress(suffix, "IR-LHC-2-1");
                else swAssy.Suppress(suffix, "IR-LHC-2-1");
                #endregion

                //----------排风腔----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0001-1");
                swEdit.FNHE0001(swComp, item.Length, item.ExNo, item.ExRightDis, item.ExLength, item.ExWidth, item.ExDis, item.WaterCollection, item.SidePanel, item.Outlet, item.BackToBack, item.ANSUL, item.ANSide, item.ANDetector, item.MARVEL, item.UVType);

                //----------排风腔前面板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0002-1");
                swEdit.FNHE0002(swComp, item.Length, item.UVType, item.ExRightDis);

                //----------KSA侧边----------

                swEdit.KSAFilter(swAssy, suffix, ksaSideLength, "FNHE0003-1", "FNHE0004-1", "FNHE0005-1");

                //----------排风滑门/导轨----------
                swEdit.ExhaustRail(swAssy, suffix, item.MARVEL, item.ExLength, item.ExWidth, item.ExNo, item.ExDis, "FNCE0013-1", "FNCE0013-4", "FNCE0018-1", "FNCE0018-2");

                //----------排风脖颈----------
                swEdit.ExhaustSpigot(swAssy, suffix, item.ANSUL, item.MARVEL, item.ExLength, item.ExWidth, item.ExHeight, "FNHE0006-2", "FNHE0007-1", "FNHE0008-1", "FNHE0009-2");

                //----------排风三角板----------
                swEdit.ExhaustSide(swAssy, suffix, item.ANSUL, item.SidePanel, "5201030401-4", "5201030401-5");

                //----------UV灯，UV灯门----------

                swEdit.UvLightDoor(swAssy, suffix, item.UVType, "5201050414-1", "5201060409-1", "5201050413-1", "5201060410-1");

                //----------MESH油网侧板----------

                swEdit.MeshFilter(swAssy, suffix, item.Length, item.ANSUL, item.ANSide, "FNHE0012-1", "FNHE0013-1");

                //----------排风腔内部零件----------
                //MESH油网下导轨
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0014-1");
                swEdit.FNHE0014(swComp, item.Length, item.ANSUL, item.ANDetector);

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0015-1");
                swEdit.FNHE0015(swComp, item.Length);

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0016-1");
                swEdit.FNHE0016(swComp, item.Length, item.UVType, item.ExRightDis);

                //----------灯具----------
                //日光灯
                swEdit.Light(swAssy, suffix, item.LightType, "5201020410-1", "5201020409-1");

                //----------MiddleRoof灯板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0001-1");
                //swEdit.FNHM0001(swComp, "UV", item.Length, item.Deepth, 555d, 400d, item.ExRightDis, midRoofTopHoleDis, midRoofSecondHoleDis, midRoofHoleNo, item.LightType, 0, item.LEDSpotNo, item.LEDSpotDis, item.ANSUL, item.ANDropNo, item.ANYDis, item.ANDropDis1, item.ANDropDis2, item.ANDropDis3, item.ANDropDis4, item.ANDropDis5, "NO", 0, 0, 0, 0, 0, 0, item.Bluetooth, item.UVType, item.MARVEL);

                //----------吊装槽钢----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100001-1");
                swEdit.Std2900100001(swComp, item.ANSUL, item.Deepth);


                //----------大侧板---------- 

                swEdit.SidePanelSpecial(swAssy, suffix, item.SidePanel, item.Deepth,555d,400d, item.WaterCollection,"V", "FNHS0012-1", "FNHS0013-1", "FNHS0014-1", "FNHS0015-1", "FNHS0005-1", "FNHS0006-1");

                //------------I型新风腔主体----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0040-1");
                swEdit.FNHA0040(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, midRoofHoleNo, midRoofSecondHoleDis, midRoofTopHoleDis, item.MARVEL, item.UVType, item.Bluetooth, item.SidePanel);

                //----------新风前面板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0041-1");
                swEdit.FNHA0041(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, frontPanelHoleNo, frontPanelHoleDis);

                //----------新风底部CJ孔板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0002-1");
                //swEdit.FNHA0002(swComp, item.Length, frontCjNo, frontCjFirstDis, frontPanelHoleNo, frontPanelHoleDis, item.Bluetooth, item.LEDlogo, item.WaterCollection, item.SidePanel);

                //----------蓝牙----------
                swEdit.Bluetooth(swAssy, suffix, item.Bluetooth, "2900200001-1");

                //----------LOGO----------
                swEdit.LedLogo(swAssy, suffix, item.LEDlogo, "2900300005-1", "5201010401-1");

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
