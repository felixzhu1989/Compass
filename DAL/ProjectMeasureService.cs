using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Models;

namespace DAL
{
    public class ProjectMeasureService
    {
        public List<ChartData> GetProjectOpenTrend(string year, string month)
        {
            return GetTargetTrend("ODPReceiveDate",year,month);
        }
        public List<ChartData> GetProductionCloseTrend(string year, string month)
        {
            return GetTargetTrend("ProdFinishActual", year, month);
        }
        public List<ChartData> GetProjectCloseTrend(string year, string month)
        {
            return GetTargetTrend("DeliverActual", year, month);
        }
        public List<ChartData> GetTargetTrend(string target, string year, string month)
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
    }
}
