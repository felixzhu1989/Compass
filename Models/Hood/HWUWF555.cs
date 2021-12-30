using System;

namespace Models
{
    [Serializable]
    public class HWUWF555 : HoodParent, IModel
    {
        public int HWUWF555Id { get; set; }
        public int ModuleTreeId { get; set; }
        
        //配置
        public string Outlet { get; set; }
        public string LEDlogo { get; set; }
        public string BackToBack { get; set; }
        public string WaterCollection { get; set; }
        public int LEDSpotNo { get; set; }
        public decimal LEDSpotDis { get; set; }
        public string LightType { get; set; }
        public decimal LightYDis { get; set; }//新增，灯具距离前端距离
        //ANSUL
        public string ANSUL { get; set; }
        public string ANSide { get; set; }
        public string ANDetectorEnd { get; set; }
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
        //水洗入水口
        public string Inlet { get; set; }
        //灯板ANSUL探测器
        public int ANDetectorNo { get; set; }
        public decimal ANDetectorDis1 { get; set; }
        public decimal ANDetectorDis2 { get; set; }
        public decimal ANDetectorDis3 { get; set; }
        public decimal ANDetectorDis4 { get; set; }
        public decimal ANDetectorDis5 { get; set; }
    }
}
