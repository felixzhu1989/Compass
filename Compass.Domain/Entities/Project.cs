using Zack.DomainCommons.Models;

namespace Compass.Domain.Entities
{
    //Project聚合，项目聚合根（项目，主要由PM维护）
    public record Project : IAggregateRoot
    {
        public long Id { get; init; }
        public DateTime CreatedTime { get; init; }//订单创建时间

        public string ProjectName { get; private set; }//必须有项目名
        
        //聚合间引用聚合根的标识符
        public string? UserId { get; private set; }//必须指定负责人Owner
        public string? CustomerId { get; set; }

        public string? OdpNumber { get; private set; }//ODP号（生成订单目录）
        public string? QuoteNumber { get; set; }//报价号（查询用）
        public string? ProdNumber { get; set; }//生产号（打印用）

        //PM:Special Request (Design:feasibility check)
        public string? SpecialRequests { get; set; }//订单级别的特殊要求（通常来自客户，客服，项目经理），全过程重点关注事项



        public string? Remark { get; set; }

        //聚合内引用实体
        //订单与图纸是一对多的关系,PM:Project Scope
        public List<Drawing> Drawings { get; set; } = new List<Drawing>();
        //Status





        private Project() { }
        public Project(string projectName,string userId)
        {
            ProjectName=projectName;
            UserId=userId;
            CreatedTime  = DateTime.Now;
        }
        
        public void ChangeName(string projectName)
        {
            ProjectName=projectName;
        }

        public void ChangeUser(string userId)
        {
            UserId=userId;
        }

        public void ChangeOdpNumber(string odpNumber)
        {
            OdpNumber = odpNumber;
        }

        //根据Item计算得出总item数，实时计算，不用存储，只展示在界面上
        public int GetTotalItem()
        {
            return Drawings.Count;
        }
        //返回总的烟罩数量，实时计算，不用存储
        public int GetTotalModules()
        {
            int totalModule = 0;
            foreach (var drw in Drawings)
            {
                totalModule += drw.GetTotalModules();
            }
            return totalModule;
        }
        //根据图纸中的每个烟罩计算得出，实时计算，不用存储
        public double GetTotalWorkLoad()
        {



            return 5d;
        }

        //烟罩概况
        public string GetModuleSummary()
        {



            return "Kvi5,UVi5";
        }

        //获取所有特殊采购件,PM:Review special material & delivery time
        public string GetAllSpecialMaterial()
        {



            return "SpecialMaterial";
        }

        //获取所有关键采购件,PM:Review special material & delivery time
        public string GetAllKeyMaterial()
        {



            return "KeyMaterial";
        }
       



        //获取烟罩注意事项
        public string GetAllKeyPoints()
        {
            return "KeyPoints";
        }
    }
}
