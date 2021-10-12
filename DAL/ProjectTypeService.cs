using System;
using System.Collections.Generic;
using Models;
using System.Data.SqlClient;

namespace DAL
{
    public class ProjectTypeService
    {
        /// <summary>
        /// 获取所有项目类型
        /// </summary>
        /// <returns></returns>
        public List<ProjectType> GetAllProjectTypes(string sbu)
        {
            string sql = "select TypeId,TypeName,KMLink from ProjectTypes"+sbu;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<ProjectType> list = new List<ProjectType>();
            while (objReader.Read())
            {
                list.Add(new ProjectType()
                {
                    TypeId = Convert.ToInt32(objReader["TypeId"]),
                    TypeName = objReader["TypeName"].ToString(),
                    KMLink = objReader["KMLink"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据Id返回类型对象
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public ProjectType GetProjectTypeById(string typeId,string sbu)
        {
            string sql = "select TypeId,TypeName,KMLink from ProjectTypes"+sbu;
            sql += $" where TypeId={typeId}";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            ProjectType objProjectType = null;
            if (objReader.Read())
            {
                objProjectType = new ProjectType()
                {
                    TypeId = Convert.ToInt32(objReader["TypeId"]),
                    TypeName = objReader["TypeName"].ToString(),
                    KMLink = objReader["KMLink"].ToString()
                };
            }
            objReader.Close();
            return objProjectType;
        }
        /// <summary>
        /// 添加项目类型
        /// </summary>
        /// <param name="objProjectType"></param>
        /// <returns></returns>
        public int AddProjectType(ProjectType objProjectType, string sbu)
        {
            string sql = "insert into ProjectTypes{0} (TypeName,KMLink) values('{1}','{2}');select @@identity";
            sql = string.Format(sql, sbu,objProjectType.TypeName,objProjectType.KMLink);
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    throw new Exception("项目类型名称重复,不能添加重复的项目类型");
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
        /// 修改项目类型
        /// </summary>
        /// <param name="objProjectType"></param>
        /// <returns></returns>
        public int EditProjectType(ProjectType objProjectType, string sbu)
        {
            string sql = "update ProjectTypes{0} set TypeName='{1}',KMLink='{2}' where TypeId={3}";
            sql = string.Format(sql, sbu,objProjectType.TypeName,objProjectType.KMLink,objProjectType.TypeId);
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

        public int DeleteProjectType(string typeId, string sbu)
        {
            string sql = "delete from ProjectTypes{0} where TypeId={1}";
            sql = string.Format(sql, sbu,typeId);
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
