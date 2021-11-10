using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmUcjfccombidxf : MetroFramework.Forms.MetroForm
    {
        readonly UCJFCCOMBIDXFService _objUcjfccombidxfService = new UCJFCCOMBIDXFService();
        private readonly UCJFCCOMBIDXF _objUcjfccombidxf = null;
        public FrmUcjfccombidxf()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmUcjfccombidxf(Drawing drawing, ModuleTree tree) : this()
        {
            _objUcjfccombidxf = (UCJFCCOMBIDXF)_objUcjfccombidxfService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objUcjfccombidxf == null) return;
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
            if (_objUcjfccombidxf == null) return;
            modelView.Tag = _objUcjfccombidxf.UCJFCCOMBIDXFId;

            //默认txtQuantity为1
            txtQuantity.Text = _objUcjfccombidxf.Quantity == 0 ? "1" : _objUcjfccombidxf.Quantity.ToString();
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
            UCJFCCOMBIDXF objUcjfccombidxf = new UCJFCCOMBIDXF()
            {
                UCJFCCOMBIDXFId = Convert.ToInt32(modelView.Tag),
                Quantity = Convert.ToInt32(txtQuantity.Text)

            };
            //提交修改
            try
            {
                if (_objUcjfccombidxfService.EditModel(objUcjfccombidxf) == 1)
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
