namespace Compass
{
    partial class FrmDrawingPlanInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefreshingData = new System.Windows.Forms.Button();
            this.lblClose = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "统计信息";
            // 
            // btnRefreshingData
            // 
            this.btnRefreshingData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnRefreshingData.FlatAppearance.BorderSize = 0;
            this.btnRefreshingData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshingData.ForeColor = System.Drawing.Color.White;
            this.btnRefreshingData.Location = new System.Drawing.Point(10, 528);
            this.btnRefreshingData.Name = "btnRefreshingData";
            this.btnRefreshingData.Size = new System.Drawing.Size(108, 28);
            this.btnRefreshingData.TabIndex = 7;
            this.btnRefreshingData.Text = "刷新显示数据";
            this.btnRefreshingData.UseVisualStyleBackColor = false;
            // 
            // lblClose
            // 
            this.lblClose.AutoSize = true;
            this.lblClose.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblClose.Location = new System.Drawing.Point(121, 5);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(89, 19);
            this.lblClose.TabIndex = 2;
            this.lblClose.Text = "[ 关闭面板 X ]";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // FrmDrawingPlanInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(210, 568);
            this.Controls.Add(this.btnRefreshingData);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDrawingPlanInfo";
            this.Text = "FrmDrawingPlanInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefreshingData;
        private System.Windows.Forms.Label lblClose;
    }
}