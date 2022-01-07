using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class LKS270HCL : IModel
    {
        public int LKS270HCLId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        //HCL
        public string HCLSide { get; set; }
        public double HCLSideLeft { get; set; }
        public double HCLSideRight { get; set; }
    }
}
