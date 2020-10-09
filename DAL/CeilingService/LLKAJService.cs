using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
   public class LLKAJService : IModelService
    {
        public int EditModel(IModel model)
        {
            LLKAJ objModel = (LLKAJ)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LLKAJ set Length=@Length,LongGlassNo=@LongGlassNo,ShortGlassNo=@ShortGlassNo,LeftLength=@LeftLength,RightLength=@RightLength where LLKAJId=@LLKAJId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@LongGlassNo",objModel.LongGlassNo),
                new SqlParameter("@ShortGlassNo",objModel.ShortGlassNo),
                new SqlParameter("@LeftLength",objModel.LeftLength),
                new SqlParameter("@RightLength",objModel.RightLength),
                new SqlParameter("@LLKAJId",objModel.LLKAJId)
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
            string sql = "select LLKAJId,LLKAJ.ModuleTreeId,Item,Module,Length,LongGlassNo,ShortGlassNo,LeftLength,RightLength from LLKAJ";
            sql += " inner join ModuleTree on LLKAJ.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where LLKAJId={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LLKAJId,ModuleTreeId,Length,LongGlassNo,ShortGlassNo,LeftLength,RightLength from LLKAJ";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LLKAJ objModel = null;
            if (objReader.Read())
            {
                objModel = new LLKAJ()
                {
                    LLKAJId = Convert.ToInt32(objReader["LLKAJId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Length"]),
                    LongGlassNo = objReader["LongGlassNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["LongGlassNo"]),
                    ShortGlassNo = objReader["ShortGlassNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["ShortGlassNo"]),
                    LeftLength = objReader["LeftLength"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["LeftLength"]),
                    RightLength = objReader["RightLength"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["RightLength"])
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
