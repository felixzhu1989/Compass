using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 项目类型
    /// </summary>
    [Serializable]
    public class ProjectType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string KMLink { get; set; }
    }
}
