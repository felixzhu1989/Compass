using System;
using System.Windows.Forms;
using DAL;
using Models;
using Common;

namespace Compass
{
    public partial class FrmRequirements : MetroFramework.Forms.MetroForm
    {
        public ProjectTypeService objProjectTypeService=new ProjectTypeService();
        public RequirementService objRequirementService=new RequirementService();
        private string sbu = Program.ObjCurrentUser.SBU;

        public FrmRequirements()
        {
            InitializeComponent();
            //绑定项目类型下拉框
            cobTypeName.DataSource = objProjectTypeService.GetAllProjectTypes(sbu);
            cobTypeName.DisplayMember = "TypeName";
            cobTypeName.ValueMember = "TypeId";
            cobTypeName.SelectedIndex = -1;//默认不要选中
            //项目等级下拉框
            cobRiskLevel.Items.Add("4");
            cobRiskLevel.Items.Add("3");
            cobRiskLevel.Items.Add("2");
            cobRiskLevel.Items.Add("1");
            cobRiskLevel.SelectedIndex = 1;
            //项目电制
            cobInputPower.Items.Add("230/50Hz");
            cobInputPower.Items.Add("230/60Hz");
            cobInputPower.Items.Add("120/50Hz");
            cobInputPower.Items.Add("120/60Hz");
            cobInputPower.Items.Add("Special");
            //MARVEL
            cobMARVEL.Items.Add("No");
            cobMARVEL.Items.Add("Yes");
            //ANSUL预埋
            cobANSULPrepipe.Items.Add("No");
            cobANSULPrepipe.Items.Add("R102");
            cobANSULPrepipe.Items.Add("Piranha");
            //ANSUL系统
            cobANSULSystem.Items.Add("No");
            cobANSULSystem.Items.Add("R102");
            cobANSULSystem.Items.Add("Piranha");
            SetPermissions();
        }
        public FrmRequirements(Project objProject) :this()
        {
            txtODPNo.Text = objProject.ODPNo;
            txtODPNo.Tag = objProject.ProjectId;
            txtProjectName.Text = objProject.ProjectName;
            GeneralRequirement objGeneralRequirement =
                objRequirementService.GetGeneralRequirementByODPNo(objProject.ODPNo,sbu);
            if (objGeneralRequirement == null)
            {
                btnGeneralRequirement.Text = "添加通用技术要求";
            }
            else
            {
                btnGeneralRequirement.Text = "修改通用技术要求";
                cobTypeName.Text = objGeneralRequirement.TypeName;
                cobInputPower.Text = objGeneralRequirement.InputPower;
                cobMARVEL.Text = objGeneralRequirement.MARVEL;
                cobANSULPrepipe.Text = objGeneralRequirement.ANSULPrePipe;
                cobANSULSystem.Text = objGeneralRequirement.ANSULSystem;
                txtGeneralRequirementId.Text = objGeneralRequirement.GeneralRequirementId.ToString();
                cobRiskLevel.Text = objGeneralRequirement.RiskLevel.ToString();
            }
            btnSpecialRequirement.Text = "添加特殊技术要求";
            dgvSpecialRequirements.AutoGenerateColumns = false;
            dgvSpecialRequirements.DataSource = objRequirementService.GetSpecialRequirementsByODPNo(objProject.ODPNo,sbu);
        }
        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            //管理员和技术部才能添加、编辑、删除模型
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2)
            {
                btnGeneralRequirement.Visible = true;
                tsmiEditSpecialRequirement.Visible = true;
                tsmiDeleteSpecialRequirement.Visible = true;
                this.dgvSpecialRequirements.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpecialRequirements_CellDoubleClick);
                this.dgvSpecialRequirements.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSpecialRequirements_KeyDown);
            }
            else
            {
                btnGeneralRequirement.Visible = false;
                tsmiEditSpecialRequirement.Visible = false;
                tsmiDeleteSpecialRequirement.Visible = false;
                this.dgvSpecialRequirements.CellDoubleClick -= new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpecialRequirements_CellDoubleClick);
                this.dgvSpecialRequirements.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.dgvSpecialRequirements_KeyDown);
            }
        }
        /// <summary>
        /// 通用技术要求添加/修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGeneralRequirement_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (cobTypeName.SelectedIndex == -1)
            {
                MessageBox.Show("请选择项目类型", "验证信息");
                cobTypeName.Focus();
                return;
            }
            if (cobRiskLevel.SelectedIndex == -1)
            {
                MessageBox.Show("请选择项目等级", "验证信息");
                cobANSULSystem.Focus();
                return;
            }
            if (cobInputPower.SelectedIndex == -1)
            {
                MessageBox.Show("请选择电制", "验证信息");
                cobInputPower.Focus();
                return;
            }
            if (cobMARVEL.SelectedIndex == -1)
            {
                MessageBox.Show("请选择MARVEL", "验证信息");
                cobMARVEL.Focus();
                return;
            }
            if (cobANSULPrepipe.SelectedIndex == -1)
            {
                MessageBox.Show("请选择ANSUL预埋", "验证信息");
                cobANSULPrepipe.Focus();
                return;
            }
            if (cobANSULSystem.SelectedIndex == -1)
            {
                MessageBox.Show("请选择ANSUL系统", "验证信息");
                cobANSULSystem.Focus();
                return;
            }
            
            #endregion

            if (txtGeneralRequirementId.Text.Trim().Length == 0)
            {
                //提交添加
                //封装对象
                GeneralRequirement objGeneralRequirement = new GeneralRequirement()
                {
                    ProjectId=Convert.ToInt32(txtODPNo.Tag),
                    TypeId=Convert.ToInt32(cobTypeName.SelectedValue),
                    InputPower=cobInputPower.Text,
                    MARVEL=cobMARVEL.Text,
                    ANSULPrePipe=cobANSULPrepipe.Text,
                    ANSULSystem=cobANSULSystem.Text,
                    RiskLevel = Convert.ToInt32(cobRiskLevel.Text)
                };
                //提交添加
                try
                {
                    int GeneralRequirementId =objRequirementService.AddGeneralRequirement(objGeneralRequirement,sbu);
                    if (GeneralRequirementId > 1)
                    {
                        SingletonObject.GetSingleton.FrmP.BtnQueryByYear_Click(null, null);
                        //提示添加成功
                        MessageBox.Show("通用技术要求添加成功", "提示信息");
                        //刷新显示
                        btnGeneralRequirement.Text = "修改通用技术要求";
                        txtGeneralRequirementId.Text = GeneralRequirementId.ToString();
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
                GeneralRequirement objGeneralRequirement = new GeneralRequirement()
                {
                    GeneralRequirementId = Convert.ToInt32(txtGeneralRequirementId.Text.Trim()),
                    ProjectId = Convert.ToInt32(txtODPNo.Tag),
                    TypeId = Convert.ToInt32(cobTypeName.SelectedValue),
                    InputPower = cobInputPower.Text,
                    MARVEL = cobMARVEL.Text,
                    ANSULPrePipe = cobANSULPrepipe.Text,
                    ANSULSystem = cobANSULSystem.Text,
                    RiskLevel = Convert.ToInt32(cobRiskLevel.Text)
                };
                //提交修改
                //调用后台方法修改对象
                try
                {
                    if (objRequirementService.EditGeneralRequirement(objGeneralRequirement,sbu) == 1)
                    {
                        SingletonObject.GetSingleton.FrmP.BtnQueryByYear_Click(null, null);
                        MessageBox.Show("修改通用技术要求成功！", "提示信息");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 特殊技术要求添加/修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSpecialRequirement_Click(object sender, EventArgs e)
        {
            //数据验证
            if (txtContant.Text.Trim().Length == 0)
            {
                MessageBox.Show("请填写特殊技术要求后操作", "验证信息");
                txtContant.Focus();
                return;
            }
            //判断添加/修改
            if (txtSpecialRequirementId.Text.Trim().Length == 0)
            {
                //提交添加
                //封装对象
                SpecialRequirement objSpecialRequirement = new SpecialRequirement()
                {
                    ProjectId = Convert.ToInt32(txtODPNo.Tag),
                    Content = txtContant.Text
                };
                //提交添加
                try
                {
                    int specialRequirementId = objRequirementService.AddSpecialRequirement(objSpecialRequirement,sbu);
                    if (specialRequirementId > 1)
                    {
                        //提示添加成功
                        MessageBox.Show("特殊技术要求添加成功", "提示信息");
                        //刷新显示
                        txtContant.Text = "";
                        txtSpecialRequirementId.Text = "";
                        dgvSpecialRequirements.DataSource = objRequirementService.GetSpecialRequirementsByODPNo(txtODPNo.Text.Trim(),sbu);
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
                SpecialRequirement objSpecialRequirement = new SpecialRequirement()
                {
                    SpecialRequirementId = Convert.ToInt32(txtSpecialRequirementId.Text.Trim()),
                    ProjectId = Convert.ToInt32(txtODPNo.Tag),
                    Content = txtContant.Text
                };
                //提交修改
                //调用后台方法修改对象
                try
                {
                    if (objRequirementService.EditSpecialRequirement(objSpecialRequirement,sbu) == 1)
                    {
                        MessageBox.Show("修改特殊技术要求成功！", "提示信息");
                        txtContant.Text = "";
                        txtSpecialRequirementId.Text = "";
                        dgvSpecialRequirements.DataSource = objRequirementService.GetSpecialRequirementsByODPNo(txtODPNo.Text.Trim(),sbu);
                        btnSpecialRequirement.Text = "添加特殊技术要求";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSpecialRequirements_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvSpecialRequirements, e);
        }
        /// <summary>
        /// 编辑特殊技术要求菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditSpecialRequirement_Click(object sender, EventArgs e)
        {
            if (dgvSpecialRequirements.RowCount == 0)
            {
                return;
            }
            if (dgvSpecialRequirements.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的特殊技术要求", "提示信息");
                return;
            }
            string specialRequirementId = dgvSpecialRequirements.CurrentRow.Cells["SpecialRequirementId"].Value.ToString();
            SpecialRequirement objSpecialRequirement = objRequirementService.GetSpecialRequirementById(specialRequirementId,sbu);
            //初始化修改信息
            txtContant.Text = objSpecialRequirement.Content;
            txtSpecialRequirementId.Text = objSpecialRequirement.SpecialRequirementId.ToString();
            btnSpecialRequirement.Text = "修改特殊技术要求";
        }
        /// <summary>
        /// 删除特殊技术要求菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeleteSpecialRequirement_Click(object sender, EventArgs e)
        {
            if (dgvSpecialRequirements.RowCount == 0)
            {
                return;
            }
            if (dgvSpecialRequirements.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的特殊技术要求", "提示信息");
                return;
            }
            string specialRequirementId = dgvSpecialRequirements.CurrentRow.Cells["SpecialRequirementId"].Value.ToString();
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除（序号Id为： " + specialRequirementId + " ）这个条特殊要求吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                if (objRequirementService.DeleteSpecialRequirement(specialRequirementId,sbu) == 1)
                {
                    dgvSpecialRequirements.DataSource = objRequirementService.GetSpecialRequirementsByODPNo(txtODPNo.Text.Trim(),sbu);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSpecialRequirements_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsmiEditSpecialRequirement_Click(null, null);
        }
        /// <summary>
        /// 按下delete按键删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSpecialRequirements_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) tsmiDeleteSpecialRequirement_Click(null, null);
        }
        /// <summary>
        /// 回车直接修改特殊技术要求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtContant_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)btnSpecialRequirement_Click(null,null);
        }
    }
}
