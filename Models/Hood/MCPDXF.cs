using System;

namespace Models
{
    [Serializable]
   public class MCPDXF:IModel
    {
        public int MCPDXFId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public int Quantity { get; set; }
    }
}
