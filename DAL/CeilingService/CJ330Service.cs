using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class CJ330Service : IModelService
    {
        public int EditModel(IModel model)
        {
            CJ330 objModel = (CJ330)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update CJ330 set Length=@Length,SidePanel=@SidePanel,SuType=@SuType,SuDis=@SuDis,");
            sqlBuilder.Append("BackCJSide=@BackCJSide,LeftDis=@LeftDis,RightDis=@RightDis,");
            sqlBuilder.Append("LeftBeamType=@LeftBeamType,LeftBeamDis=@LeftBeamDis,RightBeamType=@RightBeamType,RightBeamDis=@RightBeamDis,");
            sqlBuilder.Append("LKSide=@LKSide,GutterSide=@GutterSide,GutterWidth=@GutterWidth where CJ330Id=@CJ330Id");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@SidePanel",objModel.SidePanel),
                new SqlParameter("@SuType",objModel.SuType),
                new SqlParameter("@SuDis",objModel.SuDis),
                new SqlParameter("@BackCJSide",objModel.BackCJSide),
                new SqlParameter("@LeftDis",objModel.LeftDis),
                new SqlParameter("@RightDis",objModel.RightDis),
                new SqlParameter("@LeftBeamType",objModel.LeftBeamType),
                new SqlParameter("@LeftBeamDis",objModel.LeftBeamDis),
                new SqlParameter("@RightBeamType",objModel.RightBeamType),
                new SqlParameter("@RightBeamDis",objModel.RightBeamDis),
                new SqlParameter("@LKSide",objModel.LKSide),
                new SqlParameter("@GutterSide",objModel.GutterSide),
                new SqlParameter("@GutterWidth",objModel.GutterWidth),
                new SqlParameter("@CJ330Id",objModel.CJ330Id)
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
            string sql = "select CJ330Id,CJ330.ModuleTreeId,Item,Module,Length,SidePanel,SuType,SuDis,BackCJSide,LeftDis,RightDis," +
                         "LeftBeamType,LeftBeamDis,RightBeamType,RightBeamDis,LKSide,GutterSide,GutterWidth from CJ330";
            sql += " inner join ModuleTree on CJ330.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where CJ330Id={id}");
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select CJ330Id,ModuleTreeId,Length,SidePanel,SuType,SuDis,BackCJSide,LeftDis,RightDis," +
                "LeftBeamType,LeftBeamDis,RightBeamType,RightBeamDis,LKSide,GutterSide,GutterWidth from CJ330";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            CJ330 objModel = null;
            if (objReader.Read())
            {
                objModel = new CJ330()
                {
                    CJ330Id = Convert.ToInt32(objReader["CJ330Id"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Length"]),
                    SuDis = objReader["SuDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["SuDis"]),
                    LeftDis = objReader["LeftDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["LeftDis"]),
                    RightDis = objReader["RightDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["RightDis"]),
                    LeftBeamDis = objReader["LeftBeamDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["LeftBeamDis"]),
                    RightBeamDis = objReader["RightBeamDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["RightBeamDis"]),
                    GutterWidth = objReader["GutterWidth"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["GutterWidth"]),

                    SidePanel = objReader["SidePanel"].ToString().Length == 0 ? "" : objReader["SidePanel"].ToString(),
                    SuType = objReader["SuType"].ToString().Length == 0 ? "" : objReader["SuType"].ToString(),
                    BackCJSide = objReader["BackCJSide"].ToString().Length == 0 ? "" : objReader["BackCJSide"].ToString(),
                    LeftBeamType = objReader["LeftBeamType"].ToString().Length == 0 ? "" : objReader["LeftBeamType"].ToString(),
                    RightBeamType = objReader["RightBeamType"].ToString().Length == 0 ? "" : objReader["RightBeamType"].ToString(),
                    LKSide = objReader["LKSide"].ToString().Length == 0 ? "" : objReader["LKSide"].ToString(),
                    GutterSide = objReader["GutterSide"].ToString().Length == 0 ? "" : objReader["GutterSide"].ToString(),
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
