using System;

namespace Models
{
    [Serializable]
    public class LFUSC : IModel
    {
        public int LFUSCId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public decimal Length { get; set; }
        public int SuNo { get; set; }
        public decimal SuDia { get; set; }//200/250
        public decimal SuDis { get; set; }
        public string Japan { get; set; }
    }
}
