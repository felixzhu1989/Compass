using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HoodParent
    {
        //基本尺寸
        public decimal Length { get; set; }
        public decimal Deepth { get; set; }
        public decimal ExRightDis { get; set; }
        public int ExNo { get; set; }
        public decimal ExDis { get; set; }
        public decimal ExLength { get; set; }
        public decimal ExWidth { get; set; }
        public decimal ExHeight { get; set; }
        public string SidePanel { get; set; }
    }
}
