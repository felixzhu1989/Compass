using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 项目信息
    /// </summary>
    [Serializable]
    public class Project
    {
        public int ProjectId { get; set; }
        public string ODPNo { get; set; }
        public string BPONo { get; set; }
        public string ProjectName { get; set; }
        public int CustomerId { get; set; }
        public DateTime ShippingTime { get; set; }
        public DateTime AddedDate { get; set; }
        public int UserId { get; set; }
        public string HoodType { get; set; }
        //简单扩展
        public string UserAccount { get; set; }
        public string CustomerName { get; set; }
        public int RiskLevel { get; set; }
        public string ProjectStatusName { get; set; }
    }
}
