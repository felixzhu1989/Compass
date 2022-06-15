using Compass.Wpf.Common;
using Compass.Wpf.Common.Models;
using Compass.Wpf.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Compass.Wpf.ViewModels;

public class MainViewModel : BindableBase, IConfigureService
{

    #region 字段
    private readonly IRegionManager regionManager;
    private IRegionNavigationJournal journal; 



    #endregion



    #region 属性
    public DelegateCommand<MenuBar> NavigateCommand { get; }
    public DelegateCommand GoBackCommand { get; }
    public DelegateCommand GoForwardCommand { get; }
    public DelegateCommand HomeCommand { get; }


    private ObservableCollection<MenuBar> menuBars;
    /// <summary>
    /// 菜单集合
    /// </summary>
    public ObservableCollection<MenuBar> MenuBars
    {
        get => menuBars;
        set { menuBars = value; RaisePropertyChanged(); }
    } 
    #endregion
    public MainViewModel(IRegionManager regionManager)
    {
        this.regionManager=regionManager;
        MenuBars = new ObservableCollection<MenuBar>();        
        NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
        HomeCommand=new DelegateCommand(HomePage);
        GoBackCommand = new DelegateCommand(() =>
        {
            if (journal is { CanGoBack: true }) journal.GoBack();
        });
        GoForwardCommand = new DelegateCommand(() =>
        {
            if (journal is { CanGoForward: true }) journal.GoForward();
        });
    }

    /// <summary>
    /// 首页
    /// </summary>
    private void HomePage()
    {
        Navigate(MenuBars[1]);
    }
    /// <summary>
    /// 页面导航
    /// </summary>
    /// <param name="obj"></param>
    private void Navigate(MenuBar obj)
    {
        if (obj==null||string.IsNullOrWhiteSpace(obj.NameSpace)) return;
        regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
        {
            journal=back.Context.NavigationService.Journal;
        });
    }

    /// <summary>
    /// 初始化菜单集合
    /// </summary>
    void CreateMenuBar()
    {
        MenuBars.Add(new MenuBar { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
        MenuBars.Add(new MenuBar { Icon = "FormatListBulleted", Title = "项目列表", NameSpace = "ProjectListView" });
        MenuBars.Add(new MenuBar { Icon = "WrenchClock", Title = "制图计划", NameSpace = "DrawingPlanView" });
        MenuBars.Add(new MenuBar { Icon = "Cog", Title = "系统设置", NameSpace = "SettingsView" });
    }
    /// <summary>
    /// 初始化配置默认首页
    /// </summary>
    public void Configure()
    {
        CreateMenuBar();
        regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("ProjectListView");
    }
}
