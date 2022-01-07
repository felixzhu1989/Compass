using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class LSDOSTService : IModelService
    {
        public int EditModel(IModel model)
        {
            LSDOST objModel = (LSDOST)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LSDOST set Length=@Length,SuNo=@SuNo,SuDis=@SuDis where LSDOSTId=@LSDOSTId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@SuNo",objModel.SuNo),
                new SqlParameter("@SuDis",objModel.SuDis),
                new SqlParameter("@LSDOSTId",objModel.LSDOSTId)
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
            string sql = "select LSDOSTId,LSDOST.ModuleTreeId,Item,Module,Length,SuNo,SuDis,Deepth,SidePanel from LSDOST";
            sql += " inner join ModuleTree on LSDOST.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where LSDOSTId={id}");
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LSDOSTId,ModuleTreeId,Length,SuNo,SuDis,Deepth,SidePanel from LSDOST";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LSDOST objModel = null;
            if (objReader.Read())
            {
                objModel = new LSDOST()
                {
                    LSDOSTId = Convert.ToInt32(objReader["LSDOSTId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Length"]),
                    SuNo = objReader["SuNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["SuNo"]),
                    SuDis = objReader["SuDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["SuDis"]),
                    Deepth = objReader["Deepth"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Deepth"]),
                    SidePanel = objReader["SidePanel"].ToString().Length == 0 ? "" : objReader["SidePanel"].ToString()
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
