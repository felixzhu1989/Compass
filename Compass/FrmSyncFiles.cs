using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmSyncFiles : MetroFramework.Forms.MetroForm
    {
        private ProjectService objProjectService = new ProjectService();
        private string sbu = Program.ObjCurrentUser.SBU;
        string localPath;
        string publicPath;
        public FrmSyncFiles()
        {
            InitializeComponent();
            //项目编号下拉框
            cobODPNo.DataSource = objProjectService.GetProjectsByWhereSql("",sbu);
            cobODPNo.DisplayMember = "ODPNo";
            cobODPNo.ValueMember = "ProjectId";
            cobODPNo.SelectedIndex = -1;
            this.cobODPNo.SelectedIndexChanged += new System.EventHandler(this.CobODPNo_SelectedIndexChanged);
        }

        public FrmSyncFiles(string odpNo):this()
        {
            cobODPNo.Text = odpNo;
        }

        private void CobODPNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobODPNo.SelectedIndex == -1) return;
            Project objProject = objProjectService.GetProjectByODPNo(cobODPNo.Text, Program.ObjCurrentUser.SBU);
            txtBPONo.Text = objProject.BPONo;
            txtProjectName.Text = objProject.ProjectName;
            localPath = @"D:\MyProjects\" + objProject.ODPNo;
            string curYearPath =
                @"Z:\1-Project operation\20" + objProject.ODPNo.Substring(3, 2) + @" project\";
            if (!Directory.Exists(curYearPath))Directory.CreateDirectory(curYearPath);
            publicPath = curYearPath + objProject.ODPNo;
            if (!Directory.Exists(publicPath))
            {
                //获取文件夹列表，如果发现项目文件夹存在，但是被重命名了，则更改为默认规则（不带任何备注）
                string[] directorieStrings = Directory.GetDirectories(curYearPath);
                string oldPath = directorieStrings.FirstOrDefault(d => d.Contains(objProject.ODPNo));
                if (oldPath != null)
                {
                    DirectoryInfo di=new DirectoryInfo(oldPath);
                    di.MoveTo(publicPath);
                }
            }
            

            IniShow();
        }

        private void IniShow()
        {
            //清空列表
            lvwLocal.Items.Clear();
            lvwPublic.Items.Clear();
            //更新地址栏
            txtLocalPath.Text = localPath;
            txtPublicPath.Text = publicPath;

            if (Directory.Exists(localPath))ShowFilesList(lvwLocal, lblLocalCount, txtLocalPath, localPath);
            if (Directory.Exists(publicPath))ShowFilesList(lvwPublic, lblPublicCount, txtPublicPath, publicPath);
        }
        
        //在右窗体中显示指定路径下的所有文件/文件夹
        private void ShowFilesList(ListView lvwFiles,Label lblCount,TextBox txtPath,string path) 
        {
            //开始数据更新
            lvwFiles.BeginUpdate();
            //清空lvwFiles
            lvwFiles.Items.Clear();
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
                FileInfo[] fileInfos = directoryInfo.GetFiles();

                //列出所有文件夹
                foreach (DirectoryInfo dirInfo in directoryInfos)
                {
                    ListViewItem item = lvwFiles.Items.Add(dirInfo.Name, 0);//0是文件夹的图标
                    item.Tag = dirInfo.FullName;
                    item.SubItems.Add("文件夹");
                    item.SubItems.Add(dirInfo.LastWriteTime.ToString());
                }

                //列出所有文件
                foreach (FileInfo fileInfo in fileInfos)
                {
                    //添加图标到imagelist中
                    if (!imageList.Images.ContainsKey(fileInfo.Extension))
                    {
                        Icon fileIcon = GetSystemIcon.GetIconByFileName(fileInfo.FullName);
                        imageList.Images.Add(fileInfo.Extension, fileIcon);
                    }

                    ListViewItem item = lvwFiles.Items.Add(fileInfo.Name);
                    item.ImageKey = fileInfo.Extension;
                    item.Tag = fileInfo.FullName;
                    item.SubItems.Add(fileInfo.Extension);
                    item.SubItems.Add(fileInfo.LastWriteTime.ToString());
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //更新状态栏
            lblCount.Text = lvwFiles.Items.Count + " 个项目";
            //结束数据更新
            lvwFiles.EndUpdate();
        }

        private void BtnLocal_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtLocalPath.Text))
                Process.Start(txtLocalPath.Text);
            else MessageBox.Show("路径不存在！");
        }

        private void BtnPublic_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtPublicPath.Text))
                Process.Start(txtPublicPath.Text);
            else MessageBox.Show("路径不存在！");
        }
        
        private void Lvw_DoubleClick(object sender, EventArgs e)
        {
            ListView lvw = (ListView)sender;
            if (lvw.SelectedItems.Count == 0) return;
            ListViewItem currentItem = lvw.SelectedItems[0];
            Process.Start(currentItem.Tag.ToString());

        }
        /// <summary>
        /// 同步到公共盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLocalToPublic_Click(object sender, EventArgs e)
        {
            btnLocalToPublic.Text = "正在同步...";
            btnLocalToPublic.Enabled = false;
            
            //判断文件夹是否存在，不存在则创建
            if (!Directory.Exists(publicPath))Directory.CreateDirectory(publicPath);
            
            //拷贝文件/文件夹
            if (CopyDirectory(localPath, publicPath, true)) MessageBox.Show("同步完成");
            //更新显示
            IniShow();
            btnLocalToPublic.Enabled = true;
            btnLocalToPublic.Text = "---->> 同步到公共盘 ---->>";
        }
        /// <summary>
        /// 同步到本地盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPublicToLocal_Click(object sender, EventArgs e)
        {
            btnPublicToLocal.Text = "正在同步...";
            btnPublicToLocal.Enabled = false;
            
            if (!Directory.Exists(localPath))Directory.CreateDirectory(localPath);
            if(CopyDirectory(publicPath, localPath,true)) MessageBox.Show("同步完成");
            IniShow();
            btnPublicToLocal.Enabled = true;
            btnPublicToLocal.Text = "<<---- 同步到本地盘 <<----";
        }
        
        /// <summary>
        /// 文件夹下所有内容copy
        /// </summary>
        /// <param name="sourcePath">要Copy的文件夹</param>
        /// <param name="targetPath">要复制到哪个地方</param>
        /// <param name="overWrite">是否覆盖</param>
        /// <returns></returns>
        private bool CopyDirectory(string sourcePath, string targetPath, bool overWrite)
        {
            bool ret = false;
            try
            {
                sourcePath = sourcePath.EndsWith(@"\") ? sourcePath : sourcePath + @"\";
                targetPath = targetPath.EndsWith(@"\") ? targetPath : targetPath + @"\";

                if (Directory.Exists(sourcePath))
                {
                    if (Directory.Exists(targetPath) == false)
                        Directory.CreateDirectory(targetPath);

                    foreach (string fls in Directory.GetFiles(sourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        flinfo.CopyTo(targetPath + flinfo.Name, overWrite);
                    }
                    foreach (string drs in Directory.GetDirectories(sourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, targetPath + drinfo.Name, overWrite) == false)
                            ret = false;
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
                Debug.Print(ex.Message);
            }
            return ret;
        }
    }
}
