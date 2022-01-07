using System;

namespace Models
{
    [Serializable]
    public class FinancialData
    {
        public int FinancialDataId { get; set; }
        public int ProjectId { get; set; }
        public double SalesValue { get; set; } 
    }
}
