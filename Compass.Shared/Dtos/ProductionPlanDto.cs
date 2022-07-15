using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Dtos
{
    /// <summary>
    /// 生产计划
    /// </summary>
    public class ProductionPlanDto:BaseDto
    {
        private DateTime drwReleaseDeadline;
        public DateTime DrwReleaseDeadline
        {
            get { return drwReleaseDeadline; }
            set { drwReleaseDeadline = value; OnPropertyChanged(); }
        }
        private DateTime prodFinishDeadline;
        public DateTime ProdFinishDeadline
        {
            get { return prodFinishDeadline; }
            set { prodFinishDeadline = value; OnPropertyChanged(); }
        }

        //-------------------导航属性Project(1:1)---------------------
        private ProjectDto project;
        public ProjectDto Project
        {
            get { return project; }
            set { project = value; OnPropertyChanged(); }
        }

        //-------------------导航属性Activity(1:n)---------------------
        private List<ActivityDto> activitys;
        public List<ActivityDto> Activitys
        {
            get { return activitys; }
            set { activitys = value; OnPropertyChanged(); }
        }
        public ProductionPlanDto()
        {
            Activitys=new List<ActivityDto>();
        }
    }
}
