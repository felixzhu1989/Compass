using System;

namespace Models
{
    [Serializable]
   public class TCSBOXDXF : IModel
    {
        public int TCSBOXDXFId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public int Quantity { get; set; }
    }
}
