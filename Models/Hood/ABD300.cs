using System;

namespace Models
{
    [Serializable]
    public class ABD300:IModel
    {
        public int ABD300Id { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
    }
}
