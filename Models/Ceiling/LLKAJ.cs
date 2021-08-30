using System;

namespace Models
{
    [Serializable]
    public class LLKAJ : IModel
    {
        public int LLKAJId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public decimal Length { get; set; }
        public int LongGlassNo { get; set; }
        public int ShortGlassNo { get; set; }
        public decimal LeftLength { get; set; }
        public decimal RightLength { get; set; }
    }
}
