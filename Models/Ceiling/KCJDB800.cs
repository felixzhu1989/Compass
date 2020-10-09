using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class KCJDB800 : IModel
    {
        public int KCJDB800Id { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public decimal Length { get; set; }
        public decimal ExRightDis { get; set; }
        public decimal ExLength { get; set; }
        public decimal ExWidth { get; set; }
        public decimal ExHeight { get; set; }
        //过滤器
        public string FCType { get; set; }
        public string FCSide { get; set; }
        public decimal FCSideLeft { get; set; }
        public decimal FCSideRight { get; set; }
        public int FCBlindNo { get; set; }
        //其他配置
        public string LightType { get; set; }
        public string LightCable { get; set; }
        public string SSPType { get; set; }
        public string Gutter { get; set; }
        public decimal GutterWidth { get; set; }
        public string Japan { get; set; }
        //ANSUL/MARVEL
        public string ANSUL { get; set; }
        public string ANSide { get; set; }
        public string ANDetectorEnd { get; set; }//末端
        public int ANDetectorNo { get; set; }
        public decimal ANDetectorDis1 { get; set; }
        public decimal ANDetectorDis2 { get; set; }
        public decimal ANDetectorDis3 { get; set; }
        public decimal ANDetectorDis4 { get; set; }
        public decimal ANDetectorDis5 { get; set; }
        public string MARVEL { get; set; }
        
    }
}
