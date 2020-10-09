using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;

namespace DAL
{
    /// <summary>
    /// 从Excel中导入数据
    /// </summary>
   public class ImportDataFormExcel
    {
        #region 导入客户名称CustomerName
        /// <summary>
        /// 将选择的Excel数据表查询后封装成对象集合
        /// </summary>
        /// <param name="path">文件的路径</param>
        /// <returns></returns>
        public List<Customer> GetCustomersByExcel(string path)
        {
            List<Customer> list=new List<Customer>();
            DataSet ds = OleDbHelper.GetDataSet("select * from [Sheet1$]", path);//Sheet1是excel文件中的工作簿,用dataset接收整张表
            DataTable dt = ds.Tables[0];//第一张表存入datatable
            foreach (DataRow row in dt.Rows)//遍历datatable,封装对象集合，遍历行然后取一列的值
            {
                list.Add(new Customer()
                {
                    CustomerName =row["CustomerName"].ToString()
                });
            }
            return list;
        }
        /// <summary>
        /// 生成SQL语句集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool ImportCustomer(List<Customer> list)
        {
            //编写SQL语句
            StringBuilder sqlBuilder =new StringBuilder("insert into Customers (CustomerName) values('{0}')"); 
            List<string> sqlList=new List<string>();//用来保存生成的多条SQL语句
            //解析对象
            foreach (Customer objCustomer in list)
            {
                string sql = string.Format(sqlBuilder.ToString(), objCustomer.CustomerName);
                //将解析的SQL语句添加到集合
                sqlList.Add(sql);
            }
            //将SQL语句集合提交到数据库
            return SQLHelper.UpdateByTransaction(sqlList);
        }
        #endregion
    }
}
