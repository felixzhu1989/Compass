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
   public class MU1BOXDXFService : IModelService
    {
        public int EditModel(IModel model)
        {
            MU1BOXDXF objModel = (MU1BOXDXF)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update MU1BOXDXF set Quantity=@Quantity where MU1BOXDXFId=@MU1BOXDXFId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Quantity",objModel.Quantity),
                new SqlParameter("@MU1BOXDXFId",objModel.MU1BOXDXFId)
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
            string sql = "select MU1BOXDXFId,MU1BOXDXF.ModuleTreeId,Item,Module,Quantity from MU1BOXDXF";
            sql += " inner join ModuleTree on MU1BOXDXF.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where MU1BOXDXFId={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select MU1BOXDXFId,ModuleTreeId,Quantity from MU1BOXDXF";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            MU1BOXDXF objModel = null;
            if (objReader.Read())
            {
                objModel = new MU1BOXDXF()
                {
                    MU1BOXDXFId = Convert.ToInt32(objReader["MU1BOXDXFId"]),
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
