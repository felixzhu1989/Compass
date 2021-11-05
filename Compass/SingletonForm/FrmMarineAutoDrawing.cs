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
    public partial class FrmMarineAutoDrawing : MetroFramework.Forms.MetroForm
    {
        public FrmMarineAutoDrawing()
        {
            InitializeComponent();
        }



        #region 单例模式，重写关闭方法，显示时选择ODP号
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        public void ShowWithOdpNo(string odpNo)
        {
            //if (odpNo.Length != 0) cobODPNo.Text = odpNo;
            ShowAndFocus();
        }
        internal void ShowAndFocus()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Focus();
        }
        #endregion
    }
}
