using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.IO;

namespace SolidWorksHelper
{
    public class HWUWF650AutoDrawing : IAutoDrawing
    {
        private Component2 swComp;
        private readonly HWUWF650Service objHWUWF650Service = new HWUWF650Service();

        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            #region 准备工作
            //packandgo后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = swApp.PackAndGoHood(tree, projectPath, out string suffix);

            //查询参数
            HWUWF650 item = (HWUWF650)objHWUWF650Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            HuaWeiHoodPart swEdit = new HuaWeiHoodPart();

            //打开Pack后的模型
            var swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            var swAssy = swModel as AssemblyDoc;//装配体
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);//TopOnly参数设置成true，只重建顶层，不重建零件内部

            #endregion 准备工作
            

            #region 计算中间值
            //新风面板卡扣数量及间距
            int frontPanelKaKouNo = (int)((item.Length - 300d) / 450d) + 2;
            double frontPanelKaKouDis = Convert.ToDouble((item.Length - 300d) / (frontPanelKaKouNo - 1));
            //新风面板螺丝孔数量及间距
            int frontPanelHoleNo = (int)((item.Length - 300d) / 900d) + 2;
            double frontPanelHoleDis = Convert.ToDouble((item.Length - 300) / (frontPanelHoleNo - 1));
            //新风CJ孔数量和新风CJ孔第一个CJ距离边缘距离
            int frontCjNo = (int)((item.Length - 30d) / 32d) + 1;
            double frontCjFirstDis = Convert.ToDouble(item.Length - (frontCjNo - 1) * 32d) / 2d;
            //Midroof灯板螺丝孔数量及第二个孔距离边缘距离,灯板顶面吊装槽钢螺丝孔位距离
            int midRoofHoleNo = (int)((item.Length - 300d) / 400d);
            double midRoofSecondHoleDis = Convert.ToDouble((item.Length - (midRoofHoleNo - 1) * 400d) / 2);
            double midRoofTopHoleDis =
                Convert.ToDouble(item.Deepth - 535d - 360d - 90d -
                                  (int)((item.Deepth - 535d - 360d - 90d - 100d) / 50d) * 50d);
            //KSA数量，KSA侧板长度(以全长计算)水洗烟罩KSA在三角板内侧，减去3dm
            int ksaNo = (int)((item.Length - 2d) / 498d);
            double ksaSideLength = Convert.ToDouble((item.Length - 3d - ksaNo * 498d) / 2);
            

            #endregion 计算中间值

