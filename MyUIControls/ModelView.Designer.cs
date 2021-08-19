namespace MyUIControls
{
    partial class ModelView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbcModelView = new System.Windows.Forms.TabControl();
            this.tpgModelImage = new System.Windows.Forms.TabPage();
            this.btnItem = new System.Windows.Forms.Button();
            this.btnExplain = new System.Windows.Forms.Button();
            this.pbModelImage = new System.Windows.Forms.PictureBox();
            this.tpgModel3D = new System.Windows.Forms.TabPage();
            this.btnPublic = new System.Windows.Forms.Button();
            this.btnLocal = new System.Windows.Forms.Button();
            this.ctrlEDrw = new MyUIControls.EDrawingsUserControl();
            this.tbcModelView.SuspendLayout();
            this.tpgModelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbModelImage)).BeginInit();
            this.tpgModel3D.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcModelView
            // 
            this.tbcModelView.Controls.Add(this.tpgModelImage);
            this.tbcModelView.Controls.Add(this.tpgModel3D);
            this.tbcModelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcModelView.Location = new System.Drawing.Point(0, 0);
            this.tbcModelView.Name = "tbcModelView";
            this.tbcModelView.SelectedIndex = 0;
            this.tbcModelView.Size = new System.Drawing.Size(750, 445);
            this.tbcModelView.TabIndex = 0;
            // 
            // tpgModelImage
            // 
            this.tpgModelImage.Controls.Add(this.btnItem);
            this.tpgModelImage.Controls.Add(this.btnExplain);
            this.tpgModelImage.Controls.Add(this.pbModelImage);
            this.tpgModelImage.Location = new System.Drawing.Point(4, 22);
            this.tpgModelImage.Name = "tpgModelImage";
            this.tpgModelImage.Padding = new System.Windows.Forms.Padding(3);
            this.tpgModelImage.Size = new System.Drawing.Size(742, 419);
            this.tpgModelImage.TabIndex = 0;
            this.tpgModelImage.Text = "2D截图";
            this.tpgModelImage.UseVisualStyleBackColor = true;
            // 
            // btnItem
            // 
            this.btnItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnItem.FlatAppearance.BorderSize = 0;
            this.btnItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItem.ForeColor = System.Drawing.Color.White;
            this.btnItem.Location = new System.Drawing.Point(83, 6);
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(70, 28);
            this.btnItem.TabIndex = 37;
            this.btnItem.Text = "项目截图";
            this.btnItem.UseVisualStyleBackColor = false;
            // 
            // btnExplain
            // 
            this.btnExplain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExplain.FlatAppearance.BorderSize = 0;
            this.btnExplain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExplain.ForeColor = System.Drawing.Color.White;
            this.btnExplain.Location = new System.Drawing.Point(6, 6);
            this.btnExplain.Name = "btnExplain";
            this.btnExplain.Size = new System.Drawing.Size(70, 28);
            this.btnExplain.TabIndex = 38;
            this.btnExplain.Text = "参数解释";
            this.btnExplain.UseVisualStyleBackColor = false;
            // 
            // pbModelImage
            // 
            this.pbModelImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbModelImage.Image = global::MyUIControls.Properties.Resources.NoPic;
            this.pbModelImage.Location = new System.Drawing.Point(3, 3);
            this.pbModelImage.Name = "pbModelImage";
            this.pbModelImage.Size = new System.Drawing.Size(736, 413);
            this.pbModelImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModelImage.TabIndex = 1;
            this.pbModelImage.TabStop = false;
            // 
            // tpgModel3D
            // 
            this.tpgModel3D.Controls.Add(this.btnPublic);
            this.tpgModel3D.Controls.Add(this.btnLocal);
            this.tpgModel3D.Controls.Add(this.ctrlEDrw);
            this.tpgModel3D.Location = new System.Drawing.Point(4, 22);
            this.tpgModel3D.Name = "tpgModel3D";
            this.tpgModel3D.Padding = new System.Windows.Forms.Padding(3);
            this.tpgModel3D.Size = new System.Drawing.Size(742, 419);
            this.tpgModel3D.TabIndex = 1;
            this.tpgModel3D.Text = "3D模型";
            this.tpgModel3D.UseVisualStyleBackColor = true;
            // 
            // btnPublic
            // 
            this.btnPublic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnPublic.FlatAppearance.BorderSize = 0;
            this.btnPublic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPublic.ForeColor = System.Drawing.Color.White;
            this.btnPublic.Location = new System.Drawing.Point(80, 6);
            this.btnPublic.Name = "btnPublic";
            this.btnPublic.Size = new System.Drawing.Size(70, 28);
            this.btnPublic.TabIndex = 35;
            this.btnPublic.Text = "公共盘";
            this.btnPublic.UseVisualStyleBackColor = false;
            this.btnPublic.Click += new System.EventHandler(this.OnOpen);
            // 
            // btnLocal
            // 
            this.btnLocal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLocal.FlatAppearance.BorderSize = 0;
            this.btnLocal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocal.ForeColor = System.Drawing.Color.White;
            this.btnLocal.Location = new System.Drawing.Point(3, 6);
            this.btnLocal.Name = "btnLocal";
            this.btnLocal.Size = new System.Drawing.Size(70, 28);
            this.btnLocal.TabIndex = 36;
            this.btnLocal.Text = "本地磁盘";
            this.btnLocal.UseVisualStyleBackColor = false;
            this.btnLocal.Click += new System.EventHandler(this.OnOpen);
            // 
            // ctrlEDrw
            // 
            this.ctrlEDrw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlEDrw.Location = new System.Drawing.Point(3, 3);
            this.ctrlEDrw.Name = "ctrlEDrw";
            this.ctrlEDrw.Size = new System.Drawing.Size(736, 413);
            this.ctrlEDrw.TabIndex = 0;
            this.ctrlEDrw.EDrawingsControlLoaded += new System.Action<eDrawings.Interop.EModelViewControl.EModelViewControl>(this.OnControlLoaded);
            // 
            // ModelView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbcModelView);
            this.Name = "ModelView";
            this.Size = new System.Drawing.Size(750, 445);
            this.tbcModelView.ResumeLayout(false);
            this.tpgModelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbModelImage)).EndInit();
            this.tpgModel3D.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcModelView;
        private System.Windows.Forms.TabPage tpgModelImage;
        private System.Windows.Forms.TabPage tpgModel3D;
        private System.Windows.Forms.PictureBox pbModelImage;
        private EDrawingsUserControl ctrlEDrw;
        private System.Windows.Forms.Button btnPublic;
        private System.Windows.Forms.Button btnLocal;
        private System.Windows.Forms.Button btnItem;
        private System.Windows.Forms.Button btnExplain;
    }
}
