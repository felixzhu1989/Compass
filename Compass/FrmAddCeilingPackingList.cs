using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmAddCeilingPackingList :MetroFramework.Forms.MetroForm
    {
        private Project objProject = null;
        private CeilingAccessory objCeilingAccessory = null;
        DrawingService objDrawingService = new DrawingService();
        private List<Drawing> objDrawings = null;
        CeilingAccessoryService objCeilingAccessoryService=new CeilingAccessoryService();
        private string sbu = Program.ObjCurrentUser.SBU;

        public FrmAddCeilingPackingList()
        {
            InitializeComponent();
        }
        public FrmAddCeilingPackingList(Project project):this()
        {
            objProject = project;
            if (objProject == null) return;
            //创建第一个节点
            this.tvCeilingAccessories.Nodes.Clear();
            TreeNode rootNode=new TreeNode()
            {
                Text = "配件目录",
                ImageIndex = 4,
                Tag = objProject.ProjectId
            };
            this.tvCeilingAccessories.Nodes.Add(rootNode);
            List<CeilingAccessory> ceilingAccessoriesList =
                objCeilingAccessoryService.GetCeilingAccessoriesByWhereSql("");
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
            objDrawings = objDrawingService.GetDrawingsByProjectId(objProject.ProjectId.ToString(),sbu);
        }
        /// <summary>
        /// 选中节点后复制给配件对象，并反填数据到txt中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvCeilingAccessories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //先判断是二级节点才执行任务
            if (e.Node.Level == 1)
            {
                //需要执行的任务
                objCeilingAccessory = objCeilingAccessoryService.GetCeilingAccessoryById(e.Node.Tag.ToString());
                txtPartDescription.Text = objCeilingAccessory.PartDescription;
                if (objCeilingAccessory.CeilingAccessoryId == "7001") txtPartDescription.ReadOnly = false;
                else txtPartDescription.ReadOnly = true;
                txtPartNo.Text = objCeilingAccessory.PartNo;
                txtRemark.Text = objCeilingAccessory.Remark;
                txtQuantity.Text = objCeilingAccessory.Quantity.ToString();
                txtUnit.Text = objCeilingAccessory.Unit;
                txtLength.Text = objCeilingAccessory.Length;
                txtWidth.Text = objCeilingAccessory.Width;
                txtHeight.Text = objCeilingAccessory.Height;
                btnAddCeilingAccessory.Tag = objCeilingAccessory.CeilingAccessoryId;
            }
        }
        /// <summary>
        /// 添加配件按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCeilingAccessory_Click(object sender, EventArgs e)
        {
            //将配件对象添加项目编号，并提交SQL
            if(objCeilingAccessory==null)return;

            objCeilingAccessory.PartDescription = txtPartDescription.Text;
            objCeilingAccessory.PartNo = txtPartNo.Text.Trim();
            objCeilingAccessory.Remark = txtRemark.Text.Trim();
            objCeilingAccessory.Quantity =Convert.ToInt32(txtQuantity.Text.Trim());
            objCeilingAccessory.Length = txtLength.Text.Trim();
            objCeilingAccessory.Width = txtWidth.Text.Trim();
            objCeilingAccessory.Height = txtHeight.Text.Trim();
            objCeilingAccessory.ProjectId = objProject.ProjectId;
            objCeilingAccessory.UserId = Program.ObjCurrentUser.UserId;
            objCeilingAccessory.Location = objDrawings[0].Item;//填写区域
            List<CeilingAccessory> ceilingAccessoriesList = new List<CeilingAccessory>();
            ceilingAccessoriesList.Add(objCeilingAccessory);
            objCeilingAccessoryService.ImportCeilingPackingListByTran(ceilingAccessoriesList);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
