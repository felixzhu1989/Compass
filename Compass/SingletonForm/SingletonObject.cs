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
        private static readonly Lazy<SingletonObject> _singleton=new Lazy<SingletonObject>(()=>new SingletonObject());
        public static SingletonObject GetSingleton => _singleton.Value;

        #endregion

        //1.管理所有的单例对象
        //非嵌入
        public FrmHoodAutoDrawing FrmHAD { get; set; }
        public FrmCeilingAutoDrawing FrmCAD { get; set; }
        public FrmMarineAutoDrawing FrmMAD { get; set; }
        public FrmProjectInfo FrmPI { get; set; }
        public FrmSyncFiles FrmSF { get; set; }
        public FrmDrawingNumMatrix FrmDNM { get; set; }
        public FrmProjectMeasure FrmPM { get; set; }
        public FrmDrawingPlanQuery FrmDPQ { get; set; }
        public FrmSolidWorksTools FrmSWT { get; set; }
        public FrmCategoryTree FrmCT { get; set; }

        //嵌入
        public FrmProject FrmP { get; set; }
        public FrmModuleTree FrmMT { get; set; }
        public FrmQuickBrowse FrmQB { get; set; }
        public FrmDrawingPlan FrmDP { get; set; }
        public FrmProjectTracking FrmPT { get; set; }
        public FrmUserManage FrmUM { get; set; }
        public FrmCategories FrmC { get; set; }
        public FrmDXFCutList FrmDC { get; set; }
        public FrmDesignWorkload FrmDW { get; set; }
        public FrmStatusTypes FrmST { get; set; }
        public FrmCeilingAccessories FrmCA { get; set; }


        //2.对单例对象赋值
        public void AddMetroForm(MetroForm frmObj)
        {
            if (frmObj is FrmHoodAutoDrawing) this.FrmHAD = frmObj as FrmHoodAutoDrawing;
            if (frmObj is FrmCeilingAutoDrawing) this.FrmCAD =frmObj as FrmCeilingAutoDrawing;
            if (frmObj is FrmMarineAutoDrawing) this.FrmMAD =frmObj as FrmMarineAutoDrawing;
            if (frmObj is FrmProjectInfo) this.FrmPI = frmObj as FrmProjectInfo;
            if (frmObj is FrmSyncFiles) this.FrmSF = frmObj as FrmSyncFiles;
            if (frmObj is FrmDrawingNumMatrix) this.FrmDNM = frmObj as FrmDrawingNumMatrix;
            if (frmObj is FrmProjectMeasure) this.FrmPM = frmObj as FrmProjectMeasure;
            if (frmObj is FrmDrawingPlanQuery) this.FrmDPQ = frmObj as FrmDrawingPlanQuery;
            if (frmObj is FrmSolidWorksTools) this.FrmSWT = frmObj as FrmSolidWorksTools;
            if (frmObj is FrmCategoryTree) this.FrmCT = frmObj as FrmCategoryTree;
        }

        public void AddForm(Form frmObj)
        {
            //嵌入
            if (frmObj is FrmProject) this.FrmP = frmObj as FrmProject;
            if (frmObj is FrmModuleTree) this.FrmMT = frmObj as FrmModuleTree;
            if (frmObj is FrmQuickBrowse) this.FrmQB = frmObj as FrmQuickBrowse;
            if (frmObj is FrmDrawingPlan) this.FrmDP = frmObj as FrmDrawingPlan;
            if (frmObj is FrmProjectTracking) this.FrmPT = frmObj as FrmProjectTracking;
            if (frmObj is FrmUserManage) this.FrmUM = frmObj as FrmUserManage;
            if (frmObj is FrmCategories) this.FrmC = frmObj as FrmCategories;
            if (frmObj is FrmDXFCutList) this.FrmDC = frmObj as FrmDXFCutList;
            if (frmObj is FrmDesignWorkload) this.FrmDW = frmObj as FrmDesignWorkload;
            if (frmObj is FrmStatusTypes) this.FrmST = frmObj as FrmStatusTypes;
            if (frmObj is FrmCeilingAccessories) this.FrmCA = frmObj as FrmCeilingAccessories;
        }

    }
}
