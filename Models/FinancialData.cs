using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class FinancialData
    {
        public int FinancialDataId { get; set; }
        public int ProjectId { get; set; }
        public decimal SalesValue { get; set; } 
    }
}
