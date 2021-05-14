using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class FrmABD300 : MetroFramework.Forms.MetroForm
    {
        CategoryService objCategoryService = new CategoryService();
        ABD300Service objABD300Service = new ABD300Service();
        private ABD300 objABD300 = null;
        public FrmABD300()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmABD300(Drawing drawing, ModuleTree tree) : this()
        {
            objABD300 = (ABD300)objABD300Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objABD300 == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            Category objCategory = objCategoryService.GetCategoryByCategoryId(tree.CategoryId.ToString(), tree.SBU);
            pbModelImage.Image = objCategory.ModelImage.Length == 0
                ? Image.FromFile("NoPic.png")
                : (Image)new SerializeObjectToString().DeserializeObject(objCategory.ModelImage);
            FillData();
        }

        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (objABD300 == null) return;
            pbModelImage.Tag = objABD300.ABD300Id;
            txtLength.Text = objABD300.Length.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (pbModelImage.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 50m)
            {
                MessageBox.Show("请认真检查脖颈长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            #endregion

            //封装对象
            ABD300 objABD300 = new ABD300()
            {
                ABD300Id = Convert.ToInt32(pbModelImage.Tag),
                Length = Convert.ToDecimal(txtLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (objABD300Service.EditModel(objABD300) == 1)
                {
                    MessageBox.Show("制图数据修改成功", "提示信息");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
