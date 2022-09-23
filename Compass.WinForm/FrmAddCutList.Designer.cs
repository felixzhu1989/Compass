namespace Compass.WinForm
{
    partial class FrmAddCutList
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
            this.AddCutList = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Length = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPartDescription = new System.Windows.Forms.TextBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtThickness = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtMaterials = new System.Windows.Forms.TextBox();
            this.txtPartNo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // AddCutList
            // 
            this.AddCutList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddCutList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.AddCutList.FlatAppearance.BorderSize = 0;
            this.AddCutList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddCutList.ForeColor = System.Drawing.Color.White;
            this.AddCutList.Location = new System.Drawing.Point(200, 350);
            this.AddCutList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AddCutList.Name = "AddCutList";
            this.AddCutList.Size = new System.Drawing.Size(167, 37);
            this.AddCutList.TabIndex = 45;
            this.AddCutList.Text = "添加CutList";
            this.AddCutList.UseVisualStyleBackColor = false;
            this.AddCutList.Click += new System.EventHandler(this.AddCutList_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 16);
            this.label1.TabIndex = 46;
            this.label1.Text = "PartDescription";
            // 
            // Length
            // 
            this.Length.AutoSize = true;
            this.Length.Location = new System.Drawing.Point(86, 105);
            this.Length.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Length.Name = "Length";
            this.Length.Size = new System.Drawing.Size(55, 16);
            this.Length.TabIndex = 46;
            this.Length.Text = "Length";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 145);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 46;
            this.label3.Text = "Width";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 185);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 46;
            this.label4.Text = "Thickness";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 225);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 16);
            this.label5.TabIndex = 46;
            this.label5.Text = "Quantity";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 265);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 16);
            this.label6.TabIndex = 46;
            this.label6.Text = "Materials";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(86, 305);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 16);
            this.label7.TabIndex = 46;
            this.label7.Text = "PartNo";
            // 
            // txtPartDescription
            // 
            this.txtPartDescription.Location = new System.Drawing.Point(160, 62);
            this.txtPartDescription.Name = "txtPartDescription";
            this.txtPartDescription.Size = new System.Drawing.Size(211, 26);
            this.txtPartDescription.TabIndex = 47;
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(160, 102);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(211, 26);
            this.txtLength.TabIndex = 47;
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(160, 142);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(211, 26);
            this.txtWidth.TabIndex = 47;
            // 
            // txtThickness
            // 
            this.txtThickness.Location = new System.Drawing.Point(160, 182);
            this.txtThickness.Name = "txtThickness";
            this.txtThickness.Size = new System.Drawing.Size(211, 26);
            this.txtThickness.TabIndex = 47;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(160, 222);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(211, 26);
            this.txtQuantity.TabIndex = 47;
            // 
            // txtMaterials
            // 
            this.txtMaterials.Location = new System.Drawing.Point(160, 262);
            this.txtMaterials.Name = "txtMaterials";
            this.txtMaterials.Size = new System.Drawing.Size(211, 26);
            this.txtMaterials.TabIndex = 47;
            // 
            // txtPartNo
            // 
            this.txtPartNo.Location = new System.Drawing.Point(160, 302);
            this.txtPartNo.Name = "txtPartNo";
            this.txtPartNo.Size = new System.Drawing.Size(211, 26);
            this.txtPartNo.TabIndex = 47;
            // 
            // FrmAddCutList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 413);
            this.Controls.Add(this.txtPartNo);
            this.Controls.Add(this.txtMaterials);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.txtThickness);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.txtPartDescription);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Length);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AddCutList);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmAddCutList";
            this.Padding = new System.Windows.Forms.Padding(27, 80, 27, 27);
            this.Text = "AddCutList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddCutList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Length;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPartDescription;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtThickness;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtMaterials;
        private System.Windows.Forms.TextBox txtPartNo;
    }
}