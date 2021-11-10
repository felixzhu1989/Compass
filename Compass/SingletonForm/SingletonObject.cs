using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static readonly Lazy<SingletonObject> Singleton=new Lazy<SingletonObject>(()=>new SingletonObject());
        public static SingletonObject GetSingleton => Singleton.Value;

        #endregion

        //1.管理所有的单例对象
        //非嵌入
        public FrmHoodAutoDrawing FrmHad { get; set; }
        public FrmCeilingAutoDrawing FrmCad { get; set; }
        public FrmMarineAutoDrawing FrmMad { get; set; }
        public FrmProjectInfo FrmPi { get; set; }
        public FrmSyncFiles FrmSf { get; set; }
        public FrmDrawingNumMatrix FrmDnm { get; set; }
        public FrmProjectMeasure FrmPm { get; set; }
        public FrmDrawingPlanQuery FrmDpq { get; set; }
        public FrmSolidWorksTools FrmSwt { get; set; }
        public FrmCategoryTree FrmCt { get; set; }

        //嵌入
        public FrmProject FrmP { get; set; }
        public FrmModuleTree FrmMt { get; set; }
        public FrmQuickBrowse FrmQb { get; set; }
        public FrmDrawingPlan FrmDp { get; set; }
        public FrmProjectTracking FrmPt { get; set; }
        public FrmUserManage FrmUm { get; set; }
        public FrmCategories FrmC { get; set; }
        public FrmDxfCutList FrmDc { get; set; }
        public FrmDesignWorkload FrmDw { get; set; }
        public FrmStatusTypes FrmSt { get; set; }
        public FrmCeilingAccessories FrmCa { get; set; }


        //2.对单例对象赋值
        public void AddMetroForm(MetroForm frmObj)
        {
            if (frmObj is FrmHoodAutoDrawing) this.FrmHad = frmObj as FrmHoodAutoDrawing;
            if (frmObj is FrmCeilingAutoDrawing) this.FrmCad =frmObj as FrmCeilingAutoDrawing;
            if (frmObj is FrmMarineAutoDrawing) this.FrmMad =frmObj as FrmMarineAutoDrawing;
            if (frmObj is FrmProjectInfo) this.FrmPi = frmObj as FrmProjectInfo;
            if (frmObj is FrmSyncFiles) this.FrmSf = frmObj as FrmSyncFiles;
            if (frmObj is FrmDrawingNumMatrix) this.FrmDnm = frmObj as FrmDrawingNumMatrix;
            if (frmObj is FrmProjectMeasure) this.FrmPm = frmObj as FrmProjectMeasure;
            if (frmObj is FrmDrawingPlanQuery) this.FrmDpq = frmObj as FrmDrawingPlanQuery;
            if (frmObj is FrmSolidWorksTools) this.FrmSwt = frmObj as FrmSolidWorksTools;
            if (frmObj is FrmCategoryTree) this.FrmCt = frmObj as FrmCategoryTree;
        }

        public void AddForm(Form frmObj)
        {
            //嵌入
            if (frmObj is FrmProject) this.FrmP = frmObj as FrmProject;
            if (frmObj is FrmModuleTree) this.FrmMt = frmObj as FrmModuleTree;
            if (frmObj is FrmQuickBrowse) this.FrmQb = frmObj as FrmQuickBrowse;
            if (frmObj is FrmDrawingPlan) this.FrmDp = frmObj as FrmDrawingPlan;
            if (frmObj is FrmProjectTracking) this.FrmPt = frmObj as FrmProjectTracking;
            if (frmObj is FrmUserManage) this.FrmUm = frmObj as FrmUserManage;
            if (frmObj is FrmCategories) this.FrmC = frmObj as FrmCategories;
            if (frmObj is FrmDxfCutList) this.FrmDc = frmObj as FrmDxfCutList;
            if (frmObj is FrmDesignWorkload) this.FrmDw = frmObj as FrmDesignWorkload;
            if (frmObj is FrmStatusTypes) this.FrmSt = frmObj as FrmStatusTypes;
            if (frmObj is FrmCeilingAccessories) this.FrmCa = frmObj as FrmCeilingAccessories;
        }

    }
}
