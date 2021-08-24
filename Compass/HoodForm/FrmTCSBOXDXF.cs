using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmTCSBOXDXF : MetroFramework.Forms.MetroForm
    {
        TCSBOXDXFService objTCSBOXDXFService = new TCSBOXDXFService();
        private TCSBOXDXF objTCSBOXDXF = null;
        public FrmTCSBOXDXF()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmTCSBOXDXF(Drawing drawing, ModuleTree tree) : this()
        {
            objTCSBOXDXF = (TCSBOXDXF)objTCSBOXDXFService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objTCSBOXDXF == null) return;
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
            if (objTCSBOXDXF == null) return;
            modelView.Tag = objTCSBOXDXF.TCSBOXDXFId;

            //默认txtQuantity为1
            txtQuantity.Text = objTCSBOXDXF.Quantity == 0 ? "1" : objTCSBOXDXF.Quantity.ToString();
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
            TCSBOXDXF objTCSBOXDXF = new TCSBOXDXF()
            {
                TCSBOXDXFId = Convert.ToInt32(modelView.Tag),
                Quantity = Convert.ToInt32(txtQuantity.Text)

            };
            //提交修改
            try
            {
                if (objTCSBOXDXFService.EditModel(objTCSBOXDXF) == 1)
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
