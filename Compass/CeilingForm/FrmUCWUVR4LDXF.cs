﻿using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmUcwuvr4Ldxf : MetroFramework.Forms.MetroForm
    {
        readonly UCWUVR4LDXFService _objUcwuvr4LdxfService = new UCWUVR4LDXFService();
        private readonly UCWUVR4LDXF _objUcwuvr4Ldxf = null;
        public FrmUcwuvr4Ldxf()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmUcwuvr4Ldxf(Drawing drawing, ModuleTree tree) : this()
        {
            _objUcwuvr4Ldxf = (UCWUVR4LDXF)_objUcwuvr4LdxfService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objUcwuvr4Ldxf == null) return;
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
            if (_objUcwuvr4Ldxf == null) return;
            modelView.Tag = _objUcwuvr4Ldxf.UCWUVR4LDXFId;

            //默认txtQuantity为1
            txtQuantity.Text = _objUcwuvr4Ldxf.Quantity == 0 ? "1" : _objUcwuvr4Ldxf.Quantity.ToString();
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
            if (!DataValidate.IsInteger(txtQuantity.Text.Trim()))
            {
                MessageBox.Show("请认真检查数量是否填错", "提示信息");
                txtQuantity.Focus();
                txtQuantity.SelectAll();
                return;
            }


            #endregion
            //封装对象
            UCWUVR4LDXF objUcwuvr4Ldxf = new UCWUVR4LDXF()
            {
                UCWUVR4LDXFId = Convert.ToInt32(modelView.Tag),
                Quantity = Convert.ToInt32(txtQuantity.Text)

            };
            //提交修改
            try
            {
                if (_objUcwuvr4LdxfService.EditModel(objUcwuvr4Ldxf) == 1)
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
