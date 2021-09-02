using System;
using System.Collections.Generic;
using System.Text;
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

        #region 导入dxfCutlist
        /// <summary>
        /// 将选择的Excel数据表查询后封装成对象集合
        /// </summary>
        /// <param name="path">文件的路径</param>
        /// <returns></returns>
        public List<DXFCutList> GetDXFCutListByExcel(string path)
        {
            List<DXFCutList> list = new List<DXFCutList>();
            DataSet ds = OleDbHelper.GetDataSet("select * from [Sheet1$]", path);//Sheet1是excel文件中的工作簿,用dataset接收整张表
            DataTable dt = ds.Tables[0];//第一张表存入datatable
            foreach (DataRow row in dt.Rows)//遍历datatable,封装对象集合，遍历行然后取一列的值
            {
                list.Add(new DXFCutList()
                {
                    PartDescription = row["Part Description"].ToString(),
                    Length =Convert.ToDecimal(row["Length"]),
                    Width = Convert.ToDecimal(row["Width"]),
                    Thickness = Convert.ToDecimal(row["Thickness MM"]),
                    Quantity = Convert.ToInt32(row["QUANTITY"]),
                    Materials = row["Material"].ToString(),
                    PartNo = row["Part No"].ToString()
                });
            }
            return list;
        }
        /// <summary>
        /// 生成SQL语句集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool ImportDXFCutList(List<DXFCutList> list)
        {
            //编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder("insert into DXFCutList (CategoryId,PartDescription,Length,Width,Thickness,Quantity,Materials,PartNo) values({0},'{1}','{2}','{3}','{4}',{5},'{6}','{7}')");
            List<string> sqlList = new List<string>();//用来保存生成的多条SQL语句
            //解析对象
            foreach (DXFCutList item in list)
            {
                string sql = string.Format(sqlBuilder.ToString(), item.CategoryId,item.PartDescription,item.Length,item.Width,item.Thickness,item.Quantity,item.Materials,item.PartNo);
                //将解析的SQL语句添加到集合
                sqlList.Add(sql);
            }
            //将SQL语句集合提交到数据库
            return SQLHelper.UpdateByTransaction(sqlList);
        }
        #endregion

        #region 导入图号
        /// <summary>
        /// 将选择的Excel数据表查询后封装成对象集合
        /// </summary>
        /// <param name="path">文件的路径</param>
        /// <returns></returns>
        public List<DrawingNumMatrix> GetDrawingNumByExcel(string path)
        {
            List<DrawingNumMatrix> list = new List<DrawingNumMatrix>();
            DataSet ds = OleDbHelper.GetDataSet("select * from [Sheet1$]", path);//Sheet1是excel文件中的工作簿,用dataset接收整张表
            DataTable dt = ds.Tables[0];//第一张表存入datatable
            foreach (DataRow row in dt.Rows)//遍历datatable,封装对象集合，遍历行然后取一列的值
            {
                list.Add(new DrawingNumMatrix()
                {
                    DrawingNum = row["DrawingNum"].ToString(),
                    DrawingDesc = row["DrawingDesc"].ToString(),
                    DrawingType = row["DrawingType"].ToString(),
                    UserId = Convert.ToInt32(row["UserId"]),
                    Mark = row["Mark"].ToString()
                });
            }
            return list;
        }
        /// <summary>
        /// 生成SQL语句集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool ImportDrawingNum(List<DrawingNumMatrix> list)
        {
            //编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder("insert into DrawingNumMatrix (DrawingNum,DrawingDesc,DrawingType,Mark,UserId) values('{0}','{1}','{2}','{3}',{4})");
            List<string> sqlList = new List<string>();//用来保存生成的多条SQL语句
            //解析对象
            foreach (DrawingNumMatrix item in list)
            {
                string sql = string.Format(sqlBuilder.ToString(), item.DrawingNum, item.DrawingDesc, item.DrawingType, item.Mark, item.UserId);
                //将解析的SQL语句添加到集合
                sqlList.Add(sql);
            }
            //将SQL语句集合提交到数据库
            return SQLHelper.UpdateByTransaction(sqlList);
        }

        #endregion

    }
}
