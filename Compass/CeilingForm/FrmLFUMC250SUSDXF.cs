using Common;
using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compass
{
    public partial class FrmLFUMC250SUSDXF : MetroFramework.Forms.MetroForm
    {
        CategoryService objCategoryService = new CategoryService();
        LFUMC250SUSDXFService objLFUMC250SUSDXFService = new LFUMC250SUSDXFService();
        private LFUMC250SUSDXF objLFUMC250SUSDXF = null;
        public FrmLFUMC250SUSDXF()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLFUMC250SUSDXF(Drawing drawing, ModuleTree tree) : this()
        {
            objLFUMC250SUSDXF = (LFUMC250SUSDXF)objLFUMC250SUSDXFService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objLFUMC250SUSDXF == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            Category objCategory = objCategoryService.GetCategoryByCategoryId(tree.CategoryId.ToString());
            pbModelImage.Image = objCategory.ModelImage.Length == 0
                ? Image.FromFile("NoPic.png")
                : (Image)new SerializeObjectToString().DeserializeObject(objCategory.ModelImage);
            FillData();
        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (objLFUMC250SUSDXF == null) return;
            pbModelImage.Tag = objLFUMC250SUSDXF.LFUMC250SUSDXFId;

            //默认txtQuantity为1
            txtQuantity.Text = objLFUMC250SUSDXF.Quantity == 0 ? "1" : objLFUMC250SUSDXF.Quantity.ToString();
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
            if (pbModelImage.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsInteger(txtQuantity.Text.Trim()))
            {
                MessageBox.Show("请认真检查数量是否填错", "提示信息");
                txtQuantity.Focus();
                txtQuantity.SelectAll();
                return;
            }


            #endregion
            //封装对象
            LFUMC250SUSDXF objLFUMC250SUSDXF = new LFUMC250SUSDXF()
            {
                LFUMC250SUSDXFId = Convert.ToInt32(pbModelImage.Tag),
                Quantity = Convert.ToInt32(txtQuantity.Text)

            };
            //提交修改
            try
            {
                if (objLFUMC250SUSDXFService.EditModel(objLFUMC250SUSDXF) == 1)
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
