using System;
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
    public partial class FrmAN :MetroFramework.Forms.MetroForm
    {
        ANService objANService = new ANService();
        private AN objAN = null;
        public FrmAN()
        {
            InitializeComponent();
            SetVisibleFalse();
            IniCob();
            //管理员和技术部才能更新数据
            if (Program.ObjCurrentUser.UserGroupId == 1 || Program.ObjCurrentUser.UserGroupId == 2) btnEditData.Visible = true;
            else btnEditData.Visible = false;
        }
        public FrmAN(Drawing drawing, ModuleTree tree) : this()
        {
            objAN = (AN)objANService.GetModelByModuleTreeId(tree.ModuleTreeId.ToString());
            if (objAN == null) return;
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
            
            //ANSUL
            cobANSUL.Items.Add("YES");
            cobANSUL.Items.Add("NO");
            cobANSUL.SelectedIndex = 1;
            
            //ANSUL末端探测器
            cobANDetectorEnd.Items.Add("LEFT");
            cobANDetectorEnd.Items.Add("RIGHT");
            cobANDetectorEnd.Items.Add("NO");
            //ANSUL探测器数量
            cobANDetectorNo.Items.Add("0");
            cobANDetectorNo.Items.Add("1");
            cobANDetectorNo.Items.Add("2");
            cobANDetectorNo.Items.Add("3");
            cobANDetectorNo.Items.Add("4");
            cobANDetectorNo.Items.Add("5");
            //ANSUL下喷
            cobANDropNo.Items.Add("0");
            cobANDropNo.Items.Add("1");
            cobANDropNo.Items.Add("2");
            cobANDropNo.Items.Add("3");
            cobANDropNo.Items.Add("4");
            cobANDropNo.Items.Add("5");
            //MARVEL
            cobMARVEL.Items.Add("YES");
            cobMARVEL.Items.Add("NO");
            cobMARVEL.SelectedIndex = 1;
            //IR数量
            cobIRNo.Items.Add("0");
            cobIRNo.Items.Add("1");
            cobIRNo.Items.Add("2");
            cobIRNo.Items.Add("3");
            
        }
        /// <summary>
        /// 将分组隐藏
        /// </summary>
        private void SetVisibleFalse()
        {
            grbANSUL.Visible = false;
            grbMARVEL.Visible = false;
        }
        /// <summary>
        /// 填数据
        /// </summary>
        private void FillData()
        {
            if (objAN == null) return;
            modelView.Tag = objAN.ANId;
            
            cobANSUL.Text = objAN.ANSUL;
            
            cobANDetectorEnd.Text = objAN.ANDetectorEnd;
            cobANDropNo.Text = objAN.ANDropNo.ToString();
            cobANDetectorNo.Text = objAN.ANDetectorNo.ToString();
            cobMARVEL.Text = objAN.MARVEL;
            cobIRNo.Text = objAN.IRNo.ToString();
            
            txtLength.Text = objAN.Length.ToString();
            txtWidth.Text= objAN.Width.ToString();
            txtANYDis.Text = objAN.ANYDis.ToString();
            txtANDropDis1.Text = objAN.ANDropDis1.ToString();
            txtANDropDis2.Text = objAN.ANDropDis2.ToString();
            txtANDropDis3.Text = objAN.ANDropDis3.ToString();
            txtANDropDis4.Text = objAN.ANDropDis4.ToString();
            txtANDropDis5.Text = objAN.ANDropDis5.ToString();
            txtANDetectorDis1.Text = objAN.ANDetectorDis1.ToString();
            txtANDetectorDis2.Text = objAN.ANDetectorDis2.ToString();
            txtANDetectorDis3.Text = objAN.ANDetectorDis3.ToString();
            txtANDetectorDis4.Text = objAN.ANDetectorDis4.ToString();
            txtANDetectorDis5.Text = objAN.ANDetectorDis5.ToString();
            txtIRDis1.Text = objAN.IRDis1.ToString();
            txtIRDis2.Text = objAN.IRDis2.ToString();
            txtIRDis3.Text = objAN.IRDis3.ToString();
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
            if (!DataValidate.IsDecimal(txtLength.Text.Trim()) || Convert.ToDecimal(txtLength.Text.Trim()) < 100m)
            {
                MessageBox.Show("请认真检查ANSUL腔长度", "提示信息");
                txtLength.Focus();
                txtLength.SelectAll();
                return;
            }
            if (!DataValidate.IsDecimal(txtWidth.Text.Trim()) || Convert.ToDecimal(txtWidth.Text.Trim()) < 30m)
            {
                MessageBox.Show("请认真检查ANSUL腔宽度", "提示信息");
                txtWidth.Focus();
                txtWidth.SelectAll();
                return;
            }
            
            if (cobANSUL.SelectedIndex == -1)
            {
                MessageBox.Show("是否带ANSUL", "提示信息");
                cobANSUL.Focus();
                return;
            }
            if (cobMARVEL.SelectedIndex == -1)
            {
                MessageBox.Show("是否带MARVEL", "提示信息");
                cobMARVEL.Focus();
                return;
            }
            
            if (cobANSUL.SelectedIndex == 0)
            {
                if (cobANDetectorEnd.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择ANSUL末端探测器位置", "提示信息");
                    cobANDetectorEnd.Focus();
                    return;
                }
                if (cobANDropNo.SelectedIndex == -1)
                {
                    MessageBox.Show("请检查下喷数量", "提示信息");
                    cobANDropNo.Focus();
                    return;
                }
                if (cobANDropNo.SelectedIndex > 0)
                {
                    if (!DataValidate.IsDecimal(txtANYDis.Text.Trim()) || Convert.ToDecimal(txtANYDis.Text.Trim()) < 10m)
                    {
                        MessageBox.Show("请检查ANSUL下喷距离天花排风腔前端距离", "提示信息");
                        txtANYDis.Focus();
                        txtANYDis.SelectAll();
                        return;
                    }
                    if (!DataValidate.IsDecimal(txtANDropDis1.Text.Trim()) || Convert.ToDecimal(txtANDropDis1.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查下喷间距1", "提示信息");
                        txtANDropDis1.Focus();
                        txtANDropDis1.SelectAll();
                        return;
                    }
                }
                if (cobANDropNo.SelectedIndex > 1)
                {
                    if (!DataValidate.IsDecimal(txtANDropDis2.Text.Trim()) || Convert.ToDecimal(txtANDropDis2.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查下喷间距2", "提示信息");
                        txtANDropDis2.Focus();
                        txtANDropDis2.SelectAll();
                        return;
                    }
                }
                if (cobANDropNo.SelectedIndex > 2)
                {
                    if (!DataValidate.IsDecimal(txtANDropDis3.Text.Trim()) || Convert.ToDecimal(txtANDropDis3.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查下喷间距3", "提示信息");
                        txtANDropDis3.Focus();
                        txtANDropDis3.SelectAll();
                        return;
                    }
                }
                if (cobANDropNo.SelectedIndex > 3)
                {
                    if (!DataValidate.IsDecimal(txtANDropDis4.Text.Trim()) || Convert.ToDecimal(txtANDropDis4.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查下喷间距4", "提示信息");
                        txtANDropDis4.Focus();
                        txtANDropDis4.SelectAll();
                        return;
                    }
                }
                if (cobANDropNo.SelectedIndex > 4)
                {
                    if (!DataValidate.IsDecimal(txtANDropDis5.Text.Trim()) || Convert.ToDecimal(txtANDropDis5.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查下喷间距5", "提示信息");
                        txtANDropDis5.Focus();
                        txtANDropDis5.SelectAll();
                        return;
                    }
                }
            }
            if (cobANDetectorNo.SelectedIndex == -1)
            {
                MessageBox.Show("请检查探测器数量", "提示信息");
                cobANDetectorNo.Focus();
                return;
            }
            if (cobANDetectorNo.SelectedIndex > 0)
            {
                if (!DataValidate.IsDecimal(txtANDetectorDis1.Text.Trim()) || Convert.ToDecimal(txtANDetectorDis1.Text.Trim()) < 30m)
                {
                    MessageBox.Show("请检查探测器间距1", "提示信息");
                    txtANDetectorDis1.Focus();
                    txtANDetectorDis1.SelectAll();
                    return;
                }
            }
            if (cobANDetectorNo.SelectedIndex > 1)
            {
                if (!DataValidate.IsDecimal(txtANDetectorDis2.Text.Trim()) || Convert.ToDecimal(txtANDetectorDis2.Text.Trim()) < 30m)
                {
                    MessageBox.Show("请检查探测器间距2", "提示信息");
                    txtANDetectorDis2.Focus();
                    txtANDetectorDis2.SelectAll();
                    return;
                }
            }
            if (cobANDetectorNo.SelectedIndex > 2)
            {
                if (!DataValidate.IsDecimal(txtANDetectorDis3.Text.Trim()) || Convert.ToDecimal(txtANDetectorDis3.Text.Trim()) < 30m)
                {
                    MessageBox.Show("请检查探测器间距3", "提示信息");
                    txtANDetectorDis3.Focus();
                    txtANDetectorDis3.SelectAll();
                    return;
                }
            }
            if (cobANDetectorNo.SelectedIndex > 3)
            {
                if (!DataValidate.IsDecimal(txtANDetectorDis4.Text.Trim()) || Convert.ToDecimal(txtANDetectorDis4.Text.Trim()) < 30m)
                {
                    MessageBox.Show("请检查探测器间距4", "提示信息");
                    txtANDetectorDis4.Focus();
                    txtANDetectorDis4.SelectAll();
                    return;
                }
            }
            if (cobANDetectorNo.SelectedIndex > 4)
            {
                if (!DataValidate.IsDecimal(txtANDetectorDis5.Text.Trim()) || Convert.ToDecimal(txtANDetectorDis5.Text.Trim()) < 30m)
                {
                    MessageBox.Show("请检查探测器间距5", "提示信息");
                    txtANDetectorDis5.Focus();
                    txtANDetectorDis5.SelectAll();
                    return;
                }
            }
            if (cobMARVEL.SelectedIndex == 0)
            {
                if (cobIRNo.SelectedIndex == -1)
                {
                    MessageBox.Show("请检查IR数量", "提示信息");
                    cobIRNo.Focus();
                    return;
                }
                if (cobIRNo.SelectedIndex > 0)
                {
                    if (!DataValidate.IsDecimal(txtIRDis1.Text.Trim()) || Convert.ToDecimal(txtIRDis1.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查IR间距1", "提示信息");
                        txtIRDis1.Focus();
                        txtIRDis1.SelectAll();
                        return;
                    }
                }
                if (cobIRNo.SelectedIndex > 1)
                {
                    if (!DataValidate.IsDecimal(txtIRDis2.Text.Trim()) || Convert.ToDecimal(txtIRDis2.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查IR间距2", "提示信息");
                        txtIRDis2.Focus();
                        txtIRDis2.SelectAll();
                        return;
                    }
                }
                if (cobIRNo.SelectedIndex > 2)
                {
                    if (!DataValidate.IsDecimal(txtIRDis3.Text.Trim()) || Convert.ToDecimal(txtIRDis3.Text.Trim()) < 30m)
                    {
                        MessageBox.Show("请检查IR间距3", "提示信息");
                        txtIRDis3.Focus();
                        txtIRDis3.SelectAll();
                        return;
                    }
                }
            }

            #endregion
            //封装对象
            AN objAN = new AN()
            {
                ANId = Convert.ToInt32(modelView.Tag),
                
                ANSUL = cobANSUL.Text,
                ANDetectorEnd = cobANDetectorEnd.Text.Trim().Length == 0 ? "NO" : cobANDetectorEnd.Text,
                ANDropNo = cobANDropNo.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(cobANDropNo.Text),
                ANDetectorNo = cobANDetectorNo.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(cobANDetectorNo.Text),
                MARVEL = cobMARVEL.Text,
                IRNo = cobIRNo.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(cobIRNo.Text),
                

                Length = Convert.ToDecimal(txtLength.Text.Trim()),
                Width = Convert.ToDecimal(txtWidth.Text.Trim()),
                ANYDis = Convert.ToDecimal(txtANYDis.Text.Trim()),
                ANDropDis1 = Convert.ToDecimal(txtANDropDis1.Text.Trim()),
                ANDropDis2 = Convert.ToDecimal(txtANDropDis2.Text.Trim()),
                ANDropDis3 = Convert.ToDecimal(txtANDropDis3.Text.Trim()),
                ANDropDis4 = Convert.ToDecimal(txtANDropDis4.Text.Trim()),
                ANDropDis5 = Convert.ToDecimal(txtANDropDis5.Text.Trim()),
                ANDetectorDis1 = Convert.ToDecimal(txtANDetectorDis1.Text.Trim()),
                ANDetectorDis2 = Convert.ToDecimal(txtANDetectorDis2.Text.Trim()),
                ANDetectorDis3 = Convert.ToDecimal(txtANDetectorDis3.Text.Trim()),
                ANDetectorDis4 = Convert.ToDecimal(txtANDetectorDis4.Text.Trim()),
                ANDetectorDis5 = Convert.ToDecimal(txtANDetectorDis5.Text.Trim()),
                IRDis1 = Convert.ToDecimal(txtIRDis1.Text.Trim()),
                IRDis2 = Convert.ToDecimal(txtIRDis2.Text.Trim()),
                IRDis3 = Convert.ToDecimal(txtIRDis3.Text.Trim())
            };
            //提交修改
            try
            {
                if (objANService.EditModel(objAN) == 1)
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
        /// <summary>
        /// ANSUL分组显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobANSUL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobANSUL.SelectedIndex == 0) grbANSUL.Visible = true;
            else grbANSUL.Visible = false;
        }
        /// <summary>
        /// MARVEL分组显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobMARVEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobMARVEL.SelectedIndex == 0) grbMARVEL.Visible = true;
            else grbMARVEL.Visible = false;
        }
        
        /// <summary>
        /// 动态选择下喷距离
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobANDropNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobANDropNo.SelectedIndex == 0)
            {
                lblANYDis.Visible = false;
                lblANDropDis1.Visible = false;
                lblANDropDis2.Visible = false;
                lblANDropDis3.Visible = false;
                lblANDropDis4.Visible = false;
                lblANDropDis5.Visible = false;
                txtANYDis.Visible = false;
                txtANDropDis1.Visible = false;
                txtANDropDis2.Visible = false;
                txtANDropDis3.Visible = false;
                txtANDropDis4.Visible = false;
                txtANDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 1)
            {
                lblANYDis.Visible = true;
                lblANDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtANDropDis1.Visible = true;
                lblANDropDis2.Visible = false;
                lblANDropDis3.Visible = false;
                lblANDropDis4.Visible = false;
                lblANDropDis5.Visible = false;
                txtANDropDis2.Visible = false;
                txtANDropDis3.Visible = false;
                txtANDropDis4.Visible = false;
                txtANDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 2)
            {
                lblANYDis.Visible = true;
                lblANDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtANDropDis1.Visible = true;
                lblANDropDis2.Visible = true;
                txtANDropDis2.Visible = true;
                lblANDropDis3.Visible = false;
                lblANDropDis4.Visible = false;
                lblANDropDis5.Visible = false;
                txtANDropDis3.Visible = false;
                txtANDropDis4.Visible = false;
                txtANDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 3)
            {
                lblANYDis.Visible = true;
                lblANDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtANDropDis1.Visible = true;
                lblANDropDis2.Visible = true;
                txtANDropDis2.Visible = true;
                lblANDropDis3.Visible = true;
                txtANDropDis3.Visible = true;
                lblANDropDis4.Visible = false;
                lblANDropDis5.Visible = false;
                txtANDropDis4.Visible = false;
                txtANDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 4)
            {
                lblANYDis.Visible = true;
                lblANDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtANDropDis1.Visible = true;
                lblANDropDis2.Visible = true;
                txtANDropDis2.Visible = true;
                lblANDropDis3.Visible = true;
                txtANDropDis3.Visible = true;
                lblANDropDis4.Visible = true;
                txtANDropDis4.Visible = true;
                lblANDropDis5.Visible = false;
                txtANDropDis5.Visible = false;
            }
            else if (cobANDropNo.SelectedIndex == 5)
            {
                lblANYDis.Visible = true;
                lblANDropDis1.Visible = true;
                txtANYDis.Visible = true;
                txtANDropDis1.Visible = true;
                lblANDropDis2.Visible = true;
                txtANDropDis2.Visible = true;
                lblANDropDis3.Visible = true;
                txtANDropDis3.Visible = true;
                lblANDropDis4.Visible = true;
                txtANDropDis4.Visible = true;
                lblANDropDis5.Visible = true;
                txtANDropDis5.Visible = true;
            }
        }
        
        /// <summary>
        /// 动态选择IR数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobIRNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobIRNo.SelectedIndex == 0)
            {
                lblIRDis1.Visible = false;
                lblIRDis2.Visible = false;
                lblIRDis3.Visible = false;
                txtIRDis1.Visible = false;
                txtIRDis2.Visible = false;
                txtIRDis3.Visible = false;
            }
            else if (cobIRNo.SelectedIndex == 1)
            {
                lblIRDis1.Visible = true;
                txtIRDis1.Visible = true;
                lblIRDis2.Visible = false;
                lblIRDis3.Visible = false;
                txtIRDis2.Visible = false;
                txtIRDis3.Visible = false;
            }
            else if (cobIRNo.SelectedIndex == 2)
            {
                lblIRDis1.Visible = true;
                txtIRDis1.Visible = true;
                lblIRDis2.Visible = true;
                txtIRDis2.Visible = true;
                lblIRDis3.Visible = false;
                txtIRDis3.Visible = false;
            }
            else if (cobIRNo.SelectedIndex == 3)
            {
                lblIRDis1.Visible = true;
                txtIRDis1.Visible = true;
                lblIRDis2.Visible = true;
                txtIRDis2.Visible = true;
                lblIRDis3.Visible = true;
                txtIRDis3.Visible = true;
            }
        }
        /// <summary>
        /// 探测器数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cobDetectorNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobANDetectorNo.SelectedIndex == 0)
            {
                lblANDetectorDis1.Visible = false;
                lblANDetectorDis2.Visible = false;
                lblANDetectorDis3.Visible = false;
                lblANDetectorDis4.Visible = false;
                lblANDetectorDis5.Visible = false;
                txtANDetectorDis1.Visible = false;
                txtANDetectorDis2.Visible = false;
                txtANDetectorDis3.Visible = false;
                txtANDetectorDis4.Visible = false;
                txtANDetectorDis5.Visible = false;
            }
            else if (cobANDetectorNo.SelectedIndex == 1)
            {
                lblANDetectorDis1.Visible = true;
                txtANDetectorDis1.Visible = true;
                lblANDetectorDis2.Visible = false;
                lblANDetectorDis3.Visible = false;
                lblANDetectorDis4.Visible = false;
                lblANDetectorDis5.Visible = false;
                txtANDetectorDis2.Visible = false;
                txtANDetectorDis3.Visible = false;
                txtANDetectorDis4.Visible = false;
                txtANDetectorDis5.Visible = false;
            }
            else if (cobANDetectorNo.SelectedIndex == 2)
            {
                lblANDetectorDis1.Visible = true;
                txtANDetectorDis1.Visible = true;
                lblANDetectorDis2.Visible = true;
                txtANDetectorDis2.Visible = true;
                lblANDetectorDis3.Visible = false;
                lblANDetectorDis4.Visible = false;
                lblANDetectorDis5.Visible = false;
                txtANDetectorDis3.Visible = false;
                txtANDetectorDis4.Visible = false;
                txtANDetectorDis5.Visible = false;
            }
            else if (cobANDetectorNo.SelectedIndex == 3)
            {
                lblANDetectorDis1.Visible = true;
                txtANDetectorDis1.Visible = true;
                lblANDetectorDis2.Visible = true;
                txtANDetectorDis2.Visible = true;
                lblANDetectorDis3.Visible = true;
                txtANDetectorDis3.Visible = true;
                lblANDetectorDis4.Visible = false;
                lblANDetectorDis5.Visible = false;
                txtANDetectorDis4.Visible = false;
                txtANDetectorDis5.Visible = false;
            }
            else if (cobANDetectorNo.SelectedIndex == 4)
            {
                lblANDetectorDis1.Visible = true;
                txtANDetectorDis1.Visible = true;
                lblANDetectorDis2.Visible = true;
                txtANDetectorDis2.Visible = true;
                lblANDetectorDis3.Visible = true;
                txtANDetectorDis3.Visible = true;
                lblANDetectorDis4.Visible = true;
                txtANDetectorDis4.Visible = true;
                lblANDetectorDis5.Visible = false;
                txtANDetectorDis5.Visible = false;
            }
            else if (cobANDetectorNo.SelectedIndex == 5)
            {

                lblANDetectorDis1.Visible = true;
                txtANDetectorDis1.Visible = true;
                lblANDetectorDis2.Visible = true;
                txtANDetectorDis2.Visible = true;
                lblANDetectorDis3.Visible = true;
                txtANDetectorDis3.Visible = true;
                lblANDetectorDis4.Visible = true;
                txtANDetectorDis4.Visible = true;
                lblANDetectorDis5.Visible = true;
                txtANDetectorDis5.Visible = true;
            }
        }
    }
}
