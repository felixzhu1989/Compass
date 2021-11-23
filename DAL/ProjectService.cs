using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.Data.SqlClient;
namespace DAL
{
    public class ProjectService
    {

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sbu"></param>
        /// <returns></returns>
        public SqlDataPager GetSqlDataPager(string sbu)
        {
            StringBuilder innerJoin = new StringBuilder($"inner join Users on Projects{sbu}.UserId=Users.UserId");
            innerJoin.Append($" inner join Customers on Projects{sbu}.CustomerId=Customers.CustomerId");
            innerJoin.Append($" inner join ProjectTracking{sbu} on Projects{sbu}.ProjectId=ProjectTracking{sbu}.ProjectId");
            innerJoin.Append(
                $" inner join ProjectStatus on ProjectTracking{sbu}.ProjectStatusId=ProjectStatus.ProjectStatusId");
            innerJoin.Append($" left join GeneralRequirements{sbu} on Projects{sbu}.ProjectId=GeneralRequirements{sbu}.ProjectId");
            innerJoin.Append($" left join FinancialData{sbu} on Projects{sbu}.ProjectId=FinancialData{sbu}.ProjectId");
            innerJoin.Append($" left join ProjectTypes{sbu} on ProjectTypes{sbu}.TypeId=GeneralRequirements{sbu}.TypeId");
            innerJoin.Append($" left join (select ProjectId,SUM(SubTotalWorkload) as TotalWorkload from DrawingPlan{sbu} group by ProjectId)workload on workload.ProjectId = Projects{sbu}.ProjectId");


            //初始化分页对象
            SqlDataPager objSqlDataPager = new SqlDataPager
            {
                PrimaryKey = $"Projects{sbu}.ProjectId",
                TableName = "Projects" + sbu,
                InnerJoin1 = innerJoin.ToString(),
                FiledName =
                    $"Projects{sbu}.ProjectId,ODPNo,BPONo,ProjectName,CustomerName,ShippingTime,UserAccount,TypeName,RiskLevel,ProjectStatusName,HoodType,SalesValue,TotalWorkload",
                CurrentPage = 1,
                Sort = "ShippingTime desc",
            };
            return objSqlDataPager;
        }

        #region 项目集合

        /// <summary>
        /// 根据烟罩类型返回项目集合
        /// </summary>
        /// <param name="hoodType"></param>
        /// <returns></returns>
        public List<Project> GetProjectsByHoodType(string hoodType, string sbu)
        {
            return GetProjectsByWhereSql($" where HoodType = '{hoodType}'", sbu);
        }
        /// <summary>
        /// 根据项目号返回集合，其实只有一个订单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Project> GetProjectsByODPNo(string odpNo, string sbu)
        {
            return GetProjectsByWhereSql($" where ODPNo = '{odpNo}'", sbu);
        }
        /// <summary>
        /// 根据制图人员返回项目集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Project> GetProjectsByUserId(string userId, string sbu)
        {
            return GetProjectsByWhereSql($" where Projects{sbu}.UserId = {userId}", sbu);
        }

