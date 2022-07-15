using Compass.Shared.Const;

namespace Compass.Shared.Dtos;
public class ProjectDto : BaseDto //, IComparable<ProjectDto>
{
    #region 项目信息属性
    private string quoteNumber;
    public string QuoteNumber
    {
        get { return quoteNumber; }
        set { quoteNumber = value; OnPropertyChanged(); }
    }
    private string odpNumber;
    public string OdpNumber
    {
        get { return odpNumber; }
        set { odpNumber = value; OnPropertyChanged(); }
    }
    private string prodNumber;
    public string ProdNumber
    {
        get { return prodNumber; }
        set { prodNumber = value; OnPropertyChanged(); }
    }
    private string projectName;
    public string ProjectName
    {
        get { return projectName; }
        set { projectName = value; OnPropertyChanged(); }
    }
    #endregion

    #region 项目概况属性
    private int goodsQuantity;
    public int GoodsQuantity
    {
        get { return goodsQuantity; }
        set { goodsQuantity = value; OnPropertyChanged(); }
    }

    private int itemLine;
    public int ItemLine
    {
        get { return itemLine; }
        set { itemLine = value; OnPropertyChanged(); }
    }

    private float stdWorkload;
    public float StdWorkload
    {
        get { return stdWorkload; }
        set { stdWorkload = value; OnPropertyChanged(); }
    }
    
    private string goodsSummary;
    public string GoodsSummary
    {
        get { return goodsSummary; }
        set { goodsSummary = value; OnPropertyChanged(); }
    }

    private string specialRequirements;
    public string SpecialRequirements
    {
        get { return specialRequirements; }
        set { specialRequirements = value; OnPropertyChanged(); }
    }
    #endregion

    //-------------------导航属性Customer(n:1)---------------------
    private CustomerDto customer;
    public CustomerDto Customer
    {
        get { return customer; }
        set { customer = value; OnPropertyChanged(); }
    }
    //-------------------导航属性User(n:1)---------------------
    private UserDto user;
    public UserDto User
    {
        get { return user; }
        set { user = value; OnPropertyChanged(); }
    }


    //-------------------导航属性TechnicalOptions(1:1)---------------------
    private TechnicalOptionsDto technicalOptions;
    public TechnicalOptionsDto TechnicalOptions
    {
        get { return technicalOptions; }
        set { technicalOptions = value; OnPropertyChanged(); }
    }

    //-------------------导航属性ProjectTracing(1:1)---------------------
    private ProjectTrackingDto projectTracking;
    public ProjectTrackingDto ProjectTracking
    {
        get { return projectTracking; }
        set { projectTracking = value; OnPropertyChanged();}
    }

    //-------------------导航属性ProductionPlan(1:1)---------------------
    private ProductionPlanDto productionPlan;
    public ProductionPlanDto ProductionPlan
    {
        get { return productionPlan; }
        set { productionPlan = value; OnPropertyChanged(); }
    }

    //-------------------导航属性FinancialInformation(1:1)---------------------
    private FinancialInformationDto financialInformation;
    public FinancialInformationDto FinancialInformation
    {
        get { return financialInformation; }
        set { financialInformation = value; OnPropertyChanged(); }
    }







    //-------------------导航属性StatusRecord(1:n)---------------------
    private List<StatusRecordDto> statusRecords;
    public List<StatusRecordDto> StatusRecords
    {
        get { return statusRecords; }
        set { statusRecords = value; OnPropertyChanged(); }
    }

    //-------------------导航属性AbnormalAlarm(1:n)---------------------
    private List<AbnormalAlarmDto> abnormalAlarms;
    public List<AbnormalAlarmDto> AbnormalAlarms
    {
        get { return abnormalAlarms; }
        set { abnormalAlarms = value; OnPropertyChanged(); }
    }










    ///// <summary>
    ///// 按照完工日期排序
    ///// </summary>    
    //public int CompareTo(ProjectDto? other)
    //{
    //    if (prodFinishDate==other.prodFinishDate) return 0;
    //    else if (prodFinishDate>other.prodFinishDate) return 1;
    //    else return -1;
    //}


}