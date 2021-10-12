using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Models;

namespace DAL
{
    public class CategoryService
    {
        /// <summary>
        /// 获取所有产品分类
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAllCategories(string sbu)
        {
            string sql = $"select CategoryId,ParentId,CategoryName,CategoryDesc,Model,SubType,ModelPath from Categories{sbu}";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<Category> list = new List<Category>();
            while (objReader.Read())
            {
                if (objReader["CategoryId"].Equals(1000)) continue;
                list.Add(new Category()
                {
                    CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                    ParentId = Convert.ToInt32(objReader["ParentId"]),
                    CategoryName = objReader["CategoryName"].ToString(),
                    CategoryDesc = objReader["CategoryDesc"].ToString(),
                    Model = objReader["Model"].ToString(),
                    SubType = objReader["SubType"].ToString(),
                    ModelPath = objReader["ModelPath"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 获取所有一级目录编号和描述
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategoryId(string sbu)
        {
            string sql = $"select CategoryId,CategoryDesc from Categories{sbu}";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<Category> list = new List<Category>();
            while (objReader.Read())
            {
                string str = objReader["CategoryId"].ToString();
                if (str.Substring(str.Length - 2) == "00")
                {
                    list.Add(new Category()
                    {
                        CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                        CategoryDesc = objReader["CategoryDesc"].ToString()
                    });
                }
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public string AddCategory(Category objCategory, string sbu)
        {
            string sql = $"insert into Categories{sbu}";
            sql += " (CategoryId,ParentId,CategoryName,CategoryDesc,Model,SubType,ModelImage,KMLink,ModelPath)";
            sql += " values({0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
            sql = string.Format(sql, objCategory.CategoryId, objCategory.ParentId, objCategory.CategoryName,objCategory.CategoryDesc, objCategory.Model, objCategory.SubType, objCategory.ModelImage,objCategory.KMLink, objCategory.ModelPath);
            try
            {
                SQLHelper.GetSingleResult(sql);
                return "success";
            }
            catch (SqlException ex)
            {
                throw new Exception("添加类型时发生数据库访问异常" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据父类编号获得类型集合
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<Category> GetCategoriesByParentId(string parentId, string sbu)
        {
            string sql = $"select CategoryId,ParentId,CategoryName,CategoryDesc,Model,SubType,ModelPath from Categories{sbu}";
            sql += $" where ParentId={parentId}";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<Category> list = new List<Category>();
            while (objReader.Read())
            {
                list.Add(new Category()
                {
                    CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                    ParentId = Convert.ToInt32(objReader["ParentId"]),
                    CategoryName = objReader["CategoryName"].ToString(),
                    CategoryDesc = objReader["CategoryDesc"].ToString(),
                    Model = objReader["Model"].ToString(),
                    SubType = objReader["SubType"].ToString(),
                    ModelPath = objReader["ModelPath"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据CategoryId返回category对象
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Category GetCategoryByCategoryId(string categoryId, string sbu)
        {
            string sql =
                $"select CategoryId,ParentId,CategoryName,CategoryDesc,Model,SubType,ModelImage,KMLink,ModelPath from Categories{sbu}";
            sql += $" where CategoryId={categoryId}";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            Category objCategory = null;
            if (objReader.Read())
            {
                objCategory = new Category()
                {
                    CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                    ParentId = Convert.ToInt32(objReader["ParentId"]),
                    CategoryName = objReader["CategoryName"].ToString(),
                    CategoryDesc = objReader["CategoryDesc"].ToString(),
                    Model = objReader["Model"].ToString(),
                    SubType = objReader["SubType"].ToString(),
                    ModelImage = objReader["ModelImage"].ToString(),
                    KMLink = objReader["KMLink"].ToString(),
                    ModelPath = objReader["ModelPath"].ToString()
                };
            }
            objReader.Close();
            return objCategory;
        }

        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public int EditCategory(Category objCategory, string sbu)
        {
            string sql =$"update Categories{sbu}";
            sql += " set ParentId={0},CategoryName='{1}',CategoryDesc='{2}',Model='{3}',SubType='{4}'";
            sql += ",ModelImage='{5}',KMLink='{6}',ModelPath='{7}' where CategoryId={8}";
            sql = string.Format(sql, objCategory.ParentId, objCategory.CategoryName, objCategory.CategoryDesc,
                objCategory.Model, objCategory.SubType, objCategory.ModelImage, objCategory.KMLink,
                objCategory.ModelPath, objCategory.CategoryId);
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
        /// 删除分类
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int DeleteCategory(string categoryId, string sbu)
        {
            string sql = $"delete from Categories{sbu}";
            sql += " where CategoryId ={0}";
            sql = string.Format(sql, categoryId);
            try
            {
                return SQLHelper.Update(sql);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new Exception("该分类已被其他分类关联，不能直接删除，请仔细检查");
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
