﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Models;

namespace DAL
{
    public class ProjectMeasureService
    {
        #region 项目趋势
        public List<ChartData> GetProjectOpenTrend(int year, int month)
        {
            return GetTargetTrend("ODPReceiveDate", year, month);
        }
        public List<ChartData> GetProductionCloseTrend(int year, int month)
        {
            return GetTargetTrend("ProdFinishActual", year, month);
        }
        public List<ChartData> GetProjectCloseTrend(int year, int month)
        {
            return GetTargetTrend("DeliverActual", year, month);
        }
        public List<ChartData> GetTargetTrend(string target, int year, int month)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = $"select day({target}) as Day,sum(SubTotalWorkload) as Workload from DrawingPlan";
            sql += " inner join ProjectTracking on ProjectTracking.ProjectId=DrawingPlan.ProjectId";
            sql += $" where {target} like '{year}%' and month({target})='{month}'";
            sql += $" group by day({target}) order by Day asc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["Day"].ToString(),
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
            List<ChartData> onTimeRate = GetOnTimeProjectsCount(year);
            List<ChartData> totalDatas = GetTotalProjectsCount(year);
            for (int i = 0; i < onTimeRate.Count; i++)
            {
                onTimeRate[i].Value = Math.Round(onTimeRate[i].Value / totalDatas[i].Value * 100d, 1);
            }
            return onTimeRate;
        }

        /// <summary>
        /// 统计及时完成的项目数，按月
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetOnTimeProjectsCount(int year)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select month(ShippingTime) as Mon, count(*) as OnTime from Projects";
            sql += " inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId";
            sql += $" where ShippingTime like '{year}%' and ProdFinishActual<=ShippingTime";
            sql += " group by month(ShippingTime) order by Mon asc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
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
        /// 获取所有项目数量，按月
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetTotalProjectsCount(int year)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select month(ShippingTime) as Mon, count(*) as Total from Projects";
            sql += $" where ShippingTime like '{year}%'";
            sql += " group by month(ShippingTime) order by Mon asc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
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
            string sql = "select month(ShippingTime) as Mon, sum(SubTotalWorkload) as OnTime from Projects";
            sql += " inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId";
            sql += " inner join DrawingPlan on DrawingPlan.ProjectId=Projects.ProjectId";
            sql += $" where ShippingTime like '{year}%' and ProdFinishActual<=ShippingTime";
            sql += " group by month(ShippingTime) order by Mon asc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
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
            string sql = "select month(ShippingTime) as Mon, sum(SubTotalWorkload) as Total from Projects";
            sql += " inner join DrawingPlan on DrawingPlan.ProjectId=Projects.ProjectId";
            sql += $" where ShippingTime like '{year}%'";
            sql += " group by month(ShippingTime) order by Mon asc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
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
            string sql = "select ODPNo from Projects";
            sql += " inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId";
            sql += $" where ShippingTime like '{year}%' and ProdFinishActual>ShippingTime";
            sql += " order by ShippingTime desc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
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
            string sql = $"select month(ShippingTime) as Mon,sum(datediff(day,{date1},{date2}))/count(*)+1 as CycleTime from ProjectTracking";
            sql += " inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId";
            sql += $" where ShippingTime like '{year}%' and {date1}<>'0001-01-01' and {date2}<>'0001-01-01'";
            sql += " group by month(ShippingTime) order by Mon asc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
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
            string sql =
                $"select sum(datediff(day,{date1},{date2}))/count(*)+1 as CycleTime from ProjectTracking";
            sql += " inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId";
            sql += $" where ShippingTime like '{year}%'	and {date1}<>'0001-01-01' and {date2}<>'0001-01-01'";
            return SQLHelper.GetSingleResult(sql).ToString();
        }

        #endregion
    }
}
