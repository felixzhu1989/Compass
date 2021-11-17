using System;
using System.Windows.Forms;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmHMM : MetroFramework.Forms.MetroForm
    {
        readonly HMMService _objHMMService = new HMMService();
        private readonly HMM _objHMM;
        private readonly ModelView _modelView;
        public FrmHMM()
        {
            InitializeComponent();
            _modelView = new ModelView();
            panel1.Controls.Add(_modelView);
            _modelView.Dock = DockStyle.Fill;
            IniCob();

            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;

        }
        public FrmHMM(Drawing drawing, ModuleTree tree) : this()
        {
            _objHMM = (HMM)_objHMMService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (_objHMM == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            _modelView.GetData(drawing, tree);
            _modelView.ShowImage();
            FillData();
        }
        /// <summary>
        /// 初始化所有的下拉框
        /// </summary>
        private void IniCob()
        {
            cobHangPosition.Items.Add("Up");
            cobHangPosition.Items.Add("Mid");
            cobHangPosition.Items.Add("Down");
            cobHangPosition.SelectedIndex = -1;

            cobPlugPosition.Items.Add("Front");
            cobPlugPosition.Items.Add("Left");
            cobPlugPosition.Items.Add("Right");
            cobPlugPosition.SelectedIndex = -1;

            cobPowerPlug.Items.Add("NAC21");
            cobPowerPlug.Items.Add("NAC31");
            cobPowerPlug.Items.Add("Both");
            cobPowerPlug.Items.Add("Gland");
            cobPowerPlug.Items.Add("No");
            cobPowerPlug.SelectedIndex = -1;

            cobNetPlug.Items.Add("2xRJ12");
            cobNetPlug.Items.Add("2xRJ45");
            cobNetPlug.Items.Add("Both");
            cobNetPlug.Items.Add("No");
            cobNetPlug.SelectedIndex = -1;

            cobHeater.Items.Add("Yes");
            cobHeater.Items.Add("No");
            cobHeater.SelectedIndex = -1;

            cobTemperatureSwitch.Items.Add("Yes");
            cobTemperatureSwitch.Items.Add("No");
            cobTemperatureSwitch.SelectedIndex = -1;

            cobNamePlate.Items.Add("Yes");
            cobNamePlate.Items.Add("No");
            cobNamePlate.SelectedIndex = -1;

            cobWindPressure.Items.Add("Yes");
            cobWindPressure.Items.Add("No");
            cobWindPressure.SelectedIndex = -1;

        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (_objHMM == null) return;
            _modelView.Tag = _objHMM.HMMId;
            txtLength.Text = _objHMM.Length == 0 ? "600" : _objHMM.Length.ToString();
            txtWidth.Text = _objHMM.Width == 0 ? "500" : _objHMM.Width.ToString();
            txtHeight.Text = _objHMM.Height.ToString();
            txtInletDia.Text = _objHMM.InletDia.ToString();
            txtOutletDia.Text = _objHMM.OutletDia.ToString();
            txtOutletHeight.Text = _objHMM.OutletHeight.ToString();

            cobHeater.Text = _objHMM.Heater;
            cobWindPressure.Text = _objHMM.WindPressure;
            cobHangPosition.Text = _objHMM.HangPosition;
            cobNamePlate.Text = _objHMM.NamePlate;
            cobTemperatureSwitch.Text = _objHMM.TemperatureSwitch;
            cobPlugPosition.Text = _objHMM.PlugPosition;
            cobPowerPlug.Text = _objHMM.PowerPlug;
            cobNetPlug.Text = _objHMM.NetPlug;
            txtPowerPlugDis.Text = _objHMM.PowerPlugDis.ToString();
        }
        /// <summary>
        /// 修改参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditData_Click(object sender, EventArgs e)
        {
            #region 数据验证
            //必填项目
            if (_modelView.Tag.ToString().Length == 0) return;

            #endregion
            //封装对象
            HMM objHMM = new HMM()
            {
                HMMId = Convert.ToInt32(_modelView.Tag),

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                Width = Convert.ToDecimal(txtWidth.Text.Trim()),
                Height = Convert.ToDecimal(txtHeight.Text.Trim()),
                InletDia = Convert.ToDecimal(txtInletDia.Text.Trim()), //100,125,160
                OutletDia = Convert.ToDecimal(txtOutletDia.Text.Trim()),
                OutletHeight = Convert.ToDecimal(txtOutletHeight.Text.Trim()),
                HangPosition = cobHangPosition.Text, // 吊脚位置，Up\Mid\Down
                PlugPosition = cobPlugPosition.Text,// 插口位置，Front\Left\Right
                PowerPlug = cobPowerPlug.Text, // 电源插口， NAC21\NAC31\Both\Gland\No
                PowerPlugDis = Convert.ToDecimal(txtPowerPlugDis.Text.Trim()),
                NetPlug = cobNetPlug.Text,// 网线插口，2xRJ12\2xRJ45\Both\No

                Heater = cobHeater.Text, //Yes,No
                TemperatureSwitch = cobTemperatureSwitch.Text,//Yes,No
                NamePlate = cobNamePlate.Text,//Yes,No
                WindPressure = cobWindPressure.Text//Yes,No
            };
            //提交修改
            try
            {
                if (_objHMMService.EditModel(objHMM) == 1)
                {
                    MessageBox.Show("制图数据修改成功", "提示信息");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
