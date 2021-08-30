using System;
using System.Windows.Forms;

namespace UpdateProgram
{
    public partial class FrmTips : Form
    {
        //Point point = new Point();
        //private int height = 0;
        //private int bottom = 0;
        public FrmTips()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 用于弹出的定时触发器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    height += 10;
        //    if (this.Top <= Screen.GetWorkingArea(point).Bottom - 170)
        //    {
        //        this.timer1.Enabled = false;
        //        this.timer2.Enabled = true;
        //    }
        //    else
        //    {
        //        this.Top = Screen.GetWorkingArea(point).Bottom - height;
        //    }
        //}
        /// <summary>
        /// 窗体加载时定位窗体的初始位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void FrmTips_Load(object sender, EventArgs e)
        //{
        //    this.Left = Screen.GetWorkingArea(point).Right - this.Width;
        //    this.Top = Screen.GetWorkingArea(point).Bottom;
        //}

        //private int delay = 0;//用于窗体的停留时间
        /// <summary>
        /// 窗体退出时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void timer2_Tick(object sender, EventArgs e)
        //{
        //    delay += 1;
        //    if (delay > 500)
        //    {
        //        bottom += 1;
        //        if (this.Top >= Screen.GetWorkingArea(point).Bottom)
        //        {
        //            this.timer2.Enabled = false;
        //            this.Close();
        //        }
        //        else
        //        {
        //            this.Top = this.Top + bottom;
        //        }
        //    }
        //}

        /// <summary>
        /// 马上升级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartUpdate_Click(object sender, EventArgs e)
        {
            this.Close();
            if(MessageBox.Show("为了更新文件，将退出当前程序，请确保数据已经保存，确认退出吗？", "取消询问", MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Question) == DialogResult.Cancel)return;
            Application.Exit();
            //启动升级程序
            System.Diagnostics.Process.Start("UpdateProgram.exe");
        }
        /// <summary>
        /// 下次再说
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
