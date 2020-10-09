namespace Compass
{
    partial class FrmModuleTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmModuleTree));
            this.tvModule = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddModule = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditModule = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteModule = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAutoDrawing = new System.Windows.Forms.Button();
            this.pbLabelImage = new System.Windows.Forms.PictureBox();
            this.tsmiEditPic = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLabelImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tvModule
            // 
            this.tvModule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvModule.BackColor = System.Drawing.Color.Azure;
            this.tvModule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvModule.ContextMenuStrip = this.contextMenuStrip1;
            this.tvModule.ImageIndex = 0;
            this.tvModule.ImageList = this.imageList1;
            this.tvModule.Location = new System.Drawing.Point(0, 0);
            this.tvModule.Name = "tvModule";
            this.tvModule.SelectedImageIndex = 0;
            this.tvModule.Size = new System.Drawing.Size(210, 511);
            this.tvModule.TabIndex = 0;
            this.tvModule.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvModule_AfterCollapse);
            this.tvModule.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvModule_AfterExpand);
            this.tvModule.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvModule_AfterSelect);
            this.tvModule.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvModule_NodeMouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddModule,
            this.tsmiEditModule,
            this.tsmiDeleteModule,
            this.tsmiEditPic});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 114);
            // 
            // tsmiAddModule
            // 
            this.tsmiAddModule.Name = "tsmiAddModule";
            this.tsmiAddModule.Size = new System.Drawing.Size(180, 22);
            this.tsmiAddModule.Text = "添加分段";
            this.tsmiAddModule.Click += new System.EventHandler(this.tsmiAddModule_Click);
            // 
            // tsmiEditModule
            // 
            this.tsmiEditModule.Name = "tsmiEditModule";
            this.tsmiEditModule.Size = new System.Drawing.Size(180, 22);
            this.tsmiEditModule.Text = "修改分段";
            this.tsmiEditModule.Click += new System.EventHandler(this.tsmiEditModule_Click);
            // 
            // tsmiDeleteModule
            // 
            this.tsmiDeleteModule.Name = "tsmiDeleteModule";
            this.tsmiDeleteModule.Size = new System.Drawing.Size(180, 22);
            this.tsmiDeleteModule.Text = "删除分段";
            this.tsmiDeleteModule.Click += new System.EventHandler(this.tsmiDeleteModule_Click);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAutoDrawing);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 517);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(210, 51);
            this.panel2.TabIndex = 2;
            // 
            // btnAutoDrawing
            // 
            this.btnAutoDrawing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAutoDrawing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAutoDrawing.FlatAppearance.BorderSize = 0;
            this.btnAutoDrawing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoDrawing.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoDrawing.ForeColor = System.Drawing.Color.White;
            this.btnAutoDrawing.Location = new System.Drawing.Point(0, 0);
            this.btnAutoDrawing.Name = "btnAutoDrawing";
            this.btnAutoDrawing.Size = new System.Drawing.Size(210, 51);
            this.btnAutoDrawing.TabIndex = 10;
            this.btnAutoDrawing.Text = "自动作图 / 导出DXF图纸 / JobCard / 装箱清单";
            this.btnAutoDrawing.UseVisualStyleBackColor = false;
            this.btnAutoDrawing.Click += new System.EventHandler(this.btnAutoDrawing_Click);
            // 
            // pbLabelImage
            // 
            this.pbLabelImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLabelImage.Location = new System.Drawing.Point(159, 476);
            this.pbLabelImage.Name = "pbLabelImage";
            this.pbLabelImage.Size = new System.Drawing.Size(51, 35);
            this.pbLabelImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLabelImage.TabIndex = 61;
            this.pbLabelImage.TabStop = false;
            // 
            // tsmiEditPic
            // 
            this.tsmiEditPic.Name = "tsmiEditPic";
            this.tsmiEditPic.Size = new System.Drawing.Size(180, 22);
            this.tsmiEditPic.Text = "修改截图";
            this.tsmiEditPic.Click += new System.EventHandler(this.tsmiEditPic_Click);
            // 
            // FrmModuleTree
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(210, 568);
            this.Controls.Add(this.pbLabelImage);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tvModule);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmModuleTree";
            this.Text = "FrmModuleTree";
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLabelImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvModule;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAutoDrawing;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddModule;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditModule;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteModule;
        private System.Windows.Forms.PictureBox pbLabelImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditPic;
    }
}