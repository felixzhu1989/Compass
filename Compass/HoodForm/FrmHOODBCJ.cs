using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmHoodbcj : MetroFramework.Forms.MetroForm
    {
        readonly HOODBCJService _objHoodbcjService = new HOODBCJService();
        private readonly HOODBCJ _objHoodbcj = null;
        public FrmHoodbcj()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmHoodbcj(Drawing drawing, ModuleTree tree) : this()
        {
            _objHoodbcj = (HOODBCJ)_objHoodbcjService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objHoodbcj == null) return;
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
            if (_objHoodbcj == null) return;
            modelView.Tag = _objHoodbcj.HOODBCJId;
            
            txtLength.Text = _objHoodbcj.Length.ToString();
            txtHeight.Text = _objHoodbcj.Height.ToString();
            txtSuDis.Text = _objHoodbcj.SuDis.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 90m)
            {
                MessageBox.Show("请认真检查CJ腔长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtHeight.Text.Trim()) || Convert.ToDecimal(txtHeight.Text.Trim()) < 90m)
            {
                MessageBox.Show("请认真检查CJ腔高度", "提示信息");
                txtHeight.Focus();
                txtHeight.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtSuDis.Text.Trim()) || Convert.ToDecimal(txtSuDis.Text.Trim()) < 50m)
            {
                MessageBox.Show("请认真检查脖颈距离右端面距离", "提示信息");
                txtSuDis.Focus();
                txtSuDis.SelectAll();
                return;
            }

            #endregion
            //封装对象
            HOODBCJ objHoodbcj = new HOODBCJ()
            {
                HOODBCJId = Convert.ToInt32(modelView.Tag),

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                Height = Convert.ToDecimal(txtHeight.Text.Trim()),
                SuDis = Convert.ToDecimal(txtSuDis.Text.Trim()),

            };
            //提交修改
            try
            {
                if (_objHoodbcjService.EditModel(objHoodbcj) == 1)
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
