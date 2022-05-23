using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmSspdome : MetroFramework.Forms.MetroForm
    {
        readonly SSPDOMEService _objSspdomeService = new SSPDOMEService();
        private readonly SSPDOME _objSspdome = null;
        public FrmSspdome()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmSspdome(Drawing drawing, ModuleTree tree) : this()
        {
            _objSspdome = (SSPDOME)_objSspdomeService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objSspdome == null) return;
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
            //M型灯板数量
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
            cobMPanelNo.Items.Add("10");
            cobMPanelNo.Items.Add("11");
            cobMPanelNo.Items.Add("12");
            cobMPanelNo.Items.Add("13");
            cobMPanelNo.Items.Add("14");
            cobMPanelNo.Items.Add("15");
            cobMPanelNo.Items.Add("16");
            cobMPanelNo.Items.Add("17");
            cobMPanelNo.Items.Add("18");
            cobMPanelNo.Items.Add("19");
            cobMPanelNo.Items.Add("20");

            //灯具类型
            cobLightType.Items.Add("LED60");
            cobLightType.Items.Add("NO");
            cobLightType.SelectedIndex = 1;

            //左灯板类型
            cobLeftType.Items.Add("W");
            cobLeftType.Items.Add("Z");
            //右灯板类型
            cobRightType.Items.Add("W");
            cobRightType.Items.Add("Z");

        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objSspdome == null) return;
            modelView.Tag = _objSspdome.SSPDOMEId;
            cobLeftType.Text = _objSspdome.LeftType;
            cobRightType.Text = _objSspdome.RightType;

            txtLength.Text = _objSspdome.Length.ToString();
            txtLeftLength.Text = _objSspdome.LeftLength.ToString();
            txtRightLength.Text = _objSspdome.RightLength.ToString();

            cobMPanelNo.Text = _objSspdome.MPanelNo.ToString();
            cobLightType.Text = _objSspdome.LightType;
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
            if (cobLeftType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择左灯板类型", "提示信息");
                cobLeftType.Focus();
                return;
            }
            if (cobRightType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择右灯板类型", "提示信息");
                cobRightType.Focus();
                return;
            }
            if (cobMPanelNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择M型水洗挡板数量", "提示信息");
                cobMPanelNo.Focus();
                return;
            }
            if (cobLightType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择是否带灯具", "提示信息");
                cobLightType.Focus();
                return;
            }
            //封装对象
            SSPDOME objSspdome = new SSPDOME()
            {
                SSPDOMEId = Convert.ToInt32(modelView.Tag),
                LeftType = cobLeftType.Text,
                RightType = cobRightType.Text,
                Length = Convert.ToDouble(txtLength.Text.Trim()),
                LeftLength = Convert.ToDouble(txtLeftLength.Text.Trim()),
                RightLength = Convert.ToDouble(txtRightLength.Text.Trim()),

                MPanelNo = Convert.ToInt32(cobMPanelNo.Text),
                LightType = cobLightType.Text
            };
            //提交修改
            try
            {
                if (_objSspdomeService.EditModel(objSspdome) == 1)
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
