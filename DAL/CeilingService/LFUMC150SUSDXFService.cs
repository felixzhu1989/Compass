using Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class LFUMC150SUSDXFService : IModelService
    {
        public int EditModel(IModel model)
        {
            LFUMC150SUSDXF objModel = (LFUMC150SUSDXF)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LFUMC150SUSDXF set Quantity=@Quantity where LFUMC150SUSDXFId=@LFUMC150SUSDXFId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Quantity",objModel.Quantity),
                new SqlParameter("@LFUMC150SUSDXFId",objModel.LFUMC150SUSDXFId)
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
            string sql = "select LFUMC150SUSDXFId,LFUMC150SUSDXF.ModuleTreeId,Item,Module,Quantity from LFUMC150SUSDXF";
            sql += " inner join ModuleTree on LFUMC150SUSDXF.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where LFUMC150SUSDXFId={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LFUMC150SUSDXFId,ModuleTreeId,Quantity from LFUMC150SUSDXF";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LFUMC150SUSDXF objModel = null;
            if (objReader.Read())
            {
                objModel = new LFUMC150SUSDXF()
                {
                    LFUMC150SUSDXFId = Convert.ToInt32(objReader["LFUMC150SUSDXFId"]),
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
