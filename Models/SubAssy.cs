using System;

namespace Models
{
    [Serializable]
    public class SubAssy
    {
        public int SubAssyId { get; set; }
        public int ProjectId { get; set; }
        public string SubAssyName { get; set; }
        public string SubAssyPath { get; set; }
        //简单扩展
        public string ODPNo { get; set; }
        public string ProjectName { get; set; }
    }
}
