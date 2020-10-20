using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;

namespace SolidWorksHelper
{
    public class UCPDXFAutoDrawing : IAutoDrawing
    {
        UCPDXFService objUCPDXFService = new UCPDXFService();
        DXFCutListService objDxfCutListService = new DXFCutListService();
        HoodCutListService objHoodCutListService=new HoodCutListService();
        List<HoodCutList> hoodCutLists = new List<HoodCutList>();
        public void AutoDrawing(SldWorks swApp, ModuleTree tree, string projectPath)
        {
            //创建下料图文件夹，默认在D盘MyProjects目录下（先判断文件夹是否存在）
            string dxfPath = projectPath + @"\DXF-CUTLIST";
            if (!Directory.Exists(dxfPath)) Directory.CreateDirectory(dxfPath);
            //创建dxf图文件夹
            string newPath = dxfPath + @"\" + tree.Item + "-" + tree.Module + @"\";
            if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);
            //拷贝文件，调用通用函数
            if (!CommonFunc.CopyDxfFiles(tree.ModelPath, newPath)) return;
            //查询参数
            UCPDXF objUcpDxf = (UCPDXF)objUCPDXFService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            //查询标准DxfCutlist，根据item.categoryId查询dxfCutList对象列表
            List<DXFCutList> oldList = objDxfCutListService.GetDXFCutListsByCategoryId(tree.CategoryId.ToString());
            
            //乘以数量，赋值moduletreeid
            foreach (var item in oldList)
            {
                hoodCutLists.Add(new HoodCutList()
                {
                    ModuleTreeId = objUcpDxf.ModuleTreeId,
                    PartDescription = item.PartDescription,
                    Length = item.Length,
                    Width = item.Width,
                    Thickness = item.Thickness,
                    Quantity = item.Quantity * objUcpDxf.Quantity,//多个UCP
                    Materials = item.Materials,
                    PartNo = item.PartNo,
                    UserId = 1
                });
            }
            //基于事务hoodCutLists提交SQLServer
            if (hoodCutLists.Count == 0) return;
            try
            {
                if (objHoodCutListService.ImportCutList(hoodCutLists)) hoodCutLists.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("UCPDXF的Cutlist导入数据库失败" + ex.Message);
            }

        }
    }
}