            try
            {
                #region 装配体顶层特征

                //烟罩深度
                swModel.ChangeDim("D1@Distance81", item.Deepth);
                //判断KSA数量，KSA侧板长度，如果太小，则使用特殊小侧板侧边
                if (ksaNo == 1) swAssy.Suppress("LocalLPattern1");
                else
                {
                    swAssy.UnSuppress("LocalLPattern1");
                    swModel.ChangeDim("D1@LocalLPattern1", ksaNo); //D1阵列数量,D3阵列距离
                }
                //KSA距离左边缘
                if (ksaSideLength < 12d) swModel.ChangeDim("D1@Distance82", 0.5d);
                else swModel.ChangeDim("D1@Distance82", ksaSideLength);
                
                #region 排风脖颈数量和距离

                if (item.ExNo == 1)
                {
                    swModel.ChangeDim("D1@Distance80", item.ExRightDis - item.ExLength / 2d);
                    swAssy.Suppress("LocalLPattern2");
                }
                else
                {
                    swModel.ChangeDim("D1@Distance80", item.ExRightDis - item.ExLength - item.ExDis / 2d);
                    swAssy.UnSuppress("LocalLPattern2");
                    swModel.ChangeDim("D1@LocalLPattern2", item.ExNo); //D1阵列数量,D3阵列距离
                    swModel.ChangeDim("D3@LocalLPattern2", item.ExDis + item.ExLength); //D1阵列数量,D3阵列距离
                }

                #endregion 排风脖颈数量和距离

                //UV灯支架，两个
                if (item.UVType == "DOUBLE") swAssy.UnSuppress("LocalLPattern4");
                else swAssy.Suppress("LocalLPattern4");

                

                #region 新风脖颈,新风前面板中间加强筋

                if (item.SuNo == 1) swAssy.Suppress("LocalLPattern3");
                else
                {
                    swAssy.UnSuppress("LocalLPattern3");
                    swModel.ChangeDim("D1@LocalLPattern3", item.SuNo); //D1阵列数量,D3阵列距离
                    swModel.ChangeDim("D3@LocalLPattern3", item.SuDis); //D1阵列数量,D3阵列距离
                }
                if (item.Length > 1599d) swAssy.UnSuppress(suffix, "FNHA0124-1");
                else swAssy.Suppress(suffix, "FNHA0124-1");

                #endregion 新风脖颈,新风前面板中间加强筋

                #endregion 装配体顶层特征

                #region 排风腔

                //----------排风腔背部零件----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0179-1");
                swEdit.FNHE0179(swComp, item.Length, item.WaterCollection, item.SidePanel, item.Outlet, item.BackToBack);

                //----------排风腔顶部零件----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0148-1"));
                swEdit.FNHE0148(swComp, item.Length, midRoofSecondHoleDis, midRoofHoleNo, item.ExNo, item.ExRightDis, item.ExLength, item.ExWidth, item.ExDis, item.Inlet, item.ANSUL, item.ANSide, item.MARVEL, item.UVType, "UW");
                
                //----------排风腔前面板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0180-1"));
                swEdit.FNHE0180(swComp, item.Length, item.Inlet, item.UVType, item.ExRightDis);

                //----------水洗挡板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0150-1"));
                swEdit.FNHE0150(swComp, item.Length,"UW");

                //----------排风腔内部零件----------
                //MESH油网下导轨
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0154-1");
                swEdit.FNHE0154(swComp, item.Length);
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0153-1");
                swEdit.FNHE0153(swComp, item.Length);
                //KSA下轨道
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0151-1");
                swEdit.FNHE0151(swComp, item.Length);

                //MESH油网轨道支架
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0152-1");
                swEdit.FNHE0152(swComp, item.Length, item.UVType, item.ExRightDis);

                //----------KSA侧边----------
                swEdit.KSAFilter(swAssy, suffix, ksaSideLength, "FNHE0160-1", "FNHE0161-1", "FNHE0170-1");

                //----------排风滑门/导轨----------
                swEdit.ExaustRail(swAssy, suffix, item.ExLength, item.ExWidth, item.ExNo, "FNHE0164-1", "FNHE0165-1");

                //----------排风脖颈----------
                swEdit.ExaustSpigot(swAssy, suffix, item.ANSUL, item.ExLength, item.ExWidth, item.ExHeight, "FNHE0166-1", "FNHE0167-1", "FNHE0168-6", "FNHE0169-1");

                //----------UV灯，UV灯门----------
                var partList = new string[] { "FNHE0155-1", "FNHE0156-1", "FNHE0157-1", "FNHE0158-1", "FNHE0158-2", "FNHE0171-1", "FNHE0172-1", "FNHE0172-2" };
                swEdit.UVLightDoor(swAssy, suffix, item.UVType, partList);

                //----------MESH油网侧板----------
                swEdit.UwMeshFilter(swAssy, suffix, item.Length, item.Inlet, item.ANSUL, item.ANSide, "FNHE0162-1", "FNHE0163-1");

                #endregion 排风腔

                #region 灯具

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

                #endregion 灯具

                #region MiddleRoof灯板

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0031-2");
                swEdit.FNHM0031(swComp, "UW", item.Length, item.Deepth, 650d, 650d, item.ExRightDis, midRoofTopHoleDis, midRoofSecondHoleDis, midRoofHoleNo, item.LightType, item.LightYDis, item.LEDSpotNo, item.LEDSpotDis, item.ANSUL, item.ANDropNo, item.ANYDis, item.ANDropDis1, item.ANDropDis2, item.ANDropDis3, item.ANDropDis4, item.ANDropDis5, item.ANDetectorEnd, item.ANDetectorNo, item.ANDetectorDis1, item.ANDetectorDis2, item.ANDetectorDis3, item.ANDetectorDis4, item.ANDetectorDis5, item.Bluetooth, item.UVType, item.MARVEL, item.IRNo, item.IRDis1, item.IRDis2, item.IRDis3);

                //华为灯板左右加高
                swAssy.UnSuppress(suffix, "FNHM0032-2");
                swComp = swAssy.UnSuppress(suffix, "FNHM0032-1");
                swEdit.FNHM0032(swComp, "UW", item.Deepth, 650d, midRoofTopHoleDis);

                //----------吊装槽钢----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100001-1");
                swEdit.Std2900100001(swComp, item.Deepth, item.ANSUL);

                #endregion MiddleRoof灯板

                #region 大侧板

                swEdit.SidePanel(swAssy, suffix, item.SidePanel, item.Deepth, 650d, item.WaterCollection, "W", "FNHS0067-1", "FNHS0068-1", "FNHS0069-1", "FNHS0070-1", "FNHS0071-1", "FNHS0072-1");

                #endregion 大侧板

                #region F型新风

                //------------F型新风腔主体----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0108-1");
                swEdit.FNHA0108(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, midRoofSecondHoleDis, midRoofTopHoleDis, midRoofHoleNo, item.SuNo, item.SuDis, item.MARVEL, item.IRNo, item.IRDis1, item.IRDis2, item.IRDis3, item.Bluetooth, item.SidePanel);
                //------------F新风底部CJ孔板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0093-1");
                swEdit.FNHA0093(swComp, item.Length, frontCjNo, frontCjFirstDis, frontPanelHoleNo, frontPanelHoleDis, item.Bluetooth, item.LEDlogo, item.WaterCollection, item.SidePanel);
                //------------F新风前面板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0107-1");
                swEdit.FNHA0107(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, frontPanelHoleNo, frontPanelHoleDis);
                //------------F镀锌隔板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0006-1");
                swEdit.FNHA0006(swComp, item.Length, item.MARVEL, item.IRNo, item.IRDis1, item.IRDis2, item.IRDis3);
                //------------新风滑门导轨----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0097-1");
                swEdit.FNHA0097(swComp, item.Length);
                #endregion F型新风

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