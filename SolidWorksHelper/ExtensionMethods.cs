using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.IO;

namespace SolidWorksHelper
{
    //扩展方法
    public static class ExtensionMethods
    {
        /// <summary>
        /// 给文件名添加后缀的方法
        /// </summary>
        public static string AddSuffix(this string partName, string suffix)
        {
            //从-号拆分，-前添加suffix，例如：FNHE0001-1 -> FNHE0001_Item-M1-210203-1 其中（_Item-M1-210203）是suffix
            return $"{partName.Substring(0, partName.LastIndexOf("-", StringComparison.Ordinal))}{suffix}{partName.Substring(partName.LastIndexOf("-", StringComparison.Ordinal))}";
        }

        /// <summary>
        /// 添加半成品清单扩展方法
        /// </summary>        
        public static void AddItem(this Dictionary<string, int> semiBomDic, string drawingNum, int quantity)
        {
            if (semiBomDic.ContainsKey(drawingNum)) semiBomDic[drawingNum] += quantity;
            else semiBomDic.Add(drawingNum, quantity);
        }


        /// <summary>
        /// 无需作图，只需拷贝Dxf图的
        /// </summary>
        public static bool CopyDxfFilesTo(this string oldPath, string newPath)
        {
            string[] files = Directory.GetFiles(oldPath);
            foreach (var name in files)
            {
                string currentFile = newPath + name.Substring(name.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
                //如果文件在程序目录中已经存在，则删除，避免弹窗中断程序
                if (File.Exists(currentFile)) File.Delete(currentFile);
                File.Copy(name, currentFile);
            }
            return true;
        }


        #region PackAndGo扩展方法
        /// <summary>
        /// 普通烟罩模型打包
        /// </summary>
        public static string PackAndGoHood(this SldWorks swApp, ModuleTree tree, string projectPath, out string suffix)
        {
            //1.获得项目模型存档地址，例如:文件夹名为Item-M2-UVF555，它存在某个项目FSOXXXXXX文件夹下
            string itemPath = $@"{projectPath}\{tree.Item}-{tree.Module}-{tree.CategoryName}";
            //2.判断文件夹是否存在，如果不存在则创建它
            if (!Directory.Exists(itemPath)) Directory.CreateDirectory(itemPath);
            //3.获取PackAndGo需要添加的后缀，该后缀需要抛出给作图过程使用,例如：_Item-M1-210203
            suffix = $@"_{tree.Item}-{tree.Module}-{tree.ODPNo.Substring(tree.ODPNo.Length - 6)}";
            //4.获取理论上PackAndGo后的地址，例如：...\UVF555_Item-M1-210203.SLDASM
            string packedAssyPath = $@"{itemPath}\{tree.CategoryName.ToLower()}{suffix}.sldasm";
            //4.判断文件是否存在，如果存在将不执行PackAndGo，直接返回地址
            if (File.Exists(packedAssyPath)) return packedAssyPath;
            //5.如果文件不存在则执行PackAndGo
            return swApp.PackAndGoFunc(tree.ModelPath, itemPath, suffix);
        }


        /// <summary>
        /// 天花烟罩模型打包
        /// </summary>
        public static string PackAndGoCeiling(this SldWorks swApp, ModuleTree tree, string projectPath, out string suffix)
        {
            //1.获得项目模型存档地址，例如STA-HOT-038-M2-UVF555
            string itemPath = $@"{projectPath}\{tree.Item}-{tree.Module}-{tree.CategoryName}";
            //2.判断文件夹是否存在，如果不存在则创建它
            if (!Directory.Exists(itemPath)) Directory.CreateDirectory(itemPath);
            //3.获取PackAndGo需要添加的后缀，该后缀需要抛出给作图过程使用
            suffix = $@"{tree.Item}-{tree.Module}-{tree.ODPNo.Substring(tree.ODPNo.Length - 6)}";
            //4.获取理论上PackAndGo后的地址
            string packedAssyPath = $@"{itemPath}\{tree.CategoryName.ToLower()}{suffix}.sldasm";
            //4.判断文件是否存在，如果存在将不执行PackAndGo，直接返回地址
            if (!File.Exists(packedAssyPath)) return packedAssyPath;
            //5.如果文件不存在则执行PackAndGo
            return swApp.PackAndGoFunc(tree.ModelPath, itemPath, suffix);
        }

        /// <summary>
        /// 打包过程通用方法
        /// </summary>
        public static string PackAndGoFunc(this SldWorks swApp, string modelPath, string itemPath, string suffix)
        {
            swApp.CommandInProgress = true;
            int errors = 0;
            int warnings = 0;

            //打开需要pack的模型
            var swModelDoc = swApp.OpenDoc6(modelPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            var swModelDocExt = swModelDoc.Extension;
            var swPackAndGo = swModelDocExt.GetPackAndGo();
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
            swPackAndGo.AddSuffix = suffix;
            try
            {
                // 执行Pack and Go
                swModelDocExt.SavePackAndGo(swPackAndGo);
            }
            catch (Exception ex)
            {
                throw new Exception("PackAndGo过程中出现异常：" + ex.Message);
            }
            finally
            {
                swApp.CloseDoc(swModelDoc.GetTitle());
                swApp.CommandInProgress = false;//及时关闭外部命令调用，否则影响SolidWorks的使用
            }
            string modelPathName = modelPath.Substring(modelPath.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
            //返回PackAndGo后模型的地址
            return $@"{itemPath}\{modelPathName.Substring(0, modelPathName.LastIndexOf(".", StringComparison.Ordinal))}{suffix}.sldasm";
        }
        #endregion

        #region 绘图代码扩展方法
        /// <summary>
        /// 选择零件带后缀
        /// </summary>
        public static Component2 GetComponentByNameWithSuffix(this AssemblyDoc swAssy, string suffix, string partName)
        {
            return swAssy.GetComponentByName(partName.AddSuffix(suffix));
        }


        /// <summary>
        /// 更改尺寸，int数量
        /// </summary>
        public static void ChangeDim(this ModelDoc2 swModel, string dimName, int intValue)
        {
            swModel.Parameter(dimName).SystemValue = intValue;
        }
        /// <summary>
        /// 更改尺寸，double距离
        /// </summary>
        public static void ChangeDim(this ModelDoc2 swModel, string dimName, double dblValue)
        {
            swModel.Parameter(dimName).SystemValue = dblValue / 1000d;
        }
        /// <summary>
        /// 部件解压特征
        /// </summary>
        public static void Suppress(this Component2 swComp, string featureName)
        {
            swComp.FeatureByName(featureName).SetSuppression2(0, 2, null);
        }
        /// <summary>
        /// 部件不解压特征
        /// </summary>
        public static void UnSuppress(this Component2 swComp, string featureName)
        {
            swComp.FeatureByName(featureName).SetSuppression2(1, 2, null);
        }
        /// <summary>
        /// 装配体解压特征
        /// </summary>
        public static void Suppress(this AssemblyDoc swAssy, string featureName)
        {
            swAssy.FeatureByName(featureName).SetSuppression2(0, 2, null);
        }
        /// <summary>
        /// 装配体不解压特征
        /// </summary>
        public static void UnSuppress(this AssemblyDoc swAssy, string featureName)
        {
            swAssy.FeatureByName(featureName).SetSuppression2(1, 2, null);
        }
        /// <summary>
        /// 装配体解压部件
        /// </summary>
        public static void Suppress(this AssemblyDoc swAssy, string suffix, string compName)
        {
            swAssy.GetComponentByNameWithSuffix(suffix, compName).SetSuppression2(0);
        }
        /// <summary>
        /// 装配体不解压部件
        /// </summary>
        public static Component2 UnSuppress(this AssemblyDoc swAssy, string suffix, string compName)
        {
            Component2 swComp = swAssy.GetComponentByNameWithSuffix(suffix, compName);
            swComp.SetSuppression2(2);
            return swComp;
        } 
        #endregion

    }
}