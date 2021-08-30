using System;

namespace Models
{
    [Serializable]
    public class LFUMC250DXF : IModel
    {
        public int LFUMC250DXFId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public int Quantity { get; set; }
    }
}
