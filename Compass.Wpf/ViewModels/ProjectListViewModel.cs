using Compass.Shared;
using Compass.Shared.Const;
using Compass.Shared.Dtos;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Wpf.ViewModels;

public class ProjectListViewModel : BindableBase
{
    #region 筛选框绑定数据，订单统计
    private string search = string.Empty;
    public string Search
    {
        get { return search; }
        set { search = value; RaisePropertyChanged(); }
    }
    public DelegateCommand QueryCommand { get; }
    private ProjectListSummaryDto summary;
    public ProjectListSummaryDto Summary
    {
        get { return summary; }
        set { summary = value; RaisePropertyChanged(); }
    }
    #endregion

    #region 项目状态下拉框
    private int selectedStatus;
    public int SelectedStatus
    {
        get { return selectedStatus; }
        set { selectedStatus = value; RaisePropertyChanged(); }
    }
    private ObservableCollection<ProjectStatusDto> projectStatuses;

    public ObservableCollection<ProjectStatusDto> ProjectStatuses
    {
        get { return projectStatuses; }
        set { projectStatuses = value; RaisePropertyChanged(); }
    }
    void InitProjectStatus()
    {
        ProjectStatuses=new ObservableCollection<ProjectStatusDto>();
        foreach (var item in Enum.GetValues(typeof(ProjectStatus_e)))
        {
            ProjectStatuses.Add(new ProjectStatusDto { ProjectStatus=(ProjectStatus_e)Enum.Parse(typeof(ProjectStatus_e), item.ToString()) });
        }
    }
    #endregion

    #region 筛选年月
    private DateTime yearMonth = DateTime.Now;
    public DateTime YearMonth
    {
        get { return yearMonth; }
        set { yearMonth = value; RaisePropertyChanged(); }
    }
    #endregion
    

    #region 模拟数据
    LocalDb localDb;
    private ObservableCollection<ProjectDto> projectList;
    public ObservableCollection<ProjectDto> ProjectList
    {
        get { return projectList; }
        set { projectList = value; RaisePropertyChanged(); }
    }


    #endregion
    public DelegateCommand AddCommand { get; }
    public ProjectListViewModel()
    {
        InitProjectStatus();
        localDb=new LocalDb();
        ProjectList=new ObservableCollection<ProjectDto>();
        Summary=new ProjectListSummaryDto();        
        QueryCommand=new DelegateCommand(Query);
        Query();
    }
    /// <summary>
    /// 执行查询
    /// </summary>
    private void Query()
    {
        var models = localDb.GetProjectsBySearch(Search);
        ProjectList.Clear();
        if (models != null)
        {
            models.ForEach(x => ProjectList.Add(x));
        }        
        foreach (var item in ProjectList)
        {            
            //内部排序：给状态记录和关键路径排序
            if (item.ProjectStatusRecords!=null)
                item.ProjectStatusRecords.Sort();
            if (item.CriticalPath!=null)
                item.CriticalPath.Sort();
        }
        //统计信息
        Summary.ProjectCount=ProjectList.Count();
        Summary.TotalHoodQuantity=ProjectList.Sum(x=>x.HoodQuantity);
        Summary.TotalItemLine=ProjectList.Sum(x=>x.ItemLine);
        Summary.TotalStdWorkload=ProjectList.Sum(x=>x.StdWorkload);
        Summary.TotalVtaValue=ProjectList.Sum(x=>x.VtaValue);
    }
    //点击工位进入【工位计划】界面，该界面显示工位的订单排程（所有项目执行的明细）

}
