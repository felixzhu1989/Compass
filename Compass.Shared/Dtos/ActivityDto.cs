using Compass.Shared.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Dtos;

public class ActivityDto : BaseDto, IComparable<ActivityDto>

{
    private Activity_e activityName;
    public Activity_e ActivityName
    {
        get { return activityName; }
        set { activityName = value; OnPropertyChanged(); }
    }
    /// <summary>
    /// 顺序
    /// </summary>
    private int order;
    public int Order
    {
        get { return order; }
        set { order = value; OnPropertyChanged(); }
    }
    private DateTime start;
    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime Start
    {
        get { return start; }
        set { start = value; OnPropertyChanged(); }
    }
    private DateTime end;
    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime End
    {
        get { return end; }
        set { end = value; OnPropertyChanged(); }
    }

    /// <summary>
    /// 持续时间
    /// </summary>
    public int Duration { get { return (End-Start).Days+1; } }
    /// <summary>
    /// 实现排序
    /// </summary> 
    public int CompareTo(ActivityDto? other)
    {
        //返回值含义：小于0：放在传入对象的前面; 等于0：保持当前的位置不变,大于0：放在传入对象的后面
        if (order==other.order) return 0;
        else if (order>other.order) return 1;
        else return -1;
    }
}
