using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 项目特殊要求
    /// </summary>
    [Serializable]
    public class SpecialRequirement
    {
        public int SpecialRequirementId { get; set; }
        public int ProjectId { get; set; }
        public string Content { get; set; }
        //简单扩展
        public string ODPNo { get; set; }

    }
}
