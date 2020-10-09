using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 产品工作量
    /// </summary>
    [Serializable]
    public class DesignWorkload
    {
        public int WorkloadId { get; set; }
        public string Model { get; set; }
        public decimal WorkloadValue { get; set; }
        public string ModelDesc { get; set; }
    }
}
