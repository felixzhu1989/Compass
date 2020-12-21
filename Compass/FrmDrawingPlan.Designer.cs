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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDrawingPlan = new System.Windows.Forms.DataGridView();
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
            this.cobODPNo = new System.Windows.Forms.ComboBox();
            this.btnQueryByUserId = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cobUserId = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtToPage = new MyUIControls.SuperTextBox(this.components);
            this.label18 = new System.Windows.Forms.Label();
            this.lblRecordsCound = new System.Windows.Forms.Label();
            this.lblCurrentPage = new System.Windows.Forms.Label();
            this.lblPageCount = new System.Windows.Forms.Label();
            this.cobRecordList = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cobQueryYear = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnToPage = new System.Windows.Forms.Button();
            this.btnQueryByYear = new System.Windows.Forms.Button();
            this.btnDrawingPlanQuery = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.UserAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODPNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrReleaseTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProgressValue = new Common.DataGridViewProgressColumn();
            this.RemainingDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrReleaseActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModuleNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotalWorkload = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrawingPlanId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoodType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawingPlan)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.grbEditDrawingPlan.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.dgvDrawingPlan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDrawingPlan.BackgroundColor = System.Drawing.Color.White;
            this.dgvDrawingPlan.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            this.ProgressValue,
            this.RemainingDays,
            this.DrReleaseActual,
            this.Model,
            this.ModuleNo,
            this.SubTotalWorkload,
            this.DrawingPlanId,
            this.HoodType});
            this.dgvDrawingPlan.ContextMenuStrip = this.contextMenuStrip;
            this.dgvDrawingPlan.EnableHeadersVisualStyles = false;
            this.dgvDrawingPlan.Location = new System.Drawing.Point(12, 110);
            this.dgvDrawingPlan.Name = "dgvDrawingPlan";
            this.dgvDrawingPlan.ReadOnly = true;
            this.dgvDrawingPlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDrawingPlan.Size = new System.Drawing.Size(926, 392);
            this.dgvDrawingPlan.TabIndex = 9;
            this.dgvDrawingPlan.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDrawingPlan_CellDoubleClick);
            this.dgvDrawingPlan.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvDrawingPlan_RowPostPaint);
            this.dgvDrawingPlan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvDrawingPlan_KeyDown);
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
            this.btnQueryByProjectId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
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
            this.grbEditDrawingPlan.Location = new System.Drawing.Point(10, 136);
            this.grbEditDrawingPlan.Name = "grbEditDrawingPlan";
            this.grbEditDrawingPlan.Size = new System.Drawing.Size(928, 93);
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
            this.btnQueryByUserId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtToPage);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.lblRecordsCound);
            this.groupBox1.Controls.Add(this.lblCurrentPage);
            this.groupBox1.Controls.Add(this.lblPageCount);
            this.groupBox1.Controls.Add(this.cobRecordList);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.cobQueryYear);
            this.groupBox1.Controls.Add(this.label23);
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
            this.groupBox1.TabIndex = 45;
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
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(578, 28);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(61, 19);
            this.label18.TabIndex = 43;
            this.label18.Text = "跳转到：";
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
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(669, 28);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(22, 19);
            this.label19.TabIndex = 40;
            this.label19.Text = "页";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(308, 28);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(268, 19);
            this.label20.TabIndex = 41;
            this.label20.Text = "【共：    页 当前第：    页 记录总数：      】";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(291, 28);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(22, 19);
            this.label21.TabIndex = 42;
            this.label21.Text = "行";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(174, 28);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(74, 19);
            this.label22.TabIndex = 44;
            this.label22.Text = "每页显示：";
            // 
            // cobQueryYear
            // 
            this.cobQueryYear.FormattingEnabled = true;
            this.cobQueryYear.Location = new System.Drawing.Point(48, 24);
            this.cobQueryYear.Name = "cobQueryYear";
            this.cobQueryYear.Size = new System.Drawing.Size(68, 27);
            this.cobQueryYear.TabIndex = 25;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(3, 28);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(48, 19);
            this.label23.TabIndex = 35;
            this.label23.Text = "年度：";
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
            // btnDrawingPlanQuery
            // 
            this.btnDrawingPlanQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(72)))), ((int)(((byte)(232)))));
            this.btnDrawingPlanQuery.FlatAppearance.BorderSize = 0;
            this.btnDrawingPlanQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrawingPlanQuery.ForeColor = System.Drawing.Color.White;
            this.btnDrawingPlanQuery.Location = new System.Drawing.Point(618, 35);
            this.btnDrawingPlanQuery.Name = "btnDrawingPlanQuery";
            this.btnDrawingPlanQuery.Size = new System.Drawing.Size(108, 28);
            this.btnDrawingPlanQuery.TabIndex = 7;
            this.btnDrawingPlanQuery.Text = "统计信息";
            this.btnDrawingPlanQuery.UseVisualStyleBackColor = false;
            this.btnDrawingPlanQuery.Click += new System.EventHandler(this.btnDrawingPlanQuery_Click);
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
            // ProjectName
            // 
            this.ProjectName.DataPropertyName = "ProjectName";
            this.ProjectName.HeaderText = "项目名称";
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.ReadOnly = true;
            this.ProjectName.Width = 86;
            // 
            // Item
            // 
            this.Item.DataPropertyName = "Item";
            this.Item.HeaderText = "Item";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.Width = 62;
            // 
            // DrReleaseTarget
            // 
            this.DrReleaseTarget.DataPropertyName = "DrReleaseTarget";
            this.DrReleaseTarget.HeaderText = "计划发图";
            this.DrReleaseTarget.Name = "DrReleaseTarget";
            this.DrReleaseTarget.ReadOnly = true;
            this.DrReleaseTarget.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DrReleaseTarget.Width = 86;
            // 
            // ProgressValue
            // 
            this.ProgressValue.DataPropertyName = "ProgressValue";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.NullValue = "System.Drawing.Bitmap";
            this.ProgressValue.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProgressValue.HeaderText = "进度条";
            this.ProgressValue.Name = "ProgressValue";
            this.ProgressValue.ReadOnly = true;
            this.ProgressValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProgressValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ProgressValue.ToolTipText = "(今日-添加日)/(计划日-添加日)";
            this.ProgressValue.Width = 73;
            // 
            // RemainingDays
            // 
            this.RemainingDays.DataPropertyName = "RemainingDays";
            this.RemainingDays.HeaderText = "剩余/天";
            this.RemainingDays.Name = "RemainingDays";
            this.RemainingDays.ReadOnly = true;
            this.RemainingDays.ToolTipText = "今日-添加日";
            this.RemainingDays.Width = 79;
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
            this.ModuleNo.HeaderText = "分段数";
            this.ModuleNo.Name = "ModuleNo";
            this.ModuleNo.ReadOnly = true;
            this.ModuleNo.ToolTipText = "一组烟罩分段数量";
            this.ModuleNo.Width = 73;
            // 
            // SubTotalWorkload
            // 
            this.SubTotalWorkload.DataPropertyName = "SubTotalWorkload";
            this.SubTotalWorkload.HeaderText = "工作量";
            this.SubTotalWorkload.Name = "SubTotalWorkload";
            this.SubTotalWorkload.ReadOnly = true;
            this.SubTotalWorkload.ToolTipText = "分段数量x机型单位工作量";
            this.SubTotalWorkload.Width = 73;
            // 
            // DrawingPlanId
            // 
            this.DrawingPlanId.DataPropertyName = "DrawingPlanId";
            this.DrawingPlanId.HeaderText = "ID";
            this.DrawingPlanId.Name = "DrawingPlanId";
            this.DrawingPlanId.ReadOnly = true;
            this.DrawingPlanId.Width = 48;
            // 
            // HoodType
            // 
            this.HoodType.DataPropertyName = "HoodType";
            this.HoodType.HeaderText = "烟罩类型";
            this.HoodType.Name = "HoodType";
            this.HoodType.ReadOnly = true;
            this.HoodType.Width = 86;
            // 
            // FrmDrawingPlan
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 568);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbEditDrawingPlan);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnQueryByUserId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cobUserId);
            this.Controls.Add(this.cobODPNo);
            this.Controls.Add(this.dtpDrReleaseTarget);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnQueryByProjectId);
            this.Controls.Add(this.btnDrawingPlanQuery);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.ComboBox cobODPNo;
        private System.Windows.Forms.ComboBox cobEditODPNo;
        private System.Windows.Forms.ToolStripMenuItem tsmiRequirements;
        private System.Windows.Forms.ToolStripMenuItem tsmiProjectTracking;
        private System.Windows.Forms.Button btnQueryByUserId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cobUserId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private MyUIControls.SuperTextBox txtToPage;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblRecordsCound;
        private System.Windows.Forms.Label lblCurrentPage;
        private System.Windows.Forms.Label lblPageCount;
        private System.Windows.Forms.ComboBox cobRecordList;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cobQueryYear;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnToPage;
        private System.Windows.Forms.Button btnQueryByYear;
        private System.Windows.Forms.Button btnDrawingPlanQuery;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODPNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrReleaseTarget;
        private Common.DataGridViewProgressColumn ProgressValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemainingDays;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrReleaseActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotalWorkload;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawingPlanId;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoodType;
    }
}