namespace Compass
{
    partial class FrmProjectMeasure
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProjectMeasure));
            this.chartProjectOpen = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cobMonth = new System.Windows.Forms.ComboBox();
            this.cobYear = new System.Windows.Forms.ComboBox();
            this.chartProductionClose = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartProjectClose = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chartDeliveryReliability = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartCycleTime = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txtOdpNo = new System.Windows.Forms.TextBox();
            this.txtCycleTimeNote = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartProjectOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProductionClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProjectClose)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDeliveryReliability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCycleTime)).BeginInit();
            this.SuspendLayout();
            // 
            // chartProjectOpen
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
            this.chartProjectOpen.ChartAreas.Add(chartArea1);
            this.chartProjectOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Alignment = System.Drawing.StringAlignment.Far;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            this.chartProjectOpen.Legends.Add(legend1);
            this.chartProjectOpen.Location = new System.Drawing.Point(3, 3);
            this.chartProjectOpen.Name = "chartProjectOpen";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartProjectOpen.Series.Add(series1);
            this.chartProjectOpen.Size = new System.Drawing.Size(636, 192);
            this.chartProjectOpen.TabIndex = 0;
            this.chartProjectOpen.Tag = "Project Open Trend [Monthly]";
            this.chartProjectOpen.Text = "Project Open Trend [Monthly]";
            // 
            // cobMonth
            // 
            this.cobMonth.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobMonth.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobMonth.ForeColor = System.Drawing.Color.Red;
            this.cobMonth.FormattingEnabled = true;
            this.cobMonth.Location = new System.Drawing.Point(209, 24);
            this.cobMonth.Name = "cobMonth";
            this.cobMonth.Size = new System.Drawing.Size(52, 27);
            this.cobMonth.TabIndex = 0;
            // 
            // cobYear
            // 
            this.cobYear.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobYear.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobYear.ForeColor = System.Drawing.Color.Red;
            this.cobYear.FormattingEnabled = true;
            this.cobYear.Location = new System.Drawing.Point(136, 24);
            this.cobYear.Name = "cobYear";
            this.cobYear.Size = new System.Drawing.Size(67, 27);
            this.cobYear.TabIndex = 1;
            // 
            // chartProductionClose
            // 
            chartArea2.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX2.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX2.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY2.MajorGrid.Enabled = false;
            chartArea2.AxisY2.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY2.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.BorderColor = System.Drawing.Color.DarkGray;
            chartArea2.Name = "ChartArea1";
            this.chartProductionClose.ChartAreas.Add(chartArea2);
            this.chartProductionClose.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Alignment = System.Drawing.StringAlignment.Far;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.Name = "Legend1";
            this.chartProductionClose.Legends.Add(legend2);
            this.chartProductionClose.Location = new System.Drawing.Point(3, 201);
            this.chartProductionClose.Name = "chartProductionClose";
            this.tableLayoutPanel1.SetRowSpan(this.chartProductionClose, 2);
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chartProductionClose.Series.Add(series5);
            this.chartProductionClose.Size = new System.Drawing.Size(636, 192);
            this.chartProductionClose.TabIndex = 1;
            this.chartProductionClose.Tag = "Production Close Trend [Monthly]";
            this.chartProductionClose.Text = "Production Close Trend [Monthly]";
            // 
            // chartProjectClose
            // 
            chartArea3.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea3.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea3.AxisX2.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea3.AxisX2.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea3.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea3.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea3.AxisY2.MajorGrid.Enabled = false;
            chartArea3.AxisY2.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea3.AxisY2.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea3.BorderColor = System.Drawing.Color.DarkGray;
            chartArea3.Name = "ChartArea1";
            this.chartProjectClose.ChartAreas.Add(chartArea3);
            this.chartProjectClose.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Alignment = System.Drawing.StringAlignment.Far;
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend3.Name = "Legend1";
            this.chartProjectClose.Legends.Add(legend3);
            this.chartProjectClose.Location = new System.Drawing.Point(3, 399);
            this.chartProjectClose.Name = "chartProjectClose";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartProjectClose.Series.Add(series2);
            this.chartProjectClose.Size = new System.Drawing.Size(636, 193);
            this.chartProjectClose.TabIndex = 2;
            this.chartProjectClose.Tag = "Project Close Trend [Monthly]";
            this.chartProjectClose.Text = "Project Close Trend [Monthly]";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.21372F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.78628F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.chartProjectOpen, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chartProductionClose, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chartProjectClose, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.chartDeliveryReliability, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chartCycleTime, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtOdpNo, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCycleTimeNote, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33555F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66611F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66611F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33223F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1160, 595);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // chartDeliveryReliability
            // 
            chartArea4.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea4.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea4.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea4.AxisX2.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea4.AxisX2.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea4.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea4.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea4.AxisY2.MajorGrid.Enabled = false;
            chartArea4.AxisY2.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea4.AxisY2.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea4.BorderColor = System.Drawing.Color.DarkGray;
            chartArea4.Name = "ChartArea1";
            this.chartDeliveryReliability.ChartAreas.Add(chartArea4);
            this.chartDeliveryReliability.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Alignment = System.Drawing.StringAlignment.Far;
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend4.Name = "Legend1";
            this.chartDeliveryReliability.Legends.Add(legend4);
            this.chartDeliveryReliability.Location = new System.Drawing.Point(645, 3);
            this.chartDeliveryReliability.Name = "chartDeliveryReliability";
            this.tableLayoutPanel1.SetRowSpan(this.chartDeliveryReliability, 2);
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartDeliveryReliability.Series.Add(series3);
            this.chartDeliveryReliability.Size = new System.Drawing.Size(401, 291);
            this.chartDeliveryReliability.TabIndex = 3;
            this.chartDeliveryReliability.Text = "Delivery Reliability";
            // 
            // chartCycleTime
            // 
            chartArea5.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea5.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea5.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea5.AxisX2.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea5.AxisX2.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea5.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea5.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea5.AxisY2.MajorGrid.Enabled = false;
            chartArea5.AxisY2.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea5.AxisY2.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea5.BorderColor = System.Drawing.Color.DarkGray;
            chartArea5.Name = "ChartArea1";
            this.chartCycleTime.ChartAreas.Add(chartArea5);
            this.chartCycleTime.Dock = System.Windows.Forms.DockStyle.Fill;
            legend5.Alignment = System.Drawing.StringAlignment.Far;
            legend5.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend5.Name = "Legend1";
            this.chartCycleTime.Legends.Add(legend5);
            this.chartCycleTime.Location = new System.Drawing.Point(645, 300);
            this.chartCycleTime.Name = "chartCycleTime";
            this.tableLayoutPanel1.SetRowSpan(this.chartCycleTime, 2);
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartCycleTime.Series.Add(series4);
            this.chartCycleTime.Size = new System.Drawing.Size(401, 292);
            this.chartCycleTime.TabIndex = 4;
            this.chartCycleTime.Text = "chart1";
            // 
            // txtOdpNo
            // 
            this.txtOdpNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOdpNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOdpNo.Location = new System.Drawing.Point(1052, 3);
            this.txtOdpNo.Multiline = true;
            this.txtOdpNo.Name = "txtOdpNo";
            this.txtOdpNo.ReadOnly = true;
            this.txtOdpNo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOdpNo.Size = new System.Drawing.Size(105, 192);
            this.txtOdpNo.TabIndex = 5;
            this.txtOdpNo.Text = "FSO888888";
            // 
            // txtCycleTimeNote
            // 
            this.txtCycleTimeNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCycleTimeNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCycleTimeNote.Location = new System.Drawing.Point(1052, 399);
            this.txtCycleTimeNote.Multiline = true;
            this.txtCycleTimeNote.Name = "txtCycleTimeNote";
            this.txtCycleTimeNote.ReadOnly = true;
            this.txtCycleTimeNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCycleTimeNote.Size = new System.Drawing.Size(105, 193);
            this.txtCycleTimeNote.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(1052, 201);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(105, 93);
            this.textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(1052, 300);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(105, 93);
            this.textBox2.TabIndex = 8;
            // 
            // FrmProjectMeasure
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.cobMonth);
            this.Controls.Add(this.cobYear);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmProjectMeasure";
            this.Text = "项目测量";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.chartProjectOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProductionClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProjectClose)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDeliveryReliability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCycleTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartProjectOpen;
        private System.Windows.Forms.ComboBox cobMonth;
        private System.Windows.Forms.ComboBox cobYear;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartProductionClose;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartProjectClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDeliveryReliability;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCycleTime;
        private System.Windows.Forms.TextBox txtOdpNo;
        private System.Windows.Forms.TextBox txtCycleTimeNote;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
    }
}