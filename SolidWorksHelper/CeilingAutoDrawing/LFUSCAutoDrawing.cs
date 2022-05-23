using System;
using System.IO;
using System.Windows.Forms;
using Common;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class LFUSCAutoDrawing : IAutoDrawing
    {
        Component2 swComp; readonly LFUSCService objLFUSCService = new LFUSCService();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            //创建项目模型存放地址
            string itemPath = projectPath + @"\" + tree.Module + "-" + tree.CategoryName;
            if (!Directory.Exists(itemPath))
            {
                Directory.CreateDirectory(itemPath);
            }
            else
            {
                ShowMsg show = new ShowMsg();
                DialogResult result = show.ShowMessageBoxTimeout("模型文件夹" + itemPath + "存在，如果之前pack已经执行过，将不执行pack过程而是直接修改模型，如果要中断作图点击YES，继续作图请点击No或者3s后窗口会自动消失", "提示信息", MessageBoxButtons.YesNo, 3000);
                if (result == DialogResult.Yes) return;
            }
            //Pack的后缀
            string suffix = tree.Module + "-" + tree.ODPNo.Substring(tree.ODPNo.Length - 6);
            //判断文件是否存在，如果存在将不执行pack，如果不存在则执行pack
            //packango后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = itemPath + @"\" + tree.CategoryName.ToLower() + "_" + suffix + ".sldasm";
            if (!File.Exists(packedAssyPath)) packedAssyPath = CommonFunc.PackAndGoFunc(suffix, swApp, tree.ModelPath, itemPath);

            //查询参数
            LFUSC item = (LFUSC)objLFUSCService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix;//后缀
            ModelDoc2 swModel;
            ModelDoc2 swPart;
            AssemblyDoc swAssy;
            
            Feature swFeat;
            
            ModelDocExtension swModelDocExt;
            bool status;
            string compReName;
            //打开Pack后的模型
            swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
            swAssy = swModel as AssemblyDoc;//装配体
            string assyName = swModel.GetTitle().Substring(0, swModel.GetTitle().Length - 7);//获取装配体名称
            swModelDocExt = (ModelDocExtension)swModel.Extension;
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);
            //TopOnly参数设置成true，只重建顶层，不重建零件内部
            /*注意SolidWorks单位是m，计算是应当/1000d
             * 整形与整形运算得出的结果仍然时整形，1640 / 1000d结果为0，因此必须将其中一个转化成double型，使用后缀m就可以了
             * (int)不进行四舍五入，Convert.ToInt32会四舍五入
            */
            //-----------计算中间值，----------


            try
            {
                //----------Top Level----------
                if (item.Japan == "YES")
                {
                    //重命名装配体内部
                    compReName = "FNCA0012[LFUSC-" + tree.Module + "]{" + (int)item.Length + "}";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCA0012-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length + 17d) / 1000d;
                        swPart.Parameter("D3@Sketch2").SystemValue = item.SuDia / 1000d;
                        swPart.Parameter("D1@Sketch2").SystemValue =
                            (item.SuDis * (item.SuNo / 2d - 1d) + item.SuDis / 2d) / 1000d;
                        if (item.SuNo < 2)
                        {
                            swFeat = swComp.FeatureByName("LPattern2");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("LPattern2");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D1@LPattern2").SystemValue = item.SuNo;
                            swPart.Parameter("D3@LPattern2").SystemValue = item.SuDis / 1000d;
                        }
                    }
                    //重命名装配体内部
                    compReName = "FNCA0014[LFUP-" + tree.Module + "]{" + (int)item.Length + "}(500)";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCA0014[LFUP-]{}(500)-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D2@Skizze2").SystemValue = item.Length / 1000d;
                    }
                }
                else
                {
                    //重命名装配体内部
                    compReName = "FNCA0012[LFUSC-" + tree.Module + "]{" + (int)item.Length + "}";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCA0012-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D2@Base-Flange1").SystemValue = item.Length / 1000d;
                        swPart.Parameter("D3@Sketch2").SystemValue = item.SuDia / 1000d;
                        swPart.Parameter("D1@Sketch2").SystemValue =
                            (item.SuDis * (item.SuNo / 2d - 1d) + item.SuDis / 2d) / 1000d;
                        if (item.SuNo < 2)
                        {
                            swFeat = swComp.FeatureByName("LPattern2");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("LPattern2");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D1@LPattern2").SystemValue = item.SuNo;
                            swPart.Parameter("D3@LPattern2").SystemValue = item.SuDis / 1000d;
                        }
                    }
                    //重命名装配体内部
                    compReName = "FNCA0014[LFUP-" + tree.Module + "]{" + (int)(item.Length-10) + "}(500)";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCA0014[LFUP-]{}(500)-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D2@Skizze2").SystemValue = (item.Length-10d) / 1000d;
                    }
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
