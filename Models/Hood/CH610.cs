﻿using System;

namespace Models
{
    [Serializable]
    public class CH610 : HoodParent,IModel
    {
        public int CH610Id { get; set; }
        public int ModuleTreeId { get; set; }
        //配置
        public string LightType { get; set; }
    }
}
