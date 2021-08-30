using System;
using System.Collections.Generic;
using Models;
using System.Data.SqlClient;

namespace DAL
{
    public class RequirementService
    {
        #region 通用技术要求
        /// <summary>
        /// 根据id返回单个通用技术要求
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GeneralRequirement GetGeneralRequirementById(string id, string sbu)
        {
            return GetGeneralRequirementByWhereSql(string.Format(" where GeneralRequirementId={0}", id), sbu);
        }
        /// <summary>
        /// 根据ODPNo返回单个通用技术要求
        /// </summary>
        /// <param name="odpNo"></param>
        /// <returns></returns>
        public GeneralRequirement GetGeneralRequirementByODPNo(string odpNo, string sbu)
        {
            return GetGeneralRequirementByWhereSql(string.Format(" where ODPNo='{0}'", odpNo), sbu);
        }
        /// <summary>
        /// 根据条件返回单个通用技术要求
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public GeneralRequirement GetGeneralRequirementByWhereSql(string whereSql, string sbu)
        {
            string sql = string.Format("select GeneralRequirementId,GeneralRequirements{0}.ProjectId,ODPNo,ProjectTypes.TypeId,TypeName,InputPower,MARVEL,ANSULPrePipe,ANSULSystem,RiskLevel,MainAssyPath from GeneralRequirements{0}", sbu);
            sql += string.Format(" inner join Projects{0} on Projects{0}.ProjectId=GeneralRequirements{0}.ProjectId", sbu);
            sql += string.Format(" inner join ProjectTypes on ProjectTypes.TypeId=GeneralRequirements{0}.TypeId", sbu);
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            GeneralRequirement objGeneralRequirement = null;
            if (objReader.Read())
            {
                objGeneralRequirement = new GeneralRequirement()
                {
                    GeneralRequirementId = Convert.ToInt32(objReader["GeneralRequirementId"]),
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    ODPNo = objReader["ODPNo"].ToString(),
                    TypeId = Convert.ToInt32(objReader["TypeId"]),
                    TypeName = objReader["TypeName"].ToString(),
                    InputPower = objReader["InputPower"].ToString(),
                    MARVEL = objReader["MARVEL"].ToString(),
                    ANSULPrePipe = objReader["ANSULPrePipe"].ToString(),
                    ANSULSystem = objReader["ANSULSystem"].ToString(),
                    MainAssyPath = objReader["MainAssyPath"].ToString(),
                    RiskLevel = Convert.ToInt32(objReader["RiskLevel"])

                };
            }
            objReader.Close();
            return objGeneralRequirement;
        }

