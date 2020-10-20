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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbProjectList = new System.Windows.Forms.ToolStripButton();
            this.tsbProjectInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDarwingPlan = new System.Windows.Forms.ToolStripButton();
            this.tsbProjectTracking = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHoodAutoDrawing = new System.Windows.Forms.ToolStripButton();
            this.tsbCeilingAutoDrawing = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbUsersManage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbCategories = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbDXFCutList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbCeilingAccessories = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbWorkLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbVaultsStatusTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbUpdate = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // llblHelp
            // 
            this.llblHelp.AutoSize = true;
            this.llblHelp.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.llblHelp.Location = new System.Drawing.Point(216, 31);
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
            this.lblCurrentUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblCurrentUser.Location = new System.Drawing.Point(1047, 31);
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
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(20, 87);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.Azure;
            this.splitContainer.Panel1MinSize = 210;
            this.splitContainer.Panel2MinSize = 900;
            this.splitContainer.Size = new System.Drawing.Size(1160, 568);
            this.splitContainer.SplitterDistance = 210;
            this.splitContainer.TabIndex = 4;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblVersion.Location = new System.Drawing.Point(297, 31);
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
            this.lblUpdateTime.AutoSize = true;
            this.lblUpdateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblUpdateTime.Location = new System.Drawing.Point(437, 31);
            this.lblUpdateTime.Name = "lblUpdateTime";
            this.lblUpdateTime.Size = new System.Drawing.Size(74, 19);
            this.lblUpdateTime.TabIndex = 3;
            this.lblUpdateTime.Text = "更新日期：";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbProjectList,
            this.tsbProjectInfo,
            this.toolStripSeparator1,
            this.tsbDarwingPlan,
            this.tsbProjectTracking,
            this.toolStripSeparator2,
            this.tsbHoodAutoDrawing,
            this.tsbCeilingAutoDrawing,
            this.toolStripSeparator3,
            this.toolStripSplitButton1});
            this.toolStrip1.Location = new System.Drawing.Point(20, 60);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1160, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbProjectList
            // 
            this.tsbProjectList.Image = ((System.Drawing.Image)(resources.GetObject("tsbProjectList.Image")));
            this.tsbProjectList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProjectList.Name = "tsbProjectList";
            this.tsbProjectList.Size = new System.Drawing.Size(76, 22);
            this.tsbProjectList.Text = "项目列表";
            this.tsbProjectList.Click += new System.EventHandler(this.tsbProjectList_Click);
            // 
            // tsbProjectInfo
            // 
            this.tsbProjectInfo.Image = ((System.Drawing.Image)(resources.GetObject("tsbProjectInfo.Image")));
            this.tsbProjectInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProjectInfo.Name = "tsbProjectInfo";
            this.tsbProjectInfo.Size = new System.Drawing.Size(76, 22);
            this.tsbProjectInfo.Text = "详细信息";
            this.tsbProjectInfo.Click += new System.EventHandler(this.tsbProjectInfo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbDarwingPlan
            // 
            this.tsbDarwingPlan.Image = ((System.Drawing.Image)(resources.GetObject("tsbDarwingPlan.Image")));
            this.tsbDarwingPlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDarwingPlan.Name = "tsbDarwingPlan";
            this.tsbDarwingPlan.Size = new System.Drawing.Size(76, 22);
            this.tsbDarwingPlan.Text = "制图计划";
            this.tsbDarwingPlan.Click += new System.EventHandler(this.tsbDarwingPlan_Click);
            // 
            // tsbProjectTracking
            // 
            this.tsbProjectTracking.Image = ((System.Drawing.Image)(resources.GetObject("tsbProjectTracking.Image")));
            this.tsbProjectTracking.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProjectTracking.Name = "tsbProjectTracking";
            this.tsbProjectTracking.Size = new System.Drawing.Size(76, 22);
            this.tsbProjectTracking.Text = "计划跟踪";
            this.tsbProjectTracking.Click += new System.EventHandler(this.tsbProjectTracking_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbHoodAutoDrawing
            // 
            this.tsbHoodAutoDrawing.Image = ((System.Drawing.Image)(resources.GetObject("tsbHoodAutoDrawing.Image")));
            this.tsbHoodAutoDrawing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHoodAutoDrawing.Name = "tsbHoodAutoDrawing";
            this.tsbHoodAutoDrawing.Size = new System.Drawing.Size(76, 22);
            this.tsbHoodAutoDrawing.Text = "普通烟罩";
            this.tsbHoodAutoDrawing.Click += new System.EventHandler(this.tsbHoodAutoDrawing_Click);
            // 
            // tsbCeilingAutoDrawing
            // 
            this.tsbCeilingAutoDrawing.Image = ((System.Drawing.Image)(resources.GetObject("tsbCeilingAutoDrawing.Image")));
            this.tsbCeilingAutoDrawing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCeilingAutoDrawing.Name = "tsbCeilingAutoDrawing";
            this.tsbCeilingAutoDrawing.Size = new System.Drawing.Size(76, 22);
            this.tsbCeilingAutoDrawing.Text = "天花烟罩";
            this.tsbCeilingAutoDrawing.Click += new System.EventHandler(this.tsbCeilingAutoDrawing_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbUsersManage,
            this.tsbCategories,
            this.tsbDXFCutList,
            this.tsbCeilingAccessories,
            this.tsbWorkLoad,
            this.tsbVaultsStatusTypes,
            this.tsbUpdate});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(88, 22);
            this.toolStripSplitButton1.Text = "系统后台";
            // 
            // tsbUsersManage
            // 
            this.tsbUsersManage.Image = ((System.Drawing.Image)(resources.GetObject("tsbUsersManage.Image")));
            this.tsbUsersManage.Name = "tsbUsersManage";
            this.tsbUsersManage.Size = new System.Drawing.Size(180, 22);
            this.tsbUsersManage.Text = "用户与分组";
            this.tsbUsersManage.Click += new System.EventHandler(this.tsbUsersManage_Click);
            // 
            // tsbCategories
            // 
            this.tsbCategories.Image = ((System.Drawing.Image)(resources.GetObject("tsbCategories.Image")));
            this.tsbCategories.Name = "tsbCategories";
            this.tsbCategories.Size = new System.Drawing.Size(180, 22);
            this.tsbCategories.Text = "产品模型分类";
            this.tsbCategories.Click += new System.EventHandler(this.tsbCategories_Click);
            // 
            // tsbDXFCutList
            // 
            this.tsbDXFCutList.Image = global::Compass.Properties.Resources.Cutlist;
            this.tsbDXFCutList.Name = "tsbDXFCutList";
            this.tsbDXFCutList.Size = new System.Drawing.Size(180, 22);
            this.tsbDXFCutList.Text = "CutList模板管理";
            this.tsbDXFCutList.Click += new System.EventHandler(this.tsbDXFCutList_Click);
            // 
            // tsbCeilingAccessories
            // 
            this.tsbCeilingAccessories.Image = ((System.Drawing.Image)(resources.GetObject("tsbCeilingAccessories.Image")));
            this.tsbCeilingAccessories.Name = "tsbCeilingAccessories";
            this.tsbCeilingAccessories.Size = new System.Drawing.Size(180, 22);
            this.tsbCeilingAccessories.Text = "天花发货清单配件";
            this.tsbCeilingAccessories.Click += new System.EventHandler(this.tsbCeilingAccessories_Click);
            // 
            // tsbWorkLoad
            // 
            this.tsbWorkLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsbWorkLoad.Image")));
            this.tsbWorkLoad.Name = "tsbWorkLoad";
            this.tsbWorkLoad.Size = new System.Drawing.Size(180, 22);
            this.tsbWorkLoad.Text = "设计工作量";
            this.tsbWorkLoad.Click += new System.EventHandler(this.tsbWorkLoad_Click);
            // 
            // tsbVaultsStatusTypes
            // 
            this.tsbVaultsStatusTypes.Image = ((System.Drawing.Image)(resources.GetObject("tsbVaultsStatusTypes.Image")));
            this.tsbVaultsStatusTypes.Name = "tsbVaultsStatusTypes";
            this.tsbVaultsStatusTypes.Size = new System.Drawing.Size(180, 22);
            this.tsbVaultsStatusTypes.Text = "项目状态/类型";
            this.tsbVaultsStatusTypes.Click += new System.EventHandler(this.tsbVaultsStatusTypes_Click);
            // 
            // tsbUpdate
            // 
            this.tsbUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsbUpdate.Image")));
            this.tsbUpdate.Name = "tsbUpdate";
            this.tsbUpdate.Size = new System.Drawing.Size(180, 22);
            this.tsbUpdate.Text = "系统升级";
            this.tsbUpdate.Click += new System.EventHandler(this.tsbUpdate_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblUpdateTime);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.llblHelp);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1200, 675);
            this.Name = "FrmMain";
            this.Text = "指南针(Compass)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem tsbUsersManage;
        private System.Windows.Forms.ToolStripButton tsbProjectList;
        private System.Windows.Forms.ToolStripButton tsbDarwingPlan;
        private System.Windows.Forms.ToolStripButton tsbProjectTracking;
        private System.Windows.Forms.ToolStripButton tsbProjectInfo;
        private System.Windows.Forms.ToolStripButton tsbHoodAutoDrawing;
        private System.Windows.Forms.ToolStripButton tsbCeilingAutoDrawing;
        private System.Windows.Forms.ToolStripMenuItem tsbCategories;
        private System.Windows.Forms.ToolStripMenuItem tsbWorkLoad;
        private System.Windows.Forms.ToolStripMenuItem tsbVaultsStatusTypes;
        private System.Windows.Forms.ToolStripMenuItem tsbUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsbCeilingAccessories;
        private System.Windows.Forms.ToolStripMenuItem tsbDXFCutList;
    }
}

