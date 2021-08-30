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
    public class ANAutoDrawing : IAutoDrawing
    {
        ANService objANService = new ANService();
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
                Common.ShowMsg show = new ShowMsg();
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
            AN item = (AN)objANService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix;//后缀
            ModelDoc2 swModel = default(ModelDoc2);
            ModelDoc2 swPart = default(ModelDoc2);
            AssemblyDoc swAssy = default(AssemblyDoc);
            Component2 swComp;
            Feature swFeat = default(Feature);
            object configNames = null;
            ModelDocExtension swModelDocExt = default(ModelDocExtension);
            bool status = false;
            string compReName = string.Empty;
            //打开Pack后的模型
            swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
            swAssy = swModel as AssemblyDoc;//装配体
            string assyName = swModel.GetTitle().Substring(0, swModel.GetTitle().Length - 7);//获取装配体名称
            swModelDocExt = (ModelDocExtension)swModel.Extension;
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);
            //TopOnly参数设置成true，只重建顶层，不重建零件内部
            /*注意SolidWorks单位是m，计算是应当/1000m
             * 整形与整形运算得出的结果仍然时整形，1640 / 1000m结果为0，因此必须将其中一个转化成decimal型，使用后缀m就可以了
             * (int)不进行四舍五入，Convert.ToInt32会四舍五入
            */
            //-----------计算中间值，----------

            try
            {
                //----------Top Level----------

                //----------IR保护支架----------
                if (item.MARVEL == "NO")
                {
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCB0001-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCB0001-1"));
                    if (item.IRNo > 0) swComp.SetSuppression2(2); //2解压缩，0压缩.
                    else swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    if (item.IRNo > 1)
                    {
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swModel.Parameter("D3@LocalLPattern1").SystemValue = item.IRDis2 / 1000m;
                    }
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    if (item.IRNo > 2)
                    {
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swModel.Parameter("D3@LocalLPattern2").SystemValue = (item.IRDis2 + item.IRDis3) / 1000m;
                    }
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                //----------ANSUL探测器盒子----------
                if (item.ANSUL == "NO")
                {
                    swFeat = swAssy.FeatureByName("LocalLPattern3");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern4");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern5");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern6");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201990405-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201990405-1"));
                    if (item.ANDetectorNo > 0) swComp.SetSuppression2(2); //2解压缩，0压缩.
                    else swComp.SetSuppression2(0); //2解压缩，0压缩.

                    swFeat = swAssy.FeatureByName("LocalLPattern3");
                    if (item.ANDetectorNo > 1)
                    {
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swModel.Parameter("D3@LocalLPattern3").SystemValue = item.ANDetectorDis2 / 1000m;
                    }
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern4");
                    if (item.ANDetectorNo > 2)
                    {
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swModel.Parameter("D3@LocalLPattern4").SystemValue = (item.ANDetectorDis2 + item.ANDetectorDis3) / 1000m;
                    }
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern5");
                    if (item.ANDetectorNo > 2)
                    {
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swModel.Parameter("D3@LocalLPattern5").SystemValue = (item.ANDetectorDis2 + item.ANDetectorDis3 + item.ANDetectorDis4) / 1000m;
                    }
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern6");
                    if (item.ANDetectorNo > 2)
                    {
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swModel.Parameter("D3@LocalLPattern6").SystemValue = (item.ANDetectorDis2 + item.ANDetectorDis3 + item.ANDetectorDis4 + item.ANDetectorDis5) / 1000m;
                    }
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                //----------ANSUL腔体----------
                //重命名装配体内部
                compReName = "FNCE0025[AN-" + tree.Module + "]{" + (int)item.Length + "}(" + (int)item.Width +")";
                status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0025-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                if (status) swModelDocExt.RenameDocument(compReName);
                swModel.ClearSelection2(true);
                status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                swModel.ClearSelection2(true);
                if (status)
                {
                    swComp = swAssy.GetComponentByName(compReName + "-1");
                    swPart = swComp.GetModelDoc2(); //打开零件
                    swPart.Parameter("D2@Base-Flange1").SystemValue = item.Length / 1000m;
                    swPart.Parameter("D1@Sketch1").SystemValue = item.Width / 1000m;
                    if (item.ANSUL == "NO")
                    {
                        swFeat = swComp.FeatureByName("AN1");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("AN2");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("AN3");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("AN4");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("AN5");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩

                        swFeat = swComp.FeatureByName("ANDTEC1");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC2");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC3");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC4");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC5");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("AN1");
                        if (item.ANDropNo > 0)
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D1@Sketch7").SystemValue = item.ANYDis / 1000m;
                            swPart.Parameter("D2@Sketch7").SystemValue = item.ANDropDis1 / 1000m;
                        }
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩  
                        swFeat = swComp.FeatureByName("AN2");
                        if (item.ANDropNo > 1)
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D2@Sketch9").SystemValue = item.ANDropDis2 / 1000m;
                        }
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("AN3");
                        if (item.ANDropNo > 2)
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D2@Sketch10").SystemValue = item.ANDropDis3 / 1000m;
                        }
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        if (item.ANDropNo > 3)
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D2@Sketch11").SystemValue = item.ANDropDis4 / 1000m;
                        }
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        if (item.ANDropNo > 4)
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D2@Sketch12").SystemValue = item.ANDropDis5 / 1000m;
                        }
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩

                        swFeat = swComp.FeatureByName("ANDTEC1");
                        if (item.ANDetectorNo > 0)
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch13").SystemValue = item.ANDetectorDis1 / 1000m;
                            if (item.ANDetectorEnd == "RIGHT")
                                swPart.Parameter("D1@Sketch13").SystemValue = 195m / 1000m;
                            else swPart.Parameter("D1@Sketch13").SystemValue = 175m / 1000m;
                        }
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩  
                        swFeat = swComp.FeatureByName("ANDTEC2");
                        if (item.ANDetectorNo > 1)
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch14").SystemValue = item.ANDetectorDis2 / 1000m;
                            if (item.ANDetectorEnd == "RIGHT")
                                swPart.Parameter("D1@Sketch14").SystemValue = 195m / 1000m;
                            else swPart.Parameter("D1@Sketch14").SystemValue = 175m / 1000m;
                        }
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC3");
                        if (item.ANDetectorNo > 2)
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch15").SystemValue = item.ANDetectorDis3 / 1000m;
                            if (item.ANDetectorEnd == "RIGHT")
                                swPart.Parameter("D1@Sketch15").SystemValue = 195m / 1000m;
                            else swPart.Parameter("D1@Sketch15").SystemValue = 175m / 1000m;
                        }
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC4");
                        if (item.ANDetectorNo > 3)
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch16").SystemValue = item.ANDetectorDis4 / 1000m;
                            if (item.ANDetectorEnd == "RIGHT")
                                swPart.Parameter("D1@Sketch16").SystemValue = 195m / 1000m;
                            else swPart.Parameter("D1@Sketch16").SystemValue = 175m / 1000m;
                        }
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTEC5");
                        if (item.ANDetectorNo > 4)
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch17").SystemValue = item.ANDetectorDis5 / 1000m;
                            if (item.ANDetectorEnd == "RIGHT")
                                swPart.Parameter("D1@Sketch17").SystemValue = 195m / 1000m;
                            else swPart.Parameter("D1@Sketch17").SystemValue = 175m / 1000m;
                        }
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    if (item.MARVEL == "NO")
                    {
                        swFeat = swComp.FeatureByName("MA1");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("MA2");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("MA3");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("MA1");
                        if (item.IRNo > 0)
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D2@Sketch18").SystemValue = item.ANYDis / 1000m;
                            swPart.Parameter("D1@Sketch18").SystemValue = item.IRDis1 / 1000m;
                        }
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩    
                        swFeat = swComp.FeatureByName("MA2");
                        if (item.IRNo > 1)
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D1@Sketch19").SystemValue = item.IRDis2 / 1000m;
                        }
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("MA3");
                        if (item.IRNo > 2)
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D1@Sketch20").SystemValue = item.IRDis3 / 1000m;
                        }
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                }
                //----------ANSUL腔侧板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0001-1"));
                swPart = swComp.GetModelDoc2(); //打开零件
                swPart.Parameter("D2@Sketch2").SystemValue = (item.Width - 2m) / 1000m;
                swFeat = swComp.FeatureByName("ANDTEC HOLE");
                if (item.ANSUL == "YES" && item.ANDetectorNo > 0) swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩

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
