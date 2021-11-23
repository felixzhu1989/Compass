using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLsdost : MetroFramework.Forms.MetroForm
    {
        readonly LSDOSTService _objLsdostService = new LSDOSTService();
        private readonly LSDOST _objLsdost = null;
        public FrmLsdost()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLsdost(Drawing drawing, ModuleTree tree) : this()
        {
            _objLsdost = (LSDOST)_objLsdostService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objLsdost == null) return;
            Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
            FillData();
        }

        /// <summary>
        /// 初始化所有的下拉框
        /// </summary>
        private void IniCob()
        {
            //均流桶数量
            cobSuNo.Items.Add("1");
            cobSuNo.Items.Add("2");
            cobSuNo.Items.Add("3");
        }

        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objLsdost == null) return;
            modelView.Tag = _objLsdost.LSDOSTId;
            cobSuNo.Text = _objLsdost.SuNo == 0 ? "" : _objLsdost.SuNo.ToString();
            txtLength.Text = _objLsdost.Length.ToString();
            txtSuDis.Text = _objLsdost.SuDis.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 200m)
            {
                MessageBox.Show("请认真检查散流器长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            
            if (cobSuNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择均流桶数量", "提示信息");
                cobSuNo.Focus();
                return;
            }
            else if (cobSuNo.SelectedIndex > 0 && (!DataValidate.IsDecimal(txtSuDis.Text.Trim()) || Convert.ToDecimal(txtSuDis.Text.Trim()) < 250m))
            {
                MessageBox.Show("请认真检查均流桶间距", "提示信息");
                txtSuDis.Focus();
                txtSuDis.SelectAll();
                return;
            }
            
            #endregion
            //封装对象
            LSDOST objLsdost = new LSDOST()
            {
                LSDOSTId = Convert.ToInt32(modelView.Tag),
                SuNo = Convert.ToInt32(cobSuNo.Text),
                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                SuDis = Convert.ToDecimal(txtSuDis.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objLsdostService.EditModel(objLsdost) == 1)
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
        private void cobSuNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobSuNo.SelectedIndex == -1) return;
            if (cobSuNo.SelectedIndex > 0)
            {
                lblSuDis.Visible = true;
                txtSuDis.Visible = true;
            }
        }
    }
}
