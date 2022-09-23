using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compass.Domain.Enums;

namespace Compass.Domain.Entities
{
    //Material聚合，值对象（物料价格，主要由采购维护）
    public record MaterialPrice(double Price, PriceUnit Unit);
}
