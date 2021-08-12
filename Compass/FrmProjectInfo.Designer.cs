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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea17 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend17 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series17 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea18 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend18 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series18 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea19 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend19 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series19 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea20 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend20 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series20 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label3 = new System.Windows.Forms.Label();
            this.grbProject = new System.Windows.Forms.GroupBox();
            this.txtProjectInfo = new System.Windows.Forms.TextBox();
            this.grbGeneralRequirements = new System.Windows.Forms.GroupBox();
            this.txtGeneralRequirements = new System.Windows.Forms.TextBox();
            this.grbSpecialRequirements = new System.Windows.Forms.GroupBox();
            this.txtSpecialRequirements = new System.Windows.Forms.TextBox();
            this.cobODPNo = new System.Windows.Forms.ComboBox();
            this.cmsRequirement = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRequirement = new System.Windows.Forms.ToolStripMenuItem();
            this.grbModuleStatistics = new System.Windows.Forms.GroupBox();
            this.dgvScope = new System.Windows.Forms.DataGridView();
            this.txtSalesValue = new System.Windows.Forms.TextBox();
            this.lblSalesValue = new System.Windows.Forms.Label();
            this.grbFinancialData = new System.Windows.Forms.GroupBox();
            this.btnFinancialData = new System.Windows.Forms.Button();
            this.chartTracking = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.grbTracking = new System.Windows.Forms.GroupBox();
            this.grbProjectStatus = new System.Windows.Forms.GroupBox();
            this.chartProjectStatus = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cobYear = new System.Windows.Forms.ComboBox();
            this.cobMonth = new System.Windows.Forms.ComboBox();
            this.timerScroll = new System.Windows.Forms.Timer(this.components);
            this.grbRiskLevel = new System.Windows.Forms.GroupBox();
            this.chartRiskLevel = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.grbProjectType = new System.Windows.Forms.GroupBox();
            this.chartProjectType = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.lblProjectNum = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.grbProject.SuspendLayout();
            this.grbGeneralRequirements.SuspendLayout();
            this.grbSpecialRequirements.SuspendLayout();
            this.cmsRequirement.SuspendLayout();
            this.grbModuleStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScope)).BeginInit();
            this.grbFinancialData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTracking)).BeginInit();
            this.grbTracking.SuspendLayout();
            this.grbProjectStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartProjectStatus)).BeginInit();
            this.grbRiskLevel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRiskLevel)).BeginInit();
            this.grbProjectType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartProjectType)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(139, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "项目编号";
            // 
            // grbProject
            // 
            this.grbProject.Controls.Add(this.txtProjectInfo);
            this.grbProject.Location = new System.Drawing.Point(10, 54);
            this.grbProject.Name = "grbProject";
            this.grbProject.Size = new System.Drawing.Size(302, 227);
            this.grbProject.TabIndex = 2;
            this.grbProject.TabStop = false;
            this.grbProject.Text = "项目基本信息";
            // 
            // txtProjectInfo
            // 
            this.txtProjectInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProjectInfo.Location = new System.Drawing.Point(3, 21);
            this.txtProjectInfo.Multiline = true;
            this.txtProjectInfo.Name = "txtProjectInfo";
            this.txtProjectInfo.ReadOnly = true;
            this.txtProjectInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtProjectInfo.Size = new System.Drawing.Size(296, 203);
            this.txtProjectInfo.TabIndex = 0;
            // 
            // grbGeneralRequirements
            // 
            this.grbGeneralRequirements.Controls.Add(this.txtGeneralRequirements);
            this.grbGeneralRequirements.Location = new System.Drawing.Point(10, 287);
            this.grbGeneralRequirements.Name = "grbGeneralRequirements";
            this.grbGeneralRequirements.Size = new System.Drawing.Size(302, 150);
            this.grbGeneralRequirements.TabIndex = 3;
            this.grbGeneralRequirements.TabStop = false;
            this.grbGeneralRequirements.Text = "通用技术要求";
            // 
            // txtGeneralRequirements
            // 
            this.txtGeneralRequirements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGeneralRequirements.Location = new System.Drawing.Point(3, 21);
            this.txtGeneralRequirements.Multiline = true;
            this.txtGeneralRequirements.Name = "txtGeneralRequirements";
            this.txtGeneralRequirements.ReadOnly = true;
            this.txtGeneralRequirements.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGeneralRequirements.Size = new System.Drawing.Size(296, 126);
            this.txtGeneralRequirements.TabIndex = 0;
            // 
            // grbSpecialRequirements
            // 
            this.grbSpecialRequirements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grbSpecialRequirements.Controls.Add(this.txtSpecialRequirements);
            this.grbSpecialRequirements.Location = new System.Drawing.Point(10, 440);
            this.grbSpecialRequirements.Name = "grbSpecialRequirements";
            this.grbSpecialRequirements.Size = new System.Drawing.Size(302, 248);
            this.grbSpecialRequirements.TabIndex = 4;
            this.grbSpecialRequirements.TabStop = false;
            this.grbSpecialRequirements.Text = "特殊技术要求";
            // 
            // txtSpecialRequirements
            // 
            this.txtSpecialRequirements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSpecialRequirements.Location = new System.Drawing.Point(3, 21);
            this.txtSpecialRequirements.Multiline = true;
            this.txtSpecialRequirements.Name = "txtSpecialRequirements";
            this.txtSpecialRequirements.ReadOnly = true;
            this.txtSpecialRequirements.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSpecialRequirements.Size = new System.Drawing.Size(296, 224);
            this.txtSpecialRequirements.TabIndex = 0;
            // 
            // cobODPNo
            // 
            this.cobODPNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobODPNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobODPNo.ForeColor = System.Drawing.Color.Red;
            this.cobODPNo.FormattingEnabled = true;
            this.cobODPNo.Location = new System.Drawing.Point(204, 27);
            this.cobODPNo.Name = "cobODPNo";
            this.cobODPNo.Size = new System.Drawing.Size(108, 27);
            this.cobODPNo.TabIndex = 0;
            this.cobODPNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cobODPNo_KeyDown);
            // 
            // cmsRequirement
            // 
            this.cmsRequirement.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRequirement});
            this.cmsRequirement.Name = "cmsRequirement";
            this.cmsRequirement.Size = new System.Drawing.Size(210, 26);
            // 
            // tsmiRequirement
            // 
            this.tsmiRequirement.Name = "tsmiRequirement";
            this.tsmiRequirement.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.tsmiRequirement.Size = new System.Drawing.Size(209, 22);
            this.tsmiRequirement.Text = "编辑技术要求(&R)";
            this.tsmiRequirement.Click += new System.EventHandler(this.tsmiRequirement_Click);
            // 
            // grbModuleStatistics
            // 
            this.grbModuleStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbModuleStatistics.Controls.Add(this.dgvScope);
            this.grbModuleStatistics.Location = new System.Drawing.Point(318, 507);
            this.grbModuleStatistics.Name = "grbModuleStatistics";
            this.grbModuleStatistics.Size = new System.Drawing.Size(355, 181);
            this.grbModuleStatistics.TabIndex = 5;
            this.grbModuleStatistics.TabStop = false;
            this.grbModuleStatistics.Text = "机型统计";
            // 
            // dgvScope
            // 
            this.dgvScope.AllowUserToAddRows = false;
            this.dgvScope.AllowUserToDeleteRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Azure;
            this.dgvScope.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvScope.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvScope.BackgroundColor = System.Drawing.Color.White;
            this.dgvScope.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvScope.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvScope.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvScope.EnableHeadersVisualStyles = false;
            this.dgvScope.Location = new System.Drawing.Point(3, 21);
            this.dgvScope.Name = "dgvScope";
            this.dgvScope.ReadOnly = true;
            this.dgvScope.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvScope.Size = new System.Drawing.Size(349, 157);
            this.dgvScope.TabIndex = 0;
            this.dgvScope.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvScope_RowPostPaint);
            // 
            // txtSalesValue
            // 
            this.txtSalesValue.Location = new System.Drawing.Point(54, 20);
            this.txtSalesValue.MaxLength = 10;
            this.txtSalesValue.Name = "txtSalesValue";
            this.txtSalesValue.Size = new System.Drawing.Size(113, 25);
            this.txtSalesValue.TabIndex = 0;
            this.txtSalesValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSalesValue.TextChanged += new System.EventHandler(this.txtSalesValue_TextChanged);
            // 
            // lblSalesValue
            // 
            this.lblSalesValue.AutoSize = true;
            this.lblSalesValue.Location = new System.Drawing.Point(6, 23);
            this.lblSalesValue.Name = "lblSalesValue";
            this.lblSalesValue.Size = new System.Drawing.Size(214, 19);
            this.lblSalesValue.TabIndex = 2;
            this.lblSalesValue.Text = "销售额                               RMB元";
            // 
            // grbFinancialData
            // 
            this.grbFinancialData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbFinancialData.Controls.Add(this.txtSalesValue);
            this.grbFinancialData.Controls.Add(this.btnFinancialData);
            this.grbFinancialData.Controls.Add(this.lblSalesValue);
            this.grbFinancialData.Location = new System.Drawing.Point(318, 440);
            this.grbFinancialData.Name = "grbFinancialData";
            this.grbFinancialData.Size = new System.Drawing.Size(355, 62);
            this.grbFinancialData.TabIndex = 1;
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
            this.btnFinancialData.Location = new System.Drawing.Point(241, 23);
            this.btnFinancialData.Name = "btnFinancialData";
            this.btnFinancialData.Size = new System.Drawing.Size(108, 28);
            this.btnFinancialData.TabIndex = 1;
            this.btnFinancialData.Text = "添加财务数据";
            this.btnFinancialData.UseVisualStyleBackColor = false;
            this.btnFinancialData.Click += new System.EventHandler(this.btnFinancialData_Click);
            // 
            // chartTracking
            // 
            chartArea17.AxisX.IsLabelAutoFit = false;
            chartArea17.AxisX.LabelStyle.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea17.AxisX.MajorGrid.Enabled = false;
            chartArea17.AxisY.LabelStyle.Enabled = false;
            chartArea17.AxisY.MajorGrid.Enabled = false;
            chartArea17.AxisY.MajorTickMark.Enabled = false;
            chartArea17.Name = "ChartArea1";
            this.chartTracking.ChartAreas.Add(chartArea17);
            this.chartTracking.Dock = System.Windows.Forms.DockStyle.Fill;
            legend17.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend17.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend17.ForeColor = System.Drawing.Color.Red;
            legend17.IsTextAutoFit = false;
            legend17.MaximumAutoSize = 100F;
            legend17.Name = "Legend1";
            legend17.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartTracking.Legends.Add(legend17);
            this.chartTracking.Location = new System.Drawing.Point(3, 21);
            this.chartTracking.Name = "chartTracking";
            series17.ChartArea = "ChartArea1";
            series17.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series17.IsValueShownAsLabel = true;
            series17.IsVisibleInLegend = false;
            series17.Legend = "Legend1";
            series17.Name = "Series1";
            this.chartTracking.Series.Add(series17);
            this.chartTracking.Size = new System.Drawing.Size(349, 359);
            this.chartTracking.TabIndex = 7;
            this.chartTracking.Text = "chart1";
            // 
            // grbTracking
            // 
            this.grbTracking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbTracking.Controls.Add(this.chartTracking);
            this.grbTracking.Location = new System.Drawing.Point(318, 54);
            this.grbTracking.Name = "grbTracking";
            this.grbTracking.Size = new System.Drawing.Size(355, 383);
            this.grbTracking.TabIndex = 8;
            this.grbTracking.TabStop = false;
            this.grbTracking.Text = "跟踪信息";
            // 
            // grbProjectStatus
            // 
            this.grbProjectStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbProjectStatus.Controls.Add(this.chartProjectStatus);
            this.grbProjectStatus.Location = new System.Drawing.Point(676, 54);
            this.grbProjectStatus.Name = "grbProjectStatus";
            this.grbProjectStatus.Size = new System.Drawing.Size(511, 210);
            this.grbProjectStatus.TabIndex = 8;
            this.grbProjectStatus.TabStop = false;
            this.grbProjectStatus.Text = "项目状态分布--月";
            // 
            // chartProjectStatus
            // 
            chartArea18.AxisX.IsLabelAutoFit = false;
            chartArea18.AxisX.LabelStyle.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea18.AxisX.MajorGrid.Enabled = false;
            chartArea18.AxisY.LabelStyle.Enabled = false;
            chartArea18.AxisY.MajorGrid.Enabled = false;
            chartArea18.AxisY.MajorTickMark.Enabled = false;
            chartArea18.Name = "ChartArea1";
            this.chartProjectStatus.ChartAreas.Add(chartArea18);
            this.chartProjectStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            legend18.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            legend18.IsTextAutoFit = false;
            legend18.MaximumAutoSize = 100F;
            legend18.Name = "Legend1";
            legend18.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartProjectStatus.Legends.Add(legend18);
            this.chartProjectStatus.Location = new System.Drawing.Point(3, 21);
            this.chartProjectStatus.Name = "chartProjectStatus";
            series18.ChartArea = "ChartArea1";
            series18.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series18.IsValueShownAsLabel = true;
            series18.IsVisibleInLegend = false;
            series18.Legend = "Legend1";
            series18.Name = "Series1";
            this.chartProjectStatus.Series.Add(series18);
            this.chartProjectStatus.Size = new System.Drawing.Size(505, 186);
            this.chartProjectStatus.TabIndex = 7;
            this.chartProjectStatus.Text = "chart1";
            // 
            // cobYear
            // 
            this.cobYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cobYear.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobYear.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobYear.ForeColor = System.Drawing.Color.Red;
            this.cobYear.FormattingEnabled = true;
            this.cobYear.Location = new System.Drawing.Point(1062, 27);
            this.cobYear.Name = "cobYear";
            this.cobYear.Size = new System.Drawing.Size(67, 27);
            this.cobYear.TabIndex = 1;
            this.cobYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cobODPNo_KeyDown);
            // 
            // cobMonth
            // 
            this.cobMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cobMonth.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobMonth.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobMonth.ForeColor = System.Drawing.Color.Red;
            this.cobMonth.FormattingEnabled = true;
            this.cobMonth.Location = new System.Drawing.Point(1135, 27);
            this.cobMonth.Name = "cobMonth";
            this.cobMonth.Size = new System.Drawing.Size(52, 27);
            this.cobMonth.TabIndex = 3;
            this.cobMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cobODPNo_KeyDown);
            // 
            // timerScroll
            // 
            this.timerScroll.Enabled = true;
            this.timerScroll.Interval = 1000;
            this.timerScroll.Tick += new System.EventHandler(this.timerScroll_Tick);
            // 
            // grbRiskLevel
            // 
            this.grbRiskLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbRiskLevel.Controls.Add(this.chartRiskLevel);
            this.grbRiskLevel.Location = new System.Drawing.Point(676, 267);
            this.grbRiskLevel.Name = "grbRiskLevel";
            this.grbRiskLevel.Size = new System.Drawing.Size(511, 210);
            this.grbRiskLevel.TabIndex = 8;
            this.grbRiskLevel.TabStop = false;
            this.grbRiskLevel.Text = "风险等级分布--月";
            // 
            // chartRiskLevel
            // 
            chartArea19.AxisX.IsLabelAutoFit = false;
            chartArea19.AxisX.LabelStyle.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea19.AxisX.MajorGrid.Enabled = false;
            chartArea19.AxisY.LabelStyle.Enabled = false;
            chartArea19.AxisY.MajorGrid.Enabled = false;
            chartArea19.AxisY.MajorTickMark.Enabled = false;
            chartArea19.Name = "ChartArea1";
            this.chartRiskLevel.ChartAreas.Add(chartArea19);
            this.chartRiskLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            legend19.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            legend19.IsTextAutoFit = false;
            legend19.MaximumAutoSize = 100F;
            legend19.Name = "Legend1";
            legend19.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartRiskLevel.Legends.Add(legend19);
            this.chartRiskLevel.Location = new System.Drawing.Point(3, 21);
            this.chartRiskLevel.Name = "chartRiskLevel";
            series19.ChartArea = "ChartArea1";
            series19.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series19.IsValueShownAsLabel = true;
            series19.IsVisibleInLegend = false;
            series19.Legend = "Legend1";
            series19.Name = "Series1";
            this.chartRiskLevel.Series.Add(series19);
            this.chartRiskLevel.Size = new System.Drawing.Size(505, 186);
            this.chartRiskLevel.TabIndex = 7;
            this.chartRiskLevel.Text = "chart1";
            // 
            // grbProjectType
            // 
            this.grbProjectType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbProjectType.Controls.Add(this.chartProjectType);
            this.grbProjectType.Location = new System.Drawing.Point(676, 478);
            this.grbProjectType.Name = "grbProjectType";
            this.grbProjectType.Size = new System.Drawing.Size(511, 210);
            this.grbProjectType.TabIndex = 8;
            this.grbProjectType.TabStop = false;
            this.grbProjectType.Text = "项目类型分布--月";
            // 
            // chartProjectType
            // 
            chartArea20.AxisX.IsLabelAutoFit = false;
            chartArea20.AxisX.LabelStyle.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea20.AxisX.MajorGrid.Enabled = false;
            chartArea20.AxisY.LabelStyle.Enabled = false;
            chartArea20.AxisY.MajorGrid.Enabled = false;
            chartArea20.AxisY.MajorTickMark.Enabled = false;
            chartArea20.Name = "ChartArea1";
            this.chartProjectType.ChartAreas.Add(chartArea20);
            this.chartProjectType.Dock = System.Windows.Forms.DockStyle.Fill;
            legend20.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            legend20.IsTextAutoFit = false;
            legend20.MaximumAutoSize = 100F;
            legend20.Name = "Legend1";
            legend20.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartProjectType.Legends.Add(legend20);
            this.chartProjectType.Location = new System.Drawing.Point(3, 21);
            this.chartProjectType.Name = "chartProjectType";
            series20.ChartArea = "ChartArea1";
            series20.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series20.IsValueShownAsLabel = true;
            series20.IsVisibleInLegend = false;
            series20.Legend = "Legend1";
            series20.Name = "Series1";
            this.chartProjectType.Series.Add(series20);
            this.chartProjectType.Size = new System.Drawing.Size(505, 186);
            this.chartProjectType.TabIndex = 7;
            this.chartProjectType.Text = "chart1";
            // 
            // btnSwitch
            // 
            this.btnSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnSwitch.FlatAppearance.BorderSize = 0;
            this.btnSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitch.ForeColor = System.Drawing.Color.White;
            this.btnSwitch.Location = new System.Drawing.Point(1002, 26);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(54, 28);
            this.btnSwitch.TabIndex = 1;
            this.btnSwitch.Text = "按年";
            this.btnSwitch.UseVisualStyleBackColor = false;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // lblProjectNum
            // 
            this.lblProjectNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProjectNum.AutoSize = true;
            this.lblProjectNum.Location = new System.Drawing.Point(843, 35);
            this.lblProjectNum.Name = "lblProjectNum";
            this.lblProjectNum.Size = new System.Drawing.Size(126, 19);
            this.lblProjectNum.TabIndex = 9;
            this.lblProjectNum.Text = "统计区间项目总数：";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblTime.Location = new System.Drawing.Point(314, 8);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(463, 46);
            this.lblTime.TabIndex = 10;
            this.lblTime.Text = "2020年12月12日 12:12:16";
            // 
            // FrmProjectInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.ContextMenuStrip = this.cmsRequirement;
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblProjectNum);
            this.Controls.Add(this.cobMonth);
            this.Controls.Add(this.btnSwitch);
            this.Controls.Add(this.cobYear);
            this.Controls.Add(this.cobODPNo);
            this.Controls.Add(this.grbSpecialRequirements);
            this.Controls.Add(this.grbGeneralRequirements);
            this.Controls.Add(this.grbModuleStatistics);
            this.Controls.Add(this.grbProject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grbFinancialData);
            this.Controls.Add(this.grbProjectType);
            this.Controls.Add(this.grbRiskLevel);
            this.Controls.Add(this.grbProjectStatus);
            this.Controls.Add(this.grbTracking);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmProjectInfo";
            this.Text = "项目信息";
            this.grbProject.ResumeLayout(false);
            this.grbProject.PerformLayout();
            this.grbGeneralRequirements.ResumeLayout(false);
            this.grbGeneralRequirements.PerformLayout();
            this.grbSpecialRequirements.ResumeLayout(false);
            this.grbSpecialRequirements.PerformLayout();
            this.cmsRequirement.ResumeLayout(false);
            this.grbModuleStatistics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScope)).EndInit();
            this.grbFinancialData.ResumeLayout(false);
            this.grbFinancialData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTracking)).EndInit();
            this.grbTracking.ResumeLayout(false);
            this.grbProjectStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartProjectStatus)).EndInit();
            this.grbRiskLevel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRiskLevel)).EndInit();
            this.grbProjectType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartProjectType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grbProject;
        private System.Windows.Forms.GroupBox grbGeneralRequirements;
        private System.Windows.Forms.GroupBox grbSpecialRequirements;
        private System.Windows.Forms.ComboBox cobODPNo;
        private System.Windows.Forms.ContextMenuStrip cmsRequirement;
        private System.Windows.Forms.ToolStripMenuItem tsmiRequirement;
        private System.Windows.Forms.GroupBox grbModuleStatistics;
        private System.Windows.Forms.DataGridView dgvScope;
        private System.Windows.Forms.TextBox txtSalesValue;
        private System.Windows.Forms.Label lblSalesValue;
        private System.Windows.Forms.GroupBox grbFinancialData;
        private System.Windows.Forms.Button btnFinancialData;
        private System.Windows.Forms.TextBox txtSpecialRequirements;
        private System.Windows.Forms.TextBox txtGeneralRequirements;
        private System.Windows.Forms.TextBox txtProjectInfo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTracking;
        private System.Windows.Forms.GroupBox grbTracking;
        private System.Windows.Forms.GroupBox grbProjectStatus;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartProjectStatus;
        private System.Windows.Forms.ComboBox cobYear;
        private System.Windows.Forms.ComboBox cobMonth;
        private System.Windows.Forms.Timer timerScroll;
        private System.Windows.Forms.GroupBox grbRiskLevel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRiskLevel;
        private System.Windows.Forms.GroupBox grbProjectType;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartProjectType;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Label lblProjectNum;
        private System.Windows.Forms.Label lblTime;
    }
}