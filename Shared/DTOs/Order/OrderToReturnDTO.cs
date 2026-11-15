using Shared.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Order
{
    public class OrderToReturnDTO
    {
        public Guid id { get; set; }
        public string UserEmail { get; set; } = default!;
       

        public ICollection<OrderItemDTO>? Items { get; set; } = [];
        public string DeliveryMethod { get; set; } = default!;
        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string OrderStatus { get; set; } = default!;

        public AddressDTO Address { get; set; } = default!;
    }
}
