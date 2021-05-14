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
    public partial class FrmSSPFLAT : MetroFramework.Forms.MetroForm
    {
        CategoryService objCategoryService = new CategoryService();
        SSPFLATService objSSPFLATService = new SSPFLATService();
        private SSPFLAT objSSPFLAT = null;
        public FrmSSPFLAT()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmSSPFLAT(Drawing drawing, ModuleTree tree) : this()
        {
            objSSPFLAT = (SSPFLAT)objSSPFLATService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objSSPFLAT == null) return;
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
            //M型灯板数量
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
            cobMPanelNo.Items.Add("10");
            cobMPanelNo.Items.Add("11");
            cobMPanelNo.Items.Add("12");
            cobMPanelNo.Items.Add("13");
            cobMPanelNo.Items.Add("14");
            cobMPanelNo.Items.Add("15");
            cobMPanelNo.Items.Add("16");
            cobMPanelNo.Items.Add("17");
            cobMPanelNo.Items.Add("18");
            cobMPanelNo.Items.Add("19");
            cobMPanelNo.Items.Add("20");

            //灯具类型
            cobLightType.Items.Add("LED60");
            cobLightType.Items.Add("NO");
            cobLightType.SelectedIndex = 1;

            //左灯板类型
            cobLeftType.Items.Add("W");
            cobLeftType.Items.Add("Z");
            //右灯板类型
            cobRightType.Items.Add("W");
            cobRightType.Items.Add("Z");

        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (objSSPFLAT == null) return;
            pbModelImage.Tag = objSSPFLAT.SSPFLATId;
            cobLeftType.Text = objSSPFLAT.LeftType;
            cobRightType.Text = objSSPFLAT.RightType;

            txtLength.Text = objSSPFLAT.Length.ToString();
            txtLeftLength.Text = objSSPFLAT.LeftLength.ToString();
            txtRightLength.Text = objSSPFLAT.RightLength.ToString();

            cobMPanelNo.Text = objSSPFLAT.MPanelNo.ToString();
            cobLightType.Text = objSSPFLAT.LightType;
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
            if (cobLeftType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择左灯板类型", "提示信息");
                cobLeftType.Focus();
                return;
            }
            if (cobRightType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择右灯板类型", "提示信息");
                cobRightType.Focus();
                return;
            }
            if (cobMPanelNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择M型水洗挡板数量", "提示信息");
                cobMPanelNo.Focus();
                return;
            }
            if (cobLightType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择烟罩是否带UV", "提示信息");
                cobLightType.Focus();
                return;
            }
            //封装对象
            SSPFLAT objSSPFLAT = new SSPFLAT()
            {
                SSPFLATId = Convert.ToInt32(pbModelImage.Tag),
                LeftType = cobLeftType.Text,
                RightType = cobRightType.Text,
                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                LeftLength = Convert.ToDecimal(txtLeftLength.Text.Trim()),
                RightLength = Convert.ToDecimal(txtRightLength.Text.Trim()),

                MPanelNo = Convert.ToInt32(cobMPanelNo.Text),
                LightType = cobLightType.Text
            };
            //提交修改
            try
            {
                if (objSSPFLATService.EditModel(objSSPFLAT) == 1)
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
