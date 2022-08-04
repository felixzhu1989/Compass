using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class UWF555Service : IModelService
    {
        /// <summary>
        /// 根据项目Id查询UWF555集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DataSet GetModelByDataSet(string projectId)
        {
            string sql = "select UWF555Id,UWF555.ModuleTreeId,Item,Module,Length,Deepth,ExRightDis,ExNo,EXDis,ExLength,ExWidth,ExHeight,SuNo,SuDis,SidePanel,Outlet,Inlet,LEDlogo,Bluetooth,BackToBack,WaterCollection,LEDSpotNo,LEDSpotDis,LightType,UVType,ANSUL,ANSide,ANDetectorEnd,ANYDis,ANDropNo,ANDropDis1,ANDropDis2,ANDropDis3,ANDropDis4,ANDropDis5,ANDetectorNo,ANDetectorDis1,ANDetectorDis2,ANDetectorDis3,ANDetectorDis4,ANDetectorDis5,MARVEL from UWF555";
            sql += " inner join ModuleTree on UWF555.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }
        /// <summary>
        /// 根据UWF555ID返回UWF555
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where UWF555Id={id}");
        }
        /// <summary>
        /// 根据模型树ID返回UWF555
        /// </summary>
        /// <param name="moduleTreeId"></param>
        /// <returns></returns>
        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }
        /// <summary>
        /// 根据条件查找UWF555
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select UWF555Id,ModuleTreeId,Length,Deepth,ExRightDis,ExNo,EXDis,ExLength,ExWidth,ExHeight,SuNo,SuDis,SidePanel," +
                "Outlet,Inlet,LEDlogo,Bluetooth,BackToBack,WaterCollection,LEDSpotNo,LEDSpotDis,LightType,UVType," +
                "ANSUL,ANSide,ANDetectorEnd,ANYDis,ANDropNo,ANDropDis1,ANDropDis2,ANDropDis3,ANDropDis4,ANDropDis5," +
                "ANDetectorNo,ANDetectorDis1,ANDetectorDis2,ANDetectorDis3,ANDetectorDis4,ANDetectorDis5," +
                "MARVEL from UWF555";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            UWF555 objModel = null;
            if (objReader.Read())
            {
                objModel = new UWF555()
                {
                    UWF555Id = Convert.ToInt32(objReader["UWF555Id"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Length"]),
                    Deepth = objReader["Deepth"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Deepth"]),
                    ExRightDis = objReader["ExRightDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExRightDis"]),
                    ExNo = objReader["ExNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["ExNo"]),
                    ExDis = objReader["ExDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExDis"]),
                    ExLength = objReader["ExLength"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExLength"]),
                    ExWidth = objReader["ExWidth"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExWidth"]),
                    ExHeight = objReader["ExHeight"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExHeight"]),
                    SuNo = objReader["SuNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["SuNo"]),
                    SuDis = objReader["SuDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["SuDis"]),

                    SidePanel = objReader["SidePanel"].ToString().Length == 0 ? "" : objReader["SidePanel"].ToString(),
                    Outlet = objReader["Outlet"].ToString().Length == 0 ? "" : objReader["Outlet"].ToString(),
                    Inlet = objReader["Inlet"].ToString().Length == 0 ? "" : objReader["Inlet"].ToString(),
                    LEDlogo = objReader["LEDlogo"].ToString().Length == 0 ? "" : objReader["LEDlogo"].ToString(),
                    Bluetooth = objReader["Bluetooth"].ToString().Length == 0 ? "" : objReader["Bluetooth"].ToString(),
                    BackToBack = objReader["BackToBack"].ToString().Length == 0 ? "" : objReader["BackToBack"].ToString(),
                    WaterCollection = objReader["WaterCollection"].ToString().Length == 0 ? "" : objReader["WaterCollection"].ToString(),

                    LEDSpotNo = objReader["LEDSpotNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["LEDSpotNo"]),
                    LEDSpotDis = objReader["LEDSpotDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["LEDSpotDis"]),

                    LightType = objReader["LightType"].ToString().Length == 0 ? "" : objReader["LightType"].ToString(),
                    UVType = objReader["UVType"].ToString().Length == 0 ? "" : objReader["UVType"].ToString(),
                    ANSUL = objReader["ANSUL"].ToString().Length == 0 ? "" : objReader["ANSUL"].ToString(),
                    ANSide = objReader["ANSide"].ToString().Length == 0 ? "" : objReader["ANSide"].ToString(),
                    ANDetectorEnd = objReader["ANDetectorEnd"].ToString().Length == 0 ? "" : objReader["ANDetectorEnd"].ToString(),

                    ANYDis = objReader["ANYDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANYDis"]),
                    ANDropNo = objReader["ANDropNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["ANDropNo"]),
                    ANDropDis1 = objReader["ANDropDis1"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDropDis1"]),
                    ANDropDis2 = objReader["ANDropDis2"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDropDis2"]),
                    ANDropDis3 = objReader["ANDropDis3"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDropDis3"]),
                    ANDropDis4 = objReader["ANDropDis4"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDropDis4"]),
                    ANDropDis5 = objReader["ANDropDis5"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDropDis5"]),
                    ANDetectorNo = objReader["ANDetectorNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["ANDetectorNo"]),
                    ANDetectorDis1 = objReader["ANDetectorDis1"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDetectorDis1"]),
                    ANDetectorDis2 = objReader["ANDetectorDis2"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDetectorDis2"]),
                    ANDetectorDis3 = objReader["ANDetectorDis3"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDetectorDis3"]),
                    ANDetectorDis4 = objReader["ANDetectorDis4"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDetectorDis4"]),
                    ANDetectorDis5 = objReader["ANDetectorDis5"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDetectorDis5"]),

                    MARVEL = objReader["MARVEL"].ToString().Length == 0 ? "" : objReader["MARVEL"].ToString(),

                   
                };
            }
            objReader.Close();
            return objModel;
        }
        /// <summary>
        /// 修改UWF555的制图参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditModel(IModel model)
        {
            UWF555 objModel = (UWF555) model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update UWF555 set Length=@Length,Deepth=@Deepth,ExRightDis=@ExRightDis,ExNo=@ExNo,ExDis=@ExDis,ExLength=@ExLength,ExWidth=@ExWidth,ExHeight=@ExHeight,SuNo=@SuNo,SuDis=@SuDis,SidePanel=@SidePanel,");
            sqlBuilder.Append("Outlet=@Outlet,Inlet=@Inlet,LEDlogo=@LEDlogo,Bluetooth=@Bluetooth,BackToBack=@BackToBack,WaterCollection=@WaterCollection,LEDSpotNo=@LEDSpotNo,LEDSpotDis=@LEDSpotDis,LightType=@LightType,UVType=@UVType,");
            sqlBuilder.Append("ANSUL=@ANSUL,ANSide=@ANSide,ANDetectorEnd=@ANDetectorEnd,ANYDis=@ANYDis,ANDropNo=@ANDropNo,ANDropDis1=@ANDropDis1,ANDropDis2=@ANDropDis2,ANDropDis3=@ANDropDis3,ANDropDis4=@ANDropDis4,ANDropDis5=@ANDropDis5,");
            sqlBuilder.Append("ANDetectorNo=@ANDetectorNo,ANDetectorDis1=@ANDetectorDis1,ANDetectorDis2=@ANDetectorDis2,ANDetectorDis3=@ANDetectorDis3,ANDetectorDis4=@ANDetectorDis4,ANDetectorDis5=@ANDetectorDis5,");
            sqlBuilder.Append("MARVEL=@MARVEL where UWF555Id=@UWF555Id");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@Deepth",objModel.Deepth),
                new SqlParameter("@ExRightDis",objModel.ExRightDis),
                new SqlParameter("@ExNo",objModel.ExNo),
                new SqlParameter("@ExDis",objModel.ExDis),
                new SqlParameter("@ExLength",objModel.ExLength),
                new SqlParameter("@ExWidth",objModel.ExWidth),
                new SqlParameter("@ExHeight",objModel.ExHeight),
                new SqlParameter("@SuNo",objModel.SuNo),
                new SqlParameter("@SuDis",objModel.SuDis),
                new SqlParameter("@SidePanel",objModel.SidePanel),
                new SqlParameter("@Outlet",objModel.Outlet),
                new SqlParameter("@Inlet",objModel.Inlet),
                new SqlParameter("@LEDlogo",objModel.LEDlogo),
                new SqlParameter("@Bluetooth",objModel.Bluetooth),
                new SqlParameter("@BackToBack",objModel.BackToBack),
                new SqlParameter("@WaterCollection",objModel.WaterCollection),
                new SqlParameter("@LEDSpotNo",objModel.LEDSpotNo),
                new SqlParameter("@LEDSpotDis",objModel.LEDSpotDis),
                new SqlParameter("@LightType",objModel.LightType),
                new SqlParameter("@UVType",objModel.UVType),
                new SqlParameter("@ANSUL",objModel.ANSUL),
                new SqlParameter("@ANSide",objModel.ANSide),
                new SqlParameter("@ANDetectorEnd",objModel.ANDetectorEnd),
                new SqlParameter("@ANYDis",objModel.ANYDis),
                new SqlParameter("@ANDropNo",objModel.ANDropNo),
                new SqlParameter("@ANDropDis1",objModel.ANDropDis1),
                new SqlParameter("@ANDropDis2",objModel.ANDropDis2),
                new SqlParameter("@ANDropDis3",objModel.ANDropDis3),
                new SqlParameter("@ANDropDis4",objModel.ANDropDis4),
                new SqlParameter("@ANDropDis5",objModel.ANDropDis5),
                new SqlParameter("@ANDetectorNo",objModel.ANDetectorNo),
                new SqlParameter("@ANDetectorDis1",objModel.ANDetectorDis1),
                new SqlParameter("@ANDetectorDis2",objModel.ANDetectorDis2),
                new SqlParameter("@ANDetectorDis3",objModel.ANDetectorDis3),
                new SqlParameter("@ANDetectorDis4",objModel.ANDetectorDis4),
                new SqlParameter("@ANDetectorDis5",objModel.ANDetectorDis5),
                new SqlParameter("@MARVEL",objModel.MARVEL),

                new SqlParameter("@UWF555Id",objModel.UWF555Id)
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
    }
}
