using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compass.Domain.Enums;
using Zack.DomainCommons.Models;

namespace Compass.Domain.Entities
{
    /// <summary>
    /// Material聚合，物料聚合根（物料，主要由采购维护，供PM，设计，生产使用）
    /// </summary>
    public record Material : IAggregateRoot
    {
        public long Id { get; set; }
        public string PartNumber { get;private set; }//物料号,10位数字组成
        
        public string PartName { get;private set; }//物料名称
        public string? Description { get; set; }//描述
        public MaterialUnit Unit { get; private set; }//单位枚举，在配置中指定数据库存储字符串

        //一下两项作为keyMaterial的判断标准（1）长交货期（2）高价值物料
        public LeadTime LeadTime { get; set; }//值对象，在配置中指定ownsOne
        public MaterialPrice MaterialPrice { get; set; }//值对象，在配置中指定ownsOne

        public string ImageUrl { get; set; }//图片地址

        private  Material() { }
        public Material(string partNumber)
        {
            PartNumber=partNumber;
        }
        //修改物料号
        public void ChangePartNumber(string partNumber)
        {
            PartNumber = partNumber;
        }
        //修改物单位
        public void ChangeMaterialUnit(MaterialUnit unit)
        {
            Unit = unit;
        }


    }
}
