namespace Compass
{
    partial class FrmCeilingAccessories
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
            this.btnCeilingAccessory = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPartDescription = new System.Windows.Forms.TextBox();
            this.txtPartNo = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.dgvCeilingAccessories = new System.Windows.Forms.DataGridView();
            this.CeilingAccessoryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Width = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Height = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Material = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountingRule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditCeilingAccessory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteCeilingAccessory = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCountingRule = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCeilingAccessoryId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cobClassNo = new System.Windows.Forms.ComboBox();
            this.cobUnit = new System.Windows.Forms.ComboBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMaterial = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCeilingAccessories)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCeilingAccessory
            // 
            this.btnCeilingAccessory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnCeilingAccessory.FlatAppearance.BorderSize = 0;
            this.btnCeilingAccessory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCeilingAccessory.ForeColor = System.Drawing.Color.White;
            this.btnCeilingAccessory.Location = new System.Drawing.Point(788, 37);
            this.btnCeilingAccessory.Name = "btnCeilingAccessory";
            this.btnCeilingAccessory.Size = new System.Drawing.Size(150, 28);
            this.btnCeilingAccessory.TabIndex = 11;
            this.btnCeilingAccessory.Text = "添加配件";
            this.btnCeilingAccessory.UseVisualStyleBackColor = false;
            this.btnCeilingAccessory.Click += new System.EventHandler(this.BtnCeilingAccessory_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 27;
            this.label2.Text = "部件描述";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(236, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 19);
            this.label7.TabIndex = 28;
            this.label7.Text = "部件编号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 19);
            this.label5.TabIndex = 29;
            this.label5.Text = "备注";
            // 
            // txtPartDescription
            // 
            this.txtPartDescription.Location = new System.Drawing.Point(80, 39);
            this.txtPartDescription.Name = "txtPartDescription";
            this.txtPartDescription.Size = new System.Drawing.Size(150, 25);
            this.txtPartDescription.TabIndex = 0;
            // 
            // txtPartNo
            // 
            this.txtPartNo.Location = new System.Drawing.Point(304, 39);
            this.txtPartNo.Name = "txtPartNo";
            this.txtPartNo.Size = new System.Drawing.Size(150, 25);
            this.txtPartNo.TabIndex = 1;
            this.txtPartNo.Text = "配件(assembly parts)";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(80, 71);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(150, 25);
            this.txtRemark.TabIndex = 5;
            // 
            // dgvCeilingAccessories
            // 
            this.dgvCeilingAccessories.AllowUserToAddRows = false;
            this.dgvCeilingAccessories.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvCeilingAccessories.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCeilingAccessories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCeilingAccessories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCeilingAccessories.BackgroundColor = System.Drawing.Color.White;
            this.dgvCeilingAccessories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCeilingAccessories.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCeilingAccessories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCeilingAccessories.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CeilingAccessoryId,
            this.ClassNo,
            this.PartDescription,
            this.PartNo,
            this.Unit,
            this.Length,
            this.Width,
            this.Height,
            this.Material,
            this.Remark,
            this.CountingRule});
            this.dgvCeilingAccessories.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvCeilingAccessories.EnableHeadersVisualStyles = false;
            this.dgvCeilingAccessories.Location = new System.Drawing.Point(10, 102);
            this.dgvCeilingAccessories.Name = "dgvCeilingAccessories";
            this.dgvCeilingAccessories.ReadOnly = true;
            this.dgvCeilingAccessories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCeilingAccessories.Size = new System.Drawing.Size(928, 454);
            this.dgvCeilingAccessories.TabIndex = 26;
            this.dgvCeilingAccessories.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvCeilingAccessories_RowPostPaint);
            this.dgvCeilingAccessories.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvCeilingAccessories_KeyDown);
            // 
            // CeilingAccessoryId
            // 
            this.CeilingAccessoryId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CeilingAccessoryId.DataPropertyName = "CeilingAccessoryId";
            this.CeilingAccessoryId.HeaderText = "ID";
            this.CeilingAccessoryId.Name = "CeilingAccessoryId";
            this.CeilingAccessoryId.ReadOnly = true;
            this.CeilingAccessoryId.Width = 48;
            // 
            // ClassNo
            // 
            this.ClassNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ClassNo.DataPropertyName = "ClassNo";
            this.ClassNo.HeaderText = "分类号";
            this.ClassNo.Name = "ClassNo";
            this.ClassNo.ReadOnly = true;
            this.ClassNo.Width = 73;
            // 
            // PartDescription
            // 
            this.PartDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PartDescription.DataPropertyName = "PartDescription";
            this.PartDescription.HeaderText = "部件描述";
            this.PartDescription.Name = "PartDescription";
            this.PartDescription.ReadOnly = true;
            this.PartDescription.Width = 86;
            // 
            // PartNo
            // 
            this.PartNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PartNo.DataPropertyName = "PartNo";
            this.PartNo.HeaderText = "部件编号";
            this.PartNo.Name = "PartNo";
            this.PartNo.ReadOnly = true;
            this.PartNo.Width = 86;
            // 
            // Unit
            // 
            this.Unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Unit.DataPropertyName = "Unit";
            this.Unit.HeaderText = "单位";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 60;
            // 
            // Length
            // 
            this.Length.DataPropertyName = "Length";
            this.Length.HeaderText = "长";
            this.Length.Name = "Length";
            this.Length.ReadOnly = true;
            this.Length.Width = 47;
            // 
            // Width
            // 
            this.Width.DataPropertyName = "Width";
            this.Width.HeaderText = "宽";
            this.Width.Name = "Width";
            this.Width.ReadOnly = true;
            this.Width.Width = 47;
            // 
            // Height
            // 
            this.Height.DataPropertyName = "Height";
            this.Height.HeaderText = "高";
            this.Height.Name = "Height";
            this.Height.ReadOnly = true;
            this.Height.Width = 47;
            // 
            // Material
            // 
            this.Material.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Material.DataPropertyName = "Material";
            this.Material.HeaderText = "材质";
            this.Material.Name = "Material";
            this.Material.ReadOnly = true;
            this.Material.Width = 60;
            // 
            // Remark
            // 
            this.Remark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Remark.DataPropertyName = "Remark";
            this.Remark.HeaderText = "备注";
            this.Remark.Name = "Remark";
            this.Remark.ReadOnly = true;
            this.Remark.Width = 60;
            // 
            // CountingRule
            // 
            this.CountingRule.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CountingRule.DataPropertyName = "CountingRule";
            this.CountingRule.HeaderText = "计数规则";
            this.CountingRule.Name = "CountingRule";
            this.CountingRule.ReadOnly = true;
            this.CountingRule.Width = 86;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditCeilingAccessory,
            this.tsmiDeleteCeilingAccessory});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 48);
            // 
            // tsmiEditCeilingAccessory
            // 
            this.tsmiEditCeilingAccessory.Name = "tsmiEditCeilingAccessory";
            this.tsmiEditCeilingAccessory.Size = new System.Drawing.Size(148, 22);
            this.tsmiEditCeilingAccessory.Text = "修改配件信息";
            this.tsmiEditCeilingAccessory.Click += new System.EventHandler(this.TsmiEditCeilingAccessory_Click);
            // 
            // tsmiDeleteCeilingAccessory
            // 
            this.tsmiDeleteCeilingAccessory.Name = "tsmiDeleteCeilingAccessory";
            this.tsmiDeleteCeilingAccessory.Size = new System.Drawing.Size(148, 22);
            this.tsmiDeleteCeilingAccessory.Text = "删除配件信息";
            this.tsmiDeleteCeilingAccessory.Click += new System.EventHandler(this.TsmiDeleteCeilingAccessory_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 26);
            this.label1.TabIndex = 24;
            this.label1.Text = "天花烟罩发货清单配件管理";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(466, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 19);
            this.label3.TabIndex = 29;
            this.label3.Text = "单位";
            // 
            // txtCountingRule
            // 
            this.txtCountingRule.Location = new System.Drawing.Point(304, 71);
            this.txtCountingRule.Name = "txtCountingRule";
            this.txtCountingRule.Size = new System.Drawing.Size(150, 25);
            this.txtCountingRule.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(236, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 29;
            this.label4.Text = "计数规则";
            // 
            // txtCeilingAccessoryId
            // 
            this.txtCeilingAccessoryId.Location = new System.Drawing.Point(616, 39);
            this.txtCeilingAccessoryId.Name = "txtCeilingAccessoryId";
            this.txtCeilingAccessoryId.Size = new System.Drawing.Size(56, 25);
            this.txtCeilingAccessoryId.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(589, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 19);
            this.label6.TabIndex = 29;
            this.label6.Text = "ID";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(677, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 19);
            this.label8.TabIndex = 29;
            this.label8.Text = "分类号";
            // 
            // cobClassNo
            // 
            this.cobClassNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cobClassNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobClassNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobClassNo.FormattingEnabled = true;
            this.cobClassNo.Location = new System.Drawing.Point(726, 38);
            this.cobClassNo.Name = "cobClassNo";
            this.cobClassNo.Size = new System.Drawing.Size(55, 27);
            this.cobClassNo.TabIndex = 4;
            // 
            // cobUnit
            // 
            this.cobUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cobUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobUnit.FormattingEnabled = true;
            this.cobUnit.Location = new System.Drawing.Point(507, 38);
            this.cobUnit.Name = "cobUnit";
            this.cobUnit.Size = new System.Drawing.Size(76, 27);
            this.cobUnit.TabIndex = 2;
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(507, 71);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(76, 25);
            this.txtLength.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(466, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 19);
            this.label9.TabIndex = 29;
            this.label9.Text = "长";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(616, 71);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(56, 25);
            this.txtWidth.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(589, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 19);
            this.label10.TabIndex = 29;
            this.label10.Text = "宽";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(725, 71);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(56, 25);
            this.txtHeight.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(683, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(22, 19);
            this.label11.TabIndex = 29;
            this.label11.Text = "高";
            // 
            // txtMaterial
            // 
            this.txtMaterial.Location = new System.Drawing.Point(825, 71);
            this.txtMaterial.Name = "txtMaterial";
            this.txtMaterial.Size = new System.Drawing.Size(113, 25);
            this.txtMaterial.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(784, 74);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 19);
            this.label12.TabIndex = 29;
            this.label12.Text = "材质";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(251, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(543, 19);
            this.label13.TabIndex = 29;
            this.label13.Text = "ID开头数字规则:0电/1灯/2风机/3型材/4油网/5螺丝/6控制/7水洗ANSUL/8折弯件/9焊接件";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label14.Location = new System.Drawing.Point(251, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(634, 19);
            this.label14.TabIndex = 29;
            this.label14.Text = "分类号规则：0日本不要配件，1日本特有配件，2适用于所有项目的配件，3自制件不打标签，4自制件打标签";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.ForestGreen;
            this.label15.Location = new System.Drawing.Point(821, 4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(120, 19);
            this.label15.TabIndex = 29;
            this.label15.Text = "材质：SUS304/AL";
            // 
            // FrmCeilingAccessories
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 568);
            this.Controls.Add(this.txtMaterial);
            this.Controls.Add(this.cobUnit);
            this.Controls.Add(this.cobClassNo);
            this.Controls.Add(this.btnCeilingAccessory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPartDescription);
            this.Controls.Add(this.txtPartNo);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.txtCeilingAccessoryId);
            this.Controls.Add(this.txtCountingRule);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.dgvCeilingAccessories);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCeilingAccessories";
            this.Text = "FrmCeilingAccessories";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCeilingAccessories)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCeilingAccessory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPartDescription;
        private System.Windows.Forms.TextBox txtPartNo;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.DataGridView dgvCeilingAccessories;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCountingRule;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCeilingAccessoryId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cobClassNo;
        private System.Windows.Forms.ComboBox cobUnit;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMaterial;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditCeilingAccessory;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteCeilingAccessory;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridViewTextBoxColumn CeilingAccessoryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length;
        private new System.Windows.Forms.DataGridViewTextBoxColumn Width;
        private new System.Windows.Forms.DataGridViewTextBoxColumn Height;
        private System.Windows.Forms.DataGridViewTextBoxColumn Material;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountingRule;
    }
}