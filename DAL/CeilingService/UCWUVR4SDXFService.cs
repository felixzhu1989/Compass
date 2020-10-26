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
    public class UCWUVR4SDXFService : IModelService
    {
        public int EditModel(IModel model)
        {
            UCWUVR4SDXF objModel = (UCWUVR4SDXF)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update UCWUVR4SDXF set Quantity=@Quantity where UCWUVR4SDXFId=@UCWUVR4SDXFId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Quantity",objModel.Quantity),
                new SqlParameter("@UCWUVR4SDXFId",objModel.UCWUVR4SDXFId)
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
            string sql = "select UCWUVR4SDXFId,UCWUVR4SDXF.ModuleTreeId,Item,Module,Quantity from UCWUVR4SDXF";
            sql += " inner join ModuleTree on UCWUVR4SDXF.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where UCWUVR4SDXFId={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select UCWUVR4SDXFId,ModuleTreeId,Quantity from UCWUVR4SDXF";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            UCWUVR4SDXF objModel = null;
            if (objReader.Read())
            {
                objModel = new UCWUVR4SDXF()
                {
                    UCWUVR4SDXFId = Convert.ToInt32(objReader["UCWUVR4SDXFId"]),
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
