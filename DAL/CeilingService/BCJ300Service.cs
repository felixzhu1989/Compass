using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class BCJ300Service:IModelService
    {
        public int EditModel(IModel model)
        {
            BCJ300 objModel = (BCJ300)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update BCJ300 set Length=@Length,SidePanel=@SidePanel,SuType=@SuType,SuDis=@SuDis where BCJ300Id=@BCJ300Id");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@SidePanel",objModel.SidePanel),
                new SqlParameter("@SuType",objModel.SuType),
                new SqlParameter("@SuDis",objModel.SuDis),
                new SqlParameter("@BCJ300Id",objModel.BCJ300Id)
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
            string sql = "select BCJ300Id,BCJ300.ModuleTreeId,Item,Module,Length,SidePanel,SuType,SuDis from BCJ300";
            sql += " inner join ModuleTree on BCJ300.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where BCJ300Id={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select BCJ300Id,ModuleTreeId,Length,SidePanel,SuType,SuDis from BCJ300";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            BCJ300 objModel = null;
            if (objReader.Read())
            {
                objModel = new BCJ300()
                {
                    BCJ300Id = Convert.ToInt32(objReader["BCJ300Id"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Length"]),
                    SuDis = objReader["SuDis"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["SuDis"]),
                    
                    SidePanel = objReader["SidePanel"].ToString().Length == 0 ? "" : objReader["SidePanel"].ToString(),
                    SuType = objReader["SuType"].ToString().Length == 0 ? "" : objReader["SuType"].ToString(),
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
