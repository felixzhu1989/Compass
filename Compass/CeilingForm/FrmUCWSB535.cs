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
    public partial class FrmUCWSB535 : MetroFramework.Forms.MetroForm
    {
        CategoryService objCategoryService = new CategoryService();
        UCWSB535Service objUCWSB535Service = new UCWSB535Service();
        private UCWSB535 objUCWSB535 = null;
        public FrmUCWSB535()
        {
            InitializeComponent();
            IniCob();
        }
        public FrmUCWSB535(Drawing drawing, ModuleTree tree) : this()
        {
            objUCWSB535 = (UCWSB535)objUCWSB535Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objUCWSB535 == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            Category objCategory = objCategoryService.GetCategoryByCategoryId(tree.CategoryId.ToString());
            pbModelImage.Image = objCategory.ModelImage.Length == 0
                ? Image.FromFile("NoPic.png")
                : (Image)new SerializeObjectToString().DeserializeObject(objCategory.ModelImage);
            FillData();
        }
        private void IniCob()
        {
            //排风腔位置
            cobSidePanel.Items.Add("LEFT");
            cobSidePanel.Items.Add("RIGHT");
            cobSidePanel.Items.Add("MIDDLE");
            cobSidePanel.Items.Add("BOTH");
            //排水槽位置
            cobDPSide.Items.Add("LEFT");
            cobDPSide.Items.Add("RIGHT");
            //ANSUL腔
            cobGutter.Items.Add("YES");
            cobGutter.Items.Add("NO");
            cobGutter.SelectedIndex = 1;
            //ANSUL
            cobANSUL.Items.Add("YES");
            cobANSUL.Items.Add("NO");
            cobANSUL.SelectedIndex = 1;
            //ANSUL侧喷
            cobANSide.Items.Add("LEFT");
            cobANSide.Items.Add("RIGHT");
            cobANSide.Items.Add("NO");
            cobANSide.SelectedIndex = 2;
            //MARVEL
            cobMARVEL.Items.Add("YES");
            cobMARVEL.Items.Add("NO");
            cobMARVEL.SelectedIndex = 1;
            //W水洗挡板数量
            cobSensorNo.Items.Add("0");
            cobSensorNo.Items.Add("1");
            cobSensorNo.Items.Add("2");
            cobSensorNo.Items.Add("3");
            //UV灯长短
            cobUVType.Items.Add("SHORT");
            cobUVType.Items.Add("LONG");
            cobUVType.SelectedIndex = 0;
            //灯具类型
            cobLightType.Items.Add("LED");
            cobLightType.Items.Add("T8");
            cobLightType.Items.Add("HCL");

            //SSP灯板类型
            cobSSPType.Items.Add("DOME");
            cobSSPType.Items.Add("FLAT");
            //日本项目
            cobJapan.Items.Add("YES");
            cobJapan.Items.Add("NO");
            cobJapan.SelectedIndex = 1;
            //油网左右
            cobFCSide.Items.Add("LEFT");
            cobFCSide.Items.Add("RIGHT");
            cobFCSide.Items.Add("BOTH");
            cobFCSide.Items.Add("NO");
            //盲板数量
            cobFCBlindNo.Items.Add("0");
            cobFCBlindNo.Items.Add("1");
            cobFCBlindNo.Items.Add("2");
            cobFCBlindNo.Items.Add("3");
            cobFCBlindNo.SelectedIndex = 0;
        }

        private void txtLength_TextChanged(object sender, EventArgs e)
        {
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || txtLength.Text.Trim().Length == 0) return;
            txtExRightDis.Text = (Convert.ToDecimal(txtLength.Text.Trim()) / 2).ToString();
        }

        private void FillData()
        {
            if (objUCWSB535 == null) return;
            pbModelImage.Tag = objUCWSB535.UCWSB535Id;

            cobSidePanel.Text = objUCWSB535.SidePanel;
            cobGutter.Text = objUCWSB535.Gutter;
            cobANSUL.Text = objUCWSB535.ANSUL;
            cobANSide.Text = objUCWSB535.ANSide;
            cobMARVEL.Text = objUCWSB535.MARVEL;
            cobLightType.Text = objUCWSB535.LightType;
            cobDPSide.Text = objUCWSB535.DPSide;
            cobSSPType.Text = objUCWSB535.SSPType;
            cobJapan.Text = objUCWSB535.Japan;
            cobFCSide.Text = objUCWSB535.FCSide;
            cobFCBlindNo.Text = objUCWSB535.FCBlindNo.ToString();
            cobUVType.Text = objUCWSB535.UVType.ToString();
            cobSensorNo.Text = objUCWSB535.SensorNo.ToString();

            txtLength.Text = objUCWSB535.Length.ToString();
            txtExRightDis.Text = objUCWSB535.ExRightDis.ToString();
            txtExLength.Text = objUCWSB535.ExLength.ToString();
            txtExWidth.Text = objUCWSB535.ExWidth.ToString();
            txtExHeight.Text = objUCWSB535.ExHeight.ToString();
            txtGutterWidth.Text = objUCWSB535.GutterWidth.ToString();
            txtFCSideLeft.Text = objUCWSB535.FCSideLeft.ToString();
            txtFCSideRight.Text = objUCWSB535.FCSideRight.ToString();
            txtSensorDis1.Text = objUCWSB535.SensorDis1.ToString();
            txtSensorDis2.Text = objUCWSB535.SensorDis2.ToString();
        }
        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (pbModelImage.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 100m)
            {
                MessageBox.Show("请认真检查烟罩长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            //排风腔位置
            if (cobSidePanel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择烟罩位置", "提示信息");
                cobSidePanel.Focus();
                return;
            }
            //ANSUL腔
            if (cobGutter.SelectedIndex == -1)
            {
                MessageBox.Show("请选择是否右ANSUL腔", "提示信息");
                cobGutter.Focus();
                return;
            }
            else if (cobGutter.SelectedIndex == 0 && (!DataValidate.IsDecimal(txtGutterWidth.Text.Trim()) || Convert.ToDecimal(txtGutterWidth.Text.Trim()) < 30m))
            {
                MessageBox.Show("请认真检查ANSUL腔宽度", "提示信息");//当脖颈大于2时需要填写脖颈间距
                txtGutterWidth.Focus();
                txtGutterWidth.SelectAll();
                return;
            }

            //脖颈
            if (!DataValidate.IsDecimal(txtExLength.Text.Trim()) || Convert.ToDecimal(txtExLength.Text.Trim()) < 50m)
            {
                MessageBox.Show("请填写脖颈长度", "提示信息");
                txtExLength.Focus();
                txtExLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtExWidth.Text.Trim()) || Convert.ToDecimal(txtExWidth.Text.Trim()) < 50m)
            {
                MessageBox.Show("请填写脖颈宽度", "提示信息");
                txtExWidth.Focus();
                txtExWidth.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtExHeight.Text.Trim()) || Convert.ToDecimal(txtExHeight.Text.Trim()) < 20m)
            {
                MessageBox.Show("请填写脖颈高度", "提示信息");
                txtExHeight.Focus();
                txtExHeight.SelectAll();
                return;
            }
            //ANSUL
            if (cobANSUL.SelectedIndex == -1)
            {
                MessageBox.Show("是否带ANSUL", "提示信息");
                cobANSUL.Focus();
                return;
            }

            if (cobMARVEL.SelectedIndex == -1)
            {
                MessageBox.Show("是否带MARVEL", "提示信息");
                cobMARVEL.Focus();
                return;
            }
            if (cobANSUL.SelectedIndex == 0)
            {
                if (cobANSide.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择ANSUL侧喷位置", "提示信息");
                    cobANSide.Focus();
                    return;
                }
            }
            //UV灯及水洗挡板配置
            if (cobUVType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择UV灯类型", "提示信息");
                cobUVType.Focus();
                return;
            }
            if (cobSensorNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择W水洗挡板磁感应数量", "提示信息");
                cobUVType.Focus();
                return;
            }
            if (cobSensorNo.SelectedIndex > 0 && (!DataValidate.IsDecimal(txtSensorDis1.Text.Trim()) || Convert.ToDecimal(txtSensorDis1.Text.Trim()) < 10m))
            {
                MessageBox.Show("请认真检查右边W水洗挡板距离烟罩右端面的距离", "提示信息");
                txtSensorDis1.Focus();
                txtSensorDis1.SelectAll();
                return;
            }
            if (cobSensorNo.SelectedIndex > 1 && (!DataValidate.IsDecimal(txtSensorDis2.Text.Trim()) || Convert.ToDecimal(txtSensorDis2.Text.Trim()) < 10m))
            {
                MessageBox.Show("请认真检查W水洗挡板中心间距", "提示信息");
                txtSensorDis2.Focus();
                txtSensorDis2.SelectAll();
                return;
            }
            //其他配置
            if (cobSSPType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择SSP灯板类型", "提示信息");
                cobSSPType.Focus();
                return;
            }
            if (cobJapan.SelectedIndex == -1)
            {
                MessageBox.Show("请选择是否为日本项目", "提示信息");
                cobJapan.Focus();
                return;
            }
            //灯具类型
            if (cobLightType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择灯具类型", "提示信息");
                cobLightType.Focus();
                return;
            }
            //排水槽位置
            if (cobDPSide.SelectedIndex == -1)
            {
                MessageBox.Show("请选择排水槽位置", "提示信息");
                cobDPSide.Focus();
                return;
            }
            //油网
            if (cobFCSide.SelectedIndex == -1)
            {
                MessageBox.Show("请选择油网侧板", "提示信息");
                cobFCSide.Focus();
                return;
            }
            if ((cobFCSide.SelectedIndex == 0 || cobFCSide.SelectedIndex == 2) && (!DataValidate.IsDecimal(txtFCSideLeft.Text.Trim()) || Convert.ToDecimal(txtFCSideLeft.Text.Trim()) < 10m))
            {
                MessageBox.Show("请认真检查左油网侧板长度", "提示信息");//当脖颈大于2时需要填写脖颈间距
                txtFCSideLeft.Focus();
                txtFCSideLeft.SelectAll();
                return;
            }
            if ((cobFCSide.SelectedIndex == 1 || cobFCSide.SelectedIndex == 2) && (!DataValidate.IsDecimal(txtFCSideRight.Text.Trim()) || Convert.ToDecimal(txtFCSideRight.Text.Trim()) < 10m))
            {
                MessageBox.Show("请认真检查右油网侧板长度", "提示信息");//当脖颈大于2时需要填写脖颈间距
                txtFCSideRight.Focus();
                txtFCSideRight.SelectAll();
                return;
            }

            #endregion
            //封装对象
            UCWSB535 objUCWSB535 = new UCWSB535()
            {
                UCWSB535Id = Convert.ToInt32(pbModelImage.Tag),
                ANSUL = cobANSUL.Text,
                ANSide = cobANSide.Text.Trim().Length == 0 ? "NO" : cobANSide.Text,
                MARVEL = cobMARVEL.Text,
                SSPType = cobSSPType.Text,
                Japan = cobJapan.Text,
                Gutter = cobGutter.Text,
                FCSide = cobFCSide.Text,
                FCBlindNo = Convert.ToInt32(cobFCBlindNo.Text.Trim()),
                SidePanel = cobSidePanel.Text,
                LightType = cobLightType.Text,
                DPSide = cobDPSide.Text,
                UVType = cobUVType.Text,
                SensorNo = Convert.ToInt32(cobSensorNo.Text.Trim()),

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                ExRightDis = Convert.ToDecimal(txtExRightDis.Text.Trim()),
                ExLength = Convert.ToDecimal(txtExLength.Text.Trim()),
                ExWidth = Convert.ToDecimal(txtExWidth.Text.Trim()),
                ExHeight = Convert.ToDecimal(txtExHeight.Text.Trim()),
                GutterWidth = Convert.ToDecimal(txtGutterWidth.Text.Trim()),
                FCSideLeft = Convert.ToDecimal(txtFCSideLeft.Text.Trim()),
                FCSideRight = Convert.ToDecimal(txtFCSideRight.Text.Trim()),
                SensorDis1 = Convert.ToDecimal(txtSensorDis1.Text.Trim()),
                SensorDis2 = Convert.ToDecimal(txtSensorDis2.Text.Trim())
            };
            //提交修改
            try
            {
                if (objUCWSB535Service.EditModel(objUCWSB535) == 1)
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
        private void cobGutter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobGutter.Text.Trim() == "YES")
            {
                lblGutterWidth.Visible = true;
                txtGutterWidth.Visible = true;
            }
            else
            {
                lblGutterWidth.Visible = false;
                txtGutterWidth.Visible = false;
            }
        }

        private void cobFCSide_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cobFCSide.Text.Trim())
            {
                case "LEFT":
                    lblFCSideLeft.Visible = true;
                    lblFCSideRight.Visible = false;
                    txtFCSideLeft.Visible = true;
                    txtFCSideRight.Visible = false;
                    break;
                case "RIGHT":
                    lblFCSideLeft.Visible = false;
                    lblFCSideRight.Visible = true;
                    txtFCSideLeft.Visible = false;
                    txtFCSideRight.Visible = true;
                    break;
                case "BOTH":
                    lblFCSideLeft.Visible = true;
                    lblFCSideRight.Visible = true;
                    txtFCSideLeft.Visible = true;
                    txtFCSideRight.Visible = true;
                    break;
                default:
                    lblFCSideLeft.Visible = false;
                    lblFCSideRight.Visible = false;
                    txtFCSideLeft.Visible = false;
                    txtFCSideRight.Visible = false;
                    break;
            }
        }

        private void cobANSUL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobANSUL.Text.Trim() == "YES")
            {
                lblANSide.Visible = true;
                cobANSide.Visible = true;
            }
            else
            {
                lblANSide.Visible = false;
                cobANSide.Visible = false;
            }
        }

        private void cobSensorNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobSensorNo.SelectedIndex == 1)
            {
                lblSensorDis1.Visible = true;
                lblSensorDis2.Visible = false;
                txtSensorDis1.Visible = true;
                txtSensorDis2.Visible = false;
            }
            else if (cobSensorNo.SelectedIndex > 1)
            {
                lblSensorDis1.Visible = true;
                lblSensorDis2.Visible = true;
                txtSensorDis1.Visible = true;
                txtSensorDis2.Visible = true;
            }
            else
            {
                lblSensorDis1.Visible = false;
                lblSensorDis2.Visible = false;
                txtSensorDis1.Visible = false;
                txtSensorDis2.Visible = false;
            }
        }
    }
}
