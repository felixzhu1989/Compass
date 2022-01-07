﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class KVIR555Service : IModelService
    {

        /// <summary>
        /// 根据项目Id查询KVIR555集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DataSet GetModelByDataSet(string projectId)
        {
            string sql = "select KVIR555Id,KVIR555.ModuleTreeId,Item,Module,ExBeamLength,Length,Deepth,ExRightDis,ExNo,EXDis,ExLength,ExWidth,ExHeight,SidePanel,Outlet,LEDSpotNo,LEDSpotDis,LightType,ANSUL,ANSide,ANDetector,ANYDis,ANDropNo,ANDropDis1,ANDropDis2,ANDropDis3,ANDropDis4,ANDropDis5,MARVEL,IRNo,IRDis1,IRDis2,IRDis3 from KVIR555";
            sql += " inner join ModuleTree on KVIR555.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }
        /// <summary>
        /// 根据KVIR555ID返回KVIR555
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where KVIR555Id={id}");
        }
        /// <summary>
        /// 根据模型树ID返回KVIR555
        /// </summary>
        /// <param name="moduleTreeId"></param>
        /// <returns></returns>
        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }
        /// <summary>
        /// 根据条件查找KVIR555
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select KVIR555Id,ModuleTreeId,ExBeamLength,Length,Deepth,ExRightDis,ExNo,EXDis,ExLength,ExWidth,ExHeight,SidePanel," +
                "Outlet,LEDSpotNo,LEDSpotDis,LightType," +
                "ANSUL,ANSide,ANDetector,ANYDis,ANDropNo,ANDropDis1,ANDropDis2,ANDropDis3,ANDropDis4,ANDropDis5," +
                "MARVEL,IRNo,IRDis1,IRDis2,IRDis3 from KVIR555";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            KVIR555 objModel = null;
            if (objReader.Read())
            {
                objModel = new KVIR555()
                {
                    KVIR555Id = Convert.ToInt32(objReader["KVIR555Id"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    ExBeamLength = objReader["ExBeamLength"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExBeamLength"]),
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Length"]),
                    Deepth = objReader["Deepth"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Deepth"]),
                    ExRightDis = objReader["ExRightDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExRightDis"]),
                    ExNo = objReader["ExNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["ExNo"]),
                    ExDis = objReader["ExDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExDis"]),
                    ExLength = objReader["ExLength"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExLength"]),
                    ExWidth = objReader["ExWidth"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExWidth"]),
                    ExHeight = objReader["ExHeight"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ExHeight"]),

                    SidePanel = objReader["SidePanel"].ToString().Length == 0 ? "" : objReader["SidePanel"].ToString(),
                    Outlet = objReader["Outlet"].ToString().Length == 0 ? "" : objReader["Outlet"].ToString(),

                    LEDSpotNo = objReader["LEDSpotNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["LEDSpotNo"]),
                    LEDSpotDis = objReader["LEDSpotDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["LEDSpotDis"]),

                    LightType = objReader["LightType"].ToString().Length == 0 ? "" : objReader["LightType"].ToString(),
                    ANSUL = objReader["ANSUL"].ToString().Length == 0 ? "" : objReader["ANSUL"].ToString(),
                    ANSide = objReader["ANSide"].ToString().Length == 0 ? "" : objReader["ANSide"].ToString(),
                    ANDetector = objReader["ANDetector"].ToString().Length == 0 ? "" : objReader["ANDetector"].ToString(),

                    ANYDis = objReader["ANYDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANYDis"]),
                    ANDropNo = objReader["ANDropNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["ANDropNo"]),
                    ANDropDis1 = objReader["ANDropDis1"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDropDis1"]),
                    ANDropDis2 = objReader["ANDropDis2"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDropDis2"]),
                    ANDropDis3 = objReader["ANDropDis3"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDropDis3"]),
                    ANDropDis4 = objReader["ANDropDis4"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDropDis4"]),
                    ANDropDis5 = objReader["ANDropDis5"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["ANDropDis5"]),

                    MARVEL = objReader["MARVEL"].ToString().Length == 0 ? "" : objReader["MARVEL"].ToString(),

                    IRNo = objReader["IRNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["IRNo"]),
                    IRDis1 = objReader["IRDis1"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["IRDis1"]),
                    IRDis2 = objReader["IRDis2"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["IRDis2"]),
                    IRDis3 = objReader["IRDis3"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["IRDis3"])
                };
            }
            objReader.Close();
            return objModel;
        }
        /// <summary>
        /// 修改KVIR555的制图参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditModel(IModel model)
        {
            KVIR555 objModel = (KVIR555)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update KVIR555 set ExBeamLength=@ExBeamLength,Length=@Length,Deepth=@Deepth,ExRightDis=@ExRightDis,ExNo=@ExNo,ExDis=@ExDis,ExLength=@ExLength,ExWidth=@ExWidth,ExHeight=@ExHeight,");
            sqlBuilder.Append("Outlet=@Outlet,LEDSpotNo=@LEDSpotNo,LEDSpotDis=@LEDSpotDis,LightType=@LightType,");
            sqlBuilder.Append("ANSUL=@ANSUL,ANSide=@ANSide,ANDetector=@ANDetector,ANYDis=@ANYDis,ANDropNo=@ANDropNo,ANDropDis1=@ANDropDis1,ANDropDis2=@ANDropDis2,ANDropDis3=@ANDropDis3,ANDropDis4=@ANDropDis4,ANDropDis5=@ANDropDis5,");
            sqlBuilder.Append("MARVEL=@MARVEL,IRNo=@IRNo,IRDis1=@IRDis1,IRDis2=@IRDis2,IRDis3=@IRDis3 where KVIR555Id=@KVIR555Id");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ExBeamLength",objModel.ExBeamLength),
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@Deepth",objModel.Deepth),
                new SqlParameter("@ExRightDis",objModel.ExRightDis),
                new SqlParameter("@ExNo",objModel.ExNo),
                new SqlParameter("@ExDis",objModel.ExDis),
                new SqlParameter("@ExLength",objModel.ExLength),
                new SqlParameter("@ExWidth",objModel.ExWidth),
                new SqlParameter("@ExHeight",objModel.ExHeight),
                new SqlParameter("@Outlet",objModel.Outlet),

                new SqlParameter("@LEDSpotNo",objModel.LEDSpotNo),
                new SqlParameter("@LEDSpotDis",objModel.LEDSpotDis),
                new SqlParameter("@LightType",objModel.LightType),
                new SqlParameter("@ANSUL",objModel.ANSUL),
                new SqlParameter("@ANSide",objModel.ANSide),
                new SqlParameter("@ANDetector",objModel.ANDetector),
                new SqlParameter("@ANYDis",objModel.ANYDis),
                new SqlParameter("@ANDropNo",objModel.ANDropNo),
                new SqlParameter("@ANDropDis1",objModel.ANDropDis1),
                new SqlParameter("@ANDropDis2",objModel.ANDropDis2),
                new SqlParameter("@ANDropDis3",objModel.ANDropDis3),
                new SqlParameter("@ANDropDis4",objModel.ANDropDis4),
                new SqlParameter("@ANDropDis5",objModel.ANDropDis5),
                new SqlParameter("@MARVEL",objModel.MARVEL),
                new SqlParameter("@IRNo",objModel.IRNo),
                new SqlParameter("@IRDis1",objModel.IRDis1),
                new SqlParameter("@IRDis2",objModel.IRDis2),
                new SqlParameter("@IRDis3",objModel.IRDis3),
                new SqlParameter("@KVIR555Id",objModel.KVIR555Id)
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
