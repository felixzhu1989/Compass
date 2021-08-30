using System;

namespace Models
{
    /// <summary>
    /// 售后反馈
    /// </summary>
    [Serializable]
    public class AfterSaleFeedback
    {
        public int AfterSaleFeedbackId { get; set; }
        public int ProjectId { get; set; }
        public string Content { get; set; }//FUCK 搞了好久，所以还是一开始就要想好啊，后面要强迫症一下真的很不爽啊
        public DateTime AddedDate { get; set; }
        public int UserId { get; set; }
    }
}
