using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class BF200Service:IModelService
    {
        /// <summary>
        /// 根据项目Id查询BF200集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DataSet GetModelByDataSet(string projectId)
        {
            string sql = "select BF200Id,BF200.ModuleTreeId,Length,LeftLength,RightLength,MPanelLength,WPanelLength,MPanelNo,UVType from BF200";
            sql += " inner join ModuleTree on BF200.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }
        /// <summary>
        /// 根据BF200ID返回BF200
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where BF200Id={id}");
        }
        /// <summary>
        /// 根据模型树ID返回BF200
        /// </summary>
        /// <param name="moduleTreeId"></param>
        /// <returns></returns>
        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }
        /// <summary>
        /// 根据条件查找BF200
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select BF200Id,ModuleTreeId,Length,LeftLength,RightLength,MPanelLength,WPanelLength,MPanelNo,UVType from BF200";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            BF200 objModel = null;
            if (objReader.Read())
            {
                objModel = new BF200()
                {
                    BF200Id = Convert.ToInt32(objReader["BF200Id"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["Length"]),
                    LeftLength = objReader["LeftLength"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["LeftLength"]),
                    RightLength = objReader["RightLength"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["RightLength"]),
                    MPanelLength = objReader["MPanelLength"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["MPanelLength"]),
                    WPanelLength = objReader["WPanelLength"].ToString().Length == 0 ? 0 : Convert.ToDouble(objReader["WPanelLength"]),
                    MPanelNo = objReader["MPanelNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["MPanelNo"]),
                    UVType = objReader["UVType"].ToString().Length == 0 ? "" : objReader["UVType"].ToString()
                };
            }
            objReader.Close();
            return objModel;
        }
        /// <summary>
        /// 修改BF200的制图参数
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public int EditModel(IModel models)
        {
            BF200 objModel = (BF200)models;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update BF200 set Length=@Length,LeftLength=@LeftLength,RightLength=@RightLength,MPanelLength=@MPanelLength,WPanelLength=@WPanelLength,");
            sqlBuilder.Append("MPanelNo=@MPanelNo,UVType=@UVType where BF200Id=@BF200Id");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@LeftLength",objModel.LeftLength),
                new SqlParameter("@RightLength",objModel.RightLength),
                new SqlParameter("@MPanelLength",objModel.MPanelLength),
                new SqlParameter("@WPanelLength",objModel.WPanelLength),
                new SqlParameter("@MPanelNo",objModel.MPanelNo),
                new SqlParameter("@UVType",objModel.UVType),
                new SqlParameter("@BF200Id",objModel.BF200Id)
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
