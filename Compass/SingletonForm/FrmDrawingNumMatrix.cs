using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Common;
using DAL;
using eDrawings.Interop.EModelMarkupControl;
using eDrawings.Interop.EModelViewControl;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorksHelper;

namespace Compass
{
    public partial class FrmDrawingNumMatrix : MetroFramework.Forms.MetroForm
    {
        private readonly DrawingNumMatrixService _objDrawingNumMatrixService = new DrawingNumMatrixService();
        private readonly ImportDataFormExcel _objImportDataFormExcel = new ImportDataFormExcel();
        private List<DrawingNumMatrix> _drawingNumFromExcel;
        private readonly List<DrawingNumCodeRule> _objCodeRules;
        private List<DrawingNumMatrix> _objDrawingNumMatrices;
        private readonly SubCode _subCode;
        private EModelViewControl _mEDrawingsCtrl;
        private EModelMarkupControl _mEDrawingsMarkupCtrl;
        private string _filePath;
        private readonly string _imageDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + @"\\Compass\\DrawingNumImage\\";//获取我的文档地址，将缓存图片存在我的文档中

        public FrmDrawingNumMatrix()
        {
            InitializeComponent();
            _objCodeRules = _objDrawingNumMatrixService.GetCodeRules();//获取编号规则
            _objDrawingNumMatrices = _objDrawingNumMatrixService.GetAllDrawingNum();//获取所有图号

            IniGrbSbu();//初始化单选框

            cobDrawingType.Items.Add("ETO");
            cobDrawingType.Items.Add("Common");
            cobDrawingType.Items.Add("Standard");
            cobDrawingType.Items.Add("Purchasing");
            cobDrawingType.SelectedIndex = -1;

            _subCode = new SubCode();
            dgvDrawingNumMatrix.AutoGenerateColumns = false;
            dgvDrawingNumMatrix.DataSource = _objDrawingNumMatrices;//初始化图号表格
            SetPermissions();
            dgvDrawingNumMatrix.SelectionChanged += new EventHandler(DgvDrawingNumMatrix_SelectionChanged);

            if (!Directory.Exists(_imageDir)) Directory.CreateDirectory(_imageDir);
        }
        #region 单例模式，重写关闭方法
        protected override void OnClosing(CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
        #endregion
        internal void ShowAndFocus()
        {
            Show();
            WindowState = FormWindowState.Normal;
            Focus();
        }

        private void DgvProjects_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(dgvDrawingNumMatrix, e);
        }

        #region 设置权限
        private void SetPermissions()
        {
            btnImportFromExcel.Visible = false;
            btnImport.Visible = false;

            if (Program.ObjCurrentUser.UserGroupId == 1)
            {
                //管理员才能删除图号
                
                tsmiDeleteDrawingNum.Visible = true;
                tsmiEditDrawingNum.Visible = true;
                btnCommit.Enabled = true;
            }
            else if (Program.ObjCurrentUser.UserGroupId == 2)
            {
                //技术部能添加图号，更改图号
                tsmiDeleteDrawingNum.Visible = false;
                tsmiEditDrawingNum.Visible = true;
                btnCommit.Enabled = true;
            }
            else
            {
                tsmiDeleteDrawingNum.Visible = false;
                tsmiEditDrawingNum.Visible = false;
                btnCommit.Enabled = false;
            }
        }
        #endregion


        #region 生成单选框
        private void IniGrbSbu()
        {
            List<DrawingNumCodeRule> sbuCodeRules =
                _objCodeRules.Where(rule => rule.ParentId == 0 && rule.CodeId != 0).ToList();
            Panel pnlSbu = new Panel()
            {
                Name = "pnlSBU",
                Location = new Point(10, 60)
            };
            Controls.Add(pnlSbu);
            AddRadioButtonAndGroupBox(pnlSbu, sbuCodeRules, "SBU");
            IniProductType(pnlSbu);
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
            Controls.Add(pnlProductType);
            AddRadioButtonAndGroupBox(pnlProductType, productTypeCodeRules, "Product Type");
        }

