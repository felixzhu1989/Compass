using System;

namespace Models
{
    /// <summary>
    /// 项目经验教训
    /// </summary>
    [Serializable]
    public class ProjectLearned
    {
        public int ProjectLearnedId { get; set; }
        public int ProjectId { get; set; }
        public string Content { get; set; }
        public string MKLink { get; set; }
        public DateTime AddedDate { get; set; }
        public int UserId { get; set; }
    }
}
