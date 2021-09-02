using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmDrawingNumMatrix : MetroFramework.Forms.MetroForm
    {
        DrawingNumMatrixService objDrawingNumMatrixService = new DrawingNumMatrixService();
        ImportDataFormExcel objImportDataFormExcel = new ImportDataFormExcel();
        List<DrawingNumMatrix> drawingNumFromExcel;
        private List<DrawingNumCodeRule> objCodeRules;
        private List<DrawingNumMatrix> objDrawingNumMatrices;
        private SubCode subCode;
        public FrmDrawingNumMatrix()
        {
            InitializeComponent();
            objCodeRules = objDrawingNumMatrixService.GetCodeRules();//获取编号规则
            objDrawingNumMatrices = objDrawingNumMatrixService.GetAllDrawingNum();//获取所有图号

            IniGrbSbu();//初始化单选框

            cobDrawingType.Items.Add("ETO");
            cobDrawingType.Items.Add("Common");
            cobDrawingType.Items.Add("Standard");
            cobDrawingType.Items.Add("Purchasing");
            cobDrawingType.SelectedIndex = -1;

            subCode = new SubCode();
            dgvDrawingNumMatrix.DataSource = objDrawingNumMatrices;//初始化图号表格
            SetPermissions();
        }

        private void DgvProjects_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvDrawingNumMatrix, e);
        }
        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            //管理员才能导入清单
            if (Program.ObjCurrentUser.UserGroupId == 1)
            {
                btnImportFromExcel.Enabled = true;
            }
            else
            {
                btnImportFromExcel.Enabled = false;
            }
        }


        #region 生成单选框
        private void IniGrbSbu()
        {
            List<DrawingNumCodeRule> sbuCodeRules =
                objCodeRules.Where(rule => rule.ParentId == 0 && rule.CodeId != 0).ToList();
            Panel pnlSBU = new Panel()
            {
                Name = "pnlSBU",
                Location = new Point(10, 60)
            };
            this.Controls.Add(pnlSBU);
            AddRadioButtonAndGroupBox(pnlSBU, sbuCodeRules, "SBU");
            IniProductType(pnlSBU);
        }

        private void IniProductType(Panel lastPanel)
        {
            List<DrawingNumCodeRule> productTypeCodeRules = new List<DrawingNumCodeRule>
            {
                new DrawingNumCodeRule(){Code = 'N',CodeName = "Normal Product"},
                new DrawingNumCodeRule(){Code = 'S',CodeName = "Special Product"}
            };
            Panel pnlProductType = new Panel()
            {
                Name = "pnlProductType",
                Location = new Point(lastPanel.Location.X + lastPanel.Width + 8, lastPanel.Location.Y)
            };
            this.Controls.Add(pnlProductType);
            AddRadioButtonAndGroupBox(pnlProductType, productTypeCodeRules, "Product Type");
        }

        private void IniProductName(Panel lastPanel, string codeId)
        {
            List<DrawingNumCodeRule> productNameCodeRules =
                objCodeRules.Where(rule => rule.ParentId == Convert.ToInt32(codeId) && rule.CodeId != 0).ToList();
            Panel pnlProductName = new Panel()
            {
                Name = "pnlProductName",
                Location = new Point(lastPanel.Location.X + lastPanel.Width + 8, lastPanel.Location.Y)
            };
            this.Controls.Add(pnlProductName);
            AddRadioButtonAndGroupBox(pnlProductName, productNameCodeRules, "Product Name");
        }

        private void IniSubAssembly(Panel lastPanel, string codeId)
        {
            List<DrawingNumCodeRule> subAssemblyCodeRules =
                objCodeRules.Where(rule => rule.ParentId == Convert.ToInt32(codeId) && rule.CodeId != 0).ToList();
            Panel pnlSubAssembly = new Panel()
            {
                Name = "pnlSubAssembly",
                Location = new Point(lastPanel.Location.X + lastPanel.Width + 8, lastPanel.Location.Y)
            };
            this.Controls.Add(pnlSubAssembly);
            AddRadioButtonAndGroupBox(pnlSubAssembly, subAssemblyCodeRules, "Sub Assembly");
        }

        /// <summary>
        /// 添加单选项和组合框
        /// </summary>
        /// <param name="pnl"></param>
        /// <param name="rules"></param>
        /// <param name="groupText"></param>
        private void AddRadioButtonAndGroupBox(Panel pnl, List<DrawingNumCodeRule> rules, string groupText)
        {
            RadioButton rbt = new RadioButton(); //选项
            int width = 130;
            int width2 = 10;
            for (int i = 0; i < rules.Count; i++)
            {
                string showText = rules[i].Code + " : " + rules[i].CodeName;
                Point insertPoint = default(Point);
                if (i < 7)
                {
                    insertPoint = new Point(8, 20 * (i + 1));
                }
                else if (i > 6 && i < 14)
                {
                    insertPoint = new Point(8 + width, 20 * (i - 7 + 1));
                }
                rbt = new RadioButton
                {
                    Name = rules[i].CodeId.ToString(),
                    Text = showText,
                    Tag = rules[i].Code,
                    Width = showText.Length * 7 + 40,
                    Location = insertPoint
                };
                if (i < 7 && rbt.Width > width) width = rbt.Width;
                if (i > 6 && i < 14 && rbt.Width > width2) width2 = rbt.Width;
                pnl.Controls.Add(rbt); //将创建好的Radiobutton放入控件集合中
                rbt.BringToFront();
                rbt.CheckedChanged += new EventHandler(R_CheckedChanged); //注册每个Radiobutton的事件 
            }

            if (width2 > 10) pnl.Width = width + width2 + 10;
            else pnl.Width = width + 10;
            pnl.Height = 7 * 20 + 30;
            GroupBox gb = new GroupBox() //组合框
            {
                Text = groupText,
                Dock = DockStyle.Fill
            };
            pnl.Controls.Add(gb);
        }
        /// <summary>
        /// 添加的单选项绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void R_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbt = (RadioButton)sender;

            if (rbt.Name == "0")
            {
                return;
            }

            Panel pnl = (Panel)rbt.Parent;
            if (rbt.Name.Substring(1, 4) == "0000")
            {

                foreach (Control item in Controls)
                {
                    if (item is Panel && (item.Name.Contains("ProductName") || item.Name.Contains("SubAssembly")))
                    {
                        this.Controls.Remove(item);
                    }
                }
                if (!rbt.Checked) return;
                Panel pnlProdType = (Panel)this.Controls.Find("pnlProductType", false).First();
                IniProductName(pnlProdType, rbt.Name);
            }
            else if (rbt.Name.Substring(3, 2) == "00")
            {
                foreach (Control item in Controls)
                {
                    if (item is Panel && item.Name.Contains("SubAssembly"))
                    {
                        this.Controls.Remove(item);
                    }
                }
                if (!rbt.Checked) return;
                IniSubAssembly(pnl, rbt.Name);
            }
            else
            {
                //当选择最后一个复选框，则更新列表
                dgvDrawingNumMatrix.DataSource = null;
                if (!rbt.Checked) return;
                GetAllSubCode();//获取图号前缀并更新列表
            }
        }
        #endregion

        #region 生成图号
        /// <summary>
        /// 生成图号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreateDrawingNum_Click(object sender, EventArgs e)
        {
            txtDrawingNum.Text = "";//清空
            List<DrawingNumMatrix> currentList = GetAllSubCode();
            if (subCode.SuffixCode.Length != 4)
            {
                MessageBox.Show("请保证每一项都选中！");
                return;
            }
            if (currentList == null || currentList.Count == 0)
            {
                subCode.FinalCode = "0001";
            }
            else
            {
                int[] intCode = new int[currentList.Count];
                for (int i = 0; i < currentList.Count; i++)
                {
                    intCode[i] = Convert.ToInt32(currentList[i].DrawingNum.Substring(4));
                }
                for (int j = 1; j < intCode.Max() + 2; j++)
                {
                    if (intCode.Contains(j)) continue;
                    subCode.FinalCode = j.ToString("0000");
                    break;
                }
            }
            string drawingNum = subCode.SuffixCode + subCode.FinalCode;
            txtDrawingNum.Text = drawingNum.ToUpper();
        }
        /// <summary>
        /// 从所有单选框中获取图号代号
        /// </summary>
        private List<DrawingNumMatrix> GetAllSubCode()
        {
            foreach (Control item in Controls)
            {
                if (item is Panel panel)
                {
                    Debug.Print(panel.Text + panel.Name);
                    switch (panel.Name)
                    {
                        case "pnlSBU":
                            subCode.SbuCode = GetCode(panel);
                            break;
                        case "pnlProductType":
                            subCode.ProdTypeCode = GetCode(panel);
                            break;
                        case "pnlProductName":
                            subCode.ProdNameCode = GetCode(panel);
                            break;
                        case "pnlSubAssembly":
                            subCode.SubAssyCode = GetCode(panel);
                            break;
                        default:
                            break;
                    }
                }
            }
            subCode.SuffixCode = subCode.SbuCode + subCode.ProdTypeCode + subCode.ProdNameCode + subCode.SubAssyCode;
            List<DrawingNumMatrix> currentList =
                objDrawingNumMatrices.Where(d => d.DrawingNum.StartsWith(subCode.SuffixCode)).ToList();
            dgvDrawingNumMatrix.DataSource = currentList;
            return currentList;
            //内部方法
            string GetCode(Panel pnl)
            {
                string code = "";
                foreach (Control item in pnl.Controls)
                {
                    if (item is RadioButton rbt)
                    {
                        if (!rbt.Checked) continue;
                        code = rbt.Tag.ToString();
                    }
                }
                return code;
            }
        }
        #endregion

        #region 从Excel导入图号
        /// <summary>
        /// 选择Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImportFromExcel_Click(object sender, EventArgs e)
        {
            dgvDrawingNumMatrix.DataSource = null;
            //打开文件
            OpenFileDialog openFile = new OpenFileDialog();
            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = openFile.FileName;//获取Excel文件路径
                drawingNumFromExcel = objImportDataFormExcel.GetDrawingNumByExcel(path);
                //显示数据
                dgvDrawingNumMatrix.DataSource = drawingNumFromExcel;
            }
        }
        private void BtnImport_Click(object sender, EventArgs e)
        {
            dgvDrawingNumMatrix.DataSource = null;
            if (drawingNumFromExcel == null || drawingNumFromExcel.Count == 0)
            {
                MessageBox.Show("目前没有要导入的数据", "提示信息");
                dgvDrawingNumMatrix.DataSource = objDrawingNumMatrices;
                return;
            }
            try
            {
                if (objImportDataFormExcel.ImportDrawingNum(drawingNumFromExcel))
                {
                    MessageBox.Show("数据导入成功", "提示信息");
                    objDrawingNumMatrices = objDrawingNumMatrixService.GetAllDrawingNum();
                    dgvDrawingNumMatrix.DataSource = objDrawingNumMatrices;
                    drawingNumFromExcel.Clear();
                }
                else
                {
                    MessageBox.Show("数据导入失败", "提示信息");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据导入失败" + ex.Message, "错误提示");
            }


        }

        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            dgvDrawingNumMatrix.DataSource = null;

            List<DrawingNumMatrix> objDrawingNumQuery = objDrawingNumMatrices.Where(
                d => d.DrawingNum.Contains(txtDrawingNum.Text.Trim().ToUpper()) 
                && d.DrawingDesc.Contains(txtDrawingDesc.Text.Trim()) 
                && d.DrawingType.Contains(cobDrawingType.Text.Trim())).ToList();
            dgvDrawingNumMatrix.DataSource = objDrawingNumQuery;
        }
    }
}
