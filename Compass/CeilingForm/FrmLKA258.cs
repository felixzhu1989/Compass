using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLka258 : MetroFramework.Forms.MetroForm
    {
        readonly LKA258Service _objLka258Service = new LKA258Service();
        private readonly LKA258 _objLka258 = null;
        public FrmLka258()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLka258(Drawing drawing, ModuleTree tree) : this()
        {
            _objLka258 = (LKA258)_objLka258Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objLka258 == null) return;
            Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
            FillData();
        }
        private void IniCob()
        {
            //灯具类型
            cobLightType.Items.Add("LED");
            cobLightType.Items.Add("T8");
            //日本项目
            cobJapan.Items.Add("YES");
            cobJapan.Items.Add("NO");
            cobJapan.SelectedIndex = 1;

        }
        private void FillData()
        {
            if (_objLka258 == null) return;
            modelView.Tag = _objLka258.LKA258Id;

            cobJapan.Text = _objLka258.Japan;
            cobLightType.Text = _objLka258.LightType;

            txtLength.Text = _objLka258.Length.ToString();

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
            LKA258 objLka258 = new LKA258()
            {
                LKA258Id = Convert.ToInt32(modelView.Tag),

                LightType = cobLightType.Text,
                Japan = cobJapan.Text,
                Length = Convert.ToDecimal(txtLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objLka258Service.EditModel(objLka258) == 1)
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
