using System;

namespace Models
{
    [Serializable]
    public class DXFCutList
    {
        public int CutListId { get; set; }
        public int CategoryId { get; set; }
        //表内容
        public string PartDescription { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }
        public int Quantity { get; set; }
        public string Materials { get; set; }
        public string PartNo { get; set; }
    }
}
