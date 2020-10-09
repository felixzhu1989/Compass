using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using Models;
using Common;

namespace Compass
{
    public partial class FrmProject : Form
    {
        private CustomerService objCustomerService = new CustomerService();
        private UserService objUserService = new UserService();
        private ProjectService objProjectService = new ProjectService();
        private ProjectVaultService objProjectVaultService = new ProjectVaultService();
        private string projectId = string.Empty;
        //创建委托变量
        public ShowProjectInfoDelegate ShowProjectInfoDeg = null;
        public ShowModelTreeDelegate ShowModelTreeDeg = null;

        public FrmProject()
        {
            InitializeComponent();
            IniCustomerId(cobCustomerId);
            IniUserId(cobUserId);
            IniVaultId(cobVaultId);
            cobHoodType.Items.Add("Hood");
            cobHoodType.Items.Add("Ceiling");
            cobHoodType.SelectedIndex = -1;
            dgvProjects.AutoGenerateColumns = false;
            btnProject.Text = "添加项目信息";
            //显示全部项目
            btnQueryAllProjects_Click(null, null);
        }
        /// <summary>
        /// 初始化用户下拉框
        /// </summary>
        /// <param name="cobItem"></param>
        private void IniUserId(ComboBox cobItem)
        {
            cobItem.DataSource = objUserService.GetUserTech();
            cobItem.DisplayMember = "UserAccount";
            cobItem.ValueMember = "UserId";
            cobItem.SelectedIndex = -1;//默认不要选中
        }
        /// <summary>
        /// 初始化客户名称
        /// </summary>
        /// <param name="cobItem"></param>
        private void IniCustomerId(ComboBox cobItem)
        {
            cobItem.DataSource = objCustomerService.GetAllCustomers();
            cobItem.DisplayMember = "CustomerName";
            cobItem.ValueMember = "CustomerId";
            cobItem.SelectedIndex = -1;//默认不要选中
        }
        /// <summary>
        /// 初始化PDM项目库
        /// </summary>
        /// <param name="cobItem"></param>
        private void IniVaultId(ComboBox cobItem)
        {
            cobItem.DataSource = objProjectVaultService.GetAllProjectVaults();
            cobItem.DisplayMember = "VaultName";
            cobItem.ValueMember = "VaultId";
            cobItem.SelectedIndex = 0;//默认
        }
        /// <summary>
        /// 弹出添加客户窗口（以模式窗口弹出）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddCustomer_Click(object sender, EventArgs e)
        {
            FrmCustomer objCustomer = new FrmCustomer();
            objCustomer.ShowDialog();
            IniCustomerId(cobCustomerId);
        }
        /// <summary>
        /// 显示所有项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryAllProjects_Click(object sender, EventArgs e)
        {
            dgvProjects.DataSource = objProjectService.GetProjectsByWhereSql("");
        }
        /// <summary>
        /// 按钮，增加或修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProject_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (cobUserId.SelectedIndex == -1)
            {
                MessageBox.Show("请选择制图人员", "验证信息");
                cobUserId.Focus();
                return;
            }
            if (cobVaultId.SelectedIndex == -1)
            {
                MessageBox.Show("请选择PDM库", "验证信息");
                cobVaultId.Focus();
                return;
            }
            if (cobHoodType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择烟罩类型", "验证信息");
                cobHoodType.Focus();
                return;
            }
            if (cobCustomerId.SelectedIndex == -1)
            {
                MessageBox.Show("请选择客户名称，或者右键添加客户名称后再选择", "验证信息");
                cobCustomerId.Focus();
                return;
            }
            if (txtODPNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入项目编号，如果还没有项目编号请输入报价编号，后续再更改", "验证信息");
                txtODPNo.Focus();
                return;
            }
            if (txtBPONo.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入大工单号，请打开NAV系统查询", "验证信息");
                txtBPONo.Focus();
                return;
            }
            if (txtProjectName.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入客户名称", "验证信息");
                txtProjectName.Focus();
                return;
            }
            #endregion

