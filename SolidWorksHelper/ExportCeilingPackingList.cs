using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace SolidWorksHelper
{
    public class ExportCeilingPackingList
    {

        int warnings = 0;
        int errors = 0;
        Dictionary<string, int> sheetMetaDic = new Dictionary<string, int>();
        List<CeilingAccessory> ceilingAccessories=new List<CeilingAccessory>();
        CeilingAccessoryService objCeilingAccessoryService=new CeilingAccessoryService();
        DrawingPlanService objDrawingPlanService=new DrawingPlanService();
        
        public void CeilingAssyToPackingList(SldWorks swApp, string assyPath,Project objProject,int userId,string sbu)
        {
            swApp.CommandInProgress = true;
            List<CeilingAccessory> celingAccessories = new List<CeilingAccessory>();
            try
            {
                //打开模型
                if (!(swApp.OpenDoc6(assyPath, (int)swDocumentTypes_e.swDocASSEMBLY,
                    (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) is ModelDoc2 swModel))
                {
                    MessageBox.Show("模型不存在，请认真检查", "模型不存在");
                    return;
                }
                swModel.ForceRebuild3(true);
                AssemblyDoc swAssy = swModel as AssemblyDoc;
                //获取所有零部件集合
                var compList = swAssy.GetComponents(false);


                //遍历集合中的所有零部件对象
                foreach (var swComp in compList)
                {
                    //判断零件是否被压缩，不显示，封套，零件名称不是以sldprt或SLDPRT结尾，(导出发货清单无需判断可见，封套，无需判断钣金，只要没有被压缩就行了)
                    if (!swComp.IsSuppressed() && (swComp.GetPathName().EndsWith(".sldprt") || swComp.GetPathName().EndsWith(".SLDPRT")))
                    {
                        Component2 swParentComp = swComp.GetParent();
                        //总装没有父装配体
                        if (swParentComp == null)
                        {
                            ConfigurationManager swConfigMgr = swModel.ConfigurationManager;
                            Configuration swConfig2 = swConfigMgr.ActiveConfiguration;
                            swParentComp = swConfig2.GetRootComponent3(true);
                        }

                        //判断父装配体是否可视，并且不封套(导出发货清单无需判断可见，封套，无需判断钣金，只要没有被压缩就行了)
                        if (swParentComp.Visible == 1 && !swComp.IsSuppressed())
                        {
                            //过滤需要的零件，全部转换成大写
                            if (swComp.GetPathName().Contains("["))
                            {
                                //截取关键字
                                string keyWord = swComp.GetPathName();
                                if (keyWord.Contains(")"))
                                {
                                    keyWord = keyWord.Substring(0, keyWord.IndexOf(")") + 1);
                                    keyWord = keyWord.Substring(keyWord.IndexOf("[")).ToUpper();
                                }
                                else if (keyWord.Contains("}"))
                                {
                                    keyWord = keyWord.Substring(0, keyWord.IndexOf("}") + 1);
                                    keyWord = keyWord.Substring(keyWord.IndexOf("[")).ToUpper();
                                }
                                else
                                {
                                    keyWord = keyWord.Substring(0, keyWord.IndexOf("]") + 1);
                                    keyWord = keyWord.Substring(keyWord.IndexOf("[")).ToUpper();
                                }

                                if (sheetMetaDic.ContainsKey(keyWord)) sheetMetaDic[keyWord] += 1;
                                else sheetMetaDic.Add(keyWord, 1);
                            }
                        }
                    }
                }
                //关闭装配体零件
                //swApp.CloseDoc(assyPath);
                foreach (var item in sheetMetaDic)
                {
                    //获取关键字，查找对象
                    string partNo = "";
                    string length = "";
                    string width = "";
                    if (item.Key.Contains("-")) partNo = item.Key.Substring(1, item.Key.IndexOf("-")-1);
                    else partNo = item.Key.Substring(1, item.Key.IndexOf("]")-1);
                    CeilingAccessory objCeilingAccessory = objCeilingAccessoryService.GetCeilingAccessoryByPartNo(partNo);
                    if(objCeilingAccessory==null)continue;
                    //给对象赋值
                    objCeilingAccessory.PartNo = item.Key.Substring(1, item.Key.IndexOf("]")-1);
                    objCeilingAccessory.Quantity = item.Value;
                    objCeilingAccessory.ProjectId = objProject.ProjectId;
                    objCeilingAccessory.UserId = userId;
                    objCeilingAccessory.Location = objDrawingPlanService.GetDrawingPlanByProjectId(objProject.ProjectId.ToString(),sbu)[0].Item;

                    if (item.Key.Contains(")"))
                    {
                        width = item.Key.Substring(0, item.Key.IndexOf(")"));
                        width = width.Substring(width.IndexOf("(")+1);
                        objCeilingAccessory.Width = width;
                    }
                    if (item.Key.Contains("}"))
                    {
                        length = item.Key.Substring(0, item.Key.IndexOf("}"));
                        length = length.Substring(length.IndexOf("{") + 1);
                        objCeilingAccessory.Length = length;
                    }
                    //添加list
                    ceilingAccessories.Add(objCeilingAccessory);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(assyPath + "装配体导出发货清单过程发生异常，详细：" + ex.Message);
            }
            finally
            {
                sheetMetaDic.Clear();
                swApp.CloseDoc(assyPath);//关闭，很快
                swApp.CommandInProgress = false; //及时关闭外部命令调用，否则影响SolidWorks的使用
            }
            //基于事务ceilingCutLists提交SQLServer
            if (ceilingAccessories.Count == 0) return;
            try
            {
                if (objCeilingAccessoryService.ImportCeilingPackingListByTran(ceilingAccessories)) ceilingAccessories.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("Cutlist导入数据库失败" + ex.Message);
            }
        }
    }
}
