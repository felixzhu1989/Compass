namespace Compass
{
    partial class FrmSspdome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSspdome));
            this.grbMARVEL = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cobLightType = new System.Windows.Forms.ComboBox();
            this.btnEditData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cobRightType = new System.Windows.Forms.ComboBox();
            this.cobLeftType = new System.Windows.Forms.ComboBox();
            this.cobMPanelNo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRightLength = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLeftLength = new System.Windows.Forms.TextBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.modelView = new Compass.ModelView();
            this.grbMARVEL.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbMARVEL
            // 
            this.grbMARVEL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbMARVEL.Controls.Add(this.label5);
            this.grbMARVEL.Controls.Add(this.cobLightType);
            this.grbMARVEL.Location = new System.Drawing.Point(779, 409);
            this.grbMARVEL.Name = "grbMARVEL";
            this.grbMARVEL.Size = new System.Drawing.Size(398, 99);
            this.grbMARVEL.TabIndex = 1;
            this.grbMARVEL.TabStop = false;
            this.grbMARVEL.Text = "其他配置";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 4;
            this.label5.Text = "灯具类型";
            // 
            // cobLightType
            // 
            this.cobLightType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobLightType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobLightType.BackColor = System.Drawing.Color.Azure;
            this.cobLightType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobLightType.FormattingEnabled = true;
            this.cobLightType.Location = new System.Drawing.Point(89, 27);
            this.cobLightType.Name = "cobLightType";
            this.cobLightType.Size = new System.Drawing.Size(100, 27);
            this.cobLightType.TabIndex = 0;
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
            this.groupBox1.Controls.Add(this.cobRightType);
            this.groupBox1.Controls.Add(this.cobLeftType);
            this.groupBox1.Controls.Add(this.cobMPanelNo);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtRightLength);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtLeftLength);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Location = new System.Drawing.Point(24, 517);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 135);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "不锈钢灯板-拱板";
            // 
            // cobRightType
            // 
            this.cobRightType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobRightType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobRightType.BackColor = System.Drawing.Color.Azure;
            this.cobRightType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobRightType.FormattingEnabled = true;
            this.cobRightType.Location = new System.Drawing.Point(436, 56);
            this.cobRightType.Name = "cobRightType";
            this.cobRightType.Size = new System.Drawing.Size(100, 27);
            this.cobRightType.TabIndex = 5;
            // 
            // cobLeftType
            // 
            this.cobLeftType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobLeftType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobLeftType.BackColor = System.Drawing.Color.Azure;
            this.cobLeftType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobLeftType.FormattingEnabled = true;
            this.cobLeftType.Location = new System.Drawing.Point(436, 26);
            this.cobLeftType.Name = "cobLeftType";
            this.cobLeftType.Size = new System.Drawing.Size(100, 27);
            this.cobLeftType.TabIndex = 4;
            // 
            // cobMPanelNo
            // 
            this.cobMPanelNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobMPanelNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobMPanelNo.BackColor = System.Drawing.Color.Azure;
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(356, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 19);
            this.label6.TabIndex = 2;
            this.label6.Text = "右灯板类型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "右灯板宽度";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(356, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "左灯板类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "左灯板宽度";
            // 
            // txtRightLength
            // 
            this.txtRightLength.BackColor = System.Drawing.Color.Azure;
            this.txtRightLength.Location = new System.Drawing.Point(250, 57);
            this.txtRightLength.Name = "txtRightLength";
            this.txtRightLength.Size = new System.Drawing.Size(100, 25);
            this.txtRightLength.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "灯板长度";
            // 
            // txtLeftLength
            // 
            this.txtLeftLength.BackColor = System.Drawing.Color.Azure;
            this.txtLeftLength.Location = new System.Drawing.Point(250, 26);
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
            // FrmSSPDOME
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
            this.Name = "FrmSspdome";
            this.Text = "SSPDOME";
            this.grbMARVEL.ResumeLayout(false);
            this.grbMARVEL.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbMARVEL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cobLightType;
        private System.Windows.Forms.Button btnEditData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cobMPanelNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRightLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLeftLength;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.ComboBox cobRightType;
        private System.Windows.Forms.ComboBox cobLeftType;
        private ModelView modelView;
    }
}