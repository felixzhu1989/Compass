﻿using System;

namespace Models
{
    /// <summary>
    /// 工厂质量反馈
    /// </summary>
    [Serializable]
    public class QualityFeedback
    {
        public int QualityFeedbackId { get; set; }
        public int ProjectId { get; set; }
        public string Content { get; set; }
        public DateTime AddedDate { get; set; }
        public int UserId { get; set; }
    }
}
