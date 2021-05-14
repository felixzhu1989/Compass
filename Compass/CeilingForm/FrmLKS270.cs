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
    public partial class FrmLKS270 : MetroFramework.Forms.MetroForm
    {
        CategoryService objCategoryService = new CategoryService();
        LKS270Service objLKS270Service = new LKS270Service();
        private LKS270 objLKS270 = null;
        public FrmLKS270()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLKS270(Drawing drawing, ModuleTree tree) : this()
        {
            objLKS270 = (LKS270)objLKS270Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objLKS270 == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            Category objCategory = objCategoryService.GetCategoryByCategoryId(tree.CategoryId.ToString(), tree.SBU);
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
            cobSidePanel.SelectedIndex = 2;
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
            if (objLKS270 == null) return;
            pbModelImage.Tag = objLKS270.LKS270Id;

            cobWBeam.Text = objLKS270.WBeam;
            cobSidePanel.Text = objLKS270.SidePanel;
            cobJapan.Text = objLKS270.Japan;
            cobLightType.Text = objLKS270.LightType;
            
            txtLength.Text = objLKS270.Length.ToString();
            
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
            LKS270 objLKS270 = new LKS270()
            {
                LKS270Id = Convert.ToInt32(pbModelImage.Tag),

                WBeam = cobWBeam.Text,
                SidePanel = cobSidePanel.Text,
                LightType = cobLightType.Text,
                Japan = cobJapan.Text,
                Length = Convert.ToDecimal(txtLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (objLKS270Service.EditModel(objLKS270) == 1)
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
