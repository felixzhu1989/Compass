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
    public class ProjectVaultService
    {
        /// <summary>
        /// 返回所有项目库对象
        /// </summary>
        /// <returns></returns>
        public List<ProjectVault> GetAllProjectVaults()
        {
            string sql = "select VaultId,VaultName from ProjectVaults";
            sql += " order by VaultName asc";//按照项目库，顺序排列
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<ProjectVault> list = new List<ProjectVault>();
            while (objReader.Read())
            {
                list.Add(new ProjectVault()
                {
                    VaultId = Convert.ToInt32(objReader["VaultId"]),
                    VaultName = objReader["VaultName"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据项目库Id返回项目库对象
        /// </summary>
        /// <param name="vaultId"></param>
        /// <returns></returns>
        public ProjectVault GetProjectVaultById(string vaultId)
        {
            string sql = "select VaultId,VaultName from ProjectVaults";
            sql += string.Format(" where VaultId={0}", vaultId);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            ProjectVault objProjectVault=null;
            if (objReader.Read())
            {
                objProjectVault=new ProjectVault()
                {
                    VaultId = Convert.ToInt32(objReader["VaultId"]),
                    VaultName = objReader["VaultName"].ToString()
                };
            }
            objReader.Close();
            return objProjectVault;
        }

        /// <summary>
        /// 添加项目库
        /// </summary>
        /// <param name="objProjectVault"></param>
        /// <returns></returns>
        public int AddProjectVault(ProjectVault objProjectVault)
        {
            string sql = "insert into ProjectVaults (VaultName) values('{0}');select @@identity";
            sql = string.Format(sql, objProjectVault.VaultName);
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    throw new Exception("库名称重复,不能添加重复的项目库");
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
        /// 修改项目库
        /// </summary>
        /// <param name="objProjectVault"></param>
        /// <returns></returns>
        public int EditProjectValult(ProjectVault objProjectVault)
        {
            string sql = "update ProjectVaults set VaultName='{0}' where VaultId={1}";
            sql = string.Format(sql, objProjectVault.VaultName,objProjectVault.VaultId);
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
        /// 删除项目库
        /// </summary>
        /// <param name="vaultId"></param>
        /// <returns></returns>
        public int DeleteProjectVault(string vaultId)
        {
            string sql = "delete from ProjectVaults where VaultId={0}";
            sql = string.Format(sql, vaultId);
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
