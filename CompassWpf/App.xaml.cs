using System;
using System.Runtime.InteropServices;
using System.Windows;
using CompassWpf.View;
using System.Threading;
namespace CompassWpf
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        [DllImport("user32", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string cls, string win);
        [DllImport("user32")]
        static extern IntPtr SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32")]
        static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32")]
        static extern bool OpenIcon(IntPtr hWnd);
        //应用程序启动执行的事件
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            #region 只能运行一个程序
            new Mutex(true, "Singleton Compass", out bool canOpen);
            if (!canOpen)
            {
                ActivateOtherWindow();
                Shutdown();
            }
            void ActivateOtherWindow()
            {
                var other = FindWindow(null, "MainWindow");
                if (other != IntPtr.Zero)
                {
                    SetForegroundWindow(other);
                    if (IsIconic(other))
                        OpenIcon(other);
                }
            }
            #endregion

            #region 登陆逻辑
            MainView mainView = new MainView();
            UserLoginView userLoginView = new UserLoginView();
            if (userLoginView.ShowDialog() == true)
            {
                mainView.Show();
            }
            else
            {
                mainView.Close();
            } 
            #endregion
        }
        //保存登陆对象
        public static Models.User currentUser = null;
    }
}
