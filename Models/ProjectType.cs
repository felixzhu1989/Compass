using System;

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
