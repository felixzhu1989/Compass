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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiProjectList = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tsmiProjectInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.制图计划ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDarwingPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProjectTracking = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHoodAutoDrawing = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCeilingAutoDrawing = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUsersManage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCategories = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDXFCutList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCeilingAccessories = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWorkLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStatusTypes = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // llblHelp
            // 
            this.llblHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llblHelp.AutoSize = true;
            this.llblHelp.BackColor = System.Drawing.Color.Transparent;
            this.llblHelp.LinkColor = System.Drawing.Color.Blue;
            this.llblHelp.Location = new System.Drawing.Point(502, 4);
            this.llblHelp.Name = "llblHelp";
            this.llblHelp.Size = new System.Drawing.Size(61, 19);
            this.llblHelp.TabIndex = 2;
            this.llblHelp.TabStop = true;
            this.llblHelp.Text = "帮助文档";
            this.llblHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblHelp_LinkClicked);
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblVersion.Location = new System.Drawing.Point(652, 4);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(74, 19);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "软件版本：";
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 3000;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // lblUpdateTime
            // 
            this.lblUpdateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUpdateTime.AutoSize = true;
            this.lblUpdateTime.BackColor = System.Drawing.Color.Transparent;
            this.lblUpdateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblUpdateTime.Location = new System.Drawing.Point(792, 4);
            this.lblUpdateTime.Name = "lblUpdateTime";
            this.lblUpdateTime.Size = new System.Drawing.Size(74, 19);
            this.lblUpdateTime.TabIndex = 3;
            this.lblUpdateTime.Text = "更新日期：";
            // 
            // llblHistory
            // 
            this.llblHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llblHistory.AutoSize = true;
            this.llblHistory.BackColor = System.Drawing.Color.Transparent;
            this.llblHistory.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.llblHistory.Location = new System.Drawing.Point(574, 4);
            this.llblHistory.Name = "llblHistory";
            this.llblHistory.Size = new System.Drawing.Size(61, 19);
            this.llblHistory.TabIndex = 2;
            this.llblHistory.TabStop = true;
            this.llblHistory.Text = "更新历史";
            this.llblHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblHistory_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblUpdateTime);
            this.panel1.Controls.Add(this.llblHelp);
            this.panel1.Controls.Add(this.llblHistory);
            this.panel1.Controls.Add(this.lblVersion);
            this.panel1.Controls.Add(this.lblCurrentUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 650);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 25);
            this.panel1.TabIndex = 6;
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
            this.toolStripMenuItem2,
            this.tsmiSetting});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 25);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiProjectList
            // 
            this.tsmiProjectList.Image = global::Compass.Properties.Resources.ProjectList;
            this.tsmiProjectList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiProjectList.Name = "tsmiProjectList";
            this.tsmiProjectList.Size = new System.Drawing.Size(84, 21);
            this.tsmiProjectList.Text = "项目列表";
            this.tsmiProjectList.Click += new System.EventHandler(this.tsmiProjectList_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Category.png");
            this.imageList1.Images.SetKeyName(1, "CeilingAutoDrawing.png");
            this.imageList1.Images.SetKeyName(2, "customer_service_68.646575342466px_1208508_easyicon.net.png");
            this.imageList1.Images.SetKeyName(3, "Cutlist.png");
            this.imageList1.Images.SetKeyName(4, "DrawingPlan.png");
            this.imageList1.Images.SetKeyName(5, "HoodAutoDrawing.png");
            this.imageList1.Images.SetKeyName(6, "PackingList.png");
            this.imageList1.Images.SetKeyName(7, "ProjectInfo.png");
            this.imageList1.Images.SetKeyName(8, "ProjectList.png");
            this.imageList1.Images.SetKeyName(9, "ProjectTracking.png");
            this.imageList1.Images.SetKeyName(10, "Setting.png");
            this.imageList1.Images.SetKeyName(11, "Status.png");
            this.imageList1.Images.SetKeyName(12, "UpdateIcon.png");
            this.imageList1.Images.SetKeyName(13, "UsersManage.png");
            this.imageList1.Images.SetKeyName(14, "Workload.png");
            // 
            // tsmiProjectInfo
            // 
            this.tsmiProjectInfo.Image = global::Compass.Properties.Resources.ProjectInfo;
            this.tsmiProjectInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiProjectInfo.Name = "tsmiProjectInfo";
            this.tsmiProjectInfo.Size = new System.Drawing.Size(84, 21);
            this.tsmiProjectInfo.Text = "详细信息";
            this.tsmiProjectInfo.Click += new System.EventHandler(this.tsmiProjectInfo_Click);
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
            this.tsmiDarwingPlan.Image = global::Compass.Properties.Resources.DrawingPlan;
            this.tsmiDarwingPlan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiDarwingPlan.Name = "tsmiDarwingPlan";
            this.tsmiDarwingPlan.Size = new System.Drawing.Size(84, 21);
            this.tsmiDarwingPlan.Text = "制图计划";
            this.tsmiDarwingPlan.Click += new System.EventHandler(this.tsmiDarwingPlan_Click);
            // 
            // tsmiProjectTracking
            // 
            this.tsmiProjectTracking.Image = global::Compass.Properties.Resources.ProjectTracking;
            this.tsmiProjectTracking.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiProjectTracking.Name = "tsmiProjectTracking";
            this.tsmiProjectTracking.Size = new System.Drawing.Size(84, 21);
            this.tsmiProjectTracking.Text = "计划跟踪";
            this.tsmiProjectTracking.Click += new System.EventHandler(this.tsmiProjectTracking_Click);
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
            this.tsmiHoodAutoDrawing.Image = global::Compass.Properties.Resources.HoodAutoDrawing;
            this.tsmiHoodAutoDrawing.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiHoodAutoDrawing.Name = "tsmiHoodAutoDrawing";
            this.tsmiHoodAutoDrawing.Size = new System.Drawing.Size(84, 21);
            this.tsmiHoodAutoDrawing.Text = "普通烟罩";
            this.tsmiHoodAutoDrawing.Click += new System.EventHandler(this.tsmiHoodAutoDrawing_Click);
            // 
            // tsmiCeilingAutoDrawing
            // 
            this.tsmiCeilingAutoDrawing.Image = global::Compass.Properties.Resources.CeilingAutoDrawing;
            this.tsmiCeilingAutoDrawing.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiCeilingAutoDrawing.Name = "tsmiCeilingAutoDrawing";
            this.tsmiCeilingAutoDrawing.Size = new System.Drawing.Size(84, 21);
            this.tsmiCeilingAutoDrawing.Text = "天花烟罩";
            this.tsmiCeilingAutoDrawing.Click += new System.EventHandler(this.tsmiCeilingAutoDrawing_Click);
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
            this.tsmiUpdate});
            this.tsmiSetting.Image = global::Compass.Properties.Resources.Setting;
            this.tsmiSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiSetting.Name = "tsmiSetting";
            this.tsmiSetting.Size = new System.Drawing.Size(84, 21);
            this.tsmiSetting.Text = "系统后台";
            // 
            // tsmiUsersManage
            // 
            this.tsmiUsersManage.Image = global::Compass.Properties.Resources.UsersManage;
            this.tsmiUsersManage.Name = "tsmiUsersManage";
            this.tsmiUsersManage.Size = new System.Drawing.Size(180, 22);
            this.tsmiUsersManage.Text = "用户信息";
            this.tsmiUsersManage.Click += new System.EventHandler(this.tsmiUsersManage_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(23, 21);
            this.toolStripMenuItem2.Text = "|";
            // 
            // tsmiCategories
            // 
            this.tsmiCategories.Image = global::Compass.Properties.Resources.Category;
            this.tsmiCategories.Name = "tsmiCategories";
            this.tsmiCategories.Size = new System.Drawing.Size(180, 22);
            this.tsmiCategories.Text = "模型分类";
            this.tsmiCategories.Click += new System.EventHandler(this.tsmiCategories_Click);
            // 
            // tsmiDXFCutList
            // 
            this.tsmiDXFCutList.Image = global::Compass.Properties.Resources.Cutlist;
            this.tsmiDXFCutList.Name = "tsmiDXFCutList";
            this.tsmiDXFCutList.Size = new System.Drawing.Size(180, 22);
            this.tsmiDXFCutList.Text = "Cutlist模版";
            this.tsmiDXFCutList.Click += new System.EventHandler(this.tsmiDXFCutList_Click);
            // 
            // tsmiCeilingAccessories
            // 
            this.tsmiCeilingAccessories.Image = global::Compass.Properties.Resources.PackingList;
            this.tsmiCeilingAccessories.Name = "tsmiCeilingAccessories";
            this.tsmiCeilingAccessories.Size = new System.Drawing.Size(180, 22);
            this.tsmiCeilingAccessories.Text = "天花配件";
            this.tsmiCeilingAccessories.Click += new System.EventHandler(this.tsmiCeilingAccessories_Click);
            // 
            // tsmiWorkLoad
            // 
            this.tsmiWorkLoad.Image = global::Compass.Properties.Resources.Workload;
            this.tsmiWorkLoad.Name = "tsmiWorkLoad";
            this.tsmiWorkLoad.Size = new System.Drawing.Size(180, 22);
            this.tsmiWorkLoad.Text = "设计工作量";
            this.tsmiWorkLoad.Click += new System.EventHandler(this.tsmiWorkLoad_Click);
            // 
            // tsmiUpdate
            // 
            this.tsmiUpdate.Image = global::Compass.Properties.Resources.UpdateIcon;
            this.tsmiUpdate.Name = "tsmiUpdate";
            this.tsmiUpdate.Size = new System.Drawing.Size(180, 22);
            this.tsmiUpdate.Text = "系统升级";
            this.tsmiUpdate.Click += new System.EventHandler(this.tsmiUpdate_Click);
            // 
            // tsmiStatusTypes
            // 
            this.tsmiStatusTypes.Image = global::Compass.Properties.Resources.Status;
            this.tsmiStatusTypes.Name = "tsmiStatusTypes";
            this.tsmiStatusTypes.Size = new System.Drawing.Size(180, 22);
            this.tsmiStatusTypes.Text = "项目状态/类型";
            this.tsmiStatusTypes.Click += new System.EventHandler(this.tsmiStatusTypes_Click);
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
            this.Text = "COMPASS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
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
        private System.Windows.Forms.ImageList imageList1;
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
    }
}

