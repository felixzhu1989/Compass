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
    public class KCJDB800AutoDrawing : IAutoDrawing
    {
        readonly KCJDB800Service objKCJDB800Service = new KCJDB800Service();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            #region 准备工作
            //创建项目模型存放地址
            string itemPath = $@"{projectPath}\{tree.Module}-{tree.CategoryName}";
            if (!Directory.Exists(itemPath))
            {
                Directory.CreateDirectory(itemPath);
            }
            else
            {
                ShowMsg show = new ShowMsg();
                DialogResult result = show.ShowMessageBoxTimeout($"模型文件夹{itemPath}存在，如果之前pack已经执行过，将不执行pack过程而是直接修改模型，如果要中断作图点击YES，继续作图请点击No或者3s后窗口会自动消失", "提示信息", MessageBoxButtons.YesNo, 3000);
                if (result == DialogResult.Yes) return;
            }
            //Pack的后缀
            string suffix = $"{tree.Module}-{tree.ODPNo.Substring(tree.ODPNo.Length - 6)}";
            //判断文件是否存在，如果存在将不执行pack，如果不存在则执行pack
            //packango后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = $@"{itemPath}\{tree.CategoryName.ToLower()}_{suffix}.sldasm";
            if (!File.Exists(packedAssyPath)) packedAssyPath = CommonFunc.PackAndGoFunc(suffix, swApp, tree.ModelPath, itemPath);
            //查询参数
            KCJDB800 item = (KCJDB800)objKCJDB800Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix;//后缀
            //打开Pack后的模型
            var swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            var swAssy = swModel as AssemblyDoc;
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);
            //TopOnly参数设置成true，只重建顶层，不重建零件内部 
            #endregion

            //-----------计算中间值，----------
            if (item.FCSide == "RIGHT" || item.FCSide == "NO") item.FCSideLeft = 0.5d;//过滤掉填错的情况
            int fcNo = (int)((item.Length - item.FCSideLeft - item.FCSideRight) / 499d) - item.FCBlindNo;

            CeilingPart ceilingPart = new CeilingPart();
            try
            {
                Component2 swComp;
                //判断是否是HCL特殊灯腔
                if (item.LightType == "HCL")
                {
                    #region 压缩/解压
                    //NOHCL压缩
                    swAssy.Suppress(suffix, "FNCE0116-1");//灯腔
                    swAssy.Suppress(suffix, "FNCE0056-1");
                    swAssy.Suppress(suffix, "FNCE0056-2");
                    swAssy.Suppress(suffix, "FNCE0114-3");
                    swAssy.Suppress(suffix, "FNCE0114-2");
                    //HCL解压
                    //swAssy.UnSuppress(suffix, "FNCE0087-1");
                    //swAssy.UnSuppress(suffix, "FNCE0090-1");
                    //swAssy.UnSuppress(suffix, "FNCE0090-2");
                    //swAssy.UnSuppress(suffix, "FNCE0091-1");
                    //swAssy.UnSuppress(suffix, "FNCE0091-2");
                    //swAssy.UnSuppress(suffix, "FNCE0092-1");
                    //swAssy.UnSuppress(suffix, "FNCE0094-1");
                    //swAssy.UnSuppress(suffix, "FNCE0093-1");
                    swAssy.UnSuppress(suffix, "HCL-1000-1");
                    //三角板
                    swAssy.UnSuppress(suffix, "FNCE0088-2");
                    swAssy.UnSuppress(suffix, "FNCE0088-3");
                    #endregion

                    #region 顶层
                    //先解压缩出来零件再改顶层装配关系
                    swModel.ChangeDim("D1@Distance48", item.LightPanelLeft);
                    swModel.ChangeDim("D1@Distance49", item.LightPanelLeft);

                    #region 镀锌片数量
                    if (item.LightPanelSide == "BOTH")
                    {
                        swAssy.UnSuppress(suffix, "FNCE0093-1");
                        swModel.ChangeDim("D1@LocalLPattern8", 8);
                    }
                    else if (item.LightPanelSide == "RIGHT" || item.LightPanelSide == "LEFT")
                    {
                        swAssy.UnSuppress(suffix, "FNCE0093-1");
                        swModel.ChangeDim("D1@LocalLPattern8", 4);
                    }
                    else
                    {
                        swAssy.Suppress("LocalLPattern8");
                        swAssy.Suppress(suffix, "FNCE0093-1");
                    }
                    #endregion
                    #endregion
                    
                    //-------KCJDB800HCL排风腔内灯腔----------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0087-1");
                    ceilingPart.FNCE0087(swComp, item.Length, item.LightCable, item.LightType, item.Japan);
                    
                    //-------灯腔玻璃支架底部-------
                    swAssy.UnSuppress(suffix, "FNCE0090-1");
                    swComp = swAssy.UnSuppress(suffix, "FNCE0090-2");
                    ceilingPart.FNCE0090(swComp, item.Length, item.LightPanelSide, item.LightPanelLeft, item.LightPanelRight);

                    //-------灯腔玻璃支架支撑条上部-------
                    swAssy.UnSuppress(suffix, "FNCE0091-1");
                    swComp = swAssy.UnSuppress(suffix, "FNCE0091-2");
                    ceilingPart.FNCE0091(swComp, item.Length, item.LightPanelLeft, item.LightPanelRight);

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
                    //三角板
                    swAssy.UnSuppress(suffix, "FNCE0114-3");
                    swAssy.UnSuppress(suffix, "FNCE0114-2");
                    //HCL解压
                    swAssy.Suppress(suffix, "FNCE0087-1");
                    swAssy.Suppress(suffix, "FNCE0090-1");
                    swAssy.Suppress(suffix, "FNCE0090-2");
                    swAssy.Suppress(suffix, "FNCE0091-1");
                    swAssy.Suppress(suffix, "FNCE0091-2");
                    swAssy.Suppress(suffix, "FNCE0092-1");
                    swAssy.Suppress(suffix, "FNCE0094-1");
                    swAssy.Suppress(suffix, "FNCE0093-1");
                    swAssy.Suppress(suffix, "HCL-1000-1");
                    //三角板
                    swAssy.Suppress(suffix, "FNCE0088-2");
                    swAssy.Suppress(suffix, "FNCE0088-3");
                    swAssy.Suppress("LocalLPattern8");
                    #endregion

                    //----------灯腔----------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0116-1");
                    ceilingPart.FNCE0116(swComp, item.Length, item.LightCable, item.LightType, item.Japan);

                    swAssy.UnSuppress(suffix, "FNCE0056-1");
                    swComp = swAssy.UnSuppress(suffix, "FNCE0056-2");
                    ceilingPart.FNCE0056(swComp, item.Length);
                }

                //排风腔
                swComp = ceilingPart.RenameComp(swModel, swAssy, suffix, "KCJDB800", tree.Module, "FNCE0115", 1, item.Length, 800);
                if (swComp != null)
                    ceilingPart.FNCE0115(swComp, item.Length, item.LightType,item.LightCable, item.MARVEL, item.ANSUL, item.ANSide, item.ANDetectorEnd,item.ANDetectorNo,item.ANDetectorDis1,item.ANDetectorDis2,item.ANDetectorDis3,item.ANDetectorDis4,item.ANDetectorDis5, item.Japan, item.ExRightDis, item.ExLength, item.ExWidth);

                //----------公共零件----------
                //盲板
                ceilingPart.FCBlind(swModel, swAssy, suffix, item.FCBlindNo, item.FCSideLeft, "FNCE0107[BP-500]{500}-3", "LocalLPattern4", "D1@Distance31", "NO", "");
                ceilingPart.FCBlind(swModel, swAssy, suffix, item.FCBlindNo, item.FCSideLeft, "FNCE0107[BP-500]{500}-7", "LocalLPattern4", "D1@Distance31", "NO", "");
                
                //FC或者KSA
                ceilingPart.KSAorFC(swModel, swAssy, suffix, item.FCBlindNo, item.FCType, fcNo, item.FCSideLeft, "5202040401-1", "LocalLPattern5", "D1@Distance34", "KCJ FC FILTER-1", "LocalLPattern3", "D1@Distance33", "NO", "");
                ceilingPart.KSAorFC(swModel, swAssy, suffix, item.FCBlindNo, item.FCType, fcNo, item.FCSideLeft, "5202040401-7", "LocalLPattern5", "D1@Distance34", "KCJ FC FILTER-9", "LocalLPattern3", "D1@Distance33", "NO", "");
                
                //----------油网侧板----------
                ceilingPart.FCFilter(swModel, swAssy, suffix, tree.Module, item.FCSide, item.FCType, fcNo, item.FCSideLeft, item.FCSideRight, "FNCE0108", 3, "FNCE0109", 3);
                ceilingPart.FCFilter(swModel, swAssy, suffix, tree.Module, item.FCSide, item.FCType, fcNo, item.FCSideLeft, item.FCSideRight, "FNCE0108", 4, "FNCE0109", 4);
                
                //----------SSP灯板支撑条----------
                ceilingPart.SSPSupport(swModel, swAssy, suffix, item.Length, "NO", item.SSPType, item.Gutter, item.GutterWidth, "FNCE0035-7", "D1@Distance27", "", "FNCE0036-5", "D1@Distance28", "");
                ceilingPart.SSPSupport(swModel, swAssy, suffix, item.Length, "NO", item.SSPType, item.Gutter, item.GutterWidth, "FNCE0035-6", "D1@Distance36", "", "FNCE0036-4", "D1@Distance37", "");

                //----------日本项目需要压缩零件----------
                if (item.Japan == "YES")
                {
                    //吊装垫片
                    swAssy.Suppress(suffix, "FNCE0070-9");
                    swAssy.Suppress("LocalLPattern1");
                    //排风脖颈
                    swAssy.Suppress(suffix, "EXSPIGOT-1");
                    //排风滑门
                    swAssy.Suppress(suffix, "EXDOOR-1");
                }
                else
                {
                    //吊装垫片
                    swAssy.UnSuppress(suffix, "FNCE0070-9");
                    swAssy.UnSuppress("LocalLPattern1");

                    //排风脖颈
                    ceilingPart.ExaustSpigot(swAssy, suffix, item.ANSUL, item.MARVEL, item.ExLength, item.ExWidth, item.ExHeight, "EXSPIGOT-1");

                    //排风滑门
                    ceilingPart.ExaustRail(swAssy, suffix, item.MARVEL, item.ExLength, item.ExWidth, 1, 0, "EXDOOR-1");
                }
                
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
