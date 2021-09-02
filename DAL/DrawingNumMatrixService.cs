using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class DrawingNumMatrixService
    {
        /// <summary>
        /// 查询图号生成规则
        /// </summary>
        /// <returns></returns>
        public List<DrawingNumCodeRule> GetCodeRules()
        {
            string sql = "select CodeId,ParentId,Code,CodeName from DrawingNumCodeRule order by ParentId,CodeId asc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<DrawingNumCodeRule> list = new List<DrawingNumCodeRule>();
            while (objReader.Read())
            {
                list.Add(new DrawingNumCodeRule()
                {
                    CodeId = Convert.ToInt32(objReader["CodeId"]),
                    ParentId = Convert.ToInt32(objReader["ParentId"]),
                    Code = Convert.ToChar(objReader["Code"]),
                    CodeName = objReader["CodeName"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        
        public List<DrawingNumMatrix> GetAllDrawingNum()
        {
            string sql =
                "select DrawingId,DrawingNum,DrawingDesc,DrawingType,Mark,DrawingNumMatrix.UserId,UserAccount,AddedDate,DrawingImage from DrawingNumMatrix";
            sql += " inner join Users on Users.UserId = DrawingNumMatrix.UserId";
            sql += " order by DrawingNum asc";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<DrawingNumMatrix> list = new List<DrawingNumMatrix>();
            while (objReader.Read())
            {
                list.Add(new DrawingNumMatrix()
                {
                    DrawingId = Convert.ToInt32(objReader["DrawingId"]),
                    DrawingNum = objReader["DrawingNum"].ToString(),
                    DrawingDesc = objReader["DrawingDesc"].ToString(),
                    DrawingType = objReader["DrawingType"].ToString(),
                    Mark = objReader["Mark"].ToString(),
                    UserId = Convert.ToInt32(objReader["UserId"]),
                    UserAccount = objReader["UserAccount"].ToString(),
                    AddedDate = Convert.ToDateTime(objReader["AddedDate"]),
                    DrawingImage = objReader["DrawingImage"].ToString()
                });
            }
            objReader.Close();
            return list;
        }


    }
}
