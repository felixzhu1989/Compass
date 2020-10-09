using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
   public class SSPTSD : IModel
    {
        public int SSPTSDId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本尺寸
        public decimal Length { get; set; }
        public string LeftType { get; set; }
        public string RightType { get; set; }
        public decimal LeftLength { get; set; }
        public decimal RightLength { get; set; }
        public int MPanelNo { get; set; }
        public string LightType { get; set; }//LED60
    }
}
