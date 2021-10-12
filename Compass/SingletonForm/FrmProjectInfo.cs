using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common;
using DAL;
using System.Windows.Forms.DataVisualization.Charting;
using Models;
using System.Data;
using System.Text;


namespace Compass
{
    public partial class FrmProjectInfo : MetroFramework.Forms.MetroForm
    {
        private ProjectService objProjectService = new ProjectService();
        private RequirementService objRequirementService = new RequirementService();
        private DrawingPlanService objDrawingPlanService = new DrawingPlanService();
        private ProjectTrackingService objProjectTrackingService = new ProjectTrackingService();
        private FinancialDataService objFinancialDataService = new FinancialDataService();
        private MonthlyReportService objMonthlyReportService = new MonthlyReportService();
        private readonly string sbu = Program.ObjCurrentUser.SBU;
        private List<string> odpNoList = new List<string>();
        private int scrollNum;
        private DateTime scrollStart;
        private KeyValuePair pair = new KeyValuePair();
        private DataTable dt;
        private StringBuilder showText = new StringBuilder();
        public FrmProjectInfo()
        {
            InitializeComponent();
            //初始化年月下拉框
            int currentYear = DateTime.Now.Year;
            cobYear.Items.Add(currentYear + 1);//先添加下一年
            for (int i = 0; i <= currentYear - 2020; i++)
            {
                cobYear.Items.Add(currentYear - i);
            }
            cobYear.SelectedIndex = 1;//默认定位当前年份

            int currentMonth = DateTime.Now.Month;
            for (int i = 0; i < 12; i++)
            {
                cobMonth.Items.Add(i + 1);
            }
            cobMonth.SelectedIndex = currentMonth - 1;//默认定位当前月份
            this.cobYear.SelectedIndexChanged += new System.EventHandler(this.CobYear_SelectedIndexChanged);
            this.cobMonth.SelectedIndexChanged += new System.EventHandler(this.CobMonth_SelectedIndexChanged);
            ReportMonthly();//初始化月度统计数据

            //绑定ODPNo下拉框
            cobODPNo.DataSource = objProjectService.GetProjectsByWhereSql("", Program.ObjCurrentUser.SBU);
            cobODPNo.DisplayMember = "ODPNo";
            cobODPNo.ValueMember = "ProjectId";
            //初始化后关联事件委托
            this.cobODPNo.SelectedIndexChanged += new System.EventHandler(this.CobODPNo_SelectedIndexChanged);
            SetPermissions();

            //初始化项目列表
            dgvProjects.AutoGenerateColumns = false;
            dt = objMonthlyReportService.GetDisplayProjects(sbu, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
            dgvProjects.DataSource = AddChinese(dt);
            tabControl.SelectTab(1);//选中第二张tab选项卡

            //开启计时器
            timerScroll.Enabled = true;
            odpNoList = objMonthlyReportService.GetScrollODPNoList();//获取循环ODP列表
            scrollNum = odpNoList.Count;
            scrollStart = DateTime.Now;
            lblTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 hh:mm:ss");
            cobODPNo.Text = odpNoList[odpNoList.Count - 1];
            cobODPNo.SelectAll();
        }
        #region 单例模式，重写关闭方法，显示时选择ODP号
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        public void ShowWithOdpNo(string odpNo)
        {
            if (odpNo.Length != 0) cobODPNo.Text = odpNo;
            this.cobODPNo.SelectedIndexChanged -= new System.EventHandler(this.CobODPNo_SelectedIndexChanged);
            InitData();//初始化项目数据
            this.cobODPNo.SelectedIndexChanged += new System.EventHandler(this.CobODPNo_SelectedIndexChanged);
            cobODPNo.Focus();
            tabControl.SelectTab(0);//选中第一张tab选项卡
            timerScroll.Enabled = false;//关闭循环
            btnScroll.Text = "开始循环";
            this.Show();
        }
        #endregion

        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            //管理员和技术部才能查看财务数据/列表显示金额
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2)
            {
                grbFinancialData.Visible = true;
                //this.dgvProjects.Columns["SalesValue"].Visible = true;
            }
            else
            {
                grbFinancialData.Visible = false;
                //this.dgvProjects.Columns["SalesValue"].Visible = false;
            }
        }

