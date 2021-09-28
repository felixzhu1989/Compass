namespace Compass
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
            this.btnLabelImage = new System.Windows.Forms.Button();
            this.btnModelImage = new System.Windows.Forms.Button();
            this.pbModelImage = new System.Windows.Forms.PictureBox();
            this.tpgModel3D = new System.Windows.Forms.TabPage();
            this.btnCaptureMeasurement = new System.Windows.Forms.Button();
            this.txtMeasurements = new System.Windows.Forms.TextBox();
            this.btnOpenSolidWorks = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.btnOpeneDrawing = new System.Windows.Forms.Button();
            this.btnPublic = new System.Windows.Forms.Button();
            this.btnLocal = new System.Windows.Forms.Button();
            this.ctrlEDrw = new Compass.EDrawingsUserControl();
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
            this.tbcModelView.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcModelView.Location = new System.Drawing.Point(0, 0);
            this.tbcModelView.Name = "tbcModelView";
            this.tbcModelView.SelectedIndex = 0;
            this.tbcModelView.Size = new System.Drawing.Size(750, 445);
            this.tbcModelView.TabIndex = 0;
            // 
            // tpgModelImage
            // 
            this.tpgModelImage.Controls.Add(this.btnLabelImage);
            this.tpgModelImage.Controls.Add(this.btnModelImage);
            this.tpgModelImage.Controls.Add(this.pbModelImage);
            this.tpgModelImage.Location = new System.Drawing.Point(4, 28);
            this.tpgModelImage.Name = "tpgModelImage";
            this.tpgModelImage.Padding = new System.Windows.Forms.Padding(3);
            this.tpgModelImage.Size = new System.Drawing.Size(742, 413);
            this.tpgModelImage.TabIndex = 0;
            this.tpgModelImage.Text = "2D截图";
            this.tpgModelImage.UseVisualStyleBackColor = true;
            // 
            // btnLabelImage
            // 
            this.btnLabelImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLabelImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLabelImage.FlatAppearance.BorderSize = 0;
            this.btnLabelImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLabelImage.ForeColor = System.Drawing.Color.White;
            this.btnLabelImage.Location = new System.Drawing.Point(663, 69);
            this.btnLabelImage.Name = "btnLabelImage";
            this.btnLabelImage.Size = new System.Drawing.Size(70, 28);
            this.btnLabelImage.TabIndex = 37;
            this.btnLabelImage.Text = "项目截图";
            this.btnLabelImage.UseVisualStyleBackColor = false;
            this.btnLabelImage.Click += new System.EventHandler(this.BtnLabelImage_Click);
            // 
            // btnModelImage
            // 
            this.btnModelImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModelImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnModelImage.FlatAppearance.BorderSize = 0;
            this.btnModelImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModelImage.ForeColor = System.Drawing.Color.White;
            this.btnModelImage.Location = new System.Drawing.Point(663, 35);
            this.btnModelImage.Name = "btnModelImage";
            this.btnModelImage.Size = new System.Drawing.Size(70, 28);
            this.btnModelImage.TabIndex = 38;
            this.btnModelImage.Text = "参数解释";
            this.btnModelImage.UseVisualStyleBackColor = false;
            this.btnModelImage.Click += new System.EventHandler(this.BtnModelImage_Click);
            // 
            // pbModelImage
            // 
            this.pbModelImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbModelImage.Location = new System.Drawing.Point(3, 3);
            this.pbModelImage.Name = "pbModelImage";
            this.pbModelImage.Size = new System.Drawing.Size(736, 407);
            this.pbModelImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModelImage.TabIndex = 1;
            this.pbModelImage.TabStop = false;
            // 
            // tpgModel3D
            // 
            this.tpgModel3D.Controls.Add(this.btnCaptureMeasurement);
            this.tpgModel3D.Controls.Add(this.txtMeasurements);
            this.tpgModel3D.Controls.Add(this.btnOpenSolidWorks);
            this.tpgModel3D.Controls.Add(this.btnOpenFolder);
            this.tpgModel3D.Controls.Add(this.btnOpeneDrawing);
            this.tpgModel3D.Controls.Add(this.btnPublic);
            this.tpgModel3D.Controls.Add(this.btnLocal);
            this.tpgModel3D.Controls.Add(this.ctrlEDrw);
            this.tpgModel3D.Location = new System.Drawing.Point(4, 28);
            this.tpgModel3D.Name = "tpgModel3D";
            this.tpgModel3D.Padding = new System.Windows.Forms.Padding(3);
            this.tpgModel3D.Size = new System.Drawing.Size(742, 413);
            this.tpgModel3D.TabIndex = 1;
            this.tpgModel3D.Text = "3D模型";
            this.tpgModel3D.UseVisualStyleBackColor = true;
            // 
            // btnCaptureMeasurement
            // 
            this.btnCaptureMeasurement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCaptureMeasurement.BackColor = System.Drawing.Color.OliveDrab;
            this.btnCaptureMeasurement.FlatAppearance.BorderSize = 0;
            this.btnCaptureMeasurement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaptureMeasurement.ForeColor = System.Drawing.Color.White;
            this.btnCaptureMeasurement.Location = new System.Drawing.Point(3, 382);
            this.btnCaptureMeasurement.Name = "btnCaptureMeasurement";
            this.btnCaptureMeasurement.Size = new System.Drawing.Size(160, 28);
            this.btnCaptureMeasurement.TabIndex = 38;
            this.btnCaptureMeasurement.Tag = "1";
            this.btnCaptureMeasurement.Text = "记录测量结果";
            this.btnCaptureMeasurement.UseVisualStyleBackColor = false;
            this.btnCaptureMeasurement.Click += new System.EventHandler(this.OnCaptureMeasurement);
            // 
            // txtMeasurements
            // 
            this.txtMeasurements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMeasurements.Location = new System.Drawing.Point(3, 3);
            this.txtMeasurements.Multiline = true;
            this.txtMeasurements.Name = "txtMeasurements";
            this.txtMeasurements.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMeasurements.Size = new System.Drawing.Size(160, 378);
            this.txtMeasurements.TabIndex = 37;
            this.txtMeasurements.Text = "选择线可直接显示结果，如果选择的是面则点击记录测量结果按钮。";
            // 
            // btnOpenSolidWorks
            // 
            this.btnOpenSolidWorks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenSolidWorks.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.btnOpenSolidWorks.Enabled = false;
            this.btnOpenSolidWorks.FlatAppearance.BorderSize = 0;
            this.btnOpenSolidWorks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenSolidWorks.ForeColor = System.Drawing.Color.White;
            this.btnOpenSolidWorks.Location = new System.Drawing.Point(618, 379);
            this.btnOpenSolidWorks.Name = "btnOpenSolidWorks";
            this.btnOpenSolidWorks.Size = new System.Drawing.Size(119, 28);
            this.btnOpenSolidWorks.TabIndex = 35;
            this.btnOpenSolidWorks.Tag = "1";
            this.btnOpenSolidWorks.Text = "打开SolidWorks";
            this.btnOpenSolidWorks.UseVisualStyleBackColor = false;
            this.btnOpenSolidWorks.Click += new System.EventHandler(this.BtnOpenSolidWorks_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFolder.BackColor = System.Drawing.Color.Orange;
            this.btnOpenFolder.Enabled = false;
            this.btnOpenFolder.FlatAppearance.BorderSize = 0;
            this.btnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFolder.ForeColor = System.Drawing.Color.White;
            this.btnOpenFolder.Location = new System.Drawing.Point(618, 103);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(119, 28);
            this.btnOpenFolder.TabIndex = 35;
            this.btnOpenFolder.Tag = "1";
            this.btnOpenFolder.Text = "打开文件夹";
            this.btnOpenFolder.UseVisualStyleBackColor = false;
            this.btnOpenFolder.Click += new System.EventHandler(this.BtnOpenFolder_Click);
            // 
            // btnOpeneDrawing
            // 
            this.btnOpeneDrawing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpeneDrawing.BackColor = System.Drawing.Color.Tomato;
            this.btnOpeneDrawing.Enabled = false;
            this.btnOpeneDrawing.FlatAppearance.BorderSize = 0;
            this.btnOpeneDrawing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpeneDrawing.ForeColor = System.Drawing.Color.White;
            this.btnOpeneDrawing.Location = new System.Drawing.Point(618, 345);
            this.btnOpeneDrawing.Name = "btnOpeneDrawing";
            this.btnOpeneDrawing.Size = new System.Drawing.Size(119, 28);
            this.btnOpeneDrawing.TabIndex = 35;
            this.btnOpeneDrawing.Tag = "1";
            this.btnOpeneDrawing.Text = "打开eDrawings";
            this.btnOpeneDrawing.UseVisualStyleBackColor = false;
            this.btnOpeneDrawing.Visible = false;
            this.btnOpeneDrawing.Click += new System.EventHandler(this.BtnOpeneDrawing_Click);
            // 
            // btnPublic
            // 
            this.btnPublic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPublic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnPublic.FlatAppearance.BorderSize = 0;
            this.btnPublic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPublic.ForeColor = System.Drawing.Color.White;
            this.btnPublic.Location = new System.Drawing.Point(667, 69);
            this.btnPublic.Name = "btnPublic";
            this.btnPublic.Size = new System.Drawing.Size(70, 28);
            this.btnPublic.TabIndex = 35;
            this.btnPublic.Tag = "1";
            this.btnPublic.Text = "公共盘";
            this.btnPublic.UseVisualStyleBackColor = false;
            this.btnPublic.Click += new System.EventHandler(this.OnOpen);
            // 
            // btnLocal
            // 
            this.btnLocal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLocal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnLocal.FlatAppearance.BorderSize = 0;
            this.btnLocal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocal.ForeColor = System.Drawing.Color.White;
            this.btnLocal.Location = new System.Drawing.Point(666, 35);
            this.btnLocal.Name = "btnLocal";
            this.btnLocal.Size = new System.Drawing.Size(71, 28);
            this.btnLocal.TabIndex = 36;
            this.btnLocal.Tag = "0";
            this.btnLocal.Text = "本地盘";
            this.btnLocal.UseVisualStyleBackColor = false;
            this.btnLocal.Click += new System.EventHandler(this.OnOpen);
            // 
            // ctrlEDrw
            // 
            this.ctrlEDrw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlEDrw.Location = new System.Drawing.Point(169, 3);
            this.ctrlEDrw.Name = "ctrlEDrw";
            this.ctrlEDrw.Size = new System.Drawing.Size(570, 407);
            this.ctrlEDrw.TabIndex = 0;
            this.ctrlEDrw.EDrawingsControlLoaded += new System.Action<eDrawings.Interop.EModelViewControl.EModelViewControl>(this.OnControlLoaded);
            // 
            // ModelView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tbcModelView);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ModelView";
            this.Size = new System.Drawing.Size(750, 445);
            this.tbcModelView.ResumeLayout(false);
            this.tpgModelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbModelImage)).EndInit();
            this.tpgModel3D.ResumeLayout(false);
            this.tpgModel3D.PerformLayout();
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
        private System.Windows.Forms.Button btnLabelImage;
        private System.Windows.Forms.Button btnModelImage;
        private System.Windows.Forms.Button btnOpeneDrawing;
        private System.Windows.Forms.Button btnOpenSolidWorks;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnCaptureMeasurement;
        private System.Windows.Forms.TextBox txtMeasurements;
    }
}
