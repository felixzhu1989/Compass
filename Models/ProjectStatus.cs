﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 项目运行状态
    /// </summary>
    [Serializable]
    public class ProjectStatus
    {
        public int ProjectStatusId { get; set; }
        public string ProjectStatusName { get; set; }
        public string StatusDesc { get; set; }
    }
}
