namespace Compass
{
    partial class FrmDrawingPlan
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDrawingPlan = new System.Windows.Forms.DataGridView();
            this.UserAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODPNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrReleaseTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProgressImg = new System.Windows.Forms.DataGridViewImageColumn();
            this.RemainingDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrReleaseActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModuleNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotalWorkload = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrawingPlanId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRequirements = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProjectTracking = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiQueryByODP = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiQueryAllPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditDrawingPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteDrawingPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddDrawingPlan = new System.Windows.Forms.Button();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtModuleNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cobModel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSubTotalWorkload = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpDrReleaseTarget = new System.Windows.Forms.DateTimePicker();
            this.btnQueryByProjectId = new System.Windows.Forms.Button();
            this.btnQueryAllPlan = new System.Windows.Forms.Button();
            this.grbEditDrawingPlan = new System.Windows.Forms.GroupBox();
            this.cobEditODPNo = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpEditAddedDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEditDrReleaseTarget = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtEditDrawingPlanId = new System.Windows.Forms.TextBox();
            this.txtEditItem = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cobEditModel = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtEditSubTotalWorkload = new System.Windows.Forms.TextBox();
            this.btnEditDrawingPlan = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEditModuleNo = new System.Windows.Forms.TextBox();
            this.btnShowInfo = new System.Windows.Forms.Button();
            this.cobODPNo = new System.Windows.Forms.ComboBox();
            this.btnQueryByUserId = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cobUserId = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawingPlan)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.grbEditDrawingPlan.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "制图计划";
            // 
            // dgvDrawingPlan
            // 
            this.dgvDrawingPlan.AllowUserToAddRows = false;
            this.dgvDrawingPlan.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvDrawingPlan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDrawingPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDrawingPlan.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDrawingPlan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDrawingPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDrawingPlan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserAccount,
            this.ODPNo,
            this.ProjectName,
            this.Item,
            this.DrReleaseTarget,
            this.ProgressImg,
            this.RemainingDays,
            this.DrReleaseActual,
            this.Model,
            this.ModuleNo,
            this.SubTotalWorkload,
            this.DrawingPlanId});
            this.dgvDrawingPlan.ContextMenuStrip = this.contextMenuStrip;
            this.dgvDrawingPlan.EnableHeadersVisualStyles = false;
            this.dgvDrawingPlan.Location = new System.Drawing.Point(12, 110);
            this.dgvDrawingPlan.Name = "dgvDrawingPlan";
            this.dgvDrawingPlan.ReadOnly = true;
            this.dgvDrawingPlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDrawingPlan.Size = new System.Drawing.Size(926, 347);
            this.dgvDrawingPlan.TabIndex = 9;
            this.dgvDrawingPlan.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDrawingPlan_CellDoubleClick);
            this.dgvDrawingPlan.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvDrawingPlan_RowPostPaint);
            this.dgvDrawingPlan.SelectionChanged += new System.EventHandler(this.dgvDrawingPlan_SelectionChanged);
            this.dgvDrawingPlan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvDrawingPlan_KeyDown);
            // 
            // UserAccount
            // 
            this.UserAccount.DataPropertyName = "UserAccount";
            this.UserAccount.HeaderText = "制图";
            this.UserAccount.Name = "UserAccount";
            this.UserAccount.ReadOnly = true;
            this.UserAccount.Width = 65;
            // 
            // ODPNo
            // 
            this.ODPNo.DataPropertyName = "ODPNo";
            this.ODPNo.HeaderText = "ODP";
            this.ODPNo.Name = "ODPNo";
            this.ODPNo.ReadOnly = true;
            this.ODPNo.Width = 95;
            // 
            // ProjectName
            // 
            this.ProjectName.DataPropertyName = "ProjectName";
            this.ProjectName.HeaderText = "项目名称";
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.ReadOnly = true;
            this.ProjectName.Width = 200;
            // 
            // Item
            // 
            this.Item.DataPropertyName = "Item";
            this.Item.HeaderText = "Item";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.Width = 130;
            // 
            // DrReleaseTarget
            // 
            this.DrReleaseTarget.DataPropertyName = "DrReleaseTarget";
            this.DrReleaseTarget.HeaderText = "发图日期";
            this.DrReleaseTarget.Name = "DrReleaseTarget";
            this.DrReleaseTarget.ReadOnly = true;
            this.DrReleaseTarget.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DrReleaseTarget.Width = 86;
            // 
            // ProgressImg
            // 
            this.ProgressImg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProgressImg.DataPropertyName = "ProgressImg";
            this.ProgressImg.HeaderText = "进度";
            this.ProgressImg.Name = "ProgressImg";
            this.ProgressImg.ReadOnly = true;
            this.ProgressImg.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProgressImg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ProgressImg.Width = 104;
            // 
            // RemainingDays
            // 
            this.RemainingDays.DataPropertyName = "RemainingDays";
            this.RemainingDays.HeaderText = "剩";
            this.RemainingDays.Name = "RemainingDays";
            this.RemainingDays.ReadOnly = true;
            this.RemainingDays.Width = 50;
            // 
            // DrReleaseActual
            // 
            this.DrReleaseActual.DataPropertyName = "DrReleaseActual";
            this.DrReleaseActual.HeaderText = "实际发图";
            this.DrReleaseActual.Name = "DrReleaseActual";
            this.DrReleaseActual.ReadOnly = true;
            this.DrReleaseActual.Width = 86;
            // 
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "烟罩型号";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            this.Model.Width = 86;
            // 
            // ModuleNo
            // 
            this.ModuleNo.DataPropertyName = "ModuleNo";
            this.ModuleNo.HeaderText = "分段";
            this.ModuleNo.Name = "ModuleNo";
            this.ModuleNo.ReadOnly = true;
            this.ModuleNo.Width = 65;
            // 
            // SubTotalWorkload
            // 
            this.SubTotalWorkload.DataPropertyName = "SubTotalWorkload";
            this.SubTotalWorkload.HeaderText = "工作量";
            this.SubTotalWorkload.Name = "SubTotalWorkload";
            this.SubTotalWorkload.ReadOnly = true;
            this.SubTotalWorkload.Width = 75;
            // 
            // DrawingPlanId
            // 
            this.DrawingPlanId.DataPropertyName = "DrawingPlanId";
            this.DrawingPlanId.HeaderText = "ID";
            this.DrawingPlanId.Name = "DrawingPlanId";
            this.DrawingPlanId.ReadOnly = true;
            this.DrawingPlanId.Width = 40;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRequirements,
            this.tsmiProjectTracking,
            this.tsmiQueryByODP,
            this.tsmiQueryAllPlan,
            this.tsmiEditDrawingPlan,
            this.tsmiDeleteDrawingPlan});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(149, 136);
            // 
            // tsmiRequirements
            // 
            this.tsmiRequirements.Name = "tsmiRequirements";
            this.tsmiRequirements.Size = new System.Drawing.Size(148, 22);
            this.tsmiRequirements.Text = "添加特殊要求";
            this.tsmiRequirements.Click += new System.EventHandler(this.tsmiRequirements_Click);
            // 
            // tsmiProjectTracking
            // 
            this.tsmiProjectTracking.Name = "tsmiProjectTracking";
            this.tsmiProjectTracking.Size = new System.Drawing.Size(148, 22);
            this.tsmiProjectTracking.Text = "项目跟踪信息";
            // 
            // tsmiQueryByODP
            // 
            this.tsmiQueryByODP.Name = "tsmiQueryByODP";
            this.tsmiQueryByODP.Size = new System.Drawing.Size(148, 22);
            this.tsmiQueryByODP.Text = "查询整个订单";
            this.tsmiQueryByODP.Click += new System.EventHandler(this.tsmiQueryByODP_Click);
            // 
            // tsmiQueryAllPlan
            // 
            this.tsmiQueryAllPlan.Name = "tsmiQueryAllPlan";
            this.tsmiQueryAllPlan.Size = new System.Drawing.Size(148, 22);
            this.tsmiQueryAllPlan.Text = "显示全部计划";
            this.tsmiQueryAllPlan.Click += new System.EventHandler(this.tsmiQueryAllPlan_Click);
            // 
            // tsmiEditDrawingPlan
            // 
            this.tsmiEditDrawingPlan.Name = "tsmiEditDrawingPlan";
            this.tsmiEditDrawingPlan.Size = new System.Drawing.Size(148, 22);
            this.tsmiEditDrawingPlan.Text = "修改计划";
            this.tsmiEditDrawingPlan.Click += new System.EventHandler(this.tsmiEditDrawingPlan_Click);
            // 
            // tsmiDeleteDrawingPlan
            // 
            this.tsmiDeleteDrawingPlan.Name = "tsmiDeleteDrawingPlan";
            this.tsmiDeleteDrawingPlan.Size = new System.Drawing.Size(148, 22);
            this.tsmiDeleteDrawingPlan.Text = "删除计划";
            this.tsmiDeleteDrawingPlan.Click += new System.EventHandler(this.tsmiDeleteDrawingPlan_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "项目编号";
            // 
            // btnAddDrawingPlan
            // 
            this.btnAddDrawingPlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAddDrawingPlan.FlatAppearance.BorderSize = 0;
            this.btnAddDrawingPlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDrawingPlan.ForeColor = System.Drawing.Color.White;
            this.btnAddDrawingPlan.Location = new System.Drawing.Point(817, 71);
            this.btnAddDrawingPlan.Name = "btnAddDrawingPlan";
            this.btnAddDrawingPlan.Size = new System.Drawing.Size(117, 28);
            this.btnAddDrawingPlan.TabIndex = 6;
            this.btnAddDrawingPlan.Text = "添加计划";
            this.btnAddDrawingPlan.UseVisualStyleBackColor = false;
            this.btnAddDrawingPlan.Click += new System.EventHandler(this.btnAddDrawingPlan_Click);
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(89, 73);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(159, 25);
            this.txtItem.TabIndex = 4;
            this.txtItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 12;
            this.label2.Text = "烟罩编号";
            // 
            // txtModuleNo
            // 
            this.txtModuleNo.Location = new System.Drawing.Point(551, 73);
            this.txtModuleNo.Name = "txtModuleNo";
            this.txtModuleNo.Size = new System.Drawing.Size(61, 25);
            this.txtModuleNo.TabIndex = 5;
            this.txtModuleNo.TextChanged += new System.EventHandler(this.txtModuleNo_TextChanged);
            this.txtModuleNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtModuleNo_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(484, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 12;
            this.label5.Text = "分段数量";
            // 
            // cobModel
            // 
            this.cobModel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobModel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobModel.FormattingEnabled = true;
            this.cobModel.Location = new System.Drawing.Point(322, 72);
            this.cobModel.Name = "cobModel";
            this.cobModel.Size = new System.Drawing.Size(160, 27);
            this.cobModel.TabIndex = 1;
            this.cobModel.SelectedIndexChanged += new System.EventHandler(this.cobModel_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(254, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 19);
            this.label6.TabIndex = 13;
            this.label6.Text = "烟罩型号";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(728, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 19);
            this.label7.TabIndex = 12;
            this.label7.Text = "计划发图日期";
            // 
            // txtSubTotalWorkload
            // 
            this.txtSubTotalWorkload.Location = new System.Drawing.Point(731, 73);
            this.txtSubTotalWorkload.Name = "txtSubTotalWorkload";
            this.txtSubTotalWorkload.Size = new System.Drawing.Size(81, 25);
            this.txtSubTotalWorkload.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(618, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 19);
            this.label8.TabIndex = 12;
            this.label8.Text = "单记录总工作量";
            // 
            // dtpDrReleaseTarget
            // 
            this.dtpDrReleaseTarget.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDrReleaseTarget.Location = new System.Drawing.Point(817, 37);
            this.dtpDrReleaseTarget.Name = "dtpDrReleaseTarget";
            this.dtpDrReleaseTarget.Size = new System.Drawing.Size(117, 25);
            this.dtpDrReleaseTarget.TabIndex = 2;
            // 
            // btnQueryByProjectId
            // 
            this.btnQueryByProjectId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnQueryByProjectId.FlatAppearance.BorderSize = 0;
            this.btnQueryByProjectId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQueryByProjectId.ForeColor = System.Drawing.Color.White;
            this.btnQueryByProjectId.Location = new System.Drawing.Point(202, 35);
            this.btnQueryByProjectId.Name = "btnQueryByProjectId";
            this.btnQueryByProjectId.Size = new System.Drawing.Size(46, 28);
            this.btnQueryByProjectId.TabIndex = 7;
            this.btnQueryByProjectId.Text = "查询";
            this.btnQueryByProjectId.UseVisualStyleBackColor = false;
            this.btnQueryByProjectId.Click += new System.EventHandler(this.btnQueryByProjectId_Click);
            // 
            // btnQueryAllPlan
            // 
            this.btnQueryAllPlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnQueryAllPlan.FlatAppearance.BorderSize = 0;
            this.btnQueryAllPlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQueryAllPlan.ForeColor = System.Drawing.Color.White;
            this.btnQueryAllPlan.Location = new System.Drawing.Point(488, 35);
            this.btnQueryAllPlan.Name = "btnQueryAllPlan";
            this.btnQueryAllPlan.Size = new System.Drawing.Size(124, 28);
            this.btnQueryAllPlan.TabIndex = 7;
            this.btnQueryAllPlan.Text = "显示全部计划";
            this.btnQueryAllPlan.UseVisualStyleBackColor = false;
            this.btnQueryAllPlan.Click += new System.EventHandler(this.btnQueryAllPlan_Click);
            // 
            // grbEditDrawingPlan
            // 
            this.grbEditDrawingPlan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbEditDrawingPlan.Controls.Add(this.cobEditODPNo);
            this.grbEditDrawingPlan.Controls.Add(this.label11);
            this.grbEditDrawingPlan.Controls.Add(this.dtpEditAddedDate);
            this.grbEditDrawingPlan.Controls.Add(this.dtpEditDrReleaseTarget);
            this.grbEditDrawingPlan.Controls.Add(this.label17);
            this.grbEditDrawingPlan.Controls.Add(this.label15);
            this.grbEditDrawingPlan.Controls.Add(this.txtEditDrawingPlanId);
            this.grbEditDrawingPlan.Controls.Add(this.txtEditItem);
            this.grbEditDrawingPlan.Controls.Add(this.label14);
            this.grbEditDrawingPlan.Controls.Add(this.cobEditModel);
            this.grbEditDrawingPlan.Controls.Add(this.label13);
            this.grbEditDrawingPlan.Controls.Add(this.txtEditSubTotalWorkload);
            this.grbEditDrawingPlan.Controls.Add(this.btnEditDrawingPlan);
            this.grbEditDrawingPlan.Controls.Add(this.label16);
            this.grbEditDrawingPlan.Controls.Add(this.label12);
            this.grbEditDrawingPlan.Controls.Add(this.label10);
            this.grbEditDrawingPlan.Controls.Add(this.txtEditModuleNo);
            this.grbEditDrawingPlan.Location = new System.Drawing.Point(12, 463);
            this.grbEditDrawingPlan.Name = "grbEditDrawingPlan";
            this.grbEditDrawingPlan.Size = new System.Drawing.Size(926, 93);
            this.grbEditDrawingPlan.TabIndex = 15;
            this.grbEditDrawingPlan.TabStop = false;
            this.grbEditDrawingPlan.Text = "修改制图计划";
            this.grbEditDrawingPlan.Visible = false;
            // 
            // cobEditODPNo
            // 
            this.cobEditODPNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobEditODPNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobEditODPNo.FormattingEnabled = true;
            this.cobEditODPNo.Location = new System.Drawing.Point(77, 23);
            this.cobEditODPNo.Name = "cobEditODPNo";
            this.cobEditODPNo.Size = new System.Drawing.Size(151, 27);
            this.cobEditODPNo.TabIndex = 41;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 19);
            this.label11.TabIndex = 12;
            this.label11.Text = "项目编号";
            // 
            // dtpEditAddedDate
            // 
            this.dtpEditAddedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEditAddedDate.Location = new System.Drawing.Point(573, 59);
            this.dtpEditAddedDate.Name = "dtpEditAddedDate";
            this.dtpEditAddedDate.Size = new System.Drawing.Size(117, 25);
            this.dtpEditAddedDate.TabIndex = 2;
            // 
            // dtpEditDrReleaseTarget
            // 
            this.dtpEditDrReleaseTarget.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEditDrReleaseTarget.Location = new System.Drawing.Point(573, 24);
            this.dtpEditDrReleaseTarget.Name = "dtpEditDrReleaseTarget";
            this.dtpEditDrReleaseTarget.Size = new System.Drawing.Size(117, 25);
            this.dtpEditDrReleaseTarget.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(472, 62);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(87, 19);
            this.label17.TabIndex = 12;
            this.label17.Text = "添加计划日期";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(472, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(87, 19);
            this.label15.TabIndex = 12;
            this.label15.Text = "计划发图日期";
            // 
            // txtEditDrawingPlanId
            // 
            this.txtEditDrawingPlanId.Location = new System.Drawing.Point(739, 24);
            this.txtEditDrawingPlanId.Name = "txtEditDrawingPlanId";
            this.txtEditDrawingPlanId.ReadOnly = true;
            this.txtEditDrawingPlanId.Size = new System.Drawing.Size(61, 25);
            this.txtEditDrawingPlanId.TabIndex = 4;
            // 
            // txtEditItem
            // 
            this.txtEditItem.Location = new System.Drawing.Point(77, 56);
            this.txtEditItem.Name = "txtEditItem";
            this.txtEditItem.Size = new System.Drawing.Size(150, 25);
            this.txtEditItem.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(237, 59);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 19);
            this.label14.TabIndex = 12;
            this.label14.Text = "分段数量";
            // 
            // cobEditModel
            // 
            this.cobEditModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobEditModel.FormattingEnabled = true;
            this.cobEditModel.Location = new System.Drawing.Point(305, 23);
            this.cobEditModel.Name = "cobEditModel";
            this.cobEditModel.Size = new System.Drawing.Size(155, 27);
            this.cobEditModel.TabIndex = 1;
            this.cobEditModel.SelectedIndexChanged += new System.EventHandler(this.cobEditModel_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(350, 59);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 19);
            this.label13.TabIndex = 12;
            this.label13.Text = "工作量";
            // 
            // txtEditSubTotalWorkload
            // 
            this.txtEditSubTotalWorkload.Location = new System.Drawing.Point(401, 56);
            this.txtEditSubTotalWorkload.Name = "txtEditSubTotalWorkload";
            this.txtEditSubTotalWorkload.Size = new System.Drawing.Size(59, 25);
            this.txtEditSubTotalWorkload.TabIndex = 7;
            // 
            // btnEditDrawingPlan
            // 
            this.btnEditDrawingPlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnEditDrawingPlan.FlatAppearance.BorderSize = 0;
            this.btnEditDrawingPlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditDrawingPlan.ForeColor = System.Drawing.Color.White;
            this.btnEditDrawingPlan.Location = new System.Drawing.Point(805, 22);
            this.btnEditDrawingPlan.Name = "btnEditDrawingPlan";
            this.btnEditDrawingPlan.Size = new System.Drawing.Size(117, 28);
            this.btnEditDrawingPlan.TabIndex = 6;
            this.btnEditDrawingPlan.Text = "修改计划";
            this.btnEditDrawingPlan.UseVisualStyleBackColor = false;
            this.btnEditDrawingPlan.Click += new System.EventHandler(this.btnEditDrawingPlan_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(701, 27);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 19);
            this.label16.TabIndex = 12;
            this.label16.Text = "ID";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 19);
            this.label12.TabIndex = 12;
            this.label12.Text = "烟罩编号";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(237, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 19);
            this.label10.TabIndex = 13;
            this.label10.Text = "烟罩型号";
            // 
            // txtEditModuleNo
            // 
            this.txtEditModuleNo.Location = new System.Drawing.Point(305, 56);
            this.txtEditModuleNo.Name = "txtEditModuleNo";
            this.txtEditModuleNo.Size = new System.Drawing.Size(45, 25);
            this.txtEditModuleNo.TabIndex = 5;
            this.txtEditModuleNo.TextChanged += new System.EventHandler(this.txtEditModuleNo_TextChanged);
            // 
            // btnShowInfo
            // 
            this.btnShowInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(136)))), ((int)(((byte)(242)))));
            this.btnShowInfo.FlatAppearance.BorderSize = 0;
            this.btnShowInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowInfo.ForeColor = System.Drawing.Color.White;
            this.btnShowInfo.Location = new System.Drawing.Point(618, 35);
            this.btnShowInfo.Name = "btnShowInfo";
            this.btnShowInfo.Size = new System.Drawing.Size(108, 28);
            this.btnShowInfo.TabIndex = 7;
            this.btnShowInfo.Text = "显示统计信息";
            this.btnShowInfo.UseVisualStyleBackColor = false;
            this.btnShowInfo.Click += new System.EventHandler(this.btnShowInfo_Click);
            // 
            // cobODPNo
            // 
            this.cobODPNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobODPNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobODPNo.FormattingEnabled = true;
            this.cobODPNo.Location = new System.Drawing.Point(88, 36);
            this.cobODPNo.Name = "cobODPNo";
            this.cobODPNo.Size = new System.Drawing.Size(108, 27);
            this.cobODPNo.TabIndex = 40;
            this.cobODPNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cobODPNo_KeyDown);
            // 
            // btnQueryByUserId
            // 
            this.btnQueryByUserId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnQueryByUserId.FlatAppearance.BorderSize = 0;
            this.btnQueryByUserId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQueryByUserId.ForeColor = System.Drawing.Color.White;
            this.btnQueryByUserId.Location = new System.Drawing.Point(436, 35);
            this.btnQueryByUserId.Name = "btnQueryByUserId";
            this.btnQueryByUserId.Size = new System.Drawing.Size(46, 28);
            this.btnQueryByUserId.TabIndex = 42;
            this.btnQueryByUserId.Text = "查询";
            this.btnQueryByUserId.UseVisualStyleBackColor = false;
            this.btnQueryByUserId.Click += new System.EventHandler(this.btnQueryByUserId_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(255, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 43;
            this.label4.Text = "制图人员";
            // 
            // cobUserId
            // 
            this.cobUserId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobUserId.FormattingEnabled = true;
            this.cobUserId.Location = new System.Drawing.Point(322, 36);
            this.cobUserId.Name = "cobUserId";
            this.cobUserId.Size = new System.Drawing.Size(108, 27);
            this.cobUserId.TabIndex = 41;
            this.cobUserId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cobUserId_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label9.Location = new System.Drawing.Point(325, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(152, 19);
            this.label9.TabIndex = 44;
            this.label9.Text = "查询用，添加时不要点击";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewImageColumn1.DataPropertyName = "PressImg";
            this.dataGridViewImageColumn1.HeaderText = "进度";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 104;
            // 
            // FrmDrawingPlan
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 568);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnQueryByUserId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cobUserId);
            this.Controls.Add(this.cobODPNo);
            this.Controls.Add(this.grbEditDrawingPlan);
            this.Controls.Add(this.dtpDrReleaseTarget);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnQueryByProjectId);
            this.Controls.Add(this.btnShowInfo);
            this.Controls.Add(this.btnQueryAllPlan);
            this.Controls.Add(this.btnAddDrawingPlan);
            this.Controls.Add(this.txtModuleNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSubTotalWorkload);
            this.Controls.Add(this.cobModel);
            this.Controls.Add(this.txtItem);
            this.Controls.Add(this.dgvDrawingPlan);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDrawingPlan";
            this.Text = "FrmDrawingPlan";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawingPlan)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.grbEditDrawingPlan.ResumeLayout(false);
            this.grbEditDrawingPlan.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDrawingPlan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddDrawingPlan;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtModuleNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cobModel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSubTotalWorkload;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpDrReleaseTarget;
        private System.Windows.Forms.Button btnQueryByProjectId;
        private System.Windows.Forms.Button btnQueryAllPlan;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditDrawingPlan;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteDrawingPlan;
        private System.Windows.Forms.GroupBox grbEditDrawingPlan;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpEditDrReleaseTarget;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtEditDrawingPlanId;
        private System.Windows.Forms.TextBox txtEditItem;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cobEditModel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtEditSubTotalWorkload;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtEditModuleNo;
        private System.Windows.Forms.Button btnEditDrawingPlan;
        private System.Windows.Forms.ToolStripMenuItem tsmiQueryByODP;
        private System.Windows.Forms.ToolStripMenuItem tsmiQueryAllPlan;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DateTimePicker dtpEditAddedDate;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnShowInfo;
        private System.Windows.Forms.ComboBox cobODPNo;
        private System.Windows.Forms.ComboBox cobEditODPNo;
        private System.Windows.Forms.ToolStripMenuItem tsmiRequirements;
        private System.Windows.Forms.ToolStripMenuItem tsmiProjectTracking;
        private System.Windows.Forms.Button btnQueryByUserId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cobUserId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODPNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrReleaseTarget;
        private System.Windows.Forms.DataGridViewImageColumn ProgressImg;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemainingDays;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrReleaseActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotalWorkload;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawingPlanId;
    }
}