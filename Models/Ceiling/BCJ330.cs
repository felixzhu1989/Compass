using System;

namespace Models
{
    [Serializable]
   public class BCJ330:IModel
    {
        public int BCJ330Id { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public decimal Length { get; set; }
        public string SidePanel { get; set; }
        public string SuType { get; set; }
        public decimal SuDis { get; set; }
    }
}
