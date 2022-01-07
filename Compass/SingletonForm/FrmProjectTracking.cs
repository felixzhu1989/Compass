using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common;
using Models;
using DAL;

namespace Compass
{
    public partial class FrmProjectTracking : Form
    {
        private readonly SqlDataPager _objSqlDataPager = null;
        private readonly ProjectTrackingService _objProjectTrackingService = new ProjectTrackingService();
        private readonly ProjectStatusService _objProjectStatusService = new ProjectStatusService();
        private readonly ProjectService _objProjectService = new ProjectService();
        private readonly string _sbu = Program.ObjCurrentUser.SBU;
        private readonly KeyValuePair _pair = new KeyValuePair();
        public FrmProjectTracking()
        {
            InitializeComponent();
            toolTip.SetToolTip(cobQueryYear, "按照项目完工日期年度查询");
            IniProjectStatus(cobProjectStatus);
            IniCobOdpNo();
            dgvProjectTracking.AutoGenerateColumns = false;
            grbEditProjectTracking.Visible = false;
            //查询年度初始化
            int currentYear = DateTime.Now.Year;
            cobQueryYear.Items.Add(currentYear + 1);//先添加下一年
            for (int i = 0; i <= currentYear - 2020; i++)
            {
                cobQueryYear.Items.Add(currentYear - i);
            }
            cobQueryYear.SelectedIndex = 1;//默认定位当前年份
            //设置默认的显示条数
            cobRecordList.SelectedIndex = 0;
            //初始无数据禁用相关按钮,考虑用户体验
            btnToPage.Enabled = false;
            btnFirst.Enabled = false;
            btnPre.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
            //分页查询
            _objSqlDataPager = _objProjectTrackingService.GetSqlDataPager(_sbu);
            BtnQueryByYear_Click(null, null);
            SetPermissions();
        }
        public void IniCobOdpNo()
        {
            IniOdpNo(cobODPNo);
            IniOdpNo(cobEditODPNo);
            void IniOdpNo(ComboBox cobItem)
            {
                //绑定ODPNo下拉框
                cobItem.DataSource = _objProjectService.GetProjectsByWhereSql("", _sbu);
                cobItem.DisplayMember = "ODPNo";
                cobItem.ValueMember = "ProjectId";
                cobItem.SelectedIndex = -1;//默认不要选中
            }
        }
        /// <summary>
        /// 初始化项目状态下拉框
        /// </summary>
        /// <param name="cobItem"></param>
        private void IniProjectStatus(ComboBox cobItem)
        {
            cobProjectStatus.SelectedIndexChanged -= new EventHandler(CobProjectStatus_SelectedIndexChanged);
            cobItem.DataSource = _objProjectStatusService.GetAllProjectStatus();
            cobItem.DisplayMember = "ProjectStatusName";
            cobItem.ValueMember = "ProjectStatusId";
            cobItem.SelectedIndex = -1;//默认不要选中
            cobProjectStatus.SelectedIndexChanged += new EventHandler(CobProjectStatus_SelectedIndexChanged);
        }
        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            //管理员才能编辑跟踪信息
            if (Program.ObjCurrentUser.UserGroupId == 1)
            {
                tsmiEditProjectTracking.Visible = true;
                //this.dgvProjectTracking.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvProjectTracking_CellDoubleClick);
            }
            else
            {
                tsmiEditProjectTracking.Visible = false;
                //this.dgvProjectTracking.CellDoubleClick -= new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvProjectTracking_CellDoubleClick);
            }
        }
        /// <summary>
        /// 执行查询的公共方法
        /// </summary>
        private void Query()
        {
            //开启所有的按钮
            btnToPage.Enabled = true;
            btnFirst.Enabled = true;
            btnPre.Enabled = true;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            //【1】设置分页查询的条件
            //objSqlDataPager.Condition = string.Format("ShippingTime>='{0}/01/01' and ShippingTime<='{0}/12/31'", this.cobQueryYear.Text);
            //【2】设置每页显示的条数
            //objSqlDataPager.PageSize = Convert.ToInt32(this.cobRecordList.Text.Trim());
            //【3】执行查询
            dgvProjectTracking.DataSource = _objSqlDataPager.GetPagedData();
            //【4】显示记录总数，显示总页数，显示当前页码
            lblRecordsCound.Text = _objSqlDataPager.RecordCount.ToString();
            lblPageCount.Text = _objSqlDataPager.TotalPages.ToString();
            if (_objSqlDataPager.RecordCount == 0)
            {
                lblCurrentPage.Text = "0";
            }
            else
            {
                lblCurrentPage.Text = _objSqlDataPager.CurrentPage.ToString();
            }
            //禁用按钮的情况
            if (lblPageCount.Text == "0" || lblPageCount.Text == "1")
            {
                btnToPage.Enabled = false;
                btnFirst.Enabled = false;
                btnPre.Enabled = false;
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            else
            {
                btnToPage.Enabled = true;
            }
        }

        private void QueryByYear()
        {
            _objSqlDataPager.Condition = string.Format("ShippingTime>='{0}/01/01' and ShippingTime<='{0}/12/31'", cobQueryYear.Text);
            _objSqlDataPager.PageSize = Convert.ToInt32(cobRecordList.Text.Trim());
            Query();
        }

        //private void QureyAll()
        //{
        //    objSqlDataPager.Condition = "ShippingTime>='2020/01/01'";
        //    objSqlDataPager.PageSize = 10000;
        //    Query();
        //}



        /// <summary>
        /// dgv添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvProjectTracking_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(dgvProjectTracking, e);
            if (e.RowIndex > -1)
            {
                string projectStatus = dgvProjectTracking.Rows[e.RowIndex].Cells["ProjectStatusName"].Value.ToString();
                dgvProjectTracking.Rows[e.RowIndex].DefaultCellStyle.BackColor = _pair.ProjectStatusColorKeyValue.First(q => q.Key == projectStatus).Value;
            }
        }

