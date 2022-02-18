namespace Compass
{
    partial class FrmProjectTracking
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
            this.dgvProjectTracking = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditProjectTracking = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowProjectInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnQueryByProjectId = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cobProjectStatus = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEditDrReleaseActual = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpEditProdFinishActual = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpEditDeliverActual = new System.Windows.Forms.DateTimePicker();
            this.grbEditProjectTracking = new System.Windows.Forms.GroupBox();
            this.cobEditODPNo = new System.Windows.Forms.ComboBox();
            this.dtpEditKickOffDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEditODPReceiveDate = new System.Windows.Forms.DateTimePicker();
            this.btnEditProjectTracking = new System.Windows.Forms.Button();
            this.cobEditProjectStatus = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cobODPNo = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.lblRecordsCound = new System.Windows.Forms.Label();
            this.lblCurrentPage = new System.Windows.Forms.Label();
            this.lblPageCount = new System.Windows.Forms.Label();
            this.cobRecordList = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.cobQueryYear = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnToPage = new System.Windows.Forms.Button();
            this.btnQueryByYear = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label15 = new System.Windows.Forms.Label();
            this.txtToPage = new MyUIControls.SuperTextBox(this.components);
            this.ODPNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectStatusName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectCycle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODPReceiveDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KickOffDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrReleaseTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippingTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProdFinishActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeliverActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnomalyStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectTrackingId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjectTracking)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.grbEditProjectTracking.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvProjectTracking
            // 
            this.dgvProjectTracking.AllowUserToAddRows = false;
            this.dgvProjectTracking.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvProjectTracking.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProjectTracking.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProjectTracking.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvProjectTracking.BackgroundColor = System.Drawing.Color.White;
            this.dgvProjectTracking.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProjectTracking.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProjectTracking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjectTracking.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ODPNo,
            this.ProjectStatusName,
            this.ProjectName,
            this.ProjectCycle,
            this.ODPReceiveDate,
            this.KickOffDate,
            this.DrReleaseTarget,
            this.Item,
            this.ShippingTime,
            this.ProdFinishActual,
            this.DeliverActual,
            this.AnomalyStatus,
            this.ProjectTrackingId,
            this.UserAccount});
            this.dgvProjectTracking.ContextMenuStrip = this.contextMenuStrip;
            this.dgvProjectTracking.EnableHeadersVisualStyles = false;
            this.dgvProjectTracking.Location = new System.Drawing.Point(12, 110);
            this.dgvProjectTracking.Name = "dgvProjectTracking";
            this.dgvProjectTracking.ReadOnly = true;
            this.dgvProjectTracking.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProjectTracking.Size = new System.Drawing.Size(926, 392);
            this.dgvProjectTracking.TabIndex = 11;
            this.dgvProjectTracking.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvProjectTracking_RowPostPaint);
            this.dgvProjectTracking.SelectionChanged += new System.EventHandler(this.DgvProjectTracking_SelectionChanged);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditProjectTracking,
            this.tsmiShowProjectInfo});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(212, 48);
            // 
            // tsmiEditProjectTracking
            // 
            this.tsmiEditProjectTracking.Name = "tsmiEditProjectTracking";
            this.tsmiEditProjectTracking.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.tsmiEditProjectTracking.Size = new System.Drawing.Size(211, 22);
            this.tsmiEditProjectTracking.Text = "修改跟踪记录(&U)";
            this.tsmiEditProjectTracking.Click += new System.EventHandler(this.TsmiEditProjectTracking_Click);
            // 
            // tsmiShowProjectInfo
            // 
            this.tsmiShowProjectInfo.Name = "tsmiShowProjectInfo";
            this.tsmiShowProjectInfo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.tsmiShowProjectInfo.Size = new System.Drawing.Size(211, 22);
            this.tsmiShowProjectInfo.Text = "显示详细信息(&I)";
            this.tsmiShowProjectInfo.Click += new System.EventHandler(this.TsmiShowProjectInfo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 26);
            this.label1.TabIndex = 10;
            this.label1.Text = "项目跟踪表";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 18;
            this.label3.Text = "项目编号";
            // 
            // btnQueryByProjectId
            // 
            this.btnQueryByProjectId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnQueryByProjectId.FlatAppearance.BorderSize = 0;
            this.btnQueryByProjectId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQueryByProjectId.ForeColor = System.Drawing.Color.White;
            this.btnQueryByProjectId.Location = new System.Drawing.Point(225, 71);
            this.btnQueryByProjectId.Name = "btnQueryByProjectId";
            this.btnQueryByProjectId.Size = new System.Drawing.Size(46, 28);
            this.btnQueryByProjectId.TabIndex = 16;
            this.btnQueryByProjectId.Text = "查询";
            this.btnQueryByProjectId.UseVisualStyleBackColor = false;
            this.btnQueryByProjectId.Click += new System.EventHandler(this.BtnQueryByProjectId_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 19;
            this.label4.Text = "项目状态";
            // 
            // cobProjectStatus
            // 
            this.cobProjectStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobProjectStatus.FormattingEnabled = true;
            this.cobProjectStatus.Location = new System.Drawing.Point(80, 40);
            this.cobProjectStatus.Name = "cobProjectStatus";
            this.cobProjectStatus.Size = new System.Drawing.Size(139, 27);
            this.cobProjectStatus.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 21;
            this.label2.Text = "实际发图";
            // 
            // dtpEditDrReleaseActual
            // 
            this.dtpEditDrReleaseActual.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEditDrReleaseActual.Location = new System.Drawing.Point(282, 62);
            this.dtpEditDrReleaseActual.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtpEditDrReleaseActual.Name = "dtpEditDrReleaseActual";
            this.dtpEditDrReleaseActual.Size = new System.Drawing.Size(117, 25);
            this.dtpEditDrReleaseActual.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(400, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 19);
            this.label6.TabIndex = 21;
            this.label6.Text = "实际完工";
            // 
            // dtpEditProdFinishActual
            // 
            this.dtpEditProdFinishActual.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEditProdFinishActual.Location = new System.Drawing.Point(467, 62);
            this.dtpEditProdFinishActual.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtpEditProdFinishActual.Name = "dtpEditProdFinishActual";
            this.dtpEditProdFinishActual.Size = new System.Drawing.Size(117, 25);
            this.dtpEditProdFinishActual.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(602, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 41);
            this.label9.TabIndex = 21;
            this.label9.Text = "实际发货     客户提货时间";
            // 
            // dtpEditDeliverActual
            // 
            this.dtpEditDeliverActual.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEditDeliverActual.Location = new System.Drawing.Point(685, 62);
            this.dtpEditDeliverActual.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtpEditDeliverActual.Name = "dtpEditDeliverActual";
            this.dtpEditDeliverActual.Size = new System.Drawing.Size(117, 25);
            this.dtpEditDeliverActual.TabIndex = 20;
            // 
            // grbEditProjectTracking
            // 
            this.grbEditProjectTracking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbEditProjectTracking.Controls.Add(this.dtpEditProdFinishActual);
            this.grbEditProjectTracking.Controls.Add(this.cobEditODPNo);
            this.grbEditProjectTracking.Controls.Add(this.dtpEditKickOffDate);
            this.grbEditProjectTracking.Controls.Add(this.dtpEditODPReceiveDate);
            this.grbEditProjectTracking.Controls.Add(this.label2);
            this.grbEditProjectTracking.Controls.Add(this.btnEditProjectTracking);
            this.grbEditProjectTracking.Controls.Add(this.label6);
            this.grbEditProjectTracking.Controls.Add(this.dtpEditDeliverActual);
            this.grbEditProjectTracking.Controls.Add(this.label9);
            this.grbEditProjectTracking.Controls.Add(this.dtpEditDrReleaseActual);
            this.grbEditProjectTracking.Controls.Add(this.cobEditProjectStatus);
            this.grbEditProjectTracking.Controls.Add(this.label11);
            this.grbEditProjectTracking.Controls.Add(this.label10);
            this.grbEditProjectTracking.Controls.Add(this.label7);
            this.grbEditProjectTracking.Controls.Add(this.label19);
            this.grbEditProjectTracking.Controls.Add(this.label8);
            this.grbEditProjectTracking.Controls.Add(this.label5);
            this.grbEditProjectTracking.Location = new System.Drawing.Point(12, 137);
            this.grbEditProjectTracking.Name = "grbEditProjectTracking";
            this.grbEditProjectTracking.Size = new System.Drawing.Size(926, 96);
            this.grbEditProjectTracking.TabIndex = 24;
            this.grbEditProjectTracking.TabStop = false;
            this.grbEditProjectTracking.Text = "修改项目跟踪记录";
            // 
            // cobEditODPNo
            // 
            this.cobEditODPNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobEditODPNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobEditODPNo.FormattingEnabled = true;
            this.cobEditODPNo.Location = new System.Drawing.Point(69, 61);
            this.cobEditODPNo.Name = "cobEditODPNo";
            this.cobEditODPNo.Size = new System.Drawing.Size(139, 27);
            this.cobEditODPNo.TabIndex = 40;
            // 
            // dtpEditKickOffDate
            // 
            this.dtpEditKickOffDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEditKickOffDate.Location = new System.Drawing.Point(467, 31);
            this.dtpEditKickOffDate.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtpEditKickOffDate.Name = "dtpEditKickOffDate";
            this.dtpEditKickOffDate.Size = new System.Drawing.Size(117, 25);
            this.dtpEditKickOffDate.TabIndex = 20;
            // 
            // dtpEditODPReceiveDate
            // 
            this.dtpEditODPReceiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEditODPReceiveDate.Location = new System.Drawing.Point(282, 31);
            this.dtpEditODPReceiveDate.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtpEditODPReceiveDate.Name = "dtpEditODPReceiveDate";
            this.dtpEditODPReceiveDate.Size = new System.Drawing.Size(117, 25);
            this.dtpEditODPReceiveDate.TabIndex = 20;
            // 
            // btnEditProjectTracking
            // 
            this.btnEditProjectTracking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnEditProjectTracking.FlatAppearance.BorderSize = 0;
            this.btnEditProjectTracking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditProjectTracking.ForeColor = System.Drawing.Color.White;
            this.btnEditProjectTracking.Location = new System.Drawing.Point(814, 60);
            this.btnEditProjectTracking.Name = "btnEditProjectTracking";
            this.btnEditProjectTracking.Size = new System.Drawing.Size(108, 28);
            this.btnEditProjectTracking.TabIndex = 22;
            this.btnEditProjectTracking.Text = "修改跟踪记录";
            this.btnEditProjectTracking.UseVisualStyleBackColor = false;
            this.btnEditProjectTracking.Click += new System.EventHandler(this.BtnEditProjectTracking_Click);
            // 
            // cobEditProjectStatus
            // 
            this.cobEditProjectStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobEditProjectStatus.Enabled = false;
            this.cobEditProjectStatus.FormattingEnabled = true;
            this.cobEditProjectStatus.Location = new System.Drawing.Point(70, 30);
            this.cobEditProjectStatus.Name = "cobEditProjectStatus";
            this.cobEditProjectStatus.Size = new System.Drawing.Size(139, 27);
            this.cobEditProjectStatus.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(2, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 19);
            this.label11.TabIndex = 18;
            this.label11.Text = "项目编号";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 19);
            this.label10.TabIndex = 19;
            this.label10.Text = "项目状态";
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(215, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 27);
            this.label7.TabIndex = 21;
            this.label7.Text = "收到ODP";
            // 
            // label19
            // 
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(404, 32);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 23);
            this.label19.TabIndex = 21;
            this.label19.Text = "KickOff";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(286, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 19);
            this.label8.TabIndex = 21;
            this.label8.Text = "转MO的时间";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(592, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(329, 19);
            this.label5.TabIndex = 21;
            this.label5.Text = "提示：将日期修改为【2020/1/1】退回上一个项目状态";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.Red;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(861, 39);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(77, 28);
            this.btnAdd.TabIndex = 22;
            this.btnAdd.Text = "报告异常";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // cobODPNo
            // 
            this.cobODPNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobODPNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobODPNo.FormattingEnabled = true;
            this.cobODPNo.Location = new System.Drawing.Point(80, 72);
            this.cobODPNo.Name = "cobODPNo";
            this.cobODPNo.Size = new System.Drawing.Size(139, 27);
            this.cobODPNo.TabIndex = 39;
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
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.cobQueryYear);
            this.groupBox1.Controls.Add(this.label13);
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
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "分页显示";
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
            "100",
            "200",
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
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(308, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(268, 19);
            this.label12.TabIndex = 41;
            this.label12.Text = "【共：    页 当前第：    页 记录总数：      】";
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
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 19);
            this.label13.TabIndex = 35;
            this.label13.Text = "年度：";
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
            this.btnLast.Click += new System.EventHandler(this.BtnLast_Click);
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
            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
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
            this.btnPre.Click += new System.EventHandler(this.BtnPre_Click);
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
            this.btnFirst.Click += new System.EventHandler(this.BtnFirst_Click);
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
            this.btnToPage.Click += new System.EventHandler(this.BtnToPage_Click);
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
            this.btnQueryByYear.Click += new System.EventHandler(this.BtnQueryByYear_Click);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Blue;
            this.label15.Location = new System.Drawing.Point(649, 80);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(285, 19);
            this.label15.TabIndex = 21;
            this.label15.Text = "提示：项目周期是收到ODP到计划完工的天数。";
            // 
            // txtToPage
            // 
            this.txtToPage.Location = new System.Drawing.Point(629, 25);
            this.txtToPage.Name = "txtToPage";
            this.txtToPage.Size = new System.Drawing.Size(34, 25);
            this.txtToPage.TabIndex = 45;
            // 
            // ODPNo
            // 
            this.ODPNo.DataPropertyName = "ODPNo";
            this.ODPNo.HeaderText = "ODP";
            this.ODPNo.Name = "ODPNo";
            this.ODPNo.ReadOnly = true;
            this.ODPNo.Width = 63;
            // 
            // ProjectStatusName
            // 
            this.ProjectStatusName.DataPropertyName = "ProjectStatusName";
            this.ProjectStatusName.HeaderText = "项目状态";
            this.ProjectStatusName.Name = "ProjectStatusName";
            this.ProjectStatusName.ReadOnly = true;
            this.ProjectStatusName.Width = 86;
            // 
            // ProjectName
            // 
            this.ProjectName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ProjectName.DataPropertyName = "ProjectName";
            this.ProjectName.HeaderText = "项目名称";
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.ReadOnly = true;
            this.ProjectName.Width = 86;
            // 
            // ProjectCycle
            // 
            this.ProjectCycle.DataPropertyName = "ProjectCycle";
            dataGridViewCellStyle3.NullValue = null;
            this.ProjectCycle.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProjectCycle.HeaderText = "项目周期";
            this.ProjectCycle.Name = "ProjectCycle";
            this.ProjectCycle.ReadOnly = true;
            this.ProjectCycle.Width = 86;
            // 
            // ODPReceiveDate
            // 
            this.ODPReceiveDate.DataPropertyName = "ODPReceiveDate";
            this.ODPReceiveDate.HeaderText = "收到ODP";
            this.ODPReceiveDate.Name = "ODPReceiveDate";
            this.ODPReceiveDate.ReadOnly = true;
            this.ODPReceiveDate.Width = 89;
            // 
            // KickOffDate
            // 
            this.KickOffDate.DataPropertyName = "KickOffDate";
            this.KickOffDate.HeaderText = "KickOff";
            this.KickOffDate.Name = "KickOffDate";
            this.KickOffDate.ReadOnly = true;
            this.KickOffDate.Width = 80;
            // 
            // DrReleaseTarget
            // 
            this.DrReleaseTarget.DataPropertyName = "DrReleaseTarget";
            dataGridViewCellStyle4.NullValue = null;
            this.DrReleaseTarget.DefaultCellStyle = dataGridViewCellStyle4;
            this.DrReleaseTarget.HeaderText = "计划发图";
            this.DrReleaseTarget.Name = "DrReleaseTarget";
            this.DrReleaseTarget.ReadOnly = true;
            this.DrReleaseTarget.Width = 86;
            // 
            // Item
            // 
            this.Item.DataPropertyName = "DrReleaseActual";
            this.Item.HeaderText = "实际发图";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.Width = 86;
            // 
            // ShippingTime
            // 
            this.ShippingTime.DataPropertyName = "ShippingTime";
            this.ShippingTime.HeaderText = "计划完工";
            this.ShippingTime.Name = "ShippingTime";
            this.ShippingTime.ReadOnly = true;
            this.ShippingTime.Width = 86;
            // 
            // ProdFinishActual
            // 
            this.ProdFinishActual.DataPropertyName = "ProdFinishActual";
            this.ProdFinishActual.HeaderText = "实际完工";
            this.ProdFinishActual.Name = "ProdFinishActual";
            this.ProdFinishActual.ReadOnly = true;
            this.ProdFinishActual.Width = 86;
            // 
            // DeliverActual
            // 
            this.DeliverActual.DataPropertyName = "DeliverActual";
            this.DeliverActual.HeaderText = "实际发货";
            this.DeliverActual.Name = "DeliverActual";
            this.DeliverActual.ReadOnly = true;
            this.DeliverActual.Width = 86;
            // 
            // AnomalyStatus
            // 
            this.AnomalyStatus.HeaderText = "异常状态";
            this.AnomalyStatus.Name = "AnomalyStatus";
            this.AnomalyStatus.ReadOnly = true;
            this.AnomalyStatus.Width = 86;
            // 
            // ProjectTrackingId
            // 
            this.ProjectTrackingId.DataPropertyName = "ProjectTrackingId";
            this.ProjectTrackingId.HeaderText = "ID";
            this.ProjectTrackingId.Name = "ProjectTrackingId";
            this.ProjectTrackingId.ReadOnly = true;
            this.ProjectTrackingId.Width = 48;
            // 
            // UserAccount
            // 
            this.UserAccount.DataPropertyName = "UserAccount";
            this.UserAccount.HeaderText = "制图";
            this.UserAccount.Name = "UserAccount";
            this.UserAccount.ReadOnly = true;
            this.UserAccount.Width = 60;
            // 
            // FrmProjectTracking
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 568);
            this.Controls.Add(this.grbEditProjectTracking);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cobODPNo);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnQueryByProjectId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cobProjectStatus);
            this.Controls.Add(this.dgvProjectTracking);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label15);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmProjectTracking";
            this.Text = "FrmProjectTracking";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjectTracking)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.grbEditProjectTracking.ResumeLayout(false);
            this.grbEditProjectTracking.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProjectTracking;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnQueryByProjectId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cobProjectStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEditDrReleaseActual;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpEditProdFinishActual;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpEditDeliverActual;
        private System.Windows.Forms.GroupBox grbEditProjectTracking;
        private System.Windows.Forms.Button btnEditProjectTracking;
        private System.Windows.Forms.ComboBox cobEditProjectStatus;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditProjectTracking;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cobODPNo;
        private System.Windows.Forms.ComboBox cobEditODPNo;
        private System.Windows.Forms.GroupBox groupBox1;
        private MyUIControls.SuperTextBox txtToPage;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblRecordsCound;
        private System.Windows.Forms.Label lblCurrentPage;
        private System.Windows.Forms.Label lblPageCount;
        private System.Windows.Forms.ComboBox cobRecordList;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cobQueryYear;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnToPage;
        private System.Windows.Forms.Button btnQueryByYear;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.DateTimePicker dtpEditKickOffDate;
        private System.Windows.Forms.DateTimePicker dtpEditODPReceiveDate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowProjectInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODPNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectStatusName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectCycle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODPReceiveDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn KickOffDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrReleaseTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShippingTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProdFinishActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeliverActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnomalyStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectTrackingId;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserAccount;
    }
}