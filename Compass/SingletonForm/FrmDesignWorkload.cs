using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Models;
using DAL;
using Common;

namespace Compass
{
    public partial class FrmDesignWorkload : Form
    {
        private readonly DesignWorkloadService _objDesignWorkloadService=new DesignWorkloadService();
        private List<DesignWorkload> _designWorkloadList=new List<DesignWorkload>();
        private readonly string _sbu = Program.ObjCurrentUser.SBU;

        public FrmDesignWorkload()
        {
            InitializeComponent();
            dgvDesignWorkload.AutoGenerateColumns = false;
            RefreshData();
            grbEditWorkload.Visible = false;
        }
        private void RefreshData()
        {
            _designWorkloadList = _objDesignWorkloadService.GetAllDesignWorkload(_sbu);
            dgvDesignWorkload.DataSource = _designWorkloadList;
        }
        /// <summary>
        /// 添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDesignWorkload_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(dgvDesignWorkload, e);
        }
        /// <summary>
        /// 添加工作量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddWorkload_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (txtModel.Text.Trim().Length == 0)
            {
                MessageBox.Show("模型名称不能为空不能为空", "验证信息");
                txtModel.Focus();
                return;
            }
            if (txtWorkloadValue.Text.Trim().Length == 0)
            {
                MessageBox.Show("工作量值不能为空", "验证信息");
                txtWorkloadValue.Focus();
                return;
            }
            #endregion
            //封装对象
            DesignWorkload objDesignWorkload=new DesignWorkload()
            {
                Model = txtModel.Text.Trim(),
                WorkloadValue =Convert.ToDecimal(txtWorkloadValue.Text.Trim()),
                ModelDesc = txtModelDesc.Text.Trim()
            };
            //提交添加
            try
            {
                int userId = _objDesignWorkloadService.AddDesignWorkload(objDesignWorkload,_sbu);
                if (userId > 1)
                {
                    //提示添加成功
                    MessageBox.Show("工作量添加成功", "提示信息");
                    //刷新显示
                    RefreshData();
                    //清空内容
                    foreach (Control item in Controls)
                    {
                        if (item is TextBox)
                        {
                            item.Text = "";
                        }
                    }
                    txtModel.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// txtWorkloadValue回车添加工作量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWorkloadValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && txtWorkloadValue.Text.Trim().Length != 0)
            {
                btnAddWorkload_Click(null, null);
            }
        }
        /// <summary>
        /// 修改工作量菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditWorkload_Click(object sender, EventArgs e)
        {
            if (dgvDesignWorkload.RowCount == 0)
            {
                return;
            }
            if (dgvDesignWorkload.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的工作量条目", "提示信息");
                return;
            }
            string workloadId = dgvDesignWorkload.CurrentRow.Cells["WorkloadId"].Value.ToString();
            DesignWorkload objDesignWorkload = _objDesignWorkloadService.GetDesignWorkloadById(workloadId,_sbu);
            //初始化修改信息
            grbEditWorkload.Visible = true;//显示
            txtEditWorkloadId.Text = objDesignWorkload.WorkloadId.ToString();
            txtEditModel.Text = objDesignWorkload.Model;
            txtEditWorkloadValue.Text = objDesignWorkload.WorkloadValue.ToString();
            txtEditModelDesc.Text = objDesignWorkload.ModelDesc;
            txtEditWorkloadValue.Focus();//获取焦点
            txtEditWorkloadValue.SelectAll();//全选，直接编辑
        }
        /// <summary>
        /// 提交修改工作量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditWorkload_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (txtEditModel.Text.Trim().Length == 0)
            {
                MessageBox.Show("模型名称不能为空", "验证信息");
                txtModel.Focus();
                return;
            }
            if (txtEditWorkloadValue.Text.Trim().Length == 0)
            {
                MessageBox.Show("工作量值不能为空", "验证信息");
                txtWorkloadValue.Focus();
                return;
            }
            #endregion
            //封装对象
            DesignWorkload objDesignWorkload = new DesignWorkload()
            {
                WorkloadId = Convert.ToInt32(txtEditWorkloadId.Text.Trim()),
                Model = txtEditModel.Text.Trim(),
                WorkloadValue = Convert.ToDecimal(txtEditWorkloadValue.Text.Trim()),
                ModelDesc = txtEditModelDesc.Text.Trim()
            };
            //提交修改
            try
            {
                if (_objDesignWorkloadService.EditDesignWorkload(objDesignWorkload,_sbu) == 1)
                {
                    MessageBox.Show("修改工作量成功！", "提示信息");
                    grbEditWorkload.Visible = false;
                    RefreshData();//同步刷新显示数据
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// txtEditWorkloadValue回车修改工作量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEditWorkloadValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && txtEditWorkloadValue.Text.Trim().Length != 0)
            {
                btnEditWorkload_Click(null, null);
            }
        }
        /// <summary>
        /// 删除工作量菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsimDeleteWorkload_Click(object sender, EventArgs e)
        {
            if (dgvDesignWorkload.RowCount == 0)
            {
                return;
            }
            if (dgvDesignWorkload.CurrentRow == null)
            {
                MessageBox.Show("请选中需要删除的工作量条目", "验证信息");
                return;
            }
            string workloadId = dgvDesignWorkload.CurrentRow.Cells["WorkloadId"].Value.ToString();
            string model = dgvDesignWorkload.CurrentRow.Cells["Model"].Value.ToString();
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除模型（ " + model + " ）的工作量吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                if (_objDesignWorkloadService.DeleteDesignWorkload(workloadId,_sbu) == 1)
                {
                    RefreshData();//同步刷新显示数据
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            grbEditWorkload.Visible = false;
        }

        private void dgvDesignWorkload_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) tsimDeleteWorkload_Click(null, null);
        }
    }
}
