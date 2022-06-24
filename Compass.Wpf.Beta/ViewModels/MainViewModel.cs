using Compass.Wpf.Beta.Common;
using Compass.Wpf.Beta.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Wpf.Beta.ViewModels
{
    public class MainViewModel : BindableBase, IConfigureService
    {
        #region 字段
        
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal journal;
        #endregion

        #region 属性
        private ObservableCollection<MenuBar> menuBars;
        /// <summary>
        /// 菜单集合
        /// </summary>
        public ObservableCollection<MenuBar> MenuBars
        {
            get => menuBars;
            set { menuBars = value; RaisePropertyChanged(); }
        }
        private string title="欢迎使用COMPASS！";
        public string Title
        {
            get { return title; }
            set { title = value;RaisePropertyChanged(); }
        }       


        #endregion

        #region Command
        public DelegateCommand<MenuBar> NavigateCommand { get; }
        public DelegateCommand GoBackCommand { get; }
        public DelegateCommand GoForwardCommand { get; }
        public DelegateCommand HomeCommand { get; }
        #endregion

        public MainViewModel(IRegionManager regionManager)
        {
            this.regionManager=regionManager;
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal is { CanGoBack: true }) journal.GoBack();
            });
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal is { CanGoForward: true }) journal.GoForward();
            });
            HomeCommand=new DelegateCommand(() => { Navigate(MenuBars[0]); });
        }

        /// <summary>
        /// 菜单点击，页面导航
        /// </summary>        
        private void Navigate(MenuBar obj)
        {
            if (obj==null||string.IsNullOrWhiteSpace(obj.NameSpace)) return;
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
            {
                journal = back.Context.NavigationService.Journal;
            });
            Title=obj.Title;
        }

        /// <summary>
        /// 初始化菜单集合
        /// </summary>
        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar { Icon = "Home", Title = "项目列表", NameSpace = "ProjectsView" });
            MenuBars.Add(new MenuBar { Icon = "ClipboardTextClockOutline", Title = "制图计划", NameSpace = "DrawingPlanView" });
            MenuBars.Add(new MenuBar { Icon = "ProgressCheck", Title = "计划跟踪", NameSpace = "ProjectTrackingView" });
            MenuBars.Add(new MenuBar { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
        }

        public void Configure()
        {
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("ProjectsView");
        }
    }
}
