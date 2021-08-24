namespace Compass
{
    partial class FrmLLKS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLLKS));
            this.btnEditData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cobShortGlassNo = new System.Windows.Forms.ComboBox();
            this.cobLongGlassNo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.modelView = new Compass.ModelView();
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
            this.groupBox1.Controls.Add(this.cobShortGlassNo);
            this.groupBox1.Controls.Add(this.cobLongGlassNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Location = new System.Drawing.Point(24, 517);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 135);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "不锈钢灯腔侧板（普通玻璃）尺寸";
            // 
            // cobShortGlassNo
            // 
            this.cobShortGlassNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobShortGlassNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobShortGlassNo.BackColor = System.Drawing.Color.Azure;
            this.cobShortGlassNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobShortGlassNo.FormattingEnabled = true;
            this.cobShortGlassNo.Location = new System.Drawing.Point(76, 91);
            this.cobShortGlassNo.Name = "cobShortGlassNo";
            this.cobShortGlassNo.Size = new System.Drawing.Size(100, 27);
            this.cobShortGlassNo.TabIndex = 2;
            // 
            // cobLongGlassNo
            // 
            this.cobLongGlassNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobLongGlassNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobLongGlassNo.BackColor = System.Drawing.Color.Azure;
            this.cobLongGlassNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobLongGlassNo.FormattingEnabled = true;
            this.cobLongGlassNo.Location = new System.Drawing.Point(76, 56);
            this.cobLongGlassNo.Name = "cobLongGlassNo";
            this.cobLongGlassNo.Size = new System.Drawing.Size(100, 27);
            this.cobLongGlassNo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "短玻璃数量";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "长玻璃数量";
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
            // FrmLLKS
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.modelView);
            this.Controls.Add(this.btnEditData);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1200, 675);
            this.Name = "FrmLLKS";
            this.Text = "LLKS";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnEditData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cobLongGlassNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.ComboBox cobShortGlassNo;
        private System.Windows.Forms.Label label2;
        private ModelView modelView;
    }
}