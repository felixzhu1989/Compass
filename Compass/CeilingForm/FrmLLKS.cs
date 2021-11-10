using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLlks : MetroFramework.Forms.MetroForm
    {
        readonly LLKSService _objLlksService = new LLKSService();
        private readonly LLKS _objLlks = null;
        public FrmLlks()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLlks(Drawing drawing, ModuleTree tree) : this()
        {
            _objLlks = (LLKS)_objLlksService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objLlks == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
            FillData();
        }
        private void IniCob()
        {
            //玻璃数量
            cobLongGlassNo.Items.Add("0");
            cobLongGlassNo.Items.Add("1");
            cobLongGlassNo.Items.Add("2");
            cobLongGlassNo.Items.Add("3");
            cobLongGlassNo.Items.Add("4");
            cobLongGlassNo.Items.Add("5");
            cobLongGlassNo.Items.Add("6");
            cobLongGlassNo.Items.Add("7");
            cobLongGlassNo.Items.Add("8");
            cobLongGlassNo.Items.Add("9");
            cobLongGlassNo.Items.Add("10");
            cobLongGlassNo.Items.Add("11");
            cobLongGlassNo.Items.Add("12");
            cobLongGlassNo.Items.Add("13");
            cobLongGlassNo.Items.Add("14");
            cobLongGlassNo.Items.Add("15");
            cobLongGlassNo.Items.Add("16");
            cobLongGlassNo.Items.Add("17");
            cobLongGlassNo.Items.Add("18");
            cobLongGlassNo.Items.Add("19");
            cobLongGlassNo.Items.Add("20");

            cobShortGlassNo.Items.Add("0");
            cobShortGlassNo.Items.Add("1");
            cobShortGlassNo.Items.Add("2");
            cobShortGlassNo.Items.Add("3");
            cobShortGlassNo.Items.Add("4");
            cobShortGlassNo.Items.Add("5");
            cobShortGlassNo.Items.Add("6");
            cobShortGlassNo.Items.Add("7");
            cobShortGlassNo.Items.Add("8");
            cobShortGlassNo.Items.Add("9");
            cobShortGlassNo.Items.Add("10");
            cobShortGlassNo.Items.Add("11");
            cobShortGlassNo.Items.Add("12");
            cobShortGlassNo.Items.Add("13");
            cobShortGlassNo.Items.Add("14");
            cobShortGlassNo.Items.Add("15");
            cobShortGlassNo.Items.Add("16");
            cobShortGlassNo.Items.Add("17");
            cobShortGlassNo.Items.Add("18");
            cobShortGlassNo.Items.Add("19");
            cobShortGlassNo.Items.Add("20");

        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objLlks == null) return;
            modelView.Tag = _objLlks.LLKSId;
            cobLongGlassNo.Text = _objLlks.LongGlassNo.ToString();
            cobShortGlassNo.Text = _objLlks.ShortGlassNo.ToString();
            txtLength.Text = _objLlks.Length.ToString();
        }
        private void btnEditData_Click(object sender, EventArgs e)
        {
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 200m)
            {
                MessageBox.Show("请认真检查灯腔侧板总长", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (cobLongGlassNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择长玻璃数量", "提示信息");
                cobLongGlassNo.Focus();
                return;
            }
            if (cobShortGlassNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择短玻璃数量", "提示信息");
                cobShortGlassNo.Focus();
                return;
            }
            //封装对象
            LLKS objLlks = new LLKS()
            {
                LLKSId = Convert.ToInt32(modelView.Tag),

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                LongGlassNo = Convert.ToInt32(cobLongGlassNo.Text.Trim()),
                ShortGlassNo = Convert.ToInt32(cobShortGlassNo.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objLlksService.EditModel(objLlks) == 1)
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
