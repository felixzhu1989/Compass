using System;
using System.Drawing;

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
