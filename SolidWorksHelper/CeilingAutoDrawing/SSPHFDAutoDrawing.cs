using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class SSPHFDAutoDrawing : IAutoDrawing
    {
        SSPHFDService objSSPHFDService = new SSPHFDService();
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
            SSPHFD item = (SSPHFD)objSSPHFDService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

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
            int supportNo = item.MPanelNo * 2 - 1;//计算支撑板数量
            if (item.LeftType == "Z" && item.RightType == "Z") supportNo = supportNo * 2 + 8;
            else if (item.LeftType == "W" && item.RightType == "W") supportNo = supportNo * 2 + 4;
            else if ((item.LeftType == "Z" && item.RightType == "W") || (item.LeftType == "W" && item.RightType == "Z")) supportNo = supportNo * 2 + 6;

            try
            {
                //----------Top Level----------
                swModel.Parameter("D1@LocalLPattern3").SystemValue = supportNo;

                //----------边缘Z板----------
                swModel.Parameter("D1@Distance20").SystemValue = (item.MPanelNo * 2m - 1m) * 500m / 1000m;
                //左边
                if (item.LeftType == "Z")
                {
                    //重命名装配体内部
                    compReName = "FNCM0016[SSPDZ-" + tree.Module + ".L]{" + (int)(item.Length - 10m) + "}(" + (int)item.LeftLength + ")";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCM0016-L[SSPDZ-]{}()-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Length -410m) / 1000m;
                        swPart.Parameter("D1@Skizze1").SystemValue = item.LeftLength / 1000m;
                        swFeat = swComp.FeatureByName("LED");
                        if (item.LightType == "LED60") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //重命名装配体内部
                    compReName = "FNCM0014[SSPDW-" + tree.Module + ".L]{" + (int)(item.Length - 10m) + "}(500)";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCM0014-L[SSPDW-]{}()-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Length -410m) / 1000m;
                        swPart.Parameter("D1@Skizze1").SystemValue = 500m / 1000m;
                        swFeat = swComp.FeatureByName("LED");
                        if (item.LightType == "LED60") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCM0016-L[SSPDZ-]{}()-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    //重命名装配体内部
                    compReName = "FNCM0014[SSPDW-" + tree.Module + ".L]{" + (int)(item.Length - 10m) + "}(" + (int)item.LeftLength + ")";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCM0014-L[SSPDW-]{}()-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Length -410m) / 1000m;
                        swPart.Parameter("D1@Skizze1").SystemValue = item.LeftLength / 1000m;
                        swFeat = swComp.FeatureByName("LED");
                        if (item.LightType == "LED60") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                }
                //右边
                if (item.RightType == "Z")
                {
                    //重命名装配体内部
                    compReName = "FNCM0016[SSPDZ-" + tree.Module + ".R]{" + (int)(item.Length - 10m) + "}(" + (int)item.RightLength + ")";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCM0016-R[SSPDZ-]{}()-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Length -410m) / 1000m;
                        swPart.Parameter("D1@Skizze1").SystemValue = item.RightLength / 1000m;
                        swFeat = swComp.FeatureByName("LED");
                        if (item.LightType == "LED60") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //重命名装配体内部
                    compReName = "FNCM0014[SSPDW-" + tree.Module + ".R]{" + (int)(item.Length - 10m) + "}(500)";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCM0014-R[SSPDW-]{}()-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Length -410m) / 1000m;
                        swPart.Parameter("D1@Skizze1").SystemValue = 500m / 1000m;
                        swFeat = swComp.FeatureByName("LED");
                        if (item.LightType == "LED60") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCM0016-R[SSPDZ-]{}()-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    //重命名装配体内部
                    compReName = "FNCM0014[SSPDW-" + tree.Module + ".R]{" + (int)(item.Length - 10m) + "}(" + (int)item.RightLength + ")";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCM0014-R[SSPDW-]{}()-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Length -410m) / 1000m;
                        swPart.Parameter("D1@Skizze1").SystemValue = item.RightLength / 1000m;
                        swFeat = swComp.FeatureByName("LED");
                        if (item.LightType == "LED60") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                }
                //----------标准M板----------
                //重命名装配体内部
                compReName = "FNCM0015[SSPDM-" + tree.Module + "]{" + (int)(item.Length - 10m) + "}(500)";
                status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCM0015[SSPDM-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                if (status) swModelDocExt.RenameDocument(compReName);
                swModel.ClearSelection2(true);
                status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                swModel.ClearSelection2(true);
                if (status)
                {
                    swComp = swAssy.GetComponentByName(compReName + "-1");
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swPart = swComp.GetModelDoc2(); //打开零件
                    swPart.Parameter("D2@Skizze1").SystemValue = (item.Length -410m) / 1000m;
                    swPart.Parameter("D1@Skizze1").SystemValue = 500m / 1000m;
                    swFeat = swComp.FeatureByName("LED");
                    if (item.LightType == "LED60") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                //----------标准W板----------
                if (item.MPanelNo == 1)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCM0014[SSPDW-]{}-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                else
                {
                    //重命名装配体内部
                    compReName = "FNCM0014[SSPDW-" + tree.Module + "]{" + (int)(item.Length - 10m) + "}(500)";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCM0014[SSPDW-]{}-1") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D2@Skizze1").SystemValue = (item.Length -410m) / 1000m;
                        swPart.Parameter("D1@Skizze1").SystemValue = 500m / 1000m;
                        swFeat = swComp.FeatureByName("LED");
                        if (item.LightType == "LED60") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    swFeat = swAssy.FeatureByName("LocalLPattern2");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern2").SystemValue = item.MPanelNo;
                    swModel.Parameter("D1@LocalLPattern1").SystemValue = item.MPanelNo - 1;
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
