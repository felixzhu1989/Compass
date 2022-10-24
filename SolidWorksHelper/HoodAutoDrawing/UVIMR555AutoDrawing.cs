using System;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class UVIMR555AutoDrawing : IAutoDrawing
    {
        private Component2 swComp;
        readonly UVIMR555Service objUVIMR555Service = new UVIMR555Service();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            #region 准备工作
            //packandgo后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = swApp.PackAndGoHood(tree, projectPath, out string suffix);
            //查询参数
            UVIMR555 item = (UVIMR555)objUVIMR555Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            HoodPart swHoodPart = new HoodPart();
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
            //新风面板螺丝孔数量及间距
            int frontPanelHoleNo = (int)((item.Length - 300d) / 900d) + 2;
            double frontPanelHoleDis = Convert.ToDouble((item.Length - 300) / (frontPanelHoleNo - 1));
            //新风CJ孔数量和新风CJ孔第一个CJ距离边缘距离
            int frontCjNo = (int)((item.Length - 30d) / 32d) + 1;
            double frontCjFirstDis = Convert.ToDouble((item.Length - (frontCjNo - 1) * 32d) / 2);
            //Midroof灯板螺丝孔数量及第二个孔距离边缘距离,灯板顶面吊装槽钢螺丝孔位距离
            int midRoofHoleNo = (int)((item.Length - 300d) / 400d);
            double midRoofSecondHoleDis = Convert.ToDouble((item.Length - (midRoofHoleNo - 1) * 400d) / 2);
            double midRoofTopHoleDis = Convert.ToDouble(item.Deepth/2d - 400d - 130d - 75d -(int)((item.Deepth/2d - 400d - 130d - 75d - 50d) / 50d) * 50d);
            //KSA数量，KSA侧板长度(以全长计算)
            int ksaNo = (int)((item.Length + 1) / 498d);
            double ksaSideLength = Convert.ToDouble((item.Length - ksaNo * 498d) / 2);
            
            
            #endregion

            try
            {
                #region Top Level
                //烟罩深度
                swModel.ChangeDim("D1@Distance45", (item.Deepth - 800d) / 2d);
                swModel.ChangeDim("D1@Distance46", (item.Deepth - 800d) / 2d);
                //判断KSA数量，KSA侧板长度，如果太小，则使用特殊小侧板侧边
                if (ksaNo == 1) swAssy.Suppress("LocalLPattern1");
                else
                {
                    swAssy.UnSuppress("LocalLPattern1");
                    swModel.ChangeDim("D1@LocalLPattern1", ksaNo);
                }
                //KSA距离左边缘
                if (ksaSideLength < 12d) swModel.ChangeDim("D1@Distance41", 0.5d);
                else swModel.ChangeDim("D1@Distance41", ksaSideLength);
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

                    swModel.ChangeDim("D1@Distance40", item.ExRightDis - item.ExLength / 2d);
                    swAssy.Suppress("LocalLPattern2");
                }
                else if (item.ExNo == 2 && (item.MARVEL == "YES" || item.ExHeight == 100d))
                {
                    swAssy.Suppress("LocalLPattern2");
                }
                else
                {
                    swModel.ChangeDim("D1@Distance40", item.ExRightDis - item.ExLength - item.ExDis / 2d);
                    swAssy.UnSuppress("LocalLPattern2");
                    swModel.ChangeDim("D1@LocalLPattern2", item.ExNo);
                    swModel.ChangeDim("D3@LocalLPattern2", item.ExDis + item.ExLength);
                }
                //IR-LHC-2（前后都要控制）
                if (item.MARVEL == "YES")
                {
                    swAssy.UnSuppress(suffix, "IR-LHC-2-1");
                    swAssy.UnSuppress(suffix, "IR-LHC-2-2");
                }
                else
                {
                    swAssy.Suppress(suffix, "IR-LHC-2-1");
                    swAssy.Suppress(suffix, "IR-LHC-2-2");
                }
                #endregion

                //----------排风腔----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0017-1");
                swHoodPart.FNHE0017(swComp, item.Length, midRoofHoleNo, midRoofSecondHoleDis, item.ExNo, item.ExRightDis, item.ExLength, item.ExWidth, item.ExDis, item.WaterCollection, item.SidePanel, item.Outlet, item.ANSUL, item.ANSide, item.ANDetector, item.MARVEL, item.UVType);
                
                //----------排风腔前面板、后面板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0018-1");
                swHoodPart.FNHE0018(swComp,item.Length);

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0019-1");
                swHoodPart.FNHE0019(swComp, item.Length);
                
                //----------排风腔底部----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0020-1");
                swHoodPart.FNHE0020(swComp, item.Length,item.WaterCollection,item.SidePanel,item.Outlet);

                //----------三角板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0021-1");
                swHoodPart.FNHE0021(swComp, item.UVType);


                //----------KSA侧边----------
                swHoodPart.MHoodKSAFilter(swAssy, suffix, ksaSideLength, "FNHE0003-2", "FNHE0004-2", "FNHE0005-2", "FNHE0027-1", "FNHE0026-1", "FNHE0028-1");


                //----------排风滑门/导轨----------
                swHoodPart.ExhaustRail(swAssy, suffix, item.MARVEL, item.ExLength, item.ExWidth, item.ExNo, item.ExDis, "FNCE0013-1", "FNCE0013-2", "FNCE0018-1", "FNCE0018-2");

                //----------排风脖颈----------
                swHoodPart.ExhaustSpigot(swAssy, suffix, item.ANSUL, item.MARVEL, item.ExLength, item.ExWidth, item.ExHeight, "FNHE0006-1", "FNHE0007-2", "FNHE0008-1", "FNHE0009-2");

                //----------UV灯----------
                //swHoodPart.UvLightDoor(swAssy, suffix, item.UVType, "5201050414-1", "5201060409-1", "5201050413-1", "5201060410-1");

                if (item.UVType == "LONG")
                {
                    swAssy.UnSuppress(suffix, "5201060409-1");
                    swAssy.Suppress(suffix, "5201060410-1");
                }
                else
                {
                    swAssy.Suppress(suffix, "5201060409-1");
                    swAssy.UnSuppress(suffix, "5201060410-1");
                }

                //----------MESH油网侧板----------
                swHoodPart.MHoodMeshFilter(swAssy, suffix, item.Length, item.ANSUL, item.ANSide, "FNHE0012-2", "FNHE0013-2", "FNHE0030-1", "FNHE0029-1");
                
                //----------排风腔内部零件----------
                //MESH油网下导轨
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0022-1");
                swHoodPart.FNHE0022(swComp,item.Length);

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0023-1");
                swHoodPart.FNHE0023(swComp, item.Length);

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0024-1");
                swHoodPart.FNHE0024(swComp, item.Length);

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0025-1");
                swHoodPart.FNHE0025(swComp, item.Length);

               

                //----------MiddleRoof灯板/前----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0011-1");
                swHoodPart.FNHM0011(swComp, "UV", item.Length, item.Deepth, 555d, 555d, item.ExRightDis, midRoofTopHoleDis, midRoofSecondHoleDis, midRoofHoleNo, item.LightType, 0, item.LEDSpotNo, item.LEDSpotDis, item.ANSUL, item.ANDropNo, item.ANYDis, item.ANDropDis1, item.ANDropDis2, item.ANDropDis3, item.ANDropDis4, item.ANDropDis5, "NO", 0, 0, 0, 0, 0, 0, item.Bluetooth, item.UVType, item.MARVEL);

                
                //----------MiddleRoof灯板/后----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0012-5");
                swHoodPart.FNHM0012(swComp, "UV", item.Length, item.Deepth, 555d, 555d, item.ExRightDis, midRoofTopHoleDis, midRoofSecondHoleDis, midRoofHoleNo, item.LightType, 0, item.LEDSpotNo, item.LEDSpotDis, item.ANSUL, item.ANDropNo, item.ANYDis, item.ANDropDis1, item.ANDropDis2, item.ANDropDis3, item.ANDropDis4, item.ANDropDis5, "NO", 0, 0, 0, 0, 0, 0, item.Bluetooth, item.UVType, item.MARVEL);
                
                //----------吊装槽钢----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100001-1");
                swHoodPart.Std2900100001(swComp, item.ANSUL, item.Deepth);


                //----------大侧板(suType:MR or MT)----------
                swHoodPart.MHoodSidePanel(swAssy, suffix, item.SidePanel, item.Deepth, 555d, item.WaterCollection,"MR", "FNHS0007-1", "FNHS0008-1", "FNHS0007-2", "FNHS0009-1", "FNHS0010-1", "FNHS0010-2");



                //------------M型烟罩方形新风腔主体/前面----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0012-1");
                swHoodPart.FNHA0012(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, midRoofSecondHoleDis, midRoofTopHoleDis, midRoofHoleNo,item.MARVEL);


                //------------M型烟罩方形新风腔主体/背面----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0013-1");
                swHoodPart.FNHA0013(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, midRoofSecondHoleDis, midRoofTopHoleDis, midRoofHoleNo, item.MARVEL);


                //----------新风前面板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0003-2");
                swHoodPart.FNHA0003(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, frontPanelHoleNo, frontPanelHoleDis);

                //----------新风底部CJ孔板/前面----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0002-1");
                swHoodPart.FNHA0002(swComp, item.Length, frontCjNo, frontCjFirstDis, frontPanelHoleNo, frontPanelHoleDis, item.Bluetooth, item.LEDlogo, item.WaterCollection, item.SidePanel);
                
                //----------新风底部CJ孔板/背面----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0016-1");
                swHoodPart.FNHA0016(swComp, item.Length, frontCjNo, frontCjFirstDis, frontPanelHoleNo, frontPanelHoleDis, item.Bluetooth, item.LEDlogo, item.WaterCollection, item.SidePanel);
                
                //----------蓝牙----------
                swHoodPart.Bluetooth(swAssy, suffix, item.Bluetooth, "2900200001-1");

                //----------LOGO----------
                swHoodPart.LedLogo(swAssy, suffix, item.LEDlogo, "2900300005-1", "5201010401-1");


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
