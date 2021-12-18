/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:CompassWpf"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
//添加MVVMLight后报错修复
using CommonServiceLocator;


namespace CompassWpf.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            #region MVVMLight自己注释的代码
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////} 
            #endregion

            #region 注册ViewModel
            SimpleIoc.Default.Register<MainViewModel>(); 
            SimpleIoc.Default.Register<UserLoginViewModel>(); 



            #endregion
        }

        #region 实例化ViewModel
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public UserLoginViewModel UserLogin => ServiceLocator.Current.GetInstance<UserLoginViewModel>();




        #endregion

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}