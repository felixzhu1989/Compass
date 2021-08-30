using System;

namespace Models
{
    [Serializable]
   public class NOCJ300:IModel
    {
        public int NOCJ300Id { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public string SidePanel { get; set; }
        public string BackCJSide { get; set; }
        public decimal LeftDis { get; set; }
        public decimal RightDis { get; set; }
        public string LeftBeamType { get; set; }
        public decimal LeftBeamDis { get; set; }
        public string RightBeamType { get; set; }
        public decimal RightBeamDis { get; set; }
        public string LKSide { get; set; }
        public string GutterSide { get; set; }
        public decimal GutterWidth { get; set; }
    }
}
