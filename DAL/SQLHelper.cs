using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Common;

namespace DAL
{
    /// <summary>
    /// 通用数据访问层
    /// </summary>
    public class SQLHelper
    {
        private static readonly string ConnString = StringSecurity.DESDecrypt(ConfigurationManager.ConnectionStrings["connString"].ToString());
        /// <summary>
        /// 返回单一结果查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>对象</returns>
        public static object GetSingleResult(string sql)
        {
            SqlConnection conn=new SqlConnection(ConnString);
            SqlCommand cmd=new SqlCommand(sql,conn);
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 更新操作，增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>受影响的行数</returns>
        public static int Update(string sql)
        {
            SqlConnection conn=new SqlConnection(ConnString);
            SqlCommand cmd =new SqlCommand(sql,conn);
            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 返回一个结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection conn=new SqlConnection(ConnString);
            SqlCommand cmd =new SqlCommand(sql,conn);
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                conn.Close();
                throw ex;
            }
        }
        /// <summary>
        /// 返回数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql)
        {
            SqlConnection conn=new SqlConnection(ConnString);
            SqlCommand cmd =new SqlCommand(sql,conn);
            SqlDataAdapter da=new SqlDataAdapter(cmd);
            DataSet ds=new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 获取数据库服务器时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetServerTime()
        {
            return Convert.ToDateTime(GetSingleResult("select getdate()"));
        }


        /// <summary>
        /// 启用事务执行多条SQL语句
        /// </summary>
        /// <param name="sqlList">SQL语句集合</param>
        /// <returns></returns>
        public static bool UpdateByTransaction(List<string> sqlList)
        {
            SqlConnection conn=new SqlConnection(ConnString);
            SqlCommand cmd = new SqlCommand{Connection = conn};
            try
            {
                conn.Open();
                cmd.Transaction = conn.BeginTransaction(); //开启事务
                //遍历SQL语句
                foreach (string itemSql in sqlList)
                {
                    cmd.CommandText = itemSql; //赋值sql语句
                    cmd.ExecuteNonQuery(); //同时提交，这是在连接没有关闭的情况下提交的，效率更高
                    //这里并没有真的执行提交，只是做了一个假象
                }
                cmd.Transaction.Commit(); //提交事务，这里才是真的保存到数据库
                return true;
            }
            catch (SqlException ex)
            {
                if (cmd.Transaction != null) cmd.Transaction.Rollback(); //如果发生错误，回滚事务，什么数据都不保存
                //2627
                if (ex.Number == 2627)
                {
                    throw new Exception("数据重复,不能添加重复的信息");
                }
                else
                {
                    throw new Exception("数据库访问异常" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null) cmd.Transaction.Rollback(); //如果发生错误，回滚事务，什么数据都不保存
                throw new Exception("调用事务方法UpdateByTransaction(List<string> sqlList)时发生异常" + ex.Message);
            }
            finally
            {
                if (cmd.Transaction != null) cmd.Transaction = null;//清除事务
                conn.Close();
            }
        }

        #region 带参数SQL语句
        /// <summary>
        /// 更新操作，增删改，带参数SQL语句的重载Update方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int Update(string sql, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddRange(param);//添加参数数组
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 调用带参数的存储过程,增，删，改
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int UpdateByProcedure(string procedureName, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(ConnString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;//声明当前调用的是存储过程
                cmd.CommandText = procedureName;//存储过程名称
                cmd.Parameters.AddRange(param);//添加参数数组
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }


        #endregion
    }
}
