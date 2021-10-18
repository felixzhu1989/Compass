using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class CutList
    {
        public int CutListId { get; set; }
        //表内容
        public string PartDescription { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Thickness { get; set; }
        public int Quantity { get; set; }
        public string Materials { get; set; }
        public string PartNo { get; set; }
        public DateTime AddedDate { get; set; }
        //简单扩展，表头
        public string UserAccount { get; set; }
        public int UserId { get; set; }
    }
}
