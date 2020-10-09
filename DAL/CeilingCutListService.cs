using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
   public class CeilingCutListService
    {
        /// <summary>
        /// 根据SubAssyId返回List合集
        /// </summary>
        /// <param name="subAssyId"></param>
        /// <returns></returns>
        public List<CeilingCutList> GetCeilingCutListsBySubAssyId(string subAssyId)
        {
            return GetCeilingCutListsByWhereSql(string.Format(" where SubAssyId = '{0}'", subAssyId));
        }
        /// <summary>
        /// 根据条件返回Cutlist集合
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public List<CeilingCutList> GetCeilingCutListsByWhereSql(string whereSql)
        {
            string sql = "select CutListId,SubAssyId,PartDescription,Length,Width,Thickness,Quantity,Materials,PartNo,AddedDate,CeilingCutList.UserId,UserAccount from CeilingCutList";
            sql += " inner join Users on Users.UserId=CeilingCutList.UserId";
            sql += whereSql;
            sql += " order by Thickness desc,Materials desc,Length desc,PartNo asc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<CeilingCutList> list = new List<CeilingCutList>();
            while (objReader.Read())
            {
                list.Add(new CeilingCutList()
                {
                    CutListId = Convert.ToInt32(objReader["CutListId"]),
                    SubAssyId = Convert.ToInt32(objReader["SubAssyId"]),
                    PartDescription = objReader["PartDescription"].ToString(),
                    Length = Convert.ToDecimal(objReader["Length"]),
                    Width = Convert.ToDecimal(objReader["Width"]),
                    Thickness = Convert.ToDecimal(objReader["Thickness"]),
                    Quantity = Convert.ToInt32(objReader["Quantity"]),
                    Materials = objReader["Materials"].ToString(),
                    PartNo = objReader["PartNo"].ToString(),
                    AddedDate = Convert.ToDateTime(objReader["AddedDate"]),
                    UserId = Convert.ToInt32(objReader["UserId"]),
                    UserAccount = objReader["UserAccount"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 导出dxf后批量插入数据到SQL
        /// </summary>
        /// <param name="ceilingCutLists"></param>
        /// <returns></returns>
        public bool ImportCutList(List<CeilingCutList> ceilingCutLists)
        {
            //编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder("insert into CeilingCutList (SubAssyId,PartDescription,Length,Width,Thickness,Quantity,Materials,PartNo,UserId) values({0},'{1}','{2}','{3}','{4}',{5},'{6}','{7}',{8})");
            List<string> sqlList = new List<string>();//用来保存生成的多条SQL语句
            //解析对象
            foreach (CeilingCutList objCutList in ceilingCutLists)
            {
                string sql = string.Format(sqlBuilder.ToString(), objCutList.SubAssyId, objCutList.PartDescription,
                    objCutList.Length, objCutList.Width, objCutList.Thickness, objCutList.Quantity,
                    objCutList.Materials, objCutList.PartNo, objCutList.UserId);
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
            StringBuilder sqlBuilder = new StringBuilder("delete from CeilingCutList where CutListId={0}");
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
