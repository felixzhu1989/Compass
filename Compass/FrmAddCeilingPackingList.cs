using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmAddCeilingPackingList :MetroFramework.Forms.MetroForm
    {
        private readonly Project _objProject = null;
        private CeilingAccessory _objCeilingAccessory = null;
        readonly DrawingService _objDrawingService = new DrawingService();
        private readonly List<Drawing> _objDrawings = null;
        readonly CeilingAccessoryService _objCeilingAccessoryService=new CeilingAccessoryService();
        private readonly string _sbu = Program.ObjCurrentUser.SBU;

        public FrmAddCeilingPackingList()
        {
            InitializeComponent();
        }
        public FrmAddCeilingPackingList(Project project):this()
        {
            _objProject = project;
            if (_objProject == null) return;
            //创建第一个节点
            this.tvCeilingAccessories.Nodes.Clear();
            TreeNode rootNode=new TreeNode()
            {
                Text = "配件目录",
                ImageIndex = 4,
                Tag = _objProject.ProjectId
            };
            this.tvCeilingAccessories.Nodes.Add(rootNode);
            List<CeilingAccessory> ceilingAccessoriesList =
                _objCeilingAccessoryService.GetCeilingAccessoriesByWhereSql("");
            foreach (var item in ceilingAccessoriesList)
            {
                TreeNode node=new TreeNode()
                {
                    Text=item.PartDescription,
                    ImageIndex = 3,
                    Tag = item.CeilingAccessoryId
                };
                rootNode.Nodes.Add(node);
            }
            this.tvCeilingAccessories.ExpandAll();
            _objDrawings = _objDrawingService.GetDrawingsByProjectId(_objProject.ProjectId.ToString(),_sbu);
        }
        /// <summary>
        /// 选中节点后复制给配件对象，并反填数据到txt中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvCeilingAccessories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //先判断是二级节点才执行任务
            if (e.Node.Level == 1)
            {
                //需要执行的任务
                _objCeilingAccessory = _objCeilingAccessoryService.GetCeilingAccessoryById(e.Node.Tag.ToString());
                txtPartDescription.Text = _objCeilingAccessory.PartDescription;
                if (_objCeilingAccessory.CeilingAccessoryId == "7001") txtPartDescription.ReadOnly = false;
                else txtPartDescription.ReadOnly = true;
                txtPartNo.Text = _objCeilingAccessory.PartNo;
                txtRemark.Text = _objCeilingAccessory.Remark;
                txtQuantity.Text = _objCeilingAccessory.Quantity.ToString();
                txtUnit.Text = _objCeilingAccessory.Unit;
                txtLength.Text = _objCeilingAccessory.Length;
                txtWidth.Text = _objCeilingAccessory.Width;
                txtHeight.Text = _objCeilingAccessory.Height;
                btnAddCeilingAccessory.Tag = _objCeilingAccessory.CeilingAccessoryId;
            }
        }
        /// <summary>
        /// 添加配件按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddCeilingAccessory_Click(object sender, EventArgs e)
        {
            //将配件对象添加项目编号，并提交SQL
            if(_objCeilingAccessory==null)return;

            _objCeilingAccessory.PartDescription = txtPartDescription.Text;
            _objCeilingAccessory.PartNo = txtPartNo.Text.Trim();
            _objCeilingAccessory.Remark = txtRemark.Text.Trim();
            _objCeilingAccessory.Quantity =Convert.ToInt32(txtQuantity.Text.Trim());
            _objCeilingAccessory.Length = txtLength.Text.Trim();
            _objCeilingAccessory.Width = txtWidth.Text.Trim();
            _objCeilingAccessory.Height = txtHeight.Text.Trim();
            _objCeilingAccessory.ProjectId = _objProject.ProjectId;
            _objCeilingAccessory.UserId = Program.ObjCurrentUser.UserId;
            _objCeilingAccessory.Location = _objDrawings[0].Item;//填写区域
            List<CeilingAccessory> ceilingAccessoriesList = new List<CeilingAccessory>{_objCeilingAccessory};
            _objCeilingAccessoryService.ImportCeilingPackingListByTran(ceilingAccessoriesList);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
