using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    interface IAnsul
    {
        string ANSUL { get; set; }
        string ANSide { get; set; }
        string ANDetector { get; set; }
        decimal ANYDis { get; set; }
        int ANDropNo { get; set; }
        decimal ANDropDis1 { get; set; }
        decimal ANDropDis2 { get; set; }
        decimal ANDropDis3 { get; set; }
        decimal ANDropDis4 { get; set; }
        decimal ANDropDis5 { get; set; }
    }
}
