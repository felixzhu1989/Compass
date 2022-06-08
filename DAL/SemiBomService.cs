using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SemiBomService
    {
        /// <summary>
        /// 根据projectId返回List合集
        /// </summary>        
        public List<SemiBom> GetSemiBomsByProjectId(string projectId)
        {
            return GetSemiBomsByWhereSql($" where ProjectId = '{projectId}'");
        }
        /// <summary>
        /// 根据条件返回SemiBom集合
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public List<SemiBom> GetSemiBomsByWhereSql(string whereSql)
        {
            string sql = "select ProjectId,DrawingNum,DrawingDesc,Quantity from SemiBom";
            sql += " inner join DrawingNumMatrix on DrawingNumMatrix.DrawingNum=SemiBom.DrawingNum";
            sql += whereSql;
            sql += " order by DrawingNum";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<SemiBom> list = new List<SemiBom>();
            while (objReader.Read())
            {
                list.Add(new SemiBom()
                {
                    ProjectId= Convert.ToInt32(objReader["ProjectId"]),                    
                    DrawingNum= objReader["DrawingNum"].ToString(),
                    DrawingDesc = objReader["DrawingDesc"].ToString(),                   
                    Quantity = Convert.ToInt32(objReader["Quantity"]),
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 导出SemiBom后批量插入数据到SQL
        /// </summary>
        /// <param name="SemiBoms"></param>
        /// <returns></returns>
        public bool ImportSemiBom(List<SemiBom> SemiBoms)
        {
            //编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder("insert into SemiBom (ProjectId,DrawingNum，Quantity) values({0},'{1}',{2})");
            List<string> sqlList = new List<string>();//用来保存生成的多条SQL语句
            //解析对象
            foreach (SemiBom objSemiBom in SemiBoms)
            {
                string sql = string.Format(sqlBuilder.ToString(), objSemiBom.ProjectId, objSemiBom.DrawingNum,                    objSemiBom.Quantity);
                //将解析的SQL语句添加到集合
                sqlList.Add(sql);
            }
            //将SQL语句集合提交到数据库
            return SQLHelper.UpdateByTransaction(sqlList);
        }
        /// <summary>
        /// 批量删除SemiBom
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public bool DeleteCutlistByTran(List<int> idList)
        {
            //编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder("delete from SemiBom where SemiBomId={0}");
            List<string> sqlList = new List<string>();//用来保存生成的多条SQL语句
            //解析对象
            foreach (int SemiBomId in idList)
            {
                string sql = string.Format(sqlBuilder.ToString(), SemiBomId);
                //将解析的SQL语句添加到集合
                sqlList.Add(sql);
            }
            //将SQL语句集合提交到数据库
            return SQLHelper.UpdateByTransaction(sqlList);
        }
    }
}
