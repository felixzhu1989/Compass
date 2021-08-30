using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class LFUMC200SUSDXFService : IModelService
    {
        public int EditModel(IModel model)
        {
            LFUMC200SUSDXF objModel = (LFUMC200SUSDXF)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LFUMC200SUSDXF set Quantity=@Quantity where LFUMC200SUSDXFId=@LFUMC200SUSDXFId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Quantity",objModel.Quantity),
                new SqlParameter("@LFUMC200SUSDXFId",objModel.LFUMC200SUSDXFId)
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
            string sql = "select LFUMC200SUSDXFId,LFUMC200SUSDXF.ModuleTreeId,Item,Module,Quantity from LFUMC200SUSDXF";
            sql += " inner join ModuleTree on LFUMC200SUSDXF.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where LFUMC200SUSDXFId={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LFUMC200SUSDXFId,ModuleTreeId,Quantity from LFUMC200SUSDXF";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LFUMC200SUSDXF objModel = null;
            if (objReader.Read())
            {
                objModel = new LFUMC200SUSDXF()
                {
                    LFUMC200SUSDXFId = Convert.ToInt32(objReader["LFUMC200SUSDXFId"]),
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
