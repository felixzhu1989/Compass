using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using Common;
using Models;
using DAL;
using MetroFramework.Forms;


namespace Compass
{
    
    public partial class FrmModuleTree : Form
    {
        readonly ProjectService _objProjectService = new ProjectService();
        readonly DrawingService _objDrawingService = new DrawingService();
        readonly ModuleTreeService _objModuleTreeService = new ModuleTreeService();
        private readonly string _sbu = Program.ObjCurrentUser.SBU;
        readonly Action<Drawing, ModuleTree> _quickBrowseDeg;

        private readonly List<ModuleTree> _moduleTreesList = new List<ModuleTree>();
        private Project _objProject;
        public FrmModuleTree()
        {
            InitializeComponent();
            pbLabelImage.Visible = false;
            SetPermissions();
        }
        public FrmModuleTree(Action<Drawing, ModuleTree> del) :this()
        {
            _quickBrowseDeg = del;
        }

        #region 单例模式
        public void ShowWithOdpNo(string odpNo)
        {
            tvModule.AfterSelect -= TvModule_AfterSelect;
            _objProject = _objProjectService.GetProjectByODPNo(odpNo, _sbu);
            RefreshTree();
            if (_objProject.HoodType == "Ceiling") tsmiCeilingAssy.Visible = true;
            else tsmiCeilingAssy.Visible = false;
            tvModule.AfterSelect += TvModule_AfterSelect;
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

        #region 初始化模型树
        public void RefreshTree()
        {
            if (_objProject == null) return;
            //创建第一个节点
            tvModule.Nodes.Clear();//清空所有节点
            _moduleTreesList.Clear();//清空节点集合，否则修改一次重复添加一次
            TreeNode rootNode = new TreeNode()
            {
                Text = _objProject.ODPNo,
                Tag = _objProject.ProjectId,//默认值,作为id使用
                ImageIndex = 4//添加图标
            };
            tvModule.Nodes.Add(rootNode);//将根节点添加到treeView控件中
            GetAllNodes();
            CreateChildNode(rootNode, _objProject.ProjectId.ToString());
            tvModule.ExpandAll();
            tvModule.SelectedNode = rootNode;
        }
        /// <summary>
        /// 获取所有节点信息
        /// </summary>
        private void GetAllNodes()
        {
            List<Drawing> drawingList = _objDrawingService.GetDrawingsByProjectId(_objProject.ProjectId.ToString(), _sbu);
            foreach (var item in drawingList)
            {
                _moduleTreesList.Add(new ModuleTree()
                {
                    ModuleTreeCode = "item" + item.DrawingPlanId.ToString(),
                    ParentCode = item.ProjectId.ToString(),
                    ModuleName = item.HoodType == "Hood" ? item.Item + " (" + item.ModuleNo + "台)" : item.Item + " (" + item.ModuleNo + "片)"
                });
            }
            List<ModuleTree> moduleList =
                _objModuleTreeService.GetModuleTreesByProjectId(_objProject.ProjectId.ToString(), _sbu);
            foreach (var item in moduleList)
            {
                _moduleTreesList.Add(new ModuleTree()
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
            if (_moduleTreesList == null) return;
            var subNodeList = from list in _moduleTreesList
                              where list.ParentCode.Equals(parentCode)
                              select list;
            foreach (var item in subNodeList)
            {
                TreeNode node = new TreeNode()
                {
                    Text = item.ModuleName,
                    ImageIndex = item.ParentCode == _objProject.ProjectId.ToString() ? 1 : 3,
                    Tag = item.ModuleTreeCode
                };
                parentNode.Nodes.Add(node);
                //递归调用
                CreateChildNode(node, item.ModuleTreeCode);
            }
        }
        #endregion

        #region 编辑模型树
        /// <summary>
        /// 双击模型树添加标签图片,修改制图参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvModule_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (tvModule.SelectedNode == null) return;
            if (tvModule.SelectedNode.Level == 2)
            {
                string drawingPlanId = tvModule.SelectedNode.Parent.Tag.ToString();
                string moduleTreeId = tvModule.SelectedNode.Tag.ToString();
                if (drawingPlanId.Length < 4) return;
                drawingPlanId = drawingPlanId.Substring(4);//除去item
                Drawing objDrawing = _objDrawingService.GetDrawingById(drawingPlanId, _sbu);
                ModuleTree objModuleTree = _objModuleTreeService.GetModuleTreeById(moduleTreeId, _sbu);
                //利用反射，打开修改模型参数窗口，同时实现传递窗口参数
                object[] parameters = new object[2];
                parameters[0] = objDrawing;
                parameters[1] = objModuleTree;
                MetroForm objFrmModel = (MetroForm)Assembly.Load("Compass").CreateInstance("Compass.Frm" + objModuleTree.CategoryName, true, BindingFlags.Default, null, parameters, null, null);
                objFrmModel.Show();
            }
        }
        /// <summary>
        /// 添加模型分段菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiAddModule_Click(object sender, EventArgs e)
        {
            if (tvModule.SelectedNode == null) return;
            if (tvModule.SelectedNode.Level == 1)
            {
                string drawingPlanId = tvModule.SelectedNode.Tag.ToString();
                drawingPlanId = drawingPlanId.Substring(4);//除去item
                Drawing objDrawing = _objDrawingService.GetDrawingById(drawingPlanId, _sbu);
                SingletonObject.GetSingleton.FrmCt?.AddModule(objDrawing);
            }
        }
        /// <summary>
        /// 修改分段菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiEditModule_Click(object sender, EventArgs e)
        {
            if (tvModule.SelectedNode == null) return;
            if (tvModule.SelectedNode.Level == 2)
            {
                string drawingPlanId = tvModule.SelectedNode.Parent.Tag.ToString();
                drawingPlanId = drawingPlanId.Substring(4);//除去item
                Drawing objDrawing = _objDrawingService.GetDrawingById(drawingPlanId, _sbu);
                string moduleTreeId = tvModule.SelectedNode.Tag.ToString();
                ModuleTree objModuleTree = _objModuleTreeService.GetModuleTreeById(moduleTreeId, _sbu);
                SingletonObject.GetSingleton.FrmCt?.EditModule(objDrawing, objModuleTree);
            }
        }
        /// <summary>
        /// 删除分段菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiDeleteModule_Click(object sender, EventArgs e)
        {
            if (tvModule.SelectedNode == null) return;
            if (tvModule.SelectedNode.Level == 2)
            {
                string drawingPlanId = tvModule.SelectedNode.Parent.Tag.ToString();
                drawingPlanId = drawingPlanId.Substring(4);//除去item
                Drawing objDrawing = _objDrawingService.GetDrawingById(drawingPlanId, _sbu);
                string moduleTreeId = tvModule.SelectedNode.Tag.ToString();
                ModuleTree objModuleTree = _objModuleTreeService.GetModuleTreeById(moduleTreeId, _sbu);
                DialogResult result = MessageBox.Show("确定要删除 Item为 " + objDrawing.Item + " 中的 " + objModuleTree.Module + " 分段吗？", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;
                //执行删除
                try
                {
                    //if(objModuleTreeService.DeleteModuleTree(moduleTreeId)==1) RefreshTree();
                    if (_objModuleTreeService.DeleteModuleAndData(objModuleTree, _sbu)) RefreshTree();
                    else MessageBox.Show("删除分段出错，请联系管理员查看后台数据。");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 修改图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiEditPic_Click(object sender, EventArgs e)
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
            Drawing objDrawing = _objDrawingService.GetDrawingById(drawingPlanId, _sbu);
            if (objDrawing == null) return;
            FrmDrawing objFrmDrawing = new FrmDrawing(objDrawing);
            DialogResult result = objFrmDrawing.ShowDialog();
            if (result == DialogResult.OK) RefreshTree();
        }
        #endregion

        /// <summary>
        /// 单击选择Node时，分三层显示不同的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvModule_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(tvModule.SelectedNode==null)return;
            
            if (e.Node.Level == 1)//选择了Item
            {
                string drawingPlanId = e.Node.Tag.ToString();
                drawingPlanId = drawingPlanId.Substring(4);//除去item
                Drawing objDrawing = _objDrawingService.GetDrawingById(drawingPlanId,_sbu);
                if (objDrawing == null) return;
                pbLabelImage.Visible = true;
                pbLabelImage.Image = objDrawing.LabelImage.Length == 0
                    ? Image.FromFile("NoPic.png")
                    : (Image)new SerializeObjectToString().DeserializeObject(objDrawing.LabelImage);
            }
            else if (e.Node.Level == 2)//选择了Item中的分段
            {
                pbLabelImage.Visible = false;
                string drawingPlanId = tvModule.SelectedNode.Parent.Tag.ToString();
                drawingPlanId = drawingPlanId.Substring(4);//除去item
                Drawing objDrawing = _objDrawingService.GetDrawingById(drawingPlanId,_sbu);
                string moduleTreeId = tvModule.SelectedNode.Tag.ToString();
                ModuleTree objModuleTree = _objModuleTreeService.GetModuleTreeById(moduleTreeId,_sbu);
                //调用委托
                _quickBrowseDeg(objDrawing, objModuleTree);
            }
        }

        /// <summary>
        /// 弹出自动绘图界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAutoDrawing_Click(object sender, EventArgs e)
        {
           
            Project objProject = _objProjectService.GetProjectByODPNo(tvModule.Nodes[0].Text.Trim(),_sbu);
            
            //工厂模式，选择使用单例模式方法创建自动绘图窗口
            switch (objProject.HoodType)
            {
                case "Hood":
                    SingletonObject.GetSingleton.FrmHad?.ShowWithOdpNo(objProject.ODPNo);
                    break;
                case "Ceiling":
                    SingletonObject.GetSingleton.FrmCad?.ShowWithOdpNo(objProject.ODPNo);
                    break;
                case "Marine":
                    SingletonObject.GetSingleton.FrmMad?.ShowWithOdpNo(objProject.ODPNo);
                    break;
            }
        }
        /// <summary>
        /// 显示项目详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiShowProjectInfo_Click(object sender, EventArgs e)
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
            SingletonObject.GetSingleton.FrmPi?.ShowWithOdpNo(node.Text);
        }
        /// <summary>
        /// 打开天花总装
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiCeilingAssy_Click(object sender, EventArgs e)
        {
            FrmModelView frmModelView=new FrmModelView(_objProject);
            frmModelView.Show();
        }

        private void TvModule_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0) return;
            e.Node.ImageIndex = 2;//打开的文件夹图标
        }

        private void TvModule_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0) return;
            e.Node.ImageIndex = 1;//关闭的文件夹图标
        }
    }
}
