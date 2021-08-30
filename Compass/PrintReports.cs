using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DAL;
using Microsoft.Office.Interop.Excel;
using Models;
using Models.Model;
using Drawing = Models.Drawing;

namespace Compass
{
    public class PrintReports
    {
        #region JobCard
        /// <summary>
        /// 天花烟罩打印JobCard
        /// </summary>
        /// <param name="tree"></param>
        public void ExecPrintCeilingJobCard(Project objProject, string itemNo, string model,string sbu)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            string excelBookPath = Environment.CurrentDirectory + "\\JobCard_Ceiling.xlsx";
            excelApp.Workbooks.Add(excelBookPath);
            Microsoft.Office.Interop.Excel.Worksheet workSheet = excelApp.Worksheets[1];

            //通用信息
            workSheet.Cells[3, 3] = objProject.ODPNo;
            workSheet.Cells[4, 3] = objProject.BPONo;
            workSheet.Cells[5, 3] = objProject.ProjectName;
            workSheet.Cells[6, 3] = objProject.CustomerName;
            workSheet.Cells[13, 7] = objProject.ShippingTime.ToShortDateString();
            //天花烟罩
            workSheet.Cells[7, 3] = itemNo;
            workSheet.Cells[8, 3] = model;
            workSheet.Cells[20, 4] = "";
            workSheet.Cells[20, 5] = "";
            workSheet.Cells[20, 6] = "";
            workSheet.Cells[20, 7] = "";
            workSheet.Cells[20, 8] = "";
            //特殊要求
            List<string> srList = new RequirementService().GetSpecialRequirementList(objProject.ODPNo,sbu);
            for (int i = 0; i < srList.Count; i++)
            {
                if (i > 6) continue;
                workSheet.Cells[23 + i * 2, 3] = srList[i];
            }
            ////预览
            //excelApp.Visible = true;
            //excelApp.Sheets.PrintPreview(true);
            //打印
            workSheet.PrintOutEx();
            KillProcess(excelApp);
            excelApp = null;//对象置空
            GC.Collect(); //垃圾回收机制
        }


