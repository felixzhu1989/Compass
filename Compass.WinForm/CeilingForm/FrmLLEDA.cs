using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmLleda : MetroFramework.Forms.MetroForm
    {
        readonly LLEDAService _objLledaService = new LLEDAService();
        private readonly LLEDA _objLleda = null;
        public FrmLleda()
        {
            InitializeComponent();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmLleda(Drawing drawing, ModuleTree tree) : this()
        {
            _objLleda = (LLEDA)_objLledaService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objLleda == null) return;
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
            if (_objLleda == null) return;
            modelView.Tag = _objLleda.LLEDAId;

            txtLength.Text = _objLleda.Length.ToString();
        }
        private void btnEditData_Click(object sender, EventArgs e)
        {
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDouble(txtLength.Text.Trim()) || Convert.ToDouble(txtLength.Text.Trim()) < 200d)
            {
                MessageBox.Show("请认真检查灯腔侧板总长", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }

            //封装对象
            LLEDA objLleda = new LLEDA()
            {
                LLEDAId = Convert.ToInt32(modelView.Tag),

                Length = Convert.ToDouble(txtLength.Text.Trim())
            };
            //提交修改
            try
            {
                if (_objLledaService.EditModel(objLleda) == 1)
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
