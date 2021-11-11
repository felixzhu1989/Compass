using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using DAL;
using Models;
using SolidWorks.Interop.sldworks;
using SolidWorksHelper;
using Microsoft.VisualBasic;

namespace Compass
{
    public partial class FrmCeilingAutoDrawing : MetroFramework.Forms.MetroForm
    {
        private readonly ProjectService _objProjectService = new ProjectService();
        private readonly ModuleTreeService _objModuleTreeService = new ModuleTreeService();
        private readonly SubAssyService _objSubAssyService = new SubAssyService();
        private readonly CeilingCutListService _objCeilingCutListService = new CeilingCutListService();
        private readonly RequirementService _objRequirementService = new RequirementService();
        private readonly CeilingAccessoryService _objCeilingAccessoryService = new CeilingAccessoryService();
        private readonly DrawingPlanService _objDrawingPlanService = new DrawingPlanService();
        private GeneralRequirement _objGeneralRequirement = null;
        private Project _objProject = null;
        private readonly List<SubAssy> _subAssyTreeList = new List<SubAssy>();
        private readonly List<SubAssy> _subAssyAddList = new List<SubAssy>();//拖动文件到窗口中执行添加
        //使用BindingList动态绑定Dgv
        BindingList<ModuleTree> _waitingList = null;//待执行list，从项目中查询出来的
        readonly BindingList<ModuleTree> _execList = new BindingList<ModuleTree>();//执行list，手动添加的
        BindingList<SubAssy> _subAssyWaitingList = null;////待执行list，从项目中查询出来的,拖拽进dgv中的文件列表
        readonly BindingList<SubAssy> _subAssyExecList = new BindingList<SubAssy>();//执行list，手动添加的
        private readonly string _sbu = Program.ObjCurrentUser.SBU;

        //solidWorks程序
        private SldWorks _swApp;

        public FrmCeilingAutoDrawing()
        {
            InitializeComponent();
            dgvWaitingList.AutoGenerateColumns = false;
            dgvExecList.AutoGenerateColumns = false;
            dgvSubAssyWaitingList.AutoGenerateColumns = false;
            dgvSubAssyExecList.AutoGenerateColumns = false;
            //dgvCutList.AutoGenerateColumns = false;
            IniCobOdpNo();
            this.tvSubAssyTree.AllowDrop = true;//允许文件拖拽
            this.txtMainAssyPath.AllowDrop = true;
            btnEditCeilingAccessory.Enabled = false;
            
            dgvCeilingPackingList.AutoGenerateColumns = false;
            SetPermissions();
        }

        public void IniCobOdpNo()
        {
            this.cobODPNo.SelectedIndexChanged -= new System.EventHandler(this.CobODPNo_SelectedIndexChanged);
            //项目编号下拉框
            cobODPNo.DataSource = _objProjectService.GetProjectsByHoodType("Ceiling", _sbu);
            cobODPNo.DisplayMember = "ODPNo";
            cobODPNo.ValueMember = "ProjectId";
            cobODPNo.SelectedIndex = -1;
            this.cobODPNo.SelectedIndexChanged += new System.EventHandler(this.CobODPNo_SelectedIndexChanged);
        }
        
