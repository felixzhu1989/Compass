﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
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
        private readonly ProjectService _objProjectService = new ProjectService();
        readonly ModuleTreeService _objModuleTreeService = new ModuleTreeService();
        private Project _objProject = null;
        //使用BindingList动态绑定Dgv
        BindingList<ModuleTree> _waitingList = null;//待执行list，从项目中查询出来的

        readonly BindingList<ModuleTree> _execList = new BindingList<ModuleTree>();//执行list，手动添加的
        //solidWorks程序
        private SldWorks _swApp;
        private readonly string _sbu = Program.ObjCurrentUser.SBU;
        
        private FrmHoodAutoDrawing()
        {
            InitializeComponent();
            dgvWaitingList.AutoGenerateColumns = false;
            dgvExecList.AutoGenerateColumns = false;
            IniCobOdpNo();
        }
        #region 单例模式
        private static FrmHoodAutoDrawing instance;
        public static FrmHoodAutoDrawing GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new FrmHoodAutoDrawing();
            }
            return instance;
        }
        public void HandlerOdpNo(string odpNo)
        {
            if (odpNo.Length != 0) cobODPNo.Text = odpNo;
        }

        #endregion
        private void BtnIniCobOdpNo_Click(object sender, EventArgs e)
        {
            IniCobOdpNo();
        }
        public void IniCobOdpNo()
        {
            cobODPNo.SelectedIndexChanged -= new EventHandler(CobODPNo_SelectedIndexChanged);
            //项目编号下拉框
            cobODPNo.DataSource = _objProjectService.GetProjectsByHoodType("Hood", _sbu);
            cobODPNo.DisplayMember = "ODPNo";
            cobODPNo.ValueMember = "ProjectId";
            cobODPNo.SelectedIndex = -1;
            cobODPNo.SelectedIndexChanged += new EventHandler(CobODPNo_SelectedIndexChanged);
        }


        //public static FrmHoodAutoDrawing GetSingle()
        //{
        //    if (FrmSingle == null || FrmSingle.IsDisposed) FrmSingle = new FrmHoodAutoDrawing();
        //    return FrmSingle;
        //}
        //public static FrmHoodAutoDrawing GetSingle(string odpNo)
        //{
        //    if (FrmSingle == null || FrmSingle.IsDisposed) FrmSingle = new FrmHoodAutoDrawing(odpNo);
        //    return FrmSingle;
        //}

        /// <summary>
        /// 下拉选择项目后自动切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobODPNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobODPNo.SelectedIndex == -1) return;
            _objProject = _objProjectService.GetProjectByODPNo(cobODPNo.Text,Program.ObjCurrentUser.SBU);
            txtBPONo.Text = _objProject.BPONo;
            txtProjectName.Text = _objProject.ProjectName;
            _execList.Clear();
            _waitingList = new BindingList<ModuleTree>(_objModuleTreeService.GetModuleTreesByProjectId(cobODPNo.SelectedValue.ToString(),_sbu));
            RefreshDgv();
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void RefreshDgv()
        {
            dgvWaitingList.DataSource = _waitingList;
            dgvExecList.DataSource = _execList;
        }
        /// <summary>
        /// 添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvWaitingList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(dgvWaitingList, e);
        }
        /// <summary>
        /// 添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvExecList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(dgvExecList, e);
        }
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
            DateTime startTime=DateTime.Now;
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
        private Task AutoDrawingAsync(ModuleTree item,string projectPath)
        {
            return Task.Run(() =>
            {
                try
                {
                    IAutoDrawing objAutoDrawing = AutoDrawingFactory.ChooseDrawingType(item);
                    item.SBU = Program.ObjCurrentUser.SBU;
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
        private async void BtnJobCard_Click(object sender, EventArgs e)
        {
            if (_execList.Count == 0) return;
            btnJobCard.Enabled = false;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = _execList.Count;
            foreach (var item in _execList)
            {
                tsslStatus.Text = item.Item + "(" + item.Module + ")正在打印...";
                await PrintJobCardAsync(item);
                tspbStatus.Value += 1;
            }
            tsslStatus.Text = "JobCard打印完成！";
            tspbStatus.Value = _execList.Count;
            BtnSubAll_Click(null, null);//清除执行数据
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
        private async void BtnExportDxf_Click(object sender, EventArgs e)
        {
            if (_execList.Count == 0) return;
            btnExportDxf.Enabled = false;
            //计算时间
            DateTime startTime = DateTime.Now;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = _execList.Count;
            
            //创建下料图文件夹，默认再D盘MyProjects目录下（先判断文件夹是否存在）
            string dxfPath = @"D:\MyProjects\" + cobODPNo.Text+ @"\DXF-CUTLIST";
            if (!Directory.Exists(dxfPath))Directory.CreateDirectory(dxfPath);
            //以异步的方式开启SolidWorks程序并导图
            try
            {
                tsslStatus.Text = "正在打开(/连接)SolidWorks程序...";
                _swApp = await SolidWorksSingleton.GetApplicationAsync();
                //遍历execList，创建项目模型存放地址，判断模型类型，查询参数，执行SolidWorks
                foreach (var item in _execList)
                {
                    tsslStatus.Text = item.Item + "(" + item.Module + ")正在导图...";
                    //以异步的方式执行，让窗口可操作并且进度条更新
                    await ExportDxfAsync(item, dxfPath, Program.ObjCurrentUser.UserId);
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
            tspbStatus.Value = _execList.Count;
            BtnSubAll_Click(null, null);//清除执行数据
            btnExportDxf.Enabled = true;
        }
        /// <summary>
        /// 异步方式开始导出dxf图纸
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dxfPath"></param>
        /// <returns></returns>
        private Task ExportDxfAsync(ModuleTree item, string dxfPath,int userId)
        {
            return Task.Run(() =>
            {
                try
                {
                    new ExprotDxf().HoodAssyToDxf(_swApp, item, dxfPath,userId);
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
        private async void BtnHoodPackingList_Click(object sender, EventArgs e)
        {
            if (_execList.Count == 0) return;
            btnHoodPackingList.Enabled = false;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = _execList.Count;
            List<JobCard> jobCardList=new List<JobCard>();
            foreach (var item in _execList)
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
            tspbStatus.Value = _execList.Count;
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

        //打印英文标签页
        private async void btnEnglishLabel_Click(object sender, EventArgs e)
        {
            if (_execList.Count == 0) return;
            btnEnglishLabel.Enabled = false;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = _execList.Count;
            foreach (var item in _execList)
            {
                tsslStatus.Text = item.Item + "(" + item.Module + ")正在打印...";
                await PrintEngLabelAsync(item);
                tspbStatus.Value += 1;
            }
            tsslStatus.Text = "英文标签打印完成！";
            tspbStatus.Value = _execList.Count;
            BtnSubAll_Click(null, null);//清除执行数据
            btnEnglishLabel.Enabled = true;
        }
        /// <summary>
        /// 以异步的方式打印EngLabel
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Task PrintEngLabelAsync(ModuleTree item)
        {
            return Task.Run(() =>
            {
                try
                {
                    new PrintReports().ExecPrintEngLabel(item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
        /// <summary>
        /// 打印终检单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnFinalInspection_Click(object sender, EventArgs e)
        {
            if (_execList.Count == 0) return;
            btnFinalInspection.Enabled = false;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = _execList.Count;
            foreach (var item in _execList)
            {
                tsslStatus.Text = item.Item + "(" + item.Module + ")正在打印...";
                await PrintFinalInspectionAsync(item);
                tspbStatus.Value += 1;
            }
            tsslStatus.Text = "最终检验单打印完成！";
            tspbStatus.Value = _execList.Count;
            BtnSubAll_Click(null, null);//清除执行数据
            btnFinalInspection.Enabled = true;
        }
        private Task PrintFinalInspectionAsync(ModuleTree item)
        {
            return Task.Run(() =>
            {
                try
                {
                    new PrintReports().ExecPrintFinalInspection(item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
    }
}