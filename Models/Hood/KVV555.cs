using System;

namespace Models
{
    [Serializable]
    public class KVV555 : HoodParent, IModel
    {
        public int KVV555Id { get; set; }
        public int ModuleTreeId { get; set; }
        //配置
        public string LightType { get; set; }

    }
}
