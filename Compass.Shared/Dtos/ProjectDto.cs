using Compass.Shared.Const;

namespace Compass.Shared.Dtos;
public class ProjectDto : BaseDto,IComparable<ProjectDto>
{
    #region 项目信息
    private string poNumber;
    public string PoNumber
    {
        get { return poNumber; }
        set { poNumber = value; OnPropertyChanged(); }
    }
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

    #region 项目概况
    private DateTime odpReleaseDate;
    public DateTime OdpReleaseDate
    {
        get { return odpReleaseDate; }
        set { odpReleaseDate = value; OnPropertyChanged(); }
    }
    private DateTime prodFinishDate;
    public DateTime ProdFinishDate
    {
        get { return prodFinishDate; }
        set { prodFinishDate = value; OnPropertyChanged(); }
    }
    private int hoodQuantity;
    public int HoodQuantity
    {
        get { return hoodQuantity; }
        set { hoodQuantity = value; OnPropertyChanged(); }
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
    private string modelSummary;
    public string ModelSummary
    {
        get { return modelSummary; }
        set { modelSummary = value; OnPropertyChanged(); }
    }
    #endregion

    #region 项目状态记录
    private List<ProjectStatusRecordDto> projectStatusRecords;
    public List<ProjectStatusRecordDto> ProjectStatusRecords
    {
        get { return projectStatusRecords; }
        set { projectStatusRecords = value; OnPropertyChanged(); }
    }
    #endregion

    #region 项目属性  
    private ProjectType_e projectType;
    public ProjectType_e ProjectType
    {
        get { return projectType; }
        set { projectType = value; OnPropertyChanged(); }
    }
    private CustomerType_e customerType;
    public CustomerType_e CustomerType
    {
        get { return customerType; }
        set { customerType = value; OnPropertyChanged(); }
    }
    private int riskLevel;
    public int RiskLevel
    {
        get { return riskLevel; }
        set { riskLevel = value; OnPropertyChanged(); }
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
    #endregion

    #region 特殊要求
    private string projectSpecialRequire;
    public string ProjectSpecialRequire
    {
        get { return projectSpecialRequire; }
        set { projectSpecialRequire = value; OnPropertyChanged(); }
    }
    private List<ItemSpecialRequireDto> itemSpecialRequires;
    public List<ItemSpecialRequireDto> ItemSpecialRequires
    {
        get { return itemSpecialRequires; }
        set { itemSpecialRequires = value; OnPropertyChanged(); }
    }
    #endregion

    #region 运行状态(关键路径)
    private List<ActivityDto> criticalPath;
    public List<ActivityDto> CriticalPath
    {
        get { return criticalPath; }
        set { criticalPath = value; OnPropertyChanged(); }
    }
    #endregion

    #region 财务信息
    private decimal vtaValue;
    public decimal VtaValue
    {
        get { return vtaValue; }
        set { vtaValue = value; OnPropertyChanged(); }
    }
    private DateTime invoiceMonth;
    public DateTime InvoiceMonth
    {
        get { return invoiceMonth; }
        set { invoiceMonth = value; OnPropertyChanged(); }
    }
    private CustomerDto customer;
    public CustomerDto Customer
    {
        get { return customer; }
        set { customer = value; OnPropertyChanged(); }
    }
    /// <summary>
    /// 按照完工日期排序
    /// </summary>    
    public int CompareTo(ProjectDto? other)
    {       
        if (prodFinishDate==other.prodFinishDate) return 0;
        else if (prodFinishDate>other.prodFinishDate) return 1;
        else return -1;
    }
    #endregion

}