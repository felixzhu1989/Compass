using System;

namespace Models
{
    [Serializable]
   public class AN:IModel
    {
        public int ANId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本尺寸
        public double Length { get; set; }
        public double Width { get; set; }
        //ANSUL
        public string ANSUL { get; set; }
        public string ANSide { get; set; }//此处无用
        public string ANDetector { get; set; }//此处无用
        public double ANYDis { get; set; }
        public int ANDropNo { get; set; }
        public double ANDropDis1 { get; set; }
        public double ANDropDis2 { get; set; }
        public double ANDropDis3 { get; set; }
        public double ANDropDis4 { get; set; }
        public double ANDropDis5 { get; set; }
        //灯板ANSUL探测器
        public int ANDetectorNo { get; set; }
        public string ANDetectorEnd { get; set; }
        public double ANDetectorDis1 { get; set; }
        public double ANDetectorDis2 { get; set; }
        public double ANDetectorDis3 { get; set; }
        public double ANDetectorDis4 { get; set; }
        public double ANDetectorDis5 { get; set; }
        //MARVEL
        public string MARVEL { get; set; }
        public int IRNo { get; set; }
        public double IRDis1 { get; set; }
        public double IRDis2 { get; set; }
        public double IRDis3 { get; set; }
    }
}
