using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
   public class LKA258 : IModel
    {
        public int LKA258Id { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public decimal Length { get; set; }
        public string LightType { get; set; }
        public string Japan { get; set; }
    }
}
