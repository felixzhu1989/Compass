using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class SSPTBDService : IModelService
    {
        /// <summary>
        /// 根据项目Id查询SSPTBD集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DataSet GetModelByDataSet(string projectId)
        {
            string sql = "select SSPTBDId,SSPTBD.ModuleTreeId,Length,LeftType,RightType,LeftLength,RightLength,MPanelNo,LightType from SSPTBD";
            sql += " inner join ModuleTree on SSPTBD.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }
        /// <summary>
        /// 根据SSPTBDID返回SSPTBD
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where SSPTBDId={id}");
        }
        /// <summary>
        /// 根据模型树ID返回SSPTBD
        /// </summary>
        /// <param name="moduleTreeId"></param>
        /// <returns></returns>
        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }
        /// <summary>
        /// 根据条件查找SSPTBD
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select SSPTBDId,ModuleTreeId,Length,LeftType,RightType,LeftLength,RightLength,MPanelNo,LightType from SSPTBD";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            SSPTBD objModel = null;
            if (objReader.Read())
            {
                objModel = new SSPTBD()
                {
                    SSPTBDId = Convert.ToInt32(objReader["SSPTBDId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Length"]),
                    LeftType = objReader["LeftType"].ToString().Length == 0 ? "" : objReader["LeftType"].ToString(),
                    RightType = objReader["RightType"].ToString().Length == 0 ? "" : objReader["RightType"].ToString(),
                    LeftLength = objReader["LeftLength"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["LeftLength"]),
                    RightLength = objReader["RightLength"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["RightLength"]),
                    MPanelNo = objReader["MPanelNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["MPanelNo"]),
                    LightType = objReader["LightType"].ToString().Length == 0 ? "" : objReader["LightType"].ToString()
                };
            }
            objReader.Close();
            return objModel;
        }
        /// <summary>
        /// 修改SSPTBD的制图参数
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public int EditModel(IModel models)
        {
            SSPTBD objModel = (SSPTBD)models;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update SSPTBD set Length=@Length,LeftType=@LeftType,RightType=@RightType,LeftLength=@LeftLength,RightLength=@RightLength,");
            sqlBuilder.Append("MPanelNo=@MPanelNo,LightType=@LightType where SSPTBDId=@SSPTBDId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@LeftLength",objModel.LeftLength),
                new SqlParameter("@RightLength",objModel.RightLength),
                new SqlParameter("@LeftType",objModel.LeftType),
                new SqlParameter("@RightType",objModel.RightType),
                new SqlParameter("@MPanelNo",objModel.MPanelNo),
                new SqlParameter("@LightType",objModel.LightType),
                new SqlParameter("@SSPTBDId",objModel.SSPTBDId)
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
