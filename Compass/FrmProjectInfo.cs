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
    public partial class FrmProjectInfo : MetroFramework.Forms.MetroForm
    {
        private ProjectService objProjectService = new ProjectService();
        private RequirementService objRequirementService = new RequirementService();
        private DrawingPlanService objDrawingPlanService = new DrawingPlanService();
        private FinancialDataService objFinancialDataService = new FinancialDataService();
        private string sbu = Program.ObjCurrentUser.SBU;

        public FrmProjectInfo()
        {
            InitializeComponent();
            //绑定ODPNo下拉框
            cobODPNo.DataSource = objProjectService.GetProjectsByWhereSql("", Program.ObjCurrentUser.SBU);
            cobODPNo.DisplayMember = "ODPNo";
            cobODPNo.ValueMember = "ProjectId";
            cobODPNo.SelectedIndex = -1;//默认不要选中
            //初始化后关联事件委托
            this.cobODPNo.SelectedIndexChanged += new System.EventHandler(this.cobODPNo_SelectedIndexChanged);
            SetPermissions();
            cobODPNo.SelectedIndex = 0;
            cobODPNo.SelectAll();
        }

        public FrmProjectInfo(string odpNo) : this()
        {
            this.cobODPNo.SelectedIndexChanged -= new System.EventHandler(this.cobODPNo_SelectedIndexChanged);
            cobODPNo.Text = odpNo;
            InitData();
            this.cobODPNo.SelectedIndexChanged += new System.EventHandler(this.cobODPNo_SelectedIndexChanged);
            cobODPNo.Focus();
        }
        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            //管理员和技术部才能查看财务数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2)
            {
                grbFinancialData.Visible = true;
            }
            else
            {
                grbFinancialData.Visible = false;
            }
        }

        private void InitData()
        {
            InitProjectInfo();
            InitGeneralRequirement();
            InitSpecialRequirement();
            InitFinancialData();
        }

        /// <summary>
        /// 初始化项目基本信息
        /// </summary>
        private void InitProjectInfo()
        {
            Project objProject = objProjectService.GetProjectByODPNo(cobODPNo.Text, Program.ObjCurrentUser.SBU);
            if (objProject == null) return;

            string proInfo = "1. 项目编号：" + objProject.ODPNo + "\r\n";
            proInfo += "2. 大工单号：" + objProject.BPONo + "\r\n";
            proInfo += "3. 项目名称：" + objProject.ProjectName + "\r\n";
            proInfo += "4. 客户名称：" + objProject.CustomerName + "\r\n";
            proInfo += "5. 烟罩类型：" + objProject.HoodType + "\r\n";
            proInfo += "6. 制图人员：" + objProject.UserAccount + "\r\n";
            proInfo += "7. 发货日期：" + objProject.ShippingTime.ToShortDateString();
            txtProjectInfo.Text = proInfo;

            dgvScope.DataSource = objDrawingPlanService.GetScopeByDataSet(objProject.ProjectId.ToString(), sbu).Tables[0];
        }
        /// <summary>
        /// 初始化通用技术要求
        /// </summary>
        private void InitGeneralRequirement()
        {
            GeneralRequirement objGeneralRequirement =
                objRequirementService.GetGeneralRequirementByODPNo(cobODPNo.Text, sbu);
            if (objGeneralRequirement == null)
            {
                txtGeneralRequirements.Text = "未填写通用技术要求";
                txtGeneralRequirements.Tag = "";
            }
            else
            {
                string gReq = "1. 项目等级：" + objGeneralRequirement.RiskLevel + "\r\n";
                gReq += "2. 项目类型：" + objGeneralRequirement.TypeName + "\r\n";
                gReq += "3. 项目电制：" + objGeneralRequirement.InputPower + "\r\n";
                gReq += "4. M.A.R.V.E.L.：" + objGeneralRequirement.MARVEL + "\r\n";
                gReq += "5. ANSUL预埋：" + objGeneralRequirement.ANSULPrePipe + "\r\n";
                gReq += "6. ANSUL系统：" + objGeneralRequirement.ANSULSystem;
                txtGeneralRequirements.Text = gReq;
                txtGeneralRequirements.Tag = objGeneralRequirement.GeneralRequirementId;
            }
        }
        /// <summary>
        /// 初始化特殊技术要求
        /// </summary>
        private void InitSpecialRequirement()
        {
            List<string> specialRequirementList =
                  objRequirementService.GetSpecialRequirementList(cobODPNo.Text, sbu);

            if (specialRequirementList.Count == 0)
            {
                txtSpecialRequirements.Text = ("没有特殊要求，或者未填写");
            }
            else
            {
                string sReq = "";
                for (int i = 0; i < specialRequirementList.Count; i++)
                {
                    sReq += (i + 1) + ". " + specialRequirementList[i] + "\r\n";
                }
                txtSpecialRequirements.Text = sReq;
            }

        }
        /// <summary>
        /// 手动输入项目号按回车查询项目信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobODPNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) InitData();
        }
        /// <summary>
        /// ODP选择变更自动初始化信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobODPNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitData();
        }

        /// <summary>
        /// 弹出技术要求窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRequirement_Click(object sender, EventArgs e)
        {
            string odpNo = cobODPNo.Text.Trim();
            if (odpNo.Length == 0) return;
            Project objProject = objProjectService.GetProjectByODPNo(odpNo, Program.ObjCurrentUser.SBU);
            if (objProject == null) return;
            FrmRequirements objFrmRequirements = new FrmRequirements(objProject);
            objFrmRequirements.ShowDialog();
            InitData();
        }
        /// <summary>
        /// 删除通用技术要求菜单
        /// </summary>
        private void DeleteGeneralRequirement()
        {
            if (txtGeneralRequirements.Tag.ToString().Length == 0)
            {
                return;
            }
            string generalRequirementId = txtGeneralRequirements.Tag.ToString();
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除（项目编号ODP： " + cobODPNo.Text + " ）这个项目的通用技术要求吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                if (objRequirementService.DeleteGeneralRequirement(generalRequirementId, sbu) == 1)
                {
                    InitData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvScope_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvScope, e);
        }

        /// <summary>
        /// 输入金额千位分隔符显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSalesValue_TextChanged(object sender, EventArgs e)
        {
            decimal salseValue = 0m;
            if (!decimal.TryParse(txtSalesValue.Text.Trim(), out salseValue))
            {
                txtSalesValue.Clear();
                return;
            }
            txtSalesValue.Text = salseValue.ToString("N0");
            txtSalesValue.SelectionStart = txtSalesValue.Text.Length;
        }
        /// <summary>
        /// 初始化财务数据
        /// </summary>
        private void InitFinancialData()
        {
            FinancialData objFinancialData = objFinancialDataService.GetFinancialDataByProjectId(cobODPNo.SelectedValue.ToString(), sbu);
            if (objFinancialData == null)
            {
                txtSalesValue.Text = "";
                btnFinancialData.Text = "添加财务数据";
                btnFinancialData.Tag = 0;
            }
            else
            {
                txtSalesValue.Text = objFinancialData.SalesValue.ToString("N0");
                btnFinancialData.Text = "更新财务数据";
                btnFinancialData.Tag = objFinancialData.ProjectId;
            }

        }
        /// <summary>
        /// 财务数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinancialData_Click(object sender, EventArgs e)
        {
            if (cobODPNo.SelectedIndex == -1 || txtSalesValue.Text.Length == 0) return;
            FinancialData objFinancialData = new FinancialData()
            {
                ProjectId = Convert.ToInt32(cobODPNo.SelectedValue),
                SalesValue = Convert.ToDecimal(txtSalesValue.Text)
            };

            try
            {
                if (Convert.ToInt32(btnFinancialData.Tag) == 0)
                {
                    if (objFinancialDataService.AddFinancialData(objFinancialData, sbu) > 0) MessageBox.Show("财务数据添加成功", "提示信息");
                }
                else
                {
                    if (objFinancialDataService.EditFinancialData(objFinancialData, sbu) == 1) MessageBox.Show("财务数据更新成功！", "提示信息");
                }
                InitData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
