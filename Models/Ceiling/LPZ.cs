using System;

namespace Models
{
    [Serializable]
   public class LPZ : IModel
    {
        public int LPZId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        public double Width { get; set; }
        public int ZPanelNo { get; set; }
        public string LightType { get; set; }//LED60
    }
}
