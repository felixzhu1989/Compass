using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class NOCJ340Service:IModelService
    {
        public int EditModel(IModel model)
        {
            NOCJ340 objModel = (NOCJ340)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update NOCJ340 set Length=@Length,Width=@Width,SidePanel=@SidePanel,");
            sqlBuilder.Append("BackCJSide=@BackCJSide,DPSide=@DPSide,LeftDis=@LeftDis,RightDis=@RightDis,");
            sqlBuilder.Append("LeftBeamType=@LeftBeamType,LeftBeamDis=@LeftBeamDis,RightBeamType=@RightBeamType,RightBeamDis=@RightBeamDis,");
            sqlBuilder.Append("LKSide=@LKSide,GutterSide=@GutterSide,GutterWidth=@GutterWidth where NOCJ340Id=@NOCJ340Id");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@Width",objModel.Width),
                new SqlParameter("@SidePanel",objModel.SidePanel),
                new SqlParameter("@BackCJSide",objModel.BackCJSide),
                new SqlParameter("@DPSide",objModel.DPSide),
                new SqlParameter("@LeftDis",objModel.LeftDis),
                new SqlParameter("@RightDis",objModel.RightDis),
                new SqlParameter("@LeftBeamType",objModel.LeftBeamType),
                new SqlParameter("@LeftBeamDis",objModel.LeftBeamDis),
                new SqlParameter("@RightBeamType",objModel.RightBeamType),
                new SqlParameter("@RightBeamDis",objModel.RightBeamDis),
                new SqlParameter("@LKSide",objModel.LKSide),
                new SqlParameter("@GutterSide",objModel.GutterSide),
                new SqlParameter("@GutterWidth",objModel.GutterWidth),
                new SqlParameter("@NOCJ340Id",objModel.NOCJ340Id)
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
            string sql = "select NOCJ340Id,NOCJ340.ModuleTreeId,Item,Module,Length,Width,SidePanel,BackCJSide,DPSide,LeftDis,RightDis," +
                         "LeftBeamType,LeftBeamDis,RightBeamType,RightBeamDis,LKSide,GutterSide,GutterWidth from NOCJ340";
            sql += " inner join ModuleTree on NOCJ340.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where NOCJ340Id={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select NOCJ340Id,ModuleTreeId,Length,Width,SidePanel,BackCJSide,DPSide,LeftDis,RightDis," +
                "LeftBeamType,LeftBeamDis,RightBeamType,RightBeamDis,LKSide,GutterSide,GutterWidth from NOCJ340";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            NOCJ340 objModel = null;
            if (objReader.Read())
            {
                objModel = new NOCJ340()
                {
                    NOCJ340Id = Convert.ToInt32(objReader["NOCJ340Id"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Length"]),
                    Width = objReader["Width"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Width"]),
                    LeftDis = objReader["LeftDis"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["LeftDis"]),
                    RightDis = objReader["RightDis"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["RightDis"]),
                    LeftBeamDis = objReader["LeftBeamDis"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["LeftBeamDis"]),
                    RightBeamDis = objReader["RightBeamDis"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["RightBeamDis"]),
                    GutterWidth = objReader["GutterWidth"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["GutterWidth"]),

                    SidePanel = objReader["SidePanel"].ToString().Length == 0 ? "" : objReader["SidePanel"].ToString(),
                    BackCJSide = objReader["BackCJSide"].ToString().Length == 0 ? "" : objReader["BackCJSide"].ToString(),
                    DPSide = objReader["DPSide"].ToString().Length == 0 ? "" : objReader["DPSide"].ToString(),
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
