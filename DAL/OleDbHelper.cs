using System;
using System.Data;
using System.Data.OleDb;

namespace DAL
{
    /// <summary>
    /// 访问Access数据库的通用类
    /// </summary>
    public class OleDbHelper
    {
        //创建连接字符串(适合Excel)
        private static string connString =
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;";
        
        /// <summary>
        /// 删除修改
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Update(string sql)
        {
            OleDbConnection conn=new OleDbConnection(connString);
            OleDbCommand cmd=new OleDbCommand(sql);
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //写入日志
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 返回单一结果查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            OleDbConnection conn = new OleDbConnection(connString);
            OleDbCommand cmd = new OleDbCommand(sql);
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //写入日志
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 返回结果集查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static OleDbDataReader GetReader(string sql)
        {
            OleDbConnection conn = new OleDbConnection(connString);
            OleDbCommand cmd = new OleDbCommand(sql);
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                //写入日志
                throw ex;
            }
        }
        /// <summary>
        /// 返回数据集查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql)
        {
            OleDbConnection conn = new OleDbConnection(connString);
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            OleDbDataAdapter da=new OleDbDataAdapter(cmd);//创建数据适配器对象
            DataSet ds=new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                //写入日志
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 重载返回数据集查询方法,将指定路径的excel导入到dataset中
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql,string path)
        {
            OleDbConnection conn = new OleDbConnection(string.Format(connString,path));
            OleDbCommand cmd = new OleDbCommand(sql,conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);//创建数据适配器对象
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                //写入日志
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
