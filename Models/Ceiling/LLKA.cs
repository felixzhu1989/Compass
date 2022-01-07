using System;

namespace Models
{
    [Serializable]
    public class LLKA : IModel
    {
        public int LLKAId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        public int LongGlassNo { get; set; }
        public int ShortGlassNo { get; set; }
    }
}
