using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class ANService : IModelService
    {
        /// <summary>
        /// 根据项目Id查询AN集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DataSet GetModelByDataSet(string projectId)
        {
            string sql = "select ANId,AN.ModuleTreeId,Length,Width,ANSUL,ANYDis,ANDropNo,ANDropDis1,ANDropDis2,ANDropDis3,ANDropDis4,ANDropDis5,ANDetectorNo,ANDetectorEnd,ANDetectorDis1,ANDetectorDis2,ANDetectorDis3,ANDetectorDis4,ANDetectorDis5,MARVEL,IRNo,IRDis1,IRDis2,IRDis3 from AN";
            sql += " inner join ModuleTree on AN.ModuleTreeId=ModuleTree.ModuleTreeId";
            sql += " inner join DrawingPlan on ModuleTree.DrawingPlanId=DrawingPlan.DrawingPlanId";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " order by Item,Module";
            return SQLHelper.GetDataSet(sql);
        }
        /// <summary>
        /// 根据ANID返回AN
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IModel GetModelById(string id)
        {
            return GetModelByWhereSql(string.Format(" where ANId={0}", id));
        }
        /// <summary>
        /// 根据模型树ID返回AN
        /// </summary>
        /// <param name="moduleTreeId"></param>
        /// <returns></returns>
        public IModel GetModelByModuleTreeId(string moduleTreeId)
        {
            return GetModelByWhereSql(string.Format(" where ModuleTreeId={0}", moduleTreeId));
        }
        /// <summary>
        /// 根据条件查找AN
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public IModel GetModelByWhereSql(string whereSql)
        {
            string sql =
                "select ANId,ModuleTreeId,Length,Width,ANSUL,ANYDis,ANDropNo,ANDropDis1,ANDropDis2,ANDropDis3,ANDropDis4,ANDropDis5," +
                "ANDetectorNo,ANDetectorEnd,ANDetectorDis1,ANDetectorDis2,ANDetectorDis3,ANDetectorDis4,ANDetectorDis5," +
                "MARVEL,IRNo,IRDis1,IRDis2,IRDis3 from AN";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            AN objModel = null;
            if (objReader.Read())
            {
                objModel = new AN()
                {
                    ANId = Convert.ToInt32(objReader["ANId"]),
                    ModuleTreeId = Convert.ToInt32(objReader["ModuleTreeId"]),
                    //最好不要用=null去判断，提示类型转换错误
                    Length = objReader["Length"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Length"]),
                    Width = objReader["Width"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["Width"]),
                    ANSUL = objReader["ANSUL"].ToString().Length == 0 ? "" : objReader["ANSUL"].ToString(),
                    ANYDis = objReader["ANYDis"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANYDis"]),
                    ANDropNo = objReader["ANDropNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["ANDropNo"]),
                    ANDropDis1 = objReader["ANDropDis1"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDropDis1"]),
                    ANDropDis2 = objReader["ANDropDis2"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDropDis2"]),
                    ANDropDis3 = objReader["ANDropDis3"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDropDis3"]),
                    ANDropDis4 = objReader["ANDropDis4"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDropDis4"]),
                    ANDropDis5 = objReader["ANDropDis5"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDropDis5"]),
                    ANDetectorNo = objReader["ANDetectorNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["ANDetectorNo"]),
                    ANDetectorEnd = objReader["ANDetectorEnd"].ToString().Length == 0 ? "" : objReader["ANDetectorEnd"].ToString(),
                    ANDetectorDis1 = objReader["ANDetectorDis1"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDetectorDis1"]),
                    ANDetectorDis2 = objReader["ANDetectorDis2"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDetectorDis2"]),
                    ANDetectorDis3 = objReader["ANDetectorDis3"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDetectorDis3"]),
                    ANDetectorDis4 = objReader["ANDetectorDis4"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDetectorDis4"]),
                    ANDetectorDis5 = objReader["ANDetectorDis5"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["ANDetectorDis5"]),

                    MARVEL = objReader["MARVEL"].ToString().Length == 0 ? "" : objReader["MARVEL"].ToString(),
                    IRNo = objReader["IRNo"].ToString().Length == 0 ? 0 : Convert.ToInt32(objReader["IRNo"]),
                    IRDis1 = objReader["IRDis1"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["IRDis1"]),
                    IRDis2 = objReader["IRDis2"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["IRDis2"]),
                    IRDis3 = objReader["IRDis3"].ToString().Length == 0 ? 0 : Convert.ToDecimal(objReader["IRDis3"])
                };
            }
            objReader.Close();
            return objModel;
        }
        /// <summary>
        /// 修改AN的制图参数
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public int EditModel(IModel models)
        {
            AN objModel = (AN)models;
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update AN set Length=@Length,Width=@Width,ANSUL=@ANSUL,ANYDis=@ANYDis,ANDropNo=@ANDropNo,ANDropDis1=@ANDropDis1,ANDropDis2=@ANDropDis2,ANDropDis3=@ANDropDis3,ANDropDis4=@ANDropDis4,ANDropDis5=@ANDropDis5,");
            sqlBuilder.Append("ANDetectorNo=@ANDetectorNo,ANDetectorEnd=@ANDetectorEnd,ANDetectorDis1=@ANDetectorDis1,ANDetectorDis2=@ANDetectorDis2,ANDetectorDis3=@ANDetectorDis3,ANDetectorDis4=@ANDetectorDis4,ANDetectorDis5=@ANDetectorDis5,");
            sqlBuilder.Append("MARVEL=@MARVEL,IRNo=@IRNo,IRDis1=@IRDis1,IRDis2=@IRDis2,IRDis3=@IRDis3 where ANId=@ANId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Length",objModel.Length),
                new SqlParameter("@Width",objModel.Width),
                new SqlParameter("@ANSUL",objModel.ANSUL),
                new SqlParameter("@ANYDis",objModel.ANYDis),
                new SqlParameter("@ANDropNo",objModel.ANDropNo),
                new SqlParameter("@ANDropDis1",objModel.ANDropDis1),
                new SqlParameter("@ANDropDis2",objModel.ANDropDis2),
                new SqlParameter("@ANDropDis3",objModel.ANDropDis3),
                new SqlParameter("@ANDropDis4",objModel.ANDropDis4),
                new SqlParameter("@ANDropDis5",objModel.ANDropDis5),
                new SqlParameter("@ANDetectorNo",objModel.ANDetectorNo),
                new SqlParameter("@ANDetectorEnd",objModel.ANDetectorEnd),
                new SqlParameter("@ANDetectorDis1",objModel.ANDetectorDis1),
                new SqlParameter("@ANDetectorDis2",objModel.ANDetectorDis2),
                new SqlParameter("@ANDetectorDis3",objModel.ANDetectorDis3),
                new SqlParameter("@ANDetectorDis4",objModel.ANDetectorDis4),
                new SqlParameter("@ANDetectorDis5",objModel.ANDetectorDis5),
                new SqlParameter("@MARVEL",objModel.MARVEL),
                new SqlParameter("@IRNo",objModel.IRNo),
                new SqlParameter("@IRDis1",objModel.IRDis1),
                new SqlParameter("@IRDis2",objModel.IRDis2),
                new SqlParameter("@IRDis3",objModel.IRDis3),
                new SqlParameter("@ANId",objModel.ANId)
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
