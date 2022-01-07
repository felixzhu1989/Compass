using System;

namespace Models
{
    [Serializable]
    public class LLEDS:IModel
    {
        public int LLEDSId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
    }
}
