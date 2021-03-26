namespace Compass
{
    partial class FrmLPZ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLPZ));
            this.btnEditData = new System.Windows.Forms.Button();
            this.pbModelImage = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cobZPanelNo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbModelImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEditData
            // 
            this.btnEditData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnEditData.FlatAppearance.BorderSize = 0;
            this.btnEditData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditData.ForeColor = System.Drawing.Color.White;
            this.btnEditData.Location = new System.Drawing.Point(575, 616);
            this.btnEditData.Name = "btnEditData";
            this.btnEditData.Size = new System.Drawing.Size(198, 36);
            this.btnEditData.TabIndex = 1;
            this.btnEditData.Text = "修改参数";
            this.btnEditData.UseVisualStyleBackColor = false;
            this.btnEditData.Click += new System.EventHandler(this.btnEditData_Click);
            // 
            // pbModelImage
            // 
            this.pbModelImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbModelImage.Image = global::Compass.Properties.Resources.NoPic;
            this.pbModelImage.Location = new System.Drawing.Point(23, 63);
            this.pbModelImage.Name = "pbModelImage";
            this.pbModelImage.Size = new System.Drawing.Size(750, 445);
            this.pbModelImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModelImage.TabIndex = 49;
            this.pbModelImage.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.cobZPanelNo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtWidth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Location = new System.Drawing.Point(24, 517);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 135);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LP板-Z板阵列";
            // 
            // cobZPanelNo
            // 
            this.cobZPanelNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobZPanelNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobZPanelNo.BackColor = System.Drawing.Color.Azure;
            this.cobZPanelNo.FormattingEnabled = true;
            this.cobZPanelNo.Location = new System.Drawing.Point(76, 56);
            this.cobZPanelNo.Name = "cobZPanelNo";
            this.cobZPanelNo.Size = new System.Drawing.Size(100, 27);
            this.cobZPanelNo.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "Z板数量";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "W板宽度";
            // 
            // txtWidth
            // 
            this.txtWidth.BackColor = System.Drawing.Color.Azure;
            this.txtWidth.Location = new System.Drawing.Point(263, 25);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(100, 25);
            this.txtWidth.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "LP板长度";
            // 
            // txtLength
            // 
            this.txtLength.BackColor = System.Drawing.Color.Azure;
            this.txtLength.Location = new System.Drawing.Point(76, 25);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(100, 25);
            this.txtLength.TabIndex = 0;
            // 
            // FrmLPZ
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.btnEditData);
            this.Controls.Add(this.pbModelImage);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1200, 675);
            this.Name = "FrmLPZ";
            this.Text = "LPZ";
            ((System.ComponentModel.ISupportInitialize)(this.pbModelImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnEditData;
        private System.Windows.Forms.PictureBox pbModelImage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cobZPanelNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWidth;
    }
}