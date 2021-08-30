using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class LFUPService : IModelService
    {
        public int EditModel(IModel model)
        {
            LFUP objModel = (LFUP)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LFUP set Length=@Length,Width=@Width where LFUPId=@LFUPId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@Width",objModel.Width),
                new SqlParameter("@LFUPId",objModel.LFUPId)
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
            string sql = "select LFUPId,LFUP.ModuleTreeId,Item,Module,Length,Width from LFUP";
            sql += " inner join ModuleTree on LFUP.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where LFUPId={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LFUPId,ModuleTreeId,Length,Width from LFUP";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LFUP objModel = null;
            if (objReader.Read())
            {
                objModel = new LFUP()
                {
                    LFUPId = Convert.ToInt32(objReader["LFUPId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Length"]),
                    Width = objReader["Width"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Width"])
                    
                };
            }
            objReader.Close();
            return objModel;
        }

    }
}
