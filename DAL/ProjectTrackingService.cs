using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.Data.SqlClient;

namespace DAL
{
    public class ProjectTrackingService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sbu"></param>
        /// <returns></returns>
        public SqlDataPager GetSqlDataPager(string sbu)
        {
            StringBuilder innerJoin = new StringBuilder(string.Format("inner join ProjectStatus on ProjectStatus.ProjectStatusId=ProjectTracking{0}.ProjectStatusId", sbu));
            innerJoin.Append(string.Format(" inner join Projects{0} on ProjectTracking{0}.ProjectId=Projects{0}.ProjectId", sbu));
            innerJoin.Append(string.Format(" inner join Users on Projects{0}.UserId=Users.UserId", sbu));
            innerJoin.Append(string.Format(" left join (select ProjectId,max(DrReleaseTarget)as DrReleaseTarget from DrawingPlan{0} group by ProjectId) as PlanList on PlanList.ProjectId=Projects{0}.ProjectId", sbu));

            //初始化分页对象
            SqlDataPager objSqlDataPager = new SqlDataPager()
            {
                PrimaryKey = "ProjectTrackingId",
                TableName = string.Format("ProjectTracking{0}", sbu),
                InnerJoin1 = innerJoin.ToString(),
                InnerJoin2 = string.Format("inner join Projects{0} on ProjectTracking{0}.ProjectId=Projects{0}.ProjectId", sbu),
                FiledName = "ProjectTrackingId,ODPNo,ProjectStatusName,DrReleaseTarget,DrReleaseActual,ShippingTime,ProdFinishActual,DeliverActual,ProjectName,ODPReceiveDate,KickOffDate,UserAccount",
                CurrentPage = 1,
                Sort = "ShippingTime desc",
            };
            return objSqlDataPager;
        }






