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
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Thickness { get; set; }
        public int Quantity { get; set; }
        public string Materials { get; set; }
        public string PartNo { get; set; }
    }
}
