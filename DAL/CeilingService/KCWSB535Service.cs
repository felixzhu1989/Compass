using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class KCWSB535Service : IModelService
    {
        public int EditModel(IModel model)
        {
            KCWSB535 objModel = (KCWSB535)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update KCWSB535 set Length=@Length,ExRightDis=@ExRightDis,ExLength=@ExLength,ExWidth=@ExWidth,ExHeight=@ExHeight,");
            sqlBuilder.Append("FCSide=@FCSide,FCSideLeft=@FCSideLeft,FCSideRight=@FCSideRight,FCBlindNo=@FCBlindNo,");
            sqlBuilder.Append("SidePanel=@SidePanel,DPSide=@DPSide,LightType=@LightType,");
            sqlBuilder.Append("SSPType=@SSPType,Gutter=@Gutter,GutterWidth=@GutterWidth,ANSUL=@ANSUL,ANSide=@ANSide,");
            sqlBuilder.Append("MARVEL=@MARVEL,Japan=@Japan,HCLSide=@HCLSide,HCLSideLeft=@HCLSideLeft,HCLSideRight=@HCLSideRight where KCWSB535Id=@KCWSB535Id");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@ExRightDis",objModel.ExRightDis),
                new SqlParameter("@ExLength",objModel.ExLength),
                new SqlParameter("@ExWidth",objModel.ExWidth),
                new SqlParameter("@ExHeight",objModel.ExHeight),

                new SqlParameter("@FCSide",objModel.FCSide),
                new SqlParameter("@FCSideLeft",objModel.FCSideLeft),
                new SqlParameter("@FCSideRight",objModel.FCSideRight),
                new SqlParameter("@FCBlindNo",objModel.FCBlindNo),
                new SqlParameter("@SidePanel",objModel.SidePanel),
                new SqlParameter("@DPSide",objModel.DPSide),

                new SqlParameter("@LightType",objModel.LightType),
                new SqlParameter("@SSPType",objModel.SSPType),
                new SqlParameter("@Gutter",objModel.Gutter),
                new SqlParameter("@GutterWidth",objModel.GutterWidth),
                new SqlParameter("@ANSUL",objModel.ANSUL),
                new SqlParameter("@ANSide",objModel.ANSide),

                new SqlParameter("@MARVEL",objModel.MARVEL),
                new SqlParameter("@Japan",objModel.Japan),

                new SqlParameter("@HCLSide",objModel.HCLSide),
                new SqlParameter("@HCLSideLeft",objModel.HCLSideLeft),
                new SqlParameter("@HCLSideRight",objModel.HCLSideRight),

                new SqlParameter("@KCWSB535Id",objModel.KCWSB535Id)
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
            string sql = "select KCWSB535Id,KCWSB535.ModuleTreeId,Item,Module,Length,ExRightDis,ExLength,ExWidth,ExHeight," +
                         "FCSide,FCSideLeft,FCSideRight,FCBlindNo,SidePanel,DPSide,LightType,SSPType,Gutter,GutterWidth," +
                         "ANSUL,ANSide,MARVEL,Japan,HCLSide,HCLSideLeft,HCLSideRight from KCWSB535";
            sql += " inner join ModuleTree on KCWSB535.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where KCWSB535Id={id}");
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select KCWSB535Id,ModuleTreeId,Length,ExRightDis,ExLength,ExWidth,ExHeight," +
                "FCSide,FCSideLeft,FCSideRight,FCBlindNo,SidePanel,DPSide,LightType,SSPType,Gutter,GutterWidth," +
                "ANSUL,ANSide,MARVEL,Japan,HCLSide,HCLSideLeft,HCLSideRight from KCWSB535";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            KCWSB535 objModel = null;
            if (objReader.Read())
            {
                objModel = new KCWSB535()
                {
                    KCWSB535Id = Convert.ToInt32(objReader["KCWSB535Id"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Length"]),
                    ExRightDis = objReader["ExRightDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExRightDis"]),
                    ExLength = objReader["ExLength"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExLength"]),
                    ExWidth = objReader["ExWidth"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExWidth"]),
                    ExHeight = objReader["ExHeight"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExHeight"]),

                    FCSide = objReader["FCSide"].ToString().Length == 0 ? "" : objReader["FCSide"].ToString(),
                    FCSideLeft = objReader["FCSideLeft"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["FCSideLeft"]),
                    FCSideRight = objReader["FCSideRight"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["FCSideRight"]),
                    FCBlindNo = objReader["FCBlindNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["FCBlindNo"]),

                    SidePanel = objReader["SidePanel"].ToString().Length == 0 ? "" : objReader["SidePanel"].ToString(),
                    DPSide = objReader["DPSide"].ToString().Length == 0 ? "" : objReader["DPSide"].ToString(),

                    LightType = objReader["LightType"].ToString().Length == 0 ? "" : objReader["LightType"].ToString(),
                    SSPType = objReader["SSPType"].ToString().Length == 0 ? "" : objReader["SSPType"].ToString(),
                    Gutter = objReader["Gutter"].ToString().Length == 0 ? "" : objReader["Gutter"].ToString(),
                    GutterWidth = objReader["GutterWidth"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["GutterWidth"]),

                    ANSUL = objReader["ANSUL"].ToString().Length == 0 ? "" : objReader["ANSUL"].ToString(),
                    ANSide = objReader["ANSide"].ToString().Length == 0 ? "" : objReader["ANSide"].ToString(),
                    MARVEL = objReader["MARVEL"].ToString().Length == 0 ? "" : objReader["MARVEL"].ToString(),
                    Japan = objReader["Japan"].ToString().Length == 0 ? "" : objReader["Japan"].ToString(),
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
