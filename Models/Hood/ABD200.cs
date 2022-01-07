using System;

namespace Models
{
    [Serializable]
   public class ABD200:IModel
    {
        public int ABD200Id { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
    }
}
