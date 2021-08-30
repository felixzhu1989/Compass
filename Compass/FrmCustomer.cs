using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DAL;
using Models;
using Common;

namespace Compass
{
    public partial class FrmCustomer : MetroFramework.Forms.MetroForm
    {
        private List<Customer> customerList=null;//用来保存导入的Customer对象
        private ImportDataFormExcel objImportDataFormExcel=new ImportDataFormExcel();
        private CustomerService objCustomerService = new CustomerService();
        public FrmCustomer()
        {
            InitializeComponent();
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = objCustomerService.GetAllCustomers();
            btnCustomer.Text = "添加客户名称";
            dgvImportFromExcel.AutoGenerateColumns = false;
        }
        /// <summary>
        /// 提交添加或修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            #region 数据验证
            if (txtCustomerName.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入客户名称", "验证信息");
                txtCustomerName.Focus();
                return;
            }
            #endregion
            if (txtCustomerId.Text.Trim().Length == 0)
            {
                
                //封装对象
                Customer objCustomer = new Customer()
                {
                    CustomerName = txtCustomerName.Text.Trim()
                };
                //提交添加
                try
                {
                    int CustomerId = objCustomerService.AddCustomer(objCustomer);
                    if (CustomerId > 1)
                    {
                        //提示添加成功
                        MessageBox.Show("客户名称添加成功", "提示信息");
                        //刷新显示
                        dgvCustomers.DataSource = objCustomerService.GetAllCustomers();
                        //清空内容
                        txtCustomerName.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //封装对象
                Customer objCustomer = new Customer()
                {
                    CustomerId =Convert.ToInt32( txtCustomerId.Text.Trim()),
                    CustomerName = txtCustomerName.Text.Trim()
                };
                //调用后台方法修改对象
                try
                {
                    if (objCustomerService.EditProjectValult(objCustomer) == 1)
                    {
                        MessageBox.Show("修改客户名称成功！", "提示信息");
                        //刷新内容
                        dgvCustomers.DataSource = objCustomerService.GetAllCustomers();
                        //清空内容
                        txtCustomerId.Text = "";
                        txtCustomerName.Text = "";
                        btnCustomer.Text = "添加客户名称";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEditCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.RowCount == 0)
            {
                return;
            }
            if (dgvCustomers.CurrentRow == null)
            {
                MessageBox.Show("请选中需要修改的客户名称", "提示信息");
                return;
            }
            string customerId = dgvCustomers.CurrentRow.Cells["CustomerId"].Value.ToString();
            Customer objCustomer = objCustomerService.GetCustomerById(customerId);
            //初始化修改信息
            txtCustomerId.Text = objCustomer.CustomerId.ToString();
            txtCustomerName.Text = objCustomer.CustomerName;
            btnCustomer.Text = "修改客户名称";
        }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.RowCount == 0)
            {
                return;
            }
            if (dgvCustomers.CurrentRow == null)
            {
                MessageBox.Show("请选中需要删除的见客户名称", "验证信息");
                return;
            }
            string CustomerId = dgvCustomers.CurrentRow.Cells["CustomerId"].Value.ToString();
            string CustomerName = dgvCustomers.CurrentRow.Cells["CustomerName"].Value.ToString();
            //删除询问
            DialogResult result = MessageBox.Show("确定要删除（ " + CustomerName + " ）这个客户名称吗？", "删除询问", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            try
            {
                if (objCustomerService.DeleteCustomer(CustomerId) == 1)
                {
                    dgvCustomers.DataSource = objCustomerService.GetAllCustomers();//同步刷新显示数据
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) btnCustomer_Click(null,null);
        }

        private void dgvCustomers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)tsmiDeleteCustomer_Click(null,null);
        }

        private void dgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsmiEditCustomer_Click(null,null);
        }
        /// <summary>
        /// 添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCustomers_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvCustomers, e);
        }
        /// <summary>
        /// 从外部Excel文件导入客户名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btmImportFromExcel_Click(object sender, EventArgs e)
        {
            //1.编写一个能够从Excel读取数据的通用数据访问类OleDbHelper
            //2.编写ImportDataFromExcel类，添加查询Excel数据表的方法，以对象集合的方式存储数据，能够封装数据
            //3.在dgv中展示已经读取的数据，可不展示
            //4.在SQLHelper类中编写同时“插入多条SQL语句的事务”方法，购物车一次性提交
            //5.ImportDataFromExcel类中，编写保存多个集合对象的方法
            //6.在UI中将导入的数据保存到数据库中

            //打开文件
            OpenFileDialog openFile = new OpenFileDialog();
            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = openFile.FileName;//获取Excel文件路径
                customerList = objImportDataFormExcel.GetCustomersByExcel(path);
                //显示数据
                dgvImportFromExcel.DataSource = customerList;
            }
        }
        /// <summary>
        /// 保存到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveToDB_Click(object sender, EventArgs e)
        {
            //验证数据
            if (customerList == null || customerList.Count == 0)
            {
                MessageBox.Show("目前没有要导入的数据", "提示信息");
                return;
            }
            //1.验证数据，保证List集合中有数据
            //旧方法
            //2.遍历集合，每查询一个对象，就提交一次到数据库
            //基于事务的方法--后续有事务与索引
            //2.每遍历一次就生成一条SQL语句，基于事务保存对象，一次性提交
            //UI中不能生成SQL语句，UI中只提交数据
            //应该把循环遍历的方式写在数据访问类中，写到ImportDataFromExcel中
            try
            {
                if (objImportDataFormExcel.ImportCustomer(customerList))
                {
                    MessageBox.Show("数据导入成功","提示信息");
                    dgvImportFromExcel.DataSource = null;
                    dgvCustomers.DataSource = objCustomerService.GetAllCustomers();
                    customerList.Clear();
                }
                else
                {
                    MessageBox.Show("数据导入失败", "提示信息");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据导入失败"+ex.Message,"错误提示");
            }
        }
    }
}
