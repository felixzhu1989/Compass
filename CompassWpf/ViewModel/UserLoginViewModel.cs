using System;
using System.Collections.Generic;
using DAL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Models;

namespace CompassWpf.ViewModel
{
    public class UserLoginViewModel : ViewModelBase
    {
        public UserLoginViewModel()
        {
            InitUser();
            InitSbuList();
            RememberMe = true;
            Info = "准备就绪...";
            LoginResult = false;
        }
        #region 登陆用户信息
        private User user;
        public User User
        {
            get => user;
            set { user = value; RaisePropertyChanged(() => User); }
        }
        private void InitUser()
        {
            User = new User
            {
                UserAccount = Properties.Settings.Default.UserAccount,
                UserPwd = Properties.Settings.Default.UserPwd,
                SBU = Properties.Settings.Default.Sbu
            };
        }
        #endregion

        #region SBU下拉框
        private List<string> sbuList;
        public List<string> SbuList
        {
            get => sbuList;
            set { sbuList = value; RaisePropertyChanged(() => SbuList); }
        }
        private void InitSbuList()
        {
            SbuList = new List<string>() { "FoodService", "Marine" };//,"SBA"
        }
        #endregion

        #region 记住密码复选框
        private bool rememberMe;
        public bool RememberMe
        {
            get => rememberMe;
            set { rememberMe = value; RaisePropertyChanged(() => RememberMe); }
        }
        #endregion

        #region 提示信息
        private string info;
        public string Info
        {
            get => info;
            set { info = value; RaisePropertyChanged(() => Info); }
        }
        #endregion

        #region 登陆按钮命令
        public static Action CloseAction { get; set; }
        public static bool LoginResult { get; set; } = false;

        private RelayCommand submitCmd;
        public RelayCommand SubmitCmd
        {
            get
            {
                if (submitCmd == null) return new RelayCommand(() => ExecuteLogin());
                return submitCmd;
            }
            set => submitCmd = value;
        }
        private void ExecuteLogin()
        {
            if (ValidateData()) return;
            if (User.UserAccount == "test")
            {
                #region test用户绕开数据库验证直接登陆
                App.currentUser = User;
                LoginResult = true;
                //关闭窗口
                CloseAction();
                #endregion
            }
            else
            {
                //和后台交互判断信息是否正确
                try
                {
                    UserService objUserService = new UserService();
                    //执行查询并保存登陆信息
                    App.currentUser = objUserService.UserLogin(User);
                    if (App.currentUser != null)
                    {
                        LoginResult = true;
                        SaveProperties();
                        //关闭窗口
                        CloseAction();
                    }
                    else
                    {
                        Info = "账号或者密码错误，请重试";
                    }
                }
                catch (Exception ex)
                {
                    Info = "数据访问出现异常，登陆失败" + ex.Message;
                }
            }
        }
        /// <summary>
        /// 是否可执行
        /// </summary>
        private bool ValidateData()
        {
            if (user.UserAccount.Length == 0)
            {
                Info = "请填写账户";
                return true;
            }
            if (user.UserPwd.Length == 0)
            {
                Info = "请填写密码";
                return true;
            }
            if (user.SBU.Length == 0)
            {
                Info = "请选择事业部";
                return true;
            }
            return false;
        }
        //记住密码
        private void SaveProperties()
        {
            if (rememberMe)
            {
                Properties.Settings.Default.UserAccount = User.UserAccount;
                Properties.Settings.Default.UserPwd = User.UserPwd;
                Properties.Settings.Default.Sbu = User.SBU == "" ? "FoodService" : User.SBU;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.UserAccount = "";
                Properties.Settings.Default.UserPwd = "";
                Properties.Settings.Default.Sbu = "";
                Properties.Settings.Default.Save();
            }
        }
        #endregion

        
    }
}
