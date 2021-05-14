using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    /// <summary>
    /// 直接拷贝的DXF模版
    ///
    /// </summary>
    public class DXFCutListService
    {
        /// <summary>
        /// 根据categoryId返回List合集
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<DXFCutList> GetDXFCutListsByCategoryId(string categoryId)
        {
            return GetDXFCutListsByWhereSql(string.Format(" where CategoryId = {0}", categoryId));
        }
        /// <summary>
        /// 根据条件返回Cutlist集合
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public List<DXFCutList> GetDXFCutListsByWhereSql(string whereSql)
        {
            string sql = "select CutListId,CategoryId,PartDescription,Length,Width,Thickness,Quantity,Materials,PartNo from DXFCutList";
            sql += whereSql;
            sql += " order by Thickness desc,Materials desc,Length desc,PartNo asc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<DXFCutList> list = new List<DXFCutList>();
            while (objReader.Read())
            {
                list.Add(new DXFCutList()
                {
                    CutListId = Convert.ToInt32(objReader["CutListId"]),
                    CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                    PartDescription = objReader["PartDescription"].ToString(),
                    Length = Convert.ToDecimal(objReader["Length"]),
                    Width = Convert.ToDecimal(objReader["Width"]),
                    Thickness = Convert.ToDecimal(objReader["Thickness"]),
                    Quantity = Convert.ToInt32(objReader["Quantity"]),
                    Materials = objReader["Materials"].ToString(),
                    PartNo = objReader["PartNo"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据序号返回单个cutlist对象
        /// </summary>
        /// <param name="cutListId"></param>
        /// <returns></returns>
        public DXFCutList GetDXFCutListById(string cutListId)
        {
            return GetDXFCutListByWhereSql(string.Format(" where CutListId={0}", cutListId));
        }
        /// <summary>
        /// 根据条件返回单个Cutlist对象
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public DXFCutList GetDXFCutListByWhereSql(string whereSql)
        {
            string sql = "select CutListId,CategoryId,PartDescription,Length,Width,Thickness,Quantity,Materials,PartNo from DXFCutList";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            DXFCutList objDXFCutList = null;
            if (objReader.Read())
            {
                objDXFCutList = new DXFCutList()
                {
                    CutListId = Convert.ToInt32(objReader["CutListId"]),
                    CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                    PartDescription = objReader["PartDescription"].ToString(),
                    Length = Convert.ToDecimal(objReader["Length"].ToString()) ,
                    Width = Convert.ToDecimal(objReader["Width"].ToString()),
                    Thickness = Convert.ToDecimal(objReader["Thickness"].ToString()),
                    Quantity = Convert.ToInt32(objReader["Quantity"]),
                    Materials= objReader["Materials"].ToString(),
                    PartNo = objReader["PartNo"].ToString()
                };
            }
            objReader.Close();
            return objDXFCutList;
        }
        /// <summary>
        /// 添加配件信息
        /// </summary>
        /// <param name="objDXFCutList"></param>
        /// <returns></returns>
        public bool AddDXFCutList(DXFCutList objDXFCutList)
        {
            string sql = "insert into DXFCutList (CategoryId,PartDescription,Length,Width,Thickness,Quantity,Materials,PartNo)";
            sql += " values({0},'{1}','{2}','{3}','{4}',{5},'{6}','{7}')";
            sql = string.Format(sql, objDXFCutList.CategoryId, objDXFCutList.PartDescription, objDXFCutList.Length, objDXFCutList.Width,
                 objDXFCutList.Thickness, objDXFCutList.Quantity, objDXFCutList.Materials, objDXFCutList.PartNo);
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
        /// <param name="objDXFCutList"></param>
        /// <returns></returns>
        public int EditDXFCutList(DXFCutList objDXFCutList)
        {
            string sql = "update DXFCutList set CategoryId={0},PartDescription='{1}',Length='{2}',Width='{3}',";
            sql += "Thickness='{4}',Quantity={5},Materials='{6}',PartNo='{7}' where CutListId={8}";
            sql = string.Format(sql, objDXFCutList.CategoryId, objDXFCutList.PartDescription, objDXFCutList.Length, objDXFCutList.Width,
                objDXFCutList.Thickness, objDXFCutList.Quantity, objDXFCutList.Materials, objDXFCutList.PartNo,objDXFCutList.CutListId);
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
        /// <param name="cutListId"></param>
        /// <returns></returns>
        public int DeleteDXFCutList(string cutListId)
        {
            string sql = "delete from DXFCutList where CutListId={0}";
            sql = string.Format(sql, cutListId);
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

        /// <summary>
        /// 读取excel后批量插入数据到SQL
        /// </summary>
        /// <param name="DXFCutLists"></param>
        /// <returns></returns>
        public bool ImportCutList(List<DXFCutList> DXFCutLists)
        {
            //编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder("insert into DXFCutList (CategoryId,PartDescription,Length,Width,Thickness,Quantity,Materials,PartNo) values({0},'{1}','{2}','{3}','{4}',{5},'{6}','{7}')");
            List<string> sqlList = new List<string>();//用来保存生成的多条SQL语句
            //解析对象
            foreach (DXFCutList objCutList in DXFCutLists)
            {
                string sql = string.Format(sqlBuilder.ToString(), objCutList.CategoryId, objCutList.PartDescription,
                    objCutList.Length, objCutList.Width, objCutList.Thickness, objCutList.Quantity,
                    objCutList.Materials, objCutList.PartNo);
                //将解析的SQL语句添加到集合
                sqlList.Add(sql);
            }
            //将SQL语句集合提交到数据库
            return SQLHelper.UpdateByTransaction(sqlList);
        }
        /// <summary>
        /// 批量删除Cutlist
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool DeleteCutlistByTran(List<int> idList)
        {
            //编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder("delete from DXFCutList where CutListId={0}");
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

    }
}
