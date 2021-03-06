﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class UCPDXFService : IModelService
    {
        public int EditModel(IModel model)
        {
            UCPDXF objModel = (UCPDXF)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update UCPDXF set Quantity=@Quantity where UCPDXFId=@UCPDXFId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Quantity",objModel.Quantity),
                new SqlParameter("@UCPDXFId",objModel.UCPDXFId)
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
            string sql = "select UCPDXFId,UCPDXF.ModuleTreeId,Item,Module,Quantity from UCPDXF";
            sql += " inner join ModuleTree on UCPDXF.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }

        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where UCPDXFId={0}", id));
        }

        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }

        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select UCPDXFId,ModuleTreeId,Quantity from UCPDXF";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            UCPDXF objModel = null;
            if (objReader.Read())
            {
                objModel = new UCPDXF()
                {
                    UCPDXFId = Convert.ToInt32(objReader["UCPDXFId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Quantity = objReader["Quantity"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["Quantity"]),
                };
            }
            objReader.Close();
            return objModel;
        }
    }
}
