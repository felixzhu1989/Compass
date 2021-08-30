using System;

namespace Models
{
    [Serializable]
    public class LLEDA : IModel
    {
        public int LLEDAId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public decimal Length { get; set; }
    }
}
