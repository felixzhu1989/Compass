using System;

namespace Models
{
    [Serializable]
    public class LFUSC : IModel
    {
        public int LFUSCId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        public int SuNo { get; set; }
        public double SuDia { get; set; }//200/250
        public double SuDis { get; set; }
        public string Japan { get; set; }
    }
}
