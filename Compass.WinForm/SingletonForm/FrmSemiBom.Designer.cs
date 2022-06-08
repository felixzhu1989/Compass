namespace Compass
{
    partial class FrmSemiBom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSemiBom));
            this.BtnCreateSemiBom = new System.Windows.Forms.Button();
            this.cobODPNo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBPONo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tspbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnCreateSemiBom
            // 
            this.BtnCreateSemiBom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.BtnCreateSemiBom.FlatAppearance.BorderSize = 0;
            this.BtnCreateSemiBom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCreateSemiBom.ForeColor = System.Drawing.Color.White;
            this.BtnCreateSemiBom.Location = new System.Drawing.Point(199, 28);
            this.BtnCreateSemiBom.Name = "BtnCreateSemiBom";
            this.BtnCreateSemiBom.Size = new System.Drawing.Size(116, 27);
            this.BtnCreateSemiBom.TabIndex = 65;
            this.BtnCreateSemiBom.Text = "生成半成品清单";
            this.BtnCreateSemiBom.UseVisualStyleBackColor = false;
            this.BtnCreateSemiBom.Click += new System.EventHandler(this.BtnCreateSemiBom_Click);
            // 
            // cobODPNo
            // 
            this.cobODPNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobODPNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobODPNo.FormattingEnabled = true;
            this.cobODPNo.Location = new System.Drawing.Point(386, 28);
            this.cobODPNo.Name = "cobODPNo";
            this.cobODPNo.Size = new System.Drawing.Size(108, 27);
            this.cobODPNo.TabIndex = 63;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(679, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 60;
            this.label5.Text = "项目名称";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(745, 29);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.ReadOnly = true;
            this.txtProjectName.Size = new System.Drawing.Size(420, 25);
            this.txtProjectName.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(499, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 61;
            this.label2.Text = "大工单号";
            // 
            // txtBPONo
            // 
            this.txtBPONo.Location = new System.Drawing.Point(565, 29);
            this.txtBPONo.Name = "txtBPONo";
            this.txtBPONo.ReadOnly = true;
            this.txtBPONo.Size = new System.Drawing.Size(108, 25);
            this.txtBPONo.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(321, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 62;
            this.label3.Text = "项目编号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(321, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(204, 19);
            this.label6.TabIndex = 64;
            this.label6.Text = "请确保这里有数据后执行下列操作";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbStatus,
            this.tsslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(27, 620);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1146, 24);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 66;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tspbStatus
            // 
            this.tspbStatus.AutoSize = false;
            this.tspbStatus.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tspbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.tspbStatus.Name = "tspbStatus";
            this.tspbStatus.Size = new System.Drawing.Size(550, 18);
            // 
            // tsslStatus
            // 
            this.tsslStatus.BackColor = System.Drawing.Color.Transparent;
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(74, 19);
            this.tsslStatus.Text = "准备就绪！";
            // 
            // FrmSemiBom
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.BtnCreateSemiBom);
            this.Controls.Add(this.cobODPNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBPONo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmSemiBom";
            this.Padding = new System.Windows.Forms.Padding(27, 95, 27, 31);
            this.Text = "半成品清单";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnCreateSemiBom;
        private System.Windows.Forms.ComboBox cobODPNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBPONo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar tspbStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
    }
}