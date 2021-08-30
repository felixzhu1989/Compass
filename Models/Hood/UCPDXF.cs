using System;

namespace Models
{
    [Serializable]
    public class UCPDXF:IModel
    {
        public int UCPDXFId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public int Quantity { get; set; }
    }
}
