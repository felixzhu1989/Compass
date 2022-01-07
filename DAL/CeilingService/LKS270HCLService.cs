using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class LKS270HCLService : IModelService
    {
        public int EditModel(IModel model)
        {
            LKS270HCL objModel = (LKS270HCL)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LKS270HCL set Length=@Length,HCLSide=@HCLSide,HCLSideLeft=@HCLSideLeft,HCLSideRight=@HCLSideRight where LKS270HCLId=@LKS270HCLId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@HCLSide",objModel.HCLSide),
                new SqlParameter("@HCLSideLeft",objModel.HCLSideLeft),
                new SqlParameter("@HCLSideRight",objModel.HCLSideRight),

                new SqlParameter("@LKS270HCLId",objModel.LKS270HCLId)
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
            string sql = "select LKS270HCLId,LKS270HCL.ModuleTreeId,Item,Module,Length,HCLSide,HCLSideLeft,HCLSideRight from LKS270HCL";
            sql += " inner join ModuleTree on LKS270HCL.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where LKS270HCLId={id}");
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LKS270HCLId,ModuleTreeId,Length,HCLSide,HCLSideLeft,HCLSideRight from LKS270HCL";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LKS270HCL objModel = null;
            if (objReader.Read())
            {
                objModel = new LKS270HCL()
                {
                    LKS270HCLId = Convert.ToInt32(objReader["LKS270HCLId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Length"]),
                    HCLSide = objReader["HCLSide"].ToString().Length == 0 ? "" : objReader["HCLSide"].ToString(),
                    HCLSideLeft = objReader["HCLSideLeft"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["HCLSideLeft"]),
                    HCLSideRight = objReader["HCLSideRight"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["HCLSideRight"]),
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
