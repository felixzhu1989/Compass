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
    public partial class FrmProjectInfo : Form
    {
        private ProjectService objProjectService = new ProjectService();
        private RequirementService objRequirementService = new RequirementService();
        private DrawingPlanService objDrawingPlanService=new DrawingPlanService();
        //创建委托变量
        public ShowProjectsDelegate ShowProjectsDeg = null;
        public ShowModelTreeDelegate ShowModelTreeDeg = null;
        private string sbu = Program.ObjCurrentUser.SBU;

        public FrmProjectInfo()
        {
            InitializeComponent();
            //绑定ODPNo下拉框
            cobODPNo.DataSource = objProjectService.GetProjectsByWhereSql("",Program.ObjCurrentUser.SBU);
            cobODPNo.DisplayMember = "ODPNo";
            cobODPNo.ValueMember = "ProjectId";
            cobODPNo.SelectedIndex = -1;//默认不要选中
            //初始化后关联事件委托
            this.cobODPNo.SelectedIndexChanged += new System.EventHandler(this.cobODPNo_SelectedIndexChanged);
            SetPermissions();
        }

        public FrmProjectInfo(string odpNo) : this()
        {
            this.cobODPNo.SelectedIndexChanged -= new System.EventHandler(this.cobODPNo_SelectedIndexChanged);
            cobODPNo.Text = odpNo;
            btnRefreshData_Click(null,null);
            //ShowModuleTree();
            this.cobODPNo.SelectedIndexChanged += new System.EventHandler(this.cobODPNo_SelectedIndexChanged);
        }
        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            //管理员和技术部才能添加、编辑、删除模型
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2)
            {
                tsmiDeleteGeneralRequirement.Visible = true;
            }
            else
            {
                tsmiDeleteGeneralRequirement.Visible = false;
            }
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshData_Click(object sender, EventArgs e)
        {
            RefreshProjectInfo();
            RefreshGeneralRequirement();
            RefreshSpecialRequirement();
        }
        /// <summary>
        /// 刷新基本信息
        /// </summary>
        private void RefreshProjectInfo()
        {
            Project objProject = objProjectService.GetProjectByODPNo(cobODPNo.Text,Program.ObjCurrentUser.SBU);
            if (objProject == null)
            {
                MessageBox.Show("项目不存在", "提示信息");
                return;
            }
            txtBPONo.Text = objProject.BPONo.ToString();
            txtUserAccount.Text = objProject.UserAccount;
            txtHoodType.Text = objProject.HoodType;
            txtShippingTime.Text = objProject.ShippingTime.ToShortDateString();
            txtProjectName.Text = objProject.ProjectName;
            txtCustomerName.Text = objProject.CustomerName;
            dgvScope.DataSource = objDrawingPlanService.GetScopeByDataSet(objProject.ProjectId.ToString(),sbu).Tables[0];

        }
        /// <summary>
        /// 刷新通用技术要求
        /// </summary>
        private void RefreshGeneralRequirement()
        {
            GeneralRequirement objGeneralRequirement =
                objRequirementService.GetGeneralRequirementByODPNo(cobODPNo.Text,sbu);
            if (objGeneralRequirement == null)
            {
                txtTypeName.Text = "";
                txtInputPower.Text = "";
                txtMARVEL.Text = "";
                txtANSULPrePipe.Text = "";
                txtANSULSystem.Text = "";
                txtGeneralRequirementId.Text = "";
                txtRiskLevel.Text = "";
            }
            else
            {
                txtTypeName.Text = objGeneralRequirement.TypeName;
                txtInputPower.Text = objGeneralRequirement.InputPower;
                txtMARVEL.Text = objGeneralRequirement.MARVEL;
                txtANSULPrePipe.Text = objGeneralRequirement.ANSULPrePipe;
                txtANSULSystem.Text = objGeneralRequirement.ANSULSystem;
                txtGeneralRequirementId.Text = objGeneralRequirement.GeneralRequirementId.ToString();
                txtRiskLevel.Text = objGeneralRequirement.RiskLevel.ToString();
            }

        }
        /// <summary>
        /// 更新特殊技术要求
        /// </summary>
        private void RefreshSpecialRequirement()
        {
            List<string> specialRequirementList =
                  objRequirementService.GetSpecialRequirementList(cobODPNo.Text,sbu);
            if (specialRequirementList.Count == 0)
            {
                lbxSpecialRequirements.Items.Clear();
                lbxSpecialRequirements.Items.Add("没有特殊要求，或者未填写");
            }
            else
            {
                lbxSpecialRequirements.Items.Clear();
                lbxSpecialRequirements.Items.AddRange(specialRequirementList.ToArray());
            }

        }
        /// <summary>
        /// 手动输入项目号按回车查询项目信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobODPNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) btnRefreshData_Click(null, null);
        }
        /// <summary>
        /// ODP选择变更自动刷新信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobODPNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRefreshData_Click(null, null);
            ShowModuleTree();
        }
        /// <summary>
        /// 显示模型树
        /// </summary>
        private void ShowModuleTree()
        {
            if (cobODPNo.SelectedIndex==-1)return;
            ShowModelTreeDeg(cobODPNo.Text);
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
            Project objProject = objProjectService.GetProjectByODPNo(odpNo,Program.ObjCurrentUser.SBU);
            if (objProject == null) return;
            FrmRequirements objFrmRequirements = new FrmRequirements(objProject);
            objFrmRequirements.ShowDialog();
            btnRefreshData_Click(null, null);
        }
        /// <summary>
        /// 删除通用技术要求菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeleteGeneralRequirement_Click(object sender, EventArgs e)
        {
            if (txtGeneralRequirementId.Text.Trim().Length == 0)
            {
                return;
            }
            string generalRequirementId = txtGeneralRequirementId.Text;
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除（项目编号ODP： " + cobODPNo.Text + " ）这个项目的通用技术要求吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                if (objRequirementService.DeleteGeneralRequirement(generalRequirementId,sbu) == 1)
                {
                    btnRefreshData_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 修改项目信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditProject_Click(object sender, EventArgs e)
        {
            if(cobODPNo.SelectedIndex==-1)return;
            string id = this.cobODPNo.SelectedValue.ToString();
            //调用委托
            ShowProjectsDeg(id);
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
            if (!decimal.TryParse(txtSalesValue.Text, out salseValue))
            {
                txtSalesValue.Clear();
                return;
            }
            txtSalesValue.Text = salseValue.ToString("N0");
            txtSalesValue.SelectionStart = txtSalesValue.Text.Length;
        }
    }
}
