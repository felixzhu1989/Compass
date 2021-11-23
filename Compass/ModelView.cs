using System;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Common;
using DAL;
using eDrawings.Interop.EModelViewControl;
using eDrawings.Interop.EModelMarkupControl;
using Models;

namespace Compass
{
    public partial class ModelView : UserControl
    {
        private EModelViewControl _mEDrawingsCtrl;
        private EModelMarkupControl _mEDrawingsMarkupCtrl;
        private readonly CategoryService _objCategoryService = new CategoryService();
        private readonly ModelViewData _modelViewData = new ModelViewData();
        private readonly string _sbu = Program.ObjCurrentUser.SBU;
        public ModelView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 传递数据
        /// </summary>
        public void GetData(Drawing drawing, ModuleTree tree)
        {
            Category objCategory = _objCategoryService.GetCategoryByCategoryId(tree.CategoryId.ToString(), _sbu);
            Image modelImage = objCategory.ModelImage.Length == 0
                ? Image.FromFile("NoPic.png")
                : (Image)new SerializeObjectToString().DeserializeObject(objCategory.ModelImage);

            Image labelImage = drawing.LabelImage.Length == 0
                ? Image.FromFile("NoPic.png")
                : (Image)new SerializeObjectToString().DeserializeObject(drawing.LabelImage);

            string itemPath = drawing.ODPNo + @"\" + drawing.Item + "-" + tree.Module + "-" + tree.CategoryName;
            if (drawing.HoodType == "Ceiling")//天花烟罩的后缀少一个item
            {
                itemPath = drawing.ODPNo + @"\" + tree.Module + "-" + tree.CategoryName;
            }
            string suffix = drawing.Item + "-" + tree.Module + "-" +
                            drawing.ODPNo.Substring(drawing.ODPNo.Length - 6);
            if (drawing.HoodType == "Ceiling")//天花烟罩的后缀少一个item
            {
                suffix = tree.Module + "-" +
                         drawing.ODPNo.Substring(drawing.ODPNo.Length - 6);
            }
            string packedAssyPath = itemPath + @"\" + tree.CategoryName.ToLower() + "_" + suffix + ".sldasm";


            string localPath = @"D:\MyProjects\" + packedAssyPath;
            string publicPath = @"Z:\1-Project operation\20" + drawing.ODPNo.Substring(3, 2) + @" project\" + packedAssyPath;

            _modelViewData.ModelImage = modelImage;
            _modelViewData.LabelImage = labelImage;
            _modelViewData.LocalPath = localPath;
            _modelViewData.PublicPath = publicPath;
            _modelViewData.EDrawingPath = Properties.Settings.Default.eDrawings;
        }
        public void ShowImage()
        {
            pbModelImage.Image = _modelViewData.ModelImage;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ctrlEDrw.LoadEDrawings();
        }
        private void OnControlLoaded(EModelViewControl ctrl)
        {
            _mEDrawingsCtrl = ctrl;

            _mEDrawingsCtrl.OnFinishedLoadingDocument += OnFinishedLoadingDocument;
            _mEDrawingsCtrl.OnFailedLoadingDocument += OnFailedLoadingDocument;
            _mEDrawingsMarkupCtrl = _mEDrawingsCtrl.CoCreateInstance("EModelViewMarkup.EModelMarkupControl") as EModelMarkupControl;
        }
        private void OnFailedLoadingDocument(string fileName, int errorCode, string errorString)
        {
            Trace.WriteLine($"{fileName} failed to loaded: {errorString}");
        }
        private void OnFinishedLoadingDocument(string fileName)
        {
            Trace.WriteLine($"{fileName} loaded");
            _mEDrawingsMarkupCtrl.ViewOperator = EMVMarkupOperators.eMVOperatorMeasure;
        }
        /// <summary>
        /// 记录测量结果按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCaptureMeasurement(object sender, EventArgs e)
        {
            txtMeasurements.Text += (!string.IsNullOrEmpty(txtMeasurements.Text) ? Environment.NewLine : "")
                                    + _mEDrawingsMarkupCtrl.MeasureResultString;
        }
        /// <summary>
        /// 在嵌入eDrawings中打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnOpen(object sender, EventArgs e)
        {
            string filePath = "";
            if (((Button)sender).Tag.ToString() == "0")
            {
                filePath = _modelViewData.LocalPath;
                btnOpenFolder.Tag = "0";
                btnOpeneDrawing.Tag = "0";
                btnOpenSolidWorks.Tag = "0";
            }
            else
            {
                filePath = _modelViewData.PublicPath;
                btnOpenFolder.Tag = "1";
                btnOpeneDrawing.Tag = "1";
                btnOpenSolidWorks.Tag = "1";

            }
            if (Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                btnOpenFolder.Enabled = true;
            }
            //if (!string.IsNullOrEmpty(filePath))
            if (File.Exists(filePath))
            {
                btnOpeneDrawing.Enabled = true;
                btnOpenSolidWorks.Enabled = true;
                //开启独立线程，避免当前程序被占用
                Thread objThread = new Thread(() =>
                  {
                      if (_mEDrawingsCtrl == null)
                      {
                          throw new NullReferenceException("eDrawings control is not loaded");
                      }
                      _mEDrawingsCtrl.CloseActiveDoc("");
                      _mEDrawingsCtrl.OpenDoc(filePath, false, false, false, "");
                  })
                {
                    IsBackground = true
                };
                objThread.Start();
            }
            else
            {
                MessageBox.Show("未找到该文档,或者路径不标准!\r\n\r\n请尝试打开文件夹...");
            }
        }
        private void BtnOpenFolder_Click(object sender, EventArgs e)
        {
            btnOpenFolder.Enabled = false;
            string filePath;
            if (((Button)sender).Tag.ToString() == "0")
            {
                filePath = _modelViewData.LocalPath;
            }
            else
            {
                filePath = _modelViewData.PublicPath;
            }
            if (File.Exists(filePath))
            {
                Process.Start("Explorer.exe", "/select," + Path.GetDirectoryName(filePath) + "\\" + Path.GetFileName(filePath));
            }
            else
            {
                if (Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Process.Start("Explorer.exe", Path.GetDirectoryName(filePath));
                }
                else
                {
                    MessageBox.Show("未找到该路径,或者路径不标准!");
                }
            }
        }
        /// <summary>
        /// 打开eDrawings程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpeneDrawing_Click(object sender, EventArgs e)
        {
            btnOpeneDrawing.Enabled = false;
            string filePath;
            if (((Button)sender).Tag.ToString() == "0")
            {
                filePath = _modelViewData.LocalPath;
            }
            else
            {
                filePath = _modelViewData.PublicPath;
            }
            string linkPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms), @"SOLIDWORKS 2021\eDrawings 2021 x64 Edition.lnk");
            //string linkPath =@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\SOLIDWORKS 2021\eDrawings 2021 x64 Edition.lnk"; 
            //string exePath = @"C:\Program Files\SOLIDWORKS2021 Corp\eDrawings\eDrawings.exe";
            string exePath = _modelViewData.EDrawingPath;
            if (File.Exists(filePath))
            {
                if (File.Exists(linkPath))
                {
                    Process.Start(linkPath, filePath);
                }
                else if (File.Exists(exePath))
                {
                    Process.Start(exePath, filePath);
                }
            }
            else
            {
                MessageBox.Show("未找到该文档,或者路径不标准!\r\n\r\n请尝试打开文件夹...");
            }
        }
        private void BtnOpenSolidWorks_Click(object sender, EventArgs e)
        {
            btnOpenSolidWorks.Enabled = false;
            string filePath;
            if (((Button)sender).Tag.ToString() == "0")
            {
                filePath = _modelViewData.LocalPath;
            }
            else
            {
                filePath = _modelViewData.PublicPath;
            }
            if (File.Exists(filePath))
            {
                //int errors = 0;
                //int warnings = 0;
                //SldWorks swApp = SolidWorksSingleton.GetApplication();
                //ModelDoc2 swModel = swApp.OpenDoc6(filePath, (int)swDocumentTypes_e.swDocASSEMBLY,
                //    (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings) as ModelDoc2;
                //swModel.ForceRebuild3(true);
                Process.Start(filePath);
            }
            else
            {
                MessageBox.Show("未找到该文档,或者路径不标准!\r\n\r\n请尝试打开文件夹...");
            }
        }
        private void BtnModelImage_Click(object sender, EventArgs e)
        {
            pbModelImage.Image = _modelViewData.ModelImage;
        }
        private void BtnLabelImage_Click(object sender, EventArgs e)
        {
            pbModelImage.Image = _modelViewData.LabelImage;
        }
    }
}
