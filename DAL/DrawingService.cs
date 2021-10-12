using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Models;

namespace DAL
{
    public class DrawingService
    {
        /// <summary>
        /// 根据项目Id返回图纸集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<Drawing> GetDrawingsByProjectId(string projectId, string sbu)
        {
            return GetDrawingsByWhereSql($" where DrawingPlan{sbu}.ProjectId = {projectId}",sbu);
        }
        /// <summary>
        /// 根据项目号返回图纸集合
        /// </summary>
        /// <param name="odpNo"></param>
        /// <returns></returns>
        public List<Drawing> GetDrawingsByODPNo(string odpNo, string sbu)
        {
            return GetDrawingsByWhereSql($" where ODPNo = '{odpNo}'",sbu);
        }
        /// <summary>
        /// 根据条件返回图纸集合
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public List<Drawing> GetDrawingsByWhereSql(string whereSql,string sbu)
        {
            string sql = $"select DrawingPlanId,DrawingPlan{sbu}.ProjectId,ODPNo,Item,LabelImage,ModuleNo,ProjectName,HoodType from DrawingPlan{sbu}";
            sql += $" inner join Projects{sbu} on Projects{sbu}.ProjectId=DrawingPlan{sbu}.ProjectId";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<Drawing> list = new List<Drawing>();
            while (objReader.Read())
            {
                list.Add(new Drawing()
                {
                    DrawingPlanId = Convert.ToInt32(objReader["DrawingPlanId"]),
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    ODPNo = objReader["ODPNo"].ToString(),
                    Item = objReader["Item"].ToString(),
                    LabelImage = objReader["LabelImage"].ToString(),
                    ModuleNo = Convert.ToInt32(objReader["ModuleNo"]),
                    ProjectName = objReader["ProjectName"].ToString(),
                    HoodType=objReader["HoodType"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据图纸计划id返回单张图纸
        /// </summary>
        /// <param name="drawingPlanId"></param>
        /// <returns></returns>
        public Drawing GetDrawingById(string drawingPlanId, string sbu)
        {
            return GetDrawingByWhereSql($" where DrawingPlanId={drawingPlanId}",sbu);
        }
        /// <summary>
        /// 根据条件返回单张图纸
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public Drawing GetDrawingByWhereSql(string whereSql, string sbu)
        {
            string sql = $"select DrawingPlanId,DrawingPlan{sbu}.ProjectId,ODPNo,Item,LabelImage,ModuleNo,ProjectName,HoodType from DrawingPlan{sbu}";
            sql += $" inner join Projects{sbu} on Projects{sbu}.ProjectId=DrawingPlan{sbu}.ProjectId";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            Drawing objDrawing = null;
            if (objReader.Read())
            {
                objDrawing = new Drawing()
                {
                    DrawingPlanId = Convert.ToInt32(objReader["DrawingPlanId"]),
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    ODPNo = objReader["ODPNo"].ToString(),
                    Item = objReader["Item"].ToString(),
                    LabelImage = objReader["LabelImage"].ToString(),
                    ModuleNo= Convert.ToInt32(objReader["ModuleNo"]),
                    ProjectName = objReader["ProjectName"].ToString(),
                    HoodType=objReader["HoodType"].ToString()
                };
            }
            objReader.Close();
            return objDrawing;
        }
        /// <summary>
        /// 编辑图纸信息
        /// </summary>
        /// <param name="objDrawing"></param>
        /// <returns></returns>
        public int EditDrawing(Drawing objDrawing, string sbu)
        {
            string sql = $"update DrawingPlan{sbu}";
                         sql+=" set Item='{0}',LabelImage='{1}' where DrawingPlanId={2}";
            sql = string.Format(sql, objDrawing.Item, objDrawing.LabelImage, objDrawing.DrawingPlanId);
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
    }
}
