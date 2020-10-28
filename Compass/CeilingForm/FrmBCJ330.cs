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
    public partial class FrmBCJ330 : MetroFramework.Forms.MetroForm
    {
        CategoryService objCategoryService = new CategoryService();
        BCJ330Service objBCJ330Service = new BCJ330Service();
        private BCJ330 objBCJ330 = null;
        public FrmBCJ330()
        {
            InitializeComponent();
            IniCob();
        }
        public FrmBCJ330(Drawing drawing, ModuleTree tree) : this()
        {
            objBCJ330 = (BCJ330)objBCJ330Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objBCJ330 == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            Category objCategory = objCategoryService.GetCategoryByCategoryId(tree.CategoryId.ToString());
            pbModelImage.Image = objCategory.ModelImage.Length == 0
                ? Image.FromFile("NoPic.png")
                : (Image)new SerializeObjectToString().DeserializeObject(objCategory.ModelImage);
            FillData();
        }

        /// <summary>
        /// 初始化所有的下拉框
        /// </summary>
        private void IniCob()
        {
            //烟罩侧板
            cobSidePanel.Items.Add("LEFT");
            cobSidePanel.Items.Add("RIGHT");
            cobSidePanel.Items.Add("MIDDLE");
            cobSidePanel.Items.Add("BOTH");
            //脖颈
            cobSuType.Items.Add("SIDE");
            cobSuType.Items.Add("UP");
        }

        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (objBCJ330 == null) return;
            pbModelImage.Tag = objBCJ330.BCJ330Id;

            cobSidePanel.Text = objBCJ330.SidePanel;
            cobSuType.Text = objBCJ330.SuType;


            txtLength.Text = objBCJ330.Length.ToString();
            txtSuDis.Text = objBCJ330.SuDis.ToString();
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
            if (cobSidePanel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择CJ腔侧板", "提示信息");
                cobSidePanel.Focus();
                return;
            }
            if (cobSuType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择脖颈方向", "提示信息");
                cobSuType.Focus();
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
            BCJ330 objBCJ330 = new BCJ330()
            {
                BCJ330Id = Convert.ToInt32(pbModelImage.Tag),
                SidePanel = cobSidePanel.Text,
                SuType = cobSuType.Text,


                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                SuDis = Convert.ToDecimal(txtSuDis.Text.Trim()),

            };
            //提交修改
            try
            {
                if (objBCJ330Service.EditModel(objBCJ330) == 1)
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
