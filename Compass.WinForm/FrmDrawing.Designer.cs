namespace Compass
{
    partial class FrmDrawing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDrawing));
            this.btnClearImage = new System.Windows.Forms.Button();
            this.btnChooseImage = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.btnDrawing = new System.Windows.Forms.Button();
            this.txtProjectPlanId = new System.Windows.Forms.TextBox();
            this.txtODPNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pbLabelImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLabelImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClearImage
            // 
            this.btnClearImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnClearImage.FlatAppearance.BorderSize = 0;
            this.btnClearImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearImage.ForeColor = System.Drawing.Color.White;
            this.btnClearImage.Location = new System.Drawing.Point(137, 52);
            this.btnClearImage.Name = "btnClearImage";
            this.btnClearImage.Size = new System.Drawing.Size(108, 26);
            this.btnClearImage.TabIndex = 2;
            this.btnClearImage.Text = "清除图片";
            this.btnClearImage.UseVisualStyleBackColor = false;
            this.btnClearImage.Click += new System.EventHandler(this.btnClearImage_Click);
            // 
            // btnChooseImage
            // 
            this.btnChooseImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnChooseImage.FlatAppearance.BorderSize = 0;
            this.btnChooseImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseImage.ForeColor = System.Drawing.Color.White;
            this.btnChooseImage.Location = new System.Drawing.Point(23, 52);
            this.btnChooseImage.Name = "btnChooseImage";
            this.btnChooseImage.Size = new System.Drawing.Size(108, 26);
            this.btnChooseImage.TabIndex = 1;
            this.btnChooseImage.Text = "粘贴图片";
            this.btnChooseImage.UseVisualStyleBackColor = false;
            this.btnChooseImage.Click += new System.EventHandler(this.btnChooseImage_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(426, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 47;
            this.label5.Text = "Item/编号";
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(488, 56);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(108, 21);
            this.txtItem.TabIndex = 3;
            // 
            // btnDrawing
            // 
            this.btnDrawing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnDrawing.FlatAppearance.BorderSize = 0;
            this.btnDrawing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrawing.ForeColor = System.Drawing.Color.White;
            this.btnDrawing.Location = new System.Drawing.Point(819, 52);
            this.btnDrawing.Name = "btnDrawing";
            this.btnDrawing.Size = new System.Drawing.Size(108, 26);
            this.btnDrawing.TabIndex = 0;
            this.btnDrawing.Text = "确定更改";
            this.btnDrawing.UseVisualStyleBackColor = false;
            this.btnDrawing.Click += new System.EventHandler(this.btnDrawing_Click);
            // 
            // txtProjectPlanId
            // 
            this.txtProjectPlanId.Location = new System.Drawing.Point(679, 56);
            this.txtProjectPlanId.Name = "txtProjectPlanId";
            this.txtProjectPlanId.ReadOnly = true;
            this.txtProjectPlanId.Size = new System.Drawing.Size(108, 21);
            this.txtProjectPlanId.TabIndex = 56;
            // 
            // txtODPNo
            // 
            this.txtODPNo.Location = new System.Drawing.Point(312, 56);
            this.txtODPNo.Name = "txtODPNo";
            this.txtODPNo.ReadOnly = true;
            this.txtODPNo.Size = new System.Drawing.Size(108, 21);
            this.txtODPNo.TabIndex = 57;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(608, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 58;
            this.label8.Text = "作图计划ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 59;
            this.label3.Text = "项目编号";
            // 
            // pbLabelImage
            // 
            this.pbLabelImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLabelImage.Location = new System.Drawing.Point(23, 83);
            this.pbLabelImage.Name = "pbLabelImage";
            this.pbLabelImage.Size = new System.Drawing.Size(904, 420);
            this.pbLabelImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLabelImage.TabIndex = 60;
            this.pbLabelImage.TabStop = false;
            // 
            // FrmDrawing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 524);
            this.Controls.Add(this.pbLabelImage);
            this.Controls.Add(this.txtProjectPlanId);
            this.Controls.Add(this.txtODPNo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDrawing);
            this.Controls.Add(this.btnClearImage);
            this.Controls.Add(this.btnChooseImage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtItem);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDrawing";
            this.Padding = new System.Windows.Forms.Padding(20, 55, 20, 18);
            this.Text = "编辑标签截图";
            ((System.ComponentModel.ISupportInitialize)(this.pbLabelImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClearImage;
        private System.Windows.Forms.Button btnChooseImage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.Button btnDrawing;
        private System.Windows.Forms.TextBox txtProjectPlanId;
        private System.Windows.Forms.TextBox txtODPNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbLabelImage;
    }
}