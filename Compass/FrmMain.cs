using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using MetroFramework.Forms;
using Models;
using SolidWorks.Interop.swconst;
using UpdateProgram;

namespace Compass
{
    //声明委托
    public delegate void ShowDrawingPlanInfoDelegate();
    //显示项目信息委托
    public delegate void ShowProjectInfoDelegate(string odpNo);
    //显示项目列表委托
    public delegate void ShowProjectsDelegate(string id);
    //显示模型树委托
    public delegate void ShowModelTreeDelegate(string id);
    //【1】定义委托，快速浏览制图参数委托
    public delegate void QuickBrowseDelegate(Drawing drawing, ModuleTree tree);


    public partial class FrmMain : MetroFramework.Forms.MetroForm
    {
        public FrmMain()
        {
            InitializeComponent();
            //获取版本号
            FileStream myFile = new FileStream("UpdateList.xml", FileMode.Open);
            XmlTextReader xmlReader = new XmlTextReader(myFile);
            while (xmlReader.Read())
            {
                switch (xmlReader.Name)
                {
                    case "Version":
                        this.lblVersion.Text = "当前版本："+ xmlReader.GetAttribute("Num");
                        break;
                    case "UpdateTime":
                        this.lblUpdateTime.Text = "更新日期：" + xmlReader.GetAttribute("Date");
                        break;
                    default:
                        break;
                }
            }
            xmlReader.Close();
            myFile.Close();
            this.lblCurrentUser.Text = "当前用户：" + Program.ObjCurrentUser.UserAccount;
            tsbProjectList_Click(null, null);
            SetTsmiVisible();
        }

        private void SetTsmiVisible()
        {
            switch (Program.ObjCurrentUser.UserGroupId)
            {
                case 1:
                    break;
                default:
                    tsbCategories.Visible = false;
                    tsbWorkLoad.Visible = false;
                    tsbStatusTypes.Visible = false;
                    break;
            }
            
        }


