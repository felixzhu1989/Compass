using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Common
{
    public class ShowMsg
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

        [DllImport("user32.dll")]
        static extern bool EndDialog(IntPtr hDlg, out IntPtr nResult);

        //三个参数：1、文本提示-text，2、提示框标题-caption，3、按钮类型-MessageBoxButtons ，4、自动消失时间设置-timeout
        public DialogResult ShowMessageBoxTimeout(string text, string caption,
            MessageBoxButtons buttons, int timeout)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(CloseMessageBox),
                new CloseState(caption, timeout));
            DialogResult result = MessageBox.Show(text, caption, buttons);
            if (result == DialogResult.Yes) return DialogResult.Yes;
            else return result;
        }

        private static void CloseMessageBox(object state)
        {
            CloseState closeState = state as CloseState;

            Thread.Sleep(closeState.Timeout);
            IntPtr dlg = FindWindow(null, closeState.Caption);

            if (dlg != IntPtr.Zero)
            {
                IntPtr result;
                EndDialog(dlg, out result);
            }
        }
    }
}
