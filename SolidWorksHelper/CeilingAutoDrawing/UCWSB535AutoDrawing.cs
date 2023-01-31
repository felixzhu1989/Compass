﻿using System;
using System.IO;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class UCWSB535AutoDrawing : IAutoDrawing
    {
        Component2 swComp; readonly UCWSB535Service objUCWSB535Service = new UCWSB535Service();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            #region 准备工作
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
            UCWSB535 item = (UCWSB535)objUCWSB535Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

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
            #endregion

            #region 计算中间值
            int fcNo = (int)((item.Length - item.FCSideLeft - item.FCSideRight) / 499d) - item.FCBlindNo;
            //水洗挡板挂钩数量与间距
            int bfSupportNo = 2;
            if (item.Length > 1400) bfSupportNo = 3;
            double bfSupportDis = (item.Length - 300d) / ((bfSupportNo - 1) * 1000d); 
            #endregion

            try
            {
                #region Top Level装配体顶层
                //防水棉
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCO0004[WPSSB535]-1"));
                if (item.SidePanel == "LEFT") swComp.SetSuppression2(2); //2解压缩，0压缩.
                else swComp.SetSuppression2(0); //2解压缩，0压缩.
                //水洗挡板挂钩
                swModel.Parameter("D1@LocalLPattern3").SystemValue = bfSupportNo;
                swModel.Parameter("D3@LocalLPattern3").SystemValue = bfSupportDis;
                //UV灯
                if (item.UVType == "LONG")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "CEILING UVRACK SPECIAL 4L-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "CEILING UVRACK SPECIAL 4S-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "CEILING UVRACK SPECIAL 4L-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "CEILING UVRACK SPECIAL 4S-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                }
                #endregion

                #region 重命名水洗挂管，用于发货清单拉取长度
                compReName = "2200600027[BFHT-" + ((int)item.Length - 30) + "]{" + ((int)item.Length - 30) + "}" + tree.Module;
                status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "2200600027[BFHT-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                if (status) swModelDocExt.RenameDocument(compReName);
                swModel.ClearSelection2(true);
                status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                swModel.ClearSelection2(true);
                if (status)
                {
                    swComp = swAssy.GetComponentByName(compReName + "-1");
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D1@Boss-Extrude1").SystemValue = (item.Length - 30d) / 1000d;
                }
                #endregion

                #region 三角板
                switch (item.SidePanel)
                {
                    case "LEFT":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-4"));
                        if (item.DPSide == "LEFT") swComp.SetSuppression2(2); //2解压缩，0压缩.
                        else swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-5"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-4"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-5"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0044-6"));//LEFT
                        swFeat = swComp.FeatureByName("L1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("L2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("L3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("L4");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DL");
                        if (item.DPSide == "LEFT") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("HCL");
                        if (item.LightType == "HCL") swFeat.SetSuppression2(1, 2, null);
                        else swFeat.SetSuppression2(0, 2, null);
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0045-5"));//RIGHT
                        swFeat = swComp.FeatureByName("R1");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("R2");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("R3");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("R4");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DR");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("HCL");
                        if (item.LightType == "HCL") swFeat.SetSuppression2(1, 2, null);
                        else swFeat.SetSuppression2(0, 2, null);
                        break;

                    case "RIGHT":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-4"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-5"));
                        if (item.DPSide == "RIGHT") swComp.SetSuppression2(2); //2解压缩，0压缩.
                        else swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-4"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-5"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0044-6"));//LEFT
                        swFeat = swComp.FeatureByName("L1");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("L2");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("L3");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("L4");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DL");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("HCL");
                        if (item.LightType == "HCL") swFeat.SetSuppression2(1, 2, null);
                        else swFeat.SetSuppression2(0, 2, null);
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0045-5"));//RIGHT
                        swFeat = swComp.FeatureByName("R1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("R2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("R3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("R4");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DR");
                        if (item.DPSide == "RIGHT") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("HCL");
                        if (item.LightType == "HCL") swFeat.SetSuppression2(1, 2, null);
                        else swFeat.SetSuppression2(0, 2, null);
                        break;

                    case "BOTH":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-4"));
                        if (item.DPSide == "LEFT") swComp.SetSuppression2(2); //2解压缩，0压缩.
                        else swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-5"));
                        if (item.DPSide == "RIGHT") swComp.SetSuppression2(2); //2解压缩，0压缩.
                        else swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-4"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-5"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0044-6"));//LEFT
                        swFeat = swComp.FeatureByName("L1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("L2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("L3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("L4");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DL");
                        if (item.DPSide == "LEFT") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("HCL");
                        if (item.LightType == "HCL") swFeat.SetSuppression2(1, 2, null);
                        else swFeat.SetSuppression2(0, 2, null);
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0045-5"));//RIGHT
                        swFeat = swComp.FeatureByName("R1");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("R2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("R3");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("R4");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DR");
                        if (item.DPSide == "RIGHT") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("HCL");
                        if (item.LightType == "HCL") swFeat.SetSuppression2(1, 2, null);
                        else swFeat.SetSuppression2(0, 2, null);
                        break;

                    default:
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-4"));//3dm板
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-5"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-4"));//边缘挡板
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-5"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0044-6"));//LEFT
                        swFeat = swComp.FeatureByName("L1");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("L2");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("L3");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("L4");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DL");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("HCL");
                        if (item.LightType == "HCL") swFeat.SetSuppression2(1, 2, null);
                        else swFeat.SetSuppression2(0, 2, null);
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0045-5"));//RIGHT
                        swFeat = swComp.FeatureByName("R1");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("R2");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("R3");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("R4");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DR");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("HCL");
                        if (item.LightType == "HCL") swFeat.SetSuppression2(1, 2, null);
                        else swFeat.SetSuppression2(0, 2, null);
                        break;
                }
                #endregion

                #region 判断FC数量，FC侧板长度
                if (item.FCBlindNo > 0)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0037[BP-500]{500}-4"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern4");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern4").SystemValue = item.FCBlindNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D1@Distance82").SystemValue = item.FCSideLeft / 1000d;
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0037[BP-500]{500}-4"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern4");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2200600002-1"));
                swComp.SetSuppression2(2); //2解压缩，0压缩.
                swFeat = swAssy.FeatureByName("LocalLPattern5");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                swModel.Parameter("D1@LocalLPattern5").SystemValue = fcNo; //D1阵列数量,D3阵列距离
                swModel.Parameter("D1@Distance80").SystemValue = (item.FCSideLeft + 500d * item.FCBlindNo) / 1000d;
                #endregion

                #region 油网侧板
                switch (item.FCSide)
                {
                    case "LEFT":
                        //重命名装配体内部
                        compReName = "FNCE0058[BP-" + tree.Module + "]{" + ((int)item.FCSideLeft - 3) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0058[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status) swModelDocExt.RenameDocument(compReName);
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2();//打开零件
                            swPart.Parameter("D2@Sketch1").SystemValue = (item.FCSideLeft - 3) / 1000d;
                            if (item.FCSideLeft < 100d)
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            }
                            else
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            }
                        }
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0059[BP-]{}-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                    case "RIGHT":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0058[BP-]{}-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        //重命名装配体内部
                        compReName = "FNCE0059[BP-" + tree.Module + "]{" + ((int)item.FCSideRight - 3) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0059[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status) swModelDocExt.RenameDocument(compReName);
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2();//打开零件
                            swPart.Parameter("D2@Sketch1").SystemValue = (item.FCSideRight - 3) / 1000d;
                            if (item.FCSideLeft < 100d)
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            }
                            else
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            }
                        }
                        break;
                    case "BOTH":
                        //重命名装配体内部
                        compReName = "FNCE0058[BP-" + tree.Module + ".1]{" + ((int)item.FCSideLeft - 1.5) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0058[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status) swModelDocExt.RenameDocument(compReName);
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2();//打开零件
                            swPart.Parameter("D2@Sketch1").SystemValue = (item.FCSideLeft - 1.5) / 1000d;
                            if (item.FCSideLeft < 100d)
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            }
                            else
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            }
                        }
                        //重命名装配体内部
                        compReName = "FNCE0059[BP-" + tree.Module + ".2]{" + ((int)item.FCSideRight - 1.5) + "}";
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0059[BP-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        if (status) swModelDocExt.RenameDocument(compReName);
                        swModel.ClearSelection2(true);
                        status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-1");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                            swPart = swComp.GetModelDoc2();//打开零件
                            swPart.Parameter("D2@Sketch1").SystemValue = (item.FCSideRight -1.5) / 1000d;
                            if (item.FCSideLeft < 100d)
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            }
                            else
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            }
                        }
                        break;
                    default:
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0058[BP-]{}-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0059[BP-]{}-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                }
                #endregion

                #region HCL零件解压与压缩
                //一次性解压与压缩HCL配件
                if (item.LightType == "HCL")
                {
                    //压缩NOHCL
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0156-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0034-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    //解压HCL
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0080-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0081-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0081-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020417-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                }
                else
                {
                    //压缩NOHCL
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0034-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    //解压HCL
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0095-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0080-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0081-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0081-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020417-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                }
                #endregion

                #region 排风腔-HCL/NOHCL
                if (item.LightType == "HCL")
                {
                    //重命名排风腔体，写入排风腔信息，供发货清单导出
                    compReName = "FNCE0095[UCWSB535-" + tree.Module + "]{" + (int)item.Length + "}";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0095-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0095-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0095-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModelDocExt.RenameDocument(compReName);
                    }
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swPart = swComp.GetModelDoc2(); //打开零件
                        //公共的
                        swPart.Parameter("D1@Aufsatz-Linear austragen1").SystemValue = item.Length / 1000d;
                        swFeat = swComp.FeatureByName("UV");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D5@Sketch20").SystemValue = item.ExRightDis / 1000d;
                        if (item.UVType == "LONG") swPart.Parameter("D1@Sketch20").SystemValue = 1600d / 1000d;
                        else swPart.Parameter("D1@Sketch20").SystemValue = 893d / 1000d;
                        //水洗挡板磁感应
                        if (item.SensorNo == 1)
                        {
                            swFeat = swComp.FeatureByName("HALL SENSOR HOLE");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch23").SystemValue = item.SensorDis1 / 1000d;
                            swFeat = swComp.FeatureByName("SENSOR CABLE HOLE");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch24").SystemValue = item.SensorDis1 / 1000d;
                            swFeat = swComp.FeatureByName("LPattern2");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        else if (item.SensorNo > 1)
                        {
                            swFeat = swComp.FeatureByName("HALL SENSOR HOLE");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch23").SystemValue = item.SensorDis1 / 1000d;
                            swFeat = swComp.FeatureByName("SENSOR CABLE HOLE");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch24").SystemValue = item.SensorDis1 / 1000d;
                            swFeat = swComp.FeatureByName("LPattern2");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D1@LPattern2").SystemValue = item.SensorNo;
                            swPart.Parameter("D3@LPattern2").SystemValue = item.SensorDis2 / 1000d;
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("HALL SENSOR HOLE");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("SENSOR CABLE HOLE");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("LPattern2");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                        }
                        if (item.ANSUL == "YES")
                        {
                            //侧喷
                            if (item.ANSide == "LEFT")
                            {
                                swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            }
                            else if (item.ANSide == "RIGHT")
                            {
                                swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            }
                            else
                            {
                                swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            }
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        if (item.MARVEL == "YES")
                        {
                            swFeat = swComp.FeatureByName("MA-NTC");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D1@Sketch27").SystemValue =
                                (item.ExRightDis + item.ExLength / 2d + 50d) / 1000d;
                            swFeat = swComp.FeatureByName("MA-TAB");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("MA-NTC");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("MA-TAB");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                        }
                        swFeat = swComp.FeatureByName("L");
                        if (item.SidePanel == "LEFT" || item.SidePanel == "BOTH")
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("R");
                        if (item.SidePanel == "RIGHT" || item.SidePanel == "BOTH")
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        //HCL侧板磁铁孔
                        if (item.HCLSide == "LEFT" || item.HCLSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("Cut-Extrude7");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D6@Sketch42").SystemValue = (item.HCLSideLeft - 103) / 1000d;
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("Cut-Extrude7");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        if (item.HCLSide == "RIGHT" || item.HCLSide == "BOTH")
                        {
                            swFeat = swComp.FeatureByName("Cut-Extrude8");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                            swPart.Parameter("D6@Sketch43").SystemValue = (item.HCLSideRight - 103) / 1000d;
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("Cut-Extrude8");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        //日本天花
                        if (item.Japan == "YES")
                        {
                            swFeat = swComp.FeatureByName("EX");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("Cut-Extrude4");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("EX");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("Cut-Extrude4");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D1@Sketch14").SystemValue = item.ExRightDis / 1000d;
                            swPart.Parameter("D5@Sketch14").SystemValue = item.ExLength / 1000d;
                            swPart.Parameter("D6@Sketch14").SystemValue = item.ExWidth / 1000d;
                        }
                    }
                }
                else
                {
                    //重命名排风腔体，写入排风腔信息，供发货清单导出
                    compReName = "FNCE0156[UCWSB535-" + tree.Module + "]{" + (int)item.Length + "}";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0156-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0156-1"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0156-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModelDocExt.RenameDocument(compReName);
                    }
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swPart = swComp.GetModelDoc2(); //打开零件
                        //公共的
                        swPart.Parameter("D1@Aufsatz-Linear austragen1").SystemValue = item.Length / 1000d;
                        swFeat = swComp.FeatureByName("UV");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D5@Sketch20").SystemValue = item.ExRightDis / 1000d;
                        if (item.UVType == "LONG") swPart.Parameter("D1@Sketch20").SystemValue = 1600d / 1000d;
                        else swPart.Parameter("D1@Sketch20").SystemValue = 893d / 1000d;
                        //水洗挡板磁感应
                        if (item.SensorNo == 1)
                        {
                            swFeat = swComp.FeatureByName("HALL SENSOR HOLE");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch23").SystemValue = item.SensorDis1 / 1000d;
                            swFeat = swComp.FeatureByName("SENSOR CABLE HOLE");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch24").SystemValue = item.SensorDis1 / 1000d;
                            swFeat = swComp.FeatureByName("LPattern2");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        else if (item.SensorNo > 1)
                        {
                            swFeat = swComp.FeatureByName("HALL SENSOR HOLE");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch23").SystemValue = item.SensorDis1 / 1000d;
                            swFeat = swComp.FeatureByName("SENSOR CABLE HOLE");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch24").SystemValue = item.SensorDis1 / 1000d;
                            swFeat = swComp.FeatureByName("LPattern2");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D1@LPattern2").SystemValue = item.SensorNo;
                            swPart.Parameter("D3@LPattern2").SystemValue = item.SensorDis2 / 1000d;
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("HALL SENSOR HOLE");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("SENSOR CABLE HOLE");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("LPattern2");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                        }
                        if (item.ANSUL == "YES")
                        {
                            //侧喷
                            if (item.ANSide == "LEFT")
                            {
                                swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            }
                            else if (item.ANSide == "RIGHT")
                            {
                                swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            }
                            else
                            {
                                swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            }
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        if (item.MARVEL == "YES")
                        {
                            swFeat = swComp.FeatureByName("MA-NTC");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D1@Sketch27").SystemValue =
                                (item.ExRightDis + item.ExLength / 2d + 50d) / 1000d;
                            swFeat = swComp.FeatureByName("MA-TAB");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("MA-NTC");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("MA-TAB");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                        }
                        swFeat = swComp.FeatureByName("L");
                        if (item.SidePanel == "LEFT" || item.SidePanel == "BOTH")
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("R");
                        if (item.SidePanel == "RIGHT" || item.SidePanel == "BOTH")
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        //日本天花
                        if (item.Japan == "YES")
                        {
                            swFeat = swComp.FeatureByName("EX");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("Cut-Extrude4");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("EX");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("Cut-Extrude4");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D1@Sketch14").SystemValue = item.ExRightDis / 1000d;
                            swPart.Parameter("D5@Sketch14").SystemValue = item.ExLength / 1000d;
                            swPart.Parameter("D6@Sketch14").SystemValue = item.ExWidth / 1000d;
                        }
                    }
                }
                #endregion

                #region HCL其余配件长度修改
                if (item.LightType == "HCL")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0080-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D1@Skizze1").SystemValue = item.Length / 1000d;
                    //HCL侧板磁铁孔
                    if (item.HCLSide == "LEFT" || item.HCLSide == "BOTH")
                    {
                        swFeat = swComp.FeatureByName("Cut-Extrude9");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D6@Sketch25").SystemValue = (item.HCLSideLeft - 103) / 1000d;
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("Cut-Extrude9");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                    if (item.HCLSide == "RIGHT" || item.HCLSide == "BOTH")
                    {
                        swFeat = swComp.FeatureByName("Cut-Extrude7");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                        swPart.Parameter("D1@Sketch23").SystemValue = (item.HCLSideRight - 103) / 1000d;
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("Cut-Extrude7");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0081-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    switch (item.HCLSide)
                    {
                        case "LEFT":
                            swPart.Parameter("D1@Skizze1").SystemValue = (item.Length - item.HCLSideLeft) / 1000d;
                            //2023/01/31,代码与模型不匹配，更改模型，D1@Distance94
                            //swModel.Parameter("D1@Distance94").SystemValue = item.HCLSideLeft / 1000d;
                            break;
                        case "RIGHT":
                            swPart.Parameter("D1@Skizze1").SystemValue = (item.Length - item.HCLSideRight) / 1000d;
                            //swModel.Parameter("D1@Distance94").SystemValue = 0d;
                            break;
                        case "BOTH":
                            swPart.Parameter("D1@Skizze1").SystemValue = (item.Length - item.HCLSideLeft - item.HCLSideRight) / 1000d;
                            //swModel.Parameter("D1@Distance94").SystemValue = item.HCLSideLeft / 1000d;
                            break;
                        default:
                            swPart.Parameter("D1@Skizze1").SystemValue = item.Length / 1000d;
                            //swModel.Parameter("D1@Distance94").SystemValue = 0d;
                            break;
                    }
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0034-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D1@Skizze1").SystemValue = item.Length / 1000d;
                }
                #endregion

                #region HCL侧板
                if (item.LightType == "HCL")
                { //----------HCL侧板----------
                    switch (item.HCLSide)
                    {
                        case "LEFT":
                            swModel.Parameter("D1@Distance89").SystemValue = item.HCLSideLeft/1000d;
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
                                swPart.Parameter("D1@Sketch1").SystemValue = (item.HCLSideLeft - 3d) / 1000d;
                            }
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0083-1"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩.
                            break;
                        case "RIGHT":
                            swModel.Parameter("D1@Distance89").SystemValue =0.5d / 1000d;
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
                                swPart.Parameter("D1@Sketch1").SystemValue = (item.HCLSideRight - 3d) / 1000d;
                            }
                            break;
                        case "BOTH":
                            swModel.Parameter("D1@Distance89").SystemValue = item.HCLSideLeft / 1000d;
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
                                swPart.Parameter("D1@Sketch1").SystemValue = (item.HCLSideLeft - 3d) / 1000d;
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
                                swPart.Parameter("D1@Sketch1").SystemValue = (item.HCLSideRight - 3d) / 1000d;
                            }
                            break;
                        default:
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0082-1"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩.
                            swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0083-1"));
                            swComp.SetSuppression2(0); //2解压缩，0压缩.
                            break;
                    }
                    //镀锌铁片数量
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0084-1"));
                    if (item.HCLSide == "NO") swComp.SetSuppression2(0); //2解压缩，0压缩.
                    else swComp.SetSuppression2(2); //2解压缩，0压缩.
                    if (item.HCLSide == "NO")
                    {
                        swFeat = swAssy.FeatureByName("LocalLPattern6");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                    else if (item.HCLSide == "BOTH")
                    {
                        swFeat = swAssy.FeatureByName("LocalLPattern6");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swModel.Parameter("D1@LocalLPattern6").SystemValue = 8;
                    }
                    else
                    {
                        swFeat = swAssy.FeatureByName("LocalLPattern6");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swModel.Parameter("D1@LocalLPattern6").SystemValue = 4;
                    }
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0082-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0083-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0084-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern6");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                #endregion

                #region 灯腔零件
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0031-1"));
                swPart = swComp.GetModelDoc2();//打开零件
                swPart.Parameter("D1@Aufsatz-Linear austragen1").SystemValue = item.Length / 1000d;
                swPart.Parameter("D1@LPattern1").SystemValue = bfSupportNo;
                swPart.Parameter("D3@LPattern1").SystemValue = bfSupportDis;
                swFeat = swComp.FeatureByName("Cut-Extrude5");
                if (item.LightType == "HCL") swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                if (item.Japan == "YES")
                {
                    swFeat = swComp.FeatureByName("FC RAIL");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("LIGHT T8");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("JAP LED M8");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    switch (item.FCSide)
                    {
                        case "LEFT":
                            swFeat = swComp.FeatureByName("FC LEFT");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D6@Sketch10").SystemValue = (item.FCSideLeft - 50d) / 1000d;
                            swFeat = swComp.FeatureByName("FC RIGHT");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            break;
                        case "RIGHT":
                            swFeat = swComp.FeatureByName("FC LEFT");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("FC RIGHT");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D6@Sketch11").SystemValue = (item.FCSideRight - 50d) / 1000d;
                            break;
                        case "BOTH":
                            swFeat = swComp.FeatureByName("FC LEFT");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D6@Sketch10").SystemValue = (item.FCSideLeft - 50d) / 1000d;
                            swFeat = swComp.FeatureByName("FC RIGHT");
                            swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                            swPart.Parameter("D6@Sketch11").SystemValue = (item.FCSideRight - 50d) / 1000d;
                            break;
                        default:
                            swFeat = swComp.FeatureByName("FC LEFT");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("FC RIGHT");
                            swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                            break;
                    }
                    swFeat = swComp.FeatureByName("FC FIRST");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    if (item.FCSide == "RIGHT") swPart.Parameter("D3@Sketch12").SystemValue = 25d / 1000d;
                    else swPart.Parameter("D3@Sketch12").SystemValue = (item.FCSideLeft + 25d) / 1000d;
                    swFeat = swComp.FeatureByName("LPattern2");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern2").SystemValue = fcNo + item.FCBlindNo;
                }
                else
                {
                    swFeat = swComp.FeatureByName("FC RAIL");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("LIGHT T8");
                    if (item.LightType == "T8") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("JAP LED M8");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("FC LEFT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("FC RIGHT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("FC FIRST");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("LPattern2");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩  
                }
                #endregion

                #region 日本项目需要压缩零件
                if (item.Japan == "YES")
                {
                    //吊装垫片
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0070-15"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    //排风脖颈
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "EXSPIGOT-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    //FC下导轨
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0009-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                }
                else
                {
                    //吊装垫片
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0070-15"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(2, 2, null); //参数1：1解压，0压缩
                    //排风脖颈
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "EXSPIGOT-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0019-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = (item.ExLength + 50d) / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                    swFeat = swComp.FeatureByName("ANSUL");
                    if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0020-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = (item.ExLength + 50d) / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0047-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                    swFeat = swComp.FeatureByName("ANDTEC");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0048-2"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                    swFeat = swComp.FeatureByName("ANDTEC");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    //FC下导轨
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0009-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D1@Sketch2").SystemValue = (item.Length - 5d) / 1000d;
                }
                #endregion

                #region 其余零件
                //----------FC上导轨----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0003-1"));
                swPart = swComp.GetModelDoc2();//打开零件
                swPart.Parameter("D1@Aufsatz-Linear austragen1").SystemValue = item.Length / 1000d;
                swFeat = swComp.FeatureByName("L");
                if (item.SidePanel == "LEFT" || item.SidePanel == "BOTH") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("R");
                if (item.SidePanel == "RIGHT" || item.SidePanel == "BOTH") swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0007-1"));
                swPart = swComp.GetModelDoc2();//打开零件
                swPart.Parameter("D1@Skizze1").SystemValue = (item.Length - 5d) / 1000d;
                swFeat = swComp.FeatureByName("PIPE-UP");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0008-1"));
                swPart = swComp.GetModelDoc2();//打开零件
                swPart.Parameter("D1@Skizze1").SystemValue = (item.Length - 5d) / 1000d;
                swFeat = swComp.FeatureByName("PIPE-UP");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                //----------SSP灯板支撑条----------
                if (item.SSPType == "DOME")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0036-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0035-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@Sketch1").SystemValue = item.Length / 1000d;
                    if (item.Gutter == "YES") swModel.Parameter("D1@Distance72").SystemValue = item.GutterWidth / 1000d;
                    else swModel.Parameter("D1@Distance72").SystemValue = 0.5d / 1000d;
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0036-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@Sketch1").SystemValue = item.Length / 1000d;
                    if (item.Gutter == "YES") swModel.Parameter("D1@Distance75").SystemValue = item.GutterWidth / 1000d;
                    else swModel.Parameter("D1@Distance75").SystemValue = 0.5d / 1000d;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0035-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
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
