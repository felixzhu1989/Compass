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
using Models;

namespace Compass
{
    public partial class FrmProjectMeasure : MetroFramework.Forms.MetroForm
    {
        private ProjectMeasureService objProjectMeasureService=new ProjectMeasureService();
        public FrmProjectMeasure()
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
            cobMonth.SelectedIndex = currentMonth - 1;//默认定位当前月份
            this.cobMonth.SelectedIndexChanged += new System.EventHandler(this.CobMonth_SelectedIndexChanged);
            this.cobYear.SelectedIndexChanged += new System.EventHandler(this.CobYear_SelectedIndexChanged);
            IniTrendChart();
        }
        private void CobYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            IniTrendChart();
        }

        private void CobMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            IniTrendChart();
        }
        private void IniTrendChart()
        {
            ProjectOpenTrendChart();
            ProductionCloseTrendChart();
            ProjectCloseTrendChart();
        }
        private void ProjectOpenTrendChart()
        {
            List<ChartData> projectOpenDatas =
                objProjectMeasureService.GetProjectOpenTrend(cobYear.Text, cobMonth.Text);
            TargetTrendChart(chartProjectOpen, projectOpenDatas);
        }
        private void ProductionCloseTrendChart()
        {
            List<ChartData> productionCloseDatas =
                objProjectMeasureService.GetProductionCloseTrend(cobYear.Text, cobMonth.Text);
            TargetTrendChart(chartProductionClose, productionCloseDatas);
        }
        private void ProjectCloseTrendChart()
        {
            List<ChartData> projectCloseDatas =
                objProjectMeasureService.GetProjectCloseTrend(cobYear.Text, cobMonth.Text);
            TargetTrendChart(chartProjectClose, projectCloseDatas);
        }

        private void TargetTrendChart(Chart chartTarget, List<ChartData> datas)
        {
            chartTarget.Series.Clear();
            chartTarget.ChartAreas[0].AxisY.Maximum = double.NaN;
            chartTarget.ChartAreas[0].RecalculateAxesScale();
            Series series = new Series
            {
                ChartType = SeriesChartType.Line
            };
            chartTarget.Series.Add(series);
            series.YAxisType = AxisType.Primary;
            for (int i = 0; i <= 31; i++)
            {
                double value = 0;
                foreach (var item in datas)
                {
                    if (Convert.ToInt32(item.Text) == i) value = item.Value;
                }
                series.Points.AddXY(i, value);
                series.Points[i].LabelToolTip = value.ToString();//鼠标放到标签上面的提示
                series.Points[i].ToolTip = value.ToString();//鼠标放到图形上面的提示
                if (value != 0d) series.Points[i].Label = "#VAL";
                series.LegendText = chartTarget.Text;
            }
            chartTarget.ChartAreas[0].AxisX.Title = "日 | Day";
            chartTarget.ChartAreas[0].AxisY.Title = "工作量 | Workload";
            chartTarget.ChartAreas[0].AxisX.Minimum = 0;
            chartTarget.ChartAreas[0].AxisX.Maximum = 31.5;
            chartTarget.ChartAreas[0].AxisY.Interval = 10;//也可以设置成20
            chartTarget.ChartAreas[0].AxisX.Interval = 1;//一般情况设置成1

        }

        
    }
}
