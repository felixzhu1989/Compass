namespace Compass
{
    partial class FrmCategoryTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCategoryTree));
            this.tvCategory = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnAddModule = new System.Windows.Forms.Button();
            this.txtModule = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblODPNo = new System.Windows.Forms.Label();
            this.lblItem = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tvCategory
            // 
            this.tvCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvCategory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvCategory.ImageIndex = 0;
            this.tvCategory.ImageList = this.imageList1;
            this.tvCategory.Location = new System.Drawing.Point(23, 63);
            this.tvCategory.Name = "tvCategory";
            this.tvCategory.SelectedImageIndex = 0;
            this.tvCategory.Size = new System.Drawing.Size(454, 503);
            this.tvCategory.TabIndex = 0;
            this.tvCategory.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.TvCategory_AfterCollapse);
            this.tvCategory.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TvCategory_AfterExpand);
            this.tvCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvCategory_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Categories.png");
            this.imageList1.Images.SetKeyName(1, "FolderClosed.png");
            this.imageList1.Images.SetKeyName(2, "FolderOpen.png");
            this.imageList1.Images.SetKeyName(3, "CategoryItem.png");
            this.imageList1.Images.SetKeyName(4, "Project.png");
            // 
            // btnAddModule
            // 
            this.btnAddModule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddModule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAddModule.FlatAppearance.BorderSize = 0;
            this.btnAddModule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddModule.ForeColor = System.Drawing.Color.White;
            this.btnAddModule.Location = new System.Drawing.Point(181, 598);
            this.btnAddModule.Name = "btnAddModule";
            this.btnAddModule.Size = new System.Drawing.Size(296, 28);
            this.btnAddModule.TabIndex = 2;
            this.btnAddModule.Text = "添加：-";
            this.btnAddModule.UseVisualStyleBackColor = false;
            this.btnAddModule.Click += new System.EventHandler(this.BtnAddModule_Click);
            // 
            // txtModule
            // 
            this.txtModule.AcceptsTab = true;
            this.txtModule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtModule.Location = new System.Drawing.Point(75, 600);
            this.txtModule.Name = "txtModule";
            this.txtModule.Size = new System.Drawing.Size(100, 25);
            this.txtModule.TabIndex = 1;
            this.txtModule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtModule_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 603);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "分段号";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(24, 636);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(349, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "烟罩分段号M1,M2,M3...，天花烟罩B1.1，CJ01，SSP01...";
            // 
            // lblODPNo
            // 
            this.lblODPNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblODPNo.AutoSize = true;
            this.lblODPNo.ForeColor = System.Drawing.Color.Red;
            this.lblODPNo.Location = new System.Drawing.Point(24, 573);
            this.lblODPNo.Name = "lblODPNo";
            this.lblODPNo.Size = new System.Drawing.Size(57, 19);
            this.lblODPNo.TabIndex = 11;
            this.lblODPNo.Text = "ODPNo";
            // 
            // lblItem
            // 
            this.lblItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblItem.AutoSize = true;
            this.lblItem.ForeColor = System.Drawing.Color.Red;
            this.lblItem.Location = new System.Drawing.Point(179, 573);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(37, 19);
            this.lblItem.TabIndex = 11;
            this.lblItem.Text = "Item";
            // 
            // FrmCategoryTree
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(500, 675);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblItem);
            this.Controls.Add(this.lblODPNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtModule);
            this.Controls.Add(this.btnAddModule);
            this.Controls.Add(this.tvCategory);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCategoryTree";
            this.Text = "产品目录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvCategory;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnAddModule;
        private System.Windows.Forms.TextBox txtModule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblODPNo;
        private System.Windows.Forms.Label lblItem;
    }
}