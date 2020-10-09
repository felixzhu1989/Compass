using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 产品型号
    /// </summary>
    [Serializable]
    public class Category
    {
        public int CategoryId { get; set; }
        public int ParentId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDesc { get; set; }
        public string Model { get; set; }
        public string SubType { get; set; }
        public string ModelImage { get; set; }
        public string LastSaved { get; set; }
        public string KMLink { get; set; }
        public string ModelPath { get; set; }
    }
}
