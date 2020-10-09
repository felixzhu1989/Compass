using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using Models;
using Common;

namespace Compass
{
    public partial class FrmDrawingPlan : Form
    {
        private DrawingPlanService objDrawingPlanService = new DrawingPlanService();
        private UserService objUserService = new UserService();
        private DesignWorkloadService objDesignWorkloadService = new DesignWorkloadService();
        private ProjectService objProjectService=new ProjectService();

        //创建委托变量，显示统计信息
        public ShowDrawingPlanInfoDelegate showDrawingPlanInfoDelegate = null;
        public FrmDrawingPlan()
        {
            InitializeComponent();
            IniUserId(cobUserId);
            IniModel(cobModel);
            IniODPNo(cobODPNo);
            dgvDrawingPlan.AutoGenerateColumns = false;
            btnQueryAllPlan_Click(null, null);
            grbEditDrawingPlan.Visible = false;
            //this.cobODPNo.SelectedIndexChanged += new System.EventHandler(this.cobODPNo_SelectedIndexChanged);
        }
        /// <summary>
        /// 显示全部计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryAllPlan_Click(object sender, EventArgs e)
        {
            dgvDrawingPlan.DataSource = objDrawingPlanService.GetDrawingPlanByWhereSql("");//获取全部计划
        }
        /// <summary>
        /// 初始化制图人员下拉框
        /// </summary>
        /// <param name="cobItem"></param>
        private void IniUserId(ComboBox cobItem)
        {
            cobItem.DataSource = objUserService.GetUserTech();
            cobItem.DisplayMember = "UserAccount";
            cobItem.ValueMember = "UserId";
            cobItem.SelectedIndex = -1;//默认不要选中
        }
        /// <summary>
        /// 初始化烟罩型号下拉框，并绑定工作量
        /// </summary>
        /// <param name="cobItem"></param>
        private void IniModel(ComboBox cobItem)
        {
            cobItem.DataSource = objDesignWorkloadService.GetAllDesignWorkload();
            cobItem.DisplayMember = "Model";
            cobItem.ValueMember = "WorkloadValue";
            cobItem.SelectedIndex = -1;//默认不要选中
        }
        /// <summary>
        /// 初始化ODPNo下拉框
        /// </summary>
        /// <param name="cobItem"></param>
        private void IniODPNo(ComboBox cobItem)
        {
            //绑定ODPNo下拉框
            cobItem.DataSource = objProjectService.GetProjectsByWhereSql("");
            cobItem.DisplayMember = "ODPNo";
            cobItem.ValueMember = "ProjectId";
            cobItem.SelectedIndex = -1;//默认不要选中
        }
        /// <summary>
        /// dgv添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDrawingPlan_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvDrawingPlan, e);
            if (e.RowIndex > -1)
            {
                int remainingDays = (int)this.dgvDrawingPlan.Rows[e.RowIndex].Cells["RemainingDays"].Value;
                if (remainingDays != 0) return;
                DateTime drReleaseActual = (DateTime)this.dgvDrawingPlan.Rows[e.RowIndex].Cells["DrReleaseActual"].Value;
                DateTime drReleaseTarget = (DateTime)this.dgvDrawingPlan.Rows[e.RowIndex].Cells["DrReleaseTarget"].Value;
                if (drReleaseActual.ToString("MM/dd/yyyy") == "01/01/0001")
                    dgvDrawingPlan.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 182, 193);
                if (DateTime.Compare(drReleaseActual, drReleaseTarget) > 0)
                    dgvDrawingPlan.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 0);
            }
        }
        /// <summary>
        /// 模型选择变化时，计算subtotalworkload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtModuleNo.Text.Trim().Length == 0) return;//分段没有输入，不计算
            if (cobModel.SelectedIndex == -1) return;//模型没有选择，不计算
            txtSubTotalWorkload.Text = (Convert.ToDecimal(txtModuleNo.Text.Trim())
                * Convert.ToDecimal(cobModel.SelectedValue)).ToString();
        }
        /// <summary>
        /// 烟罩分段数量变化时，计算subtotalworkload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtModuleNo_TextChanged(object sender, EventArgs e)
        {
            if (!DataValidate.IsInteger(txtModuleNo.Text.Trim()))
            {
                //MessageBox.Show("烟罩分段数量必须是数字", "验证信息");
                txtModuleNo.Text = "";
                txtModuleNo.Focus();
                return;
            }
            if (txtModuleNo.Text.Trim().Length == 0) return;//分段没有输入，不计算
            if (cobModel.SelectedIndex == -1) return;//模型没有选择，不计算
            txtSubTotalWorkload.Text = (Convert.ToDecimal(txtModuleNo.Text.Trim())
                * Convert.ToDecimal(cobModel.SelectedValue)).ToString();
        }
        /// <summary>
        /// 添加计划记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDrawingPlan_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (cobModel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择烟罩型号", "验证信息");
                cobModel.Focus();
                return;
            }
            if (txtModuleNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输烟罩分段数量", "验证信息");
                txtModuleNo.Focus();
                return;
            }
            if (cobODPNo.SelectedIndex == -1)
            {
                MessageBox.Show("请输入选择或输入项目编号，如果没有，请到项目列表中添加后再选择", "验证信息");
                cobODPNo.Focus();
                return;
            }
            //验证数量是数字
            if (!DataValidate.IsInteger(txtModuleNo.Text.Trim()))
            {
                MessageBox.Show("烟罩分段数量必须是数字", "验证信息");
                txtModuleNo.Focus();
                txtModuleNo.SelectAll();
                return;
            }
            #endregion
            //封装制图计划对象
            DrawingPlan objDrawingPlan = new DrawingPlan()
            {
                ProjectId = Convert.ToInt32(cobODPNo.SelectedValue),
                Item = txtItem.Text.Trim(),
                Model = cobModel.Text,
                ModuleNo = Convert.ToInt32(txtModuleNo.Text.Trim()),
                DrReleaseTarget = Convert.ToDateTime(dtpDrReleaseTarget.Text),
                SubTotalWorkload = Convert.ToDecimal(txtSubTotalWorkload.Text.Trim())
            };
            //提交添加
            try
            {
                int drawingPlanId = objDrawingPlanService.AddDraingPlan(objDrawingPlan);
                if (drawingPlanId > 1)
                {
                    //提示添加成功
                    MessageBox.Show("制图计划添加成功", "提示信息");
                    //刷新显示
                    btnQueryByProjectId_Click(null, null);
                    //清空内容
                    //cobUserId.SelectedIndex = -1;//不清空制图人员
                    //cobModel.SelectedIndex = -1;//不清空型号
                    //foreach (Control item in Controls)
                    //{
                    //    if (item == txtODPNo) return;//不清空ODPNo，方便查询
                    //    if (item is TextBox)
                    //    {
                    //        item.Text = "";
                    //    }
                    //}
                    txtSubTotalWorkload.Text = "";
                    txtModuleNo.Text = "";
                    txtItem.Text = "";
                    txtItem.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        /// <summary>
        /// 根据项目ID查询制图计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryByProjectId_Click(object sender, EventArgs e)
        {
            if (cobODPNo.SelectedIndex == -1) return;
            dgvDrawingPlan.DataSource = objDrawingPlanService.GetDrawingPlanByProjectId(cobODPNo.SelectedValue.ToString());
        }
        //private void cobODPNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    btnQueryByProjectId_Click(null, null);
        //}
        /// <summary>
        /// 修改计划菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditDrawingPlan_Click(object sender, EventArgs e)
        {
            if (dgvDrawingPlan.RowCount == 0)
            {
                return;
            }
            if (dgvDrawingPlan.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的计划条目", "提示信息");
                return;
            }
            string drawingPlanId = dgvDrawingPlan.CurrentRow.Cells["DrawingPlanId"].Value.ToString();
            DrawingPlan objDrawingPlan = objDrawingPlanService.GetDrawingPlanById(drawingPlanId);
            //初始化修改信息
            grbEditDrawingPlan.Visible = true;//显示修改框
            IniModel(cobEditModel);
            IniODPNo(cobEditODPNo);
            txtEditDrawingPlanId.Text = objDrawingPlan.DrawingPlanId.ToString();
            txtEditItem.Text = objDrawingPlan.Item;

            txtEditModuleNo.Text = objDrawingPlan.ModuleNo.ToString();
            cobEditModel.Text = objDrawingPlan.Model;
            cobEditODPNo.Text = objDrawingPlan.ODPNo;

            txtEditSubTotalWorkload.Text = objDrawingPlan.SubTotalWorkload.ToString();
            dtpEditDrReleaseTarget.Text = objDrawingPlan.DrReleaseTarget.ToShortDateString();
            dtpEditAddedDate.Text = objDrawingPlan.AddedDate.ToShortDateString();
        }
        /// <summary>
        /// 双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDrawingPlan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsmiEditDrawingPlan_Click(null, null);
        }
        /// <summary>
        /// 同步计算工作量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobEditModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtEditModuleNo.Text.Trim().Length == 0) return;//分段没有输入，不计算
            if (cobEditModel.SelectedIndex == -1) return;//模型没有选择，不计算
            txtEditSubTotalWorkload.Text = (Convert.ToDecimal(txtEditModuleNo.Text.Trim())
                                        * Convert.ToDecimal(cobEditModel.SelectedValue)).ToString();
        }
        /// <summary>
        /// 同步计算工作量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEditModuleNo_TextChanged(object sender, EventArgs e)
        {
            if (!DataValidate.IsInteger(txtEditModuleNo.Text.Trim()))
            {
                //MessageBox.Show("烟罩分段数量必须是数字", "验证信息");
                txtEditModuleNo.Text = "";
                txtEditModuleNo.Focus();
                return;
            }
            if (txtEditModuleNo.Text.Trim().Length == 0) return;//分段没有输入，不计算
            if (cobEditModel.SelectedIndex == -1) return;//模型没有选择，不计算
            txtEditSubTotalWorkload.Text = (Convert.ToDecimal(txtEditModuleNo.Text.Trim())
                                        * Convert.ToDecimal(cobEditModel.SelectedValue)).ToString();
        }
        /// <summary>
        /// 提交修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditDrawingPlan_Click(object sender, EventArgs e)
        {
            #region 数据验证
            
            if (cobEditModel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择烟罩型号", "验证信息");
                cobEditModel.Focus();
                return;
            }
            if (txtEditModuleNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输烟罩分段数量", "验证信息");
                txtEditModuleNo.Focus();
                return;
            }
            if (cobEditODPNo.SelectedIndex == -1)
            {
                MessageBox.Show("请输入选择或输入项目编号，如果没有，请到项目列表中添加后再选择", "验证信息");
                cobEditODPNo.Focus();
                return;
            }
            //验证数量是数字
            if (!DataValidate.IsInteger(txtEditModuleNo.Text.Trim()))
            {
                MessageBox.Show("烟罩分段数量必须是数字", "验证信息");
                txtEditModuleNo.Focus();
                txtEditModuleNo.SelectAll();
                return;
            }
            //验证计划日期大于添加日期
            if (DateTime.Compare(dtpEditDrReleaseTarget.Value, dtpEditAddedDate.Value) < 0)
            {
                MessageBox.Show("计划发图日期必须大于添加日期，请认真检查", "验证信息");
                return;
            }
            #endregion
            //封装制图计划对象
            DrawingPlan objDrawingPlan = new DrawingPlan()
            {
                DrawingPlanId = Convert.ToInt32(txtEditDrawingPlanId.Text),
                ProjectId = Convert.ToInt32(cobEditODPNo.SelectedValue),
                Item = txtEditItem.Text.Trim(),
                Model = cobEditModel.Text,
                ModuleNo = Convert.ToInt32(txtEditModuleNo.Text.Trim()),
                DrReleaseTarget = Convert.ToDateTime(dtpEditDrReleaseTarget.Text),
                SubTotalWorkload = Convert.ToDecimal(txtEditSubTotalWorkload.Text.Trim()),
                AddedDate = Convert.ToDateTime(dtpEditAddedDate.Text.Trim())
            };
            //调用后台方法修改对象
            try
            {
                if (objDrawingPlanService.EditDrawingPlan(objDrawingPlan) == 1)
                {
                    MessageBox.Show("修改计划成功！", "提示信息");
                    grbEditDrawingPlan.Visible = false;
                    btnQueryAllPlan_Click(null, null);//同步刷新显示数据
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 删除计划菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeleteDrawingPlan_Click(object sender, EventArgs e)
        {
            if (dgvDrawingPlan.RowCount == 0)
            {
                return;
            }
            if (dgvDrawingPlan.CurrentRow == null)
            {
                MessageBox.Show("请选中需要删除的计划记录", "验证信息");
                return;
            }
            string drawingPlanId = dgvDrawingPlan.CurrentRow.Cells["DrawingPlanId"].Value.ToString();
            string odpNo = dgvDrawingPlan.CurrentRow.Cells["ODPNo"].Value.ToString();
            string item = dgvDrawingPlan.CurrentRow.Cells["Item"].Value.ToString();
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除（项目编号ODP： " + odpNo + " 烟罩编号Item：" + item + " ）这条计划吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                if (objDrawingPlanService.DeleteDrawingPlan(drawingPlanId) == 1)
                {
                    btnQueryAllPlan_Click(null, null);//同步刷新显示数据
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            grbEditDrawingPlan.Visible = false;
        }
        /// <summary>
        /// 按下删除键执行删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDrawingPlan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) tsmiDeleteDrawingPlan_Click(null,null);
        }
        /// <summary>
        /// 输入Item后输入数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtItem.Text.Trim().Length == 0)
            {
                txtItem.Focus();
                return;
            }
            if (e.KeyValue == 13)
            {
                txtModuleNo.Focus();
                txtModuleNo.SelectAll();
            }
        }
        /// <summary>
        /// 输入数量后直接提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtModuleNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtModuleNo.Text.Trim().Length == 0)
            {
                txtModuleNo.Focus();
                return;
            }
            if (e.KeyValue == 13)
            {
                btnAddDrawingPlan_Click(null, null);
            }
        }
        /// <summary>
        /// 选中行时获取订单编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDrawingPlan_SelectionChanged(object sender, EventArgs e)
        {
            cobODPNo.Text = this.dgvDrawingPlan.CurrentRow.Cells["ODPNo"].Value.ToString();
        }
        /// <summary>
        /// 查询整个订单弹出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiQueryByODP_Click(object sender, EventArgs e)
        {
            btnQueryByProjectId_Click(null, null); 
        }
        /// <summary>
        /// 查询所有计划弹出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiQueryAllPlan_Click(object sender, EventArgs e)
        {
            btnQueryAllPlan_Click(null, null);
        }

        /// <summary>
        /// 弹出技术要求窗口，添加特殊技术要求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRequirements_Click(object sender, EventArgs e)
        {
            string odpNo = cobODPNo.Text.Trim();
            if (odpNo.Length == 0) return;
            Project objProject = objProjectService.GetProjectByODPNo(odpNo);
            if (objProject == null) return;
            FrmRequirements objFrmRequirements = new FrmRequirements(objProject);
            objFrmRequirements.ShowDialog();
        }
        private void btnShowInfo_Click(object sender, EventArgs e)
        {
            //调用委托执行委托方法
            showDrawingPlanInfoDelegate();
        }
        /// <summary>
        /// 根据制图人员查询计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryByUserId_Click(object sender, EventArgs e)
        {
            if(cobUserId.SelectedIndex==-1)return;
            dgvDrawingPlan.DataSource = objDrawingPlanService.GetDrawingPlanByUserId(cobUserId.SelectedValue.ToString());
        }
        /// <summary>
        /// 按回车后根据项目查询计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobODPNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) btnQueryByProjectId_Click(null,null);
        }
        /// <summary>
        /// 按回车后根据人员查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) btnQueryByUserId_Click(null, null);
        }
    }
}
