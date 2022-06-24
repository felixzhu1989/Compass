using Compass.Wpf.Beta.Common;
using Compass.Wpf.Beta.ViewModels;
using Compass.Wpf.Beta.Views;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Compass.Wpf.Beta
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }
        protected override void OnInitialized()
        {
            var service = App.Current.MainWindow!.DataContext as IConfigureService;
            service!.Configure();
            base.OnInitialized();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ProjectsView, ProjectsViewModel>();
        }
    }
}
