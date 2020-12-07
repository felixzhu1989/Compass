using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    //----------C#分页类的设计---------------
    //总原则：提取不变的，封装变化的。不变的作为方法体，变化的作为参数。
    //【变化的】
    //1.每页的显示条数
    //2.显示的字段（属性）
    //3.表的名称
    //4.查询条件
    //5.过滤字段（主键）
    //6.过滤掉的总数（计算得出）
    //7.当前显示的页码
    //8.排序条件
    //9.记录总数（查询的结果返回）
    //10.显示的总页数（查询结果返回后进一步计算得出）

    //【不变的】
    //查询语句的核心结构

    //【编写语句的核心结构】
    //该查询方法不需要直接的参数传递，参数获取全部通过对象的属性


    /// <summary>
    /// 通用数据分页类
    /// </summary>
    public class SqlDataPager
    {
        #region 一般属性
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 显示的字段
        /// </summary>
        public string FiledName { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        public string InnerJoin { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string Condition { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 排序条件
        /// </summary>
        public string Sort { get; set; }
        #endregion

        #region 只读属性
        /// <summary>
        /// 记录的总数【不能直接赋值】
        /// </summary>
        private int recordCount;
        public int RecordCount
        {
            get { return recordCount; }//设置只读属性，外面不能直接赋值
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get
            {
                //知道查询结果需要显示的页数=记录总数/每页显示条数+1（如果【记录总数%每页显示条数】取模后不为0【未除尽】，则+1）
                if (recordCount != 0)//查询记录总数不为0
                {
                    if (recordCount % PageSize != 0)
                    {
                        return recordCount / PageSize + 1;
                    }
                    else
                    {
                        return recordCount / PageSize;
                    }
                }
                else
                {
                    this.CurrentPage = 1;//查询结果为0时，当前页码需要复位。
                    return 0;
                }
            }
        }
        #endregion

        //分页查询方法
        /// <summary>
        /// 封装sql语句
        /// </summary>
        /// <returns></returns>
        private string GetPagedsql()
        {
            //计算需要过滤的总数
            string filterCount = (PageSize * (CurrentPage - 1)).ToString();
            //组合SQL语句
            string sql = "select Top {0} {1} from {2} {3} where {4} and {5} not in ";
            sql += "(select Top {6} {7} from {8} where {9} order by {10}) order by {11};";
            sql += "select count(*) from {12} where {13}";
            sql = string.Format(sql, PageSize, FiledName, TableName, InnerJoin, Condition, PrimaryKey,
                filterCount, PrimaryKey, TableName, Condition, Sort, Sort,
                TableName, Condition);
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetPagedData()
        {
            //【1】执行查询，返回分页后的结果
            DataSet ds = SQLHelper.GetDataSet(GetPagedsql());
            //【2】获取返回记录的总数
            this.recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return ds.Tables[0];
        }
    }
}
