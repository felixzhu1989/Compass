
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    class export
    {
        HoodCutListService objHoodCutListService = new HoodCutListService();
        CeilingCutListService objCeilingCutListService = new CeilingCutListService();
        int warnings = 0;
        int errors = 0;
        Dictionary<string, int> sheetMetalDic = new Dictionary<string, int>();
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

                
                //判断为装配体时继续执行，否则跳出
                if (swModel.GetType() != (int)swDocumentTypes_e.swDocASSEMBLY) return;
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
                    //ExportDxfMethod(swApp, swPart, modulePath);
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


    }
}
