using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using eDrawings.Interop.EModelViewControl;

namespace MyUIControls
{
    public partial class ModelView : UserControl
    {
        private EModelViewControl m_EDrawingsCtrl;

        public ModelView()
        {
            InitializeComponent();
        }

        public void ShowImage(Image image)
        {
            this.pbModelImage.Image = image;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ctrlEDrw.LoadEDrawings();
        }

        private void OnControlLoaded(EModelViewControl ctrl)
        {
            m_EDrawingsCtrl = ctrl;

            m_EDrawingsCtrl.OnFinishedLoadingDocument += OnFinishedLoadingDocument;
            m_EDrawingsCtrl.OnFailedLoadingDocument += OnFailedLoadingDocument;
        }
        private void OnFailedLoadingDocument(string fileName, int errorCode, string errorString)
        {
            Trace.WriteLine($"{fileName} failed to loaded: {errorString}");
        }
        private void OnFinishedLoadingDocument(string fileName)
        {
            Trace.WriteLine($"{fileName} loaded");
        }
        public void OnOpen(object sender, EventArgs e)
        {
            string filePath = @"D:\MyProjects\FSO200000\Hood-M1-KVV555\KVV555_Hood-M1-200000.SLDASM";
            if (!string.IsNullOrEmpty(filePath))
            {
                if (m_EDrawingsCtrl == null)
                {
                    throw new NullReferenceException("eDrawings control is not loaded");
                }
                m_EDrawingsCtrl.CloseActiveDoc("");
                m_EDrawingsCtrl.OpenDoc(filePath, false, false, false, "");
            }
        }
    }
}
