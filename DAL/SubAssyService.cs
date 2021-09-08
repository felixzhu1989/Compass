using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class SubAssyService
    {
        /// <summary>
        /// 根据项目Id返回子装配合集
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<SubAssy> GetSubAssysByProjectId(string projectId) => GetSubAssysByWhereSql($" where SubAssy.ProjectId={projectId}");
        /// <summary>
        /// 根据单个条件返回子装配合集
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public List<SubAssy> GetSubAssysByWhereSql(string whereSql)
        {
            StringBuilder sql = new StringBuilder("select SubAssyId,SubAssy.ProjectId,SubAssyName,SubAssyPath,ODPNo,ProjectName from SubAssy");
            sql.Append(" inner join Projects on SubAssy.ProjectId=Projects.ProjectId");
            sql.Append(whereSql);
            sql.Append(" order by SubAssyName asc");
            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            List<SubAssy> list = new List<SubAssy>();
            while (objReader.Read())
            {
                list.Add(new SubAssy()
                {
                    SubAssyId = Convert.ToInt32(objReader["SubAssyId"]),
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    ODPNo = objReader["ODPNo"].ToString(),
                    ProjectName = objReader["ProjectName"].ToString(),
                    SubAssyName = objReader["SubAssyName"].ToString(),
                    SubAssyPath = objReader["SubAssyPath"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据ID返回单个子装配对象
        /// </summary>
        /// <param name="subAssyId"></param>
        /// <returns></returns>
        public SubAssy GetSubAssyId(string subAssyId)
        {
            string sql = "select SubAssyId,SubAssy.ProjectId,SubAssyName,SubAssyPath,ODPNo,ProjectName from SubAssy";
            sql += " inner join Projects on SubAssy.ProjectId=Projects.ProjectId";
            sql += $" where SubAssyId={subAssyId}";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            SubAssy objSubAssy = null;
            while (objReader.Read())
            {
                objSubAssy = new SubAssy()
                {
                    SubAssyId = Convert.ToInt32(objReader["SubAssyId"]),
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    ODPNo = objReader["ODPNo"].ToString(),
                    ProjectName = objReader["ProjectName"].ToString(),
                    SubAssyName = objReader["SubAssyName"].ToString(),
                    SubAssyPath = objReader["SubAssyPath"].ToString()
                };
            }
            objReader.Close();
            return objSubAssy;
        }

        /// <summary>
        /// 将子装配对象批量插入到SQL中
        /// </summary>
        /// <param name="hoodCutLists"></param>
        /// <returns></returns>
        public bool ImportSubAssy(List<SubAssy> subAssys)
        {
            //编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder("insert into SubAssy (ProjectId,SubAssyName,SubAssyPath) values({0},'{1}','{2}')");
            List<string> sqlList = new List<string>();//用来保存生成的多条SQL语句
            //解析对象
            foreach (SubAssy objSunAssy in subAssys)
            {
                string sql = string.Format(sqlBuilder.ToString(), objSunAssy.ProjectId, objSunAssy.SubAssyName, objSunAssy.SubAssyPath);
                //将解析的SQL语句添加到集合
                sqlList.Add(sql);
            }
            //将SQL语句集合提交到数据库
            return SQLHelper.UpdateByTransaction(sqlList);
        }

        /// <summary>
        /// 根据ID删除子装配对象
        /// </summary>
        /// <param name="subAssyId"></param>
        /// <returns></returns>
        public int DeleteSubAssy(string subAssyId)
        {
            string sql = "delete from SubAssy where SubAssyId={0}";
            sql = string.Format(sql, subAssyId);
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
        /// 添加SubAssy
        /// </summary>
        /// <param name="objSubAssy"></param>
        /// <returns></returns>
        public int AddSubAssy(SubAssy objSubAssy)
        {
            string sql = "insert into SubAssy (ProjectId,SubAssyName)";
            sql += " values({0},'{1}');select @@identity";
            sql = string.Format(sql, objSubAssy.ProjectId, objSubAssy.SubAssyName);
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            }
            catch (SqlException ex)
            {
                //2627
                if (ex.Number == 2627)
                {
                    throw new Exception("子装配重复,不能添加重复的子装配信息");
                }
                else
                {
                    throw new Exception("添加子装配时数据库访问异常" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
