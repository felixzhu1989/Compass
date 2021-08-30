using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLLEDS : MetroFramework.Forms.MetroForm
    {
        LLEDSService objLLEDSService = new LLEDSService();
        private LLEDS objLLEDS = null;
        public FrmLLEDS()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLLEDS(Drawing drawing, ModuleTree tree) : this()
        {
            objLLEDS = (LLEDS)objLLEDSService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objLLEDS == null) return;
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
            if (objLLEDS == null) return;
            modelView.Tag = objLLEDS.LLEDSId;

            txtLength.Text = objLLEDS.Length.ToString();
        }
        private void btnEditData_Click(object sender, EventArgs e)
        {
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 200m)
            {
                MessageBox.Show("请认真检查灯腔侧板总长", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            
            //封装对象
            LLEDS objLLEDS = new LLEDS()
            {
                LLEDSId = Convert.ToInt32(modelView.Tag),

                Length = Convert.ToDecimal(txtLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (objLLEDSService.EditModel(objLLEDS) == 1)
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
