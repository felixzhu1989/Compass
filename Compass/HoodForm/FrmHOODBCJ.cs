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
    public partial class FrmHOODBCJ : MetroFramework.Forms.MetroForm
    {
        CategoryService objCategoryService = new CategoryService();
        HOODBCJService objHOODBCJService = new HOODBCJService();
        private HOODBCJ objHOODBCJ = null;
        public FrmHOODBCJ()
        {
            InitializeComponent();
            
        }
        public FrmHOODBCJ(Drawing drawing, ModuleTree tree) : this()
        {
            objHOODBCJ = (HOODBCJ)objHOODBCJService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objHOODBCJ == null) return;
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
            if (objHOODBCJ == null) return;
            pbModelImage.Tag = objHOODBCJ.HOODBCJId;
            
            txtLength.Text = objHOODBCJ.Length.ToString();
            txtHeight.Text = objHOODBCJ.Height.ToString();
            txtSuDis.Text = objHOODBCJ.SuDis.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (pbModelImage.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 90m)
            {
                MessageBox.Show("请认真检查CJ腔长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtHeight.Text.Trim()) || Convert.ToDecimal(txtHeight.Text.Trim()) < 90m)
            {
                MessageBox.Show("请认真检查CJ腔高度", "提示信息");
                txtHeight.Focus();
                txtHeight.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtSuDis.Text.Trim()) || Convert.ToDecimal(txtSuDis.Text.Trim()) < 50m)
            {
                MessageBox.Show("请认真检查脖颈距离右端面距离", "提示信息");
                txtSuDis.Focus();
                txtSuDis.SelectAll();
                return;
            }

            #endregion
            //封装对象
            HOODBCJ objHOODBCJ = new HOODBCJ()
            {
                HOODBCJId = Convert.ToInt32(pbModelImage.Tag),

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                Height = Convert.ToDecimal(txtHeight.Text.Trim()),
                SuDis = Convert.ToDecimal(txtSuDis.Text.Trim()),

            };
            //提交修改
            try
            {
                if (objHOODBCJService.EditModel(objHOODBCJ) == 1)
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
