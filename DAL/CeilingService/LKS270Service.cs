using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class LKS270Service : IModelService
    {
        public int EditModel(IModel model)
        {
            LKS270 objModel = (LKS270)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LKS270 set Length=@Length,WBeam=@WBeam,SidePanel=@SidePanel,LightType=@LightType,Japan=@Japan where LKS270Id=@LKS270Id");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@WBeam",objModel.WBeam),
                new SqlParameter("@SidePanel",objModel.SidePanel),
                new SqlParameter("@LightType",objModel.LightType),
                new SqlParameter("@Japan",objModel.Japan),

                new SqlParameter("@LKS270Id",objModel.LKS270Id)
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
            string sql = "select LKS270Id,LKS270.ModuleTreeId,Item,Module,Length,WBeam,SidePanel,LightType,Japan from LKS270";
            sql += " inner join ModuleTree on LKS270.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where LKS270Id={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LKS270Id,ModuleTreeId,Length,WBeam,SidePanel,LightType,Japan from LKS270";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LKS270 objModel = null;
            if (objReader.Read())
            {
                objModel = new LKS270()
                {
                    LKS270Id = Convert.ToInt32(objReader["LKS270Id"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Length"]),
                    WBeam = objReader["WBeam"].ToString().Length == 0 ? "" : objReader["WBeam"].ToString(),
                    SidePanel = objReader["SidePanel"].ToString().Length == 0 ? "" : objReader["SidePanel"].ToString(),
                    LightType = objReader["LightType"].ToString().Length == 0 ? "" : objReader["LightType"].ToString(),
                    Japan = objReader["Japan"].ToString().Length == 0 ? "" : objReader["Japan"].ToString(),
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
