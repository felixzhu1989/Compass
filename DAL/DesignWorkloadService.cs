using System;
using System.Collections.Generic;
using Models;
using System.Data.SqlClient;

namespace DAL
{
    public class DesignWorkloadService
    {
        /// <summary>
        /// 获取所有设计工作量集合
        /// </summary>
        /// <returns></returns>
        public List<DesignWorkload> GetAllDesignWorkload(string sbu)
        {
            string sql = $"select WorkloadId,Model,WorkloadValue,ModelDesc from DesignWorkload{sbu}";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<DesignWorkload> list = new List<DesignWorkload>();
            while (objReader.Read())
            {
                list.Add(new DesignWorkload()
                {
                    WorkloadId = Convert.ToInt32(objReader["WorkloadId"]),
                    Model = objReader["Model"].ToString(),
                    WorkloadValue = Convert.ToDecimal(objReader["WorkloadValue"]),
                    ModelDesc = objReader["ModelDesc"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 添加工作量
        /// </summary>
        /// <param name="objDesignWorkload"></param>
        /// <returns></returns>
        public int AddDesignWorkload(DesignWorkload objDesignWorkload, string sbu)
        {
            string sql = $"insert into DesignWorkload{sbu} (Model,WorkloadValue,ModelDesc)";
            sql += " values('{0}',{1},'{2}');select @@identity";
            sql = string.Format(sql, objDesignWorkload.Model, objDesignWorkload.WorkloadValue, objDesignWorkload.ModelDesc);
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            }
            catch (SqlException ex)
            {
                throw new Exception("添加工作量时数据库访问异常" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据Id返回工作量对象
        /// </summary>
        /// <param name="workloadId"></param>
        /// <returns></returns>
        public DesignWorkload GetDesignWorkloadById(string workloadId, string sbu)
        {
            string sql = $"select WorkloadId,Model,WorkloadValue,ModelDesc from DesignWorkload{sbu}";
            sql += " where  WorkloadId={0}";
            sql = string.Format(sql, workloadId);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            DesignWorkload objDesignWorkload = null;
            if (objReader.Read())
            {
                objDesignWorkload = new DesignWorkload()
                {
                    WorkloadId = Convert.ToInt32(objReader["WorkloadId"]),
                    Model = objReader["Model"].ToString(),
                    WorkloadValue = Convert.ToDecimal(objReader["WorkloadValue"]),
                    ModelDesc = objReader["ModelDesc"].ToString()
                };
            }
            objReader.Close();
            return objDesignWorkload;
        }
        /// <summary>
        /// 修改工作量
        /// </summary>
        /// <param name="objDesignWorkload"></param>
        /// <returns></returns>
        public int EditDesignWorkload(DesignWorkload objDesignWorkload, string sbu)
        {
            string sql = $"update DesignWorkload{sbu}";
            sql += " set Model='{0}',WorkloadValue={1},ModelDesc='{2}'";
            sql += " where WorkloadId={3}";
            sql = string.Format(sql, objDesignWorkload.Model, objDesignWorkload.WorkloadValue,
                objDesignWorkload.ModelDesc, objDesignWorkload.WorkloadId);
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
        /// 删除工作量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int DeleteDesignWorkload(string workloadId, string sbu)
        {
            string sql = $"delete from DesignWorkload{sbu}";
            sql += " where WorkloadId={0}";
            sql = string.Format(sql, workloadId);
            try
            {
                return SQLHelper.Update(sql);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new Exception("该工作量条目已被其他数据表关联，不能直接删除");
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
