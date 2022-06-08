﻿using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLfuss : MetroFramework.Forms.MetroForm
    {
        readonly LFUSSService _objLfussService = new LFUSSService();
        private readonly LFUSS _objLfuss = null;
        public FrmLfuss()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLfuss(Drawing drawing, ModuleTree tree) : this()
        {
            _objLfuss = (LFUSS)_objLfussService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objLfuss == null) return;
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

            //均流桶数量
            cobSuNo.Items.Add("1");
            cobSuNo.Items.Add("2");
            cobSuNo.Items.Add("3");
            cobSuNo.Items.Add("4");
            cobSuNo.Items.Add("5");
            //均流桶直径
            cobSuDia.Items.Add("200");
            cobSuDia.Items.Add("250");

            //日本项目
            cobJapan.Items.Add("YES");
            cobJapan.Items.Add("No");
            cobJapan.SelectedIndex = 1;
        }

        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objLfuss == null) return;
            modelView.Tag = _objLfuss.LFUSSId;

            cobSidePanel.Text = _objLfuss.SidePanel;
            cobSuNo.Text = _objLfuss.SuNo == 0 ? "" : _objLfuss.SuNo.ToString();
            cobSuDia.Text = _objLfuss.SuDia == 0 ? "" : ((int)_objLfuss.SuDia).ToString();
            cobJapan.Text = _objLfuss.Japan;

            txtLength.Text = _objLfuss.Length.ToString();
            txtWidth.Text = _objLfuss.Width.ToString();
            txtSuDis.Text = _objLfuss.SuDis.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDouble(txtLength.Text.Trim()) || Convert.ToDouble(txtLength.Text.Trim()) < 200d)
            {
                MessageBox.Show("请认真检查散流器长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtWidth.Text.Trim()) || Convert.ToDouble(txtWidth.Text.Trim()) < 200d)
            {
                MessageBox.Show("请认真检查散流器宽度", "提示信息");
                txtWidth.Focus();
                txtWidth.SelectAll();
                return;
            }
            if (cobSidePanel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择散流器侧板", "提示信息");
                cobSidePanel.Focus();
                return;
            }
            if (cobSuDia.SelectedIndex == -1)
            {
                MessageBox.Show("请选择均流桶直径", "提示信息");
                cobSuDia.Focus();
                return;
            }
            if (cobSuNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择均流桶数量", "提示信息");
                cobSuNo.Focus();
                return;
            }
            else if (cobSuNo.SelectedIndex > 0 && (!DataValidate.IsDouble(txtSuDis.Text.Trim()) || Convert.ToDouble(txtSuDis.Text.Trim()) < 250d))
            {
                MessageBox.Show("请认真检查均流桶间距", "提示信息");
                txtSuDis.Focus();
                txtSuDis.SelectAll();
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
            LFUSS objLfuss = new LFUSS()
            {
                LFUSSId = Convert.ToInt32(modelView.Tag),
                SidePanel = cobSidePanel.Text,

                SuNo = Convert.ToInt32(cobSuNo.Text),
                SuDia = Convert.ToDouble(cobSuDia.Text.Trim()),
                Japan = cobJapan.Text,

                Length = Convert.ToDouble(txtLength.Text.Trim()),
                Width = Convert.ToDouble(txtWidth.Text.Trim()),
                SuDis = Convert.ToDouble(txtSuDis.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objLfussService.EditModel(objLfuss) == 1)
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
        private void cobSuNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobSuNo.SelectedIndex == -1) return;
            if (cobSuNo.SelectedIndex > 0)
            {
                lblSuDis.Visible = true;
                txtSuDis.Visible = true;
            }
        }
    }
}