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
using Models;
using DAL;

namespace Compass
{
    public partial class FrmVaultsStatusTypes : Form
    {
        private ProjectVaultService objProjectVaultService=new ProjectVaultService();
        private ProjectStatusService objProjectStatusService =new ProjectStatusService();
        private ProjectTypeService objProjectTypeService=new ProjectTypeService();
        public FrmVaultsStatusTypes()
        {
            InitializeComponent();
            dgvProjectVaults.AutoGenerateColumns = false;
            dgvProjectVaults.DataSource = objProjectVaultService.GetAllProjectVaults();
            btnEditProjectVault.Visible = false;
            btnAddProjectVault.Visible = true;

            dgvProjectStatus.AutoGenerateColumns = false;
            dgvProjectStatus.DataSource = objProjectStatusService.GetAllProjectStatus();
            btnEditProjectStatus.Visible = false;
            btnAddProjectStatus.Visible = true;

            dgvProjectTypes.AutoGenerateColumns = false;
            dgvProjectTypes.DataSource = objProjectTypeService.GetAllProjectTypes();
            btnEditProjectType.Visible = false;
            btnAddProjectType.Visible = true;
        }
        /// <summary>
        /// 添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProjectVaults_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvProjectVaults, e);
        }
        private void dgvProjectStatus_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvProjectStatus, e);
        }
        private void dgvProjectTypes_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvProjectTypes, e);
        }

        #region 文本框按回车键
        /// <summary>
        /// 文本框按回车键直接提交添加或者修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtVaultName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (txtVaultId.Text.Length == 0)//如果编号有内容则是修改，如果没有内容则是添加
                {
                    btnAddProjectVault_Click(null, null);
                }
                else
                {
                    btnEditProjectVault_Click(null, null);
                }
            }
        }
        private void txtProjectStatusName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (txtProjectStatusId.Text.Length == 0)//如果编号有内容则是修改，如果没有内容则是添加
                {
                    btnAddProjectStatus_Click(null, null);
                }
                else
                {
                    btnEditProjectStatus_Click(null, null);
                }
            }
        }
        private void txtTypeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (txtTypeId.Text.Length == 0)//如果编号有内容则是修改，如果没有内容则是添加
                {
                    btnAddProjectType_Click(null, null);
                }
                else
                {
                    btnEditProjectType_Click(null, null);
                }
            }
        }
        #endregion
        #region 添加操作
        /// <summary>
        /// 添加项目库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProjectVault_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (txtVaultName.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入项目库名称", "验证信息");
                txtVaultName.Focus();
                return;
            }
            #endregion
            //封装对象
            ProjectVault objProjectVault = new ProjectVault()
            {
                VaultName = txtVaultName.Text.Trim()
            };
            //提交添加
            try
            {
                int vaultId = objProjectVaultService.AddProjectVault(objProjectVault);
                if (vaultId > 1)
                {
                    //提示添加成功
                    MessageBox.Show("项目库添加成功", "提示信息");
                    //刷新显示
                    dgvProjectVaults.DataSource = objProjectVaultService.GetAllProjectVaults();
                    //清空内容
                    txtVaultName.Text ="";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 添加项目状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProjectStatus_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (txtProjectStatusName.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入项目状态名称", "验证信息");
                txtProjectStatusName.Focus();
                return;
            }
            #endregion
            //封装对象
            ProjectStatus objProjectStatus = new ProjectStatus()
            {
                ProjectStatusName = txtProjectStatusName.Text.Trim(),
                StatusDesc = txtStatusDesc.Text.Trim()
            };
            //提交添加
            try
            {
                int projectStatusId = objProjectStatusService.AddProjectStatus(objProjectStatus);
                if (projectStatusId > 1)
                {
                    //提示添加成功
                    MessageBox.Show("项目状态添加成功", "提示信息");
                    //刷新显示
                    dgvProjectStatus.DataSource = objProjectStatusService.GetAllProjectStatus();
                    //清空内容
                    txtProjectStatusName.Text = "";
                    txtStatusDesc.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 添加项目类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProjectType_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (txtTypeName.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入项目类型名称", "验证信息");
                txtTypeName.Focus();
                return;
            }
            #endregion
            //封装对象
            ProjectType objProjectType = new ProjectType()
            {
                TypeName = txtTypeName.Text.Trim(),
                KMLink = txtKMLink.Text.Trim()
            };
            //提交添加
            try
            {
                int typeId = objProjectTypeService.AddProjectType(objProjectType);
                if (typeId > 1)
                {
                    //提示添加成功
                    MessageBox.Show("项目类型添加成功", "提示信息");
                    //刷新显示
                    dgvProjectTypes.DataSource = objProjectTypeService.GetAllProjectTypes();
                    //清空内容
                    txtTypeName.Text = "";
                    txtKMLink.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 修改操作
        /// <summary>
        /// 修改项目库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditProjectValut_Click(object sender, EventArgs e)
        {
            btnAddProjectVault.Visible = false;
            btnEditProjectVault.Visible = true;

            if (dgvProjectVaults.RowCount == 0)
            {
                return;
            }
            if (dgvProjectVaults.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的项目库名称", "提示信息");
                return;
            }
            string vaultId = dgvProjectVaults.CurrentRow.Cells["VaultId"].Value.ToString();
            ProjectVault objProjectVault = objProjectVaultService.GetProjectVaultById(vaultId);
            //初始化修改信息
            txtVaultId.Text = objProjectVault.VaultId.ToString();
            txtVaultName.Text = objProjectVault.VaultName;
        }
        private void dgvProjectVaults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsmiEditProjectValut_Click(null, null);
        }
        /// <summary>
        /// 提交修改项目库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditProjectVault_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (txtVaultName.Text.Trim().Length == 0)
            {
                MessageBox.Show("项目库名称不能为空", "验证信息");
                txtVaultName.Focus();
                return;
            }
            #endregion
            //封装项目跟踪对象
            ProjectVault objProjectVault = new ProjectVault()
            {
                VaultId = Convert.ToInt32(txtVaultId.Text.Trim()),
                VaultName = txtVaultName.Text.Trim()
            };
            //调用后台方法修改对象
            try
            {
                if (objProjectVaultService.EditProjectValult(objProjectVault) == 1)
                {
                    MessageBox.Show("修改项目库名称成功！", "提示信息");
                    //刷新内容
                    dgvProjectVaults.DataSource = objProjectVaultService.GetAllProjectVaults();
                    //清空内容
                    txtVaultId.Text = "";
                    txtVaultName.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnAddProjectVault.Visible = true;
            btnEditProjectVault.Visible = false;
        }
        /// <summary>
        /// 修改项目状态菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditProjectStatus_Click(object sender, EventArgs e)
        {
            btnAddProjectStatus.Visible = false;
            btnEditProjectStatus.Visible = true;

            if (dgvProjectStatus.RowCount == 0)
            {
                return;
            }
            if (dgvProjectStatus.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的项目状态名称", "提示信息");
                return;
            }
            string projectStatusId = dgvProjectStatus.CurrentRow.Cells["ProjectStatusId"].Value.ToString();
            ProjectStatus objProjectStatus = objProjectStatusService.GetProjectStatusById(projectStatusId);
            //初始化修改信息
            txtProjectStatusId.Text = objProjectStatus.ProjectStatusId.ToString();
            txtProjectStatusName.Text = objProjectStatus.ProjectStatusName;
            txtStatusDesc.Text = objProjectStatus.StatusDesc;
        }
        private void dgvProjectStatus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsmiEditProjectStatus_Click(null, null);
        }
        /// <summary>
        /// 提交修改项目状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditProjectStatus_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (txtProjectStatusName.Text.Trim().Length == 0)
            {
                MessageBox.Show("项目状态名称不能为空", "验证信息");
                txtProjectStatusName.Focus();
                return;
            }
            #endregion
            //封装对象
            ProjectStatus objProjectStatus = new ProjectStatus()
            {
                ProjectStatusId =Convert.ToInt32(txtProjectStatusId.Text.Trim()),
                ProjectStatusName = txtProjectStatusName.Text.Trim(),
                StatusDesc = txtStatusDesc.Text.Trim()
            };
            //调用后台方法修改对象
            try
            {
                if (objProjectStatusService.EditProjectStatus(objProjectStatus) == 1)
                {
                    MessageBox.Show("修改项目状态名称成功！", "提示信息");
                    //刷新内容
                    dgvProjectStatus.DataSource = objProjectStatusService.GetAllProjectStatus();
                    //清空内容
                    txtProjectStatusId.Text = "";
                    txtProjectStatusName.Text = "";
                    txtStatusDesc.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnAddProjectStatus.Visible = true;
            btnEditProjectStatus.Visible = false;
        }
        /// <summary>
        /// 修改项目类型菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditProjectType_Click(object sender, EventArgs e)
        {
            btnAddProjectType.Visible = false;
            btnEditProjectType.Visible = true;

            if (dgvProjectTypes.RowCount == 0)
            {
                return;
            }
            if (dgvProjectTypes.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的项目类型名称", "提示信息");
                return;
            }
            string typeId = dgvProjectTypes.CurrentRow.Cells["TypeId"].Value.ToString();
            ProjectType objProjectType = objProjectTypeService.GetProjectTypeById(typeId);
            //初始化修改信息
            txtTypeId.Text = objProjectType.TypeId.ToString();
            txtTypeName.Text = objProjectType.TypeName;
            txtKMLink.Text = objProjectType.KMLink;
        }
        private void dgvProjectTypes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsmiEditProjectType_Click(null, null);
        }
        /// <summary>
        /// 提交修改项目类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditProjectType_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (txtTypeName.Text.Trim().Length == 0)
            {
                MessageBox.Show("项目类型名称不能为空", "验证信息");
                txtTypeName.Focus();
                return;
            }
            #endregion
            //封装对象
            ProjectType objProjectType = new ProjectType()
            {
                TypeId = Convert.ToInt32(txtTypeId.Text.Trim()),
                TypeName = txtTypeName.Text.Trim(),
                KMLink = txtKMLink.Text.Trim()
            };
            //调用后台方法修改对象
            try
            {
                if (objProjectTypeService.EditProjectType(objProjectType) == 1)
                {
                    MessageBox.Show("修改项目类型名称成功！", "提示信息");
                    //刷新内容
                    dgvProjectTypes.DataSource = objProjectTypeService.GetAllProjectTypes();
                    //清空内容
                    txtTypeId.Text = "";
                    txtTypeName.Text = "";
                    txtKMLink.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnAddProjectType.Visible = true;
            btnEditProjectType.Visible = false;
        }
        #endregion
        #region 删除操作
        /// <summary>
        /// 删除项目库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeleteProjectVault_Click(object sender, EventArgs e)
        {
            if (dgvProjectVaults.RowCount == 0)
            {
                return;
            }
            if (dgvProjectVaults.CurrentRow == null)
            {
                MessageBox.Show("请选中需要删除的项目库名称", "验证信息");
                return;
            }
            string vaultId = dgvProjectVaults.CurrentRow.Cells["VaultId"].Value.ToString();
            string vaultName = dgvProjectVaults.CurrentRow.Cells["VaultName"].Value.ToString();
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除（ " + vaultName + " ）这个库名称吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                if (objProjectVaultService.DeleteProjectVault(vaultId) == 1)
                {
                   dgvProjectVaults.DataSource=objProjectVaultService.GetAllProjectVaults();//同步刷新显示数据
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 删除项目状态菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeleteProjectStatus_Click(object sender, EventArgs e)
        {
            if (dgvProjectStatus.RowCount == 0)
            {
                return;
            }
            if (dgvProjectStatus.CurrentRow == null)
            {
                MessageBox.Show("请选中需要删除的项目状态名称", "验证信息");
                return;
            }
            string projectStatusId = dgvProjectStatus.CurrentRow.Cells["ProjectStatusId"].Value.ToString();
            string projectStatusName = dgvProjectStatus.CurrentRow.Cells["ProjectStatusName"].Value.ToString();
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除（ " + projectStatusName + " ）这个状态名称吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                if (objProjectStatusService.DeleteProjectStatus(projectStatusId) == 1)
                {
                    dgvProjectStatus.DataSource = objProjectStatusService.GetAllProjectStatus();//同步刷新显示数据
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 删除项目类型菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeleteProjectType_Click(object sender, EventArgs e)
        {
            if (dgvProjectTypes.RowCount == 0)
            {
                return;
            }
            if (dgvProjectTypes.CurrentRow == null)
            {
                MessageBox.Show("请选中需要删除的项目类型名称", "验证信息");
                return;
            }
            string typeId = dgvProjectTypes.CurrentRow.Cells["TypeId"].Value.ToString();
            string typeName = dgvProjectTypes.CurrentRow.Cells["TypeName"].Value.ToString();
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除（ " + typeName + " ）这个类型名称吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                if (objProjectTypeService.DeleteProjectType(typeId) == 1)
                {
                    dgvProjectTypes.DataSource = objProjectTypeService.GetAllProjectTypes();//同步刷新显示数据
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }























        #endregion

        private void dgvProjectVaults_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)tsmiDeleteProjectVault_Click(null,null);
        }

        private void dgvProjectStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)tsmiDeleteProjectStatus_Click(null,null);
        }

        private void dgvProjectTypes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)tsmiDeleteProjectType_Click(null,null);
        }
    }
}
