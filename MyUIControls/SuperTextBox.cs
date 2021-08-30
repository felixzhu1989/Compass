using System.ComponentModel;
//引入窗体的命名空间
using System.Windows.Forms;
//引入正则表达式的命名空间
using System.Text.RegularExpressions;

namespace MyUIControls
{
    public partial class SuperTextBox : TextBox//继承文本框
    {
        public SuperTextBox()
        {
            InitializeComponent();
        }

        public SuperTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        //非空验证
        public int BeginCheckEmpty()
        {
            if (this.Text.Trim() == "")
            {
                this.errorProvider.SetError(this, "必填项不能为空！");
                return 0;
            }
            else
            {
                this.errorProvider.SetError(this, string.Empty);//清楚错误提示
                return 1;
            }
        }
        //基于正则表达式让文本框实现复杂的验证
        /// <summary>
        /// 通用正则表达式验证方法
        /// </summary>
        /// <param name="regularExpress">正则表达式</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public int BeginCheckData(string regularExpress, string errorMsg)
        {
            if (BeginCheckEmpty() == 0) return 0;//如果为空，则直接返回。
            //正则表达式验证（忽略大小写）
            Regex objRegex = new Regex(regularExpress, RegexOptions.IgnoreCase);
            if (!objRegex.IsMatch(this.Text.Trim()))
            {
                this.errorProvider.SetError(this, errorMsg);
                return 0;
            }
            else
            {
                this.errorProvider.SetError(this, string.Empty);//清楚错误提示
                return 1;
            }
        }

        //根据需要把常用的正则表达式验证，写成方法，封装进去
        //正则表达式验证数据合理性
        /// <summary>
        /// 验证正整数
        /// </summary>
        /// <param name="objName">提示的名称</param>
        /// <returns></returns>
        public int IsInteger(string objName)
        {
            return BeginCheckData(@"^[1-9]\d*$", objName + "必须是正整数!");
        }

        /// <summary>
        /// 非零开头的最多带两位小数的数字
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public int IsDecimal(string objName)
        {
            return BeginCheckData(@"^([1-9][0-9]*)+(.[0-9]{1,2})?$", objName + "必须是非零开头的最多带两位小数的数字!");
        }
        /// <summary>
        /// 验证是否为邮箱
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public int IsEmail(string objName)
        {
            return BeginCheckData(@"\W+([-+.]\w+)*@\w+([-.]\w+)*\.\W+([-.]\w+)*", objName + "必须是邮箱!");
        }

    }
}
