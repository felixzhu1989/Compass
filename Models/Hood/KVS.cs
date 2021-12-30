using System;

namespace Models
{
    [Serializable]
   public class KVS: HoodParent, IModel
    {
        public int KVSId { get; set; }
        public int ModuleTreeId { get; set; }
        //配置
        public string LightType { get; set; }
        
    }
}
