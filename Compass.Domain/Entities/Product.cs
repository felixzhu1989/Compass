using Compass.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zack.DomainCommons.Models;

namespace Compass.Domain.Entities
{
    //Product聚合，聚合根（产品，由PM维护）
    public record Product:IAggregateRoot
    {
        public long Id { get; init; }
        public string Model { get; set; }
        public string Description { get; set; }


        private Product(){ }

        public Product(string model)
        {
            Model = model;
        }



    }
}
