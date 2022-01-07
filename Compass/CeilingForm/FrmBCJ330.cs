﻿using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmBcj330 : MetroFramework.Forms.MetroForm
    {
        readonly BCJ330Service _objBcj330Service = new BCJ330Service();
        private readonly BCJ330 _objBcj330 = null;
        public FrmBcj330()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmBcj330(Drawing drawing, ModuleTree tree) : this()
        {
            _objBcj330 = (BCJ330)_objBcj330Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objBcj330 == null) return;
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
            //脖颈
            cobSuType.Items.Add("SIDE");
            cobSuType.Items.Add("UP");
        }

        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objBcj330 == null) return;
            modelView.Tag = _objBcj330.BCJ330Id;

            cobSidePanel.Text = _objBcj330.SidePanel;
            cobSuType.Text = _objBcj330.SuType;


            txtLength.Text = _objBcj330.Length.ToString();
            txtSuDis.Text = _objBcj330.SuDis.ToString();
        }

        private void BtnEditData_Click(object sender, EventArgs e)
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
            if (cobSidePanel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择CJ腔侧板", "提示信息");
                cobSidePanel.Focus();
                return;
            }
            if (cobSuType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择脖颈方向", "提示信息");
                cobSuType.Focus();
                return;
            }
            if (!DataValidate.IsDouble(txtSuDis.Text.Trim()) || Convert.ToDouble(txtSuDis.Text.Trim()) < 50d)
            {
                MessageBox.Show("请认真检查脖颈距离右端面距离", "提示信息");
                txtSuDis.Focus();
                txtSuDis.SelectAll();
                return;
            }

            #endregion
            //封装对象
            BCJ330 objBcj330 = new BCJ330()
            {
                BCJ330Id = Convert.ToInt32(modelView.Tag),
                SidePanel = cobSidePanel.Text,
                SuType = cobSuType.Text,


                Length = Convert.ToDouble(txtLength.Text.Trim()),
                SuDis = Convert.ToDouble(txtSuDis.Text.Trim()),

            };
            //提交修改
            try
            {
                if (_objBcj330Service.EditModel(objBcj330) == 1)
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
    }
}
