using System;
using System.Drawing;
using System.Windows.Forms;
using DAL;
using Models;
using Common;

namespace Compass
{
    public partial class FrmDrawingPlan : Form
    {
        private SqlDataPager objSqlDataPager = null;
        private DrawingPlanService objDrawingPlanService = new DrawingPlanService();
        private UserService objUserService = new UserService();
        private DesignWorkloadService objDesignWorkloadService = new DesignWorkloadService();
        private ProjectService objProjectService = new ProjectService();
        private string sbu = Program.ObjCurrentUser.SBU;

        public FrmDrawingPlan()
        {
            InitializeComponent();
            toolTip.SetToolTip(cobQueryYear, "按照项目完工日期年度查询");
            IniUserId(cobUserId);
            IniModel(cobModel);
            IniModel(cobEditModel);
            IniCobODPNo();
            dgvDrawingPlan.AutoGenerateColumns = false;
            grbEditDrawingPlan.Visible = false;

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

            //分页查询
            objSqlDataPager = objDrawingPlanService.GetSqlDataPager(sbu);

            BtnQueryByYear_Click(null, null);
            SetPermissions();
        }
        public void IniCobODPNo()
        {
            IniODPNo(cobODPNo);
            IniODPNo(cobEditODPNo);
            void IniODPNo(ComboBox cobItem)
            {
                //绑定ODPNo下拉框
                cobItem.DataSource = objProjectService.GetProjectsByWhereSql("", sbu);
                cobItem.DisplayMember = "ODPNo";
                cobItem.ValueMember = "ProjectId";
                cobItem.SelectedIndex = -1;//默认不要选中
            }
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
            //管理员才能添加、编辑、删除制图计划
            if (Program.ObjCurrentUser.UserGroupId == 1)
            {
                btnAddDrawingPlan.Visible = true;
                tsmiEditDrawingPlan.Visible = true;
                tsmiDeleteDrawingPlan.Visible = true;
                this.dgvDrawingPlan.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDrawingPlan_CellDoubleClick);
                this.dgvDrawingPlan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvDrawingPlan_KeyDown);
            }
            else
            {
                btnAddDrawingPlan.Visible = false;
                tsmiEditDrawingPlan.Visible = false;
                tsmiDeleteDrawingPlan.Visible = false;
                this.dgvDrawingPlan.CellDoubleClick -= new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDrawingPlan_CellDoubleClick);
                this.dgvDrawingPlan.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.DgvDrawingPlan_KeyDown);
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
            this.dgvDrawingPlan.DataSource = objSqlDataPager.GetPagedData();
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
            objSqlDataPager.Condition = string.Format("DrawingPlan{0}.DrReleasetarget>='{1}/01/01' and DrawingPlan{0}.DrReleasetarget<='{1}/12/31'",sbu, this.cobQueryYear.Text);
            objSqlDataPager.PageSize = Convert.ToInt32(this.cobRecordList.Text.Trim());
            Query();
        }
        private void QureyAll()
        {
            objSqlDataPager.Condition = $"DrawingPlan{sbu}.DrReleasetarget>='2020/01/01'";
            objSqlDataPager.PageSize = 10000;
            Query();
        }

        /// <summary>
        /// 显示全部计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQueryAllPlan_Click(object sender, EventArgs e)
        {
            QureyAll();
        }
        /// <summary>
        /// 初始化制图人员下拉框
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
        /// 初始化烟罩型号下拉框，并绑定工作量
        /// </summary>
        /// <param name="cobItem"></param>
        private void IniModel(ComboBox cobItem)
        {
            cobItem.DataSource = objDesignWorkloadService.GetAllDesignWorkload(sbu);
            cobItem.DisplayMember = "Model";
            cobItem.ValueMember = "WorkloadValue";
            cobItem.SelectedIndex = -1;//默认不要选中
        }
        /// <summary>
        /// dgv添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvDrawingPlan_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvDrawingPlan, e);
            if (e.RowIndex > -1)
            {
                int remainingDays = (int)this.dgvDrawingPlan.Rows[e.RowIndex].Cells["RemainingDays"].Value;
                if (remainingDays != 0) return;
                DateTime drReleaseActual = new DateTime(01 / 01 / 0001);//初始化赋值
                if (this.dgvDrawingPlan.Rows[e.RowIndex].Cells["DrReleaseActual"].Value.ToString().Length!=0)
                drReleaseActual = (DateTime)this.dgvDrawingPlan.Rows[e.RowIndex].Cells["DrReleaseActual"].Value;//单元格不为空则赋值
                DateTime drReleaseTarget = (DateTime)this.dgvDrawingPlan.Rows[e.RowIndex].Cells["DrReleaseTarget"].Value;
                if (drReleaseActual.ToString("MM/dd/yyyy") == "01/01/0001")
                    dgvDrawingPlan.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                if (DateTime.Compare(drReleaseActual, drReleaseTarget) > 0)
                    dgvDrawingPlan.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            }
        }
        /// <summary>
        /// 模型选择变化时，计算subtotalworkload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtModuleNo.Text.Trim().Length == 0) return;//分段没有输入，不计算
            if (cobModel.SelectedIndex == -1) return;//模型没有选择，不计算
            txtSubTotalWorkload.Text = (Convert.ToDecimal(txtModuleNo.Text.Trim())
                * Convert.ToDecimal(cobModel.SelectedValue)).ToString();
        }
        /// <summary>
        /// 烟罩分段数量变化时，计算subtotalworkload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtModuleNo_TextChanged(object sender, EventArgs e)
        {
            if (!DataValidate.IsInteger(txtModuleNo.Text.Trim()))
            {
                //MessageBox.Show("烟罩分段数量必须是数字", "验证信息");
                txtModuleNo.Text = "";
                txtModuleNo.Focus();
                return;
            }
            if (txtModuleNo.Text.Trim().Length == 0) return;//分段没有输入，不计算
            if (cobModel.SelectedIndex == -1) return;//模型没有选择，不计算
            txtSubTotalWorkload.Text = (Convert.ToDecimal(txtModuleNo.Text.Trim())
                * Convert.ToDecimal(cobModel.SelectedValue)).ToString();
        }
        /// <summary>
        /// 添加计划记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddDrawingPlan_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (cobModel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择烟罩型号", "验证信息");
                cobModel.Focus();
                return;
            }
            if (txtModuleNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输烟罩分段数量", "验证信息");
                txtModuleNo.Focus();
                return;
            }
            if (cobODPNo.SelectedIndex == -1)
            {
                MessageBox.Show("请输入选择或输入项目编号，如果没有，请到项目列表中添加后再选择", "验证信息");
                cobODPNo.Focus();
                return;
            }
            //验证数量是数字
            if (!DataValidate.IsInteger(txtModuleNo.Text.Trim()))
            {
                MessageBox.Show("烟罩分段数量必须是数字", "验证信息");
                txtModuleNo.Focus();
                txtModuleNo.SelectAll();
                return;
            }
            #endregion
            //封装制图计划对象
            DrawingPlan objDrawingPlan = new DrawingPlan()
            {
                ProjectId = Convert.ToInt32(cobODPNo.SelectedValue),
                Item = txtItem.Text.Trim(),
                Model = cobModel.Text,
                ModuleNo = Convert.ToInt32(txtModuleNo.Text.Trim()),
                DrReleaseTarget = Convert.ToDateTime(dtpDrReleaseTarget.Text),
                SubTotalWorkload = Convert.ToDecimal(txtSubTotalWorkload.Text.Trim())
            };
            //提交添加
            try
            {
                //添加制图计划同时更新项目跟踪记录为制图中
                //添加制图计划同时更新项目跟踪记录为制图中
                if (objDrawingPlanService.AddDraingPlanAndUpdateTracking(objDrawingPlan, sbu))
                {
                    //刷新显示
                    BtnQueryByYear_Click(null, null);
                    SingletonObject.GetSingleton().FrmPT.BtnQueryByYear_Click(null, null);
                    SingletonObject.GetSingleton().FrmMT.RefreshTree();
                    //清空内容
                    txtSubTotalWorkload.Text = "";
                    txtModuleNo.Text = "";
                    txtItem.Text = "";
                    txtItem.Focus();
                    //提示添加成功
                    MessageBox.Show("制图计划添加成功", "提示信息");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 根据项目ID查询制图计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQueryByProjectId_Click(object sender, EventArgs e)
        {
            if (cobODPNo.SelectedIndex == -1) return;
            objSqlDataPager.Condition = $"DrawingPlan{sbu}.ProjectId={cobODPNo.SelectedValue}";
            objSqlDataPager.PageSize = 10000;
            Query();
        }
        
        /// <summary>
        /// 修改计划菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiEditDrawingPlan_Click(object sender, EventArgs e)
        {
            if (dgvDrawingPlan.RowCount == 0)
            {
                return;
            }
            if (dgvDrawingPlan.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的计划条目", "提示信息");
                return;
            }
            string drawingPlanId = dgvDrawingPlan.CurrentRow.Cells["DrawingPlanId"].Value.ToString();
            DrawingPlan objDrawingPlan = objDrawingPlanService.GetDrawingPlanById(drawingPlanId,sbu);
            //初始化修改信息
            grbEditDrawingPlan.Visible = true;//显示修改框
            grbEditDrawingPlan.Location = new Point(10, 9);
            btnEditDrawingPlan.Tag = objDrawingPlan.DrawingPlanId;
            txtEditItem.Text = objDrawingPlan.Item;

            txtEditModuleNo.Text = objDrawingPlan.ModuleNo.ToString();
            cobEditModel.Text = objDrawingPlan.Model;
            cobEditODPNo.Text = objDrawingPlan.ODPNo;

            txtEditSubTotalWorkload.Text = objDrawingPlan.SubTotalWorkload.ToString();
            dtpEditDrReleaseTarget.Text = objDrawingPlan.DrReleaseTarget.ToShortDateString();
            dtpEditAddedDate.Text = objDrawingPlan.AddedDate.ToShortDateString();
        }
        /// <summary>
        /// 双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvDrawingPlan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TsmiEditDrawingPlan_Click(null, null);
        }
        /// <summary>
        /// 同步计算工作量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobEditModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtEditModuleNo.Text.Trim().Length == 0) return;//分段没有输入，不计算
            if (cobEditModel.SelectedIndex == -1) return;//模型没有选择，不计算
            txtEditSubTotalWorkload.Text = (Convert.ToDecimal(txtEditModuleNo.Text.Trim())
                                        * Convert.ToDecimal(cobEditModel.SelectedValue)).ToString();
        }
        /// <summary>
        /// 同步计算工作量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtEditModuleNo_TextChanged(object sender, EventArgs e)
        {
            if (!DataValidate.IsInteger(txtEditModuleNo.Text.Trim()))
            {
                //MessageBox.Show("烟罩分段数量必须是数字", "验证信息");
                txtEditModuleNo.Text = "";
                txtEditModuleNo.Focus();
                return;
            }
            if (txtEditModuleNo.Text.Trim().Length == 0) return;//分段没有输入，不计算
            if (cobEditModel.SelectedIndex == -1) return;//模型没有选择，不计算
            txtEditSubTotalWorkload.Text = (Convert.ToDecimal(txtEditModuleNo.Text.Trim())
                                        * Convert.ToDecimal(cobEditModel.SelectedValue)).ToString();
        }
        /// <summary>
        /// 提交修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditDrawingPlan_Click(object sender, EventArgs e)
        {
            #region 数据验证

            if (cobEditModel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择烟罩型号", "验证信息");
                cobEditModel.Focus();
                return;
            }
            if (txtEditModuleNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输烟罩分段数量", "验证信息");
                txtEditModuleNo.Focus();
                return;
            }
            if (cobEditODPNo.SelectedIndex == -1)
            {
                MessageBox.Show("请输入选择或输入项目编号，如果没有，请到项目列表中添加后再选择", "验证信息");
                cobEditODPNo.Focus();
                return;
            }
            //验证数量是数字
            if (!DataValidate.IsInteger(txtEditModuleNo.Text.Trim()))
            {
                MessageBox.Show("烟罩分段数量必须是数字", "验证信息");
                txtEditModuleNo.Focus();
                txtEditModuleNo.SelectAll();
                return;
            }
            //验证计划日期大于添加日期
            if (DateTime.Compare(dtpEditDrReleaseTarget.Value, dtpEditAddedDate.Value) < 0)
            {
                MessageBox.Show("计划发图日期必须大于添加日期，请认真检查", "验证信息");
                return;
            }
            #endregion
            int firstRowIndex = dgvDrawingPlan.CurrentRow.Index;
            //封装制图计划对象
            DrawingPlan objDrawingPlan = new DrawingPlan()
            {
                DrawingPlanId = Convert.ToInt32(btnEditDrawingPlan.Tag),
                ProjectId = Convert.ToInt32(cobEditODPNo.SelectedValue),
                Item = txtEditItem.Text.Trim(),
                Model = cobEditModel.Text,
                ModuleNo = Convert.ToInt32(txtEditModuleNo.Text.Trim()),
                DrReleaseTarget = Convert.ToDateTime(dtpEditDrReleaseTarget.Text),
                SubTotalWorkload = Convert.ToDecimal(txtEditSubTotalWorkload.Text.Trim()),
                AddedDate = Convert.ToDateTime(dtpEditAddedDate.Text.Trim())
            };
            //调用后台方法修改对象
            try
            {
                if (objDrawingPlanService.EditDrawingPlan(objDrawingPlan,sbu) == 1)
                {
                    grbEditDrawingPlan.Visible = false;
                    BtnQueryByYear_Click(null, null);//同步刷新显示数据
                    SingletonObject.GetSingleton().FrmPT.BtnQueryByYear_Click(null, null);
                    SingletonObject.GetSingleton().FrmMT.RefreshTree();
                    MessageBox.Show("修改计划成功！", "提示信息");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgvDrawingPlan.ClearSelection();
            dgvDrawingPlan.Rows[firstRowIndex].Selected = true;
            dgvDrawingPlan.FirstDisplayedScrollingRowIndex = firstRowIndex;
        }
        /// <summary>
        /// 删除计划菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiDeleteDrawingPlan_Click(object sender, EventArgs e)
        {
            if (dgvDrawingPlan.RowCount == 0)
            {
                return;
            }
            if (dgvDrawingPlan.CurrentRow == null)
            {
                MessageBox.Show("请选中需要删除的计划记录", "验证信息");
                return;
            }
            string drawingPlanId = dgvDrawingPlan.CurrentRow.Cells["DrawingPlanId"].Value.ToString();
            string odpNo = dgvDrawingPlan.CurrentRow.Cells["ODPNo"].Value.ToString();
            string item = dgvDrawingPlan.CurrentRow.Cells["Item"].Value.ToString();
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除（项目编号ODP： " + odpNo + " 烟罩编号Item：" + item + " ）这条计划吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            int firstRowIndex = dgvDrawingPlan.CurrentRow.Index;
            try
            {
                if (objDrawingPlanService.DeleteDrawingPlan(drawingPlanId,sbu) == 1)
                {
                    BtnQueryAllPlan_Click(null, null);//同步刷新显示数据
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            grbEditDrawingPlan.Visible = false;
            dgvDrawingPlan.ClearSelection();
            if (firstRowIndex < dgvDrawingPlan.RowCount)
            {
                dgvDrawingPlan.Rows[firstRowIndex].Selected = true; //将刚修改的行选中
                dgvDrawingPlan.FirstDisplayedScrollingRowIndex = firstRowIndex;//将修改的行显示在第一行
            }
        }
        /// <summary>
        /// 按下删除键执行删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvDrawingPlan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) TsmiDeleteDrawingPlan_Click(null, null);
        }
        /// <summary>
        /// 输入Item后输入数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtItem.Text.Trim().Length == 0)
            {
                txtItem.Focus();
                return;
            }
            if (e.KeyValue == 13)
            {
                txtModuleNo.Focus();
                txtModuleNo.SelectAll();
            }
        }
        /// <summary>
        /// 输入数量后直接提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtModuleNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtModuleNo.Text.Trim().Length == 0)
            {
                txtModuleNo.Focus();
                return;
            }
            if (e.KeyValue == 13)
            {
                BtnAddDrawingPlan_Click(null, null);
            }
        }
        
        /// <summary>
        /// 查询整个订单弹出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiQueryByODP_Click(object sender, EventArgs e)
        {
            BtnQueryByProjectId_Click(null, null);
        }
        /// <summary>
        /// 查询所有计划弹出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiQueryAllPlan_Click(object sender, EventArgs e)
        {
            BtnQueryAllPlan_Click(null, null);
        }

        /// <summary>
        /// 弹出技术要求窗口，添加特殊技术要求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiRequirements_Click(object sender, EventArgs e)
        {
            if (dgvDrawingPlan.RowCount == 0) return;
            if (dgvDrawingPlan.CurrentRow == null) return;
            string odpNo = dgvDrawingPlan.CurrentRow.Cells["ODPNo"].Value.ToString();
            Project objProject = objProjectService.GetProjectByODPNo(odpNo,Program.ObjCurrentUser.SBU);
            if (objProject == null) return;
            FrmRequirements objFrmRequirements = new FrmRequirements(objProject);
            objFrmRequirements.ShowDialog();
        }

        /// <summary>
        /// 根据制图人员查询计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQueryByUserId_Click(object sender, EventArgs e)
        {
            if (cobUserId.SelectedIndex == -1) return;
            objSqlDataPager.Condition = $"Projects{sbu}.UserId={cobUserId.SelectedValue}";
            objSqlDataPager.PageSize = 10000;
            Query();
        }
        /// <summary>
        /// 按回车后根据项目查询计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobODPNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) BtnQueryByProjectId_Click(null, null);
        }
        /// <summary>
        /// 按回车后根据人员查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) BtnQueryByUserId_Click(null, null);
        }
        /// <summary>
        /// 显示统计信息页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDrawingPlanQuery_Click(object sender, EventArgs e)
        {
            FrmDrawingPlanQuery objDrawingPlanQuery = new FrmDrawingPlanQuery();
            objDrawingPlanQuery.Show();
        }

        /// <summary>
        /// 根据年份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnQueryByYear_Click(object sender, EventArgs e)
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
    }
}
