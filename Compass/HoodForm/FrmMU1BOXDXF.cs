using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmMu1Boxdxf :MetroFramework.Forms.MetroForm
    {
        readonly MU1BOXDXFService _objMu1BoxdxfService = new MU1BOXDXFService();
        private readonly MU1BOXDXF _objMu1Boxdxf = null;
        public FrmMu1Boxdxf()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmMu1Boxdxf(Drawing drawing, ModuleTree tree) : this()
        {
            _objMu1Boxdxf = (MU1BOXDXF)_objMu1BoxdxfService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objMu1Boxdxf == null) return;
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
            if (_objMu1Boxdxf == null) return;
            modelView.Tag = _objMu1Boxdxf.MU1BOXDXFId;

            //默认txtQuantity为1
            txtQuantity.Text = _objMu1Boxdxf.Quantity == 0 ? "1" : _objMu1Boxdxf.Quantity.ToString();
        }
        /// <summary>
        /// 修改参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditData_Click(object sender, EventArgs e)
        {

            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsInteger(txtQuantity.Text.Trim()))
            {
                MessageBox.Show("请认真检查数量是否填错", "提示信息");
                txtQuantity.Focus();
                txtQuantity.SelectAll();
                return;
            }


            #endregion
            //封装对象
            MU1BOXDXF objMu1Boxdxf = new MU1BOXDXF()
            {
                MU1BOXDXFId = Convert.ToInt32(modelView.Tag),
                Quantity = Convert.ToInt32(txtQuantity.Text)

            };
            //提交修改
            try
            {
                if (_objMu1BoxdxfService.EditModel(objMu1Boxdxf) == 1)
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
