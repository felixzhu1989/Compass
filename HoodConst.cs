using System;

namespace Compass.Const
{
    /// <summary>
    /// 烟罩大侧板类型
    /// </summary>
    public enum HoodSidePanel_e
    {
        middle,
        left,
        right,        
        both
    }
    /// <summary>
    /// 烟罩灯具类型
    /// </summary>
    public enum HoodLightType_e
    {
        fsLong,
        fsShort,        
        maLong,
        maShort,
        led60,
        led140,
    }
    /// <summary>
    /// 排风腔油塞还是油盒
    /// </summary>
    public enum HoodOutlet_e
    {
        leftTap,
        rightTap,
        vessel
    }
    /// <summary>
    /// 烟罩集水翻边
    /// </summary>
    public enum HoodWaterCollection_e
    {
        no,
        yes
    }
    /// <summary>
    /// 烟罩背靠背
    /// </summary>
    public enum HoodBackToBack_e
    {
        no,
        yes
    }

}
