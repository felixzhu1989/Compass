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
            string whereSql="";
            if (Convert.ToInt32(month)==12)
            {
                whereSql = string.Format(" where ShippingTime >='{0}/{1}/01' and ShippingTime <='{2}/01/31'", year, month, Convert.ToInt32(year) + 1);
            }
            else
            {
                whereSql = string.Format(" where ShippingTime like'{0}%' and month(ShippingTime) between {1} and {2} ", year, month, Convert.ToInt32(month) + 1);
            }
            return whereSql;
        }
        /// <summary>
        /// 获取需要循环的项目列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetScrollODPNoList()
        {
            string sql = "select ODPNo from Projects";
            //sql += " inner join ProjectTracking on ProjectTracking.ProjectId=Projects.ProjectId";
            //sql += " where ProjectStatusId between 1 and 4 order by ShippingTime asc";
            sql += WhereSql(DateTime.Now.Year.ToString(),DateTime.Now.Month.ToString()); 

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
        /// 获取需要展示的项目列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDisplayProjects(string sbu)
        {
            StringBuilder sql = new StringBuilder(string.Format("select Projects{0}.ProjectId,ODPNo,BPONo,ProjectName,Projects{0}.CustomerId,CustomerName,ShippingTime,Projects{0}.UserId,UserAccount,TypeName,RiskLevel,ProjectStatusName,HoodType,SalesValue from Projects{0}", sbu));
            sql.Append(string.Format(" inner join Users on Projects{0}.UserId=Users.UserId", sbu));
            sql.Append(string.Format(" inner join Customers on Projects{0}.CustomerId=Customers.CustomerId", sbu));
            sql.Append(string.Format(" inner join ProjectTracking{0} on Projects{0}.ProjectId=ProjectTracking{0}.ProjectId", sbu));
            sql.Append(string.Format(" inner join ProjectStatus on ProjectTracking{0}.ProjectStatusId=ProjectStatus.ProjectStatusId", sbu));
            sql.Append(string.Format(" left join GeneralRequirements{0} on Projects{0}.ProjectId=GeneralRequirements{0}.ProjectId", sbu));
            sql.Append(string.Format(" left join FinancialData{0} on Projects{0}.ProjectId=FinancialData{0}.ProjectId", sbu));
            sql.Append(string.Format(" left join ProjectTypes{0} on ProjectTypes{0}.TypeId=GeneralRequirements{0}.TypeId", sbu));
            string whereSql = WhereSql(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
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
            sql += WhereSql(year, month);
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
            sql += WhereSql(year, month);
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
            sql += WhereSql(year, month);
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
