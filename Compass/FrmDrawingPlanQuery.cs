using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;
using DAL;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;

namespace Compass
{

    public partial class FrmDrawingPlanQuery : MetroFramework.Forms.MetroForm
    {
        private DrawingPlanService objDrawingPlanService = new DrawingPlanService();
        private SuperChart superChartPlan = null;
        private SuperChart superChartPercent = null;
        private SuperChart superChartUserPercent = null;
        private List<ChartData> dataList = new List<ChartData>();//用来保存数据的集合（主图）
        private List<ChartData> dataListMonth = new List<ChartData>();//用来保存数据的集合（次图）
        private SqlDataPager objSqlDataPager = null;
        public FrmDrawingPlanQuery()
        {
            InitializeComponent();
            toolTip.SetToolTip(cobQueryYear, "按照项目完工日期年度查询");
            dgvDrawingPlan.AutoGenerateColumns = false;
            //初始化自定义图表对象
            superChartPlan = new SuperChart(this.chartPlan);
            superChartPercent = new SuperChart(this.chartPercent);
            superChartUserPercent = new SuperChart(this.chartUserPercent);
            //查询年度初始化
            int currentYear = DateTime.Now.Year;
            cobQueryYear.Items.Add("ALL");//添加所有
            cobQueryYear.Items.Add(currentYear + 1);//先添加下一年
            for (int i = 0; i <= currentYear - 2020; i++)
            {
                cobQueryYear.Items.Add(currentYear - i);
            }
            cobQueryYear.SelectedIndex = 2;//默认定位当前年份
            StringBuilder innerJoin1 = new StringBuilder("inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId");
            innerJoin1.Append(" inner join Users on Users.UserId=Projects.UserId");
            innerJoin1.Append(" left join ProjectTracking on DrawingPlan.ProjectId=ProjectTracking.ProjectId");

            //初始化分页对象
            objSqlDataPager = new SqlDataPager()
            {
                PrimaryKey = "DrawingPlanId",
                TableName = "DrawingPlan",
                InnerJoin1 = innerJoin1.ToString(),
                InnerJoin2 = "inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId",
                FiledName = "UserAccount,ODPNo,Model,ModuleNo,DrawingPlan.DrReleaseTarget,DrReleaseActual,SubTotalWorkload,HoodType,ProjectName",
                CurrentPage = 1,
                Sort = "DrawingPlan.DrReleasetarget desc",
            };
            btnQueryByYear_Click(null, null);

        }
        private void Query()
        {
            //【1】设置分页查询的条件
            //objSqlDataPager.Condition = string.Format("ShippingTime>='{0}/01/01' and ShippingTime<='{0}/12/31'", this.cobQueryYear.Text);
            //【2】设置每页显示的条数
            //objSqlDataPager.PageSize = Convert.ToInt32(this.cobRecordList.Text.Trim());
            //【3】执行查询
            this.dgvDrawingPlan.DataSource = objSqlDataPager.GetPagedData();
        }
        /// <summary>
        /// 查询制图计划到表格
        /// </summary>
        private void QueryByYear()
        {
            if (this.cobQueryYear.Text == "ALL")
            {
                objSqlDataPager.Condition = "DrawingPlan.DrReleasetarget>='2020/01/01'";
            }
            else
            {
                objSqlDataPager.Condition = string.Format("DrawingPlan.DrReleasetarget>='{0}/01/01' and DrawingPlan.DrReleasetarget<='{0}/12/31'", this.cobQueryYear.Text);
            }
            objSqlDataPager.PageSize = 10000;
            Query();
        }

        /// <summary>
        /// 根据年份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryByYear_Click(object sender, EventArgs e)
        {
            if (this.cobQueryYear.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要查询的年度", "提示信息");
                return;
            }
            objSqlDataPager.CurrentPage = 1;//每次执行查询都必须设置为第一页
            QueryByYear();
            ShowYearWorkloadPie();
            btnAllWorkload_Click(null, null);
        }

