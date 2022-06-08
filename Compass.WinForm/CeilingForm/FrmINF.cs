﻿using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmInf : MetroFramework.Forms.MetroForm
    {
        readonly INFService _objInfService = new INFService();
        private readonly INF _objInf = null;
        public FrmInf()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmInf(Drawing drawing, ModuleTree tree) : this()
        {
            _objInf = (INF)_objInfService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objInf == null) return;
            Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
            FillData();
        }

        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objInf == null) return;
            modelView.Tag = _objInf.INFId;

            txtLength.Text = _objInf.Length.ToString();
            txtWidth.Text = _objInf.Width.ToString();
        }
        private void btnEditData_Click(object sender, EventArgs e)
        {
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDouble(txtLength.Text.Trim()) || Convert.ToDouble(txtLength.Text.Trim()) < 50d)
            {
                MessageBox.Show("请认真检查长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtWidth.Text.Trim()) || Convert.ToDouble(txtWidth.Text.Trim()) < 20d)
            {
                MessageBox.Show("请认真检查宽度", "提示信息");
                txtWidth.Focus();
                txtWidth.SelectAll();
                return;
            }


            //封装对象
            INF objInf = new INF()
            {
                INFId = Convert.ToInt32(modelView.Tag),

                Length = Convert.ToDouble(txtLength.Text.Trim()),
                Width = Convert.ToDouble(txtWidth.Text.Trim())

            };
            //提交修改
            try
            {
                if (_objInfService.EditModel(objInf) == 1)
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