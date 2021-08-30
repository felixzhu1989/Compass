using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class LLKSService : IModelService
    {
        public int EditModel(IModel model)
        {
            LLKS objModel = (LLKS)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LLKS set Length=@Length,LongGlassNo=@LongGlassNo,ShortGlassNo=@ShortGlassNo where LLKSId=@LLKSId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@LongGlassNo",objModel.LongGlassNo),
                new SqlParameter("@ShortGlassNo",objModel.ShortGlassNo),
                new SqlParameter("@LLKSId",objModel.LLKSId)
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
            string sql = "select LLKSId,LLKS.ModuleTreeId,Item,Module,Length,LongGlassNo,ShortGlassNo from LLKS";
            sql += " inner join ModuleTree on LLKS.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where LLKSId={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LLKSId,ModuleTreeId,Length,LongGlassNo,ShortGlassNo from LLKS";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LLKS objModel = null;
            if (objReader.Read())
            {
                objModel = new LLKS()
                {
                    LLKSId = Convert.ToInt32(objReader["LLKSId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Length"]),
                    LongGlassNo = objReader["LongGlassNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["LongGlassNo"]),
                    ShortGlassNo = objReader["ShortGlassNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["ShortGlassNo"])
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
