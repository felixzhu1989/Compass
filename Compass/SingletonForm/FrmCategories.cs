﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DAL;
using Models;
using Common;

namespace Compass
{
    public partial class FrmCategories : Form
    {
        private readonly CategoryService _objCategoryService = new CategoryService();
        private List<Category> _categoryList = new List<Category>();
        private readonly string _sbu = Program.ObjCurrentUser.SBU;

        public FrmCategories()
        {
            InitializeComponent();
            dgvCategory.AutoGenerateColumns = false;
            IniCategoryId(cobParentId);
        }
        /// <summary>
        /// 初始化parentId下拉框
        /// </summary>
        private void IniCategoryId(ComboBox cobItem)
        {
            //断开事件委托
            cobItem.SelectedIndexChanged -= new System.EventHandler(this.CobParentId_SelectedIndexChanged);
            cobItem.DataSource = _objCategoryService.GetCategoryId(_sbu);
            cobItem.DisplayMember = "CategoryId";
            cobItem.ValueMember = "CategoryDesc";
            cobItem.SelectedIndex = -1;
            //重新关联委托
            cobItem.SelectedIndexChanged += new System.EventHandler(this.CobParentId_SelectedIndexChanged);
        }
        /// <summary>
        /// 刷新dgv显示数据，因为数据量比较小，这种就刷新可以了
        /// </summary>
        private void RefreshData(string parentId)
        {
            _categoryList = _objCategoryService.GetCategoriesByParentId(parentId,_sbu);
            dgvCategory.DataSource = _categoryList;
        }
        /// <summary>
        /// 选择编号时显示描述,并更新dgv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobParentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblParentDesc.Text = cobParentId.SelectedValue.ToString();
            RefreshData(cobParentId.Text);
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
                pbModelImage.Image = Image.FromFile(objFileDialog.FileName);
            }
        }
        /// <summary>
        /// 清除图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearImage_Click(object sender, EventArgs e)
        {
            pbModelImage.Image = Image.FromFile("NoPic.png");
        }
        /// <summary>
        /// dgv添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvCategory_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(dgvCategory, e);
        }
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            #region 数据验证

            if (txtCategoryId.Text.Trim().Length == 0)
            {
                MessageBox.Show("分类编号不能为空", "验证信息");
                txtCategoryId.Focus();
                return;
            }
            if (cobParentId.SelectedIndex == -1)
            {
                MessageBox.Show("请选择父类编号", "验证信息");
                cobParentId.Focus();
                return;
            }
            if (txtCategoryName.Text.Trim().Length == 0)
            {
                MessageBox.Show("分类名称不能为空", "验证信息");
                txtCategoryName.Focus();
                return;
            }
            //验证编号是数字
            if (!DataValidate.IsInteger(this.txtCategoryId.Text.Trim()))
            {
                MessageBox.Show("分类编号必须是数字", "验证信息");
                txtCategoryId.Focus();
                txtCategoryId.SelectAll();
                return;
            }
            #endregion

            #region 封装对象
            Category objCategory = new Category()
            {
                CategoryId = Convert.ToInt32(txtCategoryId.Text.Trim()),
                ParentId = Convert.ToInt32(cobParentId.Text),
                CategoryName = txtCategoryName.Text,
                CategoryDesc = txtCategoryDesc.Text,
                Model = txtModel.Text,
                SubType = txtSubType.Text,
                ModelImage = pbModelImage.Image != null ?
                    new SerializeObjectToString().SerializeObject(pbModelImage.Image) : null,
                KMLink = txtKMLink.Text,
                ModelPath = txtModelPath.Text
            };
            #endregion

            #region 添加到数据库

            try
            {
                string result = _objCategoryService.AddCategory(objCategory,_sbu);
                if (result == "success")
                {
                    //提示添加成功
                    MessageBox.Show("分类添加成功", "提示信息");
                    //刷新显示
                    IniCategoryId(cobParentId);//重新绑定下拉框
                    cobParentId.Text = objCategory.ParentId.ToString();//重新给下拉框赋值，防止后面刷新dgv出错
                    RefreshData(cobParentId.Text);//根据下拉框的值刷新dgv
                    //清空内容
                    pbModelImage.Image = null;
                    foreach (Control item in Controls)
                    {
                        if (item is TextBox)
                        {
                            item.Text = "";
                        }
                    }
                    txtCategoryId.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
        }
        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiEditCategory_Click(object sender, EventArgs e)
        {
            if (dgvCategory.RowCount == 0)
            {
                return;
            }
            if (dgvCategory.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的分类", "提示信息");
                return;
            }
            string categoryId = dgvCategory.CurrentRow.Cells["CategoryId"].Value.ToString();
            Category objCategory = _objCategoryService.GetCategoryByCategoryId(categoryId,_sbu);
            FrmEditCategory objFrmEditCategory = new FrmEditCategory(objCategory);
            DialogResult result = objFrmEditCategory.ShowDialog();
            if (result == DialogResult.OK)
            {
                //刷新显示
                RefreshData(cobParentId.Text);
            }
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiDeleteCategory_Click(object sender, EventArgs e)
        {
            if (dgvCategory.RowCount == 0)
            {
                return;
            }
            if (dgvCategory.CurrentRow == null)
            {
                MessageBox.Show("请选中需要删除的分类", "验证信息");
                return;
            }
            string categoryId = dgvCategory.CurrentRow.Cells["CategoryId"].Value.ToString();
            string categoryName = dgvCategory.CurrentRow.Cells["CategoryName"].Value.ToString();
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除分类（ " + categoryName + " ）吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                if (_objCategoryService.DeleteCategory(categoryId,_sbu) == 1)
                {
                    RefreshData(cobParentId.Text);//同步刷新显示数据
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 按删除键删除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)TsmiDeleteCategory_Click(null,null);
        }

        private void BtnCategoryTree_Click(object sender, EventArgs e)
        {
            FrmCategoryTree objCategoryTree=new FrmCategoryTree();
            objCategoryTree.ShowDialog();
        }
    }
}
