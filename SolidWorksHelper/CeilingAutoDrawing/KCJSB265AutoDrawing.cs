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
    public class KCJSB265AutoDrawing : IAutoDrawing
    {
        readonly KCJSB265Service objKCJSB265Service = new KCJSB265Service();
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
            KCJSB265 item = (KCJSB265)objKCJSB265Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

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
                //----------Top Level----------
                //排风腔
                var swComp = ceilingPart.RenameComp(swModel, swAssy, suffix, "KCJSB265", tree.Module, "FNCE0125", 2, item.Length, 265);
                if (swComp != null)
                    ceilingPart.FNCE0125(swComp, item.Length, item.MARVEL, item.ANSUL, item.ANSide, item.ANDetector, item.Japan, item.ExRightDis, item.ExLength, item.ExWidth);

                //盲板
                ceilingPart.FCBlind(swModel, swAssy, suffix, item.FCBlindNo, item.FCSideLeft, "FNCE0107[BP-500]{500}-1", "LocalLPattern3", "D1@Distance11", "NO", "");

                //FC或者KSA
                ceilingPart.KSAorFC(swModel, swAssy, suffix, item.FCBlindNo, item.FCType, fcNo, item.FCSideLeft, "5202040401-1", "LocalLPattern4", "D1@Distance9", "KCJ FC FILTER-1", "LocalLPattern2", "D1@Distance10", "NO", "");

                //----------油网侧板----------
                ceilingPart.FCFilter(swModel, swAssy, suffix, tree.Module, item.FCSide, item.FCType, fcNo, item.FCSideLeft, item.FCSideRight, "FNCE0108", 1, "FNCE0109", 1);

                //----------SSP灯板支撑条----------
                ceilingPart.SSPSupport(swModel, swAssy, suffix, item.Length, "NO", item.SSPType, item.Gutter, item.GutterWidth, "FNCE0035-1", "D1@Distance12", "", "FNCE0036-1", "D1@Distance2", "");


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
