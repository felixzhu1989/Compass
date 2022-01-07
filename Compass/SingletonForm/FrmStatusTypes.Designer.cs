namespace Compass
{
    partial class FrmStatusTypes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStatusTypes));
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjectStatus)).BeginInit();
            this.cmsProjectStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjectTypes)).BeginInit();
            this.cmsProjectTypes.SuspendLayout();
            this.grbProjectStatus.SuspendLayout();
            this.grbProjectType.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvProjectStatus
            // 
            this.dgvProjectStatus.AllowUserToAddRows = false;
            this.dgvProjectStatus.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvProjectStatus.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProjectStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvProjectStatus.BackgroundColor = System.Drawing.Color.White;
            this.dgvProjectStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProjectStatus.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProjectStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjectStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProjectStatusId,
            this.ProjectStatusName,
            this.StatusDesc});
            this.dgvProjectStatus.ContextMenuStrip = this.cmsProjectStatus;
            this.dgvProjectStatus.EnableHeadersVisualStyles = false;
            this.dgvProjectStatus.Location = new System.Drawing.Point(12, 185);
            this.dgvProjectStatus.Name = "dgvProjectStatus";
            this.dgvProjectStatus.ReadOnly = true;
            this.dgvProjectStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProjectStatus.Size = new System.Drawing.Size(348, 356);
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
            this.cmsProjectStatus.Size = new System.Drawing.Size(149, 48);
            // 
            // tsmiEditProjectStatus
            // 
            this.tsmiEditProjectStatus.Name = "tsmiEditProjectStatus";
            this.tsmiEditProjectStatus.Size = new System.Drawing.Size(148, 22);
            this.tsmiEditProjectStatus.Text = "修改项目状态";
            this.tsmiEditProjectStatus.Click += new System.EventHandler(this.tsmiEditProjectStatus_Click);
            // 
            // tsmiDeleteProjectStatus
            // 
            this.tsmiDeleteProjectStatus.Name = "tsmiDeleteProjectStatus";
            this.tsmiDeleteProjectStatus.Size = new System.Drawing.Size(148, 22);
            this.tsmiDeleteProjectStatus.Text = "删除项目状态";
            this.tsmiDeleteProjectStatus.Click += new System.EventHandler(this.tsmiDeleteProjectStatus_Click);
            // 
            // dgvProjectTypes
            // 
            this.dgvProjectTypes.AllowUserToAddRows = false;
            this.dgvProjectTypes.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Azure;
            this.dgvProjectTypes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvProjectTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvProjectTypes.BackgroundColor = System.Drawing.Color.White;
            this.dgvProjectTypes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProjectTypes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvProjectTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjectTypes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TypeId,
            this.TypeName,
            this.KMLink});
            this.dgvProjectTypes.ContextMenuStrip = this.cmsProjectTypes;
            this.dgvProjectTypes.EnableHeadersVisualStyles = false;
            this.dgvProjectTypes.Location = new System.Drawing.Point(366, 185);
            this.dgvProjectTypes.Name = "dgvProjectTypes";
            this.dgvProjectTypes.ReadOnly = true;
            this.dgvProjectTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProjectTypes.Size = new System.Drawing.Size(327, 356);
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
            this.cmsProjectTypes.Size = new System.Drawing.Size(149, 48);
            // 
            // tsmiEditProjectType
            // 
            this.tsmiEditProjectType.Name = "tsmiEditProjectType";
            this.tsmiEditProjectType.Size = new System.Drawing.Size(148, 22);
            this.tsmiEditProjectType.Text = "修改项目类型";
            this.tsmiEditProjectType.Click += new System.EventHandler(this.tsmiEditProjectType_Click);
            // 
            // tsmiDeleteProjectType
            // 
            this.tsmiDeleteProjectType.Name = "tsmiDeleteProjectType";
            this.tsmiDeleteProjectType.Size = new System.Drawing.Size(148, 22);
            this.tsmiDeleteProjectType.Text = "删除项目类型";
            this.tsmiDeleteProjectType.Click += new System.EventHandler(this.tsmiDeleteProjectType_Click);
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
            this.grbProjectStatus.Location = new System.Drawing.Point(12, 59);
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
            this.btnAddProjectStatus.Location = new System.Drawing.Point(93, 109);
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
            this.btnEditProjectStatus.Location = new System.Drawing.Point(234, 109);
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
            this.label4.Location = new System.Drawing.Point(6, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 24;
            this.label4.Text = "状态描述";
            // 
            // txtStatusDesc
            // 
            this.txtStatusDesc.Location = new System.Drawing.Point(93, 78);
            this.txtStatusDesc.Name = "txtStatusDesc";
            this.txtStatusDesc.Size = new System.Drawing.Size(108, 25);
            this.txtStatusDesc.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(230, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 19);
            this.label8.TabIndex = 24;
            this.label8.Text = "状态序号";
            // 
            // txtProjectStatusId
            // 
            this.txtProjectStatusId.Location = new System.Drawing.Point(234, 78);
            this.txtProjectStatusId.Name = "txtProjectStatusId";
            this.txtProjectStatusId.ReadOnly = true;
            this.txtProjectStatusId.Size = new System.Drawing.Size(108, 25);
            this.txtProjectStatusId.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 24;
            this.label5.Text = "状态名称";
            // 
            // txtProjectStatusName
            // 
            this.txtProjectStatusName.Location = new System.Drawing.Point(93, 48);
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
            this.grbProjectType.Location = new System.Drawing.Point(366, 59);
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
            this.btnAddProjectType.Location = new System.Drawing.Point(93, 109);
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
            this.btnEditProjectType.Location = new System.Drawing.Point(212, 109);
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
            this.label9.Location = new System.Drawing.Point(5, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 19);
            this.label9.TabIndex = 24;
            this.label9.Text = "帮助链接";
            // 
            // txtKMLink
            // 
            this.txtKMLink.Location = new System.Drawing.Point(93, 78);
            this.txtKMLink.Name = "txtKMLink";
            this.txtKMLink.Size = new System.Drawing.Size(108, 25);
            this.txtKMLink.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 19);
            this.label6.TabIndex = 24;
            this.label6.Text = "类型名称";
            // 
            // txtTypeName
            // 
            this.txtTypeName.Location = new System.Drawing.Point(93, 48);
            this.txtTypeName.Name = "txtTypeName";
            this.txtTypeName.Size = new System.Drawing.Size(108, 25);
            this.txtTypeName.TabIndex = 0;
            this.txtTypeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTypeName_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(208, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 19);
            this.label7.TabIndex = 24;
            this.label7.Text = "类型序号";
            // 
            // txtTypeId
            // 
            this.txtTypeId.Location = new System.Drawing.Point(212, 78);
            this.txtTypeId.Name = "txtTypeId";
            this.txtTypeId.ReadOnly = true;
            this.txtTypeId.Size = new System.Drawing.Size(108, 25);
            this.txtTypeId.TabIndex = 23;
            // 
            // FrmStatusTypes
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(950, 568);
            this.Controls.Add(this.dgvProjectTypes);
            this.Controls.Add(this.dgvProjectStatus);
            this.Controls.Add(this.grbProjectStatus);
            this.Controls.Add(this.grbProjectType);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmStatusTypes";
            this.Text = "项目状态/类型管理";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjectStatus)).EndInit();
            this.cmsProjectStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjectTypes)).EndInit();
            this.cmsProjectTypes.ResumeLayout(false);
            this.grbProjectStatus.ResumeLayout(false);
            this.grbProjectStatus.PerformLayout();
            this.grbProjectType.ResumeLayout(false);
            this.grbProjectType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvProjectStatus;
        private System.Windows.Forms.DataGridView dgvProjectTypes;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn KMLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectStatusId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectStatusName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusDesc;
    }
}