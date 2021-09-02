using System;

namespace Models
{
    [Serializable]
    public class DrawingNumCodeRule
    {
        public int CodeId { get; set; }
        public int ParentId { get; set; }
        public char Code { get; set; }
        public string CodeName { get; set; }
    }
    
    [Serializable]
    public class SubCode
    {
        public string SbuCode { get; set; }
        public string ProdTypeCode { get; set; }
        public string ProdNameCode { get; set; }
        public string SubAssyCode { get; set; }
        public string SuffixCode { get; set; }
        public string FinalCode { get; set; }
    }
}
