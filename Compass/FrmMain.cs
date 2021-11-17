using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Models;
using UpdateProgram;

namespace Compass
{
    public partial class FrmMain : Form
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
                        this.lblVersion.Text = "当前版本：" + xmlReader.GetAttribute("Num");
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
            this.lblCurrentUser.Text = "登陆用户：" + Program.ObjCurrentUser.UserAccount;
            string currentSbu = Program.ObjCurrentUser.SBU == "" ? "FoodService" : Program.ObjCurrentUser.SBU;
            this.lblCurrentSBU.Text = "当前事业部：" + currentSbu;
            this.Text = "COMPASS." + currentSbu;
            
            SetPermissions();
            //隐藏测试代码
            tsmiTestCode.Visible = false;

            InitialForm();
        }
        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            //管理员和技术部才能显示的菜单
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2)
            {
                tsmiCeilingAccessories.Visible = true;
                tsmiDXFCutList.Visible = true;
                // 管理员才能显示的菜单
                if (Program.ObjCurrentUser.UserGroupId == 1)
                {
                    tsmiCategories.Visible = true;
                    tsmiWorkLoad.Visible = true;
                    tsmiStatusTypes.Visible = true;
                }
                else
                {
                    tsmiCategories.Visible = false;
                    tsmiWorkLoad.Visible = false;
                    tsmiStatusTypes.Visible = false;
                }
            }
            else
            {
                tsmiCeilingAccessories.Visible = false;
                tsmiDXFCutList.Visible = false;
                tsmiCategories.Visible = false;
                tsmiWorkLoad.Visible = false;
                tsmiStatusTypes.Visible = false;
            }
            //sbu
            string sbu = Program.ObjCurrentUser.SBU;
            if (sbu != "")
            {
                tsmiCeilingAccessories.Visible = false;
                tsmiHoodAutoDrawing.Visible = false;
                tsmiCeilingAutoDrawing.Visible = false;
            }
            else
            {
                tsmiMarineAutoDrawing.Visible = false;
            }

        }

        #region 不再更改的代码
        /// <summary>
        /// 帮助文档链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LlblHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "http://10.9.18.31:8080/space/index");
        }
        private void LlblHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
        /// <summary>
        /// 程序启动后，自动检查更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            UpdateManager objUpdateManager;
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
                FrmTips objFrmTips = new FrmTips();
                objFrmTips.Show();
            }
            this.timerUpdate.Enabled = false;//停止定时器，防止频繁弹出
        }
        //每10分钟更新数据
        private void TimerRefreshData_Tick(object sender, EventArgs e)
        {
            SingletonObject.GetSingleton.FrmP?.BtnQueryByYear_Click(null, null);
            SingletonObject.GetSingleton.FrmDp?.BtnQueryByYear_Click(null, null);
            SingletonObject.GetSingleton.FrmPt?.BtnQueryByYear_Click(null, null);
        }
        //开机自启动
        private void TsmiSetStartUp_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser;
            Microsoft.Win32.RegistryKey run = key.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            DialogResult result = MessageBox.Show("设置Compass程序开机自启动，确定请按“是”，不要开机自启请按“否”，不想设置请按“取消”", "设置开机自启", MessageBoxButtons.YesNoCancel);
            try
            {
                if (result == DialogResult.Yes)
                {
                    run.SetValue("Compass", exePath); //将exe添加到注册表
                }
                else if (result == DialogResult.No)
                {
                    run.DeleteValue("Compass");//删除注册表
                }
            }
            catch
            {
            }
        }
        ////只能打开一次
        //private void FrmMain_Load(object sender, EventArgs e)
        //{
        //    if (!CanOpen())
        //    {
        //        MessageBox.Show("程序已打开！");
        //        Application.Exit();
        //    }
        //}
        //bool CanOpen()
        //{
        //    bool isClosed;
        //    var m = new Mutex(true, "Compass", out isClosed);
        //    return isClosed;
        //}
        #endregion

        #region 初始化单例窗体
        private void InitialForm()
        {
            
                //嵌入
                SingletonObject.GetSingleton.AddForm(new Lazy<FrmProject>(() => new FrmProject()).Value);
                SingletonObject.GetSingleton.AddForm(new FrmModuleTree(QuickBrowse));
                SingletonObject.GetSingleton.AddForm(new FrmQuickBrowse());
                ShowForm(SingletonObject.GetSingleton.FrmMt, splitContainer.Panel1);

                //独立
                SingletonObject.GetSingleton.AddMetroForm(new Lazy<FrmProjectInfo>(() => new FrmProjectInfo()).Value);
                SingletonObject.GetSingleton.AddMetroForm(new Lazy<FrmCeilingAutoDrawing>(() => new FrmCeilingAutoDrawing()).Value);
                SingletonObject.GetSingleton.AddMetroForm(new Lazy<FrmHoodAutoDrawing>(() => new FrmHoodAutoDrawing()).Value);
                SingletonObject.GetSingleton.AddMetroForm(new Lazy<FrmMarineAutoDrawing>(() => new FrmMarineAutoDrawing()).Value);
                SingletonObject.GetSingleton.AddMetroForm(new Lazy<FrmCategoryTree>(() => new FrmCategoryTree()).Value);
                TsmiProjectList_Click(null, null);
            
        }


        #endregion

        #region 显示嵌入的窗体
        private void ShowForm(Form objForm, Panel panel)
        {
            panel.Controls.Clear();
            objForm.TopLevel = false;
            objForm.FormBorderStyle = FormBorderStyle.None;
            objForm.Parent = panel;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        #endregion

        #region 项目管理菜单
        //嵌入
        private void TsmiProjectList_Click(object sender, EventArgs e)
        {
            if (splitContainer.Panel2.Controls.Count == 0 || !(splitContainer.Panel2.Controls[0] is FrmProject))
            {
                ShowForm(SingletonObject.GetSingleton.FrmP, splitContainer.Panel2);
            }
        }

        private void TsmiDarwingPlan_Click(object sender, EventArgs e)
        {
            if (!(splitContainer.Panel2.Controls[0] is FrmDrawingPlan))
            {
                if (SingletonObject.GetSingleton.FrmDp == null) SingletonObject.GetSingleton.AddForm(new Lazy<FrmDrawingPlan>(() => new FrmDrawingPlan()).Value);
                ShowForm(SingletonObject.GetSingleton.FrmDp, splitContainer.Panel2);
            }
        }

        private void TsmiProjectTracking_Click(object sender, EventArgs e)
        {
            if (!(splitContainer.Panel2.Controls[0] is FrmProjectTracking))
            {
                if (SingletonObject.GetSingleton.FrmPt == null) SingletonObject.GetSingleton.AddForm(new Lazy<FrmProjectTracking>(() => new FrmProjectTracking()).Value);
                ShowForm(SingletonObject.GetSingleton.FrmPt, splitContainer.Panel2);
            }
        }

        //非嵌入
        private void TsmiProjectInfo_Click(object sender, EventArgs e)
        {
            SingletonObject.GetSingleton.FrmPi?.ShowAndFocus();
        }
        #region 【2】根据委托创建方法

        private void QuickBrowse(Drawing drawing, ModuleTree tree)
        {
            if (!(splitContainer.Panel2.Controls[0] is FrmQuickBrowse))
            {
                ShowForm(SingletonObject.GetSingleton.FrmQb, splitContainer.Panel2);
            }
            SingletonObject.GetSingleton.FrmQb?.ShowWithItem(drawing, tree);
        }
        #endregion
        #endregion 项目信息菜单

        #region SolidWorks自动绘图
        private void TsmiHoodAutoDrawing_Click(object sender, EventArgs e)
        {
            SingletonObject.GetSingleton.FrmHad?.ShowAndFocus();
        }

        private void TsmiCeilingAutoDrawing_Click(object sender, EventArgs e)
        {
            SingletonObject.GetSingleton.FrmCad?.ShowAndFocus();
        }
        private void TsmiMarineAutoDrawing_Click(object sender, EventArgs e)
        {
            SingletonObject.GetSingleton.FrmMad?.ShowAndFocus();
        }
        #endregion  SolidWorks自动绘图

        #region 统计菜单
        private void TsmiProjectMeasure_Click(object sender, EventArgs e)
        {
            if (SingletonObject.GetSingleton.FrmPm == null) SingletonObject.GetSingleton.AddMetroForm(new Lazy<FrmProjectMeasure>(() => new FrmProjectMeasure()).Value);
            SingletonObject.GetSingleton.FrmPm?.ShowAndFocus();
        }
        /// <summary>
        /// 制图计划统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiDrawingPlanQuery_Click(object sender, EventArgs e)
        {
            if (SingletonObject.GetSingleton.FrmDpq == null) SingletonObject.GetSingleton.AddMetroForm(new Lazy<FrmDrawingPlanQuery>(() => new FrmDrawingPlanQuery()).Value);
            SingletonObject.GetSingleton.FrmDpq?.ShowAndFocus();
        }

        #endregion

        #region 系统设置菜单
        //嵌入
        private void TsmiUsersManage_Click(object sender, EventArgs e)
        {
            if (!(splitContainer.Panel2.Controls[0] is FrmUserManage))
            {
                if (SingletonObject.GetSingleton.FrmUm == null) SingletonObject.GetSingleton.AddForm(new Lazy<FrmUserManage>(() => new FrmUserManage(Program.ObjCurrentUser)).Value);
                ShowForm(SingletonObject.GetSingleton.FrmUm, splitContainer.Panel2);
            }
        }
        private void TsmiCategories_Click(object sender, EventArgs e)
        {
            if (!(splitContainer.Panel2.Controls[0] is FrmCategories))
            {
                if (SingletonObject.GetSingleton.FrmC == null) SingletonObject.GetSingleton.AddForm(new Lazy<FrmCategories>(() => new FrmCategories()).Value);
                ShowForm(SingletonObject.GetSingleton.FrmC, splitContainer.Panel2);
            }
        }
        private void TsmiDXFCutList_Click(object sender, EventArgs e)
        {
            if (!(splitContainer.Panel2.Controls[0] is FrmDxfCutList))
            {
                if (SingletonObject.GetSingleton.FrmDc == null) SingletonObject.GetSingleton.AddForm(new Lazy<FrmDxfCutList>(() => new FrmDxfCutList()).Value);
                ShowForm(SingletonObject.GetSingleton.FrmDc, splitContainer.Panel2);
            }
        }
        private void TsmiWorkLoad_Click(object sender, EventArgs e)
        {
            if (!(splitContainer.Panel2.Controls[0] is FrmDesignWorkload))
            {
                if (SingletonObject.GetSingleton.FrmDw == null) SingletonObject.GetSingleton.AddForm(new Lazy<FrmDesignWorkload>(() => new FrmDesignWorkload()).Value);
                ShowForm(SingletonObject.GetSingleton.FrmDw, splitContainer.Panel2);
            }
        }
        private void TsmiStatusTypes_Click(object sender, EventArgs e)
        {
            if (!(splitContainer.Panel2.Controls[0] is FrmStatusTypes))
            {
                if (SingletonObject.GetSingleton.FrmSt == null) SingletonObject.GetSingleton.AddForm(new Lazy<FrmStatusTypes>(() => new FrmStatusTypes()).Value);
                ShowForm(SingletonObject.GetSingleton.FrmSt, splitContainer.Panel2);
            }
        }
        private void TsmiCeilingAccessories_Click(object sender, EventArgs e)
        {
            if (!(splitContainer.Panel2.Controls[0] is FrmCeilingAccessories))
            {
                if (SingletonObject.GetSingleton.FrmCa == null) SingletonObject.GetSingleton.AddForm(new Lazy<FrmCeilingAccessories>(() => new FrmCeilingAccessories()).Value);
                ShowForm(SingletonObject.GetSingleton.FrmCa, splitContainer.Panel2);
            }
        }
        //非嵌入
        private void TsmieSolidWorksTools_Click(object sender, EventArgs e)
        {
            if (SingletonObject.GetSingleton.FrmSwt == null) SingletonObject.GetSingleton.AddMetroForm(new Lazy<FrmSolidWorksTools>(() => new FrmSolidWorksTools()).Value);
            SingletonObject.GetSingleton.FrmSwt?.ShowAndFocus();
        }

        //非窗体
        private void TsmiUpdate_Click(object sender, EventArgs e)
        {
            UpdateManager objUpdateManager;
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
        private void TsmiSolidWorksSetting_Click(object sender, EventArgs e)
        {
            SolidWorksSetting swSetting = new SolidWorksSetting();
            swSetting.SolidWorksHaltonSetting();
        }

        #endregion

        #region 其他菜单
        private void TsmiSyncFiles_Click(object sender, EventArgs e)
        {
            if (SingletonObject.GetSingleton.FrmSf == null) SingletonObject.GetSingleton.AddMetroForm(new Lazy<FrmSyncFiles>(() => new FrmSyncFiles()).Value);

            SingletonObject.GetSingleton.FrmSf?.ShowAndFocus();
        }
        private void TsmiDrawingNumMatrix_Click(object sender, EventArgs e)
        {
            if (SingletonObject.GetSingleton.FrmDnm == null) SingletonObject.GetSingleton.AddMetroForm(new Lazy<FrmDrawingNumMatrix>(() => new FrmDrawingNumMatrix()).Value);
            SingletonObject.GetSingleton.FrmDnm?.ShowAndFocus();
        }
        private void TsmiTestCode_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms));
        }



        #endregion

        
    }
}
