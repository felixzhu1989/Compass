using System;
using System.Windows.Forms;

namespace UpdateProgram
{
    public partial class FrmTips : Form
    {
        
        public FrmTips()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 马上升级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartUpdate_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
            //启动升级程序
            System.Diagnostics.Process.Start("UpdateProgram.exe");
        }
        /// <summary>
        /// 下次再说
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
