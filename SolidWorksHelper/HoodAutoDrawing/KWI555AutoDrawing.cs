using System;
using System.IO;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Windows.Forms;
using Common;

namespace SolidWorksHelper
{
    //2.实现接口具体方法
    public class KWI555AutoDrawing : IAutoDrawing
    {
        private Component2 swComp;
        private readonly KWI555Service objKwi555Service = new KWI555Service();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            //packandgo后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = swApp.PackAndGoHood(tree, projectPath, out string suffix);


            //查询参数
            KWI555 item = (KWI555)objKwi555Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

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
            
            //-----------计算中间值，----------
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
            double midRoofTopHoleDis =
                Convert.ToDouble(item.Deepth - 535d - 360d - 90d -
                                  (int)((item.Deepth - 535d - 360d - 90d - 100d) / 50d) * 50d) ;
            //KSA数量，KSA侧板长度(以全长计算)水洗烟罩KSA在三角板内侧，减去3dm
            int ksaNo = (int)((item.Length - 2d) / 498d);
            double ksaSideLength = Convert.ToDouble((item.Length - 3d - ksaNo * 498d) / 2) ;
            

            try
            {
                //----------Top Level----------
                //烟罩深度
                swModel.ChangeDim("D1@Distance76", item.Deepth);

                //判断KSA数量，KSA侧板长度，如果太小，则使用特殊小侧板侧边
                if (ksaNo == 1) swAssy.Suppress("LocalLPattern1");
                else
                {
                    swAssy.UnSuppress("LocalLPattern1");
                    swModel.ChangeDim("D1@LocalLPattern1", ksaNo);
                }

                //KSA距离左边缘
                if (ksaSideLength < 12d) swModel.ChangeDim("D1@Distance56", 0.5d);
                else swModel.ChangeDim("D1@Distance56", ksaSideLength);

                //下水口管件
                //进水口管件
                if (item.Inlet == "LEFT")
                {
                    swAssy.UnSuppress(suffix, "Connection asm Inlet L-2");
                    swAssy.UnSuppress(suffix, "Connection asm KSA L-2");
                    swAssy.UnSuppress(suffix, "FNHB0037-3");
                    swAssy.UnSuppress(suffix, "FNHB0038-3");

                    swAssy.Suppress(suffix, "Connection asm Inlet R-2");
                    swAssy.Suppress(suffix, "Connection asm KSA R-2");
                    swAssy.Suppress(suffix, "FNHB0037-4");
                    swAssy.Suppress(suffix, "FNHB0038-4");
                }
                else
                {
                    swAssy.Suppress(suffix, "Connection asm Inlet L-2");
                    swAssy.Suppress(suffix, "Connection asm KSA L-2");
                    swAssy.Suppress(suffix, "FNHB0037-3");
                    swAssy.Suppress(suffix, "FNHB0038-3");

                    swAssy.UnSuppress(suffix, "Connection asm Inlet R-2");
                    swAssy.UnSuppress(suffix, "Connection asm KSA R-2");
                    swAssy.UnSuppress(suffix, "FNHB0037-4");
                    swAssy.UnSuppress(suffix, "FNHB0038-4");
                }
                //排风脖颈数量和距离
                if (item.ExNo == 1)
                {

                    swModel.ChangeDim("D1@Distance66", item.ExRightDis - item.ExLength / 2d);
                    swAssy.Suppress("LocalLPattern2");
                }
                else if (item.ExNo == 2 && (item.MARVEL == "YES" || item.ExHeight == 100d))
                {
                    swAssy.Suppress("LocalLPattern2");
                }
                else
                {
                    swModel.ChangeDim("D1@Distance66", item.ExRightDis - item.ExLength - item.ExDis / 2d);
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
                    swHoodPart.FNHM0006(swComp, item.Deepth);
                }
                else
                {
                    swAssy.Suppress(suffix, "FNHM0006-1");
                    swAssy.Suppress(suffix, "FNHM0006-2");
                }

                //IR-LHC-2
                if (item.MARVEL == "YES") swAssy.UnSuppress(suffix, "IR-LHC-2-1");
                else swAssy.Suppress(suffix, "IR-LHC-2-1");

                

                //----------排风腔----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0031-1");
                swHoodPart.FNHE0031(swComp,item.Length,item.WaterCollection,item.SidePanel,item.Outlet,item.BackToBack);
                
                //----------排风腔顶部零件----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0032-1");
                swHoodPart.FNHE0032(swComp, item.Length, midRoofSecondHoleDis, midRoofHoleNo, item.ExNo, item.ExRightDis, item.ExLength, item.ExWidth, item.ExDis, item.Inlet, item.ANSUL, item.ANSide, item.MARVEL, "NO");

                //----------排风腔前面板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0033-1");
                swHoodPart.FNHE0033(swComp,item.Length,item.Inlet,item.MARVEL,item.ExRightDis,"NO");
                

                //----------三角板上的UV----------内部运水
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0034-1");
                swHoodPart.FNHE0034(swComp,item.Outlet,item.SidePanel,"NO");

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0035-1");
                swHoodPart.FNHE0035(swComp, item.Outlet, item.SidePanel);


                //----------水洗挡板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0036-2");
                swHoodPart.FNHE0036(swComp,item.Length,"NO");
                
                //----------KSA侧边----------
                swHoodPart.KSAFilter(swAssy, suffix, ksaSideLength, "FNHE0003-1", "FNHE0004-1", "FNHE0005-1");


                //----------排风滑门/导轨----------
                swHoodPart.ExhaustRail(swAssy, suffix, item.MARVEL, item.ExLength, item.ExWidth, item.ExNo, item.ExDis, "FNCE0013-1", "FNCE0013-2", "FNCE0018-1", "FNCE0018-2");


                //----------排风脖颈----------
                swHoodPart.ExhaustSpigot(swAssy, suffix, item.ANSUL, item.MARVEL, item.ExLength, item.ExWidth, item.ExHeight, "FNHE0006-1", "FNHE0007-1", "FNHE0008-1", "FNHE0009-1");

                
                //----------排风腔内部零件----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900500003-1");
                swHoodPart.Std2900500003(swComp, item.Length);
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0037-1");
                swHoodPart.FNHE0037(swComp,item.Length);

                //----------灯具----------
                //日光灯
                swHoodPart.Light(swAssy, suffix, item.LightType, "5201020410-1", "5201020409-1");

                
                //----------MiddleRoof灯板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0001-1");
                swHoodPart.FNHM0001(swComp, "KW", item.Length, item.Deepth, 555d, 555d, item.ExRightDis, midRoofTopHoleDis, midRoofSecondHoleDis, midRoofHoleNo, item.LightType, 0, item.LEDSpotNo, item.LEDSpotDis, item.ANSUL, item.ANDropNo, item.ANYDis, item.ANDropDis1, item.ANDropDis2, item.ANDropDis3, item.ANDropDis4, item.ANDropDis5,item.ANDetectorEnd,item.ANDetectorNo,item.ANDetectorDis1,item.ANDetectorDis2,item.ANDetectorDis3,item.ANDetectorDis4,item.ANDetectorDis5,"NO", "NO", item.MARVEL);
                
                //----------吊装槽钢----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100001-1");
                swHoodPart.Std2900100001(swComp, item.ANSUL, item.Deepth);

                //----------大侧板----------
                swHoodPart.SidePanel(swAssy, suffix, item.SidePanel, item.Deepth, 555d, item.WaterCollection, "W", "FNHS0001-1", "FNHS0002-1", "FNHS0003-1", "FNHS0004-1", "FNHS0005-1", "FNHS0006-1");
                

                //------------I型新风腔主体----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0001-1");
                swHoodPart.FNHA0001(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, midRoofHoleNo, midRoofSecondHoleDis, midRoofTopHoleDis, item.MARVEL, item.SidePanel, "NO", "NO");

                //----------I新风前面板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0003-1");
                swHoodPart.FNHA0003(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, frontPanelHoleNo, frontPanelHoleDis);

                //----------新风底部CJ孔板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0002-1");
                swHoodPart.FNHA0002(swComp, item.Length, frontCjNo, frontCjFirstDis, frontPanelHoleNo, frontPanelHoleDis, "NO", item.LEDlogo, item.WaterCollection, item.SidePanel);

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
