using System;

namespace Models
{
    [Serializable]
    public class LFUMC250SUSDXF : IModel
    {
        public int LFUMC250SUSDXFId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public int Quantity { get; set; }
    }
}
