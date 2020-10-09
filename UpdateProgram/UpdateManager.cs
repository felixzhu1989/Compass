using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml;
using System.Windows.Forms;

namespace UpdateProgram
{
    /// <summary>
    /// 升级管理器核心业务类
    /// </summary>
    public class UpdateManager
    {
        /// <summary>
        /// 构造方法，初始化对象
        /// </summary>
        public UpdateManager()
        {
            //1.初始化对象属性
            this.LastUpdateInfo = new UpdateInfo();
            this.NowUpdateInfo = new UpdateInfo();
            //2.给属性赋值
            GetLastUpdateInfo();
            GetNewUpdateInfo();
        }
        //属性
        public UpdateInfo LastUpdateInfo { get; set; }//上次更新的信息
        public UpdateInfo NowUpdateInfo { get; set; }//当前的更新信息
        public bool IsUpdate//判断是否需要更新，用更新日期比较
        {
            get
            {
                DateTime dt1 = Convert.ToDateTime(this.LastUpdateInfo.UpdateTime);
                DateTime dt2 = Convert.ToDateTime(this.NowUpdateInfo.UpdateTime);
                return dt2 > dt1;
            }
        }
        public string TempFilePath//下载文件缓存目录
        {
            get
            {
                string newTempPath = Environment.GetEnvironmentVariable("Temp") + "\\updatefiles";
                if (!Directory.Exists(newTempPath)) Directory.CreateDirectory(newTempPath);
                return newTempPath;
            }
        }
        //方法
        /// <summary>
        /// 从本地获取上次更新的信息并封装到属性【服务器url，版本，更新时间】
        /// </summary>
        private void GetLastUpdateInfo()
        {
            //封装上次更新的信息
            FileStream myFile = new FileStream("UpdateList.xml", FileMode.Open);
            XmlTextReader xmlReader = new XmlTextReader(myFile);
            while (xmlReader.Read())
            {
                switch (xmlReader.Name)
                {
                    case "URLAddress":
                        this.LastUpdateInfo.UpdateFileUrl = xmlReader.GetAttribute("URL");
                        break;
                    case "Version":
                        this.LastUpdateInfo.Version = xmlReader.GetAttribute("Num");
                        break;
                    case "UpdateTime":
                        this.LastUpdateInfo.UpdateTime = Convert.ToDateTime(xmlReader.GetAttribute("Date"));
                        break;
                    default:
                        break;
                }
            }
            xmlReader.Close();
            myFile.Close();
        }
        /// <summary>
        /// 从远程服务器上下载文件，然后获取当前更新的信息并封装到属性【服务器url，版本，更新时间】
        /// </summary>
        private void GetNewUpdateInfo()
        {
            //下载最新的更新文件，临时目录
            string newXmlTempPath = TempFilePath + "\\UpdateList.xml";
            //从网络下载文件
            WebClient objClient = new WebClient();
            objClient.DownloadFile(LastUpdateInfo.UpdateFileUrl + "\\UpdateList.xml", newXmlTempPath);
            //封装当前更新的信息
            FileStream myFile = new FileStream(newXmlTempPath, FileMode.Open);
            XmlTextReader xmlReader = new XmlTextReader(myFile);
            this.NowUpdateInfo.FileList = new List<string[]>();//因为这个是集合对象，使用前必须初始化
            while (xmlReader.Read())
            {
                switch (xmlReader.Name)
                {
                    case "URLAddress":
                        this.NowUpdateInfo.UpdateFileUrl = xmlReader.GetAttribute("URL");
                        break;
                    case "Version":
                        this.NowUpdateInfo.Version = xmlReader.GetAttribute("Num");
                        break;
                    case "UpdateTime":
                        this.NowUpdateInfo.UpdateTime =Convert.ToDateTime(xmlReader.GetAttribute("Date"));
                        break;
                    case "UpdateFile":
                        string ver = xmlReader.GetAttribute("Ver");
                        string fileName = xmlReader.GetAttribute("FileName");
                        string contentLength = xmlReader.GetAttribute("ContentLength");
                        this.NowUpdateInfo.FileList.Add(new string[] { fileName, contentLength, ver, "0" });
                        break;
                    default:
                        break;
                }
            }
            xmlReader.Close();
            myFile.Close();
        }
        //创建委托往下载程序窗口中返回百分比数据(同步显示)，【下载顺序，下载百分比】
        public delegate void ShowUpdateProgress(int fileIndex,int finishedPercent);
        //创建委托对象，然后在窗体中关联
        public ShowUpdateProgress ShowUpdateProgressDelegate;

