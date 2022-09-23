using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Domain.Entities
{
    //Project聚合内部成员（图纸，主要由PM维护）
    public record Drawing
    {
        public long Id { get; init; }
        //聚合间引用聚合根的标识符
        public string? UserId { get; private set; }//指定制图人(Role进行分组)

        public string Item { get; private set; }//Item编号
        public string? Location { get; set; }//Location区域

        //指定EndTime，根据TotalWorkload，反推StartTime,SuggestStart
        public Schedule DrwSchedule { get; set; }//针对Item图纸的计划，



        //聚合内引用实体
        //订单与图纸是一对多的关系
        public Project Project { get; set; }
        //图纸与烟罩是一对多的关系
        public List<Module> Modules { get; set; } = new List<Module>();




        private Drawing() { }

        public Drawing(string item, string userId)
        {
            Item = item;
            UserId=userId;
            
        }

        public void ChangeName(string item)
        {
            Item = item;
            
        }


        public int GetTotalModules()
        {
            return Modules.Count;
        }

        public double GetTotalWorkload()
        {
            return 5d;
        }

    }
}
