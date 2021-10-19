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
    public partial class FrmLKS270HCL : MetroFramework.Forms.MetroForm
    {
        LKS270HCLService objLKS270HCLService = new LKS270HCLService();
        private LKS270HCL objLKS270HCL = null;
        public FrmLKS270HCL()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLKS270HCL(Drawing drawing, ModuleTree tree) : this()
        {
            objLKS270HCL = (LKS270HCL)objLKS270HCLService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objLKS270HCL == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
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
            if (objLKS270HCL == null) return;
            modelView.Tag = objLKS270HCL.LKS270HCLId;
            cobHCLSide.Text = objLKS270HCL.HCLSide;


            txtLength.Text = objLKS270HCL.Length.ToString();
            txtHCLSideLeft.Text = objLKS270HCL.HCLSideLeft.ToString();
            txtHCLSideRight.Text = objLKS270HCL.HCLSideRight.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
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
            LKS270HCL objLKS270HCL = new LKS270HCL()
            {
                LKS270HCLId = Convert.ToInt32(modelView.Tag),
                HCLSide = cobHCLSide.Text,
                HCLSideLeft = Convert.ToDecimal(txtHCLSideLeft.Text.Trim()),
                HCLSideRight = Convert.ToDecimal(txtHCLSideRight.Text.Trim()),
                Length = Convert.ToDecimal(txtLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (objLKS270HCLService.EditModel(objLKS270HCL) == 1)
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
