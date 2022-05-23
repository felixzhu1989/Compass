namespace Compass
{
    partial class FrmLfusc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLfusc));
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cobJapan = new System.Windows.Forms.ComboBox();
            this.btnEditData = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cobSuNo = new System.Windows.Forms.ComboBox();
            this.cobSuDia = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSuDis = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSuDis = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.modelView = new Compass.ModelView();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.cobJapan);
            this.groupBox7.Location = new System.Drawing.Point(779, 517);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(398, 135);
            this.groupBox7.TabIndex = 39;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "其他配置";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 100);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 19);
            this.label14.TabIndex = 2;
            this.label14.Text = "日本项目";
            // 
            // cobJapan
            // 
            this.cobJapan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobJapan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobJapan.BackColor = System.Drawing.Color.Azure;
            this.cobJapan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobJapan.FormattingEnabled = true;
            this.cobJapan.Location = new System.Drawing.Point(92, 94);
            this.cobJapan.Name = "cobJapan";
            this.cobJapan.Size = new System.Drawing.Size(100, 27);
            this.cobJapan.TabIndex = 0;
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
            this.btnEditData.TabIndex = 40;
            this.btnEditData.Text = "修改参数";
            this.btnEditData.UseVisualStyleBackColor = false;
            this.btnEditData.Click += new System.EventHandler(this.btnEditData_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Controls.Add(this.cobSuNo);
            this.groupBox6.Controls.Add(this.cobSuDia);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.lblSuDis);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.txtSuDis);
            this.groupBox6.Location = new System.Drawing.Point(231, 517);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(338, 135);
            this.groupBox6.TabIndex = 38;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "均流桶";
            // 
            // cobSuNo
            // 
            this.cobSuNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobSuNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobSuNo.BackColor = System.Drawing.Color.Azure;
            this.cobSuNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobSuNo.FormattingEnabled = true;
            this.cobSuNo.Location = new System.Drawing.Point(90, 63);
            this.cobSuNo.Name = "cobSuNo";
            this.cobSuNo.Size = new System.Drawing.Size(100, 27);
            this.cobSuNo.TabIndex = 1;
            this.cobSuNo.SelectedIndexChanged += new System.EventHandler(this.cobSuNo_SelectedIndexChanged);
            // 
            // cobSuDia
            // 
            this.cobSuDia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobSuDia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobSuDia.BackColor = System.Drawing.Color.Azure;
            this.cobSuDia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobSuDia.FormattingEnabled = true;
            this.cobSuDia.Location = new System.Drawing.Point(91, 25);
            this.cobSuDia.Name = "cobSuDia";
            this.cobSuDia.Size = new System.Drawing.Size(100, 27);
            this.cobSuDia.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "均流桶数量";
            // 
            // lblSuDis
            // 
            this.lblSuDis.AutoSize = true;
            this.lblSuDis.Location = new System.Drawing.Point(3, 100);
            this.lblSuDis.Name = "lblSuDis";
            this.lblSuDis.Size = new System.Drawing.Size(74, 19);
            this.lblSuDis.TabIndex = 2;
            this.lblSuDis.Text = "均流桶间距";
            this.lblSuDis.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "均流桶直径";
            // 
            // txtSuDis
            // 
            this.txtSuDis.BackColor = System.Drawing.Color.Azure;
            this.txtSuDis.Location = new System.Drawing.Point(90, 97);
            this.txtSuDis.Name = "txtSuDis";
            this.txtSuDis.Size = new System.Drawing.Size(100, 25);
            this.txtSuDis.TabIndex = 2;
            this.txtSuDis.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Location = new System.Drawing.Point(24, 517);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 135);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "C型散流器尺寸";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "散流器长度";
            // 
            // txtLength
            // 
            this.txtLength.BackColor = System.Drawing.Color.Azure;
            this.txtLength.Location = new System.Drawing.Point(85, 25);
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
            // FrmLFUSC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.modelView);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.btnEditData);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1200, 675);
            this.Name = "FrmLfusc";
            this.Text = "LFUSC";
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cobJapan;
        private System.Windows.Forms.Button btnEditData;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cobSuNo;
        private System.Windows.Forms.ComboBox cobSuDia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSuDis;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSuDis;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLength;
        private ModelView modelView;
    }
}