using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using Models;
using Common;

namespace Compass
{
    public partial class FrmUserManage : Form
    {
        private UserService objUserService = new UserService();
        private User currentUser = null;
        public FrmUserManage()
        {
            InitializeComponent();
            this.dgvUser.AutoGenerateColumns = false;
            IniGroupName(cobGroupName);
            grbAddUserGroup.Visible = false;
            grbEditUser.Visible = false;
        }

        public FrmUserManage(User user) : this()
        {
            currentUser = user;
            switch (currentUser.UserGroupId)
            {
                case 1:
                    break;
                default:
                    grbAddUser.Visible = false;
                    contextMenuStrip.Enabled = false;
                    lblEditGroupName.Visible = false;
                    cobEditGroupName.Visible = false;
                    break;
            }
            RefreshData();
        }
        private void RefreshData()
        {
            switch (currentUser.UserGroupId)
            {
                case 1:
                    this.dgvUser.DataSource = objUserService.GetUserByWhereSql("");
                    break;
                default:
                    this.dgvUser.DataSource = objUserService.GetUserById(currentUser.UserId.ToString());
                    break;
            }
        }
        /// <summary>
        /// Dgv添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUser_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvUser, e);
        }
        /// <summary>
        /// 初始化用户分组下拉框
        /// </summary>
        /// <param name="cobItem"></param>
        private void IniGroupName(ComboBox cobItem)
        {
            cobItem.DataSource = objUserService.GetAllGroups();
            cobItem.DisplayMember = "GroupName";
            cobItem.ValueMember = "UserGroupId";
            cobItem.SelectedIndex = -1;//默认不要选中
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (txtUserAccount.Text.Trim().Length == 0)
            {
                MessageBox.Show("用户账号不能为空", "验证信息");
                //MetroFramework.MetroMessageBox.Show(this, "用户账号不能为空", "Warning", MessageBoxButtons.OK,
                //    MessageBoxIcon.Warning);
                txtUserAccount.Focus();
                return;
            }
            if (txtUserPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("用户密码不能为空", "验证信息");
                txtUserPwd.Focus();
                return;
            }
            if (cobGroupName.SelectedIndex == -1)
            {
                MessageBox.Show("请选择用户分组", "验证信息");
                cobGroupName.Focus();
                return;
            }
            #endregion
            //封装用户对象
            User objUser = new User()
            {
                UserGroupId = Convert.ToInt32(cobGroupName.SelectedValue),
                UserAccount = txtUserAccount.Text.Trim(),
                UserPwd = txtUserPwd.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Contact = txtContact.Text.Trim()
            };
            //提交添加
            try
            {
                int userId = objUserService.AddUser(objUser);
                if (userId > 1)
                {
                    //提示添加成功
                    MessageBox.Show("用户添加成功", "提示信息");
                    //刷新显示
                    RefreshData();
                    //清空内容
                    cobGroupName.SelectedIndex = -1;
                    foreach (Control item in Controls)
                    {
                        if (item is TextBox)
                        {
                            item.Text = "";
                        }
                    }
                    txtUserAccount.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUserGroup_Click(object sender, EventArgs e)
        {
            if (txtGroupName.Text.Trim().Length == 0)
            {
                MessageBox.Show("分组名称不能为空", "验证信息");
                txtGroupName.Focus();
                return;
            }
            //封装分组对象
            UserGroup objUserGroup = new UserGroup()
            {
                GroupName = txtGroupName.Text.Trim()
            };
            try
            {
                int userGroupId = objUserService.AddUserGoup(objUserGroup);
                if (userGroupId > 1)
                {
                    //提示添加成功并询问是否继续添加
                    DialogResult result = MessageBox.Show("分组添加成功，是否继续添加？", "继续添加询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result != DialogResult.OK)
                    {
                        grbAddUserGroup.Visible = false;
                    }
                    //刷新下拉框
                    IniGroupName(cobGroupName);
                    //清空内容
                    txtGroupName.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 添加分组菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddUserGroup_Click(object sender, EventArgs e)
        {
            if (grbAddUserGroup.Visible == false)
            {
                grbAddUserGroup.Visible = true;
            }
            else
            {
                grbAddUserGroup.Visible = false;
            }
        }
        /// <summary>
        /// 修改用户菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditUser_Click(object sender, EventArgs e)
        {
            if (dgvUser.RowCount == 0)
            {
                return;
            }
            if (dgvUser.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的用户", "提示信息");
                return;
            }
            string userId = dgvUser.CurrentRow.Cells["UserId"].Value.ToString();
            User objUser = objUserService.GetUserByUserId(userId);
            //初始化修改信息
            grbEditUser.Visible = true;//显示
            //不显示用户分组，不支持修改分组
            cobEditGroupName.Visible = false;
            lblEditGroupName.Visible = false;
            //将用户名只读，不允许修改
            txtEditUserAccount.ReadOnly = true;

            IniGroupName(cobEditGroupName);
            txtUserId.Text = objUser.UserId.ToString();
            cobEditGroupName.SelectedValue = objUser.UserGroupId;
            txtEditUserAccount.Text = objUser.UserAccount;
            txtEditUserPwd.Text = objUser.UserPwd;
            txtEditEmail.Text = objUser.Email;
            txtEditContact.Text = objUser.Contact;
        }
        /// <summary>
        /// 双击修改用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsmiEditUser_Click(null, null);
        }
        /// <summary>
        /// 提交修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (txtEditUserAccount.Text.Trim().Length == 0)
            {
                MessageBox.Show("用户账号不能为空");
                txtEditUserAccount.Focus();
                return;
            }
            if (txtEditUserPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("用户密码不能为空");
                txtEditUserPwd.Focus();
                return;
            }
            if (cobEditGroupName.SelectedIndex == -1)
            {
                MessageBox.Show("请选择用户分组");
                cobEditGroupName.Focus();
                return;
            }
            #endregion
            //封装用户对象
            User objUser = new User()
            {
                UserId = Convert.ToInt32(txtUserId.Text.Trim()),
                UserGroupId = Convert.ToInt32(cobEditGroupName.SelectedValue),
                UserAccount = txtEditUserAccount.Text.Trim(),
                UserPwd = txtEditUserPwd.Text.Trim(),
                Email = txtEditEmail.Text.Trim(),
                Contact = txtEditContact.Text.Trim()
            };
            //调用后台方法修改用户对象
            try
            {
                if (objUserService.EditUser(objUser) == 1)
                {
                    MessageBox.Show("修改用户成功！", "提示信息");
                    grbEditUser.Visible = false;
                    RefreshData();//同步刷新显示数据
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 删除用户菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUser.RowCount == 0)
            {
                return;
            }
            if (dgvUser.CurrentRow == null)
            {
                MessageBox.Show("请选中需要删除的用户", "验证信息");
                return;
            }
            string userId = dgvUser.CurrentRow.Cells["UserId"].Value.ToString();
            string userAccount = dgvUser.CurrentRow.Cells["UserAccount"].Value.ToString();
            if (userAccount == "admin") return;
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除用户（ " + userAccount + " ）吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                if (objUserService.DeleteUser(userId) == 1)
                {
                    RefreshData();//同步刷新显示数据
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            grbEditUser.Visible = false;
        }

        private void dgvUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) tsmiDeleteUser_Click(null, null);
        }
    }
}