        /// <summary>
        /// 添加通用技术要求
        /// </summary>
        /// <param name="objGeneralRequirement"></param>
        /// <returns></returns>
        public int AddGeneralRequirement(GeneralRequirement objGeneralRequirement, string sbu)
        {
            string sql = string.Format("insert into GeneralRequirements{0} (ProjectId,TypeId,InputPower,MARVEL,ANSULPrePipe,ANSULSystem,RiskLevel)", sbu);
            sql += " values({0},{1},'{2}','{3}','{4}','{5}',{6});select @@identity";
            sql = string.Format(sql, objGeneralRequirement.ProjectId, objGeneralRequirement.TypeId, objGeneralRequirement.InputPower,
                objGeneralRequirement.MARVEL, objGeneralRequirement.ANSULPrePipe, objGeneralRequirement.ANSULSystem, objGeneralRequirement.RiskLevel);
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            }
            catch (SqlException ex)
            {
                //2627
                if (ex.Number == 2627)
                {
                    throw new Exception("项目号重复,不能添加重复的通用技术要求");
                }
                else
                {
                    throw new Exception("添加通用技术要求时数据库访问异常" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改通用技术要求
        /// </summary>
        /// <param name="objGeneralRequirement"></param>
        /// <returns></returns>
        public int EditGeneralRequirement(GeneralRequirement objGeneralRequirement, string sbu)
        {
            string sql = string.Format("update GeneralRequirements{0}", sbu);
            sql += " set ProjectId={0},TypeId={1},InputPower='{2}',MARVEL='{3}',";
            sql += "ANSULPrePipe='{4}',ANSULSystem='{5}',RiskLevel={6} where GeneralRequirementId={7}";
            sql = string.Format(sql, objGeneralRequirement.ProjectId, objGeneralRequirement.TypeId, objGeneralRequirement.InputPower,
                objGeneralRequirement.MARVEL, objGeneralRequirement.ANSULPrePipe, objGeneralRequirement.ANSULSystem,
                objGeneralRequirement.RiskLevel, objGeneralRequirement.GeneralRequirementId);
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
        /// 更新天花烟罩总装配地址到通用技术要求中
        /// </summary>
        /// <param name="objGeneralRequirement"></param>
        /// <returns></returns>
        public int UpdateMainAssyPath(GeneralRequirement objGeneralRequirement, string sbu)
        {
            string sql = string.Format("update GeneralRequirements{0}", sbu);
            sql += " set MainAssyPath='{0}' where GeneralRequirementId={1}";
            sql = string.Format(sql, objGeneralRequirement.MainAssyPath, objGeneralRequirement.GeneralRequirementId);
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
        /// 删除通用技术要求
        /// </summary>
        /// <param name="generalRequirementId"></param>
        /// <returns></returns>
        public int DeleteGeneralRequirement(string generalRequirementId, string sbu)
        {
            string sql = string.Format("delete from GeneralRequirements{0}", sbu);
            sql += " where GeneralRequirementId={0}";
            sql = string.Format(sql, generalRequirementId);
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
        #endregion

        #region 特殊技术要求

        /// <summary>
        /// 根据id返回单条特殊技术要求
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SpecialRequirement GetSpecialRequirementById(string id, string sbu)
        {
            string sql = string.Format("select SpecialRequirementId,Content from SpecialRequirements{0}", sbu);
            sql += string.Format(" where SpecialRequirementId={0}", id);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            SpecialRequirement objSpecialRequirement = null;
            if (objReader.Read())
            {
                objSpecialRequirement = new SpecialRequirement()
                {
                    SpecialRequirementId = Convert.ToInt32(objReader["SpecialRequirementId"]),
                    Content = objReader["Content"].ToString()
                };
            }
            objReader.Close();
            return objSpecialRequirement;
        }
        /// <summary>
        /// 根据项目号ODPNo返回特殊技术要求集合
        /// </summary>
        /// <param name="ODPNo"></param>
        /// <returns></returns>
        public List<SpecialRequirement> GetSpecialRequirementsByODPNo(string ODPNo, string sbu)
        {
            string sql = string.Format("select SpecialRequirementId,Projects{0}.ProjectId,ODPNo,Content from SpecialRequirements{0}", sbu);
            sql += string.Format(" inner join Projects{0} on SpecialRequirements{0}.ProjectId=Projects{0}.ProjectId", sbu);
            sql += string.Format(" where ODPNo='{0}'", ODPNo);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<SpecialRequirement> list = new List<SpecialRequirement>();
            while (objReader.Read())
            {
                list.Add(new SpecialRequirement()
                {
                    SpecialRequirementId = Convert.ToInt32(objReader["SpecialRequirementId"]),
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    ODPNo = objReader["ODPNo"].ToString(),
                    Content = objReader["Content"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据项目号ODPNo返回特殊要求列表
        /// </summary>
        /// <param name="ODPNo"></param>
        /// <returns></returns>
        public List<string> GetSpecialRequirementList(string ODPNo, string sbu)
        {
            string sql = string.Format("select Content from SpecialRequirements{0}", sbu);
            sql += string.Format(" inner join Projects{0} on SpecialRequirements{0}.ProjectId=Projects{0}.ProjectId", sbu);
            sql += string.Format(" where ODPNo='{0}'", ODPNo);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<string> list = new List<string>();
            while (objReader.Read())
            {
                list.Add(objReader["Content"].ToString());
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 添加特殊技术要求
        /// </summary>
        /// <param name="objSpecialRequirement"></param>
        /// <returns></returns>
        public int AddSpecialRequirement(SpecialRequirement objSpecialRequirement, string sbu)
        {
            string sql = string.Format("insert into SpecialRequirements{0} (ProjectId,Content)", sbu);
            sql += " values({0},'{1}');select @@identity";
            sql = string.Format(sql, objSpecialRequirement.ProjectId, objSpecialRequirement.Content);
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            }
            catch (SqlException ex)
            {

                throw new Exception("添加特殊技术要求时数据库访问异常" + ex.Message);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改特殊技术要求
        /// </summary>
        /// <param name="objSpecialRequirement"></param>
        /// <returns></returns>
        public int EditSpecialRequirement(SpecialRequirement objSpecialRequirement, string sbu)
        {
            string sql = string.Format("update SpecialRequirements{0}", sbu);
            sql += " set ProjectId={0},Content='{1}' where SpecialRequirementId={2}";
            sql = string.Format(sql, objSpecialRequirement.ProjectId, objSpecialRequirement.Content, objSpecialRequirement.SpecialRequirementId);
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
        /// 删除特殊技术要求
        /// </summary>
        /// <param name="specialRequirementId"></param>
        /// <returns></returns>
        public int DeleteSpecialRequirement(string specialRequirementId, string sbu)
        {
            string sql = string.Format("delete from SpecialRequirements{0}", sbu);
            sql += " where SpecialRequirementId={0}";
            sql = string.Format(sql, specialRequirementId);
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
        #endregion
    }
}
