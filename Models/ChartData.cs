﻿using System;

namespace Models
{

    /// <summary>
    /// Chart数据实体类
    /// </summary>
    [Serializable]
    public class ChartData
    {
        /// <summary>
        /// 无参数的构造函数
        /// </summary>
        public ChartData()
        {
        }
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public ChartData(string text, double value)
        {
            Text = text;
            Value = value;
        }

        /// <summary>
        /// 显示的文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 显示的值
        /// </summary>
        public double Value { get; set; }
    }
}
