﻿using System;

namespace Models
{
    [Serializable]
    public class LFUMC150SUSDXF : IModel
    {
        public int LFUMC150SUSDXFId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本参数
        public int Quantity { get; set; }
    }
}
