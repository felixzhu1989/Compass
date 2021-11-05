﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using DAL;
using Models;

namespace Compass
{
    public partial class FrmHME : MetroFramework.Forms.MetroForm
    {
        HMEService objHMEService = new HMEService();
        private HME objHME;
        public FrmHME()
        {
            InitializeComponent();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmHME(Drawing drawing, ModuleTree tree) : this()
        {
            objHME = (HME)objHMEService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objHME == null) return;
            this.Text = drawing.ODPNo + " / Item: " + drawing.Item + " / Module: " + tree.Module + " - " + tree.CategoryName;
            modelView.GetData(drawing, tree);
            modelView.ShowImage();
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
            if (objHME == null) return;
            modelView.Tag = objHME.HMEId;
            txtLength.Text = objHME.Length== 0 ? "600" : objHME.Length.ToString();
            txtWidth.Text = objHME.Width== 0 ? "500" : objHME.Width.ToString();
            txtHeight.Text = objHME.Height.ToString();
            txtInletDia.Text = objHME.InletDia.ToString();
            txtOutletDia.Text = objHME.OutletDia.ToString();
            txtOutletHeight.Text = objHME.OutletHeight.ToString();

            cobHeater.Text = objHME.Heater;
            cobWindPressure.Text = objHME.WindPressure;
            cobHangPosition.Text = objHME.HangPosition;
            cobNamePlate.Text = objHME.NamePlate;
            cobTemperatureSwitch.Text = objHME.TemperatureSwitch;
            cobPlugPosition.Text = objHME.PlugPosition;
            cobPowerPlug.Text = objHME.PowerPlug;
            cobNetPlug.Text = objHME.NetPlug;
            txtPowerPlugDis.Text = objHME.PowerPlugDis.ToString();
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
            

            #endregion
            //封装对象
            HME objHME = new HME()
            {
                HMEId = Convert.ToInt32(modelView.Tag),

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
                if (objHMEService.EditModel(objHME) == 1)
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