using System;
using System.IO;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class LKS258HCLAutoDrawing : IAutoDrawing
    {
        readonly LKS258HCLService objLKS258HCLService = new LKS258HCLService();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            //创建项目模型存放地址
            string itemPath = $@"{projectPath}\{tree.Module}-{tree.CategoryName}";
            if (!CommonFunc.CreateProjectPath(itemPath)) return;
            //Pack的后缀
            string suffix = $@"{tree.Module}-{tree.ODPNo.Substring(tree.ODPNo.Length - 6)}";
            //判断文件是否存在，如果存在将不执行pack，如果不存在则执行pack
            //packango后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = $@"{itemPath}\{tree.CategoryName.ToLower()}_{suffix}.sldasm";
            if (!File.Exists(packedAssyPath)) packedAssyPath = CommonFunc.PackAndGoFunc(suffix, swApp, tree.ModelPath, itemPath);

            //查询参数
            LKS258HCL item = (LKS258HCL)objLKS258HCLService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

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
                //镀锌板
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0084-1"));
                if (item.HCLSide == "NO") swComp.SetSuppression2(0); //2解压缩，0压缩.
                else swComp.SetSuppression2(2); //2解压缩，0压缩.
                if (item.HCLSide == "NO")
                {
                    swFeat = swAssy.FeatureByName("LocalLPattern3");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                else if (item.HCLSide == "BOTH")
                {
                    swFeat = swAssy.FeatureByName("LocalLPattern3");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern3").SystemValue = 8;
                }
                else
                {
                    swFeat = swAssy.FeatureByName("LocalLPattern3");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern3").SystemValue = 4;
                }
                //----------HCL侧板----------
                switch (item.HCLSide)
                {
                    case "LEFT":
                        //重命名装配体内部
                        compReName = "FNCE0082[HCLSP-" + tree.Module + "]{" + ((int)item.HCLSideLeft - 3) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0082-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0082-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0082-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            swModelDocExt.RenameDocument(compReName);
                        }
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2();//打开零件
                            swPart.Parameter("D1@Sketch1").SystemValue = (item.HCLSideLeft - 3m) / 1000m;
                        }
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0083-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                    case "RIGHT":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0082-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                                                   //重命名装配体内部
                        compReName = "FNCE0083[HCLSP-" + tree.Module + "]{" + ((int)item.HCLSideRight - 3) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0083-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0083-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0083-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            swModelDocExt.RenameDocument(compReName);
                        }
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2();//打开零件
                            swPart.Parameter("D1@Sketch1").SystemValue = (item.HCLSideRight - 3m) / 1000m;
                        }
                        break;
                    case "BOTH":
                        //重命名装配体内部
                        compReName = "FNCE0082[HCLSP-" + tree.Module + "]{" + ((int)item.HCLSideLeft - 3) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0082-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0082-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0082-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            swModelDocExt.RenameDocument(compReName);
                        }

                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2();//打开零件
                            swPart.Parameter("D1@Sketch1").SystemValue = (item.HCLSideLeft - 3m) / 1000m;
                        }
                        //重命名装配体内部
                        compReName = "FNCE0083[HCLSP-" + tree.Module + "]{" + ((int)item.HCLSideRight - 3) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0083-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0083-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0083-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            swModelDocExt.RenameDocument(compReName);
                        }
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2();//打开零件
                            swPart.Parameter("D1@Sketch1").SystemValue = (item.HCLSideRight - 3m) / 1000m;
                        }
                        break;
                    default:
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0082-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0083-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                }


                //----------灯腔主体----------
                //重命名装配体内部
                compReName = "FNCL0036[LKS258HCL-" + tree.Module + "]{" + (int)item.Length + "}";
                status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCL0036-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                if (status) swModelDocExt.RenameDocument(compReName);
                swModel.ClearSelection2(true);
                status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                swModel.ClearSelection2(true);
                if (status)
                {
                    swComp = swAssy.GetComponentByName(compReName + "-1");
                    swPart = swComp.GetModelDoc2(); //打开零件
                    swPart.Parameter("D2@Skizze1").SystemValue = item.Length / 1000m;
                    //HCL侧板磁铁孔
                    if (item.HCLSide == "LEFT" || item.HCLSide == "BOTH")
                    {
                        swFeat = swComp.FeatureByName("Cut-Extrude5");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D5@Sketch22").SystemValue = (item.HCLSideLeft - 103) / 1000m;
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("Cut-Extrude5");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    if (item.HCLSide == "RIGHT" || item.HCLSide == "BOTH")
                    {
                        swFeat = swComp.FeatureByName("Cut-Extrude6");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                        swPart.Parameter("D6@Sketch23").SystemValue = (item.HCLSideRight - 103) / 1000m;
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("Cut-Extrude6");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                }

                //支撑条

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCL0034-1"));
                swPart = swComp.GetModelDoc2();//打开零件
                switch (item.HCLSide)
                {
                    case "LEFT":
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Length - item.HCLSideLeft) / 1000m;
                        swModel.Parameter("D1@Distance8").SystemValue = item.HCLSideLeft / 1000m;
                        break;
                    case "RIGHT":
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Length - item.HCLSideRight) / 1000m;
                        swModel.Parameter("D1@Distance8").SystemValue = 0m;
                        break;
                    case "BOTH":
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Length - item.HCLSideLeft - item.HCLSideRight) / 1000m;
                        swModel.Parameter("D1@Distance8").SystemValue = item.HCLSideLeft / 1000m;
                        break;
                    default:
                        swPart.Parameter("D1@Skizze1").SystemValue = item.Length / 1000m;
                        swModel.Parameter("D1@Distance8").SystemValue = 0m;
                        break;
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
