﻿using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmKvf555400 : MetroFramework.Forms.MetroForm
    {
        readonly KVF555400Service _objKvf555400Service = new KVF555400Service();
        private readonly KVF555400 _objKvf555400 = null;
        public FrmKvf555400()
        {
            InitializeComponent();
            SetVisibleFalse();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmKvf555400(Drawing drawing, ModuleTree tree) : this()
        {
            _objKvf555400 = (KVF555400)_objKvf555400Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objKvf555400 == null) return;
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
            //ANSUL探测器
            cobANDetector.Items.Add("LEFT");
            cobANDetector.Items.Add("RIGHT");
            cobANDetector.Items.Add("BOTH");
            cobANDetector.Items.Add("NO");
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
            //LOGO
            cobLEDLogo.Items.Add("YES");
            cobLEDLogo.Items.Add("NO");
            //油塞
            cobOutlet.Items.Add("LEFTTAP");
            cobOutlet.Items.Add("RIGHTTAP");
            cobOutlet.Items.Add("VESSEL");//油盒
            cobOutlet.SelectedIndex = 1;
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
            grbMARVEL.Visible = false;
        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objKvf555400 == null) return;
            modelView.Tag = _objKvf555400.KVF555400Id;

            cobSidePanel.Text = _objKvf555400.SidePanel;
            //默认ExNo为1
            cobExNo.Text = _objKvf555400.ExNo == 0 ? "1" : _objKvf555400.ExNo.ToString();
            cobSuNo.Text = _objKvf555400.ExNo == 0 ? "2" : _objKvf555400.SuNo.ToString();
            cobLightType.Text = _objKvf555400.LightType;
            cobLEDSpotNo.Text = _objKvf555400.LEDSpotNo.ToString();
            cobANSUL.Text = _objKvf555400.ANSUL;
            cobANSide.Text = _objKvf555400.ANSide;
            cobANDetector.Text = _objKvf555400.ANDetector;
            cobANDropNo.Text = _objKvf555400.ANDropNo.ToString();
            cobMARVEL.Text = _objKvf555400.MARVEL;
            cobIRNo.Text = _objKvf555400.IRNo.ToString();
            cobLEDLogo.Text = _objKvf555400.LEDlogo;
            cobOutlet.Text = _objKvf555400.Outlet;
            cobWaterCollection.Text = _objKvf555400.WaterCollection;
            cobBackToBack.Text = _objKvf555400.BackToBack;

            txtLength.Text = _objKvf555400.Length.ToString();
            txtDeepth.Text = _objKvf555400.Deepth.ToString();
            txtExRightDis.Text = _objKvf555400.ExRightDis.ToString();
            txtExDis.Text = _objKvf555400.ExDis.ToString();
            txtSuDis.Text = _objKvf555400.SuDis.ToString();
            txtExLength.Text = _objKvf555400.ExLength.ToString();
            txtExWidth.Text = _objKvf555400.ExWidth.ToString();
            txtExHeight.Text = _objKvf555400.ExHeight.ToString();
            //LEDSpotDis默认400
            txtLEDSpotDis.Text = _objKvf555400.LEDSpotDis == 0 ? "400" : _objKvf555400.LEDSpotDis.ToString();
            txtANYDis.Text = _objKvf555400.ANYDis.ToString();
            txtDropDis1.Text = _objKvf555400.ANDropDis1.ToString();
            txtDropDis2.Text = _objKvf555400.ANDropDis2.ToString();
            txtDropDis3.Text = _objKvf555400.ANDropDis3.ToString();
            txtDropDis4.Text = _objKvf555400.ANDropDis4.ToString();
            txtDropDis5.Text = _objKvf555400.ANDropDis5.ToString();
            txtIRDis1.Text = _objKvf555400.IRDis1.ToString();
            txtIRDis2.Text = _objKvf555400.IRDis2.ToString();
            txtIRDis3.Text = _objKvf555400.IRDis3.ToString();
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
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 500m)
            {
                MessageBox.Show("请认真检查烟罩长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtDeepth.Text.Trim()) || Convert.ToDecimal(txtDeepth.Text.Trim()) < 500m)
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
            else if (cobExNo.SelectedIndex > 0 && (!DataValidate.IsDecimal(txtExDis.Text.Trim()) || Convert.ToDecimal(txtExDis.Text.Trim()) < 40m))
            {
                MessageBox.Show("请认真检查排风脖颈间距", "提示信息");//当脖颈大于2时需要填写脖颈间距
                txtExDis.Focus();
                txtExDis.SelectAll();
                return;
            }

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
            if (cobSuNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择新风脖颈数量", "提示信息");
                cobSuNo.Focus();
                return;
            }
            else if (cobSuNo.SelectedIndex > 0 && (!DataValidate.IsDecimal(txtSuDis.Text.Trim()) || Convert.ToDecimal(txtSuDis.Text.Trim()) < 250m))
            {
                MessageBox.Show("请认真检查新风脖颈间距", "提示信息");//当脖颈大于2时需要填写脖颈间距
                txtSuDis.Focus();
                txtSuDis.SelectAll();
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
                if (!DataValidate.IsDecimal(txtLEDSpotDis.Text.Trim()) || Convert.ToDecimal(txtLEDSpotDis.Text.Trim()) < 60m)
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
                if (cobANDetector.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择ANSUL探测器进出口位置", "提示信息");
                    cobANDetector.Focus();
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
                    if (!DataValidate.IsDecimal(txtANYDis.Text.Trim()) || Convert.ToDecimal(txtANYDis.Text.Trim()) < 200m)
                    {
                        MessageBox.Show("请检查ANSUL下喷距离烟罩前端距离", "提示信息");
                        txtANYDis.Focus();
                        txtANYDis.SelectAll();
                        return;
                    }
                    if (!DataValidate.IsDecimal(txtDropDis1.Text.Trim()) || Convert.ToDecimal(txtDropDis1.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查下喷间距1", "提示信息");
                        txtDropDis1.Focus();
                        txtDropDis1.SelectAll();
                        return;
                    }
                }
                if (cobANDropNo.SelectedIndex > 1)
                {
                    if (!DataValidate.IsDecimal(txtDropDis2.Text.Trim()) || Convert.ToDecimal(txtDropDis2.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查下喷间距2", "提示信息");
                        txtDropDis2.Focus();
                        txtDropDis2.SelectAll();
                        return;
                    }
                }
                if (cobANDropNo.SelectedIndex > 2)
                {
                    if (!DataValidate.IsDecimal(txtDropDis3.Text.Trim()) || Convert.ToDecimal(txtDropDis3.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查下喷间距3", "提示信息");
                        txtDropDis3.Focus();
                        txtDropDis3.SelectAll();
                        return;
                    }
                }
                if (cobANDropNo.SelectedIndex > 3)
                {
                    if (!DataValidate.IsDecimal(txtDropDis4.Text.Trim()) || Convert.ToDecimal(txtDropDis4.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查下喷间距4", "提示信息");
                        txtDropDis4.Focus();
                        txtDropDis4.SelectAll();
                        return;
                    }
                }
                if (cobANDropNo.SelectedIndex > 4)
                {
                    if (!DataValidate.IsDecimal(txtDropDis5.Text.Trim()) || Convert.ToDecimal(txtDropDis5.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查下喷间距5", "提示信息");
                        txtDropDis5.Focus();
                        txtDropDis5.SelectAll();
                        return;
                    }
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
                    if (!DataValidate.IsDecimal(txtIRDis1.Text.Trim()) || Convert.ToDecimal(txtIRDis1.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查IR间距1", "提示信息");
                        txtIRDis1.Focus();
                        txtIRDis1.SelectAll();
                        return;
                    }
                }
                if (cobIRNo.SelectedIndex > 1)
                {
                    if (!DataValidate.IsDecimal(txtIRDis2.Text.Trim()) || Convert.ToDecimal(txtIRDis2.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查IR间距2", "提示信息");
                        txtIRDis2.Focus();
                        txtIRDis2.SelectAll();
                        return;
                    }
                }
                if (cobIRNo.SelectedIndex > 2)
                {
                    if (!DataValidate.IsDecimal(txtIRDis3.Text.Trim()) || Convert.ToDecimal(txtIRDis3.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查IR间距3", "提示信息");
                        txtIRDis3.Focus();
                        txtIRDis3.SelectAll();
                        return;
                    }
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
                MessageBox.Show("请检查油塞位置", "提示信息");
                cobOutlet.Focus();
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
            KVF555400 objKvf555400 = new KVF555400()
            {
                KVF555400Id = Convert.ToInt32(modelView.Tag),
                SidePanel = cobSidePanel.Text,
                ExNo = Convert.ToInt32(cobExNo.Text),
                SuNo = Convert.ToInt32(cobSuNo.Text),
                LightType = cobLightType.Text,
                LEDSpotNo = cobLEDSpotNo.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(cobLEDSpotNo.Text),
                ANSUL = cobANSUL.Text,
                ANSide = cobANSide.Text.Trim().Length == 0 ? "NO" : cobANSide.Text,
                ANDetector = cobANDetector.Text.Trim().Length == 0 ? "NO" : cobANDetector.Text,
                ANDropNo = cobANDropNo.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(cobANDropNo.Text),
                MARVEL = cobMARVEL.Text,
                IRNo = cobIRNo.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(cobIRNo.Text),
                LEDlogo = cobLEDLogo.Text,
                Outlet = cobOutlet.Text,
                WaterCollection = cobWaterCollection.Text,
                BackToBack = cobBackToBack.Text,

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                Deepth = Convert.ToDecimal(txtDeepth.Text.Trim()),
                ExRightDis = Convert.ToDecimal(txtExRightDis.Text.Trim()),
                ExDis = Convert.ToDecimal(txtExDis.Text.Trim()),
                SuDis = Convert.ToDecimal(txtSuDis.Text.Trim()),
                ExLength = Convert.ToDecimal(txtExLength.Text.Trim()),
                ExWidth = Convert.ToDecimal(txtExWidth.Text.Trim()),
                ExHeight = Convert.ToDecimal(txtExHeight.Text.Trim()),
                LEDSpotDis = Convert.ToDecimal(txtLEDSpotDis.Text.Trim()),
                ANYDis = Convert.ToDecimal(txtANYDis.Text.Trim()),
                ANDropDis1 = Convert.ToDecimal(txtDropDis1.Text.Trim()),
                ANDropDis2 = Convert.ToDecimal(txtDropDis2.Text.Trim()),
                ANDropDis3 = Convert.ToDecimal(txtDropDis3.Text.Trim()),
                ANDropDis4 = Convert.ToDecimal(txtDropDis4.Text.Trim()),
                ANDropDis5 = Convert.ToDecimal(txtDropDis5.Text.Trim()),
                IRDis1 = Convert.ToDecimal(txtIRDis1.Text.Trim()),
                IRDis2 = Convert.ToDecimal(txtIRDis2.Text.Trim()),
                IRDis3 = Convert.ToDecimal(txtIRDis3.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objKvf555400Service.EditModel(objKvf555400) == 1)
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
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || txtLength.Text.Trim().Length == 0) return;
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
                lblDropDis1.Visible = false;
                lblDropDis2.Visible = false;
                lblDropDis3.Visible = false;
                lblDropDis4.Visible = false;
                lblDropDis5.Visible = false;
                txtANYDis.Visible = false;
                txtDropDis1.Visible = false;
                txtDropDis2.Visible = false;
                txtDropDis3.Visible = false;
                txtDropDis4.Visible = false;
                txtDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 1)
            {
                lblANYDis.Visible = true;
                lblDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtDropDis1.Visible = true;
                lblDropDis2.Visible = false;
                lblDropDis3.Visible = false;
                lblDropDis4.Visible = false;
                lblDropDis5.Visible = false;
                txtDropDis2.Visible = false;
                txtDropDis3.Visible = false;
                txtDropDis4.Visible = false;
                txtDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 2)
            {
                lblANYDis.Visible = true;
                lblDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtDropDis1.Visible = true;
                lblDropDis2.Visible = true;
                txtDropDis2.Visible = true;
                lblDropDis3.Visible = false;
                lblDropDis4.Visible = false;
                lblDropDis5.Visible = false;
                txtDropDis3.Visible = false;
                txtDropDis4.Visible = false;
                txtDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 3)
            {
                lblANYDis.Visible = true;
                lblDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtDropDis1.Visible = true;
                lblDropDis2.Visible = true;
                txtDropDis2.Visible = true;
                lblDropDis3.Visible = true;
                txtDropDis3.Visible = true;
                lblDropDis4.Visible = false;
                lblDropDis5.Visible = false;
                txtDropDis4.Visible = false;
                txtDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 4)
            {
                lblANYDis.Visible = true;
                lblDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtDropDis1.Visible = true;
                lblDropDis2.Visible = true;
                txtDropDis2.Visible = true;
                lblDropDis3.Visible = true;
                txtDropDis3.Visible = true;
                lblDropDis4.Visible = true;
                txtDropDis4.Visible = true;
                lblDropDis5.Visible = false;
                txtDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 5)
            {
                lblANYDis.Visible = true;
                lblDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtDropDis1.Visible = true;
                lblDropDis2.Visible = true;
                txtDropDis2.Visible = true;
                lblDropDis3.Visible = true;
                txtDropDis3.Visible = true;
                lblDropDis4.Visible = true;
                txtDropDis4.Visible = true;
                lblDropDis5.Visible = true;
                txtDropDis5.Visible = true;
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
    }
}
