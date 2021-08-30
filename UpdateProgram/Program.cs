using System;
using System.Windows.Forms;

namespace UpdateProgram
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
            FrmUpdateStart frmUpdateStart=new FrmUpdateStart();
            DialogResult result = frmUpdateStart.ShowDialog();
            if(result==DialogResult.OK)
                Application.Run(new FrmUpdate());
            else
                Application.Exit();
        }
    }
}
