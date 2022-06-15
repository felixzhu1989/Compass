using Compass.Shared.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Dtos;

public class ProjectStatusDto:BaseDto
{    
    public string StatusName { get { return ProjectStatus.ToString(); } }
    private ProjectStatus_e projectStatus;
    public ProjectStatus_e ProjectStatus
    {
        get { return projectStatus; }
        set { projectStatus = value; OnPropertyChanged(nameof(StatusName)); }
    }   
}
