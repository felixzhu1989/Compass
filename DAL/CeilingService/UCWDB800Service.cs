using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class UCWDB800Service : IModelService
    {
        public int EditModel(IModel model)
        {
            UCWDB800 objModel = (UCWDB800)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update UCWDB800 set Length=@Length,ExRightDis=@ExRightDis,ExLength=@ExLength,ExWidth=@ExWidth,ExHeight=@ExHeight,");
            sqlBuilder.Append("FCSide=@FCSide,FCSideLeft=@FCSideLeft,FCSideRight=@FCSideRight,FCBlindNo=@FCBlindNo,");
            sqlBuilder.Append("SidePanel=@SidePanel,DPSide=@DPSide,LightType=@LightType,UVType=@UVType,SensorNo=@SensorNo,SensorDis1=@SensorDis1,SensorDis2=@SensorDis2,");
            sqlBuilder.Append("SSPType=@SSPType,Gutter=@Gutter,GutterWidth=@GutterWidth,ANSUL=@ANSUL,ANSide=@ANSide,");
            sqlBuilder.Append("MARVEL=@MARVEL,Japan=@Japan where UCWDB800Id=@UCWDB800Id");
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
                new SqlParameter("@LightType",objModel.LightType),

                new SqlParameter("@UVType",objModel.UVType),
                new SqlParameter("@SensorNo",objModel.SensorNo),
                new SqlParameter("@SensorDis1",objModel.SensorDis1),
                new SqlParameter("@SensorDis2",objModel.SensorDis2),

                new SqlParameter("@DPSide",objModel.DPSide),
                new SqlParameter("@SSPType",objModel.SSPType),
                new SqlParameter("@Gutter",objModel.Gutter),
                new SqlParameter("@GutterWidth",objModel.GutterWidth),
                new SqlParameter("@ANSUL",objModel.ANSUL),
                new SqlParameter("@ANSide",objModel.ANSide),

                new SqlParameter("@MARVEL",objModel.MARVEL),
                new SqlParameter("@Japan",objModel.Japan),

                new SqlParameter("@UCWDB800Id",objModel.UCWDB800Id)
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
            string sql = "select UCWDB800Id,UCWDB800.ModuleTreeId,Item,Module,Length,ExRightDis,ExLength,ExWidth,ExHeight," +
                         "FCSide,FCSideLeft,FCSideRight,FCBlindNo,SidePanel,DPSide,LightType,UVType,SensorNo,SensorDis1,SensorDis2," +
                         "SSPType,Gutter,GutterWidth,ANSUL,ANSide,MARVEL,Japan from UCWDB800";
            sql += " inner join ModuleTree on UCWDB800.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where UCWDB800Id={id}");
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select UCWDB800Id,ModuleTreeId,Length,ExRightDis,ExLength,ExWidth,ExHeight," +
                "FCSide,FCSideLeft,FCSideRight,FCBlindNo,SidePanel,DPSide,LightType,UVType,SensorNo,SensorDis1,SensorDis2," +
                "SSPType,Gutter,GutterWidth,ANSUL,ANSide,MARVEL,Japan from UCWDB800";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            UCWDB800 objModel = null;
            if (objReader.Read())
            {
                objModel = new UCWDB800()
                {
                    UCWDB800Id = Convert.ToInt32(objReader["UCWDB800Id"]),
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

                    UVType = objReader["UVType"].ToString().Length == 0 ? "" : objReader["UVType"].ToString(),
                    SensorNo = objReader["SensorNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["SensorNo"]),
                    SensorDis1 = objReader["SensorDis1"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["SensorDis1"]),
                    SensorDis2 = objReader["SensorDis2"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["SensorDis2"]),

                    SSPType = objReader["SSPType"].ToString().Length == 0 ? "" : objReader["SSPType"].ToString(),
                    Gutter = objReader["Gutter"].ToString().Length == 0 ? "" : objReader["Gutter"].ToString(),
                    GutterWidth = objReader["GutterWidth"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["GutterWidth"]),

                    ANSUL = objReader["ANSUL"].ToString().Length == 0 ? "" : objReader["ANSUL"].ToString(),
                    ANSide = objReader["ANSide"].ToString().Length == 0 ? "" : objReader["ANSide"].ToString(),
                    MARVEL = objReader["MARVEL"].ToString().Length == 0 ? "" : objReader["MARVEL"].ToString(),
                    Japan = objReader["Japan"].ToString().Length == 0 ? "" : objReader["Japan"].ToString(),
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
