using System;
using System.Collections.Generic;
using Models;
using System.Data.SqlClient;

namespace DAL
{
    public class CustomerService
    {
        /// <summary>
        /// 返回所有对象
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetAllCustomers()
        {
            string sql = "select CustomerId,CustomerName from Customers";
            sql += " order by CustomerName";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<Customer> list = new List<Customer>();
            while (objReader.Read())
            {
                list.Add(new Customer()
                {
                    CustomerId = Convert.ToInt32(objReader["CustomerId"]),
                    CustomerName = objReader["CustomerName"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据Id返回对象
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public Customer GetCustomerById(string customerId)
        {
            string sql = "select CustomerId,CustomerName from Customers";
            sql += string.Format(" where CustomerId={0}", customerId);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            Customer objCustomer = null;
            if (objReader.Read())
            {
                objCustomer = new Customer()
                {
                    CustomerId = Convert.ToInt32(objReader["CustomerId"]),
                    CustomerName = objReader["CustomerName"].ToString()
                };
            }
            objReader.Close();
            return objCustomer;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="objCustomer"></param>
        /// <returns></returns>
        public int AddCustomer(Customer objCustomer)
        {
            string sql = "insert into Customers (CustomerName) values('{0}');select @@identity";
            sql = string.Format(sql, objCustomer.CustomerName);
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    throw new Exception("客户名称重复,不能添加重复的客户");
                }
                else
                {
                    throw new Exception("添加项目库时数据库访问异常" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="objCustomer"></param>
        /// <returns></returns>
        public int EditProjectValult(Customer objCustomer)
        {
            string sql = "update Customers set CustomerName='{0}' where CustomerId={1}";
            sql = string.Format(sql, objCustomer.CustomerName, objCustomer.CustomerId);
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
        /// 删除
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public int DeleteCustomer(string CustomerId)
        {
            string sql = "delete from Customers where CustomerId={0}";
            sql = string.Format(sql, CustomerId);
            try
            {
                return SQLHelper.Update(sql);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new Exception("该客户已被其他数据表关联，不能直接删除");
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
