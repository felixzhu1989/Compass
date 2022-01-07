using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public  class LLKAService : IModelService
    {
        public int EditModel(IModel model)
        {
            LLKA objModel = (LLKA)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LLKA set Length=@Length,LongGlassNo=@LongGlassNo,ShortGlassNo=@ShortGlassNo where LLKAId=@LLKAId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@LongGlassNo",objModel.LongGlassNo),
                new SqlParameter("@ShortGlassNo",objModel.ShortGlassNo),
                new SqlParameter("@LLKAId",objModel.LLKAId)
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
            string sql = "select LLKAId,LLKA.ModuleTreeId,Item,Module,Length,LongGlassNo,ShortGlassNo from LLKA";
            sql += " inner join ModuleTree on LLKA.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where LLKAId={id}");
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LLKAId,ModuleTreeId,Length,LongGlassNo,ShortGlassNo from LLKA";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LLKA objModel = null;
            if (objReader.Read())
            {
                objModel = new LLKA()
                {
                    LLKAId = Convert.ToInt32(objReader["LLKAId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Length"]),
                    LongGlassNo = objReader["LongGlassNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["LongGlassNo"]),
                    ShortGlassNo = objReader["ShortGlassNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["ShortGlassNo"])
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
