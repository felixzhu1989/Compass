﻿using System;

namespace Models
{
    [Serializable]
   public class LKSSPEC : IModel
    {
        public int LKSSPECId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public double Length { get; set; }
        public double Height { get; set; }
        public string WBeam { get; set; }
        public string SidePanel { get; set; }
        public string LightType { get; set; }
        public string Japan { get; set; }
    }
}
