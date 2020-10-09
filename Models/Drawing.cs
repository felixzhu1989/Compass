using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 客户图纸
    /// </summary>
    [Serializable]
    public class Drawing
    {
        public int DrawingPlanId { get; set; }
        public string Item { get; set; }
        public string LabelImage { get; set; }
        //简单扩展
        public string ODPNo { get; set; }
        public int ProjectId { get; set; }
        public int ModuleNo { get; set; }//图纸中分段数量
        public string ProjectName { get; set; }
        public string HoodType { get; set; }
    }
}
