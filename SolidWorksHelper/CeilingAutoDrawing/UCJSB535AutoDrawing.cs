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
    public class UCJSB535AutoDrawing : IAutoDrawing
    {
        Component2 swComp; readonly UCJSB535Service objUCJSB535Service = new UCJSB535Service();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            //packandgo后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = swApp.PackAndGoCeiling(tree, projectPath, out string suffix);

            //查询参数
            UCJSB535 item = (UCJSB535)objUCJSB535Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

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
            if (item.FCSide == "RIGHT" || item.FCSide == "NO") item.FCSideLeft = 0.5d;//过滤掉填错的情况
            int fcNo = (int)((item.Length - item.FCSideLeft - item.FCSideRight) / 499d) - item.FCBlindNo;
            
            try
            {
                //判断是否是HCL特殊灯腔
                if (item.LightType == "HCL")
                {
                    #region 压缩/解压
                    //NOHCL压缩
                    swAssy.Suppress(suffix, "FNCE0159-2");//排风腔
                    swAssy.Suppress(suffix, "FNCE0112-1");
                    swAssy.Suppress(suffix, "FNCE0056-1");
                    swAssy.Suppress(suffix, "FNCE0145-1");//磁棒板

                    swAssy.Suppress(suffix, "FNCE0110-5");
                    swAssy.Suppress(suffix, "FNCE0110-6");
                    swAssy.Suppress(suffix, "FNCE0100-1");//侧板磁铁支架

                    //HCL解压
                    //swAssy.UnSuppress(suffix, "FNCE0102-1");
                    //swAssy.UnSuppress(suffix, "FNCE0067-1");
                    //swAssy.UnSuppress(suffix, "FNCE0069-1");
                    //swAssy.UnSuppress(suffix, "FNCE0090-1");
                    //swAssy.UnSuppress(suffix, "FNCE0093-1");
                    //swAssy.UnSuppress(suffix, "FNCE0092-1");
                    //swAssy.UnSuppress(suffix, "FNCE0094-1");
                    //swAssy.UnSuppress(suffix, "FNCE0091-1");
                    //swAssy.UnSuppress(suffix, "FNCE0091-2");
                    swAssy.UnSuppress(suffix, "HCL-1000-1");
                    swAssy.UnSuppress(suffix, "FNCE0101-1");
                    //三角板
                    swAssy.UnSuppress(suffix, "FNCE0068-2");
                    swAssy.UnSuppress(suffix, "FNCE0068-1");
                    #endregion

                    #region 顶层
                    //先解压缩出来零件再改顶层装配关系
                    swModel.ChangeDim("D1@Distance62", item.LightPanelLeft);
                    swModel.ChangeDim("D1@Distance63", item.LightPanelLeft);

                    #region 镀锌片数量
                    if (item.LightPanelSide == "BOTH")
                    {
                        swAssy.UnSuppress(suffix, "FNCE0093-1");
                        swModel.ChangeDim("D1@LocalLPattern5", 8);
                    }
                    else if (item.LightPanelSide == "RIGHT" || item.LightPanelSide == "LEFT")
                    {
                        swAssy.UnSuppress(suffix, "FNCE0093-1");
                        swModel.ChangeDim("D1@LocalLPattern5", 4);
                    }
                    else
                    {
                        swAssy.Suppress("LocalLPattern5");
                        swAssy.Suppress(suffix, "FNCE0093-1");
                    }
                    #endregion
                    #endregion

                    //-------HCL排风腔-------
                    swComp = ceilingPart.RenameComp(swModel, swAssy, suffix, "UCJSB535", tree.Module, "FNCE0102", 1, item.Length, 535);
                    if (swComp != null)
                        ceilingPart.FNCE0102(swComp, item.Length, item.UVType,item.LightCable, item.MARVEL, item.ANSUL, item.ANSide, item.ANDetector, item.LightPanelSide, item.LightPanelLeft, item.LightPanelRight, item.Japan, item.ExRightDis, item.ExLength, item.ExWidth);

                    //-------HCL排风腔内灯腔----------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0067-1");
                    ceilingPart.FNCE0067(swComp, item.Length, item.LightCable, item.LightType, item.Japan);

                    //-------HCL磁棒板----------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0069-1");
                    ceilingPart.FNCE0069(swComp,item.Length,item.FCSide,fcNo,item.FCBlindNo,item.FCSideLeft);
                    
                    //-------灯腔玻璃支架底部-------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0090-1");
                    ceilingPart.FNCE0090(swComp, item.Length, item.LightPanelSide, item.LightPanelLeft, item.LightPanelRight);

                    //-------灯腔玻璃支架支撑条上部-------
                    swAssy.UnSuppress(suffix, "FNCE0091-2");
                    swComp = swAssy.UnSuppress(suffix, "FNCE0091-1");
                    ceilingPart.FNCE0091(swComp, item.Length, item.LightPanelSide, item.LightPanelLeft, item.LightPanelRight);

                    //-------HCL灯腔侧板,重命名-------
                    ceilingPart.HCLSidePanel(swModel, swAssy, suffix, tree.Module, item.LightPanelSide, item.LightPanelLeft, item.LightPanelRight, "FNCE0092", 1, "FNCE0094", 1);
                }
                else
                {
                    #region 压缩/解压
                    //NOHCL压缩
                    //swAssy.UnSuppress(suffix, "FNCE0159-2");//排风腔
                    //swAssy.UnSuppress(suffix, "FNCE0112-1");
                    //swAssy.UnSuppress(suffix, "FNCE0056-1");
                    //swAssy.UnSuppress(suffix, "FNCE0145-1");//磁棒板

                    swAssy.UnSuppress(suffix, "FNCE0110-5");
                    swAssy.UnSuppress(suffix, "FNCE0110-6");
                    swAssy.UnSuppress(suffix, "FNCE0100-1");//侧板磁铁支架

                    //HCL解压
                    swAssy.Suppress(suffix, "FNCE0067-1");
                    swAssy.Suppress(suffix, "FNCE0069-1");
                    swAssy.Suppress(suffix, "FNCE0101-1");
                    swAssy.Suppress(suffix, "FNCE0102-1");
                    swAssy.Suppress(suffix, "FNCE0090-1");
                    swAssy.Suppress(suffix, "FNCE0091-1");
                    swAssy.Suppress(suffix, "FNCE0091-2");
                    swAssy.Suppress(suffix, "FNCE0093-1");
                    swAssy.Suppress(suffix, "FNCE0092-1");
                    swAssy.Suppress(suffix, "FNCE0094-1");
                    swAssy.Suppress(suffix, "HCL-1000-1");
                    //三角板
                    swAssy.Suppress(suffix, "FNCE0068-2");
                    swAssy.Suppress(suffix, "FNCE0068-1");
                    swAssy.Suppress("LocalLPattern5");
                    #endregion
                    //-------UCJ535排风腔-------
                    swComp = ceilingPart.RenameComp(swModel, swAssy, suffix, "UCJSB535", tree.Module, "FNCE0159", 2, item.Length, 535);
                    if (swComp != null)
                        ceilingPart.FNCE0159(swComp, item.Length, item.UVType, item.LightCable, item.MARVEL, item.ANSUL, item.ANSide, item.ANDetector,  item.Japan, item.ExRightDis, item.ExLength, item.ExWidth);
                    //----------灯腔----------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0112-1");
                    ceilingPart.FNCE0112(swComp, item.Length, item.LightCable, item.LightType, item.Japan);

                    //-------磁棒板----------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0145-1");
                    ceilingPart.FNCE0145(swComp, item.Length,item.FCSide, fcNo, item.FCBlindNo, item.FCSideLeft);

                    swComp = swAssy.UnSuppress(suffix, "FNCE0056-1");
                    ceilingPart.FNCE0056(swComp, item.Length);

                }
                //----------公共零件----------
                //盲板   HCL时为D1@Distance66
                ceilingPart.FCBlind(swModel, swAssy, suffix, item.FCBlindNo, item.FCSideLeft, "FNCE0107[BP-500]{500}-3", "LocalLPattern3", "D1@Distance35", item.LightType, "D1@Distance66");

                //FC或者KSA，HCL时为D1@Distance57
                ceilingPart.UCJFC(swModel, swAssy, suffix, item.FCBlindNo, fcNo, item.FCSideLeft,"UCJ FC COMBI-1", "LocalLPattern4", "D1@Distance53", item.LightType, "D1@Distance57");

                //----------油网侧板----------
                ceilingPart.FCFilter(swModel, swAssy, suffix, tree.Module, item.FCSide, "FC", fcNo, item.FCSideLeft, item.FCSideRight, "FNCE0136", 1, "FNCE0109", 1);


                //----------SSP灯板支撑条----------
                ceilingPart.SSPSupport(swModel, swAssy, suffix, item.Length, item.LightType, item.SSPType, item.Gutter, item.GutterWidth, "FNCE0035-1", "D1@Distance42", "D1@Distance58", "FNCE0036-1", "D1@Distance51", "D1@Distance60");

                //----------特殊UV灯架----------
                ceilingPart.SpecialUvRack(swAssy,suffix,item.UVType, "CEILING UVRACK SPECIAL 4S-1", "CEILING UVRACK SPECIAL 4L-1");

                //----------日本项目需要压缩零件----------
                if (item.Japan == "YES")
                {
                    //吊装垫片
                    swAssy.Suppress(suffix, "FNCE0070-18");
                    swAssy.Suppress("LocalLPattern2");
                    //排风脖颈
                    swAssy.Suppress(suffix, "EXSPIGOT-1");
                }
                else
                {
                    //吊装垫片
                    swAssy.UnSuppress(suffix, "FNCE0070-18");
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
