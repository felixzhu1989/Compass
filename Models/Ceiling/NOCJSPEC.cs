using System;

namespace Models
{
    [Serializable]
    public class NOCJSPEC:IModel
    {
        public int NOCJSPECId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        public double Width { get; set; }//45/90
        public double Height { get; set; }
        public double TopBend { get; set; }//顶部翻边
        public double BackBend { get; set; }//背部翻边
        public string SidePanel { get; set; }
        public string BackCJSide { get; set; }
        public double LeftDis { get; set; }
        public double RightDis { get; set; }
        public string LeftBeamType { get; set; }
        public double LeftBeamDis { get; set; }
        public string RightBeamType { get; set; }
        public double RightBeamDis { get; set; }
        public string LKSide { get; set; }
        public string GutterSide { get; set; }
        public double GutterWidth { get; set; }
    }
}
