using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
        private ProjectService objProjectService = new ProjectService();
        private ModuleTreeService objModuleTreeService = new ModuleTreeService();
        private SubAssyService objSubAssyService = new SubAssyService();
        private CeilingCutListService objCeilingCutListService = new CeilingCutListService();
        private RequirementService objRequirementService = new RequirementService();
        private CeilingAccessoryService objCeilingAccessoryService = new CeilingAccessoryService();
        private DrawingPlanService objDrawingPlanService = new DrawingPlanService();
        private GeneralRequirement objGeneralRequirement = null;
        private Project objProject = null;
        private List<SubAssy> subAssyTreeList = new List<SubAssy>();
        private List<SubAssy> subAssyAddList = new List<SubAssy>();//拖动文件到窗口中执行添加
        //使用BindingList动态绑定Dgv
        BindingList<ModuleTree> waitingList = null;//待执行list，从项目中查询出来的
        BindingList<ModuleTree> execList = new BindingList<ModuleTree>();//执行list，手动添加的
        BindingList<SubAssy> subAssyWaitingList = null;////待执行list，从项目中查询出来的,拖拽进dgv中的文件列表
        BindingList<SubAssy> subAssyExecList = new BindingList<SubAssy>();//执行list，手动添加的

        //solidWorks程序
        private SldWorks swApp;
        public FrmCeilingAutoDrawing()
        {
            InitializeComponent();
            dgvWaitingList.AutoGenerateColumns = false;
            dgvExecList.AutoGenerateColumns = false;
            dgvSubAssyWaitingList.AutoGenerateColumns = false;
            dgvSubAssyExecList.AutoGenerateColumns = false;
            //dgvCutList.AutoGenerateColumns = false;

            //项目编号下拉框
            cobODPNo.DataSource = objProjectService.GetProjectsByHoodType("Ceiling");
            cobODPNo.DisplayMember = "ODPNo";
            cobODPNo.ValueMember = "ProjectId";
            cobODPNo.SelectedIndex = -1;
            this.cobODPNo.SelectedIndexChanged += new System.EventHandler(this.cobODPNo_SelectedIndexChanged);
            this.tvSubAssyTree.AllowDrop = true;//允许文件拖拽
            this.txtMainAssyPath.AllowDrop = true;
            btnEditCeilingAccessory.Enabled = false;
            
            dgvCeilingPackingList.AutoGenerateColumns = false;
            SetPermissions();
        }

        public FrmCeilingAutoDrawing(string odpNo) : this()
        {
            if (odpNo.Length == 0) return;
            cobODPNo.Text = odpNo;
        }
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
                this.dgvCutList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvCutList_KeyDown);
                this.tvSubAssyTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvSubAssyTree_KeyDown);
                this.dgvCeilingPackingList.DoubleClick += new System.EventHandler(this.dgvCeilingPackingList_DoubleClick);
                this.dgvCeilingPackingList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvCeilingPackingList_KeyDown);
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

                this.dgvCutList.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.dgvCutList_KeyDown);
                this.tvSubAssyTree.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.tvSubAssyTree_KeyDown);
                this.dgvCeilingPackingList.DoubleClick -= new System.EventHandler(this.dgvCeilingPackingList_DoubleClick);
                this.dgvCeilingPackingList.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.dgvCeilingPackingList_KeyDown);
            }
            

            //按钮权限
            btnPrintLabel.Enabled = false;
            if (Program.ObjCurrentUser.UserId == 8) btnPrintLabel.Enabled = true;//只有生产fsprod才能打印标签
            

        }
        /// <summary>
        /// 下拉选择项目后自动切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobODPNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobODPNo.SelectedIndex == -1) return;
            objProject = objProjectService.GetProjectByODPNo(cobODPNo.Text);
            objGeneralRequirement = objRequirementService.GetGeneralRequirementByODPNo(cobODPNo.Text);
            txtBPONo.Text = objProject.BPONo;
            txtProjectName.Text = objProject.ProjectName;
            RefreshTree();
            RefreshDgv();
            if (objGeneralRequirement == null)
            {
                MessageBox.Show("请注意，项目通用技术要求没有添加，可能导致发货清单无法正常输出", "提示信息");
                btnCeilingPackingList.Enabled = false;
                btnPrintCeilingPackingList.Enabled = false;
                btnSaveToExcel.Enabled = false;
            }
            else
            {
                txtTypeName.Text = objGeneralRequirement.TypeName;
                txtMainAssyPath.Text = objGeneralRequirement.MainAssyPath;
                if (objGeneralRequirement.MainAssyPath.Length > 0 && Program.ObjCurrentUser.UserGroupId < 3) btnCeilingPackingList.Enabled = true;
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
            if (objProject == null) return;
            //创建第一个节点
            this.tvSubAssyTree.Nodes.Clear();//清空所有节点
            subAssyTreeList.Clear();//清空节点集合，否则修改一次重复添加一次
            TreeNode rootNode = new TreeNode()
            {
                Text = objProject.ODPNo,
                Tag = objProject.ProjectId,//默认值,作为id使用
                ImageIndex = 4//添加图标
            };
            this.tvSubAssyTree.Nodes.Add(rootNode);//将根节点添加到treeView控件中
            //创建子节点
            List<SubAssy> subAssyList = objSubAssyService.GetSubAssysByProjectId(objProject.ProjectId.ToString());
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
            if (objProject == null) return;
            execList.Clear();
            waitingList = new BindingList<ModuleTree>(objModuleTreeService.GetModuleTreesByProjectId(cobODPNo.SelectedValue.ToString()));
            subAssyExecList.Clear();
            subAssyWaitingList = new BindingList<SubAssy>(objSubAssyService.GetSubAssysByProjectId(cobODPNo.SelectedValue.ToString()));
            dgvWaitingList.DataSource = waitingList;
            dgvExecList.DataSource = execList;
            dgvSubAssyWaitingList.DataSource = subAssyWaitingList;
            dgvSubAssyExecList.DataSource = subAssyExecList;
            dgvCeilingPackingList.DataSource =
                objCeilingAccessoryService.GetCeilingPackingListByProjectId(objProject.ProjectId.ToString());
        }


        #region 作图页特有代码
        /// <summary>
        /// 待执行->执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvWaitingList.CurrentRow == null) return;
            int moduleTreeId = Convert.ToInt32(dgvWaitingList.CurrentRow.Cells["ModuleTreeId"].Value);
            foreach (var item in waitingList)
            {
                if (item.ModuleTreeId == moduleTreeId)
                {
                    execList.Add(item);
                    waitingList.Remove(item);
                    return;
                }
            }
        }
        /// <summary>
        /// 执行->待执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSub_Click(object sender, EventArgs e)
        {
            if (dgvExecList.CurrentRow == null) return;
            int moduleTreeId = Convert.ToInt32(dgvExecList.CurrentRow.Cells["ModuleTreeId2"].Value);
            foreach (var item in execList)
            {
                if (item.ModuleTreeId == moduleTreeId)
                {
                    waitingList.Add(item);
                    execList.Remove(item);
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
        private void btnAddAll_Click(object sender, EventArgs e)
        {
            if (dgvWaitingList.CurrentRow == null) return;
            foreach (var item in waitingList)
            {
                execList.Add(item);
            }
            //最后清空，防止foreach时list变化
            waitingList.Clear();
        }
        /// <summary>
        /// 执行->待执行/所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubAll_Click(object sender, EventArgs e)
        {
            if (dgvExecList.CurrentRow == null) return;
            foreach (var item in execList)
            {
                waitingList.Add(item);
            }
            execList.Clear();
        }
        /// <summary>
        /// 执行SolidWorks作图程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnExec_Click(object sender, EventArgs e)
        {
            if (execList.Count == 0) return;
            btnExec.Enabled = false;
            //计算时间
            DateTime startTime = DateTime.Now;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = execList.Count;
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
                swApp = await SolidWorksSingleton.GetApplicationAsync();
                //遍历execList，创建项目模型存放地址，判断模型类型，查询参数，执行SolidWorks
                foreach (var item in execList)
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
                swApp.CommandInProgress = false;//及时关闭外部命令调用，否则影响SolidWorks的使用
            }

            TimeSpan timeSpan = DateTime.Now - startTime;
            tsslStatus.Text = "作图完成,总共耗时：" + timeSpan.TotalSeconds + "秒";
            tspbStatus.Value = execList.Count;
            btnSubAll_Click(null, null);//清除执行数据
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
                    objAutoDrawing.AutoDrawing(swApp, item, projectPath);
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
        private void btnJobCard_Click(object sender, EventArgs e)
        {
            if (objProject == null) return;//未选中项目则终止
            btnJobCard.Enabled = false;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = 1;
            FrmJobCardCeiling objFrmJobCardCeiling = new FrmJobCardCeiling(objProject);
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
        private void tvSubAssy_DragDrop(object sender, DragEventArgs e)
        {
            if (cobODPNo.SelectedIndex == -1) return;
            subAssyAddList.Clear();
            //获取拖放数据
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] content = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i < content.Length; i++)
                {
                    if (Path.GetExtension(content[i]) == ".sldasm" || Path.GetExtension(content[i]) == ".SLDASM") //获取路径文件的后缀)
                    {
                        string fileName = content[i].Substring(content[i].LastIndexOf("\\") + 1);
                        subAssyAddList.Add(new SubAssy
                        {
                            ProjectId = Convert.ToInt32(cobODPNo.SelectedValue),
                            SubAssyName = fileName.Substring(0, fileName.LastIndexOf(".")),
                            SubAssyPath = content[i],
                        });
                    }
                }
                //基于事务将subAssyFilePathList提交SQLServer
                if (subAssyAddList.Count == 0) return;
                try
                {
                    if (objSubAssyService.ImportSubAssy(subAssyAddList))
                        subAssyAddList.Clear() /*MessageBox.Show("添加成功！","提示信息")*/;
                }
                catch (Exception ex)
                {
                    throw new Exception("子装配导入数据库失败" + ex.Message);
                }
                finally
                {
                    subAssyAddList.Clear();
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
        private void tvSubAssy_DragEnter(object sender, DragEventArgs e)
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
        private void tsmiDeleteSubAssy_Click(object sender, EventArgs e)
        {
            if (tvSubAssyTree.SelectedNode == null) return;
            if (tvSubAssyTree.SelectedNode.Level == 1)
            {
                //DialogResult result = MessageBox.Show("确定要删除名为 " + tvSubAssyTree.SelectedNode.Text + " 的这个子装配吗？", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (result == DialogResult.No) return;
                //执行删除
                try
                {
                    if (objSubAssyService.DeleteSubAssy(tvSubAssyTree.SelectedNode.Tag.ToString()) == 1)
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
        private void tvSubAssyTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) tsmiDeleteSubAssy_Click(null, null);
        }
        /// <summary>
        /// 将等待列表中的子装配对象添加到执行列表中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPath_Click(object sender, EventArgs e)
        {
            if (dgvSubAssyWaitingList.CurrentRow == null) return;
            int subAssyId = Convert.ToInt32(dgvSubAssyWaitingList.CurrentRow.Cells["SubAssyId"].Value);
            foreach (var item in subAssyWaitingList)
            {
                if (item.SubAssyId == subAssyId)
                {
                    subAssyExecList.Add(item);
                    subAssyWaitingList.Remove(item);
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
        private void btnSubPath_Click(object sender, EventArgs e)
        {
            if (dgvSubAssyExecList.CurrentRow == null) return;
            int subAssyId = Convert.ToInt32(dgvSubAssyExecList.CurrentRow.Cells["SubAssyId2"].Value);
            foreach (var item in subAssyExecList)
            {
                if (item.SubAssyId == subAssyId)
                {
                    subAssyWaitingList.Add(item);
                    subAssyExecList.Remove(item);
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
        private async void btnExportDxf_Click(object sender, EventArgs e)
        {
            if (subAssyExecList.Count == 0) return;
            btnExportDxf.Enabled = false;
            //计算时间
            DateTime startTime = DateTime.Now;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = subAssyExecList.Count;
            //创建下料图文件夹，默认再D盘MyProjects目录下（先判断文件夹是否存在）
            string dxfPath = @"D:\MyProjects\" + cobODPNo.Text + @"\DXF-CUTLIST";
            if (!Directory.Exists(dxfPath)) Directory.CreateDirectory(dxfPath);
            //以异步的方式开启SolidWorks程序并导图
            try
            {
                tsslStatus.Text = "正在打开(/连接)SolidWorks程序...";
                swApp = await SolidWorksSingleton.GetApplicationAsync();
                //遍历execList，创建项目模型存放地址，判断模型类型，查询参数，执行SolidWorks
                foreach (var item in subAssyExecList)
                {
                    tsslStatus.Text = "子装配体(" + item.SubAssyName + ")正在导图...";
                    //以异步的方式执行，让窗口可操作并且进度条更新
                    await exportCeilingDxfAsync(item, dxfPath, Program.ObjCurrentUser.UserId);
                    tspbStatus.Value += 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                swApp.CommandInProgress = false;//及时关闭外部命令调用，否则影响SolidWorks的使用
            }
            TimeSpan timeSpan = DateTime.Now - startTime;
            tsslStatus.Text = "导出DXF图完成,总共耗时：" + timeSpan.TotalSeconds + "秒";
            tspbStatus.Value = subAssyExecList.Count;
            //btnSubAll_Click(null, null);//清除执行数据
            foreach (var item in subAssyExecList)
            {
                subAssyWaitingList.Add(item);
            }
            subAssyExecList.Clear();
            btnExportDxf.Enabled = true;
        }
        /// <summary>
        /// 异步方式开始导出dxf图纸
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dxfPath"></param>
        /// <returns></returns>
        private Task exportCeilingDxfAsync(SubAssy item, string dxfPath, int userId)
        {
            return Task.Run(() =>
            {
                try
                {
                    new ExprotDxf().CeilingAssyToDxf(swApp, item, dxfPath, userId);
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
        private void tvSubAssyTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvSubAssyTree.SelectedNode == null) return;
            if (e.Node.Level == 1)
            {
                //更新Cutlist显示
                lblModule.Text = "子装配 (" + tvSubAssyTree.SelectedNode.Text + ") Cutlist";//标题
                dgvCutList.DataSource = objCeilingCutListService.GetCeilingCutListsBySubAssyId(tvSubAssyTree.SelectedNode.Tag.ToString());
            }
        }

        /// <summary>
        /// 打印Cutlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintCutList_Click(object sender, EventArgs e)
        {
            if (dgvCutList.RowCount == 0) return;
            btnPrintCutList.Enabled = false;
            btnPrintCutList.Text = "打印中...";
            SubAssy objSubAssy =
                objSubAssyService.GetSubAssyId(dgvCutList.Rows[0].Cells["SubAssyId"].Value.ToString());
            if (new PrintReports().ExecPrintCeilingCutList(objSubAssy, dgvCutList)) MessageBox.Show("CutList打印完成", "打印完成");
            btnPrintCutList.Enabled = true;
            btnPrintCutList.Text = "打印CustList";
        }
        /// <summary>
        /// 删除Cutlist多行数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeleteCutList_Click(object sender, EventArgs e)
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
                if (objCeilingCutListService.DeleteCutlistByTran(idList))
                    dgvCutList.DataSource = objCeilingCutListService.GetCeilingCutListsBySubAssyId(subAssyId); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 按下删除键删除多行数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCutList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) tsmiDeleteCutList_Click(null, null);
        }
        #endregion

        #region dgv添加行号
        private void dgvWaitingList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvWaitingList, e);
        }
        private void dgvExecList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvExecList, e);
        }
        private void dgvSubAssyWaitingList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvSubAssyWaitingList, e);
        }
        private void dgvSubAssyExecList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvSubAssyExecList, e);
        }
        private void dgvCutList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
        private void txtMainAssyPath_DragDrop(object sender, DragEventArgs e)
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
                    objGeneralRequirement.MainAssyPath = content[0];
                    try
                    {
                        if (objRequirementService.UpdateMainAssyPath(objGeneralRequirement) > 0)
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
        private void txtMainAssyPath_DragEnter(object sender, DragEventArgs e)
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
        private async void btnCeilingPackingList_Click(object sender, EventArgs e)
        {
            //计算时间
            DateTime startTime = DateTime.Now;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = 2;
            btnCeilingPackingList.Enabled = false;
            List<CeilingAccessory> ceilingAccessoriesList = null;
            List<CeilingAccessory> ceilingPackingList = null;
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
                if (txtTypeName.Text == "日本项目")
                    ceilingAccessoriesList = objCeilingAccessoryService.GetCeilingAccessoriesForJapan();
                else
                    ceilingAccessoriesList = objCeilingAccessoryService.GetCeilingAccessoriesForNotJapan();
                foreach (var item in ceilingAccessoriesList)
                {
                    item.ProjectId = objProject.ProjectId;
                    item.UserId = Program.ObjCurrentUser.UserId;
                    item.Location = objDrawingPlanService.GetDrawingPlanByProjectId(objProject.ProjectId.ToString())[0].Item;
                }
                //将查询到的标准配件统一提交到SQL服务器
                objCeilingAccessoryService.ImportCeilingPackingListByTran(ceilingAccessoriesList);
            }
            tspbStatus.Value = 1;
            //【2】打开装配体，获得所有零件名，查询配件和数量导入SQL
            //预先将装配体的关键字写入到零件中
            //以异步的方式开启SolidWorks程序并导图
            try
            {
                tsslStatus.Text = "正在打开(/连接)SolidWorks程序...";
                swApp = await SolidWorksSingleton.GetApplicationAsync();
                //执行SolidWorks,打开总装并执行导出发货清单程序

                tsslStatus.Text = "正在导出发货清单...";
                //以异步的方式执行，让窗口可操作并且进度条更新
                await exportCeilingPackingListAsync(txtMainAssyPath.Text, objProject);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                swApp.CommandInProgress = false;//及时关闭外部命令调用，否则影响SolidWorks的使用
            }
            //更新发货清单显示
            dgvCeilingPackingList.DataSource = objCeilingAccessoryService.GetCeilingPackingListByProjectId(objProject.ProjectId.ToString());
            //MessageBox.Show("发货清单导出成功！", "提示信息");
            TimeSpan timeSpan = DateTime.Now - startTime;
            tsslStatus.Text = "导出发货清单完成,总共耗时：" + timeSpan.TotalSeconds + "秒";
            tspbStatus.Value = 2;
            btnCeilingPackingList.Enabled = true;
        }
        private Task exportCeilingPackingListAsync(string mainAssyPath, Project objProject)
        {
            return Task.Run(() =>
            {
                try
                {
                    new ExportCeilingPackingList().CeilingAssyToPackingList(swApp, mainAssyPath, objProject, Program.ObjCurrentUser.UserId);
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
        private void btnPrintCeilingPackingList_Click(object sender, EventArgs e)
        {
            if (dgvCeilingPackingList.RowCount == 0) return;
            btnPrintCeilingPackingList.Enabled = false;
            tspbStatus.Value = 0;
            tspbStatus.Maximum = 1;
            tsslStatus.Text = "发货清单打印中...";
            if (new PrintReports().ExecPrintCeilingPackingList(objProject, dgvCeilingPackingList)) MessageBox.Show("发货清单打印完成", "打印完成");
            tsslStatus.Text = "发货清单打印完成！";
            tspbStatus.Value = 1;
            btnPrintCeilingPackingList.Enabled = true;
        }
        /// <summary>
        /// 保存为Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveToExcel_Click(object sender, EventArgs e)
        {
            if (dgvCeilingPackingList.RowCount == 0) return;
            btnSaveToExcel.Enabled = false;
            tspbStatus.Value = 0;
            tspbStatus.Maximum = 1;
            tsslStatus.Text = "发货清单Excel文件正在保存中...";
            if (new PrintReports().ExecSaveCeilingPackingList(objProject, dgvCeilingPackingList)) MessageBox.Show("发货清单Excel文件保存完成", "保存完成");
            tsslStatus.Text = "发货清单Excel文件保存完成，请至D盘MyProjects中项目文件夹下查看！";
            tspbStatus.Value = 1;
            btnSaveToExcel.Enabled = true;
        }
        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintLabel_Click(object sender, EventArgs e)
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
                    objCeilingAccessoryService.GetCeilingPackingItemById(row.Cells["CeilingPackingListId"].Value
                        .ToString());

                if (objCeilingAccessory.ClassNo == 4)
                {
                    for (int i = 1; i <= objCeilingAccessory.Quantity; i++)//根据数量添加多个标签
                    {
                        ceilingAccessories.Add(objCeilingAccessory);
                    }
                }
            }
            //调用打印程序
            if (new PrintReports().ExecPrintCeilingLabel(objProject, ceilingAccessories)) MessageBox.Show("标签打印完成", "打印完成");
        }
        /// <summary>
        /// 删除没用的发货清单条目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeleteCeilingPackingList_Click(object sender, EventArgs e)
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
                if (objCeilingAccessoryService.DeleteCeilingPackingListByTran(idList))
                    dgvCeilingPackingList.DataSource = objCeilingAccessoryService.GetCeilingPackingListByProjectId(objProject.ProjectId.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgvCeilingPackingList.Rows[firstRowIndex].Selected = true;//将刚修改的行选中
            dgvCeilingPackingList.FirstDisplayedScrollingRowIndex = firstRowIndex;//将修改的行显示在第一行
        }
        /// <summary>
        /// 修改发货清单条目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditCeilingPackingList_Click(object sender, EventArgs e)
        {
            btnEditCeilingAccessory.Enabled = true;
            if (dgvCeilingPackingList.RowCount == 0) return;
            if (dgvCeilingPackingList.CurrentRow == null) return;
            string packingId = dgvCeilingPackingList.CurrentRow.Cells["CeilingPackingListId"].Value.ToString();
            CeilingAccessory objCeilingPackingItem = objCeilingAccessoryService.GetCeilingPackingItemById(packingId);
            //初始化修改信息
            txtPartDescription.Text = objCeilingPackingItem.PartDescription;
            if (objCeilingPackingItem.CeilingAccessoryId == "7001") txtPartDescription.ReadOnly = false;
            else txtPartDescription.ReadOnly = true;
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
        private void dgvCeilingPackingList_DoubleClick(object sender, EventArgs e)
        {
            tsmiEditCeilingPackingList_Click(null, null);
        }
        private void dgvCeilingPackingList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) tsmiDeleteCeilingPackingList_Click(null, null);
        }
        /// <summary>
        /// 执行修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditCeilingAccessory_Click(object sender, EventArgs e)
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
                if (objCeilingAccessoryService.EditCeilingPackingList(objCeilingPackingItem) == 1)
                {
                    MessageBox.Show("发货清单条目信息成功！", "提示信息");
                    dgvCeilingPackingList.DataSource = objCeilingAccessoryService.GetCeilingPackingListByProjectId(objProject.ProjectId.ToString());
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
            dgvCeilingPackingList.Rows[firstRowIndex].Selected = true;//将刚修改的行选中
            dgvCeilingPackingList.FirstDisplayedScrollingRowIndex = firstRowIndex;//将修改的行显示在第一行
        }
        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) btnEditCeilingAccessory_Click(null, null);
        }
        /// <summary>
        /// 添加配件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddCeilingPackingList_Click(object sender, EventArgs e)
        {
            if (dgvCeilingPackingList.RowCount == 0) return;
            FrmAddCeilingPackingList objFrmAddCeilingPackingList = new FrmAddCeilingPackingList(objProject);
            DialogResult result = objFrmAddCeilingPackingList.ShowDialog();
            if (result == DialogResult.OK)
            {
                dgvCeilingPackingList.DataSource = objCeilingAccessoryService.GetCeilingPackingListByProjectId(objProject.ProjectId.ToString());
            }
        }
        /// <summary>
        /// 改变分区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiChangeLocation_Click(object sender, EventArgs e)
        {

            if (dgvCeilingPackingList.RowCount == 0 || dgvCeilingPackingList.SelectedRows.Count == 0) return;
            string newLocation = Interaction.InputBox("将选中的区域添加到其他区域中，请输入要变换的区域。", "变换区域", "", -1, -1);
            if(newLocation.Length==0)return;
            int firstRowIndex = dgvCeilingPackingList.CurrentRow.Index;
            //根据选中的行，获得id并查询发货清单对象，添加倒集合（旧）
            List<CeilingAccessory> oldCeilingAccessories = new List<CeilingAccessory>();
            foreach (DataGridViewRow row in dgvCeilingPackingList.SelectedRows)
            {
                oldCeilingAccessories.Add(objCeilingAccessoryService.GetCeilingPackingItemById(row.Cells["CeilingPackingListId"].Value.ToString()));
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
                    item.Quantity = item.Quantity - 1;
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
                if (objCeilingAccessoryService.ImportCeilingPackingListByTran(addCeilingAccessories))
                {
                    foreach (var item in editCeilingAccessories)
                    {
                        objCeilingAccessoryService.EditCeilingPackingList(item);
                    }
                    if(deleteIdList.Count!=0) objCeilingAccessoryService.DeleteCeilingPackingListByTran(deleteIdList); 
                }
                dgvCeilingPackingList.DataSource = objCeilingAccessoryService.GetCeilingPackingListByProjectId(objProject.ProjectId.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dgvCeilingPackingList.Rows[firstRowIndex].Selected = true;//将刚修改的行选中
            dgvCeilingPackingList.FirstDisplayedScrollingRowIndex = firstRowIndex;//将修改的行显示在第一行
        }
        #endregion
        /// <summary>
        /// 切换标签重新加载树形菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            RefreshTree();
            RefreshDgv();
        }

        
    }
}
