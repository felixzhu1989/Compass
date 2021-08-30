using System;
using System.Data.SqlClient;
using Models;

namespace DAL
{
    public class FinancialDataService
    {
        /// <summary>
        /// add FinancialData
        /// </summary>
        /// <param name="objFinancialData"></param>
        /// <param name="sbu"></param>
        /// <returns></returns>
        public int AddFinancialData(FinancialData objFinancialData, string sbu)
        {
            string sql = string.Format("insert into FinancialData{0} (ProjectId,SalesValue) values({1},{2})", sbu,
                objFinancialData.ProjectId, objFinancialData.SalesValue);
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            }
            catch (SqlException ex)
            {
                //2627
                if (ex.Number == 2627)
                {
                    throw new Exception("项目号重复,不能添加重复的财务数据");
                }
                else
                {
                    throw new Exception("添加财务数据库访问异常" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Update FinancialData
        /// </summary>
        /// <param name="objFinancialData"></param>
        /// <param name="sbu"></param>
        /// <returns></returns>
        public int EditFinancialData(FinancialData objFinancialData, string sbu)
        {
            string sql = string.Format("update FinancialData{0} set SalesValue={1} where ProjectId={2}", sbu,objFinancialData.SalesValue,objFinancialData.ProjectId);
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

        public FinancialData GetFinancialDataByProjectId(string projectId, string sbu)
        {
            return GetFinancialDataByWhereSql(string.Format(" where ProjectId={0}", projectId), sbu);
        }


        public FinancialData GetFinancialDataByWhereSql(string whereSql, string sbu)
        {
            string sql = string.Format("select FinancialDataId,ProjectId,SalesValue from FinancialData{0}", sbu);
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            FinancialData objFinancialData = null;
            if (objReader.Read())
            {
                objFinancialData = new FinancialData()
                {
                    FinancialDataId = Convert.ToInt32(objReader["FinancialDataId"]),
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    SalesValue = Convert.ToDecimal(objReader["SalesValue"])
                };
            }
            objReader.Close();
            return objFinancialData;
        }
    }
}
