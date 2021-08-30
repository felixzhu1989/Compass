using System;
using System.Windows.Forms;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmUCPDXF : MetroFramework.Forms.MetroForm
    {
        UCPDXFService objUCPDXFService = new UCPDXFService();
        private UCPDXF objUCPDXF = null;
        public FrmUCPDXF()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmUCPDXF(Drawing drawing, ModuleTree tree) : this()
        {
            objUCPDXF = (UCPDXF)objUCPDXFService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objUCPDXF == null) return;
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
            //数量
            cobQuantity.Items.Add("1");
            cobQuantity.Items.Add("2");
            cobQuantity.Items.Add("3");
            cobQuantity.Items.Add("4");
            cobQuantity.Items.Add("5");
            cobQuantity.Items.Add("6");
            cobQuantity.Items.Add("7");
            cobQuantity.Items.Add("8");
        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (objUCPDXF == null) return;
            modelView.Tag = objUCPDXF.UCPDXFId;

            //默认ExNo为1
            cobQuantity.Text = objUCPDXF.Quantity == 0 ? "1" : objUCPDXF.Quantity.ToString();
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
            if (cobQuantity.SelectedIndex == -1)
            {
                MessageBox.Show("请选择数量", "提示信息");
                cobQuantity.Focus();
                return;
            }

            #endregion
            //封装对象
            UCPDXF objUCPDXF = new UCPDXF()
            {
                UCPDXFId = Convert.ToInt32(modelView.Tag),
                Quantity = Convert.ToInt32(cobQuantity.Text)

            };
            //提交修改
            try
            {
                if (objUCPDXFService.EditModel(objUCPDXF) == 1)
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