        /// <summary>
        /// 根据where条件返回项目集合
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public List<Project> GetProjectsByWhereSql(string whereSql, string sbu)
        {
            StringBuilder sql = new StringBuilder($"select Projects{sbu}.ProjectId,ODPNo,BPONo,ProjectName,Projects{sbu}.CustomerId,CustomerName,ShippingTime,Projects{sbu}.UserId,UserAccount,TypeName,RiskLevel,ProjectStatusName,HoodType,SalesValue,TotalWorkload from Projects{sbu}");
            sql.Append($" inner join Users on Projects{sbu}.UserId=Users.UserId");
            sql.Append($" inner join Customers on Projects{sbu}.CustomerId=Customers.CustomerId");
            sql.Append($" inner join ProjectTracking{sbu} on Projects{sbu}.ProjectId=ProjectTracking{sbu}.ProjectId");
            sql.Append(
                $" inner join ProjectStatus on ProjectTracking{sbu}.ProjectStatusId=ProjectStatus.ProjectStatusId");
            sql.Append($" left join GeneralRequirements{sbu} on Projects{sbu}.ProjectId=GeneralRequirements{sbu}.ProjectId");
            sql.Append($" left join FinancialData{sbu} on Projects{sbu}.ProjectId=FinancialData{sbu}.ProjectId");
            sql.Append($" left join ProjectTypes{sbu} on ProjectTypes{sbu}.TypeId=GeneralRequirements{sbu}.TypeId");
            sql.Append($" left join (select ProjectId,SUM(SubTotalWorkload) as TotalWorkload from DrawingPlan{sbu} group by ProjectId)workload on workload.ProjectId = Projects{sbu}.ProjectId");
            sql.Append(whereSql);
            sql.Append(" order by ShippingTime desc");//按照发货日期，倒序排列

            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            List<Project> list = new List<Project>();
            while (objReader.Read())
            {
                list.Add(new Project
                {
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    ODPNo = objReader["ODPNo"].ToString(),
                    BPONo = objReader["BPONo"].ToString(),
                    ProjectName = objReader["ProjectName"].ToString(),
                    CustomerId = Convert.ToInt32(objReader["CustomerId"]),
                    CustomerName = objReader["CustomerName"].ToString(),
                    ShippingTime = Convert.ToDateTime(objReader["ShippingTime"]),
                    UserId = Convert.ToInt32(objReader["UserId"]),
                    UserAccount = objReader["UserAccount"].ToString(),
                    RiskLevel = objReader["RiskLevel"] == DBNull.Value ? 3 : Convert.ToInt32(objReader["RiskLevel"]),
                    ProjectStatusName = objReader["ProjectStatusName"].ToString(),
                    HoodType = objReader["HoodType"].ToString(),
                    SalesValue = objReader["SalesValue"] == DBNull.Value ? 0 : Convert.ToDecimal(objReader["SalesValue"]),
                    TotalWorkload = objReader["TotalWorkload"] == DBNull.Value ? 0 : Convert.ToDecimal(objReader["TotalWorkload"])
                });
            }
            objReader.Close();
            return list;
        }
        #endregion

        #region 单个项目

        /// <summary>
        /// 根据ODPNo返回单个项目信息
        /// </summary>
        /// <param name="odpNo"></param>
        /// <returns></returns>
        public Project GetProjectByODPNo(string odpNo, string sbu)
        {
            return GetProjectByWhereSql($" where ODPNo='{odpNo}'", sbu);
        }
        /// <summary>
        /// 根据序号返回单个项目信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Project GetProjectByProjectId(string projectId, string sbu)
        {
            return GetProjectByWhereSql($" where Projects{sbu}.ProjectId={projectId}", sbu);
        }
        /// <summary>
        /// 根据条件返回单个项目信息
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public Project GetProjectByWhereSql(string whereSql, string sbu)
        {
            StringBuilder sql = new StringBuilder($"select Projects{sbu}.ProjectId,ODPNo,BPONo,ProjectName,Projects{sbu}.CustomerId,CustomerName,ShippingTime,Projects{sbu}.UserId,UserAccount,RiskLevel,HoodType,SalesValue,TotalWorkload from Projects{sbu}");
            sql.Append($" inner join Users on Projects{sbu}.UserId=Users.UserId");
            sql.Append($" inner join Customers on Projects{sbu}.CustomerId=Customers.CustomerId");
            sql.Append($" left join GeneralRequirements{sbu} on Projects{sbu}.ProjectId=GeneralRequirements{sbu}.ProjectId");
            sql.Append($" left join FinancialData{sbu} on Projects{sbu}.ProjectId=FinancialData{sbu}.ProjectId");
            sql.Append($" left join (select ProjectId,SUM(SubTotalWorkload) as TotalWorkload from DrawingPlan{sbu} group by ProjectId)workload on workload.ProjectId = Projects{sbu}.ProjectId");

            sql.Append(whereSql);
            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            Project objProject = null;
            if (objReader.Read())
            {
                objProject = new Project
                {
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    ODPNo = objReader["ODPNo"].ToString(),
                    BPONo = objReader["BPONo"].ToString(),
                    ProjectName = objReader["ProjectName"].ToString(),
                    CustomerId = Convert.ToInt32(objReader["CustomerId"]),
                    CustomerName = objReader["CustomerName"].ToString(),
                    ShippingTime = Convert.ToDateTime(objReader["ShippingTime"]),
                    UserId = Convert.ToInt32(objReader["UserId"]),
                    UserAccount = objReader["UserAccount"].ToString(),
                    RiskLevel = objReader["RiskLevel"] == DBNull.Value ? 3 : Convert.ToInt32(objReader["RiskLevel"]),
                    HoodType = objReader["HoodType"].ToString(),
                    SalesValue = objReader["SalesValue"] == DBNull.Value ? 0 : Convert.ToDecimal(objReader["SalesValue"]),
                    TotalWorkload = objReader["TotalWorkload"] == DBNull.Value ? 0 : Convert.ToDecimal(objReader["TotalWorkload"])
                };
            }
            objReader.Close();
            return objProject;
        } 
        #endregion

