using System;
using System.Drawing;
using System.Windows.Forms;
using DAL;
using Models;
using Common;

namespace Compass
{
    public partial class FrmEditCategory : MetroFramework.Forms.MetroForm
    {
        private readonly CategoryService _objCategoryService = new CategoryService();
        private readonly string _sbu = Program.ObjCurrentUser.SBU;

        public FrmEditCategory()
        {
            InitializeComponent();
            //初始化下拉框
            cobParentId.DataSource = _objCategoryService.GetCategoryId(_sbu);
            cobParentId.DisplayMember = "CategoryId";
            cobParentId.ValueMember = "CategoryDesc";
            cobParentId.SelectedIndex = -1;
            cobParentId.SelectedIndexChanged += new EventHandler(CobParentId_SelectedIndexChanged);
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
        /// 提交修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditCategory_Click(object sender, EventArgs e)
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
                if (_objCategoryService.EditCategory(objCategory,_sbu) == 1)
                {
                    MessageBox.Show("分类信息修改成功", "提示信息");
                    DialogResult = DialogResult.OK;
                    Close();
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
        private void LlblHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", txtKMLink.Text);
        }
        /// <summary>
        /// 选择变化时更新描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobParentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblParentDesc.Text = cobParentId.SelectedValue.ToString();
        }
    }
}