        /// <summary>
        /// 标准烟罩打印JobCard
        /// </summary>
        /// <param name="tree"></param>
        public void ExecPrintHoodJobCard(ModuleTree tree)
        {
            JobCard objJobCard = new JobCardService().GetJobCard(tree);
            //核对信息
            if (objJobCard.Length == 0)
            {
                MessageBox.Show("编号" + objJobCard.Item + "中" + objJobCard.Module + "烟罩数据没有填写，请认真检查", "信息核对");
                return;
            }
            if (objJobCard.LabelImage.Length == 0)
            {
                MessageBox.Show("编号" + objJobCard.Item + "中" + objJobCard.Module + "JobCard标签截图没有上传，请回到模型树中双击Item上传截图，请认真检查", "信息核对");
                return;
            }
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            string excelBookPath = Environment.CurrentDirectory + "\\JobCard.xlsx";
            excelApp.Workbooks.Add(excelBookPath);
            Microsoft.Office.Interop.Excel.Worksheet workSheet = excelApp.Worksheets[1];
            //插入Item图片
            if (objJobCard.LabelImage.Length != 0)
            {
                Image image = (Image)new Common.SerializeObjectToString().DeserializeObject(objJobCard.LabelImage);
                string imagePath = Environment.CurrentDirectory + "\\LabelImage.jpg";
                if (File.Exists(imagePath)) File.Delete(imagePath);//删除图片
                else
                {
                    //保存图片到系统目录中
                    image.Save(imagePath);
                    //将图片插入excel
                    workSheet.Shapes.AddPicture(imagePath, Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoTrue, 5, 157, 580, 225);//左，上，宽度，高度
                    workSheet.Shapes.AddPicture(imagePath, Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoTrue, 5, 545, 580, 225);//左，上，宽度，高度
                    //使用完毕后删除保存的图片
                    File.Delete(imagePath);
                }
            }
            //通用信息
            workSheet.Cells[48, 3] = objJobCard.ODPNo;
            workSheet.Cells[49, 3] = objJobCard.BPONo;
            workSheet.Cells[50, 3] = objJobCard.ProjectName;
            workSheet.Cells[51, 3] = objJobCard.CustomerName;
            workSheet.Cells[58, 7] = objJobCard.ShippingTime.ToShortDateString();

            //标准烟罩
            if (objJobCard.HoodType == "Hood")
            {
                workSheet.Cells[52, 3] = objJobCard.Item + "(" + objJobCard.Module + ")";
                workSheet.Cells[53, 3] = objJobCard.Model;
                workSheet.Cells[65, 4] = objJobCard.Module;
                //长度
                if (objJobCard.Model == "KVI" || objJobCard.Model == "KVF" || objJobCard.Model == "UVI" || objJobCard.Model == "UVF"
                    || objJobCard.Model == "KWI" || objJobCard.Model == "KWF" || objJobCard.Model == "UWI" || objJobCard.Model == "UWF"
                    || objJobCard.Model == "KVIM" || objJobCard.Model == "KVFM" || objJobCard.Model == "UVIM"  || objJobCard.Model == "UVFM"
                    || objJobCard.Model == "KCH" || objJobCard.Model == "CMOD" )
                {
                    if (objJobCard.Length != 0 && objJobCard.SidePanel == "BOTH")
                        workSheet.Cells[65, 5] = objJobCard.Length + 100;
                    else if (objJobCard.SidePanel == "MIDDLE") workSheet.Cells[65, 5] = objJobCard.Length;
                    else workSheet.Cells[65, 5] = objJobCard.Length + 50;
                }
                else workSheet.Cells[65, 5] = objJobCard.Length;
                workSheet.Cells[65, 6] = objJobCard.Deepth;
                workSheet.Cells[65, 7] = objJobCard.Height;
                workSheet.Cells[65, 8] = objJobCard.SidePanel;
            }
            //天花烟罩
            else if (objJobCard.HoodType == "Ceiling")
            {
                workSheet.Cells[52, 3] = objJobCard.Item;
                workSheet.Cells[53, 3] = objJobCard.Model;
                workSheet.Cells[65, 4] = "";
                workSheet.Cells[65, 5] = "";
                workSheet.Cells[65, 6] = "";
                workSheet.Cells[65, 7] = "";
                workSheet.Cells[65, 8] = "";
            }
            //UL烟罩
            else
            {

            }
            //特殊要求
            List<string> srList = new RequirementService().GetSpecialRequirementList(objJobCard.ODPNo,tree.SBU);
            for (int i = 0; i < srList.Count; i++)
            {
                if (i > 6) continue;
                workSheet.Cells[68 + i * 2, 3] = srList[i];
            }
            //预览
            //excelApp.Visible = true;
            //excelApp.Sheets.PrintPreview(true);
            //打印
            workSheet.PrintOutEx();
            KillProcess(excelApp);
            excelApp = null;//对象置空
            GC.Collect(); //垃圾回收机制
        }
        #endregion

        #region 装箱清单
        /// <summary>
        /// 天花烟罩导出发货清单excel文件
        /// <param name="objProject"></param>
        /// <param name="dgvCeilingPackingList"></param>
        /// </summary>
        public bool ExecExportCeilingPackingList(Project objProject, DataGridView dgvCeilingPackingList)
        {
            return true;
        }
        /// <summary>
        /// 打印发货清单
        /// </summary>
        /// <param name="objProject"></param>
        /// <param name="dgvCeilingPackingList"></param>
        /// <returns></returns>
        public bool ExecPrintCeilingPackingList(Project objProject, DataGridView dgv)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            string excelBookPath = Environment.CurrentDirectory + "\\CeilingPackingList.xlsx";
            excelApp.Workbooks.Add(excelBookPath);
            Microsoft.Office.Interop.Excel.Worksheet workSheet = excelApp.Worksheets[1];

