using System.Text.RegularExpressions;

namespace Common
{
    /// <summary>
    /// 数据验证类，正则表达式
    /// </summary>
    public class DataValidate
    {
        /// <summary>
        /// 验证正整数
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static bool IsInteger(string txt)
        {
            Regex objRegex=new Regex(@"^[1-9]\d*$");
            return objRegex.IsMatch(txt);
        }
        /// <summary>
        /// 非零开头的最多带两位小数的数字
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static bool IsDecimal(string txt)
        {
            Regex objRegex = new Regex(@"^([1-9][0-9]*)+(.[0-9]{1,2})?$");
            return objRegex.IsMatch(txt);
        }

        /// <summary>
        /// 非零开头的最多带两位小数的数字
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static bool IsDouble(string txt)
        {
            Regex objRegex = new Regex(@"^([1-9][0-9]*)+(.[0-9]{1,2})?$");
            return objRegex.IsMatch(txt);
        }

        /// <summary>
        /// 验证是否为邮箱
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static bool IsEmail(string txt)
        {
            Regex objRegex = new Regex(@"\W+([-+.]\w+)*@\w+([-.]\w+)*\.\W+([-.]\w+)*");
            return objRegex.IsMatch(txt);
        }
        //其他验证后面再加

    }
}
