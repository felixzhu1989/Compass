namespace Compass
{
    partial class FrmJobCardCeiling
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
            this.lblProject = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtItemNo = new System.Windows.Forms.TextBox();
            this.btnJobCard = new System.Windows.Forms.Button();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.ForeColor = System.Drawing.Color.Red;
            this.lblProject.Location = new System.Drawing.Point(23, 55);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(55, 13);
            this.lblProject.TabIndex = 47;
            this.lblProject.Text = "项目信息";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "编号";
            // 
            // txtItemNo
            // 
            this.txtItemNo.Location = new System.Drawing.Point(88, 80);
            this.txtItemNo.Name = "txtItemNo";
            this.txtItemNo.Size = new System.Drawing.Size(108, 20);
            this.txtItemNo.TabIndex = 48;
            // 
            // btnJobCard
            // 
            this.btnJobCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJobCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(72)))), ((int)(((byte)(232)))));
            this.btnJobCard.FlatAppearance.BorderSize = 0;
            this.btnJobCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJobCard.ForeColor = System.Drawing.Color.White;
            this.btnJobCard.Location = new System.Drawing.Point(440, 73);
            this.btnJobCard.Name = "btnJobCard";
            this.btnJobCard.Size = new System.Drawing.Size(52, 85);
            this.btnJobCard.TabIndex = 64;
            this.btnJobCard.Text = "打印 Job Card";
            this.btnJobCard.UseVisualStyleBackColor = false;
            this.btnJobCard.Click += new System.EventHandler(this.btnJobCard_Click);
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(88, 126);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(108, 20);
            this.txtModel.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "型号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(23, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(323, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "编号如：B1-B4/CJ01-CJ09/ACTIVE PANEL/PASSIVE PANEL/...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(23, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "型号如：KCJ/UCJ/KCW/UCW/...";
            // 
            // FrmJobCardCeiling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 179);
            this.Controls.Add(this.btnJobCard);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtItemNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblProject);
            this.MaximizeBox = false;
            this.Name = "FrmJobCardCeiling";
            this.Text = "打印天花烟罩JobCard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtItemNo;
        private System.Windows.Forms.Button btnJobCard;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}