using System;

namespace Models
{
    [Serializable]
    public class DrawingNumMatrix
    {
        public int DrawingId { get; set; }
        public string DrawingNum { get; set; }
        public string DrawingDesc { get; set; }
        public string DrawingType { get; set; }
        public DateTime AddedDate { get; set; }
        public string Mark { get; set; }
        public string UserAccount { get; set; }
        public int UserId { get; set; }
        public string DrawingImage { get; set; }

    }
}
