﻿using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmBcj300 : MetroFramework.Forms.MetroForm
    {
        readonly BCJ300Service _objBcj300Service = new BCJ300Service();
        private readonly BCJ300 _objBcj300 = null;
        public FrmBcj300()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmBcj300(Drawing drawing, ModuleTree tree) : this()
        {
            _objBcj300 = (BCJ300)_objBcj300Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objBcj300 == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
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
            if (_objBcj300 == null) return;
            modelView.Tag = _objBcj300.BCJ300Id;

            cobSidePanel.Text = _objBcj300.SidePanel;
            cobSuType.Text = _objBcj300.SuType;
            

            txtLength.Text = _objBcj300.Length.ToString();
            txtSuDis.Text = _objBcj300.SuDis.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 90m)
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
            if (!DataValidate.IsDecimal(txtSuDis.Text.Trim()) || Convert.ToDecimal(txtSuDis.Text.Trim()) < 50m)
            {
                MessageBox.Show("请认真检查脖颈距离右端面距离", "提示信息");
                txtSuDis.Focus();
                txtSuDis.SelectAll();
                return;
            }
            
            #endregion
            //封装对象
            BCJ300 objBcj300 = new BCJ300()
            {
                BCJ300Id = Convert.ToInt32(modelView.Tag),
                SidePanel = cobSidePanel.Text,
                SuType = cobSuType.Text,
                

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                SuDis = Convert.ToDecimal(txtSuDis.Text.Trim()),
                
            };
            //提交修改
            try
            {
                if (_objBcj300Service.EditModel(objBcj300) == 1)
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
