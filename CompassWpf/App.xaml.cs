using System.Windows;
using CompassWpf.View;
using System.Threading;
using CompassWpf.ViewModel;

namespace CompassWpf
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //应用程序启动执行的事件
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            #region 只能运行一个程序
            new Mutex(true, "Singleton Compass", out bool canOpen);
            if (!canOpen)
            {
                MessageBox.Show("请不要运行多个COMPASS程序", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                Shutdown();
            }
            #endregion

            #region 登陆逻辑
            MainView mainView = new MainView();
            UserLoginView userLoginView = new UserLoginView();
            //关闭窗口委托
            if (UserLoginViewModel.CloseAction == null) UserLoginViewModel.CloseAction = userLoginView.Close;
            userLoginView.ShowDialog();
            if (UserLoginViewModel.LoginResult) mainView.Show();
            else mainView.Close();
            #endregion
        }
        //保存登陆对象
        public static Models.User currentUser = null;
    }
}
