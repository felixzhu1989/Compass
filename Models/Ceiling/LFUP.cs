using System;

namespace Models
{
    [Serializable]
    public class LFUP : IModel
    {
        public int LFUPId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        public double Width { get; set; }
    }
}
