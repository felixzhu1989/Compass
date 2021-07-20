using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 项目跟踪
    /// </summary>
    [Serializable]
    public class ProjectTracking
    {
        public int ProjectTrackingId { get; set; }
        public int ProjectId { get; set; }
        
        public int ProjectStatusId { get; set; }
        public DateTime DrReleaseTarget { get; set; }
        public DateTime DrReleaseActual { get; set; }
        public DateTime ProdFinishTarget { get; set; }
        public DateTime ProdFinishActual { get; set; }
        public DateTime DeliverActual { get; set; }
        public string KickOffStatus { get; set; }
        public DateTime ODPReceiveDate { get; set; }
        public DateTime KickOffDate { get; set; }

        //简单扩展
        public string ODPNo { get; set; }
        public string ProjectStatusName { get; set; }
        public string ProjectName { get; set; }
        public string UserAccount { get; set; }
    }
}
