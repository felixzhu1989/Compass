using System;

namespace Models
{
    [Serializable]
   public class LPZ : IModel
    {
        public int LPZId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public int ZPanelNo { get; set; }
        public string LightType { get; set; }//LED60
    }
}
