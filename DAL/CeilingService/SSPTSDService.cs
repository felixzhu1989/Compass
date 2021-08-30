using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class SSPTSDService : IModelService
    {
        /// <summary>
        /// 根据项目Id查询SSPTSD集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DataSet GetModelByDataSet(string projectId)
        {
            string sql = "select SSPTSDId,SSPTSD.ModuleTreeId,Length,LeftType,RightType,LeftLength,RightLength,MPanelNo,LightType from SSPTSD";
            sql += " inner join ModuleTree on SSPTSD.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }
        /// <summary>
        /// 根据SSPTSDID返回SSPTSD
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where SSPTSDId={0}", id));
        }
        /// <summary>
        /// 根据模型树ID返回SSPTSD
        /// </summary>
        /// <param name="moduleTreeId"></param>
        /// <returns></returns>
        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }
        /// <summary>
        /// 根据条件查找SSPTSD
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select SSPTSDId,ModuleTreeId,Length,LeftType,RightType,LeftLength,RightLength,MPanelNo,LightType from SSPTSD";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            SSPTSD objModel = null;
            if (objReader.Read())
            {
                objModel = new SSPTSD()
                {
                    SSPTSDId = Convert.ToInt32(objReader["SSPTSDId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Length"]),
                    LeftType = objReader["LeftType"].ToString().Length == 0 ? "" : objReader["LeftType"].ToString(),
                    RightType = objReader["RightType"].ToString().Length == 0 ? "" : objReader["RightType"].ToString(),
                    LeftLength = objReader["LeftLength"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["LeftLength"]),
                    RightLength = objReader["RightLength"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["RightLength"]),
                    MPanelNo = objReader["MPanelNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["MPanelNo"]),
                    LightType = objReader["LightType"].ToString().Length == 0 ? "" : objReader["LightType"].ToString()
                };
            }
            objReader.Close();
            return objModel;
        }
        /// <summary>
        /// 修改SSPTSD的制图参数
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public int EditModel(IModel models)
        {
            SSPTSD objModel = (SSPTSD)models;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update SSPTSD set Length=@Length,LeftType=@LeftType,RightType=@RightType,LeftLength=@LeftLength,RightLength=@RightLength,");
            sqlBuilder.Append("MPanelNo=@MPanelNo,LightType=@LightType where SSPTSDId=@SSPTSDId");
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
                new SqlParameter("@SSPTSDId",objModel.SSPTSDId)
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
