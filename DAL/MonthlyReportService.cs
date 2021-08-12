using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class MonthlyReportService
    {
        /*--查询项目数量（月度）
        select count(*) from Projects where ShippingTime like'2021%' and month(ShippingTime)='8'
        --查询项目数量（年度）
        select count(*) from Projects where ShippingTime like'2021%'
        --统计项目状态数量
        select ProjectStatusName,count(ProjectStatusName) from ProjectTracking
	    inner join ProjectStatus on ProjectStatus.ProjectStatusId=ProjectTracking.ProjectStatusId
	    inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId
	    where ShippingTime like'2021%' and month(ShippingTime)='7'
	    group by ProjectStatusName
        */
        /// <summary>
        /// 获取需要循环的项目列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetScrollODPNoList()
        {
            string sql = "select ODPNo from Projects";
            sql += " inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId";
            sql += " where ProjectStatusId between 1 and 4 order by ShippingTime asc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<string> list = new List<string>();
            while (objReader.Read())
            {
                list.Add(objReader["ODPNo"].ToString());
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 项目数量月度统计
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public string GetProjectNum(string year, string month)
        {
            string sql = string.Format("select count(*) from Projects where ShippingTime like'{0}%' and month(ShippingTime)='{1}'", year, month);
            return SQLHelper.GetSingleResult(sql).ToString();
        }
        /// <summary>
        /// 项目数量年度统计
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public string GetProjectNum(string year)
        {
            string sql = string.Format("select count(*) from Projects where ShippingTime like'{0}%'", year);
            return SQLHelper.GetSingleResult(sql).ToString();
        }

        /// <summary>
        /// 项目状态月度统计
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<ChartData> GetProjectStatus(string year,string month)
        {
            List<ChartData> list=new List<ChartData>();
            string sql = "select ProjectStatusName,count(ProjectStatusName) as Qty from ProjectTracking";
            sql += " inner join ProjectStatus on ProjectStatus.ProjectStatusId=ProjectTracking.ProjectStatusId";
            sql += " inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId";
            sql += string.Format(" where ShippingTime like'{0}%' and month(ShippingTime) = '{1}'", year, month);
            sql += " group by ProjectStatusName";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["ProjectStatusName"].ToString(),
                    Value = Convert.ToDouble(objReader["Qty"].ToString())
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 项目状态年度统计
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetProjectStatus(string year)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select ProjectStatusName,count(ProjectStatusName) as Qty from ProjectTracking";
            sql += " inner join ProjectStatus on ProjectStatus.ProjectStatusId=ProjectTracking.ProjectStatusId";
            sql += " inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId";
            sql += string.Format(" where ShippingTime like'{0}%'", year);
            sql += " group by ProjectStatusName";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["ProjectStatusName"].ToString(),
                    Value = Convert.ToDouble(objReader["Qty"].ToString())
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 风险等级月度统计
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<ChartData> GetRiskLevel(string year, string month)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select RiskLevel,count(RiskLevel) as Qty from GeneralRequirements";
            sql += " inner join Projects on GeneralRequirements.ProjectId=Projects.ProjectId";
            sql += string.Format(" where ShippingTime like'{0}%' and month(ShippingTime) = '{1}'", year, month);
            sql += " group by RiskLevel";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = "RiskLevel-"+objReader["RiskLevel"],
                    Value = Convert.ToDouble(objReader["Qty"].ToString())
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 风险等级年度统计
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetRiskLevel(string year)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select RiskLevel,count(RiskLevel) as Qty from GeneralRequirements";
            sql += " inner join Projects on GeneralRequirements.ProjectId=Projects.ProjectId";
            sql += string.Format(" where ShippingTime like'{0}%'", year);
            sql += " group by RiskLevel";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = "RiskLevel-" + objReader["RiskLevel"],
                    Value = Convert.ToDouble(objReader["Qty"].ToString())
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 项目类型月度统计
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<ChartData> GetProjectType(string year, string month)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select TypeName,count(TypeName) as Qty from GeneralRequirements";
            sql += " inner join ProjectTypes on ProjectTypes.TypeId=GeneralRequirements.TypeId";
            sql += " inner join Projects on GeneralRequirements.ProjectId=Projects.ProjectId";
            sql += string.Format(" where ShippingTime like'{0}%' and month(ShippingTime) = '{1}'", year, month);
            sql += " group by TypeName";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["TypeName"].ToString(),
                    Value = Convert.ToDouble(objReader["Qty"].ToString())
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 项目类型年度统计
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetProjectType(string year)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select TypeName,count(TypeName) as Qty from GeneralRequirements";
            sql += " inner join ProjectTypes on ProjectTypes.TypeId=GeneralRequirements.TypeId";
            sql += " inner join Projects on GeneralRequirements.ProjectId=Projects.ProjectId";
            sql += string.Format(" where ShippingTime like'{0}%'", year);
            sql += " group by TypeName";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["TypeName"].ToString(),
                    Value = Convert.ToDouble(objReader["Qty"].ToString())
                });
            }
            objReader.Close();
            return list;
        }
    }
}