        private void ShowYearWorkloadPie()
        {
            //年度烟罩和天花工作量占比
            List<ChartData> chartPercentChartDatas = new List<ChartData>();
            chartPercentChartDatas.Add(new ChartData("Hood", objDrawingPlanService.GetTotalHoodWorkloadByYear(cobQueryYear.Text)));
            chartPercentChartDatas.Add(new ChartData("Ceiling", objDrawingPlanService.GetTotalCeilingWorkloadByYear(cobQueryYear.Text)));
            this.superChartPercent.ShowChart(SeriesChartType.Pie, chartPercentChartDatas);
            //年度各人员工作量占比
            //chartUserPercent
            List<ChartData> chartUserPercentChartDatas = objDrawingPlanService.GetAllWorkloadByUser(cobQueryYear.Text);
            this.superChartUserPercent.ShowChart(SeriesChartType.Pie, chartUserPercentChartDatas);
            chartUserPercent.Series[0]["PieLabelStyle"] = "Outside";//在外侧显示label，参考官方文档设置
            //chartUserPercent.Series[0].IsValueShownAsLabel = true;
            chartUserPercent.Series[0]["PieLineColor"] = "Black";//绘制连线，label在外面时，连接到饼形图上
        }
        /// <summary>
        /// 年度普通烟罩数量统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHoodModuleNo_Click(object sender, EventArgs e)
        {
            lblModel.Visible = true;
            cobModel.Visible = true;
            dataList = objDrawingPlanService.GetHoodModuleNoByYear(cobQueryYear.Text);
            chartPlan.ChartAreas[0].AxisX.Maximum = double.NaN;
            chartPlan.ChartAreas[0].AxisY.Maximum = double.NaN;
            chartPlan.ChartAreas[0].AxisY2.Maximum = double.NaN;
            //重新设置轴最大值
            chartPlan.ChartAreas[0].RecalculateAxesScale();
            this.superChartPlan.ShowChart(SeriesChartType.Column, dataList);

            chartPlan.Series[0].LegendText = cobQueryYear.Text + "年烟罩数量统计 | 总数量：" + objDrawingPlanService.GetTotalHoodModuleNoByYear(cobQueryYear.Text);
            //chartPlan.Series[0].LegendText+=" | "+ cobQueryYear.Text + " | Annual statistics | Total for the year:" + objDrawingPlanService.GetTotalHoodModuleNoByYear(cobQueryYear.Text);
            //初始化型号选择框
            List<ChartData> cobModelDataList = dataList;
            this.cobModel.SelectedIndexChanged -= new System.EventHandler(this.cobModel_SelectedIndexChanged);
            lblModel.Text = "型号：";
            cobModel.DataSource = cobModelDataList;
            cobModel.DisplayMember = "Text";
            cobModel.ValueMember = "Value";
            cobModel.SelectedIndex = -1;
            this.cobModel.SelectedIndexChanged += new System.EventHandler(this.cobModel_SelectedIndexChanged);
            cobModel.SelectedIndex = 0;
            chartPlan.ChartAreas[0].AxisX.Title = "普通烟罩型号 | Hood Model";
            chartPlan.ChartAreas[0].AxisY.Title = "烟罩数量 | Number of Hoods";
        }
        /// <summary>
        /// 选择模型统计月度机型数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataListMonth = objDrawingPlanService.GetHoodModuleNoByMonth(cobQueryYear.Text, cobModel.Text);
            chartMonth.Series.Clear();
            chartMonth.ChartAreas[0].AxisY.Maximum = double.NaN;
            //重新设置轴最大值
            chartMonth.ChartAreas[0].RecalculateAxesScale();
            Series series1 = new Series();
            series1.ChartType = SeriesChartType.Column;
            chartMonth.Series.Add(series1);

