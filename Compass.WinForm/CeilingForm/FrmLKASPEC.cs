using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLkaspec : MetroFramework.Forms.MetroForm
    {
        readonly LKASPECService _objLkaspecService = new LKASPECService();
        private readonly LKASPEC _objLkaspec = null;
        public FrmLkaspec()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLkaspec(Drawing drawing, ModuleTree tree) : this()
        {
            _objLkaspec = (LKASPEC)_objLkaspecService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objLkaspec == null) return;
            Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
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
            if (_objLkaspec == null) return;
            modelView.Tag = _objLkaspec.LKASPECId;

            cobSidePanel.Text = _objLkaspec.SidePanel;
            cobJapan.Text = _objLkaspec.Japan;
            cobLightType.Text = _objLkaspec.LightType;

            txtLength.Text = _objLkaspec.Length.ToString();
            txtHeight.Text = _objLkaspec.Height.ToString();
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
            if (!DataValidate.IsDouble(txtHeight.Text.Trim()) || Convert.ToDouble(txtHeight.Text.Trim()) < 20d)
            {
                MessageBox.Show("请认真检查灯腔高度", "提示信息");
                txtHeight.Focus();
                txtHeight.SelectAll();
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
            LKASPEC objLkaspec = new LKASPEC()
            {
                LKASPECId = Convert.ToInt32(modelView.Tag),

               SidePanel = cobSidePanel.Text,
                LightType = cobLightType.Text,
                Japan = cobJapan.Text,
                Length = Convert.ToDouble(txtLength.Text.Trim()),
                Height = Convert.ToDouble(txtHeight.Text.Trim())

            };
            //提交修改
            try
            {
                if (_objLkaspecService.EditModel(objLkaspec) == 1)
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
