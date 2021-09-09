using System.Collections.Generic;
using System.Drawing;

namespace Compass
{
    /// <summary>
    /// 键值对
    /// </summary>
    public class KeyValuePair
    {
        #region 中英文键值对
        /// <summary>
        /// 项目状态名称
        /// </summary>
        private readonly Dictionary<string, string> projectStatusName = new Dictionary<string, string>()
        {
            {"GettingODP", "收到ODP/GettingODP"},
            {"KickOff", "开工会议/KickOff"},
            {"DrawingMaking", "制图中.../DrawingMaking"},
            {"InProduction", "生产中.../InProduction"},
            {"ProductCompleted", "生产完成/ProductCompleted"},
            {"ProjectCompleted", "项目完成/ProjectCompleted"},
            {"FollowUp", "跟踪/FollowUp"},
            {"Pending", "未决定/Pending"},
            {"Cancel", "取消/Cancel"},
            {"Import", "引进/Import"}
        };
        public Dictionary<string, string> ProjectStatusNameKeyValue => this.projectStatusName;
        /// <summary>
        /// 烟罩类型
        /// </summary>
        private readonly Dictionary<string, string> hoodType = new Dictionary<string, string>()
        {
            {"Hood", "烟罩/Hood"},
            {"Ceiling", "天花/Ceiling"},
        };
        public Dictionary<string, string> HoodTypeKeyValue => this.hoodType;





        #endregion 中英文键值对

        #region 颜色标识键值对



        /// <summary>
        /// 项目状态颜色
        /// </summary>
        private readonly Dictionary<string, Color> projectStatusColor = new Dictionary<string, Color>()
        {
            {"GettingODP", Color.LightSkyBlue},
            {"KickOff",Color.DeepSkyBlue},
            {"DrawingMaking", Color.Yellow},
            {"InProduction", Color.Orange},
            {"ProductCompleted", Color.GreenYellow},
            {"ProjectCompleted", Color.LimeGreen},
            {"FollowUp", Color.LightPink},
            {"Pending", Color.Red},
            {"Cancel",Color.Silver},
            {"Import", Color.Magenta}
        };
        public Dictionary<string, Color> ProjectStatusColorKeyValue => this.projectStatusColor;
        /// <summary>
        /// 项目状态颜色
        /// </summary>
        private readonly Dictionary<string, Color> projectStatusChineseColor = new Dictionary<string, Color>()
        {
            {"收到ODP/GettingODP", Color.LightSkyBlue},
            {"开工会议/KickOff",Color.DeepSkyBlue},
            {"制图中.../DrawingMaking", Color.Yellow},
            {"生产中.../InProduction", Color.Orange},
            {"生产完成/ProductCompleted", Color.GreenYellow},
            {"项目完成/ProjectCompleted", Color.LimeGreen},
            {"跟踪/FollowUp", Color.LightPink},
            {"未决定/Pending", Color.Red},
            {"取消/Cancel",Color.Silver},
            {"引进/Import", Color.Magenta}
        };
        public Dictionary<string, Color> ProjectStatusChineseColorKeyValue => this.projectStatusChineseColor;
        /// <summary>
        /// 风险等级颜色
        /// </summary>
        private readonly Dictionary<string, Color> rislLevelColor = new Dictionary<string, Color>()
        {
            {"风险等级-1", Color.Yellow},
            {"风险等级-2", Color.Orange},
            {"风险等级-3", Color.DeepSkyBlue},
            {"风险等级-4", Color.LimeGreen},
        };
        public Dictionary<string, Color> RislLevelColorKeyValue => this.rislLevelColor;

        /// <summary>
        /// 项目类型颜色
        /// </summary>
        private readonly Dictionary<string, Color> projectTypeColor = new Dictionary<string, Color>()
        {
            {"国内项目", Color.LimeGreen},
            {"港澳台项目", Color.LightPink},
            {"日本项目", Color.Orange},
            {"华为项目", Color.DeepSkyBlue},
            {"韩国项目", Color.Salmon},
            {"其他", Color.Silver}
        };
        public Dictionary<string, Color> ProjectTypeColorKeyValue => this.projectTypeColor;

        /// <summary>
        /// 项目状态颜色
        /// </summary>
        private readonly Dictionary<string, Color> userColor = new Dictionary<string, Color>()
        {
            {"jeff", Color.LightSkyBlue},
            {"july",Color.DeepSkyBlue},
            {"leo", Color.Yellow},
            {"eric", Color.Orange},
            {"felix", Color.GreenYellow},
            {"jack", Color.LimeGreen},
            {"hujian", Color.LightPink},
            {"james", Color.Red},
            {"admin",Color.Silver},
            {"sky",Color.Silver},
            {"cart", Color.Magenta}
        };
        public Dictionary<string, Color> UserColorKeyValue => this.userColor;




        #endregion 颜色标识键值对


        private readonly Dictionary<int, Color> defaultColor = new Dictionary<int, Color>()
        {
            {0,Color.DeepSkyBlue},
            {1, Color.Yellow},
            {2, Color.LightSkyBlue},
            {3, Color.Orange},
            {4, Color.GreenYellow},
            {5, Color.LightPink},
            {6, Color.LimeGreen},
            {7, Color.Red},
            {8,Color.Aquamarine},
            {9,Color.DarkKhaki},
            {10,Color.Cyan},
            {11,Color.LightCoral},
            {12,Color.DarkSeaGreen},
            {13,Color.Gold},
            {14,Color.OliveDrab},
            {15,Color.HotPink},
            {16,Color.PaleVioletRed},
            {17,Color.PowderBlue},
            {18,Color.Silver},
            {19, Color.Magenta},
            {20, Color.DarkTurquoise},
            {21, Color.DarkSalmon},
            {22, Color.DarkOliveGreen},
            {23, Color.IndianRed},
            {24, Color.DarkSeaGreen},
            {25, Color.Firebrick},
            {26, Color.LightSteelBlue},
            {27, Color.DarkGoldenrod},
            {28, Color.DarkTurquoise},
            {29, Color.Moccasin},
            {30, Color.DarkSlateGray},
            {31, Color.Brown},
            {32, Color.CornflowerBlue},
            {33, Color.Khaki},
            {34, Color.MediumAquamarine},
            {35, Color.Goldenrod},
            {36, Color.MediumTurquoise},
            {37, Color.MediumOrchid},
            {38, Color.MediumSpringGreen},
            {39, Color.MistyRose},
            {40, Color.MediumSlateBlue},

        };
        public Dictionary<int, Color> DefaultColorKeyValue => this.defaultColor;
    }
}
