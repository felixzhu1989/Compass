using Compass.Shared.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Dtos
{
    /// <summary>
    /// 项目技术配置
    /// </summary>
    public class TechnicalOptionsDto:BaseDto
    {
        //风险等级
        private int riskLevel;
        public int RiskLevel
        {
            get { return riskLevel; }
            set { riskLevel = value; OnPropertyChanged(); }
        }
        //产品类型
        private ProductType_e productType;
        public ProductType_e ProductType
        {
            get { return productType; }
            set { productType = value; OnPropertyChanged(); }
        }
        //电制，MARVEL，ANSUL预埋，ANSUL系统
        private Electricity_e electricity;
        public Electricity_e Electricity
        {
            get { return electricity; }
            set { electricity = value; OnPropertyChanged(); }
        }
        private MarvelOption_e marvelOption;
        public MarvelOption_e MarvelOption
        {
            get { return marvelOption; }
            set { marvelOption = value; OnPropertyChanged(); }
        }
        private AnsulPrePipe_e ansulPrePipe;
        public AnsulPrePipe_e AnsulPrePipe
        {
            get { return ansulPrePipe; }
            set { ansulPrePipe = value; OnPropertyChanged(); }
        }
        private AnsulSystem_e ansulSystem;
        public AnsulSystem_e AnsulSystem
        {
            get { return ansulSystem; }
            set { ansulSystem = value; OnPropertyChanged(); }
        }
        //-------------------导航属性Project(1:1)---------------------
        private ProjectDto project;
        public ProjectDto Project
        {
            get { return project; }
            set { project = value; OnPropertyChanged(); }
        }
    }
}
