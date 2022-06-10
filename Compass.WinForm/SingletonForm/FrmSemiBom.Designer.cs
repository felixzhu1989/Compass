namespace Compass
{
    partial class FrmSemiBom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSemiBom));
            this.BtnCreateSemiBom = new System.Windows.Forms.Button();
            this.cobODPNo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBPONo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tspbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvSemiBom = new System.Windows.Forms.DataGridView();
            this.SemiBomId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrawingNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrawingDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProdPriority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsSemiBom = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDeleteSemiBom = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvSemiBomTotal = new System.Windows.Forms.DataGridView();
            this.DrawingNum2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrawingDesc2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProdPriority2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddToTotal = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.txtODPList = new System.Windows.Forms.TextBox();
            this.btnClearTotal = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSemiBom)).BeginInit();
            this.cmsSemiBom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSemiBomTotal)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnCreateSemiBom
            // 
            this.BtnCreateSemiBom.BackColor = System.Drawing.Color.DodgerBlue;
            this.BtnCreateSemiBom.FlatAppearance.BorderSize = 0;
            this.BtnCreateSemiBom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCreateSemiBom.ForeColor = System.Drawing.Color.White;
            this.BtnCreateSemiBom.Location = new System.Drawing.Point(30, 61);
            this.BtnCreateSemiBom.Name = "BtnCreateSemiBom";
            this.BtnCreateSemiBom.Size = new System.Drawing.Size(116, 27);
            this.BtnCreateSemiBom.TabIndex = 65;
            this.BtnCreateSemiBom.Text = "生成半成品清单";
            this.BtnCreateSemiBom.UseVisualStyleBackColor = false;
            this.BtnCreateSemiBom.Click += new System.EventHandler(this.BtnCreateSemiBom_Click);
            // 
            // cobODPNo
            // 
            this.cobODPNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobODPNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobODPNo.FormattingEnabled = true;
            this.cobODPNo.Location = new System.Drawing.Point(225, 28);
            this.cobODPNo.Name = "cobODPNo";
            this.cobODPNo.Size = new System.Drawing.Size(108, 27);
            this.cobODPNo.TabIndex = 63;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(160, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 60;
            this.label5.Text = "项目名称";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(226, 64);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.ReadOnly = true;
            this.txtProjectName.Size = new System.Drawing.Size(417, 25);
            this.txtProjectName.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(338, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 61;
            this.label2.Text = "大工单号";
            // 
            // txtBPONo
            // 
            this.txtBPONo.Location = new System.Drawing.Point(404, 29);
            this.txtBPONo.Name = "txtBPONo";
            this.txtBPONo.ReadOnly = true;
            this.txtBPONo.Size = new System.Drawing.Size(239, 25);
            this.txtBPONo.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 62;
            this.label3.Text = "项目编号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(160, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(204, 19);
            this.label6.TabIndex = 64;
            this.label6.Text = "请确保这里有数据后执行下列操作";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbStatus,
            this.tsslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(27, 620);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1146, 24);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 66;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tspbStatus
            // 
            this.tspbStatus.AutoSize = false;
            this.tspbStatus.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tspbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.tspbStatus.Name = "tspbStatus";
            this.tspbStatus.Size = new System.Drawing.Size(550, 18);
            // 
            // tsslStatus
            // 
            this.tsslStatus.BackColor = System.Drawing.Color.Transparent;
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(74, 19);
            this.tsslStatus.Text = "准备就绪！";
            // 
            // dgvSemiBom
            // 
            this.dgvSemiBom.AllowUserToAddRows = false;
            this.dgvSemiBom.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvSemiBom.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSemiBom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvSemiBom.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSemiBom.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSemiBom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSemiBom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SemiBomId,
            this.DrawingNum,
            this.DrawingDesc,
            this.Quantity,
            this.ProdPriority});
            this.dgvSemiBom.ContextMenuStrip = this.cmsSemiBom;
            this.dgvSemiBom.EnableHeadersVisualStyles = false;
            this.dgvSemiBom.Location = new System.Drawing.Point(30, 98);
            this.dgvSemiBom.Name = "dgvSemiBom";
            this.dgvSemiBom.ReadOnly = true;
            this.dgvSemiBom.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSemiBom.Size = new System.Drawing.Size(613, 511);
            this.dgvSemiBom.TabIndex = 67;
            this.dgvSemiBom.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvWaitingList_RowPostPaint);
            // 
            // SemiBomId
            // 
            this.SemiBomId.DataPropertyName = "SemiBomId";
            this.SemiBomId.HeaderText = "ID";
            this.SemiBomId.Name = "SemiBomId";
            this.SemiBomId.ReadOnly = true;
            this.SemiBomId.Visible = false;
            this.SemiBomId.Width = 50;
            // 
            // DrawingNum
            // 
            this.DrawingNum.DataPropertyName = "DrawingNum";
            this.DrawingNum.HeaderText = "物料号";
            this.DrawingNum.Name = "DrawingNum";
            this.DrawingNum.ReadOnly = true;
            this.DrawingNum.Width = 150;
            // 
            // DrawingDesc
            // 
            this.DrawingDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DrawingDesc.DataPropertyName = "DrawingDesc";
            this.DrawingDesc.HeaderText = "描述";
            this.DrawingDesc.Name = "DrawingDesc";
            this.DrawingDesc.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "数量";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // ProdPriority
            // 
            this.ProdPriority.DataPropertyName = "ProdPriority";
            this.ProdPriority.HeaderText = "优先级";
            this.ProdPriority.Name = "ProdPriority";
            this.ProdPriority.ReadOnly = true;
            // 
            // cmsSemiBom
            // 
            this.cmsSemiBom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDeleteSemiBom});
            this.cmsSemiBom.Name = "cmsCutlist";
            this.cmsSemiBom.Size = new System.Drawing.Size(125, 26);
            // 
            // tsmiDeleteSemiBom
            // 
            this.tsmiDeleteSemiBom.Name = "tsmiDeleteSemiBom";
            this.tsmiDeleteSemiBom.Size = new System.Drawing.Size(124, 22);
            this.tsmiDeleteSemiBom.Text = "删除多行";
            this.tsmiDeleteSemiBom.Click += new System.EventHandler(this.TsmiDeleteSemiBom_Click);
            // 
            // dgvSemiBomTotal
            // 
            this.dgvSemiBomTotal.AllowUserToAddRows = false;
            this.dgvSemiBomTotal.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Azure;
            this.dgvSemiBomTotal.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSemiBomTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSemiBomTotal.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSemiBomTotal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSemiBomTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSemiBomTotal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DrawingNum2,
            this.DrawingDesc2,
            this.Quantity2,
            this.ProdPriority2});
            this.dgvSemiBomTotal.EnableHeadersVisualStyles = false;
            this.dgvSemiBomTotal.Location = new System.Drawing.Point(649, 98);
            this.dgvSemiBomTotal.Name = "dgvSemiBomTotal";
            this.dgvSemiBomTotal.ReadOnly = true;
            this.dgvSemiBomTotal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSemiBomTotal.Size = new System.Drawing.Size(524, 511);
            this.dgvSemiBomTotal.TabIndex = 67;
            this.dgvSemiBomTotal.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvWaitingList_RowPostPaint);
            // 
            // DrawingNum2
            // 
            this.DrawingNum2.DataPropertyName = "DrawingNum";
            this.DrawingNum2.HeaderText = "物料号";
            this.DrawingNum2.Name = "DrawingNum2";
            this.DrawingNum2.ReadOnly = true;
            this.DrawingNum2.Width = 150;
            // 
            // DrawingDesc2
            // 
            this.DrawingDesc2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DrawingDesc2.DataPropertyName = "DrawingDesc";
            this.DrawingDesc2.HeaderText = "描述";
            this.DrawingDesc2.Name = "DrawingDesc2";
            this.DrawingDesc2.ReadOnly = true;
            // 
            // Quantity2
            // 
            this.Quantity2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Quantity2.DataPropertyName = "Quantity";
            this.Quantity2.HeaderText = "数量";
            this.Quantity2.Name = "Quantity2";
            this.Quantity2.ReadOnly = true;
            // 
            // ProdPriority2
            // 
            this.ProdPriority2.DataPropertyName = "ProdPriority";
            this.ProdPriority2.HeaderText = "优先级";
            this.ProdPriority2.Name = "ProdPriority2";
            this.ProdPriority2.ReadOnly = true;
            // 
            // btnAddToTotal
            // 
            this.btnAddToTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnAddToTotal.FlatAppearance.BorderSize = 0;
            this.btnAddToTotal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToTotal.ForeColor = System.Drawing.Color.White;
            this.btnAddToTotal.Location = new System.Drawing.Point(649, 29);
            this.btnAddToTotal.Name = "btnAddToTotal";
            this.btnAddToTotal.Size = new System.Drawing.Size(49, 60);
            this.btnAddToTotal.TabIndex = 65;
            this.btnAddToTotal.Text = "汇总 -->";
            this.btnAddToTotal.UseVisualStyleBackColor = false;
            this.btnAddToTotal.Click += new System.EventHandler(this.btnAddToTotal_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportExcel.BackColor = System.Drawing.Color.Green;
            this.btnExportExcel.FlatAppearance.BorderSize = 0;
            this.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.Location = new System.Drawing.Point(1121, 29);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(49, 60);
            this.btnExportExcel.TabIndex = 65;
            this.btnExportExcel.Text = "导出 Excel";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // txtODPList
            // 
            this.txtODPList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtODPList.Location = new System.Drawing.Point(704, 28);
            this.txtODPList.Multiline = true;
            this.txtODPList.Name = "txtODPList";
            this.txtODPList.ReadOnly = true;
            this.txtODPList.Size = new System.Drawing.Size(355, 61);
            this.txtODPList.TabIndex = 58;
            // 
            // btnClearTotal
            // 
            this.btnClearTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearTotal.BackColor = System.Drawing.Color.Red;
            this.btnClearTotal.FlatAppearance.BorderSize = 0;
            this.btnClearTotal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearTotal.ForeColor = System.Drawing.Color.White;
            this.btnClearTotal.Location = new System.Drawing.Point(1065, 29);
            this.btnClearTotal.Name = "btnClearTotal";
            this.btnClearTotal.Size = new System.Drawing.Size(49, 60);
            this.btnClearTotal.TabIndex = 65;
            this.btnClearTotal.Text = "清空汇总";
            this.btnClearTotal.UseVisualStyleBackColor = false;
            this.btnClearTotal.Click += new System.EventHandler(this.btnClearTotal_Click);
            // 
            // FrmSemiBom
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.dgvSemiBomTotal);
            this.Controls.Add(this.dgvSemiBom);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnClearTotal);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.btnAddToTotal);
            this.Controls.Add(this.BtnCreateSemiBom);
            this.Controls.Add(this.cobODPNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtODPList);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBPONo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmSemiBom";
            this.Padding = new System.Windows.Forms.Padding(27, 95, 27, 31);
            this.Text = "半成品清单";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSemiBom)).EndInit();
            this.cmsSemiBom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSemiBomTotal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnCreateSemiBom;
        private System.Windows.Forms.ComboBox cobODPNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBPONo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar tspbStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.DataGridView dgvSemiBom;
        private System.Windows.Forms.ContextMenuStrip cmsSemiBom;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteSemiBom;
        private System.Windows.Forms.DataGridViewTextBoxColumn SemiBomId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawingNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawingDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProdPriority;
        private System.Windows.Forms.DataGridView dgvSemiBomTotal;
        private System.Windows.Forms.Button btnAddToTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawingNum2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrawingDesc2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProdPriority2;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.TextBox txtODPList;
        private System.Windows.Forms.Button btnClearTotal;
    }
}