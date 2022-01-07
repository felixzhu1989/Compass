using System;

namespace Models
{
    [Serializable]
    public class UVI450300 : HoodParent, IModel
    {
        public int UVI450300Id { get; set; }
        public int ModuleTreeId { get; set; }
        
        //配置
        public string Outlet { get; set; }
        public string LEDlogo { get; set; }
        public string BackToBack { get; set; }
        public string WaterCollection { get; set; }
        public int LEDSpotNo { get; set; }
        public double LEDSpotDis { get; set; }
        public string LightType { get; set; }
        //ANSUL
        public string ANSUL { get; set; }
        public string ANSide { get; set; }
        public string ANDetector { get; set; }
        public double ANYDis { get; set; }
        public int ANDropNo { get; set; }
        public double ANDropDis1 { get; set; }
        public double ANDropDis2 { get; set; }
        public double ANDropDis3 { get; set; }
        public double ANDropDis4 { get; set; }
        public double ANDropDis5 { get; set; }
        //MARVEL
        public string MARVEL { get; set; }
        public int IRNo { get; set; }
        public double IRDis1 { get; set; }
        public double IRDis2 { get; set; }
        public double IRDis3 { get; set; }
        //UV
        public string UVType { get; set; }
        public string Bluetooth { get; set; }
    }
}
