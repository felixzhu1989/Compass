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
    public partial class FrmUCWUVR4LDXF : MetroFramework.Forms.MetroForm
    {
        CategoryService objCategoryService = new CategoryService();
        UCWUVR4LDXFService objUCWUVR4LDXFService = new UCWUVR4LDXFService();
        private UCWUVR4LDXF objUCWUVR4LDXF = null;
        public FrmUCWUVR4LDXF()
        {
            InitializeComponent();
        }
        public FrmUCWUVR4LDXF(Drawing drawing, ModuleTree tree) : this()
        {
            objUCWUVR4LDXF = (UCWUVR4LDXF)objUCWUVR4LDXFService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objUCWUVR4LDXF == null) return;
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
            if (objUCWUVR4LDXF == null) return;
            pbModelImage.Tag = objUCWUVR4LDXF.UCWUVR4LDXFId;

            //默认txtQuantity为1
            txtQuantity.Text = objUCWUVR4LDXF.Quantity == 0 ? "1" : objUCWUVR4LDXF.Quantity.ToString();
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
            UCWUVR4LDXF objUCWUVR4LDXF = new UCWUVR4LDXF()
            {
                UCWUVR4LDXFId = Convert.ToInt32(pbModelImage.Tag),
                Quantity = Convert.ToInt32(txtQuantity.Text)

            };
            //提交修改
            try
            {
                if (objUCWUVR4LDXFService.EditModel(objUCWUVR4LDXF) == 1)
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
