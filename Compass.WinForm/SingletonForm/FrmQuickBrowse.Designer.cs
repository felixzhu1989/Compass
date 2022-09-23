namespace Compass
{
    partial class FrmQuickBrowse
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvQuickBrowse = new System.Windows.Forms.DataGridView();
            this.dgvCutList = new System.Windows.Forms.DataGridView();
            this.cmsCutlist = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDeleteCutList = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPrintCutList = new System.Windows.Forms.Button();
            this.lblModule = new System.Windows.Forms.Label();
            this.AddCutList = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuickBrowse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCutList)).BeginInit();
            this.cmsCutlist.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblTitle.Location = new System.Drawing.Point(5, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(82, 26);
            this.lblTitle.TabIndex = 25;
            this.lblTitle.Text = "ProJect";
            // 
            // dgvQuickBrowse
            // 
            this.dgvQuickBrowse.AllowUserToAddRows = false;
            this.dgvQuickBrowse.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvQuickBrowse.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvQuickBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvQuickBrowse.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvQuickBrowse.BackgroundColor = System.Drawing.Color.White;
            this.dgvQuickBrowse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQuickBrowse.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvQuickBrowse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuickBrowse.EnableHeadersVisualStyles = false;
            this.dgvQuickBrowse.Location = new System.Drawing.Point(12, 34);
            this.dgvQuickBrowse.Name = "dgvQuickBrowse";
            this.dgvQuickBrowse.ReadOnly = true;
            this.dgvQuickBrowse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQuickBrowse.Size = new System.Drawing.Size(926, 236);
            this.dgvQuickBrowse.TabIndex = 36;
            this.dgvQuickBrowse.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvQuickBrowse_CellDoubleClick);
            this.dgvQuickBrowse.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvQuickBrowse_RowPostPaint);
            // 
            // dgvCutList
            // 
            this.dgvCutList.AllowUserToAddRows = false;
            this.dgvCutList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Azure;
            this.dgvCutList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCutList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCutList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCutList.BackgroundColor = System.Drawing.Color.White;
            this.dgvCutList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCutList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCutList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCutList.ContextMenuStrip = this.cmsCutlist;
            this.dgvCutList.EnableHeadersVisualStyles = false;
            this.dgvCutList.Location = new System.Drawing.Point(12, 310);
            this.dgvCutList.Name = "dgvCutList";
            this.dgvCutList.ReadOnly = true;
            this.dgvCutList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCutList.Size = new System.Drawing.Size(926, 246);
            this.dgvCutList.TabIndex = 36;
            this.dgvCutList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvCutList_RowPostPaint);
            this.dgvCutList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvCutList_KeyDown);
            // 
            // cmsCutlist
            // 
            this.cmsCutlist.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDeleteCutList});
            this.cmsCutlist.Name = "cmsCutlist";
            this.cmsCutlist.Size = new System.Drawing.Size(125, 26);
            // 
            // tsmiDeleteCutList
            // 
            this.tsmiDeleteCutList.Name = "tsmiDeleteCutList";
            this.tsmiDeleteCutList.Size = new System.Drawing.Size(124, 22);
            this.tsmiDeleteCutList.Text = "删除多行";
            this.tsmiDeleteCutList.Click += new System.EventHandler(this.TsmiDeleteCutList_Click);
            // 
            // btnPrintCutList
            // 
            this.btnPrintCutList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintCutList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(72)))), ((int)(((byte)(232)))));
            this.btnPrintCutList.FlatAppearance.BorderSize = 0;
            this.btnPrintCutList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintCutList.ForeColor = System.Drawing.Color.White;
            this.btnPrintCutList.Location = new System.Drawing.Point(813, 276);
            this.btnPrintCutList.Name = "btnPrintCutList";
            this.btnPrintCutList.Size = new System.Drawing.Size(125, 28);
            this.btnPrintCutList.TabIndex = 44;
            this.btnPrintCutList.Text = "打印CutList";
            this.btnPrintCutList.UseVisualStyleBackColor = false;
            this.btnPrintCutList.Click += new System.EventHandler(this.BtnPrintCutList_Click);
            // 
            // lblModule
            // 
            this.lblModule.AutoSize = true;
            this.lblModule.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblModule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblModule.Location = new System.Drawing.Point(12, 281);
            this.lblModule.Name = "lblModule";
            this.lblModule.Size = new System.Drawing.Size(86, 26);
            this.lblModule.TabIndex = 25;
            this.lblModule.Text = "Module";
            // 
            // AddCutList
            // 
            this.AddCutList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddCutList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.AddCutList.FlatAppearance.BorderSize = 0;
            this.AddCutList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddCutList.ForeColor = System.Drawing.Color.White;
            this.AddCutList.Location = new System.Drawing.Point(682, 276);
            this.AddCutList.Name = "AddCutList";
            this.AddCutList.Size = new System.Drawing.Size(125, 28);
            this.AddCutList.TabIndex = 44;
            this.AddCutList.Text = "添加CutList";
            this.AddCutList.UseVisualStyleBackColor = false;
            this.AddCutList.Click += new System.EventHandler(this.AddCutList_Click);
            // 
            // FrmQuickBrowse
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 568);
            this.Controls.Add(this.AddCutList);
            this.Controls.Add(this.btnPrintCutList);
            this.Controls.Add(this.dgvCutList);
            this.Controls.Add(this.dgvQuickBrowse);
            this.Controls.Add(this.lblModule);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmQuickBrowse";
            this.Text = "FrmQuickBrowse";
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuickBrowse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCutList)).EndInit();
            this.cmsCutlist.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvQuickBrowse;
        private System.Windows.Forms.DataGridView dgvCutList;
        private System.Windows.Forms.Button btnPrintCutList;
        private System.Windows.Forms.Label lblModule;
        private System.Windows.Forms.ContextMenuStrip cmsCutlist;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteCutList;
        private System.Windows.Forms.Button AddCutList;
    }
}