        /// <summary>
        /// 根据更新文件列表，下载更新文件，并同步显示下载的百分比
        /// </summary>
        public void DownloadFiles()
        {
            List<string[]> fileList = this.NowUpdateInfo.FileList;//文件列表，为了使用方便，用变量代替属性
            for (int i = 0; i < fileList.Count; i++)
            {
                //连接远程服务器中的指定文件，并准备读取
                string fileName = fileList[i][0];//当前需要下载的文件名
                string fileUrl = this.NowUpdateInfo.UpdateFileUrl + fileName;//当前需要下载的文件的URL
                //Web服务器
                WebRequest objWebRequest = WebRequest.Create(fileUrl);//根据文件的url连接服务器创建请求对象
                WebResponse objResponse = objWebRequest.GetResponse();//根据请求对象创建响应对象
                Stream objStream = objResponse.GetResponseStream();//通过响应对象返回数据流对象
                StreamReader objReader = new StreamReader(objStream);//用数据流对象最为参数创建留读取器对象
                //在先读取已经远程连接的远程文件，并基于委托反馈文件读取下载进度
                long fileLength = objResponse.ContentLength;//通过响应获取对象接收的数据长度

                //Ftp服务器
                //FtpWebRequest objFtpWebRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                //FtpWebResponse objFtpWebResponse = (FtpWebResponse)objFtpWebRequest.GetResponse();
                //Stream objStream = objFtpWebResponse.GetResponseStream();
                //StreamReader objReader = new StreamReader(objStream);
                //long fileLength = objFtpWebResponse.ContentLength;

                byte[] bufferbyte=new byte[fileLength];//根据当前的字节数，创建字节数组
                int allByte = bufferbyte.Length;//得到总字节数，为了显示进度
                int startByte = 0;//表示第一个字节
                while (fileLength>0)
                {
                    //跨线程访问可视化控件
                    //同步显示，表示允许在该线程中同时处理其他事件，如果没有该语句，则同步更新百分比无法实现
                    Application.DoEvents();
                    int downloadByte = objStream.Read(bufferbyte, startByte, allByte);//开始读取字节流
                    if(downloadByte==0)break;
                    startByte += downloadByte;//累加已经下载的字节数
                    allByte -= downloadByte;//未下载的字节数
                    //计算完成的百分比（整数）
                    float part = (float) startByte / 1024;
                    float total = (float) bufferbyte.Length / 1024;
                    int percent = Convert.ToInt32((part / total) * 100);
                    //通过委托变量显示更新的百分比,实际调用的是窗体中的同步显示百分比的方法
                    ShowUpdateProgressDelegate(i, percent);
                }
                //保存读取完成的文件
                string newFileName = this.TempFilePath + "\\" + fileName;
                FileStream fs=new FileStream(newFileName,FileMode.OpenOrCreate,FileAccess.Write);
                fs.Write(bufferbyte,0,bufferbyte.Length);
                objStream.Close();
                objReader.Close();
                fs.Close();
            }
        }
        /// <summary>
        /// 将下载在临时目录中的文件，复制到应用程序目录
        /// </summary>
        /// <returns></returns>
        public bool CopyFiles()
        {
            string[] files = Directory.GetFiles(TempFilePath);
            foreach (var name in files)
            {
                string currentFile = name.Substring(name.LastIndexOf(@"\") + 1);
                //如果文件在程序目录中已经存在，则删除，避免弹窗中断程序
                if (File.Exists(currentFile)) File.Delete(currentFile);
                File.Copy(name,currentFile);
            }
            return true;
        }
    }
}
