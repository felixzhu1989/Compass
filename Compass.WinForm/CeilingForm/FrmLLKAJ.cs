using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLlkaj : MetroFramework.Forms.MetroForm

    {
        private ModelView modelView;
        readonly LLKAJService _objLlkajService = new LLKAJService();
        private readonly LLKAJ _objLlkaj = null;
        public FrmLlkaj()
        {
            InitializeComponent();
            modelView = new ModelView();
            panel1.Controls.Add(modelView);
            modelView.Dock = DockStyle.Fill;
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLlkaj(Drawing drawing, ModuleTree tree) : this()
        {
            _objLlkaj = (LLKAJ)_objLlkajService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objLlkaj == null) return;
            Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
            FillData();
        }
       
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objLlkaj == null) return;
            panel1.Tag = _objLlkaj.LLKAJId;
            txtLongGlassNo.Text = _objLlkaj.LongGlassNo.ToString();
            txtShortGlassNo.Text = _objLlkaj.ShortGlassNo.ToString();
            txtLength.Text = _objLlkaj.Length.ToString();
            txtLeftLength.Text = _objLlkaj.LeftLength.ToString();
            txtRightLength.Text = _objLlkaj.RightLength.ToString();
            txtMidLength.Text = _objLlkaj.MidLength.ToString();
        }
        private void btnEditData_Click(object sender, EventArgs e)
        {
            //必填项目
            if (panel1.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDouble(txtLength.Text.Trim()) || Convert.ToDouble(txtLength.Text.Trim()) < 100d)
            {
                MessageBox.Show("请认真检查灯腔侧板总长", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtLeftLength.Text.Trim()) || Convert.ToDouble(txtLeftLength.Text.Trim()) < 20d)
            {
                MessageBox.Show("请认真检查左灯腔侧板长度", "提示信息");
                txtLeftLength.Focus();
                txtLeftLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtRightLength.Text.Trim()) || Convert.ToDouble(txtRightLength.Text.Trim()) < 20d)
            {
                MessageBox.Show("请认真检查右灯腔侧板长度", "提示信息");
                txtRightLength.Focus();
                txtRightLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtMidLength.Text.Trim()) || Convert.ToDouble(txtMidLength.Text.Trim()) < 20d)
            {
                MessageBox.Show("请认真检查中间板长度", "提示信息");
                txtMidLength.Focus();
                txtMidLength.SelectAll();
                return;
            }
            if (!DataValidate.IsInteger(txtLongGlassNo.Text))
            {
                MessageBox.Show("请填写长玻璃数量", "提示信息");
                txtLongGlassNo.Focus();
                txtLongGlassNo.SelectAll();
                return;
            }
            if (!DataValidate.IsInteger(txtShortGlassNo.Text))
            {
                MessageBox.Show("请填写短玻璃数量", "提示信息");
                txtShortGlassNo.Focus();
                txtShortGlassNo.SelectAll();
                return;
            }
            //封装对象
            LLKAJ objLlkaj = new LLKAJ()
            {
                LLKAJId = Convert.ToInt32(panel1.Tag),

                Length = Convert.ToDouble(txtLength.Text.Trim()),
                LongGlassNo = Convert.ToInt32(txtLongGlassNo.Text.Trim()),
                ShortGlassNo = Convert.ToInt32(txtShortGlassNo.Text.Trim()),
                LeftLength = Convert.ToDouble(txtLeftLength.Text.Trim()),
                RightLength = Convert.ToDouble(txtRightLength.Text.Trim()),
                MidLength = Convert.ToDouble(txtMidLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objLlkajService.EditModel(objLlkaj) == 1)
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
