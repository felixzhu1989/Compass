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
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartPlan = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvDrawingPlan = new System.Windows.Forms.DataGridView();
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
            this.chartPercent = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartUserPercent = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.UserAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODPNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotalWorkload = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModuleNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrReleaseTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrReleaseActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoodType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.chartPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawingPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartUserPercent)).BeginInit();
            this.SuspendLayout();
            // 
            // chartPlan
            // 
            this.chartPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.BorderColor = System.Drawing.Color.DarkGray;
            chartArea1.Name = "ChartArea1";
            this.chartPlan.ChartAreas.Add(chartArea1);
            legend1.Alignment = System.Drawing.StringAlignment.Far;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            this.chartPlan.Legends.Add(legend1);
            this.chartPlan.Location = new System.Drawing.Point(565, 59);
            this.chartPlan.Name = "chartPlan";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPlan.Series.Add(series1);
            this.chartPlan.Size = new System.Drawing.Size(635, 328);
            this.chartPlan.TabIndex = 8;
            this.chartPlan.Text = "chart1";
            // 
            // dgvDrawingPlan
            // 
            this.dgvDrawingPlan.AllowUserToAddRows = false;
            this.dgvDrawingPlan.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvDrawingPlan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDrawingPlan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.dgvDrawingPlan.EnableHeadersVisualStyles = false;
            this.dgvDrawingPlan.Location = new System.Drawing.Point(1, 59);
            this.dgvDrawingPlan.Name = "dgvDrawingPlan";
            this.dgvDrawingPlan.ReadOnly = true;
            this.dgvDrawingPlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDrawingPlan.Size = new System.Drawing.Size(576, 194);
            this.dgvDrawingPlan.TabIndex = 10;
            // 
            // cobQueryYear
            // 
            this.cobQueryYear.FormattingEnabled = true;
            this.cobQueryYear.Location = new System.Drawing.Point(221, 26);
            this.cobQueryYear.Name = "cobQueryYear";
            this.cobQueryYear.Size = new System.Drawing.Size(68, 27);
            this.cobQueryYear.TabIndex = 36;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(183, 30);
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
            this.btnQueryByYear.Location = new System.Drawing.Point(292, 25);
            this.btnQueryByYear.Name = "btnQueryByYear";
            this.btnQueryByYear.Size = new System.Drawing.Size(46, 28);
            this.btnQueryByYear.TabIndex = 37;
            this.btnQueryByYear.Text = "查询";
            this.btnQueryByYear.UseVisualStyleBackColor = false;
            this.btnQueryByYear.Click += new System.EventHandler(this.btnQueryByYear_Click);
            // 
            // btnHoodModuleNo
            // 
            this.btnHoodModuleNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnHoodModuleNo.FlatAppearance.BorderSize = 0;
            this.btnHoodModuleNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoodModuleNo.ForeColor = System.Drawing.Color.White;
            this.btnHoodModuleNo.Location = new System.Drawing.Point(342, 25);
            this.btnHoodModuleNo.Name = "btnHoodModuleNo";
            this.btnHoodModuleNo.Size = new System.Drawing.Size(104, 28);
            this.btnHoodModuleNo.TabIndex = 37;
            this.btnHoodModuleNo.Text = "普通烟罩数量";
            this.btnHoodModuleNo.UseVisualStyleBackColor = false;
            this.btnHoodModuleNo.Click += new System.EventHandler(this.btnHoodModuleNo_Click);
            // 
            // chartMonth
            // 
            this.chartMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.BorderColor = System.Drawing.Color.DarkGray;
            chartArea2.Name = "ChartArea1";
            this.chartMonth.ChartAreas.Add(chartArea2);
            legend2.Alignment = System.Drawing.StringAlignment.Far;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.Name = "Legend1";
            this.chartMonth.Legends.Add(legend2);
            this.chartMonth.Location = new System.Drawing.Point(567, 374);
            this.chartMonth.Name = "chartMonth";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartMonth.Series.Add(series2);
            this.chartMonth.Size = new System.Drawing.Size(633, 296);
            this.chartMonth.TabIndex = 8;
            this.chartMonth.Text = "chart1";
            // 
            // cobModel
            // 
            this.cobModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cobModel.FormattingEnabled = true;
            this.cobModel.Location = new System.Drawing.Point(630, 391);
            this.cobModel.Name = "cobModel";
            this.cobModel.Size = new System.Drawing.Size(163, 27);
            this.cobModel.TabIndex = 36;
            this.cobModel.Visible = false;
            // 
            // lblModel
            // 
            this.lblModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(578, 395);
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
            this.btnCeilingWorkload.Location = new System.Drawing.Point(450, 25);
            this.btnCeilingWorkload.Name = "btnCeilingWorkload";
            this.btnCeilingWorkload.Size = new System.Drawing.Size(111, 28);
            this.btnCeilingWorkload.TabIndex = 37;
            this.btnCeilingWorkload.Text = "天花烟罩工作量";
            this.btnCeilingWorkload.UseVisualStyleBackColor = false;
            this.btnCeilingWorkload.Click += new System.EventHandler(this.btnCeilingWorkload_Click);
            // 
            // btnAllWorkload
            // 
            this.btnAllWorkload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAllWorkload.FlatAppearance.BorderSize = 0;
            this.btnAllWorkload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllWorkload.ForeColor = System.Drawing.Color.White;
            this.btnAllWorkload.Location = new System.Drawing.Point(565, 25);
            this.btnAllWorkload.Name = "btnAllWorkload";
            this.btnAllWorkload.Size = new System.Drawing.Size(111, 28);
            this.btnAllWorkload.TabIndex = 37;
            this.btnAllWorkload.Text = "所有烟罩工作量";
            this.btnAllWorkload.UseVisualStyleBackColor = false;
            this.btnAllWorkload.Click += new System.EventHandler(this.btnAllWorkload_Click);
            // 
            // chartPercent
            // 
            this.chartPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            chartArea3.Name = "ChartArea1";
            this.chartPercent.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartPercent.Legends.Add(legend3);
            this.chartPercent.Location = new System.Drawing.Point(1, 244);
            this.chartPercent.Name = "chartPercent";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartPercent.Series.Add(series3);
            this.chartPercent.Size = new System.Drawing.Size(576, 191);
            this.chartPercent.TabIndex = 39;
            this.chartPercent.Text = "chart1";
            // 
            // chartUserPercent
            // 
            this.chartUserPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            chartArea4.Name = "ChartArea1";
            this.chartUserPercent.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartUserPercent.Legends.Add(legend4);
            this.chartUserPercent.Location = new System.Drawing.Point(1, 425);
            this.chartUserPercent.Name = "chartUserPercent";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartUserPercent.Series.Add(series4);
            this.chartUserPercent.Size = new System.Drawing.Size(576, 245);
            this.chartUserPercent.TabIndex = 39;
            this.chartUserPercent.Text = "chart1";
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
            // FrmDrawingPlanQuery
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.dgvDrawingPlan);
            this.Controls.Add(this.chartUserPercent);
            this.Controls.Add(this.chartPercent);
            this.Controls.Add(this.cobModel);
            this.Controls.Add(this.cobQueryYear);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnAllWorkload);
            this.Controls.Add(this.btnCeilingWorkload);
            this.Controls.Add(this.btnHoodModuleNo);
            this.Controls.Add(this.btnQueryByYear);
            this.Controls.Add(this.chartMonth);
            this.Controls.Add(this.chartPlan);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmDrawingPlanQuery";
            this.Text = "制图计划统计";
            ((System.ComponentModel.ISupportInitialize)(this.chartPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawingPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartUserPercent)).EndInit();
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
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPercent;
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
    }
}