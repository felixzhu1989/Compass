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
    public partial class FrmLks270Hcl : MetroFramework.Forms.MetroForm
    {
        readonly LKS270HCLService _objLks270HclService = new LKS270HCLService();
        private readonly LKS270HCL _objLks270Hcl = null;
        public FrmLks270Hcl()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLks270Hcl(Drawing drawing, ModuleTree tree) : this()
        {
            _objLks270Hcl = (LKS270HCL)_objLks270HclService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objLks270Hcl == null) return;
            Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
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
            if (_objLks270Hcl == null) return;
            modelView.Tag = _objLks270Hcl.LKS270HCLId;
            cobHCLSide.Text = _objLks270Hcl.HCLSide;


            txtLength.Text = _objLks270Hcl.Length.ToString();
            txtHCLSideLeft.Text = _objLks270Hcl.HCLSideLeft.ToString();
            txtHCLSideRight.Text = _objLks270Hcl.HCLSideRight.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDouble(txtLength.Text.Trim()) || Convert.ToDouble(txtLength.Text.Trim()) < 100d)
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
            if ((cobHCLSide.SelectedIndex == 0 || cobHCLSide.SelectedIndex == 2) && (!DataValidate.IsDouble(txtHCLSideLeft.Text.Trim()) || Convert.ToDouble(txtHCLSideLeft.Text.Trim()) < 10d))
            {
                MessageBox.Show("请认真检查左HCL侧板长度", "提示信息");
                txtHCLSideLeft.Focus();
                txtHCLSideLeft.SelectAll();
                return;
            }
            if ((cobHCLSide.SelectedIndex == 1 || cobHCLSide.SelectedIndex == 2) && (!DataValidate.IsDouble(txtHCLSideRight.Text.Trim()) || Convert.ToDouble(txtHCLSideRight.Text.Trim()) < 10d))
            {
                MessageBox.Show("请认真检查右HCL侧板长度", "提示信息");
                txtHCLSideRight.Focus();
                txtHCLSideRight.SelectAll();
                return;
            }


            #endregion
            //封装对象
            LKS270HCL objLks270Hcl = new LKS270HCL()
            {
                LKS270HCLId = Convert.ToInt32(modelView.Tag),
                HCLSide = cobHCLSide.Text,
                HCLSideLeft = Convert.ToDouble(txtHCLSideLeft.Text.Trim()),
                HCLSideRight = Convert.ToDouble(txtHCLSideRight.Text.Trim()),
                Length = Convert.ToDouble(txtLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objLks270HclService.EditModel(objLks270Hcl) == 1)
                {
                    MessageBox.Show("制图数据修改成功", "提示信息");
                    DialogResult = DialogResult.OK;
                    Close();
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
