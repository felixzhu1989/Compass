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
    public partial class FrmLSDOST : MetroFramework.Forms.MetroForm
    {
        LSDOSTService objLSDOSTService = new LSDOSTService();
        private LSDOST objLSDOST = null;
        public FrmLSDOST()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLSDOST(Drawing drawing, ModuleTree tree) : this()
        {
            objLSDOST = (LSDOST)objLSDOSTService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objLSDOST == null) return;
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
            //均流桶数量
            cobSuNo.Items.Add("1");
            cobSuNo.Items.Add("2");
            cobSuNo.Items.Add("3");
        }

        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (objLSDOST == null) return;
            modelView.Tag = objLSDOST.LSDOSTId;
            cobSuNo.Text = objLSDOST.SuNo == 0 ? "" : objLSDOST.SuNo.ToString();
            txtLength.Text = objLSDOST.Length.ToString();
            txtSuDis.Text = objLSDOST.SuDis.ToString();
        }

        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 200m)
            {
                MessageBox.Show("请认真检查散流器长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            
            if (cobSuNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择均流桶数量", "提示信息");
                cobSuNo.Focus();
                return;
            }
            else if (cobSuNo.SelectedIndex > 0 && (!DataValidate.IsDecimal(txtSuDis.Text.Trim()) || Convert.ToDecimal(txtSuDis.Text.Trim()) < 250m))
            {
                MessageBox.Show("请认真检查均流桶间距", "提示信息");
                txtSuDis.Focus();
                txtSuDis.SelectAll();
                return;
            }
            
            #endregion
            //封装对象
            LSDOST objLSDOST = new LSDOST()
            {
                LSDOSTId = Convert.ToInt32(modelView.Tag),
                SuNo = Convert.ToInt32(cobSuNo.Text),
                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                SuDis = Convert.ToDecimal(txtSuDis.Text.Trim())
            };
            //提交修改
            try
            {
                if (objLSDOSTService.EditModel(objLSDOST) == 1)
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
