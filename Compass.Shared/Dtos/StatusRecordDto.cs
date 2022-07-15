using Compass.Shared.Const;

namespace Compass.Shared.Dtos;

/// <summary>
/// 项目状态记录
/// </summary>
public class StatusRecordDto:BaseDto, IComparable<StatusRecordDto>
{
    private ProjectStatus_e projectStatus;
    public ProjectStatus_e ProjectStatus
    {
        get { return projectStatus; }
        set { projectStatus = value; OnPropertyChanged(); }
    }
    private DateTime recordDate;
    public DateTime RecordDate
    {
        get { return recordDate; }
        set { recordDate = value; OnPropertyChanged(); }
    }
    public int CompareTo(StatusRecordDto? other)
    {
        if (recordDate==other.recordDate) return 0;
        else if (recordDate>other.recordDate) return 1;
        else return -1;
    }
}
