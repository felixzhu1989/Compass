using System;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmKvs : MetroFramework.Forms.MetroForm
    {
        readonly KVSService _objKvsService = new KVSService();
        private readonly KVS _objKvs = null;
        public FrmKvs()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmKvs(Drawing drawing, ModuleTree tree) : this()
        {
            _objKvs = (KVS)_objKvsService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objKvs == null) return;
            Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
            FillData();
        }
        /// <summary>
        /// 初始化所有的下拉框
        /// </summary>
        private void IniCob()
        {

            //脖颈数量
            cobExNo.Items.Add("1");
            cobExNo.Items.Add("2");
            //灯具类型
            cobLightType.Items.Add("FSLONG");
            cobLightType.Items.Add("FSSHORT");
        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objKvs == null) return;
            modelView.Tag = _objKvs.KVSId;

            //默认ExNo为1
            cobExNo.Text = _objKvs.ExNo == 0 ? "1" : _objKvs.ExNo.ToString();
            cobLightType.Text = _objKvs.LightType;
            txtLength.Text = _objKvs.Length.ToString();
            txtDeepth.Text = _objKvs.Deepth.ToString();
            txtExDis.Text = _objKvs.ExDis.ToString();
            txtExLength.Text = _objKvs.ExLength.ToString();
            txtExWidth.Text = _objKvs.ExWidth.ToString();
            txtExHeight.Text = _objKvs.ExHeight.ToString();
        }
        /// <summary>
        /// 修改参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (modelView.Tag.ToString().Length == 0) return;
            if (!DataValidate.IsDouble(txtLength.Text.Trim()) || Convert.ToDouble(txtLength.Text.Trim()) < 500d)
            {
                MessageBox.Show("请认真检查烟罩长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtDeepth.Text.Trim()) || Convert.ToDouble(txtDeepth.Text.Trim()) < 500d)
            {
                MessageBox.Show("请认真检查烟罩深度", "提示信息");
                txtDeepth.Focus();
                txtDeepth.SelectAll();
                return;
            }

            if (cobExNo.SelectedIndex == -1)
            {
                MessageBox.Show("请选择排风脖颈数量", "提示信息");
                cobExNo.Focus();
                return;
            }
            else if (cobExNo.SelectedIndex > 0 && (!DataValidate.IsDouble(txtExDis.Text.Trim()) || Convert.ToDouble(txtExDis.Text.Trim()) < 40d))
            {
                MessageBox.Show("请认真检查排风脖颈间距", "提示信息");//当脖颈大于2时需要填写脖颈间距
                txtExDis.Focus();
                txtExDis.SelectAll();
                return;
            }

            if (!DataValidate.IsDouble(txtExLength.Text.Trim()) || Convert.ToDouble(txtExLength.Text.Trim()) < 50d)
            {
                MessageBox.Show("请填写脖颈长度", "提示信息");
                txtExLength.Focus();
                txtExLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtExWidth.Text.Trim()) || Convert.ToDouble(txtExWidth.Text.Trim()) < 50d)
            {
                MessageBox.Show("请填写脖颈宽度", "提示信息");
                txtExWidth.Focus();
                txtExWidth.SelectAll();
                return;
            }
            if (!DataValidate.IsDouble(txtExHeight.Text.Trim()) || Convert.ToDouble(txtExHeight.Text.Trim()) < 20d)
            {
                MessageBox.Show("请填写脖颈高度", "提示信息");
                txtExHeight.Focus();
                txtExHeight.SelectAll();
                return;
            }
            if (cobLightType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择灯具类型", "提示信息");
                cobLightType.Focus();
                return;
            }


            #endregion
            //封装对象
            KVS objKvs = new KVS()
            {
                KVSId = Convert.ToInt32(modelView.Tag),
                ExNo = Convert.ToInt32(cobExNo.Text),
                LightType = cobLightType.Text,

                Length = Convert.ToDouble(txtLength.Text.Trim()),
                Deepth = Convert.ToDouble(txtDeepth.Text.Trim()),
                ExDis = Convert.ToDouble(txtExDis.Text.Trim()),
                ExLength = Convert.ToDouble(txtExLength.Text.Trim()),
                ExWidth = Convert.ToDouble(txtExWidth.Text.Trim()),
                ExHeight = Convert.ToDouble(txtExHeight.Text.Trim())

            };
            //提交修改
            try
            {
                if (_objKvsService.EditModel(objKvs) == 1)
                {
                    MessageBox.Show("制图数据修改成功", "提示信息");
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 动态选择排风脖颈数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobExNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobExNo.SelectedIndex > 0)
            {
                lblExDis.Visible = true;
                txtExDis.Visible = true;
            }
            else
            {
                lblExDis.Visible = false;
                txtExDis.Visible = false;
            }
        }
    }
}