        #region 其他操作
        /// <summary>
        /// 手动输入项目号按回车查询项目信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobODPNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) InitData();
        }
        /// <summary>
        /// ODP选择变更自动初始化信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobODPNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitData();
        }
        /// <summary>
        /// 弹出技术要求窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiRequirement_Click(object sender, EventArgs e)
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
        private void DgvScope_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvScope, e);
        }
        /// <summary>
        /// 输入金额千位分隔符显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSalesValue_TextChanged(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtSalesValue.Text.Trim(), out decimal salseValue))
            {
                txtSalesValue.Clear();
                return;
            }
            txtSalesValue.Text = salseValue.ToString("N0");
            txtSalesValue.SelectionStart = txtSalesValue.Text.Length;
        }
        /// <summary>
        /// 年份选择后更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportYearly();
        }
        /// <summary>
        /// 月份选择后更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportMonthly();
        }
        /// <summary>
        /// 定时器，循环项目和统计信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerScroll_Tick(object sender, EventArgs e)
        {
            //DateTime scrollEnd = scrollStart.AddSeconds(odpNoList.Count*2);//调试
            DateTime scrollEnd = scrollStart.AddMinutes(odpNoList.Count * 2);
            if (DateTime.Now < scrollEnd)
            {
                //if (DateTime.Now.Second % 2 == 0)//调试
                if (DateTime.Now.Minute % 2 == 0)
                {
                    tabControl.SelectTab(0);
                    if (scrollNum < 1) scrollNum = odpNoList.Count;
                    cobODPNo.Text = odpNoList[scrollNum - 1];
                    scrollNum--;
                }
                else
                {
                    tabControl.SelectTab(1);//选中第二张tab选项卡 
                }
            }
            else
            {
                scrollStart = DateTime.Now;
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 hh:mm:ss");
        }
        /// <summary>
        /// 暂停/开启循环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnScroll_Click(object sender, EventArgs e)
        {
            if (btnScroll.Text == "暂停循环")
            {
                timerScroll.Enabled = false;
                btnScroll.Text = "开始循环";
            }
            else
            {
                timerScroll.Enabled = true;
                btnScroll.Text = "暂停循环";
            }
        }
        /// <summary>
        /// 按年和按月切换统计信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSwitch_Click(object sender, EventArgs e)
        {
            if (btnSwitch.Text == "按月")
            {
                btnSwitch.Text = "按年";
                ReportYearly();
                //grbProjectStatus.Text = "项目状态分布--年";
                grbRiskLevel.Text = "风险等级分布--年--(项目数量)";
                grbProjectType.Text = "项目类型分布--年--(销售额单位：万元)";
            }
            else
            {
                btnSwitch.Text = "按月";
                ReportMonthly();
                grbRiskLevel.Text = "风险等级分布--月--(项目数量)";
                grbProjectType.Text = "项目类型分布--月--(销售额单位：万元)";
            }
        }
        /// <summary>
        /// 项目状态，烟罩类型内容需要中英文
        /// </summary>
        /// <param name="table"></param>
        private DataTable AddChinese(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                //获取原来每行ProjectStatusName数据
                string oldStatus = table.Rows[i]["ProjectStatusName"].ToString();
                string newStatus = pair.ProjectStatusNameKeyValue.First(q => q.Key == oldStatus).Value;
                //给当前行的地ProjectStatusName赋值
                table.Rows[i]["ProjectStatusName"] = newStatus;
                //获取原来每行ProjectStatusName数据
                string oldHood = table.Rows[i]["HoodType"].ToString();
                string newHood = pair.HoodTypeKeyValue.First(q => q.Key == oldHood).Value;
                //给当前行的地ProjectStatusName赋值
                table.Rows[i]["HoodType"] = newHood;


            }
            return table;
        }


        #endregion 其他操作

        #region 初始化各种信息
        /// <summary>
        /// 总初始化
        /// </summary>
        private void InitData()
        {
            showText.Clear();
            InitProjectInfo();
            InitGeneralRequirement();
            InitSpecialRequirement();
            InitFinancialData();
            InitTracking();
            lblShowInfo.Text = showText.ToString();
        }

        /// <summary>
        /// 1.初始化项目基本信息
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
            proInfo += "7. 完工日期：" + objProject.ShippingTime.ToShortDateString();
            txtProjectInfo.Text = proInfo;
            dgvScope.DataSource = objDrawingPlanService.GetScopeByDataSet(objProject.ProjectId.ToString(), sbu).Tables[0];
            showText.Append(objProject.ODPNo + " ★完工日期：" + objProject.ShippingTime.ToShortDateString());
        }
        /// <summary>
        /// 2.初始化通用技术要求
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
                showText.Append(" ★项目类型：" + objGeneralRequirement.TypeName);
            }
        }
        /// <summary>
        /// 3.初始化特殊技术要求
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
                StringBuilder sReq = new StringBuilder();
                showText.Append(" ★特殊要求：");
                for (int i = 0; i < specialRequirementList.Count; i++)
                {
                    sReq.Append((i + 1) + ". " + specialRequirementList[i] + "\r\n");
                    showText.Append(" ☆" + (i + 1) + ". " + specialRequirementList[i]);
                }
                txtSpecialRequirements.Text = sReq.ToString();
                showText.Append(" ）");
            }

        }
        /// <summary>
        /// 5.初始化财务数据
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
        /// 添加和更新财务数据按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFinancialData_Click(object sender, EventArgs e)
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
        /// <summary>
        /// 7.初始化跟踪图表
        /// </summary>
        private void InitTracking()
        {
            chartTracking.Series.Clear();
            //重新设置轴最大值
            chartTracking.ChartAreas[0].RecalculateAxesScale();

            Series seriesTracking = new Series
            {
                ChartType = SeriesChartType.Bar
            };
            chartTracking.Series.Add(seriesTracking);
            ProjectTracking projectTracking =
                objProjectTrackingService.GetProjectTrackingByODPNo(cobODPNo.Text, Program.ObjCurrentUser.SBU);

            DateTime dateDeliver = projectTracking.DeliverActual;
            DateTime datePrdFish = projectTracking.ProdFinishActual;
            DateTime dateShiping = projectTracking.ShippingTime;
            DateTime dateDrwRels = projectTracking.DrReleaseActual;
            DateTime dateDrwPlan = projectTracking.DrReleaseTarget;
            DateTime dateKickOff = projectTracking.KickOffDate;
            DateTime dateODPRecv = projectTracking.ODPReceiveDate;

            int daysDeliver = dateDeliver.Subtract(dateODPRecv).Days < 0 ? 0 : dateDeliver.Subtract(dateODPRecv).Days;//实际出货
            int daysPrdFish = datePrdFish.Subtract(dateODPRecv).Days < 0 ? 0 : datePrdFish.Subtract(dateODPRecv).Days;//实际完工
            int daysShiping = dateShiping.Subtract(dateODPRecv).Days < 0 ? 0 : dateShiping.Subtract(dateODPRecv).Days;//计划完工
            int daysDrwRels = dateDrwRels.Subtract(dateODPRecv).Days < 0 ? 0 : dateDrwRels.Subtract(dateODPRecv).Days;//实际发图
            int daysDrwPlan = dateDrwPlan.Subtract(dateODPRecv).Days < 0 ? 0 : dateDrwPlan.Subtract(dateODPRecv).Days;//计划发图
            int daysKickOff = dateKickOff.Subtract(dateODPRecv).Days < 0 ? 0 : dateKickOff.Subtract(dateODPRecv).Days;//Kick-Off

            //seriesTracking.Points.AddXY("Deliver: " + dateDeliver.ToShortDateString(), daysDeliver);
            //seriesTracking.Points.AddXY("PrdFish: " + datePrdFish.ToShortDateString(), daysPrdFish);
            //seriesTracking.Points.AddXY("Shiping: " + dateShiping.ToShortDateString(), daysShiping);
            //seriesTracking.Points.AddXY("DrwRels: " + dateDrwRels.ToShortDateString(), daysDrwRels);
            //seriesTracking.Points.AddXY("DrwPlan: " + dateDrwPlan.ToShortDateString(), daysDrwPlan);
            //seriesTracking.Points.AddXY("Kick-Off: " + dateKickOff.ToShortDateString(), daysKickOff);
            //seriesTracking.Points.AddXY("ODPRcv: " + dateODPRecv.ToShortDateString(), 0);

            seriesTracking.Points.AddXY("实际发货/Deliver: " + dateDeliver.ToShortDateString(), daysDeliver);
            seriesTracking.Points.AddXY("实际完工/PrdFish: " + datePrdFish.ToShortDateString(), daysPrdFish);
            seriesTracking.Points.AddXY("计划完工/PrdPlan: " + dateShiping.ToShortDateString(), daysShiping);
            seriesTracking.Points.AddXY("实际发图/DrwRels: " + dateDrwRels.ToShortDateString(), daysDrwRels);
            seriesTracking.Points.AddXY("计划发图/DrwPlan: " + dateDrwPlan.ToShortDateString(), daysDrwPlan);
            seriesTracking.Points.AddXY("开工会议/Kick-Off: " + dateKickOff.ToShortDateString(), daysKickOff);
            seriesTracking.Points.AddXY("收到ODP/ODPRcv: " + dateODPRecv.ToShortDateString(), 0);

            seriesTracking.Points[0].Color = Color.LimeGreen;//项目完成
            seriesTracking.Points[1].Color = Color.GreenYellow;//实际完工
            seriesTracking.Points[2].Color = Color.Silver;//计划完工
            seriesTracking.Points[3].Color = Color.DeepSkyBlue;//实际发图
            seriesTracking.Points[4].Color = Color.Silver;//计划发图
            seriesTracking.Points[5].Color = Color.LightSkyBlue;//开工会议


            if (daysPrdFish > daysShiping) seriesTracking.Points[1].Color = Color.Red;//实际完工
            if (daysDrwRels > daysDrwPlan) seriesTracking.Points[3].Color = Color.Red;//实际发图

            seriesTracking.IsValueShownAsLabel = true;//显示数字
            seriesTracking.IsVisibleInLegend = true;
            //seriesTracking.LegendText = "ProjectStatus: " + projectTracking.ProjectStatusName + " Unit:Days";
            seriesTracking.LegendText = "项目状态: " + pair.ProjectStatusNameKeyValue.Where(q => q.Key == projectTracking.ProjectStatusName).First().Value + " 单位:天";


            int[] nums = { daysDeliver, daysPrdFish, daysShiping, daysDrwRels, daysDrwPlan, daysKickOff };
            chartTracking.ChartAreas[0].AxisY.Maximum = nums.Max();
            chartTracking.ChartAreas[0].AxisY.LineColor = Color.White;//Y轴线白色
            chartTracking.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;//刻度值颜色
            chartTracking.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;//隐藏刻度线 
        }

        #endregion 初始化各种信息

        #region 月度和年度统计

        private void ReportMonthly()
        {
            //统计项目总数：
            lblProjectNum.Text = "统计项目总数：" + objMonthlyReportService.GetProjectNum(cobYear.Text, cobMonth.Text);
            //ProjectStatus
            List<ChartData> chartProjectStatusDatas = objMonthlyReportService.GetProjectStatus(cobYear.Text, cobMonth.Text);
            foreach (var item in chartProjectStatusDatas)
            {
                item.Text = pair.ProjectStatusIdCNKeyValue.First(q => q.Key == item.Text).Value;
            }
            SuperChart superChartProjectStatus = new SuperChart(chartProjectStatus);
            superChartProjectStatus.ShowChart(SeriesChartType.Pie, chartProjectStatusDatas);
            for (int i = 0; i < chartProjectStatus.Series[0].Points.Count; i++)
            {
                string text = chartProjectStatusDatas[i].Text;
                chartProjectStatus.Series[0].Points[i].Color = pair.ProjectStatusCNColorKeyValue.First(q => q.Key == text).Value;
            }

            //RiskLevel
            List<ChartData> chartRiskLevelDatas = objMonthlyReportService.GetRiskLevel(cobYear.Text, cobMonth.Text);
            SuperChart superChartRiskLevel = new SuperChart(chartRiskLevel);
            superChartRiskLevel.ShowChart(SeriesChartType.Pie, chartRiskLevelDatas);
            for (int i = 0; i < chartRiskLevel.Series[0].Points.Count; i++)
            {
                string text = chartRiskLevelDatas[i].Text;
                chartRiskLevel.Series[0].Points[i].Color = pair.RislLevelColorKeyValue.First(q => q.Key == text).Value;
            }

            //ProjectType
            List<ChartData> chartProjectTypeDatas = objMonthlyReportService.GetProjectType(cobYear.Text, cobMonth.Text);
            foreach (var item in chartProjectTypeDatas)
            {
                item.Text = pair.ProjectTypeIdCNKeyValue.First(q => q.Key == item.Text).Value;
            }
            SuperChart superChartProjectType = new SuperChart(chartProjectType);
            superChartProjectType.ShowChart(SeriesChartType.Pie, chartProjectTypeDatas);
            for (int i = 0; i < chartProjectType.Series[0].Points.Count; i++)
            {
                string text = chartProjectTypeDatas[i].Text;
                chartProjectType.Series[0].Points[i].Color = pair.ProjectTypeColorKeyValue.First(q => q.Key == text).Value;
            }
            RefreshProject();
        }

        private void ReportYearly()
        {
            //统计区间项目总数：
            lblProjectNum.Text = "统计项目总数：" + objMonthlyReportService.GetProjectNum(cobYear.Text);
            //ProjectStatus，不显示年度的状态信息
            //List<ChartData> chartProjectStatusDatas = objMonthlyReportService.GetProjectStatus(cobYear.Text);
            //SuperChart superChartProjectStatus = new SuperChart(chartProjectStatus);
            //superChartProjectStatus.ShowChart(SeriesChartType.Pie, chartProjectStatusDatas);
            //RiskLevel
            List<ChartData> chartRiskLevelDatas = objMonthlyReportService.GetRiskLevel(cobYear.Text);
            SuperChart superChartRiskLevel = new SuperChart(chartRiskLevel);
            superChartRiskLevel.ShowChart(SeriesChartType.Pie, chartRiskLevelDatas);
            for (int i = 0; i < chartRiskLevel.Series[0].Points.Count; i++)
            {
                string text = chartRiskLevelDatas[i].Text;
                chartRiskLevel.Series[0].Points[i].Color = pair.RislLevelColorKeyValue.First(q => q.Key == text).Value;
            }

            //ProjectType
            List<ChartData> chartProjectTypeDatas = objMonthlyReportService.GetProjectType(cobYear.Text);
            foreach (var item in chartProjectTypeDatas)
            {
                item.Text = pair.ProjectTypeIdCNKeyValue.First(q => q.Key == item.Text).Value;
            }
            SuperChart superChartProjectType = new SuperChart(chartProjectType);
            superChartProjectType.ShowChart(SeriesChartType.Pie, chartProjectTypeDatas);
            for (int i = 0; i < chartProjectType.Series[0].Points.Count; i++)
            {
                string text = chartProjectTypeDatas[i].Text;
                chartProjectType.Series[0].Points[i].Color = pair.ProjectTypeColorKeyValue.First(q => q.Key == text).Value;
            }
            RefreshProject();
        }

        void RefreshProject()
        {
            dt = objMonthlyReportService.GetDisplayProjects(sbu, cobYear.Text, cobMonth.Text);
            dgvProjects.DataSource = AddChinese(dt);
        }




        #endregion 月度和年度统计

        #region 项目列表


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
                dgvProjects.Rows[e.RowIndex].DefaultCellStyle.BackColor = pair.ProjectStatusChineseColorKeyValue.Where(q => q.Key == projectStatus).First().Value;
            }
        }

        #endregion 项目列表

        private void timerShowInfo_Tick(object sender, EventArgs e)
        {
            lblShowInfo.Left--;
            if (lblShowInfo.Right < 0) lblShowInfo.Left = this.Left;
        }
    }
}