        /// <summary>
        /// 根据项目状态返回项目跟踪合集
        /// </summary>
        /// <param name="projectStatusId"></param>
        /// <returns></returns>
        public List<ProjectTracking> GetProjectTrackingsByStatus(string projectStatusId, string sbu)
        {
            return GetProjectTrackingsByWhereSql(string.Format(" where ProjectTracking{0}.ProjectStatusId = {1}", sbu, projectStatusId), sbu);
        }
        /// <summary>
        /// 根据项目Id返回项目跟踪记录合集
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<ProjectTracking> GetProjectTrackingsByProjectId(string projectId, string sbu)
        {
            return GetProjectTrackingsByWhereSql(string.Format(" where ProjectTracking{0}.ProjectId = {1}", sbu, projectId), sbu);
        }
        /// <summary>
        /// 根据项目ODP返回项目跟踪记录合集
        /// </summary>
        /// <param name="odpNo"></param>
        /// <returns></returns>
        public List<ProjectTracking> GetProjectTrackingsByODPNo(string odpNo, string sbu)
        {
            return GetProjectTrackingsByWhereSql(string.Format(" where ODPNo = {0}", odpNo), sbu);
        }
        /// <summary>
        /// 根据单个条件返回项目跟踪合集
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public List<ProjectTracking> GetProjectTrackingsByWhereSql(string whereSql, string sbu)
        {
            string sql = string.Format("select distinct ProjectTracking{0}.ProjectId,ProjectTrackingId,ODPNo,ProjectTracking{0}.ProjectStatusId,ProjectStatusName,DrReleaseTarget,DrReleaseActual,ShippingTime,ProdFinishActual,DeliverActual,ProjectName,ODPReceiveDate,KickOffDate,UserAccount from ProjectTracking{0}", sbu);
            sql += string.Format(" inner join ProjectStatus on ProjectStatus.ProjectStatusId=ProjectTracking.ProjectStatusId", sbu);
            sql += string.Format(" inner join Projects{0} on ProjectTracking{0}.ProjectId=Projects{0}.ProjectId", sbu);
            sql += string.Format(" inner join Users on Projects{0}.UserId=Users.UserId", sbu);
            sql += string.Format(" left join (select ProjectId,max(DrReleaseTarget)as DrReleaseTarget from DrawingPlan{0} group by ProjectId) as PlanList on PlanList.ProjectId=Projects{0}.ProjectId", sbu);
            sql += whereSql;
            sql += " order by ShippingTime desc";//按照计划发货日期，倒序排列
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<ProjectTracking> list = new List<ProjectTracking>();
            while (objReader.Read())
            {
                list.Add(new ProjectTracking()
                {
                    ProjectTrackingId = Convert.ToInt32(objReader["ProjectTrackingId"]),
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    ODPNo = objReader["ODPNo"].ToString(),
                    ProjectStatusId = Convert.ToInt32(objReader["ProjectStatusId"]),
                    ProjectStatusName = objReader["ProjectStatusName"].ToString(),
                    DrReleaseTarget = objReader["DrReleaseTarget"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["DrReleaseTarget"]),
                    DrReleaseActual = objReader["DrReleaseActual"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["DrReleaseActual"]),
                    ShippingTime = objReader["ShippingTime"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["ShippingTime"]),
                    ProdFinishActual = objReader["ProdFinishActual"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["ProdFinishActual"]),
                    DeliverActual = objReader["DeliverActual"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["DeliverActual"]),
                    ODPReceiveDate = objReader["ODPReceiveDate"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["ODPReceiveDate"]),
                    KickOffDate = objReader["KickOffDate"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["KickOffDate"]),

                    ProjectName = objReader["ProjectName"].ToString(),
                    UserAccount = objReader["UserAccount"].ToString(),
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据Id返回跟踪记录
        /// </summary>
        /// <param name="projectTrackingId"></param>
        /// <returns></returns>
        public ProjectTracking GetProjectTrackingById(string projectTrackingId, string sbu)
        {
            return GetProjectTrackingByWhereSql(string.Format(" where ProjectTrackingId = {0}", projectTrackingId), sbu);
        }
        /// <summary>
        /// 根据项目Id返回项目跟踪记录
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ProjectTracking GetProjectTrackingByProjectId(string projectId, string sbu)
        {
            return GetProjectTrackingByWhereSql(string.Format(" where ProjectTracking{0}.ProjectId = {1}", sbu, projectId), sbu);
        }
        /// <summary>
        /// 根据ODP号返回项目跟踪记录
        /// </summary>
        /// <param name="odpNo"></param>
        /// <returns></returns>
        public ProjectTracking GetProjectTrackingByODPNo(string odpNo, string sbu)
        {
            return GetProjectTrackingByWhereSql(string.Format(" where ODPNo = '{0}'", odpNo), sbu);
        }
        /// <summary>
        /// 根据单个条件返回项目跟踪记录
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public ProjectTracking GetProjectTrackingByWhereSql(string whereSql, string sbu)
        {
            string sql = string.Format("select ProjectTrackingId,ProjectTracking{0}.ProjectId,ODPNo,ProjectTracking{0}.ProjectStatusId,ProjectStatusName,DrReleaseTarget,DrReleaseActual,ShippingTime,ProdFinishActual,DeliverActual,ProjectName,ODPReceiveDate,KickOffDate from ProjectTracking{0}", sbu);
            sql += string.Format(" inner join ProjectStatus on ProjectStatus.ProjectStatusId=ProjectTracking{0}.ProjectStatusId", sbu);
            sql += string.Format(" inner join Projects{0} on ProjectTracking{0}.ProjectId=Projects{0}.ProjectId", sbu);
            sql += string.Format(" left join (select ProjectId,max(DrReleaseTarget)as DrReleaseTarget from DrawingPlan{0} group by ProjectId) as PlanList on PlanList.ProjectId=Projects{0}.ProjectId", sbu);
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            ProjectTracking objProjectTracking = null;
            if (objReader.Read())
            {
                objProjectTracking = new ProjectTracking()
                {
                    ProjectTrackingId = Convert.ToInt32(objReader["ProjectTrackingId"]),
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    ODPNo = objReader["ODPNo"].ToString(),
                    ProjectStatusId = Convert.ToInt32(objReader["ProjectStatusId"]),
                    ProjectStatusName = objReader["ProjectStatusName"].ToString(),
                    DrReleaseTarget = objReader["DrReleaseTarget"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["DrReleaseTarget"]),
                    DrReleaseActual = objReader["DrReleaseActual"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["DrReleaseActual"]),
                    ShippingTime = objReader["ShippingTime"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["ShippingTime"]),
                    ProdFinishActual = objReader["ProdFinishActual"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["ProdFinishActual"]),
                    DeliverActual = objReader["DeliverActual"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["DeliverActual"]),
                    ODPReceiveDate = objReader["ODPReceiveDate"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["ODPReceiveDate"]),
                    KickOffDate = objReader["KickOffDate"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["KickOffDate"]),

                    ProjectName = objReader["ProjectName"].ToString()
                };
            }
            objReader.Close();
            return objProjectTracking;
        }

        /// <summary>
        /// 添加项目跟踪记录
        /// </summary>
        /// <param name="objProjectTracking"></param>
        /// <returns></returns>
        public int AddProjectTracking(ProjectTracking objProjectTracking, string sbu)
        {
            string sql = string.Format("insert into ProjectTracking{0} (ProjectId,ProjectStatusId,DrReleaseActual,ProdFinishActual,DeliverActual)", sbu);
            sql += " values({0},{1},'{2}','{3}','{4}');select @@identity";
            sql = string.Format(sql, objProjectTracking.ProjectId, objProjectTracking.ProjectStatusId,
                objProjectTracking.DrReleaseActual, objProjectTracking.ShippingTime,
                 objProjectTracking.ProdFinishActual, objProjectTracking.DeliverActual);
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            }
            catch (SqlException ex)
            {
                //2627
                if (ex.Number == 2627)
                {
                    throw new Exception("项目号重复,不能添加重复的跟踪记录");
                }
                else
                {
                    throw new Exception("添加项目跟踪时数据库访问异常" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改项目跟踪记录
        /// </summary>
        /// <param name="objProjectTracking"></param>
        /// <returns></returns>
        public int EditProjectTracing(ProjectTracking objProjectTracking, string sbu)
        {
            string sql = string.Format("update ProjectTracking{0}", sbu);
            sql += " set ProjectId={0},ProjectStatusId={1},DrReleaseActual='{2}',";
            sql += "ProdFinishActual='{3}',DeliverActual='{4}',ODPReceiveDate='{5}',KickOffDate='{6}' where ProjectTrackingId={7}";
            sql = string.Format(sql, objProjectTracking.ProjectId, objProjectTracking.ProjectStatusId, objProjectTracking.DrReleaseActual,
                objProjectTracking.ProdFinishActual, objProjectTracking.DeliverActual, objProjectTracking.ODPReceiveDate, objProjectTracking.KickOffDate, objProjectTracking.ProjectTrackingId);
            try
            {
                return SQLHelper.Update(sql);
            }
            catch (SqlException ex)
            {
                throw new Exception("数据库操作出现异常：" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除项目跟踪记录
        /// </summary>
        /// <param name="projectTrackingId"></param>
        /// <returns></returns>
        public int DeleteProjectTracking(string projectTrackingId, string sbu)
        {
            string sql = string.Format("delete from ProjectTracking{0}", sbu);
            sql += " where ProjectTrackingId={0}";
            sql = string.Format(sql, projectTrackingId);
            try
            {
                return SQLHelper.Update(sql);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new Exception("该记录已被其他数据表关联，不能直接删除");
                }
                else
                {
                    throw new Exception("数据库操作异常，不能执行删除：" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
