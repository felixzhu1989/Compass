namespace Compass
{
    partial class FrmProjectInfo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grbProject = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtShippingTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHoodType = new System.Windows.Forms.TextBox();
            this.txtUserAccount = new System.Windows.Forms.TextBox();
            this.txtBPONo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grbGeneralRequirements = new System.Windows.Forms.GroupBox();
            this.txtGeneralRequirementId = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTypeName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRiskLevel = new System.Windows.Forms.TextBox();
            this.txtInputPower = new System.Windows.Forms.TextBox();
            this.txtMARVEL = new System.Windows.Forms.TextBox();
            this.txtANSULPrePipe = new System.Windows.Forms.TextBox();
            this.txtANSULSystem = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.grbSpecialRequirements = new System.Windows.Forms.GroupBox();
            this.lbxSpecialRequirements = new System.Windows.Forms.ListBox();
            this.cobODPNo = new System.Windows.Forms.ComboBox();
            this.cmsRequirement = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRequirement = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditProject = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteGeneralRequirement = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvScope = new System.Windows.Forms.DataGridView();
            this.txtSalesValue = new System.Windows.Forms.TextBox();
            this.lblSalesValue = new System.Windows.Forms.Label();
            this.grbFinancialData = new System.Windows.Forms.GroupBox();
            this.btnFinancialData = new System.Windows.Forms.Button();
            this.grbProject.SuspendLayout();
            this.grbGeneralRequirements.SuspendLayout();
            this.grbSpecialRequirements.SuspendLayout();
            this.cmsRequirement.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScope)).BeginInit();
            this.grbFinancialData.SuspendLayout();
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
            this.label1.TabIndex = 25;
            this.label1.Text = "项目信息";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 31;
            this.label3.Text = "项目编号";
            // 
            // grbProject
            // 
            this.grbProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbProject.Controls.Add(this.label7);
            this.grbProject.Controls.Add(this.txtCustomerName);
            this.grbProject.Controls.Add(this.label8);
            this.grbProject.Controls.Add(this.txtProjectName);
            this.grbProject.Controls.Add(this.label6);
            this.grbProject.Controls.Add(this.label5);
            this.grbProject.Controls.Add(this.txtShippingTime);
            this.grbProject.Controls.Add(this.label4);
            this.grbProject.Controls.Add(this.txtHoodType);
            this.grbProject.Controls.Add(this.txtUserAccount);
            this.grbProject.Location = new System.Drawing.Point(10, 35);
            this.grbProject.Name = "grbProject";
            this.grbProject.Size = new System.Drawing.Size(569, 111);
            this.grbProject.TabIndex = 36;
            this.grbProject.TabStop = false;
            this.grbProject.Text = "项目基本信息";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 19);
            this.label7.TabIndex = 33;
            this.label7.Text = "客户名称";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(67, 78);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(465, 25);
            this.txtCustomerName.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 19);
            this.label8.TabIndex = 33;
            this.label8.Text = "项目名称";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(67, 49);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.ReadOnly = true;
            this.txtProjectName.Size = new System.Drawing.Size(465, 25);
            this.txtProjectName.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(361, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 19);
            this.label6.TabIndex = 31;
            this.label6.Text = "发货日期";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 31;
            this.label5.Text = "烟罩类型";
            // 
            // txtShippingTime
            // 
            this.txtShippingTime.Location = new System.Drawing.Point(424, 20);
            this.txtShippingTime.Name = "txtShippingTime";
            this.txtShippingTime.ReadOnly = true;
            this.txtShippingTime.Size = new System.Drawing.Size(108, 25);
            this.txtShippingTime.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 31;
            this.label4.Text = "制图人员";
            // 
            // txtHoodType
            // 
            this.txtHoodType.Location = new System.Drawing.Point(247, 20);
            this.txtHoodType.Name = "txtHoodType";
            this.txtHoodType.ReadOnly = true;
            this.txtHoodType.Size = new System.Drawing.Size(108, 25);
            this.txtHoodType.TabIndex = 30;
            // 
            // txtUserAccount
            // 
            this.txtUserAccount.Location = new System.Drawing.Point(69, 20);
            this.txtUserAccount.Name = "txtUserAccount";
            this.txtUserAccount.ReadOnly = true;
            this.txtUserAccount.Size = new System.Drawing.Size(108, 25);
            this.txtUserAccount.TabIndex = 30;
            // 
            // txtBPONo
            // 
            this.txtBPONo.Location = new System.Drawing.Point(434, 10);
            this.txtBPONo.Name = "txtBPONo";
            this.txtBPONo.ReadOnly = true;
            this.txtBPONo.Size = new System.Drawing.Size(108, 25);
            this.txtBPONo.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(371, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 31;
            this.label2.Text = "大工单号";
            // 
            // grbGeneralRequirements
            // 
            this.grbGeneralRequirements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbGeneralRequirements.Controls.Add(this.txtGeneralRequirementId);
            this.grbGeneralRequirements.Controls.Add(this.label14);
            this.grbGeneralRequirements.Controls.Add(this.label13);
            this.grbGeneralRequirements.Controls.Add(this.txtTypeName);
            this.grbGeneralRequirements.Controls.Add(this.label9);
            this.grbGeneralRequirements.Controls.Add(this.txtRiskLevel);
            this.grbGeneralRequirements.Controls.Add(this.txtInputPower);
            this.grbGeneralRequirements.Controls.Add(this.txtMARVEL);
            this.grbGeneralRequirements.Controls.Add(this.txtANSULPrePipe);
            this.grbGeneralRequirements.Controls.Add(this.txtANSULSystem);
            this.grbGeneralRequirements.Controls.Add(this.label15);
            this.grbGeneralRequirements.Controls.Add(this.label12);
            this.grbGeneralRequirements.Controls.Add(this.label11);
            this.grbGeneralRequirements.Controls.Add(this.label10);
            this.grbGeneralRequirements.Location = new System.Drawing.Point(10, 147);
            this.grbGeneralRequirements.Name = "grbGeneralRequirements";
            this.grbGeneralRequirements.Size = new System.Drawing.Size(928, 88);
            this.grbGeneralRequirements.TabIndex = 37;
            this.grbGeneralRequirements.TabStop = false;
            this.grbGeneralRequirements.Text = "通用技术要求";
            // 
            // txtGeneralRequirementId
            // 
            this.txtGeneralRequirementId.Location = new System.Drawing.Point(622, 21);
            this.txtGeneralRequirementId.Name = "txtGeneralRequirementId";
            this.txtGeneralRequirementId.ReadOnly = true;
            this.txtGeneralRequirementId.Size = new System.Drawing.Size(45, 25);
            this.txtGeneralRequirementId.TabIndex = 42;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(596, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 19);
            this.label14.TabIndex = 43;
            this.label14.Text = "ID";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 19);
            this.label13.TabIndex = 31;
            this.label13.Text = "项目类型";
            // 
            // txtTypeName
            // 
            this.txtTypeName.Location = new System.Drawing.Point(69, 22);
            this.txtTypeName.Name = "txtTypeName";
            this.txtTypeName.ReadOnly = true;
            this.txtTypeName.Size = new System.Drawing.Size(108, 25);
            this.txtTypeName.TabIndex = 30;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 19);
            this.label9.TabIndex = 31;
            this.label9.Text = "MARVEL";
            // 
            // txtRiskLevel
            // 
            this.txtRiskLevel.Location = new System.Drawing.Point(267, 21);
            this.txtRiskLevel.Name = "txtRiskLevel";
            this.txtRiskLevel.ReadOnly = true;
            this.txtRiskLevel.Size = new System.Drawing.Size(108, 25);
            this.txtRiskLevel.TabIndex = 30;
            // 
            // txtInputPower
            // 
            this.txtInputPower.Location = new System.Drawing.Point(464, 22);
            this.txtInputPower.Name = "txtInputPower";
            this.txtInputPower.ReadOnly = true;
            this.txtInputPower.Size = new System.Drawing.Size(108, 25);
            this.txtInputPower.TabIndex = 30;
            // 
            // txtMARVEL
            // 
            this.txtMARVEL.Location = new System.Drawing.Point(69, 51);
            this.txtMARVEL.Name = "txtMARVEL";
            this.txtMARVEL.ReadOnly = true;
            this.txtMARVEL.Size = new System.Drawing.Size(108, 25);
            this.txtMARVEL.TabIndex = 30;
            // 
            // txtANSULPrePipe
            // 
            this.txtANSULPrePipe.Location = new System.Drawing.Point(267, 51);
            this.txtANSULPrePipe.Name = "txtANSULPrePipe";
            this.txtANSULPrePipe.ReadOnly = true;
            this.txtANSULPrePipe.Size = new System.Drawing.Size(108, 25);
            this.txtANSULPrePipe.TabIndex = 30;
            // 
            // txtANSULSystem
            // 
            this.txtANSULSystem.Location = new System.Drawing.Point(464, 51);
            this.txtANSULSystem.Name = "txtANSULSystem";
            this.txtANSULSystem.ReadOnly = true;
            this.txtANSULSystem.Size = new System.Drawing.Size(108, 25);
            this.txtANSULSystem.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(184, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 19);
            this.label15.TabIndex = 31;
            this.label15.Text = "项目等级";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(381, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 19);
            this.label12.TabIndex = 31;
            this.label12.Text = "项目电制";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(381, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 19);
            this.label11.TabIndex = 31;
            this.label11.Text = "ANSUL系统";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(184, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 19);
            this.label10.TabIndex = 31;
            this.label10.Text = "ANSUL预埋";
            // 
            // grbSpecialRequirements
            // 
            this.grbSpecialRequirements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbSpecialRequirements.Controls.Add(this.lbxSpecialRequirements);
            this.grbSpecialRequirements.Location = new System.Drawing.Point(10, 235);
            this.grbSpecialRequirements.Name = "grbSpecialRequirements";
            this.grbSpecialRequirements.Size = new System.Drawing.Size(572, 321);
            this.grbSpecialRequirements.TabIndex = 37;
            this.grbSpecialRequirements.TabStop = false;
            this.grbSpecialRequirements.Text = "特殊技术要求";
            // 
            // lbxSpecialRequirements
            // 
            this.lbxSpecialRequirements.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbxSpecialRequirements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxSpecialRequirements.FormattingEnabled = true;
            this.lbxSpecialRequirements.HorizontalScrollbar = true;
            this.lbxSpecialRequirements.ItemHeight = 19;
            this.lbxSpecialRequirements.Location = new System.Drawing.Point(3, 21);
            this.lbxSpecialRequirements.Name = "lbxSpecialRequirements";
            this.lbxSpecialRequirements.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxSpecialRequirements.Size = new System.Drawing.Size(566, 297);
            this.lbxSpecialRequirements.TabIndex = 0;
            // 
            // cobODPNo
            // 
            this.cobODPNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobODPNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobODPNo.FormattingEnabled = true;
            this.cobODPNo.Location = new System.Drawing.Point(257, 9);
            this.cobODPNo.Name = "cobODPNo";
            this.cobODPNo.Size = new System.Drawing.Size(108, 27);
            this.cobODPNo.TabIndex = 38;
            this.cobODPNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cobODPNo_KeyDown);
            // 
            // cmsRequirement
            // 
            this.cmsRequirement.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRequirement,
            this.tsmiEditProject,
            this.tsmiDeleteGeneralRequirement});
            this.cmsRequirement.Name = "cmsRequirement";
            this.cmsRequirement.Size = new System.Drawing.Size(173, 70);
            // 
            // tsmiRequirement
            // 
            this.tsmiRequirement.Name = "tsmiRequirement";
            this.tsmiRequirement.Size = new System.Drawing.Size(172, 22);
            this.tsmiRequirement.Text = "编辑技术要求";
            this.tsmiRequirement.Click += new System.EventHandler(this.tsmiRequirement_Click);
            // 
            // tsmiEditProject
            // 
            this.tsmiEditProject.Name = "tsmiEditProject";
            this.tsmiEditProject.Size = new System.Drawing.Size(172, 22);
            this.tsmiEditProject.Text = "修改项目信息";
            this.tsmiEditProject.Click += new System.EventHandler(this.tsmiEditProject_Click);
            // 
            // tsmiDeleteGeneralRequirement
            // 
            this.tsmiDeleteGeneralRequirement.Name = "tsmiDeleteGeneralRequirement";
            this.tsmiDeleteGeneralRequirement.Size = new System.Drawing.Size(172, 22);
            this.tsmiDeleteGeneralRequirement.Text = "删除通用技术要求";
            this.tsmiDeleteGeneralRequirement.Click += new System.EventHandler(this.tsmiDeleteGeneralRequirement_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvScope);
            this.groupBox1.Location = new System.Drawing.Point(588, 235);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 321);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "范围";
            // 
            // dgvScope
            // 
            this.dgvScope.AllowUserToAddRows = false;
            this.dgvScope.AllowUserToDeleteRows = false;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.Azure;
            this.dgvScope.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvScope.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvScope.BackgroundColor = System.Drawing.Color.White;
            this.dgvScope.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle16.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvScope.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvScope.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvScope.EnableHeadersVisualStyles = false;
            this.dgvScope.Location = new System.Drawing.Point(3, 21);
            this.dgvScope.Name = "dgvScope";
            this.dgvScope.ReadOnly = true;
            this.dgvScope.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvScope.Size = new System.Drawing.Size(344, 297);
            this.dgvScope.TabIndex = 65;
            this.dgvScope.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvScope_RowPostPaint);
            // 
            // txtSalesValue
            // 
            this.txtSalesValue.Location = new System.Drawing.Point(54, 20);
            this.txtSalesValue.MaxLength = 10;
            this.txtSalesValue.Name = "txtSalesValue";
            this.txtSalesValue.Size = new System.Drawing.Size(113, 25);
            this.txtSalesValue.TabIndex = 39;
            this.txtSalesValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSalesValue.TextChanged += new System.EventHandler(this.txtSalesValue_TextChanged);
            // 
            // lblSalesValue
            // 
            this.lblSalesValue.AutoSize = true;
            this.lblSalesValue.Location = new System.Drawing.Point(6, 23);
            this.lblSalesValue.Name = "lblSalesValue";
            this.lblSalesValue.Size = new System.Drawing.Size(214, 19);
            this.lblSalesValue.TabIndex = 40;
            this.lblSalesValue.Text = "销售额                               RMB元";
            // 
            // grbFinancialData
            // 
            this.grbFinancialData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbFinancialData.Controls.Add(this.txtSalesValue);
            this.grbFinancialData.Controls.Add(this.btnFinancialData);
            this.grbFinancialData.Controls.Add(this.lblSalesValue);
            this.grbFinancialData.Location = new System.Drawing.Point(591, 35);
            this.grbFinancialData.Name = "grbFinancialData";
            this.grbFinancialData.Size = new System.Drawing.Size(347, 111);
            this.grbFinancialData.TabIndex = 41;
            this.grbFinancialData.TabStop = false;
            this.grbFinancialData.Text = "财务数据";
            // 
            // btnFinancialData
            // 
            this.btnFinancialData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinancialData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnFinancialData.FlatAppearance.BorderSize = 0;
            this.btnFinancialData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinancialData.ForeColor = System.Drawing.Color.White;
            this.btnFinancialData.Location = new System.Drawing.Point(233, 72);
            this.btnFinancialData.Name = "btnFinancialData";
            this.btnFinancialData.Size = new System.Drawing.Size(108, 28);
            this.btnFinancialData.TabIndex = 35;
            this.btnFinancialData.Text = "添加财务数据";
            this.btnFinancialData.UseVisualStyleBackColor = false;
            this.btnFinancialData.Click += new System.EventHandler(this.btnFinancialData_Click);
            // 
            // FrmProjectInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 568);
            this.ContextMenuStrip = this.cmsRequirement;
            this.Controls.Add(this.cobODPNo);
            this.Controls.Add(this.grbSpecialRequirements);
            this.Controls.Add(this.grbGeneralRequirements);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbProject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBPONo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grbFinancialData);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmProjectInfo";
            this.Text = "FrmProjectInfo";
            this.grbProject.ResumeLayout(false);
            this.grbProject.PerformLayout();
            this.grbGeneralRequirements.ResumeLayout(false);
            this.grbGeneralRequirements.PerformLayout();
            this.grbSpecialRequirements.ResumeLayout(false);
            this.cmsRequirement.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScope)).EndInit();
            this.grbFinancialData.ResumeLayout(false);
            this.grbFinancialData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grbProject;
        private System.Windows.Forms.TextBox txtBPONo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserAccount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtHoodType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtShippingTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.GroupBox grbGeneralRequirements;
        private System.Windows.Forms.GroupBox grbSpecialRequirements;
        private System.Windows.Forms.ComboBox cobODPNo;
        private System.Windows.Forms.ContextMenuStrip cmsRequirement;
        private System.Windows.Forms.ToolStripMenuItem tsmiRequirement;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditProject;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtTypeName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtInputPower;
        private System.Windows.Forms.TextBox txtMARVEL;
        private System.Windows.Forms.TextBox txtANSULPrePipe;
        private System.Windows.Forms.TextBox txtANSULSystem;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteGeneralRequirement;
        private System.Windows.Forms.TextBox txtGeneralRequirementId;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ListBox lbxSpecialRequirements;
        private System.Windows.Forms.TextBox txtRiskLevel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvScope;
        private System.Windows.Forms.TextBox txtSalesValue;
        private System.Windows.Forms.Label lblSalesValue;
        private System.Windows.Forms.GroupBox grbFinancialData;
        private System.Windows.Forms.Button btnFinancialData;
    }
}