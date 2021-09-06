﻿namespace Compass
{
    partial class FrmDrawingNumMatrix
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDrawingNumMatrix));
            this.btnCreateDrawingNum = new System.Windows.Forms.Button();
            this.txtDrawingNum = new System.Windows.Forms.TextBox();
            this.txtDrawingDesc = new System.Windows.Forms.TextBox();
            this.cobDrawingType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCommit = new System.Windows.Forms.Button();
            this.pnlTabel = new System.Windows.Forms.Panel();
            this.dgvDrawingNumMatrix = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditDrawingNum = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteDrawingNum = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowModel = new System.Windows.Forms.ToolStripMenuItem();
            this.txtMark = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMeasurements = new System.Windows.Forms.TextBox();
            this.btnCaptureMeasurement = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnImportFromExcel = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpgImage = new System.Windows.Forms.TabPage();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.tpgEDrawing = new System.Windows.Forms.TabPage();
            this.ctrlEDrw = new Compass.EDrawingsUserControl();
            this.btnClearImage = new System.Windows.Forms.Button();
            this.btnChooseImage = new System.Windows.Forms.Button();
            this.btnRefreshImage = new System.Windows.Forms.Button();
            this.pnlTabel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawingNumMatrix)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpgImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.tpgEDrawing.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateDrawingNum
            // 
            this.btnCreateDrawingNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnCreateDrawingNum.FlatAppearance.BorderSize = 0;
            this.btnCreateDrawingNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateDrawingNum.ForeColor = System.Drawing.Color.White;
            this.btnCreateDrawingNum.Location = new System.Drawing.Point(10, 233);
            this.btnCreateDrawingNum.Name = "btnCreateDrawingNum";
            this.btnCreateDrawingNum.Size = new System.Drawing.Size(104, 28);
            this.btnCreateDrawingNum.TabIndex = 38;
            this.btnCreateDrawingNum.Text = "生成图号";
            this.btnCreateDrawingNum.UseVisualStyleBackColor = false;
            this.btnCreateDrawingNum.Click += new System.EventHandler(this.BtnCreateDrawingNum_Click);
            // 
            // txtDrawingNum
            // 
            this.txtDrawingNum.Location = new System.Drawing.Point(118, 235);
            this.txtDrawingNum.Name = "txtDrawingNum";
            this.txtDrawingNum.Size = new System.Drawing.Size(100, 25);
            this.txtDrawingNum.TabIndex = 39;
            this.txtDrawingNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDrawingDesc
            // 
            this.txtDrawingDesc.Location = new System.Drawing.Point(273, 235);
            this.txtDrawingDesc.Name = "txtDrawingDesc";
            this.txtDrawingDesc.Size = new System.Drawing.Size(193, 25);
            this.txtDrawingDesc.TabIndex = 39;
            // 
            // cobDrawingType
            // 
            this.cobDrawingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobDrawingType.FormattingEnabled = true;
            this.cobDrawingType.Location = new System.Drawing.Point(526, 234);
            this.cobDrawingType.Name = "cobDrawingType";
            this.cobDrawingType.Size = new System.Drawing.Size(121, 27);
            this.cobDrawingType.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 19);
            this.label1.TabIndex = 41;
            this.label1.Text = "描述：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(472, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 19);
            this.label2.TabIndex = 41;
            this.label2.Text = "分类：";
            // 
            // btnCommit
            // 
            this.btnCommit.BackColor = System.Drawing.Color.Magenta;
            this.btnCommit.FlatAppearance.BorderSize = 0;
            this.btnCommit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCommit.ForeColor = System.Drawing.Color.White;
            this.btnCommit.Location = new System.Drawing.Point(763, 233);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(104, 28);
            this.btnCommit.TabIndex = 38;
            this.btnCommit.Tag = "0";
            this.btnCommit.Text = "添加图号";
            this.btnCommit.UseVisualStyleBackColor = false;
            this.btnCommit.Click += new System.EventHandler(this.BtnCommit_Click);
            // 
            // pnlTabel
            // 
            this.pnlTabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlTabel.Controls.Add(this.dgvDrawingNumMatrix);
            this.pnlTabel.Location = new System.Drawing.Point(10, 267);
            this.pnlTabel.Name = "pnlTabel";
            this.pnlTabel.Size = new System.Drawing.Size(637, 373);
            this.pnlTabel.TabIndex = 42;
            // 
            // dgvDrawingNumMatrix
            // 
            this.dgvDrawingNumMatrix.AllowUserToAddRows = false;
            this.dgvDrawingNumMatrix.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvDrawingNumMatrix.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDrawingNumMatrix.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDrawingNumMatrix.BackgroundColor = System.Drawing.Color.White;
            this.dgvDrawingNumMatrix.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDrawingNumMatrix.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDrawingNumMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDrawingNumMatrix.ContextMenuStrip = this.contextMenuStrip;
            this.dgvDrawingNumMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDrawingNumMatrix.EnableHeadersVisualStyles = false;
            this.dgvDrawingNumMatrix.Location = new System.Drawing.Point(0, 0);
            this.dgvDrawingNumMatrix.Name = "dgvDrawingNumMatrix";
            this.dgvDrawingNumMatrix.ReadOnly = true;
            this.dgvDrawingNumMatrix.RowHeadersWidth = 55;
            this.dgvDrawingNumMatrix.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDrawingNumMatrix.Size = new System.Drawing.Size(637, 373);
            this.dgvDrawingNumMatrix.TabIndex = 1;
            this.dgvDrawingNumMatrix.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvProjects_RowPostPaint);
            this.dgvDrawingNumMatrix.SelectionChanged += new System.EventHandler(this.DgvDrawingNumMatrix_SelectionChanged);
            this.dgvDrawingNumMatrix.DoubleClick += new System.EventHandler(this.DgvDrawingNumMatrix_DoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditDrawingNum,
            this.tsmiDeleteDrawingNum,
            this.tsmiShowModel});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(125, 70);
            // 
            // tsmiEditDrawingNum
            // 
            this.tsmiEditDrawingNum.Name = "tsmiEditDrawingNum";
            this.tsmiEditDrawingNum.Size = new System.Drawing.Size(124, 22);
            this.tsmiEditDrawingNum.Text = "修改图号";
            this.tsmiEditDrawingNum.Click += new System.EventHandler(this.TsmiEditDrawingNum_Click);
            // 
            // tsmiDeleteDrawingNum
            // 
            this.tsmiDeleteDrawingNum.Name = "tsmiDeleteDrawingNum";
            this.tsmiDeleteDrawingNum.Size = new System.Drawing.Size(124, 22);
            this.tsmiDeleteDrawingNum.Text = "删除图号";
            this.tsmiDeleteDrawingNum.Click += new System.EventHandler(this.TsmiDeleteDrawingNum_Click);
            // 
            // tsmiShowModel
            // 
            this.tsmiShowModel.Name = "tsmiShowModel";
            this.tsmiShowModel.Size = new System.Drawing.Size(124, 22);
            this.tsmiShowModel.Text = "显示模型";
            this.tsmiShowModel.Click += new System.EventHandler(this.TsmiShowModel_Click);
            // 
            // txtMark
            // 
            this.txtMark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMark.Location = new System.Drawing.Point(924, 235);
            this.txtMark.Name = "txtMark";
            this.txtMark.Size = new System.Drawing.Size(266, 25);
            this.txtMark.TabIndex = 39;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(872, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 19);
            this.label3.TabIndex = 41;
            this.label3.Text = "备注：";
            // 
            // txtMeasurements
            // 
            this.txtMeasurements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMeasurements.Location = new System.Drawing.Point(1030, 266);
            this.txtMeasurements.Multiline = true;
            this.txtMeasurements.Name = "txtMeasurements";
            this.txtMeasurements.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMeasurements.Size = new System.Drawing.Size(160, 374);
            this.txtMeasurements.TabIndex = 43;
            this.txtMeasurements.Text = "选择线可直接显示结果，如果选择的是面则点击记录测量结果按钮。";
            // 
            // btnCaptureMeasurement
            // 
            this.btnCaptureMeasurement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCaptureMeasurement.BackColor = System.Drawing.Color.OliveDrab;
            this.btnCaptureMeasurement.FlatAppearance.BorderSize = 0;
            this.btnCaptureMeasurement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaptureMeasurement.ForeColor = System.Drawing.Color.White;
            this.btnCaptureMeasurement.Location = new System.Drawing.Point(1030, 644);
            this.btnCaptureMeasurement.Name = "btnCaptureMeasurement";
            this.btnCaptureMeasurement.Size = new System.Drawing.Size(160, 28);
            this.btnCaptureMeasurement.TabIndex = 44;
            this.btnCaptureMeasurement.Tag = "1";
            this.btnCaptureMeasurement.Text = "记录测量结果";
            this.btnCaptureMeasurement.UseVisualStyleBackColor = false;
            this.btnCaptureMeasurement.Click += new System.EventHandler(this.OnCaptureMeasurement);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFolder.BackColor = System.Drawing.Color.Orange;
            this.btnOpenFolder.Enabled = false;
            this.btnOpenFolder.FlatAppearance.BorderSize = 0;
            this.btnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFolder.ForeColor = System.Drawing.Color.White;
            this.btnOpenFolder.Location = new System.Drawing.Point(859, 644);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(88, 28);
            this.btnOpenFolder.TabIndex = 45;
            this.btnOpenFolder.Tag = "1";
            this.btnOpenFolder.Text = "打开文件夹";
            this.btnOpenFolder.UseVisualStyleBackColor = false;
            this.btnOpenFolder.Click += new System.EventHandler(this.BtnOpenFolder_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnOpen.FlatAppearance.BorderSize = 0;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.ForeColor = System.Drawing.Color.White;
            this.btnOpen.Location = new System.Drawing.Point(953, 644);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(71, 28);
            this.btnOpen.TabIndex = 46;
            this.btnOpen.Tag = "0";
            this.btnOpen.Text = "显示模型";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // btnImportFromExcel
            // 
            this.btnImportFromExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportFromExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnImportFromExcel.Enabled = false;
            this.btnImportFromExcel.FlatAppearance.BorderSize = 0;
            this.btnImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportFromExcel.ForeColor = System.Drawing.Color.White;
            this.btnImportFromExcel.Location = new System.Drawing.Point(9, 644);
            this.btnImportFromExcel.Name = "btnImportFromExcel";
            this.btnImportFromExcel.Size = new System.Drawing.Size(105, 28);
            this.btnImportFromExcel.TabIndex = 45;
            this.btnImportFromExcel.Tag = "1";
            this.btnImportFromExcel.Text = "打开Excel文件";
            this.btnImportFromExcel.UseVisualStyleBackColor = false;
            this.btnImportFromExcel.Click += new System.EventHandler(this.BtnImportFromExcel_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.BackColor = System.Drawing.Color.Magenta;
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.Location = new System.Drawing.Point(120, 644);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(131, 28);
            this.btnImport.TabIndex = 45;
            this.btnImport.Tag = "1";
            this.btnImport.Text = "添加入库/显示全部";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnQuery.FlatAppearance.BorderSize = 0;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(653, 233);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(104, 28);
            this.btnQuery.TabIndex = 38;
            this.btnQuery.Tag = "0";
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tpgImage);
            this.tabControl.Controls.Add(this.tpgEDrawing);
            this.tabControl.Location = new System.Drawing.Point(653, 267);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(371, 373);
            this.tabControl.TabIndex = 47;
            // 
            // tpgImage
            // 
            this.tpgImage.Controls.Add(this.pbImage);
            this.tpgImage.Location = new System.Drawing.Point(4, 28);
            this.tpgImage.Name = "tpgImage";
            this.tpgImage.Padding = new System.Windows.Forms.Padding(3);
            this.tpgImage.Size = new System.Drawing.Size(363, 341);
            this.tpgImage.TabIndex = 0;
            this.tpgImage.Text = "图片";
            this.tpgImage.UseVisualStyleBackColor = true;
            // 
            // pbImage
            // 
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Image = global::Compass.Properties.Resources.NoPic;
            this.pbImage.Location = new System.Drawing.Point(3, 3);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(357, 335);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // tpgEDrawing
            // 
            this.tpgEDrawing.Controls.Add(this.ctrlEDrw);
            this.tpgEDrawing.Location = new System.Drawing.Point(4, 28);
            this.tpgEDrawing.Name = "tpgEDrawing";
            this.tpgEDrawing.Padding = new System.Windows.Forms.Padding(3);
            this.tpgEDrawing.Size = new System.Drawing.Size(363, 341);
            this.tpgEDrawing.TabIndex = 1;
            this.tpgEDrawing.Text = "模型";
            this.tpgEDrawing.UseVisualStyleBackColor = true;
            // 
            // ctrlEDrw
            // 
            this.ctrlEDrw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ctrlEDrw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlEDrw.Location = new System.Drawing.Point(3, 3);
            this.ctrlEDrw.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlEDrw.Name = "ctrlEDrw";
            this.ctrlEDrw.Size = new System.Drawing.Size(357, 335);
            this.ctrlEDrw.TabIndex = 1;
            this.ctrlEDrw.EDrawingsControlLoaded += new System.Action<eDrawings.Interop.EModelViewControl.EModelViewControl>(this.OnControlLoaded);
            // 
            // btnClearImage
            // 
            this.btnClearImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnClearImage.FlatAppearance.BorderSize = 0;
            this.btnClearImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearImage.ForeColor = System.Drawing.Color.White;
            this.btnClearImage.Location = new System.Drawing.Point(710, 644);
            this.btnClearImage.Name = "btnClearImage";
            this.btnClearImage.Size = new System.Drawing.Size(44, 28);
            this.btnClearImage.TabIndex = 48;
            this.btnClearImage.Text = "清除";
            this.btnClearImage.UseVisualStyleBackColor = false;
            this.btnClearImage.Click += new System.EventHandler(this.BtnClearImage_Click);
            // 
            // btnChooseImage
            // 
            this.btnChooseImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChooseImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnChooseImage.FlatAppearance.BorderSize = 0;
            this.btnChooseImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseImage.ForeColor = System.Drawing.Color.White;
            this.btnChooseImage.Location = new System.Drawing.Point(657, 644);
            this.btnChooseImage.Name = "btnChooseImage";
            this.btnChooseImage.Size = new System.Drawing.Size(47, 28);
            this.btnChooseImage.TabIndex = 49;
            this.btnChooseImage.Text = "浏览";
            this.btnChooseImage.UseVisualStyleBackColor = false;
            this.btnChooseImage.Click += new System.EventHandler(this.BtnChooseImage_Click);
            // 
            // btnRefreshImage
            // 
            this.btnRefreshImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefreshImage.BackColor = System.Drawing.Color.Magenta;
            this.btnRefreshImage.FlatAppearance.BorderSize = 0;
            this.btnRefreshImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshImage.ForeColor = System.Drawing.Color.White;
            this.btnRefreshImage.Location = new System.Drawing.Point(760, 644);
            this.btnRefreshImage.Name = "btnRefreshImage";
            this.btnRefreshImage.Size = new System.Drawing.Size(82, 28);
            this.btnRefreshImage.TabIndex = 38;
            this.btnRefreshImage.Tag = "0";
            this.btnRefreshImage.Text = "更新图片";
            this.btnRefreshImage.UseVisualStyleBackColor = false;
            this.btnRefreshImage.Click += new System.EventHandler(this.BtnRefreshImage_Click);
            // 
            // FrmDrawingNumMatrix
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.btnClearImage);
            this.Controls.Add(this.btnChooseImage);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnImportFromExcel);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnCaptureMeasurement);
            this.Controls.Add(this.txtMeasurements);
            this.Controls.Add(this.pnlTabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMark);
            this.Controls.Add(this.cobDrawingType);
            this.Controls.Add(this.txtDrawingDesc);
            this.Controls.Add(this.txtDrawingNum);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnRefreshImage);
            this.Controls.Add(this.btnCommit);
            this.Controls.Add(this.btnCreateDrawingNum);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDrawingNumMatrix";
            this.Text = "图号生成器";
            this.pnlTabel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawingNumMatrix)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tpgImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.tpgEDrawing.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateDrawingNum;
        private System.Windows.Forms.TextBox txtDrawingNum;
        private System.Windows.Forms.TextBox txtDrawingDesc;
        private System.Windows.Forms.ComboBox cobDrawingType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCommit;
        private System.Windows.Forms.Panel pnlTabel;
        private System.Windows.Forms.DataGridView dgvDrawingNumMatrix;
        private System.Windows.Forms.TextBox txtMark;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMeasurements;
        private System.Windows.Forms.Button btnCaptureMeasurement;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnImportFromExcel;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpgImage;
        private System.Windows.Forms.TabPage tpgEDrawing;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button btnClearImage;
        private System.Windows.Forms.Button btnChooseImage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteDrawingNum;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditDrawingNum;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowModel;
        private System.Windows.Forms.Button btnRefreshImage;
        private EDrawingsUserControl ctrlEDrw;
    }
}