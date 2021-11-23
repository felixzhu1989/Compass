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
    public class UCWDB800AutoDrawing : IAutoDrawing
    {
        readonly UCWDB800Service objUCWDB800Service = new UCWDB800Service();
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
            UCWDB800 item = (UCWDB800)objUCWDB800Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

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
            //水洗挡板挂钩数量与间距
            int bfSupportNo = 2;
            if (item.Length > 1400) bfSupportNo = 3;
            decimal bfSupportDis = (item.Length - 300m) / ((bfSupportNo - 1) * 1000m);
            try
            {
                //----------Top Level----------
                //防水棉
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCO0005[WPSDB800]-2"));
                if (item.SidePanel == "LEFT") swComp.SetSuppression2(2); //2解压缩，0压缩.
                else swComp.SetSuppression2(0); //2解压缩，0压缩.
                //水洗挡板挂钩,水洗挂管长度
                swModel.Parameter("D1@LocalLPattern3").SystemValue = bfSupportNo;
                swModel.Parameter("D3@LocalLPattern3").SystemValue = bfSupportDis;
                //UV灯
                if (item.UVType == "LONG")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "CEILING UVRACK SPECIAL 4L-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "CEILING UVRACK SPECIAL 4S-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "CEILING UVRACK SPECIAL 4L-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "CEILING UVRACK SPECIAL 4S-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                }

                //重命名装配体内部
                compReName = "2200600027[BFHT-" + ((int)item.Length - 30) + "]{" + ((int)item.Length - 30) + "}"+tree.Module;
                status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "2200600027[BFHT-]{}-3") + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                if (status) swModelDocExt.RenameDocument(compReName);
                swModel.ClearSelection2(true);
                status = swModelDocExt.SelectByID2(compReName + "-3" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                swModel.ClearSelection2(true);
                if (status)
                {
                    swComp = swAssy.GetComponentByName(compReName + "-3");
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D1@Boss-Extrude1").SystemValue = (item.Length - 30m) / 1000m;
                }
                //三角板
                switch (item.SidePanel)
                {
                    case "LEFT":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-8"));
                        if (item.DPSide == "LEFT") swComp.SetSuppression2(2); //2解压缩，0压缩.
                        else swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-7"));
                        if (item.DPSide == "LEFT") swComp.SetSuppression2(2); //2解压缩，0压缩.
                        else swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-10"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-9"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-5"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-8"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-9"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-10"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0062-3"));//LEFT
                        swFeat = swComp.FeatureByName("LF1");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LF2");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LF3");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LF4");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DLF");
                        if (item.DPSide == "LEFT") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB1");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB2");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB3");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB4");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DLB");
                        if (item.DPSide == "LEFT") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0063-2"));//RIGHT
                        swFeat = swComp.FeatureByName("RF1");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RF2");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RF3");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RF4");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DRF");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB1");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB2");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB3");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB4");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DRB");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        break;
                    case "RIGHT":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-8"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-7"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-10"));
                        if (item.DPSide == "RIGHT") swComp.SetSuppression2(2); //2解压缩，0压缩.
                        else swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-9"));
                        if (item.DPSide == "RIGHT") swComp.SetSuppression2(2); //2解压缩，0压缩.
                        else swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-5"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-8"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-9"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-10"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0062-3"));//LEFT
                        swFeat = swComp.FeatureByName("LF1");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LF2");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LF3");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LF4");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DLF");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB1");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB2");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB3");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB4");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DLB");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0063-2"));//RIGHT
                        swFeat = swComp.FeatureByName("RF1");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RF2");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RF3");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RF4");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DRF");
                        if (item.DPSide == "RIGHT") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB1");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB2");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB3");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB4");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DRB");
                        if (item.DPSide == "RIGHT") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        break;
                    case "BOTH":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-8"));
                        if (item.DPSide == "LEFT") swComp.SetSuppression2(2); //2解压缩，0压缩.
                        else swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-7"));
                        if (item.DPSide == "LEFT") swComp.SetSuppression2(2); //2解压缩，0压缩.
                        else swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-10"));
                        if (item.DPSide == "RIGHT") swComp.SetSuppression2(2); //2解压缩，0压缩.
                        else swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-9"));
                        if (item.DPSide == "RIGHT") swComp.SetSuppression2(2); //2解压缩，0压缩.
                        else swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-5"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-8"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-9"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-10"));
                        swComp.SetSuppression2(2); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0062-3"));//LEFT
                        swFeat = swComp.FeatureByName("LF1");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LF2");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LF3");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LF4");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DLF");
                        if (item.DPSide == "LEFT") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB1");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB2");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB3");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB4");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DLB");
                        if (item.DPSide == "LEFT") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0063-2"));//RIGHT
                        swFeat = swComp.FeatureByName("RF1");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RF2");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RF3");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RF4");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DRF");
                        if (item.DPSide == "RIGHT") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB1");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB2");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB3");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB4");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DRB");
                        if (item.DPSide == "RIGHT") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        break;
                    default:
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-8"));//3mm板
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-7"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-10"));//3mm板
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0017-9"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-5"));//边缘挡板
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-8"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-9"));//边缘挡板
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0016-10"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0062-3"));//LEFT
                        swFeat = swComp.FeatureByName("LF1");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LF2");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LF3");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LF4");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DLF");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB1");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB2");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB3");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB4");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DLB");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0063-2"));//RIGHT
                        swFeat = swComp.FeatureByName("RF1");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RF2");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RF3");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RF4");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DRF");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB1");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB2");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB3");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RB4");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("DRB");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        break;
                }
                //判断FC数量，FC侧板长度
                if (item.FCBlindNo > 0)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0037[BP-500]{500}-6"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0037[BP-500]{500}-8"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern5");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern5").SystemValue = item.FCBlindNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D1@Distance71").SystemValue = item.FCSideLeft / 1000m;
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0037[BP-500]{500}-6"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0037[BP-500]{500}-8"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swFeat = swAssy.FeatureByName("LocalLPattern5");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2200600002-1"));
                swComp.SetSuppression2(2); //2解压缩，0压缩.
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2200600002-5"));
                swComp.SetSuppression2(2); //2解压缩，0压缩.
                swFeat = swAssy.FeatureByName("LocalLPattern8");
                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                swModel.Parameter("D1@LocalLPattern8").SystemValue = fcNo; //D1阵列数量,D3阵列距离
                swModel.Parameter("D1@Distance64").SystemValue = (item.FCSideLeft + 500m * item.FCBlindNo) / 1000m;

                //----------油网侧板----------
                switch (item.FCSide)
                {
                    case "LEFT":
                        //重命名装配体内部
                        compReName = "FNCE0058[BP-" + tree.Module + "]{" + ((int)item.FCSideLeft - fcNo - 4) + "}";
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
                            swPart.Parameter("D2@Sketch1").SystemValue = (item.FCSideLeft - fcNo * 1m - 4m) / 1000m;
                            if (item.FCSideLeft < 100m)
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            }
                            else
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            }
                        }
                        status = swModelDocExt.SelectByID2(compReName + "-2" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                        }
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0059[BP-]{}-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0059[BP-]{}-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
                    case "RIGHT":
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0058[BP-]{}-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0058[BP-]{}-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        //重命名装配体内部
                        compReName = "FNCE0059[BP-" + tree.Module + "]{" + ((int)item.FCSideRight - fcNo - 4) + "}";
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
                            swPart.Parameter("D2@Sketch1").SystemValue = (item.FCSideRight - fcNo * 1m - 4m) / 1000m;
                            if (item.FCSideLeft < 100m)
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            }
                            else
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            }
                        }
                        status = swModelDocExt.SelectByID2(compReName + "-2" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                        }
                        break;
                    case "BOTH":
                        //重命名装配体内部
                        compReName = "FNCE0058[BP-" + tree.Module + ".1]{" + ((int)item.FCSideLeft - fcNo - 2) + "}";
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
                            swPart.Parameter("D2@Sketch1").SystemValue = (item.FCSideLeft - fcNo * 1m - 2m) / 1000m;
                            if (item.FCSideLeft < 100m)
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            }
                            else
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            }
                        }
                        status = swModelDocExt.SelectByID2(compReName + "-2" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                        }
                        //重命名装配体内部
                        compReName = "FNCE0059[BP-" + tree.Module + ".2]{" + ((int)item.FCSideRight - fcNo - 2) + "}";
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
                            swPart.Parameter("D2@Sketch1").SystemValue = (item.FCSideRight - fcNo * 1m - 2m) / 1000m;
                            if (item.FCSideLeft < 100m)
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            }
                            else
                            {
                                swFeat = swComp.FeatureByName("Edge-Flange2");
                                swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                                swFeat = swComp.FeatureByName("Edge-Flange3");
                                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            }
                        }
                        status = swModelDocExt.SelectByID2(compReName + "-2" + "@" + assyName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
                        swModel.ClearSelection2(true);
                        if (status)
                        {
                            swComp = swAssy.GetComponentByName(compReName + "-2");
                            swComp.SetSuppression2(2); //2解压缩，0压缩.
                        }
                        break;
                    default:
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0058[BP-]{}-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0058[BP-]{}-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0059[BP-]{}-1"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0059[BP-]{}-2"));
                        swComp.SetSuppression2(0); //2解压缩，0压缩.
                        break;
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
                    //排风腔
                    //重命名装配体内部
                    compReName = "FNCE0158[UCWDB800-" + tree.Module + "]{" + (int)item.Length + "}";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0158-1") + "@" + assyName,
                        "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false,
                        0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D1@Aufsatz-Linear austragen1").SystemValue = item.Length / 1000m;
                        swFeat = swComp.FeatureByName("EX");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("Cut-Extrude4");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                        swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("MA-TAB");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("UV");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D4@Sketch15").SystemValue = item.ExRightDis / 1000m;
                        if (item.UVType == "LONG") swPart.Parameter("D7@Sketch15").SystemValue = 1200m / 1000m;
                        else swPart.Parameter("D7@Sketch15").SystemValue = 600m / 1000m;
                        if (item.UVType == "LONG") swPart.Parameter("D2@Sketch15").SystemValue = 1600m / 1000m;
                        else swPart.Parameter("D2@Sketch15").SystemValue = 893m / 1000m;
                        if (item.SensorNo == 1)
                        {
                            swFeat = swComp.FeatureByName("HALL SENSOR FRONT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch19").SystemValue = item.SensorDis1 / 1000m;
                            swFeat = swComp.FeatureByName("HALL SENSOR BACK");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch20").SystemValue = item.SensorDis1 / 1000m;
                            swFeat = swComp.FeatureByName("SENSOR CABLE HOLE");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch21").SystemValue = item.SensorDis1 / 1000m;
                            swFeat = swComp.FeatureByName("LPattern1");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                        else if (item.SensorNo > 1)
                        {
                            swFeat = swComp.FeatureByName("HALL SENSOR FRONT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch19").SystemValue = item.SensorDis1 / 1000m;
                            swFeat = swComp.FeatureByName("HALL SENSOR BACK");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch20").SystemValue = item.SensorDis1 / 1000m;
                            swFeat = swComp.FeatureByName("SENSOR CABLE HOLE");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch21").SystemValue = item.SensorDis1 / 1000m;
                            swFeat = swComp.FeatureByName("LPattern1");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D1@LPattern1").SystemValue = item.SensorNo;
                            swPart.Parameter("D3@LPattern1").SystemValue = item.SensorDis2 / 1000m;
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("HALL SENSOR FRONT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("HALL SENSOR BACK");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("SENSOR CABLE HOLE");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("LPattern1");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩 
                        }
                        if (item.MARVEL == "YES")
                        {
                            swFeat = swComp.FeatureByName("MA-NTC");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D1@Sketch27").SystemValue =
                                (item.ExRightDis + item.ExLength / 2m + 50m) / 1000m;
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("MA-NTC");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                        swFeat = swComp.FeatureByName("LF");
                        if (item.SidePanel == "LEFT" || item.SidePanel == "BOTH")
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB");
                        if (item.SidePanel == "LEFT" || item.SidePanel == "BOTH")
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RF");
                        if (item.SidePanel == "RIGHT" || item.SidePanel == "BOTH")
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩.
                        swFeat = swComp.FeatureByName("RB");
                        if (item.SidePanel == "RIGHT" || item.SidePanel == "BOTH")
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //FC下导轨
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0009-3"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0009-4"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    //灯腔
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0051-1"));
                    swPart = swComp.GetModelDoc2(); //打开零件
                    swPart.Parameter("D1@Aufsatz-Linear austragen1").SystemValue = item.Length / 1000m;
                    swPart.Parameter("D1@LPattern1").SystemValue = bfSupportNo;
                    swPart.Parameter("D3@LPattern1").SystemValue = bfSupportDis;
                    swFeat = swComp.FeatureByName("FC RAIL F");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("FC RAIL B");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("LIGHT T8");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("JAP LED M8");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    switch (item.FCSide)
                    {
                        case "LEFT":
                            swFeat = swComp.FeatureByName("FC LEFT F");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D5@Sketch13").SystemValue = (item.FCSideLeft - 50m) / 1000m;
                            swFeat = swComp.FeatureByName("FC LEFT B");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D4@Sketch14").SystemValue = (item.FCSideLeft - 50m) / 1000m;
                            swFeat = swComp.FeatureByName("FC RIGHT F");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("FC RIGHT B");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            break;
                        case "RIGHT":
                            swFeat = swComp.FeatureByName("FC LEFT F");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("FC LEFT B");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("FC RIGHT F");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D6@Sketch15").SystemValue = (item.FCSideRight - 50m) / 1000m;
                            swFeat = swComp.FeatureByName("FC RIGHT B");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D5@Sketch16").SystemValue = (item.FCSideRight - 50m) / 1000m;
                            break;
                        case "BOTH":
                            swFeat = swComp.FeatureByName("FC LEFT F");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D5@Sketch13").SystemValue = (item.FCSideLeft - 50m) / 1000m;
                            swFeat = swComp.FeatureByName("FC LEFT B");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D4@Sketch14").SystemValue = (item.FCSideLeft - 50m) / 1000m;
                            swFeat = swComp.FeatureByName("FC RIGHT F");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D6@Sketch15").SystemValue = (item.FCSideRight - 50m) / 1000m;
                            swFeat = swComp.FeatureByName("FC RIGHT B");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D5@Sketch16").SystemValue = (item.FCSideRight - 50m) / 1000m;
                            break;
                        default:
                            swFeat = swComp.FeatureByName("FC LEFT F");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("FC LEFT B");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("FC RIGHT F");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("FC RIGHT B");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            break;
                    }
                    swFeat = swComp.FeatureByName("FC FIRST F");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    if (item.FCSide == "RIGHT") swPart.Parameter("D5@Sketch17").SystemValue = 25m / 1000m;
                    else swPart.Parameter("D5@Sketch17").SystemValue = (item.FCSideLeft + 25m) / 1000m;
                    swFeat = swComp.FeatureByName("FC FIRST B");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    if (item.FCSide == "RIGHT") swPart.Parameter("D3@Sketch18").SystemValue = 25m / 1000m;
                    else swPart.Parameter("D3@Sketch18").SystemValue = (item.FCSideLeft + 25m) / 1000m;
                    swFeat = swComp.FeatureByName("LPattern2");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern2").SystemValue = fcNo + item.FCBlindNo;
                }
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
                    swPart = swComp.GetModelDoc2(); //打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = (item.ExLength + 50m) / 1000m;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000m;
                    swFeat = swComp.FeatureByName("ANSUL");
                    if (item.ANSUL == "YES") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0020-1"));
                    swPart = swComp.GetModelDoc2(); //打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = (item.ExLength + 50m) / 1000m;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000m;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0047-1"));
                    swPart = swComp.GetModelDoc2(); //打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000m;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000m;
                    swFeat = swComp.FeatureByName("ANDTEC");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0048-2"));
                    swPart = swComp.GetModelDoc2(); //打开零件
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000m;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000m;
                    swFeat = swComp.FeatureByName("ANDTEC");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    //排风腔
                    //重命名装配体内部
                    compReName = "FNCE0158[UCWDB800-" + tree.Module + "]{" + (int)item.Length + "}";
                    status = swModelDocExt.SelectByID2(CommonFunc.AddSuffix(suffix, "FNCE0158-1") + "@" + assyName,
                        "COMPONENT", 0, 0, 0, false, 0, null, 0);
                    if (status) swModelDocExt.RenameDocument(compReName);
                    swModel.ClearSelection2(true);
                    status = swModelDocExt.SelectByID2(compReName + "-1" + "@" + assyName, "COMPONENT", 0, 0, 0, false,
                        0, null, 0);
                    swModel.ClearSelection2(true);
                    if (status)
                    {
                        swComp = swAssy.GetComponentByName(compReName + "-1");
                        swPart = swComp.GetModelDoc2(); //打开零件
                        swPart.Parameter("D1@Aufsatz-Linear austragen1").SystemValue = item.Length / 1000m;
                        swFeat = swComp.FeatureByName("EX");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("Cut-Extrude4");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("MA-TAB");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch1").SystemValue = item.ExRightDis / 1000m;
                        swPart.Parameter("D1@Sketch1").SystemValue = item.ExLength / 1000m;
                        swPart.Parameter("D2@Sketch1").SystemValue = item.ExWidth / 1000m;
                        swFeat = swComp.FeatureByName("UV");
                        swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        swPart.Parameter("D4@Sketch15").SystemValue = item.ExRightDis / 1000m;
                        if (item.UVType == "LONG") swPart.Parameter("D7@Sketch15").SystemValue = 1200m / 1000m;
                        else swPart.Parameter("D7@Sketch15").SystemValue = 600m / 1000m;
                        if (item.UVType == "LONG") swPart.Parameter("D2@Sketch15").SystemValue = 1600m / 1000m;
                        else swPart.Parameter("D2@Sketch15").SystemValue = 893m / 1000m;
                        if (item.SensorNo == 1)
                        {
                            swFeat = swComp.FeatureByName("HALL SENSOR FRONT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch19").SystemValue = item.SensorDis1 / 1000m;
                            swFeat = swComp.FeatureByName("HALL SENSOR BACK");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch20").SystemValue = item.SensorDis1 / 1000m;
                            swFeat = swComp.FeatureByName("SENSOR CABLE HOLE");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch21").SystemValue = item.SensorDis1 / 1000m;
                            swFeat = swComp.FeatureByName("LPattern1");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                        else if (item.SensorNo > 1)
                        {
                            swFeat = swComp.FeatureByName("HALL SENSOR FRONT");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch19").SystemValue = item.SensorDis1 / 1000m;
                            swFeat = swComp.FeatureByName("HALL SENSOR BACK");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch20").SystemValue = item.SensorDis1 / 1000m;
                            swFeat = swComp.FeatureByName("SENSOR CABLE HOLE");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch21").SystemValue = item.SensorDis1 / 1000m;
                            swFeat = swComp.FeatureByName("LPattern1");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D1@LPattern1").SystemValue = item.SensorNo;
                            swPart.Parameter("D3@LPattern1").SystemValue = item.SensorDis2 / 1000m;
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("HALL SENSOR FRONT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("HALL SENSOR BACK");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("SENSOR CABLE HOLE");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("LPattern1");
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
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("ANSULSIDE RIGHT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                            swFeat = swComp.FeatureByName("ANSULSIDE LEFT");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        }
                        if (item.MARVEL == "YES")
                        {
                            swFeat = swComp.FeatureByName("MA-NTC");
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                            swPart.Parameter("D3@Sketch24").SystemValue =
                                (item.ExRightDis + item.ExLength / 2m + 50m) / 1000m;
                        }
                        else
                        {
                            swFeat = swComp.FeatureByName("MA-NTC");
                            swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩s 
                        }
                        swFeat = swComp.FeatureByName("LF");
                        if (item.SidePanel == "LEFT" || item.SidePanel == "BOTH")
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("LB");
                        if (item.SidePanel == "LEFT" || item.SidePanel == "BOTH")
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                        swFeat = swComp.FeatureByName("RF");
                        if (item.SidePanel == "RIGHT" || item.SidePanel == "BOTH")
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩.
                        swFeat = swComp.FeatureByName("RB");
                        if (item.SidePanel == "RIGHT" || item.SidePanel == "BOTH")
                            swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                        else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    }
                    //FC下导轨
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0009-3"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0009-4"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D1@Sketch2").SystemValue = (item.Length - 5m) / 1000m;
                    //灯腔
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0051-1"));
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D1@Aufsatz-Linear austragen1").SystemValue = item.Length / 1000m;
                    swPart.Parameter("D1@LPattern1").SystemValue = bfSupportNo;
                    swPart.Parameter("D3@LPattern1").SystemValue = bfSupportDis;
                    swFeat = swComp.FeatureByName("FC RAIL F");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("FC RAIL B");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("LIGHT T8");
                    if (item.LightType == "T8") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("JAP LED M8");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("FC LEFT F");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("FC RIGHT F");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("FC FIRST F");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("FC LEFT B");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("FC RIGHT B");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("FC FIRST B");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("LPattern2");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                //----------FC上导轨----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0034-1"));
                swPart = swComp.GetModelDoc2();//打开零件
                swPart.Parameter("D1@Skizze1").SystemValue = item.Length / 1000m;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0056-3"));
                swPart = swComp.GetModelDoc2();//打开零件
                swPart.Parameter("D1@Skizze1").SystemValue = item.Length / 1000m;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0003-1"));
                swPart = swComp.GetModelDoc2();//打开零件
                swPart.Parameter("D1@Aufsatz-Linear austragen1").SystemValue = item.Length / 1000m;
                swFeat = swComp.FeatureByName("L");
                if (item.SidePanel == "LEFT" || item.SidePanel == "BOTH") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("R");
                if (item.SidePanel == "RIGHT" || item.SidePanel == "BOTH") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0074-2"));
                swPart = swComp.GetModelDoc2();//打开零件
                swPart.Parameter("D1@Aufsatz-Linear austragen1").SystemValue = item.Length / 1000m;
                swFeat = swComp.FeatureByName("L");
                if (item.SidePanel == "LEFT" || item.SidePanel == "BOTH") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("R");
                if (item.SidePanel == "RIGHT" || item.SidePanel == "BOTH") swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0007-3"));
                swPart = swComp.GetModelDoc2();//打开零件
                swPart.Parameter("D1@Skizze1").SystemValue = (item.Length - 5m) / 1000m;
                swFeat = swComp.FeatureByName("PIPE-UP");
                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0008-1"));
                swPart = swComp.GetModelDoc2();//打开零件
                swPart.Parameter("D1@Skizze1").SystemValue = (item.Length - 5m) / 1000m;
                swFeat = swComp.FeatureByName("PIPE-UP");
                swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                //----------SSP灯板支撑条----------
                if (item.SSPType == "DOME")
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0036-4"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0036-3"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0035-3"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@Sketch1").SystemValue = item.Length / 1000m;
                    if (item.Gutter == "YES") swModel.Parameter("D1@Distance59").SystemValue = item.GutterWidth / 1000m;
                    else swModel.Parameter("D1@Distance59").SystemValue = 0.5m / 1000m;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0035-4"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    if (item.Gutter == "YES") swModel.Parameter("D1@Distance58").SystemValue = item.GutterWidth / 1000m;
                    else swModel.Parameter("D1@Distance58").SystemValue = 0.5m / 1000m;
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0036-3"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    swPart = swComp.GetModelDoc2();//打开零件
                    swPart.Parameter("D2@Sketch1").SystemValue = item.Length / 1000m;
                    if (item.Gutter == "YES") swModel.Parameter("D1@Distance54").SystemValue = item.GutterWidth / 1000m;
                    else swModel.Parameter("D1@Distance54").SystemValue = 0.5m / 1000m;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0036-4"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩.
                    if (item.Gutter == "YES") swModel.Parameter("D1@Distance55").SystemValue = item.GutterWidth / 1000m;
                    else swModel.Parameter("D1@Distance55").SystemValue = 0.5m / 1000m;
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0035-4"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩.
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0035-3"));
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
