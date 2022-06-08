﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmCategoryTree : MetroFramework.Forms.MetroForm
    {
        private readonly string _sbu = Program.ObjCurrentUser.SBU;
        readonly ModuleTreeService _objModuleTreeService = new ModuleTreeService();
        private Drawing _objDrawing = null;
        public FrmCategoryTree()
        {
            InitializeComponent();
            //加载产品目录树
            LoadTvCategory();
        }
        #region 单例模式，重写关闭方法
        protected override void OnClosing(CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
        public void AddModule(Drawing drawing)
        {
            _objDrawing = drawing;
            lblODPNo.Text = _objDrawing.ODPNo;
            lblItem.Text = _objDrawing.Item;
            tvCategory.Enabled = true;
            txtModule.Text ="";
            btnAddModule.Text = "添加：-";
            btnAddModule.Tag = null;
            Show();
            WindowState = FormWindowState.Normal;
            Focus();
        }
        public void EditModule(Drawing drawing, ModuleTree tree)
        {
            _objDrawing = drawing;
            lblODPNo.Text = _objDrawing.ODPNo;
            lblItem.Text = _objDrawing.Item;
            tvCategory.Enabled = false;
            txtModule.Text = tree.Module;
            btnAddModule.Text = "修改分段名称";
            btnAddModule.Tag = tree.ModuleTreeId.ToString();
            Show();
            WindowState = FormWindowState.Normal;
            Focus();
        }
        #endregion
        private List<Category> _categoryList = null;
        private readonly CategoryService _objCategoryService = new CategoryService();
        private void LoadTvCategory()
        {
            _categoryList = _objCategoryService.GetAllCategories(_sbu);//加载产品目录树的所有节点信息
            //创建第一个节点
            tvCategory.Nodes.Clear();//清空所有节点
            TreeNode rootNode = new TreeNode
            {
                Text = "Halton产品目录",
                Tag = "1000",//默认值,作为id使用
                ImageIndex = 4//添加图标
            };
            tvCategory.Nodes.Add(rootNode);//将根节点添加到treeView控件中
            //基于递归方式添加所有子节点
            CreateChildNode(rootNode, 1000);
            //将递归出来的产品目录树节点展开
            tvCategory.Nodes[0].Expand();//只展开第一级目录
            //this.tvCategory.ExpandAll();//全部展开，不太好
        }

        private void CreateChildNode(TreeNode parentNode, int parentId)
        {
            //找到所有以该节点作为父节点的子节点，Linq的方式
            var subCategoryList = from list in _categoryList
                                  where list.ParentId.Equals(parentId)
                                  select list;
            //循环创建该节点的所有子节点
            foreach (var item in subCategoryList)
            {
                //创建子节点并设置属性
                TreeNode node = new TreeNode
                {
                    Text = item.CategoryName + "-" + item.CategoryDesc,//设置名称，加上描述
                    Tag = item.CategoryId//设置Id
                };
                //设置节点图标，父类和子类有区别
                if (item.ParentId == 1000)
                {
                    node.ImageIndex = 1;//关闭的文件夹图标
                }
                else
                {
                    node.ImageIndex = 3;
                }
                parentNode.Nodes.Add(node);//在该父节点下添加子节点
                //递归该子节点下的所有孙节点，，，
                CreateChildNode(node, item.CategoryId);//调用自己实现递归添加节点
            }
        }
        //点击节点后执行的事件，后期处理
        private void TvCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //先判断是二级节点才执行任务
            if (e.Node.Level == 2)
            {
                //需要执行的任务
                btnAddModule.Text = "添加：" + e.Node.Text;
                btnAddModule.Tag = e.Node.Tag;
            }
        }
        private void TvCategory_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0) return;
            e.Node.ImageIndex = 2;//打开的文件夹图标
        }
        private void TvCategory_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0) return;
            e.Node.ImageIndex = 1;//关闭的文件夹图标
        }
        /// <summary>
        /// 往订单中添加模型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddModule_Click(object sender, EventArgs e)
        {
            if (tvCategory.Enabled == false)
            {
                if (_objDrawing == null) return;
                if (txtModule.Text.Length == 0) return;
                if (btnAddModule.Tag == null) return;
                ModuleTree objModuleTree = new ModuleTree()
                {
                    ModuleTreeId = Convert.ToInt32(btnAddModule.Tag),
                    Module = txtModule.Text.ToUpper()
                };
                //提交修改
                try
                {
                    if (_objModuleTreeService.EditModuleTree(objModuleTree,_sbu) == 1)
                    {
                        SingletonObject.GetSingleton.FrmMt?.RefreshTree();
                        MessageBox.Show("修改分段名称成功！", "提示信息");
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                if (_objDrawing == null) return;
                //验证数据
                if (txtModule.Text.Length == 0) return;
                if (btnAddModule.Tag == null) return;
                //封装对象
                ModuleTree objModuleTree = new ModuleTree()
                {
                    DrawingPlanId = _objDrawing.DrawingPlanId,
                    CategoryId = Convert.ToInt32(btnAddModule.Tag),
                    Module = txtModule.Text.ToUpper()
                };
                //提交添加
                try
                {
                    bool result = _objModuleTreeService.AddModuleAndData(objModuleTree,_sbu);
                    if (result)
                    {
                        SingletonObject.GetSingleton.FrmMt?.RefreshTree();
                        MessageBox.Show("烟罩分段添加成功"); //this.Close();不关闭窗口
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }


        /// <summary>
        /// 输入完分段编号回车提交添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtModule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) BtnAddModule_Click(null, null);
        }
    }
}