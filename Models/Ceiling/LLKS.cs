using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class LLKS : IModel
   {
       public int LLKSId { get; set; }
       public int ModuleTreeId { get; set; }
       //基本参数
       public decimal Length { get; set; }
       public int LongGlassNo { get; set; }
       public int ShortGlassNo { get; set; }
   }
}
