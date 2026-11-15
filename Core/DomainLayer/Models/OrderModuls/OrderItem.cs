using DomainLayer.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModuls
{
    public class OrderItem:BaseEntity<int>
    {

        public ProductItemOrdered Product { get; set; } = default!;

        public decimal Price { get; set; }
        public int Quantity {  get; set; }
    }
}
