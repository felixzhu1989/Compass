using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Models
{
    /// <summary>
    /// 作图计划
    /// </summary>
    [Serializable]
    public class DrawingPlan
    {
        public int DrawingPlanId { get; set; }
        public int ProjectId { get; set; }
        public string Item { get; set; }
        public string Model { get; set; }
        public int ModuleNo { get; set; }
        public DateTime DrReleaseTarget { get; set; }
        public decimal SubTotalWorkload { get; set; }
        public DateTime AddedDate { get; set; }
        public string LabelImage { get; set; }
        //简单扩展
        public string ODPNo { get; set; }
        public string ProjectName { get; set; }
        public DateTime DrReleaseActual { get; set; }
        public string UserAccount { get; set; }
        public int RemainingDays { get; set; }
        public int ProgressValue { get; set; }
        //public Image ProgressImg
        //{
        //    get
        //    {
        //        Bitmap bmp = new Bitmap(104, 30); //这里给104是为了左边和右边空出2个像素，剩余的100就是百分比的值
        //        Graphics g = Graphics.FromImage(bmp);
        //        g.Clear(Color.White); //背景填白色
        //        if (ProgressValue < 100)
        //        {
        //            g.FillRectangle(Brushes.LimeGreen, 2, 2, this.ProgressValue, 26);  //普通效果
        //        }
        //        else
        //        {
        //            g.FillRectangle(Brushes.Gainsboro, 2, 2, this.ProgressValue, 26);
        //        }
                
        //        //填充渐变效果
        //        //g.FillRectangle(new LinearGradientBrush(new Point(30, 2), new Point(30, 30), Color.Black, Color.Gray), 2, 2, this.Press, 26);
        //        return bmp;
        //    }
        //}
    }
}
