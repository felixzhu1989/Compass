using System;

namespace Models
{
    [Serializable]
    public class LFUMC200SUSDXF : IModel
    {
        public int LFUMC200SUSDXFId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public int Quantity { get; set; }
    }
}
