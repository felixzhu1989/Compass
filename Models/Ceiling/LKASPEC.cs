using System;

namespace Models
{
    [Serializable]
   public class LKASPEC:IModel
    {
        public int LKASPECId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public decimal Length { get; set; }
        public decimal Height { get; set; }
        public string SidePanel { get; set; }
        public string LightType { get; set; }
        public string Japan { get; set; }
    }
}
