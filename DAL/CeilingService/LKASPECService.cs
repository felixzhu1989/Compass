using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class LKASPECService : IModelService
    {
        public int EditModel(IModel model)
        {
            LKASPEC objModel = (LKASPEC)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LKASPEC set Length=@Length,Height=@Height,SidePanel=@SidePanel,LightType=@LightType,Japan=@Japan where LKASPECId=@LKASPECId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@Height",objModel.Height),
                new SqlParameter("@SidePanel",objModel.SidePanel),
                new SqlParameter("@LightType",objModel.LightType),
                new SqlParameter("@Japan",objModel.Japan),

                new SqlParameter("@LKASPECId",objModel.LKASPECId)
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
            string sql = "select LKASPECId,LKASPEC.ModuleTreeId,Item,Module,Length,Height,SidePanel,LightType,Japan from LKASPEC";
            sql += " inner join ModuleTree on LKASPEC.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where LKASPECId={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LKASPECId,ModuleTreeId,Length,Height,SidePanel,LightType,Japan from LKASPEC";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LKASPEC objModel = null;
            if (objReader.Read())
            {
                objModel = new LKASPEC()
                {
                    LKASPECId = Convert.ToInt32(objReader["LKASPECId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Length"]),
                    Height = objReader["Height"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Height"]),
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
