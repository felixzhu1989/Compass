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
    public class LSDOSTAutoDrawing:IAutoDrawing
    {
        LSDOSTService objLSDOSTService = new LSDOSTService();
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
                DialogResult result = show.ShowMessageBoxTimeout("模型文件夹" + itemPath + "存在，如果之前pack已经执行过，将不执行pack过程而是直接修改模型，如果要中断作图点击YES，继续作图请点击No或者3s后窗口会自动消失", "提示信息", MessageBoxButtons.YesNo, 3000);
                if (result == DialogResult.Yes) return;
            }
            //Pack的后缀
            string suffix = tree.Item + "-" + tree.Module + "-" +
                            tree.ODPNo.Substring(tree.ODPNo.Length - 6);
            //判断文件是否存在，如果存在将不执行pack，如果不存在则执行pack
            //packango后需要接收打包完成的地址，参数为后缀
            string packedAssyPath = itemPath + @"\" + tree.CategoryName.ToLower() + "_" + suffix + ".sldasm";
            if (!File.Exists(packedAssyPath)) packedAssyPath = CommonFunc.PackAndGoFunc(suffix, swApp, tree.ModelPath, itemPath);

            //查询参数
            LSDOST item = (LSDOST)objLSDOSTService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

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

            //打开Pack后的模型
            swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
            swAssy = swModel as AssemblyDoc;//装配体
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);//TopOnly参数设置成true，只重建顶层，不重建零件内部
            /*注意SolidWorks单位是m，计算是应当/1000m
             * 整形与整形运算得出的结果仍然时整形，1640 / 1000m结果为0，因此必须将其中一个转化成decimal型，使用后缀m就可以了
             * (int)不进行四舍五入，Convert.ToInt32会四舍五入
            */
            //-----------计算中间值，----------
            //铆钉数量
            int rivetNo = (int)((item.Length - 47m) / 400m);
            decimal rivetDis = (item.Length - 47m) / rivetNo;

            try
            {
                //----------Top Level----------
                
                //排风脖颈数量和距离
                if (item.SuNo == 1)
                {
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern1").SystemValue = item.SuNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D3@LocalLPattern1").SystemValue = item.SuDis / 1000m; //D1阵列数量,D3阵列距离
                }
                //中间测风压孔，三个脖颈时解压,两块网孔板
                if (item.SuNo==3)
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNLC0005-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNLC0006-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swModel.Parameter("D1@LocalLPattern2").SystemValue = 4; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D3@LocalLPattern2").SystemValue = (item.Length-400m) / 3000m;
                }
                else
                {
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNLC0005-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNLC0006-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swModel.Parameter("D1@LocalLPattern2").SystemValue = 2; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D3@LocalLPattern2").SystemValue = item.Length - 400m / 3000m;
                }
                //----------散流器----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNLC0001-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 7m) / 1000m;
                if (item.SuNo == 1)
                {
                    swFeat = swComp.FeatureByName("LPattern1");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch30").SystemValue = 0;
                }
                else
                {
                    swPart.Parameter("D3@Sketch30").SystemValue = (item.SuDis*(item.SuNo/2m-1)+item.SuDis/2m)/1000;
                    swFeat = swComp.FeatureByName("LPattern1");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern1").SystemValue = item.SuNo; //D1阵列数量,D3阵列距离
                    swPart.Parameter("D3@LPattern1").SystemValue = item.SuDis / 1000m; //D1阵列数量,D3阵列距离
                }
                if (rivetNo < 2)
                {
                    swFeat = swComp.FeatureByName("Cut-Extrude10");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("LPattern2");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                else
                {
                    swPart.Parameter("D1@Sketch43").SystemValue = rivetDis / 1000m;
                    swFeat = swComp.FeatureByName("Cut-Extrude10");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("LPattern2");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern2").SystemValue = rivetNo; //D1阵列数量,D3阵列距离
                    swPart.Parameter("D3@LPattern2").SystemValue = rivetDis / 1000m;
                }
                swFeat = swComp.FeatureByName("Cut-Extrude11");
                if (item.SuNo == 3)swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩

                //中间测风压孔，三个脖颈时解压,两块网孔板,
                //2021.06.04，散流器中间开缺口放置装配干涉，因为框子是先焊接后喷塑，最后装配的
                swFeat = swComp.FeatureByName("Cut-Extrude12");
                if (item.SuNo == 3) swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNLC0003-3"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 4m) / 1000m;
                if (rivetNo < 2)
                {
                    swFeat = swComp.FeatureByName("Cut-Extrude4");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("LPattern1");
                    swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩
                }
                else
                {
                    swPart.Parameter("D1@Sketch11").SystemValue = rivetDis / 1000m;
                    swFeat = swComp.FeatureByName("Cut-Extrude4");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("LPattern1");
                    swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                    swPart.Parameter("D1@LPattern1").SystemValue = rivetNo; //D1阵列数量,D3阵列距离
                    swPart.Parameter("D3@LPattern1").SystemValue = rivetDis / 1000m;
                }
                swFeat = swComp.FeatureByName("Cut-Extrude3");
                if (item.SuNo == 3) swFeat.SetSuppression2(1, 2, configNames); //参数1：1解压，0压缩
                else swFeat.SetSuppression2(0, 2, configNames); //参数1：1解压，0压缩

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNLC0005-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                if (item.SuNo == 3) swPart.Parameter("D1@Skizze1").SystemValue = (item.Length - 112m-34m) / 2000m;
                else swPart.Parameter("D1@Skizze1").SystemValue = (item.Length - 112m) / 1000m;
                
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