            if (txtProjectId.Text.Trim().Length == 0)
            {
                //提交添加
                //封装对象
                Project objProject = new Project()
                {
                    ODPNo = txtODPNo.Text.Trim().ToUpper(),
                    BPONo = txtBPONo.Text.Trim().ToUpper(),
                    VaultId = Convert.ToInt32(cobVaultId.SelectedValue),
                    ProjectName = txtProjectName.Text.Trim(),
                    CustomerId = Convert.ToInt32(cobCustomerId.SelectedValue),
                    ShippingTime = Convert.ToDateTime(dtpShippingTime.Text),
                    UserId = Convert.ToInt32(cobUserId.SelectedValue),
                    HoodType = cobHoodType.Text.Trim()
                };
                //提交添加
                try
                {
                    //int projectId = objProjectService.AddProject(objProject);
                    //if (projectId > 1)

                    bool result = objProjectService.AddProjectAndTracking(objProject);//基于事务添加技术要求和跟踪
                    if (result)
                    {
                        //提示添加成功
                        MessageBox.Show("项目信息添加成功", "提示信息");
                        //刷新显示
                        btnQueryAllProjects_Click(null, null);
                        //清空内容
                        cobCustomerId.SelectedIndex = -1;
                        cobHoodType.SelectedIndex = -1;
                        foreach (Control item in Controls)
                        {
                            //if (item == txtODPNo) return;//不清空ODPNo，方便查询
                            if (item is TextBox)
                            {
                                item.Text = "";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //封装对象
                Project objProject = new Project()
                {
                    ProjectId = Convert.ToInt32(txtProjectId.Text.Trim()),
                    ODPNo = txtODPNo.Text.Trim().ToUpper(),
                    BPONo = txtBPONo.Text.Trim().ToUpper(),
                    VaultId = Convert.ToInt32(cobVaultId.SelectedValue),
                    ProjectName = txtProjectName.Text.Trim(),
                    CustomerId = Convert.ToInt32(cobCustomerId.SelectedValue),
                    ShippingTime = Convert.ToDateTime(dtpShippingTime.Text),
                    UserId = Convert.ToInt32(cobUserId.SelectedValue),
                    HoodType = cobHoodType.Text.Trim()
                };
                //提交修改
                //调用后台方法修改对象
                try
                {
                    if (objProjectService.EditProject(objProject) == 1)
                    {
                        MessageBox.Show("修改项目信息成功！", "提示信息");
                        btnQueryAllProjects_Click(null, null);//同步刷新显示数据
                        btnProject.Text = "添加项目信息";
                        cobCustomerId.SelectedIndex = -1;
                        cobHoodType.SelectedIndex = -1;
                        //清空内容
                        foreach (Control item in Controls)
                        {
                            if (item is TextBox)
                            {
                                item.Text = "";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 修改项目信息菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditProject_Click(object sender, EventArgs e)
        {
            if (dgvProjects.RowCount == 0) return;
            if (dgvProjects.CurrentRow == null) return;
            projectId = dgvProjects.CurrentRow.Cells["Id"].Value.ToString();
            Project objProject = objProjectService.GetProjectByProjectId(projectId);
            //初始化修改信息
            txtProjectId.Text = objProject.ProjectId.ToString();
            cobUserId.Text = objProject.UserAccount;
            cobVaultId.Text = objProject.VaultName;
            cobCustomerId.Text = objProject.CustomerName;
            txtODPNo.Text = objProject.ODPNo;
            txtBPONo.Text = objProject.BPONo;
            txtProjectName.Text = objProject.ProjectName;
            dtpShippingTime.Text = objProject.ShippingTime.ToShortDateString();
            btnProject.Text = "修改项目信息";
            cobHoodType.Text = objProject.HoodType;
        }
        /// <summary>
        /// 双击修改项目信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProjects_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsmiEditProject_Click(null, null);
        }
        /// <summary>
        /// 显示全部项目信息菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiQueryAllProjects_Click(object sender, EventArgs e)
        {
            btnQueryAllProjects_Click(null, null);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeleteProject_Click(object sender, EventArgs e)
        {
            if (dgvProjects.RowCount == 0)
            {
                return;
            }
            if (dgvProjects.CurrentRow == null)
            {
                MessageBox.Show("请选中需要删除的项目信息", "验证信息");
                return;
            }
            string projectId = dgvProjects.CurrentRow.Cells["Id"].Value.ToString();
            string odpNo = dgvProjects.CurrentRow.Cells["ODPNo"].Value.ToString();
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除（项目编号ODP： " + odpNo + " ）这个项目的信息吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                //if (objProjectService.DeleteProject(projectId) == 1)
                if(objProjectService.DeleteProjectAndTracking(projectId))btnQueryAllProjects_Click(null, null);//同步刷新显示数据
                else MessageBox.Show("删除项目出错，项目是否被其他数据关联，请联系管理员查看后台数据。");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// delete键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProjects_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) tsmiDeleteProject_Click(null, null);
        }
        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProjects_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvProjects, e);
            //if (e.RowIndex > -1)
            //{
            //    int risk = Convert.ToInt32(this.dgvProjects.Rows[e.RowIndex].Cells["RiskLevel"].Value);
            //    if (risk == 1)
            //    {
            //        dgvProjects.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            //    }
            //}
            if (e.RowIndex > -1)
            {
                string projectStatus = this.dgvProjects.Rows[e.RowIndex].Cells["ProjectStatusName"].Value.ToString();
                switch (projectStatus)
                {
                    case "DrawingMaking":
                        dgvProjects.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(178, 252, 255);
                        break;
                    case "InProduction":
                        dgvProjects.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(94, 223, 255);
                        break;
                    case "ProductionCompleted":
                        dgvProjects.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(0, 206, 209);
                        break;
                    default:
                        break;
                }
            }
        }

        private void tsmiShowProjectInfo_Click(object sender, EventArgs e)
        {
            if (dgvProjects.RowCount == 0)
            {
                return;
            }
            if (dgvProjects.CurrentRow == null)
            {
                MessageBox.Show("请选中需要显示的项目信息行", "验证信息");
                return;
            }
            string odpNo = dgvProjects.CurrentRow.Cells["ODPNo"].Value.ToString();
            //调用委托方法
            ShowProjectInfoDeg(odpNo);
        }
        /// <summary>
        /// 根据订单号查询订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryByODPNo_Click(object sender, EventArgs e)
        {
            if (txtODPNo.Text.Trim().Length == 0) return;
            dgvProjects.DataSource = objProjectService.GetProjectsByODPNo(txtODPNo.Text.Trim());
        }
        private void txtODPNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) btnQueryByODPNo_Click(null, null);
        }
        /// <summary>
        /// 根据制图人员查询项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryByUserId_Click(object sender, EventArgs e)
        {
            if (cobUserId.SelectedIndex == -1) return;
            dgvProjects.DataSource = objProjectService.GetProjectsByUserId(cobUserId.SelectedValue.ToString());
        }

        /// <summary>
        /// 选中订单行，回填项目号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProjects_SelectionChanged(object sender, EventArgs e)
        {
            tsmiShowModuleTree_Click(null, null);
        }
        /// <summary>
        /// 显示模型树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiShowModuleTree_Click(object sender, EventArgs e)
        {
            if (dgvProjects.RowCount == 0)
            {
                return;
            }
            if (dgvProjects.CurrentRow == null)
            {
                return;
            }
            txtODPNo.Text = this.dgvProjects.CurrentRow.Cells["ODPNo"].Value.ToString();
            ShowModelTreeDeg(txtODPNo.Text);
        }
        /// <summary>
        /// 根据列的值改变行的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProjects_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            





        }
        /// <summary>
        /// 按回车键按人员提交查询订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) btnQueryByUserId_Click(null,null);
        }
    }
}
