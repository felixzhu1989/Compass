﻿using System;

namespace Models
{
    [Serializable]
   public class FRUVF555 : HoodParent, IModel, IHoodOptions, IWaterCollection, IUV, ISuF, IAnsul, IMarvel
    {
        public int FRUVF555Id { get; set; }
        public int ModuleTreeId { get; set; }
        public string Outlet { get; set; }
        public string LEDlogo { get; set; }
        public string BackToBack { get; set; }
        public int LEDSpotNo { get; set; }
        public decimal LEDSpotDis { get; set; }
        public string LightType { get; set; }
        public string WaterCollection { get; set; }
        public string UVType { get; set; }
        public string Bluetooth { get; set; }
        public int SuNo { get; set; }
        public decimal SuDis { get; set; }
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
        public string MARVEL { get; set; }
        public int IRNo { get; set; }
        public decimal IRDis1 { get; set; }
        public decimal IRDis2 { get; set; }
        public decimal IRDis3 { get; set; }
    }
}
