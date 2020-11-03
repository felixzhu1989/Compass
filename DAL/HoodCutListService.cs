using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class HoodCutListService
    {
        /// <summary>
        /// 根据ModuleTreeId返回List合集
        /// </summary>
        /// <param name="moduleTreeId"></param>
        /// <returns></returns>
        public List<HoodCutList> GetHoodCutListsByModuleTreeId(string moduleTreeId)
        {
            return GetHoodCutListsByWhereSql(string.Format(" where ModuleTreeId = '{0}'", moduleTreeId));
        }
        /// <summary>
        /// 根据条件返回Cutlist集合
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public List<HoodCutList> GetHoodCutListsByWhereSql(string whereSql)
        {
            string sql = "select CutListId,ModuleTreeId,PartDescription,Length,Width,Thickness,Quantity,Materials,PartNo,AddedDate,HoodCutList.UserId,UserAccount from HoodCutList";
            sql += " inner join Users on Users.UserId=HoodCutList.UserId";
            sql += whereSql;
            sql += " order by Thickness desc,Materials desc,PartNo asc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<HoodCutList> list=new List<HoodCutList>();
            while (objReader.Read())
            {
                list.Add(new HoodCutList()
                {
                    CutListId= Convert.ToInt32(objReader["CutListId"]),
                    ModuleTreeId= Convert.ToInt32(objReader["ModuleTreeId"]),
                    PartDescription= objReader["PartDescription"].ToString(),
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
        /// <param name="hoodCutLists"></param>
        /// <returns></returns>
        public bool ImportCutList(List<HoodCutList> hoodCutLists)
        {
            //编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder("insert into HoodCutList (ModuleTreeId,PartDescription,Length,Width,Thickness,Quantity,Materials,PartNo,UserId) values({0},'{1}','{2}','{3}','{4}',{5},'{6}','{7}',{8})");
            List<string> sqlList = new List<string>();//用来保存生成的多条SQL语句
            //解析对象
            foreach (HoodCutList objCutList in hoodCutLists)
            {
                string sql = string.Format(sqlBuilder.ToString(), objCutList.ModuleTreeId,objCutList.PartDescription,
                    objCutList.Length,objCutList.Width,objCutList.Thickness,objCutList.Quantity,
                    objCutList.Materials,objCutList.PartNo,objCutList.UserId);
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
            StringBuilder sqlBuilder = new StringBuilder("delete from HoodCutList where CutListId={0}");
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
