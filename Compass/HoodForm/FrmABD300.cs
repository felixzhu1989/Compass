using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmABD300 : MetroFramework.Forms.MetroForm
    {
        ABD300Service objABD300Service = new ABD300Service();
        private ABD300 objABD300 = null;
        public FrmABD300()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmABD300(Drawing drawing, ModuleTree tree) : this()
        {
            objABD300 = (ABD300)objABD300Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objABD300 == null) return;
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
            if (objABD300 == null) return;
            modelView.Tag = objABD300.ABD300Id;
            txtLength.Text = objABD300.Length.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 50m)
            {
                MessageBox.Show("请认真检查脖颈长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            #endregion

            //封装对象
            ABD300 objABD300 = new ABD300()
            {
                ABD300Id = Convert.ToInt32(modelView.Tag),
                Length = Convert.ToDecimal(txtLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (objABD300Service.EditModel(objABD300) == 1)
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
