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

namespace Compass
{
    public partial class FrmJobCardCeiling : MetroFramework.Forms.MetroForm
    {
        private Project item = null;
        public FrmJobCardCeiling()
        {
            InitializeComponent();
        }
        public FrmJobCardCeiling(Project objProject):this()
        {
            lblProject.Text = objProject.ODPNo +" - "+ objProject.ProjectName;
            item = objProject;
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
                await PrintJobCardAsync(item, txtItemNo.Text.Trim() ,txtModel.Text.Trim());
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
                    new PrintReports().ExecPrintCeilingJobCard(item, itemNo, model);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
    }
}
