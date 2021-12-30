using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]

    public class HMF : IModel
    {

        public int HMFId { get; set; }
        public int ModuleTreeId { get; set; }



        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double InletDia { get; set; }
        public double OutletDia { get; set; }
        public double OutletHeight { get; set; }

        public string HangPosition { get; set; }//吊脚位置
        public string PowerPlug { get; set; }//电源插口
        public double PowerPlugDis { get; set; }//电源插口距离
        public string NetPlug { get; set; }//网线插口
        public string PlugPosition { get; set; }//插口位置
        public string Heater { get; set; }//加热
        public string TemperatureSwitch { get; set; }//复位开关
        public string NamePlate { get; set; }//铭牌
        public string WindPressure { get; set; }//风压管

    }
}
