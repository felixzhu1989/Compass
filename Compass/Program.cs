using System;
using System.Windows.Forms;
using Models;

namespace Compass
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //显示登陆窗体
            FrmUserLogin objFrmUserLogin=new FrmUserLogin();
            DialogResult result = objFrmUserLogin.ShowDialog();
            //判断登陆是否成功
            if (result == DialogResult.OK) Application.Run(new FrmMain());
            else Application.Exit();
        }
        //定义一个全局变量，用来保存登陆对象
        public static User ObjCurrentUser = null;
    }
}
