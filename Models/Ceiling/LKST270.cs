using System;

namespace Models
{
    [Serializable]
   public class LKST270 : IModel
    {
        public int LKST270Id { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        public string WBeam { get; set; }
        public string SidePanel { get; set; }
        public string LightType { get; set; }
        public string Japan { get; set; }
    }
}
