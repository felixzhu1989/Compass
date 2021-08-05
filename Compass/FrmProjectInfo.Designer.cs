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
            this.label3 = new System.Windows.Forms.Label();
            this.grbProject = new System.Windows.Forms.GroupBox();
            this.grbGeneralRequirements = new System.Windows.Forms.GroupBox();
            this.grbSpecialRequirements = new System.Windows.Forms.GroupBox();
            this.cobODPNo = new System.Windows.Forms.ComboBox();
            this.cmsRequirement = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRequirement = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvScope = new System.Windows.Forms.DataGridView();
            this.txtSalesValue = new System.Windows.Forms.TextBox();
            this.lblSalesValue = new System.Windows.Forms.Label();
            this.grbFinancialData = new System.Windows.Forms.GroupBox();
            this.btnFinancialData = new System.Windows.Forms.Button();
            this.txtSpecialRequirements = new System.Windows.Forms.TextBox();
            this.txtGeneralRequirements = new System.Windows.Forms.TextBox();
            this.txtProjectInfo = new System.Windows.Forms.TextBox();
            this.grbProject.SuspendLayout();
            this.grbGeneralRequirements.SuspendLayout();
            this.grbSpecialRequirements.SuspendLayout();
            this.cmsRequirement.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScope)).BeginInit();
            this.grbFinancialData.SuspendLayout();
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
            // grbGeneralRequirements
            // 
            this.grbGeneralRequirements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbGeneralRequirements.Controls.Add(this.txtGeneralRequirements);
            this.grbGeneralRequirements.Location = new System.Drawing.Point(10, 236);
            this.grbGeneralRequirements.Name = "grbGeneralRequirements";
            this.grbGeneralRequirements.Size = new System.Drawing.Size(544, 150);
            this.grbGeneralRequirements.TabIndex = 3;
            this.grbGeneralRequirements.TabStop = false;
            this.grbGeneralRequirements.Text = "通用技术要求";
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
            this.cmsRequirement.Size = new System.Drawing.Size(208, 26);
            // 
            // tsmiRequirement
            // 
            this.tsmiRequirement.Name = "tsmiRequirement";
            this.tsmiRequirement.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsmiRequirement.Size = new System.Drawing.Size(207, 22);
            this.tsmiRequirement.Text = "编辑技术要求(&E)";
            this.tsmiRequirement.Click += new System.EventHandler(this.tsmiRequirement_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvScope);
            this.groupBox1.Location = new System.Drawing.Point(560, 236);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 452);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "范围";
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
            this.dgvScope.Size = new System.Drawing.Size(372, 428);
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
            this.grbFinancialData.Location = new System.Drawing.Point(560, 54);
            this.grbFinancialData.Name = "grbFinancialData";
            this.grbFinancialData.Size = new System.Drawing.Size(378, 176);
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
            this.btnFinancialData.Location = new System.Drawing.Point(264, 137);
            this.btnFinancialData.Name = "btnFinancialData";
            this.btnFinancialData.Size = new System.Drawing.Size(108, 28);
            this.btnFinancialData.TabIndex = 1;
            this.btnFinancialData.Text = "添加财务数据";
            this.btnFinancialData.UseVisualStyleBackColor = false;
            this.btnFinancialData.Click += new System.EventHandler(this.btnFinancialData_Click);
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
            // FrmProjectInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(950, 700);
            this.ContextMenuStrip = this.cmsRequirement;
            this.Controls.Add(this.cobODPNo);
            this.Controls.Add(this.grbSpecialRequirements);
            this.Controls.Add(this.grbGeneralRequirements);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbProject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grbFinancialData);
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
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScope)).EndInit();
            this.grbFinancialData.ResumeLayout(false);
            this.grbFinancialData.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvScope;
        private System.Windows.Forms.TextBox txtSalesValue;
        private System.Windows.Forms.Label lblSalesValue;
        private System.Windows.Forms.GroupBox grbFinancialData;
        private System.Windows.Forms.Button btnFinancialData;
        private System.Windows.Forms.TextBox txtSpecialRequirements;
        private System.Windows.Forms.TextBox txtGeneralRequirements;
        private System.Windows.Forms.TextBox txtProjectInfo;
    }
}