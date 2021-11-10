namespace Compass
{
    partial class FrmHme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHme));
            this.btnEditData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOutletHeight = new System.Windows.Forms.TextBox();
            this.txtOutletDia = new System.Windows.Forms.TextBox();
            this.txtInletDia = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cobTemperatureSwitch = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cobNamePlate = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cobHangPosition = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cobWindPressure = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cobHeater = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cobNetPlug = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cobPowerPlug = new System.Windows.Forms.ComboBox();
            this.txtPowerPlugDis = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cobPlugPosition = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.btnEditData.Location = new System.Drawing.Point(651, 616);
            this.btnEditData.Name = "btnEditData";
            this.btnEditData.Size = new System.Drawing.Size(122, 36);
            this.btnEditData.TabIndex = 15;
            this.btnEditData.Text = "修改参数";
            this.btnEditData.UseVisualStyleBackColor = false;
            this.btnEditData.Click += new System.EventHandler(this.BtnEditData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtOutletHeight);
            this.groupBox1.Controls.Add(this.txtOutletDia);
            this.groupBox1.Controls.Add(this.txtInletDia);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtWidth);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Location = new System.Drawing.Point(24, 517);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 135);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "暖箱外框尺寸";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(116, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "出风脖颈高度";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 4;
            this.label5.Text = "出风脖颈";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "进风脖颈";
            // 
            // txtOutletHeight
            // 
            this.txtOutletHeight.BackColor = System.Drawing.Color.Azure;
            this.txtOutletHeight.Location = new System.Drawing.Point(190, 93);
            this.txtOutletHeight.Name = "txtOutletHeight";
            this.txtOutletHeight.Size = new System.Drawing.Size(76, 25);
            this.txtOutletHeight.TabIndex = 3;
            // 
            // txtOutletDia
            // 
            this.txtOutletDia.BackColor = System.Drawing.Color.Azure;
            this.txtOutletDia.Location = new System.Drawing.Point(192, 57);
            this.txtOutletDia.Name = "txtOutletDia";
            this.txtOutletDia.Size = new System.Drawing.Size(76, 25);
            this.txtOutletDia.TabIndex = 3;
            // 
            // txtInletDia
            // 
            this.txtInletDia.BackColor = System.Drawing.Color.Azure;
            this.txtInletDia.Location = new System.Drawing.Point(192, 24);
            this.txtInletDia.Name = "txtInletDia";
            this.txtInletDia.Size = new System.Drawing.Size(76, 25);
            this.txtInletDia.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "长度";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "高度";
            // 
            // txtHeight
            // 
            this.txtHeight.BackColor = System.Drawing.Color.Azure;
            this.txtHeight.Location = new System.Drawing.Point(48, 93);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(58, 25);
            this.txtHeight.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "宽度";
            // 
            // txtWidth
            // 
            this.txtWidth.BackColor = System.Drawing.Color.Azure;
            this.txtWidth.Location = new System.Drawing.Point(48, 57);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(58, 25);
            this.txtWidth.TabIndex = 1;
            this.txtWidth.Text = "500";
            // 
            // txtLength
            // 
            this.txtLength.BackColor = System.Drawing.Color.Azure;
            this.txtLength.Location = new System.Drawing.Point(48, 24);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(58, 25);
            this.txtLength.TabIndex = 0;
            this.txtLength.Text = "600";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.cobTemperatureSwitch);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.cobNamePlate);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.cobHangPosition);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.cobWindPressure);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.cobHeater);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Location = new System.Drawing.Point(325, 520);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(320, 132);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "辅件配置";
            // 
            // cobTemperatureSwitch
            // 
            this.cobTemperatureSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cobTemperatureSwitch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobTemperatureSwitch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobTemperatureSwitch.BackColor = System.Drawing.Color.Azure;
            this.cobTemperatureSwitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobTemperatureSwitch.FormattingEnabled = true;
            this.cobTemperatureSwitch.Location = new System.Drawing.Point(239, 93);
            this.cobTemperatureSwitch.Name = "cobTemperatureSwitch";
            this.cobTemperatureSwitch.Size = new System.Drawing.Size(65, 27);
            this.cobTemperatureSwitch.TabIndex = 18;
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(172, 98);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(61, 19);
            this.label21.TabIndex = 19;
            this.label21.Text = "复位开关";
            // 
            // cobNamePlate
            // 
            this.cobNamePlate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cobNamePlate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobNamePlate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobNamePlate.BackColor = System.Drawing.Color.Azure;
            this.cobNamePlate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobNamePlate.FormattingEnabled = true;
            this.cobNamePlate.Location = new System.Drawing.Point(213, 25);
            this.cobNamePlate.Name = "cobNamePlate";
            this.cobNamePlate.Size = new System.Drawing.Size(91, 27);
            this.cobNamePlate.TabIndex = 16;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(172, 28);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(35, 19);
            this.label20.TabIndex = 17;
            this.label20.Text = "铭牌";
            // 
            // cobHangPosition
            // 
            this.cobHangPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cobHangPosition.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobHangPosition.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobHangPosition.BackColor = System.Drawing.Color.Azure;
            this.cobHangPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobHangPosition.FormattingEnabled = true;
            this.cobHangPosition.Location = new System.Drawing.Point(75, 95);
            this.cobHangPosition.Name = "cobHangPosition";
            this.cobHangPosition.Size = new System.Drawing.Size(73, 27);
            this.cobHangPosition.TabIndex = 14;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(6, 101);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(61, 19);
            this.label19.TabIndex = 15;
            this.label19.Text = "吊脚位置";
            // 
            // cobWindPressure
            // 
            this.cobWindPressure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cobWindPressure.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobWindPressure.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobWindPressure.BackColor = System.Drawing.Color.Azure;
            this.cobWindPressure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobWindPressure.FormattingEnabled = true;
            this.cobWindPressure.Location = new System.Drawing.Point(75, 63);
            this.cobWindPressure.Name = "cobWindPressure";
            this.cobWindPressure.Size = new System.Drawing.Size(73, 27);
            this.cobWindPressure.TabIndex = 12;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(21, 66);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 19);
            this.label18.TabIndex = 13;
            this.label18.Text = "风压管";
            // 
            // cobHeater
            // 
            this.cobHeater.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cobHeater.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobHeater.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobHeater.BackColor = System.Drawing.Color.Azure;
            this.cobHeater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobHeater.FormattingEnabled = true;
            this.cobHeater.Location = new System.Drawing.Point(75, 28);
            this.cobHeater.Name = "cobHeater";
            this.cobHeater.Size = new System.Drawing.Size(73, 27);
            this.cobHeater.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(21, 31);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 19);
            this.label17.TabIndex = 11;
            this.label17.Text = "电加热";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cobNetPlug);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cobPowerPlug);
            this.groupBox2.Controls.Add(this.txtPowerPlugDis);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.cobPlugPosition);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(818, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(359, 208);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "电源、网线插口配置";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(60, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 19);
            this.label7.TabIndex = 19;
            this.label7.Text = "插口距离";
            // 
            // cobNetPlug
            // 
            this.cobNetPlug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cobNetPlug.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobNetPlug.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobNetPlug.BackColor = System.Drawing.Color.Azure;
            this.cobNetPlug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobNetPlug.FormattingEnabled = true;
            this.cobNetPlug.Location = new System.Drawing.Point(127, 128);
            this.cobNetPlug.Name = "cobNetPlug";
            this.cobNetPlug.Size = new System.Drawing.Size(73, 27);
            this.cobNetPlug.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(60, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 19);
            this.label9.TabIndex = 15;
            this.label9.Text = "网线插口";
            // 
            // cobPowerPlug
            // 
            this.cobPowerPlug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cobPowerPlug.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobPowerPlug.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobPowerPlug.BackColor = System.Drawing.Color.Azure;
            this.cobPowerPlug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobPowerPlug.FormattingEnabled = true;
            this.cobPowerPlug.Location = new System.Drawing.Point(127, 61);
            this.cobPowerPlug.Name = "cobPowerPlug";
            this.cobPowerPlug.Size = new System.Drawing.Size(73, 27);
            this.cobPowerPlug.TabIndex = 12;
            // 
            // txtPowerPlugDis
            // 
            this.txtPowerPlugDis.BackColor = System.Drawing.Color.Azure;
            this.txtPowerPlugDis.Location = new System.Drawing.Point(127, 97);
            this.txtPowerPlugDis.Name = "txtPowerPlugDis";
            this.txtPowerPlugDis.Size = new System.Drawing.Size(76, 25);
            this.txtPowerPlugDis.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(60, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 19);
            this.label10.TabIndex = 13;
            this.label10.Text = "电源插口";
            // 
            // cobPlugPosition
            // 
            this.cobPlugPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cobPlugPosition.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cobPlugPosition.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobPlugPosition.BackColor = System.Drawing.Color.Azure;
            this.cobPlugPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobPlugPosition.FormattingEnabled = true;
            this.cobPlugPosition.Location = new System.Drawing.Point(127, 28);
            this.cobPlugPosition.Name = "cobPlugPosition";
            this.cobPlugPosition.Size = new System.Drawing.Size(73, 27);
            this.cobPlugPosition.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(60, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 19);
            this.label12.TabIndex = 11;
            this.label12.Text = "插口位置";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(25, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 460);
            this.panel1.TabIndex = 22;
            // 
            // FrmHME
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnEditData);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1200, 675);
            this.Name = "FrmHme";
            this.Text = "HME";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        
        private System.Windows.Forms.Button btnEditData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOutletDia;
        private System.Windows.Forms.TextBox txtInletDia;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOutletHeight;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cobHeater;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cobTemperatureSwitch;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cobNamePlate;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cobHangPosition;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cobWindPressure;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cobNetPlug;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cobPowerPlug;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cobPlugPosition;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPowerPlugDis;
        private System.Windows.Forms.Panel panel1;
    }
}