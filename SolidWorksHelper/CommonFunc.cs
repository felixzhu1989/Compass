using System;
using System.IO;
using System.Windows.Forms;
using Common;
using SolidWorks.Interop.swconst;
using SolidWorks.Interop.sldworks;

namespace SolidWorksHelper
{
    public static class CommonFunc
    {
        /// <summary>
        /// 创建项目模型存放地址
        /// </summary>
        /// <param name="itemPath"></param>
        /// <returns></returns>
        public static bool CreateProjectPath(string itemPath)
        {
            if (!Directory.Exists(itemPath))
            {
                Directory.CreateDirectory(itemPath);
            }
            else
            {
                Common.ShowMsg show = new ShowMsg();
                DialogResult result = show.ShowMessageBoxTimeout("模型文件夹" + itemPath + "存在，如果之前pack已经执行过，将不执行pack过程而是直接修改模型，如果要中断作图点击YES，继续作图请点击No或者3s后窗口会自动消失", "提示信息", MessageBoxButtons.YesNo, 3000);
                if (result == DialogResult.Yes) return false;
            }
            return true;
        }


        public static bool CopyDxfFiles(string oldPath,string newPath)
        {
            string[] files = Directory.GetFiles(oldPath);
            foreach (var name in files)
            {
                string currentFile = newPath + name.Substring(name.LastIndexOf(@"\") + 1);
                //如果文件在程序目录中已经存在，则删除，避免弹窗中断程序
                if (File.Exists(currentFile)) File.Delete(currentFile);
                File.Copy(name, currentFile);
            }
            return true;
        }
        /// <summary>
        /// 文件名添加后缀
        /// </summary>
        /// <param name="suffix">后缀</param>
        /// <param name="partName">文件名</param>
        /// <returns></returns>
        public static string AddSuffix(string suffix, string partName)
        {
            return partName.Substring(0, partName.LastIndexOf("-")) + suffix + partName.Substring(partName.LastIndexOf("-"));
        }
        
        /// <summary>
        /// 模型打包
        /// </summary>
        /// <param name="suffix">后缀</param>
        /// <param name="swApp">SW程序</param>
        /// <param name="modelPath">模型地址</param>
        /// <param name="itemPath">目标地址</param>
        /// <returns></returns>
        public static string PackAndGoFunc(string suffix, SldWorks swApp, string modelPath, string itemPath)
        {
            swApp.CommandInProgress = true;
            int warnings = 0;
            int errors = 0;
            ModelDoc2 swModelDoc = default(ModelDoc2);
            ModelDocExtension swModelDocExt = default(ModelDocExtension);
            PackAndGo swPackAndGo = default(PackAndGo);
            //打开需要pack的模型
            swModelDoc = (ModelDoc2)swApp.OpenDoc6(modelPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            swModelDocExt = (ModelDocExtension)swModelDoc.Extension;
            swPackAndGo = (PackAndGo)swModelDocExt.GetPackAndGo();
            // Get number of documents in assembly
            //namesCount = swPackAndGo.GetDocumentNamesCount();
            //Debug.Print("  Number of model documents: " + namesCount);
            // Include any drawings, SOLIDWORKS Simulation results, and SOLIDWORKS Toolbox components
            swPackAndGo.IncludeDrawings = false;
            swPackAndGo.IncludeSimulationResults = false;
            swPackAndGo.IncludeToolboxComponents = false;
            swPackAndGo.IncludeSuppressed = true;

            // Set folder where to save the files,目标存放地址
            swPackAndGo.SetSaveToName(true, itemPath);
            //将文件展开到一个文件夹内，不要原始模型的文件夹结构
            // Flatten the Pack and Go folder structure; save all files to the root directory
            swPackAndGo.FlattenToSingleFolder = true;

            // Add a prefix and suffix to the filenames
            //swPackAndGo.AddPrefix = "SW_";添加后缀
            swPackAndGo.AddSuffix = "_" + suffix;
            try
            {
                // Pack and Go，执行PackAndGo
                swModelDocExt.SavePackAndGo(swPackAndGo);
            }
            catch (Exception ex)
            {
                throw new Exception("PackandGo过程中出现异常：" + ex.Message);
            }
            finally
            {
                swApp.CloseDoc(swModelDoc.GetTitle());
                swModelDoc = null;
                swApp.CommandInProgress = false;//及时关闭外部命令调用，否则影响SolidWorks的使用
            }
            string modelPathName = modelPath.Substring(modelPath.LastIndexOf(@"\") + 1);
            //返回packandgo后模型的地址
            return itemPath + @"\" + modelPathName.Substring(0, modelPathName.LastIndexOf(".")) + "_" + suffix + ".sldasm";
        }
    }
}
