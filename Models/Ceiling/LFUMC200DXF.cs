using System;

namespace Models
{
    [Serializable]
    public class LFUMC200DXF : IModel
    {
        public int LFUMC200DXFId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public int Quantity { get; set; }
    }
}
