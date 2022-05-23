using System;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Compass
{
    /// <summary>
    /// 单例类
    /// </summary>
    class SingletonObject
    {
        #region 设计单例模式
        private SingletonObject() { }
        private static readonly Lazy<SingletonObject> Singleton = new Lazy<SingletonObject>(() => new SingletonObject());
        public static SingletonObject GetSingleton => Singleton.Value;

        #endregion

        //1.管理单例对象
        //非嵌入
        public FrmCategoryTree FrmCt { get; set; }

        //嵌入
        public FrmProject FrmP { get; set; }
        public FrmModuleTree FrmMt { get; set; }
        public FrmQuickBrowse FrmQb { get; set; }
        public FrmDrawingPlan FrmDp { get; set; }
        public FrmProjectTracking FrmPt { get; set; }


        //2.对单例对象赋值
        public void AddMetroForm(MetroForm frmObj)
        {
            if (frmObj is FrmCategoryTree) FrmCt = frmObj as FrmCategoryTree;
        }

        public void AddForm(Form frmObj)
        {
            //嵌入
            if (frmObj is FrmProject) FrmP = frmObj as FrmProject;
            if (frmObj is FrmModuleTree) FrmMt = frmObj as FrmModuleTree;
            if (frmObj is FrmQuickBrowse) FrmQb = frmObj as FrmQuickBrowse;
            if (frmObj is FrmDrawingPlan) FrmDp = frmObj as FrmDrawingPlan;
            if (frmObj is FrmProjectTracking) FrmPt = frmObj as FrmProjectTracking;
        }

    }
}