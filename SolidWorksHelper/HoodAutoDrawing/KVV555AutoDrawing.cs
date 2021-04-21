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
    public class KVV555AutoDrawing : IAutoDrawing
    {
        KVV555Service objKVV555Service = new KVV555Service();

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
            KVV555 item = (KVV555)objKVV555Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

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

            //新风面板卡扣数量及间距
            int frontPanelKaKouNo = (int)((item.Length - 300m) / 450m) + 2;
            decimal frontPanelKaKouDis = Convert.ToDecimal((item.Length - 300m) / (frontPanelKaKouNo - 1)) / 1000m;
            //新风面板螺丝孔数量及间距
            int frontPanelHoleNo = (int)((item.Length - 300m) / 900m) + 2;
            decimal frontPanelHoleDis = Convert.ToDecimal((item.Length - 300) / (frontPanelHoleNo - 1)) / 1000m;
            //新风CJ孔数量和新风CJ孔第一个CJ距离边缘距离
            int frontCjNo = (int)((item.Length - 30m) / 32m) + 1;
            decimal frontCjFirstDis = Convert.ToDecimal((item.Length - (frontCjNo - 1) * 32m) / 2) / 1000m;
            //Midroof灯板螺丝孔数量及第二个孔距离边缘距离,灯板顶面吊装槽钢螺丝孔位距离
            int midRoofHoleNo = (int)((item.Length - 300m) / 400m);
            decimal midRoofSecondHoleDis = Convert.ToDecimal((item.Length - (midRoofHoleNo - 1) * 400m) / 2) / 1000m;
            decimal midRoofTopHoleDis =
                Convert.ToDecimal(item.Deepth - 535m - 360m - 90m -
                                  (int)((item.Deepth - 535m - 360m - 90m - 100m) / 50m) * 50m) / 1000m;
            //KSA数量，KSA侧板长度(以全长计算)
            int ksaNo = (int)((item.Length + 1) / 498m);
            decimal ksaSideLength = Convert.ToDecimal((item.Length - ksaNo * 498m) / 2) / 1000m;
            //MESH侧板长度(除去排风三角板3mm计算)
            //decimal meshSideLength = Convert.ToDecimal((item.Length - 3m - (int)((item.Length - 2m) / 498m) * 498m) / 2) / 1000m;
            //侧板CJ孔整列到烟罩底部
            int sidePanelDownCjNo = (int)((item.Deepth - 95m) / 32m);
            //非水洗烟罩KV/UV
            int sidePanelSideCjNo = (int)((item.Deepth - 305m) / 32m);
            //水洗烟罩KW/UW
            //int sidePanelSideCjNo = (int)((item.Deepth - 380) / 32);

            //内部灯板宽度
            decimal insidePanelWidth = default(decimal);
            //冷凝板参数
            decimal condensatePanelAngle = default(decimal);
            decimal condensatePanelHeight = default(decimal);

            switch (item.Deepth)
            {
                case 600m:
                    insidePanelWidth = 155.2m;
                    condensatePanelAngle = (160.2m * 3.1415926m) / 180m;
                    condensatePanelHeight = 357.1m;
                    break;
                case 650m:
                    insidePanelWidth = 255.2m;
                    condensatePanelAngle = (160.2m * 3.1415926m) / 180m;
                    condensatePanelHeight = 357.1m;
                    break;
                case 700m:
                    insidePanelWidth = 155.2m;
                    condensatePanelAngle = (160.2m * 3.1415926m) / 180m;
                    condensatePanelHeight = 357.1m;
                    break;
                case 750m:
                    insidePanelWidth = 255.2m;
                    condensatePanelAngle = (160.2m * 3.1415926m) / 180m;
                    condensatePanelHeight = 357.1m;
                    break;
                case 800m:
                    insidePanelWidth = 155.2m;
                    condensatePanelAngle = (146.7m * 3.1415926m) / 180m;
                    condensatePanelHeight = 402.3m;
                    break;
                case 850m:
                    insidePanelWidth = 255.2m;
                    condensatePanelAngle = (146.7m * 3.1415926m) / 180m;
                    condensatePanelHeight = 402.3m;
                    break;
                case 900m:
                    insidePanelWidth = 155.2m;
                    condensatePanelAngle = (146.7m * 3.1415926m) / 180m;
                    condensatePanelHeight = 402.3m;
                    break;
                case 950m:
                    insidePanelWidth = 255.2m;
                    condensatePanelAngle = (146.7m * 3.1415926m) / 180m;
                    condensatePanelHeight = 402.3m;
                    break;
                case 1000m:
                    insidePanelWidth = 155.2m;
                    condensatePanelAngle = (136.3m * 3.1415926m) / 180m;
                    condensatePanelHeight = 465m;
                    break;
                case 1050m:
                    insidePanelWidth = 255.2m;
                    condensatePanelAngle = (136.3m * 3.1415926m) / 180m;
                    condensatePanelHeight = 465m;
                    break;
                case 1100m://OK
                    insidePanelWidth = 155.2m;
                    condensatePanelAngle = (132m * 3.1415926m) / 180m;
                    condensatePanelHeight = 496m;
                    break;
                case 1150m:
                    insidePanelWidth = 255.2m;
                    condensatePanelAngle = (136.3m * 3.1415926m) / 180m;
                    condensatePanelHeight = 496m;
                    break;
                case 1200m://OK
                    insidePanelWidth = 155.2m;
                    condensatePanelAngle = (129m * 3.1415926m) / 180m;
                    condensatePanelHeight = 534m;
                    break;
                case 1250m://OK
                    insidePanelWidth = 255.2m;
                    condensatePanelAngle = (127m * 3.1415926m) / 180m;
                    condensatePanelHeight = 555m;
                    break;
                case 1300m://OK
                    insidePanelWidth = 155.2m;
                    condensatePanelAngle = (126m * 3.1415926m) / 180m;
                    condensatePanelHeight = 574m;
                    break;
                case 1350m://OK
                    insidePanelWidth = 255.2m;
                    condensatePanelAngle = (124m * 3.1415926m) / 180m;
                    condensatePanelHeight = 595m;
                    break;
                case 1400m://OK
                    insidePanelWidth = 155.2m;
                    condensatePanelAngle = (123m * 3.1415926m) / 180m;
                    condensatePanelHeight = 616m;
                    break;
                case 1450m://OK
                    insidePanelWidth = 255.2m;
                    condensatePanelAngle = (122m * 3.1415926m) / 180m;
                    condensatePanelHeight = 637m;
                    break;
                case 1500m://OK
                    insidePanelWidth = 155.2m;
                    condensatePanelAngle = (120m * 3.1415926m) / 180m;
                    condensatePanelHeight = 658m;
                    break;
                case 1550m:
                    insidePanelWidth = 255.2m;
                    condensatePanelAngle = (119m * 3.1415926m) / 180m;
                    condensatePanelHeight = 638.6m;
                    break;
                case 1600m:
                    insidePanelWidth = 155.2m;
                    condensatePanelAngle = (118m * 3.1415926m) / 180m;
                    condensatePanelHeight = 706m;
                    break;
                case 1650m:
                    insidePanelWidth = 255.2m;
                    condensatePanelAngle = (118m * 3.1415926m) / 180m;
                    condensatePanelHeight = 706.6m;
                    break;
                case 1700m:
                    insidePanelWidth = 155.2m;
                    condensatePanelAngle = (118m * 3.1415926m) / 180m;
                    condensatePanelHeight = 706m;
                    break;
                case 1750m:
                    insidePanelWidth = 255.2m;
                    condensatePanelAngle = (118m * 3.1415926m) / 180m;
                    condensatePanelHeight = 706.6m;
                    break;

                default:
                    break;
            }


            try
            {




                //----------Top Level----------
                //烟罩深度
                //swModel.Parameter("D1@Distance32").SystemValue = item.Deepth / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0063-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 120m) / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0064-2"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 120m) / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0065-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 147m) / 1000m;
                swPart.Parameter("D1@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D2@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D3@Sketch1").SystemValue = condensatePanelHeight / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0066-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 147m) / 1000m;
                swPart.Parameter("D1@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D2@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D3@Sketch1").SystemValue = condensatePanelHeight / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0067-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D1@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D2@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D3@Sketch1").SystemValue = condensatePanelHeight / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0068-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D1@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D2@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D3@Sketch1").SystemValue = condensatePanelHeight / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHB0053-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 300m) / 1000m;

                if (item.Deepth > 1400m)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0007-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0017-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0017-2"));
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
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0017-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0017-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0007-2"));
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

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0008-3"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 6m) / 1000m;
                swPart.Parameter("D2@Sketch1").SystemValue = insidePanelWidth / 1000m;
                swPart.Parameter("D3@Sketch39").SystemValue = (insidePanelWidth - 55.2m) / 1000m;
                swFeat = swComp.FeatureByName("PHILIPS LAMP");
                if (item.LightType == "PHILIPS") swFeat.SetSuppression2(1, 2, configNames);//参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, configNames);

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0034-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Deepth - 3.5m) / 1000m;
                swPart.Parameter("D10@Sketch25").SystemValue = condensatePanelAngle;
                swPart.Parameter("D11@Sketch25").SystemValue = condensatePanelHeight / 1000m;
                swPart.Parameter("D1@Sketch47").SystemValue = insidePanelWidth / 1000m;
                swPart.Parameter("D6@Sketch47").SystemValue = (insidePanelWidth - 55.2m) / 1000m;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0035-1"));
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
