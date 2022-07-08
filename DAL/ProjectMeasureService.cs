using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class ProjectMeasureService
    {
        #region 项目趋势
        public List<ChartData> GetProjectOpenTrend(string startDate, string endDate)
        {
            return GetTargetTrend("ODPReceiveDate", startDate, endDate);
        }
        public List<ChartData> GetProductionCloseTrend(string startDate, string endDate)
        {
            return GetTargetTrend("ProdFinishActual", startDate, endDate);
        }
        public List<ChartData> GetProjectCloseTrend(string startDate, string endDate)
        {
            return GetTargetTrend("DeliverActual", startDate, endDate);
        }
        public List<ChartData> GetTargetTrend(string target, string startDate, string endDate)
        {
            List<ChartData> list = new List<ChartData>();
            StringBuilder sql = new StringBuilder($"select {target},sum(SubTotalWorkload) as Workload from DrawingPlan");
            sql.Append(" inner join ProjectTracking on ProjectTracking.ProjectId=DrawingPlan.ProjectId");
            sql.Append($" where {target}>='{startDate}' and {target}<'{endDate}'");
            sql.Append($" group by {target} order by  {target} asc");
            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader[$"{target}"].ToString(),
                    Value = Convert.ToDouble(objReader["Workload"].ToString())
                });
            }
            objReader.Close();
            return list;
        }
        #endregion

        #region 项目及时完工
        /// <summary>
        /// 统计及时完工百分比，数量
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetOnTimeProjectsQtyRate(int year)
        {
            List<ChartData> onTimeRate=new List<ChartData>();
            Dictionary<string, double> onTimeData = GetOnTimeProjectsCount(year);
            Dictionary<string, double> totalData = GetTotalProjectsCount(year);
            //循环总数
            foreach (var item in totalData)
            {
                //判断及时完工的数据中是否存在月份的数据
                if (onTimeData.ContainsKey(item.Key))
                {
                    onTimeRate.Add(new ChartData(){Text = item.Key ,Value =Math.Round(onTimeData[item.Key]/item.Value* 100d,1)});
                    
                }
            }
            return onTimeRate;
        }

        /// <summary>
        /// 统计及时完成的项目数，按月
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public Dictionary<string, double> GetOnTimeProjectsCount(int year)
        {
            //使用字典类型
            Dictionary<string, double> list = new Dictionary<string, double>();
            StringBuilder sql = new StringBuilder("select month(ShippingTime) as Mon, count(*) as OnTime from Projects");
            sql.Append(" inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId");
            sql.Append($" where ShippingTime like '{year}%' and ProdFinishActual<>'0001-01-01' and ProdFinishActual<=ShippingTime");
            sql.Append(" group by month(ShippingTime) order by Mon asc");
            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            while (objReader.Read())
            {
                list[objReader["Mon"].ToString()] = Convert.ToDouble(objReader["OnTime"].ToString());
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 获取所有项目数量，按月
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public Dictionary<string, double> GetTotalProjectsCount(int year)
        {
            Dictionary<string, double> list = new Dictionary<string, double>();
            StringBuilder sql = new StringBuilder("select month(ShippingTime) as Mon, count(*) as Total from Projects");
            sql.Append($" where ShippingTime like '{year}%'");
            sql.Append(" group by month(ShippingTime) order by Mon asc");
            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            while (objReader.Read())
            {
                list[objReader["Mon"].ToString()] = Convert.ToDouble(objReader["Total"].ToString());
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 统计及时完工百分比YTD，数量
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public string GetOnTimeProjectsQtyRateYTD(int year)
        {
            string sqlOnTime = "select count(*) as OnTime from Projects";
            sqlOnTime += " inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId";
            sqlOnTime += $" where ShippingTime like '{year}%' and ProdFinishActual<=ShippingTime";
            double onTime = Convert.ToDouble(SQLHelper.GetSingleResult(sqlOnTime));

            string sqlTotal = "select count(*) as Total from Projects";
            sqlTotal += $" where ShippingTime like '{year}%'";
            double total = Convert.ToDouble(SQLHelper.GetSingleResult(sqlTotal));
            return (100d * onTime / total).ToString("N2");
        }

        /// <summary>
        /// 统计及时完工百分比，工作量
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetOnTimeProjectsWorkloadRate(int year)
        {
            List<ChartData> onTimeRate = GetOnTimeProjectsWorkload(year);
            List<ChartData> totalDatas = GetTotalProjectsWorkload(year);
            for (int i = 0; i < onTimeRate.Count; i++)
            {
                onTimeRate[i].Value = Math.Round(onTimeRate[i].Value / totalDatas[i].Value * 100d, 1);
            }
            return onTimeRate;
        }
        /// <summary>
        /// 统计及时完成的项目工作量，按月
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetOnTimeProjectsWorkload(int year)
        {
            List<ChartData> list = new List<ChartData>();
            StringBuilder sql = new StringBuilder("select month(ShippingTime) as Mon, sum(SubTotalWorkload) as OnTime from Projects");
            sql.Append(" inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId");
            sql.Append(" inner join DrawingPlan on DrawingPlan.ProjectId=Projects.ProjectId");
            sql.Append($" where ShippingTime like '{year}%' and ProdFinishActual<=ShippingTime");
            sql.Append(" group by month(ShippingTime) order by Mon asc");
            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["Mon"].ToString(),
                    Value = Convert.ToDouble(objReader["OnTime"].ToString())
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 获取所有项目工作量，按月
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetTotalProjectsWorkload(int year)
        {
            List<ChartData> list = new List<ChartData>();
            StringBuilder sql = new StringBuilder("select month(ShippingTime) as Mon, sum(SubTotalWorkload) as Total from Projects");
            sql.Append(" inner join DrawingPlan on DrawingPlan.ProjectId=Projects.ProjectId");
            sql.Append($" where ShippingTime like '{year}%'");
            sql.Append(" group by month(ShippingTime) order by Mon asc");
            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["Mon"].ToString(),
                    Value = Convert.ToDouble(objReader["Total"].ToString())
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 统计及时完工百分比YTD，工作量
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public string GetOnTimeProjectsWorkloadRateYTD(int year)
        {
            string sqlOnTime = "select sum(SubTotalWorkload) as OnTime from Projects";
            sqlOnTime += " inner join DrawingPlan on DrawingPlan.ProjectId=Projects.ProjectId";
            sqlOnTime += " inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId";
            sqlOnTime += $" where ShippingTime like '{year}%' and ProdFinishActual<=ShippingTime";
            double onTime = Convert.ToDouble(SQLHelper.GetSingleResult(sqlOnTime));

            string sqlTotal = "select sum(SubTotalWorkload) as Total from Projects";
            sqlTotal += " inner join DrawingPlan on DrawingPlan.ProjectId=Projects.ProjectId";
            sqlTotal += $" where ShippingTime like '{year}%'";
            double total = Convert.ToDouble(SQLHelper.GetSingleResult(sqlTotal));
            return (100d * onTime / total).ToString("N2");
        }

        /// <summary>
        /// 获取所有延迟的项目号
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<string> GetDelayODPNo(int year)
        {
            List<string> list = new List<string>();
            StringBuilder sql = new StringBuilder("select ODPNo from Projects");
            sql.Append(" inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId");
            sql.Append($" where ShippingTime like '{year}%' and ProdFinishActual>ShippingTime");
            sql.Append(" order by ShippingTime desc");
            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            while (objReader.Read())
            {
                list.Add(objReader["ODPNo"].ToString());
            }
            objReader.Close();
            return list;
        }
        #endregion

        #region 项目周期
        public List<ChartData> GetCycleTimeA(int year)
        {
            return GetCycleTimeTarget("DrReleaseActual", "ProdFinishActual", year);
        }
        public List<ChartData> GetCycleTimeB(int year)
        {
            return GetCycleTimeTarget("ODPReceiveDate", "ProdFinishActual", year);
        }
        public List<ChartData> GetCycleTimeC(int year)
        {
            return GetCycleTimeTarget("ODPReceiveDate", "DeliverActual", year);
        }
        /// <summary>
        /// 根据开始和截止时间获取循环时间，按月
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public List<ChartData> GetCycleTimeTarget(string date1, string date2, int year)
        {
            List<ChartData> list = new List<ChartData>();
            StringBuilder sql = new StringBuilder($"select month(ShippingTime) as Mon,sum(datediff(day,{date1},{date2}))/count(*)+1 as CycleTime from ProjectTracking");
            sql.Append(" inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId");
            sql.Append($" where ShippingTime like '{year}%' and {date1}<>'0001-01-01' and {date2}<>'0001-01-01'");
            sql.Append(" group by month(ShippingTime) order by Mon asc");
            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["Mon"].ToString(),
                    Value = Convert.ToDouble(objReader["CycleTime"].ToString())
                });
            }
            objReader.Close();
            return list;
        }
        public string GetCycleTimeAYTD(int year)
        {
            return GetCycleTimeTargetYTD("DrReleaseActual", "ProdFinishActual", year);
        }
        public string GetCycleTimeBYTD(int year)
        {
            return GetCycleTimeTargetYTD("ODPReceiveDate", "ProdFinishActual", year);
        }
        public string GetCycleTimeCYTD(int year)
        {
            return GetCycleTimeTargetYTD("ODPReceiveDate", "DeliverActual", year);
        }
        /// <summary>
        /// 根据开始和截止时间获取循环时间，YTD
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public string GetCycleTimeTargetYTD(string date1, string date2, int year)
        {
            StringBuilder sql = new StringBuilder($"select sum(datediff(day,{date1},{date2}))/count(*)+1 as CycleTime from ProjectTracking");
            sql.Append(" inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId");
            sql.Append($" where ShippingTime like '{year}%'	and {date1}<>'0001-01-01' and {date2}<>'0001-01-01'");
            return SQLHelper.GetSingleResult(sql.ToString()).ToString();
        }
        #endregion
    }
}
