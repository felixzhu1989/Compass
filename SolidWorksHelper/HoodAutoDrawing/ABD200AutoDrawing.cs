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
    public class ABD200AutoDrawing:IAutoDrawing
    {
        Component2 swComp;
        private readonly ABD200Service objABD200Service = new ABD200Service();
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
            ABD200 item = (ABD200)objABD200Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix;//后缀
            ModelDoc2 swModel;
            ModelDoc2 swPart;
            AssemblyDoc swAssy;            
            Feature swFeat;
            

            //打开Pack后的模型
            swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
            swAssy = swModel as AssemblyDoc;//装配体
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);//TopOnly参数设置成true，只重建顶层，不重建零件内部
            /*注意SolidWorks单位是m，计算是应当/1000d
             * 整形与整形运算得出的结果仍然时整形，1640 / 1000d结果为0，因此必须将其中一个转化成double型，使用后缀m就可以了
             * (int)不进行四舍五入，Convert.ToInt32会四舍五入
            */
            //-----------计算中间值，----------
            //铆钉数量
            //int rivetNo = (int)((item.Length - 47d) / 400d);
            //double rivetDis = (item.Length - 47d) / rivetNo;

            try
            {
                //----------Top Level----------

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHO0136-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Length - 5d) / 1000d;
                if (item.Length == 400d || item.Length == 500d)
                {
                    swFeat = swComp.FeatureByName("MIDDLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("300");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("MIDDLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("300");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHO0134-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D12@Sketch1").SystemValue = (item.Length - 6d) / 1000d;
                if (item.Length == 400d || item.Length == 750d) swPart.Parameter("D5@Sketch27").SystemValue = 28.5d / 1000d;
                else swPart.Parameter("D5@Sketch27").SystemValue = 78.5d / 1000d;
                swFeat = swComp.FeatureByName("750");
                if (item.Length == 750d) swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHO0135-2"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 25d) / 1000d;
                if (item.Length == 400d || item.Length == 750d) swPart.Parameter("D4@Sketch7").SystemValue = 17.5d / 1000d;
                else swPart.Parameter("D4@Sketch7").SystemValue = 67.5d / 1000d;
                swFeat = swComp.FeatureByName("750");
                if (item.Length == 750d) swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0006-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length +50d) / 1000d;
                swFeat = swComp.FeatureByName("ANSUL");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swFeat = swComp.FeatureByName("ABD");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                if (item.Length == 400d || item.Length == 500d)
                {
                    swFeat = swComp.FeatureByName("ABD-MIDDLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("ABD-300");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("ABD-MIDDLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("ABD-300");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0007-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length + 50d) / 1000d;
                swFeat = swComp.FeatureByName("ABD");
                swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                if (item.Length == 400d || item.Length == 500d)
                {
                    swFeat = swComp.FeatureByName("ABD-MIDDLE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("ABD-300");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("ABD-MIDDLE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("ABD-300");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0008-1"));
                swFeat = swComp.FeatureByName("ANDTEC");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0009-1"));
                swFeat = swComp.FeatureByName("ANDTEC");
                swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩

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
