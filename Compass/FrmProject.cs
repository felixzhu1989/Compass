using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DAL;
using Models;
using Common;

namespace Compass
{
    public partial class FrmProject : Form
    {
        private SqlDataPager objSqlDataPager = null;
        private CustomerService objCustomerService = new CustomerService();
        private UserService objUserService = new UserService();
        private ProjectService objProjectService = new ProjectService();
        private string projectId = string.Empty;
        KeyValuePair pair=new KeyValuePair();
        //创建委托变量
        public ShowModelTreeDelegate ShowModelTreeDeg = null;
        private string sbu = Program.ObjCurrentUser.SBU;//当前事业部

        public FrmProject()
        {
            InitializeComponent();
            toolTip.SetToolTip(cobQueryYear, "按照项目完工日期年度查询");
            this.dgvProjects.SelectionChanged -= new System.EventHandler(this.DgvProjects_SelectionChanged);
            IniCustomerId(cobCustomerId);
            IniUserId(cobUserId);

            cobHoodType.Items.Add("Hood");
            cobHoodType.Items.Add("Ceiling");
            cobHoodType.SelectedIndex = -1;
            dgvProjects.AutoGenerateColumns = false;
            btnProject.Text = "添加项目信息";

            //查询年度初始化
            int currentYear = DateTime.Now.Year;
            cobQueryYear.Items.Add(currentYear + 1);//先添加下一年
            for (int i = 0; i <= currentYear - 2020; i++)
            {
                cobQueryYear.Items.Add(currentYear - i);
            }
            cobQueryYear.SelectedIndex = 1;//默认定位当前年份
            //设置默认的显示条数
            this.cobRecordList.SelectedIndex = 0;
            //初始无数据禁用相关按钮,考虑用户体验
            this.btnToPage.Enabled = false;
            this.btnFirst.Enabled = false;
            this.btnPre.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;
            //分页sql语句
            objSqlDataPager = objProjectService.GetSqlDataPager(sbu);

            BtnQueryByYear_Click(null, null);

            this.dgvProjects.SelectionChanged += new System.EventHandler(this.DgvProjects_SelectionChanged);

            SetPermissions();
        }
        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2)
            {
                //技术部和管理员登陆后选中当前登陆用户
                cobUserId.Text = Program.ObjCurrentUser.UserAccount;
            }
            else
            {
                cobUserId.SelectedIndex = -1; //默认选中
            }