            //将区域添加到字典中并计数
            Dictionary<string, int> locationList = new Dictionary<string, int>();
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (locationList.ContainsKey(dgv.Rows[i].Cells["Location"].Value.ToString())) locationList[dgv.Rows[i].Cells["Location"].Value.ToString()] += 1;
                else locationList.Add(dgv.Rows[i].Cells["Location"].Value.ToString(), 1);
            }
            int startRow = 0;
            int endRow = 0;
            foreach (var item in locationList)
            {
                endRow = startRow + item.Value;
                workSheet.Cells[1, 1] = objProject.ODPNo + "-天花烟罩发货清单(Ceiling Hood Packing List)-" + item.Key;
                workSheet.Cells[2, 3] = objProject.ProjectName;
                workSheet.Cells[3, 3] = DateTime.Now.ToShortDateString();
                workSheet.Cells[4, 3] = dgv.Rows[1].Cells["UserAccount"].Value;

                FillCeilingPackingListDate(workSheet, dgv, startRow, endRow);
                //预览
                //excelApp.Visible = true;
                //excelApp.Sheets.PrintPreview(true);
                //打印
                workSheet.PrintOutEx();

                Microsoft.Office.Interop.Excel.Range range = workSheet.Rows["7:" + (item.Value + 7), Missing.Value];
                range.Delete(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                startRow = endRow;
            }

            KillProcess(excelApp);
            excelApp = null;//对象置空
            GC.Collect(); //垃圾回收机制
            return true;
        }
        /// <summary>
        /// 保存发货清单excel文件
        /// </summary>
        /// <param name="objProject"></param>
        /// <param name="dgvCeilingPackingList"></param>
        /// <returns></returns>
        public bool ExecSaveCeilingPackingList(Project objProject, DataGridView dgv)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            string excelBookPath = Environment.CurrentDirectory + "\\CeilingPackingList.xlsx";
            Microsoft.Office.Interop.Excel.Workbook workBook = excelApp.Workbooks.Add(excelBookPath);
            Microsoft.Office.Interop.Excel.Worksheet workSheet = excelApp.Worksheets[1];

            //将区域添加到字典中并计数
            Dictionary<string, int> locationList = new Dictionary<string, int>();
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (locationList.ContainsKey(dgv.Rows[i].Cells["Location"].Value.ToString())) locationList[dgv.Rows[i].Cells["Location"].Value.ToString()] += 1;
                else locationList.Add(dgv.Rows[i].Cells["Location"].Value.ToString(), 1);
            }
            int startRow = 0;
            int endRow = 0;
            foreach (var item in locationList)
            {
                endRow = startRow + item.Value;
                workSheet.Cells[1, 1] = objProject.ODPNo + "-天花烟罩发货清单(Ceiling Hood Packing List)-" + item.Key;
                workSheet.Cells[2, 3] = objProject.ProjectName;
                workSheet.Cells[3, 3] = DateTime.Now.ToShortDateString();
                workSheet.Cells[4, 3] = dgv.Rows[1].Cells["UserAccount"].Value;

                FillCeilingPackingListDate(workSheet, dgv, startRow, endRow);
                //预览
                //excelApp.Visible = true;
                //excelApp.Sheets.PrintPreview(true);
                //打印
                //workSheet.PrintOutEx();
                //另存为
                string excelPath = @"D:\MyProjects\" + objProject.ODPNo;
                if (!Directory.Exists(excelPath)) Directory.CreateDirectory(excelPath);
                workBook.SaveAs(excelPath + @"\" + objProject.ODPNo + "-"+ item.Key + "天花烟罩发货清单.xlsx", XlFileFormat.xlOpenXMLWorkbook);
                
                Microsoft.Office.Interop.Excel.Range range = workSheet.Rows["7:" + (item.Value + 7), Missing.Value];
                range.Delete(Microsoft.Office.Interop.Excel.XlDirection.xlDown);
                startRow = endRow;
            }
            
            

            KillProcess(excelApp);
            excelApp = null;//对象置空
            GC.Collect(); //垃圾回收机制
            return true;
        }



        /// <summary>
        /// 填天花烟罩发货清单数据方法
        /// </summary>
        /// <param name="workSheet"></param>
        /// <param name="dgv"></param>
        private void FillCeilingPackingListDate(Microsoft.Office.Interop.Excel.Worksheet workSheet, DataGridView dgv, int startRow, int endRow)
        {
            int j = 0;
            for (int i = startRow; i < endRow; i++)
            {
                workSheet.Cells[j + 7, 2] = dgv.Rows[i].Cells["Location"].Value;
                workSheet.Cells[j + 7, 3] = dgv.Rows[i].Cells["PartDescription"].Value;
                workSheet.Cells[j + 7, 4] = dgv.Rows[i].Cells["PartNo"].Value;
                workSheet.Cells[j + 7, 5] = dgv.Rows[i].Cells["Quantity"].Value;
                workSheet.Cells[j + 7, 6] = dgv.Rows[i].Cells["Unit"].Value;
                workSheet.Cells[j + 7, 7] = dgv.Rows[i].Cells["Length"].Value;
                workSheet.Cells[j + 7, 8] = dgv.Rows[i].Cells["Width"].Value;
                workSheet.Cells[j + 7, 9] = dgv.Rows[i].Cells["Height"].Value;
                workSheet.Cells[j + 7, 10] = dgv.Rows[i].Cells["Material"].Value;
                workSheet.Cells[j + 7, 11] = dgv.Rows[i].Cells["Remark"].Value;
                j++;
            }
            //设置边框
            Microsoft.Office.Interop.Excel.Range range = workSheet.get_Range("A7", "K" + (endRow - startRow + 6));
            range.Borders.Value = 1;
            //设置列宽
            workSheet.Columns.AutoFit();
            //workSheet.Cells[1, 1].ColumnWidth = 30;
        }
        /// <summary>
        /// 打印天花烟罩标签
        /// </summary>
        /// <param name="objProject"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal bool ExecPrintCeilingLabel(Project objProject, List<CeilingAccessory> itemList)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            string excelBookPath = Environment.CurrentDirectory + "\\CeilingLabel.xlsx";
            excelApp.Workbooks.Add(excelBookPath);
            Microsoft.Office.Interop.Excel.Worksheet workSheet = excelApp.Worksheets[1];
            foreach (var item in itemList)
            {
                workSheet.Cells[1, 1] = objProject.ODPNo;
                string descEng = item.PartDescription.Substring(item.PartDescription.IndexOf("(") + 1);
                workSheet.Cells[2, 1] = item.PartDescription.Substring(0, item.PartDescription.IndexOf("("));
                workSheet.Cells[3, 1] = descEng.Substring(0, descEng.Length - 1);
                workSheet.Cells[4, 1] = item.PartNo;
                workSheet.Cells[5, 1] = item.Quantity + "-" + item.Unit + " (" + item.Location + ")";
                workSheet.Cells[6, 1] = item.Material;
                workSheet.Cells[7, 1] = item.Length + "x" + item.Width + "x" + item.Height + "(mm)";
                //打印
                workSheet.PrintOutEx();
            }
            //预览
            //excelApp.Visible = true;
            //excelApp.Sheets.PrintPreview(true);
            KillProcess(excelApp);
            excelApp = null;//对象置空
            GC.Collect(); //垃圾回收机制
            return true;
        }
        /// <summary>
        /// 标准烟罩导出装箱清单
        /// </summary>
        /// <param name="jobCardList"></param>
        public void ExecExportHoodPackingList(List<JobCard> jobCardList)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            string excelBookPath = Environment.CurrentDirectory + "\\HoodPackingList.xlsm";
            Microsoft.Office.Interop.Excel.Workbook workBook = excelApp.Workbooks.Add(excelBookPath);
            Microsoft.Office.Interop.Excel.Worksheet workSheet = excelApp.Worksheets["OrigData"];
            //预览
            //excelApp.Visible = true;
            //填写项目号
            workSheet.Cells[1, 3] = jobCardList[0].ODPNo;
            //超过12台烟罩则插入行
            if (jobCardList.Count > 12)
            {
                Microsoft.Office.Interop.Excel.Range range = workSheet.get_Range("A5", "AB" + (jobCardList.Count - 8));
                range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, null);
            }
            for (int i = 0; i < jobCardList.Count; i++)
            {
                //标准烟罩
                workSheet.Cells[i + 4, 1] = i + 1;
                workSheet.Cells[i + 4, 2] = jobCardList[i].Item + "(" + jobCardList[i].Module + ")";
                workSheet.Cells[i + 4, 3] = jobCardList[i].Model;
                if (jobCardList[i].Model == "KVI" || jobCardList[i].Model == "KVF" || jobCardList[i].Model == "UVI" || jobCardList[i].Model == "UVF"
                    || jobCardList[i].Model == "KWI" || jobCardList[i].Model == "KWF" || jobCardList[i].Model == "UWI" || jobCardList[i].Model == "UWF"
                    || jobCardList[i].Model == "CMOD" || jobCardList[i].Model == "KVIM" || jobCardList[i].Model == "UVIM")
                {
                    //长度
                    if (jobCardList[i].Length != 0 && jobCardList[i].SidePanel == "BOTH")
                        workSheet.Cells[i + 4, 4] = jobCardList[i].Length + 100;
                    else if (jobCardList[i].SidePanel == "MIDDLE") workSheet.Cells[i + 4, 4] = jobCardList[i].Length;
                    else workSheet.Cells[i + 4, 4] = jobCardList[i].Length + 50;
                }
                else
                {
                    workSheet.Cells[i + 4, 4] = jobCardList[i].Length;
                }
                workSheet.Cells[i + 4, 5] = jobCardList[i].Deepth;
                workSheet.Cells[i + 4, 6] = jobCardList[i].Height;
            }
            //另存为
            string excelPath = @"D:\MyProjects\" + jobCardList[0].ODPNo;
            if (!Directory.Exists(excelPath)) Directory.CreateDirectory(excelPath);
            workBook.SaveAs(excelPath + @"\" + jobCardList[0].ODPNo + "装箱清单.xlsm", XlFileFormat.xlOpenXMLWorkbookMacroEnabled);
            KillProcess(excelApp);
            excelApp = null;//对象置空
            GC.Collect(); //垃圾回收机制
        }
        #endregion

        #region CutList
        /// <summary>
        /// 打印Ceiling的CutList
        /// </summary>
        /// <param name="drawing"></param>
        /// <param name="tree"></param>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public bool ExecPrintCeilingCutList(SubAssy subAssy, DataGridView dgv)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            string excelBookPath = Environment.CurrentDirectory + "\\CutList.xlsx";
            excelApp.Workbooks.Add(excelBookPath);
            Microsoft.Office.Interop.Excel.Worksheet workSheet = excelApp.Worksheets[1];
            workSheet.Cells[1, 2] = subAssy.ProjectName;
            workSheet.Cells[2, 2] = subAssy.ODPNo;
            workSheet.Cells[3, 2] = subAssy.SubAssyName;
            workSheet.Cells[4, 2] = "";
            workSheet.Cells[5, 2] = "";
            workSheet.Cells[7, 2] = dgv.Rows[0].Cells["AddedDate"].Value;
            workSheet.Cells[8, 2] = dgv.Rows[0].Cells["UserAccount"].Value;

            FillCutListDate(workSheet, dgv);
            //预览
            //excelApp.Visible = true;
            //excelApp.Sheets.PrintPreview(true);
            //打印
            workSheet.PrintOutEx();
            KillProcess(excelApp);
            excelApp = null;//对象置空
            GC.Collect(); //垃圾回收机制
            return true;
        }
        /// <summary>
        /// 打印Hood的CutList
        /// </summary>
        /// <param name="drawing"></param>
        /// <param name="dgv"></param>
        public bool ExecPrintHoodCutList(Drawing drawing, ModuleTree tree, DataGridView dgv)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            string excelBookPath = Environment.CurrentDirectory + "\\CutList.xlsx";
            excelApp.Workbooks.Add(excelBookPath);

            Microsoft.Office.Interop.Excel.Worksheet workSheet = excelApp.Worksheets[1];
            workSheet.Cells[1, 2] = drawing.ProjectName;
            workSheet.Cells[2, 2] = drawing.ODPNo;
            workSheet.Cells[3, 2] = drawing.Item;
            workSheet.Cells[4, 2] = tree.Module;
            workSheet.Cells[5, 2] = tree.Model;
            workSheet.Cells[7, 2] = dgv.Rows[0].Cells["AddedDate"].Value;
            workSheet.Cells[8, 2] = dgv.Rows[0].Cells["UserAccount"].Value;

            FillCutListDate(workSheet, dgv);
            //预览
            //excelApp.Visible = true;
            //excelApp.Sheets.PrintPreview(true);
            //打印
            workSheet.PrintOutEx();

            KillProcess(excelApp);
            excelApp = null;//对象置空
            GC.Collect(); //垃圾回收机制
            return true;
        }
        /// <summary>
        /// CutList填数据模块
        /// </summary>
        /// <param name="workSheet"></param>
        /// <param name="dgv"></param>
        public void FillCutListDate(Microsoft.Office.Interop.Excel.Worksheet workSheet, DataGridView dgv)
        {
            string partNo = string.Empty;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                workSheet.Cells[i + 11, 1] = dgv.Rows[i].Cells["PartDescription"].Value;
                workSheet.Cells[i + 11, 2] = dgv.Rows[i].Cells["Length"].Value;
                workSheet.Cells[i + 11, 3] = dgv.Rows[i].Cells["Width"].Value;
                workSheet.Cells[i + 11, 4] = dgv.Rows[i].Cells["Thickness"].Value;
                workSheet.Cells[i + 11, 5] = dgv.Rows[i].Cells["Quantity"].Value;
                workSheet.Cells[i + 11, 6] = dgv.Rows[i].Cells["Materials"].Value;
                workSheet.Cells[i + 11, 7] = dgv.Rows[i].Cells["PartNo"].Value;
                //Mesh油网，KSA侧板折弯后长度填入折弯栏
                if (dgv.Rows[i].Cells["PartNo"].Value.ToString().Length > 8)
                {
                    partNo = dgv.Rows[i].Cells["PartNo"].Value.ToString().Substring(0, 8).ToUpper();
                    if (partNo == "FNHE0003" || partNo == "FNHE0004" || partNo == "FNHE0026" || partNo == "FNHE0027")
                        workSheet.Cells[i + 11, 9] = Convert.ToDecimal(dgv.Rows[i].Cells["Length"].Value) == 310.67m ?
                            Convert.ToDecimal(dgv.Rows[i].Cells["Width"].Value) - 50.13m : Convert.ToDecimal(dgv.Rows[i].Cells["Length"].Value) - 50.13m;
                    else if (partNo == "FNHE0005" || partNo == "FNHE0028")
                        workSheet.Cells[i + 11, 9] = Convert.ToDecimal(dgv.Rows[i].Cells["Length"].Value) == 300m ?
                            Convert.ToDecimal(dgv.Rows[i].Cells["Width"].Value) - 29.08m : Convert.ToDecimal(dgv.Rows[i].Cells["Length"].Value) - 29.08m;
                    else if (partNo == "FNHE0012" || partNo == "FNHE0013" || partNo == "FNHE0029" || partNo == "FNHE0030" || partNo == "FNHE0038" || partNo == "FNHE0039")
                        workSheet.Cells[i + 11, 9] = Convert.ToDecimal(dgv.Rows[i].Cells["Length"].Value) == 308m ?
                            Convert.ToDecimal(dgv.Rows[i].Cells["Width"].Value) - 46.08m : Convert.ToDecimal(dgv.Rows[i].Cells["Length"].Value) - 46.08m;
                }
                //计数
                workSheet.Cells[i + 11, 11] = i + 1;
            }
            //设置边框
            Microsoft.Office.Interop.Excel.Range range = workSheet.get_Range("A11", "K" + (dgv.RowCount + 10));
            range.Borders.Value = 1;
            //设置列宽
            workSheet.Cells[1, 1].ColumnWidth = 30;
            workSheet.Cells[1, 2].ColumnWidth = 10;
            workSheet.Cells[1, 3].ColumnWidth = 10;
            workSheet.Cells[1, 4].ColumnWidth = 5;
            workSheet.Cells[1, 5].ColumnWidth = 5;
            workSheet.Cells[1, 6].ColumnWidth = 28;
            workSheet.Cells[1, 7].ColumnWidth = 30;
            workSheet.Cells[1, 8].ColumnWidth = 8;
            workSheet.Cells[1, 9].ColumnWidth = 8;
            workSheet.Cells[1, 10].ColumnWidth = 8;
            workSheet.Cells[1, 11].ColumnWidth = 5;
        }
        #endregion


        /// <summary>
        /// 引用Windows句柄，获取程序PID
        /// </summary>
        /// <param name="Hwnd"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr Hwnd, out int PID);
        /// <summary>
        /// 杀掉生成的进程
        /// </summary>
        /// <param name="AppObject">进程程对象</param>
        private static void KillProcess(Microsoft.Office.Interop.Excel.Application AppObject)
        {
            int Pid = 0;
            IntPtr Hwnd = new IntPtr(AppObject.Hwnd);
            System.Diagnostics.Process p = null;
            try
            {
                GetWindowThreadProcessId(Hwnd, out Pid);
                p = System.Diagnostics.Process.GetProcessById(Pid);
                if (p != null)
                {
                    p.Kill();
                    p.Dispose();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("进程关闭失败！异常信息：" + ex);
            }
        }
    }
}
