using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateProgram
{
    /// <summary>
    /// xml文件映射
    /// </summary>
    public class UpdateInfo
    {
        public string Version { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateFileUrl { get; set; }
        //更新文件信息列表，根据listView的显示顺序对应，集合中嵌套数组
        public List<string[]> FileList { get; set; }
    }
}
