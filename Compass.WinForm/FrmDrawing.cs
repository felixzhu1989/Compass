using System;
using System.Drawing;
using System.Windows.Forms;
using Models;
using DAL;
using Common;

namespace Compass
{
    public partial class FrmDrawing : MetroFramework.Forms.MetroForm
    {
        private readonly DrawingService _objDrawingService = new DrawingService();
        private readonly string _sbu = Program.ObjCurrentUser.SBU;

        public FrmDrawing()
        {
            InitializeComponent();
        }
        public FrmDrawing(Drawing objDrawing) : this()
        {
            txtODPNo.Text = objDrawing.ODPNo;
            txtProjectPlanId.Text = objDrawing.DrawingPlanId.ToString();
            txtItem.Text = objDrawing.Item;
            pbLabelImage.Image = objDrawing.LabelImage.Length == 0
                ? Image.FromFile("NoPic.png")
                : (Image)new SerializeObjectToString().DeserializeObject(objDrawing.LabelImage);
        }
        private void btnDrawing_Click(object sender, EventArgs e)
        {
            #region 数据验证

            if (txtItem.Text.Trim().Length == 0)
            {
                MessageBox.Show("烟罩编号不能为空", "验证信息");
                txtItem.Focus();
                return;
            }
            if (pbLabelImage.Image == null)
            {
                MessageBox.Show("请选择图片", "验证信息");
                btnChooseImage.Focus();
                return;
            }
            #endregion

            #region 封装对象
            Drawing objDrawing = new Drawing()
            {
                DrawingPlanId = Convert.ToInt32(txtProjectPlanId.Text.Trim()),
                Item = txtItem.Text.Trim(),
                LabelImage = pbLabelImage.Image != null
                    ? new SerializeObjectToString().SerializeObject(pbLabelImage.Image)
                    : null,
            };
            #endregion
            #region 提交数据库修改
            try
            {
                if (_objDrawingService.EditDrawing(objDrawing,_sbu) == 1)
                {
                    MessageBox.Show("图纸修改成功", "提示信息");
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
        }
        /// <summary>
        /// 选择图片，20220722改成从剪切板粘贴图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            //OpenFileDialog objFileDialog = new OpenFileDialog();
            //DialogResult result = objFileDialog.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    pbLabelImage.Image = Image.FromFile(objFileDialog.FileName);
            //}
            if (Clipboard.ContainsImage()) pbLabelImage.Image = Clipboard.GetImage();
            btnDrawing.Focus();
        }
        /// <summary>
        /// 清除图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearImage_Click(object sender, EventArgs e)
        {
            pbLabelImage.Image = Image.FromFile("NoPic.png");
        }


    }
}
