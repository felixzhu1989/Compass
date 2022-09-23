using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Domain.Entities
{
    public record ProductType
    {
        public long Id { get; init; }
        public string Type { get; private set; }
        //聚合内部，一对多
        public List<Product> Products { get; set; }

        private ProductType() { }

        public ProductType(string type)
        {
            Type = type;
        }


    }
}
