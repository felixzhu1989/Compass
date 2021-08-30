﻿using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmABD200 : MetroFramework.Forms.MetroForm
    {
        ABD200Service objABD200Service =new ABD200Service();
        private ABD200 objABD200 = null;
        public FrmABD200()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmABD200(Drawing drawing, ModuleTree tree) : this()
        {
            objABD200 = (ABD200)objABD200Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objABD200 == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;

            modelView.GetData(drawing, tree);
            modelView.ShowImage();

            FillData();
        }

        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (objABD200 == null) return;
            modelView.Tag = objABD200.ABD200Id;
            txtLength.Text = objABD200.Length.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 50m)
            {
                MessageBox.Show("请认真检查脖颈长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            #endregion

            //封装对象
            ABD200 objABD200 = new ABD200()
            {
                ABD200Id = Convert.ToInt32(modelView.Tag),
                Length = Convert.ToDecimal(txtLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (objABD200Service.EditModel(objABD200) == 1)
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
