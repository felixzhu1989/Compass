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
    public partial class FrmLFUSA : MetroFramework.Forms.MetroForm
    {
        CategoryService objCategoryService = new CategoryService();
        LFUSAService objLFUSAService = new LFUSAService();
        private LFUSA objLFUSA = null;
        public FrmLFUSA()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLFUSA(Drawing drawing, ModuleTree tree) : this()
        {
            objLFUSA = (LFUSA)objLFUSAService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objLFUSA == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            Category objCategory = objCategoryService.GetCategoryByCategoryId(tree.CategoryId.ToString(), tree.SBU);
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

            //均流桶数量
            cobSuNo.Items.Add("1");
            cobSuNo.Items.Add("2");
            cobSuNo.Items.Add("3");
            cobSuNo.Items.Add("4");
            cobSuNo.Items.Add("5");
            //均流桶直径
            cobSuDia.Items.Add("200");
            cobSuDia.Items.Add("250");

            //日本项目
            cobJapan.Items.Add("YES");
            cobJapan.Items.Add("No");
            cobJapan.SelectedIndex = 1;
        }

        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (objLFUSA == null) return;
            pbModelImage.Tag = objLFUSA.LFUSAId;

            cobSidePanel.Text = objLFUSA.SidePanel;
            cobSuNo.Text = objLFUSA.SuNo == 0 ? "" : objLFUSA.SuNo.ToString();
            cobSuDia.Text = objLFUSA.SuDia == 0 ? "" : ((int)objLFUSA.SuDia).ToString();
            cobJapan.Text = objLFUSA.Japan;

            txtLength.Text = objLFUSA.Length.ToString();
            txtWidth.Text = objLFUSA.Width.ToString();
            txtSuDis.Text = objLFUSA.SuDis.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (pbModelImage.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 200m)
            {
                MessageBox.Show("请认真检查散流器长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtWidth.Text.Trim()) || Convert.ToDecimal(txtWidth.Text.Trim()) < 200m)
            {
                MessageBox.Show("请认真检查散流器宽度", "提示信息");
                txtWidth.Focus();
                txtWidth.SelectAll();
                return;
            }
            if (cobSidePanel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择散流器侧板", "提示信息");
                cobSidePanel.Focus();
                return;
            }
            if (cobSuDia.SelectedIndex == -1)
            {
                MessageBox.Show("请选择均流桶直径", "提示信息");
                cobSuDia.Focus();
                return;
            }
            if (cobSuNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择均流桶数量", "提示信息");
                cobSuNo.Focus();
                return;
            }
            else if (cobSuNo.SelectedIndex > 0 && (!DataValidate.IsDecimal(txtSuDis.Text.Trim()) || Convert.ToDecimal(txtSuDis.Text.Trim()) < 250m))
            {
                MessageBox.Show("请认真检查均流桶间距", "提示信息");
                txtSuDis.Focus();
                txtSuDis.SelectAll();
                return;
            }
            if (cobJapan.SelectedIndex == -1)
            {
                MessageBox.Show("请选择是否为日本项目", "提示信息");
                cobJapan.Focus();
                return;
            }
            #endregion
            //封装对象
            LFUSA objLFUSA = new LFUSA()
            {
                LFUSAId = Convert.ToInt32(pbModelImage.Tag),
                SidePanel = cobSidePanel.Text,
                
                SuNo = Convert.ToInt32(cobSuNo.Text),
                SuDia = Convert.ToDecimal(cobSuDia.Text.Trim()),
                Japan = cobJapan.Text,

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                Width = Convert.ToDecimal(txtWidth.Text.Trim()),
                SuDis = Convert.ToDecimal(txtSuDis.Text.Trim())
            };
            //提交修改
            try
            {
                if (objLFUSAService.EditModel(objLFUSA) == 1)
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
        private void cobSuNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobSuNo.SelectedIndex == -1) return;
            if (cobSuNo.SelectedIndex > 0)
            {
                lblSuDis.Visible = true;
                txtSuDis.Visible = true;
            }
        }
    }
}