            series1.LegendText = cobQueryYear.Text + "年" + cobModel.Text + "按月数量统计 | 全年总数量：" + cobModel.SelectedValue;
            //series1.LegendText += " | " + cobQueryYear.Text + " | " + cobModel.Text + " | Monthly statistics | Total for the year:" + cobModel.SelectedValue;
            for (int i = 0; i <= 12; i++)
            {
                double value = 0;
                foreach (var item in dataListMonth)
                {
                    if (Convert.ToInt32(item.Text) == i) value = item.Value;
                }
                series1.Points.AddXY(i, value);
                series1.Points[i].LabelToolTip = value.ToString();//鼠标放到标签上面的提示
                series1.Points[i].ToolTip = value.ToString();//鼠标放到图形上面的提示

                if (value != 0d) series1.Points[i].Label = "#VAL";
            }
            chartMonth.ChartAreas[0].AxisX.Title = "月份 | Month";
            chartMonth.ChartAreas[0].AxisY.Title = "烟罩数量 | Number of Hoods";
            chartMonth.ChartAreas[0].AxisX.Minimum = 0;
            chartMonth.ChartAreas[0].AxisX.Maximum = 12.5;
            chartMonth.ChartAreas[0].AxisY.Interval = 20;//也可以设置成20
            chartMonth.ChartAreas[0].AxisX.Interval = 1;//一般情况设置成1
        }
        /// <summary>
        /// 年度天花烟罩工作量统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCeilingWorkload_Click(object sender, EventArgs e)
        {
            lblModel.Visible = false;
            cobModel.Visible = false;
            dataList.Clear();
            dataList = objDrawingPlanService.GetCeilingWorkloadByYear(cobQueryYear.Text);
            chartPlan.ChartAreas[0].AxisX.Maximum = double.NaN;
            chartPlan.ChartAreas[0].AxisY.Maximum = double.NaN;
            chartPlan.ChartAreas[0].AxisY2.Maximum = double.NaN;
            //重新设置轴最大值
            chartPlan.ChartAreas[0].RecalculateAxesScale();
            this.superChartPlan.ShowChart(SeriesChartType.Column, dataList);
            chartPlan.Series[0].LegendText = cobQueryYear.Text + "年天花烟罩工作量统计 | 总工作量：" + objDrawingPlanService.GetTotalCeilingWorkloadByYear(cobQueryYear.Text);
            chartPlan.ChartAreas[0].AxisX.Title = "天花烟罩型号 | Ceiling Model";
            chartPlan.ChartAreas[0].AxisY.Title = "天花烟罩工作量 | Workload of Ceiling";
            //月度统计
            chartMonth.Series.Clear();
            chartMonth.ChartAreas[0].AxisY.Maximum = double.NaN;
            //重新设置轴最大值
            chartMonth.ChartAreas[0].RecalculateAxesScale();
            dataListMonth.Clear();
            //循环所有模型
            foreach (var model in dataList)
            {
                dataListMonth = objDrawingPlanService.GetCeilingWorkloadByMonth(cobQueryYear.Text, model.Text);
                Series seriesMonth = new Series();
                seriesMonth.ChartType = SeriesChartType.Column;
                chartMonth.Series.Add(seriesMonth);
                seriesMonth.LegendText = model.Text + "(" + model.Value + ")";

                for (int i = 0; i <= 12; i++)
                {
                    double value = 0;
                    foreach (var item in dataListMonth)
                    {
                        if (Convert.ToInt32(item.Text) == i) value = item.Value;
                    }
                    seriesMonth.Points.AddXY(i, value);
                    seriesMonth.Points[i].LabelToolTip = value.ToString();//鼠标放到标签上面的提示
                    seriesMonth.Points[i].ToolTip = value.ToString();//鼠标放到图形上面的提示
                    if (value != 0d) seriesMonth.Points[i].Label = "#VAL";
                }
            }
            chartMonth.ChartAreas[0].AxisX.Title = "月份 | Month";
            chartMonth.ChartAreas[0].AxisY.Title = "天花烟罩工作量 | Workload of Ceiling";
            chartMonth.ChartAreas[0].AxisX.Minimum = 0;
            chartMonth.ChartAreas[0].AxisX.Maximum = 12.5;
            chartMonth.ChartAreas[0].AxisY.Interval = 25;//也可以设置成20
            chartMonth.ChartAreas[0].AxisX.Interval = 1;//一般情况设置成1
        }

