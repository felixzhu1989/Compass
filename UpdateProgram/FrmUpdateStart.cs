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
            this.Close();
        }

        private void btnStartUpdate_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
