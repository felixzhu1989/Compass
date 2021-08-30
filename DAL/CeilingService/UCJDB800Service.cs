using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class UCJDB800Service:IModelService
    {
        public int EditModel(IModel model)
        {
            UCJDB800 objModel = (UCJDB800)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update UCJDB800 set Length=@Length,ExRightDis=@ExRightDis,ExLength=@ExLength,ExWidth=@ExWidth,ExHeight=@ExHeight,");
            sqlBuilder.Append("FCSide=@FCSide,FCSideLeft=@FCSideLeft,FCSideRight=@FCSideRight,FCBlindNo=@FCBlindNo,");
            sqlBuilder.Append("UVType=@UVType,LightType=@LightType,LightCable=@LightCable,SSPType=@SSPType,Gutter=@Gutter,GutterWidth=@GutterWidth,ANSUL=@ANSUL,ANSide=@ANSide,ANDetectorEnd=@ANDetectorEnd,");
            sqlBuilder.Append("ANDetectorNo=@ANDetectorNo,ANDetectorDis1=@ANDetectorDis1,ANDetectorDis2=@ANDetectorDis2,ANDetectorDis3=@ANDetectorDis3,ANDetectorDis4=@ANDetectorDis4,ANDetectorDis5=@ANDetectorDis5,MARVEL=@MARVEL,Japan=@Japan where UCJDB800Id=@UCJDB800Id");
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
                new SqlParameter("@UVType",objModel.UVType),
                new SqlParameter("@LightType",objModel.LightType),
                new SqlParameter("@LightCable",objModel.LightCable),
                new SqlParameter("@SSPType",objModel.SSPType),
                new SqlParameter("@Gutter",objModel.Gutter),
                new SqlParameter("@GutterWidth",objModel.GutterWidth),
                new SqlParameter("@ANSUL",objModel.ANSUL),
                new SqlParameter("@ANSide",objModel.ANSide),
                new SqlParameter("@ANDetectorEnd",objModel.ANDetectorEnd),
                new SqlParameter("@ANDetectorNo",objModel.ANDetectorNo),
                new SqlParameter("@ANDetectorDis1",objModel.ANDetectorDis1),
                new SqlParameter("@ANDetectorDis2",objModel.ANDetectorDis2),
                new SqlParameter("@ANDetectorDis3",objModel.ANDetectorDis3),
                new SqlParameter("@ANDetectorDis4",objModel.ANDetectorDis4),
                new SqlParameter("@ANDetectorDis5",objModel.ANDetectorDis5),
                new SqlParameter("@MARVEL",objModel.MARVEL),
                new SqlParameter("@Japan",objModel.Japan),

                new SqlParameter("@UCJDB800Id",objModel.UCJDB800Id)
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
            string sql = "select UCJDB800Id,UCJDB800.ModuleTreeId,Item,Module,Length,ExRightDis,ExLength,ExWidth,ExHeight," +
                         "FCSide,FCSideLeft,FCSideRight,FCBlindNo,UVType,LightType,LightCable,SSPType,Gutter,GutterWidth," +
                         "ANSUL,ANSide,ANDetectorEnd,ANDetectorNo,ANDetectorDis1,ANDetectorDis2,ANDetectorDis3,ANDetectorDis4,ANDetectorDis5,MARVEL,Japan from UCJDB800";
            sql += " inner join ModuleTree on UCJDB800.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where UCJDB800Id={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select UCJDB800Id,ModuleTreeId,Length,ExRightDis,ExLength,ExWidth,ExHeight," +
                "FCSide,FCSideLeft,FCSideRight,FCBlindNo,UVType,LightType,LightCable,SSPType,Gutter,GutterWidth," +
                "ANSUL,ANSide,ANDetectorEnd,ANDetectorNo,ANDetectorDis1,ANDetectorDis2,ANDetectorDis3,ANDetectorDis4,ANDetectorDis5,MARVEL,Japan from UCJDB800";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            UCJDB800 objModel = null;
            if (objReader.Read())
            {
                objModel = new UCJDB800()
                {
                    UCJDB800Id = Convert.ToInt32(objReader["UCJDB800Id"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Length"]),
                    ExRightDis = objReader["ExRightDis"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ExRightDis"]),
                    ExLength = objReader["ExLength"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ExLength"]),
                    ExWidth = objReader["ExWidth"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ExWidth"]),
                    ExHeight = objReader["ExHeight"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ExHeight"]),

                    FCSide = objReader["FCSide"].ToString().Length == 0 ? "" : objReader["FCSide"].ToString(),
                    FCSideLeft = objReader["FCSideLeft"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["FCSideLeft"]),
                    FCSideRight = objReader["FCSideRight"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["FCSideRight"]),
                    FCBlindNo = objReader["FCBlindNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["FCBlindNo"]),

                    UVType = objReader["UVType"].ToString().Length == 0 ? "" : objReader["UVType"].ToString(),
                    LightType = objReader["LightType"].ToString().Length == 0 ? "" : objReader["LightType"].ToString(),
                    LightCable = objReader["LightCable"].ToString().Length == 0 ? "" : objReader["LightCable"].ToString(),
                    SSPType = objReader["SSPType"].ToString().Length == 0 ? "" : objReader["SSPType"].ToString(),
                    Gutter = objReader["Gutter"].ToString().Length == 0 ? "" : objReader["Gutter"].ToString(),
                    GutterWidth = objReader["GutterWidth"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["GutterWidth"]),

                    ANSUL = objReader["ANSUL"].ToString().Length == 0 ? "" : objReader["ANSUL"].ToString(),
                    ANSide = objReader["ANSide"].ToString().Length == 0 ? "" : objReader["ANSide"].ToString(),
                    ANDetectorEnd = objReader["ANDetectorEnd"].ToString().Length == 0 ? "" : objReader["ANDetectorEnd"].ToString(),
                    ANDetectorNo = objReader["ANDetectorNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["ANDetectorNo"]),
                    ANDetectorDis1 = objReader["ANDetectorDis1"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDetectorDis1"]),
                    ANDetectorDis2 = objReader["ANDetectorDis2"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDetectorDis2"]),
                    ANDetectorDis3 = objReader["ANDetectorDis3"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDetectorDis3"]),
                    ANDetectorDis4 = objReader["ANDetectorDis4"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDetectorDis4"]),
                    ANDetectorDis5 = objReader["ANDetectorDis5"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDetectorDis5"]),
                    MARVEL = objReader["MARVEL"].ToString().Length == 0 ? "" : objReader["MARVEL"].ToString(),
                    Japan = objReader["Japan"].ToString().Length == 0 ? "" : objReader["Japan"].ToString(),
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
