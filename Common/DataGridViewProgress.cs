using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Common
{
    //DataGridView显示一个进度条列
    public class DataGridViewProgressColumn : DataGridViewImageColumn
    {
        public DataGridViewProgressColumn()
        {
            CellTemplate = new DataGridViewProgressCell();
        }
    }

    public class DataGridViewProgressCell : DataGridViewImageCell
    {
        private static readonly Image emptyImage;

        static DataGridViewProgressCell()
        {
            emptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
        public DataGridViewProgressCell()
        {
            ValueType = typeof(int);
        }
        public string ShowText { get; set; } //如果要显示独立的文字而不是百分比，设置此属性。
        protected override object GetFormattedValue(object value,
            int rowIndex, ref DataGridViewCellStyle cellStyle,
            TypeConverter valueTypeConverter,
            TypeConverter formattedValueTypeConverter,
            DataGridViewDataErrorContexts context)
        {
            return emptyImage;
        }
        protected override void Paint(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            string tValue = value.ToString();
            if (tValue == "") tValue = "0";

            int progressVal;
            try { progressVal = Convert.ToInt16(tValue); }
            catch
            {
                progressVal = 0;
            }
            float percentage = ((float)progressVal / 100.0f);
            Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
            Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);
            base.Paint(g, clipBounds, cellBounds,
             rowIndex, cellState, value, formattedValue, errorText,
             cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

            string DrawStringStr = progressVal.ToString() + "%";
            if (ShowText != "")
            {
                DrawStringStr = ShowText;
            }
            if (percentage > 0.0)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(163, 189, 242)), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width - 4)), cellBounds.Height - 4);
                g.DrawString(DrawStringStr, cellStyle.Font, foreColorBrush, cellBounds.X + 30, cellBounds.Y + 5);
            }
            else
            {
                if (DataGridView.CurrentRow.Index == rowIndex)
                    g.DrawString(DrawStringStr, cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), cellBounds.X + 30, cellBounds.Y + 5);
                else
                    g.DrawString(DrawStringStr, cellStyle.Font, foreColorBrush, cellBounds.X + 30, cellBounds.Y + 5);
            }
        }



    }
}
