﻿namespace Compass
{
    partial class FrmNocjspec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNocjspec));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cobRightBeamType = new System.Windows.Forms.ComboBox();
            this.cobLeftBeamType = new System.Windows.Forms.ComboBox();
            this.cobBackCJSide = new System.Windows.Forms.ComboBox();
            this.txtRightBeamDis = new System.Windows.Forms.TextBox();
            this.txtLeftBeamDis = new System.Windows.Forms.TextBox();
            this.txtLeftDis = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblRightBeamDis = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblLeftBeamDis = new System.Windows.Forms.Label();
            this.txtRightDis = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblGutterWidth = new System.Windows.Forms.Label();
            this.cobGutterSide = new System.Windows.Forms.ComboBox();
            this.txtGutterWidth = new System.Windows.Forms.TextBox();
            this.cobLKSide = new System.Windows.Forms.ComboBox();
            this.btnEditData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBackBend = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTopBend = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.cobSidePanel = new System.Windows.Forms.ComboBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.modelView = new Compass.ModelView();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cobRightBeamType);
            this.groupBox2.Controls.Add(this.cobLeftBeamType);
            this.groupBox2.Controls.Add(this.cobBackCJSide);
            this.groupBox2.Controls.Add(this.txtRightBeamDis);
            this.groupBox2.Controls.Add(this.txtLeftBeamDis);
            this.groupBox2.Controls.Add(this.txtLeftDis);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.lblRightBeamDis);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.lblLeftBeamDis);
            this.groupBox2.Controls.Add(this.txtRightDis);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(779, 316);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(398, 192);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "射钉参数";
            // 
            // cobRightBeamType
            // 
            this.cobRightBeamType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobRightBeamType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobRightBeamType.BackColor = System.Drawing.Color.Azure;
            this.cobRightBeamType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobRightBeamType.FormattingEnabled = true;
            this.cobRightBeamType.Location = new System.Drawing.Point(288, 96);
            this.cobRightBeamType.Name = "cobRightBeamType";
            this.cobRightBeamType.Size = new System.Drawing.Size(100, 27);
            this.cobRightBeamType.TabIndex = 5;
            this.cobRightBeamType.SelectedIndexChanged += new System.EventHandler(this.cobRightBeamType_SelectedIndexChanged);
            // 
            // cobLeftBeamType
            // 
            this.cobLeftBeamType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobLeftBeamType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobLeftBeamType.BackColor = System.Drawing.Color.Azure;
            this.cobLeftBeamType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobLeftBeamType.FormattingEnabled = true;
            this.cobLeftBeamType.Location = new System.Drawing.Point(288, 21);
            this.cobLeftBeamType.Name = "cobLeftBeamType";
            this.cobLeftBeamType.Size = new System.Drawing.Size(100, 27);
            this.cobLeftBeamType.TabIndex = 3;
            this.cobLeftBeamType.SelectedIndexChanged += new System.EventHandler(this.cobLeftBeamType_SelectedIndexChanged);
            // 
            // cobBackCJSide
            // 
            this.cobBackCJSide.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobBackCJSide.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobBackCJSide.BackColor = System.Drawing.Color.Azure;
            this.cobBackCJSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobBackCJSide.FormattingEnabled = true;
            this.cobBackCJSide.Location = new System.Drawing.Point(91, 24);
            this.cobBackCJSide.Name = "cobBackCJSide";
            this.cobBackCJSide.Size = new System.Drawing.Size(100, 27);
            this.cobBackCJSide.TabIndex = 0;
            // 
            // txtRightBeamDis
            // 
            this.txtRightBeamDis.BackColor = System.Drawing.Color.Azure;
            this.txtRightBeamDis.Location = new System.Drawing.Point(288, 135);
            this.txtRightBeamDis.Name = "txtRightBeamDis";
            this.txtRightBeamDis.Size = new System.Drawing.Size(100, 25);
            this.txtRightBeamDis.TabIndex = 6;
            // 
            // txtLeftBeamDis
            // 
            this.txtLeftBeamDis.BackColor = System.Drawing.Color.Azure;
            this.txtLeftBeamDis.Location = new System.Drawing.Point(288, 60);
            this.txtLeftBeamDis.Name = "txtLeftBeamDis";
            this.txtLeftBeamDis.Size = new System.Drawing.Size(100, 25);
            this.txtLeftBeamDis.TabIndex = 4;
            // 
            // txtLeftDis
            // 
            this.txtLeftDis.BackColor = System.Drawing.Color.Azure;
            this.txtLeftDis.Location = new System.Drawing.Point(91, 63);
            this.txtLeftDis.Name = "txtLeftDis";
            this.txtLeftDis.Size = new System.Drawing.Size(100, 25);
            this.txtLeftDis.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(208, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 19);
            this.label12.TabIndex = 2;
            this.label12.Text = "右排风类型";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(208, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 19);
            this.label10.TabIndex = 2;
            this.label10.Text = "左排风类型";
            // 
            // lblRightBeamDis
            // 
            this.lblRightBeamDis.AutoSize = true;
            this.lblRightBeamDis.Location = new System.Drawing.Point(199, 138);
            this.lblRightBeamDis.Name = "lblRightBeamDis";
            this.lblRightBeamDis.Size = new System.Drawing.Size(87, 19);
            this.lblRightBeamDis.TabIndex = 2;
            this.lblRightBeamDis.Text = "双排前端距左";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 19);
            this.label14.TabIndex = 2;
            this.label14.Text = "BCJ位置";
            // 
            // lblLeftBeamDis
            // 
            this.lblLeftBeamDis.AutoSize = true;
            this.lblLeftBeamDis.Location = new System.Drawing.Point(199, 63);
            this.lblLeftBeamDis.Name = "lblLeftBeamDis";
            this.lblLeftBeamDis.Size = new System.Drawing.Size(87, 19);
            this.lblLeftBeamDis.TabIndex = 2;
            this.lblLeftBeamDis.Text = "双排前端距右";
            // 
            // txtRightDis
            // 
            this.txtRightDis.BackColor = System.Drawing.Color.Azure;
            this.txtRightDis.Location = new System.Drawing.Point(91, 96);
            this.txtRightDis.Name = "txtRightDis";
            this.txtRightDis.Size = new System.Drawing.Size(100, 25);
            this.txtRightDis.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "左空白";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label9.Location = new System.Drawing.Point(11, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(183, 19);
            this.label9.TabIndex = 2;
            this.label9.Text = "如果NOCJ左右没有凸出则为0";
            this.label9.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "右空白";
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.lblGutterWidth);
            this.groupBox7.Controls.Add(this.cobGutterSide);
            this.groupBox7.Controls.Add(this.txtGutterWidth);
            this.groupBox7.Controls.Add(this.cobLKSide);
            this.groupBox7.Location = new System.Drawing.Point(779, 517);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(398, 135);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "其他配置";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 19);
            this.label8.TabIndex = 2;
            this.label8.Text = "GUTTER位置";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 19);
            this.label6.TabIndex = 2;
            this.label6.Text = "LK位置";
            // 
            // lblGutterWidth
            // 
            this.lblGutterWidth.AutoSize = true;
            this.lblGutterWidth.Location = new System.Drawing.Point(5, 97);
            this.lblGutterWidth.Name = "lblGutterWidth";
            this.lblGutterWidth.Size = new System.Drawing.Size(84, 19);
            this.lblGutterWidth.TabIndex = 2;
            this.lblGutterWidth.Text = "GUTTER宽度";
            // 
            // cobGutterSide
            // 
            this.cobGutterSide.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobGutterSide.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobGutterSide.BackColor = System.Drawing.Color.Azure;
            this.cobGutterSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobGutterSide.FormattingEnabled = true;
            this.cobGutterSide.Location = new System.Drawing.Point(91, 58);
            this.cobGutterSide.Name = "cobGutterSide";
            this.cobGutterSide.Size = new System.Drawing.Size(100, 27);
            this.cobGutterSide.TabIndex = 1;
            this.cobGutterSide.SelectedIndexChanged += new System.EventHandler(this.cobGutterSide_SelectedIndexChanged);
            // 
            // txtGutterWidth
            // 
            this.txtGutterWidth.BackColor = System.Drawing.Color.Azure;
            this.txtGutterWidth.Location = new System.Drawing.Point(91, 94);
            this.txtGutterWidth.Name = "txtGutterWidth";
            this.txtGutterWidth.Size = new System.Drawing.Size(100, 25);
            this.txtGutterWidth.TabIndex = 2;
            // 
            // cobLKSide
            // 
            this.cobLKSide.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobLKSide.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobLKSide.BackColor = System.Drawing.Color.Azure;
            this.cobLKSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobLKSide.FormattingEnabled = true;
            this.cobLKSide.Location = new System.Drawing.Point(91, 22);
            this.cobLKSide.Name = "cobLKSide";
            this.cobLKSide.Size = new System.Drawing.Size(100, 27);
            this.cobLKSide.TabIndex = 0;
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
            this.btnEditData.TabIndex = 3;
            this.btnEditData.Text = "修改参数";
            this.btnEditData.UseVisualStyleBackColor = false;
            this.btnEditData.Click += new System.EventHandler(this.btnEditData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtBackBend);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTopBend);
            this.groupBox1.Controls.Add(this.txtWidth);
            this.groupBox1.Controls.Add(this.cobSidePanel);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(24, 517);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 135);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "NOCJ高度300";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(380, 68);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 19);
            this.label16.TabIndex = 2;
            this.label16.Text = "背部翻边";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(400, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 19);
            this.label11.TabIndex = 2;
            this.label11.Text = "高度";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(203, 67);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 19);
            this.label15.TabIndex = 2;
            this.label15.Text = "顶部翻边";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(194, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "NOCJ腔宽度";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label19.Location = new System.Drawing.Point(203, 86);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 19);
            this.label19.TabIndex = 3;
            this.label19.Text = "15/...";
            this.label19.Visible = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label18.Location = new System.Drawing.Point(379, 89);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(70, 19);
            this.label18.TabIndex = 3;
            this.label18.Text = "40/110/...";
            this.label18.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label17.Location = new System.Drawing.Point(390, 44);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 19);
            this.label17.TabIndex = 3;
            this.label17.Text = "310/...";
            this.label17.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label13.Location = new System.Drawing.Point(194, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 19);
            this.label13.TabIndex = 3;
            this.label13.Text = "90/45";
            this.label13.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "NOCJ腔侧板";
            // 
            // txtBackBend
            // 
            this.txtBackBend.BackColor = System.Drawing.Color.Azure;
            this.txtBackBend.Location = new System.Drawing.Point(441, 64);
            this.txtBackBend.Name = "txtBackBend";
            this.txtBackBend.Size = new System.Drawing.Size(100, 25);
            this.txtBackBend.TabIndex = 5;
            // 
            // txtHeight
            // 
            this.txtHeight.BackColor = System.Drawing.Color.Azure;
            this.txtHeight.Location = new System.Drawing.Point(441, 26);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(100, 25);
            this.txtHeight.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "NOCJ腔长度";
            // 
            // txtTopBend
            // 
            this.txtTopBend.BackColor = System.Drawing.Color.Azure;
            this.txtTopBend.Location = new System.Drawing.Point(279, 64);
            this.txtTopBend.Name = "txtTopBend";
            this.txtTopBend.Size = new System.Drawing.Size(100, 25);
            this.txtTopBend.TabIndex = 4;
            // 
            // txtWidth
            // 
            this.txtWidth.BackColor = System.Drawing.Color.Azure;
            this.txtWidth.Location = new System.Drawing.Point(280, 26);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(100, 25);
            this.txtWidth.TabIndex = 1;
            // 
            // cobSidePanel
            // 
            this.cobSidePanel.AllowDrop = true;
            this.cobSidePanel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobSidePanel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobSidePanel.BackColor = System.Drawing.Color.Azure;
            this.cobSidePanel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobSidePanel.FormattingEnabled = true;
            this.cobSidePanel.Location = new System.Drawing.Point(91, 64);
            this.cobSidePanel.Name = "cobSidePanel";
            this.cobSidePanel.Size = new System.Drawing.Size(100, 27);
            this.cobSidePanel.TabIndex = 3;
            // 
            // txtLength
            // 
            this.txtLength.BackColor = System.Drawing.Color.Azure;
            this.txtLength.Location = new System.Drawing.Point(91, 26);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(100, 25);
            this.txtLength.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label7.Location = new System.Drawing.Point(4, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(254, 19);
            this.label7.TabIndex = 2;
            this.label7.Text = "注意背面有NOCJ的情况选择NOCJBL/R/B";
            this.label7.Visible = false;
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
            // FrmNOCJSPEC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.modelView);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.btnEditData);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1200, 675);
            this.Name = "FrmNocjspec";
            this.Text = "NOCJSPEC";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cobRightBeamType;
        private System.Windows.Forms.ComboBox cobLeftBeamType;
        private System.Windows.Forms.ComboBox cobBackCJSide;
        private System.Windows.Forms.TextBox txtRightBeamDis;
        private System.Windows.Forms.TextBox txtLeftBeamDis;
        private System.Windows.Forms.TextBox txtLeftDis;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblRightBeamDis;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblLeftBeamDis;
        private System.Windows.Forms.TextBox txtRightDis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblGutterWidth;
        private System.Windows.Forms.ComboBox cobGutterSide;
        private System.Windows.Forms.TextBox txtGutterWidth;
        private System.Windows.Forms.ComboBox cobLKSide;
        private System.Windows.Forms.Button btnEditData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.ComboBox cobSidePanel;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtBackBend;
        private System.Windows.Forms.TextBox txtTopBend;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private ModelView modelView;
    }
}