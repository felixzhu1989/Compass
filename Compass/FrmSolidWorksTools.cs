using System;
using System.Windows.Forms;

namespace Compass
{
    public partial class FrmSolidWorksTools : MetroFramework.Forms.MetroForm
    {
        public FrmSolidWorksTools()
        {
            InitializeComponent();
            txteDrawingsPath.Text = Properties.Settings.Default.eDrawings;
        }

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
            this.Close();
        }
    }
}
