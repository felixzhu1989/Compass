using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    public class ProjectService
    {
        /// <summary>
        /// 根据烟罩类型返回项目集合
        /// </summary>
        /// <param name="hoodType"></param>
        /// <returns></returns>
        public List<Project> GetProjectsByHoodType(string hoodType)
        {
            return GetProjectsByWhereSql(string.Format(" where HoodType = '{0}'", hoodType));
        }
        /// <summary>
        /// 根据项目号返回集合，其实只有一个订单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Project> GetProjectsByODPNo(string odpNo)
        {
            return GetProjectsByWhereSql(string.Format(" where ODPNo = '{0}'", odpNo));
        }
        /// <summary>
        /// 根据制图人员返回项目集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Project> GetProjectsByUserId(string userId)
        {
            return GetProjectsByWhereSql(string.Format(" where Projects.UserId = {0}", userId));
        }
        /// <summary>
        /// 根据项目库返回项目集合
        /// </summary>
        /// <param name="vaultId"></param>
        /// <returns></returns>
        public List<Project> GetProjectsByVaultId(string vaultId)
        {
            return GetProjectsByWhereSql(string.Format(" where Projects.VaultId = {0}", vaultId));
        }
        /// <summary>
        /// 根据where条件返回项目集合
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public List<Project> GetProjectsByWhereSql(string whereSql)
        {
            StringBuilder sql = new StringBuilder("select Projects.ProjectId,ODPNo,BPONo,Projects.VaultId,VaultName,ProjectName,Projects.CustomerId,CustomerName,ShippingTime,Projects.UserId,UserAccount,RiskLevel,ProjectStatusName,HoodType from Projects");
            sql.Append(" inner join ProjectVaults on Projects.VaultId=ProjectVaults.VaultId");
            sql.Append(" inner join Users on Projects.UserId=Users.UserId");
            sql.Append(" inner join Customers on Projects.CustomerId=Customers.CustomerId");
            sql.Append(" inner join ProjectTracking on Projects.ProjectId=ProjectTracking.ProjectId");
            sql.Append(" inner join ProjectStatus on ProjectTracking.ProjectStatusId=ProjectStatus.ProjectStatusId");
            sql.Append(" left join GeneralRequirements on Projects.ProjectId=GeneralRequirements.ProjectId");
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
                    VaultId = Convert.ToInt32(objReader["VaultId"]),
                    VaultName = objReader["VaultName"].ToString(),
                    ProjectName = objReader["ProjectName"].ToString(),
                    CustomerId = Convert.ToInt32(objReader["CustomerId"]),
                    CustomerName = objReader["CustomerName"].ToString(),
                    ShippingTime = Convert.ToDateTime(objReader["ShippingTime"]),
                    UserId = Convert.ToInt32(objReader["UserId"]),
                    UserAccount = objReader["UserAccount"].ToString(),
                    RiskLevel = objReader["RiskLevel"].ToString().Length == 0 ? 4 : Convert.ToInt32(objReader["RiskLevel"]),
                    ProjectStatusName = objReader["ProjectStatusName"].ToString(),
                    HoodType = objReader["HoodType"].ToString()
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
        public Project GetProjectByODPNo(string odpNo)
        {
            return GetProjectByWhereSql(string.Format(" where ODPNo='{0}'", odpNo));
        }
        /// <summary>
        /// 根据序号返回单个项目信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Project GetProjectByProjectId(string projectId)
        {
            return GetProjectByWhereSql(string.Format(" where Projects.ProjectId={0}", projectId));
        }
        /// <summary>
        /// 根据条件返回单个项目信息
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public Project GetProjectByWhereSql(string whereSql)
        {
            string sql = "select Projects.ProjectId,ODPNo,BPONo,Projects.VaultId,VaultName,ProjectName,Projects.CustomerId,CustomerName,ShippingTime,Projects.UserId,UserAccount,RiskLevel,HoodType from Projects";
            sql += " inner join ProjectVaults on Projects.VaultId=ProjectVaults.VaultId";
            sql += " inner join Users on Projects.UserId=Users.UserId";
            sql += " inner join Customers on Projects.CustomerId=Customers.CustomerId";
            sql += " left join GeneralRequirements on Projects.ProjectId=GeneralRequirements.ProjectId";
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
                    VaultId = Convert.ToInt32(objReader["VaultId"]),
                    VaultName = objReader["VaultName"].ToString(),
                    ProjectName = objReader["ProjectName"].ToString(),
                    CustomerId = Convert.ToInt32(objReader["CustomerId"]),
                    CustomerName = objReader["CustomerName"].ToString(),
                    ShippingTime = Convert.ToDateTime(objReader["ShippingTime"]),
                    UserId = Convert.ToInt32(objReader["UserId"]),
                    UserAccount = objReader["UserAccount"].ToString(),
                    RiskLevel = objReader["RiskLevel"].ToString().Length == 0 ? 4 : Convert.ToInt32(objReader["RiskLevel"]),
                    HoodType = objReader["HoodType"].ToString()
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
        public int AddProject(Project objProject)
        {
            string sql = "insert into Projects (ODPNo,BPONo,VaultId,ProjectName,CustomerId,ShippingTime,UserId,HoodType)";
            sql += " values('{0}','{1}',{2},'{3}',{4},'{5}',{6},'{7}');select @@identity";
            sql = string.Format(sql, objProject.ODPNo, objProject.BPONo, objProject.VaultId,
                objProject.ProjectName, objProject.CustomerId, objProject.ShippingTime, objProject.UserId,objProject.HoodType);
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
        public int EditProject(Project objProject)
        {
            string sql = "update Projects set ODPNo='{0}',BPONo='{1}',VaultId={2},ProjectName='{3}',CustomerId={4},";
            sql += "ShippingTime='{5}',UserId={6},HoodType='{7}' where ProjectId={8}";
            sql = string.Format(sql, objProject.ODPNo, objProject.BPONo, objProject.VaultId, objProject.ProjectName, objProject.CustomerId,
                objProject.ShippingTime, objProject.UserId,objProject.HoodType, objProject.ProjectId);
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
        public int DeleteProject(string projectId)
        {
            string sql = "delete from Projects where ProjectId={0}";
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
        public bool AddProjectAndTracking(Project objProject)
        {
            //编写SQL语句
            string sql = "insert into Projects (ODPNo,BPONo,VaultId,ProjectName,CustomerId,ShippingTime,UserId,HoodType)";
            sql += " values('{0}','{1}',{2},'{3}',{4},'{5}',{6},'{7}');select @@identity";
            sql = string.Format(sql, objProject.ODPNo, objProject.BPONo, objProject.VaultId,
                objProject.ProjectName, objProject.CustomerId, objProject.ShippingTime, objProject.UserId,objProject.HoodType);
            List<string> sqlList = new List<string>();
            sqlList.Add(sql);
            string sqlTracking = "insert into ProjectTracking (ProjectId,ProjectStatusId) values(@@IDENTITY,1)";
            sqlList.Add(sqlTracking);
            return SQLHelper.UpdateByTransaction(sqlList);
        }
        /// <summary>
        /// 基于事务删除项目和项目跟踪，先删除跟踪后删除项目
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public bool DeleteProjectAndTracking(string projectId)
        {
            List<string> sqlList = new List<string>();
            sqlList.Add(string.Format("delete from ProjectTracking where ProjectId={0}", projectId));
            sqlList.Add(string.Format("delete from Projects where ProjectId={0}", projectId));
            return SQLHelper.UpdateByTransaction(sqlList);
        }
    }
}
