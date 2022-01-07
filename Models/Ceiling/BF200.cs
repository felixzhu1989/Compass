using System;

namespace Models
{
    [Serializable]
   public class BF200 : IModel
    {
        public int BF200Id { get; set; }
        public int ModuleTreeId { get; set; }
        //基本尺寸
        public double Length { get; set; }
        public double LeftLength { get; set; }
        public double RightLength { get; set; }
        public double MPanelLength { get; set; }
        public double WPanelLength { get; set; }
        public int MPanelNo { get; set; }
        public string UVType { get; set; }
    }
}
