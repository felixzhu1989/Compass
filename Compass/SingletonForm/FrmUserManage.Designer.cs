namespace Compass
{
    partial class FrmUserManage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddUserGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserAccount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cobGroupName = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnAddUserGroup = new System.Windows.Forms.Button();
            this.grbAddUserGroup = new System.Windows.Forms.GroupBox();
            this.grbEditUser = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEditUserPwd = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEditEmail = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEditContact = new System.Windows.Forms.TextBox();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.btnEditUser = new System.Windows.Forms.Button();
            this.txtEditUserAccount = new System.Windows.Forms.TextBox();
            this.lblEditGroupName = new System.Windows.Forms.Label();
            this.cobEditGroupName = new System.Windows.Forms.ComboBox();
            this.grbAddUser = new System.Windows.Forms.GroupBox();
            this.UserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserPwd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserGroupId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Contact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.grbAddUserGroup.SuspendLayout();
            this.grbEditUser.SuspendLayout();
            this.grbAddUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户与分组管理";
            // 
            // dgvUser
            // 
            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure;
            this.dgvUser.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUser.BackgroundColor = System.Drawing.Color.White;
            this.dgvUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserId,
            this.UserAccount,
            this.UserPwd,
            this.GroupName,
            this.UserGroupId,
            this.Email,
            this.Contact});
            this.dgvUser.ContextMenuStrip = this.contextMenuStrip;
            this.dgvUser.EnableHeadersVisualStyles = false;
            this.dgvUser.Location = new System.Drawing.Point(12, 164);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.ReadOnly = true;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(926, 392);
            this.dgvUser.TabIndex = 8;
            this.dgvUser.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvUser_CellDoubleClick);
            this.dgvUser.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DgvUser_RowPostPaint);
            this.dgvUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvUser_KeyDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditUser,
            this.tsmiDeleteUser,
            this.tsmiAddUserGroup});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(186, 70);
            // 
            // tsmiEditUser
            // 
            this.tsmiEditUser.Name = "tsmiEditUser";
            this.tsmiEditUser.Size = new System.Drawing.Size(185, 22);
            this.tsmiEditUser.Text = "修改用户";
            this.tsmiEditUser.Click += new System.EventHandler(this.TsmiEditUser_Click);
            // 
            // tsmiDeleteUser
            // 
            this.tsmiDeleteUser.Name = "tsmiDeleteUser";
            this.tsmiDeleteUser.Size = new System.Drawing.Size(185, 22);
            this.tsmiDeleteUser.Text = "删除用户";
            this.tsmiDeleteUser.Click += new System.EventHandler(this.TsmiDeleteUser_Click);
            // 
            // tsmiAddUserGroup
            // 
            this.tsmiAddUserGroup.Name = "tsmiAddUserGroup";
            this.tsmiAddUserGroup.Size = new System.Drawing.Size(185, 22);
            this.tsmiAddUserGroup.Text = "添加分组(显示/隐藏)";
            this.tsmiAddUserGroup.Click += new System.EventHandler(this.TsmiAddUserGroup_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "用户账号";
            // 
            // txtUserAccount
            // 
            this.txtUserAccount.Location = new System.Drawing.Point(86, 25);
            this.txtUserAccount.Name = "txtUserAccount";
            this.txtUserAccount.Size = new System.Drawing.Size(150, 25);
            this.txtUserAccount.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(236, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "用户密码";
            // 
            // txtUserPwd
            // 
            this.txtUserPwd.Location = new System.Drawing.Point(304, 25);
            this.txtUserPwd.Name = "txtUserPwd";
            this.txtUserPwd.Size = new System.Drawing.Size(150, 25);
            this.txtUserPwd.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(454, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "用户分组";
            // 
            // cobGroupName
            // 
            this.cobGroupName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobGroupName.FormattingEnabled = true;
            this.cobGroupName.Location = new System.Drawing.Point(522, 25);
            this.cobGroupName.Name = "cobGroupName";
            this.cobGroupName.Size = new System.Drawing.Size(150, 27);
            this.cobGroupName.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 3;
            this.label5.Text = "用户邮箱";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(236, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 19);
            this.label6.TabIndex = 3;
            this.label6.Text = "联系方式";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(86, 61);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(150, 25);
            this.txtEmail.TabIndex = 3;
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(304, 61);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(150, 25);
            this.txtContact.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 19);
            this.label7.TabIndex = 3;
            this.label7.Text = "分组名称";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGroupName.Location = new System.Drawing.Point(78, 23);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(150, 25);
            this.txtGroupName.TabIndex = 6;
            // 
            // btnAddUser
            // 
            this.btnAddUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAddUser.FlatAppearance.BorderSize = 0;
            this.btnAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddUser.ForeColor = System.Drawing.Color.White;
            this.btnAddUser.Location = new System.Drawing.Point(522, 58);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(150, 28);
            this.btnAddUser.TabIndex = 5;
            this.btnAddUser.Text = "添加用户";
            this.btnAddUser.UseVisualStyleBackColor = false;
            this.btnAddUser.Click += new System.EventHandler(this.BtnAddUser_Click);
            // 
            // btnAddUserGroup
            // 
            this.btnAddUserGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddUserGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAddUserGroup.FlatAppearance.BorderSize = 0;
            this.btnAddUserGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddUserGroup.ForeColor = System.Drawing.Color.White;
            this.btnAddUserGroup.Location = new System.Drawing.Point(78, 58);
            this.btnAddUserGroup.Name = "btnAddUserGroup";
            this.btnAddUserGroup.Size = new System.Drawing.Size(150, 28);
            this.btnAddUserGroup.TabIndex = 7;
            this.btnAddUserGroup.Text = "添加分组";
            this.btnAddUserGroup.UseVisualStyleBackColor = false;
            this.btnAddUserGroup.Click += new System.EventHandler(this.BtnAddUserGroup_Click);
            // 
            // grbAddUserGroup
            // 
            this.grbAddUserGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbAddUserGroup.Controls.Add(this.label7);
            this.grbAddUserGroup.Controls.Add(this.txtGroupName);
            this.grbAddUserGroup.Controls.Add(this.btnAddUserGroup);
            this.grbAddUserGroup.Location = new System.Drawing.Point(699, 34);
            this.grbAddUserGroup.Name = "grbAddUserGroup";
            this.grbAddUserGroup.Size = new System.Drawing.Size(239, 100);
            this.grbAddUserGroup.TabIndex = 9;
            this.grbAddUserGroup.TabStop = false;
            this.grbAddUserGroup.Text = "添加分组";
            // 
            // grbEditUser
            // 
            this.grbEditUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbEditUser.Controls.Add(this.label13);
            this.grbEditUser.Controls.Add(this.label12);
            this.grbEditUser.Controls.Add(this.label11);
            this.grbEditUser.Controls.Add(this.txtEditUserPwd);
            this.grbEditUser.Controls.Add(this.label10);
            this.grbEditUser.Controls.Add(this.txtEditEmail);
            this.grbEditUser.Controls.Add(this.label9);
            this.grbEditUser.Controls.Add(this.txtEditContact);
            this.grbEditUser.Controls.Add(this.txtUserId);
            this.grbEditUser.Controls.Add(this.btnEditUser);
            this.grbEditUser.Controls.Add(this.txtEditUserAccount);
            this.grbEditUser.Controls.Add(this.lblEditGroupName);
            this.grbEditUser.Controls.Add(this.cobEditGroupName);
            this.grbEditUser.Location = new System.Drawing.Point(10, 467);
            this.grbEditUser.Name = "grbEditUser";
            this.grbEditUser.Size = new System.Drawing.Size(928, 92);
            this.grbEditUser.TabIndex = 10;
            this.grbEditUser.TabStop = false;
            this.grbEditUser.Text = "修改用户";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(685, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 19);
            this.label13.TabIndex = 3;
            this.label13.Text = "用户序号";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 19);
            this.label12.TabIndex = 3;
            this.label12.Text = "用户账号";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 19);
            this.label11.TabIndex = 3;
            this.label11.Text = "用户邮箱";
            // 
            // txtEditUserPwd
            // 
            this.txtEditUserPwd.Location = new System.Drawing.Point(292, 21);
            this.txtEditUserPwd.Name = "txtEditUserPwd";
            this.txtEditUserPwd.Size = new System.Drawing.Size(150, 25);
            this.txtEditUserPwd.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(224, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 19);
            this.label10.TabIndex = 3;
            this.label10.Text = "用户密码";
            // 
            // txtEditEmail
            // 
            this.txtEditEmail.Location = new System.Drawing.Point(74, 57);
            this.txtEditEmail.Name = "txtEditEmail";
            this.txtEditEmail.Size = new System.Drawing.Size(150, 25);
            this.txtEditEmail.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(224, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 19);
            this.label9.TabIndex = 3;
            this.label9.Text = "联系方式";
            // 
            // txtEditContact
            // 
            this.txtEditContact.Location = new System.Drawing.Point(292, 57);
            this.txtEditContact.Name = "txtEditContact";
            this.txtEditContact.Size = new System.Drawing.Size(150, 25);
            this.txtEditContact.TabIndex = 4;
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(753, 22);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.ReadOnly = true;
            this.txtUserId.Size = new System.Drawing.Size(150, 25);
            this.txtUserId.TabIndex = 0;
            // 
            // btnEditUser
            // 
            this.btnEditUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnEditUser.FlatAppearance.BorderSize = 0;
            this.btnEditUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditUser.ForeColor = System.Drawing.Color.White;
            this.btnEditUser.Location = new System.Drawing.Point(753, 53);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(150, 28);
            this.btnEditUser.TabIndex = 5;
            this.btnEditUser.Text = "修改用户";
            this.btnEditUser.UseVisualStyleBackColor = false;
            this.btnEditUser.Click += new System.EventHandler(this.BtnEditUser_Click);
            // 
            // txtEditUserAccount
            // 
            this.txtEditUserAccount.Location = new System.Drawing.Point(74, 21);
            this.txtEditUserAccount.Name = "txtEditUserAccount";
            this.txtEditUserAccount.Size = new System.Drawing.Size(150, 25);
            this.txtEditUserAccount.TabIndex = 0;
            // 
            // lblEditGroupName
            // 
            this.lblEditGroupName.AutoSize = true;
            this.lblEditGroupName.Location = new System.Drawing.Point(442, 23);
            this.lblEditGroupName.Name = "lblEditGroupName";
            this.lblEditGroupName.Size = new System.Drawing.Size(61, 19);
            this.lblEditGroupName.TabIndex = 3;
            this.lblEditGroupName.Text = "用户分组";
            // 
            // cobEditGroupName
            // 
            this.cobEditGroupName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobEditGroupName.FormattingEnabled = true;
            this.cobEditGroupName.Location = new System.Drawing.Point(510, 21);
            this.cobEditGroupName.Name = "cobEditGroupName";
            this.cobEditGroupName.Size = new System.Drawing.Size(150, 27);
            this.cobEditGroupName.TabIndex = 2;
            // 
            // grbAddUser
            // 
            this.grbAddUser.Controls.Add(this.label2);
            this.grbAddUser.Controls.Add(this.label5);
            this.grbAddUser.Controls.Add(this.label3);
            this.grbAddUser.Controls.Add(this.label6);
            this.grbAddUser.Controls.Add(this.btnAddUser);
            this.grbAddUser.Controls.Add(this.label4);
            this.grbAddUser.Controls.Add(this.cobGroupName);
            this.grbAddUser.Controls.Add(this.txtUserAccount);
            this.grbAddUser.Controls.Add(this.txtContact);
            this.grbAddUser.Controls.Add(this.txtEmail);
            this.grbAddUser.Controls.Add(this.txtUserPwd);
            this.grbAddUser.Location = new System.Drawing.Point(10, 34);
            this.grbAddUser.Name = "grbAddUser";
            this.grbAddUser.Size = new System.Drawing.Size(683, 100);
            this.grbAddUser.TabIndex = 10;
            this.grbAddUser.TabStop = false;
            this.grbAddUser.Text = "添加用户";
            // 
            // UserId
            // 
            this.UserId.DataPropertyName = "UserId";
            this.UserId.HeaderText = "序号";
            this.UserId.Name = "UserId";
            this.UserId.ReadOnly = true;
            this.UserId.Width = 60;
            // 
            // UserAccount
            // 
            this.UserAccount.DataPropertyName = "UserAccount";
            this.UserAccount.HeaderText = "用户账号";
            this.UserAccount.Name = "UserAccount";
            this.UserAccount.ReadOnly = true;
            this.UserAccount.Width = 150;
            // 
            // UserPwd
            // 
            this.UserPwd.DataPropertyName = "UserPwd";
            this.UserPwd.HeaderText = "用户密码";
            this.UserPwd.Name = "UserPwd";
            this.UserPwd.ReadOnly = true;
            this.UserPwd.Width = 150;
            // 
            // GroupName
            // 
            this.GroupName.DataPropertyName = "GroupName";
            this.GroupName.HeaderText = "用户分组";
            this.GroupName.Name = "GroupName";
            this.GroupName.ReadOnly = true;
            // 
            // UserGroupId
            // 
            this.UserGroupId.DataPropertyName = "UserGroupId";
            this.UserGroupId.HeaderText = "分组号";
            this.UserGroupId.Name = "UserGroupId";
            this.UserGroupId.ReadOnly = true;
            // 
            // Email
            // 
            this.Email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Email.DataPropertyName = "Email";
            this.Email.HeaderText = "用户邮箱";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            // 
            // Contact
            // 
            this.Contact.DataPropertyName = "Contact";
            this.Contact.HeaderText = "联系方式";
            this.Contact.Name = "Contact";
            this.Contact.ReadOnly = true;
            this.Contact.Width = 150;
            // 
            // FrmUserManage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 568);
            this.Controls.Add(this.grbAddUser);
            this.Controls.Add(this.grbEditUser);
            this.Controls.Add(this.grbAddUserGroup);
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUserManage";
            this.Text = "用户与分组管理";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.grbAddUserGroup.ResumeLayout(false);
            this.grbAddUserGroup.PerformLayout();
            this.grbEditUser.ResumeLayout(false);
            this.grbEditUser.PerformLayout();
            this.grbAddUser.ResumeLayout(false);
            this.grbAddUser.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvUser;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditUser;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserAccount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserPwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cobGroupName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnAddUserGroup;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddUserGroup;
        private System.Windows.Forms.GroupBox grbAddUserGroup;
        private System.Windows.Forms.GroupBox grbEditUser;
        private System.Windows.Forms.GroupBox grbAddUser;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtEditUserPwd;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtEditEmail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEditContact;
        private System.Windows.Forms.Button btnEditUser;
        private System.Windows.Forms.TextBox txtEditUserAccount;
        private System.Windows.Forms.Label lblEditGroupName;
        private System.Windows.Forms.ComboBox cobEditGroupName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserPwd;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserGroupId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contact;
    }
}