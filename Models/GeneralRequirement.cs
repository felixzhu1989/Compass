using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 项目通用技术要求
    /// </summary>
    [Serializable]
    public class GeneralRequirement
    {
        public int GeneralRequirementId { get; set; }
        public int ProjectId { get; set; }
        public int TypeId { get; set; }
        public string InputPower { get; set; }
        public string MARVEL { get; set; }
        public string ANSULPrePipe { get; set; }
        public string ANSULSystem { get; set; }
        public int RiskLevel { get; set; }//风险等级
        public string MainAssyPath { get; set; }//天花烟罩总装配地址
        //简单扩展
        public string ODPNo { get; set; }
        public string TypeName { get; set; }
    }
}
