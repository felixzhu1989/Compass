using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmHwuwf555400 : MetroFramework.Forms.MetroForm
    {
        readonly HWUWF555400Service _objHwuwf555400Service = new HWUWF555400Service();
        private readonly HWUWF555400 _objHwuwf555400 = null;
        public FrmHwuwf555400()
        {
            InitializeComponent();
            SetVisibleFalse();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmHwuwf555400(Drawing drawing, ModuleTree tree) : this()
        {
            _objHwuwf555400 = (HWUWF555400)_objHwuwf555400Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objHwuwf555400 == null) return;
            Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
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
            //排风脖颈数量
            cobExNo.Items.Add("1");
            cobExNo.Items.Add("2");
            //新风脖颈数量
            cobSuNo.Items.Add("1");
            cobSuNo.Items.Add("2");
           //灯具类型
            cobLightType.Items.Add("FSLONG");
            cobLightType.Items.Add("FSSHORT");
            cobLightType.Items.Add("LED60");
            cobLightType.Items.Add("LED140");
            //筒灯数量
            cobLEDSpotNo.Items.Add("1");
            cobLEDSpotNo.Items.Add("2");
            cobLEDSpotNo.Items.Add("3");
            cobLEDSpotNo.Items.Add("4");
            cobLEDSpotNo.Items.Add("5");
            cobLEDSpotNo.Items.Add("6");
            cobLEDSpotNo.Items.Add("7");
            //ANSUL
            cobANSUL.Items.Add("YES");
            cobANSUL.Items.Add("NO");
            cobANSUL.SelectedIndex = 1;
            //ANSUL侧喷
            cobANSide.Items.Add("LEFT");
            cobANSide.Items.Add("RIGHT");
            cobANSide.Items.Add("NO");
            //ANSUL末端探测器
            cobANDetectorEnd.Items.Add("LEFT");
            cobANDetectorEnd.Items.Add("RIGHT");
            cobANDetectorEnd.Items.Add("NO");
            //ANSUL探测器数量
            cobANDetectorNo.Items.Add("0");
            cobANDetectorNo.Items.Add("1");
            cobANDetectorNo.Items.Add("2");
            cobANDetectorNo.Items.Add("3");
            cobANDetectorNo.Items.Add("4");
            cobANDetectorNo.Items.Add("5");
            //ANSUL下喷
            cobANDropNo.Items.Add("0");
            cobANDropNo.Items.Add("1");
            cobANDropNo.Items.Add("2");
            cobANDropNo.Items.Add("3");
            cobANDropNo.Items.Add("4");
            cobANDropNo.Items.Add("5");
            //MARVEL
            cobMARVEL.Items.Add("YES");
            cobMARVEL.Items.Add("NO");
            cobMARVEL.SelectedIndex = 1;
            //IR数量
            cobIRNo.Items.Add("0");
            cobIRNo.Items.Add("1");
            cobIRNo.Items.Add("2");
            cobIRNo.Items.Add("3");
            //烟罩配置信息
            //UV灯类型
            cobUVType.Items.Add("DOUBLE");
            cobUVType.Items.Add("LONG");
            cobUVType.Items.Add("SHORT");
            //蓝牙面板
            cobBluetooth.Items.Add("YES");
            cobBluetooth.Items.Add("NO");
            //LOGO
            cobLEDLogo.Items.Add("YES");
            cobLEDLogo.Items.Add("NO");
            //排水口
            cobOutlet.Items.Add("LEFT");
            cobOutlet.Items.Add("RIGHT");
            cobOutlet.Items.Add("NO");
            //入水口
            cobInlet.Items.Add("LEFT");
            cobInlet.Items.Add("RIGHT");
            //集水翻边
            cobWaterCollection.Items.Add("YES");
            cobWaterCollection.Items.Add("NO");
            //背靠背
            cobBackToBack.Items.Add("YES");
            cobBackToBack.Items.Add("NO");
            cobBackToBack.SelectedIndex = 1;
        }
        /// <summary>
        /// 将分组隐藏
        /// </summary>
        private void SetVisibleFalse()
        {
            grbLEDSpot.Visible = false;
            grbANSUL.Visible = false;
            grbMARVEL.Visible = false;
        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objHwuwf555400 == null) return;
            modelView.Tag = _objHwuwf555400.HWUWF555400Id;

            cobSidePanel.Text = _objHwuwf555400.SidePanel;
            //默认ExNo为1
            cobExNo.Text = _objHwuwf555400.ExNo == 0 ? "2" : _objHwuwf555400.ExNo.ToString();
            cobSuNo.Text = _objHwuwf555400.ExNo == 0 ? "2" : _objHwuwf555400.SuNo.ToString();
            cobLightType.Text = _objHwuwf555400.LightType;
            cobLEDSpotNo.Text = _objHwuwf555400.LEDSpotNo.ToString();
            cobANSUL.Text = _objHwuwf555400.ANSUL;
            cobANSide.Text = _objHwuwf555400.ANSide;
            cobANDetectorEnd.Text = _objHwuwf555400.ANDetectorEnd;
            cobANDropNo.Text = _objHwuwf555400.ANDropNo.ToString();
            cobANDetectorNo.Text = _objHwuwf555400.ANDetectorNo.ToString();
            cobMARVEL.Text = _objHwuwf555400.MARVEL;
            cobIRNo.Text = _objHwuwf555400.IRNo.ToString();
            cobUVType.Text = _objHwuwf555400.UVType;
            cobBluetooth.Text = _objHwuwf555400.Bluetooth;
            cobLEDLogo.Text = _objHwuwf555400.LEDlogo;
            cobOutlet.Text = _objHwuwf555400.Outlet;
            cobInlet.Text = _objHwuwf555400.Inlet;
            cobWaterCollection.Text = _objHwuwf555400.WaterCollection;
            cobBackToBack.Text = _objHwuwf555400.BackToBack;

            txtLength.Text = _objHwuwf555400.Length.ToString();
            txtDeepth.Text = _objHwuwf555400.Deepth.ToString();
            txtExRightDis.Text = _objHwuwf555400.ExRightDis.ToString();
            txtExDis.Text = _objHwuwf555400.ExDis.ToString();
            txtSuDis.Text = _objHwuwf555400.SuDis.ToString();
            txtExLength.Text = _objHwuwf555400.ExLength.ToString();
            txtExWidth.Text = _objHwuwf555400.ExWidth.ToString();
            txtExHeight.Text = _objHwuwf555400.ExHeight.ToString();
            txtLightYDis.Text = _objHwuwf555400.LightYDis.ToString();
            //LEDSpotDis默认400
            txtLEDSpotDis.Text = _objHwuwf555400.LEDSpotDis == 0 ? "400" : _objHwuwf555400.LEDSpotDis.ToString();
            txtANYDis.Text = _objHwuwf555400.ANYDis.ToString();
            txtANDropDis1.Text = _objHwuwf555400.ANDropDis1.ToString();
            txtANDropDis2.Text = _objHwuwf555400.ANDropDis2.ToString();
            txtANDropDis3.Text = _objHwuwf555400.ANDropDis3.ToString();
            txtANDropDis4.Text = _objHwuwf555400.ANDropDis4.ToString();
            txtANDropDis5.Text = _objHwuwf555400.ANDropDis5.ToString();
            txtANDetectorDis1.Text = _objHwuwf555400.ANDetectorDis1.ToString();
            txtANDetectorDis2.Text = _objHwuwf555400.ANDetectorDis2.ToString();
            txtANDetectorDis3.Text = _objHwuwf555400.ANDetectorDis3.ToString();
            txtANDetectorDis4.Text = _objHwuwf555400.ANDetectorDis4.ToString();
            txtANDetectorDis5.Text = _objHwuwf555400.ANDetectorDis5.ToString();
            txtIRDis1.Text = _objHwuwf555400.IRDis1.ToString();
            txtIRDis2.Text = _objHwuwf555400.IRDis2.ToString();
            txtIRDis3.Text = _objHwuwf555400.IRDis3.ToString();
        }
        /// <summary>
        /// 修改参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDouble(txtLength.Text.Trim()) || Convert.ToDouble(txtLength.Text.Trim()) < 500d)
            {
                MessageBox.Show("请认真检查烟罩长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtDeepth.Text.Trim()) || Convert.ToDouble(txtDeepth.Text.Trim()) < 500d)
            {
                MessageBox.Show("请认真检查烟罩深度", "提示信息");
                txtDeepth.Focus();
                txtDeepth.SelectAll();
                return;
            }
            if (cobSidePanel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择烟罩大侧板", "提示信息");
                cobSidePanel.Focus();
                return;
            }
            if (cobExNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择排风脖颈数量", "提示信息");
                cobExNo.Focus();
                return;
            }
            else if (cobExNo.SelectedIndex > 0 && (!DataValidate.IsDouble(txtExDis.Text.Trim()) || Convert.ToDouble(txtExDis.Text.Trim()) < 40d))
            {
                MessageBox.Show("请认真检查排风脖颈间距", "提示信息");//当脖颈大于2时需要填写脖颈间距
                txtExDis.Focus();
                txtExDis.SelectAll();
                return;
            }
            if (cobSuNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择新风脖颈数量", "提示信息");
                cobSuNo.Focus();
                return;
            }
            else if (cobSuNo.SelectedIndex > 0 && (!DataValidate.IsDouble(txtSuDis.Text.Trim()) || Convert.ToDouble(txtSuDis.Text.Trim()) < 250d))
            {
                MessageBox.Show("请认真检查新风脖颈间距", "提示信息");//当脖颈大于2时需要填写脖颈间距
                txtSuDis.Focus();
                txtSuDis.SelectAll();
                return;
            }
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
            if (cobLightType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择灯具类型", "提示信息");
                cobLightType.Focus();
                return;
            }

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
            if (!DataValidate.IsDouble(txtLightYDis.Text.Trim()) || Convert.ToDouble(txtLightYDis.Text.Trim()) < 360d)
            {
                MessageBox.Show("请正确填写灯具中心距离前端面的距离", "提示信息");
                txtLightYDis.Focus();
                txtLightYDis.SelectAll();
                return;
            }
            if (cobLightType.SelectedIndex == 2 || cobLightType.SelectedIndex == 3)
            {
                if (cobLEDSpotNo.SelectedIndex == -1)
                {
                    MessageBox.Show("请检查筒灯数量", "提示信息");
                    cobLEDSpotNo.Focus();
                    return;
                }
                if (!DataValidate.IsDouble(txtLEDSpotDis.Text.Trim()) || Convert.ToDouble(txtLEDSpotDis.Text.Trim()) < 60d)
                {
                    MessageBox.Show("请填写筒灯间距", "提示信息");
                    txtLEDSpotDis.Focus();
                    txtLEDSpotDis.SelectAll();
                    return;
                }
            }
            if (cobANSUL.SelectedIndex == 0)
            {
                if (cobANSide.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择ANSUL侧喷位置", "提示信息");
                    cobANSide.Focus();
                    return;
                }
                if (cobANDetectorEnd.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择ANSUL末端探测器位置", "提示信息");
                    cobANDetectorEnd.Focus();
                    return;
                }
                if (cobANDropNo.SelectedIndex == -1)
                {
                    MessageBox.Show("请检查下喷数量", "提示信息");
                    cobANDropNo.Focus();
                    return;
                }
                if (cobANDropNo.SelectedIndex > 0)
                {
                    if (!DataValidate.IsDouble(txtANYDis.Text.Trim()) || Convert.ToDouble(txtANYDis.Text.Trim()) < 200d)
                    {
                        MessageBox.Show("请检查ANSUL下喷距离烟罩前端距离", "提示信息");
                        txtANYDis.Focus();
                        txtANYDis.SelectAll();
                        return;
                    }
                    if (!DataValidate.IsDouble(txtANDropDis1.Text.Trim()) || Convert.ToDouble(txtANDropDis1.Text.Trim()) < 30d)
                    {
                        MessageBox.Show("请检查下喷间距1", "提示信息");
                        txtANDropDis1.Focus();
                        txtANDropDis1.SelectAll();
                        return;
                    }
                }
                if (cobANDropNo.SelectedIndex > 1)
                {
                    if (!DataValidate.IsDouble(txtANDropDis2.Text.Trim()) || Convert.ToDouble(txtANDropDis2.Text.Trim()) < 30d)
                    {
                        MessageBox.Show("请检查下喷间距2", "提示信息");
                        txtANDropDis2.Focus();
                        txtANDropDis2.SelectAll();
                        return;
                    }
                }
                if (cobANDropNo.SelectedIndex > 2)
                {
                    if (!DataValidate.IsDouble(txtANDropDis3.Text.Trim()) || Convert.ToDouble(txtANDropDis3.Text.Trim()) < 30d)
                    {
                        MessageBox.Show("请检查下喷间距3", "提示信息");
                        txtANDropDis3.Focus();
                        txtANDropDis3.SelectAll();
                        return;
                    }
                }
                if (cobANDropNo.SelectedIndex > 3)
                {
                    if (!DataValidate.IsDouble(txtANDropDis4.Text.Trim()) || Convert.ToDouble(txtANDropDis4.Text.Trim()) < 30d)
                    {
                        MessageBox.Show("请检查下喷间距4", "提示信息");
                        txtANDropDis4.Focus();
                        txtANDropDis4.SelectAll();
                        return;
                    }
                }
                if (cobANDropNo.SelectedIndex > 4)
                {
                    if (!DataValidate.IsDouble(txtANDropDis5.Text.Trim()) || Convert.ToDouble(txtANDropDis5.Text.Trim()) < 30d)
                    {
                        MessageBox.Show("请检查下喷间距5", "提示信息");
                        txtANDropDis5.Focus();
                        txtANDropDis5.SelectAll();
                        return;
                    }
                }
            }
            if (cobANDetectorNo.SelectedIndex == -1)
            {
                MessageBox.Show("请检查探测器数量", "提示信息");
                cobANDetectorNo.Focus();
                return;
            }
            if (cobANDetectorNo.SelectedIndex > 0)
            {
                if (!DataValidate.IsDouble(txtANDetectorDis1.Text.Trim()) || Convert.ToDouble(txtANDetectorDis1.Text.Trim()) < 30d)
                {
                    MessageBox.Show("请检查探测器间距1", "提示信息");
                    txtANDetectorDis1.Focus();
                    txtANDetectorDis1.SelectAll();
                    return;
                }
            }
            if (cobANDetectorNo.SelectedIndex > 1)
            {
                if (!DataValidate.IsDouble(txtANDetectorDis2.Text.Trim()) || Convert.ToDouble(txtANDetectorDis2.Text.Trim()) < 30d)
                {
                    MessageBox.Show("请检查探测器间距2", "提示信息");
                    txtANDetectorDis2.Focus();
                    txtANDetectorDis2.SelectAll();
                    return;
                }
            }
            if (cobANDetectorNo.SelectedIndex > 2)
            {
                if (!DataValidate.IsDouble(txtANDetectorDis3.Text.Trim()) || Convert.ToDouble(txtANDetectorDis3.Text.Trim()) < 30d)
                {
                    MessageBox.Show("请检查探测器间距3", "提示信息");
                    txtANDetectorDis3.Focus();
                    txtANDetectorDis3.SelectAll();
                    return;
                }
            }
            if (cobANDetectorNo.SelectedIndex > 3)
            {
                if (!DataValidate.IsDouble(txtANDetectorDis4.Text.Trim()) || Convert.ToDouble(txtANDetectorDis4.Text.Trim()) < 30d)
                {
                    MessageBox.Show("请检查探测器间距4", "提示信息");
                    txtANDetectorDis4.Focus();
                    txtANDetectorDis4.SelectAll();
                    return;
                }
            }
            if (cobANDetectorNo.SelectedIndex > 4)
            {
                if (!DataValidate.IsDouble(txtANDetectorDis5.Text.Trim()) || Convert.ToDouble(txtANDetectorDis5.Text.Trim()) < 30d)
                {
                    MessageBox.Show("请检查探测器间距5", "提示信息");
                    txtANDetectorDis5.Focus();
                    txtANDetectorDis5.SelectAll();
                    return;
                }
            }
            if (cobMARVEL.SelectedIndex == 0)
            {
                if (cobIRNo.SelectedIndex == -1)
                {
                    MessageBox.Show("请检查IR数量", "提示信息");
                    cobIRNo.Focus();
                    return;
                }
                if (cobIRNo.SelectedIndex > 0)
                {
                    if (!DataValidate.IsDouble(txtIRDis1.Text.Trim()) || Convert.ToDouble(txtIRDis1.Text.Trim()) < 30d)
                    {
                        MessageBox.Show("请检查IR间距1", "提示信息");
                        txtIRDis1.Focus();
                        txtIRDis1.SelectAll();
                        return;
                    }
                }
                if (cobIRNo.SelectedIndex > 1)
                {
                    if (!DataValidate.IsDouble(txtIRDis2.Text.Trim()) || Convert.ToDouble(txtIRDis2.Text.Trim()) < 30d)
                    {
                        MessageBox.Show("请检查IR间距2", "提示信息");
                        txtIRDis2.Focus();
                        txtIRDis2.SelectAll();
                        return;
                    }
                }
                if (cobIRNo.SelectedIndex > 2)
                {
                    if (!DataValidate.IsDouble(txtIRDis3.Text.Trim()) || Convert.ToDouble(txtIRDis3.Text.Trim()) < 30d)
                    {
                        MessageBox.Show("请检查IR间距3", "提示信息");
                        txtIRDis3.Focus();
                        txtIRDis3.SelectAll();
                        return;
                    }
                }
            }
            if (cobUVType.SelectedIndex == -1)
            {
                MessageBox.Show("请检查UV灯类型", "提示信息");
                cobUVType.Focus();
                return;
            }
            if (cobBluetooth.SelectedIndex == -1)
            {
                MessageBox.Show("请检查是否内置蓝牙", "提示信息");
                cobBluetooth.Focus();
                return;
            }
            if (cobLEDLogo.SelectedIndex == -1)
            {
                MessageBox.Show("请检查是否带LOGO", "提示信息");
                cobLEDLogo.Focus();
                return;
            }
            if (cobOutlet.SelectedIndex == -1)
            {
                MessageBox.Show("请检查排水口位置", "提示信息");
                cobOutlet.Focus();
                return;
            }
            if (cobInlet.SelectedIndex == -1)
            {
                MessageBox.Show("请检查入水口位置", "提示信息");
                cobInlet.Focus();
                return;
            }
            if (cobWaterCollection.SelectedIndex == -1)
            {
                MessageBox.Show("请检查是否带集水翻遍", "提示信息");
                cobWaterCollection.Focus();
                return;
            }
            if (cobBackToBack.SelectedIndex == -1)
            {
                MessageBox.Show("请检查是否背靠背", "提示信息");
                cobBackToBack.Focus();
                return;
            }


            #endregion
            //封装对象
            HWUWF555400 objHwuwf555400 = new HWUWF555400()
            {
                HWUWF555400Id = Convert.ToInt32(modelView.Tag),
                SidePanel = cobSidePanel.Text,
                ExNo = Convert.ToInt32(cobExNo.Text),
                SuNo = Convert.ToInt32(cobSuNo.Text),
                LightType = cobLightType.Text,
                LEDSpotNo = cobLEDSpotNo.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(cobLEDSpotNo.Text),
                ANSUL = cobANSUL.Text,
                ANSide = cobANSide.Text.Trim().Length == 0 ? "NO" : cobANSide.Text,
                ANDetectorEnd = cobANDetectorEnd.Text.Trim().Length == 0 ? "NO" : cobANDetectorEnd.Text,
                ANDropNo = cobANDropNo.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(cobANDropNo.Text),
                ANDetectorNo = cobANDetectorNo.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(cobANDetectorNo.Text),
                MARVEL = cobMARVEL.Text,
                IRNo = cobIRNo.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(cobIRNo.Text),
                UVType = cobUVType.Text,
                Bluetooth = cobBluetooth.Text,
                LEDlogo = cobLEDLogo.Text,
                Outlet = cobOutlet.Text,
                Inlet = cobInlet.Text,
                WaterCollection = cobWaterCollection.Text,
                BackToBack = cobBackToBack.Text,

                Length = Convert.ToDouble(txtLength.Text.Trim()),
                Deepth = Convert.ToDouble(txtDeepth.Text.Trim()),
                ExRightDis = Convert.ToDouble(txtExRightDis.Text.Trim()),
                ExDis = Convert.ToDouble(txtExDis.Text.Trim()),
                SuDis = Convert.ToDouble(txtSuDis.Text.Trim()),
                ExLength = Convert.ToDouble(txtExLength.Text.Trim()),
                ExWidth = Convert.ToDouble(txtExWidth.Text.Trim()),
                ExHeight = Convert.ToDouble(txtExHeight.Text.Trim()),
                LightYDis = Convert.ToDouble(txtLightYDis.Text.Trim()),
                LEDSpotDis = Convert.ToDouble(txtLEDSpotDis.Text.Trim()),
                ANYDis = Convert.ToDouble(txtANYDis.Text.Trim()),
                ANDropDis1 = Convert.ToDouble(txtANDropDis1.Text.Trim()),
                ANDropDis2 = Convert.ToDouble(txtANDropDis2.Text.Trim()),
                ANDropDis3 = Convert.ToDouble(txtANDropDis3.Text.Trim()),
                ANDropDis4 = Convert.ToDouble(txtANDropDis4.Text.Trim()),
                ANDropDis5 = Convert.ToDouble(txtANDropDis5.Text.Trim()),
                ANDetectorDis1 = Convert.ToDouble(txtANDetectorDis1.Text.Trim()),
                ANDetectorDis2 = Convert.ToDouble(txtANDetectorDis2.Text.Trim()),
                ANDetectorDis3 = Convert.ToDouble(txtANDetectorDis3.Text.Trim()),
                ANDetectorDis4 = Convert.ToDouble(txtANDetectorDis4.Text.Trim()),
                ANDetectorDis5 = Convert.ToDouble(txtANDetectorDis5.Text.Trim()),
                IRDis1 = Convert.ToDouble(txtIRDis1.Text.Trim()),
                IRDis2 = Convert.ToDouble(txtIRDis2.Text.Trim()),
                IRDis3 = Convert.ToDouble(txtIRDis3.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objHwuwf555400Service.EditModel(objHwuwf555400) == 1)
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
        /// <summary>
        /// 筒灯分组显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobLightType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobLightType.SelectedIndex == 2 || cobLightType.SelectedIndex == 3) grbLEDSpot.Visible = true;
            else grbLEDSpot.Visible = false;
        }
        /// <summary>
        /// ANSUL分组显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobANSUL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobANSUL.SelectedIndex == 0) grbANSUL.Visible = true;
            else grbANSUL.Visible = false;
        }
        /// <summary>
        /// MARVEL分组显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobMARVEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobMARVEL.SelectedIndex == 0) grbMARVEL.Visible = true;
            else grbMARVEL.Visible = false;
        }
        /// <summary>
        /// 填写烟罩长度时脖颈距离中心距离自动改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLength_TextChanged(object sender, EventArgs e)
        {
            if (!DataValidate.IsDouble(txtLength.Text.Trim()) || txtLength.Text.Trim().Length == 0) return;
            txtExRightDis.Text = (Convert.ToDecimal(txtLength.Text.Trim()) / 2m).ToString();
        }

        /// <summary>
        /// 填写深度信息，同步更新灯具距离前端距离
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDeepth_TextChanged(object sender, EventArgs e)
        {
            if (!DataValidate.IsDouble(txtDeepth.Text.Trim()) || txtDeepth.Text.Trim().Length == 0) return;
            txtLightYDis.Text = (360m+(Convert.ToDecimal(txtDeepth.Text.Trim())-534m-360m) / 2m).ToString();
        }

        /// <summary>
        /// 动态选择下喷距离
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobANDropNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobANDropNo.SelectedIndex == 0)
            {
                lblANYDis.Visible = false;
                lblANDropDis1.Visible = false;
                lblANDropDis2.Visible = false;
                lblANDropDis3.Visible = false;
                lblANDropDis4.Visible = false;
                lblANDropDis5.Visible = false;
                txtANYDis.Visible = false;
                txtANDropDis1.Visible = false;
                txtANDropDis2.Visible = false;
                txtANDropDis3.Visible = false;
                txtANDropDis4.Visible = false;
                txtANDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 1)
            {
                lblANYDis.Visible = true;
                lblANDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtANDropDis1.Visible = true;
                lblANDropDis2.Visible = false;
                lblANDropDis3.Visible = false;
                lblANDropDis4.Visible = false;
                lblANDropDis5.Visible = false;
                txtANDropDis2.Visible = false;
                txtANDropDis3.Visible = false;
                txtANDropDis4.Visible = false;
                txtANDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 2)
            {
                lblANYDis.Visible = true;
                lblANDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtANDropDis1.Visible = true;
                lblANDropDis2.Visible = true;
                txtANDropDis2.Visible = true;
                lblANDropDis3.Visible = false;
                lblANDropDis4.Visible = false;
                lblANDropDis5.Visible = false;
                txtANDropDis3.Visible = false;
                txtANDropDis4.Visible = false;
                txtANDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 3)
            {
                lblANYDis.Visible = true;
                lblANDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtANDropDis1.Visible = true;
                lblANDropDis2.Visible = true;
                txtANDropDis2.Visible = true;
                lblANDropDis3.Visible = true;
                txtANDropDis3.Visible = true;
                lblANDropDis4.Visible = false;
                lblANDropDis5.Visible = false;
                txtANDropDis4.Visible = false;
                txtANDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 4)
            {
                lblANYDis.Visible = true;
                lblANDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtANDropDis1.Visible = true;
                lblANDropDis2.Visible = true;
                txtANDropDis2.Visible = true;
                lblANDropDis3.Visible = true;
                txtANDropDis3.Visible = true;
                lblANDropDis4.Visible = true;
                txtANDropDis4.Visible = true;
                lblANDropDis5.Visible = false;
                txtANDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 5)
            {
                lblANYDis.Visible = true;
                lblANDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtANDropDis1.Visible = true;
                lblANDropDis2.Visible = true;
                txtANDropDis2.Visible = true;
                lblANDropDis3.Visible = true;
                txtANDropDis3.Visible = true;
                lblANDropDis4.Visible = true;
                txtANDropDis4.Visible = true;
                lblANDropDis5.Visible = true;
                txtANDropDis5.Visible = true;
            }
        }
        /// <summary>
        /// 动态选择排风脖颈数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobExNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobExNo.SelectedIndex > 0)
            {
                lblExDis.Visible = true;
                txtExDis.Visible = true;
            }
            else
            {
                lblExDis.Visible = false;
                txtExDis.Visible = false;
            }
        }
        /// <summary>
        /// 动态选择新风脖颈数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobSuNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobSuNo.SelectedIndex > 0)
            {
                lblSuDis.Visible = true;
                txtSuDis.Visible = true;
            }
            else
            {
                lblSuDis.Visible = false;
                txtSuDis.Visible = false;
            }
        }
        /// <summary>
        /// 动态选择IR数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobIRNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobIRNo.SelectedIndex == 0)
            {
                lblIRDis1.Visible = false;
                lblIRDis2.Visible = false;
                lblIRDis3.Visible = false;
                txtIRDis1.Visible = false;
                txtIRDis2.Visible = false;
                txtIRDis3.Visible = false;
            }
            else if (cobIRNo.SelectedIndex == 1)
            {
                lblIRDis1.Visible = true;
                txtIRDis1.Visible = true;
                lblIRDis2.Visible = false;
                lblIRDis3.Visible = false;
                txtIRDis2.Visible = false;
                txtIRDis3.Visible = false;
            }
            else if (cobIRNo.SelectedIndex == 2)
            {
                lblIRDis1.Visible = true;
                txtIRDis1.Visible = true;
                lblIRDis2.Visible = true;
                txtIRDis2.Visible = true;
                lblIRDis3.Visible = false;
                txtIRDis3.Visible = false;
            }
            else if (cobIRNo.SelectedIndex == 3)
            {
                lblIRDis1.Visible = true;
                txtIRDis1.Visible = true;
                lblIRDis2.Visible = true;
                txtIRDis2.Visible = true;
                lblIRDis3.Visible = true;
                txtIRDis3.Visible = true;
            }
        }
        /// <summary>
        /// 探测器数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobDetectorNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobANDetectorNo.SelectedIndex == 0)
            {
                lblANDetectorDis1.Visible = false;
                lblANDetectorDis2.Visible = false;
                lblANDetectorDis3.Visible = false;
                lblANDetectorDis4.Visible = false;
                lblANDetectorDis5.Visible = false;
                txtANDetectorDis1.Visible = false;
                txtANDetectorDis2.Visible = false;
                txtANDetectorDis3.Visible = false;
                txtANDetectorDis4.Visible = false;
                txtANDetectorDis5.Visible = false;
            }
            else if (cobANDetectorNo.SelectedIndex == 1)
            {
                lblANDetectorDis1.Visible = true;
                txtANDetectorDis1.Visible = true;
                lblANDetectorDis2.Visible = false;
                lblANDetectorDis3.Visible = false;
                lblANDetectorDis4.Visible = false;
                lblANDetectorDis5.Visible = false;
                txtANDetectorDis2.Visible = false;
                txtANDetectorDis3.Visible = false;
                txtANDetectorDis4.Visible = false;
                txtANDetectorDis5.Visible = false;
            }
            else if (cobANDetectorNo.SelectedIndex == 2)
            {
                lblANDetectorDis1.Visible = true;
                txtANDetectorDis1.Visible = true;
                lblANDetectorDis2.Visible = true;
                txtANDetectorDis2.Visible = true;
                lblANDetectorDis3.Visible = false;
                lblANDetectorDis4.Visible = false;
                lblANDetectorDis5.Visible = false;
                txtANDetectorDis3.Visible = false;
                txtANDetectorDis4.Visible = false;
                txtANDetectorDis5.Visible = false;
            }
            else if (cobANDetectorNo.SelectedIndex == 3)
            {
                lblANDetectorDis1.Visible = true;
                txtANDetectorDis1.Visible = true;
                lblANDetectorDis2.Visible = true;
                txtANDetectorDis2.Visible = true;
                lblANDetectorDis3.Visible = true;
                txtANDetectorDis3.Visible = true;
                lblANDetectorDis4.Visible = false;
                lblANDetectorDis5.Visible = false;
                txtANDetectorDis4.Visible = false;
                txtANDetectorDis5.Visible = false;
            }
            else if (cobANDetectorNo.SelectedIndex == 4)
            {
                lblANDetectorDis1.Visible = true;
                txtANDetectorDis1.Visible = true;
                lblANDetectorDis2.Visible = true;
                txtANDetectorDis2.Visible = true;
                lblANDetectorDis3.Visible = true;
                txtANDetectorDis3.Visible = true;
                lblANDetectorDis4.Visible = true;
                txtANDetectorDis4.Visible = true;
                lblANDetectorDis5.Visible = false;
                txtANDetectorDis5.Visible = false;
            }
            else if (cobANDetectorNo.SelectedIndex == 5)
            {

                lblANDetectorDis1.Visible = true;
                txtANDetectorDis1.Visible = true;
                lblANDetectorDis2.Visible = true;
                txtANDetectorDis2.Visible = true;
                lblANDetectorDis3.Visible = true;
                txtANDetectorDis3.Visible = true;
                lblANDetectorDis4.Visible = true;
                txtANDetectorDis4.Visible = true;
                lblANDetectorDis5.Visible = true;
                txtANDetectorDis5.Visible = true;
            }
        }
        
    }
}
