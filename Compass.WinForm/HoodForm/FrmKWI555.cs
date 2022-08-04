using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmKwi555 : MetroFramework.Forms.MetroForm
    {
        readonly KWI555Service _objKwi555Service = new KWI555Service();
        private readonly KWI555 _objKwi555 = null;
        private ModelView modelView;
        public FrmKwi555()
        {
            InitializeComponent();
            SetVisibleFalse();
            IniCob();
            modelView = new ModelView();
            panel1.Controls.Add(modelView);
            modelView.Dock = DockStyle.Fill;
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmKwi555(Drawing drawing, ModuleTree tree) : this()
        {
            _objKwi555 =(KWI555) _objKwi555Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objKwi555 == null) return;
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
            
            //烟罩配置信息
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
            cobWaterCollection.SelectedIndex = 1;
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
           
        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objKwi555 == null) return;
            panel1.Tag = _objKwi555.KWI555Id;

            cobSidePanel.Text = _objKwi555.SidePanel;
            //默认ExNo为1
            cobExNo.Text = _objKwi555.ExNo == 0 ? "1" : _objKwi555.ExNo.ToString();
            cobLightType.Text = _objKwi555.LightType;
            cobLEDSpotNo.Text = _objKwi555.LEDSpotNo.ToString();
            cobANSUL.Text = _objKwi555.ANSUL;
            cobANSide.Text = _objKwi555.ANSide;
            cobANDetectorEnd.Text = _objKwi555.ANDetectorEnd;
            cobANDropNo.Text = _objKwi555.ANDropNo.ToString();
            cobANDetectorNo.Text = _objKwi555.ANDetectorNo.ToString();
            cobMARVEL.Text = _objKwi555.MARVEL;
            cobLEDLogo.Text = _objKwi555.LEDlogo;
            cobOutlet.Text = _objKwi555.Outlet;
            cobInlet.Text = _objKwi555.Inlet;
            cobWaterCollection.Text = _objKwi555.WaterCollection;
            cobBackToBack.Text = _objKwi555.BackToBack;

            txtLength.Text = _objKwi555.Length.ToString();
            txtDeepth.Text = _objKwi555.Deepth.ToString();
            txtExRightDis.Text = _objKwi555.ExRightDis.ToString();
            txtExDis.Text = _objKwi555.ExDis.ToString();
            txtExLength.Text = _objKwi555.ExLength.ToString();
            txtExWidth.Text = _objKwi555.ExWidth.ToString();
            txtExHeight.Text = _objKwi555.ExHeight.ToString();
            //LEDSpotDis默认400
            txtLEDSpotDis.Text = _objKwi555.LEDSpotDis == 0 ? "400" : _objKwi555.LEDSpotDis.ToString();
            txtANYDis.Text = _objKwi555.ANYDis.ToString();
            txtANDropDis1.Text = _objKwi555.ANDropDis1.ToString();
            txtANDropDis2.Text = _objKwi555.ANDropDis2.ToString();
            txtANDropDis3.Text = _objKwi555.ANDropDis3.ToString();
            txtANDropDis4.Text = _objKwi555.ANDropDis4.ToString();
            txtANDropDis5.Text = _objKwi555.ANDropDis5.ToString();
            txtANDetectorDis1.Text = _objKwi555.ANDetectorDis1.ToString();
            txtANDetectorDis2.Text = _objKwi555.ANDetectorDis2.ToString();
            txtANDetectorDis3.Text = _objKwi555.ANDetectorDis3.ToString();
            txtANDetectorDis4.Text = _objKwi555.ANDetectorDis4.ToString();
            txtANDetectorDis5.Text = _objKwi555.ANDetectorDis5.ToString();
            
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
            if (panel1.Tag.ToString().Length == 0) return;
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
            KWI555 objKwi555 = new KWI555()
            {
                KWI555Id = Convert.ToInt32(modelView.Tag),
                SidePanel = cobSidePanel.Text,
                ExNo = Convert.ToInt32(cobExNo.Text),
                LightType = cobLightType.Text,
                LEDSpotNo = cobLEDSpotNo.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(cobLEDSpotNo.Text),
                ANSUL = cobANSUL.Text,
                ANSide = cobANSide.Text.Trim().Length == 0 ? "NO" : cobANSide.Text,
                ANDetectorEnd = cobANDetectorEnd.Text.Trim().Length == 0 ? "NO" : cobANDetectorEnd.Text,
                ANDropNo = cobANDropNo.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(cobANDropNo.Text),
                ANDetectorNo = cobANDetectorNo.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(cobANDetectorNo.Text),
                MARVEL = cobMARVEL.Text,
                
                LEDlogo = cobLEDLogo.Text,
                Outlet = cobOutlet.Text,
                Inlet = cobInlet.Text,
                WaterCollection = cobWaterCollection.Text,
                BackToBack = cobBackToBack.Text,

                Length = Convert.ToDouble(txtLength.Text.Trim()),
                Deepth = Convert.ToDouble(txtDeepth.Text.Trim()),
                ExRightDis = Convert.ToDouble(txtExRightDis.Text.Trim()),
                ExDis = Convert.ToDouble(txtExDis.Text.Trim()),
                ExLength = Convert.ToDouble(txtExLength.Text.Trim()),
                ExWidth = Convert.ToDouble(txtExWidth.Text.Trim()),
                ExHeight = Convert.ToDouble(txtExHeight.Text.Trim()),
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
                
            };
            //提交修改
            try
            {
                if (_objKwi555Service.EditModel(objKwi555) == 1)
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
        /// 填写烟罩长度时脖颈距离中心距离自动改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLength_TextChanged(object sender, EventArgs e)
        {
            if (!DataValidate.IsDouble(txtLength.Text.Trim()) || txtLength.Text.Trim().Length == 0) return;
            txtExRightDis.Text = (Convert.ToDecimal(txtLength.Text.Trim()) / 2).ToString();
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
