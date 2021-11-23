﻿using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmKvv555 : MetroFramework.Forms.MetroForm
    {
        readonly KVV555Service _objKvv555Service = new KVV555Service();
        private readonly KVV555 _objKvv555 = null;
        public FrmKvv555()
        {
            InitializeComponent();
            SetVisibleFalse();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmKvv555(Drawing drawing, ModuleTree tree) : this()
        {
            _objKvv555 = (KVV555)_objKvv555Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objKvv555 == null) return;
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
            //脖颈数量
            cobExNo.Items.Add("1");
            cobExNo.Items.Add("2");
            cobExNo.SelectedIndex = 0;
            //灯具类型
            cobLightType.Items.Add("NO");
            cobLightType.Items.Add("PHILIPS");
            cobLightType.SelectedIndex = 0;
            
        }
        /// <summary>
        /// 将分组隐藏
        /// </summary>
        private void SetVisibleFalse()
        {
            
        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objKvv555 == null) return;
            modelView.Tag = _objKvv555.KVV555Id;
            
            //默认ExNo为1
            cobExNo.Text = _objKvv555.ExNo == 0 ? "1" : _objKvv555.ExNo.ToString();
            cobLightType.Text = _objKvv555.LightType;
            
            txtLength.Text = _objKvv555.Length.ToString();
            txtDeepth.Text = _objKvv555.Deepth.ToString();
            txtExRightDis.Text = _objKvv555.ExRightDis.ToString();
            txtExDis.Text = _objKvv555.ExDis.ToString();
            txtExLength.Text = _objKvv555.ExLength.ToString();
            txtExWidth.Text = _objKvv555.ExWidth.ToString();
            txtExHeight.Text = _objKvv555.ExHeight.ToString();
            
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
            if (cobLightType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择灯具类型", "提示信息");
                cobLightType.Focus();
                return;
            }

            
            


            #endregion
            //封装对象
            KVV555 objKvv555 = new KVV555()
            {
                KVV555Id = Convert.ToInt32(modelView.Tag),
                ExNo = Convert.ToInt32(cobExNo.Text),
                LightType = cobLightType.Text,
                
                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                Deepth = Convert.ToDecimal(txtDeepth.Text.Trim()),
                ExRightDis = Convert.ToDecimal(txtExRightDis.Text.Trim()),
                ExDis = Convert.ToDecimal(txtExDis.Text.Trim()),
                ExLength = Convert.ToDecimal(txtExLength.Text.Trim()),
                ExWidth = Convert.ToDecimal(txtExWidth.Text.Trim()),
                ExHeight = Convert.ToDecimal(txtExHeight.Text.Trim())
                
            };
            //提交修改
            try
            {
                if (_objKvv555Service.EditModel(objKvv555) == 1)
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
        
    }
}
