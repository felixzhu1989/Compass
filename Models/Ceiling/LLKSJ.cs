using System;

namespace Models
{
    [Serializable]
   public class LLKSJ:IModel
    {
        public int LLKSJId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        public int LongGlassNo { get; set; }
        public int ShortGlassNo { get; set; }
        public double LeftLength { get; set; }
        public double RightLength { get; set; }
        public double MidLength { get; set; }
    }
}
