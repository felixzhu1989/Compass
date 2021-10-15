using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class ExprotDxf
    {
        HoodCutListService objHoodCutListService = new HoodCutListService();
        CeilingCutListService objCeilingCutListService = new CeilingCutListService();
        int warnings = 0;
        int errors = 0;
        Dictionary<string, int> sheetMetalDic = new Dictionary<string, int>();

        /// <summary>
        /// 遍历装配体导出钣金dxf下料图
        /// </summary>
        /// <param name="swApp"></param>
        public void AssyExportDxf(SldWorks swApp)
        {
            Dictionary<string, int> sheetMetalDic = new Dictionary<string, int>();

            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;//获取当前打开的零件
            if (swModel == null)
            {
                MessageBox.Show("没有打开装配体");
                return;
            }





            //判断为装配体时继续执行，否则跳出
            if (swModel.GetType() != (int)swDocumentTypes_e.swDocASSEMBLY) return;
            AssemblyDoc swAssy = (AssemblyDoc)swModel;
            
            //获取所有零部件集合
            var compList = swAssy.GetComponents(false);
            //遍历集合中的所有零部件对象，判断并获取需要导图的零件
            foreach (var item in compList)
            {
                Component2 swComp = (Component2)item;
                //判断需要导出下料图的零件：1.是否显示，2.是否被压缩，3.是否封套，4.是否为零件
                if (swComp.Visible != (int)swComponentVisibilityState_e.swComponentVisible
                    || swComp.IsSuppressed() || swComp.IsEnvelope()
                    || Path.GetExtension(swComp.GetPathName()).ToLower() != ".sldprt")
                    continue;//继续遍历下一个组件

                //递归判断父装配体的状态
                if (ParentCompState(swComp)) continue;

                //获取文档中的实体Body对象集合
                var bodyList = swComp.GetBodies2((int)swBodyType_e.swSolidBody);
                //遍历集合中的所有Body对象,判断是否为钣金
                foreach (var swBody in bodyList)
                {
                    //如果是钣金则将零件地址添加到字典中,存在则数量+1
                    if (!swBody.IsSheetMetal()) continue;
                    if (sheetMetalDic.ContainsKey(swComp.GetPathName())) sheetMetalDic[swComp.GetPathName()] += 1;
                    else sheetMetalDic.Add(swComp.GetPathName(), 1);
                }

            }
            string assyPath = swModel.GetPathName();
            //关闭装配体零件
            swApp.CloseDoc(assyPath);
            string dxfPath = assyPath.Substring(0, assyPath.Length - 7) + "-DXF\\";
            //判断文件夹是否存在，不存在就创建它
            if (!Directory.Exists(dxfPath)) Directory.CreateDirectory(dxfPath);

            //遍历钣金零件
            foreach (var sheetMetal in sheetMetalDic)
            {
                int errors = 0;
                int warnings = 0;

                //打开模型
                ModelDoc2 swPart = swApp.OpenDoc6(sheetMetal.Key, (int)swDocumentTypes_e.swDocPART,
                    (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);

                //导图
                string swModelName = swPart.GetPathName(); ;//零件地址
                string swModelTitle = swPart.GetTitle();
                //带后缀的情况
                string swDxfName = dxfPath + swModelTitle.Substring(0, swModelTitle.Length - 7) + ".dxf";//Dxf图地址,或者dwg文件
                //判断不带后缀的情况
                if (swModelTitle.Substring(swModelTitle.Length - 7).ToLower() != ".sldprt")
                    swDxfName = dxfPath + swModelTitle + ".dxf";

                //导出零件
                
                //关闭零件
                swApp.CloseDoc(sheetMetal.Key);
            }
            //清除字典
            sheetMetalDic.Clear();
        }

        /// <summary>
        /// 递归方法
        /// </summary>
        /// <param name="swComp"></param>
        private bool ParentCompState(Component2 swComp)
        {
            Component2 swParentComp = swComp.GetParent();//获取父装配体
            //直接装配在总装中的零件，GetParent方法会返回null，参见方法的remarks，此时无需判断父装配体
            //不为null，则需要判断父装配体：1.是否显示，2.是否被压缩，3.是否封套
            if (swParentComp != null)
            {
                Debug.Print(swParentComp.GetPathName());
                if (swParentComp.Visible != (int)swComponentVisibilityState_e.swComponentVisible
                    || swParentComp.IsSuppressed() || swParentComp.IsEnvelope())
                    return true;//继续遍历下一个 
                return ParentCompState(swParentComp);//递归操作
            }
            return false;
        }





        /// <summary>
        /// 天花子装配导出DXF图纸
        /// </summary>
        /// <param name="swApp"></param>
        /// <param name="tree"></param>
        /// <param name="dxfPath"></param>
        /// <param name="userId"></param>
        public void CeilingAssyToDxf(SldWorks swApp, SubAssy subAssy, string dxfPath, int userId)
        {
            swApp.CommandInProgress = true;
            List<CeilingCutList> celingCutLists = new List<CeilingCutList>();
            string assyPath = subAssy.SubAssyPath;
            if (assyPath.Length == 0) return;
            try
            {
                //打开模型
                if (!(swApp.OpenDoc6(assyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                    (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) is ModelDoc2 swModel))
                {
                    MessageBox.Show("模型不存在，请认真检查", "模型不存在");
                    return;
                }
                string modulePath = dxfPath + @"\" + subAssy.SubAssyName;
                if (!Directory.Exists(modulePath))
                {
                    Directory.CreateDirectory(modulePath);
                }
                swModel.ForceRebuild3(true);
                AssemblyDoc swAssy = swModel as AssemblyDoc;
                //获取所有零部件集合
                var compList = swAssy.GetComponents(false);
                //遍历集合中的所有零部件对象
                foreach (var swComp in compList)
                {
                    //判断零件是否被压缩，不显示，封套，零件名称不是以sldprt或SLDPRT结尾
                    if (swComp.Visible == 1 && !swComp.IsEnvelope() && !swComp.IsSuppressed() &&
                        (swComp.GetPathName().EndsWith(".sldprt") || swComp.GetPathName().EndsWith(".SLDPRT")))
                    {
                        Component2 swParentComp = swComp.GetParent();
                        //总装没有父装配体
                        if (swParentComp == null)
                        {
                            ConfigurationManager swConfigMgr = swModel.ConfigurationManager;
                            Configuration swConfig2 = swConfigMgr.ActiveConfiguration;
                            swParentComp = swConfig2.GetRootComponent3(true);
                        }
                        //判断父装配体是否可视，并且不封套
                        if (swParentComp.Visible == 1 && !swParentComp.IsEnvelope() && !swComp.IsSuppressed())
                        {
                            PartDoc swPart = swComp.GetModelDoc2();
                            //获取文档中的额Body对象集合
                            var bodyList = swPart.GetBodies2(0, false);
                            //遍历集合中的所有Body对象,判断是否为钣金
                            foreach (var swBody in bodyList)
                            {
                                //如果是钣金则将零件地址添加到列表中
                                if (swBody.IsSheetMetal())
                                {
                                    if (sheetMetalDic.ContainsKey(swComp.GetPathName())) sheetMetalDic[swComp.GetPathName()] += 1;
                                    else sheetMetalDic.Add(swComp.GetPathName(), 1);
                                }
                            }
                        }
                    }
                }
                //关闭装配体零件
                swApp.CloseDoc(assyPath);
                //遍历钣金零件
                foreach (var sheetMetal in sheetMetalDic)
                {
                    //打开模型
                    ModelDoc2 swPart = swApp.OpenDoc6(sheetMetal.Key, (int)swDocumentTypes_e.swDocPART,
                        (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
                    Feature swFeat = (Feature)swPart.FirstFeature();
                    CeilingCutList cutRecord = new CeilingCutList()
                    {
                        SubAssyId = subAssy.SubAssyId,
                        Quantity = sheetMetal.Value,
                        UserId = userId
                    };
                    while (swFeat != null)
                    {
                        var suppStatus = swFeat.IsSuppressed2((int)swInConfigurationOpts_e.swThisConfiguration, null);
                        if (suppStatus[0] == false && swFeat.GetTypeName() == "SolidBodyFolder")
                        {
                            BodyFolder swBodyFolder = (BodyFolder)swFeat.GetSpecificFeature2();
                            swBodyFolder.SetAutomaticCutList(true);
                            swBodyFolder.SetAutomaticUpdate(true);
                            Feature SubFeat = swFeat.GetFirstSubFeature();
                            if (SubFeat != null)
                            {
                                Feature ownerFeature = SubFeat.GetOwnerFeature();
                                BodyFolder swSubBodyFolder = ownerFeature.GetSpecificFeature2();
                                swSubBodyFolder.UpdateCutList();
                                string val = string.Empty;
                                string valout = string.Empty;
                                //bool wasResolved = false;
                                //bool linkToProp = false;
                                SubFeat.CustomPropertyManager.Get4("Bounding Box Length", false, out val, out valout);
                                cutRecord.Length = Convert.ToDecimal(valout);
                                SubFeat.CustomPropertyManager.Get4("Bounding Box Width", false, out val, out valout);
                                cutRecord.Width = Convert.ToDecimal(valout);
                                SubFeat.CustomPropertyManager.Get4("Sheet Metal Thickness", false, out val, out valout);
                                cutRecord.Thickness = Convert.ToDecimal(valout);
                                SubFeat.CustomPropertyManager.Get4("Material", false, out val, out valout);
                                cutRecord.Materials = valout;
                                //swPart.GetActiveConfiguration().CustomPropertyManager.Get6("Description", false, out valout, out val, out wasResolved, out linkToProp);
                                //cutRecord.PartDescription = valout;
                                cutRecord.PartDescription = swPart.CustomInfo["Part Name"];//不用Description了

                                cutRecord.PartNo = swPart.GetTitle().Substring(0, swPart.GetTitle().Length - 7);
                                celingCutLists.Add(cutRecord);//将信息添加到集合中
                            }
                        }
                        swFeat = swFeat.GetNextFeature();
                    }
                    ExportDxfMethod(swApp, swPart, modulePath);
                    //关闭零件
                    swApp.CloseDoc(sheetMetal.Key);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(assyPath + "导图过程发生异常，详细：" + ex.Message);
            }
            finally
            {
                sheetMetalDic.Clear();
                swApp.CloseDoc(assyPath);//关闭，很快
                swApp.CommandInProgress = false; //及时关闭外部命令调用，否则影响SolidWorks的使用
            }
            //基于事务ceilingCutLists提交SQLServer
            if (celingCutLists.Count == 0) return;
            try
            {
                if (objCeilingCutListService.ImportCutList(celingCutLists)) celingCutLists.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("Cutlist导入数据库失败" + ex.Message);
            }
        }
        /// <summary>
        /// 标准烟罩装配体导出dxf图纸
        /// </summary>
        /// <param name="swApp"></param>
        /// <param name="tree"></param>
        /// <param name="dxfPath"></param>
        public void HoodAssyToDxf(SldWorks swApp, ModuleTree tree, string dxfPath, int userId)
        {
            swApp.CommandInProgress = true;
            List<HoodCutList> hoodCutLists = new List<HoodCutList>();
            string assyPath = @"D:\MyProjects\" + tree.ODPNo + @"\" + tree.Item + "-" + tree.Module + "-" + tree.CategoryName
                              + @"\" + tree.CategoryName.ToLower() + "_" + tree.Item + "-" + tree.Module + "-" +
                              tree.ODPNo.Substring(tree.ODPNo.Length - 6) + ".sldasm";
            try
            {
                //打开模型
                if (!(swApp.OpenDoc6(assyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) is ModelDoc2 swModel))
                {
                    MessageBox.Show("模型不存在，请认真检查", "模型不存在");
                    return;
                }
                string modulePath = dxfPath + @"\" + tree.Item + "-" + tree.Module;
                if (!Directory.Exists(modulePath))
                {
                    Directory.CreateDirectory(modulePath);
                }
                swModel.ForceRebuild3(true);
                AssemblyDoc swAssy = swModel as AssemblyDoc;
                //获取所有零部件集合
                var compList = swAssy.GetComponents(false);
                //遍历集合中的所有零部件对象
                foreach (var swComp in compList)
                {
                    //判断零件是否被压缩，不显示，封套，零件名称不是以sldprt或SLDPRT结尾
                    if (swComp.Visible == 1 && !swComp.IsEnvelope() && !swComp.IsSuppressed() &&
                        (swComp.GetPathName().EndsWith(".sldprt") || swComp.GetPathName().EndsWith(".SLDPRT")))
                    {
                        Component2 swParentComp = swComp.GetParent();
                        //总装没有父装配体
                        if (swParentComp == null)
                        {
                            ConfigurationManager swConfigMgr = swModel.ConfigurationManager;
                            Configuration swConfig2 = swConfigMgr.ActiveConfiguration;
                            swParentComp = swConfig2.GetRootComponent3(true);
                        }
                        //判断父装配体是否可视，并且不封套
                        if (swParentComp.Visible == 1 && !swParentComp.IsEnvelope() && !swComp.IsSuppressed())
                        {
                            PartDoc swPart = swComp.GetModelDoc2();
                            //获取文档中的额Body对象集合
                            var bodyList = swPart.GetBodies2(0, false);
                            //遍历集合中的所有Body对象,判断是否为钣金
                            foreach (var swBody in bodyList)
                            {
                                //如果是钣金则将零件地址添加到列表中
                                if (swBody.IsSheetMetal())
                                {
                                    if (sheetMetalDic.ContainsKey(swComp.GetPathName())) sheetMetalDic[swComp.GetPathName()] += 1;
                                    else sheetMetalDic.Add(swComp.GetPathName(), 1);
                                }
                            }
                        }
                    }
                }
                //关闭装配体零件
                swApp.CloseDoc(assyPath);
                //遍历钣金零件,获取Cutlist信息
                foreach (var sheetMetal in sheetMetalDic)
                {
                    //打开模型
                    ModelDoc2 swPart = swApp.OpenDoc6(sheetMetal.Key, (int)swDocumentTypes_e.swDocPART,
                        (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
                    Feature swFeat = (Feature)swPart.FirstFeature();
                    HoodCutList cutRecord = new HoodCutList()
                    {
                        ModuleTreeId = tree.ModuleTreeId,
                        Quantity = sheetMetal.Value,
                        UserId = userId
                    };
                    while (swFeat != null)
                    {
                        var suppStatus = swFeat.IsSuppressed2((int)swInConfigurationOpts_e.swThisConfiguration, null);
                        if (suppStatus[0] == false && swFeat.GetTypeName() == "SolidBodyFolder")
                        {
                            BodyFolder swBodyFolder = (BodyFolder)swFeat.GetSpecificFeature2();
                            swBodyFolder.SetAutomaticCutList(true);
                            swBodyFolder.SetAutomaticUpdate(true);
                            Feature SubFeat = swFeat.GetFirstSubFeature();
                            if (SubFeat != null)
                            {
                                Feature ownerFeature = SubFeat.GetOwnerFeature();
                                BodyFolder swSubBodyFolder = ownerFeature.GetSpecificFeature2();
                                swSubBodyFolder.UpdateCutList();
                                string val = string.Empty;
                                string valout = string.Empty;
                                //bool wasResolved = false;
                                //bool linkToProp = false;
                                SubFeat.CustomPropertyManager.Get4("Bounding Box Length", false, out val, out valout);
                                cutRecord.Length = Convert.ToDecimal(valout);
                                SubFeat.CustomPropertyManager.Get4("Bounding Box Width", false, out val, out valout);
                                cutRecord.Width = Convert.ToDecimal(valout);
                                SubFeat.CustomPropertyManager.Get4("Sheet Metal Thickness", false, out val, out valout);
                                cutRecord.Thickness = Convert.ToDecimal(valout);
                                SubFeat.CustomPropertyManager.Get4("Material", false, out val, out valout);
                                cutRecord.Materials = valout;
                                //swPart.GetActiveConfiguration().CustomPropertyManager.Get6("Description", false, out valout, out val, out wasResolved, out linkToProp);
                                //cutRecord.PartDescription = valout;
                                cutRecord.PartDescription = swPart.CustomInfo["Part Name"];//不用Description了

                                cutRecord.PartNo = swPart.GetTitle().Substring(0, swPart.GetTitle().Length - 7);
                                hoodCutLists.Add(cutRecord);//将信息添加到集合中
                            }
                        }
                        swFeat = swFeat.GetNextFeature();
                    }
                    ExportDxfMethod(swApp, swPart, modulePath);
                    //关闭零件
                    swApp.CloseDoc(sheetMetal.Key);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(assyPath + "导图过程发生异常，详细：" + ex.Message);
            }
            finally
            {
                sheetMetalDic.Clear();
                swApp.CloseDoc(assyPath);//关闭，很快
                swApp.CommandInProgress = false; //及时关闭外部命令调用，否则影响SolidWorks的使用
            }
            //基于事务hoodCutLists提交SQLServer
            if (hoodCutLists.Count == 0) return;
            try
            {
                if (objHoodCutListService.ImportCutList(hoodCutLists)) hoodCutLists.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("Cutlist导入数据库失败" + ex.Message);
            }
        }




        #region 零件导出dxf图通用方法
        public void ExportDxfMethod(SldWorks swApp, ModelDoc2 swModel, string modulePath)
        {

            PartDoc swPart = (PartDoc)swModel;
            if (swPart == null) return;
            string swModelName = swModel.GetPathName();
            string swModelTitle = swModel.GetTitle();
            //带后缀的情况
            string swDxfName = modulePath + @"\" + swModelTitle.Substring(0, swModelTitle.Length - 6) + "dxf";
            //判断不带后缀的情况
            if (swModelTitle.Substring(swModelTitle.Length - 7).ToUpper() != ".SLDPRT")
                swDxfName = modulePath + @"\" + swModelTitle + ".dxf";

            double[] dataAlignment = new double[12];
            dataAlignment[0] = 0.0;
            dataAlignment[1] = 0.0;
            dataAlignment[2] = 0.0;
            dataAlignment[3] = 1.0;
            dataAlignment[4] = 0.0;
            dataAlignment[5] = 0.0;
            dataAlignment[6] = 0.0;
            dataAlignment[7] = 1.0;
            dataAlignment[8] = 0.0;
            dataAlignment[9] = 0.0;
            dataAlignment[10] = 0.0;
            dataAlignment[11] = 1.0;
            //Array[0], Array[1], Array[2] - XYZ coordinates of new origin
            //Array[3], Array[4], Array[5] - coordinates of new x direction vector
            //Array[6], Array[7], Array[8] - coordinates of new y direction vector
            //判断XYAXIS，长边作为X轴，短的作为Y轴，用于限定拉丝方向
            bool status = false;
            if (swModel.Extension.SelectByID2("XYAXIS", "SKETCH", 0, 0, 0, false, 0, null, 0)) status = true;
            else if (swModel.Extension.SelectByID2("xyaxis", "SKETCH", 0, 0, 0, false, 0, null, 0)) status = true;
            else if (swModel.Extension.SelectByID2("XY", "SKETCH", 0, 0, 0, false, 0, null, 0)) status = true;
            else if (swModel.Extension.SelectByID2("xy", "SKETCH", 0, 0, 0, false, 0, null, 0)) status = true;
            if (status)
            {
                Feature swFeature = swModel.SelectionManager.GetSelectedObject6(1, -1);
                Sketch swSketch = swFeature.GetSpecificFeature2();
                var swSketchPoints = swSketch.GetSketchPoints2();//获取草图中的所有点
                                                                 //用这三个点抓取直线，并判断长度，长边作为X轴，画3D草图的时候一次性画出两条线，不能分两次画出，否则会判断错误
                SketchPoint p0 = swSketchPoints[0];//最先画的点
                SketchPoint p1 = swSketchPoints[1];//作为坐标原点
                SketchPoint p2 = swSketchPoints[2];//最后画的点
                dataAlignment[0] = p1.X;
                dataAlignment[1] = p1.Y;
                dataAlignment[2] = p1.X;
                double l1 = Math.Sqrt(Math.Pow(p0.X - p1.X, 2) + Math.Pow(p0.Y - p1.Y, 2) + Math.Pow(p0.Z - p1.Z, 2));
                double l2 = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2) + Math.Pow(p2.Z - p1.Z, 2));
                if (l1 > l2)
                {
                    dataAlignment[3] = p0.X - p1.X;
                    dataAlignment[4] = p0.Y - p1.Y;
                    dataAlignment[5] = p0.Z - p1.Z;
                    dataAlignment[6] = p2.X - p1.X;
                    dataAlignment[7] = p2.Y - p1.Y;
                    dataAlignment[8] = p2.Z - p1.Z;
                }
                else
                {
                    dataAlignment[3] = p2.X - p1.X;
                    dataAlignment[4] = p2.Y - p1.Y;
                    dataAlignment[5] = p2.Z - p1.Z;
                    dataAlignment[6] = p0.X - p1.X;
                    dataAlignment[7] = p0.Y - p1.Y;
                    dataAlignment[8] = p0.Z - p1.Z;
                }
            }
            object varAlignment = dataAlignment;
            //Export sheet metal to a single drawing file将钣金零件导出单个dxf文件
            //include flat-pattern geometry，倒数第二位数字1代表钣金展开，options = 1;
            try
            {
                swPart.ExportToDWG2(swDxfName, swModelName, (int)swExportToDWG_e.swExportToDWG_ExportSheetMetal, true, varAlignment, false, false, 1, null);
            }
            catch (Exception ex)
            {
                throw new Exception(swModelName + "导图过程发生异常" + ex.Message);
            }
        } 
        #endregion
    }
}
