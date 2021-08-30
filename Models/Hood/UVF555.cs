using System;

namespace Models
{
    [Serializable]
    public class UVF555:IModel
    {
        public int UVF555Id { get; set; }
        public int ModuleTreeId { get; set; } 
        //基本尺寸
        public decimal Length { get; set; }
        public decimal Deepth { get; set; }
        public decimal ExRightDis { get; set; }
        public int ExNo { get; set; }
        public decimal ExDis { get; set; }
        public decimal ExLength { get; set; }
        public decimal ExWidth { get; set; }
        public decimal ExHeight { get; set; }
        public string SidePanel { get; set; }
        //配置
        public string Outlet { get; set; }
        public string LEDlogo { get; set; }
        public string BackToBack { get; set; }
        public string WaterCollection { get; set; }
        public int LEDSpotNo { get; set; }
        public decimal LEDSpotDis { get; set; }
        public string LightType { get; set; }
        //ANSUL
        public string ANSUL { get; set; }
        public string ANSide { get; set; }
        public string ANDetector { get; set; }
        public decimal ANYDis { get; set; }
        public int ANDropNo { get; set; }
        public decimal ANDropDis1 { get; set; }
        public decimal ANDropDis2 { get; set; }
        public decimal ANDropDis3 { get; set; }
        public decimal ANDropDis4 { get; set; }
        public decimal ANDropDis5 { get; set; }
        //MARVEL
        public string MARVEL { get; set; }
        public int IRNo { get; set; }
        public decimal IRDis1 { get; set; }
        public decimal IRDis2 { get; set; }
        public decimal IRDis3 { get; set; }
        //UV
        public string UVType { get; set; }
        public string Bluetooth { get; set; }
        //F型新风
        public int SuNo { get; set; }
        public decimal SuDis { get; set; }
    }
}
