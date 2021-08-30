using System;

namespace Models
{
    [Serializable]
    public class HOODBCJ : IModel
    {
        public int HOODBCJId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public decimal Length { get; set; }
        public decimal Height { get; set; }
        public decimal SuDis { get; set; }
    }
}
