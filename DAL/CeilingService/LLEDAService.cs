using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class LLEDAService : IModelService
    {
        public int EditModel(IModel model)
        {
            LLEDA objModel = (LLEDA)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LLEDA set Length=@Length where LLEDAId=@LLEDAId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@LLEDAId",objModel.LLEDAId)
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
            string sql = "select LLEDAId,LLEDA.ModuleTreeId,Item,Module,Length from LLEDA";
            sql += " inner join ModuleTree on LLEDA.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where LLEDAId={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LLEDAId,ModuleTreeId,Length from LLEDA";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LLEDA objModel = null;
            if (objReader.Read())
            {
                objModel = new LLEDA()
                {
                    LLEDAId = Convert.ToInt32(objReader["LLEDAId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Length"])
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
