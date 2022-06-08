using DAL;
using Models;
using SolidWorksHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace Compass
{
    public partial class FrmSemiBom : MetroFramework.Forms.MetroForm
    {
        private readonly ProjectService _objProjectService = new ProjectService();
        readonly ModuleTreeService _objModuleTreeService = new ModuleTreeService();
        private Project _objProject = null;
        //使用BindingList动态绑定Dgv
        BindingList<ModuleTree> _waitingList = null;//待执行list，从项目中查询出来的
        private readonly string _sbu = Program.ObjCurrentUser.SBU;

        public FrmSemiBom()
        {
            InitializeComponent();
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
        }

        private async void BtnCreateSemiBom_Click(object sender, EventArgs e)
        {
            if (_waitingList.Count == 0) return;
            //判断有数据则不执行生成半成品？

            BtnCreateSemiBom.Enabled = false;
            Dictionary<string,int> semiBomDic=new Dictionary<string,int>();
            //计算时间
            DateTime startTime = DateTime.Now;            
            tspbStatus.Value = 0;
            tspbStatus.Step = 1;
            tspbStatus.Maximum = _waitingList.Count;            
            foreach (var item in _waitingList)
            {
                tsslStatus.Text = item.Item + "(" + item.Module + ")正在生成半成品清单...";
                await CreateSemiBomAsync(item,semiBomDic);
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
        private Task CreateSemiBomAsync(ModuleTree item,Dictionary<string, int> semiBomDic)
        {
            return Task.Run(() =>
            {
                try
                {
                    ICreateBom objCreateBom = CreateBomFactory.ChooseDrawingType(item);
                    item.SBU = Program.ObjCurrentUser.SBU;
                    objCreateBom.CreateSemiBom(item,semiBomDic);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
    }
}
