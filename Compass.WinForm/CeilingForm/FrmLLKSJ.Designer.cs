namespace Compass
{
    partial class FrmLlksj
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLlksj));
            this.btnEditData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRightLength = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLeftLength = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMidLength = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLongGlassNo = new System.Windows.Forms.TextBox();
            this.txtShortGlassNo = new System.Windows.Forms.TextBox();
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
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtRightLength);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtMidLength);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLeftLength);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtShortGlassNo);
            this.groupBox1.Controls.Add(this.txtLongGlassNo);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Location = new System.Drawing.Point(24, 517);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 135);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "不锈钢灯腔尺寸";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "短玻璃数量";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "长玻璃数量";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(361, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "右侧板长度";
            // 
            // txtRightLength
            // 
            this.txtRightLength.BackColor = System.Drawing.Color.Azure;
            this.txtRightLength.Location = new System.Drawing.Point(437, 22);
            this.txtRightLength.Name = "txtRightLength";
            this.txtRightLength.Size = new System.Drawing.Size(100, 25);
            this.txtRightLength.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(179, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "左侧板长度";
            // 
            // txtLeftLength
            // 
            this.txtLeftLength.BackColor = System.Drawing.Color.Azure;
            this.txtLeftLength.Location = new System.Drawing.Point(255, 22);
            this.txtLeftLength.Name = "txtLeftLength";
            this.txtLeftLength.Size = new System.Drawing.Size(100, 25);
            this.txtLeftLength.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "灯腔长度";
            // 
            // txtLength
            // 
            this.txtLength.BackColor = System.Drawing.Color.Azure;
            this.txtLength.Location = new System.Drawing.Point(76, 22);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(100, 25);
            this.txtLength.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(23, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 460);
            this.panel1.TabIndex = 8;
            // 
            // txtMidLength
            // 
            this.txtMidLength.BackColor = System.Drawing.Color.Azure;
            this.txtMidLength.Location = new System.Drawing.Point(255, 62);
            this.txtMidLength.Name = "txtMidLength";
            this.txtMidLength.Size = new System.Drawing.Size(100, 25);
            this.txtMidLength.TabIndex = 1;
            this.txtMidLength.Text = "295";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(179, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 19);
            this.label6.TabIndex = 2;
            this.label6.Text = "中间板长度";
            // 
            // txtLongGlassNo
            // 
            this.txtLongGlassNo.BackColor = System.Drawing.Color.Azure;
            this.txtLongGlassNo.Location = new System.Drawing.Point(76, 62);
            this.txtLongGlassNo.Name = "txtLongGlassNo";
            this.txtLongGlassNo.Size = new System.Drawing.Size(100, 25);
            this.txtLongGlassNo.TabIndex = 0;
            // 
            // txtShortGlassNo
            // 
            this.txtShortGlassNo.BackColor = System.Drawing.Color.Azure;
            this.txtShortGlassNo.Location = new System.Drawing.Point(76, 99);
            this.txtShortGlassNo.Name = "txtShortGlassNo";
            this.txtShortGlassNo.Size = new System.Drawing.Size(100, 25);
            this.txtShortGlassNo.TabIndex = 0;
            // 
            // FrmLlksj
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnEditData);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1200, 675);
            this.Name = "FrmLlksj";
            this.Text = "LLKSJ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnEditData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRightLength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLeftLength;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMidLength;
        private System.Windows.Forms.TextBox txtShortGlassNo;
        private System.Windows.Forms.TextBox txtLongGlassNo;
    }
}