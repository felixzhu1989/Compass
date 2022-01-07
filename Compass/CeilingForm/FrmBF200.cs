using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmBf200 : MetroFramework.Forms.MetroForm
    {
        readonly BF200Service _objBf200Service = new BF200Service();
        private readonly BF200 _objBf200 = null;
        public FrmBf200()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }

        public FrmBf200(Drawing drawing, ModuleTree tree) : this()
        {
            _objBf200 = (BF200)_objBf200Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objBf200 == null) return;
            Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
            FillData();
        }
        /// <summary>
        /// 初始化所有的下拉框
        /// </summary>
        private void IniCob()
        {
            //M型水洗挡板数量
            cobMPanelNo.Items.Add("0");
            cobMPanelNo.Items.Add("1");
            cobMPanelNo.Items.Add("2");
            cobMPanelNo.Items.Add("3");
            cobMPanelNo.Items.Add("4");
            cobMPanelNo.Items.Add("5");
            cobMPanelNo.Items.Add("6");
            cobMPanelNo.Items.Add("7");
            cobMPanelNo.Items.Add("8");
            cobMPanelNo.Items.Add("9");

            //UV烟罩
            cobUVType.Items.Add("YES");
            cobUVType.Items.Add("NO");
            cobUVType.SelectedIndex = 1;
        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objBf200 == null) return;
            modelView.Tag = _objBf200.BF200Id;

            txtLength.Text = _objBf200.Length.ToString();
            txtLeftLength.Text = _objBf200.LeftLength.ToString();
            txtRightLength.Text = _objBf200.RightLength.ToString();
            txtMPanelLength.Text = _objBf200.MPanelLength.ToString();
            txtWPanelLength.Text = _objBf200.WPanelLength.ToString();

            cobMPanelNo.Text = _objBf200.MPanelNo.ToString();
            cobUVType.Text = _objBf200.UVType;
        }
        private void btnEditData_Click(object sender, EventArgs e)
        {
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDouble(txtLength.Text.Trim()) || Convert.ToDouble(txtLength.Text.Trim()) < 200d)
            {
                MessageBox.Show("请认真检查总长", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtLeftLength.Text.Trim()) || Convert.ToDouble(txtLeftLength.Text.Trim()) < 30d)
            {
                MessageBox.Show("请认真检查UL长度", "提示信息");
                txtLeftLength.Focus();
                txtLeftLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtRightLength.Text.Trim()) || Convert.ToDouble(txtRightLength.Text.Trim()) < 30d)
            {
                MessageBox.Show("请认真检查UR长度", "提示信息");
                txtRightLength.Focus();
                txtRightLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtMPanelLength.Text.Trim()) || Convert.ToDouble(txtMPanelLength.Text.Trim()) < 30d)
            {
                MessageBox.Show("请认真检查标准M型长度", "提示信息");
                txtMPanelLength.Focus();
                txtMPanelLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtWPanelLength.Text.Trim()) || Convert.ToDouble(txtWPanelLength.Text.Trim()) < 30d)
            {
                MessageBox.Show("请认真检查标准W型长度", "提示信息");
                txtWPanelLength.Focus();
                txtWPanelLength.SelectAll();
                return;
            }
            if (cobMPanelNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择M型水洗挡板数量", "提示信息");
                cobMPanelNo.Focus();
                return;
            }
            if (cobUVType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择烟罩是否带UV", "提示信息");
                cobUVType.Focus();
                return;
            }
            //封装对象
            BF200 objBf200 = new BF200()
            {
                BF200Id = Convert.ToInt32(modelView.Tag),

                Length = Convert.ToDouble(txtLength.Text.Trim()),
                LeftLength = Convert.ToDouble(txtLeftLength.Text.Trim()),
                RightLength = Convert.ToDouble(txtRightLength.Text.Trim()),
                MPanelLength = Convert.ToDouble(txtMPanelLength.Text.Trim()),
                WPanelLength = Convert.ToDouble(txtWPanelLength.Text.Trim()),
                
                MPanelNo = Convert.ToInt32(cobMPanelNo.Text),
                UVType = cobUVType.Text
            };
            //提交修改
            try
            {
                if (_objBf200Service.EditModel(objBf200) == 1)
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
    }
}
