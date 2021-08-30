using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLFUMC200DXF : MetroFramework.Forms.MetroForm
    {
        LFUMC200DXFService objLFUMC200DXFService = new LFUMC200DXFService();
        private LFUMC200DXF objLFUMC200DXF = null;
        public FrmLFUMC200DXF()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLFUMC200DXF(Drawing drawing, ModuleTree tree) : this()
        {
            objLFUMC200DXF = (LFUMC200DXF)objLFUMC200DXFService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objLFUMC200DXF == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (objLFUMC200DXF == null) return;
            modelView.Tag = objLFUMC200DXF.LFUMC200DXFId;

            //默认txtQuantity为1
            txtQuantity.Text = objLFUMC200DXF.Quantity == 0 ? "1" : objLFUMC200DXF.Quantity.ToString();
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
            LFUMC200DXF objLFUMC200DXF = new LFUMC200DXF()
            {
                LFUMC200DXFId = Convert.ToInt32(modelView.Tag),
                Quantity = Convert.ToInt32(txtQuantity.Text)

            };
            //提交修改
            try
            {
                if (objLFUMC200DXFService.EditModel(objLFUMC200DXF) == 1)
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
