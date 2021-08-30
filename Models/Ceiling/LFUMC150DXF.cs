using System;

namespace Models
{
    [Serializable]
    public class LFUMC150DXF : IModel
    {
        public int LFUMC150DXFId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public int Quantity { get; set; }
    }
}
