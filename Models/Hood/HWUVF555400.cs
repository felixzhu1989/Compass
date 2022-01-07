using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class HWUVF555400 : HoodParent, IModel
    {
        public int HWUVF555400Id { get; set; }
        public double LightYDis { get; set; }//新增，灯具距离前端距离
        public int ModuleTreeId { get; set; }
        //配置
        public string Outlet { get; set; }
        public string LEDlogo { get; set; }
        public string BackToBack { get; set; }
        public int LEDSpotNo { get; set; }
        public double LEDSpotDis { get; set; }
        public string LightType { get; set; }
        public string WaterCollection { get; set; }
        //UV
        public string UVType { get; set; }
        public string Bluetooth { get; set; }
        //F型新风
        public int SuNo { get; set; }
        public double SuDis { get; set; }
        //ANSUL
        public string ANSUL { get; set; }
        public string ANSide { get; set; }
        public string ANDetector { get; set; }
        public double ANYDis { get; set; }
        public int ANDropNo { get; set; }
        public double ANDropDis1 { get; set; }
        public double ANDropDis2 { get; set; }
        public double ANDropDis3 { get; set; }
        public double ANDropDis4 { get; set; }
        public double ANDropDis5 { get; set; }
        //MARVEL
        public string MARVEL { get; set; }
        public int IRNo { get; set; }
        public double IRDis1 { get; set; }
        public double IRDis2 { get; set; }
        public double IRDis3 { get; set; }
    }
}
