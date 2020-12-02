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
    public class HOODBCJService : IModelService
    {
        public int EditModel(IModel model)
        {
            HOODBCJ objModel = (HOODBCJ)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update HOODBCJ set Length=@Length,Height=@Height,SuDis=@SuDis where HOODBCJId=@HOODBCJId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@Height",objModel.Height),
                new SqlParameter("@SuDis",objModel.SuDis),
                new SqlParameter("@HOODBCJId",objModel.HOODBCJId)
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
            string sql = "select HOODBCJId,HOODBCJ.ModuleTreeId,Item,Module,Length,Height,SuDis from HOODBCJ";
            sql += " inner join ModuleTree on HOODBCJ.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where HOODBCJId={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select HOODBCJId,ModuleTreeId,Length,Height,SuDis from HOODBCJ";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            HOODBCJ objModel = null;
            if (objReader.Read())
            {
                objModel = new HOODBCJ()
                {
                    HOODBCJId = Convert.ToInt32(objReader["HOODBCJId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Length"]),
                    Height = objReader["Height"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Height"]),
                    SuDis = objReader["SuDis"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["SuDis"]),

                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
