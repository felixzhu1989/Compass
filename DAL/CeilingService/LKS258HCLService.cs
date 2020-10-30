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
    public class LKS258HCLService : IModelService
    {
        public int EditModel(IModel model)
        {
            LKS258HCL objModel = (LKS258HCL)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LKS258HCL set Length=@Length,HCLSide=@HCLSide,HCLSideLeft=@HCLSideLeft,HCLSideRight=@HCLSideRight where LKS258HCLId=@LKS258HCLId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@HCLSide",objModel.HCLSide),
                new SqlParameter("@HCLSideLeft",objModel.HCLSideLeft),
                new SqlParameter("@HCLSideRight",objModel.HCLSideRight),

                new SqlParameter("@LKS258HCLId",objModel.LKS258HCLId)
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
            string sql = "select LKS258HCLId,LKS258HCL.ModuleTreeId,Item,Module,Length,HCLSide,HCLSideLeft,HCLSideRight from LKS258HCL";
            sql += " inner join ModuleTree on LKS258HCL.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where LKS258HCLId={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LKS258HCLId,ModuleTreeId,Length,HCLSide,HCLSideLeft,HCLSideRight from LKS258HCL";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LKS258HCL objModel = null;
            if (objReader.Read())
            {
                objModel = new LKS258HCL()
                {
                    LKS258HCLId = Convert.ToInt32(objReader["LKS258HCLId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Length"]),
                    HCLSide = objReader["HCLSide"].ToString().Length == 0 ? "" : objReader["HCLSide"].ToString(),
                    HCLSideLeft = objReader["HCLSideLeft"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["HCLSideLeft"]),
                    HCLSideRight = objReader["HCLSideRight"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["HCLSideRight"]),
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
