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

namespace Compass
{
    
    public partial class FrmDrawingPlanQuery : MetroFramework.Forms.MetroForm
    {
        private DrawingPlanService objDrawingPlanService=new DrawingPlanService();
        private SuperChart superChart = null;
        private SuperChart superChartMonth = null;
        private List<ChartData> dataList = new List<ChartData>();//用来保存数据的集合
        private List<ChartData> dataListMonth = new List<ChartData>();//用来保存数据的集合
        private SqlDataPager objSqlDataPager = null;
        public FrmDrawingPlanQuery()
        {
            InitializeComponent();
            toolTip.SetToolTip(cobQueryYear, "按照项目完工日期年度查询");
            dgvDrawingPlan.AutoGenerateColumns = false;
            //初始化自定义图表对象
            superChart = new SuperChart(this.chartPlan);
            superChartMonth=new SuperChart(this.chartMonth);
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
                FiledName = "UserAccount,ODPNo,Model,ModuleNo,DrawingPlan.DrReleaseTarget,DrReleaseActual,SubTotalWorkload,HoodType",
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
            btnHoodModuleNo_Click(null, null);
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
            this.superChart.ShowChart(SeriesChartType.Column, dataList);
            chartPlan.Series[0].LegendText = cobQueryYear.Text+"年烟罩数量统计 | 总数量："+ objDrawingPlanService.GetTotalHoodModuleNoByYear(cobQueryYear.Text);
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
            //this.superChart.ShowChart(SeriesChartType.Pie, dataList);
            //this.superChart.ShowChart(SeriesChartType.Bar, dataList);
            //this.superChart.ShowChart(SeriesChartType.Doughnut, dataList);
        }
        /// <summary>
        /// 选择模型统计月度机型数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataListMonth = objDrawingPlanService.GetHoodModuleNoByMonth(cobQueryYear.Text,cobModel.Text);
            chartMonth.Series.Clear();
            Series series1=new Series();
            series1.ChartType = SeriesChartType.Column;
            chartMonth.Series.Add(series1);

            series1.LegendText = cobQueryYear.Text + "年"+ cobModel.Text+ "按月数量统计 | 全年总数量："+ cobModel.SelectedValue;
            //series1.LegendText += " | " + cobQueryYear.Text + " | " + cobModel.Text + " | Monthly statistics | Total for the year:" + cobModel.SelectedValue;
            for (int i = 0; i <= 12; i++)
            {
                double value = 0;
                foreach (var item in dataListMonth)
                {
                    if (Convert.ToInt32(item.Text)==i) value = item.Value;
                }
                series1.Points.AddXY(i, value);
                series1.Points[i].LabelToolTip = value.ToString();//鼠标放到标签上面的提示
                series1.Points[i].ToolTip = value.ToString();//鼠标放到图形上面的提示

                if(value!=0d) series1.Points[i].Label = "#VAL";
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
            this.superChart.ShowChart(SeriesChartType.Column, dataList);
            chartPlan.Series[0].LegendText = cobQueryYear.Text + "年天花烟罩工作量统计 | 总工作量：" + objDrawingPlanService.GetTotalCeilingWorkloadByYear(cobQueryYear.Text);
            chartPlan.ChartAreas[0].AxisX.Title = "天花烟罩型号 | Ceiling Model";
            chartPlan.ChartAreas[0].AxisY.Title = "天花烟罩工作量 | Workload of Ceiling";
            //月度统计
            chartMonth.Series.Clear();
            //循环所有模型
            foreach (var model in dataList)
            {
                dataListMonth = objDrawingPlanService.GetCeilingWorkloadByMonth(cobQueryYear.Text, model.Text);
                Series series1 = new Series();
                series1.ChartType = SeriesChartType.Column;
                chartMonth.Series.Add(series1);
                series1.LegendText = model.Text+"(" +model.Value + ")";

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
            }
            chartMonth.ChartAreas[0].AxisX.Title = "月份 | Month";
            chartMonth.ChartAreas[0].AxisY.Title = "天花烟罩工作量 | Workload of Ceiling";
            chartMonth.ChartAreas[0].AxisX.Minimum = 0;
            chartMonth.ChartAreas[0].AxisX.Maximum = 12.5;
            chartMonth.ChartAreas[0].AxisY.Interval = 10;//也可以设置成20
            chartMonth.ChartAreas[0].AxisX.Interval = 1;//一般情况设置成1
        }
        /// <summary>
        /// 年度所有烟罩工作量统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllWorkload_Click(object sender, EventArgs e)
        {

        }


        //《C#中图表的使用》
        //需要的对象1，两个数据的封装
        //1.显示的文本/名称（地区，姓名，月份...）
        //2.显示的数据

        //需要的对象2，具体图表控件的各种属性设置
        //主要是封装方法



    }
}
