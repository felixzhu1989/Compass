using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL
{
    public class CeilingAccessoryService
    {
        #region 配件模板部分  
        /// <summary>
        /// 查询日本项目的所有配件
        /// </summary>
        /// <returns></returns>
        public List<CeilingAccessory> GetCeilingAccessoriesForJapan()
        {
            return GetCeilingAccessoriesByWhereSql(string.Format(" where ClassNo=1 or ClassNo=2"));
        }
        /// <summary>
        /// 查询非日本项目的所有配件
        /// </summary>
        /// <returns></returns>
        public List<CeilingAccessory> GetCeilingAccessoriesForNotJapan()
        {
            return GetCeilingAccessoriesByWhereSql(string.Format(" where ClassNo=0 or ClassNo=2"));
        }

        /// <summary>
        /// 根据where条件返回配件集合
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public List<CeilingAccessory> GetCeilingAccessoriesByWhereSql(string whereSql)
        {
            StringBuilder sql = new StringBuilder("select CeilingAccessoryId,ClassNo,PartDescription,Quantity,PartNo,Unit,Length,Width,Height,Material,Remark,CountingRule from CeilingAccessories");
            sql.Append(whereSql);
            sql.Append(" order by CeilingAccessoryId asc");//按照Id排列

            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            List<CeilingAccessory> list = new List<CeilingAccessory>();
            while (objReader.Read())
            {
                list.Add(new CeilingAccessory()
                {

                    CeilingAccessoryId = objReader["CeilingAccessoryId"].ToString(),
                    ClassNo = Convert.ToInt32(objReader["ClassNo"]),
                    PartDescription = objReader["PartDescription"].ToString(),
                    Quantity = Convert.ToInt32(objReader["Quantity"]),
                    PartNo = objReader["PartNo"].ToString(),
                    Unit = objReader["Unit"].ToString(),
                    Length = objReader["Length"].ToString(),
                    Width = objReader["Width"].ToString(),
                    Height = objReader["Height"].ToString(),
                    Material = objReader["Material"].ToString(),
                    Remark = objReader["Remark"].ToString(),
                    CountingRule = objReader["CountingRule"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据序号返回单个配件对象
        /// </summary>
        /// <param name="ceilingAccessoryId"></param>
        /// <returns></returns>
        public CeilingAccessory GetCeilingAccessoryById(string ceilingAccessoryId)
        {
            return GetCeilingAccessoryByWhereSql(string.Format(" where CeilingAccessoryId='{0}'", ceilingAccessoryId));
        }
        /// <summary>
        /// 根据部件编号返回配件1个条目
        /// </summary>
        /// <param name="partNo"></param>
        /// <returns></returns>
        public CeilingAccessory GetCeilingAccessoryByPartNo(string partNo)
        {
            return GetCeilingAccessoryByWhereSql(string.Format(" where PartNo='{0}'", partNo));
        }
        /// <summary>
        /// 根据条件返回单个配件对象
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public CeilingAccessory GetCeilingAccessoryByWhereSql(string whereSql)
        {
            string sql = "select CeilingAccessoryId,ClassNo,PartDescription,Quantity,PartNo,Unit,Length,Width,Height,Material,Remark,CountingRule from CeilingAccessories";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            CeilingAccessory objCeilingAccessory = null;
            if (objReader.Read())
            {
                objCeilingAccessory = new CeilingAccessory()
                {
                    CeilingAccessoryId = objReader["CeilingAccessoryId"].ToString(),
                    ClassNo = Convert.ToInt32(objReader["ClassNo"]),
                    PartDescription = objReader["PartDescription"].ToString(),
                    Quantity = Convert.ToInt32(objReader["Quantity"]),
                    PartNo = objReader["PartNo"].ToString(),
                    Unit = objReader["Unit"].ToString(),
                    Length = objReader["Length"].ToString(),
                    Width = objReader["Width"].ToString(),
                    Height = objReader["Height"].ToString(),
                    Material = objReader["Material"].ToString(),
                    Remark = objReader["Remark"].ToString(),
                    CountingRule = objReader["CountingRule"].ToString()
                };
            }
            objReader.Close();
            return objCeilingAccessory;
        }
        /// <summary>
        /// 添加配件信息
        /// </summary>
        /// <param name="objCeilingAccessory"></param>
        /// <returns></returns>
        public bool AddCeilingAccessory(CeilingAccessory objCeilingAccessory)
        {
            string sql = "insert into CeilingAccessories (CeilingAccessoryId,ClassNo,PartDescription,PartNo,Unit,Length,Width,Height,Material,Remark,CountingRule)";
            sql += " values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')";
            sql = string.Format(sql, objCeilingAccessory.CeilingAccessoryId, objCeilingAccessory.ClassNo, objCeilingAccessory.PartDescription, objCeilingAccessory.PartNo,
                objCeilingAccessory.Unit, objCeilingAccessory.Length, objCeilingAccessory.Width, objCeilingAccessory.Height, objCeilingAccessory.Material,
                objCeilingAccessory.Remark, objCeilingAccessory.CountingRule);
            try
            {
                SQLHelper.GetSingleResult(sql);
                return true;
            }
            catch (SqlException ex)
            {
                //2627
                if (ex.Number == 2627)
                {
                    throw new Exception("信息重复,不能添加重复的项目信息");
                }
                else
                {
                    throw new Exception("添加配件信息时数据库访问异常" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改配件信息
        /// </summary>
        /// <param name="objCeilingAccessory"></param>
        /// <returns></returns>
        public int EditCeilingAccessory(CeilingAccessory objCeilingAccessory)
        {
            string sql = "update CeilingAccessories set ClassNo={0},PartDescription='{1}',PartNo='{2}',Unit='{3}',";
            sql += "Length='{4}',Width='{5}',Height='{6}',Material='{7}',Remark='{8}',CountingRule='{9}' where CeilingAccessoryId='{10}'";
            sql = string.Format(sql, objCeilingAccessory.ClassNo, objCeilingAccessory.PartDescription, objCeilingAccessory.PartNo, objCeilingAccessory.Unit,
                objCeilingAccessory.Length, objCeilingAccessory.Width, objCeilingAccessory.Height, objCeilingAccessory.Material,
                objCeilingAccessory.Remark, objCeilingAccessory.CountingRule, objCeilingAccessory.CeilingAccessoryId);
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
        /// 删除配件信息
        /// </summary>
        /// <param name="CeilingAccessoryId"></param>
        /// <returns></returns>
        public int DeleteCeilingAccessory(string CeilingAccessoryId)
        {
            string sql = "delete from CeilingAccessories where CeilingAccessoryId='{0}'";
            sql = string.Format(sql, CeilingAccessoryId);
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

        #region 天花烟罩发货清单部分
        //CeilingPackingList





        /// <summary>
        /// 根据项目Id返回发货清单集合
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<CeilingAccessory> GetCeilingPackingListByProjectId(string projectId)
        {
            return GetCeilingPackingListByWhereSql(string.Format(" where ProjectId = {0}", projectId));
        }
        /// <summary>
        /// 根据where条件返回发货清单集合
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public List<CeilingAccessory> GetCeilingPackingListByWhereSql(string whereSql)
        {
            StringBuilder sql = new StringBuilder("select PartDescription,Quantity,PartNo,Unit,Length,Width,Height,Material,Remark,CountingRule,AddedDate,CeilingPackingList.UserId,UserAccount,ProjectId,CeilingAccessoryId,ClassNo,CeilingPackingListId,Location from CeilingPackingList");
            sql.Append(" inner join Users on Users.UserId=CeilingPackingList.UserId");
            sql.Append(whereSql);
            sql.Append(" order by Location asc, CeilingAccessoryId asc, PartNo asc");//优先按照区域排序，然后Id，再部件编号

            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            List<CeilingAccessory> list = new List<CeilingAccessory>();
            while (objReader.Read())
            {
                list.Add(new CeilingAccessory()
                {
                    CeilingPackingListId = Convert.ToInt32(objReader["CeilingPackingListId"]),
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    CeilingAccessoryId = objReader["CeilingAccessoryId"].ToString(),
                    ClassNo = Convert.ToInt32(objReader["ClassNo"]),
                    PartDescription = objReader["PartDescription"].ToString(),
                    Quantity = Convert.ToInt32(objReader["Quantity"]),
                    PartNo = objReader["PartNo"].ToString(),
                    Unit = objReader["Unit"].ToString(),
                    Length = objReader["Length"].ToString(),
                    Width = objReader["Width"].ToString(),
                    Height = objReader["Height"].ToString(),
                    Material = objReader["Material"].ToString(),
                    Remark = objReader["Remark"].ToString(),
                    CountingRule = objReader["CountingRule"].ToString(),
                    AddedDate = Convert.ToDateTime(objReader["AddedDate"]),
                    UserId = Convert.ToInt32(objReader["UserId"]),
                    UserAccount = objReader["UserAccount"].ToString(),
                    Location = objReader["Location"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据序号返回发货清单个条目
        /// </summary>
        /// <param name="packingListId"></param>
        /// <returns></returns>
        public CeilingAccessory GetCeilingPackingItemById(string packingListId)
        {
            return GetCeilingPackingItemByWhereSql(string.Format(" where CeilingPackingListId={0}", packingListId));
        }
        /// <summary>
        /// 根据where条件返回发货清单条目
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public CeilingAccessory GetCeilingPackingItemByWhereSql(string whereSql)
        {
            StringBuilder sql = new StringBuilder("select PartDescription,Quantity,PartNo,Unit,Length,Width,Height,Material,Remark,CountingRule,AddedDate,CeilingPackingList.UserId,UserAccount,ProjectId,CeilingAccessoryId,ClassNo,CeilingPackingListId,Location from CeilingPackingList");
            sql.Append(" inner join Users on Users.UserId=CeilingPackingList.UserId");
            sql.Append(whereSql);
            SqlDataReader objReader = SQLHelper.GetReader(sql.ToString());
            CeilingAccessory objCeilingAccessory = null;
            while (objReader.Read())
            {
                objCeilingAccessory = new CeilingAccessory()
                {
                    CeilingPackingListId = Convert.ToInt32(objReader["CeilingPackingListId"]),
                    ProjectId = Convert.ToInt32(objReader["ProjectId"]),
                    CeilingAccessoryId = objReader["CeilingAccessoryId"].ToString(),
                    ClassNo = Convert.ToInt32(objReader["ClassNo"]),
                    PartDescription = objReader["PartDescription"].ToString(),
                    Quantity = Convert.ToInt32(objReader["Quantity"]),
                    PartNo = objReader["PartNo"].ToString(),
                    Unit = objReader["Unit"].ToString(),
                    Length = objReader["Length"].ToString(),
                    Width = objReader["Width"].ToString(),
                    Height = objReader["Height"].ToString(),
                    Material = objReader["Material"].ToString(),
                    Remark = objReader["Remark"].ToString(),
                    CountingRule = objReader["CountingRule"].ToString(),
                    AddedDate = Convert.ToDateTime(objReader["AddedDate"]),
                    UserId = Convert.ToInt32(objReader["UserId"]),
                    UserAccount = objReader["UserAccount"].ToString(),
                    Location = objReader["Location"].ToString()
                };
            }
            objReader.Close();
            return objCeilingAccessory;
        }


        /// <summary>
        /// 修改发货清单
        /// </summary>
        /// <param name="objCeilingAccessory"></param>
        /// <returns></returns>
        public int EditCeilingPackingList(CeilingAccessory objCeilingAccessory)
        {
            //编写带参数的SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("Update CeilingPackingList set PartDescription=@PartDescription,PartNo=@PartNo,Quantity=@Quantity,");
            sqlBuilder.Append("Length=@Length,Width=@Width,Height=@Height,Remark=@Remark,Location=@Location where CeilingPackingListId=@CeilingPackingListId");
            //定义参数数组
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@PartDescription",objCeilingAccessory.PartDescription),
                new SqlParameter("@PartNo",objCeilingAccessory.PartNo),
                new SqlParameter("@Quantity",objCeilingAccessory.Quantity),
                new SqlParameter("@Length",objCeilingAccessory.Length),
                new SqlParameter("@Width",objCeilingAccessory.Width),
                new SqlParameter("@Height",objCeilingAccessory.Height),
                new SqlParameter("@Remark",objCeilingAccessory.Remark),
                new SqlParameter("@Location",objCeilingAccessory.Location),
                new SqlParameter("@CeilingPackingListId",objCeilingAccessory.CeilingPackingListId)
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
        /// <summary>
        /// 查询到配件后批量插入数据到SQL
        /// </summary>
        /// <param name="ceilingAccessory"></param>
        /// <returns></returns>
        public bool ImportCeilingPackingListByTran(List<CeilingAccessory> ceilingAccessory)
        {
            //编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder("insert into CeilingPackingList (ProjectId,CeilingAccessoryId,ClassNo,PartDescription,Quantity,PartNo,Unit,Length,Width,Height,Material,Remark,CountingRule,UserId,Location) values({0},'{1}',{2},'{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',{13},'{14}')");
            List<string> sqlList = new List<string>();//用来保存生成的多条SQL语句
            //解析对象
            foreach (CeilingAccessory objCeilingAccessory in ceilingAccessory)
            {
                string sql = string.Format(sqlBuilder.ToString(), objCeilingAccessory.ProjectId, objCeilingAccessory.CeilingAccessoryId,
                    objCeilingAccessory.ClassNo, objCeilingAccessory.PartDescription, objCeilingAccessory.Quantity, objCeilingAccessory.PartNo,
                    objCeilingAccessory.Unit, objCeilingAccessory.Length, objCeilingAccessory.Width, objCeilingAccessory.Height,
                    objCeilingAccessory.Material, objCeilingAccessory.Remark, objCeilingAccessory.CountingRule, objCeilingAccessory.UserId,
                    objCeilingAccessory.Location);
                //将解析的SQL语句添加到集合
                sqlList.Add(sql);
            }
            //将SQL语句集合提交到数据库
            return SQLHelper.UpdateByTransaction(sqlList);
        }
        /// <summary>
        /// 批量删除CeilingPackingList
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool DeleteCeilingPackingListByTran(List<int> idList)
        {
            //编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder("delete from CeilingPackingList where CeilingPackingListId={0}");
            List<string> sqlList = new List<string>();//用来保存生成的多条SQL语句
            //解析对象
            foreach (int cutListId in idList)
            {
                string sql = string.Format(sqlBuilder.ToString(), cutListId);
                //将解析的SQL语句添加到集合
                sqlList.Add(sql);
            }
            //将SQL语句集合提交到数据库
            return SQLHelper.UpdateByTransaction(sqlList);
        }

        #endregion



    }
}
