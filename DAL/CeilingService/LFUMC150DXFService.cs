using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class LFUMC150DXFService : IModelService
    {
        public int EditModel(IModel model)
        {
            LFUMC150DXF objModel = (LFUMC150DXF)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LFUMC150DXF set Quantity=@Quantity where LFUMC150DXFId=@LFUMC150DXFId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Quantity",objModel.Quantity),
                new SqlParameter("@LFUMC150DXFId",objModel.LFUMC150DXFId)
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
            string sql = "select LFUMC150DXFId,LFUMC150DXF.ModuleTreeId,Item,Module,Quantity from LFUMC150DXF";
            sql += " inner join ModuleTree on LFUMC150DXF.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where LFUMC150DXFId={id}");
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LFUMC150DXFId,ModuleTreeId,Quantity from LFUMC150DXF";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LFUMC150DXF objModel = null;
            if (objReader.Read())
            {
                objModel = new LFUMC150DXF()
                {
                    LFUMC150DXFId = Convert.ToInt32(objReader["LFUMC150DXFId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Quantity = objReader["Quantity"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["Quantity"]),
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
