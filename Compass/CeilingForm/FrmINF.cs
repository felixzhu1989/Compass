using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmINF : MetroFramework.Forms.MetroForm
    {
       INFService objINFService = new INFService();
        private INF objINF = null;
        public FrmINF()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmINF(Drawing drawing, ModuleTree tree) : this()
        {
            objINF = (INF)objINFService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objINF == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
            FillData();
        }

        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (objINF == null) return;
            modelView.Tag = objINF.INFId;

            txtLength.Text = objINF.Length.ToString();
            txtWidth.Text = objINF.Width.ToString();
        }
        private void btnEditData_Click(object sender, EventArgs e)
        {
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 50m)
            {
                MessageBox.Show("请认真检查长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtWidth.Text.Trim()) || Convert.ToDecimal(txtWidth.Text.Trim()) < 20m)
            {
                MessageBox.Show("请认真检查宽度", "提示信息");
                txtWidth.Focus();
                txtWidth.SelectAll();
                return;
            }


            //封装对象
            INF objINF = new INF()
            {
                INFId = Convert.ToInt32(modelView.Tag),

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                Width = Convert.ToDecimal(txtWidth.Text.Trim())

            };
            //提交修改
            try
            {
                if (objINFService.EditModel(objINF) == 1)
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
