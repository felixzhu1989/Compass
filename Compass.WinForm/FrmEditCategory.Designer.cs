namespace Compass
{
    partial class FrmEditCategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditCategory));
            this.lblParentDesc = new System.Windows.Forms.Label();
            this.pbModelImage = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnClearImage = new System.Windows.Forms.Button();
            this.btnChooseImage = new System.Windows.Forms.Button();
            this.btnEditCategory = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cobParentId = new System.Windows.Forms.ComboBox();
            this.txtCategoryId = new System.Windows.Forms.TextBox();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.txtKMLink = new System.Windows.Forms.TextBox();
            this.txtSubType = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtCategoryDesc = new System.Windows.Forms.TextBox();
            this.llblHelp = new System.Windows.Forms.LinkLabel();
            this.txtModelPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbModelImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblParentDesc
            // 
            this.lblParentDesc.AutoSize = true;
            this.lblParentDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.lblParentDesc.Location = new System.Drawing.Point(317, 50);
            this.lblParentDesc.Name = "lblParentDesc";
            this.lblParentDesc.Size = new System.Drawing.Size(35, 19);
            this.lblParentDesc.TabIndex = 41;
            this.lblParentDesc.Text = "描述";
            // 
            // pbModelImage
            // 
            this.pbModelImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbModelImage.Image = ((System.Drawing.Image)(resources.GetObject("pbModelImage.Image")));
            this.pbModelImage.Location = new System.Drawing.Point(27, 216);
            this.pbModelImage.Name = "pbModelImage";
            this.pbModelImage.Size = new System.Drawing.Size(904, 347);
            this.pbModelImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbModelImage.TabIndex = 40;
            this.pbModelImage.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 33;
            this.label2.Text = "分类编号";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 19);
            this.label7.TabIndex = 35;
            this.label7.Text = "分类名称";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 34;
            this.label5.Text = "所属模型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(248, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 36;
            this.label3.Text = "分类描述";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 19);
            this.label6.TabIndex = 38;
            this.label6.Text = "子类型号";
            // 
            // btnClearImage
            // 
            this.btnClearImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnClearImage.FlatAppearance.BorderSize = 0;
            this.btnClearImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearImage.ForeColor = System.Drawing.Color.White;
            this.btnClearImage.Location = new System.Drawing.Point(781, 104);
            this.btnClearImage.Name = "btnClearImage";
            this.btnClearImage.Size = new System.Drawing.Size(150, 28);
            this.btnClearImage.TabIndex = 32;
            this.btnClearImage.Text = "清除图片";
            this.btnClearImage.UseVisualStyleBackColor = false;
            this.btnClearImage.Click += new System.EventHandler(this.BtnClearImage_Click);
            // 
            // btnChooseImage
            // 
            this.btnChooseImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnChooseImage.FlatAppearance.BorderSize = 0;
            this.btnChooseImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseImage.ForeColor = System.Drawing.Color.White;
            this.btnChooseImage.Location = new System.Drawing.Point(781, 69);
            this.btnChooseImage.Name = "btnChooseImage";
            this.btnChooseImage.Size = new System.Drawing.Size(150, 28);
            this.btnChooseImage.TabIndex = 30;
            this.btnChooseImage.Text = "浏览图片";
            this.btnChooseImage.UseVisualStyleBackColor = false;
            this.btnChooseImage.Click += new System.EventHandler(this.BtnChooseImage_Click);
            // 
            // btnEditCategory
            // 
            this.btnEditCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnEditCategory.FlatAppearance.BorderSize = 0;
            this.btnEditCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditCategory.ForeColor = System.Drawing.Color.White;
            this.btnEditCategory.Location = new System.Drawing.Point(781, 182);
            this.btnEditCategory.Name = "btnEditCategory";
            this.btnEditCategory.Size = new System.Drawing.Size(150, 28);
            this.btnEditCategory.TabIndex = 6;
            this.btnEditCategory.Text = "修改分类";
            this.btnEditCategory.UseVisualStyleBackColor = false;
            this.btnEditCategory.Click += new System.EventHandler(this.BtnEditCategory_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 39;
            this.label4.Text = "父类编号";
            // 
            // cobParentId
            // 
            this.cobParentId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobParentId.FormattingEnabled = true;
            this.cobParentId.Location = new System.Drawing.Point(317, 69);
            this.cobParentId.Name = "cobParentId";
            this.cobParentId.Size = new System.Drawing.Size(150, 27);
            this.cobParentId.TabIndex = 23;
            // 
            // txtCategoryId
            // 
            this.txtCategoryId.Location = new System.Drawing.Point(92, 69);
            this.txtCategoryId.Name = "txtCategoryId";
            this.txtCategoryId.ReadOnly = true;
            this.txtCategoryId.Size = new System.Drawing.Size(150, 25);
            this.txtCategoryId.TabIndex = 24;
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(92, 111);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(150, 25);
            this.txtCategoryName.TabIndex = 0;
            // 
            // txtKMLink
            // 
            this.txtKMLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKMLink.Location = new System.Drawing.Point(316, 184);
            this.txtKMLink.Name = "txtKMLink";
            this.txtKMLink.Size = new System.Drawing.Size(459, 25);
            this.txtKMLink.TabIndex = 5;
            // 
            // txtSubType
            // 
            this.txtSubType.Location = new System.Drawing.Point(92, 184);
            this.txtSubType.Name = "txtSubType";
            this.txtSubType.Size = new System.Drawing.Size(150, 25);
            this.txtSubType.TabIndex = 2;
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(92, 147);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(150, 25);
            this.txtModel.TabIndex = 1;
            // 
            // txtCategoryDesc
            // 
            this.txtCategoryDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCategoryDesc.Location = new System.Drawing.Point(316, 111);
            this.txtCategoryDesc.Name = "txtCategoryDesc";
            this.txtCategoryDesc.Size = new System.Drawing.Size(459, 25);
            this.txtCategoryDesc.TabIndex = 3;
            // 
            // llblHelp
            // 
            this.llblHelp.AutoSize = true;
            this.llblHelp.Location = new System.Drawing.Point(248, 185);
            this.llblHelp.Name = "llblHelp";
            this.llblHelp.Size = new System.Drawing.Size(61, 19);
            this.llblHelp.TabIndex = 42;
            this.llblHelp.TabStop = true;
            this.llblHelp.Text = "帮助链接";
            this.llblHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlblHelp_LinkClicked);
            // 
            // txtModelPath
            // 
            this.txtModelPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtModelPath.Location = new System.Drawing.Point(316, 147);
            this.txtModelPath.Name = "txtModelPath";
            this.txtModelPath.Size = new System.Drawing.Size(459, 25);
            this.txtModelPath.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 36;
            this.label1.Text = "模型地址";
            // 
            // FrmEditCategory
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(950, 570);
            this.Controls.Add(this.llblHelp);
            this.Controls.Add(this.lblParentDesc);
            this.Controls.Add(this.pbModelImage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnClearImage);
            this.Controls.Add(this.btnChooseImage);
            this.Controls.Add(this.btnEditCategory);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cobParentId);
            this.Controls.Add(this.txtCategoryId);
            this.Controls.Add(this.txtCategoryName);
            this.Controls.Add(this.txtKMLink);
            this.Controls.Add(this.txtSubType);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtModelPath);
            this.Controls.Add(this.txtCategoryDesc);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(950, 568);
            this.Name = "FrmEditCategory";
            this.Text = "修改分类";
            ((System.ComponentModel.ISupportInitialize)(this.pbModelImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblParentDesc;
        private System.Windows.Forms.PictureBox pbModelImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClearImage;
        private System.Windows.Forms.Button btnChooseImage;
        private System.Windows.Forms.Button btnEditCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cobParentId;
        private System.Windows.Forms.TextBox txtCategoryId;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.TextBox txtKMLink;
        private System.Windows.Forms.TextBox txtSubType;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtCategoryDesc;
        private System.Windows.Forms.LinkLabel llblHelp;
        private System.Windows.Forms.TextBox txtModelPath;
        private System.Windows.Forms.Label label1;
    }
}