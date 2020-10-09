﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
   public class AN:IModel
    {
        public int ANId { get; set; }
        public int ModuleTreeId { get; set; }
        //基本尺寸
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        //ANSUL
        public string ANSUL { get; set; }
        public decimal ANYDis { get; set; }
        public int ANDropNo { get; set; }
        public decimal ANDropDis1 { get; set; }
        public decimal ANDropDis2 { get; set; }
        public decimal ANDropDis3 { get; set; }
        public decimal ANDropDis4 { get; set; }
        public decimal ANDropDis5 { get; set; }
        //灯板ANSUL探测器
        public int ANDetectorNo { get; set; }
        public string ANDetectorEnd { get; set; }
        public decimal ANDetectorDis1 { get; set; }
        public decimal ANDetectorDis2 { get; set; }
        public decimal ANDetectorDis3 { get; set; }
        public decimal ANDetectorDis4 { get; set; }
        public decimal ANDetectorDis5 { get; set; }
        //MARVEL
        public string MARVEL { get; set; }
        public int IRNo { get; set; }
        public decimal IRDis1 { get; set; }
        public decimal IRDis2 { get; set; }
        public decimal IRDis3 { get; set; }
    }
}
