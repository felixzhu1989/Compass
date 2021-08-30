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
    public class CH610AutoDrawing : IAutoDrawing
    {
        CH610Service objCH610Service = new CH610Service();

        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            //创建项目模型存放地址
            string itemPath = projectPath + @"\" + tree.Item + "-" + tree.Module + "-" + tree.CategoryName;
            if (!Directory.Exists(itemPath))
            {
                Directory.CreateDirectory(itemPath);
            }
            else
            {
                Common.ShowMsg show = new ShowMsg();
                DialogResult result = show.ShowMessageBoxTimeout(
                    "模型文件夹" + itemPath + "存在，如果之前pack已经执行过，将不执行pack过程而是直接修改模型，如果要中断作图点击YES，继续作图请点击No或者3s后窗口会自动消失",
                    "提示信息", MessageBoxButtons.YesNo, 3000);
                if (result == DialogResult.Yes) return;
            }
            //Pack的后缀
            string suffix = tree.Item + "-" + tree.Module + "-" +
                            tree.ODPNo.Substring(tree.ODPNo.Length - 6);
            //判断文件是否存在，如果存在将不执行pack，如果不存在则执行pack
            //packango后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = itemPath + @"\" + tree.CategoryName.ToLower() + "_" + suffix + ".sldasm";
            if (!File.Exists(packedAssyPath))
                packedAssyPath = CommonFunc.PackAndGoFunc(suffix, swApp, tree.ModelPath, itemPath);

            //查询参数
            CH610 item = (CH610)objCH610Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix; //后缀
            ModelDoc2 swModel = default(ModelDoc2);
            ModelDoc2 swPart = default(ModelDoc2);
            AssemblyDoc swAssy = default(AssemblyDoc);
            Component2 swComp;
            Feature swFeat = default(Feature);
            object configNames = null;
            EditPart swEdit = new EditPart();


            //打开Pack后的模型
            swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
            swAssy = swModel as AssemblyDoc; //装配体
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true); //TopOnly参数设置成true，只重建顶层，不重建零件内部
            /*注意SolidWorks单位是m，计算是应当/1000m
             * 整形与整形运算得出的结果仍然时整形，1640 / 1000m结果为0，因此必须将其中一个转化成decimal型，使用后缀m就可以了
             * (int)不进行四舍五入，Convert.ToInt32会四舍五入
            */
            //-----------计算中间值，----------

            //内部灯板宽度
            decimal insidePanelWidth = 155.2m;
            if (item.Deepth % 100m > 45m) insidePanelWidth = 255.2m;
            //当烟罩宽度为100的整数倍时（如1000,1500），middleroof 取值155.2，冷凝板参考上表
            //当烟罩宽度为100的整数倍时+50（如1150,1550），middleroof 取值255.2，冷凝板参考整数档（如1550参考1500档）

            //冷凝板参数
            decimal condensatePanelAngle = default(decimal);
            decimal condensatePanelHeight = default(decimal);


            switch ((int)(item.Deepth / 100m))//取百位以上整数
            {
                case 6:
                    condensatePanelAngle = (160.2m * 3.1415926m) / 180m;
                    condensatePanelHeight = 357.1m;
                    break;
                case 7:
                    condensatePanelAngle = (160.2m * 3.1415926m) / 180m;
                    condensatePanelHeight = 357.1m;
                    break;
                case 8:
                    condensatePanelAngle = (146.7m * 3.1415926m) / 180m;
                    condensatePanelHeight = 402.3m;
                    break;
                case 9:
                    condensatePanelAngle = (146.7m * 3.1415926m) / 180m;
                    condensatePanelHeight = 402.3m;
                    break;

                case 10://OK
                    condensatePanelAngle = (136m * 3.1415926m) / 180m;
                    condensatePanelHeight = 465m;
                    break;
                case 11://OK
                    condensatePanelAngle = (132m * 3.1415926m) / 180m;//132m
                    condensatePanelHeight = 495m;//495m
                    break;
                case 12://OK
                    condensatePanelAngle = (129m * 3.1415926m) / 180m;//129m
                    condensatePanelHeight = 535m;//535m
                    break;
                case 13://OK
                    condensatePanelAngle = (125m * 3.1415926m) / 180m;//125
                    condensatePanelHeight = 575m;//575
                    break;
                case 14://OK
                    condensatePanelAngle = (123m * 3.1415926m) / 180m;//123
                    condensatePanelHeight = 616m;//616
                    break;
                case 15://OK
                    condensatePanelAngle = (120m * 3.1415926m) / 180m;//120
                    condensatePanelHeight = 658m;//658
                    break;

                case 16:
                    condensatePanelAngle = (118.4m * 3.1415926m) / 180m;
                    condensatePanelHeight = 706.6m;
                    break;
                case 17:
                    condensatePanelAngle = (118.4m * 3.1415926m) / 180m;
                    condensatePanelHeight = 706.6m;
                    break;
                default://默认是标准模版中的值，放置模型报错
                    condensatePanelAngle = (126m * 3.1415926m) / 180m;//126
                    condensatePanelHeight = 574m;//574
                    break;
            }


            try
            {
                //----------Top Level----------
                //烟罩深度
                //swModel.Parameter("D1@Distance32").SystemValue = item.Deepth / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0173-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 120m) / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0174-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 120m) / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0175-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 147m) / 1000m;
                swPart.Parameter("D1@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D2@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D3@Sketch1").SystemValue = condensatePanelHeight / 1000m;
                //1.5m以下，两个把手HANDER2，其他HANDER4
                if (item.Length > 1500m)
                {
                    swFeat = swComp.FeatureByName("HANDER2");
                    swFeat.SetSuppression2(0, 2, configNames);//参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("HANDER4");
                    swFeat.SetSuppression2(1, 2, configNames);//参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("HANDER2");
                    swFeat.SetSuppression2(1, 2, configNames);//参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("HANDER4");
                    swFeat.SetSuppression2(0, 2, configNames);//参数1：1解压，0压缩
                }


                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0176-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 147m) / 1000m;
                swPart.Parameter("D1@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D2@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D3@Sketch1").SystemValue = condensatePanelHeight / 1000m;
                //1.5m以下，两个把手HANDER2，其他HANDER4
                if (item.Length > 1500m)
                {
                    swFeat = swComp.FeatureByName("HANDER2");
                    swFeat.SetSuppression2(0, 2, configNames);//参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("HANDER4");
                    swFeat.SetSuppression2(1, 2, configNames);//参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("HANDER2");
                    swFeat.SetSuppression2(1, 2, configNames);//参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("HANDER4");
                    swFeat.SetSuppression2(0, 2, configNames);//参数1：1解压，0压缩
                }

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0177-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D1@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D2@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D3@Sketch1").SystemValue = condensatePanelHeight / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0178-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D1@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D2@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D3@Sketch1").SystemValue = condensatePanelHeight / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHB0053-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 300m) / 1000m;

                if (item.Deepth > 1400m)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0033-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0034-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0034-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();//打开零件3
                    swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 6m) / 1000m;
                    swPart.Parameter("D1@Sketch1").SystemValue = (item.Deepth - 4m) / 1000m;
                    swPart.Parameter("D1@Sketch26").SystemValue = item.ExLength / 1000m;
                    swPart.Parameter("D2@Sketch26").SystemValue = item.ExWidth / 1000m;
                    swPart.Parameter("D2@Sketch27").SystemValue = 50m * Convert.ToInt32((item.Deepth / 2m - 200m) / 50m) / 1000m;
                    swFeat = swComp.FeatureByName("PHILIPS LAMP");
                    if (item.LightType == "PHILIPS") swFeat.SetSuppression2(1, 2, configNames);//参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, configNames);
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0034-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0034-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0033-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();//打开零件3
                    swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 6m) / 1000m;
                    swPart.Parameter("D1@Sketch1").SystemValue = (item.Deepth - 4m) / 1000m;
                    swPart.Parameter("D1@Sketch26").SystemValue = item.ExLength / 1000m;
                    swPart.Parameter("D2@Sketch26").SystemValue = item.ExWidth / 1000m;
                    swPart.Parameter("D2@Sketch27").SystemValue = 50m * Convert.ToInt32((item.Deepth / 2m - 150m) / 50m) / 1000m;
                    swFeat = swComp.FeatureByName("PHILIPS LAMP");
                    if (item.LightType == "PHILIPS") swFeat.SetSuppression2(1, 2, configNames);//参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, configNames);
                }

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0035-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 6m) / 1000m;
                swPart.Parameter("D1@Sketch1").SystemValue = insidePanelWidth / 1000m;
                swPart.Parameter("D3@Sketch39").SystemValue = (insidePanelWidth - 55.2m) / 1000m;
                swFeat = swComp.FeatureByName("PHILIPS LAMP");
                if (item.LightType == "PHILIPS") swFeat.SetSuppression2(1, 2, configNames);//参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, configNames);

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0065-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Deepth - 3.5m) / 1000m;
                swPart.Parameter("D10@Sketch25").SystemValue = condensatePanelAngle;
                swPart.Parameter("D11@Sketch25").SystemValue = condensatePanelHeight / 1000m;
                swPart.Parameter("D1@Sketch47").SystemValue = insidePanelWidth / 1000m;
                swPart.Parameter("D6@Sketch47").SystemValue = (insidePanelWidth - 55.2m) / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0066-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = item.Deepth / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0013-3"));
                if (item.ExWidth == 300m) swComp.SetSuppression2(0); //2解压缩，0压缩
                else swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0013-1"));
                if (item.ExWidth == 300m) swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();//打开零件3
                    swPart.Parameter("D1@Sketch1").SystemValue = (item.ExLength / 2m + 10m) / 1000m;
                    swPart.Parameter("D2@Sketch1").SystemValue = (item.ExWidth + 40m) / 1000m;
                }

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0018-1"));
                swComp.SetSuppression2(2); //2解压缩，0压缩
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 2m + 100m) / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0006-3"));
                if (item.ExHeight == 100m)
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();//打开零件3
                    swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50m) / 1000m;
                    swPart.Parameter("D1@Sketch1").SystemValue = item.ExHeight / 1000m;
                    swFeat = swComp.FeatureByName("ANSUL");
                    swFeat.SetSuppression2(0, 2, configNames);
                }


                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0007-1"));
                if (item.ExHeight == 100m)
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2(); //打开零件3
                    swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50m) / 1000m;
                    swPart.Parameter("D1@Sketch1").SystemValue = item.ExHeight / 1000m;
                }
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0008-1"));
                if (item.ExHeight == 100m)
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2(); //打开零件3
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000m;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000m;
                }
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0009-1"));
                if (item.ExHeight == 100m)
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2(); //打开零件3
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000m;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000m;
                }
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100001-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 100) / 1000m;



                swModel.ForceRebuild3(true); //设置成true，直接更新顶层，速度很快，设置成false，每个零件都会更新，很慢
                swModel.Save(); //保存，很耗时间
                swApp.CloseDoc(packedAssyPath); //关闭，很快
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
