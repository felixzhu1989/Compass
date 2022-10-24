using System;
using System.IO;
using System.Windows.Forms;
using Common;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorksHelper.CeilingAutoDrawing;

namespace SolidWorksHelper
{
    public class UCJDB800AutoDrawing : IAutoDrawing
    {
        Component2 swComp; 
        readonly UCJDB800Service objUCJDB800Service = new UCJDB800Service();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            //创建项目模型存放地址
            //packandgo后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = swApp.PackAndGoCeiling(tree, projectPath, out string suffix);

            //查询参数
            UCJDB800 item = (UCJDB800)objUCJDB800Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            CeilingPart ceilingPart = new CeilingPart();
            //打开Pack后的模型
            var swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            var swAssy = swModel as AssemblyDoc; //装配体
           
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);
            //TopOnly参数设置成true，只重建顶层，不重建零件内部
            
            //-----------计算中间值，----------
            int fcNo = (int)((item.Length - item.FCSideLeft - item.FCSideRight) / 499d) - item.FCBlindNo;

            try
            {
                //判断是否是HCL特殊灯腔
                if (item.LightType == "HCL")
                {
                    #region 压缩/解压
                    //NOHCL压缩
                    swAssy.Suppress(suffix, "FNCE0116-1");//灯腔
                    swAssy.Suppress(suffix, "FNCE0056-1");
                    swAssy.Suppress(suffix, "FNCE0056-2");
                    swAssy.Suppress(suffix, "FNCE0145-2");//磁棒版
                    swAssy.Suppress(suffix, "FNCE0161-2");//磁棒版

                    swAssy.Suppress(suffix, "FNCE0114-3");//三角板
                    swAssy.Suppress(suffix, "FNCE0114-4");
                    swAssy.Suppress(suffix, "FNCE0100-1");//磁铁支架
                    swAssy.Suppress(suffix, "FNCE0100-2");

                    //HCL解压
                    //swAssy.UnSuppress(suffix, "FNCE0054-1");//灯腔
                    //swAssy.UnSuppress(suffix, "FNCE0069-1");//磁棒版
                    //swAssy.UnSuppress(suffix, "FNCE0071-1");//磁棒版
                    //swAssy.UnSuppress(suffix, "FNCE0099-1");
                    //swAssy.UnSuppress(suffix, "FNCE0090-1");
                    //swAssy.UnSuppress(suffix, "FNCE0091-1");
                    //swAssy.UnSuppress(suffix, "FNCE0091-2");
                    //swAssy.UnSuppress(suffix, "FNCE0092-1");
                    //swAssy.UnSuppress(suffix, "FNCE0094-1");
                    //swAssy.UnSuppress(suffix, "FNCE0093-1");

                    swAssy.UnSuppress(suffix, "HCL-1000-1");
                    //三角板
                    swAssy.UnSuppress(suffix, "FNCE0066-1");
                    swAssy.UnSuppress(suffix, "FNCE0066-2");
                    swAssy.UnSuppress(suffix, "FNCE0101-1");//磁铁支架
                    swAssy.UnSuppress(suffix, "FNCE0101-2");
                    #endregion

                    #region 顶层
                    //先解压缩出来零件再改顶层装配关系
                    swModel.ChangeDim("D1@Distance41", item.LightPanelLeft);
                    swModel.ChangeDim("D1@Distance42", item.LightPanelLeft);
                    #region 镀锌片数量
                    if (item.LightPanelSide == "BOTH")
                    {
                        swAssy.UnSuppress(suffix, "FNCE0093-1");
                        swModel.ChangeDim("D1@LocalLPattern7", 8);
                    }
                    else if (item.LightPanelSide == "RIGHT" || item.LightPanelSide == "LEFT")
                    {
                        swAssy.UnSuppress(suffix, "FNCE0093-1");
                        swModel.ChangeDim("D1@LocalLPattern7", 4);
                    }
                    else
                    {
                        swAssy.Suppress("LocalLPattern7");
                        swAssy.Suppress(suffix, "FNCE0093-1");
                    }
                    #endregion
                    #endregion

                    //-------UCJDB800HCL排风腔内灯腔----------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0054-1");
                    ceilingPart.FNCE0054(swComp, item.Length, item.LightCable, item.LightType, item.Japan);
                    //-------HCL磁棒板----------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0069-1");
                    ceilingPart.FNCE0069(swComp, item.Length, item.FCSide, fcNo, item.FCBlindNo, item.FCSideLeft);
                    swComp = swAssy.UnSuppress(suffix, "FNCE0071-1");
                    ceilingPart.FNCE0071(swComp, item.Length, item.FCSide, fcNo, item.FCBlindNo, item.FCSideRight);

                    //-------灯腔玻璃支架底部-------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0099-1");
                    ceilingPart.FNCE0099(swComp, item.Length, item.LightPanelSide, item.LightPanelLeft, item.LightPanelRight);
                    swComp = swAssy.UnSuppress(suffix, "FNCE0090-1");
                    ceilingPart.FNCE0090(swComp, item.Length, item.LightPanelSide, item.LightPanelLeft, item.LightPanelRight);

                    //-------灯腔玻璃支架支撑条上部-------
                    swAssy.UnSuppress(suffix, "FNCE0091-1");
                    swComp = swAssy.UnSuppress(suffix, "FNCE0091-2");
                    ceilingPart.FNCE0091(swComp, item.Length, item.LightPanelSide, item.LightPanelLeft, item.LightPanelRight);

                    //-------HCL灯腔侧板,重命名-------
                    ceilingPart.HCLSidePanel(swModel, swAssy, suffix, tree.Module, item.LightPanelSide, item.LightPanelLeft, item.LightPanelRight, "FNCE0092", 1, "FNCE0094", 1);

                }
                else
                {
                    #region 压缩/解压
                    //NOHCL压缩
                    //swAssy.UnSuppress(suffix, "FNCE0116-1");//灯腔
                    //swAssy.UnSuppress(suffix, "FNCE0056-1");
                    //swAssy.UnSuppress(suffix, "FNCE0056-2");
                    //swAssy.UnSuppress(suffix, "FNCE0145-2");//磁棒版
                    //swAssy.UnSuppress(suffix, "FNCE0161-2");//磁棒版

                    swAssy.UnSuppress(suffix, "FNCE0114-3");//三角板
                    swAssy.UnSuppress(suffix, "FNCE0114-4");
                    swAssy.UnSuppress(suffix, "FNCE0100-1");//磁铁支架
                    swAssy.UnSuppress(suffix, "FNCE0100-2");

                    //HCL解压
                    swAssy.Suppress(suffix, "FNCE0054-1");//灯腔
                    swAssy.Suppress(suffix, "FNCE0069-1");//磁棒版
                    swAssy.Suppress(suffix, "FNCE0071-1");//磁棒版
                    swAssy.Suppress(suffix, "FNCE0099-1");
                    swAssy.Suppress(suffix, "FNCE0090-1");
                    swAssy.Suppress(suffix, "FNCE0091-1");
                    swAssy.Suppress(suffix, "FNCE0091-2");
                    swAssy.Suppress(suffix, "FNCE0092-1");
                    swAssy.Suppress(suffix, "FNCE0094-1");
                    swAssy.Suppress(suffix, "FNCE0093-1");

                    swAssy.Suppress(suffix, "HCL-1000-1");
                    //三角板
                    swAssy.Suppress(suffix, "FNCE0066-1");
                    swAssy.Suppress(suffix, "FNCE0066-2");
                    swAssy.Suppress(suffix, "FNCE0101-1");//磁铁支架
                    swAssy.Suppress(suffix, "FNCE0101-2");
                    swAssy.Suppress("LocalLPattern7");
                    #endregion

                    //----------灯腔----------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0116-1");
                    ceilingPart.FNCE0116(swComp, item.Length,item.UVType, item.LightCable, item.LightType, item.Japan);

                    //-------磁棒板----------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0145-2");
                    ceilingPart.FNCE0145(swComp, item.Length, item.FCSide, fcNo, item.FCBlindNo, item.FCSideLeft);
                    swComp = swAssy.UnSuppress(suffix, "FNCE0161-2");
                    ceilingPart.FNCE0161(swComp, item.Length, item.FCSide, fcNo, item.FCBlindNo, item.FCSideRight);

                    swAssy.UnSuppress(suffix, "FNCE0056-1");
                    swComp = swAssy.UnSuppress(suffix, "FNCE0056-2");
                    ceilingPart.FNCE0056(swComp, item.Length);

                }

                //----------公共零件----------
                //排风腔
                swComp = ceilingPart.RenameComp(swModel, swAssy, suffix, "UCJDB800", tree.Module, "FNCE0141", 1, item.Length, 800);
                if (swComp != null)
                    ceilingPart.FNCE0141(swComp, item.Length, item.UVType,item.LightType, item.LightCable, item.MARVEL, item.ANSUL, item.ANSide, item.ANDetectorEnd, item.ANDetectorNo, item.ANDetectorDis1, item.ANDetectorDis2, item.ANDetectorDis3, item.ANDetectorDis4, item.ANDetectorDis5, item.Japan, item.ExRightDis, item.ExLength, item.ExWidth);

                //盲板
                ceilingPart.FCBlind(swModel, swAssy, suffix, item.FCBlindNo, item.FCSideLeft, "FNCE0107[BP-500]{500}-1", "LocalLPattern4", "D1@Distance29", "NO", "");
                ceilingPart.FCBlind(swModel, swAssy, suffix, item.FCBlindNo, item.FCSideLeft, "FNCE0107[BP-500]{500}-5", "LocalLPattern4", "D1@Distance31", "NO", "");

                //UCJ FC
                ceilingPart.UCJFC(swModel,swAssy,suffix,item.FCBlindNo,fcNo,item.FCSideLeft, "UCJ FC COMBI-12", "LocalLPattern3", "D1@Distance35", "NO", "");
                ceilingPart.UCJFC(swModel, swAssy, suffix, item.FCBlindNo, fcNo, item.FCSideLeft, "UCJ FC COMBI-15", "LocalLPattern3", "D1@Distance35", "NO", "");

                //----------油网侧板----------
                ceilingPart.FCFilter(swModel, swAssy, suffix, tree.Module, item.FCSide, "FC", fcNo, item.FCSideLeft, item.FCSideRight, "FNCE0136", 3, "FNCE0109", 3);
                ceilingPart.FCFilter(swModel, swAssy, suffix, tree.Module, item.FCSide, "FC", fcNo, item.FCSideLeft, item.FCSideRight, "FNCE0108", 2, "FNCE0162", 4);

                //----------SSP灯板支撑条----------
                ceilingPart.SSPSupport(swModel, swAssy, suffix, item.Length, "NO", item.SSPType, item.Gutter, item.GutterWidth, "FNCE0035-5", "D1@Distance27", "", "FNCE0036-5", "D1@Distance28", "");
                ceilingPart.SSPSupport(swModel, swAssy, suffix, item.Length, "NO", item.SSPType, item.Gutter, item.GutterWidth, "FNCE0035-6", "D1@Distance36", "", "FNCE0036-4", "D1@Distance37", "");

                //----------UV灯----------
                ceilingPart.SpecialUvRack(swAssy, suffix, item.UVType, "CEILING UVRACK SPECIAL 4S-1", "CEILING UVRACK SPECIAL 4L-1");

                //----------日本项目需要压缩零件----------
                if (item.Japan == "YES")
                {
                    //吊装垫片
                    swAssy.Suppress(suffix, "FNCE0070-10");
                    swAssy.Suppress("LocalLPattern2");
                    //排风脖颈
                    swAssy.Suppress(suffix, "EXSPIGOT-1");
                }
                else
                {
                    //吊装垫片
                    swAssy.UnSuppress(suffix, "FNCE0070-10");
                    swAssy.UnSuppress("LocalLPattern2");

                    //排风脖颈
                    ceilingPart.ExaustSpigot(swAssy, suffix, item.ANSUL, item.MARVEL, item.ExLength, item.ExWidth, item.ExHeight, "EXSPIGOT-1");
                }

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
    }
}
