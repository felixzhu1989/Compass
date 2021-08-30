using System;

namespace Models
{
    [Serializable]
   public class UCJSB385:IModel
    {
        public int UCJSB385Id { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public decimal Length { get; set; }
        public decimal ExRightDis { get; set; }
        public decimal ExLength { get; set; }
        public decimal ExWidth { get; set; }
        public decimal ExHeight { get; set; }
        //过滤器
        public string FCSide { get; set; }
        public decimal FCSideLeft { get; set; }
        public decimal FCSideRight { get; set; }
        public int FCBlindNo { get; set; }
        //其他配置
        public string UVType { get; set; }
        public string LightType { get; set; }//只做HCL判断
        public string SSPType { get; set; }
        public string Gutter { get; set; }
        public decimal GutterWidth { get; set; }
        public string Japan { get; set; }
        //ANSUL/MARVEL
        public string ANSUL { get; set; }
        public string ANSide { get; set; }
        public string ANDetector { get; set; }
        public string MARVEL { get; set; }
    }
}
