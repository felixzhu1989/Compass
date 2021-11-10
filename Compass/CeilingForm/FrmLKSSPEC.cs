using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLksspec : MetroFramework.Forms.MetroForm
    {
        readonly LKSSPECService _objLksspecService = new LKSSPECService();
        private readonly LKSSPEC _objLksspec = null;
        public FrmLksspec()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLksspec(Drawing drawing, ModuleTree tree) : this()
        {
            _objLksspec = (LKSSPEC)_objLksspecService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objLksspec == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
            FillData();
        }
        private void IniCob()
        {
            //灯腔侧板
            cobSidePanel.Items.Add("LEFT");
            cobSidePanel.Items.Add("RIGHT");
            cobSidePanel.Items.Add("BOTH");
            cobSidePanel.Items.Add("MIDDLE");
            //水洗烟罩
            cobWBeam.Items.Add("YES");
            cobWBeam.Items.Add("NO");
            cobWBeam.SelectedIndex = 1;
            //灯具类型
            cobLightType.Items.Add("LED");
            cobLightType.Items.Add("T8");
            cobLightType.Items.Add("HCL");
            //日本项目
            cobJapan.Items.Add("YES");
            cobJapan.Items.Add("NO");
            cobJapan.SelectedIndex = 1;

        }
        private void FillData()
        {
            if (_objLksspec == null) return;
            modelView.Tag = _objLksspec.LKSSPECId;

            cobWBeam.Text = _objLksspec.WBeam;
            cobSidePanel.Text = _objLksspec.SidePanel;
            cobJapan.Text = _objLksspec.Japan;
            cobLightType.Text = _objLksspec.LightType;

            txtLength.Text = _objLksspec.Length.ToString();
            txtHeight.Text = _objLksspec.Height.ToString();
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
            if (!DataValidate.IsDecimal(txtHeight.Text.Trim()) || Convert.ToDecimal(txtHeight.Text.Trim()) < 20m)
            {
                MessageBox.Show("请认真检查灯腔高度", "提示信息");
                txtHeight.Focus();
                txtHeight.SelectAll();
                return;
            }
            //侧板
            if (cobWBeam.SelectedIndex == -1)
            {
                MessageBox.Show("请选择是否为水洗烟罩", "提示信息");
                cobWBeam.Focus();
                return;
            }
            if (cobSidePanel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择是灯腔侧板", "提示信息");
                cobSidePanel.Focus();
                return;
            }
            //其他配置
            if (cobLightType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择灯具类型", "提示信息");
                cobLightType.Focus();
                return;
            }
            if (cobJapan.SelectedIndex == -1)
            {
                MessageBox.Show("请选择是否为日本项目", "提示信息");
                cobJapan.Focus();
                return;
            }


            #endregion
            //封装对象
            LKSSPEC objLksspec = new LKSSPEC()
            {
                LKSSPECId = Convert.ToInt32(modelView.Tag),

                WBeam = cobWBeam.Text,
                SidePanel = cobSidePanel.Text,
                LightType = cobLightType.Text,
                Japan = cobJapan.Text,
                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                Height = Convert.ToDecimal(txtHeight.Text.Trim())
                
            };
            //提交修改
            try
            {
                if (_objLksspecService.EditModel(objLksspec) == 1)
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
