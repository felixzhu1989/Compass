using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmProjectMeasure : MetroFramework.Forms.MetroForm
    {
        private ProjectMeasureService objProjectMeasureService = new ProjectMeasureService();
        private int currentYear;
        private int currentMonth;
        public FrmProjectMeasure()
        {
            InitializeComponent();
            //初始化年月下拉框
            currentYear = DateTime.Now.Year;
            cobYear.Items.Add(currentYear + 1);//先添加下一年
            for (int i = 0; i <= currentYear - 2020; i++)
            {
                cobYear.Items.Add(currentYear - i);
            }
            cobYear.SelectedIndex = 1;//默认定位当前年份

            currentMonth = DateTime.Now.Month;
            for (int i = 0; i < 12; i++)
            {
                cobMonth.Items.Add(i + 1);
            }
            cobMonth.SelectedIndex = currentMonth - 1;//默认定位当前月份
            cobMonth.SelectedIndex = currentMonth - 1;//默认定位当前月份
            this.cobMonth.SelectedIndexChanged += new System.EventHandler(this.CobMonth_SelectedIndexChanged);
            this.cobYear.SelectedIndexChanged += new System.EventHandler(this.CobYear_SelectedIndexChanged);
            IniAllData();
        }
        #region 单例模式，重写关闭方法
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        internal void ShowAndFocus()
        {
            IniAllData();
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            this.Focus();
        }
        #endregion
        //对外更新所有数据
        public void IniAllData()
        {
            IniTrendChart();
            IniDeliveryReliabilityChart();
            IniCycleTimeChart();
        }
        private void CobYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            IniAllData();
        }
        private void CobMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            IniTrendChart();
        }
        private void IniTrendChart()
        {
            currentYear = Convert.ToInt32(cobYear.Text);
            currentMonth = Convert.ToInt32(cobMonth.Text);
            List<ChartData> projectOpenDatas =
                objProjectMeasureService.GetProjectOpenTrend(currentYear, currentMonth);
            TargetTrendChart(chartProjectOpen, projectOpenDatas, Color.DodgerBlue);
            List<ChartData> productionCloseDatas =
                objProjectMeasureService.GetProductionCloseTrend(currentYear, currentMonth);
            TargetTrendChart(chartProductionClose, productionCloseDatas, Color.LimeGreen);
            List<ChartData> projectCloseDatas =
                objProjectMeasureService.GetProjectCloseTrend(currentYear, currentMonth);
            TargetTrendChart(chartProjectClose, projectCloseDatas, Color.DarkGreen);
        }

        private void TargetTrendChart(Chart chartTarget, List<ChartData> datas,Color color)
        {
            int daysInMonth = System.DateTime.DaysInMonth(currentYear, currentMonth);
            chartTarget.Series.Clear();
            chartTarget.ChartAreas[0].AxisY.Maximum = double.NaN;
            chartTarget.ChartAreas[0].RecalculateAxesScale();
            Series series = new Series
            {
                ChartType = SeriesChartType.Line
            };
            chartTarget.Series.Add(series);
            series.YAxisType = AxisType.Primary;
            double total = 0;
            for (int i = 0; i <= 31; i++)
            {
                double value = 0;
                foreach (var item in datas)
                {
                    if (Convert.ToInt32(item.Text) == i) { value = item.Value; total += value; }
                }
                series.Points.AddXY(i, value);
                series.Points[i].LabelToolTip = value.ToString();//鼠标放到标签上面的提示
                series.Points[i].ToolTip = value.ToString();//鼠标放到图形上面的提示
                if (value != 0d) series.Points[i].Label = "#VAL";
                series.LegendText = chartTarget.Text;
                series.Color = color;
            }
            chartTarget.ChartAreas[0].AxisX.Title = "日 | Day";
            chartTarget.ChartAreas[0].AxisY.Title = "工作量 | Workload";
            chartTarget.ChartAreas[0].AxisX.Minimum = 0;
            chartTarget.ChartAreas[0].AxisX.Maximum = daysInMonth+0.5;//X轴最大值设置为当月天数
            chartTarget.ChartAreas[0].AxisY.Interval = 10;//也可以设置成20
            chartTarget.ChartAreas[0].AxisX.Interval = 1;//一般情况设置成1
            //绘制均值线
            double avg = total / daysInMonth;
            StripLine stripAvg = new StripLine()
            {
                Text = "Average:" + avg.ToString("N2"),//展示文本
                IntervalOffset = avg,//
                Interval = 0,
                StripWidth = 0.1,//线宽
                TextAlignment = StringAlignment.Near,
                BackColor = Color.Red,//线颜色
                ForeColor = Color.Red//文字颜色
            };
            chartTarget.ChartAreas[0].AxisY.StripLines.Clear();
            chartTarget.ChartAreas[0].AxisY.StripLines.Add(stripAvg);
        }

        private void IniDeliveryReliabilityChart()
        {
            List<ChartData> qtyDatas = objProjectMeasureService.GetOnTimeProjectsQtyRate(currentYear);
            List<ChartData> workloadDatas = objProjectMeasureService.GetOnTimeProjectsWorkloadRate(currentYear);

            chartDeliveryReliability.Series.Clear();
            chartDeliveryReliability.ChartAreas[0].AxisY.Maximum = double.NaN;
            chartDeliveryReliability.ChartAreas[0].RecalculateAxesScale();

            void AddSeries(string type, List<ChartData> datas,Color color)
            {
                Series series = new Series
                {
                    ChartType = SeriesChartType.Column
                };
                chartDeliveryReliability.Series.Add(series);
                series.YAxisType = AxisType.Primary;
                for (int i = 0; i < 12; i++)
                {
                    double value = 0;
                    foreach (var item in datas)
                    {
                        if (Convert.ToInt32(item.Text) == i) { value = item.Value; }
                    }
                    series.Points.AddXY(i, value);
                    series.Points[i].LabelToolTip = value.ToString() + "%";//鼠标放到标签上面的提示
                    series.Points[i].ToolTip = value.ToString() + "%";//鼠标放到图形上面的提示
                    if (value != 0d) series.Points[i].Label = "#VAL%";
                    series.LegendText = "Delivery Reliability Rate base on " + type;
                    series.Color = color;
                }
            }

            AddSeries("Qty", qtyDatas, Color.LightPink);
            //AddSeries("Workload", workloadDatas,Color.Orange);

            chartDeliveryReliability.ChartAreas[0].AxisX.Title = "月 | Month";
            chartDeliveryReliability.ChartAreas[0].AxisY.Title = "及时完工百分比 | Delivery Reliability %";
            chartDeliveryReliability.ChartAreas[0].AxisX.Minimum = 0;
            chartDeliveryReliability.ChartAreas[0].AxisX.Maximum = 12.5;
            chartDeliveryReliability.ChartAreas[0].AxisY.Minimum = 0;
            chartDeliveryReliability.ChartAreas[0].AxisY.Maximum = 105;
            chartDeliveryReliability.ChartAreas[0].AxisY.Interval = 10;//也可以设置成20
            chartDeliveryReliability.ChartAreas[0].AxisX.Interval = 1;//一般情况设置成1
            
            //获取所有延迟的项目号
            List<string> delayList = objProjectMeasureService.GetDelayODPNo(currentYear);
            string delayStr = "Delayed Projects:\r\n";
            foreach (string item in delayList)
            {
                delayStr += item+"\r\n";
            }
            txtOdpNo.Text = delayStr;
            string rateStr= "Rate YTD\r\nBase on Qty:\r\nOnTime / Total = " + objProjectMeasureService.GetOnTimeProjectsQtyRateYTD(currentYear) + " %\r\n";
            rateStr+= "--------------------\r\nBase on Workload:\r\nOnTime / Total = " + objProjectMeasureService.GetOnTimeProjectsWorkloadRateYTD(currentYear) + " %\r\n";
            txtYTDDeliveryReliabilityNote.Text = rateStr;
        }
        private void IniCycleTimeChart()
        {
            List<ChartData> cycleTimeA = objProjectMeasureService.GetCycleTimeA(currentYear);
            List<ChartData> cycleTimeB = objProjectMeasureService.GetCycleTimeB(currentYear);
            List<ChartData> cycleTimeC = objProjectMeasureService.GetCycleTimeC(currentYear);

            chartCycleTime.Series.Clear();
            chartCycleTime.ChartAreas[0].AxisY.Maximum = double.NaN;
            chartCycleTime.ChartAreas[0].RecalculateAxesScale();

            void AddSeries(string type, List<ChartData> datas,Color color)
            {
                Series series = new Series
                {
                    ChartType = SeriesChartType.Column
                };
                chartCycleTime.Series.Add(series);
                series.YAxisType = AxisType.Primary;
                for (int i = 0; i < 12; i++)
                {
                    double value = 0;
                    foreach (var item in datas)
                    {
                        if (Convert.ToInt32(item.Text) == i) { value = item.Value; }
                    }
                    series.Points.AddXY(i, value);
                    series.Points[i].LabelToolTip = value.ToString();//鼠标放到标签上面的提示
                    series.Points[i].ToolTip = value.ToString();//鼠标放到图形上面的提示
                    if (value != 0d) series.Points[i].Label = "#VAL";
                    series.LegendText = "Cycle Time " + type;
                    series.Color = color;
                }
            }

            AddSeries("A", cycleTimeA, Color.GreenYellow);
            AddSeries("B", cycleTimeB, Color.LightSkyBlue);
            AddSeries("C", cycleTimeC, Color.LimeGreen);
            

            chartCycleTime.ChartAreas[0].AxisX.Title = "月 | Month";
            chartCycleTime.ChartAreas[0].AxisY.Title = "项目周期 | Cycle Time Days";
            chartCycleTime.ChartAreas[0].AxisX.Minimum = 0;
            chartCycleTime.ChartAreas[0].AxisX.Maximum = 12.5;
            chartCycleTime.ChartAreas[0].AxisY.Interval = 10;//也可以设置成20
            chartCycleTime.ChartAreas[0].AxisX.Interval = 1;//一般情况设置成1
            
            string cycleTimeNote = "Cycle Time YTD\r\nA : AVG(ProdCompleted - DrwRelease)\r\n= "+objProjectMeasureService.GetCycleTimeAYTD(currentYear)+" days\r\n";
            cycleTimeNote += "--------------------\r\nB : AVG(ProdCompleted - ODPRelease)\r\n= " + objProjectMeasureService.GetCycleTimeBYTD(currentYear) + " days\r\n";
            cycleTimeNote += "--------------------\r\nC : AVG(ProdDelivered - ODPRelease)\r\n= " + objProjectMeasureService.GetCycleTimeCYTD(currentYear) + " days\r\n";
            //警告信息，后续数据准确可删除
            cycleTimeNote +=
                "--------------------\r\nDue to the loss of data, the YTD result may be inaccurate.";
            txtCycleTimeNote.Text = cycleTimeNote;
        }

       
    }
}
