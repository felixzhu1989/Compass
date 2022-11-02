﻿using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.IO;

namespace SolidWorksHelper
{
    public class HWUVI650AutoDrawing : IAutoDrawing, ICreateBom
    {
        private Component2 swComp;
        readonly HWUVI650Service objHWUVI650Service = new HWUVI650Service();

        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            #region 准备工作
            //packandgo后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = swApp.PackAndGoHood(tree, projectPath, out string suffix);

            //查询参数
            HWUVI650 item = (HWUVI650)objHWUVI650Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            HuaWeiHoodPart swEdit = new HuaWeiHoodPart();

            //打开Pack后的模型
            ModelDoc2 swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            AssemblyDoc swAssy = swModel as AssemblyDoc;
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true); //TopOnly参数设置成true，只重建顶层，不重建零件内部
            #endregion 准备工作

            #region 计算中建参数
            /*注意SolidWorks单位是m，计算是应当/1000d
                 * 整形与整形运算得出的结果仍然时整形，1640 结果为0，因此必须将其中一个转化成double型，使用后缀m就可以了
                 * (int)不进行四舍五入，Convert.ToInt32会四舍五入
                */
            //-----------计算中间值，----------
            //新风面板卡扣数量及间距
            int frontPanelKaKouNo = (int)((item.Length - 300d) / 450d) + 2;
            double frontPanelKaKouDis = Convert.ToDouble((item.Length - 300d) / (frontPanelKaKouNo - 1));
            //新风面板螺丝孔数量及间距
            int frontPanelHoleNo = (int)((item.Length - 300d) / 900d) + 2;
            double frontPanelHoleDis = Convert.ToDouble((item.Length - 300d) / (frontPanelHoleNo - 1));
            //新风CJ孔数量和新风CJ孔第一个CJ距离边缘距离
            int frontCjNo = (int)((item.Length - 30d) / 32d) + 1;
            double frontCjFirstDis = Convert.ToDouble((item.Length - (frontCjNo - 1) * 32d) / 2);
            //Midroof灯板螺丝孔数量及第二个孔距离边缘距离,灯板顶面吊装槽钢螺丝孔位距离
            int midRoofHoleNo = (int)((item.Length - 300d) / 400d);
            double midRoofSecondHoleDis = Convert.ToDouble((item.Length - (midRoofHoleNo - 1) * 400d) / 2);
            double midRoofTopHoleDis =
                Convert.ToDouble(item.Deepth - 535d - 360d - 90d -
                                  (int)((item.Deepth - 535d - 360d - 90d - 100d) / 50d) * 50d);
            //KSA数量，KSA侧板长度(以全长计算)
            int ksaNo = (int)((item.Length + 1) / 498d);
            double ksaSideLength = Convert.ToDouble((item.Length - ksaNo * 498d) / 2d);
            
            
            
            

            #endregion 计算中建参数

            try
            {

                #region 装配体顶层
                //烟罩深度
                swModel.ChangeDim("D1@Distance87", item.Deepth);
                //判断KSA数量，KSA侧板长度，如果太小，则使用特殊小侧板侧边
                if (ksaNo == 1) swAssy.Suppress("LocalLPattern1");
                else
                {
                    swAssy.UnSuppress("LocalLPattern1");
                    swModel.ChangeDim("D1@LocalLPattern1", ksaNo); //D1阵列数量,D3阵列距离
                }
                //KSA距离左边缘
                if (ksaSideLength < 12d) swModel.ChangeDim("D1@Distance86", 0.5d);
                else swModel.ChangeDim("D1@Distance86", ksaSideLength);

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
                    swModel.ChangeDim("D1@Distance95", item.ExRightDis - item.ExLength / 2d);
                    swAssy.Suppress("LocalLPattern2");
                }
                else
                {
                    swModel.ChangeDim("D1@Distance95", item.ExRightDis - item.ExLength - item.ExDis / 2d);
                    swAssy.UnSuppress("LocalLPattern2");
                    swModel.ChangeDim("D1@LocalLPattern2", item.ExNo); //D1阵列数量,D3阵列距离
                    swModel.ChangeDim("D3@LocalLPattern2", item.ExDis + item.ExLength); //D1阵列数量,D3阵列距离
                }
                //UV灯支架，两个
                if (item.UVType == "DOUBLE") swAssy.UnSuppress("LocalLPattern5");
                else swAssy.Suppress("LocalLPattern5");
                #endregion 装配体顶层

                #region 排风腔
                //----------排风腔----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0186-1");
                swEdit.FNHE0186(swComp, item.Length, midRoofHoleNo, midRoofSecondHoleDis, item.ExNo, item.ExLength, item.ExWidth, item.ExDis, item.ExRightDis, item.WaterCollection, item.SidePanel, item.Outlet, item.BackToBack, item.ANSUL, item.ANSide, item.ANDetector, item.UVType);

