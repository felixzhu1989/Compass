using System;

namespace Models
{
    /// <summary>
    /// 用户分组
    /// </summary>
    [Serializable]
    public class UserGroup
    {
        public int UserGroupId { get; set; }
        public string GroupName { get; set; }
    }
}
