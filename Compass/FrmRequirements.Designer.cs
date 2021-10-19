namespace Compass
{
    partial class FrmRequirements
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRequirements));
            this.grbSpecialRequirements = new System.Windows.Forms.GroupBox();
            this.dgvSpecialRequirements = new System.Windows.Forms.DataGridView();
            this.SpecialRequirementId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODPNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Content = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditSpecialRequirement = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteSpecialRequirement = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSpecialRequirement = new System.Windows.Forms.Button();
            this.txtContant = new System.Windows.Forms.TextBox();
            this.txtSpecialRequirementId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.grbGeneralRequirements = new System.Windows.Forms.GroupBox();
            this.cobANSULSystem = new System.Windows.Forms.ComboBox();
            this.cobANSULPrepipe = new System.Windows.Forms.ComboBox();
            this.cobMARVEL = new System.Windows.Forms.ComboBox();
            this.cobInputPower = new System.Windows.Forms.ComboBox();
            this.cobRiskLevel = new System.Windows.Forms.ComboBox();
            this.cobTypeName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGeneralRequirementId = new System.Windows.Forms.TextBox();
            this.btnGeneralRequirement = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtODPNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.grbSpecialRequirements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecialRequirements)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.grbGeneralRequirements.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbSpecialRequirements
            // 
            this.grbSpecialRequirements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbSpecialRequirements.Controls.Add(this.dgvSpecialRequirements);
            this.grbSpecialRequirements.Controls.Add(this.btnSpecialRequirement);
            this.grbSpecialRequirements.Controls.Add(this.txtContant);
            this.grbSpecialRequirements.Controls.Add(this.txtSpecialRequirementId);
            this.grbSpecialRequirements.Controls.Add(this.label9);
            this.grbSpecialRequirements.Location = new System.Drawing.Point(11, 166);
            this.grbSpecialRequirements.Name = "grbSpecialRequirements";
            this.grbSpecialRequirements.Size = new System.Drawing.Size(928, 393);
            this.grbSpecialRequirements.TabIndex = 38;
            this.grbSpecialRequirements.TabStop = false;
            this.grbSpecialRequirements.Text = "特殊技术要求";
            // 
            // dgvSpecialRequirements
            // 
            this.dgvSpecialRequirements.AllowUserToAddRows = false;
            this.dgvSpecialRequirements.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvSpecialRequirements.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSpecialRequirements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSpecialRequirements.BackgroundColor = System.Drawing.Color.White;
            this.dgvSpecialRequirements.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSpecialRequirements.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSpecialRequirements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpecialRequirements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SpecialRequirementId,
            this.ODPNo,
            this.Content});
            this.dgvSpecialRequirements.ContextMenuStrip = this.contextMenuStrip;
            this.dgvSpecialRequirements.EnableHeadersVisualStyles = false;
            this.dgvSpecialRequirements.Location = new System.Drawing.Point(12, 51);
            this.dgvSpecialRequirements.Name = "dgvSpecialRequirements";
            this.dgvSpecialRequirements.ReadOnly = true;
            this.dgvSpecialRequirements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSpecialRequirements.Size = new System.Drawing.Size(910, 336);
            this.dgvSpecialRequirements.TabIndex = 42;
            this.dgvSpecialRequirements.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpecialRequirements_CellDoubleClick);
            this.dgvSpecialRequirements.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvSpecialRequirements_RowPostPaint);
            this.dgvSpecialRequirements.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSpecialRequirements_KeyDown);
            // 
            // SpecialRequirementId
            // 
            this.SpecialRequirementId.DataPropertyName = "SpecialRequirementId";
            this.SpecialRequirementId.HeaderText = "ID";
            this.SpecialRequirementId.Name = "SpecialRequirementId";
            this.SpecialRequirementId.ReadOnly = true;
            this.SpecialRequirementId.Width = 40;
            // 
            // ODPNo
            // 
            this.ODPNo.DataPropertyName = "ODPNo";
            this.ODPNo.HeaderText = "项目编号ODP";
            this.ODPNo.Name = "ODPNo";
            this.ODPNo.ReadOnly = true;
            this.ODPNo.Width = 115;
            // 
            // Content
            // 
            this.Content.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Content.DataPropertyName = "Content";
            this.Content.HeaderText = "特殊技术要求";
            this.Content.Name = "Content";
            this.Content.ReadOnly = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditSpecialRequirement,
            this.tsmiDeleteSpecialRequirement});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(173, 48);
            // 
            // tsmiEditSpecialRequirement
            // 
            this.tsmiEditSpecialRequirement.Name = "tsmiEditSpecialRequirement";
            this.tsmiEditSpecialRequirement.Size = new System.Drawing.Size(172, 22);
            this.tsmiEditSpecialRequirement.Text = "修改特殊技术要求";
            this.tsmiEditSpecialRequirement.Click += new System.EventHandler(this.tsmiEditSpecialRequirement_Click);
            // 
            // tsmiDeleteSpecialRequirement
            // 
            this.tsmiDeleteSpecialRequirement.Name = "tsmiDeleteSpecialRequirement";
            this.tsmiDeleteSpecialRequirement.Size = new System.Drawing.Size(172, 22);
            this.tsmiDeleteSpecialRequirement.Text = "删除特殊技术要求";
            this.tsmiDeleteSpecialRequirement.Click += new System.EventHandler(this.tsmiDeleteSpecialRequirement_Click);
            // 
            // btnSpecialRequirement
            // 
            this.btnSpecialRequirement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSpecialRequirement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSpecialRequirement.FlatAppearance.BorderSize = 0;
            this.btnSpecialRequirement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpecialRequirement.ForeColor = System.Drawing.Color.White;
            this.btnSpecialRequirement.Location = new System.Drawing.Point(782, 18);
            this.btnSpecialRequirement.Name = "btnSpecialRequirement";
            this.btnSpecialRequirement.Size = new System.Drawing.Size(140, 28);
            this.btnSpecialRequirement.TabIndex = 36;
            this.btnSpecialRequirement.Text = "添加特殊技术要求";
            this.btnSpecialRequirement.UseVisualStyleBackColor = false;
            this.btnSpecialRequirement.Click += new System.EventHandler(this.btnSpecialRequirement_Click);
            // 
            // txtContant
            // 
            this.txtContant.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContant.Location = new System.Drawing.Point(12, 20);
            this.txtContant.Name = "txtContant";
            this.txtContant.Size = new System.Drawing.Size(666, 25);
            this.txtContant.TabIndex = 40;
            this.txtContant.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContant_KeyDown);
            // 
            // txtSpecialRequirementId
            // 
            this.txtSpecialRequirementId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSpecialRequirementId.Location = new System.Drawing.Point(718, 20);
            this.txtSpecialRequirementId.Name = "txtSpecialRequirementId";
            this.txtSpecialRequirementId.ReadOnly = true;
            this.txtSpecialRequirementId.Size = new System.Drawing.Size(59, 25);
            this.txtSpecialRequirementId.TabIndex = 40;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(679, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 19);
            this.label9.TabIndex = 41;
            this.label9.Text = "SRID";
            // 
            // grbGeneralRequirements
            // 
            this.grbGeneralRequirements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbGeneralRequirements.Controls.Add(this.cobANSULSystem);
            this.grbGeneralRequirements.Controls.Add(this.cobANSULPrepipe);
            this.grbGeneralRequirements.Controls.Add(this.cobMARVEL);
            this.grbGeneralRequirements.Controls.Add(this.cobInputPower);
            this.grbGeneralRequirements.Controls.Add(this.cobRiskLevel);
            this.grbGeneralRequirements.Controls.Add(this.cobTypeName);
            this.grbGeneralRequirements.Controls.Add(this.label7);
            this.grbGeneralRequirements.Controls.Add(this.label6);
            this.grbGeneralRequirements.Controls.Add(this.label5);
            this.grbGeneralRequirements.Controls.Add(this.label10);
            this.grbGeneralRequirements.Controls.Add(this.label4);
            this.grbGeneralRequirements.Controls.Add(this.label2);
            this.grbGeneralRequirements.Controls.Add(this.txtGeneralRequirementId);
            this.grbGeneralRequirements.Controls.Add(this.btnGeneralRequirement);
            this.grbGeneralRequirements.Controls.Add(this.label1);
            this.grbGeneralRequirements.Location = new System.Drawing.Point(11, 63);
            this.grbGeneralRequirements.Name = "grbGeneralRequirements";
            this.grbGeneralRequirements.Size = new System.Drawing.Size(928, 101);
            this.grbGeneralRequirements.TabIndex = 39;
            this.grbGeneralRequirements.TabStop = false;
            this.grbGeneralRequirements.Text = "通用技术要求";
            // 
            // cobANSULSystem
            // 
            this.cobANSULSystem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobANSULSystem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobANSULSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobANSULSystem.FormattingEnabled = true;
            this.cobANSULSystem.Location = new System.Drawing.Point(570, 62);
            this.cobANSULSystem.Name = "cobANSULSystem";
            this.cobANSULSystem.Size = new System.Drawing.Size(108, 27);
            this.cobANSULSystem.TabIndex = 43;
            // 
            // cobANSULPrepipe
            // 
            this.cobANSULPrepipe.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobANSULPrepipe.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobANSULPrepipe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobANSULPrepipe.FormattingEnabled = true;
            this.cobANSULPrepipe.Location = new System.Drawing.Point(330, 60);
            this.cobANSULPrepipe.Name = "cobANSULPrepipe";
            this.cobANSULPrepipe.Size = new System.Drawing.Size(108, 27);
            this.cobANSULPrepipe.TabIndex = 43;
            // 
            // cobMARVEL
            // 
            this.cobMARVEL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobMARVEL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobMARVEL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobMARVEL.FormattingEnabled = true;
            this.cobMARVEL.Location = new System.Drawing.Point(85, 63);
            this.cobMARVEL.Name = "cobMARVEL";
            this.cobMARVEL.Size = new System.Drawing.Size(108, 27);
            this.cobMARVEL.TabIndex = 43;
            // 
            // cobInputPower
            // 
            this.cobInputPower.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobInputPower.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobInputPower.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobInputPower.FormattingEnabled = true;
            this.cobInputPower.Location = new System.Drawing.Point(570, 28);
            this.cobInputPower.Name = "cobInputPower";
            this.cobInputPower.Size = new System.Drawing.Size(108, 27);
            this.cobInputPower.TabIndex = 43;
            // 
            // cobRiskLevel
            // 
            this.cobRiskLevel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobRiskLevel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobRiskLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobRiskLevel.FormattingEnabled = true;
            this.cobRiskLevel.Location = new System.Drawing.Point(85, 28);
            this.cobRiskLevel.Name = "cobRiskLevel";
            this.cobRiskLevel.Size = new System.Drawing.Size(108, 27);
            this.cobRiskLevel.TabIndex = 43;
            // 
            // cobTypeName
            // 
            this.cobTypeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobTypeName.FormattingEnabled = true;
            this.cobTypeName.Location = new System.Drawing.Point(330, 27);
            this.cobTypeName.Name = "cobTypeName";
            this.cobTypeName.Size = new System.Drawing.Size(108, 27);
            this.cobTypeName.TabIndex = 43;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(484, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 19);
            this.label7.TabIndex = 42;
            this.label7.Text = "ANSUL系统";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(246, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 19);
            this.label6.TabIndex = 42;
            this.label6.Text = "ANSUL预埋";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 19);
            this.label5.TabIndex = 42;
            this.label5.Text = "MARVEL";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(486, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 19);
            this.label10.TabIndex = 42;
            this.label10.Text = "项目电制";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 42;
            this.label4.Text = "项目等级";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(263, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 42;
            this.label2.Text = "项目类型";
            // 
            // txtGeneralRequirementId
            // 
            this.txtGeneralRequirementId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGeneralRequirementId.Location = new System.Drawing.Point(825, 30);
            this.txtGeneralRequirementId.Name = "txtGeneralRequirementId";
            this.txtGeneralRequirementId.ReadOnly = true;
            this.txtGeneralRequirementId.Size = new System.Drawing.Size(97, 25);
            this.txtGeneralRequirementId.TabIndex = 40;
            // 
            // btnGeneralRequirement
            // 
            this.btnGeneralRequirement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGeneralRequirement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnGeneralRequirement.FlatAppearance.BorderSize = 0;
            this.btnGeneralRequirement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeneralRequirement.ForeColor = System.Drawing.Color.White;
            this.btnGeneralRequirement.Location = new System.Drawing.Point(782, 62);
            this.btnGeneralRequirement.Name = "btnGeneralRequirement";
            this.btnGeneralRequirement.Size = new System.Drawing.Size(140, 28);
            this.btnGeneralRequirement.TabIndex = 36;
            this.btnGeneralRequirement.Text = "添加通用技术要求";
            this.btnGeneralRequirement.UseVisualStyleBackColor = false;
            this.btnGeneralRequirement.Click += new System.EventHandler(this.btnGeneralRequirement_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(778, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 19);
            this.label1.TabIndex = 41;
            this.label1.Text = "GRID";
            // 
            // txtODPNo
            // 
            this.txtODPNo.Location = new System.Drawing.Point(341, 27);
            this.txtODPNo.Name = "txtODPNo";
            this.txtODPNo.ReadOnly = true;
            this.txtODPNo.Size = new System.Drawing.Size(108, 25);
            this.txtODPNo.TabIndex = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(274, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 41;
            this.label3.Text = "项目编号";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(497, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 19);
            this.label8.TabIndex = 41;
            this.label8.Text = "项目名称";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProjectName.Location = new System.Drawing.Point(581, 27);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.ReadOnly = true;
            this.txtProjectName.Size = new System.Drawing.Size(358, 25);
            this.txtProjectName.TabIndex = 40;
            // 
            // FrmRequirements
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(950, 568);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.txtODPNo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grbSpecialRequirements);
            this.Controls.Add(this.grbGeneralRequirements);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRequirements";
            this.Text = "通用/特殊技术要求";
            this.grbSpecialRequirements.ResumeLayout(false);
            this.grbSpecialRequirements.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecialRequirements)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.grbGeneralRequirements.ResumeLayout(false);
            this.grbGeneralRequirements.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbSpecialRequirements;
        private System.Windows.Forms.GroupBox grbGeneralRequirements;
        private System.Windows.Forms.TextBox txtODPNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGeneralRequirement;
        private System.Windows.Forms.Button btnSpecialRequirement;
        private System.Windows.Forms.TextBox txtGeneralRequirementId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cobTypeName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cobRiskLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cobANSULSystem;
        private System.Windows.Forms.ComboBox cobANSULPrepipe;
        private System.Windows.Forms.ComboBox cobMARVEL;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.TextBox txtContant;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSpecialRequirementId;
        private System.Windows.Forms.DataGridView dgvSpecialRequirements;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditSpecialRequirement;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteSpecialRequirement;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpecialRequirementId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODPNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Content;
        private System.Windows.Forms.ComboBox cobInputPower;
        private System.Windows.Forms.Label label10;
    }
}