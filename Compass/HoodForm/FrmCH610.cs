using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmCH610 : MetroFramework.Forms.MetroForm
    {
       CH610Service objCH610Service = new CH610Service();
        private CH610 objCH610 = null;
        public FrmCH610()
        {
            InitializeComponent();
            SetVisibleFalse();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmCH610(Drawing drawing, ModuleTree tree) : this()
        {
            objCH610 = (CH610)objCH610Service.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objCH610 == null) return;
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
            if (objCH610 == null) return;
            modelView.Tag = objCH610.CH610Id;

            //默认ExNo为1
            cobExNo.Text = objCH610.ExNo == 0 ? "1" : objCH610.ExNo.ToString();
            cobLightType.Text = objCH610.LightType;

            txtLength.Text = objCH610.Length.ToString();
            txtDeepth.Text = objCH610.Deepth.ToString();
            txtExRightDis.Text = objCH610.ExRightDis.ToString();
            txtExDis.Text = objCH610.ExDis.ToString();
            txtExLength.Text = objCH610.ExLength.ToString();
            txtExWidth.Text = objCH610.ExWidth.ToString();
            txtExHeight.Text = objCH610.ExHeight.ToString();

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
            CH610 objCH610 = new CH610()
            {
                CH610Id = Convert.ToInt32(modelView.Tag),
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
                if (objCH610Service.EditModel(objCH610) == 1)
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
