using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    interface IAnsulDetector
    {
        int ANDetectorNo { get; set; }
        string ANDetectorEnd { get; set; }
        decimal ANDetectorDis1 { get; set; }
        decimal ANDetectorDis2 { get; set; }
        decimal ANDetectorDis3 { get; set; }
        decimal ANDetectorDis4 { get; set; }
        decimal ANDetectorDis5 { get; set; }
    }
}
