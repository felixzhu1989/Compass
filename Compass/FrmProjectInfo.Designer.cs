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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.grbProject.SuspendLayout();
            this.grbGeneralRequirements.SuspendLayout();
            this.grbSpecialRequirements.SuspendLayout();
            this.cmsRequirement.SuspendLayout();
            this.grbModuleStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScope)).BeginInit();
            this.grbFinancialData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTracking)).BeginInit();
            this.grbTracking.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(139, 27);
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
            this.grbProject.Size = new System.Drawing.Size(544, 176);
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
            this.txtProjectInfo.Size = new System.Drawing.Size(538, 152);
            this.txtProjectInfo.TabIndex = 0;
            // 
            // grbGeneralRequirements
            // 
            this.grbGeneralRequirements.Controls.Add(this.txtGeneralRequirements);
            this.grbGeneralRequirements.Location = new System.Drawing.Point(10, 236);
            this.grbGeneralRequirements.Name = "grbGeneralRequirements";
            this.grbGeneralRequirements.Size = new System.Drawing.Size(544, 150);
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
            this.txtGeneralRequirements.Size = new System.Drawing.Size(538, 126);
            this.txtGeneralRequirements.TabIndex = 0;
            // 
            // grbSpecialRequirements
            // 
            this.grbSpecialRequirements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grbSpecialRequirements.Controls.Add(this.txtSpecialRequirements);
            this.grbSpecialRequirements.Location = new System.Drawing.Point(10, 392);
            this.grbSpecialRequirements.Name = "grbSpecialRequirements";
            this.grbSpecialRequirements.Size = new System.Drawing.Size(544, 296);
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
            this.txtSpecialRequirements.Size = new System.Drawing.Size(538, 272);
            this.txtSpecialRequirements.TabIndex = 0;
            // 
            // cobODPNo
            // 
            this.cobODPNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobODPNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobODPNo.ForeColor = System.Drawing.Color.Red;
            this.cobODPNo.FormattingEnabled = true;
            this.cobODPNo.Location = new System.Drawing.Point(204, 23);
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
            this.grbModuleStatistics.Location = new System.Drawing.Point(560, 507);
            this.grbModuleStatistics.Name = "grbModuleStatistics";
            this.grbModuleStatistics.Size = new System.Drawing.Size(628, 181);
            this.grbModuleStatistics.TabIndex = 5;
            this.grbModuleStatistics.TabStop = false;
            this.grbModuleStatistics.Text = "机型统计";
            // 
            // dgvScope
            // 
            this.dgvScope.AllowUserToAddRows = false;
            this.dgvScope.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvScope.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvScope.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvScope.BackgroundColor = System.Drawing.Color.White;
            this.dgvScope.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvScope.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvScope.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvScope.EnableHeadersVisualStyles = false;
            this.dgvScope.Location = new System.Drawing.Point(3, 21);
            this.dgvScope.Name = "dgvScope";
            this.dgvScope.ReadOnly = true;
            this.dgvScope.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvScope.Size = new System.Drawing.Size(622, 157);
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
            this.grbFinancialData.Location = new System.Drawing.Point(560, 392);
            this.grbFinancialData.Name = "grbFinancialData";
            this.grbFinancialData.Size = new System.Drawing.Size(628, 109);
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
            this.btnFinancialData.Location = new System.Drawing.Point(514, 70);
            this.btnFinancialData.Name = "btnFinancialData";
            this.btnFinancialData.Size = new System.Drawing.Size(108, 28);
            this.btnFinancialData.TabIndex = 1;
            this.btnFinancialData.Text = "添加财务数据";
            this.btnFinancialData.UseVisualStyleBackColor = false;
            this.btnFinancialData.Click += new System.EventHandler(this.btnFinancialData_Click);
            // 
            // chartTracking
            // 
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.LabelStyle.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.chartTracking.ChartAreas.Add(chartArea1);
            this.chartTracking.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.ForeColor = System.Drawing.Color.Red;
            legend1.IsTextAutoFit = false;
            legend1.MaximumAutoSize = 100F;
            legend1.Name = "Legend1";
            legend1.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartTracking.Legends.Add(legend1);
            this.chartTracking.Location = new System.Drawing.Point(3, 21);
            this.chartTracking.Name = "chartTracking";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series1.IsValueShownAsLabel = true;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartTracking.Series.Add(series1);
            this.chartTracking.Size = new System.Drawing.Size(622, 308);
            this.chartTracking.TabIndex = 7;
            this.chartTracking.Text = "chart1";
            // 
            // grbTracking
            // 
            this.grbTracking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbTracking.Controls.Add(this.chartTracking);
            this.grbTracking.Location = new System.Drawing.Point(560, 54);
            this.grbTracking.Name = "grbTracking";
            this.grbTracking.Size = new System.Drawing.Size(628, 332);
            this.grbTracking.TabIndex = 8;
            this.grbTracking.TabStop = false;
            this.grbTracking.Text = "跟踪信息";
            // 
            // FrmProjectInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.ContextMenuStrip = this.cmsRequirement;
            this.Controls.Add(this.cobODPNo);
            this.Controls.Add(this.grbSpecialRequirements);
            this.Controls.Add(this.grbGeneralRequirements);
            this.Controls.Add(this.grbModuleStatistics);
            this.Controls.Add(this.grbProject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grbFinancialData);
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
    }
}