using System.Windows;

namespace CompassWpf.View
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = $"COMPASS.{App.currentUser.SBU}";
            tblCurrentSbu.Text = $"当前事业部：{App.currentUser.SBU}";
            tblCurrentUser.Text = $"登陆用户：{App.currentUser.UserAccount}";
        }
        private void TsmiProjects_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        
    }
}
