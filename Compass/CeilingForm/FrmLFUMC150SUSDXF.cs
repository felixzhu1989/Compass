using Common;
using DAL;
using Models;
using System;
using System.Windows.Forms;

namespace Compass
{
    public partial class FrmLfumc150Susdxf : MetroFramework.Forms.MetroForm
    {
        readonly LFUMC150SUSDXFService _objLfumc150SusdxfService = new LFUMC150SUSDXFService();
        private readonly LFUMC150SUSDXF _objLfumc150Susdxf = null;
        public FrmLfumc150Susdxf()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLfumc150Susdxf(Drawing drawing, ModuleTree tree) : this()
        {
            _objLfumc150Susdxf = (LFUMC150SUSDXF)_objLfumc150SusdxfService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objLfumc150Susdxf == null) return;
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
            if (_objLfumc150Susdxf == null) return;
            modelView.Tag = _objLfumc150Susdxf.LFUMC150SUSDXFId;

            //默认txtQuantity为1
            txtQuantity.Text = _objLfumc150Susdxf.Quantity == 0 ? "1" : _objLfumc150Susdxf.Quantity.ToString();
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
            LFUMC150SUSDXF objLfumc150Susdxf = new LFUMC150SUSDXF()
            {
                LFUMC150SUSDXFId = Convert.ToInt32(modelView.Tag),
                Quantity = Convert.ToInt32(txtQuantity.Text)

            };
            //提交修改
            try
            {
                if (_objLfumc150SusdxfService.EditModel(objLfumc150Susdxf) == 1)
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
