using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Models;
using DAL;
using MetroFramework.Forms;
using MetroFramework.Interfaces;


namespace Compass
{
    //声明委托
    public delegate void RefreshTreeDelegate();

    public partial class FrmModuleTree : Form
    {
        ProjectService objProjectService = new ProjectService();
        DrawingService objDrawingService = new DrawingService();
        ModuleTreeService objModuleTreeService = new ModuleTreeService();
        private string sbu = Program.ObjCurrentUser.SBU;

        //【3】创建委托变量
        public QuickBrowseDelegate QuickBrowseDeg = null;
        public ShowProjectInfoDelegate ShowProjectInfoDeg = null;

        private List<ModuleTree> moduleTreesList = new List<ModuleTree>();
        private Project objProject = null;
        public FrmModuleTree()
        {
            InitializeComponent();
            pbLabelImage.Visible = false;
            SetPermissions();
        }
        public FrmModuleTree(string odpNo) : this()
        {
            objProject = objProjectService.GetProjectByODPNo(odpNo,sbu);
            RefreshTree();
            this.tvModule.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvModule_AfterSelect);
            
        }
        /// <summary>
        /// 设置权限
        /// </summary>
        private void SetPermissions()
        {
            //管理员和技术部才能添加、编辑、删除模型
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2)
            {
                tsmiAddModule.Visible = true;
                tsmiEditModule.Visible = true;
                tsmiDeleteModule.Visible = true;
                tsmiEditPic.Visible = true;
            }
            else
            {
                tsmiAddModule.Visible = false;
                tsmiEditModule.Visible = false;
                tsmiDeleteModule.Visible = false;
                tsmiEditPic.Visible = false;
            }
        }


        private void RefreshTree()
        {
            if (objProject == null) return;
            //创建第一个节点
            this.tvModule.Nodes.Clear();//清空所有节点
            moduleTreesList.Clear();//清空节点集合，否则修改一次重复添加一次
            TreeNode rootNode = new TreeNode()
            {
                Text = objProject.ODPNo,
                Tag = objProject.ProjectId,//默认值,作为id使用
                ImageIndex = 4//添加图标
            };
            this.tvModule.Nodes.Add(rootNode);//将根节点添加到treeView控件中
            GetAllNodes();
            CreateChildNode(rootNode, objProject.ProjectId.ToString());
            this.tvModule.ExpandAll();
        }
        /// <summary>
        /// 获取所有节点信息
        /// </summary>
        private void GetAllNodes()
        {
            List<Drawing> drawingList = objDrawingService.GetDrawingsByProjectId(objProject.ProjectId.ToString(),sbu);
            foreach (var item in drawingList)
            {
                moduleTreesList.Add(new ModuleTree()
                {
                    ModuleTreeCode = "item" + item.DrawingPlanId.ToString(),
                    ParentCode = item.ProjectId.ToString(),
                    ModuleName =item.HoodType=="Hood"? item.Item+" ("+item.ModuleNo+"台)": item.Item + " (" + item.ModuleNo + "片)"
                });
            }
            List<ModuleTree> moduleList =
                objModuleTreeService.GetModuleTreesByProjectId(objProject.ProjectId.ToString(),sbu);
            foreach (var item in moduleList)
            {
                moduleTreesList.Add(new ModuleTree()
                {
                    ModuleTreeCode = item.ModuleTreeId.ToString(),
                    ParentCode = "item" + item.DrawingPlanId.ToString(),//防止父子ID重复，导致递归死循环,人为添加区别
                    ModuleName = item.Module + "-" + item.CategoryName
                });
            }
        }
        /// <summary>
        /// 创建子节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="parentCode"></param>
        private void CreateChildNode(TreeNode parentNode, string parentCode)
        {
            if (moduleTreesList == null) return;
            var subNodeList = from list in this.moduleTreesList
                              where list.ParentCode.Equals(parentCode)
                              select list;
            foreach (var item in subNodeList)
            {
                TreeNode node = new TreeNode()
                {
                    Text = item.ModuleName,
                    ImageIndex = item.ParentCode == objProject.ProjectId.ToString() ? 1 : 3,
                    Tag = item.ModuleTreeCode
                };
                parentNode.Nodes.Add(node);
                //递归调用
                CreateChildNode(node, item.ModuleTreeCode);
            }
        }
        /// <summary>
        /// 双击模型树添加标签图片,修改制图参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvModule_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (tvModule.SelectedNode == null) return;
            if (tvModule.SelectedNode.Level == 2)
            {
                string drawingPlanId = tvModule.SelectedNode.Parent.Tag.ToString();
                string moduleTreeId = tvModule.SelectedNode.Tag.ToString();
                if(drawingPlanId.Length<4)return;
                drawingPlanId = drawingPlanId.Substring(4);//除去item
                Drawing objDrawing = objDrawingService.GetDrawingById(drawingPlanId,sbu);
                ModuleTree objModuleTree = objModuleTreeService.GetModuleTreeById(moduleTreeId,sbu);
                //利用反射，打开修改模型参数窗口，同时实现传递窗口参数
                object[] parameters = new object[2];
                parameters[0] = objDrawing;
                parameters[1] = objModuleTree;
                MetroForm objFrmModel = (MetroForm)Assembly.Load("Compass").CreateInstance("Compass.Frm" + objModuleTree.CategoryName, true, BindingFlags.Default, null, parameters, null, null);
                objFrmModel.ShowDialog();
            }
        }
        /// <summary>
        /// 添加模型分段菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddModule_Click(object sender, EventArgs e)
        {
            if (tvModule.SelectedNode == null) return;
            if (tvModule.SelectedNode.Level == 1)
            {
                string drawingPlanId = tvModule.SelectedNode.Tag.ToString();
                drawingPlanId = drawingPlanId.Substring(4);//除去item
                Drawing objDrawing = objDrawingService.GetDrawingById(drawingPlanId,sbu);
                FrmCategoryTree objFrmCategoryTree = new FrmCategoryTree(objDrawing);
                //关联委托方法和委托变量
                objFrmCategoryTree.RefreshTreeDeg = RefreshTree;
                objFrmCategoryTree.Show();

                //模式窗口
                //DialogResult result = objFrmCategoryTree.ShowDialog();
                //if (result == DialogResult.OK)
                //{
                //    //更新模型树
                //    RefreshTree();
                //}

            }
        }
        /// <summary>
        /// 修改分段菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditModule_Click(object sender, EventArgs e)
        {
            if(tvModule.SelectedNode==null)return;
            if (tvModule.SelectedNode.Level == 2)
            {
                string drawingPlanId = tvModule.SelectedNode.Parent.Tag.ToString();
                drawingPlanId = drawingPlanId.Substring(4);//除去item
                Drawing objDrawing = objDrawingService.GetDrawingById(drawingPlanId,sbu);
                string moduleTreeId = tvModule.SelectedNode.Tag.ToString();
                ModuleTree objModuleTree = objModuleTreeService.GetModuleTreeById(moduleTreeId,sbu);
                FrmCategoryTree objFrmCategoryTree = new FrmCategoryTree(objDrawing, objModuleTree);
                DialogResult result = objFrmCategoryTree.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //更新模型树
                    RefreshTree();
                }
            }
        }
        /// <summary>
        /// 删除分段菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeleteModule_Click(object sender, EventArgs e)
        {
            if(tvModule.SelectedNode==null)return;
            if (tvModule.SelectedNode.Level == 2)
            {
                string drawingPlanId = tvModule.SelectedNode.Parent.Tag.ToString();
                drawingPlanId = drawingPlanId.Substring(4);//除去item
                Drawing objDrawing = objDrawingService.GetDrawingById(drawingPlanId,sbu);
                string moduleTreeId = tvModule.SelectedNode.Tag.ToString();
                ModuleTree objModuleTree = objModuleTreeService.GetModuleTreeById(moduleTreeId,sbu);
                DialogResult result = MessageBox.Show("确定要删除 Item为 " + objDrawing.Item + " 中的 " + objModuleTree.Module + " 分段吗？", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;
                //执行删除
                try
                {
                    //if(objModuleTreeService.DeleteModuleTree(moduleTreeId)==1) RefreshTree();
                    if (objModuleTreeService.DeleteModuleAndData(objModuleTree,sbu)) RefreshTree();
                    else MessageBox.Show("删除分段出错，请联系管理员查看后台数据。");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 单击选择Node时，分三层显示不同的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvModule_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(tvModule.SelectedNode==null)return;
            if (e.Node.Level == 1)
            {
                string drawingPlanId = e.Node.Tag.ToString();
                drawingPlanId = drawingPlanId.Substring(4);//除去item
                Drawing objDrawing = objDrawingService.GetDrawingById(drawingPlanId,sbu);
                if (objDrawing == null) return;
                pbLabelImage.Visible = true;
                pbLabelImage.Image = objDrawing.LabelImage.Length == 0
                    ? Image.FromFile("NoPic.png")
                    : (Image)new SerializeObjectToString().DeserializeObject(objDrawing.LabelImage);
            }
            else if (e.Node.Level == 2)
            {
                pbLabelImage.Visible = false;
                string drawingPlanId = tvModule.SelectedNode.Parent.Tag.ToString();
                drawingPlanId = drawingPlanId.Substring(4);//除去item
                Drawing objDrawing = objDrawingService.GetDrawingById(drawingPlanId,sbu);
                string moduleTreeId = tvModule.SelectedNode.Tag.ToString();
                ModuleTree objModuleTree = objModuleTreeService.GetModuleTreeById(moduleTreeId,sbu);
                //【5】调用委托
                QuickBrowseDeg(objDrawing, objModuleTree);
            }
        }
        /// <summary>
        /// 弹出自动绘图界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoDrawing_Click(object sender, EventArgs e)
        {
            //利用反射，打开自动作图窗口，Hood和Ceiling，同时实现传递窗口参数
            Project objProject = objProjectService.GetProjectByODPNo(tvModule.Nodes[0].Text.Trim(),sbu);
            object[] parameters = new object[1];
            parameters[0] = objProject.ODPNo;
            MetroForm objFrmAutoDrawing= (MetroForm)Assembly.Load("Compass").CreateInstance("Compass.Frm" + objProject.HoodType+"AutoDrawing", true, BindingFlags.Default, null, parameters, null, null);
            objFrmAutoDrawing.Show();
        }

        private void tvModule_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0) return;
            e.Node.ImageIndex = 2;//打开的文件夹图标
        }

        private void tvModule_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0) return;
            e.Node.ImageIndex = 1;//关闭的文件夹图标
        }
        /// <summary>
        /// 修改图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditPic_Click(object sender, EventArgs e)
        {
            if (tvModule.SelectedNode == null) return;
            TreeNode node = new TreeNode();
            if (tvModule.SelectedNode.Level == 1)
            {
                node = tvModule.SelectedNode;
            }
            else if (tvModule.SelectedNode.Level == 2) //三级目录
            {
                node = tvModule.SelectedNode.Parent;
            }
            string drawingPlanId = node.Tag.ToString();
            drawingPlanId = drawingPlanId.Substring(4);//除去item
            Drawing objDrawing = objDrawingService.GetDrawingById(drawingPlanId,sbu);
            if (objDrawing == null) return;
            FrmDrawing objFrmDrawing = new FrmDrawing(objDrawing);
            DialogResult result = objFrmDrawing.ShowDialog();
            if (result == DialogResult.OK) RefreshTree();
        }
        /// <summary>
        /// 显示项目详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiShowProjectInfo_Click(object sender, EventArgs e)
        {
            
            if (tvModule.SelectedNode == null) return;
            pbLabelImage.Visible = false;
            TreeNode node=new TreeNode();
            if (tvModule.SelectedNode.Level == 0)//一级目录
            {
                node = tvModule.SelectedNode;
            }
            else if(tvModule.SelectedNode.Level == 1)//二级目录，寻找父节点
            {
                node = tvModule.SelectedNode.Parent;
            }
            else if (tvModule.SelectedNode.Level == 2)//三级目录
            {
                node = tvModule.SelectedNode.Parent.Parent;
            }
            ShowProjectInfoDeg(node.Text);//使用委托显示项目信息
        }
    }
}
