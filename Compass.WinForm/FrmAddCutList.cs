using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using Models;

namespace Compass.WinForm
{
    public partial class FrmAddCutList : MetroFramework.Forms.MetroForm
    {
        private readonly int _moduleTreeId;
        private readonly int _userId=Program.ObjCurrentUser.UserId;
        private readonly HoodCutListService _service=new HoodCutListService();
        public FrmAddCutList(int moduleTreeId)
        {
            _moduleTreeId = moduleTreeId;
            InitializeComponent();
            MaximizeBox = false;
            Resizable = false;
        }

        private void AddCutList_Click(object sender, EventArgs e)
        {
            ;

            HoodCutList cutList = new HoodCutList()
            {
                ModuleTreeId = _moduleTreeId,
                UserId = _userId,
                PartDescription = txtPartDescription.Text,
                Length =double.TryParse(txtLength.Text, out double length)?length:0,
                Width = double.TryParse(txtWidth.Text, out double width) ? width:0,
                Thickness=double.TryParse(txtThickness.Text, out double thickness) ?  thickness:0,
                Quantity = int.TryParse(txtQuantity.Text,out int quantity)?quantity:0,
                Materials=txtMaterials.Text,
                PartNo = txtPartNo.Text
            };
            _service.AddHoodCutList(cutList);
            this.DialogResult= DialogResult.OK;
            this.Close();
        }
    }
}
