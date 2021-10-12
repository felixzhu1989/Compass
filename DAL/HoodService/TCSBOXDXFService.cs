using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class TCSBOXDXFService : IModelService
    {
        public int EditModel(IModel model)
        {
            TCSBOXDXF objModel = (TCSBOXDXF)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update TCSBOXDXF set Quantity=@Quantity where TCSBOXDXFId=@TCSBOXDXFId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Quantity",objModel.Quantity),
                new SqlParameter("@TCSBOXDXFId",objModel.TCSBOXDXFId)
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
            string sql = "select TCSBOXDXFId,TCSBOXDXF.ModuleTreeId,Item,Module,Quantity from TCSBOXDXF";
            sql += " inner join ModuleTree on TCSBOXDXF.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where TCSBOXDXFId={id}");
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select TCSBOXDXFId,ModuleTreeId,Quantity from TCSBOXDXF";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            TCSBOXDXF objModel = null;
            if (objReader.Read())
            {
                objModel = new TCSBOXDXF()
                {
                    TCSBOXDXFId = Convert.ToInt32(objReader["TCSBOXDXFId"]),
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
