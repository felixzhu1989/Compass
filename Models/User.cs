using System;

namespace Models
{
    /// <summary>
    /// 用户
    /// </summary>
    [Serializable]
    public class User
    {
        public int UserId { get; set; }
        public int UserGroupId { get; set; }
        public string UserAccount { get; set; }
        public string UserPwd { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        //简单扩展内容
        public string GroupName { get; set; }
        public string SBU { get; set; }//事业部
    }
}
