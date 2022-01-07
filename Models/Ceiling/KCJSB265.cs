using System;

namespace Models
{
    [Serializable]
    public class KCJSB265 : IModel
    {
        public int KCJSB265Id { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        public double ExRightDis { get; set; }
        public double ExLength { get; set; }
        public double ExWidth { get; set; }
        public double ExHeight { get; set; }
        //过滤器
        public string FCType { get; set; }
        public string FCSide { get; set; }
        public double FCSideLeft { get; set; }
        public double FCSideRight { get; set; }
        public int FCBlindNo { get; set; }
        //其他配置
        public string SSPType { get; set; }
        public string Gutter { get; set; }
        public double GutterWidth { get; set; }
        public string Japan { get; set; }
        //ANSUL/MARVEL
        public string ANSUL { get; set; }
        public string ANSide { get; set; }
        public string ANDetector { get; set; }
        public string MARVEL { get; set; }
        

    }
}
