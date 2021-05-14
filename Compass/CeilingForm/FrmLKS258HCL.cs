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
    public partial class FrmLKS258HCL : MetroFramework.Forms.MetroForm
    {
        CategoryService objCategoryService = new CategoryService();
        LKS258HCLService objLKS258HCLService = new LKS258HCLService();
        private LKS258HCL objLKS258HCL = null;
        public FrmLKS258HCL()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLKS258HCL(Drawing drawing, ModuleTree tree) : this()
        {
            objLKS258HCL = (LKS258HCL)objLKS258HCLService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objLKS258HCL == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            Category objCategory = objCategoryService.GetCategoryByCategoryId(tree.CategoryId.ToString(), tree.SBU);
            pbModelImage.Image = objCategory.ModelImage.Length == 0
                ? Image.FromFile("NoPic.png")
                : (Image)new SerializeObjectToString().DeserializeObject(objCategory.ModelImage);
            FillData();
        }
        private void IniCob()
        {
            //HCL左右
            cobHCLSide.Items.Add("LEFT");
            cobHCLSide.Items.Add("RIGHT");
            cobHCLSide.Items.Add("BOTH");
            cobHCLSide.Items.Add("NO");

        }
        private void FillData()
        {
            if (objLKS258HCL == null) return;
            pbModelImage.Tag = objLKS258HCL.LKS258HCLId;
            cobHCLSide.Text = objLKS258HCL.HCLSide;


            txtLength.Text = objLKS258HCL.Length.ToString();
            txtHCLSideLeft.Text = objLKS258HCL.HCLSideLeft.ToString();
            txtHCLSideRight.Text = objLKS258HCL.HCLSideRight.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (pbModelImage.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 100m)
            {
                MessageBox.Show("请认真检查灯腔长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }

            //其他配置
            if (cobHCLSide.SelectedIndex == -1)
            {
                MessageBox.Show("请选择HCL侧板位置", "提示信息");
                cobHCLSide.Focus();
                return;
            }
            if ((cobHCLSide.SelectedIndex == 0 || cobHCLSide.SelectedIndex == 2) && (!DataValidate.IsDecimal(txtHCLSideLeft.Text.Trim()) || Convert.ToDecimal(txtHCLSideLeft.Text.Trim()) < 10m))
            {
                MessageBox.Show("请认真检查左HCL侧板长度", "提示信息");
                txtHCLSideLeft.Focus();
                txtHCLSideLeft.SelectAll();
                return;
            }
            if ((cobHCLSide.SelectedIndex == 1 || cobHCLSide.SelectedIndex == 2) && (!DataValidate.IsDecimal(txtHCLSideRight.Text.Trim()) || Convert.ToDecimal(txtHCLSideRight.Text.Trim()) < 10m))
            {
                MessageBox.Show("请认真检查右HCL侧板长度", "提示信息");
                txtHCLSideRight.Focus();
                txtHCLSideRight.SelectAll();
                return;
            }


            #endregion
            //封装对象
            LKS258HCL objLKS258HCL = new LKS258HCL()
            {
                LKS258HCLId = Convert.ToInt32(pbModelImage.Tag),
                HCLSide = cobHCLSide.Text,
                HCLSideLeft = Convert.ToDecimal(txtHCLSideLeft.Text.Trim()),
                HCLSideRight = Convert.ToDecimal(txtHCLSideRight.Text.Trim()),
                Length = Convert.ToDecimal(txtLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (objLKS258HCLService.EditModel(objLKS258HCL) == 1)
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

        private void cobHCLSide_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cobHCLSide.Text.Trim())
            {
                case "LEFT":
                    lblHCLSideLeft.Visible = true;
                    lblHCLSideRight.Visible = false;
                    txtHCLSideLeft.Visible = true;
                    txtHCLSideRight.Visible = false;
                    break;
                case "RIGHT":
                    lblHCLSideLeft.Visible = false;
                    lblHCLSideRight.Visible = true;
                    txtHCLSideLeft.Visible = false;
                    txtHCLSideRight.Visible = true;
                    break;
                case "BOTH":
                    lblHCLSideLeft.Visible = true;
                    lblHCLSideRight.Visible = true;
                    txtHCLSideLeft.Visible = true;
                    txtHCLSideRight.Visible = true;
                    break;
                default:
                    lblHCLSideLeft.Visible = false;
                    lblHCLSideRight.Visible = false;
                    txtHCLSideLeft.Visible = false;
                    txtHCLSideRight.Visible = false;
                    break;
            }
        }
    }
}
