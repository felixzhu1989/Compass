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
    public class LKASPECAutoDrawing : IAutoDrawing
    {
        readonly LKASPECService objLKASPECService = new LKASPECService();
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
            LKASPEC item = (LKASPEC)objLKASPECService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix;//后缀
            ModelDoc2 swModel;
            ModelDoc2 swPart;
            AssemblyDoc swAssy;
            Component2 swComp;
            Feature swFeat;
            object configNames = null;
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
                //----------侧板----------
                switch (item.SidePanel)
                {
                    case "LEFT":
                    case "RIGHT":
                        //重命名装配体内部
                        compReName = "FNCL0018[LKEC-" + tree.Module + "]{255}(" + (int)(item.Height - 5d) + ")";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCL0032[LKEC-]{255}()-3") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status) swModelDocExt.RenameDocument(compReName);
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-3" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-3");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2(); //打开零件
                            swPart.Parameter("D1@Sketch1").SystemValue = (item.Height - 5d) / 1000d;
                            swComp = swAssy.GetComponentByName(compReName + "-4");
                            swComp.SetSuppression2(0); //2解压缩，0压缩.
                        }
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCL0016[LKRPS]-6"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCL0016[LKRPS]-7"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCL0017[LKRPT]-3"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        break;
                    case "MIDDLE":
                        //重命名装配体内部
                        compReName = "FNCL0032[LKEC-" + tree.Module + "]{255}(" + (int)(item.Height - 5d) + ")";
                        status = swModelDocExt.SelectByID2(compReName + "-3" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-3");
                            swComp.SetSuppression2(0); //2解压缩，0压缩.
                            swComp = swAssy.GetComponentByName(compReName + "-4");
                            swComp.SetSuppression2(0); //2解压缩，0压缩.
                        }
                        else
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCL0032[LKEC-]{255}()-3"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩.
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCL0032[LKEC-]{255}()-4"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩. 
                        }
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCL0016[LKRPS]-6"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCL0016[LKRPS]-7"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCL0017[LKRPT]-3"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        break;
                    default:
                        //重命名装配体内部
                        compReName = "FNCL0032[LKEC-" + tree.Module + "]{255}(" + (int)(item.Height - 5d) + ")";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCL0032[LKEC-]{255}()-3") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status) swModelDocExt.RenameDocument(compReName);
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-3" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-3");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2(); //打开零件
                            swPart.Parameter("D1@Sketch1").SystemValue = (item.Height - 5d) / 1000d;
                            swComp = swAssy.GetComponentByName(compReName + "-4");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                        }
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCL0016[LKRPS]-6"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCL0016[LKRPS]-7"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCL0017[LKRPT]-3"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                }


                //----------灯腔主体----------
                //重命名装配体内部
                compReName = "FNCL0002[LKASPEC-" + tree.Module + "]{" + (int)item.Length + "}";
                status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCL0002-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                if (status) swModelDocExt.RenameDocument(compReName);
                swModel.ClearSelection2(true);
                status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                swModel.ClearSelection2(true);
                if (status)
                {
                    swComp = swAssy.GetComponentByName(compReName + "-1");
                    swPart = swComp.GetModelDoc2(); //打开零件
                    swPart.Parameter("D2@Skizze1").SystemValue = item.Length / 1000d;
                    swPart.Parameter("D7@Kante-Lasche1").SystemValue = item.Height / 1000d;
                    swFeat = swComp.FeatureByName("Cut-Extrude2");
                    if (item.Japan == "YES") swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("LIGHT T8");
                    if (item.LightType == "T8") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                if (item.Japan == "YES")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0070-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0070-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
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
