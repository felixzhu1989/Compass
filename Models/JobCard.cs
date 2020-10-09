using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    [Serializable]
    public class JobCard
    {
        public string ODPNo { get; set; }
        public string BPONo { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public string Item { get; set; }
        public string Module { get; set; }
        public string Model { get; set; }
        public DateTime ShippingTime { get; set; }
        public int Length { get; set; }
        public int Deepth { get; set; }
        public int Width { get; set; }
        public string Height { get; set; }
        public string SidePanel { get; set; }
        public string LabelImage { get; set; }
        //简单扩展
        public string HoodType { get; set; }
    }
}
