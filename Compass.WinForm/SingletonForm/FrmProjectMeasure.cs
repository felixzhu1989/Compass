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
        private readonly ProjectMeasureService _objProjectMeasureService = new ProjectMeasureService();
        private int _currentYear;
        private int _currentMonth;
        private FrmProjectMeasure()
        {
            InitializeComponent();
            //初始化年月下拉框
            _currentYear = DateTime.Now.Year;
            cobYear.Items.Add(_currentYear + 1);//先添加下一年
            for (int i = 0; i <= _currentYear - 2020; i++)
            {
                cobYear.Items.Add(_currentYear - i);
            }
            cobYear.SelectedIndex = 1;//默认定位当前年份

            _currentMonth = DateTime.Now.Month;
            for (int i = 0; i < 12; i++)
            {
                cobMonth.Items.Add(i + 1);
            }
            cobMonth.SelectedIndex = _currentMonth - 1;//默认定位当前月份
            cobMonth.SelectedIndex = _currentMonth - 1;//默认定位当前月份
            cobMonth.SelectedIndexChanged += new EventHandler(CobMonth_SelectedIndexChanged);
            cobYear.SelectedIndexChanged += new EventHandler(CobYear_SelectedIndexChanged);
            IniAllData();
        }
        #region 单例模式
        private static FrmProjectMeasure instance;
        public static FrmProjectMeasure GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new FrmProjectMeasure();
            }
            return instance;
        }
        //对外更新所有数据
        public void IniAllData()
        {
            IniTrendChart();
            IniDeliveryReliabilityChart();
            IniCycleTimeChart();
        }
        #endregion


        #region 下拉框动作
        private void CobYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            IniAllData();
        }
        private void CobMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            IniTrendChart();
        }
        #endregion

        #region 初始化趋势图表
        private void IniTrendChart()
        {
            _currentYear = Convert.ToInt32(cobYear.Text);
            _currentMonth = Convert.ToInt32(cobMonth.Text);
            DateTime endDate = Convert.ToDateTime(_currentMonth == 12 ? $"{_currentYear + 1}/{1}/1" : $"{_currentYear}/{_currentMonth + 1}/1");
            DateTime startOpen = Convert.ToDateTime(_currentMonth == 1 ? $"{_currentYear - 1}/{12}/15" : $"{_currentYear}/{_currentMonth - 1}/15");
            DateTime endOpen = Convert.ToDateTime($"{_currentYear}/{_currentMonth}/15");
            DateTime startDate = Convert.ToDateTime($"{_currentYear}/{_currentMonth}/1");

            List<ChartData> projectOpenDatas =
                _objProjectMeasureService.GetProjectOpenTrend(startOpen.ToShortDateString(), endOpen.ToShortDateString());
            TargetTrendChart(chartProjectOpen, projectOpenDatas, startOpen, endOpen, Color.DodgerBlue);
            List<ChartData> productionCloseDatas =
                _objProjectMeasureService.GetProductionCloseTrend(startDate.ToShortDateString(), endDate.ToShortDateString());
            TargetTrendChart(chartProductionClose, productionCloseDatas, startDate, endDate, Color.LimeGreen);
            List<ChartData> projectCloseDatas =
                _objProjectMeasureService.GetProjectCloseTrend(startDate.ToShortDateString(), endDate.ToShortDateString());
            TargetTrendChart(chartProjectClose, projectCloseDatas, startDate, endDate, Color.DarkGreen);
        }
        private void TargetTrendChart(Chart chartTarget, List<ChartData> datas, DateTime startDate, DateTime endDate, Color color)
        {
            chartTarget.Series.Clear();
            chartTarget.ChartAreas[0].AxisY.Maximum = double.NaN;
            chartTarget.ChartAreas[0].RecalculateAxesScale();
            Series series = new Series
            {
                ChartType = SeriesChartType.Line,
                XValueType = ChartValueType.Date
            };
            chartTarget.Series.Add(series);
            series.YAxisType = AxisType.Primary;
            double total = 0;
            int i = 0;
            for (DateTime dt= startDate;dt <endDate;dt=dt.AddDays(1))
            {
                double value = 0;
                foreach (var item in datas)
                {
                    if (Convert.ToDateTime(item.Text).Equals(dt)) { value = item.Value; total += value; }
                }
                series.Points.AddXY(dt, value);
                if (value != 0d) series.Points[i].Label = "#VAL";
                i++;
                series.LegendText = chartTarget.Text;
                series.Color = color;
            }
            chartTarget.ChartAreas[0].AxisY.Title = "工作量 | Workload";
            chartTarget.ChartAreas[0].AxisY.Interval = 10;//也可以设置成20
            chartTarget.ChartAreas[0].AxisX.Interval = 1;//一般情况设置成1
            chartTarget.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            //绘制均值线
            int daysInMonth = endDate.Subtract(startDate).Days;
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
        #endregion

        #region 初始化完工率图
        private void IniDeliveryReliabilityChart()
        {
            List<ChartData> qtyData = _objProjectMeasureService.GetOnTimeProjectsQtyRate(_currentYear);
            List<ChartData> workloadData = _objProjectMeasureService.GetOnTimeProjectsWorkloadRate(_currentYear);

            chartDeliveryReliability.Series.Clear();
            chartDeliveryReliability.ChartAreas[0].AxisY.Maximum = double.NaN;
            chartDeliveryReliability.ChartAreas[0].RecalculateAxesScale();

            void AddSeries(string type, List<ChartData> datas, Color color)
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

            AddSeries("Qty", qtyData, Color.LightPink);
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
            List<string> delayList = _objProjectMeasureService.GetDelayODPNo(_currentYear);
            string delayStr = "Delayed Projects:\r\n";
            foreach (string item in delayList)
            {
                delayStr += item + "\r\n";
            }
            txtOdpNo.Text = delayStr;
            string rateStr = "Rate YTD\r\nBase on Qty:\r\nOnTime / Total = " + _objProjectMeasureService.GetOnTimeProjectsQtyRateYTD(_currentYear) + " %\r\n";
            rateStr += "--------------------\r\nBase on Workload:\r\nOnTime / Total = " + _objProjectMeasureService.GetOnTimeProjectsWorkloadRateYTD(_currentYear) + " %\r\n";
            txtYTDDeliveryReliabilityNote.Text = rateStr;
        } 
        #endregion

        #region 初始化循环周期图表
        private void IniCycleTimeChart()
        {
            List<ChartData> cycleTimeA = _objProjectMeasureService.GetCycleTimeA(_currentYear);
            List<ChartData> cycleTimeB = _objProjectMeasureService.GetCycleTimeB(_currentYear);
            List<ChartData> cycleTimeC = _objProjectMeasureService.GetCycleTimeC(_currentYear);

            chartCycleTime.Series.Clear();
            chartCycleTime.ChartAreas[0].AxisY.Maximum = double.NaN;
            chartCycleTime.ChartAreas[0].RecalculateAxesScale();

            void AddSeries(string type, List<ChartData> datas, Color color)
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

            string cycleTimeNote = "Cycle Time YTD\r\nA : AVG(ProdCompleted - DrwRelease)\r\n= " + _objProjectMeasureService.GetCycleTimeAYTD(_currentYear) + " days\r\n";
            cycleTimeNote += "--------------------\r\nB : AVG(ProdCompleted - ODPRelease)\r\n= " + _objProjectMeasureService.GetCycleTimeBYTD(_currentYear) + " days\r\n";
            cycleTimeNote += "--------------------\r\nC : AVG(ProdDelivered - ODPRelease)\r\n= " + _objProjectMeasureService.GetCycleTimeCYTD(_currentYear) + " days\r\n";
            //警告信息，后续数据准确可删除
            cycleTimeNote +=
                "--------------------\r\nDue to the loss of data, the YTD result may be inaccurate.";
            txtCycleTimeNote.Text = cycleTimeNote;
        } 
        #endregion


    }
}
