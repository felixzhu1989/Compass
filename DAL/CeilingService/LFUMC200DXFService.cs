using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class LFUMC200DXFService : IModelService
    {
        public int EditModel(IModel model)
        {
            LFUMC200DXF objModel = (LFUMC200DXF)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LFUMC200DXF set Quantity=@Quantity where LFUMC200DXFId=@LFUMC200DXFId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Quantity",objModel.Quantity),
                new SqlParameter("@LFUMC200DXFId",objModel.LFUMC200DXFId)
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
            string sql = "select LFUMC200DXFId,LFUMC200DXF.ModuleTreeId,Item,Module,Quantity from LFUMC200DXF";
            sql += " inner join ModuleTree on LFUMC200DXF.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where LFUMC200DXFId={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LFUMC200DXFId,ModuleTreeId,Quantity from LFUMC200DXF";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LFUMC200DXF objModel = null;
            if (objReader.Read())
            {
                objModel = new LFUMC200DXF()
                {
                    LFUMC200DXFId = Convert.ToInt32(objReader["LFUMC200DXFId"]),
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
