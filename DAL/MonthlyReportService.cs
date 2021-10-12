using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
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
        public string WhereSql(string year,string month)
        {
            string whereSql;
            if (Convert.ToInt32(month)==12)
            {
                whereSql = $" where ShippingTime >='{year}/11/01' and ShippingTime <'{Convert.ToInt32(year) + 1}/02/1'";
            }
            else if (Convert.ToInt32(month) == 1)
            {
                whereSql = $" where ShippingTime >='{Convert.ToInt32(year) - 1}/12/01' and ShippingTime <'{year}/03/1'";
            }
            else
            {
                whereSql =
                    $" where ShippingTime like'{year}%' and month(ShippingTime) between {Convert.ToInt32(month) - 1} and {Convert.ToInt32(month) + 1} ";
            }
            return whereSql;
        }
        /// <summary>
        /// 获取需要循环的项目列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetScrollODPNoList()
        {
            StringBuilder sql =new StringBuilder( "select ODPNo from Projects");
            //sql += " inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId";
            //sql += " where ProjectStatusId between 1 and 4 order by ShippingTime asc";
            sql.Append(WhereSql(DateTime.Now.Year.ToString(),DateTime.Now.Month.ToString()));
            sql.Append(" order by ShippingTime asc");
            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            List<string> list = new List<string>();
            while (objReader.Read())
            {
                list.Add(objReader["ODPNo"].ToString());
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 获取需要展示的项目列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDisplayProjects(string sbu, string year, string month)
        {
            StringBuilder sql = new StringBuilder(
                $"select Projects{sbu}.ProjectId,ODPNo,BPONo,ProjectName,Projects{sbu}.CustomerId,CustomerName,ShippingTime,Projects{sbu}.UserId,UserAccount,TypeName,RiskLevel,ProjectStatusName,HoodType,SalesValue from Projects{sbu}");
            sql.Append($" inner join Users on Projects{sbu}.UserId=Users.UserId");
            sql.Append($" inner join Customers on Projects{sbu}.CustomerId=Customers.CustomerId");
            sql.Append($" inner join ProjectTracking{sbu} on Projects{sbu}.ProjectId=ProjectTracking{sbu}.ProjectId");
            sql.Append(
                $" inner join ProjectStatus on ProjectTracking{sbu}.ProjectStatusId=ProjectStatus.ProjectStatusId");
            sql.Append(
                $" left join GeneralRequirements{sbu} on Projects{sbu}.ProjectId=GeneralRequirements{sbu}.ProjectId");
            sql.Append($" left join FinancialData{sbu} on Projects{sbu}.ProjectId=FinancialData{sbu}.ProjectId");
            sql.Append($" left join ProjectTypes{sbu} on ProjectTypes{sbu}.TypeId=GeneralRequirements{sbu}.TypeId");
            string whereSql = WhereSql(year,month);
            sql.Append(whereSql);
            sql.Append(" order by ShippingTime desc");//按照发货日期，倒序排列

            DataSet ds = SQLHelper.GetDataSet(sql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 项目数量月度统计
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public string GetProjectNum(string year, string month)
        {
            string sql = "select count(*) from Projects"+WhereSql(year, month);
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
            string sql = $"select count(*) from Projects where ShippingTime like'{year}%'";
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
            StringBuilder sql =new StringBuilder("select ProjectStatusId,count(ProjectStatusId) as Qty from ProjectTracking");
            sql.Append(" inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId");
            sql.Append( WhereSql(year, month));
            sql.Append( " group by ProjectStatusId order by ProjectStatusId asc");
            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["ProjectStatusId"].ToString(),
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
            StringBuilder sql =new StringBuilder( "select ProjectStatusName,count(ProjectStatusName) as Qty from ProjectTracking");
            sql.Append( " inner join ProjectStatus on ProjectStatus.ProjectStatusId=ProjectTracking.ProjectStatusId");
            sql.Append(" inner join Projects on ProjectTracking.ProjectId=Projects.ProjectId");
            sql.Append($" where ShippingTime like'{year}%'");
            sql.Append(" group by ProjectStatusName");
            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
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
            StringBuilder sql =new StringBuilder( "select RiskLevel,count(RiskLevel) as Qty from GeneralRequirements");
            sql.Append(" inner join Projects on GeneralRequirements.ProjectId=Projects.ProjectId");
            sql.Append(WhereSql(year, month));
            sql.Append(" group by RiskLevel");
            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = "风险等级-" + objReader["RiskLevel"],
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
                    Text = "风险等级-" + objReader["RiskLevel"],
                    Value = Convert.ToDouble(objReader["Qty"].ToString())
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 项目类型月度统计(按照金额计算)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<ChartData> GetProjectType(string year, string month)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select TypeId,sum(SalesValue) as SalesValue from GeneralRequirements";
            sql += " inner join Projects on GeneralRequirements.ProjectId=Projects.ProjectId";
            sql += " inner join FinancialData on Projects.ProjectId=FinancialData.ProjectId";
            sql += WhereSql(year, month);
            sql += " group by TypeId order by TypeId asc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["TypeId"].ToString(),
                    Value = Convert.ToDouble((Convert.ToDouble(objReader["SalesValue"].ToString())/10000d).ToString("N2"))
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 项目类型年度统计（按照金额计算）
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetProjectType(string year)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select TypeId,sum(SalesValue) as SalesValue from GeneralRequirements";
            sql += " inner join Projects on GeneralRequirements.ProjectId=Projects.ProjectId";
            sql += " inner join FinancialData on Projects.ProjectId=FinancialData.ProjectId";
            sql += string.Format(" where ShippingTime like'{0}%'", year);
            sql += " group by TypeId order by TypeId asc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["TypeId"].ToString(),
                    Value = Convert.ToDouble((Convert.ToDouble(objReader["SalesValue"].ToString()) / 10000d).ToString("N2"))
                });
            }
            objReader.Close();
            return list;
        }
    }
}
