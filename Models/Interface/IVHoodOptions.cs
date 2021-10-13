using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    interface IHoodOptions
    {
        //配置Options
        string Outlet { get; set; }
        string LEDlogo { get; set; }
        string BackToBack { get; set; }
       
        int LEDSpotNo { get; set; }
        decimal LEDSpotDis { get; set; }
        string LightType { get; set; }
    }
}
