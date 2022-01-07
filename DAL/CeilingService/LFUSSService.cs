﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class LFUSSService : IModelService
    {
        public int EditModel(IModel model)
        {
            LFUSS objModel = (LFUSS)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update LFUSS set Length=@Length,Width=@Width,SuNo=@SuNo,SuDia=@SuDia,SuDis=@SuDis,");
            sqlBuilder.Append("SidePanel=@SidePanel,Japan=@Japan where LFUSSId=@LFUSSId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@Width",objModel.Width),
                new SqlParameter("@SuNo",objModel.SuNo),
                new SqlParameter("@SuDia",objModel.SuDia),
                new SqlParameter("@SuDis",objModel.SuDis),
                new SqlParameter("@SidePanel",objModel.SidePanel),
                new SqlParameter("@Japan",objModel.Japan),
                new SqlParameter("@LFUSSId",objModel.LFUSSId)
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
            string sql = "select LFUSSId,LFUSS.ModuleTreeId,Item,Module,Length,Width," +
                         "SuNo,SuDia,SuDis,SidePanel,Japan from LFUSS";
            sql += " inner join ModuleTree on LFUSS.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where LFUSSId={id}");
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select LFUSSId,ModuleTreeId,Length,Width," +
                "SuNo,SuDia,SuDis,SidePanel,Japan from LFUSS";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            LFUSS objModel = null;
            if (objReader.Read())
            {
                objModel = new LFUSS()
                {
                    LFUSSId = Convert.ToInt32(objReader["LFUSSId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Length"]),
                    Width = objReader["Width"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Width"]),
                    SuNo = objReader["SuNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["SuNo"]),
                    SuDia = objReader["SuDia"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["SuDia"]),
                    SuDis = objReader["SuDis"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["SuDis"]),

                    SidePanel = objReader["SidePanel"].ToString().Length == 0 ? "" : objReader["SidePanel"].ToString(),
                    Japan = objReader["Japan"].ToString().Length == 0 ? "" : objReader["Japan"].ToString()
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
