﻿using System;
using System.IO;
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
            string currentSBU = Program.ObjCurrentUser.SBU == "" ? "FoodService" : Program.ObjCurrentUser.SBU;
            this.lblCurrentSBU.Text = "当前事业部：" + currentSBU;
            this.Text = "COMPASS." + currentSBU;
            SetPermissions();
            //隐藏测试代码
            tsmiTestCode.Visible = false;

            //初始化所有单例窗体
            InitialForm();

            TsmiProjectList_Click(null, null);
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
                FrmTips objFrmTips = new FrmTips();
                objFrmTips.Show();
            }
            this.timerUpdate.Enabled = false;//停止定时器，防止频繁弹出
        }
        #endregion

        /// <summary>
        /// 3.初始化所有单例窗体
        /// </summary>
        private void InitialForm()
        {
            SingletonObject.GetSingleton().AddMetroForm(new FrmHoodAutoDrawing());
            SingletonObject.GetSingleton().AddMetroForm(new FrmCeilingAutoDrawing());
            SingletonObject.GetSingleton().AddMetroForm(new FrmProjectInfo());
            SingletonObject.GetSingleton().AddMetroForm(new FrmSyncFiles());
            SingletonObject.GetSingleton().AddMetroForm(new FrmDrawingNumMatrix());
            SingletonObject.GetSingleton().AddMetroForm(new FrmProjectMeasure());
            SingletonObject.GetSingleton().AddMetroForm(new FrmDrawingPlanQuery());
            SingletonObject.GetSingleton().AddMetroForm(new FrmSolidWorksTools());
            //嵌入
            SingletonObject.GetSingleton().AddForm(new FrmProject(this.ShowModuleTree));
            SingletonObject.GetSingleton().AddForm(new FrmModuleTree(this.QuickBrowse));
            SingletonObject.GetSingleton().AddForm(new FrmQuickBrowse());
            SingletonObject.GetSingleton().AddForm(new FrmDrawingPlan());
            SingletonObject.GetSingleton().AddForm(new FrmProjectTracking());
            SingletonObject.GetSingleton().AddForm(new FrmUserManage(Program.ObjCurrentUser));
            SingletonObject.GetSingleton().AddForm(new FrmCategories());
            SingletonObject.GetSingleton().AddForm(new FrmDXFCutList());
            SingletonObject.GetSingleton().AddForm(new FrmDesignWorkload());
            SingletonObject.GetSingleton().AddForm(new FrmStatusTypes());
            SingletonObject.GetSingleton().AddForm(new FrmCeilingAccessories());

        }




        #region 显示嵌入的窗体
        
        private void ShowForm(Form objForm, Panel panel)
        {
            panel.Controls.Clear();
            //foreach (Control item in panel.Controls)
            //{
            //    if (item is Form objControl) objControl.Hide();
            //}
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
            ShowForm(SingletonObject.GetSingleton().FrmP, splitContainer.Panel2);
        }
        private void TsmiDarwingPlan_Click(object sender, EventArgs e)
        {
            ShowForm(SingletonObject.GetSingleton().FrmDP, splitContainer.Panel2);
        }
        private void TsmiProjectTracking_Click(object sender, EventArgs e)
        {
            ShowForm(SingletonObject.GetSingleton().FrmPT, splitContainer.Panel2);
        }
        //非嵌入
        private void TsmiProjectInfo_Click(object sender, EventArgs e)
        {
            SingletonObject.GetSingleton().FrmPI.Show();
            SingletonObject.GetSingleton().FrmPI.WindowState = FormWindowState.Maximized;
        }
        #region 【2】根据委托创建方法
        private void ShowModuleTree(string odpNo)
        {
            ShowForm(SingletonObject.GetSingleton().FrmMT, splitContainer.Panel1);
            SingletonObject.GetSingleton().FrmMT.ShowWithOdpNo(odpNo);
        }
        private void QuickBrowse(Drawing drawing, ModuleTree tree)
        {
            ShowForm(SingletonObject.GetSingleton().FrmQB, splitContainer.Panel2);
            SingletonObject.GetSingleton().FrmQB.ShowWithItem(drawing, tree);
        } 
        #endregion
        #endregion 项目信息菜单

        #region SolidWorks自动绘图
        private void TsmiHoodAutoDrawing_Click(object sender, EventArgs e)
        {
            SingletonObject.GetSingleton().FrmHAD.Show();
            SingletonObject.GetSingleton().FrmHAD.WindowState = FormWindowState.Normal;
        }

        private void TsmiCeilingAutoDrawing_Click(object sender, EventArgs e)
        {
            SingletonObject.GetSingleton().FrmCAD.Show();
            SingletonObject.GetSingleton().FrmCAD.WindowState = FormWindowState.Normal;
        }
        private void TsmiMarineAutoDrawing_Click(object sender, EventArgs e)
        {

        }
        #endregion  SolidWorks自动绘图

        #region 统计菜单
        private void TsmiProjectMeasure_Click(object sender, EventArgs e)
        {
            SingletonObject.GetSingleton().FrmPM.Show();
            SingletonObject.GetSingleton().FrmPM.WindowState = FormWindowState.Maximized;
        }
        /// <summary>
        /// 制图计划统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiDrawingPlanQuery_Click(object sender, EventArgs e)
        {
            SingletonObject.GetSingleton().FrmDPQ.Show();
            SingletonObject.GetSingleton().FrmDPQ.WindowState = FormWindowState.Maximized;
        }

        #endregion

        #region 系统设置菜单
        //嵌入
        private void TsmiUsersManage_Click(object sender, EventArgs e)
        {
            ShowForm(SingletonObject.GetSingleton().FrmUM, splitContainer.Panel2);
        }
        private void TsmiCategories_Click(object sender, EventArgs e)
        {
            ShowForm(SingletonObject.GetSingleton().FrmC, splitContainer.Panel2);
        }
        private void TsmiDXFCutList_Click(object sender, EventArgs e)
        {

            ShowForm(SingletonObject.GetSingleton().FrmDC, splitContainer.Panel2);
        }
        private void TsmiWorkLoad_Click(object sender, EventArgs e)
        {
            ShowForm(SingletonObject.GetSingleton().FrmDW, splitContainer.Panel2);
        }
        private void TsmiStatusTypes_Click(object sender, EventArgs e)
        {
            ShowForm(SingletonObject.GetSingleton().FrmST, splitContainer.Panel2);
        }
        private void TsmiCeilingAccessories_Click(object sender, EventArgs e)
        {
            ShowForm(SingletonObject.GetSingleton().FrmCA, splitContainer.Panel2);
        }
        //非嵌入
        private void TsmieSolidWorksTools_Click(object sender, EventArgs e)
        {
            FrmSolidWorksTools frmTools = new FrmSolidWorksTools();
            frmTools.Show();
            SingletonObject.GetSingleton().FrmSWT.Show();
            SingletonObject.GetSingleton().FrmSWT.WindowState = FormWindowState.Normal;
        }

        //非窗体
        private void TsmiUpdate_Click(object sender, EventArgs e)
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
        private void TsmiSolidWorksSetting_Click(object sender, EventArgs e)
        {
            SolidWorksSetting swSetting = new SolidWorksSetting();
            swSetting.SolidWorksHaltonSetting();
        }
        
        #endregion

        #region 其他菜单
        private void TsmiSyncFiles_Click(object sender, EventArgs e)
        {
            SingletonObject.GetSingleton().FrmSF.Show();
            SingletonObject.GetSingleton().FrmSF.WindowState = FormWindowState.Normal;
        }
        private void TsmiDrawingNumMatrix_Click(object sender, EventArgs e)
        {
            SingletonObject.GetSingleton().FrmDNM.Show();
            SingletonObject.GetSingleton().FrmDNM.WindowState = FormWindowState.Normal;
        }
        private void TsmiTestCode_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms));
        }
        #endregion


        
    }
}
