﻿using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmNocj340 : MetroFramework.Forms.MetroForm
    {
        readonly NOCJ340Service _objNocj340Service = new NOCJ340Service();
        private readonly NOCJ340 _objNocj340 = null;
        public FrmNocj340()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmNocj340(Drawing drawing, ModuleTree tree) : this()
        {
            _objNocj340 = (NOCJ340)_objNocj340Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objNocj340 == null) return;
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
            cobSidePanel.Items.Add("NOCJBACKL");
            cobSidePanel.Items.Add("NOCJBACKR");
            cobSidePanel.Items.Add("NOCJBACKB");
            //背面CJ腔位置
            cobBackCJSide.Items.Add("LEFT");
            cobBackCJSide.Items.Add("RIGHT");
            cobBackCJSide.Items.Add("NO");
            cobBackCJSide.Items.Add("BOTH");
            //DP腔位置
            cobDPSide.Items.Add("LEFT");
            cobDPSide.Items.Add("RIGHT");
            cobDPSide.Items.Add("NO");
            cobDPSide.Items.Add("BOTH");

            //左排风腔
            cobLeftBeamType.Items.Add("NO");
            //cobLeftBeamType.Items.Add("KCJSB265");
            //cobLeftBeamType.Items.Add("KCJSB290");
            //cobLeftBeamType.Items.Add("KCJSB535");
            //cobLeftBeamType.Items.Add("KCJDB800");
            //cobLeftBeamType.Items.Add("UCJSB385");
            //cobLeftBeamType.Items.Add("UCJSB535");
            //cobLeftBeamType.Items.Add("UCJDB800");
            cobLeftBeamType.Items.Add("KCWSB265");
            cobLeftBeamType.Items.Add("KCWSB535");
            cobLeftBeamType.Items.Add("KCWDB800");
            cobLeftBeamType.Items.Add("UCWSB535");
            cobLeftBeamType.Items.Add("UCWDB800");
            //右排风腔
            cobRightBeamType.Items.Add("NO");
            //cobRightBeamType.Items.Add("KCJSB265");
            //cobRightBeamType.Items.Add("KCJSB290");
            //cobRightBeamType.Items.Add("KCJSB535");
            //cobRightBeamType.Items.Add("KCJDB800");
            //cobRightBeamType.Items.Add("UCJSB385");
            //cobRightBeamType.Items.Add("UCJSB535");
            //cobRightBeamType.Items.Add("UCJDB800");
            cobRightBeamType.Items.Add("KCWSB265");
            cobRightBeamType.Items.Add("KCWSB535");
            cobRightBeamType.Items.Add("KCWDB800");
            cobRightBeamType.Items.Add("UCWSB535");
            cobRightBeamType.Items.Add("UCWDB800");
            //LK270位置
            cobLKSide.Items.Add("LEFT");
            cobLKSide.Items.Add("RIGHT");
            cobLKSide.Items.Add("NO");
            cobLKSide.Items.Add("BOTH");
            cobLKSide.SelectedIndex = 2;
            //GUTTER位置
            cobGutterSide.Items.Add("LEFT");
            cobGutterSide.Items.Add("RIGHT");
            cobGutterSide.Items.Add("NO");
            cobGutterSide.Items.Add("BOTH");
            cobGutterSide.SelectedIndex = 2;

            lblLeftBeamDis.Visible = false;
            txtLeftBeamDis.Visible = false;
            lblRightBeamDis.Visible = false;
            txtRightBeamDis.Visible = false;
        }

        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objNocj340 == null) return;
            modelView.Tag = _objNocj340.NOCJ340Id;

            cobSidePanel.Text = _objNocj340.SidePanel;

            cobBackCJSide.Text = _objNocj340.BackCJSide;
            cobDPSide.Text = _objNocj340.DPSide;
            cobLeftBeamType.Text = _objNocj340.LeftBeamType;
            cobRightBeamType.Text = _objNocj340.RightBeamType;
            cobLKSide.Text = _objNocj340.LKSide;
            cobGutterSide.Text = _objNocj340.GutterSide;

            txtLength.Text = _objNocj340.Length.ToString();
            txtWidth.Text = _objNocj340.Width.ToString();
            txtLeftDis.Text = _objNocj340.LeftDis.ToString();
            txtRightDis.Text = _objNocj340.RightDis.ToString();
            txtLeftBeamDis.Text = _objNocj340.LeftBeamDis.ToString();
            txtRightBeamDis.Text = _objNocj340.RightBeamDis.ToString();
            txtGutterWidth.Text = _objNocj340.GutterWidth.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDouble(txtLength.Text.Trim()) || Convert.ToDouble(txtLength.Text.Trim()) < 90d)
            {
                MessageBox.Show("请认真检查CJ腔长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtWidth.Text.Trim()) || Convert.ToDouble(txtWidth.Text.Trim()) < 30d)
            {
                MessageBox.Show("请认真检查CJ腔宽度", "提示信息");
                txtWidth.Focus();
                txtWidth.SelectAll();
                return;
            }
            if (cobSidePanel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择CJ腔侧板", "提示信息");
                cobSidePanel.Focus();
                return;
            }

            if (cobBackCJSide.SelectedIndex == -1)
            {
                MessageBox.Show("请选择BCJ位置，如果没有请选择NO", "提示信息");
                cobBackCJSide.Focus();
                return;
            }
            if (cobDPSide.SelectedIndex == -1)
            {
                MessageBox.Show("请选择DP位置，如果没有请选择NO", "提示信息");
                cobDPSide.Focus();
                return;
            }
            if (cobLeftBeamType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择左排风腔类型，如果没有请选择NO", "提示信息");
                cobBackCJSide.Focus();
                return;
            }
            if (cobRightBeamType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择右排风腔类型，如果没有请选择NO", "提示信息");
                cobBackCJSide.Focus();
                return;
            }
            if (cobLKSide.SelectedIndex == -1)
            {
                MessageBox.Show("请选择LK270位置", "提示信息");
                cobLKSide.Focus();
                return;
            }

            if (cobGutterSide.SelectedIndex == -1)
            {
                MessageBox.Show("请选择ANSUL腔的位置", "提示信息");
                cobGutterSide.Focus();
                return;
            }
            if (cobGutterSide.SelectedIndex != 2)
            {
                if (!DataValidate.IsDouble(txtGutterWidth.Text.Trim()) || Convert.ToDouble(txtGutterWidth.Text.Trim()) < 30d)
                {
                    MessageBox.Show("请认真检查ANSUL腔的宽度", "提示信息");
                    txtGutterWidth.Focus();
                    txtGutterWidth.SelectAll();
                    return;
                }
            }
            #endregion
            //封装对象
            NOCJ340 objNocj340 = new NOCJ340()
            {
                NOCJ340Id = Convert.ToInt32(modelView.Tag),
                SidePanel = cobSidePanel.Text,
                BackCJSide = cobBackCJSide.Text,
                DPSide = cobDPSide.Text,
                LeftBeamType = cobLeftBeamType.Text,
                RightBeamType = cobRightBeamType.Text,
                LKSide = cobLKSide.Text,
                GutterSide = cobGutterSide.Text,

                Length = Convert.ToDouble(txtLength.Text.Trim()),
                Width = Convert.ToDouble(txtWidth.Text.Trim()),
                LeftDis = Convert.ToDouble(txtLeftDis.Text.Trim()),
                RightDis = Convert.ToDouble(txtRightDis.Text.Trim()),
                LeftBeamDis = Convert.ToDouble(txtLeftBeamDis.Text.Trim()),
                RightBeamDis = Convert.ToDouble(txtRightBeamDis.Text.Trim()),
                GutterWidth = Convert.ToDouble(txtGutterWidth.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objNocj340Service.EditModel(objNocj340) == 1)
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

        private void cobLeftBeamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobLeftBeamType.SelectedIndex == 3 || cobLeftBeamType.SelectedIndex == 5 )
            {
                lblLeftBeamDis.Visible = true;
                txtLeftBeamDis.Visible = true;
            }
            else
            {
                lblLeftBeamDis.Visible = false;
                txtLeftBeamDis.Visible = false;
            }
        }

        private void cobRightBeamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobRightBeamType.SelectedIndex == 3 || cobRightBeamType.SelectedIndex == 5 )
            {
                lblRightBeamDis.Visible = true;
                txtRightBeamDis.Visible = true;
            }
            else
            {
                lblRightBeamDis.Visible = false;
                txtRightBeamDis.Visible = false;
            }
        }

        private void cobGutterSide_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobGutterSide.SelectedIndex == 2 || cobGutterSide.SelectedIndex == -1)
            {
                lblGutterWidth.Visible = false;
                txtGutterWidth.Visible = false;
            }
            else
            {
                lblGutterWidth.Visible = true;
                txtGutterWidth.Visible = true;
            }
        }
    }
}