using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;

namespace Compass
{
    public partial class FrmJobCardCeiling : MetroFramework.Forms.MetroForm
    {
        private readonly Project _item = null;
        private readonly string _sbu = Program.ObjCurrentUser.SBU;

        public FrmJobCardCeiling()
        {
            InitializeComponent();
        }
        public FrmJobCardCeiling(Project objProject):this()
        {
            lblProject.Text = objProject.ODPNo +" - "+ objProject.ProjectName;
            _item = objProject;
        }

        private async void btnJobCard_Click(object sender, EventArgs e)
        {
            btnJobCard.Enabled = false;
            if (txtModel.Text.Trim().Length==0||txtItemNo.Text.Trim().Length ==0)
            {
                MessageBox.Show("请填写编号和型号的数据", "提示信息");
                btnJobCard.Enabled = true;
                return;
            }
            try
            {
                await PrintJobCardAsync(_item, txtItemNo.Text.Trim() ,txtModel.Text.Trim());
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }
        /// <summary>
        /// 以异步的方式打印JobCard
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Task PrintJobCardAsync(Project item,string itemNo,string model)
        {
            return Task.Run(() =>
            {
                try
                {
                    new PrintReports().ExecPrintCeilingJobCard(item, itemNo, model,_sbu);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
    }
}
