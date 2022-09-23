using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Zack.DomainCommons.Models;

namespace Compass.Domain.Entities
{
    //Customer聚合，聚合根（客户，由PM维护）
    public record Customer : IAggregateRoot
    {
        public long Id { get; init; } 
        public string Name { get;private set; }
        public bool IsVip { get;private set; }//重点关注客户

        private Customer() { }

        public Customer(string name,bool isVip)
        {
            Name=name;
            IsVip=isVip;
        }

        public void ChangeName(string name)
        {
            Name=name;
        }
        public void ChangeVip(bool isVip)
        {
            IsVip=isVip;
        }

    }
}
