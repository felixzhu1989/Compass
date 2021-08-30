using System;

namespace Models
{
    [Serializable]
    public class INF:IModel
    {
        public int INFId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public decimal Length { get; set; }
        public decimal Width { get; set; }
    }
}
