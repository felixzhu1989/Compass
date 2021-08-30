using System;

namespace Models
{
    [Serializable]
   public class BF200 : IModel
    {
        public int BF200Id { get; set; }
        public int ModuleTreeId { get; set; }
        //基本尺寸
        public decimal Length { get; set; }
        public decimal LeftLength { get; set; }
        public decimal RightLength { get; set; }
        public decimal MPanelLength { get; set; }
        public decimal WPanelLength { get; set; }
        public int MPanelNo { get; set; }
        public string UVType { get; set; }
    }
}