                //--------排风腔前面板-------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0187-1");
                swEdit.FNHE0187(swComp, item.Length, item.UVType, item.ExRightDis);
                //--------MESH油网后导轨-------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0188-1");
                swEdit.FNHE0188(swComp, item.Length, item.ANSUL, item.ANDetector);
                //--------MESH油网前导轨-------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0189-1");
                swEdit.FNHE0189(swComp, item.Length);
                //--------MESH油网轨道支架-------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHE0190-1");
                swEdit.FNHE0190(swComp, item.Length, item.UVType, item.ExRightDis);
                //--------KSA侧边-------
                swEdit.KSAFilter(swAssy, suffix, ksaSideLength, "FNHE0160-1", "FNHE0161-1", "FNHE0170-1");

                //----------排风滑门/导轨----------
                swEdit.ExaustRail(swAssy, suffix, item.ExLength, item.ExWidth, item.ExNo, "FNHE0164-1", "FNHE0165-1");

                //----------排风脖颈----------
                swEdit.ExaustSpigot(swAssy, suffix, item.ANSUL, item.ExLength, item.ExWidth, item.ExHeight, "FNHE0166-1", "FNHE0167-1", "FNHE0168-6", "FNHE0169-1");

                //----------UV灯，UV灯门----------
                var partList = new string[] { "FNHE0155-1", "FNHE0156-1", "FNHE0157-1", "FNHE0158-1", "FNHE0158-2" };
                swEdit.UVLightDoor(swAssy, suffix, item.UVType, partList);

                //----------MESH油网侧板----------
                swEdit.MeshFilter(swAssy, suffix, item.Length, item.ANSUL, item.ANSide, "FNHE0162-1", "FNHE0163-1");

                #endregion 排风腔

                #region MiddleRoof灯板

                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHM0031-1");
                swEdit.FNHM0031(swComp, "UV", item.Length, item.Deepth, 650d, 650d, item.ExRightDis, midRoofTopHoleDis, midRoofSecondHoleDis, midRoofHoleNo, item.LightType, item.LightYDis, item.LEDSpotNo, item.LEDSpotDis, item.ANSUL, item.ANDropNo, item.ANYDis, item.ANDropDis1, item.ANDropDis2, item.ANDropDis3, item.ANDropDis4, item.ANDropDis5, "NO", 0, 0, 0, 0, 0, 0, item.Bluetooth, item.UVType, item.MARVEL, item.IRNo, item.IRDis1, item.IRDis2, item.IRDis3);

                //华为灯板左右加高

                swAssy.UnSuppress(suffix, "FNHM0032-2");
                swComp = swAssy.UnSuppress(suffix, "FNHM0032-1");
                swEdit.FNHM0032(swComp, "UV", item.Deepth, 650d, midRoofTopHoleDis);

                //----------吊装槽钢----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "2900100001-1");
                swEdit.Std2900100001(swComp, item.Deepth, item.ANSUL);

                #endregion MiddleRoof灯板

                #region 大侧板
                swEdit.SidePanel(swAssy, suffix, item.SidePanel, item.Deepth, 650d, item.WaterCollection, "V", "FNHS0067-1", "FNHS0068-1", "FNHS0069-1", "FNHS0070-1", "FNHS0071-1", "FNHS0072-1");
                #endregion 大侧板

                #region I型新风腔
                //------------I型新风腔主体----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0113-1");
                swEdit.FNHA0113(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, midRoofHoleNo, midRoofSecondHoleDis, midRoofTopHoleDis, item.MARVEL, item.IRNo, item.IRDis1, item.IRDis2, item.IRDis3, item.Bluetooth, item.SidePanel);
                //----------新风前面板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0120-1");
                swEdit.FNHA0120(swComp, item.Length, frontPanelKaKouNo, frontPanelKaKouDis, frontPanelHoleNo, frontPanelHoleDis);
                //----------新风底部CJ孔板----------
                swComp = swAssy.GetComponentByNameWithSuffix(suffix, "FNHA0114-1");
                swEdit.FNHA0114(swComp, item.Length, frontCjNo, frontCjFirstDis, frontPanelHoleNo, frontPanelHoleDis, item.Bluetooth, item.LEDlogo, item.WaterCollection, item.SidePanel);
                #endregion I型新风腔

                swModel.ForceRebuild3(true);//设置成true，直接更新顶层，速度很快，设置成false，每个零件都会更新，很慢
                swModel.Save();//保存，很耗时间
                swApp.CloseDoc(packedAssyPath);//关闭，很快
            }
            catch (Exception ex)
            {
                throw new Exception($"{packedAssyPath} 作图过程发生异常。\n零件：{swComp.Name}\n详细：{ex.Message}");
            }
            finally
            {
                swApp.CommandInProgress = false; //及时关闭外部命令调用，否则影响SolidWorks的使用
            }

        }

        public void CreateSemiBom(ModuleTree tree, Dictionary<string, int> semiBomDic)
        {
            
        }
    }
}