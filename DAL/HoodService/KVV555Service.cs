using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class KVV555Service : IModelService
    {
        /// <summary>
        /// 根据项目Id查询KVV555集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DataSet GetModelByDataSet(string projectId)
        {
            string sql = "select KVV555Id,KVV555.ModuleTreeId,Item,Module,Length,Deepth,ExRightDis,ExNo,EXDis,ExLength,ExWidth,ExHeight,SidePanel,LightType from KVV555";
            sql += " inner join ModuleTree on KVV555.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += $" where ProjectId={projectId}";
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }
        /// <summary>
        /// 根据KVV555ID返回KVV555
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql($" where KVV555Id={id}");
        }
        /// <summary>
        /// 根据模型树ID返回KVV555
        /// </summary>
        /// <param name="moduleTreeId"></param>
        /// <returns></returns>
        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql($" where ModuleTreeId={moduleTreeId}");
        }
        /// <summary>
        /// 根据条件查找KVV555
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select KVV555Id,ModuleTreeId,Length,Deepth,ExRightDis,ExNo,EXDis,ExLength,ExWidth,ExHeight,SidePanel,LightType from KVV555";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            KVV555 objModel = null;
            if (objReader.Read())
            {
                objModel = new KVV555()
                {
                    KVV555Id = Convert.ToInt32(objReader["KVV555Id"]),
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

                    SidePanel = objReader["SidePanel"].ToString().Length == 0 ? "" : objReader["SidePanel"].ToString(),
                    

                    LightType = objReader["LightType"].ToString().Length == 0 ? "" : objReader["LightType"].ToString()
                    
                };
            }
            objReader.Close();
            return objModel;
        }
        /// <summary>
        /// 修改KVV555的制图参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditModel(IModel model)
        {
            KVV555 objModel = (KVV555)model;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update KVV555 set Length=@Length,Deepth=@Deepth,ExRightDis=@ExRightDis,ExNo=@ExNo,EXDis=@EXDis,ExLength=@ExLength,ExWidth=@ExWidth,ExHeight=@ExHeight");
            sqlBuilder.Append(",LightType=@LightType where KVV555Id=@KVV555Id");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@Deepth",objModel.Deepth),
                new SqlParameter("@ExRightDis",objModel.ExRightDis),
                new SqlParameter("@ExNo",objModel.ExNo),
                new SqlParameter("@EXDis",objModel.ExDis),
                new SqlParameter("@ExLength",objModel.ExLength),
                new SqlParameter("@ExWidth",objModel.ExWidth),
                new SqlParameter("@ExHeight",objModel.ExHeight),
                new SqlParameter("@LightType",objModel.LightType),
                new SqlParameter("@KVV555Id",objModel.KVV555Id)
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
