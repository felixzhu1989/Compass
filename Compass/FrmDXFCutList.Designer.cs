namespace Compass
{
    partial class FrmDXFCutList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblDesc = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDXFCutlist = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cobCategoryId = new System.Windows.Forms.ComboBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.txtPartNo = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtPartDescription = new System.Windows.Forms.TextBox();
            this.dgvDXFCutList = new System.Windows.Forms.DataGridView();
            this.CutListId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Width = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Thickness = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Materials = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditDXFCutList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteDXFCutList = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveToDB = new System.Windows.Forms.Button();
            this.btnImportFromExcel = new System.Windows.Forms.Button();
            this.txtThickness = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvDXFCutListFromExcel = new System.Windows.Forms.DataGridView();
            this.CategoryId2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartDescription2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Length2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Width2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Thickness2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Materials2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtMaterials = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDXFCutList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDXFCutListFromExcel)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblDesc.Location = new System.Drawing.Point(71, 27);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(35, 19);
            this.lblDesc.TabIndex = 36;
            this.lblDesc.Text = "描述";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(304, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 19);
            this.label7.TabIndex = 31;
            this.label7.Text = "长";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(721, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 19);
            this.label9.TabIndex = 32;
            this.label9.Text = "零件号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(376, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 19);
            this.label5.TabIndex = 33;
            this.label5.Text = "宽";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 19);
            this.label3.TabIndex = 34;
            this.label3.Text = "描述";
            // 
            // btnDXFCutlist
            // 
            this.btnDXFCutlist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDXFCutlist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnDXFCutlist.FlatAppearance.BorderSize = 0;
            this.btnDXFCutlist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDXFCutlist.ForeColor = System.Drawing.Color.White;
            this.btnDXFCutlist.Location = new System.Drawing.Point(856, 49);
            this.btnDXFCutlist.Name = "btnDXFCutlist";
            this.btnDXFCutlist.Size = new System.Drawing.Size(82, 28);
            this.btnDXFCutlist.TabIndex = 29;
            this.btnDXFCutlist.Text = "添加行";
            this.btnDXFCutlist.UseVisualStyleBackColor = false;
            this.btnDXFCutlist.Click += new System.EventHandler(this.btnDXFCutlist_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 35;
            this.label4.Text = "分类编号";
            // 
            // cobCategoryId
            // 
            this.cobCategoryId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobCategoryId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobCategoryId.FormattingEnabled = true;
            this.cobCategoryId.Location = new System.Drawing.Point(75, 50);
            this.cobCategoryId.Name = "cobCategoryId";
            this.cobCategoryId.Size = new System.Drawing.Size(68, 27);
            this.cobCategoryId.TabIndex = 23;
            this.cobCategoryId.SelectedIndexChanged += new System.EventHandler(this.cobCategoryId_SelectedIndexChanged);
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(330, 51);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(45, 25);
            this.txtLength.TabIndex = 25;
            // 
            // txtPartNo
            // 
            this.txtPartNo.Location = new System.Drawing.Point(773, 51);
            this.txtPartNo.Name = "txtPartNo";
            this.txtPartNo.Size = new System.Drawing.Size(78, 25);
            this.txtPartNo.TabIndex = 28;
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(399, 51);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(45, 25);
            this.txtWidth.TabIndex = 26;
            // 
            // txtPartDescription
            // 
            this.txtPartDescription.Location = new System.Drawing.Point(187, 51);
            this.txtPartDescription.Name = "txtPartDescription";
            this.txtPartDescription.Size = new System.Drawing.Size(114, 25);
            this.txtPartDescription.TabIndex = 27;
            // 
            // dgvDXFCutList
            // 
            this.dgvDXFCutList.AllowUserToAddRows = false;
            this.dgvDXFCutList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Azure;
            this.dgvDXFCutList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDXFCutList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDXFCutList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDXFCutList.BackgroundColor = System.Drawing.Color.White;
            this.dgvDXFCutList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDXFCutList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDXFCutList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDXFCutList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CutListId,
            this.CategoryId,
            this.PartDescription,
            this.Length,
            this.Width,
            this.Thickness,
            this.Quantity,
            this.Materials,
            this.PartNo});
            this.dgvDXFCutList.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvDXFCutList.EnableHeadersVisualStyles = false;
            this.dgvDXFCutList.Location = new System.Drawing.Point(12, 82);
            this.dgvDXFCutList.Name = "dgvDXFCutList";
            this.dgvDXFCutList.ReadOnly = true;
            this.dgvDXFCutList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDXFCutList.Size = new System.Drawing.Size(926, 227);
            this.dgvDXFCutList.TabIndex = 30;
            this.dgvDXFCutList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDXFCutList_CellDoubleClick);
            this.dgvDXFCutList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvDXFCutList_RowPostPaint);
            this.dgvDXFCutList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvDXFCutList_KeyDown);
            // 
            // CutListId
            // 
            this.CutListId.DataPropertyName = "CutListId";
            this.CutListId.HeaderText = "Id";
            this.CutListId.Name = "CutListId";
            this.CutListId.ReadOnly = true;
            this.CutListId.Width = 46;
            // 
            // CategoryId
            // 
            this.CategoryId.DataPropertyName = "CategoryId";
            this.CategoryId.HeaderText = "分类编号";
            this.CategoryId.Name = "CategoryId";
            this.CategoryId.ReadOnly = true;
            this.CategoryId.Width = 86;
            // 
            // PartDescription
            // 
            this.PartDescription.DataPropertyName = "PartDescription";
            this.PartDescription.HeaderText = "部件描述";
            this.PartDescription.Name = "PartDescription";
            this.PartDescription.ReadOnly = true;
            this.PartDescription.Width = 86;
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
            // Thickness
            // 
            this.Thickness.DataPropertyName = "Thickness";
            this.Thickness.HeaderText = "厚度";
            this.Thickness.Name = "Thickness";
            this.Thickness.ReadOnly = true;
            this.Thickness.Width = 60;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "数量";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 60;
            // 
            // Materials
            // 
            this.Materials.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Materials.DataPropertyName = "Materials";
            this.Materials.HeaderText = "材料";
            this.Materials.Name = "Materials";
            this.Materials.ReadOnly = true;
            this.Materials.Width = 60;
            // 
            // PartNo
            // 
            this.PartNo.DataPropertyName = "PartNo";
            this.PartNo.HeaderText = "零件号";
            this.PartNo.Name = "PartNo";
            this.PartNo.ReadOnly = true;
            this.PartNo.Width = 73;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditDXFCutList,
            this.tsmiDeleteDXFCutList});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 48);
            // 
            // tsmiEditDXFCutList
            // 
            this.tsmiEditDXFCutList.Name = "tsmiEditDXFCutList";
            this.tsmiEditDXFCutList.Size = new System.Drawing.Size(112, 22);
            this.tsmiEditDXFCutList.Text = "修改行";
            this.tsmiEditDXFCutList.Click += new System.EventHandler(this.tsmiEditDXFCutList_Click);
            // 
            // tsmiDeleteDXFCutList
            // 
            this.tsmiDeleteDXFCutList.Name = "tsmiDeleteDXFCutList";
            this.tsmiDeleteDXFCutList.Size = new System.Drawing.Size(112, 22);
            this.tsmiDeleteDXFCutList.Text = "删除行";
            this.tsmiDeleteDXFCutList.Click += new System.EventHandler(this.tsmiDeleteDXFCutList_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 26);
            this.label1.TabIndex = 24;
            this.label1.Text = "DXF图CutList模板管理";
            // 
            // btnSaveToDB
            // 
            this.btnSaveToDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveToDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(136)))), ((int)(((byte)(242)))));
            this.btnSaveToDB.FlatAppearance.BorderSize = 0;
            this.btnSaveToDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveToDB.ForeColor = System.Drawing.Color.White;
            this.btnSaveToDB.Location = new System.Drawing.Point(830, 315);
            this.btnSaveToDB.Name = "btnSaveToDB";
            this.btnSaveToDB.Size = new System.Drawing.Size(108, 28);
            this.btnSaveToDB.TabIndex = 40;
            this.btnSaveToDB.Text = "保存到数据库";
            this.btnSaveToDB.UseVisualStyleBackColor = false;
            this.btnSaveToDB.Click += new System.EventHandler(this.btnSaveToDB_Click);
            // 
            // btnImportFromExcel
            // 
            this.btnImportFromExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportFromExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnImportFromExcel.FlatAppearance.BorderSize = 0;
            this.btnImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportFromExcel.ForeColor = System.Drawing.Color.White;
            this.btnImportFromExcel.Location = new System.Drawing.Point(10, 315);
            this.btnImportFromExcel.Name = "btnImportFromExcel";
            this.btnImportFromExcel.Size = new System.Drawing.Size(108, 28);
            this.btnImportFromExcel.TabIndex = 41;
            this.btnImportFromExcel.Text = "从Excel导入";
            this.btnImportFromExcel.UseVisualStyleBackColor = false;
            this.btnImportFromExcel.Click += new System.EventHandler(this.btnImportFromExcel_Click);
            // 
            // txtThickness
            // 
            this.txtThickness.Location = new System.Drawing.Point(482, 51);
            this.txtThickness.Name = "txtThickness";
            this.txtThickness.Size = new System.Drawing.Size(35, 25);
            this.txtThickness.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(444, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 19);
            this.label2.TabIndex = 33;
            this.label2.Text = "厚度";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(556, 51);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(23, 25);
            this.txtQuantity.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(518, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 19);
            this.label6.TabIndex = 33;
            this.label6.Text = "数量";
            // 
            // dgvDXFCutListFromExcel
            // 
            this.dgvDXFCutListFromExcel.AllowUserToAddRows = false;
            this.dgvDXFCutListFromExcel.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvDXFCutListFromExcel.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDXFCutListFromExcel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDXFCutListFromExcel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDXFCutListFromExcel.BackgroundColor = System.Drawing.Color.White;
            this.dgvDXFCutListFromExcel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDXFCutListFromExcel.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDXFCutListFromExcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDXFCutListFromExcel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CategoryId2,
            this.PartDescription2,
            this.Length2,
            this.Width2,
            this.Thickness2,
            this.Quantity2,
            this.Materials2,
            this.PartNo2});
            this.dgvDXFCutListFromExcel.EnableHeadersVisualStyles = false;
            this.dgvDXFCutListFromExcel.Location = new System.Drawing.Point(10, 349);
            this.dgvDXFCutListFromExcel.Name = "dgvDXFCutListFromExcel";
            this.dgvDXFCutListFromExcel.ReadOnly = true;
            this.dgvDXFCutListFromExcel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDXFCutListFromExcel.Size = new System.Drawing.Size(926, 207);
            this.dgvDXFCutListFromExcel.TabIndex = 30;
            this.dgvDXFCutListFromExcel.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvDXFCutListFromExcel_RowPostPaint);
            // 
            // CategoryId2
            // 
            this.CategoryId2.DataPropertyName = "CategoryId";
            this.CategoryId2.HeaderText = "分类编号";
            this.CategoryId2.Name = "CategoryId2";
            this.CategoryId2.ReadOnly = true;
            this.CategoryId2.Width = 86;
            // 
            // PartDescription2
            // 
            this.PartDescription2.DataPropertyName = "PartDescription";
            this.PartDescription2.HeaderText = "部件描述";
            this.PartDescription2.Name = "PartDescription2";
            this.PartDescription2.ReadOnly = true;
            this.PartDescription2.Width = 86;
            // 
            // Length2
            // 
            this.Length2.DataPropertyName = "Length";
            this.Length2.HeaderText = "长";
            this.Length2.Name = "Length2";
            this.Length2.ReadOnly = true;
            this.Length2.Width = 47;
            // 
            // Width2
            // 
            this.Width2.DataPropertyName = "Width";
            this.Width2.HeaderText = "宽";
            this.Width2.Name = "Width2";
            this.Width2.ReadOnly = true;
            this.Width2.Width = 47;
            // 
            // Thickness2
            // 
            this.Thickness2.DataPropertyName = "Thickness";
            this.Thickness2.HeaderText = "厚度";
            this.Thickness2.Name = "Thickness2";
            this.Thickness2.ReadOnly = true;
            this.Thickness2.Width = 60;
            // 
            // Quantity2
            // 
            this.Quantity2.DataPropertyName = "Quantity";
            this.Quantity2.HeaderText = "数量";
            this.Quantity2.Name = "Quantity2";
            this.Quantity2.ReadOnly = true;
            this.Quantity2.Width = 60;
            // 
            // Materials2
            // 
            this.Materials2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Materials2.DataPropertyName = "Materials";
            this.Materials2.HeaderText = "材料";
            this.Materials2.Name = "Materials2";
            this.Materials2.ReadOnly = true;
            this.Materials2.Width = 60;
            // 
            // PartNo2
            // 
            this.PartNo2.DataPropertyName = "PartNo";
            this.PartNo2.HeaderText = "零件号";
            this.PartNo2.Name = "PartNo2";
            this.PartNo2.ReadOnly = true;
            this.PartNo2.Width = 73;
            // 
            // txtMaterials
            // 
            this.txtMaterials.Location = new System.Drawing.Point(623, 50);
            this.txtMaterials.Name = "txtMaterials";
            this.txtMaterials.Size = new System.Drawing.Size(92, 25);
            this.txtMaterials.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(584, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 19);
            this.label8.TabIndex = 32;
            this.label8.Text = "材料";
            // 
            // FrmDXFCutList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 568);
            this.Controls.Add(this.btnSaveToDB);
            this.Controls.Add(this.btnImportFromExcel);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDXFCutlist);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cobCategoryId);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.txtMaterials);
            this.Controls.Add(this.txtThickness);
            this.Controls.Add(this.txtPartNo);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.txtPartDescription);
            this.Controls.Add(this.dgvDXFCutListFromExcel);
            this.Controls.Add(this.dgvDXFCutList);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDXFCutList";
            this.Text = "FrmDXFCutList";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDXFCutList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDXFCutListFromExcel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDXFCutlist;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cobCategoryId;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.TextBox txtPartNo;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtPartDescription;
        private System.Windows.Forms.DataGridView dgvDXFCutList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveToDB;
        private System.Windows.Forms.Button btnImportFromExcel;
        private System.Windows.Forms.TextBox txtThickness;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvDXFCutListFromExcel;
        private System.Windows.Forms.TextBox txtMaterials;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryId2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartDescription2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Width2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Thickness2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Materials2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNo2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditDXFCutList;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteDXFCutList;
        private System.Windows.Forms.DataGridViewTextBoxColumn CutListId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length;
        private System.Windows.Forms.DataGridViewTextBoxColumn Width;
        private System.Windows.Forms.DataGridViewTextBoxColumn Thickness;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Materials;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNo;
    }
}