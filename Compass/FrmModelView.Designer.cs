namespace Compass
{
    partial class FrmModelView
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
            this.ctrlEDrw = new Compass.EDrawingsUserControl();
            this.btnCaptureMeasurement = new System.Windows.Forms.Button();
            this.txtMeasurements = new System.Windows.Forms.TextBox();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.btnPublic = new System.Windows.Forms.Button();
            this.btnLocal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlEDrw
            // 
            this.ctrlEDrw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlEDrw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ctrlEDrw.Location = new System.Drawing.Point(178, 50);
            this.ctrlEDrw.Name = "ctrlEDrw";
            this.ctrlEDrw.Size = new System.Drawing.Size(1013, 591);
            this.ctrlEDrw.TabIndex = 0;
            this.ctrlEDrw.EDrawingsControlLoaded += new System.Action<eDrawings.Interop.EModelViewControl.EModelViewControl>(this.OnControlLoaded);
            // 
            // btnCaptureMeasurement
            // 
            this.btnCaptureMeasurement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCaptureMeasurement.BackColor = System.Drawing.Color.OliveDrab;
            this.btnCaptureMeasurement.FlatAppearance.BorderSize = 0;
            this.btnCaptureMeasurement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaptureMeasurement.ForeColor = System.Drawing.Color.White;
            this.btnCaptureMeasurement.Location = new System.Drawing.Point(12, 642);
            this.btnCaptureMeasurement.Name = "btnCaptureMeasurement";
            this.btnCaptureMeasurement.Size = new System.Drawing.Size(160, 28);
            this.btnCaptureMeasurement.TabIndex = 40;
            this.btnCaptureMeasurement.Tag = "1";
            this.btnCaptureMeasurement.Text = "记录测量结果";
            this.btnCaptureMeasurement.UseVisualStyleBackColor = false;
            this.btnCaptureMeasurement.Click += new System.EventHandler(this.OnCaptureMeasurement);
            // 
            // txtMeasurements
            // 
            this.txtMeasurements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMeasurements.Location = new System.Drawing.Point(12, 50);
            this.txtMeasurements.Multiline = true;
            this.txtMeasurements.Name = "txtMeasurements";
            this.txtMeasurements.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMeasurements.Size = new System.Drawing.Size(160, 591);
            this.txtMeasurements.TabIndex = 39;
            this.txtMeasurements.Text = "选择线可直接显示结果，如果选择的是面则点击记录测量结果按钮。";
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenFolder.BackColor = System.Drawing.Color.Orange;
            this.btnOpenFolder.Enabled = false;
            this.btnOpenFolder.FlatAppearance.BorderSize = 0;
            this.btnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFolder.ForeColor = System.Drawing.Color.White;
            this.btnOpenFolder.Location = new System.Drawing.Point(331, 642);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(119, 28);
            this.btnOpenFolder.TabIndex = 41;
            this.btnOpenFolder.Tag = "1";
            this.btnOpenFolder.Text = "打开文件夹";
            this.btnOpenFolder.UseVisualStyleBackColor = false;
            this.btnOpenFolder.Click += new System.EventHandler(this.BtnOpenFolder_Click);
            // 
            // btnPublic
            // 
            this.btnPublic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPublic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnPublic.FlatAppearance.BorderSize = 0;
            this.btnPublic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPublic.ForeColor = System.Drawing.Color.White;
            this.btnPublic.Location = new System.Drawing.Point(255, 642);
            this.btnPublic.Name = "btnPublic";
            this.btnPublic.Size = new System.Drawing.Size(70, 28);
            this.btnPublic.TabIndex = 42;
            this.btnPublic.Tag = "1";
            this.btnPublic.Text = "公共盘";
            this.btnPublic.UseVisualStyleBackColor = false;
            this.btnPublic.Click += new System.EventHandler(this.OnOpen);
            // 
            // btnLocal
            // 
            this.btnLocal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLocal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnLocal.FlatAppearance.BorderSize = 0;
            this.btnLocal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocal.ForeColor = System.Drawing.Color.White;
            this.btnLocal.Location = new System.Drawing.Point(178, 642);
            this.btnLocal.Name = "btnLocal";
            this.btnLocal.Size = new System.Drawing.Size(71, 28);
            this.btnLocal.TabIndex = 43;
            this.btnLocal.Tag = "0";
            this.btnLocal.Text = "本地盘";
            this.btnLocal.UseVisualStyleBackColor = false;
            this.btnLocal.Click += new System.EventHandler(this.OnOpen);
            // 
            // FrmModelView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnPublic);
            this.Controls.Add(this.btnLocal);
            this.Controls.Add(this.btnCaptureMeasurement);
            this.Controls.Add(this.txtMeasurements);
            this.Controls.Add(this.ctrlEDrw);
            this.Name = "FrmModelView";
            this.Text = "天花总装";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EDrawingsUserControl ctrlEDrw;
        private System.Windows.Forms.Button btnCaptureMeasurement;
        private System.Windows.Forms.TextBox txtMeasurements;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnPublic;
        private System.Windows.Forms.Button btnLocal;
    }
}