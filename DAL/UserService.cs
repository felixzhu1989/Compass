using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    /// <summary>
    /// 用户数据访问类
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// 根据用户名和密码登陆用户
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public User UserLogin(User objUser)
        {
            string sql = "select UserId,UserGroupId,Email,Contact from Users where UserAccount='{0}' and UserPwd='{1}'";
            sql = string.Format(sql, objUser.UserAccount, objUser.UserPwd);
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql);
                if (objReader.Read())
                {
                    objUser.UserId = Convert.ToInt32(objReader["UserId"]);
                    objUser.UserGroupId = Convert.ToInt32(objReader["UserGroupId"]);
                    objUser.Email = objReader["Email"].ToString();
                    objUser.Contact = objReader["Contact"].ToString();
                }
                else
                {
                    objUser = null;
                }
                objReader.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("数据库异常" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objUser;
        }
        /// <summary>
        /// 根据用户id返回用户集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<User> GetUserById(string id)
        {
            return GetUserByWhereSql(string.Format(" where UserId={0}", id));
        }
        /// <summary>
        /// 获取所有用户集合
        /// </summary>
        /// <returns></returns>
        public List<User> GetUserByWhereSql(string whereSql)
        {
            string sql = "select UserId,GroupName,UserAccount,UserPwd,Email,Contact from Users";
            sql += " inner join UserGroups on UserGroups.UserGroupId=Users.UserGroupId";
            sql += whereSql;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<User> list=new List<User>();
            while (objReader.Read())
            {
                list.Add(new User()
                {
                    UserId = Convert.ToInt32(objReader["UserId"]),
                    GroupName = objReader["GroupName"].ToString(),
                    UserAccount = objReader["UserAccount"].ToString(),
                    UserPwd = objReader["UserPwd"].ToString(),
                    Email = objReader["Email"].ToString(),
                    Contact = objReader["Contact"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 获取技术部人员
        /// </summary>
        /// <returns></returns>
        public List<User> GetUserTech()
        {
            string sql = "select UserId,GroupName,UserAccount from Users";
            sql += " inner join UserGroups on UserGroups.UserGroupId=Users.UserGroupId";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<User> list = new List<User>();
            while (objReader.Read())
            {
                if (objReader["GroupName"].ToString() == "admin" || objReader["GroupName"].ToString() == "tech_user")
                {
                    list.Add(new User()
                    {
                        UserId = Convert.ToInt32(objReader["UserId"]),
                        UserAccount = objReader["UserAccount"].ToString()
                    });
                }
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public int AddUser(User objUser)
        {
            string sql = "insert into Users (UserGroupId,UserAccount,UserPwd,Email,Contact)";
            sql += " values({0},'{1}','{2}','{3}','{4}');select @@identity";
            sql = string.Format(sql, objUser.UserGroupId, objUser.UserAccount, objUser.UserPwd, objUser.Email,
                objUser.Contact);
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            }
            catch (SqlException ex)
            {
                throw new Exception("添加用户时数据库访问异常"+ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据用户Id返回用户对象
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserByUserId(string userId)
        {
            string sql = "select UserId,UserGroupId,UserAccount,UserPwd,Email,Contact from Users where UserId={0}";
            sql = string.Format(sql, userId);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            User objUser = null;
            if (objReader.Read())
            {
                objUser=new User()
                {
                    UserId = Convert.ToInt32(objReader["UserId"]),
                    UserGroupId = Convert.ToInt32(objReader["UserGroupId"]),
                    UserAccount = objReader["UserAccount"].ToString(),
                    UserPwd = objReader["UserPwd"].ToString(),
                    Email = objReader["Email"].ToString(),
                    Contact = objReader["Contact"].ToString()
                };
            }
            objReader.Close();
            return objUser;
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public int EditUser(User objUser)
        {
            string sql = "update Users set UserGroupId={0},UserAccount='{1}',UserPwd='{2}',Email='{3}',Contact='{4}'";
            sql += " where UserId={5}";
            sql = string.Format(sql, objUser.UserGroupId, objUser.UserAccount, objUser.UserPwd, objUser.Email,
                objUser.Contact, objUser.UserId);
            try
            {
                return SQLHelper.Update(sql);
            }
            catch (SqlException ex)
            {
                throw new Exception("数据库操作出现异常："+ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int DeleteUser(string userId)
        {
            string sql = "delete from Users where UserId={0}";
            sql = string.Format(sql, userId);
            try
            {
                return SQLHelper.Update(sql);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    throw new Exception("该用户已被其他数据表关联，不能直接删除，你可以设置其分组为disable");
                }
                else
                {
                    throw new Exception("数据库操作异常，不能执行删除："+ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region 用户分组操作
        /// <summary>
        /// 获取所有用户分组
        /// </summary>
        /// <returns></returns>
        public List<UserGroup> GetAllGroups()
        {
            string sql = "select UserGroupId,GroupName from UserGroups";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<UserGroup> list = new List<UserGroup>();
            while (objReader.Read())
            {
                list.Add(new UserGroup()
                {
                    UserGroupId = Convert.ToInt32(objReader["UserGroupId"]),
                    GroupName = objReader["GroupName"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 添加用户分组
        /// </summary>
        /// <param name="objUserGroup"></param>
        /// <returns></returns>
        public int AddUserGoup(UserGroup objUserGroup)
        {
            string sql = "insert into UserGroups (GroupName) values('{0}');select @@identity";
            sql = string.Format(sql, objUserGroup.GroupName);
            try
            {
                return Convert.ToInt32(SQLHelper.GetSingleResult(sql));
            }
            catch (SqlException ex)
            {
                throw new Exception("添加用户分组时数据库访问异常" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
