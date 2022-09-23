using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Domain.Entities
{
    //Project聚合内部成员（烟罩，主要由PM维护）
    public record Module
    {
        public long Id { get; init; }
        public string Name { get; private set; }//分段
        public string Model { get; set; }//型号--> 来自Product聚合，包含Workload

        


        //烟罩和物料清单，聚合内部，一对多关系
        public List<BillOfMaterial> BillOfMaterials { get; set; }



        //制图人员在制图过程中填写
        public string? KeyPoints { get; set; }//烟罩注意事项，提醒生产过程，重点关注，

        private Module(){ }
        public Module(string name)
        {
            Name = name;
        }

        //手动添加特殊物料（线下购买物料，BOM中不体现的物料）
        public void AddSpecialMaterial()
        {
            //PM:Review special material & delivery time
            //BillOfMaterials.Add();

        }

        //关键物料（交货期长，价值高），提醒项目经理
        public string GetKeyMaterial()
        {
            //Design fill module data
            //PM:Review special material & delivery time
            //Material的条件



            return "sssss";
        }

        //生成BOM
        public void CreateBom()
        {

        }
    }
}