        #region 单例模式，重写关闭方法，显示时选择ODP号
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        public void ShowWithOdpNo(string odpNo)
        {
            if (odpNo.Length != 0) cobODPNo.Text = odpNo;
            ShowAndFocus();
        }
        internal void ShowAndFocus()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Focus();
        }
        #endregion


        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            //管理员和技术部才能添加、编辑、删除模型
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2)
            {
                tsmiAddCeilingPackingList.Visible = true;
                tsmiChangeLocation.Visible = true;
                tsmiDeleteCutList.Visible = true;
                tsmiDeleteSubAssy.Visible = true;
                tsmiEditCeilingPackingList.Visible = true;
                tsmiDeleteCeilingPackingList.Visible = true;
                btnEditCeilingAccessory.Visible = true;
                btnCeilingPackingList.Enabled = true;//只有技术部能够生成发货清单
                this.dgvCutList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvCutList_KeyDown);
                this.tvSubAssyTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TvSubAssyTree_KeyDown);
                this.dgvCeilingPackingList.DoubleClick += new System.EventHandler(this.DgvCeilingPackingList_DoubleClick);
            }
            else
            {
                tsmiAddCeilingPackingList.Visible = false;
                tsmiChangeLocation.Visible = false;
                tsmiDeleteCutList.Visible = false;
                tsmiDeleteSubAssy.Visible = false;
                tsmiEditCeilingPackingList.Visible = false;
                tsmiDeleteCeilingPackingList.Visible = false;
                btnCeilingPackingList.Enabled = false;
                btnEditCeilingAccessory.Visible = false;

                this.dgvCutList.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.DgvCutList_KeyDown);
                this.tvSubAssyTree.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.TvSubAssyTree_KeyDown);
                this.dgvCeilingPackingList.DoubleClick -= new System.EventHandler(this.DgvCeilingPackingList_DoubleClick);
            }

            //按钮权限，如果是fsprod则为true
            btnPrintLabel.Enabled = false || Program.ObjCurrentUser.UserId == 8;
        }
        /// <summary>
        /// 下拉选择项目后自动切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobODPNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobODPNo.SelectedIndex == -1) return;
            _objProject = _objProjectService.GetProjectByODPNo(cobODPNo.Text,_sbu);
            _objGeneralRequirement = _objRequirementService.GetGeneralRequirementByODPNo(cobODPNo.Text,_sbu);
            txtBPONo.Text = _objProject.BPONo;
            txtProjectName.Text = _objProject.ProjectName;
            RefreshTree();
            RefreshDgv();
            if (_objGeneralRequirement == null)
            {
                MessageBox.Show("请注意，项目通用技术要求没有添加，可能导致发货清单无法正常输出", "提示信息");
                btnCeilingPackingList.Enabled = false;
                btnPrintCeilingPackingList.Enabled = false;
                btnSaveToExcel.Enabled = false;
            }
            else
            {
                txtTypeName.Text = _objGeneralRequirement.TypeName;
                txtMainAssyPath.Text = _objGeneralRequirement.MainAssyPath;
                if (_objGeneralRequirement.MainAssyPath.Length > 0 && Program.ObjCurrentUser.UserGroupId < 3) btnCeilingPackingList.Enabled = true;
                if (dgvCeilingPackingList.RowCount > 0)
                {
                    btnPrintCeilingPackingList.Enabled = true;
                    btnSaveToExcel.Enabled = true;
                }
            }
        }
        /// <summary>
        /// 更新子装配树
        /// </summary>
        private void RefreshTree()
        {
            if (_objProject == null) return;
            //创建第一个节点
            this.tvSubAssyTree.Nodes.Clear();//清空所有节点
            _subAssyTreeList.Clear();//清空节点集合，否则修改一次重复添加一次
            TreeNode rootNode = new TreeNode()
            {
                Text = _objProject.ODPNo,
                Tag = _objProject.ProjectId,//默认值,作为id使用
                ImageIndex = 4//添加图标
            };
            this.tvSubAssyTree.Nodes.Add(rootNode);//将根节点添加到treeView控件中
            //创建子节点
            List<SubAssy> subAssyList = _objSubAssyService.GetSubAssysByProjectId(_objProject.ProjectId.ToString());
            foreach (var item in subAssyList)
            {
                TreeNode node = new TreeNode()
                {
                    Text = item.SubAssyName,
                    ImageIndex = 3,
                    Tag = item.SubAssyId
                };
                rootNode.Nodes.Add(node);
            }
            this.tvSubAssyTree.ExpandAll();
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void RefreshDgv()
        {
            if (_objProject == null) return;
            _execList.Clear();
            _waitingList = new BindingList<ModuleTree>(_objModuleTreeService.GetModuleTreesByProjectId(cobODPNo.SelectedValue.ToString(),_sbu));
            _subAssyExecList.Clear();
            _subAssyWaitingList = new BindingList<SubAssy>(_objSubAssyService.GetSubAssysByProjectId(cobODPNo.SelectedValue.ToString()));
            dgvWaitingList.DataSource = _waitingList;
            dgvExecList.DataSource = _execList;
            dgvSubAssyWaitingList.DataSource = _subAssyWaitingList;
            dgvSubAssyExecList.DataSource = _subAssyExecList;
            dgvCeilingPackingList.DataSource =
                _objCeilingAccessoryService.GetCeilingPackingListByProjectId(_objProject.ProjectId.ToString());
        }


        #region 作图页特有代码
        /// <summary>
        /// 待执行->执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (dgvWaitingList.CurrentRow == null) return;
            int moduleTreeId = Convert.ToInt32(dgvWaitingList.CurrentRow.Cells["ModuleTreeId"].Value);
            foreach (var item in _waitingList)
            {
                if (item.ModuleTreeId == moduleTreeId)
                {
                    _execList.Add(item);
                    _waitingList.Remove(item);
                    return;
                }
            }
        }
        /// <summary>
        /// 执行->待执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSub_Click(object sender, EventArgs e)
        {
            if (dgvExecList.CurrentRow == null) return;
            int moduleTreeId = Convert.ToInt32(dgvExecList.CurrentRow.Cells["ModuleTreeId2"].Value);
            foreach (var item in _execList)
            {
                if (item.ModuleTreeId == moduleTreeId)
                {
                    _waitingList.Add(item);
                    _execList.Remove(item);
                    //及时跳出，防止foreach时list变化
                    return;
                }
            }
        }
        /// <summary>
        /// 待执行->执行/所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddAll_Click(object sender, EventArgs e)
        {
            if (dgvWaitingList.CurrentRow == null) return;
            foreach (var item in _waitingList)
            {
                _execList.Add(item);
            }
            //最后清空，防止foreach时list变化
            _waitingList.Clear();
        }
        /// <summary>
        /// 执行->待执行/所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSubAll_Click(object sender, EventArgs e)
        {
            if (dgvExecList.CurrentRow == null) return;
            foreach (var item in _execList)
            {
                _waitingList.Add(item);
            }
            _execList.Clear();
        }
        /// <summary>
        /// 执行SolidWorks作图程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnExec_Click(object sender, EventArgs e)
        {
            if (_execList.Count == 0) return;
            btnExec.Enabled = false;
            //计算时间
            DateTime startTime = DateTime.Now;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = _execList.Count;
            //创建项目文件夹，默认再D盘MyProjects目录下（先判断文件夹是否存在）
            string projectPath = @"D:\MyProjects\" + cobODPNo.Text;
            if (!Directory.Exists(projectPath))
            {
                Directory.CreateDirectory(projectPath);
            }
            //以异步的方式开启SolidWorks程序并自动作图
            try
            {
                tsslStatus.Text = "正在打开(/连接)SolidWorks程序...";
                _swApp = await SolidWorksSingleton.GetApplicationAsync();
                //遍历execList，创建项目模型存放地址，判断模型类型，查询参数，执行SolidWorks
                foreach (var item in _execList)
                {
                    tsslStatus.Text = item.Item + "(" + item.Module + ")正在作图...";
                    //6.根据工厂提供的选择，执行具体的接口实现方式
                    //以异步的方式执行，让窗口可操作并且进度条更新
                    await AutoDrawingAsync(item, projectPath);
                    tspbStatus.Value += 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _swApp.CommandInProgress = false;//及时关闭外部命令调用，否则影响SolidWorks的使用
            }

            TimeSpan timeSpan = DateTime.Now - startTime;
            tsslStatus.Text = "作图完成,总共耗时：" + timeSpan.TotalSeconds + "秒";
            tspbStatus.Value = _execList.Count;
            BtnSubAll_Click(null, null);//清除执行数据
            btnExec.Enabled = true;
        }
        /// <summary>
        /// 异步方式执行作图程序
        /// </summary>
        /// <param name="item"></param>
        /// <param name="projectPath"></param>
        /// <returns></returns>
        private Task AutoDrawingAsync(ModuleTree item, string projectPath)
        {
            return Task.Run(() =>
            {
                try
                {
                    IAutoDrawing objAutoDrawing = AutoDrawingFactory.ChooseDrawingType(item);
                    item.SBU = _sbu;
                    objAutoDrawing.AutoDrawing(_swApp, item, projectPath);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        /// <summary>
        /// 打印JobCard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnJobCard_Click(object sender, EventArgs e)
        {
            if (_objProject == null) return;//未选中项目则终止
            btnJobCard.Enabled = false;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = 1;
            FrmJobCardCeiling objFrmJobCardCeiling = new FrmJobCardCeiling(_objProject);
            DialogResult result = objFrmJobCardCeiling.ShowDialog();
            //判断打印是否成功
            if (result == DialogResult.OK)
            {
                tspbStatus.Value = 1;
                tsslStatus.Text = "JobCard打印完成";
            }
            else
            {
                tspbStatus.Value = 0;
                tsslStatus.Text = "JobCard打印失败";
            }
            btnJobCard.Enabled = true;
        }
        #endregion

        #region 导图页特有代码
        /// <summary>
        /// 拖放文件获取文件路径并写入SQL，然后更新tree和dgv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvSubAssy_DragDrop(object sender, DragEventArgs e)
        {
            if (cobODPNo.SelectedIndex == -1) return;
            _subAssyAddList.Clear();
            //获取拖放数据
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] content = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i < content.Length; i++)
                {
                    if (Path.GetExtension(content[i]) == ".sldasm" || Path.GetExtension(content[i]) == ".SLDASM") //获取路径文件的后缀)
                    {
                        string fileName = content[i].Substring(content[i].LastIndexOf("\\") + 1);
                        _subAssyAddList.Add(new SubAssy
                        {
                            ProjectId = Convert.ToInt32(cobODPNo.SelectedValue),
                            SubAssyName = fileName.Substring(0, fileName.LastIndexOf(".")),
                            SubAssyPath = content[i],
                        });
                    }
                }
                //基于事务将subAssyFilePathList提交SQLServer
                if (_subAssyAddList.Count == 0) return;
                try
                {
                    if (_objSubAssyService.ImportSubAssy(_subAssyAddList))
                        _subAssyAddList.Clear() /*MessageBox.Show("添加成功！","提示信息")*/;
                }
                catch (Exception ex)
                {
                    throw new Exception("子装配导入数据库失败" + ex.Message);
                }
                finally
                {
                    _subAssyAddList.Clear();
                }
                RefreshTree();
                RefreshDgv();
            }
        }
        /// <summary>
        /// 拖放文件到框引发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvSubAssy_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;//将拖放的图标变成加号
            else e.Effect = DragDropEffects.None;
        }
        /// <summary>
        /// 从子装配树中删除子装配
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiDeleteSubAssy_Click(object sender, EventArgs e)
        {
            if (tvSubAssyTree.SelectedNode == null) return;
            if (tvSubAssyTree.SelectedNode.Level == 1)
            {
                //DialogResult result = MessageBox.Show("确定要删除名为 " + tvSubAssyTree.SelectedNode.Text + " 的这个子装配吗？", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (result == DialogResult.No) return;
                //执行删除
                try
                {
                    if (_objSubAssyService.DeleteSubAssy(tvSubAssyTree.SelectedNode.Tag.ToString()) == 1)
                    {
                        RefreshTree();
                        RefreshDgv();
                    }
                    else MessageBox.Show("删除分段出错，请联系管理员查看后台数据。");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void TvSubAssyTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) TsmiDeleteSubAssy_Click(null, null);
        }
        /// <summary>
        /// 将等待列表中的子装配对象添加到执行列表中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddPath_Click(object sender, EventArgs e)
        {
            if (dgvSubAssyWaitingList.CurrentRow == null) return;
            int subAssyId = Convert.ToInt32(dgvSubAssyWaitingList.CurrentRow.Cells["SubAssyId"].Value);
            foreach (var item in _subAssyWaitingList)
            {
                if (item.SubAssyId == subAssyId)
                {
                    _subAssyExecList.Add(item);
                    _subAssyWaitingList.Remove(item);
                    //及时跳出，防止foreach时list变化
                    return;
                }
            }
        }
        /// <summary>
        /// 将选中的子装配对象移出到执行列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSubPath_Click(object sender, EventArgs e)
        {
            if (dgvSubAssyExecList.CurrentRow == null) return;
            int subAssyId = Convert.ToInt32(dgvSubAssyExecList.CurrentRow.Cells["SubAssyId2"].Value);
            foreach (var item in _subAssyExecList)
            {
                if (item.SubAssyId == subAssyId)
                {
                    _subAssyWaitingList.Add(item);
                    _subAssyExecList.Remove(item);
                    //及时跳出，防止foreach时list变化
                    return;
                }
            }
        }
        /// <summary>
        /// 导出天花DXF图纸和Cutlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnExportDxf_Click(object sender, EventArgs e)
        {
            if (_subAssyExecList.Count == 0) return;
            btnExportDxf.Enabled = false;
            //计算时间
            DateTime startTime = DateTime.Now;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = _subAssyExecList.Count;
            //创建下料图文件夹，默认再D盘MyProjects目录下（先判断文件夹是否存在）
            string dxfPath = @"D:\MyProjects\" + cobODPNo.Text + @"\DXF-CUTLIST";
            if (!Directory.Exists(dxfPath)) Directory.CreateDirectory(dxfPath);
            //以异步的方式开启SolidWorks程序并导图
            try
            {
                tsslStatus.Text = "正在打开(/连接)SolidWorks程序...";
                _swApp = await SolidWorksSingleton.GetApplicationAsync();
                //遍历execList，创建项目模型存放地址，判断模型类型，查询参数，执行SolidWorks
                foreach (var item in _subAssyExecList)
                {
                    tsslStatus.Text = "子装配体(" + item.SubAssyName + ")正在导图...";
                    //以异步的方式执行，让窗口可操作并且进度条更新
                    await ExportCeilingDxfAsync(item, dxfPath, Program.ObjCurrentUser.UserId);
                    tspbStatus.Value += 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _swApp.CommandInProgress = false;//及时关闭外部命令调用，否则影响SolidWorks的使用
            }
            TimeSpan timeSpan = DateTime.Now - startTime;
            tsslStatus.Text = "导出DXF图完成,总共耗时：" + timeSpan.TotalSeconds + "秒";
            tspbStatus.Value = _subAssyExecList.Count;
            //btnSubAll_Click(null, null);//清除执行数据
            foreach (var item in _subAssyExecList)
            {
                _subAssyWaitingList.Add(item);
            }
            _subAssyExecList.Clear();
            btnExportDxf.Enabled = true;
        }
        /// <summary>
        /// 异步方式开始导出dxf图纸
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dxfPath"></param>
        /// <returns></returns>
        private Task ExportCeilingDxfAsync(SubAssy item, string dxfPath, int userId)
        {
            return Task.Run(() =>
            {
                try
                {
                    new ExprotDxf().CeilingAssyToDxf(_swApp, item, dxfPath, userId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        /// <summary>
        /// 单击子装配树，显示CutList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvSubAssyTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvSubAssyTree.SelectedNode == null) return;
            if (e.Node.Level == 1)
            {
                //更新Cutlist显示
                lblModule.Text = "子装配 (" + tvSubAssyTree.SelectedNode.Text + ") Cutlist";//标题
                dgvCutList.DataSource = _objCeilingCutListService.GetCeilingCutListsBySubAssyId(tvSubAssyTree.SelectedNode.Tag.ToString());
            }
        }

        /// <summary>
        /// 打印Cutlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintCutList_Click(object sender, EventArgs e)
        {
            if (dgvCutList.RowCount == 0) return;
            btnPrintCutList.Enabled = false;
            btnPrintCutList.Text = "打印中...";
            SubAssy objSubAssy =
                _objSubAssyService.GetSubAssyId(dgvCutList.Rows[0].Cells["SubAssyId"].Value.ToString());
            if (new PrintReports().ExecPrintCeilingCutList(objSubAssy, dgvCutList)) MessageBox.Show("CutList打印完成", "打印完成");
            btnPrintCutList.Enabled = true;
            btnPrintCutList.Text = "打印CustList";
        }
        /// <summary>
        /// 删除Cutlist多行数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiDeleteCutList_Click(object sender, EventArgs e)
        {
            if (dgvCutList.RowCount == 0) return;
            DialogResult result = MessageBox.Show("确定删除选中的多行数据吗?", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No || dgvCutList.SelectedRows.Count == 0) return;
            string subAssyId = dgvCutList.Rows[0].Cells["SubAssyId"].Value.ToString();
            List<int> idList = new List<int>();
            foreach (DataGridViewRow row in dgvCutList.SelectedRows)
            {
                idList.Add(Convert.ToInt32(row.Cells["CutListId"].Value));//将id添加到集合中
            }
            try
            {
                if (_objCeilingCutListService.DeleteCutlistByTran(idList))
                    dgvCutList.DataSource = _objCeilingCutListService.GetCeilingCutListsBySubAssyId(subAssyId); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //解决按键重复响应

        /// <summary>
        /// 按下删除键删除多行数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvCutList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) TsmiDeleteCutList_Click(null, null);
        }
        #endregion

        #region dgv添加行号
        private void DgvWaitingList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvWaitingList, e);
        }
        private void DgvExecList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvExecList, e);
        }
        private void DgvSubAssyWaitingList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvSubAssyWaitingList, e);
        }
        private void DgvSubAssyExecList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvSubAssyExecList, e);
        }
        private void DgvCutList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvCutList, e);
        }
        private void CeilingPackingList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvCeilingPackingList, e);
        }
        #endregion

        #region 发货清单页面
        /// <summary>
        /// 拖拽文件添加地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtMainAssyPath_DragDrop(object sender, DragEventArgs e)
        {
            if (cobODPNo.SelectedIndex == -1) return;
            //获取拖放数据
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] content = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Path.GetExtension(content[0]) == ".sldasm" || Path.GetExtension(content[0]) == ".SLDASM"
                ) //获取路径文件的后缀)
                {
                    txtMainAssyPath.Text = content[0];
                    _objGeneralRequirement.MainAssyPath = content[0];
                    try
                    {
                        if (_objRequirementService.UpdateMainAssyPath(_objGeneralRequirement,_sbu) > 0)
                            btnCeilingPackingList.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("装配地址导入数据库失败" + ex.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 拖拽文件入框后图标变成复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtMainAssyPath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;//将拖放的图标变成加号
            else e.Effect = DragDropEffects.None;
        }
        /// <summary>
        /// 导出天花烟罩发货清单按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnCeilingPackingList_Click(object sender, EventArgs e)
        {
            //计算时间
            DateTime startTime = DateTime.Now;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = 2;
            btnCeilingPackingList.Enabled = false;
            if (dgvCeilingPackingList.RowCount > 0)
            {
                DialogResult result = MessageBox.Show("发货清单有内容，标准配件将不会重复添加，如果需要添加标准件请手动添加或者删除本数据后再生成发货清单,继续请按YES，不生成发货清单请按NO?", "生成发货清单询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    btnCeilingPackingList.Enabled = true;
                    return;
                }
            }
            else
            {
                //【1】查询项目的所有配件部分
                List<CeilingAccessory> ceilingAccessoriesList = txtTypeName.Text == "日本项目"
                    ? _objCeilingAccessoryService.GetCeilingAccessoriesForJapan()
                    : _objCeilingAccessoryService.GetCeilingAccessoriesForNotJapan();
                foreach (var item in ceilingAccessoriesList)
                {
                    item.ProjectId = _objProject.ProjectId;
                    item.UserId = Program.ObjCurrentUser.UserId;
                    item.Location = _objDrawingPlanService.GetDrawingPlanByProjectId(_objProject.ProjectId.ToString(),_sbu)[0].Item;
                }
                //将查询到的标准配件统一提交到SQL服务器
                _objCeilingAccessoryService.ImportCeilingPackingListByTran(ceilingAccessoriesList);
            }
            tspbStatus.Value = 1;
            //【2】打开装配体，获得所有零件名，查询配件和数量导入SQL
            //预先将装配体的关键字写入到零件中
            //以异步的方式开启SolidWorks程序并导图
            try
            {
                tsslStatus.Text = "正在打开(/连接)SolidWorks程序...";
                _swApp = await SolidWorksSingleton.GetApplicationAsync();
                //执行SolidWorks,打开总装并执行导出发货清单程序

                tsslStatus.Text = "正在导出发货清单...";
                //以异步的方式执行，让窗口可操作并且进度条更新
                await ExportCeilingPackingListAsync(txtMainAssyPath.Text, _objProject);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _swApp.CommandInProgress = false;//及时关闭外部命令调用，否则影响SolidWorks的使用
            }
            //更新发货清单显示
            dgvCeilingPackingList.DataSource = _objCeilingAccessoryService.GetCeilingPackingListByProjectId(_objProject.ProjectId.ToString());
            //MessageBox.Show("发货清单导出成功！", "提示信息");
            TimeSpan timeSpan = DateTime.Now - startTime;
            tsslStatus.Text = "导出发货清单完成,总共耗时：" + timeSpan.TotalSeconds + "秒";
            tspbStatus.Value = 2;
            btnCeilingPackingList.Enabled = true;
        }
        private Task ExportCeilingPackingListAsync(string mainAssyPath, Project objProject)
        {
            return Task.Run(() =>
            {
                try
                {
                    new ExportCeilingPackingList().CeilingAssyToPackingList(_swApp, mainAssyPath, objProject, Program.ObjCurrentUser.UserId,_sbu);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
        /// <summary>
        /// 打印发货清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintCeilingPackingList_Click(object sender, EventArgs e)
        {
            if (dgvCeilingPackingList.RowCount == 0) return;
            btnPrintCeilingPackingList.Enabled = false;
            tspbStatus.Value = 0;
            tspbStatus.Maximum = 1;
            tsslStatus.Text = "发货清单打印中...";
            if (new PrintReports().ExecPrintCeilingPackingList(_objProject, dgvCeilingPackingList)) MessageBox.Show("发货清单打印完成", "打印完成");
            tsslStatus.Text = "发货清单打印完成！";
            tspbStatus.Value = 1;
            btnPrintCeilingPackingList.Enabled = true;
        }
        /// <summary>
        /// 保存为Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveToExcel_Click(object sender, EventArgs e)
        {
            if (dgvCeilingPackingList.RowCount == 0) return;
            btnSaveToExcel.Enabled = false;
            tspbStatus.Value = 0;
            tspbStatus.Maximum = 1;
            tsslStatus.Text = "发货清单Excel文件正在保存中...";
            if (new PrintReports().ExecSaveCeilingPackingList(_objProject, dgvCeilingPackingList)) MessageBox.Show("发货清单Excel文件保存完成", "保存完成");
            tsslStatus.Text = "发货清单Excel文件保存完成，请至D盘MyProjects中项目文件夹下查看！";
            tspbStatus.Value = 1;
            btnSaveToExcel.Enabled = true;
        }
        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintLabel_Click(object sender, EventArgs e)
        {
            //判断有没有数据，弹窗确定要打印选中的条目
            if (dgvCeilingPackingList.RowCount == 0) return;
            DialogResult result = MessageBox.Show("确定要打印选中的多行打标签吗?", "打标签询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No || dgvCeilingPackingList.SelectedRows.Count == 0) return;
            //获取选择的标签
            List<CeilingAccessory> ceilingAccessories = new List<CeilingAccessory>();
            foreach (DataGridViewRow row in dgvCeilingPackingList.SelectedRows)
            {
                //循环选中的行，并根据ID查询对象，判断ClassNo==4，将对象填入List
                CeilingAccessory objCeilingAccessory =
                    _objCeilingAccessoryService.GetCeilingPackingItemById(row.Cells["CeilingPackingListId"].Value
                        .ToString());

                //编号为4的打印多个，其余编号打印一个标签
                if (objCeilingAccessory.ClassNo == 4)
                {
                    for (int i = 1; i <= objCeilingAccessory.Quantity; i++)//根据数量添加多个标签
                    {
                        ceilingAccessories.Add(objCeilingAccessory);
                    }
                }
                else
                {
                    ceilingAccessories.Add(objCeilingAccessory);
                }
            }
            //调用打印程序
            if (new PrintReports().ExecPrintCeilingLabel(_objProject, ceilingAccessories)) MessageBox.Show("标签打印完成", "打印完成");
        }
        /// <summary>
        /// 删除没用的发货清单条目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiDeleteCeilingPackingList_Click(object sender, EventArgs e)
        {
            if (dgvCeilingPackingList.RowCount == 0) return;
            int firstRowIndex = dgvCeilingPackingList.CurrentRow.Index;
            DialogResult result = MessageBox.Show("确定删除选中的多行数据吗?", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No || dgvCeilingPackingList.SelectedRows.Count == 0) return;
            List<int> idList = new List<int>();
            foreach (DataGridViewRow row in dgvCeilingPackingList.SelectedRows)
            {
                idList.Add(Convert.ToInt32(row.Cells["CeilingPackingListId"].Value));//将id添加到集合中
            }
            try
            {
                if (_objCeilingAccessoryService.DeleteCeilingPackingListByTran(idList))
                    dgvCeilingPackingList.DataSource = _objCeilingAccessoryService.GetCeilingPackingListByProjectId(_objProject.ProjectId.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgvCeilingPackingList.ClearSelection();
            dgvCeilingPackingList.Rows[firstRowIndex].Selected = true;//将刚修改的行选中
            dgvCeilingPackingList.FirstDisplayedScrollingRowIndex = firstRowIndex;//将修改的行显示在第一行
        }
        /// <summary>
        /// 修改发货清单条目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiEditCeilingPackingList_Click(object sender, EventArgs e)
        {
            btnEditCeilingAccessory.Enabled = true;
            if (dgvCeilingPackingList.RowCount == 0) return;
            if (dgvCeilingPackingList.CurrentRow == null) return;
            string packingId = dgvCeilingPackingList.CurrentRow.Cells["CeilingPackingListId"].Value.ToString();
            CeilingAccessory objCeilingPackingItem = _objCeilingAccessoryService.GetCeilingPackingItemById(packingId);
            //初始化修改信息
            txtPartDescription.Text = objCeilingPackingItem.PartDescription;
            txtPartDescription.ReadOnly = false;
            //if (objCeilingPackingItem.CeilingAccessoryId == "7001") txtPartDescription.ReadOnly = false;
            //else txtPartDescription.ReadOnly = true;
            txtPartNo.Text = objCeilingPackingItem.PartNo;
            txtRemark.Text = objCeilingPackingItem.Remark;
            txtQuantity.Text = objCeilingPackingItem.Quantity.ToString();
            txtLength.Text = objCeilingPackingItem.Length;
            txtWidth.Text = objCeilingPackingItem.Width;
            txtHeight.Text = objCeilingPackingItem.Height;
            txtLocation.Text = objCeilingPackingItem.Location;
            txtPartDescription.Tag = objCeilingPackingItem.CeilingPackingListId;
            txtQuantity.Focus();
            txtQuantity.SelectAll();
        }
        private void DgvCeilingPackingList_DoubleClick(object sender, EventArgs e)
        {
            TsmiEditCeilingPackingList_Click(null, null);
        }
        //按键重复响应？
        /// <summary>
        /// 执行修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditCeilingAccessory_Click(object sender, EventArgs e)
        {
            if (txtPartDescription.Tag == null) return;
            int firstRowIndex = dgvCeilingPackingList.CurrentRow.Index;
            //封装对象
            CeilingAccessory objCeilingPackingItem = new CeilingAccessory()
            {
                CeilingPackingListId = Convert.ToInt32(txtPartDescription.Tag),
                PartDescription = txtPartDescription.Text.Trim(),
                PartNo = txtPartNo.Text.Trim(),
                Remark = txtRemark.Text.Trim(),
                Quantity = Convert.ToInt32(txtQuantity.Text.Trim()),
                Length = txtLength.Text.Trim(),
                Width = txtWidth.Text.Trim(),
                Height = txtHeight.Text.Trim(),
                Location = txtLocation.Text.Trim()
            };
            //提交修改
            //调用后台方法修改对象
            try
            {
                if (_objCeilingAccessoryService.EditCeilingPackingList(objCeilingPackingItem) == 1)
                {
                    MessageBox.Show("发货清单条目信息成功！", "提示信息");
                    dgvCeilingPackingList.DataSource = _objCeilingAccessoryService.GetCeilingPackingListByProjectId(_objProject.ProjectId.ToString());
                    //清空内容
                    txtPartDescription.Text = "";
                    txtPartDescription.ReadOnly = true;
                    txtPartNo.Text = "";
                    txtRemark.Text = "";
                    txtQuantity.Text = "";
                    txtLength.Text = "";
                    txtWidth.Text = "";
                    txtHeight.Text = "";
                    //txtLocation.Text = "";
                    txtPartDescription.Tag = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnEditCeilingAccessory.Enabled = false;
            dgvCeilingPackingList.ClearSelection();
            dgvCeilingPackingList.Rows[firstRowIndex].Selected = true;//将刚修改的行选中
            dgvCeilingPackingList.FirstDisplayedScrollingRowIndex = firstRowIndex;//将修改的行显示在第一行
        }
        private void TxtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) BtnEditCeilingAccessory_Click(null, null);
        }
        /// <summary>
        /// 添加配件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiAddCeilingPackingList_Click(object sender, EventArgs e)
        {
            if (dgvCeilingPackingList.RowCount == 0) return;
            FrmAddCeilingPackingList objFrmAddCeilingPackingList = new FrmAddCeilingPackingList(_objProject);
            DialogResult result = objFrmAddCeilingPackingList.ShowDialog();
            if (result == DialogResult.OK)
            {
                dgvCeilingPackingList.DataSource = _objCeilingAccessoryService.GetCeilingPackingListByProjectId(_objProject.ProjectId.ToString());
            }
        }
        /// <summary>
        /// 改变分区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiChangeLocation_Click(object sender, EventArgs e)
        {

            if (dgvCeilingPackingList.RowCount == 0 || dgvCeilingPackingList.SelectedRows.Count == 0) return;
            string newLocation = Interaction.InputBox("将选中的区域添加到其他区域中，请输入要变换的区域。", "变换区域", "", -1, -1);
            if(newLocation.Length==0)return;
            int firstRowIndex = dgvCeilingPackingList.CurrentRow.Index;
            //根据选中的行，获得id并查询发货清单对象，添加倒集合（旧）
            List<CeilingAccessory> oldCeilingAccessories = new List<CeilingAccessory>();
            foreach (DataGridViewRow row in dgvCeilingPackingList.SelectedRows)
            {
                oldCeilingAccessories.Add(_objCeilingAccessoryService.GetCeilingPackingItemById(row.Cells["CeilingPackingListId"].Value.ToString()));
            }
            //新建新集合，循环旧集合将对象添加到新集合并赋值数量为1，区域为新值
            //判断对象数量 >1时减1（最后用于修改），否则将id添加到新的idList中（删除）
            List<CeilingAccessory> addCeilingAccessories = new List<CeilingAccessory>();
            List<CeilingAccessory> editCeilingAccessories = new List<CeilingAccessory>();
            List<int> deleteIdList = new List<int>();
            foreach (var item in oldCeilingAccessories)
            {
                addCeilingAccessories.Add(new CeilingAccessory()
                {
                    ProjectId = item.ProjectId,
                    CeilingAccessoryId = item.CeilingAccessoryId,
                    ClassNo = item.ClassNo,
                    PartDescription = item.PartDescription,
                    Quantity = 1,//数量为1
                    PartNo = item.PartNo,
                    Unit = item.Unit,
                    Length = item.Length,
                    Width = item.Width,
                    Height = item.Height,
                    Material = item.Material,
                    Remark = item.Remark,
                    CountingRule = item.CountingRule,
                    UserId = item.UserId,
                    Location = newLocation
                });
                if (item.Quantity > 1)
                {
                    item.Quantity -= 1;
                    editCeilingAccessories.Add(item);
                }
                else
                {
                    deleteIdList.Add(item.CeilingPackingListId);
                }
            }
            //将新集合添加到SQL,修改对象,删除对象
            try
            {
                if (_objCeilingAccessoryService.ImportCeilingPackingListByTran(addCeilingAccessories))
                {
                    foreach (var item in editCeilingAccessories)
                    {
                        _objCeilingAccessoryService.EditCeilingPackingList(item);
                    }
                    if(deleteIdList.Count!=0) _objCeilingAccessoryService.DeleteCeilingPackingListByTran(deleteIdList); 
                }
                dgvCeilingPackingList.DataSource = _objCeilingAccessoryService.GetCeilingPackingListByProjectId(_objProject.ProjectId.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgvCeilingPackingList.ClearSelection();
            dgvCeilingPackingList.Rows[firstRowIndex].Selected = true;//将刚修改的行选中
            dgvCeilingPackingList.FirstDisplayedScrollingRowIndex = firstRowIndex;//将修改的行显示在第一行
        }
        #endregion
        /// <summary>
        /// 切换标签重新加载树形菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            RefreshTree();
            RefreshDgv();
        }

        
    }
}
