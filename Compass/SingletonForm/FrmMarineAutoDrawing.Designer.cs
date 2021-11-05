namespace Compass
{
    partial class FrmMarineAutoDrawing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMarineAutoDrawing));
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnExportDxf = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSubAll = new System.Windows.Forms.Button();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnSub = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.CategoryName2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODPNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModuleTreeId2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Module = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ODPNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModuleTreeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvWaitingList = new System.Windows.Forms.DataGridView();
            this.cobODPNo = new System.Windows.Forms.ComboBox();
            this.btnExec = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBPONo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Module2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvExecList = new System.Windows.Forms.DataGridView();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaitingList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExecList)).BeginInit();
            this.SuspendLayout();
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(654, 19);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.ReadOnly = true;
            this.txtProjectName.Size = new System.Drawing.Size(378, 25);
            this.txtProjectName.TabIndex = 48;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(230, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(204, 19);
            this.label6.TabIndex = 66;
            this.label6.Text = "请确保这里有数据后执行下列操作";
            // 
            // tsslStatus
            // 
            this.tsslStatus.BackColor = System.Drawing.Color.Transparent;
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(74, 19);
            this.tsslStatus.Text = "准备就绪！";
            // 
            // tspbStatus
            // 
            this.tspbStatus.AutoSize = false;
            this.tspbStatus.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tspbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.tspbStatus.Name = "tspbStatus";
            this.tspbStatus.Size = new System.Drawing.Size(550, 18);
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
            this.statusStrip1.TabIndex = 67;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnExportDxf
            // 
            this.btnExportDxf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnExportDxf.FlatAppearance.BorderSize = 0;
            this.btnExportDxf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportDxf.ForeColor = System.Drawing.Color.White;
            this.btnExportDxf.Location = new System.Drawing.Point(574, 450);
            this.btnExportDxf.Name = "btnExportDxf";
            this.btnExportDxf.Size = new System.Drawing.Size(52, 85);
            this.btnExportDxf.TabIndex = 58;
            this.btnExportDxf.Text = "导出DXF图纸";
            this.btnExportDxf.UseVisualStyleBackColor = false;
            this.btnExportDxf.Click += new System.EventHandler(this.BtnExportDxf_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label4.Location = new System.Drawing.Point(624, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 65;
            this.label4.Text = "执行列表";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label1.Location = new System.Drawing.Point(23, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 64;
            this.label1.Text = "等待列表";
            // 
            // btnSubAll
            // 
            this.btnSubAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnSubAll.FlatAppearance.BorderSize = 0;
            this.btnSubAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubAll.ForeColor = System.Drawing.Color.White;
            this.btnSubAll.Location = new System.Drawing.Point(578, 185);
            this.btnSubAll.Name = "btnSubAll";
            this.btnSubAll.Size = new System.Drawing.Size(43, 28);
            this.btnSubAll.TabIndex = 60;
            this.btnSubAll.Text = "<<";
            this.btnSubAll.UseVisualStyleBackColor = false;
            this.btnSubAll.Click += new System.EventHandler(this.BtnSubAll_Click);
            // 
            // btnAddAll
            // 
            this.btnAddAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAddAll.FlatAppearance.BorderSize = 0;
            this.btnAddAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAll.ForeColor = System.Drawing.Color.White;
            this.btnAddAll.Location = new System.Drawing.Point(578, 151);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(43, 28);
            this.btnAddAll.TabIndex = 57;
            this.btnAddAll.Text = ">>";
            this.btnAddAll.UseVisualStyleBackColor = false;
            this.btnAddAll.Click += new System.EventHandler(this.BtnAddAll_Click);
            // 
            // btnSub
            // 
            this.btnSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnSub.FlatAppearance.BorderSize = 0;
            this.btnSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSub.ForeColor = System.Drawing.Color.White;
            this.btnSub.Location = new System.Drawing.Point(578, 117);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(43, 28);
            this.btnSub.TabIndex = 56;
            this.btnSub.Text = "<";
            this.btnSub.UseVisualStyleBackColor = false;
            this.btnSub.Click += new System.EventHandler(this.BtnSub_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(578, 83);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(43, 28);
            this.btnAdd.TabIndex = 55;
            this.btnAdd.Text = ">";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(588, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 51;
            this.label5.Text = "项目名称";
            // 
            // CategoryName2
            // 
            this.CategoryName2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CategoryName2.DataPropertyName = "CategoryName";
            this.CategoryName2.HeaderText = "模型";
            this.CategoryName2.Name = "CategoryName2";
            this.CategoryName2.ReadOnly = true;
            // 
            // Item2
            // 
            this.Item2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Item2.DataPropertyName = "Item";
            this.Item2.HeaderText = "烟罩编号";
            this.Item2.Name = "Item2";
            this.Item2.ReadOnly = true;
            // 
            // ODPNo2
            // 
            this.ODPNo2.DataPropertyName = "ODPNo";
            this.ODPNo2.HeaderText = "项目编号";
            this.ODPNo2.Name = "ODPNo2";
            this.ODPNo2.ReadOnly = true;
            this.ODPNo2.Width = 120;
            // 
            // ModuleTreeId2
            // 
            this.ModuleTreeId2.DataPropertyName = "ModuleTreeId";
            this.ModuleTreeId2.HeaderText = "ID";
            this.ModuleTreeId2.Name = "ModuleTreeId2";
            this.ModuleTreeId2.ReadOnly = true;
            this.ModuleTreeId2.Width = 50;
            // 
            // CategoryName
            // 
            this.CategoryName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CategoryName.DataPropertyName = "CategoryName";
            this.CategoryName.HeaderText = "模型";
            this.CategoryName.Name = "CategoryName";
            this.CategoryName.ReadOnly = true;
            // 
            // Module
            // 
            this.Module.DataPropertyName = "Module";
            this.Module.HeaderText = "分段";
            this.Module.Name = "Module";
            this.Module.ReadOnly = true;
            this.Module.Width = 65;
            // 
            // Item
            // 
            this.Item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Item.DataPropertyName = "Item";
            this.Item.HeaderText = "烟罩编号";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            // 
            // ODPNo
            // 
            this.ODPNo.DataPropertyName = "ODPNo";
            this.ODPNo.HeaderText = "项目编号";
            this.ODPNo.Name = "ODPNo";
            this.ODPNo.ReadOnly = true;
            this.ODPNo.Width = 120;
            // 
            // ModuleTreeId
            // 
            this.ModuleTreeId.DataPropertyName = "ModuleTreeId";
            this.ModuleTreeId.HeaderText = "ID";
            this.ModuleTreeId.Name = "ModuleTreeId";
            this.ModuleTreeId.ReadOnly = true;
            this.ModuleTreeId.Width = 50;
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.dgvWaitingList.Location = new System.Drawing.Point(22, 83);
            this.dgvWaitingList.Name = "dgvWaitingList";
            this.dgvWaitingList.ReadOnly = true;
            this.dgvWaitingList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWaitingList.Size = new System.Drawing.Size(550, 543);
            this.dgvWaitingList.TabIndex = 62;
            this.dgvWaitingList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvWaitingList_RowPostPaint);
            // 
            // cobODPNo
            // 
            this.cobODPNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobODPNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobODPNo.FormattingEnabled = true;
            this.cobODPNo.Location = new System.Drawing.Point(295, 18);
            this.cobODPNo.Name = "cobODPNo";
            this.cobODPNo.Size = new System.Drawing.Size(108, 27);
            this.cobODPNo.TabIndex = 61;
            this.cobODPNo.SelectedIndexChanged += new System.EventHandler(this.CobODPNo_SelectedIndexChanged);
            // 
            // btnExec
            // 
            this.btnExec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExec.FlatAppearance.BorderSize = 0;
            this.btnExec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExec.ForeColor = System.Drawing.Color.White;
            this.btnExec.Location = new System.Drawing.Point(574, 541);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(52, 85);
            this.btnExec.TabIndex = 54;
            this.btnExec.Text = "开始作图";
            this.btnExec.UseVisualStyleBackColor = false;
            this.btnExec.Click += new System.EventHandler(this.BtnExec_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(408, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 50;
            this.label2.Text = "大工单号";
            // 
            // txtBPONo
            // 
            this.txtBPONo.Location = new System.Drawing.Point(474, 19);
            this.txtBPONo.Name = "txtBPONo";
            this.txtBPONo.ReadOnly = true;
            this.txtBPONo.Size = new System.Drawing.Size(108, 25);
            this.txtBPONo.TabIndex = 49;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 52;
            this.label3.Text = "项目编号";
            // 
            // Module2
            // 
            this.Module2.DataPropertyName = "Module";
            this.Module2.HeaderText = "分段";
            this.Module2.Name = "Module2";
            this.Module2.ReadOnly = true;
            this.Module2.Width = 65;
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
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.dgvExecList.Location = new System.Drawing.Point(628, 83);
            this.dgvExecList.Name = "dgvExecList";
            this.dgvExecList.ReadOnly = true;
            this.dgvExecList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExecList.Size = new System.Drawing.Size(550, 543);
            this.dgvExecList.TabIndex = 63;
            this.dgvExecList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvExecList_RowPostPaint);
            // 
            // FrmMarineAutoDrawing
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnExportDxf);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubAll);
            this.Controls.Add(this.btnAddAll);
            this.Controls.Add(this.btnSub);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvWaitingList);
            this.Controls.Add(this.cobODPNo);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBPONo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvExecList);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1200, 675);
            this.Name = "FrmMarineAutoDrawing";
            this.Text = "Marine自动作图";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaitingList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExecList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.ToolStripProgressBar tspbStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnExportDxf;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSubAll;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Button btnSub;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryName2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODPNo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleTreeId2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Module;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewTextBoxColumn ODPNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleTreeId;
        private System.Windows.Forms.DataGridView dgvWaitingList;
        private System.Windows.Forms.ComboBox cobODPNo;
        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBPONo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Module2;
        private System.Windows.Forms.DataGridView dgvExecList;
    }
}