namespace Compass
{
    partial class FrmHoodAutoDrawing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHoodAutoDrawing));
            this.cobODPNo = new System.Windows.Forms.ComboBox();
            this.btnExec = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBPONo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvWaitingList = new System.Windows.Forms.DataGridView();
            this.ModuleTreeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODPNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Module = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvExecList = new System.Windows.Forms.DataGridView();
            this.ModuleTreeId2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODPNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Module2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryName2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSub = new System.Windows.Forms.Button();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnSubAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnJobCard = new System.Windows.Forms.Button();
            this.btnExportDxf = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tspbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnHoodPackingList = new System.Windows.Forms.Button();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaitingList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExecList)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cobODPNo
            // 
            this.cobODPNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobODPNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobODPNo.FormattingEnabled = true;
            this.cobODPNo.Location = new System.Drawing.Point(295, 17);
            this.cobODPNo.Name = "cobODPNo";
            this.cobODPNo.Size = new System.Drawing.Size(108, 27);
            this.cobODPNo.TabIndex = 44;
            // 
            // btnExec
            // 
            this.btnExec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExec.FlatAppearance.BorderSize = 0;
            this.btnExec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExec.ForeColor = System.Drawing.Color.White;
            this.btnExec.Location = new System.Drawing.Point(574, 540);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(52, 85);
            this.btnExec.TabIndex = 43;
            this.btnExec.Text = "开始作图";
            this.btnExec.UseVisualStyleBackColor = false;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(408, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 41;
            this.label2.Text = "大工单号";
            // 
            // txtBPONo
            // 
            this.txtBPONo.Location = new System.Drawing.Point(474, 18);
            this.txtBPONo.Name = "txtBPONo";
            this.txtBPONo.ReadOnly = true;
            this.txtBPONo.Size = new System.Drawing.Size(108, 25);
            this.txtBPONo.TabIndex = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 42;
            this.label3.Text = "项目编号";
            // 
            // dgvWaitingList
            // 
            this.dgvWaitingList.AllowUserToAddRows = false;
            this.dgvWaitingList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvWaitingList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvWaitingList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWaitingList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvWaitingList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWaitingList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ModuleTreeId,
            this.ODPNo,
            this.Item,
            this.Module,
            this.CategoryName});
            this.dgvWaitingList.EnableHeadersVisualStyles = false;
            this.dgvWaitingList.Location = new System.Drawing.Point(22, 82);
            this.dgvWaitingList.Name = "dgvWaitingList";
            this.dgvWaitingList.ReadOnly = true;
            this.dgvWaitingList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWaitingList.Size = new System.Drawing.Size(550, 543);
            this.dgvWaitingList.TabIndex = 45;
            this.dgvWaitingList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvWaitingList_RowPostPaint);
            // 
            // ModuleTreeId
            // 
            this.ModuleTreeId.DataPropertyName = "ModuleTreeId";
            this.ModuleTreeId.HeaderText = "ID";
            this.ModuleTreeId.Name = "ModuleTreeId";
            this.ModuleTreeId.ReadOnly = true;
            this.ModuleTreeId.Width = 50;
            // 
            // ODPNo
            // 
            this.ODPNo.DataPropertyName = "ODPNo";
            this.ODPNo.HeaderText = "项目编号";
            this.ODPNo.Name = "ODPNo";
            this.ODPNo.ReadOnly = true;
            this.ODPNo.Width = 120;
            // 
            // Item
            // 
            this.Item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Item.DataPropertyName = "Item";
            this.Item.HeaderText = "烟罩编号";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            // 
            // Module
            // 
            this.Module.DataPropertyName = "Module";
            this.Module.HeaderText = "分段";
            this.Module.Name = "Module";
            this.Module.ReadOnly = true;
            this.Module.Width = 65;
            // 
            // CategoryName
            // 
            this.CategoryName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CategoryName.DataPropertyName = "CategoryName";
            this.CategoryName.HeaderText = "模型";
            this.CategoryName.Name = "CategoryName";
            this.CategoryName.ReadOnly = true;
            // 
            // dgvExecList
            // 
            this.dgvExecList.AllowUserToAddRows = false;
            this.dgvExecList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Azure;
            this.dgvExecList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvExecList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExecList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvExecList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExecList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ModuleTreeId2,
            this.ODPNo2,
            this.Item2,
            this.Module2,
            this.CategoryName2});
            this.dgvExecList.EnableHeadersVisualStyles = false;
            this.dgvExecList.Location = new System.Drawing.Point(628, 82);
            this.dgvExecList.Name = "dgvExecList";
            this.dgvExecList.ReadOnly = true;
            this.dgvExecList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExecList.Size = new System.Drawing.Size(550, 543);
            this.dgvExecList.TabIndex = 45;
            this.dgvExecList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvExecList_RowPostPaint);
            // 
            // ModuleTreeId2
            // 
            this.ModuleTreeId2.DataPropertyName = "ModuleTreeId";
            this.ModuleTreeId2.HeaderText = "ID";
            this.ModuleTreeId2.Name = "ModuleTreeId2";
            this.ModuleTreeId2.ReadOnly = true;
            this.ModuleTreeId2.Width = 50;
            // 
            // ODPNo2
            // 
            this.ODPNo2.DataPropertyName = "ODPNo";
            this.ODPNo2.HeaderText = "项目编号";
            this.ODPNo2.Name = "ODPNo2";
            this.ODPNo2.ReadOnly = true;
            this.ODPNo2.Width = 120;
            // 
            // Item2
            // 
            this.Item2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Item2.DataPropertyName = "Item";
            this.Item2.HeaderText = "烟罩编号";
            this.Item2.Name = "Item2";
            this.Item2.ReadOnly = true;
            // 
            // Module2
            // 
            this.Module2.DataPropertyName = "Module";
            this.Module2.HeaderText = "分段";
            this.Module2.Name = "Module2";
            this.Module2.ReadOnly = true;
            this.Module2.Width = 65;
            // 
            // CategoryName2
            // 
            this.CategoryName2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CategoryName2.DataPropertyName = "CategoryName";
            this.CategoryName2.HeaderText = "模型";
            this.CategoryName2.Name = "CategoryName2";
            this.CategoryName2.ReadOnly = true;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(578, 82);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(43, 28);
            this.btnAdd.TabIndex = 43;
            this.btnAdd.Text = ">";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSub
            // 
            this.btnSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnSub.FlatAppearance.BorderSize = 0;
            this.btnSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSub.ForeColor = System.Drawing.Color.White;
            this.btnSub.Location = new System.Drawing.Point(578, 116);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(43, 28);
            this.btnSub.TabIndex = 43;
            this.btnSub.Text = "<";
            this.btnSub.UseVisualStyleBackColor = false;
            this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
            // 
            // btnAddAll
            // 
            this.btnAddAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAddAll.FlatAppearance.BorderSize = 0;
            this.btnAddAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAll.ForeColor = System.Drawing.Color.White;
            this.btnAddAll.Location = new System.Drawing.Point(578, 150);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(43, 28);
            this.btnAddAll.TabIndex = 43;
            this.btnAddAll.Text = ">>";
            this.btnAddAll.UseVisualStyleBackColor = false;
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // btnSubAll
            // 
            this.btnSubAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnSubAll.FlatAppearance.BorderSize = 0;
            this.btnSubAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubAll.ForeColor = System.Drawing.Color.White;
            this.btnSubAll.Location = new System.Drawing.Point(578, 184);
            this.btnSubAll.Name = "btnSubAll";
            this.btnSubAll.Size = new System.Drawing.Size(43, 28);
            this.btnSubAll.TabIndex = 43;
            this.btnSubAll.Text = "<<";
            this.btnSubAll.UseVisualStyleBackColor = false;
            this.btnSubAll.Click += new System.EventHandler(this.btnSubAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label1.Location = new System.Drawing.Point(23, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 46;
            this.label1.Text = "等待列表";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label4.Location = new System.Drawing.Point(624, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 46;
            this.label4.Text = "执行列表";
            // 
            // btnJobCard
            // 
            this.btnJobCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(72)))), ((int)(((byte)(232)))));
            this.btnJobCard.FlatAppearance.BorderSize = 0;
            this.btnJobCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJobCard.ForeColor = System.Drawing.Color.White;
            this.btnJobCard.Location = new System.Drawing.Point(574, 267);
            this.btnJobCard.Name = "btnJobCard";
            this.btnJobCard.Size = new System.Drawing.Size(52, 85);
            this.btnJobCard.TabIndex = 43;
            this.btnJobCard.Text = "打印 Job Card";
            this.btnJobCard.UseVisualStyleBackColor = false;
            this.btnJobCard.Click += new System.EventHandler(this.btnJobCard_Click);
            // 
            // btnExportDxf
            // 
            this.btnExportDxf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnExportDxf.FlatAppearance.BorderSize = 0;
            this.btnExportDxf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportDxf.ForeColor = System.Drawing.Color.White;
            this.btnExportDxf.Location = new System.Drawing.Point(574, 449);
            this.btnExportDxf.Name = "btnExportDxf";
            this.btnExportDxf.Size = new System.Drawing.Size(52, 85);
            this.btnExportDxf.TabIndex = 43;
            this.btnExportDxf.Text = "导出DXF图纸";
            this.btnExportDxf.UseVisualStyleBackColor = false;
            this.btnExportDxf.Click += new System.EventHandler(this.btnExportDxf_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbStatus,
            this.tsslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(20, 631);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1160, 24);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 47;
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
            // btnHoodPackingList
            // 
            this.btnHoodPackingList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(136)))), ((int)(((byte)(242)))));
            this.btnHoodPackingList.FlatAppearance.BorderSize = 0;
            this.btnHoodPackingList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoodPackingList.ForeColor = System.Drawing.Color.White;
            this.btnHoodPackingList.Location = new System.Drawing.Point(574, 358);
            this.btnHoodPackingList.Name = "btnHoodPackingList";
            this.btnHoodPackingList.Size = new System.Drawing.Size(52, 85);
            this.btnHoodPackingList.TabIndex = 43;
            this.btnHoodPackingList.Text = "导出装箱清单";
            this.btnHoodPackingList.UseVisualStyleBackColor = false;
            this.btnHoodPackingList.Click += new System.EventHandler(this.btnHoodPackingList_Click);
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(654, 18);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.ReadOnly = true;
            this.txtProjectName.Size = new System.Drawing.Size(378, 25);
            this.txtProjectName.TabIndex = 40;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(588, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 41;
            this.label5.Text = "项目名称";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(230, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(204, 19);
            this.label6.TabIndex = 46;
            this.label6.Text = "请确保这里有数据后执行下列操作";
            // 
            // FrmHoodAutoDrawing
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvExecList);
            this.Controls.Add(this.dgvWaitingList);
            this.Controls.Add(this.cobODPNo);
            this.Controls.Add(this.btnSubAll);
            this.Controls.Add(this.btnAddAll);
            this.Controls.Add(this.btnSub);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnHoodPackingList);
            this.Controls.Add(this.btnJobCard);
            this.Controls.Add(this.btnExportDxf);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBPONo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1200, 675);
            this.Name = "FrmHoodAutoDrawing";
            this.Resizable = false;
            this.Text = "标准烟罩自动作图";
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaitingList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExecList)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cobODPNo;
        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBPONo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvWaitingList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleTreeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODPNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn Module;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryName;
        private System.Windows.Forms.DataGridView dgvExecList;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSub;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Button btnSubAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleTreeId2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODPNo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Module2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryName2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button btnJobCard;
        private System.Windows.Forms.Button btnExportDxf;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar tspbStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.Button btnHoodPackingList;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}