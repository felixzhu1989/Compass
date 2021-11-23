using System;
using System.Windows.Forms;

namespace UpdateProgram
{
    public partial class FrmUpdateStart : Form
    {
        public FrmUpdateStart()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnStartUpdate_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
