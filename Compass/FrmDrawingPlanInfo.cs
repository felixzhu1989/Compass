using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;
using DAL;

namespace Compass
{
    public partial class FrmDrawingPlanInfo : Form
    {
        private DrawingPlanService objDrawingPlanService=new DrawingPlanService();
        public FrmDrawingPlanInfo()
        {
            InitializeComponent();

        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
