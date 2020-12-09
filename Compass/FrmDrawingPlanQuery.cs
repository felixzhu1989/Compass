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
    
    public partial class FrmDrawingPlanQuery : MetroFramework.Forms.MetroForm
    {
        private DrawingPlanService objDrawingPlanService=new DrawingPlanService();
        
        public FrmDrawingPlanQuery()
        {
            InitializeComponent();
        }
        
    }
}