            //管理员和技术部才能添加、编辑、删除项目、显示金额
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2)
            {
                btnProject.Visible = true;
                tsmiAddCustomer.Visible = true;
                tsmiEditProject.Visible = true;
                tsmiDeleteProject.Visible = true;
                this.dgvProjects.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvProjects_CellDoubleClick);
                this.dgvProjects.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvProjects_KeyDown);
                this.dgvProjects.Columns["SalesValue"].Visible = true;
            }
            else
            {
                btnProject.Visible = false;
                tsmiAddCustomer.Visible = false;
                tsmiEditProject.Visible = false;
                tsmiDeleteProject.Visible = false;
                this.dgvProjects.CellDoubleClick -= new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvProjects_CellDoubleClick);
                this.dgvProjects.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.DgvProjects_KeyDown);
                this.dgvProjects.Columns["SalesValue"].Visible = false;
            }
        }
        /// <summary>
        /// 执行查询的公共方法
        /// </summary>
        private void Query()
        {
            //开启所有的按钮
            this.btnToPage.Enabled = true;
            this.btnFirst.Enabled = true;
            this.btnPre.Enabled = true;
            this.btnNext.Enabled = true;
            this.btnLast.Enabled = true;
            //【1】设置分页查询的条件
            //objSqlDataPager.Condition = string.Format("ShippingTime>='{0}/01/01' and ShippingTime<='{0}/12/31'", this.cobQueryYear.Text);
            //【2】设置每页显示的条数
            //objSqlDataPager.PageSize = Convert.ToInt32(this.cobRecordList.Text.Trim());
            //【3】执行查询
            this.dgvProjects.DataSource = objSqlDataPager.GetPagedData();
            //【4】显示记录总数，显示总页数，显示当前页码
            this.lblRecordsCound.Text = objSqlDataPager.RecordCount.ToString();
            this.lblPageCount.Text = objSqlDataPager.TotalPages.ToString();
            if (objSqlDataPager.RecordCount == 0)
            {
                this.lblCurrentPage.Text = "0";
            }
            else
            {
                this.lblCurrentPage.Text = objSqlDataPager.CurrentPage.ToString();
            }
            //禁用按钮的情况
            if (this.lblPageCount.Text == "0" || this.lblPageCount.Text == "1")
            {
                this.btnToPage.Enabled = false;
                this.btnFirst.Enabled = false;
                this.btnPre.Enabled = false;
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
            }
            else
            {
                this.btnToPage.Enabled = true;
            }
        }

        private void QueryByYear()
        {
            objSqlDataPager.Condition = string.Format(" ShippingTime like '{0}%'", this.cobQueryYear.Text);
            objSqlDataPager.PageSize = Convert.ToInt32(this.cobRecordList.Text.Trim());
            Query();
        }

        private void QureyAll()
        {
            objSqlDataPager.Condition = " ShippingTime>='2020/01/01'";
            objSqlDataPager.PageSize = 10000;
            Query();
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
        }

        /// <summary>
        /// 弹出添加客户窗口（以模式窗口弹出）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiAddCustomer_Click(object sender, EventArgs e)
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
        private void BtnQueryAllProjects_Click(object sender, EventArgs e)
        {
            QureyAll();
        }
        /// <summary>
        /// 按钮，增加或修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnProject_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (cobUserId.SelectedIndex == -1)
            {
                MessageBox.Show("请选择制图人员", "验证信息");
                cobUserId.Focus();
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

                    bool result = objProjectService.AddProjectAndTracking(objProject, sbu);//基于事务添加技术要求和跟踪
                    if (result)
                    {
                        //提示添加成功
                        MessageBox.Show("项目信息添加成功", "提示信息");
                        //刷新显示
                        BtnQueryByYear_Click(null, null);
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
                int firstRowIndex = dgvProjects.CurrentRow.Index;
                //封装对象
                Project objProject = new Project()
                {
                    ProjectId = Convert.ToInt32(txtProjectId.Text.Trim()),
                    ODPNo = txtODPNo.Text.Trim().ToUpper(),
                    BPONo = txtBPONo.Text.Trim().ToUpper(),
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
                    if (objProjectService.EditProject(objProject,sbu)==1)
                    {
                        MessageBox.Show("修改项目信息成功！", "提示信息");
                        BtnQueryByYear_Click(null, null); //同步刷新显示数据
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
                dgvProjects.ClearSelection();
                dgvProjects.Rows[firstRowIndex].Selected = true;//将刚修改的行选中
                dgvProjects.FirstDisplayedScrollingRowIndex = firstRowIndex;//将修改的行显示在第一行
            }
        }
        /// <summary>
        /// 修改项目信息菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiEditProject_Click(object sender, EventArgs e)
        {
            if (dgvProjects.RowCount == 0) return;
            if (dgvProjects.CurrentRow == null) return;
            projectId = dgvProjects.CurrentRow.Cells["Id"].Value.ToString();
            Project objProject = objProjectService.GetProjectByProjectId(projectId,sbu);
            //初始化修改信息
            txtProjectId.Text = objProject.ProjectId.ToString();
            cobUserId.Text = objProject.UserAccount;
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
        private void DgvProjects_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TsmiEditProject_Click(null, null);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiDeleteProject_Click(object sender, EventArgs e)
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
            int firstRowIndex = dgvProjects.CurrentRow.Index;
            try
            {
                //if (objProjectService.DeleteProject(projectId) == 1)
                if (objProjectService.DeleteProjectAndTracking(projectId,sbu)) BtnQueryAllProjects_Click(null, null);//同步刷新显示数据
                else MessageBox.Show("删除项目出错，项目是否被其他数据关联，请联系管理员查看后台数据。");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgvProjects.ClearSelection();
            dgvProjects.Rows[firstRowIndex].Selected = true;//将刚修改的行选中
            dgvProjects.FirstDisplayedScrollingRowIndex = firstRowIndex;//将修改的行显示在第一行
        }
        /// <summary>
        /// delete键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvProjects_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) TsmiDeleteProject_Click(null, null);
        }
        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvProjects_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
                dgvProjects.Rows[e.RowIndex].DefaultCellStyle.BackColor =pair.ProjectStatusColorKeyValue.Where(q => q.Key == projectStatus).First().Value;
            }
        }

        private void TsmiShowProjectInfo_Click(object sender, EventArgs e)
        {
            if (dgvProjects.RowCount == 0) return;
            if (dgvProjects.CurrentRow == null) return;
            string odpNo = dgvProjects.CurrentRow.Cells["ODPNo"].Value.ToString();
            FrmProjectInfo objFrmProjectInfo = new FrmProjectInfo(odpNo);
            objFrmProjectInfo.Show();
        }
        /// <summary>
        /// 根据订单号查询订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQueryByODPNo_Click(object sender, EventArgs e)
        {
            if (txtODPNo.Text.Trim().Length == 0) return;
            objSqlDataPager.Condition = string.Format("ODPNo = '{0}'", txtODPNo.Text.Trim());
            objSqlDataPager.PageSize = Convert.ToInt32(cobRecordList.Text);
            Query();
        }
        private void TxtODPNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) BtnQueryByODPNo_Click(null, null);
        }
        /// <summary>
        /// 根据制图人员查询项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQueryByUserId_Click(object sender, EventArgs e)
        {
            if (cobUserId.SelectedIndex == -1) return;
            objSqlDataPager.Condition = string.Format("Projects{0}.UserId = {1} and ShippingTime like '{2}%'", sbu,cobUserId.SelectedValue.ToString(), this.cobQueryYear.Text);
            objSqlDataPager.PageSize = 10000;
            Query();
        }

        /// <summary>
        /// 选中订单行，回填项目号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvProjects_SelectionChanged(object sender, EventArgs e)
        {
            TsmiShowModuleTree_Click(null, null);
        }
        /// <summary>
        /// 显示模型树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiShowModuleTree_Click(object sender, EventArgs e)
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
        /// 按回车键按人员提交查询订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) BtnQueryByUserId_Click(null, null);
        }
        /// <summary>
        /// 根据年份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQueryByYear_Click(object sender, EventArgs e)
        {
            if (this.cobQueryYear.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要查询的年度", "提示信息");
                return;
            }
            objSqlDataPager.CurrentPage = 1;//每次执行查询都必须设置为第一页
            QueryByYear();
            //禁用上一页按钮
            this.btnFirst.Enabled = false;
            this.btnPre.Enabled = false;
        }

        /// <summary>
        /// 第一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFirst_Click(object sender, EventArgs e)
        {
            objSqlDataPager.CurrentPage = 1;//每次执行查询都必须设置为第一页
            QueryByYear();
            //禁用上一页按钮和第一页
            this.btnFirst.Enabled = false;
            this.btnPre.Enabled = false;
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPre_Click(object sender, EventArgs e)
        {
            objSqlDataPager.CurrentPage -= 1;//在当前页码上减一
            QueryByYear();
            //禁用下一页和最后一页按钮
            if (objSqlDataPager.CurrentPage == 1)
            {
                this.btnFirst.Enabled = false;
                this.btnPre.Enabled = false;
            }
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNext_Click(object sender, EventArgs e)
        {
            objSqlDataPager.CurrentPage += 1;//在当前页码上加一
            QueryByYear();
            //禁用下一页和最后一页按钮
            if (objSqlDataPager.CurrentPage == objSqlDataPager.TotalPages)
            {
                this.btnLast.Enabled = false;
                this.btnNext.Enabled = false;
            }
        }
        /// <summary>
        /// 最后一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLast_Click(object sender, EventArgs e)
        {
            objSqlDataPager.CurrentPage = objSqlDataPager.TotalPages;//在当前页码上加一
            QueryByYear();
            //禁用下一页和最后一页按钮
            this.btnLast.Enabled = false;
            this.btnNext.Enabled = false;
        }
        /// <summary>
        /// 跳转到
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnToPage_Click(object sender, EventArgs e)
        {
            int a = this.txtToPage.IsInteger("跳转的页码");
            if (a != 0)
            {
                int toPage = Convert.ToInt32(this.txtToPage.Text.Trim());
                if (toPage > objSqlDataPager.TotalPages)
                {
                    BtnLast_Click(null, null);//直接为最后一页
                }
                else if (toPage == 0)
                {
                    BtnFirst_Click(null, null);//第一页
                }
                else
                {
                    objSqlDataPager.CurrentPage = toPage;//跳转到给定页码
                    Query();
                    if (objSqlDataPager.CurrentPage == 1)
                    {
                        //禁用上一页按钮和第一页
                        this.btnFirst.Enabled = false;
                        this.btnPre.Enabled = false;
                    }
                    else if (objSqlDataPager.CurrentPage == objSqlDataPager.TotalPages)
                    {
                        //禁用下一页和最后一页按钮
                        this.btnLast.Enabled = false;
                        this.btnNext.Enabled = false;
                    }
                }
            }
        }
        /// <summary>
        /// 添加特殊要求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiRequirements_Click(object sender, EventArgs e)
        {
            string odpNo = txtODPNo.Text;
            if (odpNo.Length == 0) return;
            Project objProject = objProjectService.GetProjectByODPNo(odpNo,sbu);
            if (objProject == null) return;
            FrmRequirements objFrmRequirements = new FrmRequirements(objProject);
            objFrmRequirements.ShowDialog();
        }
    }
}
