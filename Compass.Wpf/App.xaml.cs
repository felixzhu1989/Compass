using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Compass.Wpf.Common;
using Compass.Wpf.ViewModels;
using Compass.Wpf.Views;
using Prism.DryIoc;
using Prism.Ioc; 

namespace Compass.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            //创建启动页面
            return Container.Resolve<MainWindow>();
        }
        //依赖注入
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ProjectTrackingView,ProjectTrackingViewModel>();
            containerRegistry.RegisterForNavigation<ProductionPlanView, ProductionPlanViewModel>();
            containerRegistry.RegisterForNavigation<DrawingPlanView, DrawingPlanViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        }
        /// <summary>
        /// 初始化默认首页
        /// </summary>
        protected override void OnInitialized()
        {
            var service = Current.MainWindow!.DataContext as IConfigureService;
            service!.Configure();
            base.OnInitialized();
        }
    }
}
