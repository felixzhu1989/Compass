using Common;
using DAL;
using Models;
using SolidWorksHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compass
{
    public partial class FrmSemiBom : MetroFramework.Forms.MetroForm
    {
        private readonly ProjectService _objProjectService = new ProjectService();
        private readonly ModuleTreeService _objModuleTreeService = new ModuleTreeService();
        private readonly SemiBomService _objSemiBomService = new SemiBomService();
        private Project _objProject = null;
        //使用BindingList动态绑定Dgv
        BindingList<ModuleTree> _waitingList;//待执行list，从项目中查询出来的
        List<SemiBom> _semiBomList;
        List<SemiBom> _semiBomTotalList;
        BindingList<SemiBom> _bindingList;
        private readonly string _sbu = Program.ObjCurrentUser.SBU;

        public FrmSemiBom()
        {
            InitializeComponent();
            _semiBomList=new List<SemiBom>();
            _semiBomTotalList=new List<SemiBom>();  
            dgvSemiBom.AutoGenerateColumns = false;
            dgvSemiBomTotal.AutoGenerateColumns = false;
            _bindingList=new BindingList<SemiBom>(_semiBomTotalList);
            dgvSemiBomTotal.DataSource=_bindingList;
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
        #region 单例模式
        private static FrmSemiBom instance;
        public static FrmSemiBom GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new FrmSemiBom();
            }
            return instance;
        }
        public void HandlerOdpNo(string odpNo)
        {
            if (odpNo.Length != 0) cobODPNo.Text = odpNo;
        }
        #endregion

        /// <summary>
        /// 下拉选择项目后自动切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CobODPNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobODPNo.SelectedIndex == -1) return;
            _objProject = _objProjectService.GetProjectByODPNo(cobODPNo.Text, Program.ObjCurrentUser.SBU);
            txtBPONo.Text = _objProject.BPONo;
            txtProjectName.Text = _objProject.ProjectName;
            _waitingList = new BindingList<ModuleTree>(_objModuleTreeService.GetModuleTreesByProjectId(cobODPNo.SelectedValue.ToString(), _sbu));
            dgvSemiBomTotal.DataSource = _semiBomTotalList;
            RefreshDgv();
        }
        private void RefreshDgv()
        {
            _semiBomList.Clear();
            _semiBomList =_objSemiBomService.GetSemiBomsByProjectId(_objProject.ProjectId.ToString());
            dgvSemiBom.DataSource =_semiBomList;
        }
        /// <summary>
        /// 添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvWaitingList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(dgvSemiBom, e);
        }

        private async void BtnCreateSemiBom_Click(object sender, EventArgs e)
        {
            if (_waitingList.Count == 0) return;
            if (_semiBomList.Count!=0)
            {
                MessageBox.Show("已生成半成品清单，请删除现有清单再重新生成。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //判断有数据则不执行生成半成品？

            BtnCreateSemiBom.Enabled = false;
            Dictionary<string, int> semiBomDic = new Dictionary<string, int>();
            //计算时间
            DateTime startTime = DateTime.Now;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = _waitingList.Count;
            foreach (var item in _waitingList)
            {
                tsslStatus.Text = item.Item + "(" + item.Module + ")正在生成半成品清单...";
                await CreateSemiBomAsync(item, semiBomDic);
                tspbStatus.Value += 1;
            }
            //将字典转化成List，同时添加上项目号
            List<SemiBom> semiBomList = new List<SemiBom>();
            foreach (var semi in semiBomDic)
            {
                semiBomList.Add(new SemiBom() { ProjectId=_objProject.ProjectId, DrawingNum=semi.Key, Quantity=semi.Value });
            }
            //基于事务semiBomList提交SQLServer
            if (semiBomList.Count==0) return;
            SemiBomService objSemiBomService = new SemiBomService();
            try
            {
                if (objSemiBomService.ImportSemiBom(semiBomList)) semiBomList.Clear();
                RefreshDgv();
            }
            catch (Exception ex)
            {
                throw new Exception($"半成品清单导入数据库失败" + ex.Message);
            }

            //保存Excel？


            TimeSpan timeSpan = DateTime.Now - startTime;
            tsslStatus.Text = $"半成品清单导出完成！耗时：{timeSpan.TotalSeconds}秒";
            tspbStatus.Value = _waitingList.Count;
            BtnCreateSemiBom.Enabled = true;
        }
        /// <summary>
        /// 以异步的方式计算半成品清单
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Task CreateSemiBomAsync(ModuleTree item, Dictionary<string, int> semiBomDic)
        {
            return Task.Run(() =>
            {
                try
                {
                    ICreateBom objCreateBom = CreateBomFactory.ChooseDrawingType(item);
                    item.SBU = Program.ObjCurrentUser.SBU;
                    objCreateBom.CreateSemiBom(item, semiBomDic);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
        /// <summary>
        /// SemiBom删除多行数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiDeleteSemiBom_Click(object sender, EventArgs e)
        {
            if (dgvSemiBom.RowCount == 0) return;
            DialogResult result = MessageBox.Show("确定删除选中的多行数据吗?", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No || dgvSemiBom.SelectedRows.Count == 0) return;
            string moduleTreeId = dgvSemiBom.Rows[0].Cells["SemiBomId"].Value.ToString();
            List<int> idList = new List<int>();
            foreach (DataGridViewRow row in dgvSemiBom.SelectedRows)
            {
                idList.Add(Convert.ToInt32(row.Cells["SemiBomId"].Value));//将id添加到集合中
            }
            try
            {
                if (_objSemiBomService.DeleteSemiBomByTran(idList))
                    RefreshDgv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 将订单半成品汇总
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToTotal_Click(object sender, EventArgs e)
        {
            if (txtODPList.Text.Contains(_objProject.ODPNo))
            {
                MessageBox.Show("请勿重复添加！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //将ODP添加到文本框
            string odpList = txtODPList.Text;
            if (odpList.Length==0) odpList=_objProject.ODPNo;
            else odpList = $"{txtODPList.Text},{_objProject.ODPNo}"; ;
            txtODPList.Text=odpList;
            //汇总数据
            foreach (var semi in _semiBomList)
            {
                if (_semiBomTotalList.Exists(s => s.DrawingNum==semi.DrawingNum))
                {
                    var semiTotal = _semiBomTotalList.FirstOrDefault(s => s.DrawingNum==semi.DrawingNum);
                    semiTotal.Quantity+=semi.Quantity;
                }
                else
                {
                    _semiBomTotalList.Add(semi);
                }                
            }
            _bindingList=new BindingList<SemiBom>(_semiBomTotalList);
            dgvSemiBomTotal.DataSource=_bindingList;
        }
        /// <summary>
        /// 清空汇总列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearTotal_Click(object sender, EventArgs e)
        {
            txtODPList.Text="";
            _bindingList.Clear();
        }
        /// <summary>
        /// 导出半成品清单Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (_semiBomTotalList.Count == 0) return;
            btnExportExcel.Enabled = false;
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = _semiBomTotalList.Count;            
            await ExportSemiBomAsync(txtODPList.Text,_semiBomTotalList);
            tsslStatus.Text = "半成品清单清单导出完成，请在桌面查看！";
            tspbStatus.Value = _semiBomTotalList.Count;
            btnExportExcel.Enabled = true;
        }

        /// <summary>
        /// 以异步的方式导出装箱信息
        /// </summary>
        /// <param name="jobCardList"></param>
        /// <returns></returns>
        private Task ExportSemiBomAsync(string odpListStr,List<SemiBom> semiBomTotalList)
        {
            return Task.Run(() =>
            {
                try
                {
                    new PrintReports().ExecExportSemiBom(txtODPList.Text, _semiBomTotalList);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
    }
}
