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
    public class KVSAutoDrawing : IAutoDrawing
    {
        readonly KVSService objKVSService = new KVSService();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            //创建项目模型存放地址
            string itemPath =$@"{projectPath}\{tree.Item}-{tree.Module}-{tree.CategoryName}";
            if (!CommonFunc.CreateProjectPath(itemPath)) return;
            //Pack的后缀
            string suffix = $@"{tree.Item}-{tree.Module}-{tree.ODPNo.Substring(tree.ODPNo.Length - 6)}";

            //判断文件是否存在，如果存在将不执行pack，如果不存在则执行pack
            //packango后需要接收打包完成的地址，参数为后缀
            string packedAssyPath =$@"{itemPath}\{tree.CategoryName.ToLower()}_{suffix}.sldasm";
            if (!File.Exists(packedAssyPath)) packedAssyPath = CommonFunc.PackAndGoFunc(suffix, swApp, tree.ModelPath, itemPath);
            
            //查询参数
            KVS item = (KVS)objKVSService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            var warnings = 0;
            int errors = 0;
            suffix = "_" + suffix;//后缀

            //打开Pack后的模型
            var swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
            var swAssy = swModel as AssemblyDoc;
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true);//TopOnly参数设置成true，只重建顶层，不重建零件内部
            /*注意SolidWorks单位是m，计算是应当/1000d
             * 整形与整形运算得出的结果仍然时整形，1640 / 1000d结果为0，因此必须将其中一个转化成double型，使用后缀m就可以了
             * (int)不进行四舍五入，Convert.ToInt32会四舍五入
            */
            //-----------计算中间值，----------
            //新风CJ孔数量和新风CJ孔第一个CJ距离边缘距离
            int frontCjNo = (int)((item.Length - 3d - 30d) / 32d) + 1;
            double frontCjFirstDis = (item.Length - 3d - (frontCjNo - 1) * 32d) / 2d;
            //铆钉数量
            int rivetNo = (int)((item.Length - 200d) / 300d);
            double rivetDis = (item.Length - 200d) / rivetNo;
            //为了适应肯德基脖颈缺口
            double midRoofSecondHoleDis = 0d;
            if (item.ExNo == 2) midRoofSecondHoleDis = (item.Length - 300d - item.ExDis - item.ExLength) / 4d;
            else if (item.ExNo == 1) midRoofSecondHoleDis = (item.Length - 300d) / 2d;
            //KSA数量，KSA侧板长度(以全长计算)
            int ksaNo = (int)((item.Length - 7d) / 498d);
            double ksaSideLength = (item.Length - 8d - ksaNo * 498d) / 2d;


            try
            {
                //----------Top Level----------
                ////判断KSA数量，KSA侧板长度，如果太小，则使用特殊小侧板侧边
                //swFeat = swAssy.FeatureByName("LocalLPattern1");
                //if (ksaNo == 1) swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                //else
                //{
                //    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                //    swModel.Parameter("D1@LocalLPattern1").SystemValue = ksaNo; //D1阵列数量,D3阵列距离
                //}
                ////KSA距离左边缘
                //if (ksaSideLength < 12d / 1000d) swModel.Parameter("D1@Distance15").SystemValue = 0.5d / 1000d;
                //else swModel.Parameter("D1@Distance15").SystemValue = ksaSideLength;

                //排风脖颈数量和距离
                Feature swFeat;
                if (item.ExNo == 1)
                {
                    swModel.Parameter("D1@Distance52").SystemValue = (item.Length - item.ExLength) / 2000d;
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                else if (item.ExNo == 2)
                {
                    swModel.Parameter("D1@Distance52").SystemValue = ((item.Length - item.ExDis) / 2d - item.ExLength) / 1000d;
                    swFeat = swAssy.FeatureByName("LocalLPattern1");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swModel.Parameter("D1@LocalLPattern1").SystemValue = item.ExNo; //D1阵列数量,D3阵列距离
                    swModel.Parameter("D3@LocalLPattern1").SystemValue = (item.ExDis + item.ExLength) / 1000d; //D1阵列数量,D3阵列距离
                }
                //日光灯
                Component2 swComp;
                if (item.LightType == "FSSHORT")
                {
                    //SHORT
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0003-3"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0004-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0005-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0005-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020404-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020406-S-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020424-3"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020424-4"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2200300026-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2200300026-3"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    //LONG
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0002-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0002-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0003-4"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0004-3"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020405-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020407-S-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020424-5"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020424-6"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2200300023-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2200300023-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                }
                else
                {
                    //SHORT
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0003-3"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0004-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0005-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0005-2"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020404-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020406-S-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020424-3"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020424-4"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2200300026-1"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2200300026-3"));
                    swComp.SetSuppression2(0); //2解压缩，0压缩
                    //LONG
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0002-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0002-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0003-4"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0004-3"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020405-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020407-S-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020424-5"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020424-6"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2200300023-1"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩
                    swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "2200300023-2"));
                    swComp.SetSuppression2(2); //2解压缩，0压缩 
                }
                //----------排风腔----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0110-1"));
                ModelDoc2 swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Length - 7d) / 1000d;
                swPart.Parameter("D5@Sketch62").SystemValue = (item.ExDis + item.ExLength) / 1000d;
                //排风口
                if (item.ExNo == 1)
                {
                    swFeat = swComp.FeatureByName("EXCOONE");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("EXCOTWO");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D1@Sketch115").SystemValue = item.ExLength / 1000d;
                    swPart.Parameter("D2@Sketch115").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D5@Sketch62").SystemValue = midRoofSecondHoleDis / 1000d;
                }
                else
                {
                    swFeat = swComp.FeatureByName("EXCOONE");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swFeat = swComp.FeatureByName("EXCOTWO");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D4@Sketch114").SystemValue = item.ExDis / 1000d;
                    swPart.Parameter("D2@Sketch114").SystemValue = item.ExLength / 1000d;
                    swPart.Parameter("D1@Sketch114").SystemValue = item.ExWidth / 1000d;
                    swPart.Parameter("D5@Sketch62").SystemValue = (item.ExDis + item.ExLength) / 1000d;
                    if (midRoofSecondHoleDis > 300d)
                    {
                        swFeat = swComp.FeatureByName("MIDROOFINSDIS2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch116").SystemValue = midRoofSecondHoleDis / 1000d;
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("MIDROOFINSDIS2");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                }
                //----------其他零件----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0111-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 6d) / 1000d;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0112-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch3").SystemValue = ksaSideLength / 1000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0113-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch3").SystemValue = ksaSideLength / 1000d;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0114-2"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.ExLength + 50d) / 1000d;
                swPart.Parameter("D2@Sketch1").SystemValue = (item.ExHeight + 54.8d) / 1000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0115-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = (item.ExLength + 50d) / 1000d;
                swPart.Parameter("D2@Sketch1").SystemValue = (item.ExHeight + 54.8d) / 1000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0117-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D1@Sketch1").SystemValue = item.ExLength / 2000d;
                swPart.Parameter("D2@Sketch1").SystemValue = (item.ExWidth + 20d) / 1000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0118-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.ExLength * 2d) / 1000d;

                //----------MiddleRoof灯板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHM0023-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = (item.Length - 3d) / 1000d;
                swPart.Parameter("D1@Sketch1").SystemValue = (item.Deepth - 788.5d) / 1000d;
                if (item.ExNo == 1)
                {
                    swFeat = swComp.FeatureByName("MIDROOFINSDIS2");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D2@Sketch20").SystemValue = midRoofSecondHoleDis / 1000d;
                }
                else
                {
                    swPart.Parameter("D2@Sketch20").SystemValue = (item.ExDis + item.ExLength) / 1000d;
                    if (midRoofSecondHoleDis > 300d)
                    {
                        swFeat = swComp.FeatureByName("MIDROOFINSDIS2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D1@Sketch26").SystemValue = midRoofSecondHoleDis / 1000d;
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("MIDROOFINSDIS2");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                }
                //灯具
                if (item.LightType == "FSSHORT")
                {
                    swFeat = swComp.FeatureByName("FSLONG");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("FSSHORT");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                }
                else
                {
                    swFeat = swComp.FeatureByName("FSLONG");
                    swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩 
                    swFeat = swComp.FeatureByName("FSSHORT");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                }
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "5201020401-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Deepth - 793d) / 1000d;
                //----------大侧板----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0036-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = item.Deepth / 1000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0037-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = item.Deepth / 1000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0038-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = item.Deepth / 1000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHS0039-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Sketch1").SystemValue = item.Deepth / 1000d;
                
                //------------新风腔----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0052-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length -7d)/ 1000d;
                if (item.ExNo == 1)
                {
                    swFeat = swComp.FeatureByName("MIDROOFINSDIS2");
                    swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    swPart.Parameter("D3@Sketch50").SystemValue = midRoofSecondHoleDis / 1000d;
                }
                else
                {
                    swPart.Parameter("D3@Sketch50").SystemValue = (item.ExDis + item.ExLength) / 1000d;
                    if (midRoofSecondHoleDis > 300d)
                    {
                        swFeat = swComp.FeatureByName("MIDROOFINSDIS2");
                        swFeat.SetSuppression2(1, 2, null); //参数1：1解压，0压缩
                        swPart.Parameter("D3@Sketch127").SystemValue = midRoofSecondHoleDis / 1000d;
                    }
                    else
                    {
                        swFeat = swComp.FeatureByName("MIDROOFINSDIS2");
                        swFeat.SetSuppression2(0, 2, null); //参数1：1解压，0压缩
                    }
                }
                swPart.Parameter("D1@LPattern2").SystemValue = rivetNo + 1;
                swPart.Parameter("D3@LPattern2").SystemValue = rivetDis/1000d;

                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0053-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 103d) / 1000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0054-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 3d) / 1000d;
                swPart.Parameter("D1@LPattern1").SystemValue = frontCjNo;
                swPart.Parameter("D10@Sketch8").SystemValue = frontCjFirstDis/1000d;
                swPart.Parameter("D1@LPattern2").SystemValue = rivetNo + 1;
                swPart.Parameter("D3@LPattern2").SystemValue = rivetDis / 1000d;
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0055-1"));
                swPart = swComp.GetModelDoc2();
                swPart.Parameter("D2@Base-Flange1").SystemValue = (item.Length - 7d) / 1000d;
                
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
