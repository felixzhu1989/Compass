using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLLKSJ : MetroFramework.Forms.MetroForm
    {
        LLKSJService objLLKSJService = new LLKSJService();
        private LLKSJ objLLKSJ = null;
        public FrmLLKSJ()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLLKSJ(Drawing drawing, ModuleTree tree) : this()
        {
            objLLKSJ = (LLKSJ)objLLKSJService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objLLKSJ == null) return;
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
            if (objLLKSJ == null) return;
            modelView.Tag = objLLKSJ.LLKSJId;
            cobLongGlassNo.Text = objLLKSJ.LongGlassNo.ToString();
            cobShortGlassNo.Text = objLLKSJ.ShortGlassNo.ToString();
            txtLength.Text = objLLKSJ.Length.ToString();
            txtLeftLength.Text = objLLKSJ.LeftLength.ToString();
            txtRightLength.Text = objLLKSJ.RightLength.ToString();
        }
        private void btnEditData_Click(object sender, EventArgs e)
        {
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 100m)
            {
                MessageBox.Show("请认真检查灯腔侧板总长", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtLeftLength.Text.Trim()) || Convert.ToDecimal(txtLeftLength.Text.Trim()) < 20m)
            {
                MessageBox.Show("请认真检查左灯腔侧板长度", "提示信息");
                txtLeftLength.Focus();
                txtLeftLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtRightLength.Text.Trim()) || Convert.ToDecimal(txtRightLength.Text.Trim()) < 20m)
            {
                MessageBox.Show("请认真检查右灯腔侧板长度", "提示信息");
                txtRightLength.Focus();
                txtRightLength.SelectAll();
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
            LLKSJ objLLKSJ = new LLKSJ()
            {
                LLKSJId = Convert.ToInt32(modelView.Tag),

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                LongGlassNo = Convert.ToInt32(cobLongGlassNo.Text.Trim()),
                ShortGlassNo = Convert.ToInt32(cobShortGlassNo.Text.Trim()),
                LeftLength = Convert.ToDecimal(txtLeftLength.Text.Trim()),
                RightLength = Convert.ToDecimal(txtRightLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (objLLKSJService.EditModel(objLLKSJ) == 1)
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
