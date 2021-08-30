using System;

namespace Models
{
    [Serializable]
   public class UCJFCCOMBIDXF : IModel
    {
        public int UCJFCCOMBIDXFId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public int Quantity { get; set; }
    }
}
