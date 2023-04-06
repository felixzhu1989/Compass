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
    public class HOODBCJAutoDrawing : IAutoDrawing
    {
        Component2 swComp;
        readonly HOODBCJService objHOODBCJService = new HOODBCJService();
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
            HOODBCJ item = (HOODBCJ)objHOODBCJService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());

            swApp.CommandInProgress = true; //告诉SolidWorks，现在是用外部程序调用命令
            int warnings = 0;
            int errors = 0;
            suffix = "_" + suffix;//后缀
            ModelDoc2 swModel;
            ModelDoc2 swPart;
            AssemblyDoc swAssy;
            
           
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
            int cjNumber = (int)((item.Length - 40d) / 30d);//天花烟罩马蹄形CJ孔阵列距离为30
            double firstCjDis = (item.Length - 30d * cjNumber) / 2d;
            if (firstCjDis < 20d) firstCjDis += 20d;
            try
            {
                //----------Main Body----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHA0084-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.ChangeDim("Length@SketchBase", item.Length);
                swPart.ChangeDim("Height@SketchBase", item.Length+1d);
                swPart.ChangeDim("Dis@SketchCj", firstCjDis);
                swPart.ChangeDim("ToRight@SketchSpigot", item.SuDis);


                //----------Side Panel----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNHE0102-1"));
                swPart = swComp.GetModelDoc2();//打开零件3
                swPart.ChangeDim("Height@SketchBase", item.Height-1d);

                //----------其他零件----------
                swComp = swAssy.GetComponentByName(CommonFunc.AddSuffix(suffix, "FNCJ0016-1"));
                swPart = swComp.GetModelDoc2();//打开零件
                swPart.ChangeDim("Length@SketchBase", item.Length-10d);


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
