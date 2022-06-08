﻿using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmUcwdb800 : MetroFramework.Forms.MetroForm
    {
        readonly UCWDB800Service _objUcwdb800Service = new UCWDB800Service();
        private readonly UCWDB800 _objUcwdb800 = null;
        public FrmUcwdb800()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmUcwdb800(Drawing drawing, ModuleTree tree) : this()
        {
            _objUcwdb800 = (UCWDB800)_objUcwdb800Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objUcwdb800 == null) return;
            Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
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
            if (!DataValidate.IsDouble(txtLength.Text.Trim()) || txtLength.Text.Trim().Length == 0) return;
            txtExRightDis.Text = (Convert.ToDecimal(txtLength.Text.Trim()) / 2).ToString();
        }

        private void FillData()
        {
            if (_objUcwdb800 == null) return;
            modelView.Tag = _objUcwdb800.UCWDB800Id;

            cobSidePanel.Text = _objUcwdb800.SidePanel;
            cobGutter.Text = _objUcwdb800.Gutter;
            cobANSUL.Text = _objUcwdb800.ANSUL;
            cobANSide.Text = _objUcwdb800.ANSide;
            cobMARVEL.Text = _objUcwdb800.MARVEL;
            cobLightType.Text = _objUcwdb800.LightType;
            cobDPSide.Text = _objUcwdb800.DPSide;
            cobSSPType.Text = _objUcwdb800.SSPType;
            cobJapan.Text = _objUcwdb800.Japan;
            cobFCSide.Text = _objUcwdb800.FCSide;
            cobFCBlindNo.Text = _objUcwdb800.FCBlindNo.ToString();
            cobUVType.Text = _objUcwdb800.UVType.ToString();
            cobSensorNo.Text = _objUcwdb800.SensorNo.ToString();

            txtLength.Text = _objUcwdb800.Length.ToString();
            txtExRightDis.Text = _objUcwdb800.ExRightDis.ToString();
            txtExLength.Text = _objUcwdb800.ExLength.ToString();
            txtExWidth.Text = _objUcwdb800.ExWidth.ToString();
            txtExHeight.Text = _objUcwdb800.ExHeight.ToString();
            txtGutterWidth.Text = _objUcwdb800.GutterWidth.ToString();
            txtFCSideLeft.Text = _objUcwdb800.FCSideLeft.ToString();
            txtFCSideRight.Text = _objUcwdb800.FCSideRight.ToString();
            txtSensorDis1.Text = _objUcwdb800.SensorDis1.ToString();
            txtSensorDis2.Text = _objUcwdb800.SensorDis2.ToString();
        }
        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDouble(txtLength.Text.Trim()) || Convert.ToDouble(txtLength.Text.Trim()) < 100d)
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
            else if (cobGutter.SelectedIndex == 0 && (!DataValidate.IsDouble(txtGutterWidth.Text.Trim()) || Convert.ToDouble(txtGutterWidth.Text.Trim()) < 30d))
            {
                MessageBox.Show("请认真检查ANSUL腔宽度", "提示信息");//当脖颈大于2时需要填写脖颈间距
                txtGutterWidth.Focus();
                txtGutterWidth.SelectAll();
                return;
            }

            //脖颈
            if (!DataValidate.IsDouble(txtExLength.Text.Trim()) || Convert.ToDouble(txtExLength.Text.Trim()) < 50d)
            {
                MessageBox.Show("请填写脖颈长度", "提示信息");
                txtExLength.Focus();
                txtExLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtExWidth.Text.Trim()) || Convert.ToDouble(txtExWidth.Text.Trim()) < 50d)
            {
                MessageBox.Show("请填写脖颈宽度", "提示信息");
                txtExWidth.Focus();
                txtExWidth.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtExHeight.Text.Trim()) || Convert.ToDouble(txtExHeight.Text.Trim()) < 20d)
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
            if (cobSensorNo.SelectedIndex > 0 && (!DataValidate.IsDouble(txtSensorDis1.Text.Trim()) || Convert.ToDouble(txtSensorDis1.Text.Trim()) < 10d))
            {
                MessageBox.Show("请认真检查右边W水洗挡板距离烟罩右端面的距离", "提示信息");
                txtSensorDis1.Focus();
                txtSensorDis1.SelectAll();
                return;
            }
            if (cobSensorNo.SelectedIndex > 1 && (!DataValidate.IsDouble(txtSensorDis2.Text.Trim()) || Convert.ToDouble(txtSensorDis2.Text.Trim()) < 10d))
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
            if ((cobFCSide.SelectedIndex == 0 || cobFCSide.SelectedIndex == 2) && (!DataValidate.IsDouble(txtFCSideLeft.Text.Trim()) || Convert.ToDouble(txtFCSideLeft.Text.Trim()) < 10d))
            {
                MessageBox.Show("请认真检查左油网侧板长度", "提示信息");//当脖颈大于2时需要填写脖颈间距
                txtFCSideLeft.Focus();
                txtFCSideLeft.SelectAll();
                return;
            }
            if ((cobFCSide.SelectedIndex == 1 || cobFCSide.SelectedIndex == 2) && (!DataValidate.IsDouble(txtFCSideRight.Text.Trim()) || Convert.ToDouble(txtFCSideRight.Text.Trim()) < 10d))
            {
                MessageBox.Show("请认真检查右油网侧板长度", "提示信息");//当脖颈大于2时需要填写脖颈间距
                txtFCSideRight.Focus();
                txtFCSideRight.SelectAll();
                return;
            }

            #endregion
            //封装对象
            UCWDB800 objUcwdb800 = new UCWDB800()
            {
                UCWDB800Id = Convert.ToInt32(modelView.Tag),
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

                Length = Convert.ToDouble(txtLength.Text.Trim()),
                ExRightDis = Convert.ToDouble(txtExRightDis.Text.Trim()),
                ExLength = Convert.ToDouble(txtExLength.Text.Trim()),
                ExWidth = Convert.ToDouble(txtExWidth.Text.Trim()),
                ExHeight = Convert.ToDouble(txtExHeight.Text.Trim()),
                GutterWidth = Convert.ToDouble(txtGutterWidth.Text.Trim()),
                FCSideLeft = Convert.ToDouble(txtFCSideLeft.Text.Trim()),
                FCSideRight = Convert.ToDouble(txtFCSideRight.Text.Trim()),
                SensorDis1 = Convert.ToDouble(txtSensorDis1.Text.Trim()),
                SensorDis2 = Convert.ToDouble(txtSensorDis2.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objUcwdb800Service.EditModel(objUcwdb800) == 1)
                {
                    MessageBox.Show("制图数据修改成功", "提示信息");
                    DialogResult = DialogResult.OK;
                    Close();
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