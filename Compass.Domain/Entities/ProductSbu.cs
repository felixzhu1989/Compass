using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Domain.Entities
{
    //Product聚合，
    public record ProductSbu
    {
        public long Id { get; init; }   
        public string Sbu { get; private set; }
        //聚合内部，一对多
        public List<ProductType> ProductTypes { get; set; }

        private ProductSbu() { }
        public ProductSbu(string sbu)
        {
            Sbu=sbu;
        }
    }
}
