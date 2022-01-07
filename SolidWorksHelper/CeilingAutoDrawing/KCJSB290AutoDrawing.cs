﻿using System;
using System.IO;
using System.Windows.Forms;
using Common;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class KCJSB290AutoDrawing : IAutoDrawing
    {
        readonly KCJSB290Service objKCJSB290Service = new KCJSB290Service();
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
            KCJSB290 item = (KCJSB290)objKCJSB290Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

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
            int fcNo = (int)((item.Length - item.FCSideLeft - item.FCSideRight) / 499d) - item.FCBlindNo;

            try
            {
                //----------Top Level----------
                //判断FC数量，FC侧板长度
                if (item.FCBlindNo > 0)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0107[BP-500]{500}-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern3");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern3").SystemValue = item.FCBlindNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D1@Distance9").SystemValue = item.FCSideLeft / 1000d;
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0107[BP-500]{500}-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern3");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                //判断FC/KSA
                if (item.FCType == "KSA")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5202040401-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern2").SystemValue = fcNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D1@Distance4").SystemValue = (item.FCSideLeft + 500d * item.FCBlindNo) / 1000d;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "KCJ FC FILTER-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "KCJ FC FILTER-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern1").SystemValue = fcNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D1@Distance3").SystemValue = (item.FCSideLeft + 500d * item.FCBlindNo) / 1000d;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5202040401-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                //----------油网侧板----------
                switch (item.FCSide)
                {
                    case "LEFT":
                        //重命名装配体内部
                        if (item.FCType == "KSA") compReName = "FNCE0108[BP-" + tree.Module + "]{" + (int)(item.FCSideLeft + fcNo * 2.5d) + "}";
                        else compReName = "FNCE0108[BP-" + tree.Module + "]{" + (int)(item.FCSideLeft - fcNo) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0108[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status) swModelDocExt.RenameDocument(compReName);
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2(); //打开零件
                            if (item.FCType == "KSA") swPart.Parameter("D2@草图1").SystemValue = (item.FCSideLeft + fcNo * 2.5d) / 1000d;
                            else swPart.Parameter("D2@草图1").SystemValue = (item.FCSideLeft - fcNo) / 1000d;
                        }
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0109[BP-]{}-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                    case "RIGHT":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0108[BP-]{}-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        //重命名装配体内部
                        if (item.FCType == "KSA") compReName = "FNCE0109[BP-" + tree.Module + "]{" + (int)(item.FCSideRight + fcNo * 2.5d) + "}";
                        else compReName = "FNCE0109[BP-" + tree.Module + "]{" + (int)(item.FCSideRight - fcNo) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0109[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status) swModelDocExt.RenameDocument(compReName);
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2(); //打开零件
                            if (item.FCType == "KSA") swPart.Parameter("D2@草图1").SystemValue = (item.FCSideRight + fcNo * 2.5d) / 1000d;
                            else swPart.Parameter("D2@草图1").SystemValue = (item.FCSideRight - fcNo) / 1000d;
                        }
                        break;
                    case "BOTH":
                        //重命名装配体内部
                        if (item.FCType == "KSA") compReName = "FNCE0108[BP-" + tree.Module + ".1]{" + (int)(item.FCSideLeft + fcNo * 1.25d) + "}";
                        else compReName = "FNCE0108[BP-" + tree.Module + "]{" + (int)(item.FCSideLeft - fcNo /2d) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0108[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status) swModelDocExt.RenameDocument(compReName);
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2(); //打开零件
                            if (item.FCType == "KSA") swPart.Parameter("D2@草图1").SystemValue = (item.FCSideLeft + fcNo * 1.25d) / 1000d;
                            else swPart.Parameter("D2@草图1").SystemValue = (item.FCSideLeft - fcNo / 2d) / 1000d;
                        }
                        //重命名装配体内部
                        if (item.FCType == "KSA") compReName = "FNCE0109[BP-" + tree.Module + ".2]{" + (int)(item.FCSideRight + fcNo * 1.25d) + "}";
                        else compReName = "FNCE0109[BP-" + tree.Module + "]{" + (int)(item.FCSideRight - fcNo / 2d) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0109[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status) swModelDocExt.RenameDocument(compReName);
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2(); //打开零件
                            if (item.FCType == "KSA") swPart.Parameter("D2@草图1").SystemValue = (item.FCSideRight + fcNo * 1.25d) / 1000d;
                            else swPart.Parameter("D2@草图1").SystemValue = (item.FCSideRight - fcNo / 2d) / 1000d;
                        }
                        break;
                    default:
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0108[BP-]{}-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0109[BP-]{}-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                }
                //----------日本项目需要压缩零件----------
                if (item.Japan == "YES")
                {
                    //吊装垫片
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0070-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern4");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    //排风脖颈
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "EXSPIGOT-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    //排风滑门
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "EXDOOR-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    //排风腔
                    //重命名装配体内部
                    compReName = "FNCE0127[KCJSB290-" + tree.Module + "]{" + (int)item.Length + "}";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0127-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D2@Base-Flange1").SystemValue = item.Length / 1000d;
                        swFeat = swComp.FeatureByName("EX");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("Cut-Extrude4");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTECSIDE RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANDTECSIDE LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
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
                    }
                }
                else
                {
                    //吊装垫片
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0070-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern4");
                    swFeat.SetSuppression2(2, 2, configNames); //参数1：1解压，0压缩
                    //排风脖颈
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "EXSPIGOT-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0019-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = (item.ExLength + 50d) / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                    swFeat = swComp.FeatureByName("ANSUL");
                    if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0020-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = (item.ExLength + 50d) / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0047-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                    swFeat = swComp.FeatureByName("ANDTEC");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0048-2"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                    swFeat = swComp.FeatureByName("ANDTEC");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    //排风滑门
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "EXDOOR-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D1@Distance3").SystemValue = (item.ExWidth + 20d) / 1000d;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0018-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 2d + 100d) / 1000d;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0013-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D1@Sketch1").SystemValue = (item.ExLength / 2d + 10d) / 1000d;
                    swPart.Parameter("D2@Sketch1").SystemValue = (item.ExWidth + 20d) / 1000d;
                    //排风腔
                    //重命名装配体内部
                    compReName = "FNCE0127[KCJSB290-" + tree.Module + "]{" + (int)item.Length + "}";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0127-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D2@Base-Flange1").SystemValue = item.Length / 1000d;
                        swFeat = swComp.FeatureByName("EX");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("Cut-Extrude4");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch6").SystemValue = item.ExRightDis / 1000d;
                        swPart.Parameter("D2@Sketch6").SystemValue = item.ExLength / 1000d;
                        swPart.Parameter("D1@Sketch6").SystemValue = item.ExWidth / 1000d;
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
                    }
                }
                //----------SSP灯板支撑条----------
                if (item.SSPType == "DOME")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0036-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0035-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@Sketch1").SystemValue = item.Length / 1000d;
                    if (item.Gutter == "YES") swModel.Parameter("D1@Distance5").SystemValue = item.GutterWidth / 1000d;
                    else swModel.Parameter("D1@Distance5").SystemValue = 0.5d / 1000d;
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0036-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@Sketch1").SystemValue = item.Length / 1000d;
                    if (item.Gutter == "YES") swModel.Parameter("D1@Distance7").SystemValue = item.GutterWidth / 1000d;
                    else swModel.Parameter("D1@Distance7").SystemValue = 0.5d / 1000d;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0035-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
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