        private void IniProductName(Panel lastPanel, string codeId)
        {
            List<DrawingNumCodeRule> productNameCodeRules =
                _objCodeRules.Where(rule => rule.ParentId == Convert.ToInt32(codeId) && rule.CodeId != 0).ToList();
            Panel pnlProductName = new Panel()
            {
                Name = "pnlProductName",
                Location = new Point(lastPanel.Location.X + lastPanel.Width + 8, lastPanel.Location.Y)
            };
            Controls.Add(pnlProductName);
            AddRadioButtonAndGroupBox(pnlProductName, productNameCodeRules, "Product Name");
        }

        private void IniSubAssembly(Panel lastPanel, string codeId)
        {
            List<DrawingNumCodeRule> subAssemblyCodeRules =
                _objCodeRules.Where(rule => rule.ParentId == Convert.ToInt32(codeId) && rule.CodeId != 0).ToList();
            Panel pnlSubAssembly = new Panel()
            {
                Name = "pnlSubAssembly",
                Location = new Point(lastPanel.Location.X + lastPanel.Width + 8, lastPanel.Location.Y)
            };
            Controls.Add(pnlSubAssembly);
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
                        Controls.Remove(item);
                    }
                }
                if (!rbt.Checked) return;
                Panel pnlProdType = (Panel)Controls.Find("pnlProductType", false).First();
                IniProductName(pnlProdType, rbt.Name);
            }
            else if (rbt.Name.Substring(3, 2) == "00")
            {
                foreach (Control item in Controls)
                {
                    if (item is Panel && item.Name.Contains("SubAssembly"))
                    {
                        Controls.Remove(item);
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
            if (_subCode.SuffixCode.Length != 4)
            {
                MessageBox.Show("请保证每一项都选中！");
                return;
            }
            if (currentList == null || currentList.Count == 0)
            {
                _subCode.FinalCode = "0001";
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
                    _subCode.FinalCode = j.ToString("0000");
                    break;
                }
            }
            string drawingNum = _subCode.SuffixCode + _subCode.FinalCode;
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
                            _subCode.SbuCode = GetCode(panel);
                            break;
                        case "pnlProductType":
                            _subCode.ProdTypeCode = GetCode(panel);
                            break;
                        case "pnlProductName":
                            _subCode.ProdNameCode = GetCode(panel);
                            break;
                        case "pnlSubAssembly":
                            _subCode.SubAssyCode = GetCode(panel);
                            break;
                        default:
                            break;
                    }
                }
            }
            _subCode.SuffixCode = _subCode.SbuCode + _subCode.ProdTypeCode + _subCode.ProdNameCode + _subCode.SubAssyCode;
            List<DrawingNumMatrix> currentList =
                _objDrawingNumMatrices.Where(d => d.DrawingNum.StartsWith(_subCode.SuffixCode)).ToList();
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

        #region 添加/更新/删除图号

        private void BtnCommit_Click(object sender, EventArgs e)
        {
            #region 验证数据
            if (txtDrawingNum.Text.Trim().Length == 0)
            {
                MessageBox.Show("图号不能为空", "验证信息");
                txtDrawingNum.Focus();
                return;
            }

            if (cobDrawingType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择分类", "验证信息");
                cobDrawingType.Focus();
                return;
            }
            if (txtDrawingDesc.Text.Trim().Length == 0)
            {
                MessageBox.Show("描述不能为空", "验证信息");
                txtDrawingDesc.Focus();
                return;
            }
            #endregion
            //封装对象
            DrawingNumMatrix objDrawingNum = new DrawingNumMatrix()
            {
                DrawingNum = txtDrawingNum.Text,
                DrawingDesc = txtDrawingDesc.Text,
                DrawingType = cobDrawingType.Text,
                Mark = txtMark.Text,
                UserId = Program.ObjCurrentUser.UserId
            };



            if (btnCommit.Tag.ToString() == "0")//添加图号
            {
                try
                {
                    string result = _objDrawingNumMatrixService.AddDrawingNum(objDrawingNum);
                    if (result == "success")
                    {
                        MessageBox.Show("图号添加成功！", "提示信息");
                        RefreshData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else//修改图号
            {
                try
                {
                    objDrawingNum.DrawingId = Convert.ToInt32(btnCommit.Tag);
                    if (_objDrawingNumMatrixService.EditDrawingNum(objDrawingNum) == 1)
                    {
                        MessageBox.Show("图号修改成功", "提示信息");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    btnCommit.Tag = "0";
                    btnCommit.Text = "添加图号";
                    RefreshData();
                }
            }
            void RefreshData()
            {
                //刷新列表和dgv显示（组内）
                _objDrawingNumMatrices = _objDrawingNumMatrixService.GetAllDrawingNum();
                dgvDrawingNumMatrix.DataSource = null;
                GetAllSubCode();
                //清除txt
                txtDrawingNum.Text = "";
                txtDrawingDesc.Text = "";
                txtMark.Text = "";
                cobDrawingType.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// 修改图号菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiEditDrawingNum_Click(object sender, EventArgs e)
        {
            if (dgvDrawingNumMatrix.RowCount == 0)
            {
                return;
            }
            if (dgvDrawingNumMatrix.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的图号", "验证信息");
                return;
            }
            string drawingId = dgvDrawingNumMatrix.CurrentRow.Cells["DrawingId"].Value.ToString();
            DrawingNumMatrix objDrawingNum = _objDrawingNumMatrixService.GetDrawingNumById(drawingId);
            //解析对象，显示在界面上
            txtDrawingNum.Text = objDrawingNum.DrawingNum;
            txtDrawingDesc.Text = objDrawingNum.DrawingDesc;
            txtMark.Text = objDrawingNum.Mark;
            cobDrawingType.Text = objDrawingNum.DrawingType;
            //将按钮的tag更改成id，显示为更新图号
            btnCommit.Tag = objDrawingNum.DrawingId;
            btnCommit.Text = "更新图号";

        }

        /// <summary>
        /// 删除图号菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiDeleteDrawingNum_Click(object sender, EventArgs e)
        {
            if (dgvDrawingNumMatrix.RowCount == 0)
            {
                return;
            }
            if (dgvDrawingNumMatrix.CurrentRow == null)
            {
                MessageBox.Show("请选中需要删除的图号", "验证信息");
                return;
            }
            string drawingId = dgvDrawingNumMatrix.CurrentRow.Cells["DrawingId"].Value.ToString();
            string drawingNum = dgvDrawingNumMatrix.CurrentRow.Cells["DrawingNum"].Value.ToString();
            if (DataValidate.IsInteger(drawingNum.Substring(0, 1)))
            {
                MessageBox.Show("不允许删除标准件和采购件，删除请联系管理员", "删除询问");
                return;
            }
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除图号（ " + drawingNum + " ）吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                if (_objDrawingNumMatrixService.DeleteDrawingNum(drawingId) == 1)
                {
                    //刷新列表和dgv显示（组内）
                    _objDrawingNumMatrices = _objDrawingNumMatrixService.GetAllDrawingNum();
                    dgvDrawingNumMatrix.DataSource = null;
                    GetAllSubCode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 选择图号时更新显示图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvDrawingNumMatrix_SelectionChanged(object sender, EventArgs e)
        {
            DrawingNumMatrix currentDrawingNum = GetCurrentDrawingNum();
            if (currentDrawingNum == null) return;
            //改成图片缓存本地
            string imagePath = _imageDir + currentDrawingNum.DrawingNum + ".png";
            if (File.Exists(imagePath))
            {
                pbImage.Image = Image.FromFile(imagePath);
            }
            else
            {
                string drawingImage = _objDrawingNumMatrixService.GetDrawingImage(currentDrawingNum.DrawingId.ToString());
                if (drawingImage.Length != 0)
                {
                    ((Image)new SerializeObjectToString().DeserializeObject(drawingImage)).Save(imagePath);
                    pbImage.Image = Image.FromFile(imagePath);
                }
                else
                {
                    pbImage.Image = Image.FromFile("NoPic.png");
                }
            }
        }
        /// <summary>
        /// 从表单获取当前的图号
        /// </summary>
        /// <returns></returns>
        private DrawingNumMatrix GetCurrentDrawingNum()
        {
            if (dgvDrawingNumMatrix.RowCount == 0)
            {
                return null;
            }
            if (dgvDrawingNumMatrix.CurrentRow == null)
            {
                return null;
            }
            int drawingId = Convert.ToInt32(dgvDrawingNumMatrix.CurrentRow.Cells["DrawingId"].Value.ToString());
            List<DrawingNumMatrix> drawingNum = _objDrawingNumMatrices.Where(d => d.DrawingId == drawingId).ToList();
            return drawingNum[0];
        }
        /// <summary>
        /// 选择图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog objFileDialog = new OpenFileDialog();
            DialogResult result = objFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                pbImage.Image = Image.FromFile(objFileDialog.FileName);
            }
        }
        /// <summary>
        /// 清除图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearImage_Click(object sender, EventArgs e)
        {
            pbImage.Image = Image.FromFile("NoPic.png");
        }
        /// <summary>
        /// 更新图片，同时删除本地缓存的图片（待完善）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshImage_Click(object sender, EventArgs e)
        {


            if (dgvDrawingNumMatrix.RowCount == 0)
            {
                return;
            }
            if (dgvDrawingNumMatrix.CurrentRow == null)
            {
                MessageBox.Show("请选中需要更新图片的图号", "验证信息");
                return;
            }
            string drawingId = dgvDrawingNumMatrix.CurrentRow.Cells["DrawingId"].Value.ToString();
            string image = pbImage.Image != null ? new SerializeObjectToString().SerializeObject(pbImage.Image) : null;
            try
            {
                if (_objDrawingNumMatrixService.RefreshImage(image, drawingId) == 1)
                {
                    _objDrawingNumMatrices = _objDrawingNumMatrixService.GetAllDrawingNum();
                    dgvDrawingNumMatrix.DataSource = _objDrawingNumMatrices;
                    //改成图片缓存本地
                    string saveImagePath = _imageDir + dgvDrawingNumMatrix.CurrentRow.Cells["DrawingNum"].Value.ToString() + ".png";
                    if (File.Exists(saveImagePath)) File.Delete(saveImagePath);//先删除本地，然后在将图片保存下来
                    pbImage.Image.Save(saveImagePath);
                    MessageBox.Show("图片更新成功成功", "提示信息");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            dgvDrawingNumMatrix.SelectionChanged -= new EventHandler(DgvDrawingNumMatrix_SelectionChanged);
            dgvDrawingNumMatrix.DataSource = null;
            //打开文件
            OpenFileDialog openFile = new OpenFileDialog();
            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = openFile.FileName;//获取Excel文件路径
                _drawingNumFromExcel = _objImportDataFormExcel.GetDrawingNumByExcel(path);
                //显示数据
                dgvDrawingNumMatrix.DataSource = _drawingNumFromExcel;
            }
        }
        private void BtnImport_Click(object sender, EventArgs e)
        {
            dgvDrawingNumMatrix.DataSource = null;
            if (_drawingNumFromExcel == null || _drawingNumFromExcel.Count == 0)
            {
                MessageBox.Show("目前没有要导入的数据", "提示信息");
                dgvDrawingNumMatrix.DataSource = _objDrawingNumMatrices;
                return;
            }
            try
            {
                if (_objImportDataFormExcel.ImportDrawingNum(_drawingNumFromExcel))
                {
                    MessageBox.Show("数据导入成功", "提示信息");
                    dgvDrawingNumMatrix.DataSource = null;
                    _objDrawingNumMatrices = _objDrawingNumMatrixService.GetAllDrawingNum();
                    dgvDrawingNumMatrix.DataSource = _objDrawingNumMatrices;
                    _drawingNumFromExcel.Clear();
                }
                else
                {
                    MessageBox.Show("数据导入失败", "提示信息");
                    dgvDrawingNumMatrix.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据导入失败" + ex.Message, "错误提示");
                dgvDrawingNumMatrix.DataSource = null;
            }
            dgvDrawingNumMatrix.SelectionChanged += new EventHandler(DgvDrawingNumMatrix_SelectionChanged);
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

            List<DrawingNumMatrix> objDrawingNumQuery = _objDrawingNumMatrices.Where(
                d => d.DrawingNum.Contains(txtDrawingNum.Text.Trim().ToUpper())
                && d.DrawingDesc.Contains(txtDrawingDesc.Text.Trim())
                && d.DrawingType.Contains(cobDrawingType.Text.Trim())).ToList();
            dgvDrawingNumMatrix.DataSource = objDrawingNumQuery;
        }

        #region 显示EDrawing模型
        /// <summary>
        /// 显示模型菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiShowModel_Click(object sender, EventArgs e)
        {
            BtnOpen_Click(null, null);
        }
        /// <summary>
        /// 显示模型按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab(1);
            GetFilePath(".SLDPRT");
            OnOpen(sender, e);
        }
        private void BtnOpenDrawing_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab(1);
            GetFilePath(".SLDDRW");
            OnOpen(sender, e);
        }
        private void GetFilePath(string extension)
        {
            DrawingNumMatrix currentDrawingNum = GetCurrentDrawingNum();
            if (currentDrawingNum == null) return;
            string drawingNum = currentDrawingNum.DrawingNum;
            string fsPath = @"D:\halton\01 Tech Dept\02 Self Making Parts\";
            string purchPath = @"D:\halton\01 Tech Dept\03 Purchasing Components\";

            switch (drawingNum.Substring(0, 1))
            {
                case "2":
                    _filePath = purchPath + drawingNum + extension;
                    break;
                case "5":
                    _filePath = fsPath + @"01 Standard\" + drawingNum + extension;
                    break;
                case "F":
                    if (currentDrawingNum.DrawingType == "ETO")
                    {
                        _filePath = fsPath + @"03 ETO\" + drawingNum + extension;
                    }
                    else
                    {
                        _filePath = fsPath + @"02 Common\" + drawingNum + extension;
                    }
                    break;
                case "M":
                    //Marine项目将来完善
                    if (currentDrawingNum.DrawingType == "ETO")
                    {

                        _filePath = @"D:\Marine\01 Parts\03 ETO\" + drawingNum + extension;
                    }
                    else
                    {
                        _filePath = @"D:\Marine\01 Parts\02 Common\" + drawingNum + extension;
                    }
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 直接打开模型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            GetFilePath(".SLDPRT");
            if (File.Exists(_filePath)) Process.Start(_filePath);
            else MessageBox.Show("未找到该路径!");
        }

        /// <summary>
        /// 打开文件夹按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenFolder_Click(object sender, EventArgs e)
        {
            GetFilePath(".SLDPRT");
            if (File.Exists(_filePath))
            {
                Process.Start("Explorer.exe", "/select," + Path.GetDirectoryName(_filePath) + "\\" + Path.GetFileName(_filePath));
            }
            else
            {
                if (Directory.Exists(Path.GetDirectoryName(_filePath)))
                {
                    Process.Start("Explorer.exe", Path.GetDirectoryName(_filePath));
                }
                else
                {
                    MessageBox.Show("未找到该路径!");
                }
            }
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ctrlEDrw.LoadEDrawings();
        }
        private void OnControlLoaded(EModelViewControl ctrl)
        {
            _mEDrawingsCtrl = ctrl;
            _mEDrawingsCtrl.OnFinishedLoadingDocument += OnFinishedLoadingDocument;
            _mEDrawingsCtrl.OnFailedLoadingDocument += OnFailedLoadingDocument;
            _mEDrawingsMarkupCtrl = _mEDrawingsCtrl.CoCreateInstance("EModelViewMarkup.EModelMarkupControl") as EModelMarkupControl;
        }
        private void OnFailedLoadingDocument(string fileName, int errorCode, string errorString)
        {
            Trace.WriteLine($"{fileName} failed to loaded: {errorString}");
        }
        private void OnFinishedLoadingDocument(string fileName)
        {
            Trace.WriteLine($"{fileName} loaded");
            _mEDrawingsMarkupCtrl.ViewOperator = EMVMarkupOperators.eMVOperatorMeasure;
        }
        private void OnOpen(object sender, EventArgs e)
        {
            if (Directory.Exists(Path.GetDirectoryName(_filePath)))
            {
                btnOpenFolder.Enabled = true;
                btnAddCustomInfo.Enabled = true;
            }
            if (File.Exists(_filePath))
            {
                if (_mEDrawingsCtrl == null)
                {
                    throw new NullReferenceException("eDrawings control is not loaded");
                }
                //txtMeasurements.Clear();
                txtMeasurements.Text = "选择线可直接显示结果，如果选择的是面则点击记录测量结果按钮。";
                _mEDrawingsCtrl.CloseActiveDoc("");
                _mEDrawingsCtrl.OpenDoc(_filePath, false, false, false, "");
            }
            else
            {
                MessageBox.Show("未找到该文档!\r\n\r\n请尝试打开文件夹...");
            }
        }
        private void OnCaptureMeasurement(object sender, EventArgs e) => txtMeasurements.Text += (!string.IsNullOrEmpty(txtMeasurements.Text) ? System.Environment.NewLine : "")
+ _mEDrawingsMarkupCtrl.MeasureResultString;

        #endregion

        private void TxtDrawingNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) BtnQuery_Click(null, null);
        }

        private void TxtDrawingDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) BtnQuery_Click(null, null);
        }

        private void TsmiBathImportImage_Click(object sender, EventArgs e)
        {
            //获取路径
            string imagePath = string.Empty;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                imagePath = fbd.SelectedPath;
            }
            //获取文件列表
            string[] imageFiles = Directory.GetFiles(imagePath);
            Dictionary<string, string> imagesDic = new Dictionary<string, string>();
            Dictionary<string, Image> imageList = new Dictionary<string, Image>();
            foreach (string imageFile in imageFiles)
            {
                //序列化
                string image = Image.FromFile(imageFile) != null ? new SerializeObjectToString().SerializeObject(Image.FromFile(imageFile)) : null;
                //添加键值对
                imagesDic.Add(Path.GetFileNameWithoutExtension(imageFile), image);
                imageList.Add(Path.GetFileNameWithoutExtension(imageFile), Image.FromFile(imageFile));
            }
            //提交数据库
            if (_objDrawingNumMatrixService.BathImportDrawingImage(imagesDic))
            {
                dgvDrawingNumMatrix.DataSource = null;
                _objDrawingNumMatrices = _objDrawingNumMatrixService.GetAllDrawingNum();
                dgvDrawingNumMatrix.DataSource = _objDrawingNumMatrices;
                imagesDic.Clear();

                //改成图片缓存本地

                foreach (var item in imageList)
                {
                    string saveImagePath = _imageDir + item.Key + ".png";
                    if (File.Exists(saveImagePath)) File.Delete(saveImagePath);//先删除本地，然后在将图片保存下来
                    item.Value.Save(saveImagePath);
                }
                imageList.Clear();
                MessageBox.Show("批量导入图片成功！");
            }

        }
        /// <summary>
        /// 将信息写入SolidWorks属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnAddCustomInfo_Click(object sender, EventArgs e)
        {
            if (dgvDrawingNumMatrix.RowCount == 0)
            {
                return;
            }
            if (dgvDrawingNumMatrix.CurrentRow == null)
            {
                MessageBox.Show("请选中需要更新图片的图号", "验证信息");
                return;
            }
            string userAccount = dgvDrawingNumMatrix.CurrentRow.Cells["UserAccount"].Value.ToString();
            string drawingType = dgvDrawingNumMatrix.CurrentRow.Cells["DrawingType"].Value.ToString();
            string drawingDesc = dgvDrawingNumMatrix.CurrentRow.Cells["DrawingDesc"].Value.ToString();

            SldWorks swApp = await SolidWorksSingleton.GetApplicationAsync();
            if (swApp == null) return;
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;

            //SBU，Product Class，
            //DrawnBy，Parts Class，Part Name，Description

            swModel.DeleteCustomInfo2("", "DrawnBy");
            swModel.AddCustomInfo3("", "DrawnBy", (int)swCustomInfoType_e.swCustomInfoText, userAccount);

            swModel.DeleteCustomInfo2("", "Parts Class");
            swModel.AddCustomInfo3("", "Parts Class", (int)swCustomInfoType_e.swCustomInfoText, drawingType);

            swModel.DeleteCustomInfo2("", "Part Name");
            swModel.AddCustomInfo3("", "Part Name", (int)swCustomInfoType_e.swCustomInfoText, drawingDesc);

            swModel.DeleteCustomInfo2("", "Description");
            swModel.AddCustomInfo3("", "Description", (int)swCustomInfoType_e.swCustomInfoText, drawingDesc);
            MessageBox.Show("属性写入完成");

            swApp.CommandInProgress = false;
        }


    }
}
