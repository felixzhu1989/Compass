using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmCj330 : MetroFramework.Forms.MetroForm
    {
        readonly CJ330Service _objCj330Service = new CJ330Service();
        private readonly CJ330 _objCj330 = null;
        public FrmCj330()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmCj330(Drawing drawing, ModuleTree tree) : this()
        {
            _objCj330 = (CJ330)_objCj330Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objCj330 == null) return;
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
            //脖颈
            cobSuType.Items.Add("SIDE");
            cobSuType.Items.Add("UP");
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
            if (_objCj330 == null) return;
            modelView.Tag = _objCj330.CJ330Id;

            cobSidePanel.Text = _objCj330.SidePanel;
            cobSuType.Text = _objCj330.SuType;
            cobBackCJSide.Text = _objCj330.BackCJSide;
            cobLeftBeamType.Text = _objCj330.LeftBeamType;
            cobRightBeamType.Text = _objCj330.RightBeamType;
            cobLKSide.Text = _objCj330.LKSide;
            cobGutterSide.Text = _objCj330.GutterSide;

            txtLength.Text = _objCj330.Length.ToString();
            txtSuDis.Text = _objCj330.SuDis.ToString();
            txtLeftDis.Text = _objCj330.LeftDis.ToString();
            txtRightDis.Text = _objCj330.RightDis.ToString();
            txtLeftBeamDis.Text = _objCj330.LeftBeamDis.ToString();
            txtRightBeamDis.Text = _objCj330.RightBeamDis.ToString();
            txtGutterWidth.Text = _objCj330.GutterWidth.ToString();
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
            if (cobSidePanel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择CJ腔侧板", "提示信息");
                cobSidePanel.Focus();
                return;
            }
            if (cobSuType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择脖颈方向", "提示信息");
                cobSuType.Focus();
                return;
            }
            if (!DataValidate.IsDecimal(txtSuDis.Text.Trim()) || Convert.ToDecimal(txtSuDis.Text.Trim()) < 50m)
            {
                MessageBox.Show("请认真检查脖颈距离右端面距离", "提示信息");
                txtSuDis.Focus();
                txtSuDis.SelectAll();
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
            CJ330 objCj330 = new CJ330()
            {
                CJ330Id = Convert.ToInt32(modelView.Tag),
                SidePanel = cobSidePanel.Text,
                SuType = cobSuType.Text,
                BackCJSide = cobBackCJSide.Text,
                LeftBeamType = cobLeftBeamType.Text,
                RightBeamType = cobRightBeamType.Text,
                LKSide = cobLKSide.Text,
                GutterSide = cobGutterSide.Text,

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                SuDis = Convert.ToDecimal(txtSuDis.Text.Trim()),
                LeftDis = Convert.ToDecimal(txtLeftDis.Text.Trim()),
                RightDis = Convert.ToDecimal(txtRightDis.Text.Trim()),
                LeftBeamDis = Convert.ToDecimal(txtLeftBeamDis.Text.Trim()),
                RightBeamDis = Convert.ToDecimal(txtRightBeamDis.Text.Trim()),
                GutterWidth = Convert.ToDecimal(txtGutterWidth.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objCj330Service.EditModel(objCj330) == 1)
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
