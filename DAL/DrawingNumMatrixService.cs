using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        /// <summary>
        /// 查询所有图号集合
        /// </summary>
        /// <returns></returns>
        public List<DrawingNumMatrix> GetAllDrawingNum()
        {
            string sql =
                "select DrawingId,DrawingNum,DrawingDesc,DrawingType,Mark,DrawingNumMatrix.UserId,UserAccount,AddedDate from DrawingNumMatrix";
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
                    AddedDate = Convert.ToDateTime(objReader["AddedDate"])
                    
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 添加图号
        /// </summary>
        /// <param name="objDrawingNum"></param>
        /// <returns></returns>
        public string AddDrawingNum(DrawingNumMatrix objDrawingNum)
        {
            string sql = "insert into DrawingNumMatrix (DrawingNum,DrawingDesc,DrawingType,Mark,UserId) values('{0}','{1}','{2}','{3}','{4}')";
            sql = string.Format(sql, objDrawingNum.DrawingNum, objDrawingNum.DrawingDesc, objDrawingNum.DrawingType, objDrawingNum.Mark, objDrawingNum.UserId);
            try
            {
                SQLHelper.GetSingleResult(sql);
                return "success";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    throw new Exception("图号重复,不能添加重复的图号");
                }
                else
                {
                    throw new Exception("添加图号时数据库访问异常" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据Id查询图号信息
        /// </summary>
        /// <param name="drawingId"></param>
        /// <returns></returns>
        public DrawingNumMatrix GetDrawingNumById(string drawingId)
        {
            string sql ="select DrawingId,DrawingNum,DrawingDesc,DrawingType,Mark from DrawingNumMatrix";
            sql += string.Format(" where DrawingId={0}", drawingId);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            DrawingNumMatrix objDrawingNum = null;
            if (objReader.Read())
            {
                objDrawingNum = new DrawingNumMatrix()
                {
                    DrawingId = Convert.ToInt32(objReader["DrawingId"]),
                    DrawingNum = objReader["DrawingNum"].ToString(),
                    DrawingDesc = objReader["DrawingDesc"].ToString(),
                    DrawingType = objReader["DrawingType"].ToString(),
                    Mark = objReader["Mark"].ToString()
                };
            }
            objReader.Close();
            return objDrawingNum;
        }
        /// <summary>
        /// 修改图号
        /// </summary>
        /// <param name="objDrawingNum"></param>
        /// <returns></returns>
        public int EditDrawingNum(DrawingNumMatrix objDrawingNum)
        {
            string sql = "update DrawingNumMatrix set DrawingNum='{0}',DrawingDesc='{1}',DrawingType='{2}',Mark='{3}',UserId='{4}' where DrawingId={5}";
            sql = string.Format(sql, objDrawingNum.DrawingNum, objDrawingNum.DrawingDesc, objDrawingNum.DrawingType, objDrawingNum.Mark, objDrawingNum.UserId,objDrawingNum.DrawingId);
            try
            {
                return SQLHelper.Update(sql);
            }
            catch (SqlException ex)
            {

                throw new Exception("数据库操作异常" + ex.Message);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据Id查询图片
        /// </summary>
        /// <param name="drawingId"></param>
        /// <returns></returns>
        public string GetDrawingImage(string drawingId)
        {
            string sql = "select DrawingImage from DrawingNumMatrix where DrawingId={0}";
            sql = string.Format(sql, drawingId);
            return SQLHelper.GetSingleResult(sql).ToString();
        }

        /// <summary>
        /// 只更新图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="drawingId"></param>
        /// <returns></returns>
        public int RefreshImage(string image,string drawingId)
        {
            string sql = "update DrawingNumMatrix set DrawingImage='{0}' where DrawingId={1}";
            sql = string.Format(sql, image, drawingId);
            try
            {
                return SQLHelper.Update(sql);
            }
            catch (SqlException ex)
            {

                throw new Exception("数据库操作异常" + ex.Message);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// 删除图号
        /// </summary>
        /// <param name="drawingId"></param>
        /// <returns></returns>
        public int DeleteDrawingNum(string drawingId)
        {
            string sql = "delete from DrawingNumMatrix";
            sql += " where DrawingId={0}";
            sql = string.Format(sql, drawingId);
            try
            {
                return SQLHelper.Update(sql);
            }
            catch (SqlException ex)
            {
                throw new Exception("数据库操作异常，不能执行删除：" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
