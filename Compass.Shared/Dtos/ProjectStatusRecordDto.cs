using Compass.Shared.Const;

namespace Compass.Shared.Dtos;
public class ProjectStatusRecordDto:ProjectStatusDto,IComparable<ProjectStatusRecordDto>
{    
    private DateTime startDate;
    public DateTime StartDate
    {
        get { return startDate; }
        set { startDate = value; OnPropertyChanged(); }
    }
    public int CompareTo(ProjectStatusRecordDto? other)
    {
        if (startDate==other.startDate) return 0;
        else if (startDate>other.startDate) return 1;
        else return -1;
    }
}
