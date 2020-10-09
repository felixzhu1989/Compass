using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
