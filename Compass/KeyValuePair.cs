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
        private readonly Dictionary<string, string> _projectStatusName = new Dictionary<string, string>()
        {
            {"GettingODP", "收到ODP"},
            {"KickOff", "开工会议"},
            {"DrawingMaking", "制图中"},
            {"InProduction", "生产中"},
            {"ProductCompleted", "生产完成"},
            {"ProjectCompleted", "项目完成"},
            {"FollowUp", "跟踪"},
            {"Pending", "未决定"},
            {"Cancel", "取消"},
            {"Import", "引进"}
        };
        public Dictionary<string, string> ProjectStatusNameKeyValue => _projectStatusName;
        /// <summary>
        /// 烟罩类型
        /// </summary>
        private readonly Dictionary<string, string> _hoodType = new Dictionary<string, string>()
        {
            {"Hood", "烟罩"},
            {"Ceiling", "天花"},
            {"Marine", "海工"}
        };
        public Dictionary<string, string> HoodTypeKeyValue => _hoodType;

        #endregion 中英文键值对
        
        
        
        
        #region 颜色标识键值对


        /*  1	GettingODP	收到ODP     
            2	KickOff	开工会议      
            3	DrawingMaking	制图中       
            4	InProduction	生产中       
            5	ProductCompleted	生产完成      
            6	ProjectCompleted	项目完成      
            7	FollowUp	跟踪        
            8	Pending	未决定       
            9	Cancel	取消        
            10	Import	引进        
         */
        /// <summary>
        /// 项目状态Id-中文
        /// </summary>
        private readonly Dictionary<string, string> _projectStatusIdCn = new Dictionary<string, string>()
        {
            {"1", "收到ODP"},
            {"2", "开工会议"},
            {"3", "制图中"},
            {"4", "生产中"},
            {"5", "生产完成"},
            {"6", "项目完成"},
            {"7", "跟踪"},
            {"8", "未决定"},
            {"9", "取消"},
            {"10","引进"}
        };
        public Dictionary<string, string> ProjectStatusIdCnKeyValue => _projectStatusIdCn;
        /// <summary>
        /// 项目状态中文-颜色
        /// </summary>
        private readonly Dictionary<string, Color> _projectStatusCnColor = new Dictionary<string, Color>()
        {
            {"收到ODP", Color.LightSkyBlue},
            {"开工会议", Color.DeepSkyBlue},
            {"制图中", Color.Yellow},
            {"生产中", Color.Orange},
            {"生产完成", Color.GreenYellow},
            {"项目完成", Color.LimeGreen},
            {"跟踪", Color.LightPink},
            {"未决定", Color.Red},
            {"取消", Color.Silver},
            {"引进", Color.Magenta}
        };
        public Dictionary<string, Color> ProjectStatusCnColorKeyValue => _projectStatusCnColor;
        /// <summary>
        /// 项目状态颜色
        /// </summary>
        private readonly Dictionary<string, Color> _projectStatusColor = new Dictionary<string, Color>()
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
        public Dictionary<string, Color> ProjectStatusColorKeyValue => _projectStatusColor;
        /// <summary>
        /// 项目状态中文颜色
        /// </summary>
        private readonly Dictionary<string, Color> _projectStatusChineseColor = new Dictionary<string, Color>()
        {
            {"收到ODP", Color.LightSkyBlue},
            {"开工会议",Color.DeepSkyBlue},
            {"制图中", Color.Yellow},
            {"生产中", Color.Orange},
            {"生产完成", Color.GreenYellow},
            {"项目完成", Color.LimeGreen},
            {"跟踪", Color.LightPink},
            {"未决定", Color.Red},
            {"取消",Color.Silver},
            {"引进", Color.Magenta}
        };





        public Dictionary<string, Color> ProjectStatusChineseColorKeyValue => _projectStatusChineseColor;
        /// <summary>
        /// 风险等级颜色
        /// </summary>
        private readonly Dictionary<string, Color> _rislLevelColor = new Dictionary<string, Color>()
        {
            {"风险等级-1", Color.Yellow},
            {"风险等级-2", Color.Orange},
            {"风险等级-3", Color.DeepSkyBlue},
            {"风险等级-4", Color.LimeGreen},
        };
        public Dictionary<string, Color> RislLevelColorKeyValue => _rislLevelColor;

        /*
        1	国内项目
        3	港澳台项目
        4	日本项目
        5	韩国项目
        7	华为项目
        8	中东项目
        9	其他
         */

        /// <summary>
        /// 项目类型Id-中文
        /// </summary>
        private readonly Dictionary<string, string> _projectTypeIdCn = new Dictionary<string, string>()
        {
            {"1", "国内项目"},
            {"3", "港澳台项目"},
            {"4", "日本项目"},
            {"5", "韩国项目"},
            {"7", "华为项目"},
            {"8", "中东项目"},
            {"9", "其他"}
            
        };
        public Dictionary<string, string> ProjectTypeIdCnKeyValue => _projectTypeIdCn;
        /// <summary>
        /// 项目类型颜色
        /// </summary>
        private readonly Dictionary<string, Color> _projectTypeColor = new Dictionary<string, Color>()
        {
            {"国内项目", Color.LimeGreen},
            {"港澳台项目", Color.LightPink},
            {"日本项目", Color.Orange},
            {"韩国项目", Color.Salmon},
            {"华为项目", Color.DeepSkyBlue},
            {"中东项目", Color.LightYellow},
            {"其他", Color.Silver}
        };
        public Dictionary<string, Color> ProjectTypeColorKeyValue => _projectTypeColor;

        /// <summary>
        /// 项目状态颜色
        /// </summary>
        private readonly Dictionary<string, Color> _userColor = new Dictionary<string, Color>()
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
        public Dictionary<string, Color> UserColorKeyValue => _userColor;




        #endregion 颜色标识键值对


        private readonly Dictionary<int, Color> _defaultColor = new Dictionary<int, Color>()
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
        public Dictionary<int, Color> DefaultColorKeyValue => _defaultColor;
    }
}
