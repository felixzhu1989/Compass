using System;
using System.Collections.Generic;
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
        /// 初始化
        /// </summary>
        private void Init()
        {
            //设置相关按钮
            btnFinish.Visible = false;
            //将同步显示进度的方法和委托变量关联
            objUpdateManager.ShowUpdateProgressDelegate = this.ShowUpdateProgress;
            //显示需要更新的文件列表
            List<string[]> fileList = objUpdateManager.NowUpdateInfo.FileList;
            foreach (var item in fileList)
            {
                this.lvUpdateList.Items.Add(new ListViewItem(item));
            }
            //显示当前版本号和最近一次更新的事件
            this.lblVersion.Text = objUpdateManager.LastUpdateInfo.Version;
            this.lblLastUpdateTime.Text = objUpdateManager.LastUpdateInfo.UpdateTime.ToString();
        }
        /// <summary>
        /// 根据委托定义一个同步显示下载百分比的方法
        /// </summary>
        /// <param name="fileIndex"></param>
        /// <param name="finishedPercent">已经完成的百分比</param>
        private void ShowUpdateProgress(int fileIndex, int finishedPercent)
        {
            //在列表中对应的文件信息最后显示下载的百分比
            this.lvUpdateList.Items[fileIndex].SubItems[3].Text = finishedPercent + "%";
            //进度条的显示
            this.pbDownLoadFile.Maximum = 100;
            this.pbDownLoadFile.Value = finishedPercent;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                if (objUpdateManager.CopyFiles()) //调用复制文件方法，复制新文件到程序根目录
                {
                   //启动主程序
                    System.Diagnostics.Process.Start("Compass.exe");
                    //关闭升级程序
                    Application.ExitThread();
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 下一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            //this.btnNext.Enabled = false;
            try
            {
                lblUpdateStatus.Text = "正在下载更新文件，请稍候...";
                lblTips.Text = "点击“取消”可以结束升级...";
                //this.btnClose.Enabled = false;//升级过程中禁止关闭窗口
                //开始下载文件，同时异步显示下载的百分比
                this.objUpdateManager.DownloadFiles();

                this.lblTips.Text = "全部文件已下载，点击“完成”结束升级...";
                this.lblUpdateStatus.Visible = false;
                this.btnNext.Visible = false;
                this.btnCancel.Visible = false;
                this.btnFinish.Location = this.btnCancel.Location;//将完成按钮移动至右边
                this.btnFinish.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认取消升级吗？", "取消询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) ==
                DialogResult.OK)this.Close();
        }
    }
}
