using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class HMFService : IModelService
    {
        /// <summary>
        /// 根据项目Id查询HMF集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DataSet GetModelByDataSet(string projectId)
        {
            string sql = "select HMFId,HMF.ModuleTreeId,Item,Module,Length,Width,Height,InletDia,OutletDia,OutletHeight,HangPosition,PowerPlug,PowerPlugDis,NetPlug,PlugPosition,Heater,TemperatureSwitch,NamePlate,WindPressure from HMF";
            sql += " inner join ModuleTreeMarine on HMF.ModuleTreeId=ModuleTreeMarine.ModuleTreeId";
            sql += " inner join DrawingPlanMarine on ModuleTreeMarine.DrawingPlanId=DrawingPlanMarine.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }
        /// <summary>
        /// 根据HMFID返回HMF
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where HMFId={id}");
        }
        /// <summary>
        /// 根据模型树ID返回HMF
        /// </summary>
        /// <param name="moduleTreeId"></param>
        /// <returns></returns>
        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }
        /// <summary>
        /// 根据条件查找HMF
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select HMFId,ModuleTreeId,Length,Width,Height,InletDia,OutletDia,OutletHeight,HangPosition,PowerPlug,PowerPlugDis," +
                "NetPlug,PlugPosition,Heater,TemperatureSwitch,NamePlate,WindPressure from HMF";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            HMF objModel = null;
            if (objReader.Read())
            {
                objModel = new HMF()
                {
                    HMFId = Convert.ToInt32(objReader["HMFId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"] == DBNull.Value ? 0 : Convert.ToDouble(objReader["Length"]),
                    Width = objReader["Width"] == DBNull.Value ? 0 : Convert.ToDouble(objReader["Width"]),
                    Height = objReader["Height"] == DBNull.Value ? 0 : Convert.ToDouble(objReader["Height"]),
                    InletDia = objReader["InletDia"] == DBNull.Value ? 0 : Convert.ToDouble(objReader["InletDia"]),

                    OutletDia = objReader["OutletDia"] == DBNull.Value ? 0 : Convert.ToDouble(objReader["OutletDia"]),
                    OutletHeight = objReader["OutletHeight"] == DBNull.Value ? 0 : Convert.ToDouble(objReader["OutletHeight"]),


                    HangPosition = objReader["HangPosition"] == DBNull.Value ? "" : objReader["HangPosition"].ToString(),
                    PowerPlug = objReader["PowerPlug"] == DBNull.Value ? "" : objReader["PowerPlug"].ToString(),

                    PowerPlugDis = objReader["PowerPlugDis"] == DBNull.Value ? 0 : Convert.ToDouble(objReader["PowerPlugDis"]),
                    NetPlug = objReader["NetPlug"] == DBNull.Value ? "" : objReader["NetPlug"].ToString(),
                    PlugPosition = objReader["PlugPosition"] == DBNull.Value ? "" : objReader["PlugPosition"].ToString(),
                    Heater = objReader["Heater"] == DBNull.Value ? "" : objReader["Heater"].ToString(),
                    TemperatureSwitch = objReader["TemperatureSwitch"] == DBNull.Value ? "" : objReader["TemperatureSwitch"].ToString(),
                    NamePlate = objReader["NamePlate"] == DBNull.Value ? "" : objReader["NamePlate"].ToString(),
                    WindPressure = objReader["WindPressure"] == DBNull.Value ? "" : objReader["WindPressure"].ToString(),
                };
            }
            objReader.Close();
            return objModel;
        }
        /// <summary>
        /// 修改HMF的制图参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditModel(IModel model)
        {
            HMF objModel = (HMF)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update HMF set Length=@Length,Width=@Width,Height=@Height,InletDia=@InletDia,OutletDia=@OutletDia,OutletHeight=@OutletHeight,HangPosition=@HangPosition,PowerPlug=@PowerPlug,PowerPlugDis=@PowerPlugDis,");

            sqlBuilder.Append("NetPlug=@NetPlug,PlugPosition=@PlugPosition,Heater=@Heater,TemperatureSwitch=@TemperatureSwitch,NamePlate=@NamePlate,WindPressure=@WindPressure where HMFId=@HMFId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@Width",objModel.Width),
                new SqlParameter("@Height",objModel.Height),
                new SqlParameter("@InletDia",objModel.InletDia),
                new SqlParameter("@OutletDia",objModel.OutletDia),
                new SqlParameter("@OutletHeight",objModel.OutletHeight),
                new SqlParameter("@HangPosition",objModel.HangPosition),
                new SqlParameter("@PowerPlug",objModel.PowerPlug),
                new SqlParameter("@PowerPlugDis",objModel.PowerPlugDis),
                new SqlParameter("@NetPlug",objModel.NetPlug),
                new SqlParameter("@PlugPosition",objModel.PlugPosition),
                new SqlParameter("@Heater",objModel.Heater),
                new SqlParameter("@TemperatureSwitch",objModel.TemperatureSwitch),
                new SqlParameter("@NamePlate",objModel.NamePlate),
                new SqlParameter("@WindPressure",objModel.WindPressure),

                new SqlParameter("@HMFId",objModel.HMFId)
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

   
