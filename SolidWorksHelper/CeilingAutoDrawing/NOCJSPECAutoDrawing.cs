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
    public class NOCJSPECAutoDrawing:IAutoDrawing
    {
        readonly NOCJSPECService objNOCJSPECService = new NOCJSPECService();
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
            NOCJSPEC item = (NOCJSPEC)objNOCJSPECService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

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
            decimal leftSBDis = item.LeftDis;
            decimal rightSBDis = item.RightDis;

            try
            {
                //----------Top Level----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCS0021-1"));
                swComp.SetSuppression2(0); //2解压缩，0压缩.
                //----------侧板----------
                switch (item.SidePanel)
                {
                    case "LEFT":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0029-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2();//打开零件
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Width - 2m) / 1000m;
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Height - 3m) / 1000m;
                        swPart.Parameter("D1@Sketch1").SystemValue = (item.BackBend - 2.5m) / 1000m;
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0031-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0032-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0030-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0031-3"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2();//打开零件
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Width - 2m) / 1000m;
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Height - 3m) / 1000m;
                        swPart.Parameter("D1@Sketch3").SystemValue = (item.BackBend - 2.5m) / 1000m;
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0033-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                    case "RIGHT":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0029-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0031-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2();//打开零件
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Width - 2m) / 1000m;
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Height - 3m) / 1000m;
                        swPart.Parameter("D1@Sketch3").SystemValue = (item.BackBend - 2.5m) / 1000m;
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0032-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0030-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2();//打开零件
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Width - 2m) / 1000m;
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Height - 3m) / 1000m;
                        swPart.Parameter("D1@Sketch1").SystemValue = (item.BackBend - 2.5m) / 1000m;
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0031-3"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0033-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                    case "MIDDLE":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0029-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0031-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0032-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0030-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0031-3"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2();//打开零件
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Width - 2m) / 1000m;
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Height - 3m) / 1000m;
                        swPart.Parameter("D1@Sketch3").SystemValue = (item.BackBend - 2.5m) / 1000m;
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0033-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                    case "NOCJBACKL":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0029-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0031-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0032-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2();//打开零件
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Width - 2m) / 1000m;
                        swPart.Parameter("D7@Edge-Flange1").SystemValue = item.Width / 1000m;
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Height - 3m) / 1000m;
                        swPart.Parameter("D1@Sketch1").SystemValue = (item.BackBend - 1m) / 1000m;
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0030-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0031-3"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2();//打开零件
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Width - 2m) / 1000m;
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Height - 3m) / 1000m;
                        swPart.Parameter("D1@Sketch3").SystemValue = (item.BackBend - 2.5m) / 1000m;
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0033-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                    case "NOCJBACKR":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0029-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0031-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2();//打开零件
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Width - 2m) / 1000m;
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Height - 3m) / 1000m;
                        swPart.Parameter("D1@Sketch3").SystemValue = (item.BackBend - 2.5m) / 1000m;
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0032-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0030-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0031-3"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0033-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2();//打开零件
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Width - 2m) / 1000m;
                        swPart.Parameter("D7@Edge-Flange1").SystemValue = item.Width / 1000m;
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Height - 3m) / 1000m;
                        swPart.Parameter("D1@Sketch1").SystemValue = (item.BackBend - 1m) / 1000m;
                        break;
                    case "NOCJBACKB":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0029-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0031-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0032-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2();//打开零件
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Width - 2m) / 1000m;
                        swPart.Parameter("D7@Edge-Flange1").SystemValue = item.Width / 1000m;
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Height - 3m) / 1000m;
                        swPart.Parameter("D1@Sketch1").SystemValue = (item.BackBend - 1m) / 1000m;
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0030-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0031-3"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0033-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2();//打开零件
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Width - 2m) / 1000m;
                        swPart.Parameter("D7@Edge-Flange1").SystemValue = item.Width / 1000m;
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Height - 3m) / 1000m;
                        swPart.Parameter("D1@Sketch1").SystemValue = (item.BackBend - 1m) / 1000m;
                        break;
                    default:
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0029-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2();//打开零件
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Width - 2m) / 1000m;
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Height - 3m) / 1000m;
                        swPart.Parameter("D1@Sketch1").SystemValue = (item.BackBend - 2.5m) / 1000m;
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0031-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0032-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0030-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2();//打开零件
                        swPart.Parameter("D1@Skizze1").SystemValue = (item.Width - 2m) / 1000m;
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Height - 3m) / 1000m;
                        swPart.Parameter("D1@Sketch1").SystemValue = (item.BackBend - 2.5m) / 1000m;
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0031-3"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0033-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                }

                //----------NOCJ腔主体----------
                //重命名装配体内部
                compReName = "FNCJ0028[NOCJSPEC-" + tree.Module + "]{" + (int)item.Length + "}("+(int)item.Width+")";
                status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCJ0028-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                if (status) swModelDocExt.RenameDocument(compReName);
                swModel.ClearSelection2(true);
                status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                swModel.ClearSelection2(true);
                if (status)
                {
                    swComp = swAssy.GetComponentByName(compReName + "-1");
                    swPart = swComp.GetModelDoc2(); //打开零件
                    swPart.Parameter("D2@Skizze1").SystemValue = item.Length / 1000m;
                    swPart.Parameter("D7@Kante-Lasche1").SystemValue = item.Width / 1000m;
                    swPart.Parameter("D1@Skizze1").SystemValue = item.Height / 1000m;
                    swPart.Parameter("D1@Skizze17").SystemValue = item.TopBend / 1000m;
                    swPart.Parameter("D7@Kante-Lasche2").SystemValue = item.BackBend / 1000m;

                    //NOCJBACKL/R/B
                    if (item.SidePanel == "NOCJBACKL")
                    {
                        swFeat = swComp.FeatureByName("BNOCJL");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch54").SystemValue = item.Width / 1000m;
                        swFeat = swComp.FeatureByName("BNOCJR");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    else if (item.SidePanel == "NOCJBACKR")
                    {
                        swFeat = swComp.FeatureByName("BNOCJL");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("BNOCJR");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch55").SystemValue = item.Width / 1000m;
                    }
                    else if (item.SidePanel == "NOCJBACKB")
                    {
                        swFeat = swComp.FeatureByName("BNOCJL");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch54").SystemValue = item.Width / 1000m;
                        swFeat = swComp.FeatureByName("BNOCJR");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch55").SystemValue = item.Width / 1000m;
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("BNOCJL");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("BNOCJR");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //BCJ
                    if (item.BackCJSide == "LEFT" || item.BackCJSide == "BOTH")
                    {
                        swFeat = swComp.FeatureByName("BCJ-LEFT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D11@Sketch19").SystemValue = (item.LeftDis + 1m) / 1000m;
                        leftSBDis += 90m;
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("BCJ-LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    if (item.BackCJSide == "RIGHT" || item.BackCJSide == "BOTH")
                    {
                        swFeat = swComp.FeatureByName("BCJ-RIGHT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D11@Sketch20").SystemValue = (item.RightDis + 1m) / 1000m;
                        rightSBDis += 90m;
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("BCJ-RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //----------左----------
                    //左类型排风腔KCJDB800
                    if (item.LeftBeamType == "KCJDB800" || item.LeftBeamType == "UCJDB800")
                    {
                        swFeat = swComp.FeatureByName("BCJ-LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("KCJDB800-LEFT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D59@Sketch46").SystemValue = (item.LeftBeamDis + 1m) / 1000m;
                        if (item.GutterSide == "LEFT" || item.GutterSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("GUTTER-LEFT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D4@Sketch48").SystemValue = (item.GutterWidth - 2m) / 1000m;
                            swPart.Parameter("D5@Sketch48").SystemValue = (item.GutterWidth - 62m) / 1000m;
                            swPart.Parameter("D6@Sketch48").SystemValue = (item.Length - item.LeftBeamDis + 1m) / 1000m;
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("KCJDB800-LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //左类型排风腔KCWDB800
                    if (item.LeftBeamType == "KCWDB800" || item.LeftBeamType == "UCWDB800")
                    {
                        swFeat = swComp.FeatureByName("BCJ-LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("KCWDB800-LEFT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D31@Sketch32").SystemValue = (item.LeftBeamDis + 1m) / 1000m;
                        if (item.GutterSide == "LEFT" || item.GutterSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("GUTTER-LEFT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D4@Sketch48").SystemValue = (item.GutterWidth - 2m) / 1000m;
                            swPart.Parameter("D5@Sketch48").SystemValue = (item.GutterWidth - 62m) / 1000m;
                            swPart.Parameter("D6@Sketch48").SystemValue = (item.Length - item.LeftBeamDis + 1m) / 1000m;
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("KCWDB800-LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //左类型排风腔KCJSB535
                    if (item.LeftBeamType == "KCJSB535" || item.LeftBeamType == "UCJSB535")
                    {
                        swFeat = swComp.FeatureByName("KCJSB535-LEFT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D46@Sketch35").SystemValue = (leftSBDis + 1m) / 1000m;
                        if (item.GutterSide == "LEFT" || item.GutterSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("GUTTER-LEFT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D4@Sketch48").SystemValue = (item.GutterWidth - 2m) / 1000m;
                            swPart.Parameter("D5@Sketch48").SystemValue = (item.GutterWidth - 62m) / 1000m;
                            swPart.Parameter("D6@Sketch48").SystemValue = (leftSBDis + 535m + 1m) / 1000m;
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("KCJSB535-LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //左类型排风腔KCWSB535
                    if (item.LeftBeamType == "KCWSB535" || item.LeftBeamType == "UCWSB535")
                    {
                        swFeat = swComp.FeatureByName("KCWSB535-LEFT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D23@Sketch28").SystemValue = (leftSBDis + 1m) / 1000m;
                        if (item.GutterSide == "LEFT" || item.GutterSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("GUTTER-LEFT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D4@Sketch48").SystemValue = (item.GutterWidth - 2m) / 1000m;
                            swPart.Parameter("D5@Sketch48").SystemValue = (item.GutterWidth - 62m) / 1000m;
                            swPart.Parameter("D6@Sketch48").SystemValue = (leftSBDis + 535m + 1m) / 1000m;
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("KCWSB535-LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //左类型排风腔UCJSB385
                    if (item.LeftBeamType == "UCJSB385")
                    {
                        swFeat = swComp.FeatureByName("UCJSB385-LEFT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D16@Sketch43").SystemValue = (leftSBDis + 1m) / 1000m;
                        if (item.GutterSide == "LEFT" || item.GutterSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("GUTTER-LEFT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D4@Sketch48").SystemValue = (item.GutterWidth - 2m) / 1000m;
                            swPart.Parameter("D5@Sketch48").SystemValue = (item.GutterWidth - 62m) / 1000m;
                            swPart.Parameter("D6@Sketch48").SystemValue = (leftSBDis + 385m + 1m) / 1000m;
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("UCJSB385-LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //左类型排风腔KCJSB290
                    if (item.LeftBeamType == "KCJSB290")
                    {
                        swFeat = swComp.FeatureByName("KCJSB290-LEFT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D14@Sketch41").SystemValue = (leftSBDis + 1m) / 1000m;
                        if (item.GutterSide == "LEFT" || item.GutterSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("GUTTER-LEFT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D4@Sketch48").SystemValue = (item.GutterWidth - 2m) / 1000m;
                            swPart.Parameter("D5@Sketch48").SystemValue = (item.GutterWidth - 62m) / 1000m;
                            swPart.Parameter("D6@Sketch48").SystemValue = (leftSBDis + 290m + 1m) / 1000m;
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("KCJSB290-LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //左类型排风腔KCJSB265
                    if (item.LeftBeamType == "KCJSB265")
                    {
                        swFeat = swComp.FeatureByName("KCJSB265-LEFT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D19@Sketch38").SystemValue = (leftSBDis + 1m) / 1000m;
                        if (item.LKSide == "LEFT" || item.LKSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("LKS270-LEFT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D24@Sketch51").SystemValue = (leftSBDis + 265m + 1m) / 1000m;
                            if (item.GutterSide == "LEFT" || item.GutterSide == "BOTH")
                            {
                                swFeat = swComp.FeatureByName("GUTTER-LEFT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                                swPart.Parameter("D4@Sketch48").SystemValue = (item.GutterWidth - 2m) / 1000m;
                                swPart.Parameter("D5@Sketch48").SystemValue = (item.GutterWidth - 62m) / 1000m;
                                swPart.Parameter("D6@Sketch48").SystemValue = (leftSBDis + 270m + 265m + 1m) / 1000m;
                            }
                        }
                        else
                        {
                            if (item.GutterSide == "LEFT" || item.GutterSide == "BOTH")
                            {
                                swFeat = swComp.FeatureByName("GUTTER-LEFT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                                swPart.Parameter("D4@Sketch48").SystemValue = (item.GutterWidth - 2m) / 1000m;
                                swPart.Parameter("D5@Sketch48").SystemValue = (item.GutterWidth - 62m) / 1000m;
                                swPart.Parameter("D6@Sketch48").SystemValue = (leftSBDis + 265m + 1m) / 1000m;
                            }
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("KCJSB265-LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //左类型排风腔KCWSB265
                    if (item.LeftBeamType == "KCWSB265")
                    {
                        swFeat = swComp.FeatureByName("KCWSB265-LEFT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D15@Sketch22").SystemValue = (leftSBDis + 1m) / 1000m;
                        if (item.LKSide == "LEFT" || item.LKSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("LKS270-LEFT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D24@Sketch51").SystemValue = (leftSBDis + 265m + 1m) / 1000m;
                            if (item.GutterSide == "LEFT" || item.GutterSide == "BOTH")
                            {
                                swFeat = swComp.FeatureByName("GUTTER-LEFT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                                swPart.Parameter("D4@Sketch48").SystemValue = (item.GutterWidth - 2m) / 1000m;
                                swPart.Parameter("D5@Sketch48").SystemValue = (item.GutterWidth - 62m) / 1000m;
                                swPart.Parameter("D6@Sketch48").SystemValue = (leftSBDis + 270m + 265m + 1m) / 1000m;
                            }
                        }
                        else
                        {
                            if (item.GutterSide == "LEFT" || item.GutterSide == "BOTH")
                            {
                                swFeat = swComp.FeatureByName("GUTTER-LEFT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                                swPart.Parameter("D4@Sketch48").SystemValue = (item.GutterWidth - 2m) / 1000m;
                                swPart.Parameter("D5@Sketch48").SystemValue = (item.GutterWidth - 62m) / 1000m;
                                swPart.Parameter("D6@Sketch48").SystemValue = (leftSBDis + 265m + 1m) / 1000m;
                            }

                        }

                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("KCWSB265-LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }

                    //----------右----------
                    //右类型排风腔KCJDB800
                    if (item.RightBeamType == "KCJDB800" || item.RightBeamType == "UCJDB800")
                    {
                        swFeat = swComp.FeatureByName("BCJ-RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("KCJDB800-RIGHT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D59@Sketch47").SystemValue = (item.RightBeamDis + 1m) / 1000m;
                        if (item.GutterSide == "RIGHT" || item.GutterSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("GUTTER-RIGHT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D4@Sketch50").SystemValue = (item.GutterWidth - 2m) / 1000m;
                            swPart.Parameter("D5@Sketch50").SystemValue = (item.GutterWidth - 62m) / 1000m;
                            swPart.Parameter("D7@Sketch50").SystemValue =
                                (item.Length - item.RightBeamDis + 1m) / 1000m;
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("KCJDB800-RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //右类型排风腔KCWDB800
                    if (item.RightBeamType == "KCWDB800" || item.RightBeamType == "UCWDB800")
                    {
                        swFeat = swComp.FeatureByName("BCJ-RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("KCWDB800-RIGHT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D31@Sketch45").SystemValue = (item.RightBeamDis + 1m) / 1000m;
                        if (item.GutterSide == "RIGHT" || item.GutterSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("GUTTER-RIGHT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D4@Sketch50").SystemValue = (item.GutterWidth - 2m) / 1000m;
                            swPart.Parameter("D5@Sketch50").SystemValue = (item.GutterWidth - 62m) / 1000m;
                            swPart.Parameter("D7@Sketch50").SystemValue =
                                (item.Length - item.RightBeamDis + 1m) / 1000m;
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("KCWDB800-RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //右类型排风腔KCJSB535
                    if (item.RightBeamType == "KCJSB535" || item.RightBeamType == "UCJSB535")
                    {
                        swFeat = swComp.FeatureByName("KCJSB535-RIGHT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D46@Sketch37").SystemValue = (rightSBDis + 1m) / 1000m;
                        if (item.GutterSide == "RIGHT" || item.GutterSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("GUTTER-RIGHT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D4@Sketch50").SystemValue = (item.GutterWidth - 2m) / 1000m;
                            swPart.Parameter("D5@Sketch50").SystemValue = (item.GutterWidth - 62m) / 1000m;
                            swPart.Parameter("D7@Sketch50").SystemValue = (rightSBDis + 535m + 1m) / 1000m;
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("KCJSB535-RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //右类型排风腔KCWSB535
                    if (item.RightBeamType == "KCWSB535" || item.RightBeamType == "UCWSB535")
                    {
                        swFeat = swComp.FeatureByName("KCWSB535-RIGHT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D23@Sketch29").SystemValue = (rightSBDis + 1m) / 1000m;
                        if (item.GutterSide == "RIGHT" || item.GutterSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("GUTTER-RIGHT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D4@Sketch50").SystemValue = (item.GutterWidth - 2m) / 1000m;
                            swPart.Parameter("D5@Sketch50").SystemValue = (item.GutterWidth - 62m) / 1000m;
                            swPart.Parameter("D7@Sketch50").SystemValue = (rightSBDis + 535m + 1m) / 1000m;
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("KCWSB535-RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //右类型排风腔UCJSB385
                    if (item.RightBeamType == "UCJSB385")
                    {
                        swFeat = swComp.FeatureByName("UCJSB385-RIGHT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D16@Sketch44").SystemValue = (rightSBDis + 1m) / 1000m;
                        if (item.GutterSide == "RIGHT" || item.GutterSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("GUTTER-RIGHT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D4@Sketch50").SystemValue = (item.GutterWidth - 2m) / 1000m;
                            swPart.Parameter("D5@Sketch50").SystemValue = (item.GutterWidth - 62m) / 1000m;
                            swPart.Parameter("D7@Sketch50").SystemValue = (rightSBDis + 385m + 1m) / 1000m;
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("UCJSB385-RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //右类型排风腔KCJSB290
                    if (item.RightBeamType == "KCJSB290")
                    {
                        swFeat = swComp.FeatureByName("KCJSB290-RIGHT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D14@Sketch42").SystemValue = (rightSBDis + 1m) / 1000m;
                        if (item.GutterSide == "RIGHT" || item.GutterSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("GUTTER-RIGHT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D4@Sketch50").SystemValue = (item.GutterWidth - 2m) / 1000m;
                            swPart.Parameter("D5@Sketch50").SystemValue = (item.GutterWidth - 62m) / 1000m;
                            swPart.Parameter("D7@Sketch50").SystemValue = (rightSBDis + 290m + 1m) / 1000m;
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("KCJSB290-RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //右类型排风腔KCJSB265
                    if (item.RightBeamType == "KCJSB265")
                    {
                        swFeat = swComp.FeatureByName("KCJSB265-RIGHT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D18@Sketch40").SystemValue = (rightSBDis + 1m) / 1000m;
                        if (item.LKSide == "RIGHT" || item.LKSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("LKS270-RIGHT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D24@Sketch53").SystemValue = (rightSBDis + 265m + 1m) / 1000m;
                            if (item.GutterSide == "RIGHT" || item.GutterSide == "BOTH")
                            {
                                swFeat = swComp.FeatureByName("GUTTER-RIGHT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                                swPart.Parameter("D4@Sketch50").SystemValue = (item.GutterWidth - 2m) / 1000m;
                                swPart.Parameter("D5@Sketch50").SystemValue = (item.GutterWidth - 62m) / 1000m;
                                swPart.Parameter("D7@Sketch50").SystemValue = (rightSBDis + 270m + 265m + 1m) / 1000m;
                            }
                        }
                        else
                        {
                            if (item.GutterSide == "RIGHT" || item.GutterSide == "BOTH")
                            {
                                swFeat = swComp.FeatureByName("GUTTER-RIGHT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                                swPart.Parameter("D4@Sketch50").SystemValue = (item.GutterWidth - 2m) / 1000m;
                                swPart.Parameter("D5@Sketch50").SystemValue = (item.GutterWidth - 62m) / 1000m;
                                swPart.Parameter("D7@Sketch50").SystemValue = (rightSBDis + 265m + 1m) / 1000m;
                            }
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("KCJSB265-RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //右类型排风腔KCWSB265
                    if (item.RightBeamType == "KCWSB265")
                    {
                        swFeat = swComp.FeatureByName("KCWSB265-RIGHT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D14@Sketch23").SystemValue = (rightSBDis + 1m) / 1000m;
                        if (item.LKSide == "RIGHT" || item.LKSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("LKS270-RIGHT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                            swPart.Parameter("D24@Sketch53").SystemValue = (rightSBDis + 265m + 1m) / 1000m;
                            if (item.GutterSide == "RIGHT" || item.GutterSide == "BOTH")
                            {
                                swFeat = swComp.FeatureByName("GUTTER-RIGHT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                                swPart.Parameter("D4@Sketch50").SystemValue = (item.GutterWidth - 2m) / 1000m;
                                swPart.Parameter("D5@Sketch50").SystemValue = (item.GutterWidth - 62m) / 1000m;
                                swPart.Parameter("D7@Sketch50").SystemValue = (rightSBDis + 270m + 265m + 1m) / 1000m;
                            }
                        }
                        else
                        {
                            if (item.GutterSide == "RIGHT" || item.GutterSide == "BOTH")
                            {
                                swFeat = swComp.FeatureByName("GUTTER-RIGHT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩 
                                swPart.Parameter("D4@Sketch50").SystemValue = (item.GutterWidth - 2m) / 1000m;
                                swPart.Parameter("D5@Sketch50").SystemValue = (item.GutterWidth - 62m) / 1000m;
                                swPart.Parameter("D7@Sketch50").SystemValue = (rightSBDis + 265m + 1m) / 1000m;
                            }
                        }
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("KCWSB265-RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
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
