using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLfumc200Susdxf : MetroFramework.Forms.MetroForm
    {
        readonly LFUMC200SUSDXFService _objLfumc200SusdxfService = new LFUMC200SUSDXFService();
        private readonly LFUMC200SUSDXF _objLfumc200Susdxf = null;
        public FrmLfumc200Susdxf()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLfumc200Susdxf(Drawing drawing, ModuleTree tree) : this()
        {
            _objLfumc200Susdxf = (LFUMC200SUSDXF)_objLfumc200SusdxfService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objLfumc200Susdxf == null) return;
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
            if (_objLfumc200Susdxf == null) return;
            modelView.Tag = _objLfumc200Susdxf.LFUMC200SUSDXFId;

            //默认txtQuantity为1
            txtQuantity.Text = _objLfumc200Susdxf.Quantity == 0 ? "1" : _objLfumc200Susdxf.Quantity.ToString();
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
            LFUMC200SUSDXF objLfumc200Susdxf = new LFUMC200SUSDXF()
            {
                LFUMC200SUSDXFId = Convert.ToInt32(modelView.Tag),
                Quantity = Convert.ToInt32(txtQuantity.Text)

            };
            //提交修改
            try
            {
                if (_objLfumc200SusdxfService.EditModel(objLfumc200Susdxf) == 1)
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
