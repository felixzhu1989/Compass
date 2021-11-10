﻿namespace Compass
{
    partial class FrmLksspec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLksspec));
            this.grbMARVEL = new System.Windows.Forms.GroupBox();
            this.cobWBeam = new System.Windows.Forms.ComboBox();
            this.cobLightType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cobJapan = new System.Windows.Forms.ComboBox();
            this.btnEditData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cobSidePanel = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.modelView = new Compass.ModelView();
            this.grbMARVEL.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbMARVEL
            // 
            this.grbMARVEL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grbMARVEL.Controls.Add(this.cobWBeam);
            this.grbMARVEL.Controls.Add(this.cobLightType);
            this.grbMARVEL.Controls.Add(this.label3);
            this.grbMARVEL.Controls.Add(this.label5);
            this.grbMARVEL.Controls.Add(this.label2);
            this.grbMARVEL.Controls.Add(this.label22);
            this.grbMARVEL.Controls.Add(this.cobJapan);
            this.grbMARVEL.Location = new System.Drawing.Point(779, 372);
            this.grbMARVEL.Name = "grbMARVEL";
            this.grbMARVEL.Size = new System.Drawing.Size(398, 136);
            this.grbMARVEL.TabIndex = 1;
            this.grbMARVEL.TabStop = false;
            this.grbMARVEL.Text = "其他配置";
            // 
            // cobWBeam
            // 
            this.cobWBeam.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobWBeam.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobWBeam.BackColor = System.Drawing.Color.Azure;
            this.cobWBeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobWBeam.FormattingEnabled = true;
            this.cobWBeam.Location = new System.Drawing.Point(94, 55);
            this.cobWBeam.Name = "cobWBeam";
            this.cobWBeam.Size = new System.Drawing.Size(100, 27);
            this.cobWBeam.TabIndex = 1;
            this.cobWBeam.Visible = false;
            // 
            // cobLightType
            // 
            this.cobLightType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobLightType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobLightType.BackColor = System.Drawing.Color.Azure;
            this.cobLightType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobLightType.FormattingEnabled = true;
            this.cobLightType.Location = new System.Drawing.Point(94, 22);
            this.cobLightType.Name = "cobLightType";
            this.cobLightType.Size = new System.Drawing.Size(100, 27);
            this.cobLightType.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "灯具类型";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label5.Location = new System.Drawing.Point(7, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(204, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "注意：只针对不带灯腔灯水洗烟罩";
            this.label5.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "水洗烟罩";
            this.label2.Visible = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(200, 25);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(61, 19);
            this.label22.TabIndex = 2;
            this.label22.Text = "日本项目";
            this.label22.Visible = false;
            // 
            // cobJapan
            // 
            this.cobJapan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobJapan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobJapan.BackColor = System.Drawing.Color.Azure;
            this.cobJapan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobJapan.FormattingEnabled = true;
            this.cobJapan.Location = new System.Drawing.Point(279, 21);
            this.cobJapan.Name = "cobJapan";
            this.cobJapan.Size = new System.Drawing.Size(100, 27);
            this.cobJapan.TabIndex = 2;
            this.cobJapan.Visible = false;
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
            this.groupBox1.Controls.Add(this.cobSidePanel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Location = new System.Drawing.Point(24, 517);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 135);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "不锈钢灯腔尺寸";
            // 
            // cobSidePanel
            // 
            this.cobSidePanel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobSidePanel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobSidePanel.BackColor = System.Drawing.Color.Azure;
            this.cobSidePanel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobSidePanel.FormattingEnabled = true;
            this.cobSidePanel.Location = new System.Drawing.Point(76, 56);
            this.cobSidePanel.Name = "cobSidePanel";
            this.cobSidePanel.Size = new System.Drawing.Size(100, 27);
            this.cobSidePanel.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "灯腔侧板";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(194, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 19);
            this.label6.TabIndex = 2;
            this.label6.Text = "灯腔高度";
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
            // txtHeight
            // 
            this.txtHeight.BackColor = System.Drawing.Color.Azure;
            this.txtHeight.Location = new System.Drawing.Point(266, 25);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(100, 25);
            this.txtHeight.TabIndex = 1;
            // 
            // txtLength
            // 
            this.txtLength.BackColor = System.Drawing.Color.Azure;
            this.txtLength.Location = new System.Drawing.Point(76, 25);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(100, 25);
            this.txtLength.TabIndex = 0;
            // 
            // modelView
            // 
            this.modelView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modelView.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelView.Location = new System.Drawing.Point(25, 48);
            this.modelView.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.modelView.Name = "modelView";
            this.modelView.Size = new System.Drawing.Size(750, 460);
            this.modelView.TabIndex = 77;
            // 
            // FrmLKSSPEC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.modelView);
            this.Controls.Add(this.grbMARVEL);
            this.Controls.Add(this.btnEditData);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1200, 675);
            this.Name = "FrmLksspec";
            this.Text = "LKSSPEC";
            this.grbMARVEL.ResumeLayout(false);
            this.grbMARVEL.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbMARVEL;
        private System.Windows.Forms.ComboBox cobWBeam;
        private System.Windows.Forms.ComboBox cobLightType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cobJapan;
        private System.Windows.Forms.Button btnEditData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cobSidePanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtHeight;
        private ModelView modelView;
    }
}