        /// <summary>
        /// 添加项目信息
        /// </summary>
        /// <param name="objProject"></param>
        /// <returns></returns>
        public int AddProject(Project objProject, string sbu)
        {
            string sql = "insert into Projects" + sbu;
            sql += "(ODPNo,BPONo,ProjectName,CustomerId,ShippingTime,UserId,HoodType)";
            sql += " values('{0}','{1}','{2}',{3},'{4}',{5},'{6}');select @@identity";
            sql = string.Format(sql, objProject.ODPNo, objProject.BPONo,
                objProject.ProjectName, objProject.CustomerId, objProject.ShippingTime, objProject.UserId, objProject.HoodType);
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            }
            catch (SqlException ex)
            {
                //2627
                if (ex.Number == 2627)
                {
                    throw new Exception("项目号重复,不能添加重复的项目信息");
                }
                else
                {
                    throw new Exception("添加项目信息时数据库访问异常" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改项目信息
        /// </summary>
        /// <param name="objProject"></param>
        /// <returns></returns>
        public int EditProject(Project objProject, string sbu)
        {
            string sql = "update Projects" + sbu;
            sql += " set ODPNo='{0}',BPONo='{1}',ProjectName='{2}',CustomerId={3},";
            sql += "ShippingTime='{4}',UserId={5},HoodType='{6}' where ProjectId={7}";
            sql = string.Format(sql, objProject.ODPNo, objProject.BPONo, objProject.ProjectName, objProject.CustomerId,
                objProject.ShippingTime, objProject.UserId, objProject.HoodType, objProject.ProjectId);
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
        /// 删除项目信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public int DeleteProject(string projectId, string sbu)
        {
            string sql = "delete from Projects" + sbu;
            sql += " where ProjectId={0}";
            sql = string.Format(sql, projectId);
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


        /// <summary>
        /// 基于事务添加项目和项目跟踪，先添加项目后添加跟踪
        /// </summary>
        /// <param name="objProject"></param>
        /// <returns></returns>
        public bool AddProjectAndTracking(Project objProject, string sbu)
        {
            //编写SQL语句
            string sql = "insert into Projects" + sbu;
            sql += " (ODPNo,BPONo,ProjectName,CustomerId,ShippingTime,UserId,HoodType)";
            sql += " values('{0}','{1}','{2}',{3},'{4}',{5},'{6}');select @@identity";
            sql = string.Format(sql, objProject.ODPNo, objProject.BPONo,
                objProject.ProjectName, objProject.CustomerId, objProject.ShippingTime, objProject.UserId, objProject.HoodType);
            List<string> sqlList = new List<string>{sql};
            string sqlTracking = $"insert into ProjectTracking{sbu} (ProjectId,ProjectStatusId) values(@@IDENTITY,1)";
            sqlList.Add(sqlTracking);
            return SQLHelper.UpdateByTransaction(sqlList);
        }
        /// <summary>
        /// 基于事务删除项目和项目跟踪，先删除跟踪后删除项目
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public bool DeleteProjectAndTracking(string projectId, string sbu)
        {
            List<string> sqlList = new List<string>
            {
                $"delete from ProjectTracking{sbu} where ProjectId={projectId}",
                $"delete from FinancialData{sbu} where ProjectId={projectId}",
                $"delete from Projects{sbu} where ProjectId={projectId}"
            };
            return SQLHelper.UpdateByTransaction(sqlList);
        }

        public bool EditProjectAndFinancialData(Project objProject, string sbu)
        {
            List<string> sqlList = new List<string>();
            string sqlProject = "update Projects{0} set ODPNo='{1}',BPONo='{2}',ProjectName='{3}',CustomerId={4},";
            sqlProject += "ShippingTime='{5}',UserId={6},HoodType='{7}' where ProjectId={8}";
            sqlProject = string.Format(sqlProject, sbu,objProject.ODPNo, objProject.BPONo, objProject.ProjectName, objProject.CustomerId,objProject.ShippingTime, objProject.UserId, objProject.HoodType, objProject.ProjectId);
            sqlList.Add(sqlProject);
            string sqlFinancialData =
                $"update FinancialData{sbu} set SalesValue={objProject.SalesValue} where ProjectId={objProject.ProjectId}";
            sqlList.Add(sqlFinancialData);

            return SQLHelper.UpdateByTransaction(sqlList);
        }
    }
}
