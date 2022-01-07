using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class LFUSCService : IModelService
    {
        public int EditModel(IModel model)
        {
            LFUSC objModel = (LFUSC)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LFUSC set Length=@Length,SuNo=@SuNo,SuDia=@SuDia,SuDis=@SuDis,Japan=@Japan where LFUSCId=@LFUSCId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@SuNo",objModel.SuNo),
                new SqlParameter("@SuDia",objModel.SuDia),
                new SqlParameter("@SuDis",objModel.SuDis),
                new SqlParameter("@Japan",objModel.Japan),
                new SqlParameter("@LFUSCId",objModel.LFUSCId)
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
            string sql = "select LFUSCId,LFUSC.ModuleTreeId,Item,Module,Length,SuNo,SuDia,SuDis,Japan from LFUSC";
            sql += " inner join ModuleTree on LFUSC.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where LFUSCId={id}");
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LFUSCId,ModuleTreeId,Length,SuNo,SuDia,SuDis,Japan from LFUSC";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LFUSC objModel = null;
            if (objReader.Read())
            {
                objModel = new LFUSC()
                {
                    LFUSCId = Convert.ToInt32(objReader["LFUSCId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Length"]),
                    SuNo = objReader["SuNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["SuNo"]),
                    SuDia = objReader["SuDia"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["SuDia"]),
                    SuDis = objReader["SuDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["SuDis"]),

                    Japan = objReader["Japan"].ToString().Length == 0 ? "" : objReader["Japan"].ToString()
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
