using System;
using System.Windows;
using System.Windows.Controls;
using DAL;
using Models;

namespace CompassWpf.View
{
    /// <summary>
    /// UserLoginView.xaml 的交互逻辑
    /// </summary>
    public partial class UserLoginView : Window
    {
        public UserLoginView()
        {
            InitializeComponent();
            cbxSbu.Items.Add("FoodService");
            cbxSbu.Items.Add("Marine");
            txtUserAccount.Text = Properties.Settings.Default.UserAccount;
            psbUserPwd.Password = Properties.Settings.Default.UserPwd;
            cbxSbu.Text = Properties.Settings.Default.Sbu;
        }
        private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            //验证信息
            if (InfoCheck(txtUserAccount)||InfoCheck(psbUserPwd))return;
            //封装用户实体信息
            User objUser = new User()
            {
                UserAccount = txtUserAccount.Text.Trim(),
                UserPwd = psbUserPwd.Password.Trim(),
                SBU = cbxSbu.Text == "FoodService" ? "" : cbxSbu.Text
            };
            //和后台交互判断信息是否正确
            try
            {
                UserService objUserService = new UserService();
                //执行查询并保存登陆信息
                App.currentUser = objUserService.UserLogin(objUser);
                if (App.currentUser != null)
                {
                    DialogResult = true;
                    //记住密码
                    if (ckbRememberMe.IsChecked==true)
                    {
                        Properties.Settings.Default.UserAccount = objUser.UserAccount;
                        Properties.Settings.Default.UserPwd = objUser.UserPwd;
                        Properties.Settings.Default.Sbu = objUser.SBU == "" ? "FoodService" : objUser.SBU;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Properties.Settings.Default.UserAccount = "";
                        Properties.Settings.Default.UserPwd = "";
                        Properties.Settings.Default.Sbu = "";
                        Properties.Settings.Default.Save();
                    }
                    //以后再封装权限信息
                    Close();
                }
                else
                {
                    tblState.Text = "账号或者密码错误，请重试";
                }
            }
            catch (Exception ex)
            {
                tblState.Text = "数据访问出现异常，登陆失败" + ex.Message;
            }
        }

        bool InfoCheck(Control ctrl)
        {
            int infoLength = 0;
            if (ctrl is TextBox box)
            {
                infoLength = box.Text.Trim().Length;
                tblState.Text = "请填写账户";
            }

            if (ctrl is PasswordBox psb)
            {
                infoLength = psb.Password.Trim().Length;
                tblState.Text = "请填写密码";
            }
            if (infoLength == 0)
            {
                ctrl.Focus();
                return true;
            }
            return false;
        }
        
    }
}
