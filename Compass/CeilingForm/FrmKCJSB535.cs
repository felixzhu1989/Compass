using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;


namespace Compass
{
    public partial class FrmKcjsb535 : MetroFramework.Forms.MetroForm
    {
        readonly KCJSB535Service _objKcjsb535Service = new KCJSB535Service();
        private readonly KCJSB535 _objKcjsb535 = null;
        public FrmKcjsb535()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmKcjsb535(Drawing drawing, ModuleTree tree) : this()
        {
            _objKcjsb535 = (KCJSB535)_objKcjsb535Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objKcjsb535 == null) return;
            Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
            FillData();
        }
        private void IniCob()
        {
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
            //ANSUL探测器
            cobANDetector.Items.Add("LEFT");
            cobANDetector.Items.Add("RIGHT");
            cobANDetector.Items.Add("BOTH");
            cobANDetector.Items.Add("NO");
            cobANDetector.SelectedIndex = 3;
            //MARVEL
            cobMARVEL.Items.Add("YES");
            cobMARVEL.Items.Add("NO");
            cobMARVEL.SelectedIndex = 1;
            //灯具类型
            cobLightType.Items.Add("LED");
            cobLightType.Items.Add("T8");
            cobLightType.Items.Add("HCL");
            //HCL灯板
            cobLightPanelSide.Items.Add("LEFT");
            cobLightPanelSide.Items.Add("RIGHT");
            cobLightPanelSide.Items.Add("BOTH");
            cobLightPanelSide.Items.Add("NO");
            //灯腔出线孔位置
            cobLightCable.Items.Add("LEFT");
            cobLightCable.Items.Add("RIGHT");
            cobLightCable.Items.Add("NO");
            cobLightCable.SelectedIndex = 2;
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
            //油网类型
            cobFCType.Items.Add("KSA");
            cobFCType.Items.Add("FC");
            cobFCType.SelectedIndex = 1;
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
            if (_objKcjsb535 == null) return;
            modelView.Tag = _objKcjsb535.KCJSB535Id;

            cobGutter.Text = _objKcjsb535.Gutter;
            cobANSUL.Text = _objKcjsb535.ANSUL;
            cobANSide.Text = _objKcjsb535.ANSide;
            cobANDetector.Text = _objKcjsb535.ANDetector;
            cobMARVEL.Text = _objKcjsb535.MARVEL;
            cobSSPType.Text = _objKcjsb535.SSPType;
            cobJapan.Text = _objKcjsb535.Japan;
            cobFCSide.Text = _objKcjsb535.FCSide;
            cobFCType.Text = _objKcjsb535.FCType;
            cobFCBlindNo.Text = _objKcjsb535.FCBlindNo.ToString();
            cobLightType.Text = _objKcjsb535.LightType;
            cobLightCable.Text = _objKcjsb535.LightCable;
            cobLightPanelSide.Text = _objKcjsb535.LightPanelSide;

            
            txtLength.Text = _objKcjsb535.Length.ToString();
            txtExRightDis.Text = _objKcjsb535.ExRightDis.ToString();
            txtExLength.Text = _objKcjsb535.ExLength.ToString();
            txtExWidth.Text = _objKcjsb535.ExWidth.ToString();
            txtExHeight.Text = _objKcjsb535.ExHeight.ToString();
            txtGutterWidth.Text = _objKcjsb535.GutterWidth.ToString();
            txtFCSideLeft.Text = _objKcjsb535.FCSideLeft.ToString();
            txtFCSideRight.Text = _objKcjsb535.FCSideRight.ToString();
            txtLightPanelLeft.Text = _objKcjsb535.LightPanelLeft.ToString();
            txtLightPanelRight.Text = _objKcjsb535.LightPanelRight.ToString();

        }
        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 100m)
            {
                MessageBox.Show("请认真检查烟罩长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
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
                if (cobANDetector.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择ANSUL探测器进出口位置", "提示信息");
                    cobANDetector.Focus();
                    return;
                }
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
            if (cobLightType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择灯具类型", "提示信息");
                cobLightType.Focus();
                return;
            }
            if (cobLightCable.SelectedIndex == -1)
            {
                MessageBox.Show("请选择灯腔出线孔位置", "提示信息");
                cobLightCable.Focus();
                return;
            }

            //油网侧板
            if (cobFCType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择油网类型", "提示信息");
                cobFCType.Focus();
                return;
            }
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
            KCJSB535 objKcjsb535 = new KCJSB535()
            {
                KCJSB535Id = Convert.ToInt32(modelView.Tag),
                ANSUL = cobANSUL.Text,
                ANSide = cobANSide.Text.Trim().Length == 0 ? "NO" : cobANSide.Text,
                ANDetector = cobANDetector.Text.Trim().Length == 0 ? "NO" : cobANDetector.Text,
                MARVEL = cobMARVEL.Text,
                SSPType = cobSSPType.Text,
                Japan = cobJapan.Text,
                Gutter = cobGutter.Text,
                FCSide = cobFCSide.Text,
                FCType = cobFCType.Text,
                FCBlindNo = Convert.ToInt32(cobFCBlindNo.Text.Trim()),
                LightType = cobLightType.Text,
                LightCable = cobLightCable.Text,
                LightPanelSide=cobLightPanelSide.Text,

                Length = txtLength.Text.Trim().Length == 0 ? 0 : Convert.ToDecimal(txtLength.Text.Trim()),
                ExRightDis = txtExRightDis.Text.Trim().Length == 0 ? 0 : Convert.ToDecimal(txtExRightDis.Text.Trim()),
                ExLength = txtExLength.Text.Trim().Length == 0 ? 0 : Convert.ToDecimal(txtExLength.Text.Trim()),
                ExWidth = txtExWidth.Text.Trim().Length == 0 ? 0 : Convert.ToDecimal(txtExWidth.Text.Trim()),
                ExHeight = txtExHeight.Text.Trim().Length == 0 ? 0 : Convert.ToDecimal(txtExHeight.Text.Trim()),
                GutterWidth = txtGutterWidth.Text.Trim().Length == 0 ? 0 : Convert.ToDecimal(txtGutterWidth.Text.Trim()),
                FCSideLeft = txtFCSideLeft.Text.Trim().Length == 0 ? 0 : Convert.ToDecimal(txtFCSideLeft.Text.Trim()),
                FCSideRight = txtFCSideRight.Text.Trim().Length == 0 ? 0 : Convert.ToDecimal(txtFCSideRight.Text.Trim()),
                LightPanelLeft= txtLightPanelLeft.Text.Trim().Length == 0 ? 0 : Convert.ToDecimal(txtLightPanelLeft.Text.Trim()),
                LightPanelRight = txtLightPanelRight.Text.Trim().Length == 0 ? 0 : Convert.ToDecimal(txtLightPanelRight.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objKcjsb535Service.EditModel(objKcjsb535) == 1)
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
                lblANDetector.Visible = true;
                cobANSide.Visible = true;
                cobANDetector.Visible = true;
            }
            else
            {
                lblANSide.Visible = false;
                lblANDetector.Visible = false;
                cobANSide.Visible = false;
                cobANDetector.Visible = false;
            }
        }

        private void cobLightType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobLightType.Text == "HCL")
            {
                cobLightPanelSide.Visible = true;
                lblLightPanelSide.Visible = true;
            
            }
            else
            {
                cobLightPanelSide.Visible = false;
                lblLightPanelSide.Visible = false;
                lblLightPanelLeft.Visible = false;
                lblLightPanelRight.Visible = false;
                txtLightPanelLeft.Visible = false;
                txtLightPanelRight.Visible = false;
            }
        }

        private void cobLightPanelSide_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cobLightPanelSide.Text.Trim())
            {
                case "LEFT":
                    lblLightPanelLeft.Visible = true;
                    lblLightPanelRight.Visible = false;
                    txtLightPanelLeft.Visible = true;
                    txtLightPanelRight.Visible = false;
                    break;
                case "RIGHT":
                    lblLightPanelLeft.Visible = false;
                    lblLightPanelRight.Visible = true;
                    txtLightPanelLeft.Visible = false;
                    txtLightPanelRight.Visible = true;
                    break;
                case "BOTH":
                    lblLightPanelLeft.Visible = true;
                    lblLightPanelRight.Visible = true;
                    txtLightPanelLeft.Visible = true;
                    txtLightPanelRight.Visible = true;
                    break;
                default:
                    lblLightPanelLeft.Visible = false;
                    lblLightPanelRight.Visible = false;
                    txtLightPanelLeft.Visible = false;
                    txtLightPanelRight.Visible = false;
                    break;
            }

        }
    }
}
