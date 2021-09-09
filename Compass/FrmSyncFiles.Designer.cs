namespace Compass
{
    partial class FrmSyncFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSyncFiles));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lvwLocal = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnPublicToLocal = new System.Windows.Forms.Button();
            this.txtLocalPath = new System.Windows.Forms.TextBox();
            this.txtPublicPath = new System.Windows.Forms.TextBox();
            this.lblLocalCount = new System.Windows.Forms.Label();
            this.lblPublicCount = new System.Windows.Forms.Label();
            this.btnLocal = new System.Windows.Forms.Button();
            this.btnPublic = new System.Windows.Forms.Button();
            this.lvwPublic = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnLocalToPublic = new System.Windows.Forms.Button();
            this.lblNote = new System.Windows.Forms.Label();
            this.cobODPNo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBPONo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Controls.Add(this.lvwLocal, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnPublicToLocal, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtLocalPath, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtPublicPath, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblLocalCount, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblPublicCount, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnLocal, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPublic, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lvwPublic, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnLocalToPublic, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblNote, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1160, 595);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lvwLocal
            // 
            this.lvwLocal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.tableLayoutPanel1.SetColumnSpan(this.lvwLocal, 2);
            this.lvwLocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwLocal.FullRowSelect = true;
            this.lvwLocal.HideSelection = false;
            this.lvwLocal.Location = new System.Drawing.Point(3, 33);
            this.lvwLocal.MultiSelect = false;
            this.lvwLocal.Name = "lvwLocal";
            this.tableLayoutPanel1.SetRowSpan(this.lvwLocal, 3);
            this.lvwLocal.Size = new System.Drawing.Size(539, 538);
            this.lvwLocal.SmallImageList = this.imageList;
            this.lvwLocal.TabIndex = 0;
            this.lvwLocal.UseCompatibleStateImageBehavior = false;
            this.lvwLocal.View = System.Windows.Forms.View.Details;
            this.lvwLocal.DoubleClick += new System.EventHandler(this.Lvw_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "类型";
            this.columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "修改日期";
            this.columnHeader3.Width = 150;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "folder.png");
            // 
            // btnPublicToLocal
            // 
            this.btnPublicToLocal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnPublicToLocal.FlatAppearance.BorderSize = 0;
            this.btnPublicToLocal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPublicToLocal.ForeColor = System.Drawing.Color.White;
            this.btnPublicToLocal.Location = new System.Drawing.Point(548, 33);
            this.btnPublicToLocal.Name = "btnPublicToLocal";
            this.btnPublicToLocal.Size = new System.Drawing.Size(64, 85);
            this.btnPublicToLocal.TabIndex = 45;
            this.btnPublicToLocal.Text = "<<---- 同步到本地盘 <<----";
            this.btnPublicToLocal.UseVisualStyleBackColor = false;
            this.btnPublicToLocal.Click += new System.EventHandler(this.BtnPublicToLocal_Click);
            // 
            // txtLocalPath
            // 
            this.txtLocalPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLocalPath.Location = new System.Drawing.Point(3, 3);
            this.txtLocalPath.Name = "txtLocalPath";
            this.txtLocalPath.ReadOnly = true;
            this.txtLocalPath.Size = new System.Drawing.Size(419, 25);
            this.txtLocalPath.TabIndex = 45;
            // 
            // txtPublicPath
            // 
            this.txtPublicPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPublicPath.Location = new System.Drawing.Point(618, 3);
            this.txtPublicPath.Name = "txtPublicPath";
            this.txtPublicPath.ReadOnly = true;
            this.txtPublicPath.Size = new System.Drawing.Size(419, 25);
            this.txtPublicPath.TabIndex = 45;
            // 
            // lblLocalCount
            // 
            this.lblLocalCount.AutoSize = true;
            this.lblLocalCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLocalCount.Location = new System.Drawing.Point(3, 574);
            this.lblLocalCount.Name = "lblLocalCount";
            this.lblLocalCount.Size = new System.Drawing.Size(419, 21);
            this.lblLocalCount.TabIndex = 46;
            this.lblLocalCount.Text = "0个项目";
            // 
            // lblPublicCount
            // 
            this.lblPublicCount.AutoSize = true;
            this.lblPublicCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPublicCount.Location = new System.Drawing.Point(618, 574);
            this.lblPublicCount.Name = "lblPublicCount";
            this.lblPublicCount.Size = new System.Drawing.Size(419, 21);
            this.lblPublicCount.TabIndex = 47;
            this.lblPublicCount.Text = "0个项目";
            // 
            // btnLocal
            // 
            this.btnLocal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(72)))), ((int)(((byte)(232)))));
            this.btnLocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLocal.FlatAppearance.BorderSize = 0;
            this.btnLocal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocal.ForeColor = System.Drawing.Color.White;
            this.btnLocal.Location = new System.Drawing.Point(428, 3);
            this.btnLocal.Name = "btnLocal";
            this.btnLocal.Size = new System.Drawing.Size(114, 24);
            this.btnLocal.TabIndex = 45;
            this.btnLocal.Text = "打开本地盘";
            this.btnLocal.UseVisualStyleBackColor = false;
            this.btnLocal.Click += new System.EventHandler(this.BtnLocal_Click);
            // 
            // btnPublic
            // 
            this.btnPublic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(72)))), ((int)(((byte)(232)))));
            this.btnPublic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPublic.FlatAppearance.BorderSize = 0;
            this.btnPublic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPublic.ForeColor = System.Drawing.Color.White;
            this.btnPublic.Location = new System.Drawing.Point(1043, 3);
            this.btnPublic.Name = "btnPublic";
            this.btnPublic.Size = new System.Drawing.Size(114, 24);
            this.btnPublic.TabIndex = 48;
            this.btnPublic.Text = "打开公共盘";
            this.btnPublic.UseVisualStyleBackColor = false;
            this.btnPublic.Click += new System.EventHandler(this.BtnPublic_Click);
            // 
            // lvwPublic
            // 
            this.lvwPublic.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.tableLayoutPanel1.SetColumnSpan(this.lvwPublic, 2);
            this.lvwPublic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwPublic.FullRowSelect = true;
            this.lvwPublic.HideSelection = false;
            this.lvwPublic.Location = new System.Drawing.Point(618, 33);
            this.lvwPublic.MultiSelect = false;
            this.lvwPublic.Name = "lvwPublic";
            this.tableLayoutPanel1.SetRowSpan(this.lvwPublic, 3);
            this.lvwPublic.Size = new System.Drawing.Size(539, 538);
            this.lvwPublic.SmallImageList = this.imageList;
            this.lvwPublic.TabIndex = 0;
            this.lvwPublic.UseCompatibleStateImageBehavior = false;
            this.lvwPublic.View = System.Windows.Forms.View.Details;
            this.lvwPublic.DoubleClick += new System.EventHandler(this.Lvw_DoubleClick);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "名称";
            this.columnHeader4.Width = 300;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "类型";
            this.columnHeader5.Width = 70;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "修改日期";
            this.columnHeader6.Width = 150;
            // 
            // btnLocalToPublic
            // 
            this.btnLocalToPublic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLocalToPublic.FlatAppearance.BorderSize = 0;
            this.btnLocalToPublic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocalToPublic.ForeColor = System.Drawing.Color.White;
            this.btnLocalToPublic.Location = new System.Drawing.Point(548, 305);
            this.btnLocalToPublic.Name = "btnLocalToPublic";
            this.btnLocalToPublic.Size = new System.Drawing.Size(64, 85);
            this.btnLocalToPublic.TabIndex = 44;
            this.btnLocalToPublic.Text = "---->> 同步到公共盘 ---->>";
            this.btnLocalToPublic.UseVisualStyleBackColor = false;
            this.btnLocalToPublic.Click += new System.EventHandler(this.BtnLocalToPublic_Click);
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.ForeColor = System.Drawing.Color.Red;
            this.lblNote.Location = new System.Drawing.Point(548, 400);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(61, 114);
            this.lblNote.TabIndex = 49;
            this.lblNote.Text = "请注意同步是拷贝并覆盖， 请保证同步的是最新版本";
            // 
            // cobODPNo
            // 
            this.cobODPNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobODPNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobODPNo.FormattingEnabled = true;
            this.cobODPNo.Location = new System.Drawing.Point(455, 24);
            this.cobODPNo.Name = "cobODPNo";
            this.cobODPNo.Size = new System.Drawing.Size(108, 27);
            this.cobODPNo.TabIndex = 50;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(751, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 47;
            this.label5.Text = "项目名称";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(818, 25);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.ReadOnly = true;
            this.txtProjectName.Size = new System.Drawing.Size(362, 25);
            this.txtProjectName.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(568, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 48;
            this.label2.Text = "大工单号";
            // 
            // txtBPONo
            // 
            this.txtBPONo.Location = new System.Drawing.Point(638, 25);
            this.txtBPONo.Name = "txtBPONo";
            this.txtBPONo.ReadOnly = true;
            this.txtBPONo.Size = new System.Drawing.Size(108, 25);
            this.txtBPONo.TabIndex = 46;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(390, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 49;
            this.label3.Text = "项目编号";
            // 
            // FrmSyncFiles
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.cobODPNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBPONo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmSyncFiles";
            this.Text = "同步文件(本地盘<->公共盘)";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView lvwLocal;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnPublicToLocal;
        private System.Windows.Forms.Button btnLocalToPublic;
        private System.Windows.Forms.ComboBox cobODPNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBPONo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLocalPath;
        private System.Windows.Forms.TextBox txtPublicPath;
        private System.Windows.Forms.Label lblLocalCount;
        private System.Windows.Forms.Label lblPublicCount;
        private System.Windows.Forms.Button btnLocal;
        private System.Windows.Forms.Button btnPublic;
        private System.Windows.Forms.ListView lvwPublic;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label lblNote;
    }
}