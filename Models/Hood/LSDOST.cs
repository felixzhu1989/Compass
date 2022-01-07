using System;

namespace Models
{
    [Serializable]
   public class LSDOST:IModel
    {
        public int LSDOSTId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        public int SuNo { get; set; }
        public double SuDis { get; set; }
        public double Deepth { get; set; }
        public string SidePanel { get; set; }
    }
}
