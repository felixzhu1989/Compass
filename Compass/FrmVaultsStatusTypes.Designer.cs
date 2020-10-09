namespace Compass
{
    partial class FrmVaultsStatusTypes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvProjectVaults = new System.Windows.Forms.DataGridView();
            this.VaultId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VaultName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsProjectVaults = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditProjectValut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteProjectVault = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvProjectStatus = new System.Windows.Forms.DataGridView();
            this.ProjectStatusId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectStatusName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsProjectStatus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditProjectStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteProjectStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvProjectTypes = new System.Windows.Forms.DataGridView();
            this.TypeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KMLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsProjectTypes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditProjectType = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteProjectType = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditProjectVault = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVaultId = new System.Windows.Forms.TextBox();
            this.grbProjectVaults = new System.Windows.Forms.GroupBox();
            this.btnAddProjectVault = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVaultName = new System.Windows.Forms.TextBox();
            this.grbProjectStatus = new System.Windows.Forms.GroupBox();
            this.btnAddProjectStatus = new System.Windows.Forms.Button();
            this.btnEditProjectStatus = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStatusDesc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProjectStatusId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProjectStatusName = new System.Windows.Forms.TextBox();
            this.grbProjectType = new System.Windows.Forms.GroupBox();
            this.btnAddProjectType = new System.Windows.Forms.Button();
            this.btnEditProjectType = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtKMLink = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTypeName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTypeId = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjectVaults)).BeginInit();
            this.cmsProjectVaults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjectStatus)).BeginInit();
            this.cmsProjectStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjectTypes)).BeginInit();
            this.cmsProjectTypes.SuspendLayout();
            this.grbProjectVaults.SuspendLayout();
            this.grbProjectStatus.SuspendLayout();
            this.grbProjectType.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 26);
            this.label1.TabIndex = 11;
            this.label1.Text = "项目库/状态/类型";
            // 
            // dgvProjectVaults
            // 
            this.dgvProjectVaults.AllowUserToAddRows = false;
            this.dgvProjectVaults.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvProjectVaults.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProjectVaults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvProjectVaults.BackgroundColor = System.Drawing.Color.White;
            this.dgvProjectVaults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProjectVaults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProjectVaults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjectVaults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VaultId,
            this.VaultName});
            this.dgvProjectVaults.ContextMenuStrip = this.cmsProjectVaults;
            this.dgvProjectVaults.EnableHeadersVisualStyles = false;
            this.dgvProjectVaults.Location = new System.Drawing.Point(10, 160);
            this.dgvProjectVaults.Name = "dgvProjectVaults";
            this.dgvProjectVaults.ReadOnly = true;
            this.dgvProjectVaults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProjectVaults.Size = new System.Drawing.Size(241, 396);
            this.dgvProjectVaults.TabIndex = 12;
            this.dgvProjectVaults.Visible = false;
            this.dgvProjectVaults.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProjectVaults_CellDoubleClick);
            this.dgvProjectVaults.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvProjectVaults_RowPostPaint);
            this.dgvProjectVaults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvProjectVaults_KeyDown);
            // 
            // VaultId
            // 
            this.VaultId.DataPropertyName = "VaultId";
            this.VaultId.HeaderText = "序号";
            this.VaultId.Name = "VaultId";
            this.VaultId.ReadOnly = true;
            this.VaultId.Width = 60;
            // 
            // VaultName
            // 
            this.VaultName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.VaultName.DataPropertyName = "VaultName";
            this.VaultName.HeaderText = "库名称";
            this.VaultName.Name = "VaultName";
            this.VaultName.ReadOnly = true;
            // 
            // cmsProjectVaults
            // 
            this.cmsProjectVaults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditProjectValut,
            this.tsmiDeleteProjectVault});
            this.cmsProjectVaults.Name = "cmsProjectVaults";
            this.cmsProjectVaults.Size = new System.Drawing.Size(166, 48);
            // 
            // tsmiEditProjectValut
            // 
            this.tsmiEditProjectValut.Name = "tsmiEditProjectValut";
            this.tsmiEditProjectValut.Size = new System.Drawing.Size(165, 22);
            this.tsmiEditProjectValut.Text = "修改项目库名称";
            this.tsmiEditProjectValut.Click += new System.EventHandler(this.tsmiEditProjectValut_Click);
            // 
            // tsmiDeleteProjectVault
            // 
            this.tsmiDeleteProjectVault.Name = "tsmiDeleteProjectVault";
            this.tsmiDeleteProjectVault.Size = new System.Drawing.Size(165, 22);
            this.tsmiDeleteProjectVault.Text = "删除项目库名称";
            this.tsmiDeleteProjectVault.Click += new System.EventHandler(this.tsmiDeleteProjectVault_Click);
            // 
            // dgvProjectStatus
            // 
            this.dgvProjectStatus.AllowUserToAddRows = false;
            this.dgvProjectStatus.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Azure;
            this.dgvProjectStatus.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvProjectStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvProjectStatus.BackgroundColor = System.Drawing.Color.White;
            this.dgvProjectStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProjectStatus.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvProjectStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjectStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProjectStatusId,
            this.ProjectStatusName,
            this.StatusDesc});
            this.dgvProjectStatus.ContextMenuStrip = this.cmsProjectStatus;
            this.dgvProjectStatus.EnableHeadersVisualStyles = false;
            this.dgvProjectStatus.Location = new System.Drawing.Point(257, 160);
            this.dgvProjectStatus.Name = "dgvProjectStatus";
            this.dgvProjectStatus.ReadOnly = true;
            this.dgvProjectStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProjectStatus.Size = new System.Drawing.Size(348, 396);
            this.dgvProjectStatus.TabIndex = 12;
            this.dgvProjectStatus.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProjectStatus_CellDoubleClick);
            this.dgvProjectStatus.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvProjectStatus_RowPostPaint);
            this.dgvProjectStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvProjectStatus_KeyDown);
            // 
            // ProjectStatusId
            // 
            this.ProjectStatusId.DataPropertyName = "ProjectStatusId";
            this.ProjectStatusId.HeaderText = "序号";
            this.ProjectStatusId.Name = "ProjectStatusId";
            this.ProjectStatusId.ReadOnly = true;
            this.ProjectStatusId.Width = 60;
            // 
            // ProjectStatusName
            // 
            this.ProjectStatusName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProjectStatusName.DataPropertyName = "ProjectStatusName";
            this.ProjectStatusName.HeaderText = "状态名称";
            this.ProjectStatusName.Name = "ProjectStatusName";
            this.ProjectStatusName.ReadOnly = true;
            // 
            // StatusDesc
            // 
            this.StatusDesc.DataPropertyName = "StatusDesc";
            this.StatusDesc.HeaderText = "状态描述";
            this.StatusDesc.Name = "StatusDesc";
            this.StatusDesc.ReadOnly = true;
            this.StatusDesc.Width = 90;
            // 
            // cmsProjectStatus
            // 
            this.cmsProjectStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditProjectStatus,
            this.tsmiDeleteProjectStatus});
            this.cmsProjectStatus.Name = "cmsProjectStatus";
            this.cmsProjectStatus.Size = new System.Drawing.Size(153, 48);
            // 
            // tsmiEditProjectStatus
            // 
            this.tsmiEditProjectStatus.Name = "tsmiEditProjectStatus";
            this.tsmiEditProjectStatus.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditProjectStatus.Text = "修改项目状态";
            this.tsmiEditProjectStatus.Click += new System.EventHandler(this.tsmiEditProjectStatus_Click);
            // 
            // tsmiDeleteProjectStatus
            // 
            this.tsmiDeleteProjectStatus.Name = "tsmiDeleteProjectStatus";
            this.tsmiDeleteProjectStatus.Size = new System.Drawing.Size(152, 22);
            this.tsmiDeleteProjectStatus.Text = "删除项目状态";
            this.tsmiDeleteProjectStatus.Click += new System.EventHandler(this.tsmiDeleteProjectStatus_Click);
            // 
            // dgvProjectTypes
            // 
            this.dgvProjectTypes.AllowUserToAddRows = false;
            this.dgvProjectTypes.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Azure;
            this.dgvProjectTypes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvProjectTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvProjectTypes.BackgroundColor = System.Drawing.Color.White;
            this.dgvProjectTypes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProjectTypes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvProjectTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjectTypes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TypeId,
            this.TypeName,
            this.KMLink});
            this.dgvProjectTypes.ContextMenuStrip = this.cmsProjectTypes;
            this.dgvProjectTypes.EnableHeadersVisualStyles = false;
            this.dgvProjectTypes.Location = new System.Drawing.Point(611, 160);
            this.dgvProjectTypes.Name = "dgvProjectTypes";
            this.dgvProjectTypes.ReadOnly = true;
            this.dgvProjectTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProjectTypes.Size = new System.Drawing.Size(327, 396);
            this.dgvProjectTypes.TabIndex = 12;
            this.dgvProjectTypes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProjectTypes_CellDoubleClick);
            this.dgvProjectTypes.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvProjectTypes_RowPostPaint);
            this.dgvProjectTypes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvProjectTypes_KeyDown);
            // 
            // TypeId
            // 
            this.TypeId.DataPropertyName = "TypeId";
            this.TypeId.HeaderText = "序号";
            this.TypeId.Name = "TypeId";
            this.TypeId.ReadOnly = true;
            this.TypeId.Width = 60;
            // 
            // TypeName
            // 
            this.TypeName.DataPropertyName = "TypeName";
            this.TypeName.HeaderText = "类型名称";
            this.TypeName.Name = "TypeName";
            this.TypeName.ReadOnly = true;
            // 
            // KMLink
            // 
            this.KMLink.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.KMLink.DataPropertyName = "KMLink";
            this.KMLink.HeaderText = "帮助链接";
            this.KMLink.Name = "KMLink";
            this.KMLink.ReadOnly = true;
            // 
            // cmsProjectTypes
            // 
            this.cmsProjectTypes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditProjectType,
            this.tsmiDeleteProjectType});
            this.cmsProjectTypes.Name = "cmsProjectTypes";
            this.cmsProjectTypes.Size = new System.Drawing.Size(153, 48);
            // 
            // tsmiEditProjectType
            // 
            this.tsmiEditProjectType.Name = "tsmiEditProjectType";
            this.tsmiEditProjectType.Size = new System.Drawing.Size(152, 22);
            this.tsmiEditProjectType.Text = "修改项目类型";
            this.tsmiEditProjectType.Click += new System.EventHandler(this.tsmiEditProjectType_Click);
            // 
            // tsmiDeleteProjectType
            // 
            this.tsmiDeleteProjectType.Name = "tsmiDeleteProjectType";
            this.tsmiDeleteProjectType.Size = new System.Drawing.Size(152, 22);
            this.tsmiDeleteProjectType.Text = "删除项目类型";
            this.tsmiDeleteProjectType.Click += new System.EventHandler(this.tsmiDeleteProjectType_Click);
            // 
            // btnEditProjectVault
            // 
            this.btnEditProjectVault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnEditProjectVault.FlatAppearance.BorderSize = 0;
            this.btnEditProjectVault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditProjectVault.ForeColor = System.Drawing.Color.White;
            this.btnEditProjectVault.Location = new System.Drawing.Point(124, 84);
            this.btnEditProjectVault.Name = "btnEditProjectVault";
            this.btnEditProjectVault.Size = new System.Drawing.Size(108, 28);
            this.btnEditProjectVault.TabIndex = 2;
            this.btnEditProjectVault.Text = "修改项目库";
            this.btnEditProjectVault.UseVisualStyleBackColor = false;
            this.btnEditProjectVault.Click += new System.EventHandler(this.btnEditProjectVault_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 24;
            this.label3.Text = "项目库序号";
            // 
            // txtVaultId
            // 
            this.txtVaultId.Location = new System.Drawing.Point(124, 23);
            this.txtVaultId.Name = "txtVaultId";
            this.txtVaultId.ReadOnly = true;
            this.txtVaultId.Size = new System.Drawing.Size(108, 25);
            this.txtVaultId.TabIndex = 23;
            // 
            // grbProjectVaults
            // 
            this.grbProjectVaults.Controls.Add(this.btnAddProjectVault);
            this.grbProjectVaults.Controls.Add(this.btnEditProjectVault);
            this.grbProjectVaults.Controls.Add(this.label2);
            this.grbProjectVaults.Controls.Add(this.txtVaultName);
            this.grbProjectVaults.Controls.Add(this.label3);
            this.grbProjectVaults.Controls.Add(this.txtVaultId);
            this.grbProjectVaults.Location = new System.Drawing.Point(10, 34);
            this.grbProjectVaults.Name = "grbProjectVaults";
            this.grbProjectVaults.Size = new System.Drawing.Size(241, 120);
            this.grbProjectVaults.TabIndex = 0;
            this.grbProjectVaults.TabStop = false;
            this.grbProjectVaults.Text = "项目库管理";
            this.grbProjectVaults.Visible = false;
            // 
            // btnAddProjectVault
            // 
            this.btnAddProjectVault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAddProjectVault.FlatAppearance.BorderSize = 0;
            this.btnAddProjectVault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddProjectVault.ForeColor = System.Drawing.Color.White;
            this.btnAddProjectVault.Location = new System.Drawing.Point(10, 84);
            this.btnAddProjectVault.Name = "btnAddProjectVault";
            this.btnAddProjectVault.Size = new System.Drawing.Size(108, 28);
            this.btnAddProjectVault.TabIndex = 1;
            this.btnAddProjectVault.Text = "添加项目库";
            this.btnAddProjectVault.UseVisualStyleBackColor = false;
            this.btnAddProjectVault.Click += new System.EventHandler(this.btnAddProjectVault_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 24;
            this.label2.Text = "项目库名称";
            // 
            // txtVaultName
            // 
            this.txtVaultName.Location = new System.Drawing.Point(124, 53);
            this.txtVaultName.Name = "txtVaultName";
            this.txtVaultName.Size = new System.Drawing.Size(108, 25);
            this.txtVaultName.TabIndex = 0;
            this.txtVaultName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVaultName_KeyDown);
            // 
            // grbProjectStatus
            // 
            this.grbProjectStatus.Controls.Add(this.btnAddProjectStatus);
            this.grbProjectStatus.Controls.Add(this.btnEditProjectStatus);
            this.grbProjectStatus.Controls.Add(this.label4);
            this.grbProjectStatus.Controls.Add(this.txtStatusDesc);
            this.grbProjectStatus.Controls.Add(this.label8);
            this.grbProjectStatus.Controls.Add(this.txtProjectStatusId);
            this.grbProjectStatus.Controls.Add(this.label5);
            this.grbProjectStatus.Controls.Add(this.txtProjectStatusName);
            this.grbProjectStatus.Location = new System.Drawing.Point(257, 34);
            this.grbProjectStatus.Name = "grbProjectStatus";
            this.grbProjectStatus.Size = new System.Drawing.Size(348, 120);
            this.grbProjectStatus.TabIndex = 0;
            this.grbProjectStatus.TabStop = false;
            this.grbProjectStatus.Text = "项目状态管理";
            // 
            // btnAddProjectStatus
            // 
            this.btnAddProjectStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAddProjectStatus.FlatAppearance.BorderSize = 0;
            this.btnAddProjectStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddProjectStatus.ForeColor = System.Drawing.Color.White;
            this.btnAddProjectStatus.Location = new System.Drawing.Point(93, 84);
            this.btnAddProjectStatus.Name = "btnAddProjectStatus";
            this.btnAddProjectStatus.Size = new System.Drawing.Size(108, 28);
            this.btnAddProjectStatus.TabIndex = 2;
            this.btnAddProjectStatus.Text = "添加状态";
            this.btnAddProjectStatus.UseVisualStyleBackColor = false;
            this.btnAddProjectStatus.Click += new System.EventHandler(this.btnAddProjectStatus_Click);
            // 
            // btnEditProjectStatus
            // 
            this.btnEditProjectStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnEditProjectStatus.FlatAppearance.BorderSize = 0;
            this.btnEditProjectStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditProjectStatus.ForeColor = System.Drawing.Color.White;
            this.btnEditProjectStatus.Location = new System.Drawing.Point(234, 84);
            this.btnEditProjectStatus.Name = "btnEditProjectStatus";
            this.btnEditProjectStatus.Size = new System.Drawing.Size(108, 28);
            this.btnEditProjectStatus.TabIndex = 2;
            this.btnEditProjectStatus.Text = "修改状态";
            this.btnEditProjectStatus.UseVisualStyleBackColor = false;
            this.btnEditProjectStatus.Click += new System.EventHandler(this.btnEditProjectStatus_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 24;
            this.label4.Text = "状态描述";
            // 
            // txtStatusDesc
            // 
            this.txtStatusDesc.Location = new System.Drawing.Point(93, 53);
            this.txtStatusDesc.Name = "txtStatusDesc";
            this.txtStatusDesc.Size = new System.Drawing.Size(108, 25);
            this.txtStatusDesc.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(230, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 19);
            this.label8.TabIndex = 24;
            this.label8.Text = "状态序号";
            // 
            // txtProjectStatusId
            // 
            this.txtProjectStatusId.Location = new System.Drawing.Point(234, 53);
            this.txtProjectStatusId.Name = "txtProjectStatusId";
            this.txtProjectStatusId.ReadOnly = true;
            this.txtProjectStatusId.Size = new System.Drawing.Size(108, 25);
            this.txtProjectStatusId.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 24;
            this.label5.Text = "状态名称";
            // 
            // txtProjectStatusName
            // 
            this.txtProjectStatusName.Location = new System.Drawing.Point(93, 23);
            this.txtProjectStatusName.Name = "txtProjectStatusName";
            this.txtProjectStatusName.Size = new System.Drawing.Size(108, 25);
            this.txtProjectStatusName.TabIndex = 0;
            this.txtProjectStatusName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProjectStatusName_KeyDown);
            // 
            // grbProjectType
            // 
            this.grbProjectType.Controls.Add(this.btnAddProjectType);
            this.grbProjectType.Controls.Add(this.btnEditProjectType);
            this.grbProjectType.Controls.Add(this.label9);
            this.grbProjectType.Controls.Add(this.txtKMLink);
            this.grbProjectType.Controls.Add(this.label6);
            this.grbProjectType.Controls.Add(this.txtTypeName);
            this.grbProjectType.Controls.Add(this.label7);
            this.grbProjectType.Controls.Add(this.txtTypeId);
            this.grbProjectType.Location = new System.Drawing.Point(611, 34);
            this.grbProjectType.Name = "grbProjectType";
            this.grbProjectType.Size = new System.Drawing.Size(327, 120);
            this.grbProjectType.TabIndex = 0;
            this.grbProjectType.TabStop = false;
            this.grbProjectType.Text = "项目类型管理";
            // 
            // btnAddProjectType
            // 
            this.btnAddProjectType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAddProjectType.FlatAppearance.BorderSize = 0;
            this.btnAddProjectType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddProjectType.ForeColor = System.Drawing.Color.White;
            this.btnAddProjectType.Location = new System.Drawing.Point(93, 84);
            this.btnAddProjectType.Name = "btnAddProjectType";
            this.btnAddProjectType.Size = new System.Drawing.Size(108, 28);
            this.btnAddProjectType.TabIndex = 2;
            this.btnAddProjectType.Text = "添加类型";
            this.btnAddProjectType.UseVisualStyleBackColor = false;
            this.btnAddProjectType.Click += new System.EventHandler(this.btnAddProjectType_Click);
            // 
            // btnEditProjectType
            // 
            this.btnEditProjectType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnEditProjectType.FlatAppearance.BorderSize = 0;
            this.btnEditProjectType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditProjectType.ForeColor = System.Drawing.Color.White;
            this.btnEditProjectType.Location = new System.Drawing.Point(212, 84);
            this.btnEditProjectType.Name = "btnEditProjectType";
            this.btnEditProjectType.Size = new System.Drawing.Size(108, 28);
            this.btnEditProjectType.TabIndex = 2;
            this.btnEditProjectType.Text = "修改类型";
            this.btnEditProjectType.UseVisualStyleBackColor = false;
            this.btnEditProjectType.Click += new System.EventHandler(this.btnEditProjectType_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 19);
            this.label9.TabIndex = 24;
            this.label9.Text = "帮助链接";
            // 
            // txtKMLink
            // 
            this.txtKMLink.Location = new System.Drawing.Point(93, 53);
            this.txtKMLink.Name = "txtKMLink";
            this.txtKMLink.Size = new System.Drawing.Size(108, 25);
            this.txtKMLink.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 19);
            this.label6.TabIndex = 24;
            this.label6.Text = "类型名称";
            // 
            // txtTypeName
            // 
            this.txtTypeName.Location = new System.Drawing.Point(93, 23);
            this.txtTypeName.Name = "txtTypeName";
            this.txtTypeName.Size = new System.Drawing.Size(108, 25);
            this.txtTypeName.TabIndex = 0;
            this.txtTypeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTypeName_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(208, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 19);
            this.label7.TabIndex = 24;
            this.label7.Text = "类型序号";
            // 
            // txtTypeId
            // 
            this.txtTypeId.Location = new System.Drawing.Point(212, 53);
            this.txtTypeId.Name = "txtTypeId";
            this.txtTypeId.ReadOnly = true;
            this.txtTypeId.Size = new System.Drawing.Size(108, 25);
            this.txtTypeId.TabIndex = 23;
            // 
            // FrmVaultsStatusTypes
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 568);
            this.Controls.Add(this.dgvProjectTypes);
            this.Controls.Add(this.dgvProjectStatus);
            this.Controls.Add(this.dgvProjectVaults);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grbProjectStatus);
            this.Controls.Add(this.grbProjectType);
            this.Controls.Add(this.grbProjectVaults);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmVaultsStatusTypes";
            this.Text = "FrmVaultsStatusTypes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjectVaults)).EndInit();
            this.cmsProjectVaults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjectStatus)).EndInit();
            this.cmsProjectStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjectTypes)).EndInit();
            this.cmsProjectTypes.ResumeLayout(false);
            this.grbProjectVaults.ResumeLayout(false);
            this.grbProjectVaults.PerformLayout();
            this.grbProjectStatus.ResumeLayout(false);
            this.grbProjectStatus.PerformLayout();
            this.grbProjectType.ResumeLayout(false);
            this.grbProjectType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvProjectVaults;
        private System.Windows.Forms.DataGridView dgvProjectStatus;
        private System.Windows.Forms.DataGridView dgvProjectTypes;
        private System.Windows.Forms.Button btnEditProjectVault;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVaultId;
        private System.Windows.Forms.GroupBox grbProjectVaults;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVaultName;
        private System.Windows.Forms.Button btnAddProjectVault;
        private System.Windows.Forms.GroupBox grbProjectStatus;
        private System.Windows.Forms.Button btnAddProjectStatus;
        private System.Windows.Forms.Button btnEditProjectStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtStatusDesc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProjectStatusName;
        private System.Windows.Forms.GroupBox grbProjectType;
        private System.Windows.Forms.Button btnAddProjectType;
        private System.Windows.Forms.Button btnEditProjectType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTypeName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTypeId;
        private System.Windows.Forms.ContextMenuStrip cmsProjectVaults;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditProjectValut;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteProjectVault;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProjectStatusId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtKMLink;
        private System.Windows.Forms.ContextMenuStrip cmsProjectStatus;
        private System.Windows.Forms.ContextMenuStrip cmsProjectTypes;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditProjectStatus;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteProjectStatus;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditProjectType;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteProjectType;
        private System.Windows.Forms.DataGridViewTextBoxColumn VaultId;
        private System.Windows.Forms.DataGridViewTextBoxColumn VaultName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn KMLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectStatusId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectStatusName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusDesc;
    }
}