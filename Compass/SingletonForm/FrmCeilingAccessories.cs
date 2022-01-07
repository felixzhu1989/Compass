using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmCeilingAccessories : MetroFramework.Forms.MetroForm
    {
        private readonly CeilingAccessoryService _objCeilingAccessoryService = new CeilingAccessoryService();

        #region 单例模式
        private FrmCeilingAccessories()
        {
            InitializeComponent();
            btnCeilingAccessory.Tag = 0;//0代表添加，1代表修改
            btnCeilingAccessory.Text = "添加配件";
            cobUnit.Items.Add("PCS");
            cobUnit.Items.Add("M/米");
            cobUnit.Items.Add("Yes/No");
            cobUnit.SelectedIndex = 0;
            cobClassNo.Items.Add("0");
            cobClassNo.Items.Add("1");
            cobClassNo.Items.Add("2");
            cobClassNo.Items.Add("3");
            cobClassNo.Items.Add("4");
            dgvCeilingAccessories.AutoGenerateColumns = false;
            dgvCeilingAccessories.DataSource = _objCeilingAccessoryService.GetCeilingAccessoriesByWhereSql("");
        }
        private static FrmCeilingAccessories instance;
        public static FrmCeilingAccessories GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new FrmCeilingAccessories();
            }
            return instance;
        }
        #endregion


        /// <summary>
        /// 按钮，增加或修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCeilingAccessory_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (txtPartDescription.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入部件描述", "验证信息");
                txtPartDescription.Focus();
                return;
            }
            if (txtPartNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入部件编号", "验证信息");
                txtPartNo.Focus();
                return;
            }
            if (txtCeilingAccessoryId.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入ID（长度4位）,ID开头数字规则:0电/1灯/2风机/3型材/4油网/5螺丝/6控制/7水洗ANSUL/8折弯件/9焊接件", "验证信息");
                txtCeilingAccessoryId.Focus();
                return;
            }
            if (txtCeilingAccessoryId.Text.Trim().Length != 4)
            {
                MessageBox.Show("ID长度4位", "验证信息");
                txtCeilingAccessoryId.Focus();
                return;
            }
            if (cobUnit.SelectedIndex == -1)
            {
                MessageBox.Show("请选择单位", "验证信息");
                cobUnit.Focus();
                return;
            }
            if (cobClassNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择分类号，分类号规则：0日本不要配件，1日本特有配件,2适用于所有项目的配件，3自制折弯件，4自制焊接件（打标签）", "验证信息");
                cobClassNo.Focus();
                return;
            }
            #endregion
            int firstRowIndex = dgvCeilingAccessories.CurrentRow.Index;
            if (btnCeilingAccessory.Tag.Equals(0))
            {
                //提交添加
                //封装对象
                CeilingAccessory objCeilingAccessory = new CeilingAccessory()
                {
                    CeilingAccessoryId = txtCeilingAccessoryId.Text.Trim(),
                    ClassNo = Convert.ToInt32(cobClassNo.Text),
                    PartDescription = txtPartDescription.Text.Trim(),
                    PartNo = txtPartNo.Text.Trim(),
                    Unit = cobUnit.Text,
                    Length = txtLength.Text.Trim(),
                    Width = txtWidth.Text.Trim(),
                    Height = txtHeight.Text.Trim(),
                    Material = txtMaterial.Text.Trim(),
                    Remark = txtRemark.Text.Trim(),
                    CountingRule = txtCountingRule.Text.Trim()
                };
                //提交添加
                try
                {
                    if (_objCeilingAccessoryService.AddCeilingAccessory(objCeilingAccessory))
                    {
                        //提示添加成功
                        MessageBox.Show("配件信息添加成功", "提示信息");
                        //刷新显示
                        dgvCeilingAccessories.DataSource = _objCeilingAccessoryService.GetCeilingAccessoriesByWhereSql("");
                        //清空内容
                        foreach (Control item in Controls)
                        {
                            if (item == txtPartNo) continue;
                            if (item is TextBox)
                            {
                                item.Text = "";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //封装对象
                //封装对象
                CeilingAccessory objCeilingAccessory = new CeilingAccessory()
                {
                    CeilingAccessoryId = txtCeilingAccessoryId.Text.Trim(),
                    ClassNo = Convert.ToInt32(cobClassNo.Text),
                    PartDescription = txtPartDescription.Text.Trim(),
                    PartNo = txtPartNo.Text.Trim(),
                    Unit = cobUnit.Text,
                    Length = txtLength.Text.Trim(),
                    Width = txtWidth.Text.Trim(),
                    Height = txtHeight.Text.Trim(),
                    Material = txtMaterial.Text.Trim(),
                    Remark = txtRemark.Text.Trim(),
                    CountingRule = txtCountingRule.Text.Trim()
                };
                //提交
                //提交修改
                //调用后台方法修改对象
                try
                {
                    if (_objCeilingAccessoryService.EditCeilingAccessory(objCeilingAccessory) == 1)
                    {
                        MessageBox.Show("修改配件信息成功！", "提示信息");
                        dgvCeilingAccessories.DataSource =
                            _objCeilingAccessoryService.GetCeilingAccessoriesByWhereSql("");
                        //清空内容
                        foreach (Control item in Controls)
                        {
                            if (item == txtPartNo) continue;
                            if (item is TextBox)
                            {
                                item.Text = "";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    btnCeilingAccessory.Tag = 0;//0代表添加，1代表修改
                    btnCeilingAccessory.Text = "添加配件";
                    txtCeilingAccessoryId.ReadOnly = false;
                }
            }
            dgvCeilingAccessories.ClearSelection();
            dgvCeilingAccessories.Rows[firstRowIndex].Selected = true;//将刚修改的行选中
            dgvCeilingAccessories.FirstDisplayedScrollingRowIndex = firstRowIndex;//将修改的行显示在第一行
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiEditCeilingAccessory_Click(object sender, EventArgs e)
        {
            if(dgvCeilingAccessories.RowCount==0)return;
            if(dgvCeilingAccessories.CurrentRow==null)return;
            string id = dgvCeilingAccessories.CurrentRow.Cells["CeilingAccessoryId"].Value.ToString();
            CeilingAccessory objCeilingAccessory = _objCeilingAccessoryService.GetCeilingAccessoryById(id);
            //初始化修改信息
            btnCeilingAccessory.Text = "修改配件";
            btnCeilingAccessory.Tag = 1;
            txtCeilingAccessoryId.Text = objCeilingAccessory.CeilingAccessoryId;
            txtCeilingAccessoryId.ReadOnly = false;

            txtPartDescription.Text = objCeilingAccessory.PartDescription;
            txtPartNo.Text = objCeilingAccessory.PartNo;
            cobUnit.Text = objCeilingAccessory.Unit;
            cobClassNo.Text =objCeilingAccessory.ClassNo.ToString();
            txtRemark.Text = objCeilingAccessory.Remark;
            txtCountingRule.Text = objCeilingAccessory.CountingRule;
            txtLength.Text = objCeilingAccessory.Length;
            txtWidth.Text = objCeilingAccessory.Width;
            txtHeight.Text = objCeilingAccessory.Height;
            txtMaterial.Text = objCeilingAccessory.Material;
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiDeleteCeilingAccessory_Click(object sender, EventArgs e)
        {
            if (dgvCeilingAccessories.RowCount == 0) return;
            if (dgvCeilingAccessories.CurrentRow == null) return;
            string id = dgvCeilingAccessories.CurrentRow.Cells["CeilingAccessoryId"].Value.ToString();
            string partDescription = dgvCeilingAccessories.CurrentRow.Cells["PartDescription"].Value.ToString();
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除【 " + partDescription + " 】这个配件的信息吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            int firstRowIndex = dgvCeilingAccessories.CurrentRow.Index;
            try
            {
                if (_objCeilingAccessoryService.DeleteCeilingAccessory(id) == 1)
                    dgvCeilingAccessories.DataSource = _objCeilingAccessoryService.GetCeilingAccessoriesByWhereSql("");
                else MessageBox.Show("删除配件信息出错，项目是否被其他数据关联，请联系管理员查看后台数据。");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgvCeilingAccessories.ClearSelection();
            dgvCeilingAccessories.Rows[firstRowIndex].Selected = true;//将刚修改的行选中
            dgvCeilingAccessories.FirstDisplayedScrollingRowIndex = firstRowIndex;//将修改的行显示在第一行
        }
        /// <summary>
        /// 添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvCeilingAccessories_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e) => DataGridViewStyle.DgvRowPostPaint(dgvCeilingAccessories, e);
        /// <summary>
        /// 按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvCeilingAccessories_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) TsmiDeleteCeilingAccessory_Click(null, null);
        }
    }
}
