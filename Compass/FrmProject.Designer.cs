namespace Compass
{
    partial class FrmProject
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnQueryAllProjects = new System.Windows.Forms.Button();
            this.btnProject = new System.Windows.Forms.Button();
            this.dtpShippingTime = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnQueryByODPNo = new System.Windows.Forms.Button();
            this.btnQueryByUserId = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cobUserId = new System.Windows.Forms.ComboBox();
            this.txtODPNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmsProject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiShowProjectInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditProject = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteProject = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowModuleTree = new System.Windows.Forms.ToolStripMenuItem();
            this.cobCustomerId = new System.Windows.Forms.ComboBox();
            this.cmsCustomer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBPONo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.txtProjectId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dgvProjects = new System.Windows.Forms.DataGridView();
            this.UserAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODPNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BPONo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Shippingtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectStatusName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectAbnormal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RiskLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoodType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.cobHoodType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtToPage = new MyUIControls.SuperTextBox(this.components);
            this.label17 = new System.Windows.Forms.Label();
            this.lblRecordsCound = new System.Windows.Forms.Label();
            this.lblCurrentPage = new System.Windows.Forms.Label();
            this.lblPageCount = new System.Windows.Forms.Label();
            this.cobRecordList = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.cobQueryYear = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnToPage = new System.Windows.Forms.Button();
            this.btnQueryByYear = new System.Windows.Forms.Button();
            this.cmsProject.SuspendLayout();
            this.cmsCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjects)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQueryAllProjects
            // 
            this.btnQueryAllProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQueryAllProjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnQueryAllProjects.FlatAppearance.BorderSize = 0;
            this.btnQueryAllProjects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQueryAllProjects.ForeColor = System.Drawing.Color.White;
            this.btnQueryAllProjects.Location = new System.Drawing.Point(830, 73);
            this.btnQueryAllProjects.Name = "btnQueryAllProjects";
            this.btnQueryAllProjects.Size = new System.Drawing.Size(108, 28);
            this.btnQueryAllProjects.TabIndex = 34;
            this.btnQueryAllProjects.Text = "显示全部项目";
            this.btnQueryAllProjects.UseVisualStyleBackColor = false;
            this.btnQueryAllProjects.Click += new System.EventHandler(this.btnQueryAllProjects_Click);
            // 
            // btnProject
            // 
            this.btnProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnProject.FlatAppearance.BorderSize = 0;
            this.btnProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProject.ForeColor = System.Drawing.Color.White;
            this.btnProject.Location = new System.Drawing.Point(830, 41);
            this.btnProject.Name = "btnProject";
            this.btnProject.Size = new System.Drawing.Size(108, 28);
            this.btnProject.TabIndex = 33;
            this.btnProject.Text = "添加项目信息";
            this.btnProject.UseVisualStyleBackColor = false;
            this.btnProject.Click += new System.EventHandler(this.btnProject_Click);
            // 
            // dtpShippingTime
            // 
            this.dtpShippingTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpShippingTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpShippingTime.Location = new System.Drawing.Point(723, 43);
            this.dtpShippingTime.Name = "dtpShippingTime";
            this.dtpShippingTime.Size = new System.Drawing.Size(105, 25);
            this.dtpShippingTime.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(719, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 19);
            this.label7.TabIndex = 32;
            this.label7.Text = "生产完工日期";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 29;
            this.label3.Text = "项目编号";
            // 
            // btnQueryByODPNo
            // 
            this.btnQueryByODPNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnQueryByODPNo.FlatAppearance.BorderSize = 0;
            this.btnQueryByODPNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQueryByODPNo.ForeColor = System.Drawing.Color.White;
            this.btnQueryByODPNo.Location = new System.Drawing.Point(189, 74);
            this.btnQueryByODPNo.Name = "btnQueryByODPNo";
            this.btnQueryByODPNo.Size = new System.Drawing.Size(46, 28);
            this.btnQueryByODPNo.TabIndex = 27;
            this.btnQueryByODPNo.Text = "查询";
            this.btnQueryByODPNo.UseVisualStyleBackColor = false;
            this.btnQueryByODPNo.Click += new System.EventHandler(this.btnQueryByODPNo_Click);
            // 
            // btnQueryByUserId
            // 
            this.btnQueryByUserId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnQueryByUserId.FlatAppearance.BorderSize = 0;
            this.btnQueryByUserId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQueryByUserId.ForeColor = System.Drawing.Color.White;
            this.btnQueryByUserId.Location = new System.Drawing.Point(189, 41);
            this.btnQueryByUserId.Name = "btnQueryByUserId";
            this.btnQueryByUserId.Size = new System.Drawing.Size(46, 28);
            this.btnQueryByUserId.TabIndex = 28;
            this.btnQueryByUserId.Text = "查询";
            this.btnQueryByUserId.UseVisualStyleBackColor = false;
            this.btnQueryByUserId.Click += new System.EventHandler(this.btnQueryByUserId_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 30;
            this.label4.Text = "制图人员";
            // 
            // cobUserId
            // 
            this.cobUserId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobUserId.FormattingEnabled = true;
            this.cobUserId.Location = new System.Drawing.Point(75, 42);
            this.cobUserId.Name = "cobUserId";
            this.cobUserId.Size = new System.Drawing.Size(108, 27);
            this.cobUserId.TabIndex = 25;
            this.cobUserId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cobUserId_KeyDown);
            // 
            // txtODPNo
            // 
            this.txtODPNo.Location = new System.Drawing.Point(75, 76);
            this.txtODPNo.Name = "txtODPNo";
            this.txtODPNo.Size = new System.Drawing.Size(108, 25);
            this.txtODPNo.TabIndex = 26;
            this.txtODPNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtODPNo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 26);
            this.label1.TabIndex = 24;
            this.label1.Text = "项目列表";
            // 
            // cmsProject
            // 
            this.cmsProject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowProjectInfo,
            this.tsmiEditProject,
            this.tsmiDeleteProject,
            this.tsmiShowModuleTree});
            this.cmsProject.Name = "cmsProject";
            this.cmsProject.Size = new System.Drawing.Size(149, 92);
            // 
            // tsmiShowProjectInfo
            // 
            this.tsmiShowProjectInfo.Name = "tsmiShowProjectInfo";
            this.tsmiShowProjectInfo.Size = new System.Drawing.Size(148, 22);
            this.tsmiShowProjectInfo.Text = "显示详细信息";
            this.tsmiShowProjectInfo.Click += new System.EventHandler(this.tsmiShowProjectInfo_Click);
            // 
            // tsmiEditProject
            // 
            this.tsmiEditProject.Name = "tsmiEditProject";
            this.tsmiEditProject.Size = new System.Drawing.Size(148, 22);
            this.tsmiEditProject.Text = "修改项目信息";
            this.tsmiEditProject.Click += new System.EventHandler(this.tsmiEditProject_Click);
            // 
            // tsmiDeleteProject
            // 
            this.tsmiDeleteProject.Name = "tsmiDeleteProject";
            this.tsmiDeleteProject.Size = new System.Drawing.Size(148, 22);
            this.tsmiDeleteProject.Text = "删除项目信息";
            this.tsmiDeleteProject.Click += new System.EventHandler(this.tsmiDeleteProject_Click);
            // 
            // tsmiShowModuleTree
            // 
            this.tsmiShowModuleTree.Name = "tsmiShowModuleTree";
            this.tsmiShowModuleTree.Size = new System.Drawing.Size(148, 22);
            this.tsmiShowModuleTree.Text = "显示模型树";
            this.tsmiShowModuleTree.Click += new System.EventHandler(this.tsmiShowModuleTree_Click);
            // 
            // cobCustomerId
            // 
            this.cobCustomerId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cobCustomerId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobCustomerId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobCustomerId.ContextMenuStrip = this.cmsCustomer;
            this.cobCustomerId.FormattingEnabled = true;
            this.cobCustomerId.Location = new System.Drawing.Point(476, 42);
            this.cobCustomerId.Name = "cobCustomerId";
            this.cobCustomerId.Size = new System.Drawing.Size(237, 27);
            this.cobCustomerId.TabIndex = 25;
            // 
            // cmsCustomer
            // 
            this.cmsCustomer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddCustomer});
            this.cmsCustomer.Name = "cmsCustomer";
            this.cmsCustomer.Size = new System.Drawing.Size(125, 26);
            // 
            // tsmiAddCustomer
            // 
            this.tsmiAddCustomer.Name = "tsmiAddCustomer";
            this.tsmiAddCustomer.Size = new System.Drawing.Size(124, 22);
            this.tsmiAddCustomer.Text = "添加客户";
            this.tsmiAddCustomer.Click += new System.EventHandler(this.tsmiAddCustomer_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(415, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 30;
            this.label2.Text = "客户名称";
            // 
            // txtBPONo
            // 
            this.txtBPONo.Location = new System.Drawing.Point(301, 76);
            this.txtBPONo.Name = "txtBPONo";
            this.txtBPONo.Size = new System.Drawing.Size(108, 25);
            this.txtBPONo.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(238, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 29;
            this.label5.Text = "大工单号";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(415, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 19);
            this.label8.TabIndex = 30;
            this.label8.Text = "项目名称";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProjectName.Location = new System.Drawing.Point(476, 76);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(237, 25);
            this.txtProjectName.TabIndex = 26;
            // 
            // txtProjectId
            // 
            this.txtProjectId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProjectId.Location = new System.Drawing.Point(753, 76);
            this.txtProjectId.Name = "txtProjectId";
            this.txtProjectId.ReadOnly = true;
            this.txtProjectId.Size = new System.Drawing.Size(75, 25);
            this.txtProjectId.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(724, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 19);
            this.label9.TabIndex = 30;
            this.label9.Text = "ID";
            // 
            // dgvProjects
            // 
            this.dgvProjects.AllowUserToAddRows = false;
            this.dgvProjects.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvProjects.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProjects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvProjects.BackgroundColor = System.Drawing.Color.White;
            this.dgvProjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProjects.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserAccount,
            this.ODPNo,
            this.BPONo,
            this.ProjectName,
            this.Shippingtime,
            this.ProjectStatusName,
            this.ProjectAbnormal,
            this.RiskLevel,
            this.HoodType,
            this.CustomerName,
            this.Id});
            this.dgvProjects.ContextMenuStrip = this.cmsProject;
            this.dgvProjects.EnableHeadersVisualStyles = false;
            this.dgvProjects.Location = new System.Drawing.Point(10, 106);
            this.dgvProjects.Name = "dgvProjects";
            this.dgvProjects.ReadOnly = true;
            this.dgvProjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProjects.Size = new System.Drawing.Size(928, 396);
            this.dgvProjects.TabIndex = 35;
            this.dgvProjects.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProjects_CellDoubleClick);
            this.dgvProjects.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvProjects_RowPostPaint);
            this.dgvProjects.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvProjects_KeyDown);
            // 
            // UserAccount
            // 
            this.UserAccount.DataPropertyName = "UserAccount";
            this.UserAccount.HeaderText = "制图";
            this.UserAccount.Name = "UserAccount";
            this.UserAccount.ReadOnly = true;
            this.UserAccount.Width = 60;
            // 
            // ODPNo
            // 
            this.ODPNo.DataPropertyName = "ODPNo";
            this.ODPNo.HeaderText = "ODP";
            this.ODPNo.Name = "ODPNo";
            this.ODPNo.ReadOnly = true;
            this.ODPNo.Width = 63;
            // 
            // BPONo
            // 
            this.BPONo.DataPropertyName = "BPONo";
            this.BPONo.HeaderText = "大工单号";
            this.BPONo.Name = "BPONo";
            this.BPONo.ReadOnly = true;
            this.BPONo.Width = 86;
            // 
            // ProjectName
            // 
            this.ProjectName.DataPropertyName = "ProjectName";
            this.ProjectName.HeaderText = "项目名称";
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.ReadOnly = true;
            this.ProjectName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProjectName.Width = 86;
            // 
            // Shippingtime
            // 
            this.Shippingtime.DataPropertyName = "Shippingtime";
            this.Shippingtime.HeaderText = "完工日期";
            this.Shippingtime.Name = "Shippingtime";
            this.Shippingtime.ReadOnly = true;
            this.Shippingtime.Width = 86;
            // 
            // ProjectStatusName
            // 
            this.ProjectStatusName.DataPropertyName = "ProjectStatusName";
            this.ProjectStatusName.HeaderText = "项目状态";
            this.ProjectStatusName.Name = "ProjectStatusName";
            this.ProjectStatusName.ReadOnly = true;
            this.ProjectStatusName.Width = 86;
            // 
            // ProjectAbnormal
            // 
            this.ProjectAbnormal.HeaderText = "异常状态";
            this.ProjectAbnormal.Name = "ProjectAbnormal";
            this.ProjectAbnormal.ReadOnly = true;
            this.ProjectAbnormal.Width = 86;
            // 
            // RiskLevel
            // 
            this.RiskLevel.DataPropertyName = "RiskLevel";
            this.RiskLevel.HeaderText = "等级";
            this.RiskLevel.Name = "RiskLevel";
            this.RiskLevel.ReadOnly = true;
            this.RiskLevel.Width = 60;
            // 
            // HoodType
            // 
            this.HoodType.DataPropertyName = "HoodType";
            this.HoodType.HeaderText = "烟罩类型";
            this.HoodType.Name = "HoodType";
            this.HoodType.ReadOnly = true;
            this.HoodType.Width = 86;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "客户名称";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Width = 86;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "ProjectId";
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 48;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label10.Location = new System.Drawing.Point(109, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(126, 19);
            this.label10.TabIndex = 36;
            this.label10.Text = "添加时请先选择人员";
            // 
            // cobHoodType
            // 
            this.cobHoodType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cobHoodType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobHoodType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobHoodType.FormattingEnabled = true;
            this.cobHoodType.Location = new System.Drawing.Point(301, 42);
            this.cobHoodType.Name = "cobHoodType";
            this.cobHoodType.Size = new System.Drawing.Size(108, 27);
            this.cobHoodType.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(237, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 19);
            this.label11.TabIndex = 30;
            this.label11.Text = "烟罩类型";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label12.Location = new System.Drawing.Point(472, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(152, 19);
            this.label12.TabIndex = 36;
            this.label12.Text = "右键此框添加没有的客户";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label13.Location = new System.Drawing.Point(297, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 19);
            this.label13.TabIndex = 36;
            this.label13.Text = "烟罩/天花";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtToPage);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.lblRecordsCound);
            this.groupBox1.Controls.Add(this.lblCurrentPage);
            this.groupBox1.Controls.Add(this.lblPageCount);
            this.groupBox1.Controls.Add(this.cobRecordList);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.cobQueryYear);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnLast);
            this.groupBox1.Controls.Add(this.btnNext);
            this.groupBox1.Controls.Add(this.btnPre);
            this.groupBox1.Controls.Add(this.btnFirst);
            this.groupBox1.Controls.Add(this.btnToPage);
            this.groupBox1.Controls.Add(this.btnQueryByYear);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 508);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(950, 60);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "分页显示";
            // 
            // txtToPage
            // 
            this.txtToPage.Location = new System.Drawing.Point(629, 25);
            this.txtToPage.Name = "txtToPage";
            this.txtToPage.Size = new System.Drawing.Size(34, 25);
            this.txtToPage.TabIndex = 45;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(578, 28);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(61, 19);
            this.label17.TabIndex = 43;
            this.label17.Text = "跳转到：";
            // 
            // lblRecordsCound
            // 
            this.lblRecordsCound.AutoSize = true;
            this.lblRecordsCound.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblRecordsCound.Location = new System.Drawing.Point(528, 28);
            this.lblRecordsCound.Name = "lblRecordsCound";
            this.lblRecordsCound.Size = new System.Drawing.Size(17, 19);
            this.lblRecordsCound.TabIndex = 37;
            this.lblRecordsCound.Text = "0";
            // 
            // lblCurrentPage
            // 
            this.lblCurrentPage.AutoSize = true;
            this.lblCurrentPage.ForeColor = System.Drawing.Color.DarkRed;
            this.lblCurrentPage.Location = new System.Drawing.Point(430, 28);
            this.lblCurrentPage.Name = "lblCurrentPage";
            this.lblCurrentPage.Size = new System.Drawing.Size(17, 19);
            this.lblCurrentPage.TabIndex = 38;
            this.lblCurrentPage.Text = "0";
            // 
            // lblPageCount
            // 
            this.lblPageCount.AutoSize = true;
            this.lblPageCount.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblPageCount.Location = new System.Drawing.Point(345, 28);
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.Size = new System.Drawing.Size(17, 19);
            this.lblPageCount.TabIndex = 39;
            this.lblPageCount.Text = "0";
            // 
            // cobRecordList
            // 
            this.cobRecordList.FormattingEnabled = true;
            this.cobRecordList.Items.AddRange(new object[] {
            "15",
            "30",
            "50",
            "100",
            "500",
            "1000"});
            this.cobRecordList.Location = new System.Drawing.Point(240, 24);
            this.cobRecordList.Name = "cobRecordList";
            this.cobRecordList.Size = new System.Drawing.Size(48, 27);
            this.cobRecordList.TabIndex = 36;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(669, 28);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 19);
            this.label14.TabIndex = 40;
            this.label14.Text = "页";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(308, 28);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(268, 19);
            this.label15.TabIndex = 41;
            this.label15.Text = "【共：    页 当前第：    页 记录总数：      】";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(291, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(22, 19);
            this.label16.TabIndex = 42;
            this.label16.Text = "行";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(174, 28);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(74, 19);
            this.label18.TabIndex = 44;
            this.label18.Text = "每页显示：";
            // 
            // cobQueryYear
            // 
            this.cobQueryYear.FormattingEnabled = true;
            this.cobQueryYear.Location = new System.Drawing.Point(48, 24);
            this.cobQueryYear.Name = "cobQueryYear";
            this.cobQueryYear.Size = new System.Drawing.Size(68, 27);
            this.cobQueryYear.TabIndex = 25;
            this.cobQueryYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cobUserId_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 19);
            this.label6.TabIndex = 35;
            this.label6.Text = "年度：";
            // 
            // btnLast
            // 
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.ForeColor = System.Drawing.Color.White;
            this.btnLast.Location = new System.Drawing.Point(901, 23);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(42, 28);
            this.btnLast.TabIndex = 28;
            this.btnLast.Text = ">>";
            this.btnLast.UseVisualStyleBackColor = false;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(850, 23);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(42, 28);
            this.btnNext.TabIndex = 28;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPre
            // 
            this.btnPre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnPre.FlatAppearance.BorderSize = 0;
            this.btnPre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPre.ForeColor = System.Drawing.Color.White;
            this.btnPre.Location = new System.Drawing.Point(799, 23);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(42, 28);
            this.btnPre.TabIndex = 28;
            this.btnPre.Text = "<";
            this.btnPre.UseVisualStyleBackColor = false;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFirst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.ForeColor = System.Drawing.Color.White;
            this.btnFirst.Location = new System.Drawing.Point(748, 23);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(42, 28);
            this.btnFirst.TabIndex = 28;
            this.btnFirst.Text = "<<";
            this.btnFirst.UseVisualStyleBackColor = false;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnToPage
            // 
            this.btnToPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnToPage.FlatAppearance.BorderSize = 0;
            this.btnToPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToPage.ForeColor = System.Drawing.Color.White;
            this.btnToPage.Location = new System.Drawing.Point(697, 23);
            this.btnToPage.Name = "btnToPage";
            this.btnToPage.Size = new System.Drawing.Size(42, 28);
            this.btnToPage.TabIndex = 28;
            this.btnToPage.Text = "GO";
            this.btnToPage.UseVisualStyleBackColor = false;
            this.btnToPage.Click += new System.EventHandler(this.btnToPage_Click);
            // 
            // btnQueryByYear
            // 
            this.btnQueryByYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnQueryByYear.FlatAppearance.BorderSize = 0;
            this.btnQueryByYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQueryByYear.ForeColor = System.Drawing.Color.White;
            this.btnQueryByYear.Location = new System.Drawing.Point(122, 23);
            this.btnQueryByYear.Name = "btnQueryByYear";
            this.btnQueryByYear.Size = new System.Drawing.Size(46, 28);
            this.btnQueryByYear.TabIndex = 28;
            this.btnQueryByYear.Text = "查询";
            this.btnQueryByYear.UseVisualStyleBackColor = false;
            this.btnQueryByYear.Click += new System.EventHandler(this.btnQueryByYear_Click);
            // 
            // FrmProject
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 568);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dgvProjects);
            this.Controls.Add(this.btnProject);
            this.Controls.Add(this.dtpShippingTime);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnQueryAllProjects);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnQueryByODPNo);
            this.Controls.Add(this.btnQueryByUserId);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cobHoodType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cobCustomerId);
            this.Controls.Add(this.txtProjectId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.txtBPONo);
            this.Controls.Add(this.cobUserId);
            this.Controls.Add(this.txtODPNo);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmProject";
            this.Text = "FrmProject";
            this.cmsProject.ResumeLayout(false);
            this.cmsCustomer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjects)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQueryAllProjects;
        private System.Windows.Forms.Button btnProject;
        private System.Windows.Forms.DateTimePicker dtpShippingTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnQueryByODPNo;
        private System.Windows.Forms.Button btnQueryByUserId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cobUserId;
        private System.Windows.Forms.TextBox txtODPNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cobCustomerId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip cmsCustomer;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddCustomer;
        private System.Windows.Forms.TextBox txtBPONo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.TextBox txtProjectId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ContextMenuStrip cmsProject;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditProject;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteProject;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowProjectInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowModuleTree;
        private System.Windows.Forms.DataGridView dgvProjects;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cobHoodType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODPNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BPONo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Shippingtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectStatusName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectAbnormal;
        private System.Windows.Forms.DataGridViewTextBoxColumn RiskLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoodType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cobQueryYear;
        private System.Windows.Forms.Button btnQueryByYear;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblRecordsCound;
        private System.Windows.Forms.Label lblCurrentPage;
        private System.Windows.Forms.Label lblPageCount;
        private System.Windows.Forms.ComboBox cobRecordList;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private MyUIControls.SuperTextBox txtToPage;
        private System.Windows.Forms.Button btnToPage;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.Button btnFirst;
    }
}