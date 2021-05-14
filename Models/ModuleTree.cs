using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 项目模型树
    /// </summary>
    [Serializable]
    public class ModuleTree
    {
        public string ModuleTreeCode { get; set; }  //DrawingPlanId     ModuleTreeId 
        public string ParentCode { get; set; }      //ProjectId         DrawingPlanId
        public string ModuleName { get; set; }      //Item              Module(M1,M2,M3...)-CategoryName(select from CategoryId) 
        //数据库
        public int ModuleTreeId { get; set; }
        public int DrawingPlanId { get; set; }  
        public int CategoryId { get; set; }
        public string  Module { get; set; }
        //简单扩展
        public int ProjectId { get; set; }
        public string ODPNo { get; set; }
        public string Item { get; set; }
        public string CategoryName { get; set; } 
        public string CategoryDesc { get; set; }
        public string Model { get; set; }
        public string SubType { get; set; }
        public string LastSave { get; set; }
        public string ModelPath { get; set; }
        public string SBU { get; set; }
    }
}
