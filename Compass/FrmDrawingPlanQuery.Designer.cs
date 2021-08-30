namespace Compass
{
    partial class FrmDrawingPlanQuery
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDrawingPlanQuery));
            this.chartPlan = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvDrawingPlan = new System.Windows.Forms.DataGridView();
            this.UserAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODPNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotalWorkload = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModuleNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrReleaseTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrReleaseActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoodType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cobQueryYear = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnQueryByYear = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnHoodModuleNo = new System.Windows.Forms.Button();
            this.chartMonth = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cobModel = new System.Windows.Forms.ComboBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.btnCeilingWorkload = new System.Windows.Forms.Button();
            this.btnAllWorkload = new System.Windows.Forms.Button();
            this.chartHoodTypePercent = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartUserPercent = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblHoodType = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.chartPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawingPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHoodTypePercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartUserPercent)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartPlan
            // 
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX2.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX2.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY2.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.BorderColor = System.Drawing.Color.DarkGray;
            chartArea1.Name = "ChartArea1";
            this.chartPlan.ChartAreas.Add(chartArea1);
            this.chartPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Alignment = System.Drawing.StringAlignment.Far;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            this.chartPlan.Legends.Add(legend1);
            this.chartPlan.Location = new System.Drawing.Point(528, 3);
            this.chartPlan.Name = "chartPlan";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPlan.Series.Add(series1);
            this.chartPlan.Size = new System.Drawing.Size(635, 291);
            this.chartPlan.TabIndex = 8;
            this.chartPlan.Text = "chart1";
            // 
            // dgvDrawingPlan
            // 
            this.dgvDrawingPlan.AllowUserToAddRows = false;
            this.dgvDrawingPlan.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvDrawingPlan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
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
            this.Model,
            this.SubTotalWorkload,
            this.ModuleNo,
            this.DrReleaseTarget,
            this.DrReleaseActual,
            this.HoodType,
            this.ProjectName});
            this.dgvDrawingPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDrawingPlan.EnableHeadersVisualStyles = false;
            this.dgvDrawingPlan.Location = new System.Drawing.Point(3, 3);
            this.dgvDrawingPlan.Name = "dgvDrawingPlan";
            this.dgvDrawingPlan.ReadOnly = true;
            this.dgvDrawingPlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDrawingPlan.Size = new System.Drawing.Size(519, 291);
            this.dgvDrawingPlan.TabIndex = 10;
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
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "烟罩型号";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            this.Model.Width = 86;
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
            // ModuleNo
            // 
            this.ModuleNo.DataPropertyName = "ModuleNo";
            this.ModuleNo.HeaderText = "分段数";
            this.ModuleNo.Name = "ModuleNo";
            this.ModuleNo.ReadOnly = true;
            this.ModuleNo.ToolTipText = "一组烟罩分段数量";
            this.ModuleNo.Width = 73;
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
            // DrReleaseActual
            // 
            this.DrReleaseActual.DataPropertyName = "DrReleaseActual";
            this.DrReleaseActual.HeaderText = "实际发图";
            this.DrReleaseActual.Name = "DrReleaseActual";
            this.DrReleaseActual.ReadOnly = true;
            this.DrReleaseActual.Width = 86;
            // 
            // HoodType
            // 
            this.HoodType.DataPropertyName = "HoodType";
            this.HoodType.HeaderText = "烟罩类型";
            this.HoodType.Name = "HoodType";
            this.HoodType.ReadOnly = true;
            this.HoodType.Width = 86;
            // 
            // ProjectName
            // 
            this.ProjectName.DataPropertyName = "ProjectName";
            this.ProjectName.HeaderText = "项目名称";
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.ReadOnly = true;
            this.ProjectName.Width = 86;
            // 
            // cobQueryYear
            // 
            this.cobQueryYear.FormattingEnabled = true;
            this.cobQueryYear.Location = new System.Drawing.Point(221, 22);
            this.cobQueryYear.Name = "cobQueryYear";
            this.cobQueryYear.Size = new System.Drawing.Size(68, 27);
            this.cobQueryYear.TabIndex = 36;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(183, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 19);
            this.label6.TabIndex = 38;
            this.label6.Text = "年度：";
            // 
            // btnQueryByYear
            // 
            this.btnQueryByYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnQueryByYear.FlatAppearance.BorderSize = 0;
            this.btnQueryByYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQueryByYear.ForeColor = System.Drawing.Color.White;
            this.btnQueryByYear.Location = new System.Drawing.Point(292, 21);
            this.btnQueryByYear.Name = "btnQueryByYear";
            this.btnQueryByYear.Size = new System.Drawing.Size(46, 28);
            this.btnQueryByYear.TabIndex = 37;
            this.btnQueryByYear.Text = "查询";
            this.btnQueryByYear.UseVisualStyleBackColor = false;
            this.btnQueryByYear.Click += new System.EventHandler(this.BtnQueryByYear_Click);
            // 
            // btnHoodModuleNo
            // 
            this.btnHoodModuleNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnHoodModuleNo.FlatAppearance.BorderSize = 0;
            this.btnHoodModuleNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoodModuleNo.ForeColor = System.Drawing.Color.White;
            this.btnHoodModuleNo.Location = new System.Drawing.Point(342, 21);
            this.btnHoodModuleNo.Name = "btnHoodModuleNo";
            this.btnHoodModuleNo.Size = new System.Drawing.Size(104, 28);
            this.btnHoodModuleNo.TabIndex = 37;
            this.btnHoodModuleNo.Text = "普通烟罩数量";
            this.btnHoodModuleNo.UseVisualStyleBackColor = false;
            this.btnHoodModuleNo.Click += new System.EventHandler(this.BtnHoodModuleNo_Click);
            // 
            // chartMonth
            // 
            chartArea2.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY2.MajorGrid.Enabled = false;
            chartArea2.AxisY2.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY2.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.BorderColor = System.Drawing.Color.DarkGray;
            chartArea2.Name = "ChartArea1";
            this.chartMonth.ChartAreas.Add(chartArea2);
            this.chartMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Alignment = System.Drawing.StringAlignment.Far;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.Name = "Legend1";
            this.chartMonth.Legends.Add(legend2);
            this.chartMonth.Location = new System.Drawing.Point(528, 300);
            this.chartMonth.Name = "chartMonth";
            this.tableLayoutPanel1.SetRowSpan(this.chartMonth, 2);
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartMonth.Series.Add(series4);
            this.chartMonth.Size = new System.Drawing.Size(635, 292);
            this.chartMonth.TabIndex = 8;
            this.chartMonth.Text = "chart1";
            // 
            // cobModel
            // 
            this.cobModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cobModel.FormattingEnabled = true;
            this.cobModel.Location = new System.Drawing.Point(607, 363);
            this.cobModel.Name = "cobModel";
            this.cobModel.Size = new System.Drawing.Size(163, 27);
            this.cobModel.TabIndex = 36;
            this.cobModel.Visible = false;
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(553, 367);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(48, 19);
            this.lblModel.TabIndex = 38;
            this.lblModel.Text = "型号：";
            this.lblModel.Visible = false;
            // 
            // btnCeilingWorkload
            // 
            this.btnCeilingWorkload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCeilingWorkload.FlatAppearance.BorderSize = 0;
            this.btnCeilingWorkload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCeilingWorkload.ForeColor = System.Drawing.Color.White;
            this.btnCeilingWorkload.Location = new System.Drawing.Point(450, 21);
            this.btnCeilingWorkload.Name = "btnCeilingWorkload";
            this.btnCeilingWorkload.Size = new System.Drawing.Size(111, 28);
            this.btnCeilingWorkload.TabIndex = 37;
            this.btnCeilingWorkload.Text = "天花烟罩工作量";
            this.btnCeilingWorkload.UseVisualStyleBackColor = false;
            this.btnCeilingWorkload.Click += new System.EventHandler(this.BtnCeilingWorkload_Click);
            // 
            // btnAllWorkload
            // 
            this.btnAllWorkload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAllWorkload.FlatAppearance.BorderSize = 0;
            this.btnAllWorkload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllWorkload.ForeColor = System.Drawing.Color.White;
            this.btnAllWorkload.Location = new System.Drawing.Point(565, 21);
            this.btnAllWorkload.Name = "btnAllWorkload";
            this.btnAllWorkload.Size = new System.Drawing.Size(111, 28);
            this.btnAllWorkload.TabIndex = 37;
            this.btnAllWorkload.Text = "所有烟罩工作量";
            this.btnAllWorkload.UseVisualStyleBackColor = false;
            this.btnAllWorkload.Click += new System.EventHandler(this.BtnAllWorkload_Click);
            // 
            // chartHoodTypePercent
            // 
            chartArea3.Name = "ChartArea1";
            this.chartHoodTypePercent.ChartAreas.Add(chartArea3);
            this.chartHoodTypePercent.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.chartHoodTypePercent.Legends.Add(legend3);
            this.chartHoodTypePercent.Location = new System.Drawing.Point(3, 300);
            this.chartHoodTypePercent.Name = "chartHoodTypePercent";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartHoodTypePercent.Series.Add(series2);
            this.chartHoodTypePercent.Size = new System.Drawing.Size(519, 130);
            this.chartHoodTypePercent.TabIndex = 39;
            this.chartHoodTypePercent.Text = "chart1";
            // 
            // chartUserPercent
            // 
            chartArea4.Name = "ChartArea1";
            this.chartUserPercent.ChartAreas.Add(chartArea4);
            this.chartUserPercent.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.chartUserPercent.Legends.Add(legend4);
            this.chartUserPercent.Location = new System.Drawing.Point(3, 436);
            this.chartUserPercent.Name = "chartUserPercent";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartUserPercent.Series.Add(series3);
            this.chartUserPercent.Size = new System.Drawing.Size(519, 156);
            this.chartUserPercent.TabIndex = 39;
            this.chartUserPercent.Text = "chart1";
            // 
            // lblHoodType
            // 
            this.lblHoodType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHoodType.AutoSize = true;
            this.lblHoodType.Location = new System.Drawing.Point(25, 469);
            this.lblHoodType.Name = "lblHoodType";
            this.lblHoodType.Size = new System.Drawing.Size(165, 19);
            this.lblHoodType.TabIndex = 38;
            this.lblHoodType.Text = "年度烟罩与天花工作量比例";
            // 
            // lblUser
            // 
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(25, 631);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(139, 19);
            this.lblUser.TabIndex = 38;
            this.lblUser.Text = "年度各人员工作量比例";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 525F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.chartPlan, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvDrawingPlan, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chartHoodTypePercent, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chartUserPercent, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.chartMonth, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1160, 595);
            this.tableLayoutPanel1.TabIndex = 40;
            // 
            // FrmDrawingPlanQuery
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.cobModel);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblHoodType);
            this.Controls.Add(this.btnAllWorkload);
            this.Controls.Add(this.cobQueryYear);
            this.Controls.Add(this.btnCeilingWorkload);
            this.Controls.Add(this.btnHoodModuleNo);
            this.Controls.Add(this.btnQueryByYear);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDrawingPlanQuery";
            this.Text = "制图计划统计";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.FrmDrawingPlanQuery_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.chartPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawingPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHoodTypePercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartUserPercent)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPlan;
        private System.Windows.Forms.DataGridView dgvDrawingPlan;
        private System.Windows.Forms.ComboBox cobQueryYear;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnQueryByYear;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnHoodModuleNo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMonth;
        private System.Windows.Forms.ComboBox cobModel;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Button btnCeilingWorkload;
        private System.Windows.Forms.Button btnAllWorkload;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHoodTypePercent;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartUserPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODPNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotalWorkload;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrReleaseTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrReleaseActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoodType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectName;
        private System.Windows.Forms.Label lblHoodType;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}