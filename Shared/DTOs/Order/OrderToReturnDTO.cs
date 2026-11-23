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
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; } = default!;
        public ICollection<OrderItemDTO> Items { get; set; } = [];
        public string DeliveryMethod { get; set; } = default!;
        public decimal DeliveryCost { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; } = default!;
        public AddressDTO ShipToAddress { get; set; } = default!;
    }
}
