using System;

namespace Models
{
    [Serializable]
   public class SSPTBD : IModel
    {
        public int SSPTBDId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本尺寸
        public double Length { get; set; }
        public string LeftType { get; set; }
        public string RightType { get; set; }
        public double LeftLength { get; set; }
        public double RightLength { get; set; }
        public int MPanelNo { get; set; }
        public string LightType { get; set; }//LED60
    }
}
