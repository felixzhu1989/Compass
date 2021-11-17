namespace Compass
{
    partial class FrmCategories
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCategories));
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCategory = new System.Windows.Forms.DataGridView();
            this.CategoryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cobParentId = new System.Windows.Forms.ComboBox();
            this.txtCategoryId = new System.Windows.Forms.TextBox();
            this.txtSubType = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtCategoryDesc = new System.Windows.Forms.TextBox();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtKMLink = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pbModelImage = new System.Windows.Forms.PictureBox();
            this.btnChooseImage = new System.Windows.Forms.Button();
            this.btnClearImage = new System.Windows.Forms.Button();
            this.lblParentDesc = new System.Windows.Forms.Label();
            this.txtModelPath = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCategoryTree = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbModelImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "模型分类表";
            // 
            // dgvCategory
            // 
            this.dgvCategory.AllowUserToAddRows = false;
            this.dgvCategory.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvCategory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCategory.BackgroundColor = System.Drawing.Color.White;
            this.dgvCategory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCategory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CategoryId,
            this.ParentId,
            this.CategoryName,
            this.CategoryDesc,
            this.Model,
            this.SubType,
            this.ModelPath});
            this.dgvCategory.ContextMenuStrip = this.contextMenuStrip;
            this.dgvCategory.EnableHeadersVisualStyles = false;
            this.dgvCategory.Location = new System.Drawing.Point(12, 198);
            this.dgvCategory.Name = "dgvCategory";
            this.dgvCategory.ReadOnly = true;
            this.dgvCategory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategory.Size = new System.Drawing.Size(926, 358);
            this.dgvCategory.TabIndex = 9;
            this.dgvCategory.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvCategory_RowPostPaint);
            this.dgvCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvCategory_KeyDown);
            // 
            // CategoryId
            // 
            this.CategoryId.DataPropertyName = "CategoryId";
            this.CategoryId.HeaderText = "分类编号";
            this.CategoryId.Name = "CategoryId";
            this.CategoryId.ReadOnly = true;
            this.CategoryId.Width = 90;
            // 
            // ParentId
            // 
            this.ParentId.DataPropertyName = "ParentId";
            this.ParentId.HeaderText = "父类编号";
            this.ParentId.Name = "ParentId";
            this.ParentId.ReadOnly = true;
            this.ParentId.Width = 90;
            // 
            // CategoryName
            // 
            this.CategoryName.DataPropertyName = "CategoryName";
            this.CategoryName.HeaderText = "分类名称";
            this.CategoryName.Name = "CategoryName";
            this.CategoryName.ReadOnly = true;
            this.CategoryName.Width = 130;
            // 
            // CategoryDesc
            // 
            this.CategoryDesc.DataPropertyName = "CategoryDesc";
            this.CategoryDesc.HeaderText = "分类描述";
            this.CategoryDesc.Name = "CategoryDesc";
            this.CategoryDesc.ReadOnly = true;
            this.CategoryDesc.Width = 220;
            // 
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "所属模型";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            this.Model.Width = 90;
            // 
            // SubType
            // 
            this.SubType.DataPropertyName = "SubType";
            this.SubType.HeaderText = "子类型号";
            this.SubType.Name = "SubType";
            this.SubType.ReadOnly = true;
            this.SubType.Width = 90;
            // 
            // ModelPath
            // 
            this.ModelPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ModelPath.DataPropertyName = "ModelPath";
            this.ModelPath.HeaderText = "模型地址";
            this.ModelPath.Name = "ModelPath";
            this.ModelPath.ReadOnly = true;
            this.ModelPath.Width = 86;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditCategory,
            this.tsmiDeleteCategory});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(125, 48);
            // 
            // tsmiEditCategory
            // 
            this.tsmiEditCategory.Name = "tsmiEditCategory";
            this.tsmiEditCategory.Size = new System.Drawing.Size(124, 22);
            this.tsmiEditCategory.Text = "修改分类";
            this.tsmiEditCategory.Click += new System.EventHandler(this.TsmiEditCategory_Click);
            // 
            // tsmiDeleteCategory
            // 
            this.tsmiDeleteCategory.Name = "tsmiDeleteCategory";
            this.tsmiDeleteCategory.Size = new System.Drawing.Size(124, 22);
            this.tsmiDeleteCategory.Text = "删除分类";
            this.tsmiDeleteCategory.Click += new System.EventHandler(this.TsmiDeleteCategory_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "分类编号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 14;
            this.label5.Text = "所属模型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(255, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 15;
            this.label3.Text = "分类描述";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 19);
            this.label6.TabIndex = 16;
            this.label6.Text = "子类型号";
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAddCategory.FlatAppearance.BorderSize = 0;
            this.btnAddCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCategory.ForeColor = System.Drawing.Color.White;
            this.btnAddCategory.Location = new System.Drawing.Point(788, 161);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(150, 28);
            this.btnAddCategory.TabIndex = 9;
            this.btnAddCategory.Text = "添加分类";
            this.btnAddCategory.UseVisualStyleBackColor = false;
            this.btnAddCategory.Click += new System.EventHandler(this.BtnAddCategory_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(256, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 17;
            this.label4.Text = "父类编号";
            // 
            // cobParentId
            // 
            this.cobParentId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobParentId.FormattingEnabled = true;
            this.cobParentId.Location = new System.Drawing.Point(324, 48);
            this.cobParentId.Name = "cobParentId";
            this.cobParentId.Size = new System.Drawing.Size(150, 27);
            this.cobParentId.TabIndex = 0;
            // 
            // txtCategoryId
            // 
            this.txtCategoryId.Location = new System.Drawing.Point(99, 48);
            this.txtCategoryId.Name = "txtCategoryId";
            this.txtCategoryId.Size = new System.Drawing.Size(150, 25);
            this.txtCategoryId.TabIndex = 1;
            // 
            // txtSubType
            // 
            this.txtSubType.Location = new System.Drawing.Point(99, 163);
            this.txtSubType.Name = "txtSubType";
            this.txtSubType.Size = new System.Drawing.Size(150, 25);
            this.txtSubType.TabIndex = 4;
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(99, 126);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(150, 25);
            this.txtModel.TabIndex = 3;
            // 
            // txtCategoryDesc
            // 
            this.txtCategoryDesc.Location = new System.Drawing.Point(323, 90);
            this.txtCategoryDesc.Name = "txtCategoryDesc";
            this.txtCategoryDesc.Size = new System.Drawing.Size(243, 25);
            this.txtCategoryDesc.TabIndex = 5;
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(99, 90);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(150, 25);
            this.txtCategoryName.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 19);
            this.label7.TabIndex = 14;
            this.label7.Text = "分类名称";
            // 
            // txtKMLink
            // 
            this.txtKMLink.Location = new System.Drawing.Point(323, 163);
            this.txtKMLink.Name = "txtKMLink";
            this.txtKMLink.Size = new System.Drawing.Size(243, 25);
            this.txtKMLink.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(255, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 19);
            this.label8.TabIndex = 16;
            this.label8.Text = "帮助链接";
            // 
            // pbModelImage
            // 
            this.pbModelImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbModelImage.Image = ((System.Drawing.Image)(resources.GetObject("pbModelImage.Image")));
            this.pbModelImage.Location = new System.Drawing.Point(572, 48);
            this.pbModelImage.Name = "pbModelImage";
            this.pbModelImage.Size = new System.Drawing.Size(210, 141);
            this.pbModelImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModelImage.TabIndex = 21;
            this.pbModelImage.TabStop = false;
            // 
            // btnChooseImage
            // 
            this.btnChooseImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnChooseImage.FlatAppearance.BorderSize = 0;
            this.btnChooseImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseImage.ForeColor = System.Drawing.Color.White;
            this.btnChooseImage.Location = new System.Drawing.Point(788, 48);
            this.btnChooseImage.Name = "btnChooseImage";
            this.btnChooseImage.Size = new System.Drawing.Size(150, 28);
            this.btnChooseImage.TabIndex = 8;
            this.btnChooseImage.Text = "浏览图片";
            this.btnChooseImage.UseVisualStyleBackColor = false;
            this.btnChooseImage.Click += new System.EventHandler(this.BtnChooseImage_Click);
            // 
            // btnClearImage
            // 
            this.btnClearImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnClearImage.FlatAppearance.BorderSize = 0;
            this.btnClearImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearImage.ForeColor = System.Drawing.Color.White;
            this.btnClearImage.Location = new System.Drawing.Point(788, 83);
            this.btnClearImage.Name = "btnClearImage";
            this.btnClearImage.Size = new System.Drawing.Size(150, 28);
            this.btnClearImage.TabIndex = 8;
            this.btnClearImage.Text = "清除图片";
            this.btnClearImage.UseVisualStyleBackColor = false;
            this.btnClearImage.Click += new System.EventHandler(this.BtnClearImage_Click);
            // 
            // lblParentDesc
            // 
            this.lblParentDesc.AutoSize = true;
            this.lblParentDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblParentDesc.Location = new System.Drawing.Point(324, 29);
            this.lblParentDesc.Name = "lblParentDesc";
            this.lblParentDesc.Size = new System.Drawing.Size(35, 19);
            this.lblParentDesc.TabIndex = 22;
            this.lblParentDesc.Text = "描述";
            // 
            // txtModelPath
            // 
            this.txtModelPath.Location = new System.Drawing.Point(323, 126);
            this.txtModelPath.Name = "txtModelPath";
            this.txtModelPath.Size = new System.Drawing.Size(243, 25);
            this.txtModelPath.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(255, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 19);
            this.label9.TabIndex = 14;
            this.label9.Text = "模型地址";
            // 
            // btnCategoryTree
            // 
            this.btnCategoryTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCategoryTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnCategoryTree.FlatAppearance.BorderSize = 0;
            this.btnCategoryTree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategoryTree.ForeColor = System.Drawing.Color.White;
            this.btnCategoryTree.Location = new System.Drawing.Point(788, 122);
            this.btnCategoryTree.Name = "btnCategoryTree";
            this.btnCategoryTree.Size = new System.Drawing.Size(150, 28);
            this.btnCategoryTree.TabIndex = 8;
            this.btnCategoryTree.Text = "产品目录树";
            this.btnCategoryTree.UseVisualStyleBackColor = false;
            this.btnCategoryTree.Visible = false;
            this.btnCategoryTree.Click += new System.EventHandler(this.BtnCategoryTree_Click);
            // 
            // FrmCategories
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 568);
            this.Controls.Add(this.lblParentDesc);
            this.Controls.Add(this.pbModelImage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCategoryTree);
            this.Controls.Add(this.btnClearImage);
            this.Controls.Add(this.btnChooseImage);
            this.Controls.Add(this.btnAddCategory);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cobParentId);
            this.Controls.Add(this.txtCategoryId);
            this.Controls.Add(this.txtCategoryName);
            this.Controls.Add(this.txtKMLink);
            this.Controls.Add(this.txtSubType);
            this.Controls.Add(this.txtModelPath);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtCategoryDesc);
            this.Controls.Add(this.dgvCategory);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCategories";
            this.Text = "FrmCategories";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbModelImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCategory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cobParentId;
        private System.Windows.Forms.TextBox txtCategoryId;
        private System.Windows.Forms.TextBox txtSubType;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtCategoryDesc;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtKMLink;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pbModelImage;
        private System.Windows.Forms.Button btnChooseImage;
        private System.Windows.Forms.Button btnClearImage;
        private System.Windows.Forms.Label lblParentDesc;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditCategory;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteCategory;
        private System.Windows.Forms.TextBox txtModelPath;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCategoryTree;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelPath;
    }
}