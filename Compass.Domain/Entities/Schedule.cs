using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Domain.Entities
{
    //Project聚合,值对象（计划，主要由PM维护）
    public record Schedule(DateTime StartTime, DateTime EndTime);
}
