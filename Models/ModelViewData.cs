using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class ModelViewData
    {
        public Image ModelImage { get; set; }
        public Image LabelImage { get; set; }
        public string LocalPath { get; set; }
        public string PublicPath { get; set; }
        public string EDrawingPath { get; set; }
    }
}
