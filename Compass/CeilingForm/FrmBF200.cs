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
    public partial class FrmBF200 : MetroFramework.Forms.MetroForm
    {
        CategoryService objCategoryService = new CategoryService();
        BF200Service objBF200Service = new BF200Service();
        private BF200 objBF200 = null;
        public FrmBF200()
        {
            InitializeComponent();
            IniCob();
        }

        

        public FrmBF200(Drawing drawing, ModuleTree tree) : this()
        {
            objBF200 = (BF200)objBF200Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objBF200 == null) return;
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
            //M型水洗挡板数量
            cobMPanelNo.Items.Add("0");
            cobMPanelNo.Items.Add("1");
            cobMPanelNo.Items.Add("2");
            cobMPanelNo.Items.Add("3");
            cobMPanelNo.Items.Add("4");
            cobMPanelNo.Items.Add("5");
            cobMPanelNo.Items.Add("6");
            cobMPanelNo.Items.Add("7");
            cobMPanelNo.Items.Add("8");
            cobMPanelNo.Items.Add("9");

            //UV烟罩
            cobUVType.Items.Add("YES");
            cobUVType.Items.Add("NO");
            cobUVType.SelectedIndex = 1;
        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (objBF200 == null) return;
            pbModelImage.Tag = objBF200.BF200Id;

            txtLength.Text = objBF200.Length.ToString();
            txtLeftLength.Text = objBF200.LeftLength.ToString();
            txtRightLength.Text = objBF200.RightLength.ToString();
            txtMPanelLength.Text = objBF200.MPanelLength.ToString();
            txtWPanelLength.Text = objBF200.WPanelLength.ToString();

            cobMPanelNo.Text = objBF200.MPanelNo.ToString();
            cobUVType.Text = objBF200.UVType;
        }
        private void btnEditData_Click(object sender, EventArgs e)
        {
            //必填项目
            if (pbModelImage.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 200m)
            {
                MessageBox.Show("请认真检查总长", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtLeftLength.Text.Trim()) || Convert.ToDecimal(txtLeftLength.Text.Trim()) < 30m)
            {
                MessageBox.Show("请认真检查UL长度", "提示信息");
                txtLeftLength.Focus();
                txtLeftLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtRightLength.Text.Trim()) || Convert.ToDecimal(txtRightLength.Text.Trim()) < 30m)
            {
                MessageBox.Show("请认真检查UR长度", "提示信息");
                txtRightLength.Focus();
                txtRightLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtMPanelLength.Text.Trim()) || Convert.ToDecimal(txtMPanelLength.Text.Trim()) < 30m)
            {
                MessageBox.Show("请认真检查标准M型长度", "提示信息");
                txtMPanelLength.Focus();
                txtMPanelLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtWPanelLength.Text.Trim()) || Convert.ToDecimal(txtWPanelLength.Text.Trim()) < 30m)
            {
                MessageBox.Show("请认真检查标准W型长度", "提示信息");
                txtWPanelLength.Focus();
                txtWPanelLength.SelectAll();
                return;
            }
            if (cobMPanelNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择M型水洗挡板数量", "提示信息");
                cobMPanelNo.Focus();
                return;
            }
            if (cobUVType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择烟罩是否带UV", "提示信息");
                cobUVType.Focus();
                return;
            }
            //封装对象
            BF200 objBF200 = new BF200()
            {
                BF200Id = Convert.ToInt32(pbModelImage.Tag),

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                LeftLength = Convert.ToDecimal(txtLeftLength.Text.Trim()),
                RightLength = Convert.ToDecimal(txtRightLength.Text.Trim()),
                MPanelLength = Convert.ToDecimal(txtMPanelLength.Text.Trim()),
                WPanelLength = Convert.ToDecimal(txtWPanelLength.Text.Trim()),
                
                MPanelNo = Convert.ToInt32(cobMPanelNo.Text),
                UVType = cobUVType.Text
            };
            //提交修改
            try
            {
                if (objBF200Service.EditModel(objBF200) == 1)
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
