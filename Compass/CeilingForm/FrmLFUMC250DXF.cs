﻿using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLfumc250Dxf : MetroFramework.Forms.MetroForm
    {
        readonly LFUMC250DXFService _objLfumc250DxfService = new LFUMC250DXFService();
        private readonly LFUMC250DXF _objLfumc250Dxf = null;
        public FrmLfumc250Dxf()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLfumc250Dxf(Drawing drawing, ModuleTree tree) : this()
        {
            _objLfumc250Dxf = (LFUMC250DXF)_objLfumc250DxfService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objLfumc250Dxf == null) return;
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
            if (_objLfumc250Dxf == null) return;
            modelView.Tag = _objLfumc250Dxf.LFUMC250DXFId;

            //默认txtQuantity为1
            txtQuantity.Text = _objLfumc250Dxf.Quantity == 0 ? "1" : _objLfumc250Dxf.Quantity.ToString();
        }
        /// <summary>
        /// 修改参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditData_Click(object sender, EventArgs e)
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
            LFUMC250DXF objLfumc250Dxf = new LFUMC250DXF()
            {
                LFUMC250DXFId = Convert.ToInt32(modelView.Tag),
                Quantity = Convert.ToInt32(txtQuantity.Text)

            };
            //提交修改
            try
            {
                if (_objLfumc250DxfService.EditModel(objLfumc250Dxf) == 1)
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
