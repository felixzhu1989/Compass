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
    public partial class FrmNOCJSPEC : MetroFramework.Forms.MetroForm
    {
        CategoryService objCategoryService = new CategoryService();
        NOCJSPECService objNOCJSPECService = new NOCJSPECService();
        private NOCJSPEC objNOCJSPEC = null;
        public FrmNOCJSPEC()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmNOCJSPEC(Drawing drawing, ModuleTree tree) : this()
        {
            objNOCJSPEC = (NOCJSPEC)objNOCJSPECService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objNOCJSPEC == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
            FillData();
        }

        /// <summary>
        /// 初始化所有的下拉框
        /// </summary>
        private void IniCob()
        {
            //烟罩侧板
            cobSidePanel.Items.Add("LEFT");
            cobSidePanel.Items.Add("RIGHT");
            cobSidePanel.Items.Add("MIDDLE");
            cobSidePanel.Items.Add("BOTH");
            cobSidePanel.Items.Add("NOCJBACKL");
            cobSidePanel.Items.Add("NOCJBACKR");
            cobSidePanel.Items.Add("NOCJBACKB");
            //背面CJ腔位置
            cobBackCJSide.Items.Add("LEFT");
            cobBackCJSide.Items.Add("RIGHT");
            cobBackCJSide.Items.Add("NO");
            cobBackCJSide.Items.Add("BOTH");
            //左排风腔
            cobLeftBeamType.Items.Add("NO");
            cobLeftBeamType.Items.Add("KCJSB265");
            cobLeftBeamType.Items.Add("KCJSB290");
            cobLeftBeamType.Items.Add("KCJSB535");
            cobLeftBeamType.Items.Add("KCJDB800");
            cobLeftBeamType.Items.Add("UCJSB385");
            cobLeftBeamType.Items.Add("UCJSB535");
            cobLeftBeamType.Items.Add("UCJDB800");
            cobLeftBeamType.Items.Add("KCWSB265");
            cobLeftBeamType.Items.Add("KCWSB535");
            cobLeftBeamType.Items.Add("KCWDB800");
            cobLeftBeamType.Items.Add("UCWSB535");
            cobLeftBeamType.Items.Add("UCWDB800");
            //右排风腔
            cobRightBeamType.Items.Add("NO");
            cobRightBeamType.Items.Add("KCJSB265");
            cobRightBeamType.Items.Add("KCJSB290");
            cobRightBeamType.Items.Add("KCJSB535");
            cobRightBeamType.Items.Add("KCJDB800");
            cobRightBeamType.Items.Add("UCJSB385");
            cobRightBeamType.Items.Add("UCJSB535");
            cobRightBeamType.Items.Add("UCJDB800");
            cobRightBeamType.Items.Add("KCWSB265");
            cobRightBeamType.Items.Add("KCWSB535");
            cobRightBeamType.Items.Add("KCWDB800");
            cobRightBeamType.Items.Add("UCWSB535");
            cobRightBeamType.Items.Add("UCWDB800");
            //LK270位置
            cobLKSide.Items.Add("LEFT");
            cobLKSide.Items.Add("RIGHT");
            cobLKSide.Items.Add("NO");
            cobLKSide.Items.Add("BOTH");
            cobLKSide.SelectedIndex = 2;
            //GUTTER位置
            cobGutterSide.Items.Add("LEFT");
            cobGutterSide.Items.Add("RIGHT");
            cobGutterSide.Items.Add("NO");
            cobGutterSide.Items.Add("BOTH");
            cobGutterSide.SelectedIndex = 2;

            lblLeftBeamDis.Visible = false;
            txtLeftBeamDis.Visible = false;
            lblRightBeamDis.Visible = false;
            txtRightBeamDis.Visible = false;
        }

        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (objNOCJSPEC == null) return;
            modelView.Tag = objNOCJSPEC.NOCJSPECId;

            cobSidePanel.Text = objNOCJSPEC.SidePanel;

            cobBackCJSide.Text = objNOCJSPEC.BackCJSide;
            cobLeftBeamType.Text = objNOCJSPEC.LeftBeamType;
            cobRightBeamType.Text = objNOCJSPEC.RightBeamType;
            cobLKSide.Text = objNOCJSPEC.LKSide;
            cobGutterSide.Text = objNOCJSPEC.GutterSide;

            txtLength.Text = objNOCJSPEC.Length.ToString();
            txtWidth.Text = objNOCJSPEC.Width.ToString();
            txtHeight.Text = objNOCJSPEC.Height.ToString();
            txtTopBend.Text = objNOCJSPEC.TopBend.ToString();
            txtBackBend.Text = objNOCJSPEC.BackBend.ToString();
            txtLeftDis.Text = objNOCJSPEC.LeftDis.ToString();
            txtRightDis.Text = objNOCJSPEC.RightDis.ToString();
            txtLeftBeamDis.Text = objNOCJSPEC.LeftBeamDis.ToString();
            txtRightBeamDis.Text = objNOCJSPEC.RightBeamDis.ToString();
            txtGutterWidth.Text = objNOCJSPEC.GutterWidth.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 90m)
            {
                MessageBox.Show("请认真检查CJ腔长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtWidth.Text.Trim()) || Convert.ToDecimal(txtWidth.Text.Trim()) < 30m)
            {
                MessageBox.Show("请认真检查CJ腔宽度", "提示信息");
                txtWidth.Focus();
                txtWidth.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtHeight.Text.Trim()) || Convert.ToDecimal(txtHeight.Text.Trim()) < 30m)
            {
                MessageBox.Show("请认真检查CJ腔高度", "提示信息");
                txtHeight.Focus();
                txtHeight.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtTopBend.Text.Trim()) || Convert.ToDecimal(txtTopBend.Text.Trim()) < 7m)
            {
                MessageBox.Show("请认真检查CJ腔顶部翻边", "提示信息");
                txtTopBend.Focus();
                txtTopBend.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtBackBend.Text.Trim()) || Convert.ToDecimal(txtBackBend.Text.Trim()) < 20m)
            {
                MessageBox.Show("请认真检查CJ腔背部翻边", "提示信息");
                txtBackBend.Focus();
                txtBackBend.SelectAll();
                return;
            }
            if (cobSidePanel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择CJ腔侧板", "提示信息");
                cobSidePanel.Focus();
                return;
            }

            if (cobBackCJSide.SelectedIndex == -1)
            {
                MessageBox.Show("请选择BCJ位置，如果没有请选择NO", "提示信息");
                cobBackCJSide.Focus();
                return;
            }

            if (cobLeftBeamType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择左排风腔类型，如果没有请选择NO", "提示信息");
                cobBackCJSide.Focus();
                return;
            }
            if (cobRightBeamType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择右排风腔类型，如果没有请选择NO", "提示信息");
                cobBackCJSide.Focus();
                return;
            }
            if (cobLKSide.SelectedIndex == -1)
            {
                MessageBox.Show("请选择LK270位置", "提示信息");
                cobLKSide.Focus();
                return;
            }

            if (cobGutterSide.SelectedIndex == -1)
            {
                MessageBox.Show("请选择ANSUL腔的位置", "提示信息");
                cobGutterSide.Focus();
                return;
            }
            if (cobGutterSide.SelectedIndex != 2)
            {
                if (!DataValidate.IsDecimal(txtGutterWidth.Text.Trim()) || Convert.ToDecimal(txtGutterWidth.Text.Trim()) < 30m)
                {
                    MessageBox.Show("请认真检查ANSUL腔的宽度", "提示信息");
                    txtGutterWidth.Focus();
                    txtGutterWidth.SelectAll();
                    return;
                }
            }
            #endregion
            //封装对象
            NOCJSPEC objNOCJSPEC = new NOCJSPEC()
            {
                NOCJSPECId = Convert.ToInt32(modelView.Tag),
                SidePanel = cobSidePanel.Text,
                BackCJSide = cobBackCJSide.Text,
                LeftBeamType = cobLeftBeamType.Text,
                RightBeamType = cobRightBeamType.Text,
                LKSide = cobLKSide.Text,
                GutterSide = cobGutterSide.Text,

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                Width = Convert.ToDecimal(txtWidth.Text.Trim()),
                Height = Convert.ToDecimal(txtHeight.Text.Trim()),
                TopBend = Convert.ToDecimal(txtTopBend.Text.Trim()),
                BackBend = Convert.ToDecimal(txtBackBend.Text.Trim()),
                LeftDis = Convert.ToDecimal(txtLeftDis.Text.Trim()),
                RightDis = Convert.ToDecimal(txtRightDis.Text.Trim()),
                LeftBeamDis = Convert.ToDecimal(txtLeftBeamDis.Text.Trim()),
                RightBeamDis = Convert.ToDecimal(txtRightBeamDis.Text.Trim()),
                GutterWidth = Convert.ToDecimal(txtGutterWidth.Text.Trim())
            };
            //提交修改
            try
            {
                if (objNOCJSPECService.EditModel(objNOCJSPEC) == 1)
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

        private void cobLeftBeamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobLeftBeamType.SelectedIndex == 4 || cobLeftBeamType.SelectedIndex == 7 ||
                cobLeftBeamType.SelectedIndex == 10 || cobLeftBeamType.SelectedIndex == 12)
            {
                lblLeftBeamDis.Visible = true;
                txtLeftBeamDis.Visible = true;
            }
            else
            {
                lblLeftBeamDis.Visible = false;
                txtLeftBeamDis.Visible = false;
            }
        }

        private void cobRightBeamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobRightBeamType.SelectedIndex == 4 || cobRightBeamType.SelectedIndex == 7 ||
                cobRightBeamType.SelectedIndex == 10 || cobRightBeamType.SelectedIndex == 12)
            {
                lblRightBeamDis.Visible = true;
                txtRightBeamDis.Visible = true;
            }
            else
            {
                lblRightBeamDis.Visible = false;
                txtRightBeamDis.Visible = false;
            }
        }

        private void cobGutterSide_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobGutterSide.SelectedIndex == 2 || cobGutterSide.SelectedIndex == -1)
            {
                lblGutterWidth.Visible = false;
                txtGutterWidth.Visible = false;
            }
            else
            {
                lblGutterWidth.Visible = true;
                txtGutterWidth.Visible = true;
            }
        }
    }
}
