﻿namespace Compass
{
    partial class FrmDesignWorkload
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDesignWorkload = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditWorkload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsimDeleteWorkload = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtWorkloadValue = new System.Windows.Forms.TextBox();
            this.txtModelDesc = new System.Windows.Forms.TextBox();
            this.btnAddWorkload = new System.Windows.Forms.Button();
            this.grbEditWorkload = new System.Windows.Forms.GroupBox();
            this.btnEditWorkload = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEditModel = new System.Windows.Forms.TextBox();
            this.txtEditWorkloadId = new System.Windows.Forms.TextBox();
            this.txtEditWorkloadValue = new System.Windows.Forms.TextBox();
            this.txtEditModelDesc = new System.Windows.Forms.TextBox();
            this.WorkloadId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkloadValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesignWorkload)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.grbEditWorkload.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "设计工作量表";
            // 
            // dgvDesignWorkload
            // 
            this.dgvDesignWorkload.AllowUserToAddRows = false;
            this.dgvDesignWorkload.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Azure;
            this.dgvDesignWorkload.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDesignWorkload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDesignWorkload.BackgroundColor = System.Drawing.Color.White;
            this.dgvDesignWorkload.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(232)))), ((int)(((byte)(155)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDesignWorkload.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDesignWorkload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDesignWorkload.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WorkloadId,
            this.Model,
            this.WorkloadValue,
            this.ModelDesc});
            this.dgvDesignWorkload.ContextMenuStrip = this.contextMenuStrip;
            this.dgvDesignWorkload.EnableHeadersVisualStyles = false;
            this.dgvDesignWorkload.Location = new System.Drawing.Point(10, 105);
            this.dgvDesignWorkload.Name = "dgvDesignWorkload";
            this.dgvDesignWorkload.ReadOnly = true;
            this.dgvDesignWorkload.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDesignWorkload.Size = new System.Drawing.Size(928, 451);
            this.dgvDesignWorkload.TabIndex = 10;
            this.dgvDesignWorkload.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDesignWorkload_CellDoubleClick);
            this.dgvDesignWorkload.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvDesignWorkload_RowPostPaint);
            this.dgvDesignWorkload.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvDesignWorkload_KeyDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditWorkload,
            this.tsimDeleteWorkload});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(140, 48);
            // 
            // tsmiEditWorkload
            // 
            this.tsmiEditWorkload.Name = "tsmiEditWorkload";
            this.tsmiEditWorkload.Size = new System.Drawing.Size(139, 22);
            this.tsmiEditWorkload.Text = "修改工作量";
            this.tsmiEditWorkload.Click += new System.EventHandler(this.tsmiEditWorkload_Click);
            // 
            // tsimDeleteWorkload
            // 
            this.tsimDeleteWorkload.Name = "tsimDeleteWorkload";
            this.tsimDeleteWorkload.Size = new System.Drawing.Size(139, 22);
            this.tsimDeleteWorkload.Text = "删除工作量";
            this.tsimDeleteWorkload.Click += new System.EventHandler(this.tsimDeleteWorkload_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 18;
            this.label2.Text = "模型名称";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(236, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 19);
            this.label7.TabIndex = 19;
            this.label7.Text = "工作量值";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 20;
            this.label5.Text = "模型描述";
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(80, 40);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(150, 25);
            this.txtModel.TabIndex = 0;
            // 
            // txtWorkloadValue
            // 
            this.txtWorkloadValue.Location = new System.Drawing.Point(304, 41);
            this.txtWorkloadValue.Name = "txtWorkloadValue";
            this.txtWorkloadValue.Size = new System.Drawing.Size(150, 25);
            this.txtWorkloadValue.TabIndex = 2;
            this.txtWorkloadValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWorkloadValue_KeyDown);
            // 
            // txtModelDesc
            // 
            this.txtModelDesc.Location = new System.Drawing.Point(80, 74);
            this.txtModelDesc.Name = "txtModelDesc";
            this.txtModelDesc.Size = new System.Drawing.Size(150, 25);
            this.txtModelDesc.TabIndex = 1;
            // 
            // btnAddWorkload
            // 
            this.btnAddWorkload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnAddWorkload.FlatAppearance.BorderSize = 0;
            this.btnAddWorkload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddWorkload.ForeColor = System.Drawing.Color.White;
            this.btnAddWorkload.Location = new System.Drawing.Point(304, 72);
            this.btnAddWorkload.Name = "btnAddWorkload";
            this.btnAddWorkload.Size = new System.Drawing.Size(150, 28);
            this.btnAddWorkload.TabIndex = 3;
            this.btnAddWorkload.Text = "添加工作量";
            this.btnAddWorkload.UseVisualStyleBackColor = false;
            this.btnAddWorkload.Click += new System.EventHandler(this.btnAddWorkload_Click);
            // 
            // grbEditWorkload
            // 
            this.grbEditWorkload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbEditWorkload.Controls.Add(this.btnEditWorkload);
            this.grbEditWorkload.Controls.Add(this.label3);
            this.grbEditWorkload.Controls.Add(this.label8);
            this.grbEditWorkload.Controls.Add(this.label4);
            this.grbEditWorkload.Controls.Add(this.label6);
            this.grbEditWorkload.Controls.Add(this.txtEditModel);
            this.grbEditWorkload.Controls.Add(this.txtEditWorkloadId);
            this.grbEditWorkload.Controls.Add(this.txtEditWorkloadValue);
            this.grbEditWorkload.Controls.Add(this.txtEditModelDesc);
            this.grbEditWorkload.Location = new System.Drawing.Point(10, 465);
            this.grbEditWorkload.Name = "grbEditWorkload";
            this.grbEditWorkload.Size = new System.Drawing.Size(928, 91);
            this.grbEditWorkload.TabIndex = 4;
            this.grbEditWorkload.TabStop = false;
            this.grbEditWorkload.Text = "修改工作量";
            // 
            // btnEditWorkload
            // 
            this.btnEditWorkload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnEditWorkload.FlatAppearance.BorderSize = 0;
            this.btnEditWorkload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditWorkload.ForeColor = System.Drawing.Color.White;
            this.btnEditWorkload.Location = new System.Drawing.Point(298, 56);
            this.btnEditWorkload.Name = "btnEditWorkload";
            this.btnEditWorkload.Size = new System.Drawing.Size(150, 28);
            this.btnEditWorkload.TabIndex = 3;
            this.btnEditWorkload.Text = "修改工作量";
            this.btnEditWorkload.UseVisualStyleBackColor = false;
            this.btnEditWorkload.Click += new System.EventHandler(this.btnEditWorkload_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 19);
            this.label3.TabIndex = 25;
            this.label3.Text = "模型名称";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(471, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 19);
            this.label8.TabIndex = 26;
            this.label8.Text = "工作量ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 26;
            this.label4.Text = "工作量值";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 19);
            this.label6.TabIndex = 27;
            this.label6.Text = "模型描述";
            // 
            // txtEditModel
            // 
            this.txtEditModel.Location = new System.Drawing.Point(74, 24);
            this.txtEditModel.Name = "txtEditModel";
            this.txtEditModel.Size = new System.Drawing.Size(150, 25);
            this.txtEditModel.TabIndex = 0;
            // 
            // txtEditWorkloadId
            // 
            this.txtEditWorkloadId.Location = new System.Drawing.Point(548, 26);
            this.txtEditWorkloadId.Name = "txtEditWorkloadId";
            this.txtEditWorkloadId.ReadOnly = true;
            this.txtEditWorkloadId.Size = new System.Drawing.Size(150, 25);
            this.txtEditWorkloadId.TabIndex = 2;
            // 
            // txtEditWorkloadValue
            // 
            this.txtEditWorkloadValue.Location = new System.Drawing.Point(298, 25);
            this.txtEditWorkloadValue.Name = "txtEditWorkloadValue";
            this.txtEditWorkloadValue.Size = new System.Drawing.Size(150, 25);
            this.txtEditWorkloadValue.TabIndex = 2;
            this.txtEditWorkloadValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEditWorkloadValue_KeyDown);
            // 
            // txtEditModelDesc
            // 
            this.txtEditModelDesc.Location = new System.Drawing.Point(74, 58);
            this.txtEditModelDesc.Name = "txtEditModelDesc";
            this.txtEditModelDesc.Size = new System.Drawing.Size(150, 25);
            this.txtEditModelDesc.TabIndex = 1;
            // 
            // WorkloadId
            // 
            this.WorkloadId.DataPropertyName = "WorkloadId";
            this.WorkloadId.HeaderText = "ID";
            this.WorkloadId.Name = "WorkloadId";
            this.WorkloadId.ReadOnly = true;
            this.WorkloadId.Width = 60;
            // 
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "模型名称";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            // 
            // WorkloadValue
            // 
            this.WorkloadValue.DataPropertyName = "WorkloadValue";
            this.WorkloadValue.HeaderText = "工作量值";
            this.WorkloadValue.Name = "WorkloadValue";
            this.WorkloadValue.ReadOnly = true;
            this.WorkloadValue.Width = 90;
            // 
            // ModelDesc
            // 
            this.ModelDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ModelDesc.DataPropertyName = "ModelDesc";
            this.ModelDesc.HeaderText = "模型描述";
            this.ModelDesc.Name = "ModelDesc";
            this.ModelDesc.ReadOnly = true;
            // 
            // FrmDesignWorkload
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 568);
            this.Controls.Add(this.grbEditWorkload);
            this.Controls.Add(this.btnAddWorkload);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtWorkloadValue);
            this.Controls.Add(this.txtModelDesc);
            this.Controls.Add(this.dgvDesignWorkload);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDesignWorkload";
            this.Text = "FrmDesignWorkload";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesignWorkload)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.grbEditWorkload.ResumeLayout(false);
            this.grbEditWorkload.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDesignWorkload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtWorkloadValue;
        private System.Windows.Forms.TextBox txtModelDesc;
        private System.Windows.Forms.Button btnAddWorkload;
        private System.Windows.Forms.GroupBox grbEditWorkload;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditWorkload;
        private System.Windows.Forms.ToolStripMenuItem tsimDeleteWorkload;
        private System.Windows.Forms.Button btnEditWorkload;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEditModel;
        private System.Windows.Forms.TextBox txtEditWorkloadValue;
        private System.Windows.Forms.TextBox txtEditModelDesc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEditWorkloadId;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkloadId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkloadValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelDesc;
    }
}