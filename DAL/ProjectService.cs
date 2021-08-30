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
            StringBuilder innerJoin = new StringBuilder(string.Format("inner join Users on Projects{0}.UserId=Users.UserId", sbu));
            innerJoin.Append(string.Format(" inner join Customers on Projects{0}.CustomerId=Customers.CustomerId", sbu));
            innerJoin.Append(string.Format(" inner join ProjectTracking{0} on Projects{0}.ProjectId=ProjectTracking{0}.ProjectId", sbu));
            innerJoin.Append(string.Format(" inner join ProjectStatus on ProjectTracking{0}.ProjectStatusId=ProjectStatus.ProjectStatusId", sbu));
            innerJoin.Append(string.Format(" left join GeneralRequirements{0} on Projects{0}.ProjectId=GeneralRequirements{0}.ProjectId", sbu));
            innerJoin.Append(string.Format(" left join FinancialData{0} on Projects{0}.ProjectId=FinancialData{0}.ProjectId", sbu));
            innerJoin.Append(string.Format(" left join ProjectTypes{0} on ProjectTypes{0}.TypeId=GeneralRequirements{0}.TypeId", sbu));

            //初始化分页对象
            SqlDataPager objSqlDataPager = new SqlDataPager()
            {
                PrimaryKey = string.Format("Projects{0}.ProjectId",
                    sbu),
                TableName = "Projects" + sbu,
                InnerJoin1 = innerJoin.ToString(),
                FiledName = string.Format("Projects{0}.ProjectId,ODPNo,BPONo,ProjectName,CustomerName,ShippingTime,UserAccount,TypeName,RiskLevel,ProjectStatusName,HoodType,SalesValue", sbu),
                CurrentPage = 1,
                Sort = "ShippingTime desc",
            };
            return objSqlDataPager;
        }
        

        /// <summary>
        /// 根据烟罩类型返回项目集合
        /// </summary>
        /// <param name="hoodType"></param>
        /// <returns></returns>
        public List<Project> GetProjectsByHoodType(string hoodType, string sbu)
        {
            return GetProjectsByWhereSql(string.Format(" where HoodType = '{0}'", hoodType), sbu);
        }
        /// <summary>
        /// 根据项目号返回集合，其实只有一个订单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Project> GetProjectsByODPNo(string odpNo, string sbu)
        {
            return GetProjectsByWhereSql(string.Format(" where ODPNo = '{0}'", odpNo), sbu);
        }
        /// <summary>
        /// 根据制图人员返回项目集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Project> GetProjectsByUserId(string userId, string sbu)
        {
            return GetProjectsByWhereSql(string.Format(" where Projects{0}.UserId = {1}", sbu, userId), sbu);
        }

        /// <summary>
        /// 根据where条件返回项目集合
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public List<Project> GetProjectsByWhereSql(string whereSql, string sbu)
        {
            StringBuilder sql = new StringBuilder(string.Format("select Projects{0}.ProjectId,ODPNo,BPONo,ProjectName,Projects{0}.CustomerId,CustomerName,ShippingTime,Projects{0}.UserId,UserAccount,TypeName,RiskLevel,ProjectStatusName,HoodType,SalesValue from Projects{0}", sbu));
            sql.Append(string.Format(" inner join Users on Projects{0}.UserId=Users.UserId", sbu));
            sql.Append(string.Format(" inner join Customers on Projects{0}.CustomerId=Customers.CustomerId", sbu));
            sql.Append(string.Format(" inner join ProjectTracking{0} on Projects{0}.ProjectId=ProjectTracking{0}.ProjectId", sbu));
            sql.Append(string.Format(" inner join ProjectStatus on ProjectTracking{0}.ProjectStatusId=ProjectStatus.ProjectStatusId", sbu));
            sql.Append(string.Format(" left join GeneralRequirements{0} on Projects{0}.ProjectId=GeneralRequirements{0}.ProjectId", sbu));
            sql.Append(string.Format(" left join FinancialData{0} on Projects{0}.ProjectId=FinancialData{0}.ProjectId", sbu));
            sql.Append(string.Format(" left join ProjectTypes{0} on ProjectTypes{0}.TypeId=GeneralRequirements{0}.TypeId", sbu));
            sql.Append(whereSql);
            sql.Append(" order by ShippingTime desc");//按照发货日期，倒序排列

            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            List<Project> list = new List<Project>();
            while (objReader.Read())
            {
                list.Add(new Project()
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
                    RiskLevel = objReader["RiskLevel"].ToString().Length == 0 ? 3 : Convert.ToInt32(objReader["RiskLevel"]),
                    ProjectStatusName = objReader["ProjectStatusName"].ToString(),
                    HoodType = objReader["HoodType"].ToString(),
                    SalesValue = objReader["SalesValue"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["SalesValue"])
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据ODPNo返回单个项目信息
        /// </summary>
        /// <param name="odpNo"></param>
        /// <returns></returns>
        public Project GetProjectByODPNo(string odpNo, string sbu)
        {
            return GetProjectByWhereSql(string.Format(" where ODPNo='{0}'", odpNo), sbu);
        }
        /// <summary>
        /// 根据序号返回单个项目信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Project GetProjectByProjectId(string projectId, string sbu)
        {
            return GetProjectByWhereSql(string.Format(" where Projects{0}.ProjectId={1}", sbu, projectId), sbu);
        }
        /// <summary>
        /// 根据条件返回单个项目信息
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public Project GetProjectByWhereSql(string whereSql, string sbu)
        {
            string sql = string.Format("select Projects{0}.ProjectId,ODPNo,BPONo,ProjectName,Projects{0}.CustomerId,CustomerName,ShippingTime,Projects{0}.UserId,UserAccount,RiskLevel,HoodType,SalesValue from Projects{0}", sbu);
            sql += string.Format(" inner join Users on Projects{0}.UserId=Users.UserId", sbu);
            sql += string.Format(" inner join Customers on Projects{0}.CustomerId=Customers.CustomerId", sbu);
            sql += string.Format(" left join GeneralRequirements{0} on Projects{0}.ProjectId=GeneralRequirements{0}.ProjectId", sbu);
            sql += string.Format(" left join FinancialData{0} on Projects{0}.ProjectId=FinancialData{0}.ProjectId", sbu);
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            Project objProject = null;
            if (objReader.Read())
            {
                objProject = new Project()
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
                    RiskLevel = objReader["RiskLevel"].ToString().Length == 0 ? 3 : Convert.ToInt32(objReader["RiskLevel"]),
                    HoodType = objReader["HoodType"].ToString(),
                    SalesValue = objReader["SalesValue"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["SalesValue"])
                };
            }
            objReader.Close();
            return objProject;
        }
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
            List<string> sqlList = new List<string>();
            sqlList.Add(sql);
            string sqlTracking = string.Format("insert into ProjectTracking{0} (ProjectId,ProjectStatusId) values(@@IDENTITY,1)", sbu);
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
            List<string> sqlList = new List<string>();
            sqlList.Add(string.Format("delete from ProjectTracking{0} where ProjectId={1}", sbu, projectId));
            sqlList.Add(string.Format("delete from FinancialData{0} where ProjectId={1}", sbu, projectId));
            sqlList.Add(string.Format("delete from Projects{0} where ProjectId={1}", sbu, projectId));
            return SQLHelper.UpdateByTransaction(sqlList);
        }

        public bool EditProjectAndFinancialData(Project objProject, string sbu)
        {
            List<string> sqlList = new List<string>();
            string sqlProject = "update Projects{0} set ODPNo='{1}',BPONo='{2}',ProjectName='{3}',CustomerId={4},";
            sqlProject += "ShippingTime='{5}',UserId={6},HoodType='{7}' where ProjectId={8}";
            sqlProject = string.Format(sqlProject, sbu,objProject.ODPNo, objProject.BPONo, objProject.ProjectName, objProject.CustomerId,objProject.ShippingTime, objProject.UserId, objProject.HoodType, objProject.ProjectId);
            sqlList.Add(sqlProject);
            string sqlFinancialData = string.Format("update FinancialData{0} set SalesValue={1} where ProjectId={2}", sbu,objProject.SalesValue,objProject.ProjectId);
            sqlList.Add(sqlFinancialData);

            return SQLHelper.UpdateByTransaction(sqlList);
        }
    }
}
