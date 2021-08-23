using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            OpenFileDialog objFileDialog = new OpenFileDialog();
            objFileDialog.InitialDirectory = "C:\\";
            objFileDialog.Filter = "exe files (*.exe)|*.exe|All files (*.*)|*.*";
            objFileDialog.RestoreDirectory = true;
            DialogResult result = objFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txteDrawingsPath.Text= objFileDialog.FileName;
            }
        }

        private void btneDrawingsPath_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.eDrawings = txteDrawingsPath.Text;
            MessageBox.Show("eDrawing路径更新成功！");
            this.Close();
        }
    }
}
