using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Dictionary<string, string> projectStatusName = new Dictionary<string, string>()
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
        public Dictionary<string, string> ProjectStatusNameKeyValue
        {
            get
            {
                return this.projectStatusName;
            }
        }
        /// <summary>
        /// 烟罩类型
        /// </summary>
        private Dictionary<string, string> hoodType = new Dictionary<string, string>()
        {
            {"Hood", "烟罩/Hood"},
            {"Ceiling", "天花/Ceiling"},
        };
        public Dictionary<string, string> HoodTypeKeyValue
        {
            get
            {
                return this.hoodType;
            }
        }





        #endregion 中英文键值对

        #region 颜色标识键值对
        /// <summary>
        /// 项目状态颜色
        /// </summary>
        private Dictionary<string, Color> projectStatusColor = new Dictionary<string, Color>()
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
        public Dictionary<string, Color> ProjectStatusColorKeyValue
        {
            get
            {
                return this.projectStatusColor;
            }
        }
        /// <summary>
        /// 项目状态颜色
        /// </summary>
        private Dictionary<string, Color> projectStatusChineseColor = new Dictionary<string, Color>()
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
        public Dictionary<string, Color> ProjectStatusChineseColorKeyValue
        {
            get
            {
                return this.projectStatusChineseColor;
            }
        }
        /// <summary>
        /// 风险等级颜色
        /// </summary>
        private Dictionary<string, Color> rislLevelColor = new Dictionary<string, Color>()
        {
            {"风险等级-1", Color.Yellow},
            {"风险等级-2", Color.Orange},
            {"风险等级-3", Color.DeepSkyBlue},
            {"风险等级-4", Color.LimeGreen},
        };
        public Dictionary<string, Color> RislLevelColorKeyValue
        {
            get
            {
                return this.rislLevelColor;
            }
        }

        /// <summary>
        /// 项目类型颜色
        /// </summary>
        private Dictionary<string, Color> projectTypeColor = new Dictionary<string, Color>()
        {
            {"国内项目", Color.LimeGreen},
            {"港澳台项目", Color.LightPink},
            {"日本项目", Color.Orange},
            {"华为项目", Color.DeepSkyBlue},
            {"韩国项目", Color.Salmon},
            {"其他", Color.Silver}
        };
        public Dictionary<string, Color> ProjectTypeColorKeyValue
        {
            get
            {
                return this.projectTypeColor;
            }
        }

        #endregion 颜色标识键值对



    }
}
