using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmAbd300 : MetroFramework.Forms.MetroForm
    {
        readonly ABD300Service _objAbd300Service = new ABD300Service();
        private readonly ABD300 _objAbd300 = null;
        public FrmAbd300()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmAbd300(Drawing drawing, ModuleTree tree) : this()
        {
            _objAbd300 = (ABD300)_objAbd300Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objAbd300 == null) return;
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
            if (_objAbd300 == null) return;
            modelView.Tag = _objAbd300.ABD300Id;
            txtLength.Text = _objAbd300.Length.ToString();
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
            ABD300 objAbd300 = new ABD300()
            {
                ABD300Id = Convert.ToInt32(modelView.Tag),
                Length = Convert.ToDecimal(txtLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objAbd300Service.EditModel(objAbd300) == 1)
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
