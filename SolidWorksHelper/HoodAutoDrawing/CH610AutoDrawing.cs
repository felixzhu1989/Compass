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
        readonly CH610Service objCH610Service = new CH610Service();

        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            //创建项目模型存放地址
            string itemPath = $@"{projectPath}\{tree.Item}-{tree.Module}-{tree.CategoryName}";
            if (!CommonFunc.CreateProjectPath(itemPath)) return;
            //Pack的后缀
            string suffix = $@"{tree.Item}-{tree.Module}-{tree.ODPNo.Substring(tree.ODPNo.Length - 6)}";

            //判断文件是否存在，如果存在将不执行pack，如果不存在则执行pack
            //packango后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = $@"{itemPath}\{tree.CategoryName.ToLower()}_{suffix}.sldasm";
            if (!File.Exists(packedAssyPath)) packedAssyPath = CommonFunc.PackAndGoFunc(suffix, swApp, tree.ModelPath, itemPath);

            //查询参数
            CH610 item = (CH610)objCH610Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix; //后缀
            ModelDoc2 swModel;
            ModelDoc2 swPart;
            AssemblyDoc swAssy;
            Component2 swComp;
            Feature swFeat;
            
            EditPart swEdit = new EditPart();


            //打开Pack后的模型
            swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
            swAssy = swModel as AssemblyDoc; //装配体
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true); //TopOnly参数设置成true，只重建顶层，不重建零件内部
            /*注意SolidWorks单位是m，计算是应当/1000d
             * 整形与整形运算得出的结果仍然时整形，1640 / 1000d结果为0，因此必须将其中一个转化成double型，使用后缀m就可以了
             * (int)不进行四舍五入，Convert.ToInt32会四舍五入
            */
            //-----------计算中间值，----------

            //内部灯板宽度
            double insidePanelWidth = 155.2d;
            if (item.Deepth % 100d > 45d) insidePanelWidth = 255.2d;
            //当烟罩宽度为100的整数倍时（如1000,1500），middleroof 取值155.2，冷凝板参考上表
            //当烟罩宽度为100的整数倍时+50（如1150,1550），middleroof 取值255.2，冷凝板参考整数档（如1550参考1500档）

            //冷凝板参数
            double condensatePanelAngle = default(double);
            double condensatePanelHeight = default(double);


            switch ((int)(item.Deepth / 100d))//取百位以上整数
            {
                case 6:
                    condensatePanelAngle = (160.2d * 3.1415926d) / 180d;
                    condensatePanelHeight = 357.1d;
                    break;
                case 7:
                    condensatePanelAngle = (160.2d * 3.1415926d) / 180d;
                    condensatePanelHeight = 357.1d;
                    break;
                case 8:
                    condensatePanelAngle = (146.7d * 3.1415926d) / 180d;
                    condensatePanelHeight = 402.3d;
                    break;
                case 9:
                    condensatePanelAngle = (146.7d * 3.1415926d) / 180d;
                    condensatePanelHeight = 402.3d;
                    break;

                case 10://OK
                    condensatePanelAngle = (136d * 3.1415926d) / 180d;
                    condensatePanelHeight = 465d;
                    break;
                case 11://OK
                    condensatePanelAngle = (132d * 3.1415926d) / 180d;//132d
                    condensatePanelHeight = 495d;//495d
                    break;
                case 12://OK
                    condensatePanelAngle = (129d * 3.1415926d) / 180d;//129d
                    condensatePanelHeight = 535d;//535d
                    break;
                case 13://OK
                    condensatePanelAngle = (125d * 3.1415926d) / 180d;//125
                    condensatePanelHeight = 575d;//575
                    break;
                case 14://OK
                    condensatePanelAngle = (123d * 3.1415926d) / 180d;//123
                    condensatePanelHeight = 616d;//616
                    break;
                case 15://OK
                    condensatePanelAngle = (120d * 3.1415926d) / 180d;//120
                    condensatePanelHeight = 658d;//658
                    break;

                case 16:
                    condensatePanelAngle = (118.4d * 3.1415926d) / 180d;
                    condensatePanelHeight = 706.6d;
                    break;
                case 17:
                    condensatePanelAngle = (118.4d * 3.1415926d) / 180d;
                    condensatePanelHeight = 706.6d;
                    break;
                default://默认是标准模版中的值，放置模型报错
                    condensatePanelAngle = (126d * 3.1415926d) / 180d;//126
                    condensatePanelHeight = 574d;//574
                    break;
            }


            try
            {
                //----------Top Level----------
                //烟罩深度
                //swModel.Parameter("D1@Distance32").SystemValue = item.Deepth / 1000d;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0173-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 120d) / 1000d;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0174-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 120d) / 1000d;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0175-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 147d) / 1000d;
                swPart.Parameter("D1@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D2@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D3@Sketch1").SystemValue = condensatePanelHeight / 1000d;
                //1.5d以下，两个把手HANDER2，其他HANDER4
                if (item.Length > 1500d)
                {
                    swFeat = swComp.FeatureByName("HANDER2");
                    swFeat.SetSuppression2(0, 2, null);//参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("HANDER4");
                    swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("HANDER2");
                    swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("HANDER4");
                    swFeat.SetSuppression2(0, 2, null);//参数1：1解压，0压缩
                }


                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0176-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 147d) / 1000d;
                swPart.Parameter("D1@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D2@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D3@Sketch1").SystemValue = condensatePanelHeight / 1000d;
                //1.5d以下，两个把手HANDER2，其他HANDER4
                if (item.Length > 1500d)
                {
                    swFeat = swComp.FeatureByName("HANDER2");
                    swFeat.SetSuppression2(0, 2, null);//参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("HANDER4");
                    swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("HANDER2");
                    swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("HANDER4");
                    swFeat.SetSuppression2(0, 2, null);//参数1：1解压，0压缩
                }

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0177-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D1@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D2@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D3@Sketch1").SystemValue = condensatePanelHeight / 1000d;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0178-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D1@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D2@Sketch1").SystemValue = condensatePanelAngle;
                swPart.Parameter("D3@Sketch1").SystemValue = condensatePanelHeight / 1000d;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHB0053-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 300d) / 1000d;

                if (item.Deepth > 1400d)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0033-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0034-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0034-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();//打开零件3
                    swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 6d) / 1000d;
                    swPart.Parameter("D1@Sketch1").SystemValue = (item.Deepth - 4d) / 1000d;
                    swPart.Parameter("D1@Sketch26").SystemValue = item.ExLength / 1000d;
                    swPart.Parameter("D2@Sketch26").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D2@Sketch27").SystemValue = 50d * Convert.ToInt32((item.Deepth / 2d - 200d) / 50d) / 1000d;
                    swFeat = swComp.FeatureByName("PHILIPS LAMP");
                    if (item.LightType == "PHILIPS") swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null);
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
                    swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 6d) / 1000d;
                    swPart.Parameter("D1@Sketch1").SystemValue = (item.Deepth - 4d) / 1000d;
                    swPart.Parameter("D1@Sketch26").SystemValue = item.ExLength / 1000d;
                    swPart.Parameter("D2@Sketch26").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D2@Sketch27").SystemValue = 50d * Convert.ToInt32((item.Deepth / 2d - 150d) / 50d) / 1000d;
                    swFeat = swComp.FeatureByName("PHILIPS LAMP");
                    if (item.LightType == "PHILIPS") swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                    else swFeat.SetSuppression2(0, 2, null);
                }

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0035-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 6d) / 1000d;
                swPart.Parameter("D1@Sketch1").SystemValue = insidePanelWidth / 1000d;
                swPart.Parameter("D3@Sketch39").SystemValue = (insidePanelWidth - 55.2d) / 1000d;
                swFeat = swComp.FeatureByName("PHILIPS LAMP");
                if (item.LightType == "PHILIPS") swFeat.SetSuppression2(1, 2, null);//参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null);

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0065-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Deepth - 3.5d) / 1000d;
                swPart.Parameter("D10@Sketch25").SystemValue = condensatePanelAngle;
                swPart.Parameter("D11@Sketch25").SystemValue = condensatePanelHeight / 1000d;
                swPart.Parameter("D1@Sketch47").SystemValue = insidePanelWidth / 1000d;
                swPart.Parameter("D6@Sketch47").SystemValue = (insidePanelWidth - 55.2d) / 1000d;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0066-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = item.Deepth / 1000d;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0013-3"));
                if (item.ExWidth == 300d) swComp.SetSuppression2(0); //2解压缩，0压缩
                else swComp.SetSuppression2(2); //2解压缩，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0013-1"));
                if (item.ExWidth == 300d) swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();//打开零件3
                    swPart.Parameter("D1@Sketch1").SystemValue = (item.ExLength / 2d + 10d) / 1000d;
                    swPart.Parameter("D2@Sketch1").SystemValue = (item.ExWidth + 40d) / 1000d;
                }

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCE0018-1"));
                swComp.SetSuppression2(2); //2解压缩，0压缩
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength * 2d + 100d) / 1000d;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0006-3"));
                if (item.ExHeight == 100d)
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2();//打开零件3
                    swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50d) / 1000d;
                    swPart.Parameter("D1@Sketch1").SystemValue = item.ExHeight / 1000d;
                    swFeat = swComp.FeatureByName("ANSUL");
                    swFeat.SetSuppression2(0, 2, null);
                }


                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0007-1"));
                if (item.ExHeight == 100d)
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2(); //打开零件3
                    swPart.Parameter("D2@Base-Flange1").SystemValue = (item.ExLength + 50d) / 1000d;
                    swPart.Parameter("D1@Sketch1").SystemValue = item.ExHeight / 1000d;
                }
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0008-1"));
                if (item.ExHeight == 100d)
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2(); //打开零件3
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                }
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0009-1"));
                if (item.ExHeight == 100d)
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                else
                {
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swPart = swComp.GetModelDoc2(); //打开零件3
                    swPart.Parameter("D2@基体-法兰1").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D3@草图1").SystemValue = item.ExHeight / 1000d;
                }
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2900100001-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@基体-法兰1").SystemValue = (item.Deepth - 100) / 1000d;



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
