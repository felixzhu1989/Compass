using System;
using System.Collections.Generic;
using Models;
using System.Data.SqlClient;

namespace DAL
{
    public class ProjectStatusService
    {
        /// <summary>
        /// 返回所有项目状态
        /// </summary>
        /// <returns></returns>
        public List<ProjectStatus> GetAllProjectStatus()
        {
            string sql = "select ProjectStatusId,ProjectStatusName,StatusDesc from ProjectStatus";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<ProjectStatus> list = new List<ProjectStatus>();
            while (objReader.Read())
            {
                list.Add(new ProjectStatus()
                {
                    ProjectStatusId = Convert.ToInt32(objReader["ProjectStatusId"]),
                    ProjectStatusName = objReader["ProjectStatusName"].ToString(),
                    StatusDesc = objReader["StatusDesc"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据Id返回项目状态对象
        /// </summary>
        /// <param name="projectStatusId"></param>
        /// <returns></returns>
        public ProjectStatus GetProjectStatusById(string projectStatusId)
        {
            string sql = "select ProjectStatusId,ProjectStatusName,StatusDesc from ProjectStatus";
            sql += $" where ProjectStatusId='{projectStatusId}'";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            ProjectStatus objProjectStatus = null;
            if (objReader.Read())
            {
                objProjectStatus = new ProjectStatus()
                {
                    ProjectStatusId = Convert.ToInt32(objReader["ProjectStatusId"]),
                    ProjectStatusName = objReader["ProjectStatusName"].ToString(),
                    StatusDesc = objReader["StatusDesc"].ToString()
                };
            }
            objReader.Close();
            return objProjectStatus;
        }
        /// <summary>
        /// 添加项目状态
        /// </summary>
        /// <param name="objProjectStatus"></param>
        /// <returns></returns>
        public int AddProjectStatus(ProjectStatus objProjectStatus)
        {
            string sql = "insert into ProjectStatus (ProjectStatusName,StatusDesc) values('{0}','{1}');select @@identity";
            sql = string.Format(sql, objProjectStatus.ProjectStatusName,objProjectStatus.StatusDesc);
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    throw new Exception("项目状态名称重复,不能添加重复的项目状态");
                }
                else
                {
                    throw new Exception("添加项目库时数据库访问异常" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改项目状态
        /// </summary>
        /// <param name="objProjectStatus"></param>
        /// <returns></returns>
        public int EditProjectStatus(ProjectStatus objProjectStatus)
        {
            string sql = "update ProjectStatus set ProjectStatusName='{0}',StatusDesc='{1}' where ProjectStatusId={2}";
            sql = string.Format(sql, objProjectStatus.ProjectStatusName,objProjectStatus.StatusDesc,objProjectStatus.ProjectStatusId);
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
        /// 删除项目状态
        /// </summary>
        /// <param name="projectStatusId"></param>
        /// <returns></returns>
        public int DeleteProjectStatus(string projectStatusId)
        {
            string sql = "delete from ProjectStatus where ProjectStatusId='{0}'";
            sql = string.Format(sql, projectStatusId);
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
