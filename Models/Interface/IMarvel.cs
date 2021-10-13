using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    interface IMarvel
    {
        string MARVEL { get; set; }
        int IRNo { get; set; }
        decimal IRDis1 { get; set; }
        decimal IRDis2 { get; set; }
        decimal IRDis3 { get; set; }
    }
}
