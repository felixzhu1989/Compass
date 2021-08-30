using System;

namespace Models
{
    [Serializable]
    public class MU1BOXDXF : IModel
    {
        public int MU1BOXDXFId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public int Quantity { get; set; }
    }
}
