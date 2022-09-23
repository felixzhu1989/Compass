using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common;
using DAL;
using Models;
using System.Reflection;
using Compass.WinForm;
using MetroFramework.Forms;

namespace Compass
{
    
    public partial class FrmQuickBrowse : Form
    {
        private readonly string _sbu = Program.ObjCurrentUser.SBU;
        private readonly HoodCutListService _objHoodCutListService = new HoodCutListService();
        readonly ModuleTreeService _objModuleTreeService = new ModuleTreeService();
        private Drawing _objDrawing = null;
        public FrmQuickBrowse()
        {
            InitializeComponent();
            //dgvCutList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//自动调整列宽
            //dgvQuickBrowse.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            SetPermissions();
        }
        public void ShowWithItem(Drawing drawing, ModuleTree tree)
        {
            if (drawing == null || tree == null) return;
            _objDrawing = drawing;
            RefreshData(drawing, tree);
            RefreshCutList(drawing, tree);
            ////天花烟罩不显示Cutlist
            //if (drawing.HoodType == "Ceiling")
            //{
            //    lblModule.Visible = false;
            //    btnPrintCutList.Visible = false;
            //    dgvCutList.Visible = false;
            //    dgvQuickBrowse.Height = this.Height - 40;
            //    dgvQuickBrowse.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            //}
        }
        

        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            //管理员和技术部才能添加、编辑、删除模型
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2)
            {
                tsmiDeleteCutList.Visible = true;
                dgvCutList.KeyDown += new KeyEventHandler(DgvCutList_KeyDown);
            }
            else
            {
                tsmiDeleteCutList.Visible = false;
                dgvCutList.KeyDown -= new KeyEventHandler(DgvCutList_KeyDown);
            }
        }
        private void RefreshCutList(Drawing drawing, ModuleTree tree)
        {
            lblModule.Text = drawing.Item + " (" + tree.Module + ")";//标题
            dgvCutList.DataSource = _objHoodCutListService.GetHoodCutListsByModuleTreeId(tree.ModuleTreeId.ToString());
        }
        /// <summary>
        /// 封装更新表中的数据方法
        /// </summary>
        /// <param name="drawing"></param>
        /// <param name="tree"></param>
        private void RefreshData(Drawing drawing, ModuleTree tree)
        {
            lblTitle.Text = drawing.ODPNo + "-" + tree.CategoryName;
            //使用反射获取DAL
            IModelService modelService =
                (IModelService)Assembly.Load("DAL").CreateInstance("DAL." + tree.CategoryName + "Service");
            dgvQuickBrowse.DataSource = modelService.GetModelByDataSet(drawing.ProjectId.ToString()).Tables[0];
        }

        /// <summary>
        /// 双击行修改制图参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvQuickBrowse_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvQuickBrowse.RowCount == 0) return;
            if (dgvQuickBrowse.CurrentRow == null) return;

            string moduleTreeId = dgvQuickBrowse.CurrentRow.Cells["ModuleTreeId"].Value.ToString();
            ModuleTree objModuleTree = _objModuleTreeService.GetModuleTreeById(moduleTreeId,_sbu);
            DialogResult result = DialogResult.No;
            //利用反射，打开修改模型参数窗口，同时实现传递窗口参数
            object[] parameters = new object[2];
            parameters[0] = _objDrawing;
            parameters[1] = objModuleTree;
            MetroForm objFrmModel = (MetroForm)Assembly.Load("Compass").CreateInstance("Compass.Frm" + objModuleTree.CategoryName, true, BindingFlags.Default, null, parameters, null, null);
            result = objFrmModel.ShowDialog();
            if (result == DialogResult.OK)
            {
                RefreshData(_objDrawing, objModuleTree);//更新数据表
            }
        }

        private void DgvQuickBrowse_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(dgvQuickBrowse, e);
        }

        private void DgvCutList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(dgvCutList, e);
        }
        /// <summary>
        /// CutList删除多行数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiDeleteCutList_Click(object sender, EventArgs e)
        {
            if (dgvCutList.RowCount == 0) return;
            DialogResult result = MessageBox.Show("确定删除选中的多行数据吗?", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No || dgvCutList.SelectedRows.Count == 0) return;
            string moduleTreeId = dgvCutList.Rows[0].Cells["ModuleTreeId"].Value.ToString();
            List<int> idList = new List<int>();
            foreach (DataGridViewRow row in dgvCutList.SelectedRows)
            {
                idList.Add(Convert.ToInt32(row.Cells["CutListId"].Value));//将id添加到集合中
            }
            try
            {
                if (_objHoodCutListService.DeleteCutListByTran(idList))
                    dgvCutList.DataSource = _objHoodCutListService.GetHoodCutListsByModuleTreeId(moduleTreeId); ;
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
        private void DgvCutList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46) TsmiDeleteCutList_Click(null, null);
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
            ModuleTree tree =
                _objModuleTreeService.GetModuleTreeById(dgvCutList.Rows[0].Cells["ModuleTreeId"].Value.ToString(),_sbu);
            if (new PrintReports().ExecPrintHoodCutList(_objDrawing, tree, dgvCutList)) MessageBox.Show("CutList打印完成", "打印完成");
            btnPrintCutList.Enabled = true;
            btnPrintCutList.Text = "打印CustList";
        }

        private void AddCutList_Click(object sender, EventArgs e)
        {
            var moduleTreeId =(int) dgvCutList.Rows[0].Cells["ModuleTreeId"].Value;
            FrmAddCutList frm = new FrmAddCutList(moduleTreeId);
            if (frm.ShowDialog()==DialogResult.OK)
            {
               var objModuleTree= _objModuleTreeService.GetModuleTreeById(moduleTreeId.ToString(),_sbu);
                RefreshCutList(_objDrawing, objModuleTree);
            }
        }
    }
}
