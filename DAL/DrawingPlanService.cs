using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 制图计划
    /// </summary>
    public class DrawingPlanService
    {
        public DataSet GetScopeByDataSet(string projectId)
        {
            string sql = "select Model as '机型',sum(ModuleNo) as '总台数' from DrawingPlan";
            sql += string.Format(" where ProjectId={0}", projectId);
            sql += " group by Model";
            return SQLHelper.GetDataSet(sql);
        }
        /// <summary>
        /// 根据项目号UserId返回制图计划集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<DrawingPlan> GetDrawingPlanByUserId(string userId)
        {
            return GetDrawingPlanByWhereSql(string.Format(" where Projects.UserId={0}", userId));
        }
        /// <summary>
        /// 根据项目号ODPNo返回制图计划集合
        /// </summary>
        /// <param name="odpNo"></param>
        /// <returns></returns>
        public List<DrawingPlan> GetDrawingPlanByODPNo(string odpNo)
        {
            return GetDrawingPlanByWhereSql(string.Format(" where ODPNo='{0}'", odpNo));
        }
        /// <summary>
        /// 根据项目Id返回制图计划集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<DrawingPlan> GetDrawingPlanByProjectId(string projectId)
        {
            return GetDrawingPlanByWhereSql(string.Format(" where DrawingPlan.ProjectId={0}", projectId));
        }
        /// <summary>
        /// 根据单个条件返回制图计划集合
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public List<DrawingPlan> GetDrawingPlanByWhereSql(string whereSql)
        {
            string sql = "select DrawingPlanId,UserAccount,DrawingPlan.ProjectId,ODPNo,Item,Model,ModuleNo,DrawingPlan.DrReleaseTarget,DrReleaseActual,SubTotalWorkload,DrawingPlan.AddedDate,ProjectName from DrawingPlan";
            sql += " inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId";
            sql += " inner join Users on Users.UserId=Projects.UserId";
            sql += " left join ProjectTracking on DrawingPlan.ProjectId=ProjectTracking.ProjectId";
            sql += whereSql;
            sql += " order by DrawingPlan.DrReleasetarget desc";//按照计划发图日期，倒序排列
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<DrawingPlan> list = new List<DrawingPlan>();
            while (objReader.Read())
            {
                list.Add(new DrawingPlan()
                {
                    DrawingPlanId = Convert.ToInt32(objReader["DrawingPlanId"]),
                    UserAccount = objReader["UserAccount"].ToString(),
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    ODPNo = objReader["ODPNo"].ToString(),
                    ProjectName=objReader["ProjectName"].ToString(),
                    Item = objReader["Item"].ToString(),
                    Model = objReader["Model"].ToString(),
                    ModuleNo = Convert.ToInt32(objReader["ModuleNo"]),
                    DrReleaseTarget = Convert.ToDateTime(objReader["DrReleaseTarget"]),
                    DrReleaseActual = objReader["DrReleaseActual"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["DrReleaseActual"]),
                    SubTotalWorkload = Convert.ToDecimal(objReader["SubTotalWorkload"]),
                    AddedDate = Convert.ToDateTime(objReader["AddedDate"]),
                    RemainingDays= (Convert.ToDateTime(objReader["DrReleaseTarget"]).Subtract(DateTime.Now).Days)<0?
                        0: (int)Convert.ToDateTime(objReader["DrReleaseTarget"]).Subtract(DateTime.Today).Days,
                    ProgressValue = Convert.ToDateTime(objReader["DrReleaseTarget"]).Subtract(Convert.ToDateTime(objReader["AddedDate"])).Days<=0 ? 100:
                        (int)(100 * (Convert.ToDateTime(objReader["DrReleaseTarget"]).Subtract(Convert.ToDateTime(objReader["AddedDate"])).Days
                        - Convert.ToDateTime(objReader["DrReleaseTarget"]).Subtract(DateTime.Today).Days)
                        / (Convert.ToDateTime(objReader["DrReleaseTarget"]).Subtract(Convert.ToDateTime(objReader["AddedDate"])).Days))


                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据计划编号返回单条计划记录
        /// </summary>
        /// <param name="drawingPlanId"></param>
        /// <returns></returns>
        public DrawingPlan GetDrawingPlanById(string drawingPlanId)
        {
            string sql = "select DrawingPlanId,UserAccount,DrawingPlan.ProjectId,ODPNo,Item,Model,ModuleNo,DrReleaseTarget,SubTotalWorkload,DrawingPlan.AddedDate,ProjectName from DrawingPlan";
            sql += " inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId";
            sql += " inner join Users on Users.UserId=Projects.UserId";
            sql += string.Format(" where DrawingPlanId = {0}", drawingPlanId);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            DrawingPlan objDrawingPlan = null;
            if (objReader.Read())
            {
                objDrawingPlan = new DrawingPlan()
                {
                    DrawingPlanId = Convert.ToInt32(objReader["DrawingPlanId"]),
                    UserAccount = objReader["UserAccount"].ToString(),
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    ODPNo = objReader["ODPNo"].ToString(),
                    ProjectName=objReader["ProjectName"].ToString(),
                    Item = objReader["Item"].ToString(),
                    Model = objReader["Model"].ToString(),
                    ModuleNo = Convert.ToInt32(objReader["ModuleNo"]),
                    DrReleaseTarget = Convert.ToDateTime(objReader["DrReleaseTarget"]),
                    SubTotalWorkload = Convert.ToDecimal(objReader["SubTotalWorkload"]),
                    AddedDate = Convert.ToDateTime(objReader["AddedDate"])
                };
            }
            objReader.Close();
            return objDrawingPlan;
        }
        /// <summary>
        /// 添加制图计划
        /// </summary>
        /// <param name="objDrawingPlan"></param>
        /// <returns></returns>
        public int AddDraingPlan(DrawingPlan objDrawingPlan)
        {
            string sql = "insert into DrawingPlan (ProjectId,Item,Model,ModuleNo,DrReleasetarget,SubTotalWorkload)";
            sql += " values({0},'{1}','{2}','{3}','{4}','{5}');select @@identity";
            sql = string.Format(sql,  objDrawingPlan.ProjectId, objDrawingPlan.Item,
                objDrawingPlan.Model, objDrawingPlan.ModuleNo, objDrawingPlan.DrReleaseTarget, objDrawingPlan.SubTotalWorkload);
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            }
            catch (SqlException ex)
            {
                throw new Exception("添加制图计划时数据库访问异常" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改制图计划记录
        /// </summary>
        /// <param name="objDrawingPlan"></param>
        /// <returns></returns>
        public int EditDrawingPlan(DrawingPlan objDrawingPlan)
        {
            string sql = "update DrawingPlan set ProjectId={0},Item='{1}',Model='{2}',ModuleNo='{3}',";
            sql += "DrReleasetarget='{4}',SubTotalWorkload='{5}',AddedDate='{6}' where DrawingPlanId={7}";
            sql = string.Format(sql, objDrawingPlan.ProjectId, objDrawingPlan.Item, objDrawingPlan.Model, objDrawingPlan.ModuleNo,
                  objDrawingPlan.DrReleaseTarget, objDrawingPlan.SubTotalWorkload, objDrawingPlan.AddedDate,objDrawingPlan.DrawingPlanId);
            try
            {
                return SQLHelper.Update(sql);
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
        /// <summary>
        /// 删除计划记录
        /// </summary>
        /// <param name="drawingPlanId"></param>
        /// <returns></returns>
        public int DeleteDrawingPlan(string drawingPlanId)
        {
            string sql = "delete from DrawingPlan where DrawingPlanId={0}";
            sql = string.Format(sql, drawingPlanId);
            try
            {
                return SQLHelper.Update(sql);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new Exception("该记录已被其他数据表关联，不能直接删除");
                }
                else
                {
                    throw new Exception("数据库操作异常，不能执行删除：" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
