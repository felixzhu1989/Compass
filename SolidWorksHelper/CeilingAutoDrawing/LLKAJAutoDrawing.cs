using System;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows.Forms;
using Common;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorksHelper.CeilingAutoDrawing;

namespace SolidWorksHelper
{
    public class LLKAJAutoDrawing : IAutoDrawing
    {
        Component2 swComp;
        readonly LLKAJService objLLKAJService = new LLKAJService();
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
            LLKAJ item = (LLKAJ)objLLKAJService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix;//后缀
            //打开Pack后的模型
            var swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            var swAssy = swModel as AssemblyDoc;
            string assyName = swModel.GetTitle().Substring(0, swModel.GetTitle().Length - 7);//获取装配体名称
            var swModelDocExt = (ModelDocExtension)swModel.Extension;
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);
            //TopOnly参数设置成true，只重建顶层，不重建零件内部
            /*注意SolidWorks单位是m，计算是应当/1000d
             * 整形与整形运算得出的结果仍然时整形，1640 / 1000d结果为0，因此必须将其中一个转化成double型，使用后缀m就可以了
             * (int)不进行四舍五入，Convert.ToInt32会四舍五入
            */
            //-----------计算中间值，---------- 
            #endregion

            CeilingPart ceilingPart = new CeilingPart();
            try
            {
                //----------Top Level----------
                swModel.ChangeDim("D1@Distance3", item.Length);
                #region 边缘板
                swComp =  ceilingPart.RenameComp(swModel, swAssy, suffix, "LLKA", $"{tree.Module}.01", "FNCL0026", 2, item.LeftLength,
                    233);
                if (swComp != null)
                    ceilingPart.FNCL0026(swComp, item.LeftLength);

                swComp =  ceilingPart.RenameComp(swModel, swAssy, suffix, "LLKA", $"{tree.Module}.02", "FNCL0027", 1, item.RightLength,
                    233);
                if (swComp != null)
                    ceilingPart.FNCL0026(swComp, item.RightLength);
                #endregion

                #region 长玻璃
                //1长灯
                if (item.LongGlassNo < 1)
                {
                    swAssy.Suppress(suffix, "2200600015-1");
                    swAssy.Suppress(suffix, "2200600003-1");
                }
                else
                {
                    swAssy.UnSuppress(suffix, "2200600015-1");
                    swAssy.UnSuppress(suffix, "2200600003-1");
                }
                //2长灯
                if (item.LongGlassNo <2)
                {
                    swAssy.Suppress(suffix, "2200600015-3");
                    swAssy.Suppress(suffix, "2200600003-4");
                    swAssy.Suppress(suffix, "2200600003-13");
                    if (swModel.Extension.SelectByID2($"{CommonFunc.AddSuffix(suffix, "FNCL0028-1")}@{assyName}", "COMPONENT", 0, 0, 0, false, 0, null, 0))
                        swAssy.Suppress(suffix, "FNCL0028-1");
                }
                else
                {
                    swAssy.UnSuppress(suffix, "2200600015-3");
                    swAssy.UnSuppress(suffix, "2200600003-4");
                    swAssy.UnSuppress(suffix, "2200600003-13");
                    if (swModel.Extension.SelectByID2($"{CommonFunc.AddSuffix(suffix, "FNCL0028-1")}@{assyName}", "COMPONENT", 0, 0, 0, false, 0, null, 0))
                        swAssy.UnSuppress(suffix, "FNCL0028-1");
                }
                //阵列
                if (item.LongGlassNo < 3)
                {
                    swAssy.Suppress("LocalLPattern1");
                }
                else
                {
                    swAssy.UnSuppress("LocalLPattern1");
                    swModel.ChangeDim("D3@LocalLPattern1", 1212d+30d+item.MidLength);
                    swModel.ChangeDim("D1@LocalLPattern1", item.LongGlassNo-1);
                }
                #endregion

                #region 短灯
                if (item.ShortGlassNo == 0 ||(item.ShortGlassNo!=0&&item.LongGlassNo < 1))
                {
                    swAssy.Suppress(suffix, "2200600003-16");
                    if (swModel.Extension.SelectByID2($"{CommonFunc.AddSuffix(suffix, "FNCL0028-7")}@{assyName}", "COMPONENT", 0, 0, 0, false, 0, null, 0))
                        swAssy.Suppress(suffix, "FNCL0028-7");
                }
                else
                {
                    swAssy.UnSuppress(suffix, "2200600003-16");
                    if (swModel.Extension.SelectByID2($"{CommonFunc.AddSuffix(suffix, "FNCL0028-7")}@{assyName}", "COMPONENT", 0, 0, 0, false, 0, null, 0))
                        swAssy.UnSuppress(suffix, "FNCL0028-7");
                }
                if (item.ShortGlassNo == 0)
                {
                    swAssy.Suppress(suffix, "2200600032-1");
                    swAssy.Suppress(suffix, "2200600003-15");
                }
                else
                {
                    swAssy.UnSuppress(suffix, "2200600032-1");
                    swAssy.UnSuppress(suffix, "2200600003-15");
                }
                #endregion

                #region 重命名中间板

                if (item.LongGlassNo > 1)
                {
                    swComp =  ceilingPart.RenameComp(swModel, swAssy, suffix, "LLKA", "MID", "FNCL0028", 1, item.MidLength,
                        233);
                    if (swComp != null)
                        ceilingPart.FNCL0026(swComp, item.MidLength);
                }

                if (item.ShortGlassNo == 0 ||(item.ShortGlassNo!=0&&item.LongGlassNo < 1))
                {
                }
                else
                {
                    swComp =  ceilingPart.RenameComp(swModel, swAssy, suffix, "LLKA", "MID", "FNCL0028", 7, item.MidLength,
                        233);
                    if (swComp != null)
                        ceilingPart.FNCL0026(swComp, item.MidLength);
                }


                #endregion

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
