namespace Compass
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.llblHelp = new System.Windows.Forms.LinkLabel();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lblVersion = new System.Windows.Forms.Label();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.lblUpdateTime = new System.Windows.Forms.Label();
            this.llblHistory = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCurrentSBU = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiProjectList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProjectInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.制图计划ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDarwingPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProjectTracking = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHoodAutoDrawing = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCeilingAutoDrawing = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMarineAutoDrawing = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSyncFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDrawingNumMatrix = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProjectMeasure = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDrawingPlanQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUsersManage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCategories = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDXFCutList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCeilingAccessories = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWorkLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStatusTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSolidWorksSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmieSolidWorksTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSetStartUp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTestCode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSemiBom = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // llblHelp
            // 
            this.llblHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llblHelp.AutoSize = true;
            this.llblHelp.BackColor = System.Drawing.Color.Transparent;
            this.llblHelp.LinkColor = System.Drawing.Color.Blue;
            this.llblHelp.Location = new System.Drawing.Point(3, 4);
            this.llblHelp.Name = "llblHelp";
            this.llblHelp.Size = new System.Drawing.Size(61, 19);
            this.llblHelp.TabIndex = 2;
            this.llblHelp.TabStop = true;
            this.llblHelp.Text = "帮助文档";
            this.llblHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlblHelp_LinkClicked);
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblCurrentUser.Location = new System.Drawing.Point(1044, 4);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(74, 19);
            this.lblCurrentUser.TabIndex = 3;
            this.lblCurrentUser.Text = "登陆用户：";
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 25);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer.Panel1MinSize = 210;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer.Panel2MinSize = 900;
            this.splitContainer.Size = new System.Drawing.Size(1200, 626);
            this.splitContainer.SplitterDistance = 210;
            this.splitContainer.TabIndex = 4;
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblVersion.Location = new System.Drawing.Point(153, 4);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(74, 19);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "软件版本：";
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 3000;
            this.timerUpdate.Tick += new System.EventHandler(this.TimerUpdate_Tick);
            // 
            // lblUpdateTime
            // 
            this.lblUpdateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUpdateTime.AutoSize = true;
            this.lblUpdateTime.BackColor = System.Drawing.Color.Transparent;
            this.lblUpdateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblUpdateTime.Location = new System.Drawing.Point(293, 4);
            this.lblUpdateTime.Name = "lblUpdateTime";
            this.lblUpdateTime.Size = new System.Drawing.Size(74, 19);
            this.lblUpdateTime.TabIndex = 3;
            this.lblUpdateTime.Text = "更新日期：";
            // 
            // llblHistory
            // 
            this.llblHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llblHistory.AutoSize = true;
            this.llblHistory.BackColor = System.Drawing.Color.Transparent;
            this.llblHistory.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.llblHistory.Location = new System.Drawing.Point(75, 4);
            this.llblHistory.Name = "llblHistory";
            this.llblHistory.Size = new System.Drawing.Size(61, 19);
            this.llblHistory.TabIndex = 2;
            this.llblHistory.TabStop = true;
            this.llblHistory.Text = "更新历史";
            this.llblHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlblHistory_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblUpdateTime);
            this.panel1.Controls.Add(this.llblHelp);
            this.panel1.Controls.Add(this.llblHistory);
            this.panel1.Controls.Add(this.lblVersion);
            this.panel1.Controls.Add(this.lblCurrentSBU);
            this.panel1.Controls.Add(this.lblCurrentUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 650);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 25);
            this.panel1.TabIndex = 6;
            // 
            // lblCurrentSBU
            // 
            this.lblCurrentSBU.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentSBU.AutoSize = true;
            this.lblCurrentSBU.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentSBU.ForeColor = System.Drawing.Color.Red;
            this.lblCurrentSBU.Location = new System.Drawing.Point(812, 4);
            this.lblCurrentSBU.Name = "lblCurrentSBU";
            this.lblCurrentSBU.Size = new System.Drawing.Size(87, 19);
            this.lblCurrentSBU.TabIndex = 3;
            this.lblCurrentSBU.Text = "当前事业部：";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiProjectList,
            this.tsmiProjectInfo,
            this.制图计划ToolStripMenuItem,
            this.tsmiDarwingPlan,
            this.tsmiProjectTracking,
            this.toolStripMenuItem1,
            this.tsmiHoodAutoDrawing,
            this.tsmiCeilingAutoDrawing,
            this.tsmiMarineAutoDrawing,
            this.tsmiSemiBom,
            this.toolStripMenuItem2,
            this.tsmiSyncFiles,
            this.tsmiDrawingNumMatrix,
            this.toolStripMenuItem4,
            this.tsmiQuery,
            this.toolStripMenuItem3,
            this.tsmiSetting,
            this.toolStripMenuItem5,
            this.tsmiTestCode});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 25);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiProjectList
            // 
            this.tsmiProjectList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiProjectList.Name = "tsmiProjectList";
            this.tsmiProjectList.Size = new System.Drawing.Size(68, 21);
            this.tsmiProjectList.Text = "项目列表";
            this.tsmiProjectList.Click += new System.EventHandler(this.TsmiProjectList_Click);
            // 
            // tsmiProjectInfo
            // 
            this.tsmiProjectInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiProjectInfo.Name = "tsmiProjectInfo";
            this.tsmiProjectInfo.Size = new System.Drawing.Size(68, 21);
            this.tsmiProjectInfo.Text = "详细信息";
            this.tsmiProjectInfo.Click += new System.EventHandler(this.TsmiProjectInfo_Click);
            // 
            // 制图计划ToolStripMenuItem
            // 
            this.制图计划ToolStripMenuItem.Enabled = false;
            this.制图计划ToolStripMenuItem.Name = "制图计划ToolStripMenuItem";
            this.制图计划ToolStripMenuItem.Size = new System.Drawing.Size(23, 21);
            this.制图计划ToolStripMenuItem.Text = "|";
            // 
            // tsmiDarwingPlan
            // 
            this.tsmiDarwingPlan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiDarwingPlan.Name = "tsmiDarwingPlan";
            this.tsmiDarwingPlan.Size = new System.Drawing.Size(68, 21);
            this.tsmiDarwingPlan.Text = "制图计划";
            this.tsmiDarwingPlan.Click += new System.EventHandler(this.TsmiDarwingPlan_Click);
            // 
            // tsmiProjectTracking
            // 
            this.tsmiProjectTracking.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiProjectTracking.Name = "tsmiProjectTracking";
            this.tsmiProjectTracking.Size = new System.Drawing.Size(68, 21);
            this.tsmiProjectTracking.Text = "计划跟踪";
            this.tsmiProjectTracking.Click += new System.EventHandler(this.TsmiProjectTracking_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(23, 21);
            this.toolStripMenuItem1.Text = "|";
            // 
            // tsmiHoodAutoDrawing
            // 
            this.tsmiHoodAutoDrawing.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiHoodAutoDrawing.Name = "tsmiHoodAutoDrawing";
            this.tsmiHoodAutoDrawing.Size = new System.Drawing.Size(68, 21);
            this.tsmiHoodAutoDrawing.Text = "烟罩作图";
            this.tsmiHoodAutoDrawing.Click += new System.EventHandler(this.TsmiHoodAutoDrawing_Click);
            // 
            // tsmiCeilingAutoDrawing
            // 
            this.tsmiCeilingAutoDrawing.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiCeilingAutoDrawing.Name = "tsmiCeilingAutoDrawing";
            this.tsmiCeilingAutoDrawing.Size = new System.Drawing.Size(68, 21);
            this.tsmiCeilingAutoDrawing.Text = "天花作图";
            this.tsmiCeilingAutoDrawing.Click += new System.EventHandler(this.TsmiCeilingAutoDrawing_Click);
            // 
            // tsmiMarineAutoDrawing
            // 
            this.tsmiMarineAutoDrawing.Name = "tsmiMarineAutoDrawing";
            this.tsmiMarineAutoDrawing.Size = new System.Drawing.Size(85, 21);
            this.tsmiMarineAutoDrawing.Text = "Marine作图";
            this.tsmiMarineAutoDrawing.Click += new System.EventHandler(this.TsmiMarineAutoDrawing_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(23, 21);
            this.toolStripMenuItem2.Text = "|";
            // 
            // tsmiSyncFiles
            // 
            this.tsmiSyncFiles.Name = "tsmiSyncFiles";
            this.tsmiSyncFiles.Size = new System.Drawing.Size(68, 21);
            this.tsmiSyncFiles.Text = "同步文件";
            this.tsmiSyncFiles.Click += new System.EventHandler(this.TsmiSyncFiles_Click);
            // 
            // tsmiDrawingNumMatrix
            // 
            this.tsmiDrawingNumMatrix.Name = "tsmiDrawingNumMatrix";
            this.tsmiDrawingNumMatrix.Size = new System.Drawing.Size(80, 21);
            this.tsmiDrawingNumMatrix.Text = "图号生成器";
            this.tsmiDrawingNumMatrix.Click += new System.EventHandler(this.TsmiDrawingNumMatrix_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(23, 21);
            this.toolStripMenuItem4.Text = "|";
            // 
            // tsmiQuery
            // 
            this.tsmiQuery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiProjectMeasure,
            this.tsmiDrawingPlanQuery});
            this.tsmiQuery.Name = "tsmiQuery";
            this.tsmiQuery.Size = new System.Drawing.Size(68, 21);
            this.tsmiQuery.Text = "查询统计";
            // 
            // tsmiProjectMeasure
            // 
            this.tsmiProjectMeasure.Name = "tsmiProjectMeasure";
            this.tsmiProjectMeasure.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.tsmiProjectMeasure.Size = new System.Drawing.Size(193, 22);
            this.tsmiProjectMeasure.Text = "项目测量(&M)";
            this.tsmiProjectMeasure.Click += new System.EventHandler(this.TsmiProjectMeasure_Click);
            // 
            // tsmiDrawingPlanQuery
            // 
            this.tsmiDrawingPlanQuery.Name = "tsmiDrawingPlanQuery";
            this.tsmiDrawingPlanQuery.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.tsmiDrawingPlanQuery.Size = new System.Drawing.Size(193, 22);
            this.tsmiDrawingPlanQuery.Text = "制图统计(&D)";
            this.tsmiDrawingPlanQuery.Click += new System.EventHandler(this.TsmiDrawingPlanQuery_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Enabled = false;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(23, 21);
            this.toolStripMenuItem3.Text = "|";
            // 
            // tsmiSetting
            // 
            this.tsmiSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUsersManage,
            this.tsmiCategories,
            this.tsmiDXFCutList,
            this.tsmiCeilingAccessories,
            this.tsmiWorkLoad,
            this.tsmiStatusTypes,
            this.tsmiUpdate,
            this.tsmiSolidWorksSetting,
            this.tsmieSolidWorksTools,
            this.tsmiSetStartUp});
            this.tsmiSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiSetting.Name = "tsmiSetting";
            this.tsmiSetting.Size = new System.Drawing.Size(68, 21);
            this.tsmiSetting.Text = "系统设置";
            // 
            // tsmiUsersManage
            // 
            this.tsmiUsersManage.Name = "tsmiUsersManage";
            this.tsmiUsersManage.Size = new System.Drawing.Size(153, 22);
            this.tsmiUsersManage.Text = "用户信息";
            this.tsmiUsersManage.Click += new System.EventHandler(this.TsmiUsersManage_Click);
            // 
            // tsmiCategories
            // 
            this.tsmiCategories.Name = "tsmiCategories";
            this.tsmiCategories.Size = new System.Drawing.Size(153, 22);
            this.tsmiCategories.Text = "模型分类";
            this.tsmiCategories.Click += new System.EventHandler(this.TsmiCategories_Click);
            // 
            // tsmiDXFCutList
            // 
            this.tsmiDXFCutList.Name = "tsmiDXFCutList";
            this.tsmiDXFCutList.Size = new System.Drawing.Size(153, 22);
            this.tsmiDXFCutList.Text = "Cutlist模版";
            this.tsmiDXFCutList.Click += new System.EventHandler(this.TsmiDXFCutList_Click);
            // 
            // tsmiCeilingAccessories
            // 
            this.tsmiCeilingAccessories.Name = "tsmiCeilingAccessories";
            this.tsmiCeilingAccessories.Size = new System.Drawing.Size(153, 22);
            this.tsmiCeilingAccessories.Text = "天花配件";
            this.tsmiCeilingAccessories.Click += new System.EventHandler(this.TsmiCeilingAccessories_Click);
            // 
            // tsmiWorkLoad
            // 
            this.tsmiWorkLoad.Name = "tsmiWorkLoad";
            this.tsmiWorkLoad.Size = new System.Drawing.Size(153, 22);
            this.tsmiWorkLoad.Text = "设计工作量";
            this.tsmiWorkLoad.Click += new System.EventHandler(this.TsmiWorkLoad_Click);
            // 
            // tsmiStatusTypes
            // 
            this.tsmiStatusTypes.Name = "tsmiStatusTypes";
            this.tsmiStatusTypes.Size = new System.Drawing.Size(153, 22);
            this.tsmiStatusTypes.Text = "项目状态/类型";
            this.tsmiStatusTypes.Click += new System.EventHandler(this.TsmiStatusTypes_Click);
            // 
            // tsmiUpdate
            // 
            this.tsmiUpdate.Name = "tsmiUpdate";
            this.tsmiUpdate.Size = new System.Drawing.Size(153, 22);
            this.tsmiUpdate.Text = "系统升级";
            this.tsmiUpdate.Click += new System.EventHandler(this.TsmiUpdate_Click);
            // 
            // tsmiSolidWorksSetting
            // 
            this.tsmiSolidWorksSetting.Name = "tsmiSolidWorksSetting";
            this.tsmiSolidWorksSetting.Size = new System.Drawing.Size(153, 22);
            this.tsmiSolidWorksSetting.Text = "SW一键设置";
            this.tsmiSolidWorksSetting.Click += new System.EventHandler(this.TsmiSolidWorksSetting_Click);
            // 
            // tsmieSolidWorksTools
            // 
            this.tsmieSolidWorksTools.Enabled = false;
            this.tsmieSolidWorksTools.Name = "tsmieSolidWorksTools";
            this.tsmieSolidWorksTools.Size = new System.Drawing.Size(153, 22);
            this.tsmieSolidWorksTools.Text = "SW实用工具";
            this.tsmieSolidWorksTools.Click += new System.EventHandler(this.TsmieSolidWorksTools_Click);
            // 
            // tsmiSetStartUp
            // 
            this.tsmiSetStartUp.Enabled = false;
            this.tsmiSetStartUp.Name = "tsmiSetStartUp";
            this.tsmiSetStartUp.Size = new System.Drawing.Size(153, 22);
            this.tsmiSetStartUp.Text = "设置开机自启";
            this.tsmiSetStartUp.Click += new System.EventHandler(this.TsmiSetStartUp_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(23, 21);
            this.toolStripMenuItem5.Text = "|";
            // 
            // tsmiTestCode
            // 
            this.tsmiTestCode.Name = "tsmiTestCode";
            this.tsmiTestCode.Size = new System.Drawing.Size(68, 21);
            this.tsmiTestCode.Text = "测试代码";
            this.tsmiTestCode.Click += new System.EventHandler(this.TsmiTestCode_Click);
            // 
            // tsmiSemiBom
            // 
            this.tsmiSemiBom.Name = "tsmiSemiBom";
            this.tsmiSemiBom.Size = new System.Drawing.Size(80, 21);
            this.tsmiSemiBom.Text = "半成品清单";
            this.tsmiSemiBom.Click += new System.EventHandler(this.tsmiSemiBom_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1200, 675);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COMPASS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.LinkLabel llblHelp;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.Label lblUpdateTime;
        private System.Windows.Forms.LinkLabel llblHistory;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiProjectList;
        private System.Windows.Forms.ToolStripMenuItem tsmiProjectInfo;
        private System.Windows.Forms.ToolStripMenuItem 制图计划ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiDarwingPlan;
        private System.Windows.Forms.ToolStripMenuItem tsmiProjectTracking;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiHoodAutoDrawing;
        private System.Windows.Forms.ToolStripMenuItem tsmiCeilingAutoDrawing;
        private System.Windows.Forms.ToolStripMenuItem tsmiSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiUsersManage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmiCategories;
        private System.Windows.Forms.ToolStripMenuItem tsmiDXFCutList;
        private System.Windows.Forms.ToolStripMenuItem tsmiCeilingAccessories;
        private System.Windows.Forms.ToolStripMenuItem tsmiWorkLoad;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiStatusTypes;
        private System.Windows.Forms.ToolStripMenuItem tsmiQuery;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmiDrawingPlanQuery;
        private System.Windows.Forms.Label lblCurrentSBU;
        private System.Windows.Forms.ToolStripMenuItem tsmiMarineAutoDrawing;
        private System.Windows.Forms.ToolStripMenuItem tsmiProjectMeasure;
        private System.Windows.Forms.ToolStripMenuItem tsmiSolidWorksSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmieSolidWorksTools;
        private System.Windows.Forms.ToolStripMenuItem tsmiSyncFiles;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem tsmiDrawingNumMatrix;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem tsmiTestCode;
        private System.Windows.Forms.ToolStripMenuItem tsmiSetStartUp;
        private System.Windows.Forms.ToolStripMenuItem tsmiSemiBom;
    }
}

