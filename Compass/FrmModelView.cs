using System;
using System.Diagnostics;
using System.Windows.Forms;
using DAL;
using eDrawings.Interop.EModelViewControl;
using eDrawings.Interop.EModelMarkupControl;
using Models;

namespace Compass
{
    public partial class FrmModelView : MetroFramework.Forms.MetroForm
    {
        private EModelViewControl m_EDrawingsCtrl;
        private EModelMarkupControl m_EDrawingsMarkupCtrl;
        private ModelViewData modelViewData = new ModelViewData();
        public FrmModelView()
        {
            InitializeComponent();
        }
        public FrmModelView(Project project) : this()
        {
            RequirementService objRequirementService = new RequirementService();
            GeneralRequirement objGeneralRequirement = objRequirementService.GetGeneralRequirementByODPNo(project.ODPNo, Program.ObjCurrentUser.SBU);
            this.Text = "天花总装：" + project.ODPNo + " - " + project.ProjectName + "("+ objGeneralRequirement.TypeName+ ")";
            modelViewData.LocalPath= objGeneralRequirement.MainAssyPath;
            modelViewData.PublicPath= @"Z:\1-Project operation\20" + project.ODPNo.Substring(3, 2) + @" project\" + project.ODPNo+@"\"+System.IO.Path.GetFileName(modelViewData.LocalPath);
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ctrlEDrw.LoadEDrawings();
        }
        private void OnControlLoaded(EModelViewControl ctrl)
        {
            m_EDrawingsCtrl = ctrl;
            m_EDrawingsCtrl.OnFinishedLoadingDocument += OnFinishedLoadingDocument;
            m_EDrawingsCtrl.OnFailedLoadingDocument += OnFailedLoadingDocument;
            m_EDrawingsMarkupCtrl = m_EDrawingsCtrl.CoCreateInstance("EModelViewMarkup.EModelMarkupControl") as EModelMarkupControl;
        }
        private void OnFailedLoadingDocument(string fileName, int errorCode, string errorString)
        {
            Trace.WriteLine($"{fileName} failed to loaded: {errorString}");
        }
        private void OnFinishedLoadingDocument(string fileName)
        {
            Trace.WriteLine($"{fileName} loaded");
            m_EDrawingsMarkupCtrl.ViewOperator = EMVMarkupOperators.eMVOperatorMeasure;
        }
        private void OnOpen(object sender, EventArgs e)
        {
            string filePath = "";
            if (((Button)sender).Tag.ToString() == "0")
            {
                filePath = modelViewData.LocalPath;
                btnOpenFolder.Tag = "0";
            }
            else
            {
                filePath = modelViewData.PublicPath;
                btnOpenFolder.Tag = "1";
            }
            if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filePath)))
            {
                btnOpenFolder.Enabled = true;
            }
            if (System.IO.File.Exists(filePath))
            {
                if (m_EDrawingsCtrl == null)
                {
                    throw new NullReferenceException("eDrawings control is not loaded");
                }
                //txtMeasurements.Clear();
                txtMeasurements.Text = "选择线可直接显示结果，如果选择的是面则点击记录测量结果按钮。";
                m_EDrawingsCtrl.CloseActiveDoc("");
                m_EDrawingsCtrl.OpenDoc(filePath, false, false, false, "");
            }
            else
            {
                MessageBox.Show("未找到该文档,或者路径不标准!\r\n\r\n请尝试打开文件夹...");
            }
        }
        private void OnCaptureMeasurement(object sender, EventArgs e)
        {
            txtMeasurements.Text += (!string.IsNullOrEmpty(txtMeasurements.Text) ? Environment.NewLine : "")
                                    + m_EDrawingsMarkupCtrl.MeasureResultString;
        }

        private void BtnOpenFolder_Click(object sender, EventArgs e)
        {
            btnOpenFolder.Enabled = false;
            string filePath = "";
            if (((Button)sender).Tag.ToString() == "0") filePath = modelViewData.LocalPath;
            else filePath = modelViewData.PublicPath;

            if (System.IO.File.Exists(filePath))
            {
                System.Diagnostics.Process.Start("Explorer.exe", "/select," + System.IO.Path.GetDirectoryName(filePath) + "\\" + System.IO.Path.GetFileName(filePath));
            }
            else
            {
                if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filePath)))
                {
                    System.Diagnostics.Process.Start("Explorer.exe", System.IO.Path.GetDirectoryName(filePath));
                }
                else
                {
                    MessageBox.Show("未找到该路径,或者路径不标准!");
                }
            }
        }
    }
}
