﻿namespace Compass
{
    partial class FrmBF200
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBF200));
            this.grbMARVEL = new System.Windows.Forms.GroupBox();
            this.btnEditData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLeftLength = new System.Windows.Forms.TextBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.pbModelImage = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cobUVType = new System.Windows.Forms.ComboBox();
            this.cobMPanelNo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRightLength = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMPanelLength = new System.Windows.Forms.TextBox();
            this.txtWPanelLength = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.grbMARVEL.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbModelImage)).BeginInit();
            this.SuspendLayout();
            // 
            // grbMARVEL
            // 
            this.grbMARVEL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbMARVEL.Controls.Add(this.label5);
            this.grbMARVEL.Controls.Add(this.cobUVType);
            this.grbMARVEL.Location = new System.Drawing.Point(779, 409);
            this.grbMARVEL.Name = "grbMARVEL";
            this.grbMARVEL.Size = new System.Drawing.Size(398, 99);
            this.grbMARVEL.TabIndex = 1;
            this.grbMARVEL.TabStop = false;
            this.grbMARVEL.Text = "其他配置";
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
            this.btnEditData.TabIndex = 2;
            this.btnEditData.Text = "修改参数";
            this.btnEditData.UseVisualStyleBackColor = false;
            this.btnEditData.Click += new System.EventHandler(this.btnEditData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.cobMPanelNo);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtWPanelLength);
            this.groupBox1.Controls.Add(this.txtRightLength);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtMPanelLength);
            this.groupBox1.Controls.Add(this.txtLeftLength);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Location = new System.Drawing.Point(24, 517);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 135);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "水洗挡板尺寸";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "UL长度";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "总长度";
            // 
            // txtLeftLength
            // 
            this.txtLeftLength.BackColor = System.Drawing.Color.Azure;
            this.txtLeftLength.Location = new System.Drawing.Point(233, 26);
            this.txtLeftLength.Name = "txtLeftLength";
            this.txtLeftLength.Size = new System.Drawing.Size(100, 25);
            this.txtLeftLength.TabIndex = 2;
            // 
            // txtLength
            // 
            this.txtLength.BackColor = System.Drawing.Color.Azure;
            this.txtLength.Location = new System.Drawing.Point(68, 25);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(100, 25);
            this.txtLength.TabIndex = 0;
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
            this.pbModelImage.TabIndex = 77;
            this.pbModelImage.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 19);
            this.label5.TabIndex = 4;
            this.label5.Text = "UV烟罩";
            // 
            // cobUVType
            // 
            this.cobUVType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobUVType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobUVType.BackColor = System.Drawing.Color.Azure;
            this.cobUVType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobUVType.FormattingEnabled = true;
            this.cobUVType.Location = new System.Drawing.Point(89, 27);
            this.cobUVType.Name = "cobUVType";
            this.cobUVType.Size = new System.Drawing.Size(100, 27);
            this.cobUVType.TabIndex = 0;
            // 
            // cobMPanelNo
            // 
            this.cobMPanelNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobMPanelNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobMPanelNo.BackColor = System.Drawing.Color.Azure;
            this.cobMPanelNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobMPanelNo.FormattingEnabled = true;
            this.cobMPanelNo.Location = new System.Drawing.Point(68, 56);
            this.cobMPanelNo.Name = "cobMPanelNo";
            this.cobMPanelNo.Size = new System.Drawing.Size(100, 27);
            this.cobMPanelNo.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 19);
            this.label9.TabIndex = 4;
            this.label9.Text = "M型数量";
            // 
            // txtRightLength
            // 
            this.txtRightLength.BackColor = System.Drawing.Color.Azure;
            this.txtRightLength.Location = new System.Drawing.Point(233, 57);
            this.txtRightLength.Name = "txtRightLength";
            this.txtRightLength.Size = new System.Drawing.Size(100, 25);
            this.txtRightLength.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "UR长度";
            // 
            // txtMPanelLength
            // 
            this.txtMPanelLength.BackColor = System.Drawing.Color.Azure;
            this.txtMPanelLength.Location = new System.Drawing.Point(437, 26);
            this.txtMPanelLength.Name = "txtMPanelLength";
            this.txtMPanelLength.Size = new System.Drawing.Size(100, 25);
            this.txtMPanelLength.TabIndex = 4;
            // 
            // txtWPanelLength
            // 
            this.txtWPanelLength.BackColor = System.Drawing.Color.Azure;
            this.txtWPanelLength.Location = new System.Drawing.Point(437, 57);
            this.txtWPanelLength.Name = "txtWPanelLength";
            this.txtWPanelLength.Size = new System.Drawing.Size(100, 25);
            this.txtWPanelLength.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(342, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "标准M型长度";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(342, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 19);
            this.label6.TabIndex = 2;
            this.label6.Text = "标准W型长度";
            // 
            // FrmBF200
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.grbMARVEL);
            this.Controls.Add(this.btnEditData);
            this.Controls.Add(this.pbModelImage);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1200, 675);
            this.Name = "FrmBF200";
            this.Text = "BF200";
            this.grbMARVEL.ResumeLayout(false);
            this.grbMARVEL.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbModelImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbMARVEL;
        private System.Windows.Forms.Button btnEditData;
        private System.Windows.Forms.PictureBox pbModelImage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLeftLength;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cobUVType;
        private System.Windows.Forms.ComboBox cobMPanelNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRightLength;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWPanelLength;
        private System.Windows.Forms.TextBox txtMPanelLength;
    }
}