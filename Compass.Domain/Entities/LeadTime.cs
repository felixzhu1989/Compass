using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compass.Domain.Enums;

namespace Compass.Domain.Entities
{
    //Material聚合，值对象（交货期，主要由采购维护）
    public record LeadTime(double Lead, LeadTimeUnit Unit);
}
