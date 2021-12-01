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
    public class KCJSB535AutoDrawing : IAutoDrawing
    {
        readonly KCJSB535Service objKCJSB535Service = new KCJSB535Service();
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
            string suffix = tree.Module + "-" +
                            tree.ODPNo.Substring(tree.ODPNo.Length - 6);
            //判断文件是否存在，如果存在将不执行pack，如果不存在则执行pack
            //packango后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = itemPath + @"\" + tree.CategoryName.ToLower() + "_" + suffix + ".sldasm";
            if (!File.Exists(packedAssyPath)) packedAssyPath = CommonFunc.PackAndGoFunc(suffix, swApp, tree.ModelPath, itemPath);

            //查询参数
            KCJSB535 item = (KCJSB535)objKCJSB535Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

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
            int fcNo = (int)((item.Length - item.FCSideLeft - item.FCSideRight) / 499m) - item.FCBlindNo;

            try
            {
                //----------Top Level----------


                //判断是否是HCL特殊灯腔
                if (item.LightType == "HCL")
                {
                    //NOHCL压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0111-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0112-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0056-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0110-3"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0110-4"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.

                    //HCL解压 
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0085-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0090-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0091-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0091-3"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0093-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "HCL-1000-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0086-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0086-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.

                    //先解压缩出来零件再改顶层装配关系
                    swModel.Parameter("D1@Distance41").SystemValue = item.LightPanelLeft / 1000m;
                    swModel.Parameter("D1@Distance42").SystemValue = item.LightPanelLeft / 1000m;
                    //镀锌片数量
                    if (item.LightPanelSide == "BOTH")
                    {
                        swModel.Parameter("D1@LocalLPattern6").SystemValue = 8;
                    }
                    else if (item.LightPanelSide == "RIGHT" || item.LightPanelSide == "LEFT")
                    {
                        swModel.Parameter("D1@LocalLPattern6").SystemValue = 4;
                    }
                    else
                    {
                        swFeat = swAssy.FeatureByName("LocalLPattern6");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0093-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                    }


                    //排风腔                    
                    //重命名装配体内部
                    compReName = "FNCE0089[KCJSB535-" + tree.Module + "]{" + (int)item.Length + "}";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0089-1") + "@" + assyName,
                        "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0089-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0089-1") + "@" + assyName,
                            "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModelDocExt.RenameDocument(compReName);
                    }
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false,
                        0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swPart = swComp.GetModelDoc2(); //打开零件
                        //公共的
                        swPart.Parameter("D1@Linear austragen1").SystemValue = item.Length / 1000m;
                        if (item.LightCable == "LEFT")
                        {
                            swFeat = swComp.FeatureByName("LIGHT HOLE LEFT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("LIGHT HOLE RIGHT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                        else if (item.LightCable == "RIGHT")
                        {
                            swFeat = swComp.FeatureByName("LIGHT HOLE LEFT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("LIGHT HOLE RIGHT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("LIGHT HOLE LEFT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("LIGHT HOLE RIGHT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                        if (item.MARVEL == "YES")
                        {
                            swFeat = swComp.FeatureByName("MA-NTC");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("MA-TAB");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("MA-NTC");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("MA-TAB");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                        }
                        if (item.ANSUL == "YES")
                        {
                            //侧喷
                            if (item.ANSide == "LEFT")
                            {
                                swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            }
                            else if (item.ANSide == "RIGHT")
                            {
                                swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            }
                            else
                            {
                                swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            }
                            //探测器
                            if (item.ANDetector == "LEFT")
                            {
                                swFeat = swComp.FeatureByName("ANDTECSIDE RIGHT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANDTECSIDE LEFT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            }
                            else if (item.ANDetector == "RIGHT")
                            {
                                swFeat = swComp.FeatureByName("ANDTECSIDE RIGHT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANDTECSIDE LEFT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            }
                            else if (item.ANDetector == "BOTH")
                            {
                                swFeat = swComp.FeatureByName("ANDTECSIDE RIGHT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANDTECSIDE LEFT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            }
                            else
                            {
                                swFeat = swComp.FeatureByName("ANDTECSIDE RIGHT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANDTECSIDE LEFT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            }
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANDTECSIDE RIGHT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANDTECSIDE LEFT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }

                        //镀锌板安装距离
                        swFeat = swComp.FeatureByName("LightPanelLeft");
                        if (item.LightPanelSide == "LEFT" || item.LightPanelSide == "BOTH")
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D5@Sketch21").SystemValue = (item.LightPanelLeft - 150) / 1000m;
                        }
                        else
                        {
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                        swFeat = swComp.FeatureByName("LightPanelRight");
                        if (item.LightPanelSide == "RIGHT" || item.LightPanelSide == "BOTH")
                        {
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D5@Sketch22").SystemValue = (item.LightPanelRight - 150) / 1000m;
                        }
                        else
                        {
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                        //日本的
                        if (item.Japan == "YES")
                        {
                            swFeat = swComp.FeatureByName("EX");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("Cut-Extrude4");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("EX");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("Cut-Extrude4");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch1").SystemValue = item.ExRightDis / 1000m;
                            swPart.Parameter("D1@Sketch1").SystemValue = item.ExLength / 1000m;
                            swPart.Parameter("D2@Sketch1").SystemValue = item.ExWidth / 1000m;
                        } //日本的结束
                    } //排风腔结束

                    //-------排风腔内灯腔----------
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0085-1"));
                    swPart = swComp.GetModelDoc2(); //打开零件
                    swPart.Parameter("D2@Base-Flange1").SystemValue = item.Length / 1000m;
                    swFeat = swComp.FeatureByName("FC SUPPORT");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("JAP LED M8");
                    if (item.Japan == "YES") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("LIGHT T8");
                    if (item.LightType == "T8") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    if (item.LightCable == "LEFT")
                    {
                        swFeat = swComp.FeatureByName("LIGHT HOLE LEFT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LIGHT HOLE RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    else if (item.LightCable == "RIGHT")
                    {
                        swFeat = swComp.FeatureByName("LIGHT HOLE LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LIGHT HOLE RIGHT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("LIGHT HOLE LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LIGHT HOLE RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }

                    //灯腔玻璃支架底部
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0090-1"));
                    swPart = swComp.GetModelDoc2(); //打开零件
                    swPart.Parameter("D1@Skizze1").SystemValue = item.Length / 1000m;
                    swFeat = swComp.FeatureByName("LightPanelLeft");
                    if (item.LightPanelSide == "LEFT" || item.LightPanelSide == "BOTH")
                    {
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D5@Sketch25").SystemValue = (item.LightPanelLeft - 150) / 1000m;
                    }
                    else
                    {
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    swFeat = swComp.FeatureByName("LightPanelRight");
                    if (item.LightPanelSide == "RIGHT" || item.LightPanelSide == "BOTH")
                    {
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D5@Sketch24").SystemValue = (item.LightPanelRight - 150) / 1000m;
                    }
                    else
                    {
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //灯腔玻璃支架支撑条上部
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0091-1"));
                    swPart = swComp.GetModelDoc2(); //打开零件
                    swPart.Parameter("D1@Skizze1").SystemValue =
                        (item.Length - item.LightPanelLeft - item.LightPanelRight) / 1000m;

                    //HCL灯腔侧板,重命名
                    switch (item.LightPanelSide)
                    {
                        case "LEFT":
                            compReName = "FNCE0092[HCLSP-" + tree.Module + "]{" + (int)item.LightPanelLeft + "}";
                            status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0092-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            if (status)
                            {
                                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0092-1"));
                                swComp.SetSuppression2(2); //2解压缩，0压缩.
                                swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0092-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                                swModelDocExt.RenameDocument(compReName);
                            }
                            swModel.ClearSelection2(true);
                            status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            swModel.ClearSelection2(true);
                            if (status)
                            {
                                swComp = swAssy.GetComponentByName(compReName + "-1");
                                swPart = swComp.GetModelDoc2(); //打开零件
                                swPart.Parameter("D1@Sketch1").SystemValue = item.LightPanelLeft / 1000m;
                                swPart.Parameter("D6@Sketch42").SystemValue = (item.LightPanelLeft - 150) / 1000m;
                            }
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0094-1"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩.
                            break;
                        case "RIGHT":
                            compReName = "FNCE0094[HCLSP-" + tree.Module + "]{" + (int)item.LightPanelRight + "}";
                            status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0094-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            if (status)
                            {
                                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0094-1"));
                                swComp.SetSuppression2(2); //2解压缩，0压缩.
                                swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0094-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                                swModelDocExt.RenameDocument(compReName);
                            }
                            swModel.ClearSelection2(true);
                            status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            swModel.ClearSelection2(true);
                            if (status)
                            {
                                swComp = swAssy.GetComponentByName(compReName + "-1");
                                swPart = swComp.GetModelDoc2(); //打开零件
                                swPart.Parameter("D1@Sketch1").SystemValue = item.LightPanelRight / 1000m;
                                swPart.Parameter("D6@Sketch42").SystemValue = (item.LightPanelRight - 150) / 1000m;
                            }
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0092-1"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩.
                            break;
                        case "BOTH":
                            compReName = "FNCE0092[HCLSP-" + tree.Module + ".1]{" + (int)item.LightPanelLeft + "}";
                            status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0092-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            if (status)
                            {
                                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0092-1"));
                                swComp.SetSuppression2(2); //2解压缩，0压缩.
                                swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0092-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                                swModelDocExt.RenameDocument(compReName);
                            }
                            swModel.ClearSelection2(true);
                            status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            swModel.ClearSelection2(true);
                            if (status)
                            {
                                swComp = swAssy.GetComponentByName(compReName + "-1");
                                swPart = swComp.GetModelDoc2(); //打开零件
                                swPart.Parameter("D1@Sketch1").SystemValue = item.LightPanelLeft / 1000m;
                                swPart.Parameter("D6@Sketch42").SystemValue = (item.LightPanelLeft - 150) / 1000m;
                            }
                            compReName = "FNCE0094[HCLSP-" + tree.Module + ".2]{" + (int)item.LightPanelRight + "}";
                            status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0094-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            if (status)
                            {
                                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0094-1"));
                                swComp.SetSuppression2(2); //2解压缩，0压缩.
                                swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0094-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                                swModelDocExt.RenameDocument(compReName);
                            }
                            swModel.ClearSelection2(true);
                            status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            swModel.ClearSelection2(true);
                            if (status)
                            {
                                swComp = swAssy.GetComponentByName(compReName + "-1");
                                swPart = swComp.GetModelDoc2(); //打开零件
                                swPart.Parameter("D1@Sketch1").SystemValue = item.LightPanelRight / 1000m;
                                swPart.Parameter("D6@Sketch42").SystemValue = (item.LightPanelRight - 150) / 1000m;
                            }
                            break;
                        default:
                            status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0092-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            if (status)
                            {
                                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0092-1"));
                                swComp.SetSuppression2(0); //2解压缩，0压缩.
                            }
                            status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0094-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            if (status)
                            {
                                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0094-1"));
                                swComp.SetSuppression2(0); //2解压缩，0压缩.
                            }
                            break;
                    }

                } //有HCL
                else
                {
                    //没有HCL
                    //NOHCL解压
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0112-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0056-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0110-3"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0110-4"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.

                    //HCL压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0089-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0085-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0090-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0091-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0091-3"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0093-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0092-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0094-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "HCL-1000-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0086-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0086-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern6");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩

                    //排风腔
                    //重命名装配体内部
                    compReName = "FNCE0111[KCJSB535-" + tree.Module + "]{" + (int)item.Length + "}";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0111-2") + "@" + assyName,
                        "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0111-2"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0111-2") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModelDocExt.RenameDocument(compReName);
                    }
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-2" + "@" + assyName, "COMPONENT", 0, 0, 0, false,0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-2");
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D1@Linear austragen1").SystemValue = item.Length / 1000m;
                        //ANSUL
                        if (item.ANSUL == "YES")
                        {
                            //侧喷
                            if (item.ANSide == "LEFT")
                            {
                                swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            }
                            else if (item.ANSide == "RIGHT")
                            {
                                swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            }
                            else
                            {
                                swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            }
                            //探测器
                            if (item.ANDetector == "LEFT")
                            {
                                swFeat = swComp.FeatureByName("ANDTECSIDE RIGHT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANDTECSIDE LEFT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            }
                            else if (item.ANDetector == "RIGHT")
                            {
                                swFeat = swComp.FeatureByName("ANDTECSIDE RIGHT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANDTECSIDE LEFT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            }
                            else if (item.ANDetector == "BOTH")
                            {
                                swFeat = swComp.FeatureByName("ANDTECSIDE RIGHT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANDTECSIDE LEFT");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            }
                            else
                            {
                                swFeat = swComp.FeatureByName("ANDTECSIDE RIGHT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANDTECSIDE LEFT");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            }
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANDTECSIDE RIGHT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANDTECSIDE LEFT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                        //MARVEL
                        if (item.MARVEL == "YES")
                        {
                            swFeat = swComp.FeatureByName("MA-NTC");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("MA-TAB");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("MA-NTC");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("MA-TAB");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                        }
                        //灯腔出线孔
                        if (item.LightCable == "LEFT")
                        {
                            swFeat = swComp.FeatureByName("LIGHT HOLE LEFT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("LIGHT HOLE RIGHT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                        else if (item.LightCable == "RIGHT")
                        {
                            swFeat = swComp.FeatureByName("LIGHT HOLE LEFT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("LIGHT HOLE RIGHT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("LIGHT HOLE LEFT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("LIGHT HOLE RIGHT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                        //小日本的订单
                        if (item.Japan == "YES")
                        {
                            swFeat = swComp.FeatureByName("EX");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("Cut-Extrude4");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("EX");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("Cut-Extrude4");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch1").SystemValue = item.ExRightDis / 1000m;
                            swPart.Parameter("D1@Sketch1").SystemValue = item.ExLength / 1000m;
                            swPart.Parameter("D2@Sketch1").SystemValue = item.ExWidth / 1000m;
                        }
                    }//排风腔结束

                    //----------灯腔----------
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0112-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D1@Linear austragen1").SystemValue = item.Length / 1000m;
                    swFeat = swComp.FeatureByName("FC SUPPORT");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("JAP LED M8");
                    if (item.Japan == "YES") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("LIGHT T8");
                    if (item.LightType == "T8") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    if (item.LightCable == "LEFT")
                    {
                        swFeat = swComp.FeatureByName("LIGHT HOLE LEFT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LIGHT HOLE RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    else if (item.LightCable == "RIGHT")
                    {
                        swFeat = swComp.FeatureByName("LIGHT HOLE LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LIGHT HOLE RIGHT");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("LIGHT HOLE LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LIGHT HOLE RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }

                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0056-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D1@Skizze1").SystemValue = item.Length / 1000m;

                }//HCL结束

                //----------公共零件----------
                //判断FC数量，FC侧板长度
                if (item.FCBlindNo > 0)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0107[BP-500]{500}-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern4");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern4").SystemValue = item.FCBlindNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D1@Distance35").SystemValue = item.FCSideLeft / 1000m;
                    swModel.Parameter("D1@Distance45").SystemValue = item.FCSideLeft / 1000m;
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0107[BP-500]{500}-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern4");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                //判断FC/KSA
                if (item.FCType == "KSA")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5202040401-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern5");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern5").SystemValue = fcNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D1@Distance37").SystemValue = (item.FCSideLeft + 500m * item.FCBlindNo) / 1000m;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "KCJ FC FILTER-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern3");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "KCJ FC FILTER-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern3");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern3").SystemValue = fcNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D1@Distance36").SystemValue = (item.FCSideLeft + 500m * item.FCBlindNo) / 1000m;
                    swModel.Parameter("D1@Distance43").SystemValue = (item.FCSideLeft + 500m * item.FCBlindNo) / 1000m;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5202040401-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern5");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                //----------油网侧板----------
                switch (item.FCSide)
                {
                    case "LEFT":
                        //重命名装配体内部
                        if (item.FCType == "KSA") compReName = "FNCE0108[BP-" + tree.Module + "]{" + (int)(item.FCSideLeft + fcNo * 2.5m) + "}";
                        else compReName = "FNCE0108[BP-" + tree.Module + "]{" + (int)(item.FCSideLeft - fcNo) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0108[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0108[BP-]{}-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0108[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            swModelDocExt.RenameDocument(compReName);
                        }
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swPart = swComp.GetModelDoc2(); //打开零件
                            if (item.FCType == "KSA") swPart.Parameter("D2@草图1").SystemValue = (item.FCSideLeft + fcNo * 2.5m) / 1000m;
                            else swPart.Parameter("D2@草图1").SystemValue = (item.FCSideLeft - fcNo) / 1000m;
                        }
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0109[BP-]{}-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                    case "RIGHT":
                        //重命名装配体内部
                        if (item.FCType == "KSA") compReName = "FNCE0109[BP-" + tree.Module + "]{" + (int)(item.FCSideRight + fcNo * 2.5m) + "}";
                        else compReName = "FNCE0109[BP-" + tree.Module + "]{" + (int)(item.FCSideRight - fcNo) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0109[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0109[BP-]{}-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0109[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            swModelDocExt.RenameDocument(compReName);
                        }
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2(); //打开零件
                            if (item.FCType == "KSA") swPart.Parameter("D2@草图1").SystemValue = (item.FCSideRight + fcNo * 2.5m) / 1000m;
                            else swPart.Parameter("D2@草图1").SystemValue = (item.FCSideRight - fcNo) / 1000m;
                        }
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0108[BP-]{}-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                    case "BOTH":
                        //重命名装配体内部
                        if (item.FCType == "KSA") compReName = "FNCE0108[BP-" + tree.Module + ".1]{" + (int)(item.FCSideLeft + fcNo * 1.25m) + "}";
                        else compReName = "FNCE0108[BP-" + tree.Module + "]{" + (int)(item.FCSideLeft - fcNo / 2m) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0108[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0108[BP-]{}-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0108[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            swModelDocExt.RenameDocument(compReName);
                        }
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2(); //打开零件
                            if (item.FCType == "KSA") swPart.Parameter("D2@草图1").SystemValue = (item.FCSideLeft + fcNo * 1.25m) / 1000m;
                            else swPart.Parameter("D2@草图1").SystemValue = (item.FCSideLeft - fcNo / 2m) / 1000m;
                        }
                        //重命名装配体内部
                        if (item.FCType == "KSA") compReName = "FNCE0109[BP-" + tree.Module + ".2]{" + (int)(item.FCSideRight + fcNo * 1.25m) + "}";
                        else compReName = "FNCE0109[BP-" + tree.Module + "]{" + (int)(item.FCSideRight - fcNo / 2m) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0109[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0109[BP-]{}-1"));
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0109[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                            swModelDocExt.RenameDocument(compReName);
                        }
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2(); //打开零件
                            if (item.FCType == "KSA") swPart.Parameter("D2@草图1").SystemValue = (item.FCSideRight + fcNo * 1.25m) / 1000m;
                            else swPart.Parameter("D2@草图1").SystemValue = (item.FCSideRight - fcNo / 2m) / 1000m;
                        }
                        break;
                    default:
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0108[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0108[BP-]{}-1"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩.
                        }
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0109[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0109[BP-]{}-1"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩.
                        }
                        break;
                }
                //----------SSP灯板支撑条----------
                if (item.SSPType == "DOME")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0036-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0035-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@Sketch1").SystemValue = item.Length / 1000m;
                    if (item.Gutter == "YES") swModel.Parameter("D1@Distance34").SystemValue = item.GutterWidth / 1000m;
                    else swModel.Parameter("D1@Distance34").SystemValue = 0.5m / 1000m;
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0036-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@Sketch1").SystemValue = item.Length / 1000m;
                    if (item.Gutter == "YES") swModel.Parameter("D1@Distance32").SystemValue = item.GutterWidth / 1000m;
                    else swModel.Parameter("D1@Distance32").SystemValue = 0.5m / 1000m;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0035-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                }
                //----------日本项目需要压缩零件----------
                if (item.Japan == "YES")
                {
                    //吊装垫片
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0070-9"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    //排风脖颈
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "EXSPIGOT-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    //排风滑门
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "EXDOOR-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                }//日本压缩零件结束
                else
                {
                    //吊装垫片
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0070-9"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(2, 2, configNames); //参数1：1解压，0压缩
                    //排风脖颈
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "EXSPIGOT-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0019-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = (item.ExLength + 50m) / 1000m;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000m;
                    swFeat = swComp.FeatureByName("ANSUL");
                    if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0020-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = (item.ExLength + 50m) / 1000m;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000m;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0047-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000m;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000m;
                    swFeat = swComp.FeatureByName("ANDTEC");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0048-2"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000m;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000m;
                    swFeat = swComp.FeatureByName("ANDTEC");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                                                               //排风滑门
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "EXDOOR-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D1@Distance3").SystemValue = (item.ExWidth + 20m) / 1000m;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0018-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 2m + 100m) / 1000m;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0013-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D1@Sketch1").SystemValue = (item.ExLength / 2m + 10m) / 1000m;
                    swPart.Parameter("D2@Sketch1").SystemValue = (item.ExWidth + 20m) / 1000m;

                }//非日本解压零件结束

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
