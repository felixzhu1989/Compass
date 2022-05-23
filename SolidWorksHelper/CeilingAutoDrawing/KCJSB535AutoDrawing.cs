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
    public class KCJSB535AutoDrawing : IAutoDrawing
    {
        Component2 swComp; 
        readonly KCJSB535Service objKCJSB535Service = new KCJSB535Service();
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
            string suffix =$"{tree.Module}-{tree.ODPNo.Substring(tree.ODPNo.Length - 6)}";
            //判断文件是否存在，如果存在将不执行pack，如果不存在则执行pack
            //packango后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = $@"{itemPath}\{tree.CategoryName.ToLower()}_{suffix}.sldasm";
            if (!File.Exists(packedAssyPath)) packedAssyPath = CommonFunc.PackAndGoFunc(suffix, swApp, tree.ModelPath, itemPath);
            //查询参数
            KCJSB535 item = (KCJSB535)objKCJSB535Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
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
                
                //判断是否是HCL特殊灯腔
                if (item.LightType == "HCL")
                {
                    #region 压缩/解压
                    //NOHCL压缩
                    swAssy.Suppress(suffix, "FNCE0111-2");
                    swAssy.Suppress(suffix, "FNCE0112-1");
                    swAssy.Suppress(suffix, "FNCE0056-1");
                    swAssy.Suppress(suffix, "FNCE0110-3");
                    swAssy.Suppress(suffix, "FNCE0110-4");
                    //HCL解压
                    //swAssy.UnSuppress(suffix, "FNCE0089-1");
                    //swAssy.UnSuppress(suffix, "FNCE0085-1");
                    //swAssy.UnSuppress(suffix, "FNCE0090-1");
                    //swAssy.UnSuppress(suffix, "FNCE0091-1");
                    //swAssy.UnSuppress(suffix, "FNCE0091-3");
                    //swAssy.UnSuppress(suffix, "FNCE0093-1");
                    //swAssy.UnSuppress(suffix, "FNCE0092-1");
                    //swAssy.UnSuppress(suffix, "FNCE0094-1");
                    swAssy.UnSuppress(suffix, "HCL-1000-1");
                    //三角板
                    swAssy.UnSuppress(suffix, "FNCE0086-2");
                    swAssy.UnSuppress(suffix, "FNCE0086-1");
                    #endregion

                    #region 顶层
                    //先解压缩出来零件再改顶层装配关系
                    swModel.ChangeDim("D1@Distance41", item.LightPanelLeft);
                    swModel.ChangeDim("D1@Distance42", item.LightPanelLeft);

                    #region 镀锌片数量
                    if (item.LightPanelSide == "BOTH")
                    {
                        swAssy.UnSuppress(suffix, "FNCE0093-1");
                        swModel.ChangeDim("D1@LocalLPattern6", 8);
                    }
                    else if (item.LightPanelSide == "RIGHT" || item.LightPanelSide == "LEFT")
                    {
                        swAssy.UnSuppress(suffix, "FNCE0093-1");
                        swModel.ChangeDim("D1@LocalLPattern6", 4);
                    }
                    else
                    {
                        swAssy.Suppress("LocalLPattern6");
                        swAssy.Suppress(suffix, "FNCE0093-1");
                    }
                    #endregion
                    #endregion

                    //-------HCL排风腔-------
                    swComp = ceilingPart.RenameComp(swModel, swAssy, suffix, "KCJSB535", tree.Module, "FNCE0089", 1, item.Length, 535);
                    if (swComp != null)
                        ceilingPart.FNCE0089(swComp, item.Length, item.LightCable, item.MARVEL, item.ANSUL, item.ANSide, item.ANDetector, item.LightPanelSide, item.LightPanelLeft, item.LightPanelRight, item.Japan, item.ExRightDis, item.ExLength, item.ExWidth);

                    //-------HCL排风腔内灯腔----------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0085-1");
                    ceilingPart.FNCE0085(swComp, item.Length, item.LightCable, item.LightType, item.Japan);


                    //-------灯腔玻璃支架底部-------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0090-1");
                    ceilingPart.FNCE0090(swComp, item.Length, item.LightPanelSide, item.LightPanelLeft, item.LightPanelRight);

                    //-------灯腔玻璃支架支撑条上部-------
                    swAssy.UnSuppress(suffix, "FNCE0091-3");
                    swComp = swAssy.UnSuppress(suffix, "FNCE0091-1");
                    ceilingPart.FNCE0091(swComp, item.Length, item.LightPanelLeft, item.LightPanelRight);

                    //-------HCL灯腔侧板,重命名-------
                    ceilingPart.HCLSidePanel(swModel, swAssy, suffix, tree.Module, item.LightPanelSide, item.LightPanelLeft, item.LightPanelRight, "FNCE0092", 1, "FNCE0094", 1);

                }
                else
                {
                    #region 压缩/解压
                    //NOHCL
                    //swAssy.UnSuppress(suffix, "FNCE0111-2");
                    //swAssy.UnSuppress(suffix, "FNCE0112-1");
                    //swAssy.UnSuppress(suffix, "FNCE0056-1");
                    //三角板
                    swAssy.UnSuppress(suffix, "FNCE0110-3");
                    swAssy.UnSuppress(suffix, "FNCE0110-4");
                    //HCL
                    swAssy.Suppress(suffix, "FNCE0089-1");
                    swAssy.Suppress(suffix, "FNCE0085-1");
                    swAssy.Suppress(suffix, "FNCE0090-1");
                    swAssy.Suppress(suffix, "FNCE0091-1");
                    swAssy.Suppress(suffix, "FNCE0091-3");
                    swAssy.Suppress(suffix, "FNCE0093-1");
                    swAssy.Suppress(suffix, "FNCE0092-1");
                    swAssy.Suppress(suffix, "FNCE0094-1");
                    swAssy.Suppress(suffix, "HCL-1000-1");
                    swAssy.Suppress(suffix, "FNCE0086-2");
                    swAssy.Suppress(suffix, "FNCE0086-1");
                    swAssy.Suppress("LocalLPattern6");
                    #endregion

                    //排风腔
                    swComp = ceilingPart.RenameComp(swModel, swAssy, suffix, "KCJSB535", tree.Module, "FNCE0111", 2, item.Length, 535);
                    if (swComp != null)
                        ceilingPart.FNCE0111(swComp, item.Length, item.LightCable, item.MARVEL, item.ANSUL, item.ANSide, item.ANDetector, item.Japan, item.ExRightDis, item.ExLength, item.ExWidth);

                    //----------灯腔----------
                    swComp = swAssy.UnSuppress(suffix, "FNCE0112-1");
                    ceilingPart.FNCE0112(swComp, item.Length, item.LightCable, item.LightType, item.Japan);

                    swComp = swAssy.UnSuppress(suffix, "FNCE0056-1");
                    ceilingPart.FNCE0056(swComp, item.Length);
                }

                //----------公共零件----------
                //盲板
                ceilingPart.FCBlind(swModel, swAssy, suffix, item.FCBlindNo, item.FCSideLeft, "FNCE0107[BP-500]{500}-1", "LocalLPattern4", "D1@Distance35", item.LightType, "D1@Distance45");

                //FC或者KSA
                ceilingPart.KSAorFC(swModel, swAssy, suffix, item.FCBlindNo, item.FCType, fcNo, item.FCSideLeft,  "5202040401-1", "LocalLPattern5", "D1@Distance37", "KCJ FC FILTER-1", "LocalLPattern3", "D1@Distance36",item.LightType, "D1@Distance43");


                //----------油网侧板----------
                ceilingPart.FCFilter(swModel, swAssy, suffix, tree.Module, item.FCSide, item.FCType, fcNo, item.FCSideLeft, item.FCSideRight, "FNCE0108", 1, "FNCE0109", 1);


                //----------SSP灯板支撑条----------
                ceilingPart.SSPSupport(swModel, swAssy, suffix, item.Length, item.LightType,item.SSPType, item.Gutter, item.GutterWidth, "FNCE0035-1", "D1@Distance34", "D1@Distance48", "FNCE0036-1", "D1@Distance32","D1@Distance50");

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
                    ceilingPart.ExaustSpigot(swAssy,suffix,item.ANSUL,item.MARVEL,item.ExLength,item.ExWidth,item.ExHeight, "EXSPIGOT-1");

                    //排风滑门
                    ceilingPart.ExaustRail(swAssy,suffix,item.MARVEL,item.ExLength,item.ExWidth,1,0, "EXDOOR-1");

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
