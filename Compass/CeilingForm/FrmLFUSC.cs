using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLFUSC : MetroFramework.Forms.MetroForm
    {
        LFUSCService objLFUSCService = new LFUSCService();
        private LFUSC objLFUSC = null;
        public FrmLFUSC()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLFUSC(Drawing drawing, ModuleTree tree) : this()
        {
            objLFUSC = (LFUSC)objLFUSCService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objLFUSC == null) return;
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
            if (objLFUSC == null) return;
            modelView.Tag = objLFUSC.LFUSCId;

            
            cobSuNo.Text = objLFUSC.SuNo == 0 ? "" : objLFUSC.SuNo.ToString();
            cobSuDia.Text = objLFUSC.SuDia == 0 ? "" : ((int)objLFUSC.SuDia).ToString();
            cobJapan.Text = objLFUSC.Japan;

            txtLength.Text = objLFUSC.Length.ToString();
            
            txtSuDis.Text = objLFUSC.SuDis.ToString();
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
            else if (cobSuNo.SelectedIndex > 0 && (!DataValidate.IsDecimal(txtSuDis.Text.Trim()) || Convert.ToDecimal(txtSuDis.Text.Trim()) < 250m))
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
            LFUSC objLFUSC = new LFUSC()
            {
                LFUSCId = Convert.ToInt32(modelView.Tag),
                

                SuNo = Convert.ToInt32(cobSuNo.Text),
                SuDia = Convert.ToDecimal(cobSuDia.Text.Trim()),
                Japan = cobJapan.Text,

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                
                SuDis = Convert.ToDecimal(txtSuDis.Text.Trim())
            };
            //提交修改
            try
            {
                if (objLFUSCService.EditModel(objLFUSC) == 1)
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
