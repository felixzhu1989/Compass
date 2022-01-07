using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Compass
{
    public partial class FrmSolidWorksTools : MetroFramework.Forms.MetroForm
    {
        #region 单例模式
        private FrmSolidWorksTools()
        {
            InitializeComponent();
            txteDrawingsPath.Text = Properties.Settings.Default.eDrawings;
        }
        private static FrmSolidWorksTools instance;
        public static FrmSolidWorksTools GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new FrmSolidWorksTools();
            }
            return instance;
        }
        #endregion
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            OpenFileDialog objFileDialog = new OpenFileDialog
            {
                InitialDirectory = "C:\\",
                Filter = "exe files (*.exe)|*.exe|All files (*.*)|*.*",
                RestoreDirectory = true
            };
            DialogResult result = objFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txteDrawingsPath.Text= objFileDialog.FileName;
            }
        }

        private void BtneDrawingsPath_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.eDrawings = txteDrawingsPath.Text;
            MessageBox.Show("eDrawing路径更新成功！");
            Close();
        }

       
    }
}
