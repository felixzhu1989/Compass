using System;

namespace Models
{
    [Serializable]
    public class HOODBCJ : IModel
    {
        public int HOODBCJId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        public double Height { get; set; }
        public double SuDis { get; set; }
    }
}
