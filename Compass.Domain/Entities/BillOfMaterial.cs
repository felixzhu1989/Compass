using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Domain.Entities
{
    //Project聚合内部成员（烟罩BOM，主要由设计维护）,有可能不在Peoject聚合内
    public record BillOfMaterial
    {
        public long Id { get; set; }    
        //聚合间引用聚合根的标识符
        public string MaterialId { get; set; }
        public double Quantity { get; set; }//物料数量（根据物料的单位，有可能物料是小数）
    }
}
