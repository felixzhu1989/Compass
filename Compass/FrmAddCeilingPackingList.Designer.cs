namespace Compass
{
    partial class FrmAddCeilingPackingList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddCeilingPackingList));
            this.tvCeilingAccessories = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnAddCeilingAccessory = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtPartDescription = new System.Windows.Forms.TextBox();
            this.txtPartNo = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tvCeilingAccessories
            // 
            this.tvCeilingAccessories.AllowDrop = true;
            this.tvCeilingAccessories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvCeilingAccessories.BackColor = System.Drawing.Color.White;
            this.tvCeilingAccessories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvCeilingAccessories.ImageIndex = 0;
            this.tvCeilingAccessories.ImageList = this.imageList1;
            this.tvCeilingAccessories.Location = new System.Drawing.Point(23, 120);
            this.tvCeilingAccessories.Name = "tvCeilingAccessories";
            this.tvCeilingAccessories.SelectedImageIndex = 0;
            this.tvCeilingAccessories.Size = new System.Drawing.Size(634, 510);
            this.tvCeilingAccessories.TabIndex = 46;
            this.tvCeilingAccessories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvCeilingAccessories_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Categories.png");
            this.imageList1.Images.SetKeyName(1, "FolderClosed.png");
            this.imageList1.Images.SetKeyName(2, "FolderOpen.png");
            this.imageList1.Images.SetKeyName(3, "CategoryItem.png");
            this.imageList1.Images.SetKeyName(4, "Project.png");
            // 
            // btnAddCeilingAccessory
            // 
            this.btnAddCeilingAccessory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAddCeilingAccessory.FlatAppearance.BorderSize = 0;
            this.btnAddCeilingAccessory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCeilingAccessory.ForeColor = System.Drawing.Color.White;
            this.btnAddCeilingAccessory.Location = new System.Drawing.Point(520, 86);
            this.btnAddCeilingAccessory.Name = "btnAddCeilingAccessory";
            this.btnAddCeilingAccessory.Size = new System.Drawing.Size(137, 28);
            this.btnAddCeilingAccessory.TabIndex = 94;
            this.btnAddCeilingAccessory.Text = "添加配件";
            this.btnAddCeilingAccessory.UseVisualStyleBackColor = false;
            this.btnAddCeilingAccessory.Click += new System.EventHandler(this.BtnAddCeilingAccessory_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 95;
            this.label9.Text = "部件描述";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(252, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 96;
            this.label10.Text = "部件编号";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(428, 94);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 13);
            this.label13.TabIndex = 99;
            this.label13.Text = "高";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(343, 94);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 13);
            this.label14.TabIndex = 97;
            this.label14.Text = "宽";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(54, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 13);
            this.label12.TabIndex = 100;
            this.label12.Text = "数量";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(234, 94);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 13);
            this.label15.TabIndex = 101;
            this.label15.Text = "长";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(476, 66);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(31, 13);
            this.label19.TabIndex = 98;
            this.label19.Text = "备注";
            // 
            // txtPartDescription
            // 
            this.txtPartDescription.Location = new System.Drawing.Point(96, 63);
            this.txtPartDescription.Name = "txtPartDescription";
            this.txtPartDescription.ReadOnly = true;
            this.txtPartDescription.Size = new System.Drawing.Size(150, 20);
            this.txtPartDescription.TabIndex = 87;
            // 
            // txtPartNo
            // 
            this.txtPartNo.Location = new System.Drawing.Point(320, 63);
            this.txtPartNo.Name = "txtPartNo";
            this.txtPartNo.Size = new System.Drawing.Size(150, 20);
            this.txtPartNo.TabIndex = 88;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(456, 91);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(56, 20);
            this.txtHeight.TabIndex = 93;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(95, 91);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(46, 20);
            this.txtQuantity.TabIndex = 90;
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(370, 91);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(56, 20);
            this.txtWidth.TabIndex = 92;
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(261, 91);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(72, 20);
            this.txtLength.TabIndex = 91;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(520, 63);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(137, 20);
            this.txtRemark.TabIndex = 89;
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(147, 91);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.ReadOnly = true;
            this.txtUnit.Size = new System.Drawing.Size(47, 20);
            this.txtUnit.TabIndex = 87;
            // 
            // FrmAddCeilingPackingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 650);
            this.Controls.Add(this.btnAddCeilingAccessory);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.txtPartDescription);
            this.Controls.Add(this.txtPartNo);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.tvCeilingAccessories);
            this.Name = "FrmAddCeilingPackingList";
            this.Text = "添加天花烟罩配件";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvCeilingAccessories;
        private System.Windows.Forms.Button btnAddCeilingAccessory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtPartDescription;
        private System.Windows.Forms.TextBox txtPartNo;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtUnit;
    }
}