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
    public partial class FrmLKST270 : MetroFramework.Forms.MetroForm
    {
        CategoryService objCategoryService = new CategoryService();
        LKST270Service objLKST270Service = new LKST270Service();
        private LKST270 objLKST270 = null;
        public FrmLKST270()
        {
            InitializeComponent();
            IniCob();
        }
        public FrmLKST270(Drawing drawing, ModuleTree tree) : this()
        {
            objLKST270 = (LKST270)objLKST270Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objLKST270 == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            Category objCategory = objCategoryService.GetCategoryByCategoryId(tree.CategoryId.ToString());
            pbModelImage.Image = objCategory.ModelImage.Length == 0
                ? Image.FromFile("NoPic.png")
                : (Image)new SerializeObjectToString().DeserializeObject(objCategory.ModelImage);
            FillData();
        }
        private void IniCob()
        {
            //灯腔侧板
            cobSidePanel.Items.Add("LEFT");
            cobSidePanel.Items.Add("RIGHT");
            cobSidePanel.Items.Add("BOTH");
            cobSidePanel.Items.Add("MIDDLE");
            //水洗烟罩
            cobWBeam.Items.Add("YES");
            cobWBeam.Items.Add("NO");
            cobWBeam.SelectedIndex = 1;
            //灯具类型
            cobLightType.Items.Add("LED");
            cobLightType.Items.Add("T8");
            cobLightType.Items.Add("HCL");
            //日本项目
            cobJapan.Items.Add("YES");
            cobJapan.Items.Add("NO");
            cobJapan.SelectedIndex = 1;

        }
        private void FillData()
        {
            if (objLKST270 == null) return;
            pbModelImage.Tag = objLKST270.LKST270Id;

            cobWBeam.Text = objLKST270.WBeam;
            cobSidePanel.Text = objLKST270.SidePanel;
            cobJapan.Text = objLKST270.Japan;
            cobLightType.Text = objLKST270.LightType;

            txtLength.Text = objLKST270.Length.ToString();

        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (pbModelImage.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 100m)
            {
                MessageBox.Show("请认真检查灯腔长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            //侧板
            if (cobWBeam.SelectedIndex == -1)
            {
                MessageBox.Show("请选择是否为水洗烟罩", "提示信息");
                cobWBeam.Focus();
                return;
            }
            if (cobSidePanel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择是灯腔侧板", "提示信息");
                cobSidePanel.Focus();
                return;
            }
            //其他配置
            if (cobLightType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择灯具类型", "提示信息");
                cobLightType.Focus();
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
            LKST270 objLKST270 = new LKST270()
            {
                LKST270Id = Convert.ToInt32(pbModelImage.Tag),

                WBeam = cobWBeam.Text,
                SidePanel = cobSidePanel.Text,
                LightType = cobLightType.Text,
                Japan = cobJapan.Text,
                Length = Convert.ToDecimal(txtLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (objLKST270Service.EditModel(objLKST270) == 1)
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
