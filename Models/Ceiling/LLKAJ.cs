using System;

namespace Models
{
    [Serializable]
    public class LLKAJ : IModel
    {
        public int LLKAJId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        public int LongGlassNo { get; set; }
        public int ShortGlassNo { get; set; }
        public double LeftLength { get; set; }
        public double RightLength { get; set; }
    }
}
