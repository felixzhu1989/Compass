using System;
using System.Collections.Generic;
using System.Linq;
namespace Compass.Shared.Const
{
    /// <summary>
    /// 项目状态
    /// </summary>
    public enum ProjectStatus_e
    {
        NoStatus,
        CsDrwMaking,
        CsDrwChecking,
        CsDrwApproved,
        GettingOdp,
        KickOff,
        DrwMaking,
        DrwReleased,
        InProduction,
        ProdCompleted,
        ProjCompleted,        
        Cancel,        
        Suspend,
        Abnormal
    }
    
    /// <summary>
    /// 订单类型
    /// </summary>
    public enum ProjectType_e
    {        
        烟罩,
        天花,
        海工,
        KFC,
        配件,
        售后,
        样品,
        半成品,
        其他
    }
    /// <summary>
    /// 客户分类
    /// </summary>
    public enum CustomerType_e
    {
        国内,
        其他,
        港澳台,
        日本,
        韩国,
        华为,
        中东,
    }
    /// <summary>
    /// 电制
    /// </summary>
    public enum Electricity_e
    {
        AC230V50Hz,
        AC230V60Hz,
        AC120V50Hz,
        AC120V60Hz,
        Special
    }
    /// <summary>
    /// MARVEL选项
    /// </summary>
    public enum MarvelOption_e
    {
        No,
        Yes
    }
    /// <summary>
    /// ANSUL预埋
    /// </summary>
    public enum AnsulPrePipe_e
    {
        No,
        R102,
        Piranha
    }
    /// <summary>
    /// ANSUL系统
    /// </summary>
    public enum AnsulSystem_e
    {
        No,
        R102,
        Piranha
    }
    /// <summary>
    /// 工序活动
    /// </summary>
    public enum Activity_e
    {
        Drawing,
        Nesting,
        Cutting,
        Bending,
        Collect,
        Welding,
        Polishing,
        Assembly,        
        Electrical,
        Piping,
        Ansul,
        Quality,
        Packing
    }
}
