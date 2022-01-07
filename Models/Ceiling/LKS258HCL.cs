using System;

namespace Models
{
    [Serializable]
    public class LKS258HCL : IModel
    {
        public int LKS258HCLId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        //HCL
        public string HCLSide { get; set; }
        public double HCLSideLeft { get; set; }
        public double HCLSideRight { get; set; }
    }
}
