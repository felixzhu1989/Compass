using System;
using System.Collections.Generic;
using System.Text;
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
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sbu"></param>
        /// <returns></returns>
        public SqlDataPager GetSqlDataPager(string sbu)
        {
            StringBuilder innerJoin1 = new StringBuilder(string.Format("inner join Projects{0} on DrawingPlan{0}.ProjectId=Projects{0}.ProjectId", sbu));
            innerJoin1.Append($" inner join Users on Users.UserId=Projects{sbu}.UserId");
            innerJoin1.Append(string.Format(" left join ProjectTracking{0} on DrawingPlan{0}.ProjectId=ProjectTracking{0}.ProjectId", sbu));

            //初始化分页对象
            SqlDataPager objSqlDataPager = new SqlDataPager()
            {
                PrimaryKey = "DrawingPlanId",
                TableName = $"DrawingPlan{sbu}",
                InnerJoin1 = innerJoin1.ToString(),
                InnerJoin2 = string.Format("inner join Projects{0} on DrawingPlan{0}.ProjectId=Projects{0}.ProjectId", sbu),
                FiledName = string.Format("DrawingPlanId,UserAccount,ODPNo,Item,Model,ModuleNo,DrawingPlan{0}.DrReleaseTarget,DrReleaseActual,SubTotalWorkload,ProjectName,HoodType,IIF(DATEDIFF(DAY,GETDATE(),DrawingPlan{0}.DrReleaseTarget)<0,0,DATEDIFF(DAY,GETDATE(),DrawingPlan{0}.DrReleaseTarget)) as RemainingDays,IIF(DATEDIFF(DAY,GETDATE(),DrawingPlan{0}.DrReleaseTarget)<=0,100,100*DATEDIFF(DAY,GETDATE(),DrawingPlan{0}.AddedDate)/DATEDIFF(DAY,DrawingPlan{0}.DrReleaseTarget,DrawingPlan{0}.AddedDate)) as ProgressValue", sbu),
                CurrentPage = 1,
                Sort = $"DrawingPlan{sbu}.DrReleasetarget desc",
            };
            return objSqlDataPager;
        }


        #region MyRegion 查询月度销售额和全年销售额
        /// <summary>
        /// 按月份统计所有烟罩销售额
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetTotalSalesValueByMonth(string year)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select month(ShippingTime) as Mon,sum(SalesValue) as TotalSalesValue from FinancialData";
            sql += " inner join Projects on FinancialData.ProjectId=Projects.ProjectId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            sql += " group by month(ShippingTime) order by Mon asc";

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["Mon"].ToString(),
                    Value = Convert.ToDouble(objReader["TotalSalesValue"].ToString())
                });
            }
            objReader.Close();
            return list;
        }


        /// <summary>
        /// 查询全年所有烟罩销售额总和
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public double GetTotalSalesValueByYear(string year)
        {
            double Value = 0;
            string sql = "select sum(SalesValue) as TotalSalesValue from FinancialData";
            sql += " inner join Projects on FinancialData.ProjectId=Projects.ProjectId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                Value = Convert.ToDouble(objReader["TotalSalesValue"].ToString());
            }
            objReader.Close();
            return Value;
        }


        #endregion 查询月度销售额和全年销售额






        #region 查询年度各制图人员工作量

        /// <summary>
        /// 查询delay数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="userAcc"></param>
        /// <returns></returns>
        public List<ChartData> GetUserDelayByMonth(string year, string userAcc)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select month(Drtarget) as Mon,sum(DATEDIFF(DAY, Drtarget, DrActual))  as TotalDelay from view_DelayQuery";
            sql += " inner join Projects on view_DelayQuery.ProjectId=Projects.ProjectId";
            sql += " inner join Users on Users.UserId=Projects.UserId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            sql += $" and UserAccount='{userAcc}'";
            sql += " and DATEDIFF(DAY,Drtarget,DrActual)>0 group by month(Drtarget) order by Mon asc";

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["Mon"].ToString(),
                    Value = Convert.ToDouble(objReader["TotalDelay"].ToString())
                });
            }
            objReader.Close();
            return list;
        }


        /// <summary>
        /// 按月份统计人员工作量
        /// </summary>
        /// <param name="year"></param>
        /// <param name="userAcc"></param>
        /// <returns></returns>
        public List<ChartData> GetUserWorkloadByMonth(string year, string userAcc)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select month(ShippingTime) as Mon,sum(SubTotalWorkload) as TotalWorkload from DrawingPlan";
            sql += " inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId";
            sql += " inner join Users on Users.UserId=Projects.UserId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            sql += $" and UserAccount='{userAcc}'";
            sql += " group by month(ShippingTime) order by Mon asc";

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["Mon"].ToString(),
                    Value = Convert.ToDouble(objReader["TotalWorkload"].ToString())
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 按人员统计所有烟罩工作量
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetAllWorkloadByUser(string year)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select UserAccount,sum(SubTotalWorkload) as TotalWorkload from DrawingPlan";
            sql += " inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId";
            sql += " inner join Users on Users.UserId=Projects.UserId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            sql += " group by UserAccount order by TotalWorkload desc";

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["UserAccount"].ToString(),
                    Value = Convert.ToDouble(objReader["TotalWorkload"].ToString())
                });
            }
            objReader.Close();
            return list;
        }

        #endregion 查询年度各制图人员工作量

        #region 查询年度全部烟罩工作量

        /// <summary>
        /// 查询delay数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="userAcc"></param>
        /// <returns></returns>
        public List<ChartData> GetTotalDelayByMonth(string year)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select month(Drtarget) as Mon,sum(DATEDIFF(DAY, Drtarget, DrActual))  as TotalDelay from view_DelayQuery";
            sql += " inner join Projects on view_DelayQuery.ProjectId=Projects.ProjectId";
            sql += " inner join Users on Users.UserId=Projects.UserId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            sql += " and DATEDIFF(DAY,Drtarget,DrActual)>0 group by month(Drtarget) order by Mon asc";

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["Mon"].ToString(),
                    Value = Convert.ToDouble(objReader["TotalDelay"].ToString())
                });
            }
            objReader.Close();
            return list;
        }


        /// <summary>
        /// 查询全年所有烟罩Delay总和
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public double GetTotalDelayByYear(string year)
        {
            double Value = 0d;
            string sql = "select sum(DATEDIFF(DAY, Drtarget, DrActual)) as TotalDelay from view_DelayQuery";
            sql += " inner join Projects on view_DelayQuery.ProjectId=Projects.ProjectId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            sql += " and DATEDIFF(DAY,Drtarget,DrActual)>0";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                //当年有数据时才赋值，否则保持为0
                if (objReader["TotalDelay"].ToString().Length != 0)
                    Value = Convert.ToDouble(objReader["TotalDelay"].ToString());
            }
            objReader.Close();
            return Value;
        }

        /// <summary>
        /// 按月份统计所有烟罩工作量
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetTotalWorkloadByMonth(string year)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select month(ShippingTime) as Mon,sum(SubTotalWorkload) as TotalWorkload from DrawingPlan";
            sql += " inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            sql += " group by month(ShippingTime) order by Mon asc";

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["Mon"].ToString(),
                    Value = Convert.ToDouble(objReader["TotalWorkload"].ToString())
                });
            }
            objReader.Close();
            return list;
        }


        /// <summary>
        /// 查询全年所有烟罩工作量总和
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public double GetTotalWorkloadByYear(string year)
        {
            double Value = 0;
            string sql = "select sum(SubTotalWorkload) as TotalWorkload from DrawingPlan";
            sql += " inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                Value = Convert.ToDouble(objReader["TotalWorkload"].ToString());
            }
            objReader.Close();
            return Value;
        }

        /// <summary>
        /// 查询全年标准烟罩工作量总和
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public double GetTotalHoodWorkloadByYear(string year)
        {
            double Value = 0;
            string sql = "select sum(SubTotalWorkload) as TotalWorkload from DrawingPlan";
            sql += " inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            sql += " and HoodType='Hood'";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                Value = objReader["TotalWorkload"] == null ? 0 : Convert.ToDouble(objReader["TotalWorkload"].ToString());
            }
            objReader.Close();
            return Value;
        }
        #endregion 查询年度全部烟罩工作量



        #region 查询年度天花烟罩工作量

        /// <summary>
        /// 按月份统计单个机型天花烟罩工作量
        /// </summary>
        /// <param name="year"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public List<ChartData> GetCeilingWorkloadByMonth(string year, string modelType)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select month(ShippingTime) as Mon,sum(SubTotalWorkload) as TotalWorkload from DrawingPlan";
            sql += " inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            sql += $" and HoodType='Ceiling' and Model='{modelType}'";
            sql += " group by month(ShippingTime) order by Mon asc";

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["Mon"].ToString(),
                    Value = Convert.ToDouble(objReader["TotalWorkload"].ToString())
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 查询全年天花烟罩工作量总和
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public double GetTotalCeilingWorkloadByYear(string year)
        {
            double Value = 0;
            string sql = "select sum(SubTotalWorkload) as TotalWorkload from DrawingPlan";
            sql += " inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            sql += " and HoodType='Ceiling'";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                Value = Convert.ToDouble(objReader["TotalWorkload"].ToString());
            }
            objReader.Close();
            return Value;
        }

        /// <summary>
        /// 查询年度天花烟罩工作量
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetCeilingWorkloadByYear(string year)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select Model,sum(SubTotalWorkload) as TotalWorkload from DrawingPlan";
            sql += " inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            sql += " and HoodType='Ceiling' group by Model order by TotalWorkload desc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["Model"].ToString(),
                    Value = Convert.ToDouble(objReader["TotalWorkload"].ToString())
                });
            }
            objReader.Close();
            return list;
        }
        #endregion 查询年度天花烟罩工作量


        #region 查询年度普通烟罩数量
        /// <summary>
        /// 按月份统计单个机型烟罩数量
        /// </summary>
        /// <param name="year"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public List<ChartData> GetHoodModuleNoByMonth(string year, string modelType)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select month(ShippingTime) as Mon,sum(ModuleNo) as TotalModuleNo from DrawingPlan";
            sql += " inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            sql += $" and HoodType='Hood' and Model='{modelType}'";
            sql += " group by month(ShippingTime) order by Mon asc";

            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["Mon"].ToString(),
                    Value = Convert.ToDouble(objReader["TotalModuleNo"].ToString())
                });
            }
            objReader.Close();
            return list;
        }


        /// <summary>
        /// 查询全年普通烟罩的数量总和
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public double GetTotalHoodModuleNoByYear(string year)
        {
            double Value = 0;
            string sql = "select sum(ModuleNo) as TotalModuleNo from DrawingPlan";
            sql += " inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            sql += " and HoodType='Hood'";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                Value = Convert.ToDouble(objReader["TotalModuleNo"].ToString());
            }
            objReader.Close();
            return Value;
        }

        /// <summary>
        /// 查询年度各个机型数量
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ChartData> GetHoodModuleNoByYear(string year)
        {
            List<ChartData> list = new List<ChartData>();
            string sql = "select Model,sum(ModuleNo) as TotalModuleNo from DrawingPlan";
            sql += " inner join Projects on DrawingPlan.ProjectId=Projects.ProjectId";
            if (year == "ALL")
            {
                sql += " where ShippingTime>='2020/01/01'";
            }
            else
            {
                sql += $" where ShippingTime like '{year}%'";
            }
            sql += " and HoodType='Hood' group by Model order by TotalModuleNo desc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new ChartData()
                {
                    Text = objReader["Model"].ToString(),
                    Value = Convert.ToDouble(objReader["TotalModuleNo"].ToString())
                });
            }
            objReader.Close();
            return list;
        }

        #endregion 查询年度普通烟罩数量




        /// <summary>
        /// 查询单项目中烟罩数量
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DataSet GetScopeByDataSet(string projectId, string sbu)
        {
            string sql = $"select Model,sum(ModuleNo) as Num from DrawingPlan{sbu}";
            sql += $" where ProjectId={projectId}";
            sql += " group by Model";
            return SQLHelper.GetDataSet(sql);
        }



        /// <summary>
        /// 根据项目号UserId返回制图计划集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<DrawingPlan> GetDrawingPlanByUserId(string userId, string sbu)
        {
            return GetDrawingPlanByWhereSql($" where Projects{sbu}.UserId={userId}", sbu);
        }



        /// <summary>
        /// 根据项目号ODPNo返回制图计划集合
        /// </summary>
        /// <param name="odpNo"></param>
        /// <returns></returns>
        public List<DrawingPlan> GetDrawingPlanByODPNo(string odpNo, string sbu)
        {
            return GetDrawingPlanByWhereSql($" where ODPNo='{odpNo}'", sbu);
        }



        /// <summary>
        /// 根据项目Id返回制图计划集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<DrawingPlan> GetDrawingPlanByProjectId(string projectId, string sbu)
        {
            return GetDrawingPlanByWhereSql($" where DrawingPlan{sbu}.ProjectId={projectId}", sbu);
        }




        /// <summary>
        /// 根据单个条件返回制图计划集合
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public List<DrawingPlan> GetDrawingPlanByWhereSql(string whereSql, string sbu)
        {
            string sql = string.Format("select DrawingPlanId,UserAccount,DrawingPlan{0}.ProjectId,ODPNo,Item,Model,ModuleNo,DrawingPlan{0}.DrReleaseTarget,DrReleaseActual,SubTotalWorkload,DrawingPlan{0}.AddedDate,ProjectName from DrawingPlan{0}", sbu);
            sql += string.Format(" inner join Projects{0} on DrawingPlan{0}.ProjectId=Projects{0}.ProjectId", sbu);
            sql += $" inner join Users on Users.UserId=Projects{sbu}.UserId";
            sql += string.Format(" left join ProjectTracking{0} on DrawingPlan{0}.ProjectId=ProjectTracking{0}.ProjectId", sbu);
            sql += whereSql;
            sql += $" order by DrawingPlan{sbu}.DrReleasetarget desc";//按照计划发图日期，倒序排列
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
                    ProjectName = objReader["ProjectName"].ToString(),
                    Item = objReader["Item"].ToString(),
                    Model = objReader["Model"].ToString(),
                    ModuleNo = Convert.ToInt32(objReader["ModuleNo"]),
                    DrReleaseTarget = Convert.ToDateTime(objReader["DrReleaseTarget"]),
                    DrReleaseActual = objReader["DrReleaseActual"].ToString().Length == 0 ? Convert.ToDateTime("1/1/0001") : Convert.ToDateTime(objReader["DrReleaseActual"]),
                    SubTotalWorkload = Convert.ToDouble(objReader["SubTotalWorkload"]),
                    AddedDate = Convert.ToDateTime(objReader["AddedDate"]),
                    RemainingDays = (Convert.ToDateTime(objReader["DrReleaseTarget"]).Subtract(DateTime.Now).Days) < 0 ?
                        0 : (int)Convert.ToDateTime(objReader["DrReleaseTarget"]).Subtract(DateTime.Today).Days,
                    ProgressValue = Convert.ToDateTime(objReader["DrReleaseTarget"]).Subtract(Convert.ToDateTime(objReader["AddedDate"])).Days <= 0 ? 100 :
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
        public DrawingPlan GetDrawingPlanById(string drawingPlanId, string sbu)
        {
            string sql = string.Format("select DrawingPlanId,UserAccount,DrawingPlan{0}.ProjectId,ODPNo,Item,Model,ModuleNo,DrReleaseTarget,SubTotalWorkload,DrawingPlan{0}.AddedDate,ProjectName from DrawingPlan{0}", sbu);
            sql += string.Format(" inner join Projects{0} on DrawingPlan{0}.ProjectId=Projects{0}.ProjectId", sbu);
            sql += $" inner join Users on Users.UserId=Projects{sbu}.UserId";
            sql += $" where DrawingPlanId = {drawingPlanId}";
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
                    ProjectName = objReader["ProjectName"].ToString(),
                    Item = objReader["Item"].ToString(),
                    Model = objReader["Model"].ToString(),
                    ModuleNo = Convert.ToInt32(objReader["ModuleNo"]),
                    DrReleaseTarget = Convert.ToDateTime(objReader["DrReleaseTarget"]),
                    SubTotalWorkload = Convert.ToDouble(objReader["SubTotalWorkload"]),
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
        public int AddDraingPlan(DrawingPlan objDrawingPlan, string sbu)
        {
            string sql =
                $"insert into DrawingPlan{sbu} (ProjectId,Item,Model,ModuleNo,DrReleasetarget,SubTotalWorkload)";
            sql += " values({0},'{1}','{2}','{3}','{4}','{5}');select @@identity";
            sql = string.Format(sql, objDrawingPlan.ProjectId, objDrawingPlan.Item,
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
        /// 添加制图计划同时更新跟踪记录
        /// </summary>
        /// <param name="objDrawingPlan"></param>
        /// <param name="sbu"></param>
        /// <returns></returns>
        public bool AddDraingPlanAndUpdateTracking(DrawingPlan objDrawingPlan, string sbu)
        {
            string sql =
                $"insert into DrawingPlan{sbu} (ProjectId,Item,Model,ModuleNo,DrReleasetarget,SubTotalWorkload)";
            sql += " values({0},'{1}','{2}','{3}','{4}','{5}');select @@identity";
            sql = string.Format(sql, objDrawingPlan.ProjectId, objDrawingPlan.Item,
                objDrawingPlan.Model, objDrawingPlan.ModuleNo, objDrawingPlan.DrReleaseTarget, objDrawingPlan.SubTotalWorkload);
            List<string> sqlList = new List<string> { sql };
            string sqlTracking =
                $"update ProjectTracking{sbu} set ProjectStatusId=3 where ProjectId={objDrawingPlan.ProjectId}";
            sqlList.Add(sqlTracking);
            return SQLHelper.UpdateByTransaction(sqlList);
        }

        /// <summary>
        /// 修改制图计划记录
        /// </summary>
        /// <param name="objDrawingPlan"></param>
        /// <returns></returns>
        public int EditDrawingPlan(DrawingPlan objDrawingPlan, string sbu)
        {
            string sql = $"update DrawingPlan{sbu}";
            sql += " set ProjectId={0},Item='{1}',Model='{2}',ModuleNo='{3}',";
            sql += "DrReleasetarget='{4}',SubTotalWorkload='{5}',AddedDate='{6}' where DrawingPlanId={7}";
            sql = string.Format(sql, objDrawingPlan.ProjectId, objDrawingPlan.Item, objDrawingPlan.Model, objDrawingPlan.ModuleNo,
                  objDrawingPlan.DrReleaseTarget, objDrawingPlan.SubTotalWorkload, objDrawingPlan.AddedDate, objDrawingPlan.DrawingPlanId);
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
        public int DeleteDrawingPlan(string drawingPlanId, string sbu)
        {
            string sql = $"delete from DrawingPlan{sbu}";
            sql += " where DrawingPlanId={0}";
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
