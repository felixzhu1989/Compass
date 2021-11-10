using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmDxfCutList : Form
    {
        private List<DXFCutList> _dxfCutList = null;//用来保存导入的cutlist对象
        private readonly ImportDataFormExcel _objImportDataFormExcel = new ImportDataFormExcel();
        private readonly DXFCutListService _objDxfCutListService = new DXFCutListService();
        private readonly CategoryService _objCategoryService = new CategoryService();
        private readonly string _sbu = Program.ObjCurrentUser.SBU;

        public FrmDxfCutList()
        {
            InitializeComponent();
            dgvDXFCutList.AutoGenerateColumns = false;
            dgvDXFCutListFromExcel.AutoGenerateColumns = false;
            btnDXFCutlist.Text = "添加行";
            btnDXFCutlist.Tag = 0;
            IniCategoryId(cobCategoryId);
        }
        /// <summary>
        /// 初始化parentId下拉框
        /// </summary>
        private void IniCategoryId(ComboBox cobItem)
        {
            //断开事件委托
            this.cobCategoryId.SelectedIndexChanged -= new System.EventHandler(this.CobCategoryId_SelectedIndexChanged);
            cobItem.DataSource = _objCategoryService.GetAllCategories(_sbu);
            cobItem.DisplayMember = "CategoryId";
            cobItem.ValueMember = "CategoryDesc";
            cobItem.SelectedIndex = -1;
            //重新关联委托
            this.cobCategoryId.SelectedIndexChanged += new System.EventHandler(this.CobCategoryId_SelectedIndexChanged);
            
        }
        
        /// <summary>
        /// 选择编号时显示描述,并更新dgv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobCategoryId_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDesc.Text = cobCategoryId.SelectedValue.ToString();
            dgvDXFCutList.DataSource = _objDxfCutListService.GetDXFCutListsByCategoryId(cobCategoryId.Text);
        }

        /// <summary>
        /// 从excel导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImportFromExcel_Click(object sender, EventArgs e)
        {
            //1.编写一个能够从Excel读取数据的通用数据访问类OleDbHelper
            //2.编写ImportDataFromExcel类，添加查询Excel数据表的方法，以对象集合的方式存储数据，能够封装数据
            //3.在dgv中展示已经读取的数据，可不展示
            //4.在SQLHelper类中编写同时“插入多条SQL语句的事务”方法，购物车一次性提交
            //5.ImportDataFromExcel类中，编写保存多个集合对象的方法
            //6.在UI中将导入的数据保存到数据库中
            if (cobCategoryId.SelectedIndex == -1)
            {
                MessageBox.Show("请选择分类编号后再导入cutlist模板", "验证信息");
                cobCategoryId.Focus();
                return;
            }
            //打开文件
            OpenFileDialog openFile = new OpenFileDialog();
            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = openFile.FileName;//获取Excel文件路径
                _dxfCutList = _objImportDataFormExcel.GetDXFCutListByExcel(path);
                foreach (var item in _dxfCutList)
                {
                    item.CategoryId = Convert.ToInt32(cobCategoryId.Text);
                }
                //显示数据
                dgvDXFCutListFromExcel.DataSource = _dxfCutList;
            }
        }
        /// <summary>
        /// 将数据保存到SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveToDB_Click(object sender, EventArgs e)
        {
            //验证数据
            if (_dxfCutList == null || _dxfCutList.Count == 0)
            {
                MessageBox.Show("目前没有要导入的数据", "提示信息");
                return;
            }

            //1.验证数据，保证List集合中有数据
            //旧方法
            //2.遍历集合，每查询一个对象，就提交一次到数据库
            //基于事务的方法--后续有事务与索引
            //2.每遍历一次就生成一条SQL语句，基于事务保存对象，一次性提交
            //UI中不能生成SQL语句，UI中只提交数据
            //应该把循环遍历的方式写在数据访问类中，写到ImportDataFromExcel中
            try
            {
                if (_objImportDataFormExcel.ImportDXFCutList(_dxfCutList))
                {
                    MessageBox.Show("数据导入成功", "提示信息");
                    dgvDXFCutListFromExcel.DataSource = null;
                    dgvDXFCutList.DataSource = _objDxfCutListService.GetDXFCutListsByCategoryId(cobCategoryId.Text);
                    _dxfCutList.Clear();
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
        /// <summary>
        /// 添加和修改条目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDXFCutlist_Click(object sender, EventArgs e)
        {

            #region 数据验证
            if (cobCategoryId.SelectedIndex == -1)
            {
                MessageBox.Show("请选择分类编号", "验证信息");
                cobCategoryId.Focus();
                return;
            }
            
            if (txtPartDescription.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入部件描述", "验证信息");
                txtPartDescription.Focus();
                return;
            }
            if (txtLength.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入长", "验证信息");
                txtLength.Focus();
                return;
            }
            if (txtWidth.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入宽", "验证信息");
                txtWidth.Focus();
                return;
            }
            if (txtThickness.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入厚度", "验证信息");
                txtThickness.Focus();
                return;
            }
            if (txtQuantity.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入数量", "验证信息");
                txtQuantity.Focus();
                return;
            }
            if (txtMaterials.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入材料", "验证信息");
                txtMaterials.Focus();
                return;
            }
            if (txtPartNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入零件号", "验证信息");
                txtPartNo.Focus();
                return;
            }
            #endregion

            if (btnDXFCutlist.Tag.Equals(0))
            {
                //提交添加
                //封装对象
                DXFCutList objDxfCutList = new DXFCutList()
                {
                    CategoryId = Convert.ToInt32(cobCategoryId.Text),
                    PartDescription = txtPartDescription.Text.Trim(),
                    Length = Convert.ToDecimal(txtLength.Text),
                    Width = Convert.ToDecimal(txtWidth.Text),
                    Thickness = Convert.ToDecimal(txtThickness.Text),
                    Quantity = Convert.ToInt32(txtQuantity.Text),
                    Materials = txtMaterials.Text.ToUpper(),
                    PartNo = txtPartNo.Text.ToUpper()
                };
                //提交添加
                try
                {
                    if (_objDxfCutListService.AddDXFCutList(objDxfCutList))
                    {
                        //提示添加成功
                        MessageBox.Show("配件信息添加成功", "提示信息");
                        //刷新显示
                        dgvDXFCutList.DataSource = _objDxfCutListService.GetDXFCutListsByCategoryId(cobCategoryId.Text);
                        //清空内容
                        foreach (Control item in Controls)
                        {
                            if (item is TextBox)
                            {
                                item.Text = "";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //封装对象
                DXFCutList objDxfCutList = new DXFCutList()
                {
                    CutListId = Convert.ToInt32(btnDXFCutlist.Tag),
                    CategoryId = Convert.ToInt32(cobCategoryId.Text),
                    PartDescription = txtPartDescription.Text.Trim(),
                    Length = Convert.ToDecimal(txtLength.Text),
                    Width = Convert.ToDecimal(txtWidth.Text),
                    Thickness = Convert.ToDecimal(txtThickness.Text),
                    Quantity = Convert.ToInt32(txtQuantity.Text),
                    Materials = txtMaterials.Text.ToUpper(),
                    PartNo = txtPartNo.Text.ToUpper()
                };
                //提交修改
                //调用后台方法修改对象
                try
                {
                    if (_objDxfCutListService.EditDXFCutList(objDxfCutList) == 1)
                    {
                        //提示添加成功
                        MessageBox.Show("配件信息修改成功", "提示信息");
                        //刷新显示
                        dgvDXFCutList.DataSource = _objDxfCutListService.GetDXFCutListsByCategoryId(cobCategoryId.Text);
                        //清空内容
                        foreach (Control item in Controls)
                        {
                            if (item is TextBox)
                            {
                                item.Text = "";
                            }
                        }
                        btnDXFCutlist.Text = "添加行";
                        btnDXFCutlist.Tag = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void TsmiEditDXFCutList_Click(object sender, EventArgs e)
        {
            if (dgvDXFCutList.RowCount == 0) return;
            if (dgvDXFCutList.CurrentRow == null) return;
            string id = dgvDXFCutList.CurrentRow.Cells["CutListId"].Value.ToString();
            DXFCutList objDxfCutList = _objDxfCutListService.GetDXFCutListById(id);
            cobCategoryId.Text = objDxfCutList.CategoryId.ToString();
            txtPartDescription.Text = objDxfCutList.PartDescription;
            txtLength.Text = objDxfCutList.Length.ToString();
            txtWidth.Text = objDxfCutList.Width.ToString();
            txtThickness.Text = objDxfCutList.Thickness.ToString();
            txtQuantity.Text = objDxfCutList.Quantity.ToString();
            txtMaterials.Text = objDxfCutList.Materials;
            txtPartNo.Text = objDxfCutList.PartNo;
            btnDXFCutlist.Text = "修改行";
            btnDXFCutlist.Tag = id;
        }

        private void TsmiDeleteDXFCutList_Click(object sender, EventArgs e)
        {
            //删除
            if (dgvDXFCutList.RowCount == 0) return;
            if (dgvDXFCutList.CurrentRow == null) return;
            string id = dgvDXFCutList.CurrentRow.Cells["CutListId"].Value.ToString();
            string partDescription = dgvDXFCutList.CurrentRow.Cells["PartDescription"].Value.ToString();
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除【 " + partDescription + " 】这个CutList的信息吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                if (_objDxfCutListService.DeleteDXFCutList(id) == 1)
                    dgvDXFCutList.DataSource = _objDxfCutListService.GetDXFCutListsByCategoryId(cobCategoryId.Text);
                else MessageBox.Show("删除CutList信息出错，项目是否被其他数据关联，请联系管理员查看后台数据。");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgvDXFCutList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TsmiEditDXFCutList_Click(null, null);
        }

        private void DgvDXFCutList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) TsmiDeleteDXFCutList_Click(null, null);
        }

        /// <summary>
        /// 添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvDXFCutList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvDXFCutList, e);
        }

        private void DgvDXFCutListFromExcel_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvDXFCutListFromExcel, e);
        }

    }
}
