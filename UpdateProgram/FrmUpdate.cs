using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateProgram
{
    public partial class FrmUpdate : Form
    {
        private UpdateManager objUpdateManager = new UpdateManager();
        public FrmUpdate()
        {
            InitializeComponent();
            Init();
        }
        /// <summary>
        /// 更新文件
        /// </summary>
        private void Init()
        {
            //将同步显示进度的方法和委托变量关联
            objUpdateManager.ShowUpdateProgressDelegate = ShowUpdateProgress;
            //显示需要更新的文件列表
            List<string[]> fileList = objUpdateManager.NowUpdateInfo.FileList;
            foreach (var item in fileList)
            {
                lvUpdateList.Items.Add(new ListViewItem(item));
            }
            //显示当前版本号和最近一次更新的事件
            lblVersion.Text = objUpdateManager.LastUpdateInfo.Version;
            lblLastUpdateTime.Text = objUpdateManager.LastUpdateInfo.UpdateTime.ToString(CultureInfo.InvariantCulture);
        }

        public async Task UpdateFiles()
        {
            try
            {
                BtnStart.Visible=false;
                btnCancel.Visible = false;
                lblUpdateStatus.Text = "正在下载更新文件，请稍候...";
                //开始下载文件，同时异步显示下载的百分比
                objUpdateManager.DownloadFiles();
                objUpdateManager.CopyFiles();
                BtnFinish.Left = btnCancel.Left;
                BtnFinish.Visible=true;
                lblUpdateStatus.Text = "更新完成！";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void BtnStart_Click(object sender, EventArgs e)
        {
            await UpdateFiles();
        }

        private void BtnFinish_Click(object sender, EventArgs e)
        {
            //启动主程序
            System.Diagnostics.Process.Start("Compass.exe");
            //关闭升级程序
            Application.ExitThread();
            Application.Exit();
        }

        /// <summary>
        /// 根据委托定义一个同步显示下载百分比的方法
        /// </summary>
        /// <param name="fileIndex"></param>
        /// <param name="finishedPercent">已经完成的百分比</param>
        private void ShowUpdateProgress(int fileIndex, int finishedPercent)
        {
            //在列表中对应的文件信息最后显示下载的百分比
            lvUpdateList.Items[fileIndex].SubItems[3].Text = finishedPercent + "%";
            //进度条的显示
            pbDownLoadFile.Maximum = 100;
            pbDownLoadFile.Value = finishedPercent;
        }
        
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
