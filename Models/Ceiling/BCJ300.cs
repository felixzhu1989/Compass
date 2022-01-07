using System;

namespace Models
{
    [Serializable]
    public class BCJ300 : IModel
    {
        public int BCJ300Id { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        public string SidePanel { get; set; }
        public string SuType { get; set; }
        public double SuDis { get; set; }
    }
}
