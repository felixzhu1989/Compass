using System;
using System.Collections.Generic;
using System.Linq;
namespace Compass.Shared.Const
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public enum Role_e
    {
        Viewer,
        Admin,
        ProjectManager,
        Designer,
        Plan,
        Production,
        Quality,
        Purchase,
        Warehouse,
        Logistic,
        Sales,
    }
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
        Suspend
    }

    public enum Abnormal_e
    {
        客服图纸问题,
        客服图纸延误,
        销售合同变更,
        销售订单拆分,
        销售要求暂停,
        技术方案变更,
        技术图纸问题,
        技术图纸延误,
        物料清单问题,
        物料清单延误,
        技术要求暂停,
        物料质量问题,
        物料库存缺料,
        生产质量问题,
        产线物料丢失,
        生产设备问题,
        生产产能问题,
        FAT整改,
        发货前货物损坏,
        收款问题,
        包装问题,
        运输问题,
    }




    /// <summary>
    /// 订单类型
    /// </summary>
    public enum ProductType_e
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
        其他,
        国内,
        华为,
        港澳台,
        日本,
        韩国,
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
