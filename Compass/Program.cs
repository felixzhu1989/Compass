using System;
using System.Threading;
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
            #region 只能运行一个程序
            bool canOpen;
            Mutex mutex = new Mutex(true, Application.ProductName, out canOpen);
            if (!canOpen)
            {
                MessageBox.Show(null, "请不要同时运行多个本程序！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //   提示信息，可以删除。
                return;//退出程序
            } 
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //显示登陆窗体
            FrmUserLogin objFrmUserLogin = new FrmUserLogin();
            DialogResult result = objFrmUserLogin.ShowDialog();
            //判断登陆是否成功
            if (result == DialogResult.OK)
            {
                Application.Run(new FrmMain());
            }
            else
            {
                Application.Exit();
            }

        }
        //定义一个全局变量，用来保存登陆对象
        public static User ObjCurrentUser = null;
    }
}
