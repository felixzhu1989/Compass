using System;

namespace Models
{
    [Serializable]
   public class LSDOST:IModel
    {
        public int LSDOSTId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public decimal Length { get; set; }
        public int SuNo { get; set; }
        public decimal SuDis { get; set; }
        public decimal Deepth { get; set; }
        public string SidePanel { get; set; }
    }
}
