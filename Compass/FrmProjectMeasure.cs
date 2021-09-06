using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DAL;
using Microsoft.VisualBasic.CompilerServices;
using Models;
using SolidWorks.Interop.sldworks;

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
            IniTrendChart();
            IniDeliveryReliabilityChart();
            IniCycleTimeChart();
        }
        private void CobYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            IniTrendChart();
            IniDeliveryReliabilityChart();
            IniCycleTimeChart();
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
                    series.LegendText = "Delivery Reliability "+type+" Rate";
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
            string delayStr = "延迟项目:\r\n";
            foreach (string item in delayList)
            {
                delayStr += item+"\r\n";
            }
            txtOdpNo.Text = delayStr;
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
            string cycleTimeNote = "Cycle time A=monthly AVE(production completed date-drawing released date)\r\n";
            cycleTimeNote += "Cycle time B=monthly AVE(production completed date-ODP released date)\r\n";
            cycleTimeNote += "Cycle time C=monthly AVE(product delivered date-ODP released date)\r\n";
            txtCycleTimeNote.Text = cycleTimeNote;
        }


    }
}
