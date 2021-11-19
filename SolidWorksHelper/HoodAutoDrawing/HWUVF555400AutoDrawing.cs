using System;
using System.IO;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class HWUVF555400AutoDrawing : IAutoDrawing
    {
        readonly HWUVF555400Service objHWUVF555400Service = new HWUVF555400Service();

        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            #region 准备工作
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
            HWUVF555400 item = (HWUVF555400)objHWUVF555400Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix; //后缀
           
            ModelDoc2 swPart;
            Component2 swComp;
            Feature swFeat;
            EditPart swEdit = new EditPart();

            //打开Pack后的模型
            ModelDoc2 swModel = swApp.OpenDoc6(packedAssyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
            AssemblyDoc swAssy = swModel as AssemblyDoc; //装配体
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModel.ForceRebuild3(true); //TopOnly参数设置成true，只重建顶层，不重建零件内部 
            #endregion

            #region 中间参数
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
            decimal meshSideLength =
                Convert.ToDecimal((item.Length - 3m - (int)((item.Length - 2m) / 498m) * 498m) / 2m - 2m) / 1000m;
            //HWUWF555400斜侧板CJ孔计算,150为排风底部长度，555-400为高度差
            int sidePanelDownCjNo = (int)(((decimal)(Math.Sqrt(Math.Pow((double)item.Deepth - 150d, 2) + Math.Pow(555d - 400d, 2))) - 95m) / 32m);
            int sidePanelSideCjNo = sidePanelDownCjNo - 3;

            #endregion

            try
            {






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
