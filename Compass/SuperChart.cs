using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Models;
//添加需要的命名空间
using System.Windows.Forms.DataVisualization.Charting;

namespace Compass
{
    /// <summary>
    /// 高级图表设置类
    /// </summary>
    public class SuperChart
    {
        private KeyValuePair pair = new KeyValuePair();
        //当前要使用的图表空间对象
        private readonly Chart chart;

        public SuperChart(Chart chart)
        {
            this.chart = chart;
        }
        /// <summary>
        /// 绘制图表的通用显示方法
        /// </summary>
        /// <param name="chartType">图表的显示类型</param>
        /// <param name="dataList">图表所需要的数据对象集合</param>
        public void ShowChart(SeriesChartType chartType, List<ChartData> dataList)
        {
            //【1】清除所有的图表序列
            this.chart.Series.Clear();
            //【2】创建一个图表序列对象，（一个图表，可以添加多个图表序列，也就是绘图对象）
            Series series1 = new Series{ChartType = chartType};//设置图表序列对象显示的类型。
            this.chart.Series.Add(series1);//添加图表序列集合
            //【3】设置当前图表序列的各种属性值
            for (int i = 0; i < dataList.Count; i++)
            {
                //3.1获取数据的两个值
                string text = dataList[i].Text;
                double value = dataList[i].Value;
                //3.2使用x和y的值，将DatePoint对象添加进去
                series1.Points.AddXY(text, value);
                //3.3设置数据点，显示标签和内容
                series1.Points[i].LabelToolTip = value.ToString();//鼠标放到标签上面的提示
                series1.Points[i].ToolTip = value.ToString();//鼠标放到图形上面的提示
                //3.4根据图形样式，设置显示的形式和内容
                if (chartType == SeriesChartType.Pie)
                {
                    //饼形图
                    series1.XValueType = ChartValueType.String;
                    //series1.Points[i].Label = "#AXISLABEL(#VAL)";//设置标签显示的内容=X轴内容+value
                    //series1.Points[i].Label = "#AXISLABEL(#PERCENT)";//设置标签显示的内容=X轴内容+百分比
                    //series1.Points[i].Label = "#AXISLABEL (#VAL) (#PERCENT)";//设置标签显示的内容=X轴内容+value+百分比
                    //series1.Points[i].Label = "#AXISLABEL";//设置标签显示的内容=X轴内容+value+百分比
                    series1.Points[i].Label = "#VAL (#PERCENT)";//设置标签显示的内容=X轴内容+value+百分比

                    //series1["PieLabelStyle"] = "Outside";//在外侧显示label，参考官方文档设置
                    //series1["PieLabelStyle"] = "Inside";//在内侧显示label，默认
                    //series1["PieLineColor"] = "Black";//绘制连线，label在外面时，连接到饼形图上
                    series1.LegendText = "#AXISLABEL";
                    //Keyword Replaced By Supports Multiple Y Values Supports Formatting String
                    //#VALX 	X value of the data point. 	No 	Yes
                    //#VALY 	Y value of the data point 	Yes 	Yes
                    //#SERIESNAME 	Series name 	No 	No
                    //#LABEL 	Data point label 	No 	No
                    //#AXISLABEL 	Data point axis label 	No 	No
                    //#INDEX 	Data point index in the series 	No 	Yes
                    //#PERCENT 	Percent of the data point Y value 	Yes 	Yes
                    //#LEGENDTEXT 	Series or data point legend text 	No 	No
                    //#CUSTOMPROPERTY(XXX) 	Series or data point XXX custom property value, where XXX is the name of the custom property. 	No 	No
                    //#TOTAL 	Total of all Y values in the series 	Yes 	Yes
                    //#AVG 	Average of all Y values in the series 	Yes 	Yes
                    //#MIN 	Minimum of all Y values in the series 	Yes 	Yes
                    //#MAX 	Maximum of all Y values in the series 	Yes 	Yes
                    //#FIRST 	Y value of the first point in the series 	Yes 	Yes
                    //#LAST 	Y value of the last point in the series 	Yes 	Yes

                }
                else if (chartType == SeriesChartType.Doughnut)
                {
                    //圆环图
                    //series1.Points[i].Label = "#AXISLABEL (#PERCENT)";
                    series1.Points[i].Label = "#VAL (#PERCENT)";
                    //series1["PieLabelStyle"] = "Inside";
                    series1.LegendText = "#AXISLABEL";
                }
                else if (chartType == SeriesChartType.Column)
                {
                    //柱状图
                    series1.Points[i].Label = "#VAL(#PERCENT)";
                }
                else
                {
                    //其他图形，显示百分比，，，或者显示数值或者组合
                    series1.Points[i].Label = "#AXISLABEL (#PERCENT)";
                }
                //3.5 X轴设置
                if (chartType != SeriesChartType.Pie || chartType != SeriesChartType.Doughnut)
                {
                    series1.Points[i].AxisLabel = $"{text}";
                }
                //设置颜色
                series1.Points[i].Color = pair.DefaultColorKeyValue.First(q => q.Key == i).Value;
            }
            //【4】设置图表绘图区域的X和Y坐标值（Y：表示具体需要显示的数值之间的间隔）
            this.chart.ChartAreas[0].AxisY.Interval = 50;//也可以设置成20
            this.chart.ChartAreas[0].AxisX.Interval = 1;//一般情况设置成1
        }
    }
}
