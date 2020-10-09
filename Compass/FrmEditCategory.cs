using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using Models;
using Common;

namespace Compass
{
    public partial class FrmEditCategory : MetroFramework.Forms.MetroForm
    {
        private CategoryService objCategoryService = new CategoryService();
        public FrmEditCategory()
        {
            InitializeComponent();
            //初始化下拉框
            cobParentId.DataSource = objCategoryService.GetCategoryId();
            cobParentId.DisplayMember = "CategoryId";
            cobParentId.ValueMember = "CategoryDesc";
            cobParentId.SelectedIndex = -1;
            this.cobParentId.SelectedIndexChanged += new System.EventHandler(this.cobParentId_SelectedIndexChanged);
        }
        /// <summary>
        /// 重载构造方法，传递分类模型对象参数
        /// </summary>
        /// <param name="objCategory"></param>
        public FrmEditCategory(Category objCategory) : this()
        {
            //解析对象，显示分类信息
            txtCategoryId.Text = objCategory.CategoryId.ToString();
            cobParentId.Text = objCategory.ParentId.ToString();
            txtCategoryName.Text = objCategory.CategoryName;
            txtCategoryDesc.Text = objCategory.CategoryDesc;
            txtModel.Text = objCategory.Model;
            txtSubType.Text = objCategory.SubType;
            txtKMLink.Text = objCategory.KMLink;
            txtModelPath.Text = objCategory.ModelPath;
            pbModelImage.Image = objCategory.ModelImage.Length == 0
                ? Image.FromFile("NoPic.png")
                : (Image)new SerializeObjectToString().DeserializeObject(objCategory.ModelImage);
        }
        /// <summary>
        /// 选择图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseImage_Click(object sender, EventArgs e)
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
        private void btnClearImage_Click(object sender, EventArgs e)
        {
            pbModelImage.Image = Image.FromFile("NoPic.png");
        }
        /// <summary>
        /// 提交修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditCategory_Click(object sender, EventArgs e)
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
            //已经是只读的，无需验证了
            ////验证编号是数字
            //if (!DataValidate.IsInteger(this.txtCategoryId.Text.Trim()))
            //{
            //    MessageBox.Show("分类编号必须是数字", "验证信息");
            //    txtCategoryId.Focus();
            //    txtCategoryId.SelectAll();
            //    return;
            //}
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

            #region 提交数据库修改

            try
            {
                if (objCategoryService.EditCategory(objCategory) == 1)
                {
                    MessageBox.Show("分类信息修改成功", "提示信息");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
        }
        /// <summary>
        /// 帮助链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llblHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", txtKMLink.Text);
        }
        /// <summary>
        /// 选择变化时更新描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobParentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblParentDesc.Text = cobParentId.SelectedValue.ToString();
        }
    }
}
