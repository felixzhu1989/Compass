using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class LLEDSService : IModelService
    {
        public int EditModel(IModel model)
        {
            LLEDS objModel = (LLEDS)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LLEDS set Length=@Length where LLEDSId=@LLEDSId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@LLEDSId",objModel.LLEDSId)
            };
            try
            {
                return SQLHelper.Update(sqlBuilder.ToString(), param);
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

        public DataSet GetModelByDataSet(string projectId)
        {
            string sql = "select LLEDSId,LLEDS.ModuleTreeId,Item,Module,Length from LLEDS";
            sql += " inner join ModuleTree on LLEDS.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where LLEDSId={id}");
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LLEDSId,ModuleTreeId,Length from LLEDS";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LLEDS objModel = null;
            if (objReader.Read())
            {
                objModel = new LLEDS()
                {
                    LLEDSId = Convert.ToInt32(objReader["LLEDSId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Length"])
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
