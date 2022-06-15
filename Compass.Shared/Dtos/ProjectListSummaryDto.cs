using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Dtos;
public class ProjectListSummaryDto:BaseDto
{
    private int projectCount;
    public int ProjectCount
    {
        get { return projectCount; }
        set { projectCount = value; OnPropertyChanged(); }
    }
    private int totalHoodQuantity;
    public int TotalHoodQuantity
    {
        get { return totalHoodQuantity; }
        set { totalHoodQuantity = value; OnPropertyChanged(); }
    }
    private int totalItemLine;
    public int TotalItemLine
    {
        get { return totalItemLine; }
        set { totalItemLine = value; OnPropertyChanged(); }
    }
    private float totalStdWorkload;
    public float TotalStdWorkload
    {
        get { return totalStdWorkload; }
        set { totalStdWorkload = value; OnPropertyChanged(); }
    }
    private decimal totalVtaValue;
    public decimal TotalVtaValue
    {
        get { return totalVtaValue; }
        set { totalVtaValue = value; OnPropertyChanged(); }
    }

}