        /// <summary>
        /// 根据项目状态查询跟踪记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQueryByProjectStatus_Click(object sender, EventArgs e)
        {
            if (cobProjectStatus.SelectedIndex == -1) return;
            _objSqlDataPager.Condition =
                $"ProjectTracking{_sbu}.ProjectStatusId = {cobProjectStatus.SelectedValue}";
            _objSqlDataPager.PageSize = 10000;
            Query();
        }
        /// <summary>
        /// 选择项目状态后直接查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobProjectStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnQueryByProjectStatus_Click(null, null);
        }
        /// <summary>
        /// 根据项目号查询跟踪记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnQueryByProjectId_Click(object sender, EventArgs e)
        {
            if (cobODPNo.SelectedIndex == -1) return;
            _objSqlDataPager.Condition = $"ProjectTracking{_sbu}.ProjectId = {cobODPNo.SelectedValue}";
            _objSqlDataPager.PageSize = 10000;
            Query();
        }
        /// <summary>
        /// 选中行回填项目号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvProjectTracking_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProjectTracking.RowCount == 0) return;
            if (dgvProjectTracking.CurrentRow == null) return;
            cobODPNo.Text = dgvProjectTracking.CurrentRow.Cells["ODPNo"].Value.ToString();
        }

        /// <summary>
        /// 修改跟踪记录菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiEditProjectTracking_Click(object sender, EventArgs e)
        {
            if (dgvProjectTracking.RowCount == 0)
            {
                return;
            }
            if (dgvProjectTracking.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的跟踪记录", "提示信息");
                return;
            }

            string projectTrackingId = dgvProjectTracking.CurrentRow.Cells["ProjectTrackingId"].Value.ToString();
            ProjectTracking objProjectTracking = _objProjectTrackingService.GetProjectTrackingById(projectTrackingId, _sbu);
            //初始化修改信息
            grbEditProjectTracking.Visible = true;//显示修改框
            grbEditProjectTracking.Location = new Point(10, 9);
            btnEditProjectTracking.Tag = objProjectTracking.ProjectTrackingId.ToString();

            IniProjectStatus(cobEditProjectStatus);

            cobEditODPNo.Text = objProjectTracking.ODPNo;
            cobEditProjectStatus.Text = objProjectTracking.ProjectStatusName;

            //断开事件委托
            dtpEditDrReleaseActual.ValueChanged -= new EventHandler(DtpEditDrReleaseActual_ValueChanged);
            dtpEditProdFinishActual.ValueChanged -= new EventHandler(DtpEditProdFinishActual_ValueChanged);
            dtpEditDeliverActual.ValueChanged -= new EventHandler(DtpEditDeliverActual_ValueChanged);

            dtpEditDrReleaseActual.Text = objProjectTracking.DrReleaseActual == DateTime.MinValue ? Convert.ToDateTime("1/1/2020").ToShortDateString() : objProjectTracking.DrReleaseActual.ToShortDateString();
            dtpEditProdFinishActual.Text = objProjectTracking.ProdFinishActual == DateTime.MinValue ? Convert.ToDateTime("1/1/2020").ToShortDateString() : objProjectTracking.ProdFinishActual.ToShortDateString();
            dtpEditDeliverActual.Text = objProjectTracking.DeliverActual == DateTime.MinValue ? Convert.ToDateTime("1/1/2020").ToShortDateString() : objProjectTracking.DeliverActual.ToShortDateString();

            //重新建立事件委托
            dtpEditDrReleaseActual.ValueChanged += new EventHandler(DtpEditDrReleaseActual_ValueChanged);
            dtpEditProdFinishActual.ValueChanged += new EventHandler(DtpEditProdFinishActual_ValueChanged);
            dtpEditDeliverActual.ValueChanged += new EventHandler(DtpEditDeliverActual_ValueChanged);

            dtpEditODPReceiveDate.Text = objProjectTracking.ODPReceiveDate == DateTime.MinValue ? Convert.ToDateTime("1/1/2020").ToShortDateString() : objProjectTracking.ODPReceiveDate.ToShortDateString();
            dtpEditKickOffDate.Text = objProjectTracking.KickOffDate == DateTime.MinValue ? Convert.ToDateTime("1/1/2020").ToShortDateString() : objProjectTracking.KickOffDate.ToShortDateString();
            SetProjectStatus();
        }
        /// <summary>
        /// 提交修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditProjectTracking_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (cobEditProjectStatus.SelectedIndex == -1)
            {
                MessageBox.Show("请选择项目状态", "验证信息");
                cobEditProjectStatus.Focus();
                return;
            }
            if (cobEditODPNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择项目编号，如果没有，请到项目列表中添加后再选择", "验证信息");
                cobEditODPNo.Focus();
                return;
            }

            //验证日期顺序的正确性
            if (dtpEditDrReleaseActual.Value.ToString("MM/dd/yyyy") != "01/01/2020" && dtpEditProdFinishActual.Value.ToString("MM/dd/yyyy") != "01/01/2020" && DateTime.Compare(dtpEditDrReleaseActual.Value, dtpEditProdFinishActual.Value) > 0)
            {
                MessageBox.Show("实际发图日期不能大于实际完工日期，请认真检查", "验证信息");
                return;
            }
            if (dtpEditProdFinishActual.Value.ToString("MM/dd/yyyy") != "01/01/2020" && dtpEditDeliverActual.Value.ToString("MM/dd/yyyy") != "01/01/2020" && DateTime.Compare(dtpEditProdFinishActual.Value, dtpEditDeliverActual.Value) > 0)
            {
                MessageBox.Show("实际完工日期不能大于实际发货日期，请认真检查", "验证信息");
                return;
            }
            #endregion
            int firstRowIndex = dgvProjectTracking.CurrentRow.Index;
            //封装项目跟踪对象
            ProjectTracking objProjectTracking = new ProjectTracking()
            {
                ProjectTrackingId = Convert.ToInt32(btnEditProjectTracking.Tag),
                ProjectId = Convert.ToInt32(cobEditODPNo.SelectedValue),
                ProjectStatusId = Convert.ToInt32(cobEditProjectStatus.SelectedValue),
                DrReleaseActual = Convert.ToDateTime(dtpEditDrReleaseActual.Text) == Convert.ToDateTime("1/1/2020") ? DateTime.MinValue : Convert.ToDateTime(dtpEditDrReleaseActual.Text),
                ProdFinishActual = Convert.ToDateTime(dtpEditProdFinishActual.Text) == Convert.ToDateTime("1/1/2020") ? DateTime.MinValue : Convert.ToDateTime(dtpEditProdFinishActual.Text),
                DeliverActual = Convert.ToDateTime(dtpEditDeliverActual.Text) == Convert.ToDateTime("1/1/2020") ? DateTime.MinValue : Convert.ToDateTime(dtpEditDeliverActual.Text),
                ODPReceiveDate = Convert.ToDateTime(dtpEditODPReceiveDate.Text) == Convert.ToDateTime("1/1/2020") ? DateTime.MinValue : Convert.ToDateTime(dtpEditODPReceiveDate.Text),
                KickOffDate = Convert.ToDateTime(dtpEditKickOffDate.Text) == Convert.ToDateTime("1/1/2020") ? DateTime.MinValue : Convert.ToDateTime(dtpEditKickOffDate.Text)
            };
            //调用后台方法修改对象
            try
            {
                if (_objProjectTrackingService.EditProjectTracing(objProjectTracking, _sbu) == 1)
                {
                    grbEditProjectTracking.Visible = false;
                    BtnQueryByYear_Click(null, null);
                    SingletonObject.GetSingleton.FrmP?.BtnQueryByYear_Click(null, null);
                    SingletonObject.GetSingleton.FrmDp?.BtnQueryByYear_Click(null, null);
                    MessageBox.Show("修改计划成功！", "提示信息");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgvProjectTracking.ClearSelection();

            dgvProjectTracking.Rows[firstRowIndex].Selected = true;//将刚修改的行选中
            dgvProjectTracking.FirstDisplayedScrollingRowIndex = firstRowIndex;//将修改的行显示在第一行
        }

        /// <summary>
        /// 更改实际发图日期时，项目状态自动切换成生产中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpEditDrReleaseActual_ValueChanged(object sender, EventArgs e)
        {
            SetProjectStatus();
        }
        /// <summary>
        /// 更改实际完工日期后，项目状态自动切换成生产完工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpEditProdFinishActual_ValueChanged(object sender, EventArgs e)
        {
            SetProjectStatus();
        }
        /// <summary>
        /// 更改实际发货日期后，项目状态自动切换成项目完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpEditDeliverActual_ValueChanged(object sender, EventArgs e)
        {
            SetProjectStatus();
        }

        void SetProjectStatus()
        {
            if (Convert.ToDateTime(dtpEditDeliverActual.Text) != Convert.ToDateTime("1/1/2020"))
            {
                cobEditProjectStatus.SelectedValue = 6;
            }
            else
            {
                if (Convert.ToDateTime(dtpEditProdFinishActual.Text) != Convert.ToDateTime("1/1/2020"))
                {
                    cobEditProjectStatus.SelectedValue = 5;
                }
                else
                {
                    cobEditProjectStatus.SelectedValue = Convert.ToDateTime(dtpEditDrReleaseActual.Text) != Convert.ToDateTime("1/1/2020") ? 4 : 3;
                }
            }
        }

        /// <summary>
        /// 根据年份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnQueryByYear_Click(object sender, EventArgs e)
        {
            if (cobQueryYear.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要查询的年度", "提示信息");
                return;
            }
            _objSqlDataPager.CurrentPage = 1;//每次执行查询都必须设置为第一页
            QueryByYear();
            //禁用上一页按钮
            btnFirst.Enabled = false;
            btnPre.Enabled = false;
        }
        /// <summary>
        /// 第一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFirst_Click(object sender, EventArgs e)
        {
            _objSqlDataPager.CurrentPage = 1;//每次执行查询都必须设置为第一页
            QueryByYear();
            //禁用上一页按钮和第一页
            btnFirst.Enabled = false;
            btnPre.Enabled = false;
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPre_Click(object sender, EventArgs e)
        {
            _objSqlDataPager.CurrentPage -= 1;//在当前页码上减一
            QueryByYear();
            //禁用下一页和最后一页按钮
            if (_objSqlDataPager.CurrentPage == 1)
            {
                btnFirst.Enabled = false;
                btnPre.Enabled = false;
            }
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNext_Click(object sender, EventArgs e)
        {
            _objSqlDataPager.CurrentPage += 1;//在当前页码上加一
            QueryByYear();
            //禁用下一页和最后一页按钮
            if (_objSqlDataPager.CurrentPage == _objSqlDataPager.TotalPages)
            {
                btnLast.Enabled = false;
                btnNext.Enabled = false;
            }
        }
        /// <summary>
        /// 最后一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLast_Click(object sender, EventArgs e)
        {
            _objSqlDataPager.CurrentPage = _objSqlDataPager.TotalPages;//在当前页码上加一
            QueryByYear();
            //禁用下一页和最后一页按钮
            btnLast.Enabled = false;
            btnNext.Enabled = false;
        }
        /// <summary>
        /// 跳转到
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnToPage_Click(object sender, EventArgs e)
        {
            int a = txtToPage.IsInteger("跳转的页码");
            if (a != 0)
            {
                int toPage = Convert.ToInt32(txtToPage.Text.Trim());
                if (toPage > _objSqlDataPager.TotalPages)
                {
                    BtnLast_Click(null, null);//直接为最后一页
                }
                else if (toPage == 0)
                {
                    BtnFirst_Click(null, null);//第一页
                }
                else
                {
                    _objSqlDataPager.CurrentPage = toPage;//跳转到给定页码
                    Query();
                    if (_objSqlDataPager.CurrentPage == 1)
                    {
                        //禁用上一页按钮和第一页
                        btnFirst.Enabled = false;
                        btnPre.Enabled = false;
                    }
                    else if (_objSqlDataPager.CurrentPage == _objSqlDataPager.TotalPages)
                    {
                        //禁用下一页和最后一页按钮
                        btnLast.Enabled = false;
                        btnNext.Enabled = false;
                    }
                }
            }
        }

        private void TsmiShowProjectInfo_Click(object sender, EventArgs e)
        {
            if (dgvProjectTracking.RowCount == 0) return;
            if (dgvProjectTracking.CurrentRow == null) return;
            string odpNo = dgvProjectTracking.CurrentRow.Cells["ODPNo"].Value.ToString();
            FrmProjectInfo form = FrmProjectInfo.GetInstance();
            form.WindowState = FormWindowState.Maximized;
            form.HandlerOdpNo(odpNo);
            form.Show();
            form.Focus();
        }
    }
}
