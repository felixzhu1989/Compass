using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class KCJSB535Service : IModelService
    {
        public int EditModel(IModel model)
        {
            KCJSB535 objModel = (KCJSB535)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update KCJSB535 set Length=@Length,ExRightDis=@ExRightDis,ExLength=@ExLength,ExWidth=@ExWidth,ExHeight=@ExHeight,");
            sqlBuilder.Append("FCType=@FCType,FCSide=@FCSide,FCSideLeft=@FCSideLeft,FCSideRight=@FCSideRight,FCBlindNo=@FCBlindNo,");
            sqlBuilder.Append("LightType=@LightType,LightCable=@LightCable,SSPType=@SSPType,Gutter=@Gutter,GutterWidth=@GutterWidth,ANSUL=@ANSUL,ANSide=@ANSide,ANDetector=@ANDetector,");
            sqlBuilder.Append("MARVEL=@MARVEL,LightPanelSide=@LightPanelSide,LightPanelLeft=@LightPanelLeft,LightPanelRight=@LightPanelRight,Japan=@Japan where KCJSB535Id=@KCJSB535Id");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@ExRightDis",objModel.ExRightDis),
                new SqlParameter("@ExLength",objModel.ExLength),
                new SqlParameter("@ExWidth",objModel.ExWidth),
                new SqlParameter("@ExHeight",objModel.ExHeight),
                new SqlParameter("@FCType",objModel.FCType),
                new SqlParameter("@FCSide",objModel.FCSide),
                new SqlParameter("@FCSideLeft",objModel.FCSideLeft),
                new SqlParameter("@FCSideRight",objModel.FCSideRight),
                new SqlParameter("@FCBlindNo",objModel.FCBlindNo),
                new SqlParameter("@LightType",objModel.LightType),
                new SqlParameter("@LightCable",objModel.LightCable),
                new SqlParameter("@SSPType",objModel.SSPType),
                new SqlParameter("@Gutter",objModel.Gutter),
                new SqlParameter("@GutterWidth",objModel.GutterWidth),
                new SqlParameter("@ANSUL",objModel.ANSUL),
                new SqlParameter("@ANSide",objModel.ANSide),
                new SqlParameter("@ANDetector",objModel.ANDetector),
                new SqlParameter("@MARVEL",objModel.MARVEL),
                new SqlParameter("@Japan",objModel.Japan),
                new SqlParameter("@LightPanelSide",objModel.LightPanelSide),
                new SqlParameter("@LightPanelLeft",objModel.LightPanelLeft),
                new SqlParameter("@LightPanelRight",objModel.LightPanelRight),

                new SqlParameter("@KCJSB535Id",objModel.KCJSB535Id)
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
            string sql = "select KCJSB535Id,KCJSB535.ModuleTreeId,Item,Module,Length,ExRightDis,ExLength,ExWidth,ExHeight," +
                         "FCType,FCSide,FCSideLeft,FCSideRight,FCBlindNo,LightType,LightCable,SSPType,Gutter,GutterWidth," +
                         "ANSUL,ANSide,ANDetector,MARVEL,LightPanelSide,LightPanelLeft,LightPanelRight,Japan from KCJSB535";
            sql += " inner join ModuleTree on KCJSB535.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where KCJSB535Id={id}");
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select KCJSB535Id,ModuleTreeId,Length,ExRightDis,ExLength,ExWidth,ExHeight," +
                "FCType,FCSide,FCSideLeft,FCSideRight,FCBlindNo,LightType,LightCable,SSPType,Gutter,GutterWidth," +
                "ANSUL,ANSide,ANDetector,MARVEL,LightPanelSide,LightPanelLeft,LightPanelRight,Japan from KCJSB535";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            KCJSB535 objModel = null;
            if (objReader.Read())
            {
                objModel = new KCJSB535()
                {
                    KCJSB535Id = Convert.ToInt32(objReader["KCJSB535Id"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Length"]),
                    ExRightDis = objReader["ExRightDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExRightDis"]),
                    ExLength = objReader["ExLength"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExLength"]),
                    ExWidth = objReader["ExWidth"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExWidth"]),
                    ExHeight = objReader["ExHeight"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExHeight"]),

                    FCType = objReader["FCType"].ToString().Length == 0 ? "" : objReader["FCType"].ToString(),
                    FCSide = objReader["FCSide"].ToString().Length == 0 ? "" : objReader["FCSide"].ToString(),
                    FCSideLeft = objReader["FCSideLeft"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["FCSideLeft"]),
                    FCSideRight = objReader["FCSideRight"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["FCSideRight"]),
                    FCBlindNo = objReader["FCBlindNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["FCBlindNo"]),

                    LightType = objReader["LightType"].ToString().Length == 0 ? "" : objReader["LightType"].ToString(),
                    LightCable = objReader["LightCable"].ToString().Length == 0 ? "" : objReader["LightCable"].ToString(),
                    SSPType = objReader["SSPType"].ToString().Length == 0 ? "" : objReader["SSPType"].ToString(),
                    Gutter = objReader["Gutter"].ToString().Length == 0 ? "" : objReader["Gutter"].ToString(),
                    GutterWidth = objReader["GutterWidth"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["GutterWidth"]),

                    ANSUL = objReader["ANSUL"].ToString().Length == 0 ? "" : objReader["ANSUL"].ToString(),
                    ANSide = objReader["ANSide"].ToString().Length == 0 ? "" : objReader["ANSide"].ToString(),
                    ANDetector = objReader["ANDetector"].ToString().Length == 0 ? "" : objReader["ANDetector"].ToString(),
                    MARVEL = objReader["MARVEL"].ToString().Length == 0 ? "" : objReader["MARVEL"].ToString(),
                    LightPanelSide = objReader["LightPanelSide"].ToString().Length == 0 ? "" : objReader["LightPanelSide"].ToString(),
                    LightPanelLeft = objReader["LightPanelLeft"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["LightPanelLeft"]),
                    LightPanelRight = objReader["LightPanelRight"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["LightPanelRight"]),
                  
                    Japan = objReader["Japan"].ToString().Length == 0 ? "" : objReader["Japan"].ToString(),
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