        #region 关闭当前已经嵌入的窗体，嵌入新的窗体
        /// <summary>
        /// 关闭当前已经嵌入的窗体
        /// </summary>
        private void ClosePreForm(Panel panel)
        {
            foreach (Control item in panel.Controls)
            {
                if (item is Form)
                {
                    Form objControl = (Form)item;
                    objControl.Close();
                }
            }
        }
        private void OpenForm(Form objForm, Panel panel)
        {
            objForm.TopLevel = false;
            objForm.FormBorderStyle = FormBorderStyle.None;
            objForm.Parent = panel;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        #endregion

        #region 系统后台菜单
        private void tsbUsersManage_Click(object sender, EventArgs e)
        {
            ClosePreForm(splitContainer.Panel2);
            OpenForm(new FrmUserManage(Program.ObjCurrentUser), splitContainer.Panel2);
        }

        private void tsbCategories_Click(object sender, EventArgs e)
        {
            ClosePreForm(splitContainer.Panel2);
            OpenForm(new FrmCategories(), splitContainer.Panel2);
        }
        private void tsbDXFCutList_Click(object sender, EventArgs e)
        {
            ClosePreForm(splitContainer.Panel2);
            OpenForm(new FrmDXFCutList(), splitContainer.Panel2);
        }

        private void tsbWorkLoad_Click(object sender, EventArgs e)
        {
            ClosePreForm(splitContainer.Panel2);
            OpenForm(new FrmDesignWorkload(), splitContainer.Panel2);
        }

        private void tsbStatusTypes_Click(object sender, EventArgs e)
        {
            ClosePreForm(splitContainer.Panel2);
            OpenForm(new FrmStatusTypes(), splitContainer.Panel2);
        }
        /// <summary>
        /// 天花烟罩发货清单配件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbCeilingAccessories_Click(object sender, EventArgs e)
        {
            ClosePreForm(splitContainer.Panel2);
            OpenForm(new FrmCeilingAccessories(), splitContainer.Panel2);
        }
        #endregion


        #region 项目管理菜单
        /// <summary>
        /// 添加计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbDarwingPlan_Click(object sender, EventArgs e)
        {
            ClosePreForm(splitContainer.Panel2);
            FrmDrawingPlan objFrmDrawingPlan = new FrmDrawingPlan();
            //关联委托方法与委托对象
            objFrmDrawingPlan.showDrawingPlanInfoDelegate = ShowDrawingPlanInfo;
            OpenForm(objFrmDrawingPlan, splitContainer.Panel2);
        }
        //根据委托创建方法
        private void ShowDrawingPlanInfo()
        {
            ClosePreForm(splitContainer.Panel1);
            OpenForm(new FrmDrawingPlanInfo(), splitContainer.Panel1);
        }
        /// <summary>
        /// 项目跟踪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbProjectTracking_Click(object sender, EventArgs e)
        {
            ClosePreForm(splitContainer.Panel2);
            OpenForm(new FrmProjectTracking(), splitContainer.Panel2);
        }

        #endregion

        #region 项目信息菜单
        private void tsbProjectList_Click(object sender, EventArgs e)
        {
            ClosePreForm(splitContainer.Panel2);
            //关联委托方法与委托变量
            FrmProject objFrmProject = new FrmProject();
            objFrmProject.ShowProjectInfoDeg = this.ShowProjectInfo;
            objFrmProject.ShowModelTreeDeg = this.ShowModuleTree;
            OpenForm(objFrmProject, splitContainer.Panel2);
        }

        private void tsbProjectInfo_Click(object sender, EventArgs e)
        {
            ClosePreForm(splitContainer.Panel2);
            //关联委托方法与委托变量
            FrmProjectInfo objFrmProjectInfo = new FrmProjectInfo();
            objFrmProjectInfo.ShowProjectsDeg = this.ShowProjects;
            objFrmProjectInfo.ShowModelTreeDeg = this.ShowModuleTree;
            OpenForm(objFrmProjectInfo, splitContainer.Panel2);
        }

        



        //根据委托创建方法
        private void ShowProjectInfo(string odpNo)
        {
            ClosePreForm(splitContainer.Panel2);
            FrmProjectInfo objFrmProjectInfo = new FrmProjectInfo(odpNo);
            objFrmProjectInfo.ShowProjectsDeg = this.ShowProjects;
            objFrmProjectInfo.ShowModelTreeDeg = this.ShowModuleTree;
            OpenForm(objFrmProjectInfo, splitContainer.Panel2);
        }
        private void ShowModuleTree(string odpNo)
        {
            ClosePreForm(splitContainer.Panel1);
            FrmModuleTree objFrmModuleTree = new FrmModuleTree(odpNo);
            //【4】关联委托变量
            objFrmModuleTree.QuickBrowseDeg = this.QuickBrowse;
            objFrmModuleTree.ShowProjectInfoDeg = this.ShowProjectInfo;
            OpenForm(objFrmModuleTree, splitContainer.Panel1);
        }
        private void ShowProjects(string id)
        {
            ClosePreForm(splitContainer.Panel2);
            FrmProject objFrmProject = new FrmProject();
            objFrmProject.ShowProjectInfoDeg = this.ShowProjectInfo;
            objFrmProject.ShowModelTreeDeg = this.ShowModuleTree;
            OpenForm(objFrmProject, splitContainer.Panel2);
        }

        //【2】根据委托创建方法
        private void QuickBrowse(Drawing drawing, ModuleTree tree)
        {
            ClosePreForm(splitContainer.Panel2);
            FrmQuickBrowse objFrmQuickBrowse = new FrmQuickBrowse(drawing, tree);
            OpenForm(objFrmQuickBrowse, splitContainer.Panel2);
        }
        #endregion



        /// <summary>
        /// 帮助文档链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llblHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "http://10.9.18.31:8080/");
        }
        private void llblHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/felixzhu1989/Compass/commits/main");
        }
        /// <summary>
        /// 退出询问
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确认退出程序吗？", "退出询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK) e.Cancel = true;
        }







        #region SolidWorks自动绘图
        private void tsbHoodAutoDrawing_Click(object sender, EventArgs e)
        {
            FrmHoodAutoDrawing objFrmHoodAutoDrawing = new FrmHoodAutoDrawing();
            objFrmHoodAutoDrawing.Show();
        }

        private void tsbCeilingAutoDrawing_Click(object sender, EventArgs e)
        {
            FrmCeilingAutoDrawing objFrmCeilingAutoDrawing = new FrmCeilingAutoDrawing();
            objFrmCeilingAutoDrawing.Show();
        }
        #endregion
        /// <summary>
        /// 升级程序入口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbUpdate_Click(object sender, EventArgs e)
        {
            UpdateManager objUpdateManager = null;
            try
            {
                //因为需要联网下载最新的更新文件，有可能出错
                objUpdateManager = new UpdateManager();
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法连接服务器，请检查网络连接，错误：" + ex.Message);
                return;
            }
            if (!objUpdateManager.IsUpdate)
            {
                MessageBox.Show("当前版本已经是最新，不需要升级！", "提示信息");
                return;
            }
            if (MessageBox.Show("为了更新文件，将退出当前程序，请确保数据已经保存，确认退出吗？", "取消询问", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.Cancel) return;
            Application.Exit();
            //启动升级程序
            System.Diagnostics.Process.Start("UpdateProgram.exe");
        }
        /// <summary>
        /// 程序启动后，自动检查更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            UpdateManager objUpdateManager = null;
            try
            {
                //因为需要联网下载最新的更新文件，有可能出错
                objUpdateManager = new UpdateManager();
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法连接服务器，请检查网络连接，错误：" + ex.Message);
                return;
            }
            if (objUpdateManager.IsUpdate)
            {
                FrmTips objFrmTips=new FrmTips();
                objFrmTips.Show();
            }
            this.timerUpdate.Enabled = false;//停止定时器，防止频繁弹出
        }

        
    }
}
