namespace Compass
{
    partial class FrmSolidWorksTools
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSolidWorksTools));
            this.txteDrawingsPath = new System.Windows.Forms.TextBox();
            this.btneDrawingsPath = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txteDrawingsPath
            // 
            this.txteDrawingsPath.Location = new System.Drawing.Point(20, 213);
            this.txteDrawingsPath.Name = "txteDrawingsPath";
            this.txteDrawingsPath.Size = new System.Drawing.Size(788, 25);
            this.txteDrawingsPath.TabIndex = 2;
            // 
            // btneDrawingsPath
            // 
            this.btneDrawingsPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btneDrawingsPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btneDrawingsPath.FlatAppearance.BorderSize = 0;
            this.btneDrawingsPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btneDrawingsPath.ForeColor = System.Drawing.Color.White;
            this.btneDrawingsPath.Location = new System.Drawing.Point(850, 211);
            this.btneDrawingsPath.Name = "btneDrawingsPath";
            this.btneDrawingsPath.Size = new System.Drawing.Size(133, 28);
            this.btneDrawingsPath.TabIndex = 3;
            this.btneDrawingsPath.Text = "更新eDrawing路径";
            this.btneDrawingsPath.UseVisualStyleBackColor = false;
            this.btneDrawingsPath.Click += new System.EventHandler(this.BtneDrawingsPath_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(814, 211);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(30, 28);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "...";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // FrmSolidWorksTools
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(998, 271);
            this.Controls.Add(this.txteDrawingsPath);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btneDrawingsPath);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmSolidWorksTools";
            this.Padding = new System.Windows.Forms.Padding(27, 88, 27, 29);
            this.Text = "SolidWorksTools";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txteDrawingsPath;
        private System.Windows.Forms.Button btneDrawingsPath;
        private System.Windows.Forms.Button btnSearch;
    }
}