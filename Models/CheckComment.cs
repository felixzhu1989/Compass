using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 互检评论
    /// </summary>
    [Serializable]
    public class CheckComment
    {
        public int CheckCommentId { get; set; }
        public int ProjectId { get; set; }
        public string Content { get; set; }
        public int CheckStatus  { get; set; }
        public DateTime AdddedDate { get; set; }
        public int UserId { get; set; }
    }
}
