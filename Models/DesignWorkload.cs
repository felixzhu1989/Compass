using System;

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
