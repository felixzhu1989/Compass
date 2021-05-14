using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;
using DAL;
using Common;
using Models.Model;
using SolidWorks.Interop.sldworks;
using SolidWorksHelper;

namespace Compass
{
    public partial class FrmHoodAutoDrawing : MetroFramework.Forms.MetroForm
    {
        private ProjectService objProjectService = new ProjectService();
        ModuleTreeService objModuleTreeService = new ModuleTreeService();
        private Project objProject = null;
        //使用BindingList动态绑定Dgv
        BindingList<ModuleTree> waitingList = null;//待执行list，从项目中查询出来的
        BindingList<ModuleTree> execList = new BindingList<ModuleTree>();//执行list，手动添加的
        //solidWorks程序
        private SldWorks swApp;

        private string sbu = Program.ObjCurrentUser.SBU;

        public FrmHoodAutoDrawing()
        {
            InitializeComponent();
            dgvWaitingList.AutoGenerateColumns = false;
            dgvExecList.AutoGenerateColumns = false;
            //项目编号下拉框
            cobODPNo.DataSource = objProjectService.GetProjectsByHoodType("Hood",Program.ObjCurrentUser.SBU);
            cobODPNo.DisplayMember = "ODPNo";
            cobODPNo.ValueMember = "ProjectId";
            cobODPNo.SelectedIndex = -1;
            this.cobODPNo.SelectedIndexChanged += new System.EventHandler(this.cobODPNo_SelectedIndexChanged);
        }

        public FrmHoodAutoDrawing(string odpNo) : this()
        {
            if (odpNo.Length == 0) return;
            cobODPNo.Text = odpNo;
        }
        /// <summary>
        /// 下拉选择项目后自动切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobODPNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobODPNo.SelectedIndex == -1) return;
            objProject = objProjectService.GetProjectByODPNo(cobODPNo.Text,Program.ObjCurrentUser.SBU);
            txtBPONo.Text = objProject.BPONo;
            txtProjectName.Text = objProject.ProjectName;
            execList.Clear();
            waitingList = new BindingList<ModuleTree>(objModuleTreeService.GetModuleTreesByProjectId(cobODPNo.SelectedValue.ToString(),sbu));
            RefreshDgv();
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void RefreshDgv()
        {
            dgvWaitingList.DataSource = waitingList;
            dgvExecList.DataSource = execList;
        }
        /// <summary>
        /// 添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvWaitingList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvWaitingList, e);
        }
        /// <summary>
        /// 添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvExecList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvExecList, e);
        }
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
            DateTime startTime=DateTime.Now;
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
                    tsslStatus.Text = item.Item +"("+ item.Module + ")正在作图...";
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
        private Task AutoDrawingAsync(ModuleTree item,string projectPath)
        {
            return Task.Run(() =>
            {
                try
                {
                    IAutoDrawing objAutoDrawing = AutoDrawingFactory.ChooseDrawingType(item);
                    item.SBU = Program.ObjCurrentUser.SBU;
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
        private async void btnJobCard_Click(object sender, EventArgs e)
        {
            if (execList.Count == 0) return;
            btnJobCard.Enabled = false;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = execList.Count;
            foreach (var item in execList)
            {
                tsslStatus.Text = item.Item + "(" + item.Module + ")正在打印...";
                await PrintJobCardAsync(item);
                tspbStatus.Value += 1;
            }
            tsslStatus.Text = "JobCard打印完成！";
            tspbStatus.Value = execList.Count;
            btnSubAll_Click(null, null);//清除执行数据
            btnJobCard.Enabled = true;
        }
        /// <summary>
        /// 以异步的方式打印JobCard
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Task PrintJobCardAsync(ModuleTree item)
        {
            return Task.Run(() =>
            {
                try
                {
                    new PrintReports().ExecPrintHoodJobCard(item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
        /// <summary>
        /// 导出标准烟罩DXF图纸和Cutlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnExportDxf_Click(object sender, EventArgs e)
        {
            if (execList.Count == 0) return;
            btnExportDxf.Enabled = false;
            //计算时间
            DateTime startTime = DateTime.Now;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = execList.Count;
            
            //创建下料图文件夹，默认再D盘MyProjects目录下（先判断文件夹是否存在）
            string dxfPath = @"D:\MyProjects\" + cobODPNo.Text+ @"\DXF-CUTLIST";
            if (!Directory.Exists(dxfPath))Directory.CreateDirectory(dxfPath);
            //以异步的方式开启SolidWorks程序并导图
            try
            {
                tsslStatus.Text = "正在打开(/连接)SolidWorks程序...";
                swApp = await SolidWorksSingleton.GetApplicationAsync();
                //遍历execList，创建项目模型存放地址，判断模型类型，查询参数，执行SolidWorks
                foreach (var item in execList)
                {
                    tsslStatus.Text = item.Item + "(" + item.Module + ")正在导图...";
                    //以异步的方式执行，让窗口可操作并且进度条更新
                    await exportDxfAsync(item, dxfPath, Program.ObjCurrentUser.UserId);
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
            tspbStatus.Value = execList.Count;
            btnSubAll_Click(null, null);//清除执行数据
            btnExportDxf.Enabled = true;
        }
        /// <summary>
        /// 异步方式开始导出dxf图纸
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dxfPath"></param>
        /// <returns></returns>
        private Task exportDxfAsync(ModuleTree item, string dxfPath,int userId)
        {
            return Task.Run(() =>
            {
                try
                {
                    new ExprotDxf().HoodAssyToDxf(swApp, item, dxfPath,userId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
        /// <summary>
        /// 保存装箱清单Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnHoodPackingList_Click(object sender, EventArgs e)
        {
            if (execList.Count == 0) return;
            btnHoodPackingList.Enabled = false;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = execList.Count;
            List<JobCard> jobCardList=new List<JobCard>();
            foreach (var item in execList)
            {
                tsslStatus.Text = item.Item + "(" + item.Module + ")正在导出数据...";
                JobCard objJobCard = new JobCardService().GetJobCard(item);
                //核对信息
                if (objJobCard.Length == 0)
                {
                    MessageBox.Show("编号" + objJobCard.Item + "中" + objJobCard.Module + "烟罩数据没有填写，请认真检查", "信息核对");
                    return;
                }
                jobCardList.Add(objJobCard);
                tspbStatus.Value += 1;
            }
            tsslStatus.Text = "正在将数据写入excel文件...";
            await ExportHoodPackingListAsync(jobCardList);
            tsslStatus.Text = "装箱清单导出完成，请在项目文件夹中查看！";
            tspbStatus.Value = execList.Count;
            btnHoodPackingList.Enabled = true;
        }
        /// <summary>
        /// 以异步的方式导出装箱信息
        /// </summary>
        /// <param name="jobCardList"></param>
        /// <returns></returns>
        private Task ExportHoodPackingListAsync(List<JobCard> jobCardList)
        {
            return Task.Run(() =>
            {
                try
                {
                    new PrintReports().ExecExportHoodPackingList(jobCardList);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
    }
}
