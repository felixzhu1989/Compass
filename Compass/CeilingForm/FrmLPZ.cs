using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLpz : MetroFramework.Forms.MetroForm
    {
        readonly LPZService _objLpzService = new LPZService();
        private readonly LPZ _objLpz = null;
        public FrmLpz()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLpz(Drawing drawing, ModuleTree tree) : this()
        {
            _objLpz = (LPZ)_objLpzService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objLpz == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
            FillData();
        }
        private void IniCob()
        {
            //Z板数量
            cobZPanelNo.Items.Add("0");
            cobZPanelNo.Items.Add("1");
            cobZPanelNo.Items.Add("2");
            cobZPanelNo.Items.Add("3");
            cobZPanelNo.Items.Add("4");
            cobZPanelNo.Items.Add("5");
            cobZPanelNo.Items.Add("6");
            cobZPanelNo.Items.Add("7");
            cobZPanelNo.Items.Add("8");
            cobZPanelNo.Items.Add("9");
            cobZPanelNo.Items.Add("10");
            cobZPanelNo.Items.Add("11");
            cobZPanelNo.Items.Add("12");
            cobZPanelNo.Items.Add("13");
            cobZPanelNo.Items.Add("14");
            cobZPanelNo.Items.Add("15");
            cobZPanelNo.Items.Add("16");
            cobZPanelNo.Items.Add("17");
            cobZPanelNo.Items.Add("18");
            cobZPanelNo.Items.Add("19");
            cobZPanelNo.Items.Add("20");
            //灯具类型
            cobLightType.Items.Add("LED60");
            cobLightType.Items.Add("NO");
            cobLightType.SelectedIndex = 1;
        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objLpz == null) return;
            modelView.Tag = _objLpz.LPZId;
            cobZPanelNo.Text = _objLpz.ZPanelNo.ToString();
            
            txtLength.Text = _objLpz.Length.ToString();
            txtWidth.Text = _objLpz.Width.ToString();

            cobLightType.Text = _objLpz.LightType;
        }
        private void btnEditData_Click(object sender, EventArgs e)
        {
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 50m)
            {
                MessageBox.Show("请认真检查LP板长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtWidth.Text.Trim()) || Convert.ToDecimal(txtWidth.Text.Trim()) < 20m)
            {
                MessageBox.Show("请认真检查W板宽度", "提示信息");
                txtWidth.Focus();
                txtWidth.SelectAll();
                return;
            }
            if (cobZPanelNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择Z板阵列数量", "提示信息");
                cobZPanelNo.Focus();
                return;
            }
            if (cobLightType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择是否带灯具", "提示信息");
                cobLightType.Focus();
                return;
            }
            //封装对象
            LPZ objLpz = new LPZ()
            {
                LPZId = Convert.ToInt32(modelView.Tag),

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                Width = Convert.ToDecimal(txtWidth.Text.Trim()),
                ZPanelNo = Convert.ToInt32(cobZPanelNo.Text.Trim()),
                LightType = cobLightType.Text
            };
            //提交修改
            try
            {
                if (_objLpzService.EditModel(objLpz) == 1)
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
