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
    public partial class FrmMU1BOXDXF :MetroFramework.Forms.MetroForm
    {
        MU1BOXDXFService objMU1BOXDXFService = new MU1BOXDXFService();
        private MU1BOXDXF objMU1BOXDXF = null;
        public FrmMU1BOXDXF()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmMU1BOXDXF(Drawing drawing, ModuleTree tree) : this()
        {
            objMU1BOXDXF = (MU1BOXDXF)objMU1BOXDXFService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objMU1BOXDXF == null) return;
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
            if (objMU1BOXDXF == null) return;
            modelView.Tag = objMU1BOXDXF.MU1BOXDXFId;

            //默认txtQuantity为1
            txtQuantity.Text = objMU1BOXDXF.Quantity == 0 ? "1" : objMU1BOXDXF.Quantity.ToString();
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
            MU1BOXDXF objMU1BOXDXF = new MU1BOXDXF()
            {
                MU1BOXDXFId = Convert.ToInt32(modelView.Tag),
                Quantity = Convert.ToInt32(txtQuantity.Text)

            };
            //提交修改
            try
            {
                if (objMU1BOXDXFService.EditModel(objMU1BOXDXF) == 1)
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
