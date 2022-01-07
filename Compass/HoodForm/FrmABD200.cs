using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmAbd200 : MetroFramework.Forms.MetroForm
    {
        readonly ABD200Service _objAbd200Service =new ABD200Service();
        private readonly ABD200 _objAbd200 = null;
        public FrmAbd200()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmAbd200(Drawing drawing, ModuleTree tree) : this()
        {
            _objAbd200 = (ABD200)_objAbd200Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objAbd200 == null) return;
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
            if (_objAbd200 == null) return;
            modelView.Tag = _objAbd200.ABD200Id;
            txtLength.Text = _objAbd200.Length.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDouble(txtLength.Text.Trim()) || Convert.ToDouble(txtLength.Text.Trim()) < 50d)
            {
                MessageBox.Show("请认真检查脖颈长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            #endregion

            //封装对象
            ABD200 objAbd200 = new ABD200()
            {
                ABD200Id = Convert.ToInt32(modelView.Tag),
                Length = Convert.ToDouble(txtLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objAbd200Service.EditModel(objAbd200) == 1)
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