        /// <summary>
        /// 年度所有烟罩工作量统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllWorkload_Click(object sender, EventArgs e)
        {
            lblModel.Visible = false;
            cobModel.Visible = false;

            //--------------------主图--------------------
            //月度所有统计
            chartPlan.Series.Clear();
            chartPlan.ChartAreas[0].AxisY.Maximum = double.NaN;
            //重新设置轴最大值
            chartPlan.ChartAreas[0].RecalculateAxesScale();

            //循环所有模型，工作量-延误-销售额
            List<ChartData> totalWorkloadChartDatas =
                objDrawingPlanService.GetTotalWorkloadByMonth(cobQueryYear.Text);
            List<ChartData> totalSalesValueDatas = objDrawingPlanService.GetTotalSalesValueByMonth(cobQueryYear.Text);
            List<ChartData> totalDelayChartDatas =
                objDrawingPlanService.GetTotalDelayByMonth(cobQueryYear.Text);
            
            //--------------------月度工作量(主图)--------------------
            Series seriesWorkloadMonthly = new Series();
            seriesWorkloadMonthly.ChartType = SeriesChartType.Column;
            chartPlan.Series.Add(seriesWorkloadMonthly);
            seriesWorkloadMonthly.YAxisType = AxisType.Primary;
            for (int i = 0; i <= 12; i++)
            {
                double value = 0;
                foreach (var item in totalWorkloadChartDatas)
                {
                    if (Convert.ToInt32(item.Text) == i) value = item.Value;
                }
                seriesWorkloadMonthly.Points.AddXY(i, value);
                seriesWorkloadMonthly.Points[i].LabelToolTip = value.ToString();//鼠标放到标签上面的提示
                seriesWorkloadMonthly.Points[i].ToolTip = value.ToString();//鼠标放到图形上面的提示
                if (value != 0d) seriesWorkloadMonthly.Points[i].Label = "#VAL";
            }
            chartPlan.ChartAreas[0].AxisX.Title = "月份 | Month";
            chartPlan.ChartAreas[0].AxisY.Title = "工作量 | Workload [销售额/万元RMB | SalesValue]";
            chartPlan.ChartAreas[0].AxisX.Minimum = 0;
            chartPlan.ChartAreas[0].AxisX.Maximum = 12.5;
            chartPlan.ChartAreas[0].AxisY.Interval = 50;//也可以设置成20
            chartPlan.ChartAreas[0].AxisX.Interval = 1;//一般情况设置成1

            //--------------------月度销售额(主图)--------------------
            Series seriesSalesValueMonthly = new Series();
            seriesSalesValueMonthly.ChartType = SeriesChartType.Line;
            chartPlan.Series.Add(seriesSalesValueMonthly);
            seriesSalesValueMonthly.YAxisType = AxisType.Primary;
            seriesSalesValueMonthly.LabelForeColor = Color.Red;
            for (int i = 0; i <= 12; i++)
            {
                double value = 0;
                foreach (var item in totalSalesValueDatas)
                {
                    if (Convert.ToInt32(item.Text) == i) value =Convert.ToDouble( (item.Value/10000d).ToString("N2"));
                }
                seriesSalesValueMonthly.Points.AddXY(i, value);
                seriesSalesValueMonthly.Points[i].LabelToolTip = value.ToString();//鼠标放到标签上面的提示
                seriesSalesValueMonthly.Points[i].ToolTip = value.ToString();//鼠标放到图形上面的提示
                if (value != 0d) seriesSalesValueMonthly.Points[i].Label = "#VAL";
                seriesSalesValueMonthly.Points[i].Color=Color.Red;
            }

            //--------------------月度Delay(主图)--------------------

            Series seriesDelayMonthly = new Series();
            seriesDelayMonthly.ChartType = SeriesChartType.BoxPlot;
            chartPlan.Series.Add(seriesDelayMonthly);
            seriesDelayMonthly.YAxisType = AxisType.Secondary;

            for (int i = 0; i <= 12; i++)
            {
                double value = 0;
                foreach (var item in totalDelayChartDatas)
                {
                    if (Convert.ToInt32(item.Text) == i)
                    {
                        foreach (var load in totalWorkloadChartDatas)
                        {
                            if (Convert.ToInt32(load.Text) == i && load.Value > 0d) value = 100d * double.Parse((item.Value / load.Value).ToString("0.000"));
                        }
                    }
                }
                seriesDelayMonthly.Points.AddXY(i, value);
                seriesDelayMonthly.Points[i].Color = Color.Black;
                //seriesDelay.Points[i].LabelToolTip = value.ToString();//鼠标放到标签上面的提示
                //seriesDelay.Points[i].ToolTip = value.ToString();//鼠标放到图形上面的提示
                if (value != 0d) seriesDelayMonthly.Points[i].Label = "#VAL %";
            }
            chartPlan.ChartAreas[0].AxisY2.Title = "Delay天数占工作量的百分比 | DelayDays / Workload %";
            //chartPlan.ChartAreas[0].AxisY2.Interval = 5;//也可以设置成20
            chartPlan.ChartAreas[0].AxisY2.Minimum = 0;
            chartPlan.ChartAreas[0].AxisY2.Maximum = 50;

            //--------------------主图标签--------------------
            double yearWorkload = objDrawingPlanService.GetTotalWorkloadByYear(cobQueryYear.Text);
            double yearSalesValue = objDrawingPlanService.GetTotalSalesValueByYear(cobQueryYear.Text);
            double yearDelay = objDrawingPlanService.GetTotalDelayByYear(cobQueryYear.Text);
            
            chartPlan.Series[0].IsVisibleInLegend = true;
            chartPlan.Series[0].LegendText = cobQueryYear.Text + "年总工作量:" + yearWorkload;
            chartPlan.Series[1].IsVisibleInLegend = true;
            chartPlan.Series[1].LegendText ="总销售额:" + (yearSalesValue/10000d).ToString("N2")+"万元RMB";
            chartPlan.Series[1].Color = Color.Red;
            if (yearWorkload > 0)
            {
                chartPlan.Series[2].IsVisibleInLegend = true;
                chartPlan.Series[2].Color= Color.Black;
                chartPlan.Series[2].LegendText = "Delay："
                                                 + 100d * double.Parse((yearDelay / yearWorkload).ToString("0.000")) +
                                                 "%";
            }
            else
            {
                chartPlan.Series[2].IsVisibleInLegend = false;
            }
            //--------------------次图--------------------
            //月度按人统计
            chartMonth.Series.Clear();
            chartMonth.ChartAreas[0].AxisY.Maximum = double.NaN;
            //重新设置轴最大值
            chartMonth.ChartAreas[0].RecalculateAxesScale();
            dataListMonth.Clear();

            List<ChartData> userWorkloadChartDatas = objDrawingPlanService.GetAllWorkloadByUser(cobQueryYear.Text);
            //--------------------人员月度工作量（次图）-------------------
            //循环所有人员
            foreach (var userData in userWorkloadChartDatas)
            {
                List<ChartData> monthlyWorkloadChartDatas= objDrawingPlanService.GetUserWorkloadByMonth(cobQueryYear.Text, userData.Text);
                Series seriesMonth = new Series();
                seriesMonth.ChartType = SeriesChartType.Column;
                chartMonth.Series.Add(seriesMonth);
                seriesMonth.LegendText = userData.Text + "(" + userData.Value + ")";//标签
                seriesMonth.YAxisType = AxisType.Primary;

                for (int i = 0; i <= 12; i++)
                {
                    double value = 0;
                    foreach (var item in monthlyWorkloadChartDatas)
                    {
                        if (Convert.ToInt32(item.Text) == i) value = item.Value;
                    }
                    seriesMonth.Points.AddXY(i, value);
                    seriesMonth.Points[i].LabelToolTip = value.ToString();//鼠标放到标签上面的提示
                    seriesMonth.Points[i].ToolTip = value.ToString();//鼠标放到图形上面的提示
                    if (value != 0d) seriesMonth.Points[i].Label = "#VAL";
                }
            }
            chartMonth.ChartAreas[0].AxisX.Title = "月份 | Month";
            chartMonth.ChartAreas[0].AxisY.Title = "工作量 | Workload";
            chartMonth.ChartAreas[0].AxisX.Minimum = 0;
            chartMonth.ChartAreas[0].AxisX.Maximum = 12.5;
            chartMonth.ChartAreas[0].AxisY.Interval = 25;//也可以设置成20
            chartMonth.ChartAreas[0].AxisX.Interval = 1;//一般情况设置成1

            //--------------------人员月度Delay（次图）-------------------
            //循环所有人员
            foreach (var userData in userWorkloadChartDatas)
            {
                List<ChartData> delayChartDatas =
                    objDrawingPlanService.GetUserDelayByMonth(cobQueryYear.Text, userData.Text);
                List<ChartData> workloadChartDatas =
                    objDrawingPlanService.GetUserWorkloadByMonth(cobQueryYear.Text, userData.Text);

                Series seriesDelay = new Series();
                seriesDelay.ChartType = SeriesChartType.BoxPlot;
                chartMonth.Series.Add(seriesDelay);
                //seriesDelay.LegendText = userData.Text;
                seriesDelay.IsVisibleInLegend = false;
                seriesDelay.YAxisType = AxisType.Secondary;
                for (int i = 0; i <= 12; i++)
                {
                    double value = 0;
                    foreach (var item in delayChartDatas)
                    {
                        if (Convert.ToInt32(item.Text) == i)
                        {
                            foreach (var load in workloadChartDatas)
                            {
                                if (Convert.ToInt32(load.Text) == i && load.Value > 0d) value = 100d * double.Parse((item.Value / load.Value).ToString("0.000"));
                            }
                        }
                    }
                    seriesDelay.Points.AddXY(i, value);
                    seriesDelay.Points[i].Color = Color.Black;
                    //seriesDelay.Points[i].LabelToolTip = value.ToString();//鼠标放到标签上面的提示
                    //seriesDelay.Points[i].ToolTip = value.ToString();//鼠标放到图形上面的提示
                    if (value != 0d) seriesDelay.Points[i].Label = "#VAL %";
                }
            }
            chartMonth.ChartAreas[0].AxisY2.Title = "Delay天数占工作量的百分比 | DelayDays / Workload %";
            //chartMonth.ChartAreas[0].AxisY2.Interval = 5;
            chartMonth.ChartAreas[0].AxisY2.Minimum = 0;
            chartMonth.ChartAreas[0].AxisY2.Maximum = 50;

           



        }

        //《C#中图表的使用》
        //需要的对象1，两个数据的封装
        //1.显示的文本/名称（地区，姓名，月份...）
        //2.显示的数据

        //需要的对象2，具体图表控件的各种属性设置
        //主要是封装方法

    }
}
