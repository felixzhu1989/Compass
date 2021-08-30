using System;

namespace Models
{
    [Serializable]
    public class LFUP : IModel
    {
        public int LFUPId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public decimal Length { get; set; }
        public decimal Width { get; set; }
    }
}
